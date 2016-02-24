$().ready(function () {
    
    (function () {
        $('body').on('click', '.rating-input', function () {
            var $this = $(this),
                selectedValue = $this.attr('data-value') | 0,
                $parent = $this.parent(),
                identifier = $this.parent().attr('data-identifier'),
                inputSelector = $parent.attr('data-selector'),
                filedsetInputsSelector;

            if (identifier) {
                inputSelector = '#' + inputSelector + '-' + identifier;
            } else {
                inputSelector = '#' + inputSelector;
            }

            if (identifier) {
                filedsetInputsSelector = '.adjustableRating-' + identifier + ' input';
            } else {
                filedsetInputsSelector = '.adjustableRating input';
            }

            $(filedsetInputsSelector).prop('checked', false);

            var $newRadioToCheck = $(filedsetInputsSelector + "[value='" + selectedValue + "']");
            $newRadioToCheck.prop('checked', true);

            // Set numer to the input box
            $(inputSelector).val(selectedValue);
        })        
    }())

})