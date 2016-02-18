namespace TripDestination.Common.Infrastructure.Constants
{
    using System;

    public class ModelConstants
    {
        // Common
        public const string EmailRegEx = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
         @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
         @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        public const int IpMinLength = 3;  // in order to store local ip, just for testing, otherwise make this value to 11
        public const int IpMaxLength = 45;

        // User
        public const int UserFirstNameMinLength = 3;
        public const int UserFirstNameMaxLength = 50;

        public const int UserLastNameMinLength = 3;
        public const int UserLastNameMaxLength = 50;

        public const int UserDescriptionMinLength = 20;
        public const int UserDescriptionMaxLength = 1000;

        // Car
        public const int CarTotalSeatsMinLength = 1;
        public const int CarTotalSeatsMaxLength = 5;

        public const int CarBrandMinLength = 3;
        public const int CarBrandMaxLength = 50;

        public const int CarModelMinLength = 2;
        public const int CarModelMaxLength = 50;

        public const int CarColorMinLength = 3;
        public const int CarColorMaxLength = 50;

        public const int CarYearMinYear = 1900;
        public const int CarYearMaxYear = 2016;

        public const int CarDescriptionMinLength = 20;
        public const int CarDescriptionMaxLength = 1000;

        // Town
        public const int TownNameMinLength = 3;
        public const int TownNameMaxLength = 50;

        // Newsletter
        public const int NewsletterEmailMinLength = 6;
        public const int NewsletterEmailMaxLength = 100;

        public const int NewsletterUserAgenMinLength = 10;
        public const int NewsletterUserAgentMaxLength = 2000;

        // Trip
        public const int TripDescriptionMinLength = 10;
        public const int TripDescriptionMaxLength = 1000;

        public const int TripAvailableSeatsMin = 0;
        public const int TripAvailableSeatsMax = 5;

        public const int TripPlaceOfLeavingMinLength = 5;
        public const int TripPlaceOfLeavingMaxLength = 200;

        // Comment
        public const int CommentTextMinLength = 5;
        public const int CommenttextMaxLength = 1000;

        // Rating
        public const double RatingValueMin = 0.0;
        public const double RatingValueMax = 5.0;

        // Notification
        public const int NotificationTitleMinLength = 5;
        public const int NotificationTitleMaxLength = 150;

        public const int NotificationMessageMinLength = 10;
        public const int NotificationMessageMaxLength = 500;

        // Notification type
        public const int NotificationTypeDescriptionMaxLength = 500;

        // Page
        public const int PageHeadingMinLength = 5;
        public const int PageHeadingMaxLength = 50;

        public const int PageSubHeadingMinLength = 5;
        public const int PageSubHeadingMaxLength = 50;

        // Page paragraph
        public const int PageParagraphMainHeadingMinLength = 5;
        public const int PageParagraphMainHeadingMaxLength = 50;

        public const int PageParagraphMainSubHeadingMinLength = 5;
        public const int PageParagraphMainSubHeadingMaxLength = 150;

        public const int PageParagraphHeadingMinLength = 5;
        public const int PageParagraphHeadingMaxLength = 150;

        public const int PageParagraphTextMinLength = 10;
        public const int PageParagraphTextMaxLength = 10000;

        // Contact form
        public const int ContactFormNameMinLength = 4;
        public const int ContactFormNameMaxLength = 50;

        public const int ContactFormEmailMaxLength = 100;

        public const int ContactFormSubjectMinLength = 4;
        public const int ContactFormSubjectMaxLength = 50;

        public const int ContactFormMessageMinLength = 10;
        public const int ContactFormMessageMaxLength = 500;
    }
}
