namespace TripDestination.Services.Web.Providers
{
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
            var tripsPerPageSelectList = new List<SelectListItem>
            {
                new SelectListItem { Text = "3", Value = "3" },
                new SelectListItem { Text = "6", Value = "6" },
                new SelectListItem { Text = "9", Value = "9", Selected = true },
                new SelectListItem { Text = "12", Value = "12" },
                new SelectListItem { Text = "15", Value = "15" },
                new SelectListItem { Text = "18", Value = "18" },
                new SelectListItem { Text = "21", Value = "21" },
                new SelectListItem { Text = "24", Value = "24" }
            };

            return tripsPerPageSelectList;
        }

        public IEnumerable<SelectListItem> GetOrderBySelectList()
        {
            var oserBySelectList = new List<SelectListItem>
            {
                new SelectListItem { Text = "Order by date of leaving", Value = "dateOfLeaving" },
                new SelectListItem { Text = "Order by travel destination", Value = "to" },
                new SelectListItem { Text = "Order by leaving from", Value = "from" },
                new SelectListItem { Text = "Order by available seats", Value = "seats" },
                new SelectListItem { Text = "Order by price", Value = "price" },
                new SelectListItem { Text = "Order by driver name", Value = "driver" }
            };

            return oserBySelectList;
        }

        public IEnumerable<SelectListItem> GetleftAvailableSeatsSelectList(int tripId)
        {
            int availableLeftSeatsCount = this.tripServices.GetAvailableLeftSeatsCount(tripId);

            var leftAvailableSeatsSelectList = new List<SelectListItem>();

            for (int i = availableLeftSeatsCount; i > 0; i--)
            {
                leftAvailableSeatsSelectList.Add(new SelectListItem()
                {
                    Text = i.ToString(),
                    Value = i.ToString()
                });
            }

            return leftAvailableSeatsSelectList;
        }
    }
}
