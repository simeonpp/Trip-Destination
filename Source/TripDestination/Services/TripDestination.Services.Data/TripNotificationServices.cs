﻿namespace TripDestination.Services.Data
{
    using System;
    using System.Linq;
    using Common.Infrastructure.Models;
    using Contracts;
    using System.Data.Entity;
    using TripDestination.Data.Common;
    using TripDestination.Data.Models;

    public class TripNotificationServices : ITripNotificationServices
    {
        private readonly IDbRepository<TripNotification> tripNotificationRepos;

        private readonly ITripServices tripServices;

        private readonly INotificationTypeServices notificationTypeServices;

        public TripNotificationServices(IDbRepository<TripNotification> tripNotificationRepos, ITripServices tripServices, INotificationTypeServices notificationTypeServices)
        {
            this.tripNotificationRepos = tripNotificationRepos;
            this.tripServices = tripServices;
            this.notificationTypeServices = notificationTypeServices;
        }

        public TripNotification GetById(int id)
        {
            return this.tripNotificationRepos
                .All()
                .Where(n => n.Id == id)
                .FirstOrDefault();
        }

        public IQueryable<Notification> GetForUser(string userId)
        {
            var now = DateTime.Now;
            var notificiations = this.tripNotificationRepos
                .All()
                .Where(n => n.ForUserId == userId
                        && DbFunctions.TruncateTime(n.AvailableAfter) < now
                        /*&& DbFunctions.TruncateTime(n.DueTo) > now*/
                        && n.IsDeleted == false)
                .OrderByDescending(n => n.CreatedOn);

            return notificiations;
        }

        public IQueryable<Notification> GetNotSeenForUser(string userId)
        {
            var now = DateTime.Now;
            var notificiations = this.tripNotificationRepos
                .All()
                .Where(n => n.ForUserId == userId
                        && DbFunctions.TruncateTime(n.AvailableAfter) < now
                        /*&& DbFunctions.TruncateTime(n.DueTo) > now*/
                        && n.IsDeleted == false
                        && n.Seen == false)
                .OrderByDescending(n => n.CreatedOn);

            return notificiations;
        }

        public void SetAsSeen(int id)
        {
            var dbNotification = this.GetById(id);

            if (dbNotification == null)
            {
                throw new Exception("Notification not found.");
            }

            dbNotification.Seen = true;
            this.tripNotificationRepos.Save();
        }

        public BaseResponseAjaxModel ApproveNotification(int notificationId, string userId)
        {
            var response = new BaseResponseAjaxModel();
            var notification = this.GetById(notificationId);

            if (notification == null)
            {
                response.ErrorMessage = "No such notification.";
                return response;
            }

            if (notification.ForUserId != userId)
            {
                response.ErrorMessage = "Only the owner of the notifcation can perfom this action";
                return response;
            }

            if (notification.ActionHasBeenTaken == true)
            {
                response.ErrorMessage = "Action for this notification has been already taken.";
                return response;
            }

            notification.ActionHasBeenTaken = true;

            if (notification.Type.Key == NotificationKey.JoinTripRequest)
            {
                return this.tripServices.ApproveJoinRequest(notification.TripId, notification.FromUser.UserName, userId);
            }

            if (notification.Type.Key == NotificationKey.FinishTripDriverRequest)
            {
                notification.Trip.Status = TripStatus.Finished;
                this.tripNotificationRepos.Save();
                response.Status = true;

                response.SignalRModel = this.tripServices.NotifyTripPassengersAndDriverForTripFinish(notification.Trip, userId);
                response.SignalRModel.RedirectToUrl = true;
                response.SignalRModel.UrlToRedirectTo = string.Format("/Trip/Rate/{0}/{1}-{2}", notification.TripId, notification.Trip.From.Name, notification.Trip.To.Name);
            }

            return response;
        }

        public BaseResponseAjaxModel DisapproveNotification(int notificationId, string userId)
        {
            var response = new BaseResponseAjaxModel();
            var notification = this.GetById(notificationId);

            if (notification == null)
            {
                response.ErrorMessage = "No such notification.";
                return response;
            }

            if (notification.ForUserId != userId)
            {
                response.ErrorMessage = "Only the owner of the notifcation can perfom this action";
                return response;
            }

            if (notification.ActionHasBeenTaken == true)
            {
                response.ErrorMessage = "Action for this notification has been already taken.";
                return response;
            }

            notification.ActionHasBeenTaken = true;

            if (notification.Type.Key == NotificationKey.JoinTripRequest)
            {
                return this.tripServices.DisapproveJoinRequest(notification.TripId, notification.FromUser.UserName, userId);
            }

            return response;
        }

        public void SetTripFinishActionHasBeenTakenToTrue(TripNotification tripNotification)
        {
            tripNotification.ActionHasBeenTaken = true;
            this.tripNotificationRepos.Save();
        }

        public TripNotification GetTripFinishTripNotificationByTripAndForUser(int tripId, string userId)
        {
            var notificationType = this.notificationTypeServices.GetByKey(NotificationKey.TripFinished);

            if (notificationType == null)
            {
                throw new Exception("No such Notification type");
            }

            TripNotification tripNotification = this.tripNotificationRepos
                .All()
                .Where(tn => tn.Type.Key == notificationType.Key && tn.TripId == tripId && tn.ForUserId == userId)
                .FirstOrDefault();

            return tripNotification;
        }

        public IQueryable<TripNotification> GetAll()
        {
            return this.tripNotificationRepos.All();
        }
    }
}
