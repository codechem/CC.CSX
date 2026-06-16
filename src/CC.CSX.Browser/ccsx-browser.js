// CC.CSX.Browser runtime glue.
// Shipped as an embedded resource and imported as an ES module via a data: URL,
// so host apps need no static-asset or main.js wiring beyond starting the runtime.

let rootEl = null;
let dispatch = null;
let dispatchHx = null;
const listening = new Set();

export function init(selector, dispatchFn, dispatchHxFn) {
  rootEl = document.querySelector(selector);
  if (!rootEl) throw new Error(`CC.CSX.Browser: mount root '${selector}' not found`);
  dispatch = dispatchFn;
  dispatchHx = dispatchHxFn;
  initHx();
}

// One capture-phase listener per event type on the mount root.
// Capture catches non-bubbling events (focus, blur, scroll) too.
export function ensureListener(name) {
  if (!rootEl || listening.has(name)) return;
  listening.add(name);
  rootEl.addEventListener(name, handle, { capture: true });
  // drop only fires on elements whose dragover is cancelled, and the async
  // dispatch to .NET can't do that — cancel it here for bound drop targets
  if (name === 'drop') rootEl.addEventListener('dragover', allowDrop, { capture: true });
}

function allowDrop(e) {
  const el = e.target instanceof Element ? e.target.closest('[data-ht-on-drop]') : null;
  if (el && rootEl.contains(el)) e.preventDefault();
}

function handle(e) {
  const attr = `data-ht-on-${e.type}`;
  const el = e.target instanceof Element ? e.target.closest(`[${attr}]`) : null;
  if (!el || !rootEl.contains(el)) return;
  // a handled submit would otherwise reload the page
  if (e.type === 'submit') e.preventDefault();
  // a handled dragover/drop must be cancelled for the drop to be allowed
  if (e.type === 'dragover' || e.type === 'drop') e.preventDefault();
  // Firefox only starts a drag when dragstart sets data
  if (e.type === 'dragstart' && e.dataTransfer && !e.dataTransfer.types.length) {
    e.dataTransfer.setData('text/plain', el.id || '');
  }
  dispatch(el.getAttribute(attr), payload(e));
}

function payload(e) {
  return JSON.stringify(eventData(e));
}

function eventData(e) {
  const t = e.target;
  return {
    type: e.type,
    value: t && 'value' in t ? String(t.value) : null,
    checked: t && 'checked' in t ? !!t.checked : null,
    key: e.key ?? null,
    code: e.code ?? null,
    button: typeof e.button === 'number' ? e.button : null,
    clientX: typeof e.clientX === 'number' ? e.clientX : null,
    clientY: typeof e.clientY === 'number' ? e.clientY : null,
    deltaX: typeof e.deltaX === 'number' ? e.deltaX : null,
    deltaY: typeof e.deltaY === 'number' ? e.deltaY : null,
    ctrlKey: !!e.ctrlKey,
    shiftKey: !!e.shiftKey,
    altKey: !!e.altKey,
    metaKey: !!e.metaKey,
    targetId: (t && t.id) || null,
    // readable in drop/dragstart; protected (empty) in the other drag events
    dataTransfer: e.dataTransfer ? (e.dataTransfer.getData('text/plain') || null) : null,
    scrollTop: t && typeof t.scrollTop === 'number' ? t.scrollTop : null,
    scrollLeft: t && typeof t.scrollLeft === 'number' ? t.scrollLeft : null,
  };
}

export function morph(selector, html) {
  const root = document.querySelector(selector);
  const tpl = document.createElement('template');
  tpl.innerHTML = html;
  morphChildren(root, tpl.content);
}

// Index-based DOM morph: updates in place so focus, caret and scroll positions
// survive re-renders. Good enough until swapped for idiomorph.
function morphChildren(from, to) {
  const fc = Array.from(from.childNodes);
  const tc = Array.from(to.childNodes);
  const len = Math.max(fc.length, tc.length);
  for (let i = 0; i < len; i++) {
    const f = fc[i], t = tc[i];
    if (t === undefined) { from.removeChild(f); continue; }
    if (f === undefined) { from.appendChild(t.cloneNode(true)); continue; }
    if (f.nodeType !== t.nodeType || (f.nodeType === Node.ELEMENT_NODE && f.tagName !== t.tagName)) {
      from.replaceChild(t.cloneNode(true), f);
      continue;
    }
    if (f.nodeType === Node.TEXT_NODE || f.nodeType === Node.COMMENT_NODE) {
      if (f.nodeValue !== t.nodeValue) f.nodeValue = t.nodeValue;
      continue;
    }
    if (f.nodeType === Node.ELEMENT_NODE) morphElement(f, t);
  }
}

function morphElement(from, to) {
  for (const { name } of Array.from(from.attributes)) {
    if (!to.hasAttribute(name)) from.removeAttribute(name);
  }
  for (const { name, value } of Array.from(to.attributes)) {
    if (from.getAttribute(name) !== value) from.setAttribute(name, value);
  }
  // keep live form state in sync without clobbering the caret (no-op when equal)
  if (from instanceof HTMLInputElement || from instanceof HTMLTextAreaElement || from instanceof HTMLSelectElement) {
    const desired = to.getAttribute('value');
    if (desired !== null && from.value !== desired) from.value = desired;
    if (from instanceof HTMLInputElement && (from.type === 'checkbox' || from.type === 'radio')) {
      const checked = to.hasAttribute('checked');
      if (from.checked !== checked) from.checked = checked;
    }
  }
  morphChildren(from, to);
}

// --- htmx-style interception -------------------------------------------------
// Elements carrying hx-get/hx-post/… (CC.CSX.Htmx attributes) are routed to
// .NET handlers registered via BrowserApp.MapGet/MapPost/… — no server needed.

const HX_VERBS = ['get', 'post', 'put', 'patch', 'delete'];
const hxInflight = new Map();
let hxSeq = 0;

function initHx() {
  for (const ev of ['click', 'submit', 'change']) {
    rootEl.addEventListener(ev, maybeHx, { capture: true });
  }
}

function hxVerb(el) {
  for (const v of HX_VERBS) {
    const path = el.getAttribute(`hx-${v}`);
    if (path !== null) return { method: v.toUpperCase(), path: path.trim() };
  }
  return null;
}

// htmx default triggers: form → submit, form controls → change, else click.
// hx-trigger overrides with its first token (modifiers unsupported for now).
function hxTrigger(el) {
  const spec = el.getAttribute('hx-trigger');
  if (spec) return spec.split(/[\s,]+/)[0];
  if (el.tagName === 'FORM') return 'submit';
  if (el.tagName === 'INPUT' || el.tagName === 'TEXTAREA' || el.tagName === 'SELECT') return 'change';
  return 'click';
}

function maybeHx(e) {
  let el = e.target instanceof Element ? e.target : null;
  while (el && rootEl.contains(el)) {
    const req = hxVerb(el);
    if (req && hxTrigger(el) === e.type) {
      if (e.type === 'submit' || e.type === 'click') e.preventDefault();
      fireHx(el, req, e);
      return;
    }
    el = el.parentElement;
  }
}

function fireHx(el, req, e) {
  const params = {};
  const form = el.tagName === 'FORM' ? el : el.closest('form');
  if (form) {
    for (const [k, v] of new FormData(form).entries()) {
      if (typeof v === 'string') params[k] = v;
    }
  }
  if (el.name && 'value' in el) params[el.name] = String(el.value);
  const reqId = ++hxSeq;
  hxInflight.set(reqId, el);
  dispatchHx(JSON.stringify({ reqId, method: req.method, path: req.path, params, event: eventData(e) }));
}

// called from .NET once the route handler has produced (or declined) a response
export function hxComplete(reqId, html, hasContent) {
  const el = hxInflight.get(reqId);
  hxInflight.delete(reqId);
  if (!el || !el.isConnected) return;
  const swap = (el.getAttribute('hx-swap') || 'innerHTML').split(' ')[0];
  const target = hxTargetOf(el);
  if (!target) return;
  if (swap === 'delete') { target.remove(); return; }
  if (swap === 'none' || !hasContent) return;
  switch (swap) {
    case 'outerHTML': target.outerHTML = html; break;
    case 'beforebegin':
    case 'afterbegin':
    case 'beforeend':
    case 'afterend': target.insertAdjacentHTML(swap, html); break;
    case 'morph': { const tpl = document.createElement('template'); tpl.innerHTML = html; morphChildren(target, tpl.content); break; }
    default: target.innerHTML = html;
  }
}

function hxTargetOf(el) {
  const spec = el.getAttribute('hx-target');
  if (!spec || spec === 'this') return el;
  if (spec.startsWith('closest ')) return el.closest(spec.slice(8));
  if (spec.startsWith('find ')) return el.querySelector(spec.slice(5));
  if (spec === 'next') return el.nextElementSibling;
  if (spec === 'previous') return el.previousElementSibling;
  return document.querySelector(spec);
}
