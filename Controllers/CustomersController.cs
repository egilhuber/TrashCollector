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
using System.Security.Claims;


namespace TrashCollector.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Customers
        public ActionResult Index()
        {
            return View();
        }

        // GET: Customers/Details/5
        public ActionResult Details(Customer cust)
        {
            //var id = User.Identity.GetUserId();
            ////select * from customers where applicationid = id
            if (cust.ApplicationId == null)
            {
                cust.ApplicationId = User.Identity.GetUserId();
                
                Customer customer1 = db.Customers.Find(cust.ApplicationId); 
                cust = customer1;
            }
            if (cust == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Customer customer = db.Customers.Find(cust.Id);
            if (cust == null)
            {
                return HttpNotFound();
            }
            return View(cust);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Email,Password,UserName,StreetAddress,City,State,ZipCode,PickupDay,StartSuspend,EndSuspend,Balance,Role")] Customer customer)
        {

            if (ModelState.IsValid)
            {
                var transaction = db.Database.BeginTransaction();
                customer.Role = "Customer";
                customer.ApplicationId = User.Identity.GetUserId();
                db.Customers.Add(customer);
                db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Customers ON;");
                db.SaveChanges();
                db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Customers OFF;");
                transaction.Commit();
                return RedirectToAction("Details", (customer));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Email,Password,UserName,StreetAddress,City,State,ZipCode,PickupDay,StartSuspend,EndSuspend,Balance,AppliationId,PickupDate")]  Customer cust)
        {

            if (cust.ApplicationId == null)
            {
                cust.ApplicationId = User.Identity.GetUserId();

                Customer customer1 = db.Customers.Find(cust.ApplicationId); 
                cust = customer1;
            }
            if (cust == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Customer customer = db.Customers.Find(cust.Id);
            if (cust == null)
            {
                return HttpNotFound();
            }
            return View(cust);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? i, Customer customer)
        {
            if (ModelState.IsValid)
            {
                
                db.Entry(customer).State = EntityState.Modified;
                var transaction = db.Database.BeginTransaction();
                db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Customers ON;");
                db.SaveChanges();
                db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Customers OFF;");
                transaction.Commit();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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




    }
}
