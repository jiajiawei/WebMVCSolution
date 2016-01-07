using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.Response
{
    /// <summary>
    /// 客服消息
    /// </summary>
    [System.Xml.Serialization.XmlRoot(ElementName = "xml")]
    public class ResponseCustomer : BaseMessage
    {

        public ResponseCustomer()
        {
            this.MsgType = ResponseMsgType.transfer_customer_service.ToString().ToLower();
        }

        public ResponseCustomer(BaseMessage info) : this()
        {
            this.FromUserName = info.ToUserName;
            this.ToUserName = info.FromUserName;
        }
    }
}
