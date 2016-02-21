$().ready(function () {
    (function () {
        $('#hint-arrow a').click(function () {
            $('html, body').animate({
                scrollTop: $("#homeBody").offset().top
            }, 700);
            return false;
        });
    }())

    (function () {
        $('body').on('click', '.topDestinationLi', function () {
            var $this = $(this),
                from = $this.attr('data-from'),
                to = $this.attr('data-to');

            $('.topDestinationLi').removeClass('active');
            $this.addClass('active');

            $("#FromId option").filter(function () {
                return $(this).text() == from;
            }).prop('selected', true);

            $("#ToId option").filter(function () {
                return $(this).text() == to;
            }).prop('selected', true);
        });
    }());
})