# htnet vs Blazor — production SSR comparison

A production-grade, apples-to-apples comparison of the two **optimized** server-side rendering paths
for the same page (a report with a table of *N* rows):

- **htnet (optimized)** — a `[RenderOptimized]` view compiled to a render plan: static markup baked to
  `byte[]` and written by memcpy, only the dynamic values written per request.
- **Blazor (optimized)** — the same page as a Blazor component rendered via `HtmlRenderer.WriteHtmlTo`
  (streams to a `TextWriter`, no output string) — Blazor's fastest SSR configuration.

A **hand-written** byte writer is included as the theoretical floor. The unoptimized "build a tree
then render" path is intentionally excluded — this is a best-vs-best, production comparison.

> The page: `<div class="uk-container"><h1>Report</h1><table class="uk-table">…<tbody>` +
> *N* rows of `<tr class="even|odd"><td>{id}</td><td>{name}</td><td>{email}</td></tr>`. Both engines
> emit identical HTML.
>
> Full multi-way numbers (including the unoptimized path and Blazor's `AddMarkupContent` variant):
> see [`render-plan-benchmarks.md`](./render-plan-benchmarks.md).

## Conclusions

**In production-grade configuration, htnet's render plan is roughly 7–14× faster than the
best-optimized Blazor SSR, and uses on the order of 17–240× less memory per render.** In plain terms:

- **Big table (1,000 rows).** htnet renders it in **~66 µs using ~23 KB**; optimized Blazor takes
  **~471 µs using ~376 KB**. So htnet is about **7× faster** and uses about **17× less memory**.
- **Medium page (100 rows).** htnet: **~6 µs / ~0.2 KB**. Optimized Blazor: **~49 µs / ~44 KB**.
  About **8× faster** and **~240× less memory** — htnet's allocation is essentially flat; it doesn't
  grow with the row count the way Blazor's does.
- **Small page (10 rows).** htnet: **~0.6 µs / ~0.2 KB**. Optimized Blazor: **~8.6 µs / ~6.3 KB**.
  About **14× faster** and **~34× less memory** — the advantage is there even on tiny pages, not just
  big tables.

**Why it matters in production:** htnet's per-render allocation stays near-constant (~0.2 KB, rising
only to ~23 KB on the biggest page), while Blazor's grows with page size (44 KB → 376 KB). Under load
that's the difference between being CPU-bound and being GC-bound — fewer/cheaper garbage collections,
flatter tail latencies, and a higher requests-per-second ceiling on the same hardware.

**And it tracks the hand-written floor.** The render plan renders in essentially the same time as a
hand-written byte writer (66 µs vs 67 µs at 1,000 rows) — there's effectively no overhead left to
remove — while you still write plain, declarative C# views.

## Numbers

`net10.0` · AMD Ryzen 9 5900X · BenchmarkDotNet (ShortRun) · lower is better.
"htnet advantage" = optimized Blazor ÷ htnet.

| Rows | htnet (optimized) | Blazor (optimized) | hand-written (floor) | htnet advantage |
|---:|---|---|---|---|
| 10 | **0.61 µs · 184 B** | 8.56 µs · 6,288 B | 0.72 µs · 64 B | **14× faster · 34× less mem** |
| 100 | **6.13 µs · 184 B** | 48.78 µs · 44,288 B | 6.67 µs · 64 B | **8× faster · 240× less mem** |
| 1000 | **66.0 µs · 22,584 B** | 470.6 µs · 376,002 B | 67.1 µs · 64 B | **7× faster · 17× less mem** |

"Blazor (optimized)" is `HtmlRenderer.WriteHtmlTo(TextWriter)` — Blazor's fastest SSR path. (Typical
Blazor via `.ToHtmlString()` is slower still: ~598 µs / 695 KB at 1,000 rows. Blazor's
`AddMarkupContent` "raw markup" technique did **not** help — see the full report.)

## How htnet's render plan works (one paragraph)

A view marked `[RenderOptimized]` is analyzed at compile time. Because the CC.CSX element/attribute
factories are *pure functions of their arguments*, a Roslyn source generator recursively splits the
view into **static chunks** (baked to `static readonly byte[]`, written via memcpy) and **dynamic
holes** (the values that read parameters). `Select`/`foreach` become real loops with the per-row
scaffold baked; node-producing conditionals become `if`/`else` with per-branch sub-plans. A C#
**interceptor** transparently redirects each call site to the generated builder — existing call sites
get the speedup with no code change. Anything not provably static falls back to rendering live, so
correctness is never traded for speed. Blazor, by contrast, always builds and walks a
`RenderTreeFrame[]` at render time — there is no SSR path that skips the tree the way a compiled plan
does, which is why even optimized Blazor stays several times behind.

## Methodology & caveats

- **BenchmarkDotNet ShortRun** (3 iterations) — solid for the order-of-magnitude ratios here; re-run
  without `--job short` for publication-grade confidence intervals.
- Both engines render to a discarding output (htnet → discarding `IBufferWriter<byte>`; Blazor →
  `TextWriter.Null`), so the numbers reflect the *renderer*, not output storage. In production htnet
  writes straight to the response `PipeWriter`.
- `RenderOptions.Indent = 0` (production shape). Render plans apply only at `Indent = 0`.
- The render-plan generator is a spike (`CC.CSX.RenderPlan.Generator`, branch `feature/render-plan`).
  The numbers only improve as codegen polish lands — e.g. `TryFormat` for numeric holes removes htnet's
  last remaining allocation (the per-row `int.ToString()`).

## Reproduce

```bash
dotnet run -c Release --project tests/CC.CSX.Benchmarks -- --filter "*RealisticBenchmarks*" --job short
```
