namespace TripDestination.Services.Data
{
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
    using System;

    public class UserServices : IUserServices
    {
        private readonly IDbGenericRepository<User, string> userRepos;

        public UserServices(IDbGenericRepository<User, string> userRepos)
        {
            this.userRepos = userRepos;
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
            var usermanager = new UserManager<User>(new UserStore<User>(new TripDestinationDbContext()));
            var roles = usermanager.GetRoles(id);
            return roles.ToArray();
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
                UserUrl = "www.google.com", // TODO: Implement URL
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
                    UserUrl = "http://www.google.com", // TODO: Fix url
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
    }
}
