// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener("DOMContentLoaded", function () {
    // DataTables
    $('#itemsTable').DataTable();

    // Item Edit Modal
    document.querySelectorAll('.edit-btn').forEach(button => {
        button.addEventListener('click', function () {
            var itemId = this.getAttribute('data-id');
            var itemName = this.closest('tr').querySelector('td:nth-child(2)').textContent;
            var itemDescription = this.closest('tr').querySelector('td:nth-child(3)').textContent;

            if (document.getElementById('editItemId')) {
                // If it's an item modal
                document.getElementById('editItemId').value = itemId;
                document.getElementById('editItemName').value = itemName;
                document.getElementById('editItemDescription').value = itemDescription;

                var editModal = new bootstrap.Modal(document.getElementById('editItemModal'));
                editModal.show();
            } else {
                // If it's a borrower modal
                var borrowerName = this.closest('tr').querySelector('td:nth-child(1)').textContent.trim();
                var borrowerAddress = this.closest('tr').querySelector('td:nth-child(2)').textContent.trim();

                document.getElementById('editBorrowerId').value = itemId;
                document.getElementById('editBorrowerName').value = borrowerName;
                document.getElementById('editBorrowerAddress').value = borrowerAddress;

                document.getElementById('editBorrowerForm').action = `/Borrowers/EditBorrower/${itemId}`;

                var editBorrowerModal = new bootstrap.Modal(document.getElementById('editBorrowerModal'));
                editBorrowerModal.show();
            }
        });
    });

    // Archive item logic
    document.querySelectorAll('.archive-btn').forEach(button => {
        button.addEventListener('click', function () {
            var itemId = this.getAttribute('data-id');
            if (confirm('Are you sure you want to archive this item?')) {
                var form = document.querySelector('#archiveForm-' + itemId);
                form.submit();
            }
        });
    });

    // Auto-hide alerts
    setTimeout(function () {
        document.querySelectorAll('.alert').forEach(alert => {
            alert.style.display = 'none';
        });
    }, 3000);
});
