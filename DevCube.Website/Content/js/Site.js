
$(window).scroll(function () {
    if ($(this).scrollTop() >= 50) {        
        $('#arrow').fadeIn(200);   
    } else {
        $('#arrow').fadeOut(200);   
    }
});

$('#arrow').click(function () {      
    $('body,html').animate({
        scrollTop: 0                      
    }, 500);
});