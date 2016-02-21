$().ready(function () {
    $('.customDateTimePickers').datetimepicker({
        format: 'DD MMMM YYYY LT',
        defaultDate: new Date(),
        icons: {
            previous: 'glyphicon glyphicon-arrow-left',
            next: 'glyphicon glyphicon-arrow-right'
        }
    });
})