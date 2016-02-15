namespace TripDestination.Services.Web.Providers.Contracts
{
    using System.Web.Mvc;
    using System.Collections.Generic;

    public interface ITripProvider
    {
        IEnumerable<SelectListItem> GetAvailableSeatsSelectList();

        IEnumerable<SelectListItem> GetAddressPickUpSelectList();

        IEnumerable<SelectListItem> GetLuggageSpcaceSelectList();

        IEnumerable<SelectListItem> GetTripsPerPageSelectList();

        IEnumerable<SelectListItem> GetOrderBySelectList();
    }
}
