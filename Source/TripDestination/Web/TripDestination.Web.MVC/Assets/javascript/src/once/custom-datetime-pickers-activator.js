$().ready(function () {
    $('.customDateTimePickers').datetimepicker({
        format: 'DD MMMM YYYY LT',
        icons: {
            previous: 'glyphicon glyphicon-arrow-left',
            next: 'glyphicon glyphicon-arrow-right'
        }
    });
})