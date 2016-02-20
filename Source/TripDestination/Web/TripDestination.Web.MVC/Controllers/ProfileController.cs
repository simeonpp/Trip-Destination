namespace TripDestination.Web.MVC.Controllers
{
    using System;
    using Data.Models;
    using Services.Data.Contracts;
    using System.Web.Mvc;
    using ViewModels.Shared;
    using System.Web.Security;
    using ViewModels.Profile;
    using Microsoft.AspNet.Identity;
    using Data.Data;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Linq;

    public class ProfileController : BaseController
    {
        private IUserServices userServices;

        public ProfileController(IUserServices userServices)
        {
            this.userServices = userServices;
        }

        public ActionResult Index(string username)
        {
            User user = this.userServices.GetByUsername(username);
            if (user == null)
            {
                throw new Exception("User not found,");
            }

            var roles = this.userServices.GetUserRoles(user.Id);
            var viewModel = new ProfileViewModel()
            {
                Role = roles[0],
                User = this.Mapper.Map<ExtendedUserViewModel>(user)
            };

            return this.View(viewModel);
        }
    }
}