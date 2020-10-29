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
    public class 客戶聯絡人Controller : Controller
    {
        //private 客戶資料Entities db = new 客戶資料Entities();

        客戶資料Repository repo;
        客戶聯絡人Repository repo_Contact;

        public 客戶聯絡人Controller()
        {
            repo = RepositoryHelper.Get客戶資料Repository();
            repo_Contact = RepositoryHelper.Get客戶聯絡人Repository(repo.UnitOfWork);
        }

        // GET: 客戶聯絡人
        public ActionResult Index()
        {
            return View(repo_Contact.All().Where(p => p.isDelete == false));
        }

        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(repo.All().Where(p => p.isDelete == false), "Id", "客戶名稱");
            return View();
        }

        [HttpPost]
        public ActionResult Create(客戶聯絡人 Contact)
        {
            if (ModelState.IsValid)
            {
                repo_Contact.Add(Contact);
                repo_Contact.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(repo.All().Where(p => p.isDelete == false), "Id", "客戶名稱");

            return View(Contact);
        }

        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = repo_Contact.All().FirstOrDefault(p => p.Id == Id);
            if (data == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(repo.All().Where(p => p.isDelete == false), "Id", "客戶名稱", data.客戶Id);
            return View(data);
        }


        [HttpPost]
        public ActionResult Edit(int Id, 客戶聯絡人 Contact)
        {
            if (ModelState.IsValid)
            {
                var data = repo_Contact.All().FirstOrDefault(p=>p.Id== Id);
                data.InjectFrom(Contact);
                //data.客戶Id = Contact.客戶Id;
                //data.職稱 = Contact.職稱;
                //data.姓名 = Contact.姓名;
                //data.Email = Contact.Email;
                //data.手機 = Contact.手機;
                //data.電話 = Contact.電話;
                repo_Contact.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(repo.All().Where(p => p.isDelete == false), "Id", "客戶名稱");
            return View(Contact);
        }

        public ActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = repo_Contact.All().FirstOrDefault(p => p.Id == Id);
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
            var data = repo_Contact.All().FirstOrDefault(p => p.Id == Id);
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
            //    var data = db.客戶聯絡人.Find(Id);
            //    db.客戶聯絡人.Remove(data);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            repo_Contact.UnitOfWork.Context.Configuration.ValidateOnSaveEnabled = false;

            if (ModelState.IsValid)
            {
                var data = repo_Contact.All().FirstOrDefault(p => p.Id == Id);
                data.isDelete  = true;
                repo_Contact.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }


            return View(Id);
        }
    }
}