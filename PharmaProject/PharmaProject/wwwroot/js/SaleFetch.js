$(document).ready(function () {
    Fetchpur();
});

function Fetchpur() {
    $.ajax({
        url: '/OSale/Getsale',
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
                            <td>${pur.saleId}</td>
                            <td>${pur.customerName}</td>
                            <td>${pur.saleDate}</td>
                            <td><span class="badge bg-success fw-semibold">₹${pur.totalAmount}</span></td>
                            <td>${pur.createdAt}</td>
                            <td>${pur.createdBy}</td>
                            <td class="text-nowrap">
                            <button type="button" class="btn btn-outline-primary btn-sm mb-1 me-1" onclick="getpuri('${pur.saleId}')">
                                <i class="bi bi-pencil-square me-1"></i> Detail
                            </button>
                            <button type="button" class="btn btn-outline-danger btn-sm mb-1" onclick="deletepur('${pur.saleId}')">
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
            alert('Error fetching sale');
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
    if (confirm('Are you sure you want to delete this sale?')) {
        $.ajax({
            url: '/OSale/DeleteSale?id=' + id,
            type: 'GET',
            success: function (res) {
                if (res.success) {
                    Fetchpur();
                } else {
                    alert('Cannot delete. sale items are associated with this record.');
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
        url: '/OSale/GetSaleItemById?id=' + id,
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
                            <td>${puri.itemId}</td>
                            <td>${puri.mname}</td>
                            <td>${puri.quantity}</td>
                            <td><span class="badge bg-success fw-semibold">₹${puri.unitPrice}</span></td>
                            <td>${puri.discount}</td>
                            <td>
                            <button type="button" class="btn btn-outline-danger btn-sm me-2" onclick="deletepuri('${puri.itemId}','${id}')">
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
    if (confirm('Are you sure you want to delete this sale item?')) {
        $.ajax({
            url: '/OSale/DeleteSaleItem?id=' + id,
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



