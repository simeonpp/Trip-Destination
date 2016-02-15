$().ready(function () {
    $('.customDateTimePickers').datetimepicker({
        format: 'DD MMMM YYYY',
        icons: {
            previous: 'glyphicon glyphicon-arrow-left',
            next: 'glyphicon glyphicon-arrow-right'
        }
    });
})