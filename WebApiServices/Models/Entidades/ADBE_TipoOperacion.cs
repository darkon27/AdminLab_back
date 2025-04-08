using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace WebApiServices.Models.Entidades
{
    [Serializable()]
    public class ADBE_TipoOperacionPK
    {
        #region Variables privadas

        private Nullable<int> _TipoOperacionId;
        private Nullable<int> _UnidadNegocio;
        private Nullable<int> _TipoAdmision;
        private Nullable<int> _TipoPaciente;
        private ADBE_PersonaMast _Persona;
        private ADBE_Sede _Sede;
        private Nullable<int> _SedeCliente;

        #endregion

        #region Propiedades publicas

        public Nullable<int> UnidadNegocio
        {
            get { return _UnidadNegocio; }
            set { _UnidadNegocio = value; }
        }

        public Nullable<int> SedeCliente
        {
            get { return _SedeCliente; }
            set { _SedeCliente = value; }
        }

        public Nullable<int> TipoPaciente
        {
            get { return _TipoPaciente; }
            set { _TipoPaciente = value; }
        }

        public Nullable<int> TipoAdmision
        {
            get { return _TipoAdmision; }
            set { _TipoAdmision = value; }
        }

        public Nullable<int> TipoOperacionId
        {
            get { return _TipoOperacionId; }
            set { _TipoOperacionId = value; }
        }

        public ADBE_PersonaMast Persona
        {
            get { return _Persona; }
            set { _Persona = value; }
        }

        public ADBE_Sede Sede
        {
            get { return _Sede; }
            set { _Sede = value; }
        }

        #endregion

        #region Constructor

        public ADBE_TipoOperacionPK()
        {
            //_UnidadNegocio = new ADBE_Unegocio();
            //_TipoAdmision = new ADBE_TipoAdmision();
            //_TipoPaciente = new COBE_TipoPaciente();
            _Persona = new ADBE_PersonaMast();
            _Sede = new ADBE_Sede();
            //_SedeCliente = new ADBE_SedeCliente();
        }

        #endregion
    }

    [Serializable()]
    public class ADBE_TipoOperacion
    {
        #region Variables Locales
        private Nullable<int> _TipEstado;
        private Nullable<int> _TpContratoId;
        private string _CodigoContrato;
        /**/
        private Nullable<int> _IdlistaBase;
        private Nullable<int> _TPAplicaFactor;
        private Nullable<Decimal> _Factor;
        /**/
        private ADBE_TipoOperacionPK _pk;
        private string _UsuarioCreacion;
        private Nullable<DateTime> _FechaCreacion;
        private string _UsuarioModificacion;
        private Nullable<DateTime> _FechaModificacion;
        private string _IpCreacion;
        private string _IpModificacion;
        private Nullable<int> _IdPolizaPlan;
        /**/
        private Nullable<Decimal> _MonedaEmpresa;
        private Nullable<DateTime> _Fechinicio;
        private Nullable<DateTime> _FechTermino;
        private string _Moneda;
        private Nullable<int> _AplicaFormula;
        private Nullable<int> _FlaCon;
        private Nullable<int> _FlatAprobacion;
        private string _Observacion;
        private Nullable<int> _FlagRedondeo;

        private Nullable<int> _FlagAdelanto;
        private Nullable<Decimal> _MontoMinimo;
        private Nullable<int> _FlagCortesia;
        private Nullable<int> _FlagArchivo;
        private string _RutaArchivo;

        /**/
        #endregion

        #region Constructor

        /**/

        /**/

        public ADBE_TipoOperacion()
        {
            _pk = new ADBE_TipoOperacionPK();
        }

        #endregion

        #region Propiedades


        public ADBE_TipoOperacionPK Pk
        {
            get { return _pk; }
            set { _pk = value; }
        }

        public string RutaArchivo
        {
            get
            {
                return _RutaArchivo;
            }
            set
            {
                if (_RutaArchivo == value)
                    return;
                _RutaArchivo = value;
            }
        }

        public Nullable<int> FlagArchivo
        {
            get
            {
                return _FlagArchivo;
            }
            set
            {
                if (_FlagArchivo == value)
                    return;
                _FlagArchivo = value;
            }
        }

        public Nullable<int> FlagAdelanto
        {
            get
            {
                return _FlagAdelanto;
            }
            set
            {
                if (_FlagAdelanto == value)
                    return;
                _FlagAdelanto = value;
            }
        }

        public Nullable<Decimal> MontoMinimo
        {
            get
            {
                return _MontoMinimo;
            }
            set
            {
                if (_MontoMinimo == value)
                    return;
                _MontoMinimo = value;
            }
        }

        public Nullable<int> FlagCortesia
        {
            get
            {
                return _FlagCortesia;
            }
            set
            {
                if (_FlagCortesia == value)
                    return;
                _FlagCortesia = value;
            }
        }


        public Nullable<int> IdlistaBase
        {
            get { return _IdlistaBase; }
            set { _IdlistaBase = value; }
        }

        public Nullable<int> FlagRedondeo
        {
            get
            {
                return _FlagRedondeo;
            }
            set
            {
                if (_FlagRedondeo == value)
                    return;
                _FlagRedondeo = value;
            }
        }

        public Nullable<int> FlaCon
        {
            get
            {
                return _FlaCon;
            }
            set
            {
                if (_FlaCon == value)
                    return;
                _FlaCon = value;
            }
        }

        public Nullable<int> FlatAprobacion
        {
            get
            {
                return _FlatAprobacion;
            }
            set
            {
                if (_FlatAprobacion == value)
                    return;
                _FlatAprobacion = value;
            }
        }

        public string Observacion
        {
            get
            {
                return _Observacion;
            }
            set
            {
                if (_Observacion == value)
                    return;
                _Observacion = value;
            }
        }

        public Nullable<int> IdPolizaPlan
        {
            get { return _IdPolizaPlan; }
            set { _IdPolizaPlan = value; }
        }

        public Nullable<int> TPAplicaFactor
        {
            get { return _TPAplicaFactor; }
            set { _TPAplicaFactor = value; }
        }
        public Nullable<Decimal> Factor
        {
            get { return _Factor; }
            set { _Factor = value; }
        }

        public Nullable<int> AplicaFormula
        {
            get { return _AplicaFormula; }
            set { _AplicaFormula = value; }
        }

        public Nullable<DateTime> Fechinicio
        {
            get { return _Fechinicio; }
            set { _Fechinicio = value; }
        }
        public Nullable<DateTime> FechTermino
        {
            get { return _FechTermino; }
            set { _FechTermino = value; }
        }

        public string Moneda
        {
            get { return _Moneda; }
            set { _Moneda = value; }
        }

        public Nullable<Decimal> MonedaEmpresa
        {
            get { return _MonedaEmpresa; }
            set { _MonedaEmpresa = value; }
        }

        public string IpModificacion
        {
            get { return _IpModificacion; }
            set { _IpModificacion = value; }
        }

        public Nullable<int> TipEstado
        {
            get { return _TipEstado; }
            set { _TipEstado = value; }
        }

        public Nullable<int> TpContratoId
        {
            get { return _TpContratoId; }
            set { _TpContratoId = value; }
        }

        public string IpCreacion
        {
            get { return _IpCreacion; }
            set { _IpCreacion = value; }
        }

        public string UsuarioCreacion
        {
            get { return _UsuarioCreacion; }
            set { _UsuarioCreacion = value; }
        }

        public string CodigoContrato
        {
            get { return _CodigoContrato; }
            set { _CodigoContrato = value; }
        }

        public Nullable<DateTime> FechaCreacion
        {
            get { return _FechaCreacion; }
            set { _FechaCreacion = value; }
        }

        public string UsuarioModificacion
        {
            get { return _UsuarioModificacion; }
            set { _UsuarioModificacion = value; }
        }

        public Nullable<DateTime> FechaModificacion
        {
            get { return _FechaModificacion; }
            set { _FechaModificacion = value; }
        }

        #endregion
    }
}