using System;
using System.Collections.Generic;
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
            if (!db.Users.Where(e => e.Login == newUser.Login).Any())
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

        [HttpPost]
        public ActionResult AddInfoBuyer(Buyer newBuyer)
        {
            
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                User connectedUser = db.Users.First(e => e.Login == User.Identity.Name);
                if (connectedUser != null)
                {
                    newBuyer.User = connectedUser;
                    newBuyer.Orders_count = 0;
                    newBuyer.Registration_date = DateTime.Now;
                    db.Buyers.Add(newBuyer);
                    db.SaveChanges();
                    return Content("Успешная регистрация");
                }
            }

            return View("AddInfoBuyer");
        }

    }
}
