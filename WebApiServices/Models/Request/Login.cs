using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiServices.Models.Request
{
    public class Login
    {
        public bool success { get; set; }

        public List<WCO_AccesoUsuario_Result> user { get; set; }

        public string tokem { get; set; }
    }
}