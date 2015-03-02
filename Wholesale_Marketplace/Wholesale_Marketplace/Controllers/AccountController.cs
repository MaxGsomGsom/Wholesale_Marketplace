using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            if (!db.Users.Where(e => e.Login == newUser.Login).Any())
            {
                if (newUser.RoleID == 0)
                {
                    db.Users.Add(newUser);
                    db.SaveChanges();
                    return View("AddInfoBuyer");
                }
            }

            return Register();
        }

    }
}
