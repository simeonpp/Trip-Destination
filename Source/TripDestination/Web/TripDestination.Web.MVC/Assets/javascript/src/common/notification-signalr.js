$(document).ready(function () {

    $.connection.hub.start();

    var notificationSignalR = $.connection.notification;

    $('#koleda').click(function () {
        notificationSignalR.server.joinRoom(msg);
    });

    notificationSignalR.client.notificationUpdate = notificationUpdate;
});

function notificationUpdate(data) {
    console.log(data);
}