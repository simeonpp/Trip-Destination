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

    (function () {
        $('body').on('click', '.paginationLink', function () {
            var $this = $(this),
                page = $this.attr('data-page');

            var currentUrl = window.location.href;
            var url = new URI(currentUrl);
            if (url.hasQuery("page") === true) {
                url.setQuery("page", page);
            } else {
                url.addQuery("page", page);
            }

            window.location.href = url;
        })
    }())
})