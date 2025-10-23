
document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll('.btn-edit').forEach(btn => {
        btn.addEventListener('click', function () {
            const id = this.dataset.noteId;
            toggleEdit(id, true);
        });
    });

    document.querySelectorAll('.btn-cancel').forEach(btn => {
        btn.addEventListener('click', function () {
            const id = this.dataset.noteId;
            toggleEdit(id, false);
        });
    });

    function toggleEdit(id, show) {
        const form = document.getElementById('edit-form-' + id);
        const content = document.getElementById('content-' + id);
        if (!form || !content) return;
        form.style.display = show ? 'block' : 'none';
        content.style.display = show ? 'none' : 'block';
    }
});
