﻿$().ready(function () {
    var commentTextMinLength = 5,
        commentTextMaxLength = 1000,
        ajaxAFT = $('#ajaxAFT input[name="__RequestVerificationToken"]:first').val();

    $('body').on('click', '.addNewCommentButton', function () {
        var $this = $(this),
            identifier = $this.attr('data-identifier'),
            ajaxUrl = $this.attr('data-ajaxUrl'),
            id = $this.attr('data-id'),
            $commentArea = $('#commentArea-' + identifier),
            commentText = $commentArea.val(),
            commentTextLength = commentText.length,
            commentsCountSpanSelector = '#commentsCount-' + identifier,
            loadMoreTripCommentsSelector = '#loadMoreComments-' + identifier;

        if (commentTextLength < commentTextMinLength || commentTextMinLength > commentTextMaxLength) {
            toastr.error('Comment text should be between ' + commentTextMinLength + ' and ' + commentTextMaxLength + ' symbols.');
            return;
        }

        $.ajax({
            type: "POST",
            url: ajaxUrl,
            data: {
                __RequestVerificationToken: ajaxAFT,
                id: id,
                commentText: commentText
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

    function addCommentToCommentsList(selector, comment, append) {
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
            $('ul#' + selector + '-CommentsList').append(renderedTemplateHTML);
        } else {
            $('ul#' + selector + '-CommentsList').prepend(renderedTemplateHTML);
        }
    }

    function updateCommentsCount(selector, newCommentCount) {
        var $span = $(selector);
        $span.text(newCommentCount);
    }

    function updateLoadMoreTripCommentsButtonOffset(selector) {
        $button = $(selector);

        if ($button) {
            var currentOffsetValue = $button.attr('data-offset') | 0,
            newOffsetValue = currentOffsetValue + 1;

            $button.attr('data-offset', newOffsetValue);
        }
    }
})