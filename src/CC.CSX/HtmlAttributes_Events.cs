namespace CC.CSX;
public static partial class HtmlElements
{
    ///<summary>
    /// script	Fires the moment that the element loses focus
    ///</summary>
    public static HtmlEventAttribute onBlur(string script) =>new HtmlEventAttribute("onblur", script);

    ///<summary>
    /// script	Fires the moment when the value of the element is changed
    ///</summary>
    public static HtmlEventAttribute onChange(string script) =>new HtmlEventAttribute("onchange", script);

    ///<summary>
    /// script	Script to be run when a context menu is triggered
    ///</summary>
    public static HtmlEventAttribute onContextmenu(string script) =>new HtmlEventAttribute("oncontextmenu", script);

    ///<summary>
    /// script	Fires the moment when the element gets focus
    ///</summary>
    public static HtmlEventAttribute onFocus(string script) =>new HtmlEventAttribute("onfocus", script);

    ///<summary>
    /// script	Script to be run when an element gets user input
    ///</summary>
    public static HtmlEventAttribute onInput(string script) =>new HtmlEventAttribute("oninput", script);

    ///<summary>
    /// script	Script to be run when an element is invalid
    ///</summary>
    public static HtmlEventAttribute onInvalid(string script) =>new HtmlEventAttribute("oninvalid", script);

    ///<summary>
    /// script	Fires when the Reset button in a form is clicked
    ///</summary>
    public static HtmlEventAttribute onReset(string script) =>new HtmlEventAttribute("onreset", script);

    ///<summary>
    /// script	Fires when the user writes something in a search field (for <input="search">)
    ///</summary>
    public static HtmlEventAttribute onSearch(string script) =>new HtmlEventAttribute("onsearch", script);

    ///<summary>
    /// script	Fires after some text has been selected in an element
    ///</summary>
    public static HtmlEventAttribute onSelect(string script) =>new HtmlEventAttribute("onselect", script);

    ///<summary>
    /// script	Fires when a form is submitted
    ///</summary>
    public static HtmlEventAttribute onSubmit(string script) =>new HtmlEventAttribute("onsubmit", script);

    ///<summary>
    /// script	Fires when a user is pressing a key
    ///</summary>
    public static HtmlEventAttribute onKeydown(string script) =>new HtmlEventAttribute("onkeydown", script);

    ///<summary>
    /// script	Fires when a user presses a key
    ///</summary>
    public static HtmlEventAttribute onKeypress(string script) =>new HtmlEventAttribute("onkeypress", script);

    ///<summary>
    /// script	Fires when a user releases a key
    ///</summary>
    public static HtmlEventAttribute onKeyup(string script) =>new HtmlEventAttribute("onkeyup", script);

    ///<summary>
    /// script	Fires on a mouse click on the element
    ///</summary>
    public static HtmlEventAttribute onClick(string script) =>new HtmlEventAttribute("onclick", script);

    ///<summary>
    /// script	Fires on a mouse double-click on the element
    ///</summary>
    public static HtmlEventAttribute onDblclick(string script) =>new HtmlEventAttribute("ondblclick", script);

    ///<summary>
    /// script	Fires when a mouse button is pressed down on an element
    ///</summary>
    public static HtmlEventAttribute onMousedown(string script) =>new HtmlEventAttribute("onmousedown", script);

    ///<summary>
    /// script	Fires when the mouse pointer is moving while it is over an element
    ///</summary>
    public static HtmlEventAttribute onMousemove(string script) =>new HtmlEventAttribute("onmousemove", script);

    ///<summary>
    /// script	Fires when the mouse pointer moves out of an element
    ///</summary>
    public static HtmlEventAttribute onMouseout(string script) =>new HtmlEventAttribute("onmouseout", script);

    ///<summary>
    /// script	Fires when the mouse pointer moves over an element
    ///</summary>
    public static HtmlEventAttribute onMouseover(string script) =>new HtmlEventAttribute("onmouseover", script);

    ///<summary>
    /// script	Fires when a mouse button is released over an element
    ///</summary>
    public static HtmlEventAttribute onMouseup(string script) =>new HtmlEventAttribute("onmouseup", script);

    ///<summary>
    /// script	Deprecated. Use the onwheel attribute instead
    ///</summary>
    public static HtmlEventAttribute onMousewheel(string script) =>new HtmlEventAttribute("onmousewheel", script);

    ///<summary>
    /// script	Fires when the mouse wheel rolls up or down over an element
    ///</summary>
    public static HtmlEventAttribute onWheel(string script) =>new HtmlEventAttribute("onwheel", script);

    ///<summary>
    /// script	Script to be run when an element is dragged
    ///</summary>
    public static HtmlEventAttribute onDrag(string script) =>new HtmlEventAttribute("ondrag", script);

    ///<summary>
    /// script	Script to be run at the end of a drag operation   
    ///</summary>
    public static HtmlEventAttribute onDragend(string script) =>new HtmlEventAttribute("ondragend", script);

    ///<summary>
    /// script	Script to be run when an element has been dragged to a valid drop target   
    ///</summary>
    public static HtmlEventAttribute onDragenter(string script) =>new HtmlEventAttribute("ondragenter", script);

    ///<summary>
    ///  script	Script to be run when an element leaves a valid drop target   
    ///</summary>
    public static HtmlEventAttribute onDragleave(string script) =>new HtmlEventAttribute("ondragleave", script);

    ///<summary>
    ///  script	Script to be run when an element is being dragged over a valid drop target   
    ///</summary>
    public static HtmlEventAttribute onDragover(string script) =>new HtmlEventAttribute("ondragover", script);

    ///<summary>
    ///  script	Script to be run at the start of a drag operation   
    ///</summary>
    public static HtmlEventAttribute onDragstart(string script) =>new HtmlEventAttribute("ondragstart", script);

    ///<summary>
    ///  script	Script to be run when dragged element is being dropped   
    ///</summary>
    public static HtmlEventAttribute onDrop(string script) =>new HtmlEventAttribute("ondrop", script);

    ///<summary>
    ///  script	Script to be run when an element's scrollbar is being scrolled   
    ///</summary>
    public static HtmlEventAttribute onScroll(string script) =>new HtmlEventAttribute("onscroll", script);

    ///<summary>
    ///  script	Fires when the user copies the content of an element   
    ///</summary>
    public static HtmlEventAttribute onCopy(string script) =>new HtmlEventAttribute("oncopy", script);

    ///<summary>
    ///  script	Fires when the user cuts the content of an element   
    ///</summary>
    public static HtmlEventAttribute onCut(string script) =>new HtmlEventAttribute("oncut", script);

    ///<summary>
    ///  script	Fires when the user pastes some content in an element   
    ///</summary> 
    public static HtmlEventAttribute onPaste(string script) =>new HtmlEventAttribute("onpaste", script);
}
