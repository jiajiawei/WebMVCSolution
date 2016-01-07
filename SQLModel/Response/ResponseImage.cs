using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.Response
{
    /// <summary>
    /// 回复图片消息
    /// </summary>
    [System.Xml.Serialization.XmlRoot(ElementName = "xml")]
    public class ResponseImage : BaseMessage
    {
        public ResponseImage()
        {
            this.MsgType = ResponseMsgType.Image.ToString().ToLower();
        }

        public ResponseImage(BaseMessage info) : this()
        {
            this.FromUserName = info.ToUserName;
            this.ToUserName = info.FromUserName;
        }

        /// <summary>
        /// 内容
        /// </summary>        
        public string Content { get; set; }
    }
}
