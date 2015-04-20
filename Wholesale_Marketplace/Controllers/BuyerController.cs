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

        public ActionResult Dialogs()
        {
            if (Helpers.UserCheck(db, ViewBag) && ViewBag.RoleID == 0)
            {
                int buyerID = ViewBag.BuyerID;
                IEnumerable<Dialog_dispute> dialogs = db.Dialog_dispute.Where(m => m.BuyerID == buyerID);
                dialogs = dialogs.OrderByDescending(m =>
                {
                    DateTime key = new DateTime(1999,1,1);
                    try
                    {
                        key = m.Messages.Last().Post_date;
                    }
                    catch
                    {
                        key = m.Open_date;
                    }
                    return key;
                });
                return View("Dialogs", dialogs);
            }

            return Redirect("/Home/Index");
        }
    }
}