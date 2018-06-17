$(document).ready(function () {
    "use strict";

    $(".delete-link").click(function () {
        let id = $(this).attr("data-id").valueOf();

        $(".hidden-id").val(id);
    });

    $("#delete-button").click(function () {
        let id = $(".hidden-id").val();

        let customUrl = Url + "/" +  id.toString();

        $.ajax({
            type: "POST",
            url: customUrl,
            dataType: "json",
            data: id,
            success: function (succes) {
                location.reload();
            }
        });
    });
});
