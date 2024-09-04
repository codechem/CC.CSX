namespace CC.CSX.Tests;
using static CC.CSX.HtmlAttributeKeys;

public class GeneralRenderingTests
{
    HtmlNode _sut = Div(A(Label("test")));
    string _expectedNoIndent = "<div><a><label>test</label></a></div>";
    string _expected = @"<div>
  <a>
    <label>
      test
    </label>
  </a>
</div>".Replace("\r", string.Empty);

    [Fact]
    public void Indent_Should_BeRespected()
    {
        RenderOptions.Indent = 0;
        Assert.Equal(_expectedNoIndent, _sut.ToString().Replace("\r\n","\n"));
        RenderOptions.Indent = 2;
        Assert.Equal(_expected, _sut.ToString().Replace("\r\n","\n"));
    }

    [Fact]
    public void AllCreationMethodsShouldWorkProperly()
    {
        RenderOptions.Indent = 0;
        var attrsOnly2 = A(href("https://google.com"), @class("test"));
        var attrsOnly1 = A((href, "https://google.com"), (@class, "test"));
        Assert.Equal(attrsOnly1.ToString(), attrsOnly2.ToString().Replace("\r\n","\n"));
    }
    [Fact]
    public void WriteTo_Should_ProduceTheSameOutputAsToString()
    {
        var tw = new StringWriter() as TextWriter;
        _sut.WriteTo(ref tw);
        Assert.Equal(_expected, tw.ToString()!.Replace("\r\n","\n"));
    }

    [Fact]
    public void MultiAttribute_Should_OnlyRenderItsChildren()
    {
        var sut = new MultiHtmlAttribute("#test", new[] { new HtmlAttribute("test", "test"), new HtmlAttribute("test2", "test2") });
        var tw = new StringWriter() as TextWriter;
        sut.WriteTo(ref tw);
        Assert.Equal("test=\"test\" test2=\"test2\"", sut.ToString()!.Replace("\r\n","\n"));
        Assert.Equal("test=\"test\" test2=\"test2\"", tw.ToString()!.Replace("\r\n","\n"));
    }

    [Fact]
    void CheckedAttribute_ShouldRenderConditionally()
    {
        var sutRendered = @checked(true);
        var sutNotRendered = @checked(false);

        var tw = new StringWriter() as TextWriter;
        sutNotRendered.WriteTo(ref tw);
        // should not append anything
        Assert.Equal(string.Empty, tw.ToString());
        sutRendered.WriteTo(ref tw);
        Assert.Equal("checked", tw.ToString());
    }
}