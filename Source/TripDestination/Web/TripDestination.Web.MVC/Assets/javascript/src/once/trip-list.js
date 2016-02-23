$().ready(function () {

    (function () {
        $('body').on('click', '.calendarDateLink', function () {
            var $this = $(this),
                date = $this.attr('data-date'),
                inputSelector = "#calendarDate",
                formSelector = "#tripCalendarForm";

            $(inputSelector).val(date);
            $(formSelector).submit();
        })

        var $orderBySelectList = $('#orderBySelectList'),
            $tripFilterForm = $('#tripFilterForm');
        $orderBySelectList.on('change', function () {
            var $this = $(this),
                selectedValue = $this.val(),
                inputSelector = "#OrderBy";

            $(inputSelector).val(selectedValue);
            $tripFilterForm.submit();
        });

        var $sortDirectionAscending = $('#sortDirectionAscending'),
            $sortDirectionDescending = $('#sortDirectionDescending');

        $sortDirectionAscending.on('click', function() {
            var inputSelector = "#Sort";
            $(inputSelector).val('ASC');
            $tripFilterForm.submit();
        })

        $sortDirectionDescending.on('click', function () {
            var inputSelector = "#Sort";
            $(inputSelector).val('DESC');
            $tripFilterForm.submit();
        })
    }());
})