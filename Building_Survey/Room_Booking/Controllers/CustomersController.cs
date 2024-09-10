using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Room_Booking;
using Room_Booking.ViewModel;

namespace Room_Booking.Controllers
{
    public class CustomersController : Controller
    {
        private DbEntities db = new DbEntities();
        private List<Customer> lstCust;
        private List<CustomerVM> lstCustVM= new List<CustomerVM>();
        // GET: Customers
        public ActionResult Index()
        {
            lstCust = db.Customers.ToList();
            foreach (Customer item in lstCust)
            {
               lstCustVM.Add(ToVM(item));
            }
            
            return View(lstCustVM);
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
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
            return View(ToVM(customer));
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
        public ActionResult Create([Bind(Include = "Id,name,phone,email")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.status=true;
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
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
            return View(ToVM(customer));
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name,phone,email,checkInDate,status")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }
        

        
        public ActionResult Checkout()
        {
            return View();
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
            return View(ToVM(customer));
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
        private CustomerVM ToVM(Customer item)
        {
            CustomerVM  newCustomer= new CustomerVM ();            
            newCustomer.Id= item.Id;
            newCustomer.name= item.name;
            newCustomer.email= item.email;
            newCustomer.phone= item.phone;            
            newCustomer.status= item.status;
            return newCustomer;
        }



    }
}
