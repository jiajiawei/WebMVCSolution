using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.JsonResult
{
    public class CommonResult : BaseJsonResult
    {
        public object ErrorMessage { get; set; }
        public bool Success { get; set; }
    }
}
