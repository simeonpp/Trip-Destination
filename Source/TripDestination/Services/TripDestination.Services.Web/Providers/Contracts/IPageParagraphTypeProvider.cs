namespace TripDestination.Services.Web.Providers.Contracts
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public interface IPageParagraphTypeProvider
    {
        IEnumerable<SelectListItem> GetPagePargraphTypes();
    }
}
