$(document).ready(function () {
    'use strict'

    $("#search-form").submit(function () {
        let filter = $("#search-form_input").val();

        let programmerUrl = programmerNameUrl + "/" + filter.toString();
        //let url = programmerSkillNameUrl + "/" + filter.toString();

        $.ajax({
            type: "POST",
            url: programmerNameUrl,
            dataType: "json",
            data: filter,
            success: function (succes) {
                console.log("succes");
            }
        });
    });
});