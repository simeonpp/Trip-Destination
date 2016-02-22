$().ready(function () {
    var ajaxAFT = $('#ajaxAFT input[name="__RequestVerificationToken"]:first').val();

    (function () {
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
                        $commentArea.val('');
                        addCommentToCommentsList(identifier, response.Data, false);
                        updateCommentsCount(commentsCountSpanSelector, response.Data.CommentTotalCount);
                        updateLoadMoreTripCommentsButtonOffset(loadMoreTripCommentsSelector);
                        toastr.success("You comment was successfully added.");
                    } else {
                        if (response.ErrorMessage) {
                            toastr.error(response.ErrorMessage);
                        } else {
                            toastr.error("Comment can not be add to this trip. Please contact our team.");
                        }
                    }
                }
            })
        })
    })
})