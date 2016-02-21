$().ready(function() {
    (function() {
        $('#hint-arrow a').click(function () {
            $('html, body').animate({
                scrollTop: $("#homeBody").offset().top
            }, 700);
            return false;
        });
    }())
})