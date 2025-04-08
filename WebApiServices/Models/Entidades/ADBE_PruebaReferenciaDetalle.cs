using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApiServices.Models.Entidades
{
    [Serializable()]
    public class ADBE_PruebaReferenciaDetallePK
    {
        #region "Variables Locales"

        private Nullable<Int32> _Secuencia;
        private ADBE_PruebaReferencia _Referencia;

        #endregion

        #region Constructor

        public ADBE_PruebaReferenciaDetallePK()
        {
            _Referencia = new ADBE_PruebaReferencia();
        }

        #endregion

        #region Propiedades

        public ADBE_PruebaReferencia Referencia
        {
            get { return _Referencia; }
            set { _Referencia = value; }
        }

        public Nullable<Int32> Secuencia
        {
            get { return _Secuencia; }
            set { _Secuencia = value; }
        }

        #endregion

    }

    [Serializable()]
    public class ADBE_PruebaReferenciaDetalle
    {
        private ADBE_PruebaReferenciaDetallePK _PK;
        private Nullable<int> _IdProvedorReferencia;
        private Nullable<int> _IdAdmisionReferencia;
        private string _CodigoComponente;
        private string _CodigoComponenteReferencia;
        private Nullable<int> _Estado;
        private string _UsuarioCreacion;
        private Nullable<DateTime> _FechaCreacion;
        private string _IpCreacion;
        private string _UsuarioModificacion;
        private Nullable<DateTime> _FechaModificacion;
        private string _IpModificacion;

        public ADBE_PruebaReferenciaDetalle()
        {
            _PK = new ADBE_PruebaReferenciaDetallePK();
        }

        public ADBE_PruebaReferenciaDetallePK PK
        {
            get { return _PK; }
            set { _PK = value; }
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

        public Nullable<int> IdProvedorReferencia
        {
            get
            {
                return _IdProvedorReferencia;
            }
            set
            {
                if (_IdProvedorReferencia == value)
                    return;
                _IdProvedorReferencia = value;
            }
        }

        public Nullable<int> IdAdmisionReferencia
        {
            get
            {
                return _IdAdmisionReferencia;
            }
            set
            {
                if (_IdAdmisionReferencia == value)
                    return;
                _IdAdmisionReferencia = value;
            }
        }

        public string CodigoComponenteReferencia
        {
            get { return _CodigoComponenteReferencia; }
            set { _CodigoComponenteReferencia = value; }
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