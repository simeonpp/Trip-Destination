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