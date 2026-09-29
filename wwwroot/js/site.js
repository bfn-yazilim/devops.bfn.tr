const csrfToken = () => document.querySelector('input[name="__RequestVerificationToken"]')?.value;

window.setupStepSorting = () => {
  const list = document.querySelector('#steps');
  if (!list) return;
  let dragged;
  list.querySelectorAll('.step-item').forEach(item => {
    item.addEventListener('dragstart', () => { dragged = item; item.classList.add('dragging'); });
    item.addEventListener('dragend', async () => {
      item.classList.remove('dragging');
      const ids = [...list.querySelectorAll('.step-item')].map(x => Number(x.dataset.stepId));
      const response = await fetch(`/Projects/ReorderSteps?projectId=${list.dataset.projectId}`, { method: 'POST', headers: { 'Content-Type': 'application/json', 'RequestVerificationToken': csrfToken() }, body: JSON.stringify(ids) });
      if (!response.ok) { alert('Sıralama kaydedilemedi. Sayfa yenilenecek.'); location.reload(); }
    });
    item.addEventListener('dragover', event => {
      event.preventDefault();
      if (!dragged || dragged === item) return;
      const box = item.getBoundingClientRect();
      list.insertBefore(dragged, event.clientY < box.top + box.height / 2 ? item : item.nextSibling);
    });
  });
};

window.openCardDetail = async (id) => {
  const dialog = document.querySelector('#card_detail');
  const content = document.querySelector('#card_detail_content');
  if (!dialog || !content) return;
  content.innerHTML = '<div class="p-6 text-center text-base-content/50">Yükleniyor…</div>';
  dialog.showModal();
  try {
    const response = await fetch(`/Boards/CardDetail/${id}`);
    content.innerHTML = response.ok ? await response.text() : '<div class="p-6 text-error">Kart yüklenemedi.</div>';
  } catch { content.innerHTML = '<div class="p-6 text-error">Kart yüklenemedi.</div>'; }
};

window.openArchivedCards = async (boardId) => {
  const dialog = document.querySelector('#archived_cards');
  const content = document.querySelector('#archived_cards_content');
  if (!dialog || !content) return;
  content.innerHTML = '<div class="p-6 text-center text-base-content/50">Yükleniyor…</div>';
  dialog.showModal();
  try {
    const response = await fetch(`/Boards/ArchivedCards?boardId=${boardId}`);
    content.innerHTML = response.ok ? await response.text() : '<div class="p-6 text-error">Yüklenemedi.</div>';
  } catch { content.innerHTML = '<div class="p-6 text-error">Yüklenemedi.</div>'; }
};

window.setupBoard = () => {
  const columns = document.querySelector('.board-columns');
  if (columns) {
    let draggedColumn;
    columns.querySelectorAll('.board-column').forEach(col => {
      col.addEventListener('dragstart', event => { draggedColumn = col; col.classList.add('dragging'); event.stopPropagation(); });
      col.addEventListener('dragend', async event => {
        event.stopPropagation();
        col.classList.remove('dragging');
        const ids = [...columns.querySelectorAll('.board-column')].map(x => Number(x.dataset.columnId));
        const response = await fetch(`/Boards/ReorderColumns?boardId=${columns.dataset.boardId}`, { method: 'POST', headers: { 'Content-Type': 'application/json', 'RequestVerificationToken': csrfToken() }, body: JSON.stringify(ids) });
        if (!response.ok) { alert('Kolon sıralaması kaydedilemedi. Sayfa yenilenecek.'); location.reload(); }
      });
    });
    columns.addEventListener('dragover', event => {
      if (!draggedColumn) return;
      event.preventDefault();
      const after = [...columns.querySelectorAll('.board-column:not(.dragging)')].find(x => event.clientX < x.getBoundingClientRect().left + x.offsetWidth / 2);
      columns.insertBefore(draggedColumn, after || null);
    });
  }

  let dragged;
  document.querySelectorAll('.board-card').forEach(card => {
    card.addEventListener('dragstart', () => { dragged = card; card.classList.add('dragging'); });
    card.addEventListener('dragend', async () => {
      card.classList.remove('dragging');
      const list = card.closest('.card-list');
      const sortOrder = [...list.querySelectorAll('.board-card')].indexOf(card);
      const response = await fetch('/Boards/MoveCard', { method: 'POST', headers: { 'Content-Type': 'application/json', 'RequestVerificationToken': csrfToken() }, body: JSON.stringify({ cardId: Number(card.dataset.cardId), columnId: Number(list.dataset.columnId), sortOrder }) });
      if (!response.ok) { alert('Kart taşınamadı. Sayfa yenilenecek.'); location.reload(); }
    });
  });
  document.querySelectorAll('.card-list').forEach(list => list.addEventListener('dragover', event => {
    event.preventDefault();
    if (!dragged) return;
    const after = [...list.querySelectorAll('.board-card:not(.dragging)')].find(x => event.clientY < x.getBoundingClientRect().top + x.offsetHeight / 2);
    list.insertBefore(dragged, after || null);
  }));

  document.querySelectorAll('.add-card-area').forEach(area => {
    const btn = area.querySelector('.add-card-btn');
    const form = area.querySelector('.add-card-form');
    const textarea = form.querySelector('textarea');
    const open = () => { btn.classList.add('hidden'); form.classList.remove('hidden'); textarea.focus(); };
    const close = () => { form.classList.add('hidden'); btn.classList.remove('hidden'); textarea.value = ''; };
    btn.addEventListener('click', open);
    form.querySelector('.cancel-add-card').addEventListener('click', close);
    textarea.addEventListener('keydown', event => {
      if (event.key === 'Enter' && !event.shiftKey) { event.preventDefault(); form.requestSubmit(); }
      if (event.key === 'Escape') close();
    });
  });
};
