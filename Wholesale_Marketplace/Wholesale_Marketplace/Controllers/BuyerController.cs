using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wholesale_Marketplace.Models;

namespace Wholesale_Marketplace.Controllers
{
    public class BuyerController : Controller
    {
        WMDB db = new WMDB();

        // GET: Buyer
        public ActionResult Orders()
        {
            if (Helpers.UserCheck(db, ViewBag) && ViewBag.RoleID == 0)
            {
                int buyerID = ViewBag.BuyerID;
                IEnumerable<Order> orders = db.Orders.Where(m => m.BuyerID == buyerID).OrderByDescending(m =>m.Open_date);
                return View("Orders", orders);
            }

            return Redirect("/Home/Index");
        }
    }
}