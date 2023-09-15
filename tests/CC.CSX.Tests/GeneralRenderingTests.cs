namespace CC.CSX.Tests;
using ha = CC.CSX.HtmlAttributeKeys;
public class GeneralRenderingTests
{
    [Fact]
    public void Indent_Should_BeRespected()
    {
        RenderOptions.Indent = 0;
        int[] a = [1, 2, 3, 4];
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
        var attrsOnly2 = A(href("https://google.com"), @class("test", "something else"));
        Console.WriteLine(attrsOnly2);
    }
}