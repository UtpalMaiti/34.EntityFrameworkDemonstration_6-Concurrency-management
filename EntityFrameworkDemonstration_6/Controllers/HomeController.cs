using EntityFrameworkDemonstration_6.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EntityFrameworkDemonstration_6.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            PragimDBEntities dbContext = new PragimDBEntities();

            return View(dbContext.EmpInfoes.ToList());
        }

        //Concurrency:   management
        public ActionResult EditEmployee(int Id)
        {
            PragimDBEntities dbContext = new PragimDBEntities();

            var employee = dbContext.EmpInfoes.Find(Id);

            bool isLocked = employee.IsLocked ?? false;

            ViewBag.IsLocked = isLocked;

            if (!isLocked)
            {
                employee.IsLocked = true;

                dbContext.SaveChanges();
            }
            return View(employee);
        }

        [HttpPost]
        public ActionResult EditEmployee(EmpInfo emp)
        {
            PragimDBEntities dbContext = new PragimDBEntities();

            dbContext.Entry(emp).State = System.Data.Entity.EntityState.Modified;

            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}