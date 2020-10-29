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
        //private 客戶資料Entities db = new 客戶資料Entities();
        客戶資料Repository repo;
        客戶銀行資訊Repository repo_Bank;
        public 客戶銀行資訊Controller()
        {
            repo = RepositoryHelper.Get客戶資料Repository();
            repo_Bank = RepositoryHelper.Get客戶銀行資訊Repository(repo.UnitOfWork);
        }
        // GET: 客戶銀行資訊
        public ActionResult Index()
        {
            return View(repo_Bank.All().Where(p => p.isDelete == false));
        }

        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(repo.All().Where(p => p.isDelete == false), "Id", "客戶名稱");
            return View();
        }

        [HttpPost]
        public ActionResult Create(客戶銀行資訊 Bank)
        {
            if (ModelState.IsValid)
            {
                repo_Bank.Add(Bank);
                repo_Bank.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(repo.All().Where(p => p.isDelete == false), "Id", "客戶名稱");

            return View(Bank);
        }

        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = repo_Bank.All().FirstOrDefault(p => p.Id == Id);
            if (data == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(repo.All().Where(p => p.isDelete == false), "Id", "客戶名稱", data.客戶Id);
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(int Id, 客戶銀行資訊 Bank)
        {
            if (ModelState.IsValid)
            {
                var data = repo_Bank.All().FirstOrDefault(p => p.Id == Id);
                data.InjectFrom(Bank);
                //data.客戶Id = Bank.客戶Id;
                //data.銀行代碼 = Bank.銀行代碼;
                //data.銀行代碼 = Bank.銀行代碼;
                //data.分行代碼 = Bank.分行代碼;
                //data.帳戶名稱 = Bank.帳戶名稱;
                //data.帳戶號碼 = Bank.帳戶號碼;
                repo_Bank.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(repo.All().Where(p => p.isDelete == false), "Id", "客戶名稱");
            return View(Bank);
        }

        public ActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = repo_Bank.All().FirstOrDefault(p => p.Id == Id);
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
            var data = repo_Bank.All().FirstOrDefault(p => p.Id == Id);
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

            repo_Bank.UnitOfWork.Context.Configuration.ValidateOnSaveEnabled = false;

            if (ModelState.IsValid)
            {
                var data = repo_Bank.All().FirstOrDefault(p => p.Id == Id);
                data.isDelete = true;
                repo_Bank.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(Id);
        }
    }
}