using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebApplication.Controllers
{
    public static class VqiRequest
    {
        static HttpContextBase context = null;

        public static HttpContextBase Context
        {
            get
            {
                return context;
            }

            set
            {
                context = value;
            }
        }

        public static string GetQueryString(string key)
        {
            return context.Request.QueryString[key];
        }
    }
}
