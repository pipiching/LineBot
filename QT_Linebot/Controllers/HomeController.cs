using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QT_Linebot.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace QT_Linebot.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            StreamReader r = new StreamReader(@"C:\Users\cliff_li\Source\Repos\QT_LineBot\QT_Linebot\appsetting.json");
            string json = r.ReadToEnd();
            r.Close();
            LineBotConfig Items = JsonConvert.DeserializeObject<LineBotConfig>(json);

            Pushobject pushobject = new Pushobject();
            pushobject.to = Items.UserID[0];
            List<Message> messages = new List<Message>();
            messages.Add(new Message() { type = "text", text = "Hello World" });
            pushobject.messages = messages;

            push_message(pushobject);

            return View();
        }

        [HttpPost]
        public string Index(Receiveobject receiveobject)
        {
            try
            {
                Replyobject replyobject = new Replyobject();
                replyobject.replyToken = receiveobject.events[0].replyToken;

                List<Message> messages = new List<Message>();
                messages.Add(new Message() { type = "text", text = receiveobject.events[0].message.text });
                replyobject.messages = messages;
                
                return reply_message(replyobject);
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public string reply_message(Replyobject replyobject)
        {            
            string url = "https://api.line.me/v2/bot/message/reply";
            string payload = JsonConvert.SerializeObject(replyobject);

            return Requests(url, payload);
        }
        public string push_message(Pushobject pushobject)
        {
            string url = "https://api.line.me/v2/bot/message/push";
            string payload = JsonConvert.SerializeObject(pushobject);

            return Requests(url, payload);
        }

        public string Requests(string url, string payload)
        {
            StreamReader r = new StreamReader(@"C:\Users\cliff_li\Source\Repos\QT_LineBot\QT_Linebot\appsetting.json");
            string json = r.ReadToEnd();
            r.Close();
            LineBotConfig Items = JsonConvert.DeserializeObject<LineBotConfig>(json);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.KeepAlive = true;
            request.ContentType = "application/json";
            request.Headers.Set("Authorization", $"Bearer {Items.LineBot.accessToken}");

            byte[] byteArray = Encoding.UTF8.GetBytes(payload);

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(byteArray, 0, byteArray.Length);
            }
            string returnString;
            using (WebResponse response = (HttpWebResponse)request.GetResponse())
            {
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                returnString = reader.ReadToEnd();
                response.Close();
            }

            return returnString;
        }
    }
}