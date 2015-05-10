using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Wholesale_Marketplace.Models;
using System.Security.Cryptography;
using System.IO;

namespace Wholesale_Marketplace.Controllers
{
    public class AccountController : Controller
    {

        WMDB db = new WMDB();
        MD5 passHash = MD5.Create();

        [HttpGet]
        public ActionResult Register()
        {
            Helpers.UserCheck(db, ViewBag);
            return View("Register");
        }

        [HttpPost]
        public ActionResult Register(User newUser)
        {
            Helpers.UserCheck(db, ViewBag);
            if (ModelState.IsValid && !db.Users.Where(e => e.Login == newUser.Login).Any())
            {
                if (newUser.RoleID == 0)
                {
                    //newUser.Password = passHash.ComputeHash(Helpers.GetBytes(newUser.Password));
                    db.Users.Add(newUser);
                    db.SaveChanges();
                    FormsAuthentication.SetAuthCookie(newUser.Login, true);
                    ViewBag.RoleID = newUser.RoleID;
                    ViewBag.UserID = newUser.UserID;
                    ViewBag.Login = newUser.Login;

                    Buyer modelOut = new Buyer();
                    modelOut.Name = "";
                    modelOut.Address = "";
                    return View("AddInfoBuyer", modelOut);
                }

                if (newUser.RoleID == 1)
                {
                    db.Users.Add(newUser);
                    db.SaveChanges();
                    FormsAuthentication.SetAuthCookie(newUser.Login, true);
                    ViewBag.RoleID = newUser.RoleID;
                    ViewBag.UserID = newUser.UserID;
                    ViewBag.Login = newUser.Login;

                    Seller modelOut = new Seller();
                    modelOut.Name = "";
                    return View("AddInfoSeller", modelOut);
                }
            } 
            
            return View("Register");

        }

        //[HttpPost]
        public ActionResult AddInfoBuyer([Bind(Exclude = "Avatar")] Buyer newBuyer, HttpPostedFileBase Avatar)
        {
            if (Helpers.UserCheck(db, ViewBag) && ModelState.IsValid)
            {
                if (ViewBag.RoleID == 0)
                {
                    int curUserID = ViewBag.UserID;
                    
                    if (db.Buyers.Any(e => e.UserID == curUserID))
                    {
                        Buyer curBuyer = db.Buyers.First(e => e.UserID == curUserID);
                        curBuyer.Name = newBuyer.Name;
                        curBuyer.Address = newBuyer.Address;
                        if (Avatar != null) curBuyer.Avatar = (new BinaryReader(Avatar.InputStream)).ReadBytes((int)Avatar.InputStream.Length);
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

            Buyer modelOut = new Buyer() ;
            modelOut.Name = "";
            modelOut.Address = "";
            if (ViewBag.UserID >= 0)
            {
                int curUser = ViewBag.UserID;
                try
                {
                    modelOut = db.Buyers.First(m => m.UserID == curUser);
                }
                catch { }
            }
            return View("AddInfoBuyer", modelOut);
        }

        [HttpGet]
        public ActionResult Login()
        {
            Helpers.UserCheck(db, ViewBag);
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(User u)
        {
            if (db.Users.Any(e => e.Login == u.Login))
            {
                User curUser = db.Users.First(e => e.Login == u.Login);
                //u.Password = passHash.ComputeHash(Helpers.GetBytes(u.Password));
                if (curUser.Password == u.Password)
                {
                    FormsAuthentication.SetAuthCookie(curUser.Login, true);
                    return Redirect("/Home/Index");
                }
            }

            return View("Login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/Home/Index");
        }


        public ActionResult AddInfoSeller([Bind(Exclude = "Avatar")] Seller newSeller, HttpPostedFileBase Avatar, String SecretCode)
        {
            if (Helpers.UserCheck(db, ViewBag) && ModelState.IsValid)
            {
                if (ViewBag.RoleID == 1)
                {
                    int curUserID = ViewBag.UserID;

                    if (db.Sellers.Any(e => e.UserID == curUserID))
                    {
                        Seller curSeller = db.Sellers.First(e => e.UserID == curUserID);
                        curSeller.Name = newSeller.Name;

                        if (Avatar != null) curSeller.Avatar = (new BinaryReader(Avatar.InputStream)).ReadBytes((int)Avatar.InputStream.Length);
                        
                        
                        db.Entry(curSeller).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        newSeller.UserID = curUserID;
                        newSeller.Registration_date = DateTime.Now;


                        if (db.Stores.Any(m=>m.StoreID == newSeller.StoreID) && db.Stores.Find(newSeller.StoreID).SecretCode == SecretCode)
                        {
                            db.Sellers.Add(newSeller);
                            db.SaveChanges();
                            return Redirect("/Home/Index");
                        }

                             newSeller.StoreID = null;
                            db.Sellers.Add(newSeller);
                            db.SaveChanges();
                            
                            return Redirect("/Store/CreateStore");

                        

                    }
                    
                }
            }

            Seller modelOut = new Seller();
            modelOut.Name = "";
            if (ViewBag.UserID >= 0)
            {
                int curUser = ViewBag.UserID;
                try
                {
                    modelOut = db.Sellers.First(m => m.UserID == curUser);
                }
                catch { }
            }
            return View("AddInfoSeller", modelOut);
        }


        public ActionResult Avatar(int UserID)
        {
            if (UserID==-1) return File("~/Content/default-avatar.png", "image/png");
            User curUser = db.Users.Find(UserID);

            if (curUser.RoleID == 0)
            {
                try
                {
                    byte[] img = db.Buyers.First(m => m.UserID == curUser.UserID).Avatar;
                    if (img != null) return File(img, "image/jpeg");
                }
                catch { }
            }
            else if (curUser.RoleID == 1)
            {
                try
                {
                    byte[] img = db.Sellers.First(m => m.UserID == curUser.UserID).Avatar;
                    if (img != null) return File(img, "image/jpeg");
                }
                catch { }
            }

            return File("~/Content/default-avatar.png", "image/png");
        }

    }
}
