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
    
    public partial class WCO_ListaBase_Result
    {
        public int IdListaBase { get; set; }
        public string Moneda { get; set; }
        public string TipoLista { get; set; }
        public Nullable<int> IdCliente { get; set; }
        public Nullable<System.DateTime> FechaValidezInicio { get; set; }
        public Nullable<System.DateTime> FechaValidezFin { get; set; }
        public Nullable<int> Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Codigo { get; set; }
        public Nullable<int> IndicadorPrecioFijo { get; set; }
        public Nullable<int> IdEmpresaAnteriorUnificacion { get; set; }
        public Nullable<int> TipoPaciente { get; set; }
    }
}
