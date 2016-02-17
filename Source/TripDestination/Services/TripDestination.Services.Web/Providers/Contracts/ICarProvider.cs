namespace TripDestination.Services.Web.Providers.Contracts
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public interface ICarProvider
    {
        IEnumerable<SelectListItem> GetAvailableSeatsSelectList();

        IEnumerable<SelectListItem> GetAvailableLuggageSelectList();
    }
}
