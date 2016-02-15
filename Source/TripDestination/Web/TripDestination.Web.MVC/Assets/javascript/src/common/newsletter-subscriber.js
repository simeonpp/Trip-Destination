$().ready(function () {
    var $newsletterButton = $('#newsletterButton');

    $newsletterButton.on('click', function () {
        var $newsletterEmailInput = $('#newsletterEmailInput'),
            emailPattern = /^[-a-z0-9~!$%^&*_=+}{\'?]+(\.[-a-z0-9~!$%^&*_=+}{\'?]+)*@([a-z0-9_][-a-z0-9_]*(\.[-a-z0-9_]+)*\.(aero|arpa|biz|com|coop|edu|gov|info|int|mil|museum|name|net|org|pro|travel|mobi|[a-z][a-z])|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,5})?$/i,
            email = $newsletterEmailInput.val();

        if (emailPattern.test(email)) {
            $.getJSON(
                '/Newsletter/Subscribe',
                {
                    email: email
                },
                function (response) {
                    if (reseponse.status) {
                        toastr.success('You have successfully subscribed email ' + response.data + ' to our newsletter.');
                    } else {
                        toastr.error('An error has occured during trying to register to newsletter. Please try again or contact our team for help.');
                    }
                }
            )
        } else {
            toastr.error('You have entered invalid email.');
        }
    });
})