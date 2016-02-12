using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TripDestination.Web.MVC.Startup))]
namespace TripDestination.Web.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}
