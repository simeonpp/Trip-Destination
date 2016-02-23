﻿$().ready(function () {
    var datePickerSelectedDate = $('.customDateTimePickers').find('input').first().attr('data-date');

    if (datePickerSelectedDate) {
        datePickerSelectedDate = new Date(datePickerSelectedDate);
    } else {
        datePickerSelectedDate = new Date();
    }

    $('.customDateTimePickers').datetimepicker({
        format: 'DD MMMM YYYY',
        defaultDate: datePickerSelectedDate,
        icons: {
            previous: 'glyphicon glyphicon-arrow-left',
            next: 'glyphicon glyphicon-arrow-right'
        }
    });
})