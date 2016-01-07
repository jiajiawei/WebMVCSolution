using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.JsonResult
{
    public class CreateGroupResult : BaseJsonResult
    {
        public GroupJson group { get; set; }
    }
}
