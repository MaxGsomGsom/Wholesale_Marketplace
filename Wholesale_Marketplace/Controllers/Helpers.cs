using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wholesale_Marketplace.Models;

namespace Wholesale_Marketplace.Controllers
{
    public static class Helpers
    {
        public static bool UserCheck(WMDB db, dynamic ViewBag)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                User curUser = db.Users.First(e => e.Login == HttpContext.Current.User.Identity.Name);
                if (curUser!=null)
                {
                    ViewBag.RoleID = curUser.RoleID;
                    ViewBag.UserID = curUser.UserID;
                    ViewBag.Login = curUser.Login;
                    return true;
                }
                else
                {
                    ViewBag.RoleID = -1;
                    ViewBag.UserID = -1;
                    ViewBag.Login = "";
                    return false;
                }
            }
            else
            {
                ViewBag.RoleID = -1;
                ViewBag.UserID = -1;
                ViewBag.Login = "";
                return false;
            }
        }
    }
}