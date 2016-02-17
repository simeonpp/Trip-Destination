$().ready(function () {
    var $selectRole = $('#Role'),
        $driverAdditionalWrap = $('#driverAdditionalWrap'),
        adnimationShowSpeed = 250;

    initCheck()

    $selectRole.on('change', function () {
        var $this = $(this),
            selectedValue = $this.val();

        toogleDriverAdditinalWrap(selectedValue);
    })

    function toogleDriverAdditinalWrap(value) {
        if (value == "Driver") {
            $driverAdditionalWrap.show(adnimationShowSpeed);
        } else {
            $driverAdditionalWrap.hide(adnimationShowSpeed);
        }
    }

    function initCheck() {
        var initialValue = $("#Role option:selected").val();
        toogleDriverAdditinalWrap(initialValue);
    }
});