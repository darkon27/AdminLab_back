using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiServices.Models.Entidades;

namespace WebApiServices.Models.Request
{
    public class Admision
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public virtual List<ADBE_PreAdmision> list_Admision { get; set; }
    }
}