using MVC5Homework.Models;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVC5Homework.Controllers
{
    public class 客戶銀行資訊Controller : Controller
    {
        private 客戶資料Entities db = new 客戶資料Entities();
        // GET: 客戶銀行資訊
        public ActionResult Index()
        {
            return View(db.客戶銀行資訊.Where(p => p.isDelete == false));
        }

        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");
            return View();
        }

        [HttpPost]
        public ActionResult Create(客戶銀行資訊 Bank)
        {
            if (ModelState.IsValid)
            {
                db.客戶銀行資訊.Add(Bank);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");

            return View(Bank);
        }

        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = db.客戶銀行資訊.Find(Id);
            if (data == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", data.客戶Id);
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(int Id, 客戶銀行資訊 Bank)
        {
            if (ModelState.IsValid)
            {
                var data = db.客戶銀行資訊.Find(Id);
                data.InjectFrom(Bank);
                //data.客戶Id = Bank.客戶Id;
                //data.銀行代碼 = Bank.銀行代碼;
                //data.銀行代碼 = Bank.銀行代碼;
                //data.分行代碼 = Bank.分行代碼;
                //data.帳戶名稱 = Bank.帳戶名稱;
                //data.帳戶號碼 = Bank.帳戶號碼;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");
            return View(Bank);
        }

        public ActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = db.客戶銀行資訊.Find(Id);
            if (data == null)
            { return HttpNotFound(); }
            return View(data);
        }

        public ActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = db.客戶銀行資訊.Find(Id);
            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePOST(int Id)
        {
            //if (ModelState.IsValid)
            //{
            //    var data = db.客戶銀行資訊.Find(Id);
            //    db.客戶銀行資訊.Remove(data);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            db.Configuration.ValidateOnSaveEnabled = false;

            if (ModelState.IsValid)
            {
                var data = db.客戶銀行資訊.Find(Id);
                data.isDelete = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Id);
        }
    }
}