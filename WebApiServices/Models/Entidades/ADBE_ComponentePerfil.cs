using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiServices.Models.Entidades
{
    public class ADBE_ComponentePerfilPK
    {
        private ADBE_PersonaMast _Cliente;
        private ADBE_Unegocio _UnidadNegocio;
        private COBE_Componente _Componente;

        public COBE_Componente Componente
        {
            get { return _Componente; }
            set { _Componente = value; }
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

        public ADBE_PersonaMast Cliente
        {
            get
            {
                return _Cliente;
            }
            set
            {
                if (_Cliente == value)
                    return;
                _Cliente = value;
            }
        }

        #region Constructor

        public ADBE_ComponentePerfilPK()
        {
            _Cliente = new ADBE_PersonaMast();
            _Componente = new COBE_Componente();
            _UnidadNegocio = new ADBE_Unegocio();
        }

        #endregion

    }
    [Serializable()]
    public class ADBE_ComponentePerfil
    {
        #region Variables Locales

        private ADBE_ComponentePerfilPK _Pk;
        private string _CodigoHomologado;
        private Nullable<int> _Estado;
        private string _UsuarioCreacion;
        private Nullable<DateTime> _FechaCreacion;
        private string _UsuarioModificacion;
        private Nullable<DateTime> _FechaModificacion;
        private string _IpCreacion;
        private string _IpModificacion;

        #endregion

        #region Constructor

        public ADBE_ComponentePerfil()
        {
            _Pk = new ADBE_ComponentePerfilPK();
        }

        #endregion

        #region Propiedades

        public ADBE_ComponentePerfilPK Pk
        {
            get
            {
                return _Pk;
            }
            set
            {
                if (_Pk == value)
                    return;
                _Pk = value;
            }
        }

        public string CodigoHomologado
        {
            get
            {
                return _CodigoHomologado;
            }
            set
            {
                if (_CodigoHomologado == value)
                    return;
                _CodigoHomologado = value;
            }
        }

        public Nullable<int> Estado
        {
            get
            {
                return _Estado;
            }
            set
            {
                if (_Estado == value)
                    return;
                _Estado = value;
            }
        }

        public string UsuarioCreacion
        {
            get
            {
                return _UsuarioCreacion;
            }
            set
            {
                if (_UsuarioCreacion == value)
                    return;
                _UsuarioCreacion = value;
            }
        }

        public Nullable<DateTime> FechaCreacion
        {
            get
            {
                return _FechaCreacion;
            }
            set
            {
                if (_FechaCreacion == value)
                    return;
                _FechaCreacion = value;
            }
        }

        public string UsuarioModificacion
        {
            get
            {
                return _UsuarioModificacion;
            }
            set
            {
                if (_UsuarioModificacion == value)
                    return;
                _UsuarioModificacion = value;
            }
        }

        public Nullable<DateTime> FechaModificacion
        {
            get
            {
                return _FechaModificacion;
            }
            set
            {
                if (_FechaModificacion == value)
                    return;
                _FechaModificacion = value;
            }
        }

        public string IpCreacion
        {
            get
            {
                return _IpCreacion;
            }
            set
            {
                if (_IpCreacion == value)
                    return;
                _IpCreacion = value;
            }
        }

        public string IpModificacion
        {
            get
            {
                return _IpModificacion;
            }
            set
            {
                if (_IpModificacion == value)
                    return;
                _IpModificacion = value;
            }
        }


        #endregion

    }
}