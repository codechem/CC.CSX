// CC.CSX.Browser runtime glue.
// Shipped as an embedded resource and imported as an ES module via a data: URL,
// so host apps need no static-asset or main.js wiring beyond starting the runtime.

let rootEl = null;
let dispatch = null;
const listening = new Set();

export function init(selector, dispatchFn) {
  rootEl = document.querySelector(selector);
  if (!rootEl) throw new Error(`CC.CSX.Browser: mount root '${selector}' not found`);
  dispatch = dispatchFn;
}

// One capture-phase listener per event type on the mount root.
// Capture catches non-bubbling events (focus, blur, scroll) too.
export function ensureListener(name) {
  if (!rootEl || listening.has(name)) return;
  listening.add(name);
  rootEl.addEventListener(name, handle, { capture: true });
}

function handle(e) {
  const attr = `data-ht-on-${e.type}`;
  const el = e.target instanceof Element ? e.target.closest(`[${attr}]`) : null;
  if (!el || !rootEl.contains(el)) return;
  // a handled submit would otherwise reload the page
  if (e.type === 'submit') e.preventDefault();
  dispatch(el.getAttribute(attr), payload(e));
}

function payload(e) {
  const t = e.target;
  return JSON.stringify({
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
    scrollTop: t && typeof t.scrollTop === 'number' ? t.scrollTop : null,
    scrollLeft: t && typeof t.scrollLeft === 'number' ? t.scrollLeft : null,
  });
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
