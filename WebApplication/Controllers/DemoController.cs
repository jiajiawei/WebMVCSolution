using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class DemoController : Controller
    {
        /// <summary>
        /// http://localhost:1847/Demo/ContentResultDemo
        /// </summary>
        /// <returns></returns>
        public ActionResult ContentResultDemo()
        {
            string contentString = "ContextResultDemo! 请查看 Controllers/DemoController.cs文件,里面包含所有类型ActionResult的用法.";
            return Content(contentString);
        }

        /// <summary>
        /// http://localhost:1847/Demo/EmptyResultDemo
        /// </summary>
        /// <returns>返回一个空的结果</returns>
        public ActionResult EmptyResultDemo()
        {
            return new EmptyResult(); //空方法
        }

        /// <summary>
        /// http://localhost:1847/Demo/FileContentResultDemo
        /// </summary>
        /// <returns>将一个字节数组作为一个文件返回到前台</returns>
        public ActionResult FileContentResultDemo()
        {
            //初始化一个文件流（即：创建一个文件流）
            FileStream fs = new FileStream(Server.MapPath(@"/resource/Images/1.gif"), FileMode.Open, FileAccess.Read);

            //定义一个数组，这个数组的长度就是文件流的长度
            byte[] buffer = new byte[Convert.ToInt32(fs.Length)];

            //通过文件流fs,将1.gif这个文件读到buffer数组中
            fs.Read(buffer, 0, Convert.ToInt32(fs.Length));
            return File(buffer, @"image/gif");
        }

        /// <summary>
        /// http://localhost:1847/Demo/FilePathResultDemo
        /// </summary>
        /// <returns>将一个文件路径返回到前台</returns>
        public ActionResult FilePathResultDemo()
        {
            //可以将一个jpg格式的图像输出为gif格式
            return File(Server.MapPath(@"/resource/Images/2.jpg"), @"image/gif");
        }

        /// <summary>
        /// http://localhost:1847/Demo/FileStreamResultDemo
        /// </summary>
        /// <returns>将一个文件的流返回到前台</returns>
        public ActionResult FileStreamResultDemo()
        {
            FileStream fs = new FileStream(Server.MapPath(@"/resource/Images/1.gif"), FileMode.Open, FileAccess.Read);
            return File(fs, @"image/gif");
        }

        /// <summary>
        /// http://localhost:1847/Demo/HttpUnauthorizedResultDemo
        /// </summary>
        /// <returns>返回一个未验证的；401</returns>
        public ActionResult HttpUnauthorizedResultDemo()
        {
            return new HttpUnauthorizedResult();
        }

        /// <summary>
        /// http://localhost:1847/Demo/JavaScriptResultDemo
        /// </summary>
        /// <returns></returns>
        public ActionResult JavaScriptResultDemo()
        {
            return JavaScript(@"alert(""Test JavaScriptResultDemo!"")");
        }

        /// <summary>
        /// http://localhost:1847/Demo/JsonResultDemo
        /// </summary>
        /// <returns>返回一个Json的结果</returns>
        public ActionResult JsonResultDemo()
        {
            var tempObj = new { Controller = "DemoController", Action = "JsonResultDemo" };
            return Json(tempObj);
        }

        /// <summary>
        /// http://localhost:1847/Demo/RedirectResultDemo
        /// </summary>
        /// <returns></returns>
        public ActionResult RedirectResultDemo()
        {
            return Redirect(@"http://localhost:1847/Demo/ContentResultDemo");
        }

        /// <summary>
        /// http://localhost:1847/Demo/RedirectToRouteResultDemo
        /// </summary>
        /// <returns></returns>
        public ActionResult RedirectToRouteResultDemo()
        {
            return RedirectToAction(@"FileStreamResultDemo");
        }

        /// <summary>
        /// http://localhost:1847/Demo/PartialViewResultDemo
        /// </summary>
        /// <returns></returns>
        public ActionResult PartialViewResultDemo()
        {

            return PartialView();
        }

        /// <summary>
        /// http://localhost:1847/Demo/ViewResultDemo
        /// </summary>
        /// <returns></returns>
        public ActionResult ViewResultDemo()
        {
            //如果没有传入View名称, 默认寻找与Action名称相同的View页面.
            return View();
        }

    }
}
