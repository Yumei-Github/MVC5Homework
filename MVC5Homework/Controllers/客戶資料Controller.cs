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
    public class 客戶資料Controller : Controller
    {
        //private 客戶資料Entities db = new 客戶資料Entities();

        客戶資料Repository repo = RepositoryHelper.Get客戶資料Repository();


        // GET: 客戶資料
        public ActionResult Index()
        {

            //return View(db.客戶資料.Where(p => p.isDelete == false));
            return View(repo.All().Where(p => p.isDelete == false));
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
                repo.Add(Client);
                repo.UnitOfWork.Commit();
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
            var data = repo.All().First(p => p.Id == Id);
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
                var data = repo.All().First(p => p.Id == Id);
                data.InjectFrom(Client);
                //data.客戶名稱 = Client.客戶名稱;
                //data.統一編號 = Client.統一編號;
                //data.電話 = Client.電話;
                //data.傳真 = Client.傳真;
                //data.地址 = Client.地址;
                //data.Email = Client.Email;
                repo.UnitOfWork.Commit();
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
            var data = repo.All().First(p => p.Id == Id);
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
            var data = repo.All().First(p => p.Id == Id);
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
            //var data = db.客戶資料.Find(Id);
            //db.客戶資料.Remove(data);
            //db.SaveChanges();
            //db.Configuration.ValidateOnSaveEnabled = false;
            repo.UnitOfWork.Context.Configuration.ValidateOnSaveEnabled = false;
            if (ModelState.IsValid)
            {
                var data = repo.All().First(p => p.Id == Id);
                data.isDelete = true;
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(Id);

        }

    }
}