namespace TripDestination.Services.Web.Providers.Contracts
{
    using System.Web.Mvc;
    using System.Collections.Generic;
    using TripDestination.Data.Models;

    public interface ITripProvider
    {
        IEnumerable<SelectListItem> GetAvailableSeatsSelectList();

        IEnumerable<SelectListItem> GetAddressPickUpSelectList();

        IEnumerable<SelectListItem> GetLuggageSpcaceSelectList();

        IEnumerable<SelectListItem> GetTripsPerPageSelectList();

        IEnumerable<SelectListItem> GetOrderBySelectList(string selectedValue);

        IEnumerable<SelectListItem> GetleftAvailableSeatsSelectList(Trip trip);

        bool UserCanRateTrip(Trip trip, string userId);

        int GetValidRate(int rate);
    }
}
