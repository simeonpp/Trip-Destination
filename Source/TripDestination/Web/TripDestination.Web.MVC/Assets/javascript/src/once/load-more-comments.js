$().ready(function () {
    var $loadMoreTripComments = $('#loadMoreTripComments');

    $('body').on('click', '.loadMoreComments', function () {
        var $this = $(this),
            id = $this.attr('data-id'),
            currentOffset = $this.attr('data-offset'),
            type = $this.attr('data-type');

        $.ajax({
            type: "POST",
            url: ajaxUrl,
            data: {
                id: id,
                offset: currentOffset,
                type: type
            },
            success: function (response) {
                if (response.Status) {
                    if (!response.Data.HasMoreCommentsToLoad) {
                        $loadMoreTripComments.remove();
                    }

                    var comments = response.data.comments;
                    $.each(comments, function (index, comment) {
                        addCommentToCommentsList(comment, true);
                    });

                    updateLoadMoreButtonOffset(response.data.offset);
                } else {
                    if (response.error && response.error.message) {
                        toastr.error(response.error.message);
                    } else {
                        toastr.error("Unable to load more comment. Please contact our team.");
                    }
                }
            }
        })
    })

    function addCommentToCommentsList(comment, append) {
        if (typeof append === 'undefined') {
            append = true;
        }

        var sourceTemplate = $("#commentLiTemplate").html();
        var template = Handlebars.compile(sourceTemplate);
        var context = {
            firstName: comment.firstName,
            lastName: comment.lastName,
            userUrl: comment.userUrl,
            userImageSrc: comment.userImageSrc,
            createdOnFormatted: comment.createdOnFormatted,
            commentText: comment.commentText
        };
        var renderedTempalteHTML = template(context);

        if (append) {
            $('ul#tripCommentsList').append(renderedTempalteHTML);
        } else {
            $('ul#tripCommentsList').prepend(renderedTempalteHTML);
        }
    }

    function updateLoadMoreButtonOffset(offset) {
        $loadMoreTripComments.attr('data-offset', offset);
    }

    function executeAjaxGetCommentsRequest(offset) {
        offset = offset | 0;

        var promise = new Promise(function (resolve, reject) {
            var responseSuccess = {
                status: true,
                data: {
                    comments: [
                        {
                            firstName: "Strahil",
                            lastName: "Strahilov",
                            userUrl: "http://www.google.com",
                            userImageSrc: "http://media02.hongkiat.com/ww-flower-wallpapers/roundflower.jpg",
                            createdOnFormatted: "02.01.2016 18:30",
                            commentText: "lorem lorem lorem lorem loremlorem lorem loremloremlorem lorem"
                        },
                        {
                            firstName: "Stamat",
                            lastName: "Stamatov",
                            userUrl: "http://www.google.com",
                            userImageSrc: "http://media02.hongkiat.com/ww-flower-wallpapers/roundflower.jpg",
                            createdOnFormatted: "02.01.2016 20:30",
                            commentText: "lorem lorem."
                        },
                        {
                            firstName: "Stasi",
                            lastName: "Stasev",
                            userUrl: "http://www.google.com",
                            userImageSrc: "http://media02.hongkiat.com/ww-flower-wallpapers/roundflower.jpg",
                            createdOnFormatted: "02.01.2016 21:00",
                            commentText: "lorem lorem lorem lorem loremlorem lorem loremloremlorem lorem lorem lorem loremloremlorem loremlorem lorem."
                        }
                    ],
                    hasMore: true,
                    offset: offset + 3
                }
            };

            var responseFail = {
                status: false,
                error: {
                    message: "No left comments."
                }
            };

            if (offset >= 9) {
                resolve(responseFail);
            } else {
                resolve(responseSuccess);
            }
        });

        return promise;


    }
})