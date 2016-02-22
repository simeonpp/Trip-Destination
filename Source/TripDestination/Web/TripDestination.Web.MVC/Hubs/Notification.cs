namespace TripDestination.Web.MVC.Hubs
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Common.Infrastructure.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.SignalR;

    public class Notification : Hub
    {
        private static Dictionary<string, string> usersConnections = new Dictionary<string, string>();

        public override Task OnConnected()
        {
            string userId = this.Context.Request.User.Identity.GetUserId();
            string connectionId = this.Context.ConnectionId;
            usersConnections.Add(userId, connectionId);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string userId = this.Context.Request.User.Identity.GetUserId();
            string connectionId = this.Context.ConnectionId;

            if (usersConnections.ContainsKey(userId))
            {
                usersConnections.Remove(userId);
            }

            return base.OnDisconnected(stopCalled);
        }

        public void SendNotificationUpdate(BaseSignalRModel model)
        {
        }

        public void JoinRoom()
        {
            this.Clients.All.notificationUpdate("opa");
        }
    }
}