namespace TripDestination.Data.Services
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
    using TripDestination.Services.Data;
    using System.Linq.Dynamic;

    public class TripServices : ITripServices
    {
        public const string DefaultSortDirection = "ASC";

        public const string DefaultOrder = "DateOfLeaving";

        private readonly IDbRepository<Trip> tripRepos;

        private readonly IDbRepository<PassengerTrip> passengerTripsRepos;

        private readonly IUserServices userServices;

        private readonly INotificationServices notificationServices;

        public TripServices(IDbRepository<Trip> tripRepos, IDbRepository<PassengerTrip> passengerTripsRepos, IUserServices userServices, INotificationServices notificationServices)
        {
            this.tripRepos = tripRepos;
            this.passengerTripsRepos = passengerTripsRepos;
            this.userServices = userServices;
            this.notificationServices = notificationServices;
        }

        public IQueryable<Trip> GetAll()
        {
            return this.tripRepos.All();
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
            this.tripRepos.Reload(trip);

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

        public Trip GetByIdWithStatusCheck(int id)
        {
            Trip trip = this.tripRepos
                .All()
                .Where(t => t.Id == id)
                .FirstOrDefault();

            var now = DateTime.Now;
            if (trip.Status == TripStatus.Open && (trip.DateOfLeaving - now).Ticks < 0 && (trip.ETA - now).Ticks > 0)
            {
                trip.Status = TripStatus.InProgress;
                this.tripRepos.Save();
            }

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

        public BaseResponseAjaxModel Edit(int tripId, DateTime dateOfLeaving, int leftAvailableSeats, string placeOfLeaving, bool pickUpFromAddress, string description, DateTime ETA, IEnumerable<string> usernamesToBeRemoved)
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
                    this.notificationServices.Create(
                    dbTrip.Id,
                    dbTrip.DriverId,
                    passengerToRemove.UserId,
                    NotificationConstants.DriverRemovePassegerTitle,
                    string.Format(NotificationConstants.DriverRemovePassegerFormat, dbTrip.Driver.UserName, dbTrip.From.Name, dbTrip.To.Name, dbTrip.DateOfLeaving.ToString("dd/MM/yyyy HH:mm")),
                    NotificationKey.DriverRemovePassenger,
                    dbTrip.DateOfLeaving);

                    this.passengerTripsRepos.HardDelete(passengerToRemove);
                }
            }

            foreach (var passenger in dbTrip.Passengers.Where(p => p.Approved == true))
            {
                this.notificationServices.Create(
                    dbTrip.Id,
                    dbTrip.DriverId,
                    passenger.UserId,
                    NotificationConstants.TripChangedTitle,
                    string.Format(NotificationConstants.TripChangedpMessageFormat, dbTrip.Driver.UserName, dbTrip.From.Name, dbTrip.To.Name, dbTrip.DateOfLeaving.ToString("dd/MM/yyyy HH:mm")),
                    NotificationKey.TripChanged,
                    dbTrip.DateOfLeaving);
            }

            this.passengerTripsRepos.Save();
            this.tripRepos.Save();

            var response = new BaseResponseAjaxModel();
            response.SignalRModel = this.notificationServices.SendNotifications(new string[] { dbTrip.DriverId });

            return response;
        }

        public Trip AdminEdit(int tripId, int leftAvailableSeats, string placeOfLeaving, bool pickUpFromAddress, string description)
        {
            var dbTrip = this.GetById(tripId);

            int availableSeats = leftAvailableSeats;
            dbTrip.AvailableSeats = availableSeats;
            dbTrip.PlaceOfLeaving = placeOfLeaving;
            dbTrip.PickUpFromAddress = pickUpFromAddress;
            dbTrip.Description = description;

            this.passengerTripsRepos.Save();
            this.tripRepos.Save();

            return dbTrip;
        }

        public void Delete(int id, string userId)
        {
            var dbTrip = this.GetById(id);

            if (dbTrip != null && dbTrip.DriverId == userId)
            {
                this.tripRepos.Delete(dbTrip);
                this.tripRepos.Save();
            }

            return;
        }

        public void AdminDelete(int id)
        {
            var dbTrip = this.GetById(id);

            if (dbTrip != null)
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

            var passenger = this.userServices.GetById(userId);
            this.notificationServices.Create(
                dbTrip.Id,
                userId,
                dbTrip.DriverId,
                NotificationConstants.JoinTripRequestTitle,
                string.Format(NotificationConstants.JoinTripRequestMessageFormat, passenger.UserName, dbTrip.From.Name, dbTrip.To.Name, dbTrip.DateOfLeaving.ToString("dd/MM/yyyy HH:mm")),
                NotificationKey.JoinTripRequest,
                dbTrip.DateOfLeaving);

            response.Status = true;
            response.SignalRModel = this.notificationServices.SendNotifications(new string[] { dbTrip.DriverId });
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

            var passengerToLeave = this.userServices.GetById(userId);

            passengerTrip.IsDeleted = true;
            this.tripRepos.Save();

            // Add 1 available seat to trip
            if (passengerTrip.Approved == true)
            {
                dbTrip.AvailableSeats = dbTrip.AvailableSeats + 1;
            }

            this.notificationServices.Create(
                dbTrip.Id,
                userId,
                dbTrip.Driver.Id,
                NotificationConstants.PassengerLeftTripTitle,
                string.Format(NotificationConstants.PassengerLeftTripMessageFormat, passengerTrip.User.UserName, dbTrip.From.Name, dbTrip.To.Name, dbTrip.DateOfLeaving.ToString("dd/MM/yyyy HH:mm")),
                NotificationKey.PassengerLeftTrip,
                dbTrip.DateOfLeaving);

            response.Status = true;
            response.Data = new LeftTripResponseModel()
            {
                PassengersCount = dbTrip.Passengers.Where(p => p.Approved == true && p.IsDeleted == false).Count(),
                AvailableSeatsCount = dbTrip.AvailableSeats,
                UserName = passengerToLeave.UserName
            };
            response.SignalRModel = this.notificationServices.SendNotifications(new string[] { dbTrip.DriverId });
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

        public bool CheckIfUserIsJoinedTrip(int tripId, string userId)
        {
            var dbTrip = this.GetById(tripId);

            if (dbTrip != null)
            {
                bool userIsJoinedTrip = dbTrip.Passengers
                    .Where(p => p.UserId == userId && p.Approved == true && p.IsDeleted == false)
                    .Any();

                return userIsJoinedTrip;
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
                UserUrl = ServicesDataProvider.GetProfileUrl(author.UserName, author.FirstName, author.LastName),
                UserImageSrc = ServicesDataProvider.GetUserImageSmallUrl(author.Avatar.FileName),
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

            this.notificationServices.Create(
                dbTrip.Id,
                actionUserId,
                passengerTrip.UserId,
                NotificationConstants.TripRequestApprovedTitle,
                string.Format(NotificationConstants.TripRequestApprovedFormat, passengerTrip.User.UserName, dbTrip.From.Name, dbTrip.To.Name, dbTrip.DateOfLeaving.ToString("dd/MM/yyyy HH:mm")),
                NotificationKey.JoinTripApproved,
                dbTrip.DateOfLeaving);

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
            response.SignalRModel = this.notificationServices.SendNotifications(new string[] { dbTrip.DriverId });

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

            this.notificationServices.Create(
                dbTrip.Id,
                actionUserId,
                passengerTrip.UserId,
                NotificationConstants.TripRequestDisaprovedTitle,
                string.Format(NotificationConstants.TripRequestDisaprovedFormat, passengerTrip.User.UserName, dbTrip.From.Name, dbTrip.To.Name, dbTrip.DateOfLeaving.ToString("dd/MM/yyyy HH:mm")),
                NotificationKey.JoinTripDisApproved,
                dbTrip.DateOfLeaving);

            passengerTrip.IsDeleted = true;
            this.tripRepos.Save();

            response.Status = true;
            response.Data = new ApprovedJoinRequestResponseModel()
            {
                PendingApproveUsersCount = dbTrip.Passengers.Where(p => p.Approved == false && p.IsDeleted == false).Count(),
            };
            response.SignalRModel = this.notificationServices.SendNotifications(new string[] { dbTrip.DriverId });

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
                    UserUrl = ServicesDataProvider.GetProfileUrl(c.Author.UserName, c.Author.FirstName, c.Author.LastName),
                    UserImageSrc = ServicesDataProvider.GetUserImageSmallUrl(c.Author.Avatar.FileName),
                    CreatedOnFormatted = c.CreatedOn.ToString("dd.MM.yyyy HH:mm"),
                    CommentText = c.Text
                })
                .ToList();

            if (comments.Count() > 0)
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

        public IQueryable<Trip> GetDynamicFIltered(
            int fromId,
            int toId,
            int availableSeaets,
            DateTime dateOfLeaving,
            string driverUsername,
            decimal priceMin,
            decimal priceMax,
            string sortBy,
            string orderBy)
        {
            if (string.IsNullOrEmpty(sortBy) || !WebApplicationConstants.OrderTripOptions.ContainsKey(sortBy))
            {
                sortBy = DefaultOrder;
            }

            if (string.IsNullOrEmpty(orderBy) || (orderBy.ToUpper() != "ASC" && orderBy.ToUpper() != "DESC"))
            {
                orderBy = DefaultSortDirection;
            }

            orderBy = orderBy.ToUpper();

            var query = this.GetBaseFIlterQuery(fromId, toId, availableSeaets, dateOfLeaving, driverUsername, priceMin, priceMax);

            switch (sortBy)
            {
                case "Driver":
                    if (orderBy == "ASC")
                    {
                        query = query.OrderBy(t => t.Driver.UserName);
                    }
                    else
                    {
                        query = query.OrderByDescending(t => t.Driver.UserName);
                    }

                    break;
                default:
                    query = query.OrderBy(sortBy + " " + orderBy);
                    break;
            }

            return query;
        }

        private IQueryable<Trip> GetBaseFIlterQuery(
            int fromId,
            int toId,
            int availableSeaets,
            DateTime dateOfLeaving,
            string driverUsername,
            decimal priceMin,
            decimal priceMax)
        {
            if (string.IsNullOrEmpty(driverUsername))
            {
                driverUsername = string.Empty;
            }

            var filteredTrips = this.tripRepos
                .All()
                .Where(t => DbFunctions.TruncateTime(t.DateOfLeaving) == dateOfLeaving
                            && t.FromId == fromId
                            && t.ToId == toId
                            && t.Status == TripStatus.Open
                            && t.AvailableSeats >= availableSeaets
                            && t.Price >= priceMin && t.Price <= priceMax
                            && t.Driver.UserName.Contains(driverUsername));

            return filteredTrips;
        }
    }
}
