using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wholesale_Marketplace.Models;

namespace Wholesale_Marketplace.Controllers
{
    public class HomeController : Controller
    {
        WMDB db = new WMDB();
        public ActionResult Index()
        {
            Helpers.UserCheck(db, ViewBag);
            return View();
        }

        public ActionResult Help()
        {
            Helpers.UserCheck(db, ViewBag);
            return View("Help");
        }

    }
}