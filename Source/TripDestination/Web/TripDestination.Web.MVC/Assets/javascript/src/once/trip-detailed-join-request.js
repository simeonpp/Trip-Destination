$().ready(function () {
    $('[data-toggle="tooltip"]').tooltip();

    var $tripActionWrap = $('#tripActionWrap'),
        $tripMessage = $('#tripMessage');

    $('body').on('click', '#joinTripButton', function () {
        var $joinTripButton = $('#joinTripButton');

        $.ajax({
            type: "POST",
            url: '/TripAjax/JoinRequest',
            data: {
                tripid: tripId
            },
            success: function (response) {
                $joinTripButton.remove();

                if (response.Status) {
                    addPendingMessage();
                    addRecallJoinTripButton();
                    toastr.success("You have successfully send request to join this trip.");
                } else {
                    if (response.Data) {
                        toastr.error(response.Data);
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
                tripid: tripId
            },
            success: function (response) {
                $recallJoinTripButton.remove();

                if (response.Status) {
                    removePendingMessage();
                    addJoinTripButton();
                    toastr.success("You have successfully recall your join trip request.");
                } else {
                    if (response.Data) {
                        toastr.error(response.Data);
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
})