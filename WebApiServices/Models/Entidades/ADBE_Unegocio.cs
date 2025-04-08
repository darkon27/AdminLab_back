using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiServices.Models.Entidades
{
     [Serializable()]
    public class ADBE_UnegocioPK
    {
        #region Variables privadas
        private Nullable<int> _UneuNegocioId;

        #endregion

        #region Propiedades publicas

        public Nullable<int> UneuNegocioId
        {
            get { return _UneuNegocioId; }
            set { _UneuNegocioId = value; }
        }

        #endregion

        #region Constructor
        public ADBE_UnegocioPK()
        {

        }
        #endregion
    }

    [Serializable()]
    public class ADBE_Unegocio
    {
        #region Variables Locales
        private string _CompaniaCodigo;
        private string _UneCodigo;
        private string _UneDescripcion;
        private Nullable<int> _UneEstado;
        private ADBE_UnegocioPK _pk;
        private string _UsuarioCreacion;
        private Nullable<DateTime> _FechaCreacion;
        private string _UsuarioModificacion;
        private Nullable<DateTime> _FechaModificacion;
        private string _IpCreacion;
        private string _IpModificacion;
        #endregion

        #region Constructor


        public ADBE_Unegocio()
        {
            _pk = new ADBE_UnegocioPK();
        }
        #endregion

        #region Propiedades

        public ADBE_UnegocioPK Pk
        {
            get { return _pk; }
            set { _pk = value; }
        }

        public string IpModificacion
        {
            get { return _IpModificacion; }
            set { _IpModificacion = value; }
        }

        public string CompaniaCodigo
        {
            get { return _CompaniaCodigo; }
            set { _CompaniaCodigo = value; }
        }

        public string UneDescripcion
        {
            get { return _UneDescripcion; }
            set { _UneDescripcion = value; }
        }

        public string UneCodigo
        {
            get { return _UneCodigo; }
            set { _UneCodigo = value; }
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

        public Nullable<DateTime> FechaCreacion
        {
            get { return _FechaCreacion; }
            set { _FechaCreacion = value; }
        }

        public Nullable<int> UneEstado
        {
            get { return _UneEstado; }
            set { _UneEstado = value; }
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