using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiServices.Models.Entidades;

namespace WebApiServices.Models.Request
{
    public class RequestAtencion
    {
        public string message { get; set; }
        public string fail { get; set; }
        public virtual List<COBE_OrdenAtencionDetalle> list_OrdenDetalle { get; set; }
    }
}