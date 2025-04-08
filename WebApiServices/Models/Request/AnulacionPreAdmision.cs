using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiServices.Models.Request
{
    public class AnulacionPreAdmision
    {
        public string Cliente { get; set; }
        public string Acceso { get; set; }
        public string Mensaje { get; set; }
        public int IdOrdenAtencion { get; set; }
        public int Linea { get; set; }
    }
}