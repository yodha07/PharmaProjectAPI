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
            var rows = '';
            $.each(result, function (index, supplier) {
                rows += `
                        <tr>
                            <td>${supplier.supplierId}</td>
                            <td>${supplier.name}</td>
                            <td>${supplier.contact}</td>
                            <td>${supplier.address}</td>
                            <td>${supplier.createdAt}</td>
                            <td>${supplier.createdBy}</td>
                            <td>${supplier.modifiedAt}</td>
                            <td>${supplier.modifiedBy}</td>
                            <td>
                            <button type="button" class="btn btn-sm btn-danger" onclick="deleteSupp('${supplier.supplierId}')">Delete</button>
                            <button type="button" class="btn btn-primary" onclick="editSupp('${supplier.supplierId}')">Edit</button>
                            </td>
                        </tr>`;
            });
            $('#supTable').html(rows);
        },
        error: function () {
            alert('Error fetching suppliers');
        }
    });
}

$('#suppModal').click(function () {
    $('#SuppForm')[0].reset();
    $('#SupplierId').val("");
    $('#exModal').modal('show');
});

$('#savebtn').click(function () {

    var obj = $('#SuppForm').serialize();

    var id = $('#SupplierId').val();
    if (id == null || id == "0" || id == "") {
        $.ajax({
            url: '/Supplier/AddSupplier',
            type: 'Post',
            dataType: 'json',
            contentType: 'Application/x-www-form-urlencoded;charset=utf-8',
            data: obj,
            success: function () {
                alert('Supplier Added');
                $('#exModal').modal('hide');
                FetchSupp();
            },
            error: function () {
                alert('Add Error');
            }
        });
    }
    else {
        $.ajax({
            url: '/Supplier/EditSupplier',
            type: 'Post',
            dataType: 'json',
            contentType: 'Application/x-www-form-urlencoded;charset=utf-8',
            data: obj,
            success: function () {
                alert('Supplier Updated');
                $('#exModal').modal('hide');
                FetchSupp();
            },
            error: function () {
                alert('Update Error');
            }
        });
    }
});    

function deleteSupp(id) {
    if (confirm('Are you sure')) {
        $.ajax({
            url: '/Supplier/DeleteSupplier?id=' + id,
            success: function () {
                FetchSupp()
            },
            error: function () {
                alert('Delete Error');
            }
        });
    }
    else {
        alert('Ok Fine');
    }
}

function editSupp(id) {
    $.ajax({
        url: '/Supplier/GetSupplierById?id=' + id,
        type: 'Get',
        dataType: 'json',
        contentType: 'Application/json;charset=utf-8',
        success: function (result) {
            $('#exModal').modal('show');
            $('#SupplierId').val(result.supplierId);
            $('#Name').val(result.name);
            $('#Contact').val(result.contact);
            $('#Address').val(result.address);
        },
        error: function () {
            alert('Get by Id Not Found');
        }
    });
}   

//$('#SuppForm').submit(function (e) {
//    e.preventDefault();

//    var obj = $(this).serialize();

//    $.ajax({
//        url: '/Supplier/AddSupplier',
//        type: 'Post',
//        dataType: 'json',
//        contentType: 'Application/x-www-form-urlencoded; charset=UTF-8',
//        data: obj,
//        success: function () {
//            alert('Supplier Added');
//            window.location.href = '/Supplier/Index';
//        },
//        error: function () {
//            alert('Error');
//        }
//    });
//});


