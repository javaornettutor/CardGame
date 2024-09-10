using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Room_Booking.Constants
{
     /***
     * The class will be responsible for all the setting required for the entired website
     * which could be reading off the web.config or any constants website required     
     * @author - William Wang
     */
    public class App_Constants
    {   
        public static string APP_CONFIG_SETTING_MISSING="Configuration Data {0} is missing";
        public static string APP_NAME=getConfigSettings("APP_NAME");
        public static string APP_COMPANY_NAME=getConfigSettings("COMPANY_NAME");        
        
       
        // this class acts as a internal class which maps to web.config
        public static string getConfigSettings(String key)
        {
            try
            {
                string result= ConfigurationManager.AppSettings[key];
                return String.IsNullOrEmpty(result)? string.Format(App_Constants.APP_CONFIG_SETTING_MISSING,key): result;
            }
            catch (Exception ex)
            {

                return ex.Message;
            }

           
        }
    }
}