namespace TripDestination.Services.Web.Providers
{
    using Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    class TripProvider : ITripProvider
    {
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
    }
}
