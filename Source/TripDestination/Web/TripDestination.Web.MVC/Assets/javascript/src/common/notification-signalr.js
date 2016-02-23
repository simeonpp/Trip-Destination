$(document).ready(function () {
    $.connection.hub.start();
    var chat = $.connection.notificationHub;
    chat.client.addMessage = addMessage;
});

function addMessage(data) {
    $('#notificationsCount').text(data);
    toastr.info("New notification.");
}