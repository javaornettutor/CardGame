using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Room_Booking.ViewModel
{
    public class RoomVM:BaseModel
    {
        [DisplayName("Room Name")]
        public string name { get; set; }        
    }
}