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
    public class CheckoutController : Controller
    {
        private DbEntities db = new DbEntities();
        private ClientCheckoutVM vm= new ClientCheckoutVM();

        
        [HttpGet]
        public ActionResult ClientCheckout(int bookingId)
        {
            ClientCheckoutVM vm =new ClientCheckoutVM();
            vm.CurrentBookingsDetails = db.StaffBookings.SingleOrDefault(u=>u.Id== bookingId);           
            return View(vm);
        }
       [HttpPost]
        public ActionResult ClientCheckout([Bind(Include = "Id")] ClientCheckoutVM curObj)
        {
            ClientCheckout clientCkOutObj= new ClientCheckout();
            clientCkOutObj.checkoutDate= DateTime.Now;
            clientCkOutObj.bookingIdFk= curObj.Id;
            
            db.ClientCheckouts.Add(clientCkOutObj);
            
            StaffBooking curBooking = db.StaffBookings.SingleOrDefault(u=>u.Id==curObj.Id);
            curBooking.status=false;
            db.SaveChanges();
            return RedirectToAction("Index", "Bookings");
        }

       
    }
}
