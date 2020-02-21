using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QT_Linebot.Models
{
    public class Replyobject
    {
        public string replyToken { get; set; }
        public List<Message> messages { get; set; }
    }
}