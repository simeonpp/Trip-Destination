namespace TripDestination.Services.Web.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Contracts;
    using Common.Infrastructure.Constants;

    public class RoleProvider : IRoleProvider
    {
        public IEnumerable<SelectListItem> GetPublicUserRoles()
        {
            var publicRoles = new List<SelectListItem>()
            {
                new SelectListItem() { Text = RoleConstants.PassengerRole, Value = RoleConstants.PassengerRole, Selected = true },
                new SelectListItem() { Text = RoleConstants.DriverRole, Value = RoleConstants.DriverRole }
            };

            return publicRoles;
        }
    }
}
