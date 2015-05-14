using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wholesale_Marketplace.Models;

namespace Wholesale_Marketplace.Controllers
{
    public class SellerController : Controller
    {
        WMDB db = new WMDB();

        public ActionResult Orders(int page = 0, int part_page = 0)
        {
            if (Helpers.UserCheck(db, ViewBag) && ViewBag.RoleID == 1)
            {
                int sellerID = ViewBag.SellerID;
                int storeID = ViewBag.StoreID;

                IEnumerable<Order> orders = db.Orders.Where(m =>
                    m.SellerID == sellerID || (m.Item.StoreID == storeID && m.Seller == null)
                    ).OrderByDescending(m => m.Open_date);


                ViewBag.waitPay = orders.Where(m => m.Order_status.Order_statusID == 0).Count();
                ViewBag.waitShipping = orders.Where(m => m.Order_status.Order_statusID == 1).Count();
                ViewBag.waitClosing = orders.Where(m => m.Order_status.Order_statusID == 2).Count();
                ViewBag.inDispute = orders.Where(m => m.Order_status.Order_statusID == 4).Count();
                ViewBag.noSeller = orders.Where(m => m.Seller == null).Count();

                orders = orders.Skip(page * 10).Take(10);

                if (part_page == 1)
                {
                    return PartialView("OrdersPart", orders);
                }

                return View("Orders", orders);

            }
            return Redirect("/Home/Index");

        }


        public ActionResult Dialogs(int page = 0, int part_page = 0)
        {

            if (Helpers.UserCheck(db, ViewBag) && ViewBag.RoleID == 1)
            {
                int sellerID = ViewBag.SellerID;
                int storeID = ViewBag.StoreID;

                IEnumerable<Dialog_dispute> dialogs = db.Dialog_dispute.Where(m => 
                    m.SellerID == sellerID || (m.Item!=null && m.Item.StoreID == storeID) || (m.Order!=null && m.Order.Item.StoreID == storeID));

                ViewBag.dialogsNum = dialogs.Count();
                ViewBag.disputesNum = dialogs.Where(m => m.Order!=null && m.Order.Order_statusID == 4).Count();
                ViewBag.noSeller = dialogs.Where(m => m.Seller == null).Count();

                dialogs = dialogs.OrderByDescending(m =>
                {
                    DateTime key = new DateTime(1999, 1, 1);
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

                dialogs = dialogs.Skip(page * 10).Take(10);

                if (part_page == 1)
                {
                    return PartialView("DialogsPart", dialogs);
                }

                return View("Dialogs", dialogs);
            }

            return Redirect("/Home/Index");

        }

   
    }
}