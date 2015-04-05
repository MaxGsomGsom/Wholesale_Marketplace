using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wholesale_Marketplace.Models;

namespace Wholesale_Marketplace
{
    public static class Helpers
    {
        public static bool UserCheck(WMDB db, dynamic ViewBag)
        {
            ViewBag.Categories = db.Item_category;

            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                User curUser = db.Users.First(e => e.Login == HttpContext.Current.User.Identity.Name);
                if (curUser!=null)
                {
                    ViewBag.RoleID = curUser.RoleID;
                    ViewBag.UserID = curUser.UserID;
                    ViewBag.Login = curUser.Login;
                    switch (curUser.RoleID)
                    {
                        case 0:
                            {
                                ViewBag.Address = db.Buyers.First(e => e.UserID == curUser.UserID).Address;
                                ViewBag.BuyerID = db.Buyers.First(e => e.UserID == curUser.UserID).BuyerID;
                                break;
                            }
                    }
                    return true;
                }
            }

                ViewBag.RoleID = -1;
                ViewBag.UserID = -1;
                ViewBag.Login = "";
                return false;
            
        }



        public static MvcHtmlString Stars(int mark)
        {
            string outStr = "";
            for (int i = 1; i <= 5; i++)
            {
                if (mark >= i) outStr += "<span class=\"glyphicon glyphicon-star\" style=\"color: blueviolet; display:inline\"></span>";
                else outStr += "<span class=\"glyphicon glyphicon-star\" style=\"color: lightgray; display:inline\"></span>";
            }
            return new MvcHtmlString(outStr);
        }

    }
}