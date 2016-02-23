namespace TripDestination.Services.Data
{
    using System;
    using Common.Infrastructure.Constants;
    using Common.Infrastructure.Models;
    using Contracts;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System.Linq;
    using TripDestination.Data.Common;
    using TripDestination.Data.Data;
    using TripDestination.Data.Models;

    public class UserServices : IUserServices
    {
        private readonly IDbGenericRepository<User, string> userRepos;

        private readonly UserManager<User> userManager;

        private readonly TripDestinationDbContext dbContext;

        private readonly IDbRepository<UserComment> userCommentRepos;

        public UserServices(IDbGenericRepository<User, string> userRepos, IDbRepository<UserComment> userCommentRepos)
        {
            this.userRepos = userRepos;
            this.dbContext = new TripDestinationDbContext();
            this.userManager = new UserManager<User>(new UserStore<User>(this.dbContext));
            this.userCommentRepos = userCommentRepos;
        }

        public User GetById(string id)
        {
            var user = this.userRepos.All()
                .Where(u => u.Id == id)
                .FirstOrDefault();

            return user;
        }

        public User GetByUsername(string username)
        {
            var user = this.userRepos.All()
                .Where(u => u.UserName == username)
                .FirstOrDefault();

            return user;
        }

        public string[] GetUserRoles(string id)
        {
            var roles = this.userManager.GetRoles(id);
            return roles.ToArray();
        }

        public int GetUsersCountInRole(string role)
        {
            var roleStore = new RoleStore<IdentityRole>(this.dbContext);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            int count = roleManager.FindByName(role).Users.Count;
            return count;
        }

        public BaseResponseAjaxModel AddComment(string userId, string fromUserId, string commentText)
        {
            var response = new BaseResponseAjaxModel();

            if (commentText.Length < ModelConstants.CommentTextMinLength || commentText.Length > ModelConstants.CommentTextMaxLength)
            {
                response.ErrorMessage = string.Format("Comment text should be between {0} and {1} symbols long", ModelConstants.CommentTextMinLength, ModelConstants.CommentTextMaxLength);
                return response;
            }

            var dbUser = this.GetById(userId);

            if (dbUser == null)
            {
                response.ErrorMessage = "No such User";
                return response;
            }

            UserComment comment = new UserComment()
            {
                UserId = userId,
                AuthorId = fromUserId,
                Text = commentText
            };

            dbUser.Comments.Add(comment);
            this.userRepos.Save();
            this.userRepos.Reload(dbUser);

            var author = this.GetById(fromUserId);

            response.Status = true;
            response.Data = new CommentResponseModel()
            {
                FirstName = author.FirstName,
                LastName = author.LastName,
                UserUrl = ServicesDataProvider.GetProfileUrl(author.UserName, author.FirstName, author.LastName),
                UserImageSrc = ServicesDataProvider.GetUserImageSmallUrl(author.Avatar.FileName),
                CreatedOnFormatted = comment.CreatedOn.ToString("dd.MM.yyyy HH:mm"),
                CommentText = comment.Text,
                CommentTotalCount = dbUser.Comments.Count
            };

            return response;
        }

        public BaseResponseAjaxModel LoadComments(string userId, int offset)
        {
            var dbUser = this.GetById(userId);
            var response = new BaseResponseAjaxModel();

            if (dbUser == null)
            {
                response.ErrorMessage = "No such User.";
                return response;
            }

            var comments = dbUser.Comments
                .Where(c => c.IsDeleted == false)
                .OrderByDescending(c => c.CreatedOn)
                .Skip(offset)
                .Take(WebApplicationConstants.CommentsOfset)
                .Select(c => new CommentResponseModel()
                {
                    FirstName = c.Author.FirstName,
                    LastName = c.Author.LastName,
                    UserUrl = ServicesDataProvider.GetProfileUrl(c.Author.UserName, c.Author.FirstName, c.Author.LastName),
                    UserImageSrc = ServicesDataProvider.GetUserImageSmallUrl(c.Author.Avatar.FileName),
                    CreatedOnFormatted = c.CreatedOn.ToString("dd.MM.yyyy HH:mm"),
                    CommentText = c.Text
                })
                .ToList();

            if (comments.Count() > 0)
            {
                int newOffset = offset + WebApplicationConstants.CommentsOfset;
                bool hasMoreCommentsToLoad = this.CheckIfTripHasMoreCommentsToLoad(dbUser.Id, newOffset);

                response.Status = true;
                response.Data = new LoadedCommentsResponseModel()
                {
                    HasMoreCommentsToLoad = hasMoreCommentsToLoad,
                    Offset = newOffset,
                    Comments = comments
                };
            }

            return response;
        }

        public bool CheckIfTripHasMoreCommentsToLoad(string userId, int currentLoadedComments)
        {
            var dbUser = this.GetById(userId);

            if (dbUser != null)
            {
                var result = (dbUser.Comments.Where(c => c.IsDeleted == false).Count() - currentLoadedComments) > 0 ? true : false;
                return result;
            }

            return false;
        }

        public User Update(string userId, string email, string firstName, string lastName, string phoneNumber, string description)
        {
            var dbUser = this.GetById(userId);

            if (dbUser == null)
            {
                throw new Exception("User not found.");
            }

            dbUser.Email = email;
            dbUser.FirstName = firstName;
            dbUser.LastName = lastName;
            dbUser.PhoneNumber = phoneNumber;
            dbUser.Description = description;

            this.userRepos.Save();
            return dbUser;
        }

        public IQueryable<UserComment> GetComments(string userId)
        {
            var comments = this.userCommentRepos
                .All()
                .Where(c => c.UserId == userId && c.IsDeleted == false)
                .OrderByDescending(c => c.CreatedOn)
                .Take(WebApplicationConstants.CommentsOfset);

            return comments;
        }

        public int GetTotatlComments(string userId)
        {
            int commentForUserCount = this.userCommentRepos
                .All()
                .Where(c => c.UserId == userId && c.IsDeleted == false)
                .OrderByDescending(c => c.CreatedOn)
                .Count();

            return commentForUserCount;
        }

        public IQueryable<User> GetAll()
        {
            return this.userRepos.All();
        }

        public User CreateAdmin(string username, string email, string password, string firstName, string lastName)
        {
            var user = new User
            {
                UserName = username,
                Email = email,
                FirstName = firstName,
                LastName = lastName
            };

            var userManager = new UserManager<User>(new UserStore<User>(new TripDestinationDbContext()));
            userManager.Create(user, password);
            userManager.AddToRole(user.Id, RoleConstants.AdminRole);
            return user;
        }

        public void Delete(string userID)
        {
            var user = this.GetById(userID);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            this.userRepos.Delete(user);
            this.userRepos.Save();
        }

        public User Edit(string userId, string email, string firstName, string lastName)
        {
            var user = this.GetById(userId);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            user.Email = email;
            user.FirstName = firstName;
            user.LastName = lastName;
            return user;
        }
    }
}
