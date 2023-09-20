namespace CC.CSX.Htmx;

public partial class HtmxAttributeKeys
{
    ///<summary>
    ///  add or remove progressive enhancement for links and forms
    ///</summary>
    public const string hxBoost = "hx-boost";

    ///<summary>
    ///  issues a GET to the specified URL
    ///</summary>
    public const string hxGet = "hx-get";

    ///<summary>
    ///  issues a POST to the specified URL
    ///</summary>
    public const string hxPost = "hx-post";

    ///<summary>
    ///  handle any event with a script inline
    ///</summary>
    public const string hxOn = "hx-on";

    ///<summary>
    ///  pushes the URL into the browser location bar, creating a new history entry
    ///</summary>
    public const string hxPush = "url hx-push-url";

    ///<summary>
    ///  select content to swap in from a response
    ///</summary>
    public const string hxSelect = "hx-select";

    ///<summary>
    ///  select content to swap in from a response, out of band (somewhere other than the target)
    ///</summary>
    public const string hxSelectOob = "hx-select-oob";

    ///<summary>
    ///  controls how content is swapped in (outerHTML, beforeend, afterend, …)
    ///</summary>
    public const string hxSwap = "hx-swap";

    ///<summary>
    ///  marks content in a response to be out of band (should swap in somewhere other than the target)
    ///</summary>
    public const string hxSwapOob = "hx-swap-oob";

    ///<summary>
    ///  specifies the target element to be swapped
    ///</summary>
    public const string hxTarget = "hx-target";

    ///<summary>
    ///  specifies the event that triggers the request
    ///</summary>
    public const string hxTrigger = "hx-trigger";

    ///<summary>
    ///  adds values to the parameters to submit with the request (JSON-formatted)
    ///</summary>
    public const string hxVals = "hx-vals";

    ///<summary>
    //  shows a confirm() dialog before issuing a request
    ///</summary>   
    public const string hxConfirm = "hx-confirm ";
    ///<summary>
    //  issues a DELETE to the specified URL
    ///</summary>   
    public const string hxDelete = "hx-delete ";
    ///<summary>
    // disables htmx processing for the given node and any children nodes
    ///</summary>
    public const string hxDisable = "hx-disable ";
    ///<summary>
    // control and disable automatic attribute inheritance for child nodes
    ///</summary>
    public const string hxDisinherit = "hx-disinherit ";
    ///<summary>
    // changes the request encoding type
    ///</summary>
    public const string hxEncoding = "hx-encoding ";
    ///<summary>
    // extensions to use for this element
    ///</summary>
    public const string hxExt = "hx-ext ";
    ///<summary>
    // adds to the headers that will be submitted with the request
    ///</summary>
    public const string hxHeaders = "hx-headers ";
    ///<summary>
    // prevent sensitive data being saved to the history cache
    ///</summary>
    public const string hxHistory = "hx-history ";
    //<summary>
    // -elt the element to snapshot and restore during history navigation
    ///</summary>
    public const string hxHistoryElt = "hx-history-elt";
    ///<summary>
    // include additional data in requests
    ///</summary>
    public const string hxInclude = "hx-include ";
    ///<summary>
    // the element to put the htmx-request class on during the request
    ///</summary>
    public const string hxIndicator = "hx-indicator ";
    ///<summary>
    // filters the parameters that will be submitted with a request
    ///</summary>
    public const string hxParams = "hx-params ";
    ///<summary>
    // issues a PATCH to the specified URL
    ///</summary>
    public const string hxPatch = "hx-patch ";
    ///<summary>
    // specifies elements to keep unchanged between requests
    ///</summary>
    public const string hxPreserve = "hx-preserve ";
    ///<summary>
    // shows a prompt() before submitting a request
    ///</summary>
    public const string hxPrompt = "hx-prompt ";
    ///<summary>
    // issues a PUT to the specified URL
    ///</summary>
    public const string hxPut = "hx-put ";
    //<summary>
    // replace the URL in the browser location bar
    ///</summary>
    public const string hxReplaceUrl = "hx-replace-url";
    ///<summary>
    // configures various aspects of the request
    ///</summary>
    public const string hxRequest = "hx-request";
    ///<summary>
    // has been moved to an extension. Documentation for older versions
    ///</summary>
    public const string hxSse = "hx-sse";
    ///<summary>
    // control how requests made by different elements are synchronized
    ///</summary>
    public const string hxSync = "hx-sync";
    ///<summary>
    // force elements to validate themselves before a request
    ///</summary>
    public const string hxValidate = "hx-validate";
}