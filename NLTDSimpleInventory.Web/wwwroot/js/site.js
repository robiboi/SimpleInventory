﻿// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener("DOMContentLoaded", function () {

    const borrowersData = JSON.parse(document.querySelector('#itemsTable').getAttribute('data-borrowers'));
    const columnCount = document.querySelectorAll('#itemsTable thead th').length;
    const table = $('#itemsTable').DataTable({
        responsive: true,
        order: [],
        columnDefs: [
            { targets: [0, columnCount - 1], orderable: false }
        ]
    });

    function format(borrowerId) {
        const borrower = borrowersData.find(b => b.Id === borrowerId);
        if (!borrower || !borrower.BorrowHistory || borrower.BorrowHistory.length === 0) {
            return '<div class="p-2 text-muted">No borrow history.</div>';
        }

        let html = `
            <table class="table table-bordered mb-0 small" style="background-color: #f0f8ff; margin: 0 !important;">
                <thead>
                    <tr>
                        <th>Item SKU</th>
                        <th>Item Name</th>
                        <th>Date Borrowed</th>
                        <th>Date Returned</th>
                    </tr>
                </thead>
                <tbody>
        `;
        borrower.BorrowHistory.forEach(h => {
            html += `
                <tr>
                    <td>${h.ItemSKU}</td>
                    <td>${h.ItemName}</td>
                    <td>${h.DateBorrowed}</td>
                    <td>${h.DateReturned}</td>
                </tr>
            `;
        });
        html += `</tbody></table>`;
        return html;
    }

    $('#itemsTable tbody').on('click', 'td.details-control', function () {
        const tr = $(this).closest('tr');
        const row = table.row(tr); 
        const borrowerId = tr.data('borrower-id');

        if (row.child.isShown()) {
            row.child.hide();
            tr.removeClass('shown');
            $(this).find('i').removeClass('fa-caret-up').addClass('fa-caret-down');
        } else {
            row.child(format(borrowerId)).show();
            tr.addClass('shown');
            $(this).find('i').removeClass('fa-caret-down').addClass('fa-caret-up');
        }
    });

    const newBorrowerCheckbox = document.getElementById("IsNewBorrower");
    const newBorrowerFields = document.getElementById("newBorrowerFields");

    IsNewBorrower.addEventListener("change", function () {
        newBorrowerFields.style.display = this.checked ? "block" : "none";
        searchBorrowerInput.disabled = this.checked;
    });

    const borrowButtons = document.querySelectorAll(".borrow-btn");
    const itemInput = document.getElementById("ItemInput");
    const itemIdInput = document.getElementById("selectedItemId");

    borrowButtons.forEach(button => {
        button.addEventListener("click", function () {
            const itemName = this.getAttribute("data-name");
            const itemId = this.getAttribute("data-id");

            itemInput.value = itemName;
            itemIdInput.value = itemId;
        });
    });

    document.getElementById("searchBorrowerInput").addEventListener("input", function () {
        search(this.value.trim(), "/Borrowers/SearchBorrowers", "borrowerResults", (id, name) => {
            document.getElementById("searchBorrowerInput").value = name;
            document.getElementById("selectedBorrowerId").value = id;
            document.getElementById("borrowerResults").innerHTML = "";
        });
    });

    var successMessage = document.getElementById("successMessage");
    if (successMessage) {
        setTimeout(function () {
            successMessage.style.display = "none";
        }, 5000); 
    }

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

function search(query, endpoint, resultContainer, onSelect) {
    if (query.length === 0) {
        document.getElementById(resultContainer).innerHTML = "";
        return;
    }

    fetch(`${endpoint}?query=${encodeURIComponent(query)}`)
        .then(response => response.json())
        .then(data => {
            let results = document.getElementById(resultContainer);
            results.innerHTML = "";

            if (data.length === 0) {
                results.innerHTML = "<p class='text-muted'>No results found</p>";
                return;
            }

            data.forEach(item => {
                const option = document.createElement("a");
                option.href = "#";
                option.classList.add("list-group-item", "list-group-item-action");
                option.textContent = item.name;
                option.dataset.id = item.id;

                option.addEventListener("click", function () {
                    onSelect(this.dataset.id, this.textContent);
                    results.innerHTML = "";
                });

                results.appendChild(option);
            });
        })
        .catch(error => console.error("Error fetching data:", error));
}