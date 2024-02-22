namespace CC.CSX;
/// <summary>
/// All available HTML attribute keys.
/// </summary>
public static class HtmlAttributeKeys
{
    ///<summary>
    /// accept	&amp;lt;input&amp;gt;	Specifies the types of files that the server accepts (only for type="file")
    ///</summary>
    public const string accept = "accept";
    ///<summary>
    /// Global Attributes	Specifies a shortcut key to activate/focus an element
    ///</summary>
    public const string accesskey = "accesskey";
    ///<summary>
    /// &amp;lt;form&amp;gt;	Specifies where to send the form-data when a form is submitted
    ///</summary>
    public const string action = "action";
    ///<summary>
    /// Not supported in HTML 5.	Specifies the alignment according to surrounding elements. Use CSS instead
    ///</summary>
    public const string align = "align";
    ///<summary>
    /// &amp;lt;area&amp;gt;, &amp;lt;img&amp;gt;, &amp;lt;input&amp;gt;	Specifies an alternate text when the original element fails to display
    ///</summary>
    public const string alt = "alt";
    ///<summary>
    /// &amp;lt;script&amp;gt;	Specifies that the script is executed asynchronously (only for external scripts)
    ///</summary>
    public const string async = "async";
    ///<summary>
    /// &amp;lt;form&amp;gt;, &amp;lt;input&amp;gt;	Specifies whether the &amp;lt;form&amp;gt; or the &amp;lt;input&amp;gt; element should have autocomplete enabled
    ///</summary>
    public const string autocomplete = "autocomplete";
    ///<summary>
    /// &amp;lt;button&amp;gt;, &amp;lt;input&amp;gt;, &amp;lt;select&amp;gt;, &amp;lt;textarea&amp;gt;	Specifies that the element should automatically get focus when the page loads
    ///</summary>
    public const string autofocus = "autofocus";
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Specifies that the audio/video will start playing as soon as it is ready
    ///</summary>
    public const string autoplay = "autoplay";
    ///<summary>
    /// Not supported in HTML 5.	Specifies the background color of an element. Use CSS instead
    ///</summary>
    public const string bgcolor = "bgcolor";
    ///<summary>
    /// Not supported in HTML 5.	Specifies the width of the border of an element. Use CSS instead
    ///</summary>
    public const string border = "border";
    ///<summary>
    /// &amp;lt;meta&amp;gt;, &amp;lt;script&amp;gt;	Specifies the character encoding
    ///</summary>
    public const string charset = "charset";
    ///<summary>
    /// &amp;lt;input&amp;gt;	Specifies that an &amp;lt;input&amp;gt; element should be pre-selected when the page loads (for type="checkbox" or type="radio")
    ///</summary>
    public const string @checked = "checked";
    ///<summary>
    /// &amp;lt;blockquote&amp;gt;, &amp;lt;del&amp;gt;, &amp;lt;ins&amp;gt;, &amp;lt;q&amp;gt;	Specifies a URL which explains the quote/deleted/inserted text
    ///</summary>
    public const string cite = "cite";
    ///<summary>
    /// Global Attributes	Specifies one or more classnames for an element (refers to a class in a style sheet)
    ///</summary>
    public const string @class = "class";
    ///<summary>
    /// Not supported in HTML 5.	Specifies the text color of an element. Use CSS instead
    ///</summary>
    public const string color = "color";
    ///<summary>
    /// &amp;lt;textarea&amp;gt;	Specifies the visible width of a text area
    ///</summary>
    public const string cols = "cols";
    ///<summary>
    /// &amp;lt;td&amp;gt;, &amp;lt;th&amp;gt;	Specifies the number of columns a table cell should span
    ///</summary>
    public const string colspan = "colspan";
    ///<summary>
    /// &amp;lt;meta&amp;gt;	Gives the value associated with the http-equiv or name attribute
    ///</summary>
    public const string content = "content";
    ///<summary>
    /// Global Attributes	Specifies whether the content of an element is editable or not
    ///</summary>
    public const string contenteditable = "contenteditable";
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Specifies that audio/video controls should be displayed (such as a play/pause button etc)
    ///</summary>
    public const string controls = "controls";
    ///<summary>
    /// &amp;lt;area&amp;gt;	Specifies the coordinates of the area
    ///</summary>
    public const string coords = "coords";
    ///<summary>
    /// &amp;lt;object&amp;gt;	Specifies the URL of the resource to be used by the object
    ///</summary>
    public const string data = "data";
    ///<summary>
    /// &amp;lt;del&amp;gt;, &amp;lt;ins&amp;gt;, &amp;lt;time&amp;gt;	Specifies the date and time
    ///</summary>
    public const string datetime = "datetime";
    ///<summary>
    /// &amp;lt;track&amp;gt;	Specifies that the track is to be enabled if the user's preferences do not indicate that another track would be more appropriate
    ///</summary>
    public const string @default = "default";
    ///<summary>
    /// &amp;lt;script&amp;gt;	Specifies that the script is executed when the page has finished parsing (only for external scripts)
    ///</summary>
    public const string defer = "defer";
    ///<summary>
    /// Global Attributes	Specifies the text direction for the content in an element
    ///</summary>
    public const string dir = "dir";
    ///<summary>
    /// &amp;lt;input&amp;gt;, &amp;lt;textarea&amp;gt;	Specifies that the text direction will be submitted
    ///</summary>
    public const string dirname = "dirname";
    ///<summary>
    /// &amp;lt;button&amp;gt;, &amp;lt;fieldset&amp;gt;, &amp;lt;input&amp;gt;, &amp;lt;optgroup&amp;gt;, &amp;lt;option&amp;gt;, &amp;lt;select&amp;gt;, &amp;lt;textarea&amp;gt;	Specifies that the specified element/group of elements should be disabled
    ///</summary>
    public const string disabled = "disabled";
    ///<summary>
    /// &amp;lt;a&amp;gt;, &amp;lt;area&amp;gt;	Specifies that the target will be downloaded when a user clicks on the hyperlink
    ///</summary>
    public const string download = "download";
    ///<summary>
    /// Global Attributes	Specifies whether an element is draggable or not
    ///</summary>
    public const string draggable = "draggable";
    ///<summary>
    /// &amp;lt;form&amp;gt;	Specifies how the form-data should be encoded when submitting it to the server (only for method="post")
    ///</summary>
    public const string enctype = "enctype";
    ///<summary>
    /// &amp;lt;label&amp;gt;, &amp;lt;output&amp;gt;	Specifies which form element(s) a label/calculation is bound to
    ///</summary>
    public const string @for = "for";
    ///<summary>
    /// &amp;lt;button&amp;gt;, &amp;lt;fieldset&amp;gt;, &amp;lt;input&amp;gt;, &amp;lt;label&amp;gt;, &amp;lt;meter&amp;gt;, &amp;lt;object&amp;gt;, &amp;lt;output&amp;gt;, &amp;lt;select&amp;gt;, &amp;lt;textarea&amp;gt;	Specifies the name of the form the element belongs to
    ///</summary>
    public const string form = "form";
    ///<summary>
    /// &amp;lt;button&amp;gt;, &amp;lt;input&amp;gt;	Specifies where to send the form-data when a form is submitted. Only for type="submit"
    ///</summary>
    public const string formaction = "formaction";
    ///<summary>
    /// &amp;lt;td&amp;gt;, &amp;lt;th&amp;gt;	Specifies one or more headers cells a cell is related to
    ///</summary>
    public const string headers = "headers";
    ///<summary>
    /// &amp;lt;canvas&amp;gt;, &amp;lt;embed&amp;gt;, &amp;lt;iframe&amp;gt;, &amp;lt;img&amp;gt;, &amp;lt;input&amp;gt;, &amp;lt;object&amp;gt;, &amp;lt;video&amp;gt;	Specifies the height of the element
    ///</summary>
    public const string height = "height";
    ///<summary>
    /// Global Attributes	Specifies that an element is not yet, or is no longer, relevant
    ///</summary>
    public const string hidden = "hidden";
    ///<summary>
    /// &amp;lt;meter&amp;gt;	Specifies the range that is considered to be a high value
    ///</summary>
    public const string high = "high";
    ///<summary>
    /// &amp;lt;a&amp;gt;, &amp;lt;area&amp;gt;, &amp;lt;base&amp;gt;, &amp;lt;link&amp;gt;	Specifies the URL of the page the link goes to
    ///</summary>
    public const string href = "href";
    ///<summary>
    /// &amp;lt;a&amp;gt;, &amp;lt;area&amp;gt;, &amp;lt;link&amp;gt;	Specifies the language of the linked document
    ///</summary>
    public const string hreflang = "hreflang";
    ///<summary>
    /// Global Attributes	Specifies a unique id for an element
    ///</summary>
    public const string id = "id";
    ///<summary>
    /// &amp;lt;img&amp;gt;	Specifies an image as a server-side image map
    ///</summary>
    public const string ismap = "ismap";
    ///<summary>
    /// &amp;lt;track&amp;gt;	Specifies the kind of text track
    ///</summary>
    public const string kind = "kind";
    ///<summary>
    /// &amp;lt;track&amp;gt;, &amp;lt;option&amp;gt;, &amp;lt;optgroup&amp;gt;	Specifies the title of the text track
    ///</summary>
    public const string label = "label";
    ///<summary>
    /// Global Attributes	Specifies the language of the element's content
    ///</summary>
    public const string lang = "lang";
    ///<summary>
    /// &amp;lt;input&amp;gt;	Refers to a &amp;lt;datalist&amp;gt; element that contains pre-defined options for an &amp;lt;input&amp;gt; element
    ///</summary>
    public const string list = "list";
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Specifies that the audio/video will start over again, every time it is finished
    ///</summary>
    public const string loop = "loop";
    ///<summary>
    /// &amp;lt;meter&amp;gt;	Specifies the range that is considered to be a low value
    ///</summary>
    public const string low = "low";
    ///<summary>
    /// &amp;lt;input&amp;gt;, &amp;lt;meter&amp;gt;, &amp;lt;progress&amp;gt;	Specifies the maximum value
    ///</summary>
    public const string max = "max";
    ///<summary>
    /// &amp;lt;input&amp;gt;, &amp;lt;textarea&amp;gt;	Specifies the maximum number of characters allowed in an element
    ///</summary>
    public const string maxlength = "maxlength";
    ///<summary>
    /// &amp;lt;a&amp;gt;, &amp;lt;area&amp;gt;, &amp;lt;link&amp;gt;, &amp;lt;source&amp;gt;, &amp;lt;style&amp;gt;	Specifies what media/device the linked document is optimized for
    ///</summary>
    public const string media = "media";
    ///<summary>
    /// &amp;lt;form&amp;gt;	Specifies the HTTP method to use when sending form-data
    ///</summary>
    public const string method = "method";
    ///<summary>
    /// &amp;lt;input&amp;gt;, &amp;lt;meter&amp;gt;	Specifies a minimum value
    ///</summary>
    public const string min = "min";
    ///<summary>
    /// &amp;lt;input&amp;gt;, &amp;lt;select&amp;gt;	Specifies that a user can enter more than one value
    ///</summary>
    public const string multiple = "multiple";
    ///<summary>
    /// &amp;lt;video&amp;gt;, &amp;lt;audio&amp;gt;	Specifies that the audio output of the video should be muted
    ///</summary>
    public const string muted = "muted";
    ///<summary>
    /// &amp;lt;button&amp;gt;, &amp;lt;fieldset&amp;gt;, &amp;lt;form&amp;gt;, &amp;lt;iframe&amp;gt;, &amp;lt;input&amp;gt;, &amp;lt;map&amp;gt;, &amp;lt;meta&amp;gt;, &amp;lt;object&amp;gt;, &amp;lt;output&amp;gt;, &amp;lt;param&amp;gt;, &amp;lt;select&amp;gt;, &amp;lt;textarea&amp;gt;	Specifies the name of the element
    ///</summary>
    public const string name = "name";
    ///<summary>
    /// &amp;lt;form&amp;gt;	Specifies that the form should not be validated when submitted
    ///</summary>
    public const string novalidate = "novalidate";
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;embed&amp;gt;, &amp;lt;img&amp;gt;, &amp;lt;object&amp;gt;, &amp;lt;video&amp;gt;	Script to be run on abort
    ///</summary>
    public const string onabort = "onabort";
    ///<summary>
    /// &amp;lt;body&amp;gt;	Script to be run after the document is printed
    ///</summary>
    public const string onafterprint = "onafterprint";
    ///<summary>
    /// &amp;lt;body&amp;gt;	Script to be run before the document is printed
    ///</summary>
    public const string onbeforeprint = "onbeforeprint";
    ///<summary>
    /// &amp;lt;body&amp;gt;	Script to be run when the document is about to be unloaded
    ///</summary>
    public const string onbeforeunload = "onbeforeunload";
    ///<summary>
    /// All visible elements.	Script to be run when the element loses focus
    ///</summary>
    public const string onblur = "onblur";
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;embed&amp;gt;, &amp;lt;object&amp;gt;, &amp;lt;video&amp;gt;	Script to be run when a file is ready to start playing (when it has buffered enough to begin)
    ///</summary>
    public const string oncanplay = "oncanplay";
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Script to be run when a file can be played all the way to the end without pausing for buffering
    ///</summary>
    public const string oncanplaythrough = "oncanplaythrough";
    ///<summary>
    /// All visible elements.	Script to be run when the value of the element is changed
    ///</summary>
    public const string onchange = "onchange";
    ///<summary>
    /// All visible elements.	Script to be run when the element is being clicked
    ///</summary>
    public const string onclick = "onclick";
    ///<summary>
    /// All visible elements.	Script to be run when a context menu is triggered
    ///</summary>
    public const string oncontextmenu = "oncontextmenu";
    ///<summary>
    /// All visible elements.	Script to be run when the content of the element is being copied
    ///</summary>
    public const string oncopy = "oncopy";
    ///<summary>
    /// &amp;lt;track&amp;gt;	Script to be run when the cue changes in a &amp;lt;track&amp;gt; element
    ///</summary>
    public const string oncuechange = "oncuechange";
    ///<summary>
    /// All visible elements.	Script to be run when the content of the element is being cut
    ///</summary>
    public const string oncut = "oncut";
    ///<summary>
    /// All visible elements.	Script to be run when the element is being double-clicked
    ///</summary>
    public const string ondblclick = "ondblclick";
    ///<summary>
    /// All visible elements.	Script to be run when the element is being dragged
    ///</summary>
    public const string ondrag = "ondrag";
    ///<summary>
    /// All visible elements.	Script to be run at the end of a drag operation
    ///</summary>
    public const string ondragend = "ondragend";
    ///<summary>
    /// All visible elements.	Script to be run when an element has been dragged to a valid drop target
    ///</summary>
    public const string ondragenter = "ondragenter";
    ///<summary>
    /// All visible elements.	Script to be run when an element leaves a valid drop target
    ///</summary>
    public const string ondragleave = "ondragleave";
    ///<summary>
    /// All visible elements.	Script to be run when an element is being dragged over a valid drop target
    ///</summary>
    public const string ondragover = "ondragover";
    ///<summary>
    /// All visible elements.	Script to be run at the start of a drag operation
    ///</summary>
    public const string ondragstart = "ondragstart";
    ///<summary>
    /// All visible elements.	Script to be run when dragged element is being dropped
    ///</summary>
    public const string ondrop = "ondrop";
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Script to be run when the length of the media changes
    ///</summary>
    public const string ondurationchange = "ondurationchange";
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Script to be run when something bad happens and the file is suddenly unavailable (like unexpectedly disconnects)
    ///</summary>
    public const string onemptied = "onemptied";
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Script to be run when the media has reach the end (a useful event for messages like "thanks for listening")
    ///</summary>
    public const string onended = "onended";
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;body&amp;gt;, &amp;lt;embed&amp;gt;, &amp;lt;img&amp;gt;, &amp;lt;object&amp;gt;, &amp;lt;script&amp;gt;, &amp;lt;style&amp;gt;, &amp;lt;video&amp;gt;	Script to be run when an error occurs
    ///</summary>
    public const string onerror = "onerror";
    ///<summary>
    /// All visible elements.	Script to be run when the element gets focus
    ///</summary>
    public const string onfocus = "onfocus";
    ///<summary>
    /// &amp;lt;body&amp;gt;	Script to be run when there has been changes to the anchor part of the a URL
    ///</summary>
    public const string onhashchange = "onhashchange";
    ///<summary>
    /// All visible elements.	Script to be run when the element gets user input
    ///</summary>
    public const string oninput = "oninput";
    ///<summary>
    /// All visible elements.	Script to be run when the element is invalid
    ///</summary>
    public const string oninvalid = "oninvalid";
    ///<summary>
    /// All visible elements.	Script to be run when a user is pressing a key
    ///</summary>
    public const string onkeydown = "onkeydown";
    ///<summary>
    /// All visible elements.	Script to be run when a user presses a key
    ///</summary>
    public const string onkeypress = "onkeypress";
    ///<summary>
    /// All visible elements.	Script to be run when a user releases a key
    ///</summary>
    public const string onkeyup = "onkeyup";
    ///<summary>
    /// &amp;lt;body&amp;gt;, &amp;lt;iframe&amp;gt;, &amp;lt;img&amp;gt;, &amp;lt;input&amp;gt;, &amp;lt;link&amp;gt;, &amp;lt;script&amp;gt;, &amp;lt;style&amp;gt;	Script to be run when the element is finished loading
    ///</summary>
    public const string onload = "onload";
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Script to be run when media data is loaded
    ///</summary>
    public const string onloadeddata = "onloadeddata";
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Script to be run when meta data (like dimensions and duration) are loaded
    ///</summary>
    public const string onloadedmetadata = "onloadedmetadata";
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Script to be run just as the file begins to load before anything is actually loaded
    ///</summary>
    public const string onloadstart = "onloadstart";
    ///<summary>
    /// All visible elements.	Script to be run when a mouse button is pressed down on an element
    ///</summary>
    public const string onmousedown = "onmousedown";
    ///<summary>
    /// All visible elements.	Script to be run as long as the  mouse pointer is moving over an element
    ///</summary>
    public const string onmousemove = "onmousemove";
    ///<summary>
    /// All visible elements.	Script to be run when a mouse pointer moves out of an element
    ///</summary>
    public const string onmouseout = "onmouseout";
    ///<summary>
    /// All visible elements.	Script to be run when a mouse pointer moves over an element
    ///</summary>
    public const string onmouseover = "onmouseover";
    ///<summary>
    /// All visible elements.	Script to be run when a mouse button is released over an element
    ///</summary>
    public const string onmouseup = "onmouseup";
    ///<summary>
    /// All visible elements.	Script to be run when a mouse wheel is being scrolled over an element
    ///</summary>
    public const string onmousewheel = "onmousewheel";
    ///<summary>
    /// &amp;lt;body&amp;gt;	Script to be run when the browser starts to work offline
    ///</summary>
    public const string onoffline = "onoffline";
    ///<summary>
    /// &amp;lt;body&amp;gt;	Script to be run when the browser starts to work online
    ///</summary>
    public const string ononline = "ononline";
    ///<summary>
    /// &amp;lt;body&amp;gt;	Script to be run when a user navigates away from a page
    ///</summary>
    public const string onpagehide = "onpagehide";
    ///<summary>
    /// &amp;lt;body&amp;gt;	Script to be run when a user navigates to a page
    ///</summary>
    public const string onpageshow = "onpageshow";
    ///<summary>
    /// All visible elements.	Script to be run when the user pastes some content in an element
    ///</summary>
    public const string onpaste = "onpaste";
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Script to be run when the media is paused either by the user or programmatically
    ///</summary>
    public const string onpause = "onpause";
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Script to be run when the media has started playing
    ///</summary>
    public const string onplay = "onplay";
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Script to be run when the media has started playing
    ///</summary>
    public const string onplaying = "onplaying";
    ///<summary>
    /// &amp;lt;body&amp;gt;	Script to be run when the window's history changes.
    ///</summary>
    public const string onpopstate = "onpopstate";
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Script to be run when the browser is in the process of getting the media data
    ///</summary>
    public const string onprogress = "onprogress";
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Script to be run each time the playback rate changes (like when a user switches to a slow motion or fast forward mode).
    ///</summary>
    public const string onratechange = "onratechange";
    ///<summary>
    /// &amp;lt;form&amp;gt;	Script to be run when a reset button in a form is clicked.
    ///</summary>
    public const string onreset = "onreset";
    ///<summary>
    /// &amp;lt;body&amp;gt;	Script to be run when the browser window is being resized.
    ///</summary>
    public const string onresize = "onresize";
    ///<summary>
    /// All visible elements.	Script to be run when an element's scrollbar is being scrolled
    ///</summary>
    public const string onscroll = "onscroll";
    ///<summary>
    /// &amp;lt;input&amp;gt;	Script to be run when the user writes something in a search field (for &amp;lt;input type=&amp;quot;search&amp;quot;&amp;gt;)
    ///</summary>
    public const string onsearch = "onsearch";
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Script to be run when the seeking attribute is set to false indicating that seeking has ended
    ///</summary>
    public const string onseeked = "onseeked";
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Script to be run when the seeking attribute is set to true indicating that seeking is active
    ///</summary>
    public const string onseeking = "onseeking";
    ///<summary>
    /// All visible elements.	Script to be run when the element gets selected
    ///</summary>
    public const string onselect = "onselect";
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Script to be run when the browser is unable to fetch the media data for whatever reason
    ///</summary>
    public const string onstalled = "onstalled";
    ///<summary>
    /// &amp;lt;body&amp;gt;	Script to be run when a Web Storage area is updated
    ///</summary>
    public const string onstorage = "onstorage";
    ///<summary>
    /// &amp;lt;form&amp;gt;	Script to be run when a form is submitted
    ///</summary>
    public const string onsubmit = "onsubmit";
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Script to be run when fetching the media data is stopped before it is completely loaded for whatever reason
    ///</summary>
    public const string onsuspend = "onsuspend";
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Script to be run when the playing position has changed (like when the user fast forwards to a different point in the media)
    ///</summary>
    public const string ontimeupdate = "ontimeupdate";
    ///<summary>
    /// &amp;lt;details&amp;gt;	Script to be run when the user opens or closes the &amp;lt;details&amp;gt; element
    ///</summary>
    public const string ontoggle = "ontoggle";
    ///<summary>
    /// &amp;lt;body&amp;gt;	Script to be run when a page has unloaded (or the browser window has been closed)
    ///</summary>
    public const string onunload = "onunload";
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Script to be run each time the volume of a video/audio has been changed
    ///</summary>
    public const string onvolumechange = "onvolumechange";
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Script to be run when the media has paused but is expected to resume (like when the media pauses to buffer more data)
    ///</summary>
    public const string onwaiting = "onwaiting";
    ///<summary>
    /// All visible elements.	Script to be run when the mouse wheel rolls up or down over an element
    ///</summary>
    public const string onwheel = "onwheel";
    ///<summary>
    /// &amp;lt;details&amp;gt;	Specifies that the details should be visible (open) to the user
    ///</summary>
    public const string open = "open";
    ///<summary>
    /// &amp;lt;meter&amp;gt;	Specifies what value is the optimal value for the gauge
    ///</summary>
    public const string optimum = "optimum";
    ///<summary>
    /// &amp;lt;input&amp;gt;	Specifies a regular expression that an &amp;lt;input&amp;gt; element's value is checked against
    ///</summary>
    public const string pattern = "pattern";
    ///<summary>
    /// &amp;lt;input&amp;gt;, &amp;lt;textarea&amp;gt;	Specifies a short hint that describes the expected value of the element
    ///</summary>
    public const string placeholder = "placeholder";
    ///<summary>
    /// &amp;lt;video&amp;gt;	Specifies an image to be shown while the video is downloading, or until the user hits the play button
    ///</summary>
    public const string poster = "poster";
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;video&amp;gt;	Specifies if and how the author thinks the audio/video should be loaded when the page loads
    ///</summary>
    public const string preload = "preload";
    ///<summary>
    /// &amp;lt;input&amp;gt;, &amp;lt;textarea&amp;gt;	Specifies that the element is read-only
    ///</summary>
    public const string @readonly = "readonly";
    ///<summary>
    /// &amp;lt;a&amp;gt;, &amp;lt;area&amp;gt;, &amp;lt;form&amp;gt;, &amp;lt;link&amp;gt;	Specifies the relationship between the current document and the linked document
    ///</summary>
    public const string rel = "rel";
    ///<summary>
    /// &amp;lt;input&amp;gt;, &amp;lt;select&amp;gt;, &amp;lt;textarea&amp;gt;	Specifies that the element must be filled out before submitting the form
    ///</summary>
    public const string required = "required";
    ///<summary>
    /// &amp;lt;ol&amp;gt;	Specifies that the list order should be descending (9,8,7...)
    ///</summary>
    public const string reversed = "reversed";
    ///<summary>
    /// &amp;lt;textarea&amp;gt;	Specifies the visible number of lines in a text area
    ///</summary>
    public const string rows = "rows";
    ///<summary>
    /// &amp;lt;td&amp;gt;, &amp;lt;th&amp;gt;	Specifies the number of rows a table cell should span
    ///</summary>
    public const string rowspan = "rowspan";
    ///<summary>
    /// &amp;lt;iframe&amp;gt;	Enables an extra set of restrictions for the content in an &amp;lt;iframe&amp;gt;
    ///</summary>
    public const string sandbox = "sandbox";
    ///<summary>
    /// &amp;lt;th&amp;gt;	Specifies whether a header cell is a header for a column, row, or group of columns or rows
    ///</summary>
    public const string scope = "scope";
    ///<summary>
    /// &amp;lt;option&amp;gt;	Specifies that an option should be pre-selected when the page loads
    ///</summary>
    public const string selected = "selected";
    ///<summary>
    /// &amp;lt;area&amp;gt;	Specifies the shape of the area
    ///</summary>
    public const string shape = "shape";
    ///<summary>
    /// &amp;lt;input&amp;gt;, &amp;lt;select&amp;gt;	Specifies the width, in characters (for &amp;lt;input&amp;gt;) or specifies the number of visible options (for &amp;lt;select&amp;gt;)
    ///</summary>
    public const string size = "size";
    ///<summary>
    /// &amp;lt;img&amp;gt;, &amp;lt;link&amp;gt;, &amp;lt;source&amp;gt;	Specifies the size of the linked resource
    ///</summary>
    public const string sizes = "sizes";
    ///<summary>
    /// &amp;lt;col&amp;gt;, &amp;lt;colgroup&amp;gt;	Specifies the number of columns to span
    ///</summary>
    public const string span = "span";
    ///<summary>
    /// Global Attributes	Specifies whether the element is to have its spelling and grammar checked or not
    ///</summary>
    public const string spellcheck = "spellcheck";
    ///<summary>
    /// &amp;lt;audio&amp;gt;, &amp;lt;embed&amp;gt;, &amp;lt;iframe&amp;gt;, &amp;lt;img&amp;gt;, &amp;lt;input&amp;gt;, &amp;lt;script&amp;gt;, &amp;lt;source&amp;gt;, &amp;lt;track&amp;gt;, &amp;lt;video&amp;gt;	Specifies the URL of the media file
    ///</summary>
    public const string src = "src";
    ///<summary>
    /// &amp;lt;iframe&amp;gt;	Specifies the HTML content of the page to show in the &amp;lt;iframe&amp;gt;
    ///</summary>
    public const string srcdoc = "srcdoc";
    ///<summary>
    /// &amp;lt;track&amp;gt;	Specifies the language of the track text data (required if kind="subtitles")
    ///</summary>
    public const string srclang = "srclang";
    ///<summary>
    /// &amp;lt;img&amp;gt;, &amp;lt;source&amp;gt;	Specifies the URL of the image to use in different situations
    ///</summary>
    public const string srcset = "srcset";
    ///<summary>
    /// &amp;lt;ol&amp;gt;	Specifies the start value of an ordered list
    ///</summary>
    public const string start = "start";
    ///<summary>
    /// &amp;lt;input&amp;gt;	Specifies the legal number intervals for an input field
    ///</summary>
    public const string step = "step";
    ///<summary>
    /// Global Attributes	Specifies an inline CSS style for an element
    ///</summary>
    public const string style = "style";
    ///<summary>
    /// Global Attributes	Specifies the tabbing order of an element
    ///</summary>
    public const string tabindex = "tabindex";
    ///<summary>
    /// &amp;lt;a&amp;gt;, &amp;lt;area&amp;gt;, &amp;lt;base&amp;gt;, &amp;lt;form&amp;gt;	Specifies the target for where to open the linked document or where to submit the form
    ///</summary>
    public const string target = "target";
    ///<summary>
    /// Global Attributes	Specifies extra information about an element
    ///</summary>
    public const string title = "title";
    ///<summary>
    /// Global Attributes	Specifies whether the content of an element should be translated or not
    ///</summary>
    public const string translate = "translate";
    ///<summary>
    /// &amp;lt;a&amp;gt;, &amp;lt;button&amp;gt;, &amp;lt;embed&amp;gt;, &amp;lt;input&amp;gt;, &amp;lt;link&amp;gt;, &amp;lt;menu&amp;gt;, &amp;lt;object&amp;gt;, &amp;lt;script&amp;gt;, &amp;lt;source&amp;gt;, &amp;lt;style&amp;gt;	Specifies the type of element
    ///</summary>
    public const string type = "type";
    ///<summary>
    /// &amp;lt;img&amp;gt;, &amp;lt;object&amp;gt;	Specifies an image as a client-side image map
    ///</summary>
    public const string usemap = "usemap";
    ///<summary>
    /// &amp;lt;button&amp;gt;, &amp;lt;input&amp;gt;, &amp;lt;li&amp;gt;, &amp;lt;option&amp;gt;, &amp;lt;meter&amp;gt;, &amp;lt;progress&amp;gt;, &amp;lt;param&amp;gt;	Specifies the value of the element
    ///</summary>
    public const string value = "value";
    ///<summary>
    /// &amp;lt;canvas&amp;gt;, &amp;lt;embed&amp;gt;, &amp;lt;iframe&amp;gt;, &amp;lt;img&amp;gt;, &amp;lt;input&amp;gt;, &amp;lt;object&amp;gt;, &amp;lt;video&amp;gt;	Specifies the width of the element
    ///</summary>
    public const string width = "width";
    ///<summary>
    /// &amp;lt;textarea&amp;gt;	Specifies how the text in a text area is to be wrapped when submitted in a form
    ///</summary>
    public const string wrap = "wrap";
    ///<summary>
    /// &amp;lt;iframe&amp;gt; Specifies a Permissions Policy for the &amp;lt;iframe&amp;gt;. The policy defines what features are available to the &amp;lt;iframe&amp;gt; (for example, access to the microphone, camera, battery, web-share, etc.) based on the origin of the request.
    ///</summary>
    public const string allow = "allow";
    
    ///<summary>
    ///</summary>
    public const string allowfullscreen = "allowfullscreen";
    ///<summary>
    /// &amp;lt;iframe&amp;gt; [Deprecated Non-standard] Set to true if a cross-origin &amp;lt;iframe&amp;gt; should be allowed to invoke the Payment Request API.
    ///</summary>
    public const string allowpaymentrequest = "allowpaymentrequest";
    ///<summary>
    /// &amp;lt;iframe&amp;gt; [Experimental, Non-standard] Set to true to make the &amp;lt;iframe&amp;gt; credentialless, meaning that its content will be loaded in a new, ephemeral context. It doesn't have access to the network, cookies, and storage data associated with its origin. It uses a new context local to the top-level document lifetime. In return, the Cross-Origin-Embedder-Policy (COEP) embedding rules can be lifted, so documents with COEP set can embed third-party documents that do not. See IFrame credentialless for more details.
    ///</summary>
    public const string credentialless = "credentialless";
    ///<summary>
    /// &amp;lt;iframe&amp;gt; [Experimental] A Content Security Policy enforced for the embedded resource. See HTMLIFrameElement.csp for details.
    ///</summary>
    public const string csp = "csp";
    ///<summary>
    /// &amp;lt;iframe&amp;gt; Indicates when the browser should load the iframe.
    ///</summary>
    public const string loading = "loading";
    ///<summary>
    /// &amp;lt;iframe&amp;gt; Indicates which referrer to send when fetching the frame's resource:
    ///</summary>
    public const string referrerpolicy = "referrerpolicy";

    ///<summary>
    /// http-equiv
    ///</summary>
    public const string httpEquiv = "http-equiv";
}