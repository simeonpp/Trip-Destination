$().ready(function () {
    var $newsletterButton = $('#newsletterButton');

    $newsletterButton.on('click', function () {
        var $newsletterEmailInput = $('#newsletterEmailInput'),
            emailPattern = /^[-a-z0-9~!$%^&*_=+}{\'?]+(\.[-a-z0-9~!$%^&*_=+}{\'?]+)*@([a-z0-9_][-a-z0-9_]*(\.[-a-z0-9_]+)*\.(aero|arpa|biz|com|coop|edu|gov|info|int|mil|museum|name|net|org|pro|travel|mobi|[a-z][a-z])|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,5})?$/i;

        if (emailPattern.test($newsletterEmailInput.val())) {
            toastr.success('You have successfully subscribed to our newsletter.');
        } else {
            toastr.error('You have entered invalid email.');
        }
    });
})