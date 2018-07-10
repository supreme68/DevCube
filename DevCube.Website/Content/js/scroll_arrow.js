$(document).ready(() => {
    "use strict";

    $(window).scroll(() => {
        if ($(this).scrollTop() >= 50) {
            $('#arrow').fadeIn(200);
        }
        else {
            $('#arrow').fadeOut(200);
        }
    });

    $('#arrow').click(() => {
        $('body,html').animate({
            scrollTop: 0
        }, 500);
    });
});