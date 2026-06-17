import { useMemo, useState } from 'react'

const RANGES = {
  today: {
    label: 'Today',
    labels: ['00', '04', '08', '12', '16', '20'],
    base: { revenue: 6240, orders: 84, visitors: 1920, conversion: 4.4 },
  },
  '7d': {
    label: 'Last 7 days',
    labels: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'],
    base: { revenue: 48250, orders: 612, visitors: 14820, conversion: 3.8 },
  },
  '30d': {
    label: 'Last 30 days',
    labels: ['Wk 1', 'Wk 2', 'Wk 3', 'Wk 4'],
    base: { revenue: 198400, orders: 2580, visitors: 61240, conversion: 4.1 },
  },
}

const STATUSES = ['paid', 'pending', 'refunded']
const CUSTOMERS = [
  'Ava Bennett', 'Liam Carter', 'Noah Diaz', 'Mia Foster', 'Ethan Gray',
  'Sofia Hayes', 'Lucas Reed', 'Emma Shaw', 'Mason Wells', 'Olivia Price',
  'Jack Turner', 'Chloe Vance',
]

const rand = (min, max) => Math.random() * (max - min) + min
const randInt = (min, max) => Math.floor(rand(min, max + 1))
const pick = (arr) => arr[randInt(0, arr.length - 1)]

function buildKpis(range) {
  const b = RANGES[range].base
  const jitter = (v) => v * rand(0.85, 1.15)
  const delta = () => ({ up: Math.random() > 0.4, value: rand(1, 18) })
  return {
    revenue: { value: Math.round(jitter(b.revenue)), delta: delta() },
    orders: { value: Math.round(jitter(b.orders)), delta: delta() },
    visitors: { value: Math.round(jitter(b.visitors)), delta: delta() },
    conversion: { value: jitter(b.conversion), delta: delta() },
  }
}

function buildChart(range) {
  const { labels, base } = RANGES[range]
  const avg = base.revenue / labels.length
  return labels.map((label) => ({
    label,
    value: Math.round(rand(avg * 0.4, avg * 1.4)),
  }))
}

function buildOrders() {
  return Array.from({ length: 10 }, (_, i) => ({
    id: 10400 + i * 100 + randInt(1, 99),
    customer: pick(CUSTOMERS),
    status: pick(STATUSES),
    amount: Math.round(rand(18, 940) * 100) / 100,
  }))
}

const cap = (s) => s.charAt(0).toUpperCase() + s.slice(1)
const fmtMoney = (n) =>
  '$' + n.toLocaleString('en-US', { maximumFractionDigits: 0 })
const fmtMoney2 = (n) =>
  '$' + n.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })
const fmtInt = (n) => n.toLocaleString('en-US')

function KpiCard({ label, value, delta }) {
  return (
    <div className="card kpi">
      <p className="kpi-label">{label}</p>
      <p className="kpi-value">{value}</p>
      <span className={`kpi-delta ${delta.up ? 'up' : 'down'}`}>
        {delta.up ? '▲' : '▼'} {delta.value.toFixed(1)}%
      </span>
    </div>
  )
}

export default function App() {
  const [range, setRange] = useState('7d')
  const [kpis, setKpis] = useState(() => buildKpis('7d'))
  const [chart, setChart] = useState(() => buildChart('7d'))
  const [orders, setOrders] = useState(() => buildOrders())
  const [filter, setFilter] = useState('all')
  const [sortDir, setSortDir] = useState(null)

  const onRangeChange = (e) => {
    const r = e.target.value
    setRange(r)
    setKpis(buildKpis(r))
    setChart(buildChart(r))
  }

  const onRefresh = () => {
    setKpis(buildKpis(range))
    setChart(buildChart(range))
    setOrders(buildOrders())
  }

  const onSort = () => setSortDir((d) => (d === 'asc' ? 'desc' : 'asc'))

  const maxBar = useMemo(() => Math.max(...chart.map((d) => d.value), 1), [chart])

  const visibleOrders = useMemo(() => {
    let rows = filter === 'all' ? orders : orders.filter((o) => o.status === filter)
    if (sortDir) {
      rows = [...rows].sort((a, b) =>
        sortDir === 'asc' ? a.amount - b.amount : b.amount - a.amount,
      )
    }
    return rows
  }, [orders, filter, sortDir])

  return (
    <div className="app">
      <div className="topbar">
        <div className="brand">
          <span className="dot"></span>
          <h1>Pulse</h1>
          <small>Analytics overview</small>
        </div>
        <div className="controls">
          <select className="select" value={range} onChange={onRangeChange}>
            {Object.entries(RANGES).map(([key, r]) => (
              <option key={key} value={key}>{r.label}</option>
            ))}
          </select>
          <button className="btn btn-primary" onClick={onRefresh}>Refresh</button>
        </div>
      </div>

      <div className="kpi-grid">
        <KpiCard label="Revenue" value={fmtMoney(kpis.revenue.value)} delta={kpis.revenue.delta} />
        <KpiCard label="Orders" value={fmtInt(kpis.orders.value)} delta={kpis.orders.delta} />
        <KpiCard label="Visitors" value={fmtInt(kpis.visitors.value)} delta={kpis.visitors.delta} />
        <KpiCard label="Conversion" value={`${kpis.conversion.value.toFixed(1)}%`} delta={kpis.conversion.delta} />
      </div>

      <div className="section">
        <div className="section-head">
          <h2 className="section-title">Revenue over time</h2>
        </div>
        <div className="card">
          <div className="chart">
            {chart.map((d, i) => (
              <div className="bar-col" key={i}>
                <div className="bar" style={{ height: `${Math.round((d.value / maxBar) * 100)}%` }}></div>
                <span className="bar-label">{d.label}</span>
              </div>
            ))}
          </div>
        </div>
      </div>

      <div className="section">
        <div className="section-head">
          <h2 className="section-title">Recent orders</h2>
          <div className="filter-tabs">
            {['all', ...STATUSES].map((s) => (
              <button
                key={s}
                className={`filter-tab ${filter === s ? 'active' : ''}`}
                onClick={() => setFilter(s)}
              >
                {s === 'all' ? 'All' : cap(s)}
              </button>
            ))}
          </div>
        </div>
        <div className="card">
          <table className="table">
            <thead>
              <tr>
                <th>Order</th>
                <th>Customer</th>
                <th>Status</th>
                <th className="sortable num" onClick={onSort}>
                  Amount
                  {sortDir && <span className="arrow">{sortDir === 'asc' ? '▲' : '▼'}</span>}
                </th>
              </tr>
            </thead>
            <tbody>
              {visibleOrders.length === 0 ? (
                <tr>
                  <td className="empty" colSpan={4}>No orders</td>
                </tr>
              ) : (
                visibleOrders.map((o) => (
                  <tr key={o.id}>
                    <td className="mono">#{o.id}</td>
                    <td>{o.customer}</td>
                    <td><span className={`badge ${o.status}`}>{cap(o.status)}</span></td>
                    <td className="num">{fmtMoney2(o.amount)}</td>
                  </tr>
                ))
              )}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  )
}
