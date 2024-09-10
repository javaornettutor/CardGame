using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Room_Booking;
using Room_Booking.Utils;
using Room_Booking.ViewModel;

namespace Room_Booking.Controllers
{
    public class BookingsController : BaseController
    {
        private DbEntities db = new DbEntities();
        private List<StaffBooking> lstBookings;
        private List<BookingVM> lstBookingsVM= new List<BookingVM>();
        private BookingVM newBooking = new BookingVM();
        
        // GET: Bookings        
        public ActionResult Index()
        {
            initResources();
            //db = new DbEntities();
            lstBookings = db.StaffBookings.OrderByDescending(u=>u.lastUpdated).ToList();
            foreach (StaffBooking item in lstBookings)
            {
               int intCheckOutCount = db.ClientCheckouts.Count(u=>u.bookingIdFk== item.Id);
               BookingVM bookingObj = ToVM(item);
               bookingObj.hasBeenCheckout =false;
               
                if(intCheckOutCount!=0)
                    bookingObj.hasBeenCheckout =true;

               lstBookingsVM.Add(bookingObj );
            }
            return View(lstBookingsVM);
        }

        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffBooking booking = db.StaffBookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            
            return View(ToVM(booking));
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {  
            newBooking.RoomSelectListItem= UtilClass.CreateCustomerListBox(db, db.Rooms.ToList<Object>(), -1, false);
            newBooking.CustomerSelectListItem = UtilClass.CreateCustomerListBox(db,db.Customers.ToList<Object>(), -1,true);
            return View(newBooking);
        }

        private bool initResources()
        { 
            ErrorMessage=string.Empty;
            newBooking.CustomerSelectListItem = UtilClass.CreateCustomerListBox(db,db.Customers.ToList<Object>(), -1,false);
            newBooking.RoomSelectListItem= UtilClass.CreateCustomerListBox(db, db.Rooms.ToList<Object>(), -1);
            //if(newBooking.CustomerSelectListItem.Count()==0)
                //ErrorMessage+="\nPlease create a new customer before make a booking";
            if(newBooking.RoomSelectListItem.Count()==0)
                ErrorMessage+="\nPlease create a new room before make a booking";
            ViewBag.ErrorMessage =ErrorMessage;
            
            return string.IsNullOrEmpty(ErrorMessage);
        }
        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "roomId,customerId,CheckInDate,CheckOutDate")] StaffBooking booking)
        {
            
            if (ModelState.IsValid)
            {
                booking.status=true;
                booking.lastUpdated= DateTime.Now;
                db.StaffBookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(booking);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffBooking booking = db.StaffBookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            
            newBooking= ToVM(booking);
            newBooking.CustomerSelectListItem = UtilClass.CreateCustomerListBox(db,db.Customers.ToList<Object>(), -1,true);
            newBooking.RoomSelectListItem = UtilClass.CreateCustomerListBox(db,db.Rooms.ToList<Object>(), -1,true);
            return View(newBooking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,roomId,customerId,CheckInDate,CheckOutDate,status")] StaffBooking booking)
        {
            if (ModelState.IsValid)
            {
                booking.lastUpdated= DateTime.Now;
                db.Entry(booking).State = EntityState.Modified;
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffBooking booking = db.StaffBookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(ToVM(booking));
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StaffBooking booking = db.StaffBookings.Find(id);
            if(booking==null)   return HttpNotFound();
            booking.lastUpdated= DateTime.Now;
            booking.status=false;            
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
        private BookingVM ToVM(StaffBooking item)
        {
            if(item==null) return new BookingVM();

            BookingVM  newBooking= new BookingVM ();            
            newBooking.Id= item.Id;
            Customer cust=db.Customers.SingleOrDefault(u=>u.Id == item.customerId);
            newBooking.customerName= "";
            newBooking.customerName= cust==null?string.Empty:cust.name;
            newBooking.CheckInDate= item.checkInDate;
            newBooking.CheckOutDate= item.checkOutDate;
               
            newBooking.customerId= item.customerId;
            newBooking.status= item.status;
            newBooking.roomId= item.roomId;
            
            Room curRoom=db.Rooms.SingleOrDefault(u=>u.Id == item.roomId);            
            newBooking.roomName =  curRoom==null?string.Empty:curRoom.name;
            
            return newBooking;
        }
    }
}
