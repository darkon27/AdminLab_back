using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApiServices.Models.Request
{
    public class ViewModalReporte
    {
        public int CodigoError { get; set; }
        public int LineaError { get; set; }
        public decimal? ValorDecimal { get; set; }
        public String ValorDevolucion { get; set; }
        public byte[] ValorByte { get; set; }
        public Stream ValorStem { get; set; }
        public string MensajeError { get; set; }
    }
}