$().ready(function () {

    (function () {
        $('body').on('click', '.calendarDateLink', function () {
            var $this = $(this),
                date = $this.attr('data-date'),
                inputSelector = "#calendarDate",
                formSelector = "#tripCalendarForm";

            $(calendarDate).val(date);
            $(formSelector).submit();
        })
    }());

})