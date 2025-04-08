using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiServices.Models.Request
{
    public class InformeRequest
    {
        public int IdOrdenAtencion { get; set; }
        public String Sucursal { get; set; }
        public String Clave { get; set; }
        public int Linea { get; set; }
    }
}