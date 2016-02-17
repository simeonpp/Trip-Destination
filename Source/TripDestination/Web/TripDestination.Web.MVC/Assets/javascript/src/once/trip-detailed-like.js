$().ready(function () {
    var $likeDislikeTripButton = $('#likeDislikeTripButton'),
        $likesCount = $('#likesCount');

    $likeDislikeTripButton.on('click', function () {
        var $this = $(this),
            valueAsString = $this.attr('data-value'),
            value = false

        if (valueAsString == 'like') {
            value = true;
        }

        $.ajax({
            type: "POST",
            url: '/TripAjax/LikeDislikeTrip',
            data: {
                tripId: tripId,
                value: value
            },
            success: function (response) {
                if (response.Status) {
                    if (value) {
                        addDislikeButton();
                    } else {
                        addLikeButton();
                    }

                    updateLikeCounts(response.Data);
                    toastr.success("You have successfully " + valueAsString + " this trip.");
                } else {
                    if (response.ErrorMessage) {
                        toastr.error(response.ErrorMessage);
                    } else {
                        toastr.error("Unable to " + valueAsString + " this trip.");
                    }
                }
            }
        })
    });
        
    function updateLikeCounts(likesCount) {
        $likesCount.text(likesCount);
    }

    function addDislikeButton() {
        $likeDislikeTripButton.text('Dislike');
        $likeDislikeTripButton.attr('data-value', 'dislike');
    }

    function addLikeButton() {
        $likeDislikeTripButton.text('Like');
        $likeDislikeTripButton.attr('data-value', 'like');
    }
})