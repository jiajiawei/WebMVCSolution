using BLL;
using DataModel.JsonResult;
using DataModel.Menu;
using IBLL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;

namespace WebApplication
{
    /// <summary>
    /// wxapi 的摘要说明
    /// </summary>
    public class wxapi : IHttpHandler
    {
        string token = string.Empty;
        string openId = string.Empty;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string postString = string.Empty;
            if (HttpContext.Current.Request.HttpMethod.ToUpper() == "POST")
            {
                using (Stream stream = HttpContext.Current.Request.InputStream)
                {
                    Byte[] postBytes = new Byte[stream.Length];
                    stream.Read(postBytes, 0, (Int32)stream.Length);
                    postString = Encoding.UTF8.GetString(postBytes);
                }

                if (!string.IsNullOrEmpty(postString))
                {
                    Execute(postString);
                }
            }
            else
            {
                Auth(); //微信接入的测试
            }
        }

        /// <summary>
        /// 处理各种请求信息并应答（通过POST的请求）
        /// </summary>
        /// <param name="postStr">POST方式提交的数据</param>
        private void Execute(string postStr)
        {
            //WeixinApiDispatch dispatch = new WeixinApiDispatch();
            //string responseContent = dispatch.Execute(postStr);

            //HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
            //HttpContext.Current.Response.Write(responseContent);
        }

        #region 验证服务器数据：Auth()
        /// <summary>
        /// 成为开发者的第一步，验证并相应服务器的数据
        /// </summary>
        private void Auth()
        {
            string token = ConfigurationManager.AppSettings["WeixinToken"];//从配置文件获取Token
            if (string.IsNullOrEmpty(token))
            {
                //LogTextHelper.Error(string.Format("WeixinToken 配置项没有配置！"));
            }

            string echoString = HttpContext.Current.Request.QueryString["echoStr"];
            string signature = HttpContext.Current.Request.QueryString["signature"];
            string timestamp = HttpContext.Current.Request.QueryString["timestamp"];
            string nonce = HttpContext.Current.Request.QueryString["nonce"];

            if (CheckSignature(token, signature, timestamp, nonce))
            {
                if (!string.IsNullOrEmpty(echoString))
                {
                    HttpContext.Current.Response.Write(echoString);
                    HttpContext.Current.Response.End();
                }
            }
        }


        /// <summary>
        /// 验证微信签名
        /// </summary>
        public bool CheckSignature(string token, string signature, string timestamp, string nonce)
        {
            string[] ArrTmp = { token, timestamp, nonce };

            Array.Sort(ArrTmp);
            string tmpStr = string.Join("", ArrTmp);

            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            tmpStr = tmpStr.ToLower();

            if (tmpStr == signature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        private void btnGetUsers_Click(object sender, EventArgs e)
        {
            IUserApi userBLL = new UserApi();
            List<string> userList = userBLL.GetUserList(token);
            foreach (string openId in userList)
            {
                UserJson userInfo = userBLL.GetUserDetail(token, openId);
                if (userInfo != null)
                {
                    string tips = string.Format("{0}:{1}", userInfo.nickname, userInfo.openid);
                    Console.WriteLine(tips);
                }
            }
        }

        private void btnGetGroupList_Click(object sender, EventArgs e)
        {
            IUserApi userBLL = new UserApi();
            List<GroupJson> list = userBLL.GetGroupList(token);
            foreach (GroupJson info in list)
            {
                string tips = string.Format("{0}:{1}", info.name, info.id);
                Console.WriteLine(tips);
            }
        }

        private void btnFindUserGroup_Click(object sender, EventArgs e)
        {
            IUserApi userBLL = new UserApi();
            int groupId = userBLL.GetUserGroupId(token, openId);

            string tips = string.Format("GroupId:{0}", groupId);
            Console.WriteLine(tips);
        }

        private void btnCreateGroup_Click(object sender, EventArgs e)
        {
            IUserApi userBLL = new UserApi();
            GroupJson info = userBLL.CreateGroup(token, "创建测试分组");
            if (info != null)
            {
                string tips = string.Format("GroupId:{0} GroupName:{1}", info.id, info.name);
                Console.WriteLine(tips);

                string newName = "创建测试修改";
                CommonResult result = userBLL.UpdateGroupName(token, info.id, newName);
                Console.WriteLine("修改分组名称：" + (result.Success ? "成功" : "失败:" + result.ErrorMessage));
            }
        }

        private void btnUpdateGroup_Click(object sender, EventArgs e)
        {
            int groupId = 111;
            string newName = "创建测试修改";

            IUserApi userBLL = new UserApi();
            CommonResult result = userBLL.UpdateGroupName(token, groupId, newName);
            Console.WriteLine("修改分组名称：" + (result.Success ? "成功" : "失败:" + result.ErrorMessage));
        }

        private void btnMoveToGroup_Click(object sender, EventArgs e)
        {
            int togroup_id = 111;//输入分组ID

            if (togroup_id > 0)
            {
                IUserApi userBLL = new UserApi();
                CommonResult result = userBLL.MoveUserToGroup(token, openId, togroup_id);

                Console.WriteLine("移动用户分组名称：" + (result.Success ? "成功" : "失败:" + result.ErrorMessage));
            }
        }

        private void btnGetMenuJson_Click(object sender, EventArgs e)
        {
            IMenuApi menuBLL = new MenuApi();
            MenuJson menu = menuBLL.GetMenu(token);
            if (menu != null)
            {
                Console.WriteLine(menu.ToJson());
            }
        }

        private void btnCreateMenu_Click(object sender, EventArgs e)
        {
            MenuInfo productInfo = new MenuInfo("软件产品", new MenuInfo[] {
                new MenuInfo("病人资料管理系统", ButtonType.click, "patient"),
                new MenuInfo("客户关系管理系统", ButtonType.click, "crm"),
                new MenuInfo("酒店管理系统", ButtonType.click, "hotel"),
                new MenuInfo("送水管理系统", ButtonType.click, "water")
            });

            MenuInfo frameworkInfo = new MenuInfo("框架产品", new MenuInfo[] {
                new MenuInfo("Win开发框架", ButtonType.click, "win"),
                new MenuInfo("WCF开发框架", ButtonType.click, "wcf"),
                new MenuInfo("混合式框架", ButtonType.click, "mix"),
                new MenuInfo("Web开发框架", ButtonType.click, "web"),
                new MenuInfo("代码生成工具", ButtonType.click, "database2sharp")
            });

            MenuInfo relatedInfo = new MenuInfo("相关链接", new MenuInfo[] {
                new MenuInfo("公司介绍", ButtonType.click, "Event_Company"),
                new MenuInfo("官方网站", ButtonType.view, "http://www.iqidi.com"),
                new MenuInfo("提点建议", ButtonType.click, "Event_Suggestion"),
                new MenuInfo("联系客服", ButtonType.click, "Event_Contact"),
                new MenuInfo("发邮件", ButtonType.view, "http://mail.qq.com/cgi-bin/qm_share?t=qm_mailme&email=S31yfX15fn8LOjplKCQm")
            });

            MenuJson menuJson = new MenuJson();
            menuJson.button.AddRange(new MenuInfo[] { productInfo, frameworkInfo, relatedInfo });

            //Console.WriteLine(menuJson.ToJson());

            //if (MessageUtil.ShowYesNoAndWarning("您确认要创建菜单吗") == System.Windows.Forms.DialogResult.Yes)
            //{
            //    IMenuApi menuBLL = new MenuApi();
            //    CommonResult result = menuBLL.CreateMenu(token, menuJson);
            //    Console.WriteLine("创建菜单：" + (result.Success ? "成功" : "失败:" + result.ErrorMessage));
            //}
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }


    static partial class Object
    {
        /// <summary>
        /// 把对象为json字符串
        /// </summary>
        /// <param name="obj">待序列号对象</param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
    }
}