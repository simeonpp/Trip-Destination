$().ready(function () {
    var $tripActionWrap = $('#tripActionWrap'),
        $tripMessage = $('#tripMessage');

    $('body').on('click', '#joinTripButton', function () {
        var $joinTripButton = $('#joinTripButton');

        executeAJAXJoinTripRequest()
            .then(function (response) {
                $joinTripButton.remove();

                if (response.status) {
                    addPendingMessage();
                    addRecallJoinTripButton();
                    toastr.success("You have successfully send request to join this trip.");
                } else {
                    if (response.error && response.error.message) {
                        toastr.error(response.error.message);
                    } else {
                        toastr.error("Unable to join trip.");
                    }
                }
            })
    });

    $('body').on('click', '#recallJoinTripButton', function () {
        var $recallJoinTripButton = $('#recallJoinTripButton');

        executeAJAXRecallJoinTripRequest()
            .then(function (response) {
                $recallJoinTripButton.remove();

                if (response.status) {
                    removePendingMessage();
                    addJoinTripButton();
                    toastr.success("You have successfully recall your join trip request.");
                } else {
                    if (response.error && response.error.message) {
                        toastr.error(response.error.message);
                    } else {
                        // Possible the drive has already approved his join request
                        location.reload();
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
        $tripMessage.addClass('tripSuccessMessage');
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

    function executeAJAXJoinTripRequest() {
        var promise = new Promise(function (resolve, reject) {
            var responseSuccess = {
                status: true
            };

            var responseFailWithMessage = {
                status: false,
                error: {
                    message: "No available spaces"
                }
            };

            resolve(responseSuccess);
        });

        return promise;
    }

    function executeAJAXRecallJoinTripRequest() {
        var promise = new Promise(function (resolve, reject) {
            var responseSuccess = {
                status: true
            };

            var responseFailWithMessage = {
                status: false,
                error: {
                    message: "You need to send join trip request first."
                    // another option here is to be approved, handle?? refresh page
                }
            };

            resolve(responseSuccess);
        });

        return promise;
    }
})