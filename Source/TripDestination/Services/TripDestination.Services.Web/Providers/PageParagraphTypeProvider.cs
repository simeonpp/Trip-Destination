namespace TripDestination.Services.Web.Providers
{
    using Contracts;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using TripDestination.Data.Models;
    public class PageParagraphTypeProvider : IPageParagraphTypeProvider
    {
        public IEnumerable<SelectListItem> GetPagePargraphTypes()
        {
            List<SelectListItem> pageParagraphSelectList = new List<SelectListItem>();

            foreach (var type in Enum.GetValues(typeof(PageParagraphType)))
            {
                pageParagraphSelectList.Add(new SelectListItem { Text = type.ToString(), Value = 2.ToString() });
            }

            return pageParagraphSelectList;
        }
    }
}
