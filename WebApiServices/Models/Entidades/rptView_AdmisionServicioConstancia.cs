using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiServices.Models.Entidades
{
    public class rptView_AdmisionServicioConstancia
    {
        public int IdAdmision { get; set; }
        public int Linea { get; set; }
        public string CodigoComponente { get; set; }
        public string NroPeticion { get; set; }
        public string NombreCompleto { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public int TipoOperacionID { get; set; }
        public int Persona { get; set; }
        public int IdSede { get; set; }
        public int TIPOADMISIONID { get; set; }
        public string Sexo { get; set; }
        public int edad { get; set; }
        public string Telefono { get; set; }
        public string MEDICO { get; set; }
        public string CLIENTE { get; set; }
        public string PROCEDENCIA { get; set; }
        public string HistoriaClinica { get; set; }
        public string EMPRESA { get; set; }
        public string Usuario { get; set; }
        public string CorreoElectronico { get; set; }
        public string OrdenAtencion { get; set; }
        public string Clave { get; set; }
        public string Documento { get; set; }
        public string FechaAdmision { get; set; }
        public string NombreEmpresa { get; set; }
        public string OrdenSinapa { get; set; }
        public string TipoPaciente { get; set; }
        public string TipoOrden { get; set; }
        public string TipoAtencion { get; set; }
        public string Portal { get; set; }
        public decimal redondeo { get; set; }

    }
}