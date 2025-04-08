using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace WebApiServices.Models.Entidades
{
    [Serializable()]
    public class ADBE_AdmisionPK
    {
        #region "Variables Locales"
        private Nullable<Int32> _Admision;
        private ADBE_PersonaMast _Persona;
        private ADBE_PersonaMast _Empresa;
        private ADBE_PersonaMast _Titular;
        private ADBE_TipoOperacion _TipoOperacion;
        private ADBE_Sede _Sede;
        #endregion

        #region Constructor

        public ADBE_AdmisionPK()
        {
            _Persona = new ADBE_PersonaMast();
            _Empresa = new ADBE_PersonaMast();       
            _Sede = new ADBE_Sede();
            _Titular = new ADBE_PersonaMast();
            _TipoOperacion = new ADBE_TipoOperacion();
        }

        #endregion

        #region Propiedades

        public ADBE_TipoOperacion TipoOperacion
        {
            get { return _TipoOperacion; }
            set { _TipoOperacion = value; }
        }
        public ADBE_PersonaMast Persona
        {
            get { return _Persona; }
            set { _Persona = value; }
        }

        public ADBE_PersonaMast Empresa
        {
            get { return _Empresa; }
            set { _Empresa = value; }
        }

        public ADBE_Sede Sede
        {
            get { return _Sede; }
            set { _Sede = value; }
        }

        public ADBE_PersonaMast Titular
        {
            get { return _Titular; }
            set { _Titular = value; }
        }

        public Nullable<Int32> IdAdmision
        {
            get { return _Admision; }
            set { _Admision = value; }
        }

        #endregion

    }

    [Serializable()]
    public class ADBE_Admision
    {
        private ADBE_AdmisionPK _PK;
        private Nullable<DateTime> _FechaAdmision;
        private Nullable<DateTime> _FechaLimite;

        private string _ClasificadorMovimiento;
        private string _HistoriaClinica;
        private string _NroPeticion;
        private string _OrdenAtencion;
        private string _OrdenSinapa;
        private Nullable<int> _TipoPacienteId;
        private Nullable<int> _MedicoId;
        private Nullable<int> _Estado;  
        private Nullable<int> _UneuNegocioId;

        private string _UsuarioCreacion;
        private Nullable<DateTime> _FechaCreacion;
        private string _IpCreacion;
        private string _UsuarioModificacion;
        private Nullable<DateTime> _FechaModificacion;
        private string _IpModificacion;
        private string _MedicoDescripcion;
        private string _Cama;
        private Nullable<Decimal> _CoaSeguro;
        private Nullable<Int32> _TipoAtencion;  //1	AMBULATORIO // 2	HOSPITALIZADO // 3	EMERGENCIA //4	DOMICILIO
        private string _TipoOrden; //S	URGENTE     R	RUTINARIO       P	PRIORITARIO     
        private string _Comentario;
        private Nullable<int> _FlatCoaSeguro;
        private Nullable<int> _FlatMovilidad;
        private Nullable<decimal> _Afecto;
        private Nullable<decimal> _Igv;
        private Nullable<decimal> _Total;
        private Nullable<decimal> _Anticipo;
        private Nullable<decimal> _Saldo;
        private Nullable<decimal> _Redondeo;
        private Nullable<int> _AseguradoraId;
        private string _Aseguradora_Nombre;        
        private Nullable<int> _IdEspecialidad;
        private Nullable<int> _TipoAdmision;


        public ADBE_Admision()
        {
            _PK = new ADBE_AdmisionPK();
        }

        public ADBE_AdmisionPK PK
        {
            get { return _PK; }
            set { _PK = value; }
        }

        public string Aseguradora_Nombre
        {
            get
            {
                return _Aseguradora_Nombre;
            }
            set
            {
                if (_Aseguradora_Nombre == value)
                    return;
                _Aseguradora_Nombre = value;
            }
        }
        public Nullable<DateTime> FechaAdmision
        {
            get { return _FechaAdmision; }
            set { _FechaAdmision = value; }
        }
        
        public Nullable<int> TipoAdmision
        {
            get { return _TipoAdmision; }
            set { _TipoAdmision = value; }
        }

        public Nullable<int> UneuNegocioId
        {
            get { return _UneuNegocioId; }
            set { _UneuNegocioId = value; }
        }

        public Nullable<int> TipoPacienteId
        {
            get { return _TipoPacienteId; }
            set { _TipoPacienteId = value; }
        }
        public Nullable<int> FlatMovilidad
        {
            get { return _FlatMovilidad; }
            set { _FlatMovilidad = value; }
        }

        public Nullable<int> IdEspecialidad
        {
            get { return _IdEspecialidad; }
            set { _IdEspecialidad = value; }
        }

        public Nullable<int> AseguradoraId
        {
            get { return _AseguradoraId; }
            set { _AseguradoraId = value; }
        }
        
         public Nullable<int> FlatCoaSeguro
        {
            get { return _FlatCoaSeguro; }
            set { _FlatCoaSeguro = value; }
        }
        
        public string ClasificadorMovimiento
        {
            get
            {
                return _ClasificadorMovimiento;
            }
            set
            {
                if (_ClasificadorMovimiento == value)
                    return;
                _ClasificadorMovimiento = value;
            }
        }

        public string Comentario
        {
            get
            {
                return _Comentario;
            }
            set
            {
                if (_Comentario == value)
                    return;
                _Comentario = value;
            }
        }

        public string OrdenSinapa
        {
            get
            {
                return _OrdenSinapa;
            }
            set
            {
                if (_OrdenSinapa == value)
                    return;
                _OrdenSinapa = value;
            }
        }

        public Nullable<DateTime> FechaLimite
        {
            get
            {
                return _FechaLimite;
            }
            set
            {
                _FechaLimite = value;
            }
        }

        public string HistoriaClinica
        {
            get { return _HistoriaClinica; }
            set { _HistoriaClinica = value; }
        }

        public string Cama
        {
            get { return _Cama; }
            set { _Cama = value; }
        }

        public string NroPeticion
        {
            get { return _NroPeticion; }
            set { _NroPeticion = value; }
        }

        public Nullable<Decimal> CoaSeguro
        {
            get { return _CoaSeguro; }
            set { _CoaSeguro = value; }
        }

        public string OrdenAtencion
        {
            get { return _OrdenAtencion; }
            set { _OrdenAtencion = value; }
        }

        public string TipoOrden
        {
            get { return _TipoOrden; }
            set { _TipoOrden = value; }
        }

        public string MedicoDescripcion
        {
            get { return _MedicoDescripcion; }
            set { _MedicoDescripcion = value; }
        }

        public Nullable<int> MedicoId
        {
            get { return _MedicoId; }
            set { _MedicoId = value; }
        }

        public Nullable<Int32> TipoAtencion
        {
            get { return _TipoAtencion; }
            set { _TipoAtencion = value; }
        }
        public Nullable<decimal> Afecto
        {
            get { return _Afecto; }
            set { _Afecto = value; }
        }
        public Nullable<decimal> Igv
        {
            get { return _Igv; }
            set { _Igv = value; }
        }
        public Nullable<decimal> Total
        {
            get { return _Total; }
            set { _Total = value; }
        }
        public Nullable<decimal> Anticipo
        {
            get { return _Anticipo; }
            set { _Anticipo = value; }
        }
        public Nullable<decimal> Saldo
        {
            get { return _Saldo; }
            set { _Saldo = value; }
        }

        public Nullable<decimal> Redondeo
        {
            get { return _Redondeo; }
            set { _Redondeo = value; }
        }

        public Nullable<int> Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }

        public string UsuarioCreacion
        {
            get { return _UsuarioCreacion; }
            set { _UsuarioCreacion = value; }
        }

        public Nullable<DateTime> FechaCreacion
        {
            get { return _FechaCreacion; }
            set { _FechaCreacion = value; }
        }

        public string IpCreacion
        {
            get { return _IpCreacion; }
            set { _IpCreacion = value; }
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

        public string IpModificacion
        {
            get { return _IpModificacion; }
            set { _IpModificacion = value; }
        }
    }
}