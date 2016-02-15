namespace TripDestination.Services.Web.Providers
{
    using Contracts;
    using Data.Contracts;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Linq;

    public class TownProvider : ITownProvider
    {
        public TownProvider(ITownsServices townServices)
        {
            this.TownServices = townServices;
        }

        public ITownsServices TownServices { get; set; }

        public IEnumerable<SelectListItem> GetTowns()
        {
            var towns = this.TownServices
                    .GetAll()
                    .Select(t => new SelectListItem
                    {
                        Value = t.Id.ToString(),
                        Text = t.Name
                    })
                    .ToList();

            return towns;
        }
    }
}
