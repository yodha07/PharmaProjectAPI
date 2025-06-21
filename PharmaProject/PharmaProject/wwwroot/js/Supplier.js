$(document).ready(function () {
    $('#supTable').DataTable({
        pageLength: 5,
        lengthMenu: [5, 10, 25, 50],
        columnDefs: [{ targets: -1, orderable: false }]
    });
    FetchSupp();     
    setTimeout(function () {
        $('#supTable').DataTable();
    }, 500);
});

function FetchSupp() {
    $.ajax({
        url: '/Supplier/GetAllSupplier',
        type: 'GET',
        dataType: 'json',
        contentType: 'Application/json; charset=UTF-8',
        success: function (result) {
            if ($.fn.DataTable.isDataTable('#supTable')) {
                $('#supTable').DataTable().destroy();
            }
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
                            <button type="button" class="btn btn-outline-primary btn-sm" onclick="editSupp('${supplier.supplierId}')">
                                <i class="bi bi-pencil-square me-1"></i>
                            </button>
                            <button type="button" class="btn btn-outline-danger btn-sm me-2" onclick="deleteSupp('${supplier.supplierId}')">
                                <i class="bi bi-trash3-fill me-1"></i>
                            </button>
                            </td>
                        </tr>`;
            });
            $('#supTable tbody').html(rows);
            $('#supTable').DataTable();

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
    $('#exampleModalLabel').html("Add Supplier");

    $('#Name, #Contact, #Address')
        .removeClass('is-valid is-invalid');
    $('#savebtn').prop('disabled', true);
});

$('#savebtn').click(function () {
    var form = document.getElementById('SuppForm');

    if (!form.checkValidity()) {
        form.reportValidity();
        $('#Name').addClass('is-invalid');
        $('#Contact').addClass('is-invalid');
        $('#Address').addClass('is-invalid');
        return;
    }
    else {
        $('#Name, #Contact, #Address').removeClass('is-invalid').addClass('is-valid');
    }
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

$('#btnclose').click(function () {
    $('#exModal').modal('hide');
});


$('#btnclose2').click(function () {
    $('#exModal').modal('hide');
});
function deleteSupp(id) {
    if (confirm('Are you sure')) {
        $.ajax({
            url: '/Supplier/DeleteSupplier?id=' + id,
            type: 'GET',
            success: function (res) {
                if (res.success) {
                    FetchSupp();
                } else {
                    alert('Cannot delete this record, Purchase data is associated with this data');
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

function editSupp(id) {
    $('#Name, #Contact, #Address')
        .removeClass('is-invalid is-valid');
    $('#Name, #Contact, #Address')
        .addClass('is-valid');
    checkFormValidity();

    $.ajax({
        url: '/Supplier/GetSupplierById?id=' + id,
        type: 'Get',
        dataType: 'json',
        contentType: 'Application/json;charset=utf-8',
        success: function (result) {
            $('#exModal').modal('show');
            $('#exampleModalLabel').html("Edit Supplier");
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
function checkFormValidity() {
    const allValid = $('#Name').hasClass('is-valid') &&
        $('#Contact').hasClass('is-valid') &&
        $('#Address').hasClass('is-valid');
    $('#savebtn').prop('disabled', !allValid);
}


$('#Name').keyup(function () {
    var name = $(this).val();

    if (name.length>=2 && name.length<=30) {
        $(this).removeClass('is-invalid');
        $(this).addClass('is-valid');
    } else {
        $(this).addClass('is-invalid');
        $(this).removeClass('is-valid');
    }
    checkFormValidity();
});

$('#Contact').keyup(function () {
    var phone = $(this).val();
    var phoneRegex = /^[6-9]\d{9}$/; // 10-digit number starting with 6-9

    if (!phoneRegex.test(phone)) {
        $(this).addClass('is-invalid');
        $(this).removeClass('is-valid');
    } else {
        $(this).removeClass('is-invalid');
        $(this).addClass('is-valid');
    }
    checkFormValidity();
});

$('#Address').keyup(function () {
    var add = $(this).val();

    if (add.length >= 5 && add.length <= 50) {
        $(this).removeClass('is-invalid');
        $(this).addClass('is-valid');
    } else {
        $(this).addClass('is-invalid');
        $(this).removeClass('is-valid');
    }
    checkFormValidity();
});

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


