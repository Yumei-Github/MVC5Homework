using MVC5Homework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVC5Homework.Controllers
{
    public class ReportsController : Controller
    {
        private 客戶資料Entities db = new 客戶資料Entities();
        public ReportsController()
        {

        }
        // GET: Reports
        public ActionResult Index()
        {
            return View(db.客戶報表);
        }
    }
}