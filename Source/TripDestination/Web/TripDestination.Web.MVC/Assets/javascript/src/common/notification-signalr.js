$(document).ready(function () {

    $.connection.hub.start();

    var chat = $.connection.notificationHub;

    $('#send-message').click(function () {
        chat.server.sendMessage("asd");
    });

    chat.client.addMessage = addMessage;
});

function addMessage(data) {
    $('#notificationsCount').text(data.NotificationCount);
}