using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wholesale_Marketplace.Models;

namespace Wholesale_Marketplace.Controllers
{
    public class DialogController : Controller
    {
        WMDB db = new WMDB();

        public ActionResult Show(int id)
        {
            if (Helpers.UserCheck(db, ViewBag) && ViewBag.RoleID == 0)
            {
                Dialog_dispute dialog = db.Dialog_dispute.Find(id);
                if (dialog.Buyer.UserID == ViewBag.UserID)
                {
                    ViewBag.DisputeStates = db.DisputeStates;
                    return View("Show", dialog);
                }
            }
            else if (ViewBag.RoleID == 1)
            {
                Dialog_dispute dialog = db.Dialog_dispute.Find(id);
                if ((dialog.Item != null && dialog.Item.StoreID == ViewBag.StoreID) || (dialog.Order != null && dialog.Order.Item.StoreID == ViewBag.StoreID))
                {
                    ViewBag.DisputeStates = db.DisputeStates;
                    return View("ShowSeller", dialog);
                }
            }

            return Redirect("/Home/Index");
        }



        public ActionResult ShowNewMessages(int id, long lastTime)
        {
            if (Helpers.UserCheck(db, ViewBag) && ViewBag.RoleID == 0)
            {
                Dialog_dispute dialog = db.Dialog_dispute.Find(id);
                if (dialog.Buyer.UserID == ViewBag.UserID)
                {
                    DateTime lastTimeConverted = new DateTime(lastTime * 10000 + (new DateTime(1970, 1, 1, 3, 0, 0)).Ticks); // зависимость от нашего часового пояса
                    IEnumerable<Message> messages = db.Messages.Where(m => m.DisputeID == id && m.Post_date > lastTimeConverted);
                    return PartialView("NewMessagesPart", messages);
                }
            }
            else if (ViewBag.RoleID == 1)
            {
                Dialog_dispute dialog = db.Dialog_dispute.Find(id);
                if ((dialog.Item != null && dialog.Item.StoreID == ViewBag.StoreID) || (dialog.Order != null && dialog.Order.Item.StoreID == ViewBag.StoreID))
                {
                    DateTime lastTimeConverted = new DateTime(lastTime * 10000 + (new DateTime(1970, 1, 1, 3, 0, 0)).Ticks);
                    IEnumerable<Message> messages = db.Messages.Where(m => m.DisputeID == id && m.Post_date > lastTimeConverted);
                    return PartialView("NewMessagesPart", messages);
                }
            }

            return Content("");
        }


        public ActionResult PostMessage(Message newMessage, HttpPostedFileBase messageImage)
        {
            if (Helpers.UserCheck(db, ViewBag) && ViewBag.RoleID == 0)
            {
                if (db.Dialog_dispute.Find(newMessage.DisputeID).Buyer.UserID == ViewBag.UserID)
                {
                    if (newMessage.Text == null) return Content("");

                    newMessage.Post_date = DateTime.Now;
                    newMessage.UserID = ViewBag.UserID;

                    db.Messages.Add(newMessage);
                    db.SaveChanges();

                    if (messageImage != null)
                    {
                        Picture newPic = new Picture();
                        newPic.Image = (new BinaryReader(messageImage.InputStream)).ReadBytes((int)messageImage.InputStream.Length);
                        newPic.MessageID = newMessage.MessageID;
                        db.Pictures.Add(newPic);
                        db.SaveChanges();
                    }


                    return Content("");
                }
            }
            else if (ViewBag.RoleID == 1)
            {
                Dialog_dispute dialog = db.Dialog_dispute.Find(newMessage.DisputeID);
                if (dialog.Seller != null && dialog.Seller.UserID == ViewBag.UserID)
                {
                    if (newMessage.Text == null) return Content("");

                    newMessage.Post_date = DateTime.Now;
                    newMessage.UserID = ViewBag.UserID;

                    db.Messages.Add(newMessage);
                    db.SaveChanges();

                    if (messageImage != null)
                    {
                        Picture newPic = new Picture();
                        newPic.Image = (new BinaryReader(messageImage.InputStream)).ReadBytes((int)messageImage.InputStream.Length);
                        newPic.MessageID = newMessage.MessageID;
                        db.Pictures.Add(newPic);
                        db.SaveChanges();
                    }


                    return Content("");
                }
            }
            return Content("");
        }


        public ActionResult GetImage(int id)
        {
            if (Helpers.UserCheck(db, ViewBag) && ViewBag.RoleID == 0)
            {
                try
                {
                    if (db.Pictures.Find(id).Message.Dialog_dispute.Buyer.UserID == ViewBag.UserID)
                    {
                        byte[] img = db.Pictures.Find(id).Image;
                        if (img != null) return File(img, "image/jpeg");
                    }
                }
                catch { }
            }
            if (ViewBag.RoleID == 1)
            {
                try
                {
                    Dialog_dispute dialog = db.Pictures.Find(id).Message.Dialog_dispute;
                    if ((dialog.Item != null && dialog.Item.StoreID == ViewBag.StoreID) || (dialog.Order != null && dialog.Order.Item.StoreID == ViewBag.StoreID))
                    {
                        byte[] img = db.Pictures.Find(id).Image;
                        if (img != null) return File(img, "image/jpeg");
                    }
                }
                catch { }
            }

            return Content("");
        }

        public ActionResult CreateDialogForItem(int itemID)
        {
            if (Helpers.UserCheck(db, ViewBag) && ViewBag.RoleID == 0)
            {
                if (db.Items.Any(m => m.ItemID == itemID))
                {
                    int buyer = ViewBag.BuyerID;
                    if (db.Dialog_dispute.Any(m => m.ItemID == itemID && m.BuyerID == buyer))
                    {
                        ViewBag.DisputeStates = db.DisputeStates;
                        return View("Show", db.Dialog_dispute.First(m => m.ItemID == itemID && m.BuyerID == buyer));
                    }
                    else
                    {
                        Dialog_dispute newDialog = new Dialog_dispute();
                        newDialog.Open_date = DateTime.Now;
                        newDialog.ItemID = itemID;
                        newDialog.IsDispute = false;
                        newDialog.HasNewMessages = false;
                        newDialog.BuyerID = ViewBag.BuyerID;
                        db.Dialog_dispute.Add(newDialog);
                        db.SaveChanges();
                        ViewBag.DisputeStates = db.DisputeStates;
                        return View("Show", newDialog);
                    }
                }


            }

            return Redirect("/Home/Index");
        }


        public ActionResult CreateDialogForOrder(int orderID)
        {
            if (Helpers.UserCheck(db, ViewBag) && ViewBag.RoleID == 0)
            {
                if (db.Orders.Any(m => m.OrderID == orderID))
                {
                    int buyer = ViewBag.BuyerID;
                    if (db.Dialog_dispute.Any(m => m.OrderID == orderID && m.BuyerID == buyer))
                    {
                        ViewBag.DisputeStates = db.DisputeStates;
                        return View("Show", db.Dialog_dispute.First(m => m.OrderID == orderID && m.BuyerID == buyer));
                    }
                    else
                    {
                        Dialog_dispute newDialog = new Dialog_dispute();
                        newDialog.Open_date = DateTime.Now;
                        newDialog.OrderID = orderID;
                        newDialog.IsDispute = false;
                        newDialog.HasNewMessages = false;
                        newDialog.BuyerID = ViewBag.BuyerID;
                        db.Dialog_dispute.Add(newDialog);
                        db.SaveChanges();
                        ViewBag.DisputeStates = db.DisputeStates;
                        newDialog.Order = db.Orders.Find(orderID);
                        return View("Show", newDialog);
                    }
                }


            }

            return Redirect("/Home/Index");
        }


        public ActionResult OpenDispute(int orderID)
        {
            if (Helpers.UserCheck(db, ViewBag) && ViewBag.RoleID == 0)
            {
                if (db.Orders.Any(m => m.OrderID == orderID))
                {
                    Order curOrder = db.Orders.First(m => m.OrderID == orderID);

                    if (curOrder.Order_statusID == 1 || curOrder.Order_statusID == 2)
                    {

                        int buyer = ViewBag.BuyerID;
                        if (db.Dialog_dispute.Any(m => m.OrderID == orderID && m.BuyerID == buyer))
                        {
                            Dialog_dispute curDispute = db.Dialog_dispute.First(m => m.OrderID == orderID && m.BuyerID == buyer);
                            curDispute.IsDispute = true;
                            curDispute.DisputeStateID = 3;
                            curDispute.RefundValue = 0;
                            curOrder.Order_statusID = 4;
                            db.Entry(curDispute).State = EntityState.Modified;
                            db.Entry(curOrder).State = EntityState.Modified;
                            db.SaveChanges();
                            ViewBag.DisputeStates = db.DisputeStates;
                            return View("Show", db.Dialog_dispute.First(m => m.OrderID == orderID && m.BuyerID == buyer));
                        }
                        else
                        {
                            Dialog_dispute newDialog = new Dialog_dispute();
                            newDialog.Open_date = DateTime.Now;
                            newDialog.OrderID = orderID;
                            newDialog.IsDispute = true;
                            newDialog.DisputeStateID = 3;
                            newDialog.RefundValue = 0;
                            newDialog.BuyerID = ViewBag.BuyerID;
                            db.Dialog_dispute.Add(newDialog);
                            db.SaveChanges();
                            ViewBag.DisputeStates = db.DisputeStates;
                            newDialog.Order = db.Orders.Find(orderID);
                            return View("Show", newDialog);
                        }
                    }
                }


            }

            return Redirect("/Home/Index");
        }


        public ActionResult Agree(int id)
        {
            if (Helpers.UserCheck(db, ViewBag) && ViewBag.RoleID == 0)
            {
                Dialog_dispute dialog = db.Dialog_dispute.Find(id);
                if (dialog.Buyer.UserID == ViewBag.UserID && dialog.IsDispute == true)
                {
                    dialog.BuyerAgree = true;
                    db.Entry(dialog).State = EntityState.Modified;

                    if (dialog.BuyerAgree == true && dialog.SellerAgree == true && dialog.Order.Order_statusID == 4)
                    {
                        Order curOrder = dialog.Order;
                        curOrder.Order_statusID = 5;
                        db.Entry(curOrder).State = EntityState.Modified;
                    }
                    db.SaveChanges();
                    ViewBag.DisputeStates = db.DisputeStates;
                    return View("Show", dialog);
                }
            }
            else if (ViewBag.RoleID == 1)
            {
                Dialog_dispute dialog = db.Dialog_dispute.Find(id);
                if (dialog.Seller.UserID == ViewBag.UserID && dialog.IsDispute == true)
                {
                    dialog.SellerAgree = true;
                    db.Entry(dialog).State = EntityState.Modified;

                    if (dialog.BuyerAgree == true && dialog.SellerAgree == true && dialog.Order.Order_statusID == 4)
                    {
                        Order curOrder = dialog.Order;
                        curOrder.Order_statusID = 5;
                        db.Entry(curOrder).State = EntityState.Modified;
                    }
                    db.SaveChanges();
                    ViewBag.DisputeStates = db.DisputeStates;
                    return View("ShowSeller", dialog);
                }
            }

            return Redirect("/Home/Index");
        }


        public ActionResult ChangeSolution(int id, int refundValue, int disputeState)
        {
            if (Helpers.UserCheck(db, ViewBag) && ViewBag.RoleID == 0)
            {
                Dialog_dispute dialog = db.Dialog_dispute.Find(id);
                if (dialog.Buyer.UserID == ViewBag.UserID && dialog.IsDispute == true && refundValue >= 0
                    && refundValue <= dialog.Order.Total_price && disputeState >= 0 && disputeState <= 3)
                {
                    dialog.BuyerAgree = false;
                    dialog.SellerAgree = false;
                    dialog.RefundValue = refundValue;
                    dialog.DisputeStateID = disputeState;
                    db.Entry(dialog).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.DisputeStates = db.DisputeStates;
                    return View("Show", dialog);
                }
            }
            else if (ViewBag.RoleID == 1)
            {
                Dialog_dispute dialog = db.Dialog_dispute.Find(id);
                if (dialog.Seller.UserID == ViewBag.UserID && dialog.IsDispute == true && refundValue >= 0
                    && refundValue <= dialog.Order.Total_price && disputeState >= 0 && disputeState <= 3)
                {
                    dialog.BuyerAgree = false;
                    dialog.SellerAgree = false;
                    dialog.RefundValue = refundValue;
                    dialog.DisputeStateID = disputeState;
                    db.Entry(dialog).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.DisputeStates = db.DisputeStates;
                    return View("ShowSeller", dialog);
                }
            }

            return Redirect("/Home/Index");
        }


        public ActionResult ConnectDialogToSeller(int id)
        {
            if (Helpers.UserCheck(db, ViewBag) && ViewBag.RoleID == 1)
            {
                try
                {
                    Dialog_dispute curDialog = db.Dialog_dispute.Find(id);

                    if (((curDialog.Item != null && curDialog.Item.StoreID == ViewBag.StoreID) || (curDialog.Order != null && curDialog.Order.Item.StoreID == ViewBag.StoreID)) && curDialog.Seller == null)
                    {
                        curDialog.SellerID = ViewBag.SellerID;
                        db.Entry(curDialog).State = EntityState.Modified;
                        db.SaveChanges();

                        ViewBag.DisputeStates = db.DisputeStates;
                        return View("ShowSeller", curDialog);
                    }
                }
                catch { }




            }
            return Redirect("/Home/Index");

        }

    }
}