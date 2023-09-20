namespace CC.CSX.Htmx;
using CC.CSX;

using Keys = CC.CSX.Htmx.HtmxAttributeKeys;

public partial class HtmxAttributes
{
    ///<summary>
    ///  add or remove progressive enhancement for links and forms
    ///</summary>
    public static HtmlAttribute hxBoost(string val) => new HtmlAttribute(Keys.hxBoost, val);

    ///<summary>
    ///  issues a GET to the specified URL
    ///</summary>
    public static HtmlAttribute hxGet(string val) => new HtmlAttribute(Keys.hxGet, val);

    ///<summary>
    ///  issues a POST to the specified URL
    ///</summary>
    public static HtmlAttribute hxPost(string val) => new HtmlAttribute(Keys.hxPost, val);

    ///<summary>
    ///  handle any event with a script inline
    ///</summary>
    public static HtmlAttribute hxOn(string val) => new HtmlAttribute(Keys.hxOn, val);

    ///<summary>
    ///  pushes the URL into the browser location bar, creating a new history entry
    ///</summary>
    public static HtmlAttribute hxPushUrl(string val) => new HtmlAttribute(Keys.hxPush, val);

    ///<summary>
    ///  select content to swap in from a response
    ///</summary>
    public static HtmlAttribute hxSelect(string val) => new HtmlAttribute(Keys.hxSelect, val);

    ///<summary>
    ///  select content to swap in from a response, out of band (somewhere other than the target)
    ///</summary>
    public static HtmlAttribute hxSelectOob(string val) => new HtmlAttribute(Keys.hxSelectOob, val);

    ///<summary>
    ///  controls how content is swapped in (outerHTML, beforeend, afterend, …)
    ///</summary>
    public static HtmlAttribute hxSwap(string val) => new HtmlAttribute(Keys.hxSwap, val);

    ///<summary>
    ///  marks content in a response to be out of band (should swap in somewhere other than the target)
    ///</summary>
    public static HtmlAttribute hxSwapOob(string val) => new HtmlAttribute(Keys.hxSwapOob, val);

    ///<summary>
    ///  specifies the target element to be swapped
    ///</summary>
    public static HtmlAttribute hxTarget(string val) => new HtmlAttribute(Keys.hxTarget, val);

    ///<summary>
    ///  specifies the event that triggers the request
    ///</summary>
    public static HtmlAttribute hxTrigger(string val) => new HtmlAttribute(Keys.hxTrigger, val);

    ///<summary>
    ///  adds values to the parameters to submit with the request (JSON-formatted)
    ///</summary>
    public static HtmlAttribute hxVals(string val) => new HtmlAttribute(Keys.hxVals, val);

    ///<summary>
    //  shows a confirm() dialog before issuing a request
    ///</summary>   
    public static HtmlAttribute hxConfirm(string val) => new HtmlAttribute(Keys.hxConfirm, val);
    ///<summary>
    //  issues a DELETE to the specified URL
    ///</summary>   
    public static HtmlAttribute hxDelete(string val) => new HtmlAttribute(Keys.hxDelete, val);
    ///<summary>
    // disables htmx processing for the given node and any children nodes
    ///</summary>
    public static HtmlAttribute hxDisable(string val) => new HtmlAttribute(Keys.hxDisable, val);
    ///<summary>
    // control and disable automatic attribute inheritance for child nodes
    ///</summary>
    public static HtmlAttribute hxDisinherit(string val) => new HtmlAttribute(Keys.hxDisinherit, val);
    ///<summary>
    // changes the request encoding type
    ///</summary>
    public static HtmlAttribute hxEncoding(string val) => new HtmlAttribute(Keys.hxEncoding, val);
    ///<summary>
    // extensions to use for this element
    ///</summary>
    public static HtmlAttribute hxExt(string val) => new HtmlAttribute(Keys.hxExt, val);
    ///<summary>
    // adds to the headers that will be submitted with the request
    ///</summary>
    public static HtmlAttribute hxHeaders(string val) => new HtmlAttribute(Keys.hxHeaders, val);
    ///<summary>
    // prevent sensitive data being saved to the history cache
    ///</summary>
    public static HtmlAttribute hxHistory(string val) => new HtmlAttribute(Keys.hxHistory, val);
    //<summary>
    // -elt the element to snapshot and restore during history navigation
    ///</summary>
    public static HtmlAttribute hxHistoryElt(string val) => new HtmlAttribute(Keys.hxHistoryElt, val);
    ///<summary>
    // include additional data in requests
    ///</summary>
    public static HtmlAttribute hxInclude(string val) => new HtmlAttribute(Keys.hxInclude, val);
    ///<summary>
    // the element to put the htmx-request class on during the request
    ///</summary>
    public static HtmlAttribute hxIndicator(string val) => new HtmlAttribute(Keys.hxIndicator, val);
    ///<summary>
    // filters the parameters that will be submitted with a request
    ///</summary>
    public static HtmlAttribute hxParams(string val) => new HtmlAttribute(Keys.hxParams, val);
    ///<summary>
    // issues a PATCH to the specified URL
    ///</summary>
    public static HtmlAttribute hxPatch(string val) => new HtmlAttribute(Keys.hxPatch, val);
    ///<summary>
    // specifies elements to keep unchanged between requests
    ///</summary>
    public static HtmlAttribute hxPreserve(string val) => new HtmlAttribute(Keys.hxPreserve, val);
    ///<summary>
    // shows a prompt() before submitting a request
    ///</summary>
    public static HtmlAttribute hxPrompt(string val) => new HtmlAttribute(Keys.hxPrompt, val);
    ///<summary>
    // issues a PUT to the specified URL
    ///</summary>
    public static HtmlAttribute hxPut(string val) => new HtmlAttribute(Keys.hxPut, val);
    //<summary>
    // replace the URL in the browser location bar
    ///</summary>
    public static HtmlAttribute hxReplaceUrl(string val) => new HtmlAttribute(Keys.hxReplaceUrl, val);
    ///<summary>
    // configures various aspects of the request
    ///</summary>
    public static HtmlAttribute hxRequest(string val) => new HtmlAttribute(Keys.hxRequest, val);
    ///<summary>
    // has been moved to an extension. Documentation for older versions
    ///</summary>
    public static HtmlAttribute hxSse(string val) => new HtmlAttribute(Keys.hxSse, val);
    ///<summary>
    // control how requests made by different elements are synchronized
    ///</summary>
    public static HtmlAttribute hxSync(string val) => new HtmlAttribute(Keys.hxSync, val);
    ///<summary>
    // force elements to validate themselves before a request
    ///</summary>
    public static HtmlAttribute hxValidate(string val) => new HtmlAttribute(Keys.hxValidate, val);
}