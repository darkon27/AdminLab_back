using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace WebApiServices.Models.Entidades
{
    [Serializable()]
    public class ADBE_ReferenciaPK
    {
        #region "Variables Locales"

        private Nullable<Int32> _IdReferencia;
        private ADBE_Admision _Admision;

        #endregion

        #region Constructor

        public ADBE_ReferenciaPK()
        {
            _Admision = new ADBE_Admision();
        }

        #endregion

        #region Propiedades

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

        public Nullable<Int32> IdReferencia
        {
            get { return _IdReferencia; }
            set
            {
                if (_IdReferencia == value)
                    return;
                _IdReferencia = value;
            }
        }

        #endregion

    }

    [Serializable()]
    public class ADBE_Referencia
    {
        private ADBE_ReferenciaPK _PK;
        private string _CodigoOrden;
        private string _CodigoIngreso;
        private string _Comentario;
        private string _CodigoExcepcion;
        private Nullable<DateTime> _FechaRecepcionOrden;
        private Nullable<DateTime> _HoraRecepcionOrden;

        private Nullable<int> _Estado;
        private string _UsuarioCreacion;
        private Nullable<DateTime> _FechaCreacion;
        private string _IpCreacion;
        private string _UsuarioModificacion;
        private Nullable<DateTime> _FechaModificacion;
        private string _IpModificacion;

        public ADBE_Referencia()
        {
            _PK = new ADBE_ReferenciaPK();
        }

        public ADBE_ReferenciaPK PK
        {
            get { return _PK; }
            set { _PK = value; }
        }

        public Nullable<DateTime> FechaRecepcionOrden
        {
            get { return _FechaRecepcionOrden; }
            set { _FechaRecepcionOrden = value; }
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

        public string CodigoOrden
        {
            get
            {
                return _CodigoOrden;
            }
            set
            {
                if (_CodigoOrden == value)
                    return;
                _CodigoOrden = value;
            }
        }

        public Nullable<DateTime> HoraRecepcionOrden
        {
            get
            {
                return _HoraRecepcionOrden;
            }
            set
            {
                _HoraRecepcionOrden = value;
            }
        }

        public string CodigoIngreso
        {
            get { return _CodigoIngreso; }
            set { _CodigoIngreso = value; }
        }

        public string CodigoExcepcion
        {
            get { return _CodigoExcepcion; }
            set { _CodigoExcepcion = value; }
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