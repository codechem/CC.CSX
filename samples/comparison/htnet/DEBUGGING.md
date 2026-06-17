# Debugging the htnet (.NET WASM) app — setting C# breakpoints in the browser

The app is a standalone **.NET WebAssembly** app (no Blazor). When run in **Debug**
(`dotnet run` defaults to Debug), the runtime ships PDB symbols, `debugLevel` is
auto-enabled, and `dotnet run` hosts a debug proxy at `/_framework/debug/ws-proxy`.
You can then debug the actual **C#** (Program.cs) in Chromium DevTools or VS Code.

## Method 1 — Chrome/Edge DevTools (no extra tooling)

.NET's WASM debugger attaches through the browser's remote-debugging port, so the
browser must be launched with that port enabled.

1. Make sure the app is running in Debug:
   ```
   cd comparison/htnet
   dotnet run            # serves http://0.0.0.0:5210  (Debug by default)
   ```
2. Fully quit Chrome/Edge, then relaunch it with remote debugging and a throwaway
   profile, pointed at the app:
   ```
   # Linux Chrome
   google-chrome --remote-debugging-port=9222 --user-data-dir=/tmp/htnet-debug http://localhost:5210/
   # Chromium
   chromium       --remote-debugging-port=9222 --user-data-dir=/tmp/htnet-debug http://localhost:5210/
   # macOS Chrome
   "/Applications/Google Chrome.app/Contents/MacOS/Google Chrome" --remote-debugging-port=9222 --user-data-dir=/tmp/htnet-debug http://localhost:5210/
   # Edge
   msedge --remote-debugging-port=9222 --user-data-dir=/tmp/htnet-debug http://localhost:5210/
   ```
   (If you browse from another machine, replace `localhost` with the host IP, e.g.
   `http://100.85.182.40:5210/`.)
3. Let the dashboard load, then open **DevTools (F12) → Sources** tab.
4. In the file tree, expand the `file://` (or `dotnet://` / project) node — you'll see
   your **C# source**, including `Program.cs`. Open it and click the line gutter to set
   a breakpoint.
5. Interact with the dashboard (click **Refresh**, change the date range, click a filter
   tab or the **Amount** header). Execution stops at your breakpoint; inspect locals,
   the call stack, and step through C# right there.

Tip: breakpoints in the event handlers (`onClick`, `onChange`) and in `View()` /
`Reseed*()` are the most useful to watch the render + state flow.

## Method 2 — VS Code

A launch config is provided at `comparison/htnet/.vscode/launch.json`.

1. **Stop** any `dotnet run` already serving on port 5210 (this config starts the app
   itself).
2. Open the `comparison/htnet` folder in VS Code (with the C# Dev Kit extension).
3. Set breakpoints in `Program.cs`, press **F5** ("Debug htnet WASM (Chrome)").
   VS Code launches the app + a debug browser and binds breakpoints to the editor.

## Notes
- Debugging only works in **Debug** builds — `dotnet publish -c Release` strips this.
- Optimizations are off in Debug, so stepping maps cleanly to source.
- This is plain .NET WASM debugging; it is not tied to CC.CSX — any C# in the app
  (library code in `CC.CSX*` too, since their PDBs are shipped) is steppable.
