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
                        <div class="card shadow-sm w-100 border-0 rounded-4 position-relative">
                            <div class="card-body">
                            <h5 class="card-title fw-bold" id="Name" name="Name"><strong>Name:</strong> ${med.name}</h5>
                            <p class="mb-1" id="Category" name="Category"><strong>Category:</strong> ${med.category}</p>
                            <p class="mb-1" id="Manufacturer" name="Manufacturer"><strong>Manufacturer:</strong> ${med.manufacturer}</p>
                            <p class="mb-1" id="BatchNo" name="BatchNo"><strong>Batch No:</strong> ${med.batchNo}</p>
                            <p class="mb-1" id="ExpiryDate" name="ExpiryDate"><strong>Expiry:</strong> ${med.expiryDate}</p>
                            <p class="mb-1" id="PricePerUnit" name="PricePerUnit"><strong>Price Per Unit:</strong> ${med.pricePerUnit}</p>
                            <p class="mb-1"><strong>Quantity:</strong>
                                <select name="Quantity" class="form-select">
                                    ${quantityOptions}
                                </select>
                            </p>
                            </div>
                            <div class="card-footer bg-white border-top-0 d-flex justify-content-between">
                                <button type="button" class="btn btn-primary" onclick="addCart('${med.medicineId}')">Add to Cart</button>
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

$('#supp').change(function () {
    var op = $('#supp').val();
    if (op != 0 || op != "0") {
        FetchMed();
    }
    else
    {
        $('#cardContainer').html(null);
    }
});
function addCart(id){
    let cart = [];
}