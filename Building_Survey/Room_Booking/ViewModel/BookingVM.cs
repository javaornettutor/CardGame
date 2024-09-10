using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Room_Booking.ViewModel
{
    public class BookingVM: BaseModel
    {
        [DisplayName("Room ID")]
        public int roomId { get; set; }
        
        [DisplayName("Room Name")]
        public string roomName { get; set; }
        
        public IEnumerable<SelectListItem> RoomSelectListItem { get; set; }



        [DisplayName("Customer")]
        public int customerId { get; set; }
        
        [DisplayName("Customer Name")]
        public string customerName{ get; set; }       
        public IEnumerable<SelectListItem> CustomerSelectListItem { get; set; }
       

        [DisplayName("Check In Date")]
        public DateTime CheckInDate{ get; set; }       

        [DisplayName("Check Out Date")]
        public DateTime CheckOutDate{ get; set; }       

        [DisplayName("hasBeenCheckout")]
        public bool hasBeenCheckout { get; set; }       

    }
}