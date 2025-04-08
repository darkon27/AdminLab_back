using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiServices.Models.Entidades
{
    public class COBE_OrdenAtencionDetalle
    {        
        public Nullable<int> IdOrdenAtencion { get; set; }
        public Nullable<int> Linea { get; set; }
        public string Acceso { get; set; }
        public string Servicio { get; set; }
        public string Sucursal { get; set; }
        public string CodigoHC { get; set; }
        public string CodigoOA { get; set; }
        public string TipoAtencion { get; set; }
        public string TipoOrden { get; set; }
        public string Numerocama { get; set; }
        public string TipoDocumento { get; set; }
        public string Documento { get; set; }
        public string PacienteNombres { get; set; }
        public string PacienteAPPaterno { get; set; }
        public string PacienteAPMaterno { get; set; }
        public Nullable<System.DateTime> FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public string PacienteTelefono { get; set; }
        public string Pacienteemail { get; set; }
        public string Componente { get; set; }
        public string ComponenteNombre { get; set; }
        public Nullable<int> CantidadSolicitada { get; set; }
        public string MedicoNombres { get; set; }
        public string MedicoAPPaterno { get; set; }
        public string MedicoAPMaterno { get; set; }
        public string CMP { get; set; }
        public string Especialidad_Nombre { get; set; }
        public string Aseguradora_RUC { get; set; }
        public string Aseguradora_Nombre { get; set; }
        public string Empleadora_RUC { get; set; }
        public string Empleadora_Nombre { get; set; }
        public Nullable<System.DateTime> FechaLimiteAtencion { get; set; }
        public string Observacion { get; set; }
        public string Mensaje { get; set; }
        public Nullable<int> Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IpCreacion { get; set; }
        public string IpModificacion { get; set; }
        public Nullable<int> EstadoDocumento { get; set; }
        public Nullable<int> SituacionInterfase { get; set; }
        public Nullable<int> GrupoInterfase { get; set; }
    }
}