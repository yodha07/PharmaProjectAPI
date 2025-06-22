$(document).ready(function () {
    Fetchpur();
});

function Fetchpur() {
    $.ajax({
        url: '/PurchaseItem/GetPurchase',
        type: 'GET',
        dataType: 'json',
        contentType: 'Application/json; charset=UTF-8',
        success: function (result) {
            if ($.fn.DataTable.isDataTable('#purTable')) {
                $('#purTable').DataTable().destroy();
            }
            var rows = '';
            $.each(result, function (index, pur) {
                rows += `
                        <tr>
                            <td>${pur.purchaseId}</td>
                            <td>${pur.sname}</td>
                            <td>${pur.purchaseDate}</td>
                            <td><span class="badge bg-success fw-semibold">₹${pur.totalAmount}</span></td>
                            <td>${pur.createdAt}</td>
                            <td>${pur.createdBy}</td>
                            <td class="text-nowrap">
                            <button type="button" class="btn btn-outline-primary btn-sm mb-1 me-1" onclick="getpuri('${pur.purchaseId}')">
                                <i class="bi bi-pencil-square me-1"></i> Detail
                            </button>
                            <a class="btn btn-outline-success btn-sm mb-1 me-1" href="${pur.invoiceNo}" target="_blank">
                                <i class="fas fa-file-pdf me-1"></i> Invoice
                            </a>
                            <button type="button" class="btn btn-outline-danger btn-sm mb-1" onclick="deletepur('${pur.purchaseId}')">
                                <i class="bi bi-trash3-fill me-1"></i>
                            </button>
                            </td>
                        </tr>`;
            });
            $('#purTable tbody').html(rows);
            $('#purTable').DataTable({
                pageLength: 5,
                lengthMenu: [5, 10, 25, 50],
                columnDefs: [{ targets: -1, orderable: false }]
            });

        },
        error: function () {
            alert('Error fetching purchase');
        }
    });
}

$('#btnclose').click(function () {
    $('#exModal').modal('hide');
});

$('#btnclose2').click(function () {
    $('#exModal').modal('hide');
});
function deletepur(id) {
    if (confirm('Are you sure you want to delete this purchase?')) {
        $.ajax({
            url: '/PurchaseItem/DeletePurchase?id=' + id,
            type: 'GET',
            success: function (res) {
                if (res.success) {
                    Fetchpur();
                } else {
                    alert('Cannot delete. Purchase items are associated with this record.');
                }
            },
            error: function () {
                alert('Something went wrong while deleting');
            }
        });
    }
}

function getpuri(id) {

    $.ajax({
        url: '/PurchaseItem/GetPurchaseItemById?id=' + id,
        type: 'Get',
        dataType: 'json',
        contentType: 'Application/json;charset=utf-8',
        success: function (result) {
            $('#exModal').modal('show');
            if ($.fn.DataTable.isDataTable('#puriTable')) {
                $('#puriTable').DataTable().destroy();
            }
            var rows = '';
            $.each(result, function (index, puri) {
                rows += `
                        <tr>
                            <td>${puri.purchaseItemId}</td>
                            <td>${puri.mname}</td>
                            <td>${puri.quantity}</td>
                            <td><span class="badge bg-success fw-semibold">₹${puri.costPrice}</span></td>
                            <td>
                            <button type="button" class="btn btn-outline-danger btn-sm me-2" onclick="deletepuri('${puri.purchaseItemId}','${id}')">
                                <i class="bi bi-trash3-fill me-1"></i>
                            </button>
                            </td>
                        </tr>`;
            });
            $('#puriTable tbody').html(rows);
            $('#puriTable').DataTable({
                pageLength: 5,
                columnDefs: [{ targets: -1, orderable: false }]
            });

        },
        error: function () {
            alert('Get by Id Not Found');
        }
    });
}

function deletepuri(id, pid) {
    if (confirm('Are you sure you want to delete this purchase item?')) {
        $.ajax({
            url: '/PurchaseItem/DeletePurchaseItem?id=' + id,
            type: 'GET',
            success: function (res) {
                if (res.success) {
                    getpuri(pid); 
                } else {
                    alert('Cannot delete. This item is linked with sales data.');
                }
            },
            error: function () {
                alert('Something went wrong while deleting');
            }
        });
    }
}



