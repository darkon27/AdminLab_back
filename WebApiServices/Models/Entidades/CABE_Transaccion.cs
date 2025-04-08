using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiServices.Models.Entidades
{
    [Serializable()]
    public class CABE_TransaccionPK
    {
        #region "Variables Locales"

        private Nullable<Int32> _IdTransaccion;
        private ADBE_Admision _Admision;
        private ADBE_Unegocio _UnidadNegocio; 
        #endregion

        #region Constructor

        public CABE_TransaccionPK()
        {
            _Admision = new ADBE_Admision();
            _UnidadNegocio = new ADBE_Unegocio();
        }

        #endregion

        #region Propiedades

        public Nullable<Int32> IdTransaccion
        {
            get { return _IdTransaccion; }
            set { _IdTransaccion = value; }
        }

        public ADBE_Admision Admision
        {
            get
            {
                return _Admision;
            }
            set
            {
                if (_Admision == value)
                    return;
                _Admision = value;
            }
        }

        public ADBE_Unegocio UnidadNegocio
        {
            get
            {
                return _UnidadNegocio;
            }
            set
            {
                if (_UnidadNegocio == value)
                    return;
                _UnidadNegocio = value;
            }
        }


        #endregion

    }

    [Serializable()]
    public class CABE_Transaccion
    {
        private CABE_TransaccionPK _PK;


        public CABE_Transaccion()
        {
            _PK = new CABE_TransaccionPK();
        }

        public CABE_TransaccionPK PK
        {
            get { return _PK; }
            set { _PK = value; }
        }

        private Nullable<int> _IdTransaccionRelacionada;
        public Nullable<int> IdTransaccionRelacionada
        {
            get { return _IdTransaccionRelacionada; }
            set { _IdTransaccionRelacionada = value; }
        }

        private string _CodigoTransaccion;
        public string CodigoTransaccion
        {
            get { return _CodigoTransaccion; }
            set { _CodigoTransaccion = value; }
        }

        private Nullable<DateTime> _FechaPreparacion;
        public Nullable<DateTime> FechaPreparacion
        {
            get { return _FechaPreparacion; }
            set { _FechaPreparacion = value; }
        }

        private Nullable<int> _PeriodoEmision;
        public Nullable<int> PeriodoEmision
        {
            get { return _PeriodoEmision; }
            set { _PeriodoEmision = value; }
        }

        private Nullable<DateTime> _FechaEmision;
        public Nullable<DateTime> FechaEmision
        {
            get { return _FechaEmision; }
            set { _FechaEmision = value; }
        }

        private Nullable<int> _IdClienteFacturacion;
        public Nullable<int> IdClienteFacturacion
        {
            get { return _IdClienteFacturacion; }
            set { _IdClienteFacturacion = value; }
        }

        private Nullable<int> _IndicadorVarios;
        public Nullable<int> IndicadorVarios
        {
            get { return _IndicadorVarios; }
            set { _IndicadorVarios = value; }
        }

        private Nullable<DateTime> _FechaPago;
        public Nullable<DateTime> FechaPago
        {
            get { return _FechaPago; }
            set { _FechaPago = value; }
        }

        private Nullable<DateTime> _FechaCancelacion;
        public Nullable<DateTime> FechaCancelacion
        {
            get { return _FechaCancelacion; }
            set { _FechaCancelacion = value; }
        }

        private Nullable<int> _IdMotivoCancelacion;
        public Nullable<int> IdMotivoCancelacion
        {
            get { return _IdMotivoCancelacion; }
            set { _IdMotivoCancelacion = value; }
        }

        private Nullable<int> _IdResponsableCancelacion;
        public Nullable<int> IdResponsableCancelacion
        {
            get { return _IdResponsableCancelacion; }
            set { _IdResponsableCancelacion = value; }
        }

        private string _Moneda;
        public string Moneda
        {
            get { return _Moneda; }
            set { _Moneda = value; }
        }

        private Nullable<decimal> _TipoCambio;
        public Nullable<decimal> TipoCambio
        {
            get { return _TipoCambio; }
            set { _TipoCambio = value; }

        }

        private Nullable<int> _EstadoDocumentoAnterior;
        public Nullable<int> EstadoDocumentoAnterior
        {
            get { return _EstadoDocumentoAnterior; }
            set { _EstadoDocumentoAnterior = value; }

        }

        private Nullable<int> _EstadoDocumento;
        public Nullable<int> EstadoDocumento
        {
            get { return _EstadoDocumento; }
            set { _EstadoDocumento = value; }

        }

        private Nullable<int> _IdEstablecimiento;
        public Nullable<int> IdEstablecimiento
        {
            get { return _IdEstablecimiento; }
            set { _IdEstablecimiento = value; }

        }

        private Nullable<int> _IdCaja;
        public Nullable<int> IdCaja
        {
            get { return _IdCaja; }
            set { _IdCaja = value; }

        }

        private string _CentroCosto;
        public string CentroCosto
        {
            get { return _CentroCosto; }
            set { _CentroCosto = value; }

        }

        private string _ClasificadorMovimiento;
        public string ClasificadorMovimiento
        {
            get { return _ClasificadorMovimiento; }
            set { _ClasificadorMovimiento = value; }

        }

        private string _UnidadOrganizacional;
        public string UnidadOrganizacional
        {
            get { return _UnidadOrganizacional; }
            set { _UnidadOrganizacional = value; }

        }

        private string _Proyecto;
        public string Proyecto
        {
            get { return _Proyecto; }
            set { _Proyecto = value; }

        }

        private string _ConceptoGasto;
        public string ConceptoGasto
        {
            get { return _ConceptoGasto; }
            set { _ConceptoGasto = value; }

        }

        private Nullable<int> _IdOpc;
        public Nullable<int> IdOpc
        {
            get { return _IdOpc; }
            set { _IdOpc = value; }

        }

        private string _TipoOpc;
        public string TipoOpc
        {
            get { return _TipoOpc; }
            set { _TipoOpc = value; }

        }

        private string _SerieOpc;
        public string SerieOpc
        {
            get { return _SerieOpc; }
            set { _SerieOpc = value; }

        }

        private Nullable<int> _NumeroOpc;
        public Nullable<int> NumeroOpc
        {
            get { return _NumeroOpc; }
            set { _NumeroOpc = value; }

        }

        private string _UnidadReplicacion;
        public string UnidadReplicacion
        {
            get { return _UnidadReplicacion; }
            set { _UnidadReplicacion = value; }

        }

        private Nullable<int> _Procedencia;
        public Nullable<int> Procedencia
        {
            get { return _Procedencia; }
            set { _Procedencia = value; }

        }

        private Nullable<int> _TipoFacturacion;
        public Nullable<int> TipoFacturacion
        {
            get { return _TipoFacturacion; }
            set { _TipoFacturacion = value; }

        }

        private Nullable<int> _FormaFacturacion;
        public Nullable<int> FormaFacturacion
        {
            get { return _FormaFacturacion; }
            set { _FormaFacturacion = value; }

        }

        private Nullable<int> _FormaPago;
        public Nullable<int> FormaPago
        {
            get { return _FormaPago; }
            set { _FormaPago = value; }

        }

        private Nullable<int> _ConceptoFacturacion;
        public Nullable<int> ConceptoFacturacion
        {
            get { return _ConceptoFacturacion; }
            set { _ConceptoFacturacion = value; }

        }

        private Nullable<int> _TipoVenta;
        public Nullable<int> TipoVenta
        {
            get { return _TipoVenta; }
            set { _TipoVenta = value; }

        }

        private string _Descripcion;
        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }

        }

        private string _Observacion;
        public string Observacion
        {
            get { return _Observacion; }
            set { _Observacion = value; }

        }

        private Nullable<int> _IdComprobante;
        public Nullable<int> IdComprobante
        {
            get { return _IdComprobante; }
            set { _IdComprobante = value; }

        }

        private string _TipoComprobante;
        public string TipoComprobante
        {
            get { return _TipoComprobante; }
            set { _TipoComprobante = value; }

        }

        private string _SerieComprobante;
        public string SerieComprobante
        {
            get { return _SerieComprobante; }
            set { _SerieComprobante = value; }

        }

        private string _NumeroComprobante;
        public string NumeroComprobante
        {
            get { return _NumeroComprobante; }
            set { _NumeroComprobante = value; }
        }

        private Nullable<int> _IdVendedor;
        public Nullable<int> IdVendedor
        {
            get { return _IdVendedor; }
            set { _IdVendedor = value; }

        }

        private Nullable<decimal> _MontoDescuentos;
        public Nullable<decimal> MontoDescuentos
        {
            get { return _MontoDescuentos; }
            set { _MontoDescuentos = value; }

        }

        private Nullable<decimal> _MontoAfecto;
        public Nullable<decimal> MontoAfecto
        {
            get { return _MontoAfecto; }
            set { _MontoAfecto = value; }

        }

        private Nullable<decimal> _MontoNoAfecto;
        public Nullable<decimal> MontoNoAfecto
        {
            get { return _MontoNoAfecto; }
            set { _MontoNoAfecto = value; }

        }

        private Nullable<decimal> _MontoImpuestoVentas;
        public Nullable<decimal> MontoImpuestoVentas
        {
            get { return _MontoImpuestoVentas; }
            set { _MontoImpuestoVentas = value; }

        }

        private Nullable<decimal> _MontoImpuestosRetenido;
        public Nullable<decimal> MontoImpuestosRetenido
        {
            get { return _MontoImpuestosRetenido; }
            set { _MontoImpuestosRetenido = value; }

        }

        private Nullable<decimal> _MontoImpuestos;
        public Nullable<decimal> MontoImpuestos
        {
            get { return _MontoImpuestos; }
            set { _MontoImpuestos = value; }

        }

        private Nullable<decimal> _MontoDescuentosLocal;
        public Nullable<decimal> MontoDescuentosLocal
        {
            get { return _MontoDescuentosLocal; }
            set { _MontoDescuentosLocal = value; }

        }

        private Nullable<decimal> _MontoPagado;
        public Nullable<decimal> MontoPagado
        {
            get { return _MontoPagado; }
            set { _MontoPagado = value; }

        }

        private Nullable<decimal> _MontoAdelanto;
        public Nullable<decimal> MontoAdelanto
        {
            get { return _MontoAdelanto; }
            set { _MontoAdelanto = value; }

        }

        private Nullable<decimal> _MontoAdelantoSaldo;
        public Nullable<decimal> MontoAdelantoSaldo
        {
            get { return _MontoAdelantoSaldo; }
            set { _MontoAdelantoSaldo = value; }

        }

        private Nullable<decimal> _MontoRedondeo;
        public Nullable<decimal> MontoRedondeo
        {
            get { return _MontoRedondeo; }
            set { _MontoRedondeo = value; }

        }

        private Nullable<decimal> _MontoTotal;
        public Nullable<decimal> MontoTotal
        {
            get { return _MontoTotal; }
            set { _MontoTotal = value; }

        }

        private Nullable<decimal> _MontoAfectoLocal;
        public Nullable<decimal> MontoAfectoLocal
        {
            get { return _MontoAfectoLocal; }
            set { _MontoAfectoLocal = value; }

        }

        private Nullable<decimal> _MontoNoAfectoLocal;
        public Nullable<decimal> MontoNoAfectoLocal
        {
            get { return _MontoNoAfectoLocal; }
            set { _MontoNoAfectoLocal = value; }

        }

        private Nullable<decimal> _MontoImpuestoVentasLocal;
        public Nullable<decimal> MontoImpuestoVentasLocal
        {
            get { return _MontoImpuestoVentasLocal; }
            set { _MontoImpuestoVentasLocal = value; }

        }

        private Nullable<decimal> _MontoImpuestosRetenidoLocal;
        public Nullable<decimal> MontoImpuestosRetenidoLocal
        {
            get { return _MontoImpuestosRetenidoLocal; }
            set { _MontoImpuestosRetenidoLocal = value; }

        }

        private Nullable<decimal> _MontoImpuestosLocal;
        public Nullable<decimal> MontoImpuestosLocal
        {
            get { return _MontoImpuestosLocal; }
            set { _MontoImpuestosLocal = value; }

        }

        private Nullable<decimal> _MontoPagadoLocal;
        public Nullable<decimal> MontoPagadoLocal
        {
            get { return _MontoPagadoLocal; }
            set { _MontoPagadoLocal = value; }

        }

        private Nullable<decimal> _MontoAdelantoLocal;
        public Nullable<decimal> MontoAdelantoLocal
        {
            get { return _MontoAdelantoLocal; }
            set { _MontoAdelantoLocal = value; }

        }

        private Nullable<decimal> _MontoAdelantoSaldoLocal;
        public Nullable<decimal> MontoAdelantoSaldoLocal
        {
            get { return _MontoAdelantoSaldoLocal; }
            set { _MontoAdelantoSaldoLocal = value; }

        }

        private Nullable<decimal> _MontoRedondeoLocal;
        public Nullable<decimal> MontoRedondeoLocal
        {
            get { return _MontoRedondeoLocal; }
            set { _MontoRedondeoLocal = value; }

        }

        private Nullable<decimal> _MontoTotalLocal;
        public Nullable<decimal> MontoTotalLocal
        {
            get { return _MontoTotalLocal; }
            set { _MontoTotalLocal = value; }

        }

        private string _DocumentoCliente;
        public string DocumentoCliente
        {
            get { return _DocumentoCliente; }
            set { _DocumentoCliente = value; }

        }

        private string _NombreCliente;
        public string NombreCliente
        {
            get { return _NombreCliente; }
            set { _NombreCliente = value; }

        }

        private string _DireccionCliente;
        public string DireccionCliente
        {
            get { return _DireccionCliente; }
            set { _DireccionCliente = value; }

        }

        private Nullable<decimal> _Saldo;
        public Nullable<decimal> Saldo
        {
            get { return _Saldo; }
            set { _Saldo = value; }

        }

        private Nullable<decimal> _SaldoLocal;
        public Nullable<decimal> SaldoLocal
        {
            get { return _SaldoLocal; }
            set { _SaldoLocal = value; }

        }

        private Nullable<int> _Estado;
        public Nullable<int> Estado
        {
            get { return _Estado; }
            set { _Estado = value; }

        }

        private Nullable<DateTime> _FechaModificacion;
        public Nullable<DateTime> FechaModificacion
        {
            get { return _FechaModificacion; }
            set { _FechaModificacion = value; }

        }

        private string _UsuarioCreacion;
        public string UsuarioCreacion
        {
            get { return _UsuarioCreacion; }
            set { _UsuarioCreacion = value; }

        }

        private Nullable<DateTime> _FechaCreacion;
        public Nullable<DateTime> FechaCreacion
        {
            get { return _FechaCreacion; }
            set { _FechaCreacion = value; }

        }

        private string _UsuarioModificacion;
        public string UsuarioModificacion
        {
            get { return _UsuarioModificacion; }
            set { _UsuarioModificacion = value; }

        }

        private Nullable<DateTime> _FechaVencimiento;
        public Nullable<DateTime> FechaVencimiento
        {
            get { return _FechaVencimiento; }
            set { _FechaVencimiento = value; }

        }

        private Nullable<int> _TipoPaciente;
        public Nullable<int> TipoPaciente
        {
            get { return _TipoPaciente; }
            set { _TipoPaciente = value; }

        }

        private Nullable<int> _TipoAtencion;
        public Nullable<int> TipoAtencion
        {
            get { return _TipoAtencion; }
            set { _TipoAtencion = value; }

        }

        private Nullable<int> _IdPaciente;
        public Nullable<int> IdPaciente
        {
            get { return _IdPaciente; }
            set { _IdPaciente = value; }

        }

        private Nullable<int> _IdPreparadoPor;
        public Nullable<int> IdPreparadoPor
        {
            get { return _IdPreparadoPor; }
            set { _IdPreparadoPor = value; }

        }

        private Nullable<int> _IdEmpresaAseguradora;
        public Nullable<int> IdEmpresaAseguradora
        {
            get { return _IdEmpresaAseguradora; }
            set { _IdEmpresaAseguradora = value; }

        }

        private Nullable<int> _IdEmpresaEmpleadora;
        public Nullable<int> IdEmpresaEmpleadora
        {
            get { return _IdEmpresaEmpleadora; }
            set { _IdEmpresaEmpleadora = value; }

        }

        private Nullable<int> _IdContrato;
        public Nullable<int> IdContrato
        {
            get { return _IdContrato; }
            set { _IdContrato = value; }

        }

        private Nullable<int> _Idpoliza;
        public Nullable<int> Idpoliza
        {
            get { return _Idpoliza; }
            set { _Idpoliza = value; }

        }

        private Nullable<int> _IdPlan;
        public Nullable<int> IdPlan
        {
            get { return _IdPlan; }
            set { _IdPlan = value; }

        }

        private Nullable<int> _IdCobertura;
        public Nullable<int> IdCobertura
        {
            get { return _IdCobertura; }
            set { _IdCobertura = value; }

        }

        private Nullable<decimal> _CopagoFijo;
        public Nullable<decimal> CopagoFijo
        {
            get { return _CopagoFijo; }
            set { _CopagoFijo = value; }

        }

        private Nullable<decimal> _CopagoVariable;
        public Nullable<decimal> CopagoVariable
        {
            get { return _CopagoVariable; }
            set { _CopagoVariable = value; }

        }

        private Nullable<int> _TipoParentesco;
        public Nullable<int> TipoParentesco
        {
            get { return _TipoParentesco; }
            set { _TipoParentesco = value; }

        }

        private Nullable<int> _IndicadorTitular;
        public Nullable<int> IndicadorTitular
        {
            get { return _IndicadorTitular; }
            set { _IndicadorTitular = value; }

        }

        private Nullable<int> _IdTitular;
        public Nullable<int> IdTitular
        {
            get { return _IdTitular; }
            set { _IdTitular = value; }

        }

        private string _NombreTitular;
        public string NombreTitular
        {
            get { return _NombreTitular; }
            set { _NombreTitular = value; }

        }

        private Nullable<int> _TipoTransaccion;
        public Nullable<int> TipoTransaccion
        {
            get { return _TipoTransaccion; }
            set { _TipoTransaccion = value; }

        }

        private string _AlmacenDestino;
        public string AlmacenDestino
        {
            get { return _AlmacenDestino; }
            set { _AlmacenDestino = value; }

        }

        private Nullable<int> _IdServicio;
        public Nullable<int> IdServicio
        {
            get { return _IdServicio; }
            set { _IdServicio = value; }

        }

        private Nullable<int> _Especialidad;
        public Nullable<int> Especialidad
        {
            get { return _Especialidad; }
            set { _Especialidad = value; }

        }

        private string _CodigoSiteds;
        public string CodigoSiteds
        {
            get { return _CodigoSiteds; }
            set { _CodigoSiteds = value; }

        }

        private string _Origen;
        public string Origen
        {
            get { return _Origen; }
            set { _Origen = value; }

        }

        private Nullable<int> _IdTurno;
        public Nullable<int> IdTurno
        {
            get { return _IdTurno; }
            set { _IdTurno = value; }

        }

        private Nullable<int> _TipoOrdenAtencion;
        public Nullable<int> TipoOrdenAtencion
        {
            get { return _TipoOrdenAtencion; }
            set { _TipoOrdenAtencion = value; }

        }

        private string _TipoRelacion;
        public string TipoRelacion
        {
            get { return _TipoRelacion; }
            set { _TipoRelacion = value; }

        }

        private Nullable<int> _Modalidad;
        public Nullable<int> Modalidad
        {
            get { return _Modalidad; }
            set { _Modalidad = value; }

        }

        private Nullable<int> _GrupoAtencion;
        public Nullable<int> GrupoAtencion
        {
            get { return _GrupoAtencion; }
            set { _GrupoAtencion = value; }

        }

        private Nullable<int> _GrupoTransaccion;
        public Nullable<int> GrupoTransaccion
        {
            get { return _GrupoTransaccion; }
            set { _GrupoTransaccion = value; }

        }

        private string _CopagoMoneda;
        public string CopagoMoneda
        {
            get { return _CopagoMoneda; }
            set { _CopagoMoneda = value; }

        }

        private Nullable<DateTime> _FechaAprobacionSeguro;
        public Nullable<DateTime> FechaAprobacionSeguro
        {
            get { return _FechaAprobacionSeguro; }
            set { _FechaAprobacionSeguro = value; }

        }

        private Nullable<decimal> _MontoGarantia;
        public Nullable<decimal> MontoGarantia
        {
            get { return _MontoGarantia; }
            set { _MontoGarantia = value; }

        }

        private string _MonedaGarantia;
        public string MonedaGarantia
        {
            get { return _MonedaGarantia; }
            set { _MonedaGarantia = value; }

        }


        private string _Sucursal;
                    public string Sucursal
        {
            get { return _Sucursal; }
            set { _Sucursal = value; }

        }

        private string _CartaGarantia;
        public string CartaGarantia
        {
            get { return _CartaGarantia; }
            set { _CartaGarantia = value; }

        }

        private Nullable<int> _IdOARelacionado;
        public Nullable<int> IdOARelacionado
        {
            get { return _IdOARelacionado; }
            set { _IdOARelacionado = value; }

        }

        private Nullable<int> _IdTurnoCaja;
        public Nullable<int> IdTurnoCaja
        {
            get { return _IdTurnoCaja; }
            set { _IdTurnoCaja = value; }

        }

        private Nullable<int> _IdHospitalizacion;
        public Nullable<int> IdHospitalizacion
        {
            get { return _IdHospitalizacion; }
            set { _IdHospitalizacion = value; }

        }

        private Nullable<decimal> _PorcentajeImpuesto;
        public Nullable<decimal> PorcentajeImpuesto
        {
            get { return _PorcentajeImpuesto; }
            set { _PorcentajeImpuesto = value; }

        }

        private string _UsuarioCaja;
        public string UsuarioCaja
        {
            get { return _UsuarioCaja; }
            set { _UsuarioCaja = value; }

        }

        private Nullable<int> _TipoCobro;
        public Nullable<int> TipoCobro
        {
            get { return _TipoCobro; }
            set { _TipoCobro = value; }

        }

        private string _CodigoSitedsFarmacia;
        public string CodigoSitedsFarmacia
        {
            get { return _CodigoSitedsFarmacia; }
            set { _CodigoSitedsFarmacia = value; }

        }

        private Nullable<int> _IndicadorSitedsFarmacia;
        public Nullable<int> IndicadorSitedsFarmacia
        {
            get { return _IndicadorSitedsFarmacia; }
            set { _IndicadorSitedsFarmacia = value; }

        }

        private string _TipoDocumentoGuiaSalida;
        public string TipoDocumentoGuiaSalida
        {
            get { return _TipoDocumentoGuiaSalida; }
            set { _TipoDocumentoGuiaSalida = value; }

        }

        private string _SerieGuiaSalida;
        public string SerieGuiaSalida
        {
            get { return _SerieGuiaSalida; }
            set { _SerieGuiaSalida = value; }

        }

        private string _NumeroGuiaSalida;
        public string NumeroGuiaSalida
        {
            get { return _NumeroGuiaSalida; }
            set { _NumeroGuiaSalida = value; }

        }

        private Nullable<int> _IndicadorGeneraGuiaSalida;
        public Nullable<int> IndicadorGeneraGuiaSalida
        {
            get { return _IndicadorGeneraGuiaSalida; }
            set { _IndicadorGeneraGuiaSalida = value; }

        }

        private Nullable<int> _IndicadorCierrePresupuesto;
        public Nullable<int> IndicadorCierrePresupuesto
        {
            get { return _IndicadorCierrePresupuesto; }
            set { _IndicadorCierrePresupuesto = value; }

        }

        private Nullable<int> _IdProcedimiento;
        public Nullable<int> IdProcedimiento
        {
            get { return _IdProcedimiento; }
            set { _IdProcedimiento = value; }

        }

        private Nullable<int> _IndicadorTicket;
        public Nullable<int> IndicadorTicket
        {
            get { return _IndicadorTicket; }
            set { _IndicadorTicket = value; }

        }

        private Nullable<int> _IdPersonaAntUnificacion;
        public Nullable<int> IdPersonaAntUnificacion
        {
            get { return _IdPersonaAntUnificacion; }
            set { _IdPersonaAntUnificacion = value; }

        }

        private Nullable<int> _IndicadorAperturaEmergencia;
        public Nullable<int> IndicadorAperturaEmergencia
        {
            get { return _IndicadorAperturaEmergencia; }
            set { _IndicadorAperturaEmergencia = value; }

        }

        private Nullable<int> _IdPrograma;
        public Nullable<int> IdPrograma
        {
            get { return _IdPrograma; }
            set { _IdPrograma = value; }

        }

        private string _TipoDespacho;
        public string TipoDespacho
        {
            get { return _TipoDespacho; }
            set { _TipoDespacho = value; }

        }

        private Nullable<int> _SituacionDespacho;
        public Nullable<int> SituacionDespacho
        {
            get { return _SituacionDespacho; }
            set { _SituacionDespacho = value; }

        }

        private Nullable<int> _RegularizacionEmergencia;
        public Nullable<int> RegularizacionEmergencia
        {
            get { return _RegularizacionEmergencia; }
            set { _RegularizacionEmergencia = value; }

        }

        private Nullable<int> _IdEmpresaAnteriorUnificacion;
        public Nullable<int> IdEmpresaAnteriorUnificacion
        {
            get { return _IdEmpresaAnteriorUnificacion; }
            set { _IdEmpresaAnteriorUnificacion = value; }

        }

        private Nullable<int> _IndicadorGeneracionCG;
        public Nullable<int> IndicadorGeneracionCG
        {
            get { return _IndicadorGeneracionCG; }
            set { _IndicadorGeneracionCG = value; }

        }

        private string _descripcionempresa_seguros;
        public string descripcionempresa_seguros
        {
            get { return _descripcionempresa_seguros; }
            set { _descripcionempresa_seguros = value; }

        }

        private Nullable<int> _PersonaAntUnificacion;
        public Nullable<int> PersonaAntUnificacion
        {
            get { return _PersonaAntUnificacion; }
            set { _PersonaAntUnificacion = value; }

        }

        private string _UsuarioSupervisor;
        public string UsuarioSupervisor
        {
            get { return _UsuarioSupervisor; }
            set { _UsuarioSupervisor = value; }

        }

        private Nullable<int> _IndicadorTipoFarmacia;
        public Nullable<int> IndicadorTipoFarmacia
        {
            get { return _IndicadorTipoFarmacia; }
            set { _IndicadorTipoFarmacia = value; }

        }

        private Nullable<int> _TipoFarmacia;
        public Nullable<int> TipoFarmacia
        {
            get { return _TipoFarmacia; }
            set { _TipoFarmacia = value; }

        }

        private Nullable<int> _IdDiagnostico;
        public Nullable<int> IdDiagnostico
        {
            get { return _IdDiagnostico; }
            set { _IdDiagnostico = value; }

        }

        private Nullable<int> _TipoTransaccionHospitalizacion;
        public Nullable<int> TipoTransaccionHospitalizacion
        {
            get { return _TipoTransaccionHospitalizacion; }
            set { _TipoTransaccionHospitalizacion = value; }

        }

        private string _CodigoAsegurado;
        public string CodigoAsegurado
        {
            get { return _CodigoAsegurado; }
            set { _CodigoAsegurado = value; }

        }

        private Nullable<int> _IdGarantia;
        public Nullable<int> IdGarantia
        {
            get { return _IdGarantia; }
            set { _IdGarantia = value; }

        }

        private Nullable<int> _IdListaTarifa;
        public Nullable<int> IdListaTarifa
        {
            get { return _IdListaTarifa; }
            set { _IdListaTarifa = value; }

        }

        private Nullable<int> _IndicadorTarifa;
        public Nullable<int> IndicadorTarifa
        {
            get { return _IndicadorTarifa; }
            set { _IndicadorTarifa = value; }

        }

        private Nullable<int> _IndicadorUsoTransaccion;
        public Nullable<int> IndicadorUsoTransaccion
        {
            get { return _IndicadorUsoTransaccion; }
            set { _IndicadorUsoTransaccion = value; }
        }

        private string _ObservacionAnulado;
        public string ObservacionAnulado
        {
            get { return _ObservacionAnulado; }
            set { _ObservacionAnulado = value; }

        }

    }
}