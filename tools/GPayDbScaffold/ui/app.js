(() => {
  const state = {
    manifest: null,
    selectedIndex: -1,
    filter: '',
  };

  const ICONS = {
    Added: 'i-plus',
    Modified: 'i-edit',
    Formatting: 'i-spark',
    Unchanged: 'i-dash',
    Skipped: 'i-skip',
    MissingInDb: 'i-skip',
    Removed: 'i-close',
  };

  const TINTS = {
    Added: 'tint-blue',
    Modified: 'tint-amber',
    Formatting: 'tint-blue',
    Unchanged: 'tint-green',
    Skipped: 'tint-rose',
    MissingInDb: 'tint-rose',
    Removed: 'tint-rose',
  };

  const els = {
    summary: document.getElementById('summary-cards'),
    meta: document.getElementById('meta-line'),
    list: document.getElementById('table-list'),
    filter: document.getElementById('filter'),
    detail: document.getElementById('detail'),
    empty: document.getElementById('detail-empty'),
    title: document.getElementById('detail-title'),
    sub: document.getElementById('detail-sub'),
    include: document.getElementById('detail-include'),
    sql: document.getElementById('sql-delta'),
    modelDiff: document.getElementById('model-diff'),
    cols: document.getElementById('col-list'),
    status: document.getElementById('footer-status'),
    toast: document.getElementById('toast'),
  };

  function icon(id, size = 16) {
    return `<svg class="ico" width="${size}" height="${size}" aria-hidden="true"><use href="#${id}"/></svg>`;
  }

  function toast(msg, kind = 'ok') {
    els.toast.textContent = msg;
    els.toast.className = `toast ${kind}`;
    clearTimeout(toast._t);
    toast._t = setTimeout(() => els.toast.classList.add('hidden'), 3200);
  }

  function renderConnection() {
    const source = state.manifest.connectionSource || state.manifest.connection || '';
    const masked = state.manifest.connectionMasked || '';
    const userName = (state.manifest.userName || '').trim();
    const banner = document.getElementById('conn-banner');
    const srcEl = document.getElementById('conn-source');
    const maskEl = document.getElementById('conn-masked');
    const optSrc = document.getElementById('options-conn-source');
    const optMask = document.getElementById('options-conn-masked');
    const sessionLabel = document.getElementById('session-user-label');

    if (sessionLabel) {
      sessionLabel.textContent = userName
        ? `Scaffolding by: ${userName}'s`
        : 'Review session';
      sessionLabel.title = userName ? `Scaffolding by: ${userName}` : '';
    }

    if (srcEl) srcEl.textContent = source ? `Source: ${source}` : 'Connection configured';
    if (maskEl) maskEl.textContent = masked || '—';
    if (optSrc) optSrc.textContent = source || '—';
    if (optMask) optMask.textContent = masked || '';
    if (banner) banner.hidden = !(source || masked);

    if (els.meta) {
      els.meta.textContent = [
        userName ? `Scaffolding by: ${userName}'s` : null,
        source || 'Connection configured',
        masked || null,
        `Generated ${new Date(state.manifest.generatedAt).toLocaleString()}`,
        state.manifest.ignoreAbpTables ? 'ABP tables ignored' : 'ABP tables included',
      ].filter(Boolean).join(' · ');
    }
  }

  async function load() {
    const res = await fetch('/api/manifest');
    if (!res.ok) throw new Error('Failed to load manifest');
    state.manifest = await res.json();
    // Default: nothing selected — user opts in on the left list
    (state.manifest.tables || []).forEach((t) => {
      t.included = false;
    });
    renderConnection();
    renderSummary();
    renderList();
    updateStatus();
    document.body.classList.add('is-ready');
  }

  function renderSummary() {
    const s = state.manifest.summary || {};
    els.summary.innerHTML = [
      card(s.added, 'Added', 'add', 'i-plus'),
      card(s.modified, 'Modified', 'mod', 'i-edit'),
      card(s.formatting, 'Formatting', 'fmt', 'i-spark'),
      card(s.unchanged, 'Unchanged', 'ok', 'i-dash'),
      card((s.skipped || 0) + (s.missingInDb || 0), 'Skipped', 'skip', 'i-skip'),
    ].join('');
  }

  function card(n, label, cls, ico) {
    return `
      <div class="card ${cls}">
        <div class="card-ico">${icon(ico, 16)}</div>
        <strong>${n ?? 0}</strong>
        <span>${label}</span>
      </div>`;
  }

  function visibleTables() {
    const q = state.filter.trim().toLowerCase();
    return (state.manifest.tables || [])
      .map((t, index) => ({ t, index }))
      .filter(({ t }) => {
        if (!q) return true;
        return (
          (t.tableName || '').toLowerCase().includes(q) ||
          (t.className || '').toLowerCase().includes(q) ||
          (t.changeType || '').toLowerCase().includes(q)
        );
      });
  }

  function isSelectable(t) {
    return t && !['Skipped', 'Unchanged', 'MissingInDb'].includes(t.changeType);
  }

  function tooltipFor(t) {
    switch (t.changeType) {
      case 'Unchanged':
        return 'No changes — model already matches the database';
      case 'Skipped':
        return 'Skipped — hand-maintained or excluded from scaffold';
      case 'MissingInDb':
        return 'Present in Domain models but not returned by scaffold';
      case 'Added':
        return 'New table — include to add the ABP model';
      case 'Modified':
        return 'Schema differs — include to update the model';
      case 'Formatting':
        return 'Cosmetic / attribute differences only';
      default:
        return t.summary || t.changeType || '';
    }
  }

  function syncSelectAllCheckbox() {
    const master = document.getElementById('select-all-tables');
    if (!master || !state.manifest) return;
    const selectable = (state.manifest.tables || []).filter(isSelectable);
    const selected = selectable.filter((t) => t.included);
    master.disabled = selectable.length === 0;
    master.checked = selectable.length > 0 && selected.length === selectable.length;
    master.indeterminate = selected.length > 0 && selected.length < selectable.length;
  }

  function setAllSelectable(included) {
    (state.manifest.tables || []).forEach((t) => {
      if (isSelectable(t)) t.included = included;
    });
    renderList();
    if (state.selectedIndex >= 0) {
      const cur = state.manifest.tables[state.selectedIndex];
      els.include.checked = !!cur.included;
      els.include.disabled = !isSelectable(cur);
    }
    updateStatus();
  }

  function renderList() {
    const rows = visibleTables();
    els.list.innerHTML = rows.map(({ t, index }, i) => {
      const active = index === state.selectedIndex ? 'active' : '';
      const tint = TINTS[t.changeType] || 'tint-blue';
      const ico = ICONS[t.changeType] || 'i-table';
      const locked = !isSelectable(t);
      const tip = tooltipFor(t);
      return `
        <div class="row ${active} ${locked ? 'is-locked' : ''}" data-index="${index}" style="--i:${Math.min(i, 24)}" title="${escapeHtml(tip)}">
          <input type="checkbox" data-include="${index}"
            ${t.included ? 'checked' : ''}
            ${locked ? 'disabled' : ''}
            title="${escapeHtml(tip)}" />
          <span class="row-ico ${tint}">${icon(ico, 14)}</span>
          <div>
            <div class="name">${escapeHtml(t.tableName)}</div>
            <div class="sub">${escapeHtml(t.className)} · ${escapeHtml(t.summary || '')}</div>
          </div>
          <span class="badge ${escapeHtml(t.changeType)}">${escapeHtml(t.changeType)}</span>
        </div>`;
    }).join('') || `
      <div class="empty">
        <span class="empty-ico">${icon('i-search', 28)}</span>
        <strong>No matches</strong>
        <span>Try another filter.</span>
      </div>`;

    els.list.querySelectorAll('.row').forEach((row) => {
      row.addEventListener('click', (e) => {
        if (e.target.matches('input[type=checkbox]')) return;
        selectTable(Number(row.dataset.index));
      });
    });
    els.list.querySelectorAll('input[data-include]').forEach((cb) => {
      cb.addEventListener('change', (e) => {
        e.stopPropagation();
        const i = Number(cb.dataset.include);
        const t = state.manifest.tables[i];
        if (!isSelectable(t)) return;
        t.included = cb.checked;
        if (state.selectedIndex === i) els.include.checked = cb.checked;
        syncSelectAllCheckbox();
        updateStatus();
      });
    });
    syncSelectAllCheckbox();
  }

  async function selectTable(index) {
    state.selectedIndex = index;
    const t = state.manifest.tables[index];
    if (!t) return;
    els.empty.classList.add('hidden');
    els.detail.classList.remove('hidden');
    els.detail.classList.remove('detail');
    void els.detail.offsetWidth;
    els.detail.classList.add('detail');
    els.title.textContent = t.tableName;
    els.sub.textContent = `${t.className} · ${t.changeType}${t.reason ? ' · ' + t.reason : ''}`;
    els.include.checked = !!t.included;
    els.include.disabled = !isSelectable(t);
    if (!isSelectable(t)) {
      els.include.parentElement.title = tooltipFor(t);
    } else {
      els.include.parentElement.title = 'Include this table';
    }

    // Lazy-load SQL/model content (keeps initial manifest small so reload works)
    els.sql.textContent = 'Loading…';
    els.modelDiff.innerHTML = '<p class="empty">Loading model diff…</p>';
    try {
      await ensureTableContent(t);
      els.sql.textContent = t.sqlDelta || '-- No SQL delta';
      renderModelDiff(t);
    } catch (err) {
      els.sql.textContent = `-- Failed to load content: ${err.message || err}`;
      els.modelDiff.innerHTML = `<p class="empty">${escapeHtml(String(err.message || err))}</p>`;
    }
    renderColumns(t);
    renderList();
  }

  async function ensureTableContent(t) {
    if (t.sqlDelta != null && (t.oldContent != null || t.newContent != null || t._contentLoaded)) {
      return;
    }
    const key = t.contentKey || t.className;
    const res = await fetch(`/api/content/${encodeURIComponent(key)}`);
    if (!res.ok) throw new Error(`Content HTTP ${res.status}`);
    const data = await res.json();
    t.sqlDelta = data.sqlDelta || '';
    t.oldContent = data.oldContent || '';
    t.newContent = data.newContent || '';
    t._contentLoaded = true;
  }

  function normalizeNewlines(s) {
    return String(s ?? '').replace(/\r\n/g, '\n').replace(/\r/g, '\n');
  }

  function renderModelDiff(t) {
    const oldText = normalizeNewlines(t.oldContent);
    const newText = normalizeNewlines(t.newContent);
    if (!window.Diff || !window.Diff2Html) {
      els.modelDiff.innerHTML = `<pre class="sql">${escapeHtml(newText || oldText || 'No content')}</pre>`;
      return;
    }
    const patch = Diff.createTwoFilesPatch(
      `${t.className}.cs`,
      `${t.className}.cs`,
      oldText,
      newText,
      'current',
      'scaffolded',
      { context: 4 }
    );
    els.modelDiff.innerHTML = Diff2Html.html(patch, {
      drawFileList: false,
      matching: 'lines',
      outputFormat: 'side-by-side',
      synchronisedScroll: true,
      renderNothingWhenEmpty: true,
    });

    // Real column headers (avoid sticky ::before overlapping code / line numbers)
    const sides = els.modelDiff.querySelectorAll('.d2h-file-side-diff');
    if (sides.length >= 2) {
      const labels = ['Current', 'New (scaffolded)'];
      sides.forEach((side, i) => {
        if (side.querySelector('.diff-side-label')) return;
        const label = document.createElement('div');
        label.className = 'diff-side-label';
        label.textContent = labels[i] || '';
        side.insertBefore(label, side.firstChild);
      });
    }
  }

  function renderColumns(t) {
    const cols = t.columns || [];
    if (!cols.length) {
      els.cols.innerHTML = `
        <div class="empty">
          <span class="empty-ico">${icon('i-cols', 28)}</span>
          <strong>No column changes</strong>
          <span>Nothing to include or exclude here.</span>
        </div>`;
      return;
    }
    els.cols.innerHTML = cols.map((c, ci) => {
      const ico = ICONS[c.change] || 'i-cols';
      const tint = TINTS[c.change] || 'tint-blue';
      return `
        <div class="col-row ${c.isNav ? 'nav-prop' : ''}" style="--i:${Math.min(ci, 20)}">
          <input type="checkbox" data-col="${ci}" ${c.included ? 'checked' : ''} />
          <span class="row-ico ${tint}">${icon(ico, 13)}</span>
          <div>
            <div class="name">${escapeHtml(c.name)} <span class="badge ${escapeHtml(c.change)}">${escapeHtml(c.change)}</span></div>
            <div class="types">${escapeHtml([c.oldType, c.newType].filter(Boolean).join(' → '))}${c.column ? ' · col ' + escapeHtml(c.column) : ''}${c.isNav ? ' · navigation' : ''}</div>
          </div>
        </div>`;
    }).join('');

    els.cols.querySelectorAll('input[data-col]').forEach((cb) => {
      cb.addEventListener('change', () => {
        const ci = Number(cb.dataset.col);
        t.columns[ci].included = cb.checked;
        updateStatus();
      });
    });
  }

  function updateStatus() {
    if (!state.manifest) return;
    const tables = state.manifest.tables || [];
    const included = tables.filter((t) => t.included).length;
    const colExcluded = tables.reduce((n, t) => n + (t.columns || []).filter((c) => !c.included).length, 0);
    els.status.textContent = `${included} table(s) selected · ${colExcluded} column(s) excluded`;
  }

  function escapeHtml(s) {
    return String(s ?? '')
      .replace(/&/g, '&amp;')
      .replace(/</g, '&lt;')
      .replace(/>/g, '&gt;')
      .replace(/"/g, '&quot;');
  }

  document.querySelectorAll('.pivot-item').forEach((tab) => {
    tab.addEventListener('click', () => {
      document.querySelectorAll('.pivot-item').forEach((t) => t.classList.remove('active'));
      document.querySelectorAll('.tab-pane').forEach((p) => p.classList.remove('active'));
      tab.classList.add('active');
      const pane = document.getElementById(`tab-${tab.dataset.tab}`);
      pane.classList.add('active');
    });
  });

  if (els.filter) {
    els.filter.addEventListener('input', () => {
      state.filter = els.filter.value;
      renderList();
    });
  }

  els.include.addEventListener('change', () => {
    if (state.selectedIndex < 0) return;
    state.manifest.tables[state.selectedIndex].included = els.include.checked;
    renderList();
    updateStatus();
  });

  document.getElementById('btn-all').addEventListener('click', () => setAllSelectable(true));
  document.getElementById('btn-none').addEventListener('click', () => setAllSelectable(false));
  document.getElementById('btn-select-all').addEventListener('click', () => setAllSelectable(true));
  document.getElementById('btn-unselect-all').addEventListener('click', () => setAllSelectable(false));
  document.getElementById('select-all-tables').addEventListener('change', (e) => {
    setAllSelectable(e.target.checked);
  });

  // Expand / collapse sections
  document.querySelectorAll('.expander-toggle').forEach((btn) => {
    btn.addEventListener('click', () => {
      const section = btn.closest('.expander');
      if (section && section.classList.contains('is-fullscreen')) return;
      const open = !section.classList.contains('open');
      section.classList.toggle('open', open);
      btn.setAttribute('aria-expanded', String(open));
    });
  });

  // Tables & details — true window fullscreen (Fullscreen API + CSS), limited chrome + OK/Cancel
  const tablesFsRoot = document.getElementById('tables-fs-root');
  const tablesSection = document.getElementById('tables-section');
  const expandBtn = document.getElementById('btn-tables-expand');
  const expandLabel = document.getElementById('tables-expand-label');
  const fsChrome = document.getElementById('fs-chrome');
  const exitFsBtn = document.getElementById('btn-tables-exit-fs');
  const commandBar = document.getElementById('command-bar');
  const confirmOverlay = document.getElementById('confirm-overlay');
  const toastEl = document.getElementById('toast');
  let commandBarHome = commandBar ? commandBar.parentElement : null;
  let overlayHome = confirmOverlay ? confirmOverlay.parentElement : null;
  let toastHome = toastEl ? toastEl.parentElement : null;
  let fsRootHome = null;
  let fsRootNext = null;
  let fsBusy = false;

  function syncExpandUi(on) {
    if (!expandBtn) return;
    expandBtn.setAttribute('aria-pressed', String(on));
    expandBtn.title = on ? 'Exit full screen (Esc)' : 'Full screen (like F11)';
    const use = expandBtn.querySelector('use');
    if (use) use.setAttribute('href', on ? '#i-compress' : '#i-expand');
    if (expandLabel) expandLabel.textContent = on ? 'Exit' : 'Expand';
    if (fsChrome) fsChrome.hidden = !on;
  }

  async function setTablesFullscreen(on) {
    if (!tablesFsRoot || !tablesSection || fsBusy) return;
    fsBusy = true;
    try {
      tablesSection.classList.add('open');
      const toggle = tablesSection.querySelector('.expander-toggle');
      if (toggle) toggle.setAttribute('aria-expanded', 'true');

      document.body.classList.toggle('tables-fullscreen', on);
      tablesFsRoot.classList.toggle('is-fullscreen', on);
      tablesSection.classList.toggle('is-fullscreen', on);
      syncExpandUi(on);

      if (on) {
        // Detach from shell so we cover the whole window (and Fullscreen API works cleanly)
        if (tablesFsRoot.parentElement !== document.body) {
          fsRootHome = tablesFsRoot.parentElement;
          fsRootNext = tablesFsRoot.nextSibling;
          document.body.appendChild(tablesFsRoot);
        }
        // OK/Cancel + dialogs must live inside the fullscreen element
        if (commandBar && commandBar.parentElement !== tablesFsRoot) {
          tablesFsRoot.appendChild(commandBar);
        }
        if (confirmOverlay && confirmOverlay.parentElement !== tablesFsRoot) {
          tablesFsRoot.appendChild(confirmOverlay);
        }
        if (toastEl && toastEl.parentElement !== tablesFsRoot) {
          tablesFsRoot.appendChild(toastEl);
        }
        try {
          if (!document.fullscreenElement && tablesFsRoot.requestFullscreen) {
            await tablesFsRoot.requestFullscreen();
          }
        } catch (_) {
          // CSS inset:0 cover still applies
        }
      } else {
        if (commandBar && commandBarHome && commandBar.parentElement === tablesFsRoot) {
          commandBarHome.appendChild(commandBar);
        }
        if (confirmOverlay && overlayHome && confirmOverlay.parentElement === tablesFsRoot) {
          overlayHome.appendChild(confirmOverlay);
        }
        if (toastEl && toastHome && toastEl.parentElement === tablesFsRoot) {
          toastHome.appendChild(toastEl);
        }
        if (fsRootHome) {
          if (fsRootNext && fsRootNext.parentElement === fsRootHome) {
            fsRootHome.insertBefore(tablesFsRoot, fsRootNext);
          } else {
            fsRootHome.appendChild(tablesFsRoot);
          }
          fsRootHome = null;
          fsRootNext = null;
        }
        try {
          if (document.fullscreenElement) await document.exitFullscreen();
        } catch (_) {}
      }
    } finally {
      fsBusy = false;
    }
  }

  if (expandBtn) {
    expandBtn.addEventListener('click', (e) => {
      e.preventDefault();
      e.stopPropagation();
      setTablesFullscreen(!tablesFsRoot.classList.contains('is-fullscreen'));
    });
  }
  if (exitFsBtn) {
    exitFsBtn.addEventListener('click', (e) => {
      e.preventDefault();
      setTablesFullscreen(false);
    });
  }
  document.addEventListener('keydown', (e) => {
    if (e.key === 'Escape' && tablesFsRoot?.classList.contains('is-fullscreen')) {
      // Browser may exit fullscreen first; still clear our state
      setTablesFullscreen(false);
    }
  });
  document.addEventListener('fullscreenchange', () => {
    if (!document.fullscreenElement && tablesFsRoot?.classList.contains('is-fullscreen')) {
      setTablesFullscreen(false);
    }
  });

  function buildApplyQueue() {
    return (state.manifest.tables || [])
      .filter((t) => t.included)
      .map((t) => ({
        tableName: t.tableName,
        className: t.className,
        excludeColumns: (t.columns || [])
          .filter((c) => !c.included)
          .map((c) => c.name),
      }));
  }

  function openConfirmDialog() {
    const included = (state.manifest.tables || []).filter((t) => t.included);
    const overlay = document.getElementById('confirm-overlay');
    const lead = document.getElementById('confirm-lead');
    const list = document.getElementById('confirm-tables-list');
    const label = document.getElementById('confirm-tables-label');
    const body = document.getElementById('confirm-tables-body');
    const toggle = document.getElementById('confirm-tables-toggle');
    const applyBtn = document.getElementById('btn-confirm-apply');

    showConfirmStep('ask');
    if (applyBtn) {
      applyBtn.disabled = false;
      applyBtn.innerHTML = '<svg class="ico" width="16" height="16"><use href="#i-ok"/></svg> OK';
    }

    if (lead) {
      lead.textContent = included.length === 1
        ? 'This will change 1 table.'
        : `This will change ${included.length} tables.`;
    }
    if (label) {
      label.textContent = included.length
        ? `Show table names (${included.length})`
        : 'No tables selected';
    }
    if (list) {
      list.innerHTML = included.map((t) => `
      <li>
        <strong>${escapeHtml(t.tableName)}</strong>
        <span>${escapeHtml(t.changeType || '')}</span>
      </li>`).join('') || '<li><strong>None</strong><span>—</span></li>';
    }

    if (body) body.classList.remove('open');
    if (toggle) toggle.setAttribute('aria-expanded', 'false');
    if (overlay) overlay.classList.remove('hidden');
  }

  function closeConfirmDialog() {
    document.getElementById('confirm-overlay').classList.add('hidden');
  }

  function setApplyProgress(index, total, tableName, message) {
    const pct = total > 0 ? Math.round((index / total) * 100) : 0;
    const fill = document.getElementById('apply-progress-fill');
    const bar = document.getElementById('apply-progress');
    if (fill) fill.style.width = `${pct}%`;
    if (bar) bar.setAttribute('aria-valuenow', String(pct));
    const pctEl = document.getElementById('apply-progress-pct');
    const countEl = document.getElementById('apply-progress-count');
    const leadEl = document.getElementById('apply-progress-lead');
    const curEl = document.getElementById('apply-current-table');
    if (pctEl) pctEl.textContent = `${pct}%`;
    if (countEl) countEl.textContent = `${index} / ${total}`;
    if (leadEl) {
      leadEl.textContent =
        index >= total ? 'Finishing…' : `Applying table ${index + 1} of ${total}`;
    }
    if (curEl) {
      curEl.textContent = tableName
        ? (message ? `${tableName} — ${message}` : tableName)
        : '—';
    }
  }

  function appendApplyLog(tableName, ok, message) {
    const log = document.getElementById('apply-log');
    if (!log) return;
    const li = document.createElement('li');
    li.className = ok ? 'ok' : 'err';
    li.innerHTML = `<strong>${escapeHtml(tableName)}</strong><span>${escapeHtml(message || (ok ? 'Applied' : 'Failed'))}</span>`;
    log.prepend(li);
    while (log.children.length > 40) log.removeChild(log.lastChild);
  }

  function showConfirmStep(step) {
    const ask = document.getElementById('confirm-step-ask');
    const progress = document.getElementById('confirm-step-progress');
    const done = document.getElementById('confirm-step-done');
    const cancel = document.getElementById('confirm-step-cancel');
    if (ask) ask.classList.toggle('hidden', step !== 'ask');
    if (progress) progress.classList.toggle('hidden', step !== 'progress');
    if (done) done.classList.toggle('hidden', step !== 'done');
    if (cancel) cancel.classList.toggle('hidden', step !== 'cancel');
  }

  function openCancelDialog() {
    const overlay = document.getElementById('confirm-overlay');
    showConfirmStep('cancel');
    if (overlay) overlay.classList.remove('hidden');
  }

  async function confirmCancelAndClose() {
    try {
      await postJson('/api/cancel', {});
    } catch (_) {}
    try {
      window.close();
    } catch (_) {}
    // Fallback if the browser blocks window.close()
    document.body.innerHTML = '<p style="font:14px Segoe UI,sans-serif;padding:24px;color:#5b6b7c">Cancelled. You can close this tab.</p>';
  }

  async function postJson(url, body) {
    console.log('[gpay-scaffold]', 'POST', url, body || null);
    const res = await fetch(url, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json; charset=utf-8' },
      body: body === undefined ? undefined : JSON.stringify(body),
    });
    const data = await res.json().catch(() => ({}));
    console.log('[gpay-scaffold]', res.status, url, data);
    return { res, data };
  }

  async function applyConfirm(ev) {
    if (ev) ev.preventDefault();
    console.log('[gpay-scaffold] OK clicked — starting chunked apply');

    const queue = buildApplyQueue();
    if (queue.length === 0) {
      toast('Select at least one table to apply.', 'err');
      console.warn('[gpay-scaffold] empty apply queue');
      return;
    }

    const applyBtn = document.getElementById('btn-confirm-apply');
    if (applyBtn) {
      applyBtn.disabled = true;
      applyBtn.textContent = 'Applying…';
    }

    // Network first — do not let a missing progress DOM node block the POST
    try {
      const { res: startRes, data: startData } = await postJson('/api/apply/start', {
        total: queue.length,
      });
      if (!startRes.ok || startData.ok === false) {
        throw new Error(startData.message || `Start failed (HTTP ${startRes.status})`);
      }

      showConfirmStep('progress');
      const logEl = document.getElementById('apply-log');
      if (logEl) logEl.innerHTML = '';
      setApplyProgress(0, queue.length, null, null);
      if (els.status) els.status.textContent = `Applying ${queue.length} table(s)…`;

      let failed = 0;
      for (let i = 0; i < queue.length; i++) {
        const item = queue[i];
        setApplyProgress(i, queue.length, item.tableName, 'sending…');
        if (els.status) {
          els.status.textContent = `Applying ${item.tableName} (${i + 1} of ${queue.length})…`;
        }

        const { res, data } = await postJson('/api/apply/one', {
          tableName: item.tableName,
          className: item.className,
          excludeColumns: item.excludeColumns,
          index: i + 1,
          total: queue.length,
        });
        const softOk = res.ok && (data.ok === true || data.skipped === true);
        if (!softOk) failed += 1;
        appendApplyLog(
          item.tableName || item.className,
          softOk,
          data.message || (softOk ? (data.skipped ? 'Skipped' : 'Applied') : `HTTP ${res.status}`)
        );
        setApplyProgress(
          i + 1,
          queue.length,
          item.tableName,
          data.message || (softOk ? 'done' : 'error')
        );
      }

      setApplyProgress(queue.length, queue.length, null, null);
      const { res: finishRes, data: finishData } = await postJson('/api/apply/finish', {});
      if (!finishRes.ok || finishData.ok === false) {
        throw new Error(finishData.message || `Finish failed (HTTP ${finishRes.status})`);
      }

      const appliedN = finishData.appliedCount ?? queue.length - failed;
      const skippedN = finishData.skippedCount ?? failed;
      showConfirmStep('done');
      const doneLead = document.getElementById('confirm-done-lead');
      if (doneLead) {
        doneLead.textContent =
          skippedN > 0
            ? `Applied ${appliedN} table(s), skipped ${skippedN}.`
            : `Applied ${appliedN} table(s).`;
      }
      if (els.status) {
        els.status.textContent = 'Apply complete. You can close this window and return to Visual Studio.';
      }
      const btnOk = document.getElementById('btn-ok');
      const btnCancel = document.getElementById('btn-cancel');
      if (btnOk) btnOk.disabled = true;
      if (btnCancel) btnCancel.disabled = true;
      toast('Apply complete.');
    } catch (err) {
      console.error('[gpay-scaffold] apply failed', err);
      showConfirmStep('ask');
      if (applyBtn) {
        applyBtn.disabled = false;
        applyBtn.innerHTML = '<svg class="ico" width="16" height="16"><use href="#i-ok"/></svg> OK';
      }
      const msg = String(err.message || err);
      toast(msg, 'err');
      if (els.status) els.status.textContent = msg;
    }
  }

  document.getElementById('confirm-tables-toggle').addEventListener('click', () => {
    const toggle = document.getElementById('confirm-tables-toggle');
    const body = document.getElementById('confirm-tables-body');
    const open = !body.classList.contains('open');
    body.classList.toggle('open', open);
    toggle.setAttribute('aria-expanded', String(open));
    document.getElementById('confirm-tables-label').textContent = open
      ? 'Hide table names'
      : `Show table names (${(state.manifest.tables || []).filter((t) => t.included).length})`;
  });

  document.getElementById('btn-confirm-cancel').addEventListener('click', closeConfirmDialog);
  document.getElementById('confirm-overlay').addEventListener('click', (e) => {
    if (e.target.id !== 'confirm-overlay') return;
    if (!document.getElementById('confirm-step-done')?.classList.contains('hidden')) return;
    if (!document.getElementById('confirm-step-progress')?.classList.contains('hidden')) return;
    closeConfirmDialog();
  });
  document.getElementById('btn-confirm-apply').addEventListener('click', (e) => {
    applyConfirm(e).catch((err) => {
      console.error('[gpay-scaffold] unhandled', err);
      toast(String(err.message || err), 'err');
    });
  });

  document.getElementById('btn-ok').addEventListener('click', () => {
    try {
      openConfirmDialog();
    } catch (err) {
      console.error('[gpay-scaffold] open confirm failed', err);
      toast(String(err.message || err), 'err');
    }
  });

  document.getElementById('btn-cancel').addEventListener('click', () => {
    try {
      openCancelDialog();
    } catch (err) {
      console.error('[gpay-scaffold] open cancel failed', err);
      toast(String(err.message || err), 'err');
    }
  });
  document.getElementById('btn-cancel-keep')?.addEventListener('click', closeConfirmDialog);
  document.getElementById('btn-cancel-confirm')?.addEventListener('click', () => {
    confirmCancelAndClose().catch((err) => {
      console.error('[gpay-scaffold] cancel failed', err);
      toast(String(err.message || err), 'err');
    });
  });

  load().then(updateStatus).catch((err) => {
    console.error('[gpay-scaffold] load failed', err);
    els.status.textContent = String(err.message || err);
    toast(String(err.message || err), 'err');
  });
})();
