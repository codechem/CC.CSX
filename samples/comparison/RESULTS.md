# Pulse — three-way build comparison (results)

Same spec (`SPEC.md`), same style guide (`shared/dashboard.css`), three stacks, one
build agent each (same model, run in parallel). The htnet agent additionally received
`HTNET_GUIDE.md` because the model has no prior knowledge of CC.CSX.

## Metrics

| Metric | Blazor (WASM) | React (Vite) | htnet (CC.CSX.Browser) |
|---|---:|---:|---:|
| **Tokens — total processed** | 290,311 | 318,492 | 651,174 |
| &nbsp;&nbsp;input (fresh) | 14,305 | 14,305 | 17,360 |
| &nbsp;&nbsp;cache write (incl. generated files) | 70,215 | 52,593 | 77,062 |
| &nbsp;&nbsp;cache read (context re-read/turn) | 205,032 | 247,576 | 552,117 |
| &nbsp;&nbsp;output | 759 | 4,018 | 4,635 |
| Agent turns | 13 | 13 | 22 |
| Harness "subagent_tokens" | 28,056 | 30,884 | 37,657 |
| **Lines authored (non-blank)** | 208 | 188 | 155 |
| Lines authored (total) | 231 | 206 | 179 |
| Files authored | 1 (`Home.razor`) | 1 (`App.jsx`) | 1 (`Program.cs`) |
| **Payload — raw** | 9.11 MB | 217 KB | 7.73 MB |
| **Payload — compressed transfer** | 2.71 MB (brotli) | ~64 KB (gzip) | 2.22 MB (brotli) |
| Payload file count | 46 | 6 | 23 |
| Build result | ✅ clean | ✅ clean | ✅ (expected CA1416/CS1591 warnings) |

## Notes / caveats

- **Token accounting:** this harness records model-generated *tool content* (the written
  files) under `cache write`, not `output`, so `output` alone understates work. Use
  **total processed** for comparison. htnet's ~2× total is dominated by **cache read**:
  it took 22 turns vs 13 (reading the primer + more build/iterate round-trips), and every
  turn re-reads the cached context (billed at ~10%). Genuinely-new tokens
  (input + cache-write + output) were closer: Blazor ~85k, React ~71k, htnet ~99k.
- **Lines:** htnet was the *most* compact implementation (155 non-blank), then React,
  then Blazor — even though htnet was the "unknown" framework. The shared CSS did the
  visual work in all three; agents only wrote structure + state + handlers.
- **Payload / "final page size":** React ships ~64 KB gzip (just app JS + CSS + HTML).
  Blazor and htnet each ship the .NET WASM runtime + assemblies → ~2–2.7 MB compressed.
  htnet's payload is ~0.5 MB smaller and ~half the file count of Blazor (no Blazor
  component framework / no `blazor.webassembly.js` machinery — just CC.CSX + runtime).
- **Rendered DOM size** was not captured live (browser navigation was denied during the
  run). By construction it is ~identical across the three (same spec, same classes,
  same node count) — re-runnable if browser access is enabled.

## How to reproduce / run

```
# each app:
cd comparison/react  && npm run dev        # http://localhost:5173
cd comparison/blazor && dotnet run         # serves the WASM app
cd comparison/htnet  && dotnet run         # serves the WASM app
```

Measurement scripts/data: `measure-lines.sh`; token data parsed from
`~/.claude/projects/<proj>/<session>/subagents/agent-*.jsonl`.
