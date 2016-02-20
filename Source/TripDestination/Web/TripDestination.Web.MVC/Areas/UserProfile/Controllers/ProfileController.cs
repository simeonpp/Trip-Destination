namespace TripDestination.Web.MVC.Areas.UserProfile.Controllers
{
    using System;
    using System.Web.Mvc;
    using Data.Models;
    using Services.Data.Contracts;
    using TripDestination.Web.MVC.Controllers;
    using ViewModels;
    using Microsoft.AspNet.Identity;

    public class ProfileController : BaseController
    {
        private IUserServices userServices;

        public ProfileController(IUserServices userServices)
        {
            this.userServices = userServices;
        }

        [HttpGet]
        public ActionResult Index(string username)
        {
            User user = this.userServices.GetByUsername(username);
            var viewModel = this.GetBasicInfo(user);
            return this.View("~/Areas/UserProfile/Views/Profile/Index.cshtml", viewModel);
        }

        [HttpGet]
        public ActionResult Car(string username)
        {
            User user = this.userServices.GetByUsername(username);
            var viewModel = this.GetBasicInfo(user);
            return this.View("~/Areas/UserProfile/Views/Profile/Car.cshtml", viewModel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Trips(string username)
        {
            User user = this.userServices.GetByUsername(username);
            if (this.User.Identity.GetUserName() != user.UserName)
            {
                throw new Exception("Not auhtorized.");
            }

            var viewModel = this.GetBasicInfo(user);
            return this.View("~/Areas/UserProfile/Views/Profile/Trips.cshtml", viewModel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditProfile(string username)
        {
            User user = this.userServices.GetByUsername(username);
            if (this.User.Identity.GetUserName() != user.UserName)
            {
                throw new Exception("Not auhtorized.");
            }

            var viewModel = this.GetBasicEditInfo(user);
            return this.View("~/Areas/UserProfile/Views/Profile/EditProfile.cshtml", viewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditProfile(BaseEditModel model)
        {
            if (this.User.Identity.GetUserName() != model.CurrentUsername)
            {
                throw new Exception("Not auhtorized.");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View("~/Areas/UserProfile/Views/Profile/EditProfile.cshtml", model);
            }

            this.userServices.Update(
                this.User.Identity.GetUserId(),
                model.Email,
                model.FirstName,
                model.LastName,
                model.PhoneNumber,
                model.Description);

            return this.RedirectToAction("Index", new { username = model.CurrentUsername });
        }

        private ProfileViewModel GetBasicInfo(User user)
        {
            if (user == null)
            {
                throw new Exception("User not found,");
            }

            var roles = this.userServices.GetUserRoles(user.Id);
            var basicViewModel = new ProfileViewModel()
            {
                Role = roles[0],
                User = this.Mapper.Map<UserViewModel>(user),
                CurrentUsername = this.User.Identity.GetUserName()
            };

            return basicViewModel;
        }

        private BaseEditModel GetBasicEditInfo(User user)
        {
            if (user == null)
            {
                throw new Exception("User not found,");
            }

            var roles = this.userServices.GetUserRoles(user.Id);
            var basicViewModel = this.Mapper.Map<BaseEditModel>(user);
            basicViewModel.Role = roles[0];
            basicViewModel.CurrentUsername = this.User.Identity.GetUserName();
            return basicViewModel;
        }
    }
}