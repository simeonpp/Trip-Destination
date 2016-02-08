namespace TripDestination.Web.MVC
{
    using Data.Data;
    using Data.Data.Migrations;
    using System.Data.Entity;

    public class DatabaseConfig
    {
        public static void Register()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TripDestinationDbContext, Configuration>());
        }
    }
}