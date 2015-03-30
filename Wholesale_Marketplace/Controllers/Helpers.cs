using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
                    switch (curUser.RoleID)
                    {
                        case 0:
                            {
                                //ViewBag.Avatar = db.Buyers.First(e => e.User == curUser);
                                ViewBag.Address = db.Buyers.First(e => e.UserID == curUser.UserID).Address;
                                ViewBag.BuyerID = db.Buyers.First(e => e.UserID == curUser.UserID).BuyerID;
                                break;
                            }
                    }
                    return true;
                }
                else
                {
                    ViewBag.RoleID = -1;
                    ViewBag.UserID = -1;
                    ViewBag.Login = "";
                    //ViewBag.Avatar = null;
                    return false;
                }
            }
            else
            {
                ViewBag.RoleID = -1;
                ViewBag.UserID = -1;
                ViewBag.Login = "";
                //ViewBag.Avatar = null;
                return false;
            }
        }


        public static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }


        public static MvcHtmlString Stars(int pos, int neg)
        {
            if ((pos + neg) < 5) return new MvcHtmlString("-");
            int num = pos/((pos+neg) / 5);
            string outStr = "";
            for (int i = 0; i < 5; i++)
            {
                if (num>i) outStr += "<span class=\"glyphicon glyphicon-star\" style=\"color: blueviolet; display:inline\"></span>";
                else outStr += "<span class=\"glyphicon glyphicon-star\" style=\"color: lightgray; display:inline\"></span>";
            }
            return new MvcHtmlString(outStr);
        }

        public static MvcHtmlString Stars(int mark)
        {
            return Stars(mark, 5 - mark);
        }
    }
}