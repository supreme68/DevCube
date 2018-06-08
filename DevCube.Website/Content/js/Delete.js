$(document).ready(function () {
    $("#DeleteButton").click(function () {
        $.ajax({
            type: "POST",
            data: URL,
            url: URL,
            success: function (data) {
                window.location.href = data;
            },
            error: function () {
                console.log("error");
            }
        });
    });
});
