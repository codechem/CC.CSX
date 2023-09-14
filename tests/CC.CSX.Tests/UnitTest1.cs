namespace CC.CSX.Tests;
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
            Assert.Equal( expected, sut.ToString());
    }
}