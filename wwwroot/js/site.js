// Place this script in an external JavaScript file (e.g., your-script.js)

function openItemDetailsModal(name, description, quantity, price) {
    // Set the item details in the modal
    document.getElementById("itemName").textContent = name;
    document.getElementById("itemDescription").textContent = description;
    document.getElementById("itemQuantity").textContent = quantity;
    document.getElementById("itemPrice").textContent = price;

    // Open the modal
    $('#itemDetailsModal').modal('show');
}


function closeCreateModal() {
    $('#createModal').modal('hide');
}