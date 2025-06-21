$(document).ready(function () {
    FetchSupp();
});

function FetchSupp() {
    $.ajax({
        url: '/Supplier/GetAllSupplier',
        type: 'GET',
        dataType: 'json',
        contentType: 'Application/json; charset=UTF-8',
        success: function (result) {
            var rows = '<option value="0">--Select Supplier--</option>';
            $.each(result, function (index, supplier) {
                rows += `
                        <option value="${supplier.supplierId}">${supplier.name}</option>`;
            });
            $('#supp').html(rows);
            CheckCartLock();
        },
        error: function () {
            alert('Error fetching suppliers');
        }
    });
}
function FetchMed() {
    $.ajax({
        url: '/Purchase/GetMedicines',
        type: 'GET',
        dataType: 'json',
        contentType: 'Application/json; charset=UTF-8',
        success: function (result) {
            var rows = '';
            $.each(result, function (index, med) {
                var quantityOptions = '';
                for (var i = 10; i <= 100; i += 10) {
                    quantityOptions += `<option value="${i}">${i}</option>`;
                }
                rows += `
                        <div class="col-md-4 d-flex">
                            <div class="card border border-primary shadow-sm rounded-4 w-100">
                                <div class="card-body">
                                    <h5 class="card-title fw-bold text-primary">
                                        <i class="fas fa-capsules me-1"></i>${med.name}
                                    </h5>
                                    <p class="mb-1"><i class="fas fa-tags me-1 text-muted"></i><strong>Category:</strong> ${med.category}</p>
                                    <p class="mb-1"><i class="fas fa-industry me-1 text-muted"></i><strong>Manufacturer:</strong> ${med.manufacturer}</p>
                                    <p class="mb-1"><i class="fas fa-barcode me-1 text-muted"></i><strong>Batch No:</strong> ${med.batchNo}</p>
                                    <p class="mb-1"><i class="fas fa-calendar-alt me-1 text-muted"></i><strong>Expiry:</strong> ${med.expiryDate}</p>
                                    <p class="mb-1"><i class="fas fa-rupee-sign me-1 text-muted"></i><strong>Price Per Unit:</strong> ₹${med.pricePerUnit}</p>
                                    <p class="mb-2"><i class="fas fa-sort-amount-up-alt me-1 text-muted"></i><strong>Quantity:</strong>
                                        <select name="Quantity" class="form-select form-select-sm shadow-sm mt-1" id="Quantity_${med.medicineId}">
                                            ${quantityOptions}
                                        </select>
                                    </p>
                                </div>
                                <div class="card-footer bg-light border-top border-primary rounded-bottom-4 d-flex justify-content-end">
                                    <button type="button" class="btn btn-outline-success btn-sm px-3"
                                        onclick="addCart('${med.medicineId}', '${med.pricePerUnit}')">
                                        <i class="fas fa-cart-plus me-1"></i> Add to Cart
                                    </button>
                                </div>
                            </div>
                        </div>
                        `;

            });
            $('#cardContainer').html(rows);
        },
        error: function () {
            alert('Error fetching meds');
        }
    });
}

function FetchCart(){
    $.ajax({
        url: '/Purchase/GetCart',
        type: 'GET',
        dataType: 'json',
        contentType: 'Application/json; charset=UTF-8',
        success: function (data) {
            let rows = '';
            if (data.length === 0) {
                rows = `<tr><td colspan="7" class="text-center">🛒 Cart is empty</td></tr>`;
                $('#supp').attr('disabled', false);
                $('#lockMsg').remove();
            } else {
                let grandTotal = 0;
                $.each(data, function (i, item) {
                    let total = item.quantity * item.ppu;
                    grandTotal += total
                    rows += `
                    <tr>
                        <td>${item.sname}</td>
                        <td>${item.mname}</td>
                        <td>${item.quantity}</td>
                        <td>${item.ppu}</td>
                        <td><strong>${total.toFixed(2)}</strong></td>
                        <td>
                            <button type="button" class="btn btn-outline-danger btn-sm me-2" onclick="delSingCart('${item.cartId}')">
                            <i class="bi bi-trash3-fill me-1"></i>
                            </button>
                        </td>
                    </tr>`;
                });
                let cgst = grandTotal * 0.05;
                let sgst = grandTotal * 0.05;
                let finalTotal = grandTotal + cgst + sgst;

                rows += `
                <tr class="table-info fw-bold">
                    <td colspan="4" class="text-end">Subtotal</td>
                    <td colspan="2">₹ ${grandTotal.toFixed(2)}</td>
                </tr>
                <tr class="table-warning fw-bold">
                    <td colspan="4" class="text-end">CGST (5%)</td>
                    <td colspan="2">₹ ${cgst.toFixed(2)}</td>
                </tr>
                <tr class="table-warning fw-bold">
                    <td colspan="4" class="text-end">SGST (5%)</td>
                    <td colspan="2">₹ ${sgst.toFixed(2)}</td>
                </tr>
                <tr class="table-success fw-bold">
                    <td colspan="4" class="text-end">Total Payable</td>
                    <td colspan="2">₹ ${finalTotal.toFixed(2)}</td>
                </tr>`;
            }
            $('#cartTableBody').html(rows);
            $('#exModal').modal('show');
        },
        error: function () {
            alert("Error fetching cart data.");
        }
    });
}

$('#supp').change(function () {
    var op = $('#supp').val();
    if (op != 0 || op != "0") {
        FetchMed();
    }
    else{
        $('#cardContainer').html(null);
    }
});
function addCart(medId, ppu) {
    var supplierId = $('#supp').val();
    var quantity = $(`#Quantity_${medId}`).val();

    if (!supplierId || supplierId == "0") {
        alert("Please select a supplier first.");
        return;
    }

    const obj = {
        supplierId: parseInt(supplierId),
        medicineId: parseInt(medId),
        quantity: parseInt(quantity),
        ppu: parseFloat(ppu)
    };

    $.ajax({
        url: '/Purchase/AddCart',
        type: 'POST',
        dataType: 'json',
        contentType: 'Application/x-www-form-urlencoded;charset=utf-8',
        data: obj,
        success: function (res) {
            if (res.success) {
                alert("Added to cart!");
                if (!$('#supp').prop('disabled')) {
                    $('#supp').attr('disabled', true);

                    if ($('#lockMsg').length === 0) {
                        $('#supp').after(`<small class="text-danger ms-2" id="lockMsg">Supplier locked after adding item.</small>`);
                    }
                }
            } else {
                alert("Error while adding to cart.");
            }
        },
        error: function () {
            alert("Failed to add cart.");
        }
    });
}

function delSingCart(cartId) {
    if (confirm('Are you sure')) {
        $.ajax({
            url: '/Purchase/DeleteSingleCart?id=' + cartId,
            type: 'GET',
            success: function (res) {
                if (res.success) {
                    $('#exModal').modal('show');
                    FetchCart();
                } else {
                    alert('Cannot delete this record');
                }
            },
            error: function () {
                alert('Something went wrong while deleting');
            }
        });
    }
    else {
        alert('Ok Fine');
    }
}

function CheckCartLock() {
    $.ajax({
        url: '/Purchase/GetCart',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            if (data && data.length > 0) {
                const lockedSupplierId = data[0].supplierId;
                $('#supp').val(lockedSupplierId).attr('disabled', true);

                if ($('#lockMsg').length === 0) {
                    $('#supp').after(`<small class="text-danger ms-2" id="lockMsg">Supplier locked due to existing cart items.</small>`);
                }
                FetchMed();
            }
        },
        error: function () {
            console.warn("Failed to check cart lock.");
        }
    });
}

$('#cartModal').click(function () {
    $('#exModal').modal('show');
    FetchCart();
    
});

$('#btnclose').click(function () {
    $('#exModal').modal('hide');
});


$('#btnclose2').click(function () {
    $('#exModal').modal('hide');
});