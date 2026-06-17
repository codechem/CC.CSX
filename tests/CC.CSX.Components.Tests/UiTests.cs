namespace CC.CSX.Components.Tests;

using Xunit;

using CC.CSX;
using CC.CSX.Css;
using CC.CSX.Components;

using static CC.CSX.HtmlAttributes;
using static CC.CSX.Components.UI;

public class UiTests
{
    public UiTests() => Theme.Current = new DaisyUiTheme(); // ensure a known theme

    [Fact]
    public void Button_default_renders_btn()
    {
        var html = Button("Save").ToString();
        Assert.Contains("<button", html);
        Assert.Contains("class=\"btn\"", html);
        Assert.Contains("Save", html);
    }

    [Fact]
    public void Button_variant_and_size_map_to_daisy_classes()
    {
        var html = Button(Variant.Primary, Size.Lg, "Go").ToString();
        Assert.Contains("btn", html);
        Assert.Contains("btn-primary", html);
        Assert.Contains("btn-lg", html);
    }

    [Fact]
    public void Caller_class_is_merged_into_single_attribute()
    {
        var html = Button(Variant.Primary, @class("mt-4"), "Go").ToString();
        // exactly one class attribute, containing both theme and caller classes
        Assert.Equal(1, CountOccurrences(html, "class=\""));
        Assert.Contains("btn", html);
        Assert.Contains("btn-primary", html);
        Assert.Contains("mt-4", html);
    }

    [Fact]
    public void Other_attributes_pass_through()
    {
        var html = Button(id("save"), ("data-x", "1"), "Go").ToString();
        Assert.Contains("id=\"save\"", html);
        Assert.Contains("data-x=\"1\"", html);
    }

    [Fact]
    public void Alert_sets_role_and_variant()
    {
        var html = Alert(Variant.Success, "Done").ToString();
        Assert.Contains("role=\"alert\"", html);
        Assert.Contains("alert", html);
        Assert.Contains("alert-success", html);
    }

    [Fact]
    public void Card_wraps_body()
    {
        var html = Card(CardTitle("Hi"), "body").ToString();
        Assert.Contains("class=\"card", html);
        Assert.Contains("card-body", html);
        Assert.Contains("card-title", html);
        Assert.Contains("Hi", html);
    }

    [Fact]
    public void Theme_With_scopes_a_custom_theme()
    {
        var html = Theme.With(new UppercaseButtonTheme(), () => Button("x").ToString());
        Assert.Contains("MY-BTN", html);
        // reverts afterwards
        Assert.Contains("class=\"btn\"", Button("y").ToString());
    }

    sealed class UppercaseButtonTheme : DaisyUiTheme
    {
        public override CssClass Button(Variant variant, Size size) => "MY-BTN";
    }

    static int CountOccurrences(string haystack, string needle)
    {
        int count = 0, i = 0;
        while ((i = haystack.IndexOf(needle, i, StringComparison.Ordinal)) >= 0) { count++; i += needle.Length; }
        return count;
    }
}
