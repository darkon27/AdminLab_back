using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApiServices.Models.Entidades
{
    [Serializable()]
    public class ADBE_PruebaReferenciaPK
    {
        #region "Variables Locales"

        private Nullable<Int32> _IdPruebaReferencia;
        private ADBE_Admision _Admision;

        #endregion

        #region Constructor

        public ADBE_PruebaReferenciaPK()
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

        public Nullable<Int32> IdPruebaReferencia
        {
            get { return _IdPruebaReferencia; }
            set
            {
                if (_IdPruebaReferencia == value)
                    return;
                _IdPruebaReferencia = value;
            }
        }

        #endregion

    }

    [Serializable()]
    public class ADBE_PruebaReferencia
    {
        private ADBE_PruebaReferenciaPK _PK;
        private string _Prioridad;
        private string _PrioridadReferencia;
        private Nullable<int> _Idsede;
        private Nullable<int> _IdSedeReferencia;
        private Nullable<int> _TipoAtencion;
        private Nullable<int> _TipoAtencionReferencia;
        private Nullable<int> _TipoContratoID;
        private Nullable<int> _TipoContratoReferencia;

        private Nullable<int> _Estado;
        private string _UsuarioCreacion;
        private Nullable<DateTime> _FechaCreacion;
        private string _IpCreacion;
        private string _UsuarioModificacion;
        private Nullable<DateTime> _FechaModificacion;
        private string _IpModificacion;

        public ADBE_PruebaReferencia()
        {
            _PK = new ADBE_PruebaReferenciaPK();
        }

        public ADBE_PruebaReferenciaPK PK
        {
            get { return _PK; }
            set { _PK = value; }
        }

        public Nullable<int> TipoContratoID
        {
            get
            {
                return _TipoContratoID;
            }
            set
            {
                if (_TipoContratoID == value)
                    return;
                _TipoContratoID = value;
            }
        }

        public Nullable<int> TipoContratoReferencia
        {
            get
            {
                return _TipoContratoReferencia;
            }
            set
            {
                if (_TipoContratoReferencia == value)
                    return;
                _TipoContratoReferencia = value;
            }
        }

        public Nullable<int> Idsede
        {
            get
            {
                return _Idsede;
            }
            set
            {
                if (_Idsede == value)
                    return;
                _Idsede = value;
            }
        }

        public Nullable<int> IdSedeReferencia
        {
            get
            {
                return _IdSedeReferencia;
            }
            set
            {
                if (_IdSedeReferencia == value)
                    return;
                _IdSedeReferencia = value;
            }
        }

        public Nullable<int> TipoAtencion
        {
            get
            {
                return _TipoAtencion;
            }
            set
            {
                if (_TipoAtencion == value)
                    return;
                _TipoAtencion = value;
            }
        }

        public Nullable<int> TipoAtencionReferencia
        {
            get
            {
                return _TipoAtencionReferencia;
            }
            set
            {
                if (_TipoAtencionReferencia == value)
                    return;
                _TipoAtencionReferencia = value;
            }
        }

        public string Prioridad
        {
            get
            {
                return _Prioridad;
            }
            set
            {
                if (_Prioridad == value)
                    return;
                _Prioridad = value;
            }
        }

        public string PrioridadReferencia
        {
            get
            {
                return _PrioridadReferencia;
            }
            set
            {
                if (_PrioridadReferencia == value)
                    return;
                _PrioridadReferencia = value;
            }
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