namespace TripDestination.Tests.Web.MVC.Controllers
{
    using Common.Infrastructure.Mapping;
    using NUnit.Framework;
    using TripDestination.Web.MVC.Controllers;
    using Moq;
    using Services.Data.Contracts;
    using Data.Models;
    using System.Linq;
    using System.Collections.Generic;
    using TestStack.FluentMVCTesting;
    using TripDestination.Web.MVC.ViewModels.Page;

    [TestFixture]
    public class ContactControllerTests
    {
        private ContactController ContactController;

        [SetUp]
        public void Init()
        {
            AutoMapperConfig.Register(typeof(PageController).Assembly);

            var contactFormServices = new Mock<IContactFormServices>();
            //contactFormServices.Setup(x => x.Create())

            var contactController = new ContactController(contactFormServices.Object);

            contactController.MapperConfiguration = AutoMapperConfig.Configuration;
            this.ContactController = contactController;
        }

        [Test]
        public void IndexhouldRenderCorrectView()
        {
            this.ContactController.WithCallTo(x => x.Index())
                .ShouldRenderView("Index");
        }
    }
}
