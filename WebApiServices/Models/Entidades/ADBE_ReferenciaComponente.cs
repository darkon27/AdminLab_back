using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace WebApiServices.Models.Entidades
{
    [Serializable()]
    public class ADBE_ReferenciaComponentePK
    {
        #region "Variables Locales"

        private Nullable<Int32> _IdSecuencia;
        private ADBE_Referencia _Referencia;
        private ADBE_Admision _Admision;

        #endregion

        #region Constructor

        public ADBE_ReferenciaComponentePK()
        {
            _Referencia = new ADBE_Referencia();
            _Admision = new ADBE_Admision();
        }

        #endregion

        #region Propiedades

        public ADBE_Referencia Referencia
        {
            get { return _Referencia; }
            set { _Referencia = value; }
        }

        public ADBE_Admision Admision
        {
            get { return _Admision; }
            set { _Admision = value; }
        }

        public Nullable<Int32> IdSecuencia
        {
            get { return _IdSecuencia; }
            set { _IdSecuencia = value; }
        }

        #endregion

    }

    [Serializable()]
    public class ADBE_ReferenciaComponente
    {
        private ADBE_ReferenciaComponentePK _PK;
        private Nullable<int> _IdProveedorReferencia;
        private string _Des;
        private string _CodigoExterno;
        private string _ClasificadorMovimiento;
        private string _CodigoComponente;
        private string _CodigoHomologacion;
        private Nullable<int> _IdSedeReferencia;
        private Nullable<Int32> _Estado;
        private string _UsuarioCreacion;
        private Nullable<DateTime> _FechaCreacion;
        private string _UsuarioModificacion;
        private Nullable<DateTime> _FechaModificacion;

        public ADBE_ReferenciaComponente()
        {
            _PK = new ADBE_ReferenciaComponentePK();
        }

        public Nullable<Int32> IdProveedorReferencia
        {
            get { return _IdProveedorReferencia; }
            set { _IdProveedorReferencia = value; }
        }

        public ADBE_ReferenciaComponentePK PK
        {
            get { return _PK; }
            set { _PK = value; }
        }

        public string CodigoExterno
        {
            get
            {
                return _CodigoExterno;
            }
            set
            {
                if (_CodigoExterno == value)
                    return;
                _CodigoExterno = value;
            }
        }

        public string CodigoComponente
        {
            get
            {
                return _CodigoComponente;
            }
            set
            {
                if (_CodigoComponente == value)
                    return;
                _CodigoComponente = value;
            }
        }

        public string CodigoHomologacion
        {
            get
            {
                return _CodigoHomologacion;
            }
            set
            {
                if (_CodigoHomologacion == value)
                    return;
                _CodigoHomologacion = value;
            }
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

        public string Des
        {
            get
            {
                return _Des;
            }
            set
            {
                if (_Des == value)
                    return;
                _Des = value;
            }
        }

        public Nullable<Int32> IdSedeReferencia
        {
            get { return _IdSedeReferencia; }
            set { _IdSedeReferencia = value; }
        }

        public Nullable<Int32> Estado
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
    }
}