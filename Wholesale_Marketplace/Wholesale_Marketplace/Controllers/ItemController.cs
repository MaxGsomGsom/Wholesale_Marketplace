using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wholesale_Marketplace.Models;

namespace Wholesale_Marketplace.Controllers
{
    public class ItemController : Controller
    {
        WMDB db = new WMDB();
        public ActionResult Info(int itemID=0)
        {
            Helpers.UserCheck(db, ViewBag);
            Item curItem = db.Items.Find(itemID);
            if (curItem == null) return Redirect("/Home/Index");
            else return View("Info", curItem);
        }


        public ActionResult Photo(int id)
        {
            Picture curPic = db.Pictures.Find(id);

            if (curPic != null)
            {
                return File(curPic.Image, "image/jpeg");
            }

            return File("~/Content/Logo.jpg", "image/jpeg");
        }



    }
}