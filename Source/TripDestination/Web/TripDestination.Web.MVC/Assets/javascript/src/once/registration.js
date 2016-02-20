$().ready(function () {
    var $selectRole = $('#Role'),
        $driverAdditionalWrap = $('#driverAdditionalWrap'),
        adnimationShowSpeed = 250,
        $addNewPhotoButton = $('#addNewPhotoButton'),
        $carPhotosWrap = $('#carPhotosWrap');

    initCheck()

    $selectRole.on('change', function () {
        var $this = $(this),
            selectedValue = $this.val();

        toogleDriverAdditinalWrap(selectedValue);
    })

    $addNewPhotoButton.on('click', function () {
        // <input type="file" name="CarPhotos" class="input-file" />
        var newInputFile = $('<input>')
            .attr('type', 'file')
            .attr('name', 'CarPhotos')
            .addClass('input-file');

        $carPhotosWrap.append(newInputFile);
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