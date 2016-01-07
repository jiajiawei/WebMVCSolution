using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL;
using System.Xml;
using DataModel;
using DataModel.Send;
using System.Data.Entity;
using System.Linq.Expressions;
using DataModel.Response;

namespace DAL
{
    public class WeiXin:IWeiXin
    {

        /// <summary>
        /// 订阅或者显示公司信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private string ShowCompanyInfo(BaseMessage info)
        {
            string result = "";
            //使用在微信平台上的图文信息(单图文信息)
            ResponseNews response = new ResponseNews(info);
            ArticleEntity entity = new ArticleEntity();
            entity.Title = "广州爱奇迪软件科技有限公司";
            entity.Description = "欢迎关注广州爱奇迪软件--专业的单位信息化软件和软件开发框架提供商，我们立志于为客户提供最好的软件及服务。\r\n";
            entity.Description += "我们是一家极富创新性的软件科技公司，从事研究、开发并销售最可靠的、安全易用的技术产品及优质专业的服务，帮助全球客户和合作伙伴取得成功。\r\n......（此处省略1000字，哈哈）";
            entity.PicUrl = "http://www.iqidi.com/WeixinImage/company.png";
            entity.Url = "http://www.iqidi.com";

            response.Articles.Add(entity);
            result = response.ToXml();

            return result;
        }


        public string SendText(SText model)
        {
            return string.Format(@"<xml>
                        <ToUserName><![CDATA[{0}]]></ToUserName>
                        <FromUserName><![CDATA[{1}]]></FromUserName>
                        <CreateTime>{2}</CreateTime>
                        <MsgType><![CDATA[{3}]]></MsgType>
                        <Content><![CDATA[{4}]]></Content>
                     </xml>",model.ToUserName,model.FromUserName,model.CreateTime,model.MsgType,model.Content);
        }



        public string SendImg(SImg model)
        {
            return string.Format(@"<xml>
                                    <ToUserName><![CDATA[{0}]]></ToUserName>
                                    <FromUserName><![CDATA[{1}]]></FromUserName>
                                    <CreateTime>{2}</CreateTime>
                                    <MsgType><![CDATA[{3}]]></MsgType>
                                    <Image>
                                    <MediaId><![CDATA[{4}]]></MediaId>
                                    </Image>
                                    </xml>", model.ToUserName, model.FromUserName, model.CreateTime, model.MsgType, model.MediaId);
        }

        public string SendVoice(SVoice model)
        {
            return string.Format(@"<xml>
                                    <ToUserName><![CDATA[{0}]]></ToUserName>
                                    <FromUserName><![CDATA[{1}]]></FromUserName>
                                    <CreateTime>{2}</CreateTime>
                                    <MsgType><![CDATA[{3}]]></MsgType>
                                    <Voice>
                                    <MediaId><![CDATA[{4}]]></MediaId>
                                    </Voice>
                                    </xml>", model.ToUserName, model.FromUserName, model.CreateTime, model.MsgType, model.MediaId);
        }

        public string SendVideo(SVideo model)
        {
            return string.Format(@"<xml>
                                    <ToUserName><![CDATA[{0}]]></ToUserName>
                                    <FromUserName><![CDATA[{1}]]></FromUserName>
                                    <CreateTime>{2}</CreateTime>
                                    <MsgType><![CDATA[{3}]]></MsgType>
                                    <Video>
                                    <MediaId><![CDATA[{4}]]></MediaId>
                                    <Title><![CDATA[{5}]]></Title>
                                    <Description><![CDATA[{6}]]></Description>
                                    </Video> 
                                    </xml>", model.ToUserName, model.FromUserName, model.CreateTime, model.MsgType, model.MediaId,model.Title,model.Description);
        
        }

        public string SendMusic(Smusic model)
        {
            return string.Format(@"<xml>
                                    <ToUserName><![CDATA[{0}]]></ToUserName>
                                    <FromUserName><![CDATA[{1}]]></FromUserName>
                                    <CreateTime>{2}</CreateTime>
                                    <MsgType><![CDATA[{3}]]></MsgType>
                                    <Music>
                                    <Title><![CDATA[{4}]]></Title>
                                    <Description><![CDATA[{5}]]></Description>
                                    <MusicUrl><![CDATA[{6}]]></MusicUrl>
                                    <HQMusicUrl><![CDATA[{7}]]></HQMusicUrl>
                                    <ThumbMediaId><![CDATA[{8}]]></ThumbMediaId>
                                    </Music>
                                    </xml>", model.ToUserName, model.FromUserName,
                                           model.CreateTime, model.MsgType,
                                           model.Title, model.Description,
                                           model.MusicURL,model.HQMusicUrl,model.ThumbMediaId);
        
        
        }

        public string SendNews(SNews model)
        {
            string art="";
            foreach(var one in model.Articles)
            {

                art+=string.Format(@"
                                    <item>
                                    <Title><![CDATA[{0}]]></Title>
                                    <Description><![CDATA[{1}]]></Description>
                                    <PicUrl><![CDATA[{2}]]></PicUrl>
                                    <Url><![CDATA[{3}]]></Url>
                                    </item>",one.Title,one.Description,one.PicUrl,one.Url);
                                    
            
            }

            return string.Format(@"<xml>
                                    <ToUserName><![CDATA[{0}]]></ToUserName>
                                    <FromUserName><![CDATA[{1}]]></FromUserName>
                                    <CreateTime>{2}</CreateTime>
                                    <MsgType><![CDATA[{3}]]></MsgType>
                                    <ArticleCount>{4}</ArticleCount>
                                    <Articles>{5}</Articles>
                                    </xml> ",model.ToUserName,model.FromUserName,model.CreateTime,model.MsgType,
                                            model.ArticleCount,art);
        }


        
    }
}
