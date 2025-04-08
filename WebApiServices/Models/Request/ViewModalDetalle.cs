using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiServices.Models.Request
{
    public class ViewModalDetalle
    {
        public bool success { get; set; }
        public Nullable<int> valor { get; set; }
        public string tokem { get; set; }
        public string mensaje { get; set; }
        public object cabecera { get; set; }
        public object detalle { get; set; }

        //public WCO_ListarComprobante_Result cabecera { get; set; }

        //public List<WCO_ListarComprobanteDETAsult> detalle { get; set; }
    }
}