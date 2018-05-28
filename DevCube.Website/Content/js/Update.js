$(document).ready(function () {

    var programmerId = ("#id").val(); 
    
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "dataType",
        url: "Programmer/UpdateProgrammer",
        data: "data",
        success: function (response) {
        }
    });
});
