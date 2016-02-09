namespace TripDestination.Web.MVC
{
    using System.Web.Mvc;

    public static class ViewEnginesConfig
    {
        public static void Register(ViewEngineCollection Engines)
        {
            Engines.Clear();
            Engines.Add(new RazorViewEngine());
        }
    }
}