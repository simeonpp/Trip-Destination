namespace TripDestination.Web.MVC.Areas.UserProfile.Controllers
{
    using System;
    using System.Web.Mvc;
    using Data.Models;
    using Services.Data.Contracts;
    using TripDestination.Web.MVC.Controllers;
    using ViewModels;
    using Microsoft.AspNet.Identity;
    using MVC.ViewModels.Shared;
    using Common.Infrastructure.Mapping;
    using System.Linq;

    public class ProfileController : BaseController
    {
        private IUserServices userServices;

        private ICarServices carServices;

        private IStatisticsServices statisticsServices;

        public ProfileController(IUserServices userServices, ICarServices carServices, IStatisticsServices statisticsServices)
        {
            this.userServices = userServices;
            this.carServices = carServices;
            this.statisticsServices = statisticsServices;
        }

        [HttpGet]
        public ActionResult Index(string username)
        {
            User user = this.userServices.GetByUsername(username);
            var viewModel = this.GetBasicInfo(user);
            viewModel.Comments = this.UserServices.GetComments(user.Id).To<BaseCommentViewModel>().ToList();
            viewModel.TotalComments = this.UserServices.GetTotatlComments(user.Id);
            viewModel.HasMoreComments = viewModel.TotalComments - viewModel.Comments.Count() > 0 ? true : false;
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

            var viewModel = this.Mapper.Map<EditProfileInputModel>(user);
            viewModel.BaseModel = this.GetBasicEditInfo(user);
            return this.View("~/Areas/UserProfile/Views/Profile/EditProfile.cshtml", viewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditProfile(EditProfileInputModel model)
        {
            if (this.User.Identity.GetUserName() != model.BaseModel.CurrentUsername)
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

            return this.RedirectToAction("Index", new { username = model.BaseModel.CurrentUsername });
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditCar(string username)
        {
            User user = this.userServices.GetByUsername(username);
            if (this.User.Identity.GetUserName() != user.UserName)
            {
                throw new Exception("Not auhtorized.");
            }

            var viewModel = this.Mapper.Map<EditCarInputModel>(user.Car);
            viewModel.Firsname = user.FirstName;
            viewModel.Lastname = user.LastName;
            viewModel.BaseModel = this.GetBasicEditInfo(user);
            return this.View("~/Areas/UserProfile/Views/Profile/EditCar.cshtml", viewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditCar(EditCarInputModel model)
        {
            if (this.User.Identity.GetUserName() != model.BaseModel.CurrentUsername)
            {
                throw new Exception("Not auhtorized.");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View("~/Areas/UserProfile/Views/Profile/EditCar.cshtml", model);
            }

            this.carServices.Update(
                this.User.Identity.GetUserId(),
                model.Brand,
                model.CarModel,
                model.Color,
                model.Year,
                model.TotalSeats,
                model.SpaceForLugage,
                model.Description);

            return this.RedirectToAction("Car", new { username = model.BaseModel.CurrentUsername });
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

            basicViewModel.User.Rating = this.statisticsServices.GetRatingForUser(user.Id);

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