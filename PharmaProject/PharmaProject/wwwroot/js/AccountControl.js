$(document).ready(function () {
    $("#UserName").on("keyup", function () {
        let userName = $(this).val();

        $.ajax({
            url: '/Auth/CheckUsername',
            type: "GET",
            data: { username: userName }, 
            success: function (data) {
                if (data === true) {
                    $("#usernameValidation")
                        .text("✅ Username available")
                        .removeClass("text-danger")
                        .addClass("text-success");
                } else {
                    $("#usernameValidation")
                        .text("❌ Username already exists. Please choose another one.")
                        .removeClass("text-success")
                        .addClass("text-danger");
                }
            },
            error: function () {
                if (data === null) {
                    $("#usernameValidation")
                        .text("⚠️ Error checking username.")
                        .removeClass("text-success")
                        .addClass("text-danger");
                }
            }
        });
    });
});
