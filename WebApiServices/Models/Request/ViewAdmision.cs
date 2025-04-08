using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiServices.Models.Entidades;

namespace WebApiServices.Models.Request
{
    public class ViewAdmision
    {
        public bool success { get; set; }
        public Nullable<int> valor { get; set; }
        public string mensaje { get; set; }
        public Nullable<int> IdAdmision;
        public string NroPeticion { get; set; }
        public string OrdenAtencion { get; set; }
        public string Comentario { get; set; }

        public Nullable<int> Id { get; set; }
        public string Estado { get; set; }
        public Nullable<System.DateTime> FechaIni { get; set; }
        public Nullable<System.DateTime> FechaFin { get; set; }
        //public virtual ADBE_Admision Admision { get; set; }
    }
}