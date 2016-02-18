$().ready(function () {
    var $addNewCommentButton = $('#addNewCommentButton'),
        $commentArea = $('#commentArea'),
        $commentsCount = $('#commentsCount'),
        $loadMoreTripComments = $('#loadMoreTripComments'),
        commentTextMinLength = 5,
        commentTextMaxLength = 1000,
        ajaxAFT = $('#ajaxAFT input[name="__RequestVerificationToken"]:first').val();

    $addNewCommentButton.on('click', function () {
        var commentText = $commentArea.val(),
            commentTextLength = commentText.length;

        if (commentTextLength < commentTextMinLength || commentTextMinLength > commentTextMaxLength) {
            toastr.error('Comment text should be between ' + commentTextMinLength + ' and ' + commentTextMaxLength + ' symbols.');
            return;
        }

        $.ajax({
            type: "POST",
            url: '/TripAjax/AddComments',
            data: {
                __RequestVerificationToken: ajaxAFT,
                tripid: tripId,
                commentText: commentText
            },
            success: function (response) {
                if (response.Status) {
                    $commentArea.val('');
                    addCommentToCommentsList(response.Data, false);
                    updateCommentsCount(response.Data.CommentTotalCount);
                    updateLoadMoreTripCommentsButtonOffset();
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
    });

    function addCommentToCommentsList(comment, append) {
        if (typeof append === 'undefined') {
            append = true;
        }

        var sourceTemplate = $("#commentLiTemplate").html();
        var template = Handlebars.compile(sourceTemplate);
        var context = {
            firstName: comment.FirstName,
            lastName: comment.LastName,
            userUrl: comment.UserUrl,
            userImageSrc: comment.UserImageSrc,
            createdOnFormatted: comment.CreatedOnFormatted,
            commentText: comment.CommentText
        };
        var renderedTemplateHTML = template(context);

        if (append) {
            $('ul#tripCommentsList').append(renderedTemplateHTML);
        } else {
            $('ul#tripCommentsList').prepend(renderedTemplateHTML);
        }
    }

    function updateCommentsCount(newCommentCount) {
        $commentsCount.text(newCommentCount);
    }

    function updateLoadMoreTripCommentsButtonOffset() {
        if ($loadMoreTripComments) {
            var currentOffsetValue = $loadMoreTripComments.attr('data-offset') | 0,
            newOffsetValue = currentOffsetValue + 1;

            $loadMoreTripComments.attr('data-offset', newOffsetValue);
        }
    }
})