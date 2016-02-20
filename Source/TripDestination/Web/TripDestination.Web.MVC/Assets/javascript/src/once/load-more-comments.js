$().ready(function () {
    var $loadMoreTripComments = $('#loadMoreTripComments');

    $('body').on('click', '.loadMoreComments', function () {
        var $this = $(this),
            identifier = $this.attr('data-type'),
            ajaxUrl = $this.attr('data-ajaxUrl'),
            id = $this.attr('data-id'),
            currentOffset = $this.attr('data-offset'),
            type = $this.attr('data-type'),
            loadMoreCommentsSelector = '#loadMoreComments-' + identifier;

        $.ajax({
            type: "GET",
            url: ajaxUrl,
            data: {
                id: id,
                offset: currentOffset,
                type: type
            },
            success: function (response) {
                if (response.Status) {
                    if (!response.Data.HasMoreCommentsToLoad) {
                        $(loadMoreCommentsSelector).remove();
                    }

                    var comments = response.Data.Comments;
                    $.each(comments, function (index, comment) {
                        addCommentToCommentsList(identifier, comment, true);
                    });

                    updateLoadMoreButtonOffset(loadMoreCommentsSelector, response.Data.Offset);
                } else {
                    if (response.ErrorMessage) {
                        toastr.error(response.ErrorMessage);
                    } else {
                        toastr.error("Unable to load more comment. Please contact with our team.");
                    }
                }
            }
        })
    })

    function addCommentToCommentsList(identifier, comment, append) {
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
        var renderedTempalteHTML = template(context);

        if (append) {
            $('ul#' + identifier + '-CommentsList').append(renderedTempalteHTML);
        } else {
            $('ul#' + identifier + '-CommentsList').prepend(renderedTempalteHTML);
        }
    }

    function updateLoadMoreButtonOffset(selector, offset) {
        $(selector).attr('data-offset', offset);
    }
})