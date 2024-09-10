


using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Room_Booking.Helper
{    
    public static class HTMLHelperExtensions
    {
        private static string html ="";
        public static IHtmlString ShowYesOrNot(bool yesOrNo)
        {
            string filename= yesOrNo?"tick.png":"cross.png";
            html = "<img src='/Content/Image/" + filename +"' width='20px' height='20px'/>";
            return MvcHtmlString.Create(html);            
        }

     

        public static MvcHtmlString DisplayErrorMessag(string msg)
        {
           
            return MvcHtmlString.Create(msg);
        }
        public static IHtmlString SubmitLink(string text, string formId)
        {
            return MvcHtmlString.Create(string.Format("<a class='k-button' onclick='$(\"#{1}\").submit();'>{1}</a>", text, formId));
        }

    }
}
 