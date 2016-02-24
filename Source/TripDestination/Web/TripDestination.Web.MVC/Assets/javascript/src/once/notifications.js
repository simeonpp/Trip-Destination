$().ready(function () {
    var ajaxAFT = $('#ajaxAFT input[name="__RequestVerificationToken"]:first').val();
    
    $('body').on('click', '.approveNotification', function () {
        var $this = $(this),
            id = $this.attr('data-id'),
            ajaxUrl = $this.attr('data-ajaxUrl');

        $.ajax({
            type: "POST",
            url: "/NotificationAjax/ApproveNotification",
            data: {
                __RequestVerificationToken: ajaxAFT,
                id: id
            },
            success: function (response) {
                if (response.Status) {
                    removeActionButtons(id);
                    toastr.success("You successfully approved this notification.");

                    if (response.RedirectToUrl) {
                        window.location.href = response.UrlToRedirectTo;
                    }
                } else {
                    if (response.ErrorMessage) {
                        toastr.error(response.ErrorMessage);
                    } else {
                        toastr.error("Notification can not be approved right now. Please contact our team.");
                    }
                }
            }
        })
    })

    $('body').on('click', '.disapproveNotification', function () {
        var $this = $(this),
            id = $this.attr('data-id'),
            ajaxUrl = $this.attr('data-ajaxUrl');

        $.ajax({
            type: "POST",
            url: "/NotificationAjax/DisapproveNotification",
            data: {
                __RequestVerificationToken: ajaxAFT,
                id: id
            },
            success: function (response) {
                if (response.Status) {
                    removeActionButtons(id);
                    toastr.success("You successfully disapproved this notification.");
                } else {
                    if (response.ErrorMessage) {
                        toastr.error(response.ErrorMessage);
                    } else {
                        toastr.error("Notification can not be disapproved right now. Please contact our team.");
                    }
                }
            }
        })
    })

    function removeActionButtons(id) {
        $('.actionButton[data-id="' + id + '"]').remove();
    }
})