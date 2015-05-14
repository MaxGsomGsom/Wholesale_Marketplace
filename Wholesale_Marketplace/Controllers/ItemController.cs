using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
            else
            {
                if (ViewBag.RoleID == 1) return View("InfoSeller", curItem);
                return View("Info", curItem);
            }
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
        public ActionResult Search(string search_keywords = "", int page = 0, int sort = 0, int price_min = 0, int price_max = int.MaxValue, int good_sellers = 0, int category = -1, int part_page = 0, int storeId=-1)
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
            collect = db.Items.Where(m => m.Name.Contains(search_keywords) || m.Description.Contains(search_keywords)).Where(m => m.Close_date >= DateTime.Now || m.Close_date==null);
            collect = collect.Where(m => m.Price >= price_min && m.Price <= price_maxForCheck);
            if (good_sellers==1) collect = collect.Where(m => m.Store.Marks_count >= 5 && m.Store.Average_mark >= 4);
            if (category >= 0 && category <= 10) collect = collect.Where(m => m.CategoryID == category);
            if (storeId > 0) collect = collect.Where(m => m.StoreID == storeId);

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




        public ActionResult AddItem()
        {
            if (Helpers.UserCheck(db, ViewBag) && ViewBag.RoleID == 1)
            {
                return View("AddItem");
            }
            return Redirect("/Home/Index");
        }

        
        [HttpPost]
        public ActionResult AddItem(Item newItem, IEnumerable<HttpPostedFileBase> Pics)
        {
            if (Helpers.UserCheck(db, ViewBag) && ViewBag.RoleID == 1)
            {
                if (ModelState.IsValid)
                {
                    if (newItem.Minimum_order < 1) newItem.Minimum_order = 1;
                    newItem.Open_date = DateTime.Now;
                    newItem.StoreID = ViewBag.StoreID;
                    newItem.Marks_count = 0;
                    newItem.Orders_count = 0;
                    db.Items.Add(newItem);
                    db.SaveChanges();


                    if (Pics != null && Pics.ElementAt(0)!=null)
                    {
                        foreach (HttpPostedFileBase img in Pics)
                        {
                            Picture pic = new Picture();
                            pic.ItemID = newItem.ItemID;
                            pic.Image = (new BinaryReader(img.InputStream)).ReadBytes((int)img.InputStream.Length);
                            db.Pictures.Add(pic);
                            
                            
                        }
                        db.SaveChanges();
                    }

                    

                    return Redirect("/Home/Index");
                }
            }
            return View("AddItem");
        }


        
        public ActionResult EditItem(int id)
        {
            if (Helpers.UserCheck(db, ViewBag) && ViewBag.RoleID == 1)
            {
                try
                {
                    Item outModel = db.Items.Find(id);
                    return View("EditItem", outModel);
                }
                catch { }
            }
            return Redirect("/Home/Index");
        }
        

        [HttpPost]
        public ActionResult EditItem(Item editedItem, IEnumerable<HttpPostedFileBase> Pics)
        {
            if (Helpers.UserCheck(db, ViewBag) && ViewBag.RoleID == 1)
            {
                if (ModelState.IsValid)
                {
                    Item oldItem = db.Items.Find(editedItem.ItemID);
                    if (oldItem.StoreID == ViewBag.StoreID)
                    {
                        oldItem.CategoryID = editedItem.CategoryID;
                        oldItem.Minimum_order = editedItem.Minimum_order;
                        if (oldItem.Minimum_order < 1) oldItem.Minimum_order = 1;

                        oldItem.Description = editedItem.Description;
                        oldItem.Left_goods_count = editedItem.Left_goods_count;
                        oldItem.Price = editedItem.Price;
                        oldItem.Name = editedItem.Name;

                        db.Entry(oldItem).State = EntityState.Modified;
                        db.SaveChanges();

                        if (Pics != null && Pics.ElementAt(0) != null)
                        {
                            foreach (HttpPostedFileBase img in Pics)
                            {
                                Picture pic = new Picture();
                                pic.ItemID = editedItem.ItemID;
                                pic.Image = (new BinaryReader(img.InputStream)).ReadBytes((int)img.InputStream.Length);
                                db.Pictures.Add(pic);


                            }
                            db.SaveChanges();
                        }

                        return Redirect("/Home/Index");
                    }
                }
            }
            return View("EditItem");
        }

        
        public ActionResult DeleteItem(int id)
        {
            if (Helpers.UserCheck(db, ViewBag) && ViewBag.RoleID == 1)
            {
              
                    Item delItem = db.Items.Find(id);
                    if (delItem.StoreID == ViewBag.StoreID)
                    {
                        delItem.Close_date = DateTime.Now;
                        db.Entry(delItem).State = EntityState.Modified;
                        db.SaveChanges();
                        return Redirect("/Home/Index");
                    }
    
            }
            return Redirect("/Item/Info?id=" + id);
        }
    }

}