using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wholesale_Marketplace.Models;

namespace Wholesale_Marketplace.Controllers
{
    public class OrderController : Controller
    {
        WMDB db = new WMDB();

        [HttpGet]
        public ActionResult Confirm(int Item=-1)
        {
            if (Item == -1) return Redirect("/Home/Index");

            if (Helpers.UserCheck(db, ViewBag) && ViewBag.RoleID == 0)
            {
                Item curItem = db.Items.Find(Item);

                if (curItem.Close_date > DateTime.Now && curItem.Left_goods_count > 0)
                {
                    ViewBag.ShippingTypes = db.Shipping_type;
                    return View("Confirm", curItem);
                }
            }

            return Redirect("/Account/Login");
        }

        [HttpPost]
        public ActionResult Confirm(Order NewOrder)
        {
            if (Helpers.UserCheck(db, ViewBag) && ViewBag.RoleID == 0)
            {
                Item curItem = db.Items.Find(NewOrder.ItemID);

                if (curItem.Close_date > DateTime.Now && curItem.Left_goods_count > 0)
                {
                    //единичные агент продавец и писпут означают что их пока нет
                    //NewOrder.AgentID = 1;
                    //NewOrder.SellerID = 1;
                    //NewOrder.DisputeID = 1;


                    NewOrder.Order_statusID = 0;
                    NewOrder.BuyerID = ViewBag.BuyerID;
                    NewOrder.Open_date = DateTime.Now;
                    NewOrder.Total_price = curItem.Price * NewOrder.Amount;
                    db.Orders.Add(NewOrder);
                    db.SaveChanges();
                    return Pay(NewOrder.OrderID);
                }
            }

            return Redirect("/Account/Login");
        }

        [HttpGet]
        public ActionResult Pay(int id)
        {
            if (Helpers.UserCheck(db, ViewBag) && ViewBag.RoleID == 0)
            {
                Order curOrder = db.Orders.Find(id);
                if (curOrder.BuyerID == ViewBag.BuyerID)
                {
                    return View("Pay", curOrder);
                }
            }

            return Redirect("/Home/Index");
        }

        [HttpPost]
        public ActionResult Pay(int id, int MakePayment = 0)
        {
            if (Helpers.UserCheck(db, ViewBag) && ViewBag.RoleID == 0)
            {
                Order curOrder = db.Orders.Find(id);
                if (curOrder.BuyerID == ViewBag.BuyerID)
                {
                    if (MakePayment == 1)
                    {
                        curOrder.Order_statusID = 1;
                        db.Entry(curOrder).State = EntityState.Modified;
                        db.SaveChanges();
                        return Content("Оплачено");
                    }
                    else return Content("Не оплачено");
                }
            }
            return Redirect("/Home/Index");
        }

        [HttpGet]
        public ActionResult Cancel(int id)
        {
            if (Helpers.UserCheck(db, ViewBag) && ViewBag.RoleID == 0)
            {
                    Order curOrder = db.Orders.Find(id);
                    if (curOrder.BuyerID == ViewBag.BuyerID)
                    {
                        curOrder.Order_statusID = 6;
                        db.Entry(curOrder).State = EntityState.Modified;
                        db.SaveChanges();
                        return Content("Заказ отменен");
                    }
            }

            return Redirect("/Home/Index");
        }


        [HttpGet]
        public ActionResult Review(int id = -1)
        {
            if (id == -1) return Redirect("/Home/Index");

            if (Helpers.UserCheck(db, ViewBag) && ViewBag.RoleID == 0)
            {
                Order curOrder = db.Orders.Find(id);
                if (curOrder.BuyerID == ViewBag.BuyerID)
                {
                    if (curOrder.Order_statusID == 2 || curOrder.Order_statusID == 5)
                    {
                        return View("Review", curOrder);
                    }
                }
            }

            return Redirect("/Home/Index");
        }


        [HttpPost]
        public ActionResult Review(int id, int Mark)
        {
            if (Helpers.UserCheck(db, ViewBag) && ViewBag.RoleID == 0 && Mark>=0 && Mark<=5)
            {
                    Order curOrder = db.Orders.Find(id);
                    if (curOrder.BuyerID == ViewBag.BuyerID)
                    {
                        if (curOrder.Order_statusID == 2 || curOrder.Order_statusID == 5)
                        {
                            curOrder.Order_statusID = 3;
                            curOrder.Mark = Mark;
                            db.Entry(curOrder).State = EntityState.Modified;
                            db.SaveChanges();
                            return Content("Отзыв оставлен");
                        }
                    }
            }

            return Redirect("/Home/Index");
        }


        [HttpGet]
        public ActionResult Info(int id)
        {
            if (Helpers.UserCheck(db, ViewBag) && ViewBag.RoleID == 0)
            {
                Order curOrder = db.Orders.Find(id);
                if (curOrder.BuyerID == ViewBag.BuyerID)
                {
                    return View("Info", curOrder);
                }
            }

            return Redirect("/Home/Index");
        }

    }
}