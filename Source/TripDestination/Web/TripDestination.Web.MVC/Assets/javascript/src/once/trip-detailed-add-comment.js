$().ready(function () {
    var $addNewCommentButton = $('#addNewCommentButton'),
        $commentArea = $('#commentArea'),
        $commentsCount = $('#commentsCount'),
        $loadMoreTripComments = $('#loadMoreTripComments');

    $addNewCommentButton.on('click', function () {
        var commentText = $commentArea.val();

        $.ajax({
            type: "POST",
            url: '/TripAjax/AddComments',
            data: {
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