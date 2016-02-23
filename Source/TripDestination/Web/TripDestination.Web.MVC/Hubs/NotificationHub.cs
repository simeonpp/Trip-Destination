namespace TripDestination.Web.MVC.Hubs
{
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Infrastructure.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.SignalR;
    using Services.Web.Services;
    using Services.Web.Services.Contracts;

    public class NotificationHub : Hub
    {
        private static readonly ConnectionMapping<string> Connections = new ConnectionMapping<string>();

        public static void UpdateNotify(BaseSignalRModel model)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            foreach (var userNotificationCounts in model.UsersNotificationCounts)
            {
                string userId = userNotificationCounts.Item1;
                int notSeendNorificationCounts = userNotificationCounts.Item2;
                context.Clients.Group(userId).addMessage(notSeendNorificationCounts);
            }
        }

        public override Task OnConnected()
        {
            if (this.Context.User.Identity.IsAuthenticated)
            {
                string userId = this.Context.User.Identity.GetUserId();
                this.Groups.Add(this.Context.ConnectionId, userId);
            }

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string name = this.Context.User.Identity.Name;
            Connections.Remove(name, this.Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            string name = this.Context.User.Identity.Name;
            if (!Connections.GetConnections(name).Contains(this.Context.ConnectionId))
            {
                Connections.Add(name, this.Context.ConnectionId);
            }

            return base.OnReconnected();
        }
    }
}