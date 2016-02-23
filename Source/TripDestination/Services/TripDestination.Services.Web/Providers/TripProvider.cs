namespace TripDestination.Services.Web.Providers
{
    using Common.Infrastructure.Constants;
    using Contracts;
    using Data.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using TripDestination.Data.Models;

    public class TripProvider : ITripProvider
    {
        private readonly ITripServices tripServices;

        public TripProvider(ITripServices tripServices)
        {
            this.tripServices = tripServices;
        }

        public IEnumerable<SelectListItem> GetAvailableSeatsSelectList()
        {
            var availableSeatsSelectList = new List<SelectListItem>
            {
                new SelectListItem { Text = "1", Value = "1" },
                new SelectListItem { Text = "2", Value = "2" },
                new SelectListItem { Text = "3", Value = "3" }
            };

            return availableSeatsSelectList;
        }

        public IEnumerable<SelectListItem> GetAddressPickUpSelectList()
        {
            var addressPickUpSelectList = new List<SelectListItem>
            {
                new SelectListItem { Text = "Yes", Value = "true" },
                new SelectListItem { Text = "No", Value = "false" }
            };

            return addressPickUpSelectList;
        }

        public IEnumerable<SelectListItem> GetLuggageSpcaceSelectList()
        {
            var luggageSpaceSelectList = new List<SelectListItem>();

            foreach (var luggageSpace in Enum.GetValues(typeof(SpaceForLugage)))
            {
                luggageSpaceSelectList.Add(new SelectListItem()
                {
                    Text = luggageSpace.ToString(),
                    Value = luggageSpace.ToString()
                });
            }

            return luggageSpaceSelectList;
        }

        public IEnumerable<SelectListItem> GetTripsPerPageSelectList()
        {
            var tripsPerPageSelectList = new List<SelectListItem>();
            for (int i = 3; i <= WebApplicationConstants.MaxItemsPerPage; i += 3)
            {
                var item = new SelectListItem { Text = i.ToString(), Value = i.ToString() };
                if (i == 9)
                {
                    item.Selected = true;
                }

                tripsPerPageSelectList.Add(item);
            }

            return tripsPerPageSelectList;
        }

        public IEnumerable<SelectListItem> GetOrderBySelectList(string selectedValue)
        {
            var oserBySelectList = new List<SelectListItem>();
            foreach (var orderOption in WebApplicationConstants.OrderTripOptions)
            {
                SelectListItem currentSelectListItem = new SelectListItem { Text = orderOption.Value, Value = orderOption.Key };
                if (selectedValue != null && orderOption.Key.ToLower() == selectedValue.ToLower())
                {
                    currentSelectListItem.Selected = true;
                }

                oserBySelectList.Add(currentSelectListItem);
            }

            return oserBySelectList;
        }

        public IEnumerable<SelectListItem> GetleftAvailableSeatsSelectList(Trip trip)
        {
            int carSeats = trip.Driver.Car.TotalSeats;
            int availableLeftSeatsCount = this.tripServices.GetTakenSeatsCount(trip.Id);
            int maxAvailableLeftSeats = carSeats - availableLeftSeatsCount;

            var leftAvailableSeatsSelectList = new List<SelectListItem>();

            for (int i = 0; i <= maxAvailableLeftSeats; i++)
            {
                leftAvailableSeatsSelectList.Add(new SelectListItem()
                {
                    Text = i.ToString(),
                    Value = i.ToString()
                });
            }

            return leftAvailableSeatsSelectList;
        }

        public bool UserCanRateTrip(Trip trip, string userId)
        {
            bool result = trip.Status == TripStatus.Finished
                && (trip.DriverId == userId || trip.Passengers.Any(p => p.UserId == userId))
                && !trip.Ratings.Any(r => r.FromUserId == userId);
            return result;
        }
    }
}
