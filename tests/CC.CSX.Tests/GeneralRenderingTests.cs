namespace CC.CSX.Tests;
using static CC.CSX.HtmlAttributeKeys;
public class GeneralRenderingTests
{
    [Fact]
    public void Indent_Should_BeRespected()
    {
        RenderOptions.Indent = 0;
        var sut = Div(A(Label("test")));
        var expected = "<div><a><label>test</label></a></div>";
        Assert.Equal(expected, sut.ToString());

        expected = @"<div>
  <a>
    <label>
      test
    </label>
  </a>
</div>".Replace("\r", string.Empty);
        RenderOptions.Indent = 2;
        Assert.Equal(expected, sut.ToString());
    }

    [Fact]
    public void AllCreationMethodsShouldWorkProperly()
    {
        RenderOptions.Indent = 0;
        var attrsOnly2 = A(href("https://google.com"), @class("test"));
        var attrsOnly1 = A((href, "https://google.com"), (@class, "test"));
        Assert.Equal(attrsOnly1.ToString(), attrsOnly2.ToString());
    }
}