using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiServices.Models.Request
{
    public class file
    {
        public string key { get; set; }
        public string expandedIcon { get; set; }
        public string collapsedIcon { get; set; }
        public string label { get; set; }
        public string data { get; set; }
        public string icon { get; set; }
        public object children { get; set; }
    }
}