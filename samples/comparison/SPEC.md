# Pulse — Analytics Dashboard (shared spec)

Build a **single-page analytics dashboard** called **Pulse**. This is a client-side
interactive SPA. The same spec is implemented in three stacks (Blazor, React, htnet);
your job is one of them. Match this spec exactly so the three are comparable.

All data is **mock/in-memory** — no backend, no network calls. Generate it in code.

## Layout (top to bottom)

1. **Top bar**
   - Brand on the left: a small colored dot (`<span class="dot">`), the title `Pulse`,
     and a muted subtitle `Analytics overview`.
   - Controls on the right:
     - A **date-range `<select class="select">`** with options: `Today`, `Last 7 days`,
       `Last 30 days` (default `Last 7 days`).
     - A **Refresh `<button class="btn btn-primary">`**.

2. **KPI row** — a `.kpi-grid` of **four** `.card.kpi` cards. Each card:
   - `.kpi-label` (uppercase label)
   - `.kpi-value` (the big number)
   - `.kpi-delta` with class `up` or `down` and text like `▲ 12.4%` / `▼ 3.1%`
   - The four KPIs: **Revenue** (currency, e.g. `$48,250`), **Orders** (integer),
     **Visitors** (integer), **Conversion** (percent, e.g. `3.8%`).

3. **Revenue chart section** — a `.section` with `.section-head` (title
   `Revenue over time`) and a `.card` containing a `.chart`. The chart is a row of
   bar columns, one per data point. Each column is:
   ```
   <div class="bar-col"><div class="bar" style="height:NN%"></div><span class="bar-label">Mon</span></div>
   ```
   Bar height is a percentage of the max value in the current dataset.
   7 bars (weekday labels Mon–Sun) for the 7-day range; use a sensible set for the
   other ranges (e.g. `Today` → hourly buckets, `Last 30 days` → 4 weekly buckets).

4. **Recent orders section** — a `.section` with a `.section-head`: title
   `Recent orders` on the left, and on the right a **status filter** as
   `.filter-tabs` containing `.filter-tab` buttons: `All`, `Paid`, `Pending`,
   `Refunded` (the active one has the extra class `active`).
   Below, a `.card` wrapping a `<table class="table">` with columns:
   **Order** (id, render in a `.mono` cell, e.g. `#10428`), **Customer**,
   **Status** (a `.badge` whose extra class is `paid` / `pending` / `refunded`),
   **Amount** (right-aligned, use `<td class="num">`, currency).
   Seed ~10 orders with mixed statuses.

## Interactivity (all client-side, in-memory state)

- **Date-range select** changes the KPI values **and** the chart dataset. Provide three
  distinct datasets (one per range) so switching is visibly different.
- **Refresh button** re-randomizes the KPI numbers and the chart values for the
  currently selected range (deltas may flip up/down), and re-rolls the orders list.
- **Status filter tabs** filter the orders table to the selected status; `All` shows
  everything. Update the `active` class on the chosen tab. If a filter yields no rows,
  show a single full-width `.empty` cell with text `No orders`.
- **Sortable Amount column**: the `Amount` `<th>` has class `sortable`; clicking it
  toggles ascending/descending sort of the table by amount. Show a `▲`/`▼` indicator
  in a `<span class="arrow">` inside that header for the active direction.

## Styling rules (important for fairness)

- Use **only** the classes defined in `dashboard.css` (provided, already linked in the
  page). Do **not** add new CSS, do not edit `dashboard.css`, do not pull in a CSS
  framework. The only inline style you may set is the bar `height:NN%`.
- Keep the markup structure as described so all three render identically.

## Scope rules

- Keep everything in the app's main view/component file(s) for your stack — don't
  over-engineer into many files. Idiomatic for your framework, but lean.
- No tests, no README, no comments beyond what's natural. Just the working app.
- When done, the app must build and render the dashboard with all four interactions working.
