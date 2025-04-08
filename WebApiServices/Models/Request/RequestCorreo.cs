using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiServices.Models.Request
{
    public class RequestCorreo
    {
        public bool success { get; set; }
        public Nullable<int> valor { get; set; }
        public string tokem { get; set; }
        public Nullable<int> UneuNegocioId { get; set; }
        public string NombreCompleto { get; set; }
        public string PerNumeroDocumento { get; set; }
        public string Password { get; set; }
        public string str_pTo { get; set; }
        public string str_pCC { get; set; }
        public string str_pBCC { get; set; }

        //public string asunto { get; set; }
        //public string mensaje { get; set; }
        //public string ruta_archivo_adjunto { get; set; }
        //public string Acceso { get; set; }
    }
}