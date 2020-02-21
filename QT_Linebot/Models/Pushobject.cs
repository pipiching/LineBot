using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QT_Linebot.Models
{
    public class Pushobject
    {
        public string to { get; set; }
        public List<Message> messages { get; set; }
    }
}