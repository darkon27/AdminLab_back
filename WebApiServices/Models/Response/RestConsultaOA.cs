using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiServices.Models.Response
{
    public class RestConsultaOA
    {
        public int IdCliente { get; set; }
        public int TipoOrdenAtencion { get; set; }
        public string Sucursal { get; set; }
        public string CodigoOA { get; set; }
        public string Documento { get; set; }
        public string NombreCompleto { get; set; }
        public int Cantidad { get; set; }
        public decimal Tiempo { get; set; }
        public string mensaje { get; set; }

    }
}