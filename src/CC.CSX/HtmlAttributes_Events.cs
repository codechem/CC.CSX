namespace CC.CSX;
using Keys=CC.CSX.HtmlElementEventKeys;
public static partial class HtmlElements
{
    ///<summary>
    /// script: Fires the moment that the element loses focus
    ///</summary>
    public static HtmlEventAttribute onBlur(string script) =>new HtmlEventAttribute(Keys.onBlur, script);

    ///<summary>
    /// script: Fires the moment when the value of the element is changed
    ///</summary>
    public static HtmlEventAttribute onChange(string script) =>new HtmlEventAttribute(Keys.onChange, script);

    ///<summary>
    /// script: Script to be run when a context menu is triggered
    ///</summary>
    public static HtmlEventAttribute onContextmenu(string script) =>new HtmlEventAttribute(Keys.onContextmenu, script);

    ///<summary>
    /// script: Fires the moment when the element gets focus
    ///</summary>
    public static HtmlEventAttribute onFocus(string script) =>new HtmlEventAttribute(Keys.onFocus, script);

    ///<summary>
    /// script: Script to be run when an element gets user input
    ///</summary>
    public static HtmlEventAttribute onInput(string script) =>new HtmlEventAttribute(Keys.onInput, script);

    ///<summary>
    /// script: Script to be run when an element is invalid
    ///</summary>
    public static HtmlEventAttribute onInvalid(string script) =>new HtmlEventAttribute(Keys.onInvalid, script);

    ///<summary>
    /// script: Fires when the Reset button in a form is clicked
    ///</summary>
    public static HtmlEventAttribute onReset(string script) =>new HtmlEventAttribute(Keys.onReset, script);

    ///<summary>
    /// script: Fires when the user writes something in a search field (for &amp;lt;input=&amp;quot;search&amp;quot;&amp;gt;)
    ///</summary>
    public static HtmlEventAttribute onSearch(string script) =>new HtmlEventAttribute(Keys.onSearch, script);

    ///<summary>
    /// script: Fires after some text has been selected in an element
    ///</summary>
    public static HtmlEventAttribute onSelect(string script) =>new HtmlEventAttribute(Keys.onSelect, script);

    ///<summary>
    /// script: Fires when a form is submitted
    ///</summary>
    public static HtmlEventAttribute onSubmit(string script) =>new HtmlEventAttribute(Keys.onSubmit, script);

    ///<summary>
    /// script: Fires when a user is pressing a key
    ///</summary>
    public static HtmlEventAttribute onKeydown(string script) =>new HtmlEventAttribute(Keys.onKeydown, script);

    ///<summary>
    /// script: Fires when a user presses a key
    ///</summary>
    public static HtmlEventAttribute onKeypress(string script) =>new HtmlEventAttribute(Keys.onKeypress, script);

    ///<summary>
    /// script: Fires when a user releases a key
    ///</summary>
    public static HtmlEventAttribute onKeyup(string script) =>new HtmlEventAttribute(Keys.onKeyup, script);

    ///<summary>
    /// script: Fires on a mouse click on the element
    ///</summary>
    public static HtmlEventAttribute onClick(string script) =>new HtmlEventAttribute(Keys.onClick, script);

    ///<summary>
    /// script: Fires on a mouse double-click on the element
    ///</summary>
    public static HtmlEventAttribute onDblclick(string script) =>new HtmlEventAttribute(Keys.onDblclick, script);

    ///<summary>
    /// script: Fires when a mouse button is pressed down on an element
    ///</summary>
    public static HtmlEventAttribute onMousedown(string script) =>new HtmlEventAttribute(Keys.onMousedown, script);

    ///<summary>
    /// script: Fires when the mouse pointer is moving while it is over an element
    ///</summary>
    public static HtmlEventAttribute onMousemove(string script) =>new HtmlEventAttribute(Keys.onMousemove, script);

    ///<summary>
    /// script: Fires when the mouse pointer moves out of an element
    ///</summary>
    public static HtmlEventAttribute onMouseout(string script) =>new HtmlEventAttribute(Keys.onMouseout, script);

    ///<summary>
    /// script: Fires when the mouse pointer moves over an element
    ///</summary>
    public static HtmlEventAttribute onMouseover(string script) =>new HtmlEventAttribute(Keys.onMouseover, script);

    ///<summary>
    /// script: Fires when a mouse button is released over an element
    ///</summary>
    public static HtmlEventAttribute onMouseup(string script) =>new HtmlEventAttribute(Keys.onMouseup, script);

    ///<summary>
    /// script: Deprecated. Use the onwheel attribute instead
    ///</summary>
    public static HtmlEventAttribute onMousewheel(string script) =>new HtmlEventAttribute(Keys.onMousewheel, script);

    ///<summary>
    /// script: Fires when the mouse wheel rolls up or down over an element
    ///</summary>
    public static HtmlEventAttribute onWheel(string script) =>new HtmlEventAttribute(Keys.onWheel, script);

    ///<summary>
    /// script: Script to be run when an element is dragged
    ///</summary>
    public static HtmlEventAttribute onDrag(string script) =>new HtmlEventAttribute(Keys.onDrag, script);

    ///<summary>
    /// script: Script to be run at the end of a drag operation   
    ///</summary>
    public static HtmlEventAttribute onDragend(string script) =>new HtmlEventAttribute(Keys.onDragend, script);

    ///<summary>
    /// script: Script to be run when an element has been dragged to a valid drop target   
    ///</summary>
    public static HtmlEventAttribute onDragenter(string script) =>new HtmlEventAttribute(Keys.onDragenter, script);

    ///<summary>
    ///  script: Script to be run when an element leaves a valid drop target   
    ///</summary>
    public static HtmlEventAttribute onDragleave(string script) =>new HtmlEventAttribute(Keys.onDragleave, script);

    ///<summary>
    ///  script: Script to be run when an element is being dragged over a valid drop target   
    ///</summary>
    public static HtmlEventAttribute onDragover(string script) =>new HtmlEventAttribute(Keys.onDragover, script);

    ///<summary>
    ///  script: Script to be run at the start of a drag operation   
    ///</summary>
    public static HtmlEventAttribute onDragstart(string script) =>new HtmlEventAttribute(Keys.onDragstart, script);

    ///<summary>
    ///  script: Script to be run when dragged element is being dropped   
    ///</summary>
    public static HtmlEventAttribute onDrop(string script) =>new HtmlEventAttribute(Keys.onDrop, script);

    ///<summary>
    ///  script: Script to be run when an element's scrollbar is being scrolled   
    ///</summary>
    public static HtmlEventAttribute onScroll(string script) =>new HtmlEventAttribute(Keys.onScroll, script);

    ///<summary>
    ///  script: Fires when the user copies the content of an element   
    ///</summary>
    public static HtmlEventAttribute onCopy(string script) =>new HtmlEventAttribute(Keys.onCopy, script);

    ///<summary>
    ///  script: Fires when the user cuts the content of an element   
    ///</summary>
    public static HtmlEventAttribute onCut(string script) =>new HtmlEventAttribute(Keys.onCut, script);

    ///<summary>
    ///  script: Fires when the user pastes some content in an element   
    ///</summary> 
    public static HtmlEventAttribute onPaste(string script) =>new HtmlEventAttribute(Keys.onPaste, script);
}