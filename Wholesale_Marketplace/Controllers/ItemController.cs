using System;
using System.Collections;
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
        public ActionResult Info(int id=0)
        {
            Helpers.UserCheck(db, ViewBag);
            Item curItem = db.Items.Find(id);
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

        //сортировка 0 возрастание цены, 1 убывание цены, 2 убывание рейтингу, 3 убывание количества заказов, 4 убывание дате добавления, возрастание даты добавления
        //реализовать бесплатную доставку
        public ActionResult Search(string search_keywords = "", int page = 0, int sort = 0, int price_min = 0, int price_max = int.MaxValue, int good_sellers = 0, int category = -1, int part_page = 0)
        {
            Helpers.UserCheck(db, ViewBag);

            ViewBag.search_keywords = search_keywords;
            ViewBag.sort = sort;
            ViewBag.price_min = price_min;
            ViewBag.price_max = price_max;
            ViewBag.good_sellers = good_sellers;
            ViewBag.category = category;

            int price_maxForCheck = (price_max >= int.MaxValue) ? int.MaxValue : price_max;

            IEnumerable<Item> collect;
            collect = db.Items.Where(m => m.Name.Contains(search_keywords) || m.Description.Contains(search_keywords)).Where(m=> m.Close_date >= DateTime.Now);
            collect = collect.Where(m => m.Price >= price_min && m.Price <= price_maxForCheck);
            if (good_sellers==1) collect = collect.Where(m => m.Store.Marks_count >= 5 && m.Store.Average_mark >= 4);
            if (category >= 0 && category <= 10) collect = collect.Where(m => m.CategoryID == category);

            ViewBag.FoundCount = collect.Count();

            switch (sort)
            {
                case 0: collect = collect.OrderBy(m => m.Price); break;
                case 1: collect = collect.OrderByDescending(m => m.Price); break;
                case 2: collect = collect.OrderByDescending(m => m.Average_mark); break;
                case 3: collect = collect.OrderByDescending(m => m.Orders_count); break;
                case 4: collect = collect.OrderBy(m => m.Open_date); break;
                case 5: collect = collect.OrderByDescending(m => m.Open_date); break;
            }

            collect = collect.Skip(page * 10).Take(10);

            if (part_page==1)
            {
                return PartialView("SearchPart", collect.ToList());
            }

            return View("Search", collect.ToList());
        }



    }

}