namespace TripDestination.Services.Web.Providers.Contracts
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public interface IRoleProvider
    {
        IEnumerable<SelectListItem> GetPublicUserRolesSelectList();
    }
}
