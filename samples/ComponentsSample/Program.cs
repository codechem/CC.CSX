// CC.CSX.Components rendered client-side by CC.CSX.Browser (.NET WASM) — no server, no htmx.
// State lives in C#; mutating it from a delegate handler re-renders the view and morphs the DOM.

string theme = "light";
int likes = 0;
string[] themes = ["light", "dark", "cupcake", "synthwave"];

await BrowserApp.MountAsync("#app", View);
await BrowserApp.RunAsync();

HtmlNode View() =>
    // daisyUI theming: data-theme on the app root, swapped in-browser by the buttons below.
    Div(("data-theme", theme), @class("min-h-screen bg-base-200 text-base-content"),
        Div(@class("max-w-3xl mx-auto p-8 flex flex-col gap-6"),
            H1(@class("text-3xl font-bold"), "CC.CSX.Components"),
            P(@class("opacity-70"),
                "Themeable components over daisyUI, rendered client-side by CC.CSX in .NET WASM. "
                + "Production CSS is precompiled — no CDN, no runtime Tailwind."),

            ThemeSwitcher(),

            Card(
                CardTitle("Buttons"),
                Div(@class("flex flex-wrap gap-2 items-center"),
                    Button(Variant.Primary, "Primary"),
                    Button(Variant.Secondary, "Secondary"),
                    Button(Variant.Accent, "Accent"),
                    Button(Variant.Ghost, "Ghost"),
                    Button(Variant.Error, Size.Sm, "Small error"),
                    Button(Variant.Success, Size.Lg, "Large success")
                )
            ),

            Card(
                CardTitle("Badges"),
                Div(@class("flex flex-wrap gap-2 items-center"),
                    Badge(Variant.Primary, "new"),
                    Badge(Variant.Success, "ok"),
                    Badge(Variant.Warning, "beta"),
                    Badge(Variant.Error, Size.Lg, "alert")
                )
            ),

            Card(
                CardTitle("Alerts"),
                Div(@class("flex flex-col gap-2"),
                    Alert(Variant.Info, "Heads up — this is an info alert."),
                    Alert(Variant.Success, "Saved successfully."),
                    Alert(Variant.Warning, "Careful with that."),
                    Alert(Variant.Error, "Something went wrong.")
                )
            ),

            Card(
                CardTitle("Interactive (C# in the browser)"),
                Button(Variant.Primary, onClick(() => likes++), $"♥ Like ({likes})")
            )
        )
    );

HtmlNode ThemeSwitcher() =>
    Card(
        CardTitle("Theme"),
        P(@class("opacity-70"), "Same C#, different look — switch daisyUI theme in-browser:"),
        Div(@class("flex flex-wrap gap-2"),
            themes.Select(t =>
                Button(t == theme ? Variant.Primary : Variant.Default, Size.Sm,
                    onClick(() => theme = t), t)).ToArray()
        )
    );
