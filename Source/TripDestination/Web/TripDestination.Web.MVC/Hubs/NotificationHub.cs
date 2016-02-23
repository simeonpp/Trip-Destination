namespace TripDestination.Web.MVC.Hubs
{
    using System.Threading.Tasks;
    using Common.Infrastructure.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.SignalR;
    using Services.Web.Services;
    using Services.Web.Services.Contracts;

    public class NotificationHub : Hub
    {
        private static readonly ConnectionMapping<string> Connections = new ConnectionMapping<string>();

        public override Task OnConnected()
        {
            string name = this.Context.User.Identity.Name;
            Connections.Add(name, this.Context.ConnectionId);
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

        public void SendMessage(string message)
        {
            var msg = string.Format("{0}: {1}", this.Context.ConnectionId, message);
            UpdateNotify(new BaseSignalRModel() { NotificationCount = 2 });
        }

        public static void UpdateNotify(BaseSignalRModel model)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            context.Clients.All.addMessage(model);
        }
    }
}