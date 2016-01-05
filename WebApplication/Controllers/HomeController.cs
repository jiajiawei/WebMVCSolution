using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 验证url权限， 接入服务器
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool ValidUrl(string token)
        {
            Common.WriteText("test");
            string echoStr = VqiRequest.GetQueryString("echoStr");
            if (CheckSignature(token))
            {
                if (!string.IsNullOrEmpty(echoStr))
                {
                    Common.WriteText(echoStr);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 验证微信签名
        /// </summary>
        /// * 将token、timestamp、nonce三个参数进行字典序排序
        /// * 将三个参数字符串拼接成一个字符串进行sha1加密
        /// * 开发者获得加密后的字符串可与signature对比，标识该请求来源于微信。
        /// <returns></returns>
        public static bool CheckSignature(string token)
        {
            string signature = VqiRequest.GetQueryString("signature");
            string timestamp = VqiRequest.GetQueryString("timestamp");
            string nonce = VqiRequest.GetQueryString("nonce");
            string[] ArrTmp = { token, timestamp, nonce };
            Array.Sort(ArrTmp);     //字典排序
            string tmpStr = string.Join("", ArrTmp);
            //tmpStr = Utils.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            //tmpStr = tmpStr.ToLower();
            if (tmpStr == signature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public ActionResult Index()
        {
            VqiRequest.Context = HttpContext;



            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}