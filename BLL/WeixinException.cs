using System;
using System.Runtime.Serialization;

namespace BLL
{
    [Serializable]
    internal class WeixinException : Exception
    {
        public WeixinException()
        {
        }

        public WeixinException(string message) : base(message)
        {
        }

        public WeixinException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WeixinException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}