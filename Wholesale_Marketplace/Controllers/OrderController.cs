﻿using System;
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
        public ActionResult Confirm(int Item = -1)
        {
            if (Item == -1) return Redirect("/Home/Index");

            if (Helpers.UserCheck(db, ViewBag) && ViewBag.RoleID == 0)
            {
                Item curItem = db.Items.Find(Item);

                if ((curItem.Close_date == null || curItem.Close_date > DateTime.Now) && curItem.Left_goods_count > 0)
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

                if ((curItem.Close_date == null || curItem.Close_date > DateTime.Now) && curItem.Left_goods_count > 0)
                {
                    NewOrder.Order_statusID = 0;
                    NewOrder.BuyerID = ViewBag.BuyerID;
                    NewOrder.Open_date = DateTime.Now;
                    NewOrder.Shipping_type = db.Shipping_type.Find(NewOrder.ShippingID);
                    NewOrder.Total_price = curItem.Price * NewOrder.Amount + NewOrder.Shipping_type.Price;
                    db.Orders.Add(NewOrder);

                    NewOrder.Item.Left_goods_count -= 1;
                    NewOrder.Item.Orders_count += 1;
                    db.Entry(NewOrder.Item).State = EntityState.Modified;

                    NewOrder.Item.Store.Orders_count += 1;
                    db.Entry(NewOrder.Item.Store).State = EntityState.Modified;

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
                        return Redirect("/Order/Info?id=" + id);
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
                    return Redirect("/Order/Info?id=" + id);
                }
            }
            else if (ViewBag.RoleID == 1)
            {
                Order curOrder = db.Orders.Find(id);
                if (curOrder.Item.StoreID == ViewBag.StoreID)
                {
                    curOrder.Order_statusID = 6;
                    db.Entry(curOrder).State = EntityState.Modified;
                    db.SaveChanges();
                    return Redirect("/Order/Info?id=" + id);
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
        public ActionResult Review(int id, int Mark, string Review_text)
        {
            if (Helpers.UserCheck(db, ViewBag) && ViewBag.RoleID == 0 && Mark >= 0 && Mark <= 5)
            {
                Order curOrder = db.Orders.Find(id);
                if (curOrder.BuyerID == ViewBag.BuyerID)
                {
                    if (curOrder.Order_statusID == 2 || curOrder.Order_statusID == 5)
                    {
                        curOrder.Review_text = Review_text;
                        curOrder.Order_statusID = 3;
                        curOrder.Mark = Mark;
                        curOrder.Item.Average_mark = (double)((double)curOrder.Item.Marks_count * curOrder.Item.Average_mark + (double)curOrder.Mark) / (double)(curOrder.Item.Marks_count + 1);
                        curOrder.Item.Store.Average_mark = (double)((double)curOrder.Item.Store.Marks_count * curOrder.Item.Store.Average_mark + (double)curOrder.Mark) / (double)(curOrder.Item.Store.Marks_count + 1);
                        curOrder.Item.Marks_count += 1;
                        curOrder.Item.Store.Marks_count += 1;

                        db.Entry(curOrder.Item.Store).State = EntityState.Modified;
                        db.Entry(curOrder.Item).State = EntityState.Modified;
                        db.Entry(curOrder).State = EntityState.Modified;
                        db.SaveChanges();
                        return Redirect("/Order/Info?id=" + id);
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
            else if (ViewBag.RoleID == 1)
            {
                Order curOrder = db.Orders.Find(id);
                if (curOrder.Item.StoreID == ViewBag.StoreID)
                {
                    return View("InfoSeller", curOrder);
                }
            }

            return Redirect("/Home/Index");
        }






        [HttpGet]
        public ActionResult Ship(int id)
        {
            if (Helpers.UserCheck(db, ViewBag) && ViewBag.RoleID == 1)
            {
                Order curOrder = db.Orders.Find(id);
                if (curOrder.Item.StoreID == ViewBag.StoreID)
                {
                    return View("Ship", curOrder);
                }
            }

            return Redirect("/Home/Index");
        }

        [HttpPost]
        public ActionResult Ship(int id, string forBuyerInfo, int MakeShipment = 0)
        {
            if (Helpers.UserCheck(db, ViewBag) && ViewBag.RoleID == 1)
            {
                Order curOrder = db.Orders.Find(id);
                if (curOrder.Item.StoreID == ViewBag.StoreID)
                {
                    if (MakeShipment == 1)
                    {
                        curOrder.Order_statusID = 2;
                        curOrder.ForBuyerInfo = forBuyerInfo;
                        db.Entry(curOrder).State = EntityState.Modified;
                        db.SaveChanges();
                        return Redirect("/Order/Info?id=" + id);
                    }
                    else return Content("Не отправлено");
                }
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
                        foreach (Dialog_dispute dialog in curOrder.Dialog_dispute)
                        {
                            dialog.SellerID = ViewBag.SellerID;
                            db.Entry(dialog).State = EntityState.Modified;
                        }
                        db.Entry(curOrder).State = EntityState.Modified;
                        db.SaveChanges();


                        return View("InfoSeller", curOrder);
                    }
                }
                catch { }




            }
            return Redirect("/Home/Index");

        }

    }
}