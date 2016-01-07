using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.JsonResult
{
    public class ErrorJsonResult : BaseJsonResult
    {
        public ReturnCode errcode { get; set; }
        public object errmsg { get; set; }
    }
}
