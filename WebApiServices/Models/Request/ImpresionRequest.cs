using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiServices.Models.Request
{
    public class ImpresionRequest
    {
        public int IdReporte { get; set; } //1=Constancia ,2= ConstanciaSequence , 3= Boletas 4=Factura
        public int IdAdmision { get; set; }
        public String NroPeticion { get; set; }

        //public String TipoComprobante { get; set; }
        //public String NroComprobante { get; set; }        
        //public int IdComprobante { get; set; }
        //public String Reporte { get; set; }
    }
}