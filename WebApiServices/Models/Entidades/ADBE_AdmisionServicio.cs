using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace WebApiServices.Models.Entidades
{
    [Serializable()]
    public class ADBE_AdmisionServicioPK
    {
        #region "Variables Locales"

        private Nullable<Int32> _AdmisionServicio;
        private ADBE_Admision _Admision;
        private COBE_Componente _Componente;
        private ADBE_Unegocio _UnidadNegocio;
        private ADBE_PersonaMast _Persona;

        #endregion

        #region Constructor

        public ADBE_AdmisionServicioPK()
        {
            _Admision = new ADBE_Admision();
            _UnidadNegocio = new ADBE_Unegocio();
            _Componente = new COBE_Componente();
            _Persona = new ADBE_PersonaMast();
        }

        #endregion

        #region Propiedades

        public ADBE_Admision Admision
        {
            get { return _Admision; }
            set { _Admision = value; }
        }

        public COBE_Componente Componente
        {
            get { return _Componente; }
            set { _Componente = value; }
        }

        public ADBE_Unegocio UnidadNegocio
        {
            get { return _UnidadNegocio; }
            set { _UnidadNegocio = value; }
        }

        public ADBE_PersonaMast Persona
        {
            get { return _Persona; }
            set { _Persona = value; }
        }

        public Nullable<Int32> IdAdmServicio
        {
            get { return _AdmisionServicio; }
            set { _AdmisionServicio = value; }
        }

        #endregion

    }

    [Serializable()]
    public class ADBE_AdmisionServicio
    {
        private ADBE_AdmisionServicioPK _PK;
        private Nullable<int> _IdCobertura;
        private Nullable<int> _Cantidad;
        private Nullable<decimal> _Valor;
        private Nullable<int> _Conexion;
        private string _Periodo;
        private string _UsuarioCreacion;
        private Nullable<DateTime> _FechaCreacion;
        private string _IpCreacion;
        private string _UsuarioModificacion;
        private Nullable<DateTime> _FechaModificacion;
        private Nullable<int> _Estado;
        private string _IpModificacion;

        public ADBE_AdmisionServicio()
        {
            _PK = new ADBE_AdmisionServicioPK();
        }

        public ADBE_AdmisionServicioPK PK
        {
            get { return _PK; }
            set { _PK = value; }
        }

        public string Periodo
        {
            get
            {
                return _Periodo;
            }
            set
            {
                if (_Periodo == value)
                    return;
                _Periodo = value;
            }
        }

        public Nullable<int> IdCobertura
        {
            get { return _IdCobertura; }
            set { _IdCobertura = value; }
        }

        public Nullable<int> Cantidad
        {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }

        public Nullable<decimal> Valor
        {
            get { return _Valor; }
            set { _Valor = value; }
        }

        public Nullable<int> Conexion
        {
            get { return _Conexion; }
            set { _Conexion = value; }
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