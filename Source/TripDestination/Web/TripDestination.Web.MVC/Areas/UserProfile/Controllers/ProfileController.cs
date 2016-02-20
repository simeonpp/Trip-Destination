namespace TripDestination.Web.MVC.Areas.UserProfile.Controllers
{
    using System;
    using System.Web.Mvc;
    using Data.Models;
    using MVC.ViewModels.Shared;
    using Services.Data.Contracts;
    using TripDestination.Web.MVC.Controllers;
    using ViewModels;

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

            return this.View("~/Areas/UserProfile/Views/Profile/Index.cshtml", viewModel);
        }
    }
}