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
    public class BaseController : Controller
    {
        public string ErrorMessage { get; set; }
    }
}
