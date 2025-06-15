$(document).ready(function () {
    $('#savebtn').click(function () {
        e.preventDefault();
        var obj = $('#SuppForm').serialize();

        $.ajax({
            url: '/Supplier/AddSupplier',
            type: 'Post',
            dataType: 'json',
            contentType: 'Application/x-www-form-urlencoded; charset=UTF-8',
            data: obj,
            success: function () {
                alert('Supplier Added');
                window.location.href = '/Supplier/Index';
            },
            error: function () {
                alert('Error');
            }
        });
    });
});


