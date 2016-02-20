﻿namespace TripDestination.Web.MVC.Areas.UserProfile.Controllers
{
    using System;
    using System.Web.Mvc;
    using Data.Models;
    using MVC.ViewModels.Shared;
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

        public ActionResult Index(string username)
        {
            User user = this.userServices.GetByUsername(username);
            var viewModel = this.GetBasicInfo(user);
            return this.View("~/Areas/UserProfile/Views/Profile/Index.cshtml", viewModel);
        }

        public ActionResult Car(string username)
        {
            User user = this.userServices.GetByUsername(username);
            var viewModel = this.GetBasicInfo(user);
            return this.View("~/Areas/UserProfile/Views/Profile/Car.cshtml", viewModel);
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
                User = this.Mapper.Map<ExtendedUserViewModel>(user),
                CurrentUsername = this.User.Identity.GetUserName()
            };

            return basicViewModel;
        }
    }
}