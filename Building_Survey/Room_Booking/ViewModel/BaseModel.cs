using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Room_Booking.ViewModel
{
    public class BaseModel
    {
        public int Id { get; set; }

        [DisplayName("Is Active")]        
        public Nullable<bool> status { get; set; }
        
        public bool IsValid{ get; set; }
    }
}