namespace TripDestination.Tests.Services.Web
{
    using System.Linq;
    using System.Collections.Generic;
    using Moq;
    using NUnit.Framework;
    using TripDestination.Services.Data.Contracts;
    using TripDestination.Services.Web.Helpers.Contracts;
    using TripDestination.Services.Web.Helpers;
    using TripDestination.Services.Web.Providers.Contracts;
    using TripDestination.Services.Web.Providers;
    using System;
    [TestFixture]
    public class DateProviderTests
    {
        [Test]
        public void GetWeekAhedDaysShoudlReturnCorrectData()
        {
            var day = DateTime.Now;
            IDateProvider dateProvider = new DateProvider();
            var actual = dateProvider.GetWeekAhedDays(day);

            Assert.AreEqual(day.DayOfYear, actual.FirstOrDefault().Datetime.DayOfYear);
            Assert.AreEqual(day.Year, actual.FirstOrDefault().Datetime.Year);
            Assert.AreEqual(day.AddDays(6).DayOfYear, actual.ElementAt(6).Datetime.DayOfYear);
            Assert.AreEqual(day.AddDays(6).Year, actual.ElementAt(6).Datetime.Year);
        }

        [Test]
        public void CovertDateFromStringToDateTimeShouldReturnCorrectResult()
        {
            string date = "2016-1-1";
            IDateProvider dateProvider = new DateProvider();
            var actual = dateProvider.CovertDateFromStringToDateTime(date);

            Assert.AreEqual(new DateTime(2016, 1, 1).DayOfYear, actual.DayOfYear);
            Assert.AreEqual(new DateTime(2016, 1, 1).Year, actual.Year);
        }
    }
}
