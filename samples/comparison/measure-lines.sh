#!/usr/bin/env bash
# Counts application source lines (total / non-blank) for each implementation.
# App source = the files an agent writes the dashboard into, excluding generated
# output, deps, the shared dashboard.css, and host boilerplate (index.html/main.js).
set -euo pipefail
cd "$(dirname "$0")"

count() { # $1 = label, rest = files
  local label="$1"; shift
  local total=0 nonblank=0
  for f in "$@"; do
    [ -f "$f" ] || continue
    total=$((total + $(wc -l < "$f")))
    nonblank=$((nonblank + $(grep -cve '^[[:space:]]*$' "$f" || true)))
  done
  printf "%-8s total=%-5s non-blank=%-5s files: %s\n" "$label" "$total" "$nonblank" "$*"
}

echo "== htnet =="
count htnet $(find htnet -name '*.cs' -not -path '*/obj/*' -not -path '*/bin/*')

echo "== blazor =="
count blazor $(find blazor \( -name '*.razor' -o -name '*.cs' \) -not -path '*/obj/*' -not -path '*/bin/*')

echo "== react =="
count react $(find react/src -name '*.jsx' -o -name '*.js' 2>/dev/null | grep -v node_modules)
