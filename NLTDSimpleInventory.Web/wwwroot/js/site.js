// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener("DOMContentLoaded", function () {
    $('#itemsTable').DataTable();

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


