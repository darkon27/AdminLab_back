//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApiServices.Models
{
    using System;
    
    public partial class WCO_ListarCajeros_Result
    {
        public int IdCaja { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public Nullable<int> IdEstablecimiento { get; set; }
        public string Descripcion { get; set; }
        public string Observacion { get; set; }
        public Nullable<int> Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public Nullable<int> IndicadorTodos { get; set; }
        public Nullable<int> IdTipoAtencionDefecto { get; set; }
        public Nullable<int> IndicadorAplicaTicket { get; set; }
        public string HostName { get; set; }
        public string Ticket { get; set; }
        public string NombrePinPad { get; set; }
        public Nullable<int> IndicadorAplicaEstacion { get; set; }
        public string ESTADOdesc { get; set; }
    }
}
