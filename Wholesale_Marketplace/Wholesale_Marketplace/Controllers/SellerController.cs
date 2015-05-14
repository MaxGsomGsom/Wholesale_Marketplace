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

                orders = orders.Skip(page * 10).Take(10);

                if (part_page == 1)
                {
                    return PartialView("OrdersPart", orders);
                }

                return View("Orders", orders);

            }
            return Redirect("/Home/Index");

        }



        public ActionResult ConnectOrderToSeller(int id)
        {
            if (Helpers.UserCheck(db, ViewBag) && ViewBag.RoleID == 1)
            {
                try
                {
                    Order curOrder = db.Orders.Find(id);

                    if (curOrder.Item.StoreID == ViewBag.StoreID && curOrder.Seller == null)
                    {
                        curOrder.SellerID = ViewBag.SellerID;
                        db.Entry(curOrder).State = EntityState.Modified;
                        db.SaveChanges();


                        return Redirect("/Order/Info?id=" + id);
                    }
                }
                catch { }
                

                

            }
            return Redirect("/Home/Index");

        }
    }
}