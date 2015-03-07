using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Wholesale_Marketplace.Models;

namespace Wholesale_Marketplace.Controllers
{
    public class AccountController : Controller
    {

        WMDB db = new WMDB();

        [HttpGet]
        public ActionResult Register()
        {

            return View("Register");
        }

        [HttpPost]
        public ActionResult Register(User newUser)
        {
            if (ModelState.IsValid && !db.Users.Where(e => e.Login == newUser.Login).Any())
            {
                if (newUser.RoleID == 0)
                {
                    db.Users.Add(newUser);
                    db.SaveChanges();
                    FormsAuthentication.SetAuthCookie(newUser.Login, true);
                    return View("AddInfoBuyer");
                }
            } 
            
            return View("Register");

        }

        //[HttpPost]
        public ActionResult AddInfoBuyer(Buyer newBuyer)
        {
            if (ModelState.IsValid && Helpers.UserCheck(db, ViewBag))
            {
                if (ViewBag.RoleID == 0)
                {
                    int curUserID = ViewBag.UserID;
                    
                    if (db.Buyers.Any(e => e.UserID == curUserID))
                    {
                        Buyer curBuyer = db.Buyers.First(e => e.UserID == curUserID);
                        curBuyer.Name = newBuyer.Name;
                        curBuyer.Address = newBuyer.Address;
                        db.Entry(curBuyer).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        newBuyer.UserID = curUserID;
                        newBuyer.Orders_count = 0;
                        newBuyer.Registration_date = DateTime.Now;
                        db.Buyers.Add(newBuyer);
                        db.SaveChanges();
                    }
                    return Redirect("/Home/Index");
                }
            }

            //return View("AddInfoBuyer");
            return View("AddInfoBuyer");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(User u)
        {
            User curUser = db.Users.Where(e => e.Login == u.Login).First();
            if (curUser!=null && curUser.Password == u.Password)
            {
                FormsAuthentication.SetAuthCookie(curUser.Login, true);
                return Content("Успешно");
            }

            return Content("Ошибка");
        }

    }
}
