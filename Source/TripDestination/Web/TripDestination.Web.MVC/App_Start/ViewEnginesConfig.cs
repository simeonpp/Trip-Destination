namespace TripDestination.Web.MVC
{
    using System.Web.Mvc;

    public static class ViewEnginesConfig
    {
        public static void Register(ViewEngineCollection engines)
        {
            engines.Clear();
            engines.Add(new RazorViewEngine());
        }
    }
}