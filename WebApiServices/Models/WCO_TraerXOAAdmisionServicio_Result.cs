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
    
    public partial class WCO_TraerXOAAdmisionServicio_Result
    {
        public int IdAdmision { get; set; }
        public Nullable<int> UneuNegocioId { get; set; }
        public Nullable<int> TipoOperacionId { get; set; }
        public Nullable<int> Persona { get; set; }
        public Nullable<System.DateTime> FechaAdmision { get; set; }
        public string HistoriaClinica { get; set; }
        public string NroPeticion { get; set; }
        public string OrdenAtencion { get; set; }
        public Nullable<int> MedicoId { get; set; }
        public Nullable<int> IdSede { get; set; }
        public Nullable<int> Estado { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string IpCreacion { get; set; }
        public string IpModificacion { get; set; }
        public string MedicoDescripcion { get; set; }
        public Nullable<int> IdEmpresaPaciente { get; set; }
        public Nullable<int> IdAseguradora { get; set; }
        public string TipoOrden { get; set; }
        public string Cama { get; set; }
        public Nullable<int> Grupo { get; set; }
        public Nullable<int> IdSedeEmpresa { get; set; }
        public Nullable<decimal> CoaSeguro { get; set; }
        public Nullable<System.DateTime> FechaLimite { get; set; }
        public Nullable<int> IdExamen { get; set; }
        public Nullable<int> IdInsumo { get; set; }
        public string ClasificadorMovimiento { get; set; }
        public Nullable<int> IdAprobador { get; set; }
        public string OrdenSinapa { get; set; }
        public Nullable<decimal> ValorDescuento { get; set; }
        public Nullable<int> FlatSeguro { get; set; }
        public Nullable<int> FlatCoaSeguro { get; set; }
        public Nullable<int> FlatMovilidad { get; set; }
        public Nullable<decimal> Afecto { get; set; }
        public Nullable<decimal> Igv { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<decimal> Anticipo { get; set; }
        public Nullable<decimal> Saldo { get; set; }
        public Nullable<decimal> Redondeo { get; set; }
        public int IdOrdenAtencion { get; set; }
        public Nullable<int> IdPolizaPlan { get; set; }
        public Nullable<int> SecuencialPolizaPlan { get; set; }
        public Nullable<int> TipoOrdenAtencion { get; set; }
        public string CodigoOA { get; set; }
        public Nullable<int> Especialidad { get; set; }
        public Nullable<System.DateTime> FechaPreparacion { get; set; }
        public Nullable<System.DateTime> FechaInicio { get; set; }
        public Nullable<System.DateTime> FechaFinal { get; set; }
        public Nullable<int> IdOrdenRelacionada { get; set; }
        public Nullable<int> TipoAtencion { get; set; }
        public Nullable<int> TipoPaciente { get; set; }
        public Nullable<int> TipoParentesco { get; set; }
        public Nullable<int> Modalidad { get; set; }
        public Nullable<int> IdServicio { get; set; }
        public Nullable<int> IdTurno { get; set; }
        public Nullable<int> IdPaciente { get; set; }
        public Nullable<int> IdEmpresaAseguradora { get; set; }
        public string Compania { get; set; }
        public string Sucursal { get; set; }
        public Nullable<int> IdEstablecimiento { get; set; }
        public Nullable<int> IdCaja { get; set; }
        public string CentroCosto { get; set; }
        public string ClasificadorMovimiento1 { get; set; }
        public string UnidadOrganizacional { get; set; }
        public string Proyecto { get; set; }
        public string ConceptoGasto { get; set; }
        public Nullable<int> IdOPC { get; set; }
        public string TipoOPC { get; set; }
        public string SerieOPC { get; set; }
        public Nullable<int> NumeroOPC { get; set; }
        public Nullable<int> IdTransaccion { get; set; }
        public Nullable<int> TipoAlta { get; set; }
        public Nullable<System.DateTime> FechaAlta { get; set; }
        public Nullable<int> IdMedicoAutoriza { get; set; }
        public string ObservacionAlta { get; set; }
        public Nullable<int> EstadoDocumento { get; set; }
        public Nullable<int> EstadoDocumentoAnterior { get; set; }
        public Nullable<int> Estado1 { get; set; }
        public string UsuarioCreacion1 { get; set; }
        public Nullable<System.DateTime> FechaCreacion1 { get; set; }
        public string UsuarioModificacion1 { get; set; }
        public Nullable<System.DateTime> FechaModificacion1 { get; set; }
        public Nullable<int> IndicadorTMH { get; set; }
        public Nullable<int> IdEmpleado { get; set; }
        public Nullable<int> IdAlumno { get; set; }
        public Nullable<System.DateTime> FechaEmision { get; set; }
        public Nullable<int> SecuencialTMH { get; set; }
        public Nullable<int> IdEmpresaEmpleadora { get; set; }
        public Nullable<int> IdLineaOrdenRelacionada { get; set; }
        public Nullable<int> IdContrato { get; set; }
        public Nullable<int> IdPoliza { get; set; }
        public Nullable<int> IdPlan { get; set; }
        public Nullable<int> IdCobertura { get; set; }
        public Nullable<decimal> CopagoFijo { get; set; }
        public Nullable<decimal> CopagoVariable { get; set; }
        public Nullable<int> IdPersonaAntUnificacion { get; set; }
        public Nullable<System.DateTime> FechaAnterior { get; set; }
        public string CopagoMoneda { get; set; }
        public string CodigoSiteds { get; set; }
        public string TitularNombre { get; set; }
        public Nullable<int> CoberturaMedico { get; set; }
        public Nullable<int> SituacionControlOA { get; set; }
        public Nullable<int> IdEmpresaAnteriorUnificacion { get; set; }
        public string descripcionempresa_seguros { get; set; }
        public Nullable<int> TipoAltaMedica { get; set; }
        public Nullable<int> IdConsultaExternaPrincipal { get; set; }
        public Nullable<int> IdTopico { get; set; }
        public Nullable<int> IndicadorOcultarOA { get; set; }
        public Nullable<int> IndicadorAltaMedica { get; set; }
        public Nullable<int> CoberturaMedicoOriginal { get; set; }
        public Nullable<int> TipoAltaMedicaOriginal { get; set; }
        public string ObservacionAltaOriginal { get; set; }
        public Nullable<int> IndicadorAltaMedicaOriginal { get; set; }
        public Nullable<int> IdConsultaExternaPrincipalOriginal { get; set; }
        public Nullable<int> IndHospitalizado { get; set; }
        public Nullable<int> IndicadorEmergenciaRM { get; set; }
        public Nullable<int> bkestadodocumento_2011_05_17 { get; set; }
        public Nullable<int> bkestadodocumentoanterior_2011_05_17 { get; set; }
        public string ListaExamenComplementario { get; set; }
        public string ListaProcedimiento { get; set; }
        public string UsuarioAmpliacionOA { get; set; }
        public string UsuarioAnteriorAmpliacionOA { get; set; }
        public Nullable<int> IndicadorAuditoria { get; set; }
        public Nullable<int> IndicadorMigradoHospital { get; set; }
    }
}
