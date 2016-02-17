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
                    if (response.error && response.error.message) {
                        toastr.error(response.error.message);
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

    function executeAjaxAddCommentRequest() {
        var promise = new Promise(function (resolve, reject) {
            var responseSuccess = {
                status: true,
                data: {
                    comment: {
                        firstName: "Strahil",
                        lastName: "Strahilov",
                        userUrl: "http://www.google.com",
                        userImageSrc: "http://media02.hongkiat.com/ww-flower-wallpapers/roundflower.jpg",
                        createdOnFormatted: "02.01.2016 18:30",
                        commentText: "lorem lorem lorem lorem loremlorem lorem loremloremlorem lorem"
                    },
                    totalComment: 6
                }
            };

            var responseFailServer = {
                status: false
            };

            var responseFailWithErrorMessage = {
                status: false,
                error: {
                    message: "Comment length can no be less than 10 symbols long."
                }
            };

            resolve(responseSuccess);
        });

        return promise;
    }
})