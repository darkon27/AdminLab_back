using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiServices.Models.Request
{
    public class ViewTransaccion
    {
        public int IdAdmision { get; set; }
        public int IdClienteFacturacion { get; set; }
        public string ClasificadorMovimiento { get; set; }
        public string NroPeticion { get; set; }   
        public string NombreCliente { get; set; }
        public string TipoComprobante { get; set; }
        public string Moneda { get; set; }    
        public Nullable<decimal> MontoTotalLocal { get; set; }
        public Nullable<decimal> MontoTotal { get; set; }
        public string UsuarioCreacion { get; set; }
    }
}