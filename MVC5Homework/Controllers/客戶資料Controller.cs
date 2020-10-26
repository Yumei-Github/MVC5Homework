using MVC5Homework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVC5Homework.Controllers
{
    public class 客戶資料Controller : Controller
    {
        private 客戶資料Entities db = new 客戶資料Entities();

        // GET: 客戶資料
        public ActionResult Index()
        {

            return View(db.客戶資料);
        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(客戶資料 Client)
        {
            if (ModelState.IsValid)
            {
                db.客戶資料.Add(Client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Client);
        }

        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = db.客戶資料.Find(Id);
            if (data == null)
            {
                return HttpNotFound();
            }

            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(int Id, 客戶資料 Client)
        {
            if (ModelState.IsValid)
            {
                var data = db.客戶資料.Find(Id);
                data.客戶名稱 = Client.客戶名稱;
                data.統一編號 = Client.統一編號;
                data.電話 = Client.電話;
                data.傳真 = Client.傳真;
                data.地址 = Client.地址;
                data.Email = Client.Email;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Client);
        }

        public ActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = db.客戶資料.Find(Id);
            if (data == null)
            {
                return HttpNotFound();
            }

            return View(data);
        }

        public ActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = db.客戶資料.Find(Id);
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
            var data = db.客戶資料.Find(Id);
            db.客戶資料.Remove(data);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

    }
}