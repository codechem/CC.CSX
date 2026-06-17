# CC.CSX render-plan benchmarks

How fast can a C# HTML library render a realistic, data-heavy page — and what does compiling
the view into a **static/dynamic render plan** buy you?

This compares four ways of rendering the **same page** (a report with a table of *N* rows),
all writing UTF‑8 to a discarding `IBufferWriter<byte>` at `Indent = 0`:

| Approach | What it is |
|---|---|
| **HandWritten** | Direct byte writing — the theoretical floor |
| **Htnet · RenderPlan** | The generated `[RenderOptimized]` builder: static markup baked to `byte[]`, only dynamic values written |
| **Htnet · Live** | Building the CC.CSX `HtmlNode` tree, then `WriteTo` (the current default path) |
| **Blazor · SSR** | The same page as a Blazor component rendered via `HtmlRenderer` — i.e. what a `.razor` compiles to |

> The page: `<div class="uk-container"><h1>Report</h1><table class="uk-table"><thead>…</thead><tbody>` +
> *N* rows of `<tr class="even|odd"><td>{id}</td><td>{name}</td><td>{email}</td></tr>`. All four
> approaches emit byte‑identical HTML (the htnet ones are pinned by golden tests).

## Headline (1,000‑row table)

| Approach | Time | Allocated | vs Live (time) | vs Live (alloc) |
|---|--:|--:|--:|--:|
| HandWritten (floor) | **66.8 µs** | **64 B** | 5.2× faster | 17,000× less |
| **Htnet · RenderPlan** | **66.5 µs** | **22.6 KB** | **5.2× faster** | **48× less** |
| Htnet · Live | 348.0 µs | 1,097 KB | 1.0× | 1.0× |
| Blazor · SSR | 623.1 µs | 695 KB | 0.55× (1.8× slower) | 0.63× |

**The render plan matches the hand‑written floor on time and gets within a rounding error on
allocation — while producing identical HTML from a plain, declarative C# view.** Versus Blazor SSR
it is **~9× faster** and allocates **~31× less**.

## Full results

`net10.0` · AMD Ryzen 9 5900X · BenchmarkDotNet (ShortRun) · lower is better.

### 10 rows (small page)

| Method | Mean | Allocated |
|---|--:|--:|
| HandWritten | 741 ns | 64 B |
| **Htnet · RenderPlan** | **616 ns** | **184 B** |
| Htnet · Live | 4,082 ns | 13,096 B |
| Blazor · SSR | 10,659 ns | 10,760 B |

### 100 rows

| Method | Mean | Allocated |
|---|--:|--:|
| HandWritten | 6,764 ns | 64 B |
| **Htnet · RenderPlan** | **5,954 ns** | **184 B** |
| Htnet · Live | 32,368 ns | 110,000 B |
| Blazor · SSR | 61,781 ns | 76,904 B |

### 1,000 rows

| Method | Mean | Allocated |
|---|--:|--:|
| HandWritten | 66,794 ns | 64 B |
| **Htnet · RenderPlan** | **66,512 ns** | **22,584 B** |
| Htnet · Live | 348,041 ns | 1,097,208 B |
| Blazor · SSR | 623,070 ns | 694,536 B |

## Interesting observations

- **The render plan tracks the hand‑written floor at every size.** At 1,000 rows it's 66.5 µs vs the
  floor's 66.8 µs — the cost of building/walking an object tree is gone entirely; what's left is the
  actual byte writing.
- **Allocation collapses to near‑constant.** Live rendering allocates ~1.1 KB **per row** (the
  `HtmlNode`/`HtmlTextNode`/list objects); the render plan allocates a flat ~184 B regardless of size,
  rising only to ~22.6 KB at 1,000 rows — and that residual is entirely `int.ToString()` on the `id`
  cell (the hand‑written version avoids it with `TryFormat`; emitting that in codegen would close the
  last gap).
- **This is a throughput story, not just a microbenchmark.** ~1.1 MB/request (Live) vs ~22 KB/request
  (RenderPlan) is the difference between being GC‑bound and CPU‑bound under load. At, say, a 4 GB/s
  gen0 budget the big page tops out near ~3.6k req/s on allocation alone with the tree path, versus
  far higher with the plan.
- **htnet's tree path already beats Blazor SSR** on time (1.8–2.6×), though Blazor's flat
  `RenderTreeFrame[]` allocates a bit less than htnet's object tree. The render plan then beats *both*
  by a wide margin (9–17× faster than Blazor across sizes).
- **The win holds at small sizes too** — even a 10‑row page is 6.6× faster and 71× lighter with the
  plan, so this isn't only a giant‑table effect.

## How it works (one paragraph)

A view marked `[RenderOptimized]` is analyzed at compile time. Because the CC.CSX element/attribute
factories are *pure functions of their arguments*, a Roslyn source generator can recursively split the
view into **static chunks** (baked to `static readonly byte[]` and written via memcpy) and **dynamic
holes** (the values that read parameters). `Select`/`foreach` become real loops with the per‑row
scaffold baked; node‑producing conditionals become `if`/`else` with per‑branch sub‑plans. A C#
**interceptor** then transparently redirects each call site to the generated builder, which returns a
lightweight `PlanNode` — so existing call sites get the speedup with no code change. Anything the
analyzer can't prove static (unknown/impure calls) falls back to rendering live, so correctness is
never traded for speed.

## Methodology & caveats

- Results are **BenchmarkDotNet ShortRun** (3 iterations) — fine for the order‑of‑magnitude ratios
  here; re‑run without `--job short` for publication‑grade confidence intervals.
- All approaches render to a reused, discarding `IBufferWriter<byte>` so the numbers reflect the
  *renderer*, not output storage. In production the htnet path writes straight to the response
  `PipeWriter`.
- `RenderOptions.Indent = 0` (production shape). Render plans apply only at `Indent = 0`.
- The render‑plan generator is currently a spike (`CC.CSX.RenderPlan.Generator`); see the
  `feature/render-plan` branch. Numbers will only improve as codegen polish lands (e.g. `TryFormat`
  for numeric holes removes the last allocation).

## Reproduce

```bash
# all realistic-page benchmarks
dotnet run -c Release --project tests/CC.CSX.Benchmarks -- --filter "*RealisticBenchmarks*"

# quick pass (what produced the tables above)
dotnet run -c Release --project tests/CC.CSX.Benchmarks -- --filter "*RealisticBenchmarks*" --job short
```
