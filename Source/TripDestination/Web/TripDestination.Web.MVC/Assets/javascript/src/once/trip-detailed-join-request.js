﻿$().ready(function () {
    $('[data-toggle="tooltip"]').tooltip();

    var $tripActionWrap = $('#tripActionWrap'),
        $tripMessage = $('#tripMessage'),
        ajaxAFT = $('#ajaxAFT input[name="__RequestVerificationToken"]:first').val();

    $('body').on('click', '#joinTripButton', function () {
        var $joinTripButton = $('#joinTripButton');

        $.ajax({
            type: "POST",
            url: '/TripAjax/JoinRequest',
            data: {
                __RequestVerificationToken: ajaxAFT,
                tripid: tripId
            },
            success: function (response) {
                $joinTripButton.remove();

                if (response.Status) {
                    addPendingMessage();
                    addRecallJoinTripButton();
                    toastr.success("You have successfully send request to join this trip.");
                } else {
                    if (response.ErrorMessage) {
                        toastr.error(response.ErrorMessage);
                    } else {
                        toastr.error("Unable to join trip.");
                    }
                }
            }
        })
    });

    $('body').on('click', '#recallJoinTripButton', function () {
        var $recallJoinTripButton = $('#recallJoinTripButton');

        $.ajax({
            type: "POST",
            url: '/TripAjax/LeaveTrip',
            data: {
                __RequestVerificationToken: ajaxAFT,
                tripid: tripId
            },
            success: function (response) {
                $recallJoinTripButton.remove();

                if (response.Status) {
                    removePendingMessage();
                    addJoinTripButton();
                    toastr.success("You have successfully recall your join trip request.");
                } else {
                    if (response.ErrorMessage) {
                        toastr.error(response.ErrorMessage);
                    } else {
                        toastr.error("Unable to leave this trip.");
                    }
                }
            }
        })
    });

    $('body').on('click', '#leaveTripButton', function () {
        var $leaveTripButton = $('#leaveTripButton');

        $.ajax({
            type: "POST",
            url: '/TripAjax/LeaveTrip',
            data: {
                __RequestVerificationToken: ajaxAFT,
                tripid: tripId
            },
            success: function (response) {
                $leaveTripButton.remove();

                if (response.Status) {
                    removePendingMessage();
                    addJoinTripButton();
                    removePassengerFromList(response.Data);
                    toastr.success("You have successfully leave this trip.");
                } else {
                    if (response.ErrorMessage) {
                        toastr.error(response.ErrorMessage);
                    } else {
                        toastr.error("Unable to leave this trip.");
                    }
                }
            }
        })
    });

    function addPendingMessage() {
        var sourceTemplate   = $("#pendingJoinRequestTemplate").html();
        var template = Handlebars.compile(sourceTemplate);
        var renderedTemplateHTML = template({});
        $tripMessage.append(renderedTemplateHTML);
        $tripMessage.addClass('tripInfoMessage');
        $tripMessage.removeClass('tripSuccessMessage');
        $('[data-toggle="tooltip"]').tooltip();
    }

    function removePendingMessage() {
        $tripMessage.removeClass('tripInfoMessage');
        $tripMessage.text('');
    }

    function addRecallJoinTripButton() {
        var sourceTemplate   = $("#recallJoinTripRequestButtonTemplate").html();
        var template = Handlebars.compile(sourceTemplate);
        var renderedTemplateHTML = template({});
        $tripActionWrap.append(renderedTemplateHTML);
    }

    function addJoinTripButton() {
        var sourceTemplate   = $("#joinTripRequestButtonTemplate").html();
        var template = Handlebars.compile(sourceTemplate);
        var renderedTemplateHTML = template({});
        $tripActionWrap.append(renderedTemplateHTML);
    }

    function removePassengerFromList(data) {
        var $passengersCount = $('#passengersCount'),
            $leftAvailableSeats = $('#availableSeats'),
            $fullTripDetailsAvailableSeats = $('#fullTripDetailsAvailableSeats');

        var liToBeRemove = $('ul#passengersList li[data-passengerusername="' + data.UserName + '"');
        liToBeRemove.remove();

        $passengersCount.text(data.PassengersCount);
        $leftAvailableSeats.text(data.AvailableSeatsCount);
        $fullTripDetailsAvailableSeats.text(data.AvailableSeatsCount);
    }
})