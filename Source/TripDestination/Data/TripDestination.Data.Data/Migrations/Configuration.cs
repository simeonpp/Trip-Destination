namespace TripDestination.Data.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using TripDestination.Common.Infrastructure.Constants;

    public sealed class Configuration : DbMigrationsConfiguration<TripDestinationDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(TripDestinationDbContext context)
        {
            this.SeedRolesAndAdmins(context);
            this.SeedNotificationTypes(context);
            this.SeedTowns(context);
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

                var avatar = new Photo()
                {
                    ContentType = "image/jpeg",
                    Extension = ".jpg",
                    OriginalName = "adminImage",
                    SizeInBytes = 123123,
                    CreatedOn = DateTime.Now,
                    FileName = "admin_image_image_iamge_image"
                };

                var adminUser = new User
                {
                    UserName = UserConstants.AdminUsername,
                    Email = UserConstants.AdminEmail,
                    FirstName = UserConstants.AdminFirstName,
                    LastName = UserConstants.AdminlastName,
                    Avatar = avatar
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
                string townsFilePath = basePath + @"\files\cities.csv";

                if (File.Exists(townsFilePath))
                {
                    var towns = File.ReadAllLines(townsFilePath);

                    foreach (string town in towns)
                    {
                        if (!String.IsNullOrEmpty(town))
                        {
                            Town dbTownToBeSaved = new Town
                            {
                                Name = town.Trim()
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
