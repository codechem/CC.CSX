namespace CC.CSX;
using Keys = CC.CSX.HtmlAttributeKeys;
///<summary>
/// All html attributes 
/// https://www.w3schools.com/tags/ref_attributes.asp
///</summary>
public static class HtmlAttributes
{
    ///<summary>
    /// accept	&amp;lt;input&amp;gt;	Specifies the types of files that the server accepts (only for type="file")
    ///</summary>
    public static HtmlAttribute accept(string value) => new HtmlAttribute(Keys.accept, value);
    ///<summary>
    /// Global Attributes	Specifies a shortcut key to activate/focus an element
    ///</summary>
    public static HtmlAttribute accesskey(string value) => new HtmlAttribute(Keys.accesskey, value);
    ///<summary>
    /// &amp;lt;form&amp;gt;	Specifies where to send the form-data when a form is submitted
    ///</summary>
    public static HtmlAttribute action(string value) => new HtmlAttribute(Keys.action, value);
    ///<summary>
    /// Not supported in HTML 5.	Specifies the alignment according to surrounding elements. Use CSS instead
    ///</summary>
    public static HtmlAttribute align(string value) => new HtmlAttribute(Keys.align, value);
    ///<summary>
    /// &amp;lt;area&amp;gt;, &amp;lt;img&amp;gt;, &amp;lt;input&amp;gt;	Specifies an alternate text when the original element fails to display
    ///</summary>
    public static HtmlAttribute alt(string value) => new HtmlAttribute(Keys.alt, value);
    ///<summary>
    /// &amp;lt;script&amp;gt;	Specifies that the script is executed asynchronously (only for external scripts)
    ///</summary>
    public static HtmlAttribute async(string value) => new HtmlAttribute(Keys.async, value);
    ///<summary>
    /// &amp;lt;form&amp;gt;, &amp;lt;input&amp;gt;	Specifies whether the &amp;lt;form&amp;gt; or the &amp;lt;input&amp;gt; element should have autocomplete enabled
    ///</summary>
    public static HtmlAttribute autocomplete(string value) => new HtmlAttribute(Keys.autocomplete, value);
    ///<summary>
    /// &amp;lt;button&amp;gt;, &amp;lt;input&amp;gt;, &amp;lt;select&amp;gt;, &amp;lt;textarea&amp;gt;	Specifies that the element should automatically get focus when the page loads
    ///</summary>
    public static HtmlAttribute autofocus(string value) => new HtmlAttribute(Keys.autofocus, value);
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Specifies that the audio/video will start playing as soon as it is ready
    ///</summary>
    public static HtmlAttribute autoplay(string value) => new HtmlAttribute(Keys.autoplay, value);
    ///<summary>
    /// Not supported in HTML 5.	Specifies the background color of an element. Use CSS instead
    ///</summary>
    public static HtmlAttribute bgcolor(string value) => new HtmlAttribute(Keys.bgcolor, value);
    ///<summary>
    /// Not supported in HTML 5.	Specifies the width of the border of an element. Use CSS instead
    ///</summary>
    public static HtmlAttribute border(string value) => new HtmlAttribute(Keys.border, value);
    ///<summary>
    /// &amp;lt;meta&amp;gt;, &amp;lt;script&amp;gt;	Specifies the character encoding
    ///</summary>
    public static HtmlAttribute charset(string value) => new HtmlAttribute(Keys.charset, value);
    ///<summary>
    /// &amp;lt;input&amp;gt;	Specifies that an &amp;lt;input&amp;gt; element should be pre-selected when the page loads (for type="checkbox" or type="radio")
    ///</summary>
    public static HtmlAttribute @checked(string value) => new HtmlAttribute(Keys.@checked, value);
    ///<summary>
    /// &amp;lt;blockquote&amp;gt;, &amp;lt;del&amp;gt;, &amp;lt;ins&amp;gt;, &amp;lt;q&amp;gt;	Specifies a URL which explains the quote/deleted/inserted text
    ///</summary>
    public static HtmlAttribute cite(string value) => new HtmlAttribute(Keys.cite, value);
    ///<summary>
    /// Global Attributes	Specifies one or more classnames for an element (refers to a class in a style sheet)
    ///</summary>
    public static HtmlAttribute @class(params string[] values) => new HtmlAttribute(Keys.@class, string.Join(" ", values));
    ///<summary>
    /// Not supported in HTML 5.	Specifies the text color of an element. Use CSS instead
    ///</summary>
    public static HtmlAttribute color(string value) => new HtmlAttribute(Keys.color, value);
    ///<summary>
    /// &amp;lt;textarea&amp;gt;	Specifies the visible width of a text area
    ///</summary>
    public static HtmlAttribute cols(string value) => new HtmlAttribute(Keys.cols, value);
    ///<summary>
    /// &amp;lt;td&amp;gt;, &amp;lt;th&amp;gt;	Specifies the number of columns a table cell should span
    ///</summary>
    public static HtmlAttribute colspan(string value) => new HtmlAttribute(Keys.colspan, value);
    ///<summary>
    /// &amp;lt;meta&amp;gt;	Gives the value associated with the http-equiv or name attribute
    ///</summary>
    public static HtmlAttribute content(string value) => new HtmlAttribute(Keys.content, value);
    ///<summary>
    /// Global Attributes	Specifies whether the content of an element is editable or not
    ///</summary>
    public static HtmlAttribute contenteditable(string value) => new HtmlAttribute(Keys.contenteditable, value);
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Specifies that audio/video controls should be displayed (such as a play/pause button etc)
    ///</summary>
    public static HtmlAttribute controls(string value) => new HtmlAttribute(Keys.controls, value);
    ///<summary>
    /// &amp;lt;area&amp;gt;	Specifies the coordinates of the area
    ///</summary>
    public static HtmlAttribute coords(string value) => new HtmlAttribute(Keys.coords, value);
    ///<summary>
    /// &amp;lt;object&amp;gt;	Specifies the URL of the resource to be used by the object
    ///</summary>
    public static HtmlAttribute data(string value) => new HtmlAttribute(Keys.data, value);
    ///<summary>
    /// &amp;lt;del&amp;gt;, &amp;lt;ins&amp;gt;, &amp;lt;time&amp;gt;	Specifies the date and time
    ///</summary>
    public static HtmlAttribute datetime(string value) => new HtmlAttribute(Keys.datetime, value);
    ///<summary>
    /// &amp;lt;track&amp;gt;	Specifies that the track is to be enabled if the user's preferences do not indicate that another track would be more appropriate
    ///</summary>
    public static HtmlAttribute @default(string value) => new HtmlAttribute(Keys.@default, value);
    ///<summary>
    /// &amp;lt;script&amp;gt;	Specifies that the script is executed when the page has finished parsing (only for external scripts)
    ///</summary>
    public static HtmlAttribute defer(string value) => new HtmlAttribute(Keys.defer, value);
    ///<summary>
    /// Global Attributes	Specifies the text direction for the content in an element
    ///</summary>
    public static HtmlAttribute dir(string value) => new HtmlAttribute(Keys.dir, value);
    ///<summary>
    /// &amp;lt;input&amp;gt;, &amp;lt;textarea&amp;gt;	Specifies that the text direction will be submitted
    ///</summary>
    public static HtmlAttribute dirname(string value) => new HtmlAttribute(Keys.dirname, value);
    ///<summary>
    /// &amp;lt;button&amp;gt;, &amp;lt;fieldset&amp;gt;, &amp;lt;input&amp;gt;, &amp;lt;optgroup&amp;gt;, &amp;lt;option&amp;gt;, &amp;lt;select&amp;gt;, &amp;lt;textarea&amp;gt;	Specifies that the specified element/group of elements should be disabled
    ///</summary>
    public static HtmlAttribute disabled(string value) => new HtmlAttribute(Keys.disabled, value);
    ///<summary>
    /// &amp;lt;a&amp;gt;, &amp;lt;area&amp;gt;	Specifies that the target will be downloaded when a user clicks on the hyperlink
    ///</summary>
    public static HtmlAttribute download(string value) => new HtmlAttribute(Keys.download, value);
    ///<summary>
    /// Global Attributes	Specifies whether an element is draggable or not
    ///</summary>
    public static HtmlAttribute draggable(string value) => new HtmlAttribute(Keys.draggable, value);
    ///<summary>
    /// &amp;lt;form&amp;gt;	Specifies how the form-data should be encoded when submitting it to the server (only for method="post")
    ///</summary>
    public static HtmlAttribute enctype(string value) => new HtmlAttribute(Keys.enctype, value);
    ///<summary>
    /// &amp;lt;label&amp;gt;, &amp;lt;output&amp;gt;	Specifies which form element(s) a label/calculation is bound to
    ///</summary>
    public static HtmlAttribute @for(string value) => new HtmlAttribute(Keys.@for, value);
    ///<summary>
    /// &amp;lt;button&amp;gt;, &amp;lt;fieldset&amp;gt;, &amp;lt;input&amp;gt;, &amp;lt;label&amp;gt;, &amp;lt;meter&amp;gt;, &amp;lt;object&amp;gt;, &amp;lt;output&amp;gt;, &amp;lt;select&amp;gt;, &amp;lt;textarea&amp;gt;	Specifies the name of the form the element belongs to
    ///</summary>
    public static HtmlAttribute form(string value) => new HtmlAttribute(Keys.form, value);
    ///<summary>
    /// &amp;lt;button&amp;gt;, &amp;lt;input&amp;gt;	Specifies where to send the form-data when a form is submitted. Only for type="submit"
    ///</summary>
    public static HtmlAttribute formaction(string value) => new HtmlAttribute(Keys.formaction, value);
    ///<summary>
    /// &amp;lt;td&amp;gt;, &amp;lt;th&amp;gt;	Specifies one or more headers cells a cell is related to
    ///</summary>
    public static HtmlAttribute headers(string value) => new HtmlAttribute(Keys.headers, value);
    ///<summary>
    /// &amp;lt;canvas&amp;gt;, &amp;lt;embed&amp;gt;, &amp;lt;iframe&amp;gt;, &amp;lt;img&amp;gt;, &amp;lt;input&amp;gt;, &amp;lt;object&amp;gt;, &amp;lt;video&amp;gt;	Specifies the height of the element
    ///</summary>
    public static HtmlAttribute height(string value) => new HtmlAttribute(Keys.height, value);
    ///<summary>
    /// Global Attributes	Specifies that an element is not yet, or is no longer, relevant
    ///</summary>
    public static HtmlAttribute hidden(string value) => new HtmlAttribute(Keys.hidden, value);
    ///<summary>
    /// &amp;lt;meter&amp;gt;	Specifies the range that is considered to be a high value
    ///</summary>
    public static HtmlAttribute high(string value) => new HtmlAttribute(Keys.high, value);
    ///<summary>
    /// &amp;lt;a&amp;gt;, &amp;lt;area&amp;gt;, &amp;lt;base&amp;gt;, &amp;lt;link&amp;gt;	Specifies the URL of the page the link goes to
    ///</summary>
    public static HtmlAttribute href(string value) => new HtmlAttribute(Keys.href, value);
    ///<summary>
    /// &amp;lt;a&amp;gt;, &amp;lt;area&amp;gt;, &amp;lt;link&amp;gt;	Specifies the language of the linked document
    ///</summary>
    public static HtmlAttribute hreflang(string value) => new HtmlAttribute(Keys.hreflang, value);
    ///<summary>
    /// Global Attributes	Specifies a unique id for an element
    ///</summary>
    public static HtmlAttribute id(string value) => new HtmlAttribute(Keys.id, value);
    ///<summary>
    /// &amp;lt;img&amp;gt;	Specifies an image as a server-side image map
    ///</summary>
    public static HtmlAttribute ismap(string value) => new HtmlAttribute(Keys.ismap, value);
    ///<summary>
    /// &amp;lt;track&amp;gt;	Specifies the kind of text track
    ///</summary>
    public static HtmlAttribute kind(string value) => new HtmlAttribute(Keys.kind, value);
    ///<summary>
    /// &amp;lt;track&amp;gt;, &amp;lt;option&amp;gt;, &amp;lt;optgroup&amp;gt;	Specifies the title of the text track
    ///</summary>
    public static HtmlAttribute label(string value) => new HtmlAttribute(Keys.label, value);
    ///<summary>
    /// Global Attributes	Specifies the language of the element's content
    ///</summary>
    public static HtmlAttribute lang(string value) => new HtmlAttribute(Keys.lang, value);
    ///<summary>
    /// &amp;lt;input&amp;gt;	Refers to a &amp;lt;datalist&amp;gt; element that contains pre-defined options for an &amp;lt;input&amp;gt; element
    ///</summary>
    public static HtmlAttribute list(string value) => new HtmlAttribute(Keys.list, value);
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Specifies that the audio/video will start over again, every time it is finished
    ///</summary>
    public static HtmlAttribute loop(string value) => new HtmlAttribute(Keys.loop, value);
    ///<summary>
    /// &amp;lt;meter&amp;gt;	Specifies the range that is considered to be a low value
    ///</summary>
    public static HtmlAttribute low(string value) => new HtmlAttribute(Keys.low, value);
    ///<summary>
    /// &amp;lt;input&amp;gt;, &amp;lt;meter&amp;gt;, &amp;lt;progress&amp;gt;	Specifies the maximum value
    ///</summary>
    public static HtmlAttribute max(string value) => new HtmlAttribute(Keys.max, value);
    ///<summary>
    /// &amp;lt;input&amp;gt;, &amp;lt;textarea&amp;gt;	Specifies the maximum number of characters allowed in an element
    ///</summary>
    public static HtmlAttribute maxlength(string value) => new HtmlAttribute(Keys.maxlength, value);
    ///<summary>
    /// &amp;lt;a&amp;gt;, &amp;lt;area&amp;gt;, &amp;lt;link&amp;gt;, &amp;lt;source&amp;gt;, &amp;lt;style&amp;gt;	Specifies what media/device the linked document is optimized for
    ///</summary>
    public static HtmlAttribute media(string value) => new HtmlAttribute(Keys.media, value);
    ///<summary>
    /// &amp;lt;form&amp;gt;	Specifies the HTTP method to use when sending form-data
    ///</summary>
    public static HtmlAttribute method(string value) => new HtmlAttribute(Keys.method, value);
    ///<summary>
    /// &amp;lt;input&amp;gt;, &amp;lt;meter&amp;gt;	Specifies a minimum value
    ///</summary>
    public static HtmlAttribute min(string value) => new HtmlAttribute(Keys.min, value);
    ///<summary>
    /// &amp;lt;input&amp;gt;, &amp;lt;select&amp;gt;	Specifies that a user can enter more than one value
    ///</summary>
    public static HtmlAttribute multiple(string value) => new HtmlAttribute(Keys.multiple, value);
    ///<summary>
    /// &amp;lt;video&amp;gt;, &amp;lt;audio&amp;gt;	Specifies that the audio output of the video should be muted
    ///</summary>
    public static HtmlAttribute muted(string value) => new HtmlAttribute(Keys.muted, value);
    ///<summary>
    /// &amp;lt;button&amp;gt;, &amp;lt;fieldset&amp;gt;, &amp;lt;form&amp;gt;, &amp;lt;iframe&amp;gt;, &amp;lt;input&amp;gt;, &amp;lt;map&amp;gt;, &amp;lt;meta&amp;gt;, &amp;lt;object&amp;gt;, &amp;lt;output&amp;gt;, &amp;lt;param&amp;gt;, &amp;lt;select&amp;gt;, &amp;lt;textarea&amp;gt;	Specifies the name of the element
    ///</summary>
    public static HtmlAttribute name(string value) => new HtmlAttribute(Keys.name, value);
    ///<summary>
    /// &amp;lt;form&amp;gt;	Specifies that the form should not be validated when submitted
    ///</summary>
    public static HtmlAttribute novalidate(string value) => new HtmlAttribute(Keys.novalidate, value);
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;embed&amp;gt;, &amp;lt;img&amp;gt;, &amp;lt;object&amp;gt;, &amp;lt;video&amp;gt;	Script to be run on abort
    ///</summary>
    public static HtmlAttribute onabort(string value) => new HtmlAttribute(Keys.onabort, value);
    ///<summary>
    /// &amp;lt;body&amp;gt;	Script to be run after the document is printed
    ///</summary>
    public static HtmlAttribute onafterprint(string value) => new HtmlAttribute(Keys.onafterprint, value);
    ///<summary>
    /// &amp;lt;body&amp;gt;	Script to be run before the document is printed
    ///</summary>
    public static HtmlAttribute onbeforeprint(string value) => new HtmlAttribute(Keys.onbeforeprint, value);
    ///<summary>
    /// &amp;lt;body&amp;gt;	Script to be run when the document is about to be unloaded
    ///</summary>
    public static HtmlAttribute onbeforeunload(string value) => new HtmlAttribute(Keys.onbeforeunload, value);
    ///<summary>
    /// All visible elements.	Script to be run when the element loses focus
    ///</summary>
    public static HtmlAttribute onblur(string value) => new HtmlAttribute(Keys.onblur, value);
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;embed&amp;gt;, &amp;lt;object&amp;gt;, &amp;lt;video&amp;gt;	Script to be run when a file is ready to start playing (when it has buffered enough to begin)
    ///</summary>
    public static HtmlAttribute oncanplay(string value) => new HtmlAttribute(Keys.oncanplay, value);
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Script to be run when a file can be played all the way to the end without pausing for buffering
    ///</summary>
    public static HtmlAttribute oncanplaythrough(string value) => new HtmlAttribute(Keys.oncanplaythrough, value);
    ///<summary>
    /// All visible elements.	Script to be run when the value of the element is changed
    ///</summary>
    public static HtmlAttribute onchange(string value) => new HtmlAttribute(Keys.onchange, value);
    ///<summary>
    /// All visible elements.	Script to be run when the element is being clicked
    ///</summary>
    public static HtmlAttribute onclick(string value) => new HtmlAttribute(Keys.onclick, value);
    ///<summary>
    /// All visible elements.	Script to be run when a context menu is triggered
    ///</summary>
    public static HtmlAttribute oncontextmenu(string value) => new HtmlAttribute(Keys.oncontextmenu, value);
    ///<summary>
    /// All visible elements.	Script to be run when the content of the element is being copied
    ///</summary>
    public static HtmlAttribute oncopy(string value) => new HtmlAttribute(Keys.oncopy, value);
    ///<summary>
    /// &amp;lt;track&amp;gt;	Script to be run when the cue changes in a &amp;lt;track&amp;gt; element
    ///</summary>
    public static HtmlAttribute oncuechange(string value) => new HtmlAttribute(Keys.oncuechange, value);
    ///<summary>
    /// All visible elements.	Script to be run when the content of the element is being cut
    ///</summary>
    public static HtmlAttribute oncut(string value) => new HtmlAttribute(Keys.oncut, value);
    ///<summary>
    /// All visible elements.	Script to be run when the element is being double-clicked
    ///</summary>
    public static HtmlAttribute ondblclick(string value) => new HtmlAttribute(Keys.ondblclick, value);
    ///<summary>
    /// All visible elements.	Script to be run when the element is being dragged
    ///</summary>
    public static HtmlAttribute ondrag(string value) => new HtmlAttribute(Keys.ondrag, value);
    ///<summary>
    /// All visible elements.	Script to be run at the end of a drag operation
    ///</summary>
    public static HtmlAttribute ondragend(string value) => new HtmlAttribute(Keys.ondragend, value);
    ///<summary>
    /// All visible elements.	Script to be run when an element has been dragged to a valid drop target
    ///</summary>
    public static HtmlAttribute ondragenter(string value) => new HtmlAttribute(Keys.ondragenter, value);
    ///<summary>
    /// All visible elements.	Script to be run when an element leaves a valid drop target
    ///</summary>
    public static HtmlAttribute ondragleave(string value) => new HtmlAttribute(Keys.ondragleave, value);
    ///<summary>
    /// All visible elements.	Script to be run when an element is being dragged over a valid drop target
    ///</summary>
    public static HtmlAttribute ondragover(string value) => new HtmlAttribute(Keys.ondragover, value);
    ///<summary>
    /// All visible elements.	Script to be run at the start of a drag operation
    ///</summary>
    public static HtmlAttribute ondragstart(string value) => new HtmlAttribute(Keys.ondragstart, value);
    ///<summary>
    /// All visible elements.	Script to be run when dragged element is being dropped
    ///</summary>
    public static HtmlAttribute ondrop(string value) => new HtmlAttribute(Keys.ondrop, value);
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Script to be run when the length of the media changes
    ///</summary>
    public static HtmlAttribute ondurationchange(string value) => new HtmlAttribute(Keys.ondurationchange, value);
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Script to be run when something bad happens and the file is suddenly unavailable (like unexpectedly disconnects)
    ///</summary>
    public static HtmlAttribute onemptied(string value) => new HtmlAttribute(Keys.onemptied, value);
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Script to be run when the media has reach the end (a useful event for messages like "thanks for listening")
    ///</summary>
    public static HtmlAttribute onended(string value) => new HtmlAttribute(Keys.onended, value);
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;body&amp;gt;, &amp;lt;embed&amp;gt;, &amp;lt;img&amp;gt;, &amp;lt;object&amp;gt;, &amp;lt;script&amp;gt;, &amp;lt;style&amp;gt;, &amp;lt;video&amp;gt;	Script to be run when an error occurs
    ///</summary>
    public static HtmlAttribute onerror(string value) => new HtmlAttribute(Keys.onerror, value);
    ///<summary>
    /// All visible elements.	Script to be run when the element gets focus
    ///</summary>
    public static HtmlAttribute onfocus(string value) => new HtmlAttribute(Keys.onfocus, value);
    ///<summary>
    /// &amp;lt;body&amp;gt;	Script to be run when there has been changes to the anchor part of the a URL
    ///</summary>
    public static HtmlAttribute onhashchange(string value) => new HtmlAttribute(Keys.onhashchange, value);
    ///<summary>
    /// All visible elements.	Script to be run when the element gets user input
    ///</summary>
    public static HtmlAttribute oninput(string value) => new HtmlAttribute(Keys.oninput, value);
    ///<summary>
    /// All visible elements.	Script to be run when the element is invalid
    ///</summary>
    public static HtmlAttribute oninvalid(string value) => new HtmlAttribute(Keys.oninvalid, value);
    ///<summary>
    /// All visible elements.	Script to be run when a user is pressing a key
    ///</summary>
    public static HtmlAttribute onkeydown(string value) => new HtmlAttribute(Keys.onkeydown, value);
    ///<summary>
    /// All visible elements.	Script to be run when a user presses a key
    ///</summary>
    public static HtmlAttribute onkeypress(string value) => new HtmlAttribute(Keys.onkeypress, value);
    ///<summary>
    /// All visible elements.	Script to be run when a user releases a key
    ///</summary>
    public static HtmlAttribute onkeyup(string value) => new HtmlAttribute(Keys.onkeyup, value);
    ///<summary>
    /// &amp;lt;body&amp;gt;, &amp;lt;iframe&amp;gt;, &amp;lt;img&amp;gt;, &amp;lt;input&amp;gt;, &amp;lt;link&amp;gt;, &amp;lt;script&amp;gt;, &amp;lt;style&amp;gt;	Script to be run when the element is finished loading
    ///</summary>
    public static HtmlAttribute onload(string value) => new HtmlAttribute(Keys.onload, value);
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Script to be run when media data is loaded
    ///</summary>
    public static HtmlAttribute onloadeddata(string value) => new HtmlAttribute(Keys.onloadeddata, value);
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Script to be run when meta data (like dimensions and duration) are loaded
    ///</summary>
    public static HtmlAttribute onloadedmetadata(string value) => new HtmlAttribute(Keys.onloadedmetadata, value);
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Script to be run just as the file begins to load before anything is actually loaded
    ///</summary>
    public static HtmlAttribute onloadstart(string value) => new HtmlAttribute(Keys.onloadstart, value);
    ///<summary>
    /// All visible elements.	Script to be run when a mouse button is pressed down on an element
    ///</summary>
    public static HtmlAttribute onmousedown(string value) => new HtmlAttribute(Keys.onmousedown, value);
    ///<summary>
    /// All visible elements.	Script to be run as long as the  mouse pointer is moving over an element
    ///</summary>
    public static HtmlAttribute onmousemove(string value) => new HtmlAttribute(Keys.onmousemove, value);
    ///<summary>
    /// All visible elements.	Script to be run when a mouse pointer moves out of an element
    ///</summary>
    public static HtmlAttribute onmouseout(string value) => new HtmlAttribute(Keys.onmouseout, value);
    ///<summary>
    /// All visible elements.	Script to be run when a mouse pointer moves over an element
    ///</summary>
    public static HtmlAttribute onmouseover(string value) => new HtmlAttribute(Keys.onmouseover, value);
    ///<summary>
    /// All visible elements.	Script to be run when a mouse button is released over an element
    ///</summary>
    public static HtmlAttribute onmouseup(string value) => new HtmlAttribute(Keys.onmouseup, value);
    ///<summary>
    /// All visible elements.	Script to be run when a mouse wheel is being scrolled over an element
    ///</summary>
    public static HtmlAttribute onmousewheel(string value) => new HtmlAttribute(Keys.onmousewheel, value);
    ///<summary>
    /// &amp;lt;body&amp;gt;	Script to be run when the browser starts to work offline
    ///</summary>
    public static HtmlAttribute onoffline(string value) => new HtmlAttribute(Keys.onoffline, value);
    ///<summary>
    /// &amp;lt;body&amp;gt;	Script to be run when the browser starts to work online
    ///</summary>
    public static HtmlAttribute ononline(string value) => new HtmlAttribute(Keys.ononline, value);
    ///<summary>
    /// &amp;lt;body&amp;gt;	Script to be run when a user navigates away from a page
    ///</summary>
    public static HtmlAttribute onpagehide(string value) => new HtmlAttribute(Keys.onpagehide, value);
    ///<summary>
    /// &amp;lt;body&amp;gt;	Script to be run when a user navigates to a page
    ///</summary>
    public static HtmlAttribute onpageshow(string value) => new HtmlAttribute(Keys.onpageshow, value);
    ///<summary>
    /// All visible elements.	Script to be run when the user pastes some content in an element
    ///</summary>
    public static HtmlAttribute onpaste(string value) => new HtmlAttribute(Keys.onpaste, value);
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Script to be run when the media is paused either by the user or programmatically
    ///</summary>
    public static HtmlAttribute onpause(string value) => new HtmlAttribute(Keys.onpause, value);
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Script to be run when the media has started playing
    ///</summary>
    public static HtmlAttribute onplay(string value) => new HtmlAttribute(Keys.onplay, value);
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Script to be run when the media has started playing
    ///</summary>
    public static HtmlAttribute onplaying(string value) => new HtmlAttribute(Keys.onplaying, value);
    ///<summary>
    /// &amp;lt;body&amp;gt;	Script to be run when the window's history changes.
    ///</summary>
    public static HtmlAttribute onpopstate(string value) => new HtmlAttribute(Keys.onpopstate, value);
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Script to be run when the browser is in the process of getting the media data
    ///</summary>
    public static HtmlAttribute onprogress(string value) => new HtmlAttribute(Keys.onprogress, value);
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Script to be run each time the playback rate changes (like when a user switches to a slow motion or fast forward mode).
    ///</summary>
    public static HtmlAttribute onratechange(string value) => new HtmlAttribute(Keys.onratechange, value);
    ///<summary>
    /// &amp;lt;form&amp;gt;	Script to be run when a reset button in a form is clicked.
    ///</summary>
    public static HtmlAttribute onreset(string value) => new HtmlAttribute(Keys.onreset, value);
    ///<summary>
    /// &amp;lt;body&amp;gt;	Script to be run when the browser window is being resized.
    ///</summary>
    public static HtmlAttribute onresize(string value) => new HtmlAttribute(Keys.onresize, value);
    ///<summary>
    /// All visible elements.	Script to be run when an element's scrollbar is being scrolled
    ///</summary>
    public static HtmlAttribute onscroll(string value) => new HtmlAttribute(Keys.onscroll, value);
    ///<summary>
    /// &amp;lt;input&amp;gt;	Script to be run when the user writes something in a search field (for &amp;lt;input type=&amp;quot;search&amp;quot;&amp;gt;)
    ///</summary>
    public static HtmlAttribute onsearch(string value) => new HtmlAttribute(Keys.onsearch, value);
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Script to be run when the seeking attribute is set to false indicating that seeking has ended
    ///</summary>
    public static HtmlAttribute onseeked(string value) => new HtmlAttribute(Keys.onseeked, value);
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Script to be run when the seeking attribute is set to true indicating that seeking is active
    ///</summary>
    public static HtmlAttribute onseeking(string value) => new HtmlAttribute(Keys.onseeking, value);
    ///<summary>
    /// All visible elements.	Script to be run when the element gets selected
    ///</summary>
    public static HtmlAttribute onselect(string value) => new HtmlAttribute(Keys.onselect, value);
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Script to be run when the browser is unable to fetch the media data for whatever reason
    ///</summary>
    public static HtmlAttribute onstalled(string value) => new HtmlAttribute(Keys.onstalled, value);
    ///<summary>
    /// &amp;lt;body&amp;gt;	Script to be run when a Web Storage area is updated
    ///</summary>
    public static HtmlAttribute onstorage(string value) => new HtmlAttribute(Keys.onstorage, value);
    ///<summary>
    /// &amp;lt;form&amp;gt;	Script to be run when a form is submitted
    ///</summary>
    public static HtmlAttribute onsubmit(string value) => new HtmlAttribute(Keys.onsubmit, value);
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Script to be run when fetching the media data is stopped before it is completely loaded for whatever reason
    ///</summary>
    public static HtmlAttribute onsuspend(string value) => new HtmlAttribute(Keys.onsuspend, value);
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Script to be run when the playing position has changed (like when the user fast forwards to a different point in the media)
    ///</summary>
    public static HtmlAttribute ontimeupdate(string value) => new HtmlAttribute(Keys.ontimeupdate, value);
    ///<summary>
    /// &amp;lt;details&amp;gt;	Script to be run when the user opens or closes the &amp;lt;details&amp;gt; element
    ///</summary>
    public static HtmlAttribute ontoggle(string value) => new HtmlAttribute(Keys.ontoggle, value);
    ///<summary>
    /// &amp;lt;body&amp;gt;	Script to be run when a page has unloaded (or the browser window has been closed)
    ///</summary>
    public static HtmlAttribute onunload(string value) => new HtmlAttribute(Keys.onunload, value);
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Script to be run each time the volume of a video/audio has been changed
    ///</summary>
    public static HtmlAttribute onvolumechange(string value) => new HtmlAttribute(Keys.onvolumechange, value);
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Script to be run when the media has paused but is expected to resume (like when the media pauses to buffer more data)
    ///</summary>
    public static HtmlAttribute onwaiting(string value) => new HtmlAttribute(Keys.onwaiting, value);
    ///<summary>
    /// All visible elements.	Script to be run when the mouse wheel rolls up or down over an element
    ///</summary>
    public static HtmlAttribute onwheel(string value) => new HtmlAttribute(Keys.onwheel, value);
    ///<summary>
    /// &amp;lt;details&amp;gt;	Specifies that the details should be visible (open) to the user
    ///</summary>
    public static HtmlAttribute open(string value) => new HtmlAttribute(Keys.open, value);
    ///<summary>
    /// &amp;lt;meter&amp;gt;	Specifies what value is the optimal value for the gauge
    ///</summary>
    public static HtmlAttribute optimum(string value) => new HtmlAttribute(Keys.optimum, value);
    ///<summary>
    /// &amp;lt;input&amp;gt;	Specifies a regular expression that an &amp;lt;input&amp;gt; element's value is checked against
    ///</summary>
    public static HtmlAttribute pattern(string value) => new HtmlAttribute(Keys.pattern, value);
    ///<summary>
    /// &amp;lt;input&amp;gt;, &amp;lt;textarea&amp;gt;	Specifies a short hint that describes the expected value of the element
    ///</summary>
    public static HtmlAttribute placeholder(string value) => new HtmlAttribute(Keys.placeholder, value);
    ///<summary>
    /// &amp;lt;video&amp;gt;	Specifies an image to be shown while the video is downloading, or until the user hits the play button
    ///</summary>
    public static HtmlAttribute poster(string value) => new HtmlAttribute(Keys.poster, value);
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Specifies if and how the author thinks the audio/video should be loaded when the page loads
    ///</summary>
    public static HtmlAttribute preload(string value) => new HtmlAttribute(Keys.preload, value);
    ///<summary>
    /// &amp;lt;input&amp;gt;, &amp;lt;textarea&amp;gt;	Specifies that the element is read-only
    ///</summary>
    public static HtmlAttribute @readonly(string value) => new HtmlAttribute(Keys.@readonly, value);
    ///<summary>
    /// &amp;lt;a&amp;gt;, &amp;lt;area&amp;gt;, &amp;lt;form&amp;gt;, &amp;lt;link&amp;gt;	Specifies the relationship between the current document and the linked document
    ///</summary>
    public static HtmlAttribute rel(string value) => new HtmlAttribute(Keys.rel, value);
    ///<summary>
    /// &amp;lt;input&amp;gt;, &amp;lt;select&amp;gt;, &amp;lt;textarea&amp;gt;	Specifies that the element must be filled out before submitting the form
    ///</summary>
    public static HtmlAttribute required(string value) => new HtmlAttribute(Keys.required, value);
    ///<summary>
    /// &amp;lt;ol&amp;gt;	Specifies that the list order should be descending (9,8,7...)
    ///</summary>
    public static HtmlAttribute reversed(string value) => new HtmlAttribute(Keys.reversed, value);
    ///<summary>
    /// &amp;lt;textarea&amp;gt;	Specifies the visible number of lines in a text area
    ///</summary>
    public static HtmlAttribute rows(string value) => new HtmlAttribute(Keys.rows, value);
    ///<summary>
    /// &amp;lt;td&amp;gt;, &amp;lt;th&amp;gt;	Specifies the number of rows a table cell should span
    ///</summary>
    public static HtmlAttribute rowspan(string value) => new HtmlAttribute(Keys.rowspan, value);
    ///<summary>
    /// &amp;lt;iframe&amp;gt;	Enables an extra set of restrictions for the content in an &amp;lt;iframe&amp;gt;
    ///</summary>
    public static HtmlAttribute sandbox(string value) => new HtmlAttribute(Keys.sandbox, value);
    ///<summary>
    /// &amp;lt;th&amp;gt;	Specifies whether a header cell is a header for a column, row, or group of columns or rows
    ///</summary>
    public static HtmlAttribute scope(string value) => new HtmlAttribute(Keys.scope, value);
    ///<summary>
    /// &amp;lt;option&amp;gt;	Specifies that an option should be pre-selected when the page loads
    ///</summary>
    public static HtmlAttribute selected(string value) => new HtmlAttribute(Keys.selected, value);
    ///<summary>
    /// &amp;lt;area&amp;gt;	Specifies the shape of the area
    ///</summary>
    public static HtmlAttribute shape(string value) => new HtmlAttribute(Keys.shape, value);
    ///<summary>
    /// &amp;lt;input&amp;gt;, &amp;lt;select&amp;gt;	Specifies the width, in characters (for &amp;lt;input&amp;gt;) or specifies the number of visible options (for &amp;lt;select&amp;gt;)
    ///</summary>
    public static HtmlAttribute size(string value) => new HtmlAttribute(Keys.size, value);
    ///<summary>
    /// &amp;lt;img&amp;gt;, &amp;lt;link&amp;gt;, &amp;lt;source&amp;gt;	Specifies the size of the linked resource
    ///</summary>
    public static HtmlAttribute sizes(string value) => new HtmlAttribute(Keys.sizes, value);
    ///<summary>
    /// &amp;lt;col&amp;gt;, &amp;lt;colgroup&amp;gt;	Specifies the number of columns to span
    ///</summary>
    public static HtmlAttribute span(string value) => new HtmlAttribute(Keys.span, value);
    ///<summary>
    /// Global Attributes	Specifies whether the element is to have its spelling and grammar checked or not
    ///</summary>
    public static HtmlAttribute spellcheck(string value) => new HtmlAttribute(Keys.spellcheck, value);
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;embed&amp;gt;, &amp;lt;iframe&amp;gt;, &amp;lt;img&amp;gt;, &amp;lt;input&amp;gt;, &amp;lt;script&amp;gt;, &amp;lt;source&amp;gt;, &amp;lt;track&amp;gt;, &amp;lt;video&amp;gt;	Specifies the URL of the media file
    ///</summary>
    public static HtmlAttribute src(string value) => new HtmlAttribute(Keys.src, value);
    ///<summary>
    /// &amp;lt;iframe&amp;gt;	Specifies the HTML content of the page to show in the &amp;lt;iframe&amp;gt;
    ///</summary>
    public static HtmlAttribute srcdoc(string value) => new HtmlAttribute(Keys.srcdoc, value);
    ///<summary>
    /// &amp;lt;track&amp;gt;	Specifies the language of the track text data (required if kind="subtitles")
    ///</summary>
    public static HtmlAttribute srclang(string value) => new HtmlAttribute(Keys.srclang, value);
    ///<summary>
    /// &amp;lt;img&amp;gt;, &amp;lt;source&amp;gt;	Specifies the URL of the image to use in different situations
    ///</summary>
    public static HtmlAttribute srcset(string value) => new HtmlAttribute(Keys.srcset, value);
    ///<summary>
    /// &amp;lt;ol&amp;gt;	Specifies the start value of an ordered list
    ///</summary>
    public static HtmlAttribute start(string value) => new HtmlAttribute(Keys.start, value);
    ///<summary>
    /// &amp;lt;input&amp;gt;	Specifies the legal number intervals for an input field
    ///</summary>
    public static HtmlAttribute step(string value) => new HtmlAttribute(Keys.step, value);
    ///<summary>
    /// Global Attributes	Specifies an inline CSS style for an element
    ///</summary>
    public static HtmlAttribute style(string value) => new HtmlAttribute(Keys.style, value);
    ///<summary>
    /// Global Attributes	Specifies the tabbing order of an element
    ///</summary>
    public static HtmlAttribute tabindex(string value) => new HtmlAttribute(Keys.tabindex, value);
    ///<summary>
    /// &amp;lt;a&amp;gt;, &amp;lt;area&amp;gt;, &amp;lt;base&amp;gt;, &amp;lt;form&amp;gt;	Specifies the target for where to open the linked document or where to submit the form
    ///</summary>
    public static HtmlAttribute target(string value) => new HtmlAttribute(Keys.target, value);
    ///<summary>
    /// Global Attributes	Specifies extra information about an element
    ///</summary>
    public static HtmlAttribute title(string value) => new HtmlAttribute(Keys.title, value);
    ///<summary>
    /// Global Attributes	Specifies whether the content of an element should be translated or not
    ///</summary>
    public static HtmlAttribute translate(string value) => new HtmlAttribute(Keys.translate, value);
    ///<summary>
    /// &amp;lt;a&amp;gt;, &amp;lt;button&amp;gt;, &amp;lt;embed&amp;gt;, &amp;lt;input&amp;gt;, &amp;lt;link&amp;gt;, &amp;lt;menu&amp;gt;, &amp;lt;object&amp;gt;, &amp;lt;script&amp;gt;, &amp;lt;source&amp;gt;, &amp;lt;style&amp;gt;	Specifies the type of element
    ///</summary>
    public static HtmlAttribute type(string value) => new HtmlAttribute(Keys.type, value);
    ///<summary>
    /// &amp;lt;img&amp;gt;, &amp;lt;object&amp;gt;	Specifies an image as a client-side image map
    ///</summary>
    public static HtmlAttribute usemap(string value) => new HtmlAttribute(Keys.usemap, value);
    ///<summary>
    /// &amp;lt;button&amp;gt;, &amp;lt;input&amp;gt;, &amp;lt;li&amp;gt;, &amp;lt;option&amp;gt;, &amp;lt;meter&amp;gt;, &amp;lt;progress&amp;gt;, &amp;lt;param&amp;gt;	Specifies the value of the element
    ///</summary>
    public static HtmlAttribute value(string value) => new HtmlAttribute(Keys.value, value);
    ///<summary>
    /// &amp;lt;canvas&amp;gt;, &amp;lt;embed&amp;gt;, &amp;lt;iframe&amp;gt;, &amp;lt;img&amp;gt;, &amp;lt;input&amp;gt;, &amp;lt;object&amp;gt;, &amp;lt;video&amp;gt;	Specifies the width of the element
    ///</summary>
    public static HtmlAttribute width(string value) => new HtmlAttribute(Keys.width, value);
    ///<summary>
    /// &amp;lt;textarea&amp;gt;	Specifies how the text in a text area is to be wrapped when submitted in a form
    ///</summary>
    public static HtmlAttribute wrap(string value) => new HtmlAttribute(Keys.wrap, value);
    ///<summary>
    /// &amp;lt;iframe&amp;gt; Specifies a Permissions Policy for the &amp;lt;iframe&amp;gt;. The policy defines what features are available to the &amp;lt;iframe&amp;gt; (for example, access to the microphone, camera, battery, web-share, etc.) based on the origin of the request.
    ///</summary>
    public static HtmlAttribute allow(string value) => new HtmlAttribute(Keys.allow, value);
    ///<summary>
    /// &amp;lt;iframe&amp;gt; Set to true if the &amp;lt;iframe&amp;gt; can activate fullscreen mode by calling the requestFullscreen() method.
    ///</summary>
    public static HtmlAttribute allowfullscreen(string value) => new HtmlAttribute(Keys.allowfullscreen, value);
    ///<summary>
    /// &amp;lt;iframe&amp;gt; [Deprecated Non-standard] Set to true if a cross-origin &amp;lt;iframe&amp;gt; should be allowed to invoke the Payment Request API.
    ///</summary>
    public static HtmlAttribute allowpaymentrequest(string value) => new HtmlAttribute(Keys.allowpaymentrequest, value);
    ///<summary>
    /// &amp;lt;iframe&amp;gt; [Experimental, Non-standard] Set to true to make the &amp;lt;iframe&amp;gt; credentialless, meaning that its content will be loaded in a new, ephemeral context. It doesn't have access to the network, cookies, and storage data associated with its origin. It uses a new context local to the top-level document lifetime. In return, the Cross-Origin-Embedder-Policy (COEP) embedding rules can be lifted, so documents with COEP set can embed third-party documents that do not. See IFrame credentialless for more details.
    ///</summary>
    public static HtmlAttribute credentialless(string value) => new HtmlAttribute(Keys.credentialless, value);
    ///<summary>
    /// &amp;lt;iframe&amp;gt; [Experimental] A Content Security Policy enforced for the embedded resource. See HTMLIFrameElement.csp for details.
    ///</summary>
    public static HtmlAttribute csp(string value) => new HtmlAttribute(Keys.csp, value);
    ///<summary>
    /// &amp;lt;iframe&amp;gt; Indicates when the browser should load the iframe.
    ///</summary>
    public static HtmlAttribute loading(string value) => new HtmlAttribute(Keys.loading, value);
    ///<summary>
    /// &amp;lt;iframe&amp;gt; Indicates which referrer to send when fetching the frame's resource:
    ///</summary>
    public static HtmlAttribute referrerpolicy(string value) => new HtmlAttribute(Keys.referrerpolicy, value);
}