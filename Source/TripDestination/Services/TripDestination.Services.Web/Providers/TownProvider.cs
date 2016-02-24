namespace TripDestination.Services.Web.Providers
{
    using Contracts;
    using Data.Contracts;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Linq;
    using Services.Contracts;
    using Common.Infrastructure.Constants;
    public class TownProvider : ITownProvider
    {
        public TownProvider(ITownsServices townServices, ICacheServices CacheServices)
        {
            this.TownServices = townServices;
            this.CacheServices = CacheServices;
        }

        public ITownsServices TownServices { get; set; }

        public ICacheServices CacheServices { get; set; }

        public IEnumerable<SelectListItem> GetTowns()
        {
            var towns = this.CacheServices.Get(
                "towns",
                () => this.TownServices
                    .GetAll()
                    .Select(t => new SelectListItem
                    {
                        Value = t.Id.ToString(),
                        Text = t.Name
                    })
                    .ToList(),
                CacheTimeConstants.Towns);

            return towns;
        }
    }
}
