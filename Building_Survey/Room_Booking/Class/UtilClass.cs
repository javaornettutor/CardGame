using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Room_Booking.Utils
{
    public class UtilClass
    {
        public static IEnumerable<SelectListItem> CreateCustomerListBox(DbEntities db,List<Object> customers, int curId, bool returnAll= false)
        {
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            foreach (Object item in customers)
            {
                if(item is Customer)
                { 
                    
                    Customer custObj =  (Customer)item;
                    if(!custObj.status.Value) continue;
                    // if current customer has no booking
                    if(db.StaffBookings.Count(u=>u.customerId==custObj.Id && u.status.Value==true)==0 || returnAll)
                    {
                        if(curId==custObj.Id)
                            listSelectListItems.Add(new SelectListItem() { Text = custObj.name, Value = custObj.Id.ToString() });
                        else
                            listSelectListItems.Add(new SelectListItem() { Text = custObj.name, Selected= true , Value = custObj.Id.ToString() });    
                    }
                }
                else if(item is Room)
                { 
                    Room roomObj =  (Room)item;
                    if(!roomObj.status.Value) continue;
                    if(db.StaffBookings.Count(u=>u.roomId==roomObj.Id && u.status.Value)==0 || returnAll)
                    {  
                        if(curId==roomObj.Id)
                            listSelectListItems.Add(new SelectListItem() { Text = roomObj.name, Value = roomObj.Id.ToString() });
                        else
                            listSelectListItems.Add(new SelectListItem() { Text = roomObj.name, Selected= true , Value = roomObj.Id.ToString() });    
                    }
                }

                
            }
            return listSelectListItems;
        }
    }
}