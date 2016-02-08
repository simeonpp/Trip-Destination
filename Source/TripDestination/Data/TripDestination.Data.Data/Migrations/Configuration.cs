namespace TripDestination.Data.Data.Migrations
{
    using Common.Infrastructure.Constants;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    public sealed class Configuration : DbMigrationsConfiguration<TripDestinationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(TripDestinationDbContext context)
        {
            SeedRolesAndAdmins(context);
            SeedNotificationTypes(context);
            SeedTowns(context);
        }

        private void SeedRolesAndAdmins(TripDestinationDbContext context)
        {
            // Roles
            if (!context.Roles.Any())
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                if (!context.Roles.Any(r => r.Name == RoleConstants.AdminRole))
                {
                    var roleAdmin = new IdentityRole { Name = RoleConstants.AdminRole };
                    roleManager.Create(roleAdmin);
                }

                if (!context.Roles.Any(r => r.Name == RoleConstants.DriverRole))
                {
                    var roleAdmin = new IdentityRole { Name = RoleConstants.DriverRole };
                    roleManager.Create(roleAdmin);
                }

                if (!context.Roles.Any(r => r.Name == RoleConstants.PassengerRole))
                {
                    var roleAdmin = new IdentityRole { Name = RoleConstants.PassengerRole };
                    roleManager.Create(roleAdmin);
                }
            }

            // Create admin
            if (!context.Users.Any(u => u.UserName == UserConstants.AdminUsername))
            {
                var userStore = new UserStore<User>(context);
                var userManager = new UserManager<User>(userStore);

                userManager.PasswordValidator = new PasswordValidator
                {
                    RequiredLength = UserConstants.RequiredLength,
                    RequireNonLetterOrDigit = UserConstants.RequireNonLetterOrDigit,
                    RequireDigit = UserConstants.RequireDigit,
                    RequireLowercase = UserConstants.RequireLowercase,
                    RequireUppercase = UserConstants.RequireUppercase,
                };

                var adminUser = new User
                {
                    UserName = UserConstants.AdminUsername,
                    Email = UserConstants.AdminEmail,
                    FirstName = UserConstants.AdminFirstName,
                    LastName = UserConstants.AdminlastName
                };

                IdentityResult result = userManager.Create(adminUser, UserConstants.AdminPassword);
                if (result.Succeeded == false)
                {
                    throw new Exception(result.Errors.First());
                }

                userManager.AddToRole(adminUser.Id, RoleConstants.AdminRole);
            }
        }

        private void SeedNotificationTypes(TripDestinationDbContext context)
        {
            if (!context.NotificationTypes.Any())
            {
                var upperCaseReplaceExpression = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);

                foreach (NotificationKey key in Enum.GetValues(typeof(NotificationKey)))
                {
                    string description = upperCaseReplaceExpression.Replace(key.ToString(), " ");
                    NotificationType notificationType = new NotificationType
                    {
                        Key = key,
                        Description = description
                    };

                    context.NotificationTypes.Add(notificationType);
                }

                context.SaveChanges();
            }

        }

        private void SeedTowns(TripDestinationDbContext context)
        {
            if (!context.Towns.Any())
            {
                var appDomain = AppDomain.CurrentDomain;
                var basePath = appDomain.RelativeSearchPath ?? appDomain.BaseDirectory;
                string townsFilePath = Path.GetFullPath(Path.Combine(basePath, @"..\..\files\cities.csv"));

                if (File.Exists(townsFilePath))
                {
                    var towns = File.ReadAllLines(townsFilePath);

                    foreach (string town in towns)
                    {
                        if (!String.IsNullOrEmpty(town))
                        {
                            Town dbTownToBeSaved = new Town
                            {
                                Name = town
                            };

                            context.Towns.Add(dbTownToBeSaved);
                        }
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}
