using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBLL;
using DataModel;
using DataModel.Send;
using Common;
using IDAL;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;


namespace BLL
{
    public class DoWei : IDoWei
    {
        public IDAL.IWeiXin DALWei = new DAL.WeiXin();

        /// <summary>
        /// 获取每次操作微信API的Token访问令牌
        /// </summary>
        /// <param name="appid">应用ID</param>
        /// <param name="secret">开发者凭据</param>
        /// <returns></returns>
        public string GetAccessToken(string appid, string secret)
        {
            //正常情况下access_token有效期为7200秒,这里使用缓存设置短于这个时间即可
            //string access_token = MemoryCacheHelper.GetCacheItem<string>("access_token", delegate ()
            //{
            //    string grant_type = "client_credential";
            //    var url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type={0}&appid={1}&secret={2}", grant_type, appid, secret);

            //    HttpHelper helper = new HttpHelper();
            //    string result = helper.GetHtml(url);
            //    string regex = "\"access_token\":\"(?<token>.*?)\"";
            //    string token = CRegex.GetText(result, regex, "token");
            //    return token;
            //},
            //    new TimeSpan(0, 0, 7000)//7000秒过期
            //);

            return string.Empty;// access_token;
        }

        //---------------------------------------------------------------------------------
        #region 接收普通消息
        //接收到文本消息  处理
        public void DoText(Dictionary<string, string> model)
        {

            SText mT = new SText();
            string text = ReadXml.ReadModel("Content", model).Trim();
            mT.FromUserName = ReadXml.ReadModel("ToUserName", model);
            mT.ToUserName = ReadXml.ReadModel("FromUserName", model);
            mT.CreateTime = long.Parse(ReadXml.ReadModel("CreateTime", model));
            if (text == "?" || text == "？" || text == "帮助")
            {
                mT.Content = mT.Content = ReadXml.Menu();
                mT.MsgType = "text";
                ReadXml.ResponseToEnd(DALWei.SendText(mT));
            }
            else
            {


                SNews mN = new SNews();
                mN.FromUserName = ReadXml.ReadModel("ToUserName", model);
                mN.ToUserName = ReadXml.ReadModel("FromUserName", model);
                mN.CreateTime = long.Parse(ReadXml.ReadModel("CreateTime", model));
                mN.MsgType = "news";

                //   以下为文章内容，  实际使用时，此处应该是一个跟数据库交互的方法，查询出文章
                //文章条数，  文章内容等   都应该由数据库查询出来的数据决定   这里测试，就模拟几条

                mN.ArticleCount = 5;
                List<ArticlesModel> listNews = new List<ArticlesModel>();
                for (int i = 0; i < 5; i++)
                {
                    ArticlesModel ma = new ArticlesModel();
                    ma.Title = "这是第" + (i + 1).ToString() + "篇文章";
                    ma.Description = "-描述-" + i.ToString() + "-描述-";
                    ma.PicUrl = "http://image6.tuku.cn/pic/wallpaper/dongwu/taipingniaogaoqingbizhi/s00" + (i + 1).ToString() + ".jpg";
                    ma.Url = "http://www.cnblogs.com/mochen/";
                    listNews.Add(ma);
                }
                mN.Articles = listNews;
                ReadXml.ResponseToEnd(DALWei.SendNews(mN));
            }
        }








        //接收到图片消息
        public void DoImg(Dictionary<string, string> model)
        {

        }

        //接收到语音消息
        public void DoVoice(Dictionary<string, string> model)
        {

        }
        //接收到视频消息
        public void DoVideo(Dictionary<string, string> model)
        {

        }


        //接收到地理位置信息
        public void DoLocation(Dictionary<string, string> model)
        {

        }
        //接收到链接消息
        public void DoLink(Dictionary<string, string> Model)
        {

        }
        #endregion
        //----------------------------------------------------------------------------

        //----------------------------------------------------------------------------
        #region 接收事件消息
        //普通关注
        public void DoOn(Dictionary<string, string> model)
        {
            SText mT = new SText();
            mT.Content = ReadXml.Menu();
            mT.FromUserName = ReadXml.ReadModel("ToUserName", model);
            mT.ToUserName = ReadXml.ReadModel("FromUserName", model);
            mT.MsgType = "text";
            mT.CreateTime = long.Parse(ReadXml.ReadModel("CreateTime", model));
            ReadXml.ResponseToEnd(DALWei.SendText(mT));
        }
        //取消关注
        public void DoUnOn(Dictionary<string, string> model)
        {

        }
        //未关注用户扫描二维码参数
        public void DoOnCode(Dictionary<string, string> model)
        {
            SText mT = new SText();
            mT.Content = ReadXml.Menu();
            mT.FromUserName = ReadXml.ReadModel("ToUserName", model);
            mT.ToUserName = ReadXml.ReadModel("FromUserName", model);
            mT.MsgType = "text";
            mT.CreateTime = long.Parse(ReadXml.ReadModel("CreateTime", model));
            ReadXml.ResponseToEnd(DALWei.SendText(mT));
        }
        //已经关注的用户扫描二维码参数
        public void DoSubCode(Dictionary<string, string> model)
        {
            SText mT = new SText();
            mT.Content = ReadXml.Menu();
            mT.FromUserName = ReadXml.ReadModel("ToUserName", model);
            mT.ToUserName = ReadXml.ReadModel("FromUserName", model);
            mT.MsgType = "text";
            mT.CreateTime = long.Parse(ReadXml.ReadModel("CreateTime", model));
            ReadXml.ResponseToEnd(DALWei.SendText(mT));
        }





        //用户提交地理位置
        public void DoSubLocation(Dictionary<string, string> Model)
        {
            throw new NotImplementedException();
        }
        //自定义菜单点击
        public void DoSubClick(Dictionary<string, string> Model)
        {
            throw new NotImplementedException();
        }
        //自定义菜单跳转
        public void DoSubView(Dictionary<string, string> Model)
        {
            throw new NotImplementedException();
        }

        #endregion
        //-------------------------------------------------------------------------------------






    }
}
