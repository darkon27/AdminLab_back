using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiServices.Models.Entidades;

namespace WebApiServices.Models.Request
{
    public class RequestAdmision
    {
        public int valor { get; set; }
        public int IndicadorWS { get; set; }
        public string mensaje { get; set; }
        public virtual WCO_TraerXAdmisionServicio_Result Admision { get; set; }
        public virtual List<WCO_ListarAdmisionServicioDetalle_Result> list_AdmisionServicio { get; set; }

    }
}