using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class EmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Employees
        public ActionResult Index(Employee emp, string sortOrder)
        {


            if (emp.ApplicationId == null)
            {
                emp.ApplicationId = User.Identity.GetUserId();
                Employee employee = db.Employees.Find(emp.ApplicationId);
                emp = employee;
            }
            if (emp == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //List<Customer> custList = new List<Customer>();

            //var cust = (from c in db.Customers
            //            where c.ZipCode == emp.ZipCode
            //            select c).ToList();

            //return View(cust);











            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var customers = from c in db.Customers
                            where c.ZipCode == emp.ZipCode
                           select c;
            switch (sortOrder)
            {
                case "date_desc":
                    customers = customers.OrderByDescending(c => c.PickupDay);
                    break;
                default:
                    customers = customers.OrderBy(s => s.LastName);
                    break;
            }
            return View(customers.ToList());





        }

        // GET: Employees/Details/5
        public ActionResult Details(Employee emp)
        {

            if (emp.ApplicationId == null)
            {
                emp.ApplicationId = User.Identity.GetUserId();
                Employee employee = db.Employees.Find(emp.ApplicationId);
                emp = employee;
            }
            if (emp == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (emp == null)
            {
                return HttpNotFound();
            }
            return View(emp);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Email,Password,UserName,ZipCode,Role")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.Role = "Employee";
                employee.ApplicationId = User.Identity.GetUserId();
                db.Employees.Add(employee);
                var transaction = db.Database.BeginTransaction();
                db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Employees ON;");
                db.SaveChanges();
                db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Employees OFF;");
                transaction.Commit();
                return RedirectToAction("Details", employee);
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id, Employee emp)
        {
            if (emp.ApplicationId == null)
            {
                emp.ApplicationId = User.Identity.GetUserId();

                Employee employee = db.Employees.Find(emp.ApplicationId);
                emp = employee;
            }
            if (emp == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (emp == null)
            {
                return HttpNotFound();
            }
            return View(emp);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Email,Password,UserName,ZipCode")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                var transaction = db.Database.BeginTransaction();
                db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Employees ON;");
                db.SaveChanges();
                db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Employees OFF;");
                transaction.Commit();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public ActionResult Complete(string id)
        {
            Customer customer1 = db.Customers.Find(id);
            customer1.Balance += 10;
            db.Entry(customer1).State = EntityState.Modified;
            var transaction = db.Database.BeginTransaction();
            db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Customers ON;");
            db.SaveChanges();
            db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Customers OFF;");
            transaction.Commit();
            return RedirectToAction("Index");
        }


    }
}
