using System;
using System.Collections.Generic;
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
                    return View("Show", dialog);
                }
            }

            return Redirect("/Home/Index");
        }

        public ActionResult ShowNewMessages(int id, long lastTime)
        {
            if (Helpers.UserCheck(db, ViewBag) && ViewBag.RoleID == 0)
            {
                if (db.Dialog_dispute.Find(id).Buyer.UserID == ViewBag.UserID)
                {
                    DateTime lastTimeConverted = new DateTime(lastTime*10000+(new DateTime(1970,1,1,3,0,0)).Ticks); // зависимость от нашего часового пояса
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

            return Content("");
        }

    }
}