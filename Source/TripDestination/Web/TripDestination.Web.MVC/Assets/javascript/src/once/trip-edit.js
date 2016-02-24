$().ready(function () {

    (function () {
        var $removePassengerButton = $('.removePassengerButton');

        $removePassengerButton.on('click', function () {
            var $this = $(this),
                $li = $this.parent(),
                username = $this.attr('data-username');

            updateUsernamesToBeRemovedInput(username);
            $li.remove();
            decreasePassengersCount();
        })

        function updateUsernamesToBeRemovedInput(newUsername) {
            var $usernamesToBeRemoved = $('#UsernamesToBeRemoved'),
                currentValuesAsString,
                currentValues,
                newValueAsStringifiedJSON;

            currentValuesAsString = $usernamesToBeRemoved.val();
            if (currentValuesAsString) {
                currentValues = $.parseJSON(currentValuesAsString);
            } else {
                currentValues = [];
            }

            currentValues.push(newUsername);
            newValueAsStringifiedJSON = JSON.stringify(currentValues);
            $usernamesToBeRemoved.val(newValueAsStringifiedJSON);
        }

        function decreasePassengersCount() {
            var $passengersCount = $('#passengersCount'),
                currentCount = $passengersCount.text() | 0,
                newCount;

            newCount = currentCount - 1;
            $passengersCount.text(newCount);
        }
    }())
    
});