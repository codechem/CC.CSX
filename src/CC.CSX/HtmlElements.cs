namespace CC.CSX;
using Keys = CC.CSX.HtmlElementKeys;

///<summary>
/// All availalble html elements
///</summary>
public static partial class HtmlElements
{
    ///<summary> 
    ///  &amp;lt;a&amp;gt; Defines a hyperlink
    ///</summary>
    public static HtmlNode A(params HtmlItem[] items) => new HtmlNode(Keys.a, items);

    ///<summary>
    ///  &amp;lt;abbr&amp;gt; Defines an abbreviation or an acronym
    ///</summary>
    public static HtmlNode Abbr(params HtmlItem[] items) => new HtmlNode(Keys.abbr, items);

    ///<summary>
    ///  &amp;lt;address&amp;gt; Defines contact information for the author/owner of a document
    ///</summary>
    public static HtmlNode Address(params HtmlItem[] items) => new HtmlNode(Keys.address, items);

    ///<summary>
    ///  &amp;lt;area&amp;gt; Defines an area inside an image map
    ///</summary>
    public static HtmlNode Area(params HtmlItem[] items) => new HtmlNode(Keys.area, items);

    ///<summary>
    ///  &amp;lt;article&amp;gt; Defines an article
    ///</summary>
    public static HtmlNode Article(params HtmlItem[] items) => new HtmlNode(Keys.article, items);

    ///<summary>
    ///  &amp;lt;aside&amp;gt; Defines content aside from the page content
    ///</summary>
    public static HtmlNode Aside(params HtmlItem[] items) => new HtmlNode(Keys.aside, items);

    ///<summary>
    ///  &amp;lt;audio&amp;gt; Defines embedded sound content
    ///</summary>
    public static HtmlNode Audio(params HtmlItem[] items) => new HtmlNode(Keys.audio, items);

    ///<summary>
    ///  &amp;lt;b&amp;gt; Defines bold text
    ///</summary>
    public static HtmlNode B(params HtmlItem[] items) => new HtmlNode(Keys.b, items);

    ///<summary>
    ///  &amp;lt;base&amp;gt; Specifies the base URL/target for all relative URLs in a document
    ///</summary>
    public static HtmlNode Base(params HtmlItem[] items) => new HtmlNode(Keys.@base, items);

    ///<summary>
    ///  &amp;lt;bdi&amp;gt; Isolates a part of text that might be formatted in a different direction from other text outside it
    ///</summary>
    public static HtmlNode Bdi(params HtmlItem[] items) => new HtmlNode(Keys.bdi, items);

    ///<summary>
    ///  &amp;lt;bdo&amp;gt; Overrides the current text direction
    ///</summary>
    public static HtmlNode Bdo(params HtmlItem[] items) => new HtmlNode(Keys.bdo, items);

    ///<summary>
    ///  &amp;lt;blockquote&amp;gt; Defines a section that is quoted from another source
    ///</summary>
    public static HtmlNode Blockquote(params HtmlItem[] items) => new HtmlNode(Keys.blockquote, items);

    ///<summary>
    ///  &amp;lt;body&amp;gt; Defines the document's body
    ///</summary>
    public static HtmlNode Body(params HtmlItem[] items) => new HtmlNode(Keys.body, items);

    ///<summary>
    ///  &amp;lt;br&amp;gt; Defines a single line break
    ///</summary>
    public static HtmlNode Br(params HtmlItem[] items) => new HtmlNode(Keys.br, items);

    ///<summary>
    ///  &amp;lt;button&amp;gt; Defines a clickable button
    ///</summary>
    public static HtmlNode Button(params HtmlItem[] items) => new HtmlNode(Keys.button, items);

    ///<summary>
    ///  &amp;lt;canvas&amp;gt; Used to draw graphics, on the fly, via scripting (usually JavaScript)
    ///</summary>
    public static HtmlNode Canvas(params HtmlItem[] items) => new HtmlNode(Keys.canvas, items);

    ///<summary>
    ///  &amp;lt;caption&amp;gt; Defines a table caption
    ///</summary>
    public static HtmlNode Caption(params HtmlItem[] items) => new HtmlNode(Keys.caption, items);

    ///<summary>
    ///  &amp;lt;cite&amp;gt; Defines the title of a work
    ///</summary>
    public static HtmlNode Cite(params HtmlItem[] items) => new HtmlNode(Keys.cite, items);

    ///<summary>
    ///  &amp;lt;code&amp;gt; Defines a piece of computer code
    ///</summary>
    public static HtmlNode Code(params HtmlItem[] items) => new HtmlNode(Keys.code, items);

    ///<summary>
    ///  &amp;lt;col&amp;gt; Specifies column properties for each column within a &amp;lt;colgroup&amp;gt; element 
    ///</summary>
    public static HtmlNode Col(params HtmlItem[] items) => new HtmlNode(Keys.col, items);

    ///<summary>
    ///  &amp;lt;colgroup&amp;gt; Specifies a group of one or more columns in a table for formatting
    ///</summary>
    public static HtmlNode Colgroup(params HtmlItem[] items) => new HtmlNode(Keys.colgroup, items);

    ///<summary>
    ///  &amp;lt;data&amp;gt; Adds a machine-readable translation of a given content
    ///</summary>
    public static HtmlNode Data(params HtmlItem[] items) => new HtmlNode(Keys.data, items);

    ///<summary>
    ///  &amp;lt;datalist&amp;gt; Specifies a list of pre-defined options for input controls
    ///</summary>
    public static HtmlNode Datalist(params HtmlItem[] items) => new HtmlNode(Keys.datalist, items);

    ///<summary>
    ///  &amp;lt;dd&amp;gt; Defines a description/value of a term in a description list
    ///</summary>
    public static HtmlNode Dd(params HtmlItem[] items) => new HtmlNode(Keys.dd, items);

    ///<summary>
    ///  &amp;lt;del&amp;gt; Defines text that has been deleted from a document
    ///</summary>
    public static HtmlNode Del(params HtmlItem[] items) => new HtmlNode(Keys.del, items);

    ///<summary>
    ///  &amp;lt;details&amp;gt; Defines additional details that the user can view or hide
    ///</summary>
    public static HtmlNode Details(params HtmlItem[] items) => new HtmlNode(Keys.details, items);

    ///<summary>
    ///  &amp;lt;dfn&amp;gt; Specifies a term that is going to be defined within the content
    ///</summary>
    public static HtmlNode Dfn(params HtmlItem[] items) => new HtmlNode(Keys.dfn, items);

    ///<summary>
    ///  &amp;lt;dialog&amp;gt; Defines a dialog box or window
    ///</summary>
    public static HtmlNode Dialog(params HtmlItem[] items) => new HtmlNode(Keys.dialog, items);

    ///<summary>
    ///  &amp;lt;div&amp;gt; Defines a section in a document
    ///</summary>
    public static HtmlNode Div(params HtmlItem[] items) => new HtmlNode(Keys.div, items);

    ///<summary>
    ///  &amp;lt;dl&amp;gt; Defines a description list
    ///</summary>
    public static HtmlNode Dl(params HtmlItem[] items) => new HtmlNode(Keys.dl, items);

    ///<summary>
    ///  &amp;lt;dt&amp;gt; Defines a term/name in a description list
    ///</summary>
    public static HtmlNode Dt(params HtmlItem[] items) => new HtmlNode(Keys.dt, items);

    ///<summary>
    ///  &amp;lt;em&amp;gt; Defines emphasized text 
    ///</summary>
    public static HtmlNode Em(params HtmlItem[] items) => new HtmlNode(Keys.em, items);

    ///<summary>
    ///  &amp;lt;embed&amp;gt; Defines a container for an external application
    ///</summary>
    public static HtmlNode Embed(params HtmlItem[] items) => new HtmlNode(Keys.embed, items);

    ///<summary>
    ///  &amp;lt;fieldset&amp;gt; Groups related elements in a form
    ///</summary>
    public static HtmlNode Fieldset(params HtmlItem[] items) => new HtmlNode(Keys.fieldset, items);

    ///<summary>
    ///  &amp;lt;figcaption&amp;gt; Defines a caption for a &amp;lt;figure&amp;gt; element
    ///</summary>
    public static HtmlNode Figcaption(params HtmlItem[] items) => new HtmlNode(Keys.figcaption, items);

    ///<summary>
    ///  &amp;lt;figure&amp;gt; Specifies self-contained content
    ///</summary>
    public static HtmlNode Figure(params HtmlItem[] items) => new HtmlNode(Keys.figure, items);

    ///<summary>
    ///  &amp;lt;footer&amp;gt; Defines a footer for a document or section
    ///</summary>
    public static HtmlNode Footer(params HtmlItem[] items) => new HtmlNode(Keys.footer, items);

    ///<summary>
    ///  &amp;lt;form&amp;gt; Defines an HTML form for user input
    ///</summary>
    public static HtmlNode Form(params HtmlItem[] items) => new HtmlNode(Keys.form, items);

    ///<summary>
    ///  &amp;lt;h1&amp;gt; to &amp;lt;h6&amp;gt;	Defines HTML headings
    ///</summary>
    public static HtmlNode H1(params HtmlItem[] items) => new HtmlNode(Keys.h1, items);
    ///<summary>
    ///  &amp;lt;h1&amp;gt; to &amp;lt;h6&amp;gt;	Defines HTML headings
    ///</summary>
    public static HtmlNode H2(params HtmlItem[] items) => new HtmlNode(Keys.h2, items);
    ///<summary>
    ///  &amp;lt;h1&amp;gt; to &amp;lt;h6&amp;gt;	Defines HTML headings
    ///</summary>
    public static HtmlNode H3(params HtmlItem[] items) => new HtmlNode(Keys.h3, items);
    ///<summary>
    ///  &amp;lt;h1&amp;gt; to &amp;lt;h6&amp;gt;	Defines HTML headings
    ///</summary>
    public static HtmlNode H4(params HtmlItem[] items) => new HtmlNode(Keys.h4, items);
    ///<summary>
    ///  &amp;lt;h1&amp;gt; to &amp;lt;h6&amp;gt;	Defines HTML headings
    ///</summary>
    public static HtmlNode H5(params HtmlItem[] items) => new HtmlNode(Keys.h5, items);
    ///<summary>
    ///  &amp;lt;h1&amp;gt; to &amp;lt;h6&amp;gt;	Defines HTML headings
    ///</summary>
    public static HtmlNode H6(params HtmlItem[] items) => new HtmlNode(Keys.h6, items);
    ///<summary>
    ///  &amp;lt;head&amp;gt; Contains metadata/information for the document
    ///</summary>
    public static HtmlNode Head(params HtmlItem[] items) => new HtmlNode(Keys.head, items);

    ///<summary>
    ///  &amp;lt;header&amp;gt; Defines a header for a document or section
    ///</summary>
    public static HtmlNode Header(params HtmlItem[] items) => new HtmlNode(Keys.header, items);

    ///<summary>
    ///  &amp;lt;hr&amp;gt; Defines a thematic change in the content
    ///</summary>
    public static HtmlNode Hr(params HtmlItem[] items) => new HtmlNode(Keys.hr, items);

    ///<summary>
    ///  &amp;lt;html&amp;gt; Defines the root of an HTML document
    ///</summary>
    public static HtmlNode Html(params HtmlItem[] items) => new HtmlNode(Keys.html, items);

    ///<summary> 
    ///  &amp;lt;i&amp;gt; Defines a part of text in an alternate voice or mood
    ///</summary>
    public static HtmlNode I(params HtmlItem[] items) => new HtmlNode(Keys.i, items);

    ///<summary>
    ///  &amp;lt;iframe&amp;gt; Defines an inline frame
    ///</summary>
    public static HtmlNode IFrame(params HtmlItem[] items) => new HtmlNode(Keys.iframe, items);

    ///<summary>
    ///  &amp;lt;img&amp;gt; Defines an image
    ///</summary>
    public static HtmlNode Img(params HtmlItem[] items) => new HtmlNode(Keys.img, items);

    ///<summary>
    ///  &amp;lt;input&amp;gt; Defines an input control
    ///</summary>
    public static HtmlNode Input(params HtmlItem[] items) => new HtmlNode(Keys.input, items);

    ///<summary>
    ///  &amp;lt;ins&amp;gt; Defines a text that has been inserted into a document
    ///</summary>
    public static HtmlNode Ins(params HtmlItem[] items) => new HtmlNode(Keys.ins, items);

    ///<summary>
    ///  &amp;lt;kbd&amp;gt; Defines keyboard input
    ///</summary>
    public static HtmlNode Kbd(params HtmlItem[] items) => new HtmlNode(Keys.kbd, items);

    ///<summary>
    ///  &amp;lt;label&amp;gt; Defines a label for an &amp;lt;input&amp;gt; element
    ///</summary>
    public static HtmlNode Label(params HtmlItem[] items) => new HtmlNode(Keys.label, items);

    ///<summary>
    ///  &amp;lt;legend&amp;gt; Defines a caption for a &amp;lt;fieldset&amp;gt; element
    ///</summary>
    public static HtmlNode Legend(params HtmlItem[] items) => new HtmlNode(Keys.legend, items);

    ///<summary>
    ///  &amp;lt;li&amp;gt; Defines a list item
    ///</summary>
    public static HtmlNode Li(params HtmlItem[] items) => new HtmlNode(Keys.li, items);

    ///<summary>
    ///  &amp;lt;link&amp;gt; Defines the relationship between a document and an external resource (most used to link to style sheets)
    ///</summary>
    public static HtmlNode Link(params HtmlItem[] items) => new HtmlNode(Keys.link, items);

    ///<summary>
    ///  &amp;lt;main&amp;gt; Specifies the main content of a document
    ///</summary>
    public static HtmlNode Main(params HtmlItem[] items) => new HtmlNode(Keys.main, items);

    ///<summary>
    ///  &amp;lt;map&amp;gt; Defines an image map
    ///</summary>
    public static HtmlNode Map(params HtmlItem[] items) => new HtmlNode(Keys.map, items);

    ///<summary>
    ///  &amp;lt;mark&amp;gt; Defines marked/highlighted text
    ///</summary>
    public static HtmlNode Mark(params HtmlItem[] items) => new HtmlNode(Keys.mark, items);

    ///<summary>
    ///  &amp;lt;meta&amp;gt; Defines metadata about an HTML document
    ///</summary>
    public static HtmlNode Meta(params HtmlItem[] items) => new HtmlNode(Keys.meta, items);

    ///<summary>
    ///  &amp;lt;meter&amp;gt; Defines a scalar measurement within a known range (a gauge)
    ///</summary>
    public static HtmlNode Meter(params HtmlItem[] items) => new HtmlNode(Keys.meter, items);

    ///<summary>
    ///  &amp;lt;nav&amp;gt; Defines navigation links
    ///</summary>
    public static HtmlNode Nav(params HtmlItem[] items) => new HtmlNode(Keys.nav, items);

    ///<summary>
    ///  &amp;lt;noscript&amp;gt; Defines an alternate content for users that do not support client-side scripts
    ///</summary>
    public static HtmlNode Noscript(params HtmlItem[] items) => new HtmlNode(Keys.noscript, items);

    ///<summary>
    ///  &amp;lt;object&amp;gt; Defines a container for an external application
    ///</summary>
    public static HtmlNode @object(params HtmlItem[] items) => new HtmlNode(Keys.@object, items);

    ///<summary>
    ///  &amp;lt;ol&amp;gt; Defines an ordered list
    ///</summary>
    public static HtmlNode Ol(params HtmlItem[] items) => new HtmlNode(Keys.ol, items);

    ///<summary>
    ///  &amp;lt;optgroup&amp;gt; Defines a group of related options in a drop-down list
    ///</summary>
    public static HtmlNode Optgroup(params HtmlItem[] items) => new HtmlNode(Keys.optgroup, items);

    ///<summary>
    ///  &amp;lt;option&amp;gt; Defines an option in a drop-down list
    ///</summary>
    public static HtmlNode Option(params HtmlItem[] items) => new HtmlNode(Keys.option, items);

    ///<summary>
    ///  &amp;lt;output&amp;gt; Defines the result of a calculation
    ///</summary>
    public static HtmlNode Output(params HtmlItem[] items) => new HtmlNode(Keys.output, items);

    ///<summary>
    ///  &amp;lt;p&amp;gt; Defines a paragraph
    ///</summary>
    public static HtmlNode P(params HtmlItem[] items) => new HtmlNode(Keys.p, items);

    ///<summary>
    ///  &amp;lt;param&amp;gt; Defines a parameter for an object
    ///</summary>
    public static HtmlNode Param(params HtmlItem[] items) => new HtmlNode(Keys.param, items);

    ///<summary>
    ///  &amp;lt;picture&amp;gt; Defines a container for multiple image resources
    ///</summary>
    public static HtmlNode Picture(params HtmlItem[] items) => new HtmlNode(Keys.picture, items);

    ///<summary>
    ///  &amp;lt;pre&amp;gt; Defines preformatted text
    ///</summary>
    public static HtmlNode Pre(params HtmlItem[] items) => new HtmlNode(Keys.pre, items);

    ///<summary>
    ///  &amp;lt;progress&amp;gt; Represents the progress of a task
    ///</summary>
    public static HtmlNode Progress(params HtmlItem[] items) => new HtmlNode(Keys.progress, items);

    ///<summary>
    ///  &amp;lt;q&amp;gt;Defines a short quotation
    ///</summary>
    public static HtmlNode Q(params HtmlItem[] items) => new HtmlNode(Keys.q, items);

    ///<summary>
    ///  &amp;lt;rp&amp;gt; Defines what to show in browsers that do not support ruby annotations
    ///</summary>
    public static HtmlNode Rp(params HtmlItem[] items) => new HtmlNode(Keys.rp, items);

    ///<summary>
    ///  &amp;lt;rt&amp;gt; Defines an explanation/pronunciation of characters (for East Asian typography)
    ///</summary>
    public static HtmlNode Rt(params HtmlItem[] items) => new HtmlNode(Keys.rt, items);

    ///<summary>
    ///  &amp;lt;ruby&amp;gt; Defines a ruby annotation (for East Asian typography)
    ///</summary>
    public static HtmlNode Ruby(params HtmlItem[] items) => new HtmlNode(Keys.ruby, items);

    ///<summary>
    ///  &amp;lt;s&amp;gt;Defines text that is no longer correct
    ///</summary>
    public static HtmlNode S(params HtmlItem[] items) => new HtmlNode(Keys.s, items);

    ///<summary>
    ///  &amp;lt;samp&amp;gt; Defines sample output from a computer program
    ///</summary>
    public static HtmlNode Samp(params HtmlItem[] items) => new HtmlNode(Keys.samp, items);

    ///<summary>
    ///  &amp;lt;script&amp;gt; Defines a client-side script
    ///</summary>
    public static HtmlNode Script(params HtmlItem[] items) => new HtmlNode(Keys.script, items);

    ///<summary>
    ///  &amp;lt;section&amp;gt; Defines a section in a document
    ///</summary>
    public static HtmlNode Section(params HtmlItem[] items) => new HtmlNode(Keys.section, items);

    ///<summary>
    ///  &amp;lt;select&amp;gt; Defines a drop-down list
    ///</summary>
    public static HtmlNode Select(params HtmlItem[] items) => new HtmlNode(Keys.select, items);

    ///<summary>
    ///  &amp;lt;small&amp;gt; Defines smaller text
    ///</summary>
    public static HtmlNode Small(params HtmlItem[] items) => new HtmlNode(Keys.small, items);

    ///<summary>
    ///  &amp;lt;source&amp;gt; Defines multiple media resources for media elements (&amp;lt;video&amp;gt; and &amp;lt;audio&amp;gt;)
    ///</summary>
    public static HtmlNode Source(params HtmlItem[] items) => new HtmlNode(Keys.source, items);

    ///<summary>
    ///  &amp;lt;span&amp;gt; Defines a section in a document
    ///</summary>
    public static HtmlNode Span(params HtmlItem[] items) => new HtmlNode(Keys.span, items);

    ///<summary>
    ///  &amp;lt;strong&amp;gt; Defines important text
    ///</summary>
    public static HtmlNode Strong(params HtmlItem[] items) => new HtmlNode(Keys.strong, items);

    ///<summary>
    ///  &amp;lt;style&amp;gt; Defines style information for a document
    ///</summary>
    public static HtmlNode Style(params HtmlItem[] items) => new HtmlNode(Keys.style, items);

    ///<summary>
    ///   &amp;lt;sub&amp;gt; Defines subscripted text
    ///</summary>
    public static HtmlNode Sub(params HtmlItem[] items) => new HtmlNode(Keys.sub, items);

    ///<summary>
    ///   &amp;lt;summary&amp;gt; Defines a visible heading for a &amp;lt;details&amp;gt; element
    ///</summary>
    public static HtmlNode Summary(params HtmlItem[] items) => new HtmlNode(Keys.summary, items);

    ///<summary>
    ///   &amp;lt;sup&amp;gt; Defines superscripted text
    ///</summary>
    public static HtmlNode Sup(params HtmlItem[] items) => new HtmlNode(Keys.sup, items);

    ///<summary>
    ///   &amp;lt;svg&amp;gt; Defines a container for SVG graphics
    ///</summary>
    public static HtmlNode Svg(params HtmlItem[] items) => new HtmlNode(Keys.svg, items);

    ///<summary>
    ///   &amp;lt;table&amp;gt; Defines a table
    ///</summary>
    public static HtmlNode Table(params HtmlItem[] items) => new HtmlNode(Keys.table, items);

    ///<summary>
    ///   &amp;lt;tbody&amp;gt; Groups the body content in a table
    ///</summary>
    public static HtmlNode Tbody(params HtmlItem[] items) => new HtmlNode(Keys.tbody, items);

    ///<summary>
    ///   &amp;lt;td&amp;gt; Defines a cell in a table
    ///</summary>
    public static HtmlNode Td(params HtmlItem[] items) => new HtmlNode(Keys.td, items);

    ///<summary>
    ///   &amp;lt;template&amp;gt; Defines a container for content that should be hidden when the page loads
    ///</summary>
    public static HtmlNode Template(params HtmlItem[] items) => new HtmlNode(Keys.template, items);

    ///<summary>
    ///   &amp;lt;textarea&amp;gt; Defines a multiline input control (text area)
    ///</summary>
    public static HtmlNode Textarea(params HtmlItem[] items) => new HtmlNode(Keys.textarea, items);

    ///<summary>
    ///   &amp;lt;tfoot&amp;gt; Groups the footer content in a table
    ///</summary>
    public static HtmlNode Tfoot(params HtmlItem[] items) => new HtmlNode(Keys.tfoot, items);

    ///<summary>
    ///   &amp;lt;th&amp;gt; Defines a header cell in a table
    ///</summary>
    public static HtmlNode Th(params HtmlItem[] items) => new HtmlNode(Keys.th, items);

    ///<summary>
    ///   &amp;lt;thead&amp;gt; Groups the header content in a table
    ///</summary>
    public static HtmlNode Thead(params HtmlItem[] items) => new HtmlNode(Keys.thead, items);

    ///<summary>
    ///   &amp;lt;time&amp;gt; Defines a specific time (or datetime)
    ///</summary>
    public static HtmlNode Time(params HtmlItem[] items) => new HtmlNode(Keys.time, items);

    ///<summary>
    ///   &amp;lt;title&amp;gt; Defines a title for the document
    ///</summary>
    public static HtmlNode Title(params HtmlItem[] items) => new HtmlNode(Keys.title, items);

    ///<summary>
    ///   &amp;lt;tr&amp;gt; Defines a row in a table
    ///</summary>
    public static HtmlNode Tr(params HtmlItem[] items) => new HtmlNode(Keys.tr, items);

    ///<summary>
    ///   &amp;lt;track&amp;gt; Defines text tracks for media elements (&amp;lt;video&amp;gt; and &amp;lt;audio&amp;gt;)
    ///</summary>
    public static HtmlNode Track(params HtmlItem[] items) => new HtmlNode(Keys.track, items);

    ///<summary>
    ///  Defines some text that is unarticulated and styled differently from normal text
    ///</summary>
    public static HtmlNode U(params HtmlItem[] items) => new HtmlNode(Keys.u, items);

    ///<summary>
    ///   &amp;lt;ul&amp;gt; Defines an unordered list
    ///</summary>
    public static HtmlNode Ul(params HtmlItem[] items) => new HtmlNode(Keys.ul, items);

    ///<summary>
    ///   &amp;lt;var&amp;gt; Defines a variable
    ///</summary>
    public static HtmlNode Var(params HtmlItem[] items) => new HtmlNode(Keys.var, items);

    ///<summary>
    ///   &amp;lt;video&amp;gt; Defines embedded video content
    ///</summary>
    public static HtmlNode Video(params HtmlItem[] items) => new HtmlNode(Keys.video, items);

    ///<summary>
    ///   &amp;lt;wbr&amp;gt; Defines a possible line-break
    ///</summary>
    public static HtmlNode Wbr(params HtmlItem[] items) => new HtmlNode(Keys.wbr, items);
}