namespace TripDestination.Tests.Services.Web
{
    using System.Linq;
    using System.Collections.Generic;
    using Moq;
    using NUnit.Framework;
    using TripDestination.Services.Data.Contracts;
    using TripDestination.Services.Web.Helpers.Contracts;
    using TripDestination.Services.Web.Helpers;

    [TestFixture]
    public class TripHelperTests
    {
        [Test]
        public void GetTopDestinationsShouldReturnsFourTuplesIfEnoughtTripsAreCreated()
        {
            var tripServicesMock = new Mock<ITripServices>();
            tripServicesMock.Setup(x => x.GetTopTownsDestination(true, It.IsAny<int>()))
                .Returns(new List<string>() { "Sofia", "Burgas" }.AsQueryable());
            tripServicesMock.Setup(x => x.GetTopTownsDestination(false, It.IsAny<int>()))
                .Returns(new List<string>() { "Sofia", "Burgas", "Plovdiv" }.AsQueryable());

            ITripHelper tripHelper = new TripHelper(tripServicesMock.Object);
            var actual = tripHelper.GetTopDestinations();
            Assert.AreEqual(4, actual.Count());
        }

        [Test]
        public void GetTopDestinationsShouldNotHaveDuplicates()
        {
            var tripServicesMock = new Mock<ITripServices>();
            tripServicesMock.Setup(x => x.GetTopTownsDestination(true, It.IsAny<int>()))
                .Returns(new List<string>() { "Sofia", "Burgas" }.AsQueryable());
            tripServicesMock.Setup(x => x.GetTopTownsDestination(false, It.IsAny<int>()))
                .Returns(new List<string>() { "Sofia", "Burgas", "Plovdiv" }.AsQueryable());

            ITripHelper tripHelper = new TripHelper(tripServicesMock.Object);
            var actual = tripHelper.GetTopDestinations();

            Assert.AreEqual("Sofia", actual.First().Item1);
            Assert.AreEqual("Burgas", actual.First().Item2);

            Assert.AreEqual("Sofia", actual.ElementAt(1).Item1);
            Assert.AreEqual("Plovdiv", actual.ElementAt(1).Item2);

            Assert.AreEqual("Burgas", actual.ElementAt(2).Item1);
            Assert.AreEqual("Sofia", actual.ElementAt(2).Item2);

            Assert.AreEqual("Burgas", actual.ElementAt(3).Item1);
            Assert.AreEqual("Plovdiv", actual.ElementAt(3).Item2);
        }
    }
}
