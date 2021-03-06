﻿$().ready(function () {
    var $approvePassengerJoinRequest = $('.approvePassengerJoinRequest'),
        $disapprovePassengerJoinRequest = $('.disapprovePassengerJoinRequest'),
        pendingUsernames = [],
        ajaxAFT = $('#ajaxAFT input[name="__RequestVerificationToken"]:first').val();

    $disapprovePassengerJoinRequest.on('click', function () {
        var $this = $(this),
            $li = $this.parent().parent(),
            username = $this.parent().attr('data-username');

        if ($.inArray(username, pendingUsernames)) {
            addPendingUser(username);
            $.ajax({
                __RequestVerificationToken: ajaxAFT,
                type: "POST",
                url: '/TripAjax/DisapproveJoinRequest',
                data: {
                    __RequestVerificationToken: ajaxAFT,
                    tripId: tripId,
                    username: username
                },
                success: function (response) {
                    if (response.Status) {
                        $li.toggle(300, function () {
                            $li.remove();
                        });

                        updatePendingApprovePassengersCount(response.Data.PendingApproveUsersCount);
                        toastr.info(username + "'s request to join your trip was successfully REJECTED.");
                    } else {
                        if (response.ErrorMessage) {
                            toastr.error(response.ErrorMessage);
                        } else {
                            toastr.error("An error has occurred during trying to reject " + username + "'s request to join your trip. Please report to our team.");
                        }
                    }
                }
            })
        }
    });

    $approvePassengerJoinRequest.on('click', function () {
        var $this = $(this),
            $li = $this.parent().parent(),
            username = $this.parent().attr('data-username');

        if ($.inArray(username, pendingUsernames)) {
            $.ajax({
                type: "POST",
                url: '/TripAjax/ApproveJoinRequest',
                data: {
                    __RequestVerificationToken: ajaxAFT,
                    tripId: tripId,
                    username: username
                },
                success: function (response) {
                    if (response.Status) {
                        addPendingUser(username);
                        // Remove user from current list
                        $li.toggle(300, function () {
                            $li.remove();
                        });

                        updatePendingApprovePassengersCount(response.Data.PendingApproveUsersCount);
                        addUserToPassengersList(response.Data);
                        toastr.success(username + "'s request to join your trip was successfully APPROVED.");
                    } else {
                        if (response.ErrorMessage) {
                            toastr.error(response.ErrorMessage);
                        } else {
                            toastr.error("An error has occurred during trying to approve " + username + "'s request to join your trip. Please report to our team.");
                        }
                    }
                }
            })
        }
    });

    function addPendingUser(username) {
        pendingUsernames.push(username);
    }

    function addUserToPassengersList(data) {
        var $passengersCount = $('#passengersCount'),
            $leftAvailableSeats = $('#availableSeats'),
            $fullTripDetailsAvailableSeats = $('#fullTripDetailsAvailableSeats');

        var sourceTemplate = $("#passengerLiTemplate").html();
        var template = Handlebars.compile(sourceTemplate);
        var context = {
            firstName: data.FirstName,
            lastName: data.LastName,
            imageSrc: data.ImageSrc,
            userProfileLink: data.UserProfileLink
        };
        var renderedTempalteHTML = template(context);
        $('ul#passengersList').append(renderedTempalteHTML);

        $passengersCount.text(data.PassengersCount);
        $leftAvailableSeats.text(data.AvailableSeatsCount);
        $fullTripDetailsAvailableSeats.text(data.AvailableSeatsCount);
    }

    function updatePendingApprovePassengersCount(newCount) {
        newCount = newCount | 0;

        var $span = $('#pendingApprovePassengersCount');
        $span.text(newCount);
    }
});