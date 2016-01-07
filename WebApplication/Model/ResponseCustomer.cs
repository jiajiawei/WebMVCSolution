using DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace WebApplication.Model
{
    /// <summary>
    /// 客服消息
    /// </summary>
    public class Customer : BaseMessage
    {

        [Key]
        public int id { get; set; }
    }
}
