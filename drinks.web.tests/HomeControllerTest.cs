using System;
using System.Web.Mvc;
using drinks.infrastructure;
using drinks.infrastructure.Request;
using drinks.web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace drinks.web.tests
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void IndexViewResultNotNull()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void IndexViewEqualIndexCshtml()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Index() as ViewResult;

            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void Test()
        {
            var mock = new Mock<HttpService>();
            mock.Verify(a => a.PostAsync("asdasd", new DefaultRequest()));
            HomeController controller = new HomeController();
            var result = controller.Index();


        }

        //[TestMethod]
        //public void IndexStringInViewbag()
        //{
        //    HomeController controller = new HomeController();

        //    ViewResult result = controller.Index() as ViewResult;

        //    Assert.AreEqual("Hello world!", result.ViewBag.Message);
        //}
    }
}
