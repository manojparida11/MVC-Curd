using MVCApp.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace MVCApp.Controllers
{
    public class SampleController : Controller
    {
        // GET: Sample
        db_mvctestEntities dbObj = new db_mvctestEntities();
        public ActionResult Index(tbl_TestData obj)
        {
            
            if (obj != null)
                return View(obj);
            else
            return View();
        }

        [HttpPost]
        public ActionResult AddSample(tbl_TestData model)
        {
            if (ModelState.IsValid)
            {
                tbl_TestData obj = new tbl_TestData();
                obj.ID = model.ID;
                obj.Type = model.Type;
                obj.Content = model.Content;
                obj.H1 = model.H1;
                obj.Title = model.Title;
                obj.Description = model.Description;
                obj.Keyword = model.Keyword;
                obj.URL = model.URL;

                if (model.ID == 0)
                {
                    dbObj.tbl_TestData.Add(obj);

                    dbObj.SaveChanges();
                }
                else
                {
                    dbObj.Entry(obj).State = EntityState.Modified;
                    dbObj.SaveChanges();
                }
            }
            ModelState.Clear();
            var list = dbObj.tbl_TestData.ToList();

            return View("SampleList", list);
            //return View("SampleList");

        }

        public ActionResult SampleList()
        {
            var res = dbObj.tbl_TestData.ToList();

            return View("SampleList", res);
        }

        public ActionResult Delete (int id)
        {
            var res = dbObj.tbl_TestData.Where(x => x.ID == id).First();

            dbObj.tbl_TestData.Remove(res);
            dbObj.SaveChanges();

            var list = dbObj.tbl_TestData.ToList();

            return View("SampleList", list);
        }
    }
}