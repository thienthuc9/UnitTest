using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Controllers;
using System.Linq;


namespace WebApplication1.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestIndex()
        {
            var db = new CHSEntities(); //add Entiy Framework - References
            var controller = new CHSController();

            var result = controller.Index();
            var view = result as ViewResult;

            Assert.IsNotNull(view);
            var model = view.Model as List<Table_1>;
            Assert.IsNotNull(model);
            Assert.AreEqual(db.Table_1.Count(), model.Count);


        }
        [TestMethod]
        public void TestDetail()
        {
            var db = new CHSEntities();
            var item = db.Table_1.First();
            var controller = new CHSController();

            var result = controller.Details(item.MaSach);
            var view = result as ViewResult;
            Assert.IsNotNull(view);
            var model = view.Model as Table_1;
            Assert.IsNotNull(model);
            Assert.AreEqual(item.MaSach, model.MaSach);

            result = controller.Details(0);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }
        [TestMethod]
        public void TestCreate()
        {
            var controller = new CHSController();

            var result = controller.Create() as ViewResult;

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void TestEditG()
        {
            var controller = new CHSController();
            var result0 = controller.Edit(0);
            Assert.IsInstanceOfType(result0, typeof(HttpNotFoundResult));

            var db = new CHSEntities();
            var item = db.Table_1.First();
            var result = controller.Edit(item.MaSach) as ViewResult;
            Assert.IsNotNull(result);
            var model = result.Model as Table_1;
            Assert.AreEqual(item.MaSach, model.MaSach);
        }
        [TestMethod]
        public void TestCreateP()
        {
            var db = new CHSEntities();
            var model = new Table_1
            {
                TenSach = " Dragonball VL",
                Giá = "25000",
                TG = "tran chau trang",
                SL = 5
            };
            var controller = new CHSController();

            var result = controller.Create(model);
            var redirect = result as RedirectToRouteResult;
            Assert.IsNotNull(redirect);
            Assert.AreEqual("Index", redirect.RouteValues["action"]);
            var item = db.Table_1.Find(model.MaSach);
            Assert.IsNotNull(item);
            Assert.AreEqual(model.TenSach, item.TenSach);
            Assert.AreEqual(model.Giá, item.Giá);
            Assert.AreEqual(model.TG, item.TG);
            Assert.AreEqual(model.SL, item.SL);
           
        }



    }
}
