$().ready(function () {
    
    (function () {
        $('body').on('click', '.rating-input', function () {
            var $this = $(this),
                selectedValue = $this.attr('data-value') | 0,
                $parent = $this.parent(),
                identifier = $this.parent().attr('data-identifier'),
                inputSelector = $parent.attr('data-selector'),
                filedsetInputsSelector,
                givenRateWrapSelector,
                giveRateSelector;

            if (identifier) {
                inputSelector = '#' + inputSelector + '-' + identifier;
                givenRateWrapSelector = "#givenRateWrap-" + identifier;
                giveRateSelector = '#givenRate-' + identifier;
                filedsetInputsSelector = '#adjustableRating-' + identifier + ' input';
            } else {
                inputSelector = '#' + inputSelector;
                givenRateWrapSelector = "#givenRateWrap";
                giveRateSelector = '#givenRate';
                filedsetInputsSelector = '#adjustableRating input';
            }

            $(filedsetInputsSelector).prop('checked', false);

            var $newRadioToCheck = $(filedsetInputsSelector + "[value='" + selectedValue + "']");
            $newRadioToCheck.prop('checked', true);
            $(givenRateWrapSelector).show(250);
            $(giveRateSelector).text(selectedValue);
            $parent.toggle(250);

            // Set numer to the input box
            $(inputSelector).val(selectedValue);
        })        
    }())

})