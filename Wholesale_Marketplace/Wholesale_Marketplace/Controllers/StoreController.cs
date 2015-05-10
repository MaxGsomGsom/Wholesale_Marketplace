using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Wholesale_Marketplace.Models;

//доделать регистрацию магаза, проверить почему ошипка при попытке редактирования

namespace Wholesale_Marketplace.Controllers
{
    public class StoreController : Controller
    {
        WMDB db = new WMDB();


        [HttpGet]
        public ActionResult CreateStore()
        {
            if (Helpers.UserCheck(db, ViewBag) && ViewBag.RoleID == 1)
            {
                Store modelOut2 = new Store();
                modelOut2.Description = "";
                modelOut2.Name = "";
                modelOut2.SecretCode = "";

                return View("CreateStore", modelOut2);
            }
            return Redirect("/Home/Index");
        }

        [HttpPost]
        public ActionResult CreateStore(Store newStore)
        {
            if (Helpers.UserCheck(db, ViewBag) && ViewBag.RoleID == 1)
            {
                if (ModelState.IsValid)
                {
                    newStore.Created_date = DateTime.Now;
                    newStore.Marks_count = 0;
                    newStore.Orders_count = 0;
                    newStore.Average_mark = 0;
                    newStore.Owner_sellerID = ViewBag.SellerID;
                    db.Stores.Add(newStore);
                    db.SaveChanges();

                    Seller me = db.Sellers.Find(ViewBag.SellerID);
                    me.Store = newStore;
                    db.Entry(me).State = EntityState.Modified;
                    db.SaveChanges();

                    return Redirect("/Home/Index");
                }
                return View("/Store/CreateStore");
            }

            return Redirect("/Home/Index");

        }
    }
}