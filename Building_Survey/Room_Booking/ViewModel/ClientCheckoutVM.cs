using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Room_Booking.ViewModel
{
    public class ClientCheckoutVM:BaseModel
    {   
        public StaffBooking CurrentBookingsDetails{ get; set; }

    }
}