using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Room_Booking.ViewModel
{
    public class CustomerVM:BaseModel
    {
        
        [DisplayName("Customer Name")]
        public string name { get; set; }
        
        [DisplayName("Customer Phone")]
        public string phone { get; set; }
        
        [DisplayName("Customer Email")]
        public string email { get; set; }
        
         
        
    }
}