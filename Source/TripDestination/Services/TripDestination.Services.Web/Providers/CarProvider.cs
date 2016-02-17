namespace TripDestination.Services.Web.Providers
{
    using Common.Infrastructure.Constants;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using TripDestination.Data.Models;
    using TripDestination.Services.Web.Providers.Contracts;

    public class CarProvider : ICarProvider
    {
        public IEnumerable<SelectListItem> GetAvailableSeatsSelectList()
        {
            var availableSeats = new List<SelectListItem>();

            for (int i = ModelConstants.CarTotalSeatsMinLength; i <= ModelConstants.CarTotalSeatsMaxLength; i++)
            {
                availableSeats.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });
            }

            return availableSeats;
        }

        public IEnumerable<SelectListItem> GetAvailableLuggageSelectList()
        {
            var availableLuggages = new List<SelectListItem>();

            foreach (var luggage in Enum.GetValues(typeof(SpaceForLugage)))
            {
                availableLuggages.Add(new SelectListItem { Value = luggage.ToString(), Text = luggage.ToString() });
            }

            return availableLuggages;
        }
    }
}
