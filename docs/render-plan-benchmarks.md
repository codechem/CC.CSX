# CC.CSX render-plan benchmarks

How fast can a C# HTML library render a realistic, data-heavy page — and what does compiling
the view into a **static/dynamic render plan** buy you, versus building an object tree, versus
Blazor SSR (including Blazor's own optimizations)?

All approaches render the **same page** (a report with a table of *N* rows), writing to a
discarding output at `Indent = 0`:

| Approach | What it is |
|---|---|
| **HandWritten** | Direct byte writing — the theoretical floor |
| **Htnet · RenderPlan** | The generated `[RenderOptimized]` builder: static markup baked to `byte[]`, only dynamic values written |
| **Htnet · Live** | Building the CC.CSX `HtmlNode` tree, then `WriteTo` (the current default path) |
| **Blazor · ToString** | The page as a Blazor component via `HtmlRenderer`, `.ToHtmlString()` — typical SSR |
| **Blazor · WriteTo** | Same component, but `WriteHtmlTo(TextWriter)` — Blazor opt #1 (no output string) |
| **Blazor · Markup+WriteTo** | Static HTML as raw markup (`AddMarkupContent`) + `WriteHtmlTo` — Blazor opt #2 (the hand-tuned analog of baked chunks) |

> The page: `<div class="uk-container"><h1>Report</h1><table class="uk-table"><thead>…</thead><tbody>` +
> *N* rows of `<tr class="even|odd"><td>{id}</td><td>{name}</td><td>{email}</td></tr>`. The htnet
> approaches emit byte-identical HTML (pinned by golden tests).

## Headline (1,000-row table)

| Approach | Time | Allocated | vs Live |
|---|--:|--:|--:|
| HandWritten (floor) | **67.1 µs** | **64 B** | 5.5× faster |
| **Htnet · RenderPlan** | **66.0 µs** | **22.6 KB** | **5.6× faster, 49× less mem** |
| Htnet · Live | 370.9 µs | 1,097 KB | baseline |
| Blazor · ToString | 598.4 µs | 695 KB | 0.62× (1.6× slower) |
| **Blazor · WriteTo** *(best Blazor)* | 470.6 µs | 376 KB | 0.79× (1.3× slower) |
| Blazor · Markup+WriteTo | 612.4 µs | 704 KB | 0.61× |

**The render plan matches the hand-written floor** while producing identical HTML from a plain,
declarative C# view. Versus the **best** Blazor configuration it is **~7× faster** and allocates
**~17× less**.

## Scenario 1 — data table (static-heavy)

`net10.0` · AMD Ryzen 9 5900X · BenchmarkDotNet (ShortRun) · lower is better.

### 10 rows (small page)

| Method | Mean | Allocated |
|---|--:|--:|
| HandWritten | 718 ns | 64 B |
| **Htnet · RenderPlan** | **613 ns** | **184 B** |
| Htnet · Live | 3,905 ns | 13,096 B |
| Blazor · ToString | 10,768 ns | 10,760 B |
| Blazor · WriteTo | 8,561 ns | 6,288 B |
| Blazor · Markup+WriteTo | 9,033 ns | 6,192 B |

### 100 rows

| Method | Mean | Allocated |
|---|--:|--:|
| HandWritten | 6,670 ns | 64 B |
| **Htnet · RenderPlan** | **6,130 ns** | **184 B** |
| Htnet · Live | 32,343 ns | 110,000 B |
| Blazor · ToString | 61,537 ns | 76,904 B |
| Blazor · WriteTo | 48,784 ns | 44,288 B |
| Blazor · Markup+WriteTo | 59,264 ns | 44,192 B |

### 1,000 rows

| Method | Mean | Allocated |
|---|--:|--:|
| HandWritten | 67,147 ns | 64 B |
| **Htnet · RenderPlan** | **65,975 ns** | **22,584 B** |
| Htnet · Live | 370,909 ns | 1,097,208 B |
| Blazor · ToString | 598,432 ns | 694,536 B |
| Blazor · WriteTo | 470,617 ns | 376,002 B |
| Blazor · Markup+WriteTo | 612,449 ns | 703,552 B |

## Scenario 2 — product catalog (dynamic-heavy)

A loop of product cards, each with a conditional CSS class, a computed price string, a **structural
conditional** (the SALE badge → `if`/`else`), and a **nested loop** (tags), plus an inlined
`[HtmlPure]` component. A much higher dynamic:static ratio than the table — a more honest test.

| Method | 10 items | 100 items | 1,000 items |
|---|--:|--:|--:|
| **Htnet · RenderPlan** | **1.32 µs · 504 B** | **12.99 µs · 3,648 B** | **133.8 µs · 39,648 B** |
| Htnet · Live | 7.62 µs · 22,232 B | 71.6 µs · 207,720 B | 779.9 µs · 2,064,129 B |
| Blazor · ToString | 17.1 µs · 19,040 B | 121.1 µs · 146,504 B | 1,042.8 µs · 1,230,811 B |
| Blazor · WriteTo *(best Blazor)* | 13.75 µs · 11,488 B | 94.75 µs · 86,312 B | 704.4 µs · 696,711 B |

The render plan's lead **narrows** vs the static table (the holes now do real work — string
interpolation for price, nested tag lists) but holds: **~5–10× faster and ~18–24× less memory than
the best Blazor**. Note that here optimized Blazor (`WriteTo`) actually edges out htnet's *live tree*
path on time (704 µs vs 780 µs at 1,000 items) — Blazor's flat `RenderTreeFrame[]` handles a deep,
dynamic page well — so the compiled render plan, not the tree, is what keeps htnet ahead in production.

## Interesting observations

- **The render plan tracks the hand-written floor at every size** (66.0 µs vs 67.1 µs at 1,000 rows).
  The cost of building/walking an object tree is gone; what's left is the actual byte writing.
- **Allocation collapses to near-constant.** Live rendering allocates ~1.1 KB **per row**; the plan
  allocates a flat ~184 B regardless of size, rising only to ~22.6 KB at 1,000 rows — and that
  residual is entirely `int.ToString()` on the id cell (the hand-written version avoids it with
  `TryFormat`; emitting that in codegen would close the last gap).
- **Blazor's own optimizations help — partly.** Rendering to a `TextWriter` (`WriteHtmlTo`) instead of
  `ToHtmlString()` is a real win: ~21% faster and ~46% less allocation at 1,000 rows (598→471 µs,
  695→376 KB), because it skips materializing the output string. **Using `AddMarkupContent` for the
  static HTML did *not* help** — it was slightly slower and allocated *more* than the plain element
  component (612 µs / 704 KB vs 471 µs / 376 KB). Blazor's renderer still builds and walks a
  `RenderTreeFrame[]` either way; there's no SSR path that skips the tree the way a compiled plan does.
- **htnet's plain tree path already beats even optimized Blazor on time** (1.3× faster than
  `Blazor · WriteTo`), though optimized Blazor allocates less than htnet's object tree. The render plan
  then beats *everything* — ~7× faster and ~17× less memory than the best Blazor across the table.
- **This is a throughput story.** ~1.1 MB/request (Live) vs ~22 KB/request (RenderPlan) is the
  difference between being GC-bound and CPU-bound under load.
- **The win holds at small sizes too** — a 10-row page is still ~14× faster and ~34× lighter than the
  best Blazor.

## How it works (one paragraph)

A view marked `[RenderOptimized]` is analyzed at compile time. Because the CC.CSX element/attribute
factories are *pure functions of their arguments*, a Roslyn source generator recursively splits the
view into **static chunks** (baked to `static readonly byte[]`, written via memcpy) and **dynamic
holes** (the values that read parameters). `Select`/`foreach` become real loops with the per-row
scaffold baked; node-producing conditionals become `if`/`else` with per-branch sub-plans. A C#
**interceptor** then transparently redirects each call site to the generated builder, which returns a
lightweight `PlanNode` — so existing call sites get the speedup with no code change. Anything the
analyzer can't prove static falls back to rendering live, so correctness is never traded for speed.

## Methodology & caveats

- Results are **BenchmarkDotNet ShortRun** (3 iterations) — fine for the order-of-magnitude ratios
  here; re-run without `--job short` for publication-grade confidence intervals.
- htnet renders UTF-8 bytes to a reused, discarding `IBufferWriter<byte>`; Blazor `WriteTo` variants
  render to `TextWriter.Null`; `ToString` builds the string. So the numbers reflect the *renderer*,
  not output storage. In production the htnet path writes straight to the response `PipeWriter`.
- `RenderOptions.Indent = 0` (production shape). Render plans apply only at `Indent = 0`.
- The render-plan generator is a spike (`CC.CSX.RenderPlan.Generator`, branch `feature/render-plan`).
  Numbers only improve as codegen polish lands (e.g. `TryFormat` for numeric holes removes the last
  allocation).

## Reproduce

```bash
dotnet run -c Release --project tests/CC.CSX.Benchmarks -- --filter "*RealisticBenchmarks*" --job short
```
