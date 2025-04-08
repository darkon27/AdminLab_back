using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiServices.Models.Entidades
{
    public class WCO_PreAdmision
    {
        public string message { get; set; }
        public string fail { get; set; }
        public virtual List<ADBE_PreAdmision> list_OrdenDetalle { get; set; }
    }
}