namespace TripDestination.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TripDestination.Data.Models;
    using Common.Infrastructure.Models;

    public interface ITripServices
    {
        IQueryable<Trip> GetAll();

        IQueryable<Trip> GetDynamicFIltered(
            int fromId,
            int toId,
            int availableSeaets,
            DateTime dateOfLeaving,
            string driverUsername,
            decimal priceMin,
            decimal priceMax,
            string sortBy,
            string orderBy);

        Trip Create(
            int fromTownId,
            int toTownId,
            DateTime dateOfLeaving,
            int availableSeats,
            string placeOfLeaving,
            bool pickUpFromAddress,
            string description,
            DateTime eta,
            decimal price,
            string driverId);

        Trip GetById(int id);

        Trip GetByIdWithStatusCheck(int id);

        IQueryable<Trip> GetForDay(DateTime day);

        IQueryable<Trip> GetLatest(int count);

        IQueryable<Trip> GetTodayTrips(int count);

        IQueryable<string> GetTopTownsDestination(bool townsTo = true, int count = 2);

        IQueryable<PassengerTrip> GetPassengers(Trip trip);

        BaseResponseAjaxModel Edit(
            int tripId,
            DateTime dateOfLeaving,
            int leftAvailableSeats,
            string placeOfLeaving,
            bool pickUpFromAddress,
            string description,
            DateTime eta,
            IEnumerable<string> usernamesToBeRemoved);

        Trip AdminEdit(
            int tripId,
            int leftAvailableSeats,
            string placeOfLeaving,
            bool pickUpFromAddress,
            string description);

        void Delete(int id, string userId);

        void AdminDelete(int id);

        int GetTakenSeatsCount(int tripId);

        BaseResponseAjaxModel JoinRequest(int tripId, string userId);

        BaseResponseAjaxModel LeaveTrip(int tripId, string userId);

        bool CheckIfUserHasPendingRequest(int tripId, string userId);

        bool CheckIfUserIsJoinedTrip(int tripId, string userId);

        bool CheckIfTripHasMoreCommentsToLoad(int tripId, int currentLoadedComments);

        BaseResponseAjaxModel AddComment(int tripId, string userId, string commentText);

        BaseResponseAjaxModel ApproveJoinRequest(int tripId, string username, string actionUserId);

        BaseResponseAjaxModel DisapproveJoinRequest(int tripId, string username, string actionUserId);

        BaseResponseAjaxModel LoadComments(int tripId, int offset);

        bool CheckIfUserLikedTrip(Trip trip, string userId);

        BaseResponseAjaxModel LikeDislike(int tripId, string userId, bool value);

        int GetLikesCount(Trip trip);

        BaseSignalRModel NotifyTripPassengersAndDriverForTripFinish(Trip trip, string userId);
    }
}
