using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QT_Linebot.Models
{
    public class LineBotConfig
    {
        public Linebot LineBot { get; set; }
        public string[] UserID { get; set; }
    }
    public class Linebot
    {
        public string channelSecret { get; set; }
        public string accessToken { get; set; }
    }
}

