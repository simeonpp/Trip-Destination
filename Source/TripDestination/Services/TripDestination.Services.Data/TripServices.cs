﻿namespace TripDestination.Data.Services
{
    using System;
    using System.Linq;
    using System.Data.Entity;
    using TripDestination.Services.Data.Contracts;
    using Common;
    using Models;
    using System.Collections.Generic;
    using TripDestination.Common.Infrastructure.Models;
    using TripDestination.Services.Data.Models;
    using TripDestination.Common.Infrastructure.Constants;
    public class TripServices : ITripServices
    {
        private readonly IDbRepository<Trip> tripRepos;

        private readonly IDbRepository<PassengerTrip> passengerTripsRepos;

        private readonly IUserServices userServices;

        public TripServices(IDbRepository<Trip> tripRepos, IDbRepository<PassengerTrip> passengerTripsRepos, IUserServices userServices)
        {
            this.tripRepos = tripRepos;
            this.passengerTripsRepos = passengerTripsRepos;
            this.userServices = userServices;
        }

        public Trip Create(int fromTownId, int toTownId, DateTime dateOfLeaving, int availableSeats, string placeOfLeaving, bool pickUpFromAddress, string description, DateTime ETA, decimal price, string driverId)
        {
            Trip trip = new Trip()
            {
                DateOfLeaving = dateOfLeaving,
                FromId = fromTownId,
                ToId = toTownId,
                DriverId = driverId,
                Description = description,
                ETA = ETA,
                PlaceOfLeaving = placeOfLeaving,
                PickUpFromAddress = pickUpFromAddress,
                AvailableSeats = availableSeats,
                Price = price
            };

            this.tripRepos.Add(trip);
            this.tripRepos.Save();

            return trip;
        }

        public Trip GetById(int id)
        {
            Trip trip = this.tripRepos
                .All()
                .Where(t => t.Id == id)
                .FirstOrDefault();

            return trip;
        }

        public IQueryable<Trip> GetForDay(DateTime day)
        {
            var trips = this.tripRepos
                .All()
                .Where(t => DbFunctions.TruncateTime(t.DateOfLeaving) == day
                            && t.Status == TripStatus.Open
                            && t.AvailableSeats >= 1)
                .OrderByDescending(t => t.DateOfLeaving);

            return trips;
        }

        public IQueryable<Trip> GetLatest(int count)
        {
            var trips = this.tripRepos
                .All()
                .OrderByDescending(t => t.CreatedOn)
                .Take(count);

            return trips;
        }

        public IQueryable<Trip> GetTodayTrips(int count)
        {
            var trips = this.tripRepos
                .All()
                .Where(t => DbFunctions.TruncateTime(t.DateOfLeaving) == DateTime.Today)
                .OrderByDescending(t => t.CreatedOn)
                .Take(count);

            return trips;
        }

        public IQueryable<string> GetTopTownsDestination(bool townsFrom = true, int count = 2)
        {
            var towns = this.tripRepos
               .All()
               .GroupBy(t => townsFrom == true ? t.From.Name : t.To.Name)
               .Select(group => new { TownName = group.Key, Count = group.Count() })
               .OrderByDescending(e => e.Count)
               .Take(count)
               .Select(t => t.TownName);

            return towns;
        }

        public IQueryable<PassengerTrip> GetPassengers(Trip trip)
        {
            var passengers = this.passengerTripsRepos.All()
                .Where(pt => pt.Trip == trip && pt.Approved == true && pt.IsDeleted == false)
                .OrderBy(pt => pt.CreatedOn);

            return passengers;
        }

        public Trip Edit(int tripId, DateTime dateOfLeaving, int leftAvailableSeats, string placeOfLeaving, bool pickUpFromAddress, string description, DateTime ETA, IEnumerable<string> usernamesToBeRemoved)
        {
            var dbTrip = this.GetById(tripId);

            int availableSeats = leftAvailableSeats;
            dbTrip.AvailableSeats = availableSeats;

            dbTrip.DateOfLeaving = dateOfLeaving;
            dbTrip.PlaceOfLeaving = placeOfLeaving;
            dbTrip.PickUpFromAddress = pickUpFromAddress;
            dbTrip.Description = description;
            dbTrip.ETA = ETA;

            foreach (var username in usernamesToBeRemoved)
            {
                PassengerTrip passengerToRemove = dbTrip.Passengers
                    .Where(p => p.User.UserName == username && p.Approved == true && p.IsDeleted == false)
                    .FirstOrDefault();

                if (passengerToRemove != null)
                {
                    this.passengerTripsRepos.HardDelete(passengerToRemove);
                }
            }

            this.passengerTripsRepos.Save();
            this.tripRepos.Save();

            return dbTrip;
        }

        public void Delete(int id, string userId)
        {
            var dbTrip = this.GetById(id);

            if (dbTrip != null && dbTrip.DriverId == userId )
            {
                this.tripRepos.Delete(dbTrip);
                this.tripRepos.Save();
            }

            return;
        }

        public int GetTakenSeatsCount(int tripId)
        {
            var dbTrip = this.GetById(tripId);
            int takenSeats = dbTrip.Passengers.Where(p => p.IsDeleted == false && p.Approved == true).Count();
            return takenSeats;
        }

        public BaseResponseAjaxModel JoinRequest(int tripId, string userId)
        {
            var response = new BaseResponseAjaxModel();
            var dbTrip = this.GetById(tripId);

            if (dbTrip == null)
            {
                response.ErrorMessage = "No such trip";
                return response;
            }

            bool tripHasAvailableSeats = dbTrip.AvailableSeats > 0;

            if (!tripHasAvailableSeats)
            {
                response.ErrorMessage = "Sorry, no available seats left.";
                return response;
            }

            bool currentPassengerAlreadyHasJoinRequest = dbTrip.Passengers
                .Where(p => p.UserId == userId && p.IsDeleted == false)
                .FirstOrDefault()
                != null ? true : false;

            if (currentPassengerAlreadyHasJoinRequest)
            {
                response.ErrorMessage = "You already has pending join request";
                return response;
            }

            PassengerTrip passengerTrip = new PassengerTrip()
            {
                Trip = dbTrip,
                UserId = userId,
                Approved = false
            };

            dbTrip.Passengers.Add(passengerTrip);
            this.tripRepos.Save();

            response.Status = true;
            return response;
        }

        public BaseResponseAjaxModel LeaveTrip(int tripId, string userId)
        {
            var response = new BaseResponseAjaxModel();
            var dbTrip = this.GetById(tripId);

            if (dbTrip == null)
            {
                response.ErrorMessage = "No such trip";
                return response;
            }

            var passengerTrip = dbTrip.Passengers
                .Where(p => p.UserId == userId && p.IsDeleted == false)
                .FirstOrDefault();

            if (passengerTrip == null)
            {
                response.ErrorMessage = "You first need to send join request.";
                return response;
            }

            passengerTrip.IsDeleted = true;
            this.tripRepos.Save();

            response.Status = true;
            return response;

        }

        public bool CheckIfUserHasPendingRequest(int tripId, string userId)
        {
            var dbTrip = this.GetById(tripId);

            if (dbTrip != null)
            {
                bool hasPendingRequest = dbTrip.Passengers
                    .Where(p => p.UserId == userId && p.Approved == false && p.IsDeleted == false)
                    .Any();

                return hasPendingRequest;
            }

            return false;
        }

        public bool CheckIfTripHasMoreCommentsToLoad(int tripId, int currentLoadedComments)
        {
            var dbTrip = this.GetById(tripId);

            if (dbTrip != null)
            {
                var result = (dbTrip.Comments.Where(c => c.IsDeleted == false).Count() - currentLoadedComments) > 0 ? true : false;
                return result;
            }

            return false;
        }

        public BaseResponseAjaxModel AddComment(int tripId, string userId, string commentText)
        {

            var response = new BaseResponseAjaxModel();

            if (commentText.Length < ModelConstants.CommentTextMinLength || commentText.Length > ModelConstants.CommentTextMaxLength)
            {
                response.ErrorMessage = string.Format("Comment text should be between {0} and {1} symbols long", ModelConstants.CommentTextMinLength, ModelConstants.CommentTextMaxLength);
                return response;
            }

            var dbTrip = this.GetById(tripId);

            if (dbTrip == null)
            {
                response.ErrorMessage = "No such trip";
                return response;
            }

            TripComment comment = new TripComment()
            {
                TripId = tripId,
                AuthorId = userId,
                Text = commentText
            };

            dbTrip.Comments.Add(comment);
            this.tripRepos.Save();
            this.tripRepos.Reload(dbTrip);

            var author = this.userServices.GetById(userId);

            response.Status = true;
            response.Data = new CommentResponseModel()
            {
                FirstName = author.FirstName,
                LastName = author.LastName,
                UserUrl = "www.google.com", // TODO: Implement URL
                UserImageSrc = this.GetUserImageSmallUrl(author.Avatar.FileName),
                CreatedOnFormatted = comment.CreatedOn.ToString("dd.MM.yyyy HH:mm"),
                CommentText = comment.Text,
                CommentTotalCount = dbTrip.Comments.Count
            };

            return response;
        }

        public BaseResponseAjaxModel ApproveJoinRequest(int tripId, string username, string actionUserId)
        {
            var dbTrip = this.GetById(tripId);
            var response = new BaseResponseAjaxModel();

            if (dbTrip == null)
            {
                response.ErrorMessage = "No such trip.";
                return response;
            }

            if (dbTrip.DriverId != actionUserId)
            {
                response.ErrorMessage = "Only driver of the trip is authorized to perfome this action.";
                return response;
            }

            var passengerTrip = dbTrip.Passengers
                .Where(p => p.TripId == tripId && p.User.UserName == username && p.Approved == false && p.IsDeleted == false)
                .FirstOrDefault();

            if (passengerTrip == null)
            {
                response.ErrorMessage = "Such join request does not exist.";
                return response;
            }

            bool tripHasAvailableSeats = dbTrip.AvailableSeats > 0;

            if (!tripHasAvailableSeats)
            {
                response.ErrorMessage = "Sorry, no available seats left.";

                return response;
            }

            passengerTrip.Approved = true;
            dbTrip.AvailableSeats = dbTrip.AvailableSeats - 1;
            this.tripRepos.Save();

            response.Status = true;
            response.Data = new ApprovedJoinRequestResponseModel()
            {
                PassengersCount = dbTrip.Passengers.Where(p => p.Approved == true && p.IsDeleted == false).Count(),
                AvailableSeatsCount = dbTrip.AvailableSeats,
                PendingApproveUsersCount = dbTrip.Passengers.Where(p => p.Approved == false && p.IsDeleted == false).Count(),
                FirstName = passengerTrip.User.FirstName,
                LastName = passengerTrip.User.LastName,
                ImageSrc = "http://www.keenthemes.com/preview/conquer/assets/plugins/jcrop/demos/demo_files/image1.jpg", // TODO: Implement imageSrc
                UserProfileLink = "www.google.com", // TODO: Implement URL
            };

            return response;
        }

        public BaseResponseAjaxModel DisapproveJoinRequest(int tripId, string username, string actionUserId)
        {
            var dbTrip = this.GetById(tripId);
            var response = new BaseResponseAjaxModel();

            if (dbTrip == null)
            {
                response.ErrorMessage = "No such trip.";
                return response;
            }

            if (dbTrip.DriverId != actionUserId)
            {
                response.ErrorMessage = "Only driver of the trip is authorized to perfome this action.";
                return response;
            }

            var passengerTrip = dbTrip.Passengers
                .Where(p => p.TripId == tripId && p.User.UserName == username && p.Approved == false && p.IsDeleted == false)
                .FirstOrDefault();

            if (passengerTrip == null)
            {
                response.ErrorMessage = "Such join request does not exist.";
                return response;
            }

            passengerTrip.IsDeleted = true;
            this.tripRepos.Save();

            response.Status = true;
            response.Data = new ApprovedJoinRequestResponseModel()
            {
                PendingApproveUsersCount = dbTrip.Passengers.Where(p => p.Approved == false && p.IsDeleted == false).Count(),
            };

            return response;
        }

        public BaseResponseAjaxModel LoadComments(int tripId, int offset)
        {
            var dbTrip = this.GetById(tripId);
            var response = new BaseResponseAjaxModel();

            if (dbTrip == null)
            {
                response.ErrorMessage = "No such trip.";
                return response;
            }            
            var comments = dbTrip.Comments
                .Where(c => c.IsDeleted == false)
                .OrderByDescending(c => c.CreatedOn)
                .Skip(offset)
                .Take(WebApplicationConstants.CommentsOfset)
                .Select(c => new CommentResponseModel()
                {
                    FirstName = c.Author.FirstName,
                    LastName = c.Author.LastName,
                    UserUrl = "http://www.google.com", // TODO: Fix url
                    UserImageSrc = this.GetUserImageSmallUrl(c.Author.Avatar.FileName),
                    CreatedOnFormatted = c.CreatedOn.ToString("dd.MM.yyyy HH:mm"),
                    CommentText = c.Text
                })
                .ToList();


            if (comments.Count > 0)
            {
                int newOffset = offset + WebApplicationConstants.CommentsOfset;
                bool hasMoreCommentsToLoad = this.CheckIfTripHasMoreCommentsToLoad(dbTrip.Id, newOffset);

                response.Status = true;
                response.Data = new LoadedCommentsResponseModel()
                {
                    HasMoreCommentsToLoad = hasMoreCommentsToLoad,
                    Offset = newOffset,
                    Comments = comments
                };
            }

            return response;
        }

        public bool CheckIfUserLikedTrip(Trip trip, string userId)
        {
            Like like = trip.Likes
                .Where(l => l.UserId == userId && l.IsDeleted == false)
                .FirstOrDefault();

            if (like != null)
            {
                return like.Value;
            }

            return false;
        }

        public BaseResponseAjaxModel LikeDislike(int tripId, string userId, bool value)
        {
            var dbTrip = this.GetById(tripId);
            var response = new BaseResponseAjaxModel();

            if (dbTrip == null)
            {
                response.ErrorMessage = "No such trip.";
                return response;
            }

            Like like = dbTrip.Likes
                .Where(l => l.UserId == userId && l.IsDeleted == false)
                .FirstOrDefault();

            if (like == null)
            {
                like = new Like()
                {
                    TripId = dbTrip.Id,
                    UserId = userId,
                    Value = value
                };

                dbTrip.Likes.Add(like);
            }
            else
            {
                like.Value = value;
            }

            this.tripRepos.Save();

            response.Status = true;
            response.Data = this.GetLikesCount(dbTrip);

            return response;
        }

        public int GetLikesCount(Trip trip)
        {
            int likes = trip.Likes
                .Where(l => l.IsDeleted == false && l.Value == true)
                .Count();

            int dislikes = trip.Likes
                .Where(l => l.IsDeleted == false && l.Value == false)
                .Count();

            int likesCount = likes - dislikes;
            return likesCount;
        }

        private string GetUserImageSmallUrl(string fileName)
        {
            return string.Format(
                    "/{0}/{1}/{2}",
                    WebApplicationConstants.ImageRouteUrl,
                    fileName,
                    WebApplicationConstants.ImageUserAvatarSmallWidth);
        }
    }
}
