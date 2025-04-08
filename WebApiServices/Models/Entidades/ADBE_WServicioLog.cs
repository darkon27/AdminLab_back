using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiServices.Models.Entidades
{
     [Serializable()]
    public class ADBE_WServicioLogPK
    {
        private string _IdCodigo;
        private Nullable<int> _Secuencia;
        #region Constructor
        public ADBE_WServicioLogPK()
        {
          
        }

        public Nullable<int> Secuencia
        {
            get { return _Secuencia; }
            set { _Secuencia = value; }
        }
        public string IdCodigo
        {
            get { return _IdCodigo; }
            set { _IdCodigo = value; }
        }

        #endregion
    }

    [Serializable()]
    public class ADBE_WServicioLog
    {
        #region Variables Locales

        private ADBE_WServicioLogPK _pk;

        private string _TipoMsj;
        private string _Sucursal;      
        private string _Observacion;
        private string _CodigoOA;
        private string _Parametros;
        private string _Trama;
        private Nullable<int> _IdSede;  
        private Nullable<int> _Estado;
        private Nullable<DateTime> _FechaIni;
        private Nullable<DateTime> _FechaFin;

        #endregion

        #region Constructor


        public ADBE_WServicioLog()
        {
            _pk = new ADBE_WServicioLogPK();
        }
        #endregion

        #region Propiedades

        public ADBE_WServicioLogPK Pk
        {
            get { return _pk; }
            set { _pk = value; }
        }

        public Nullable<int> Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }

        public Nullable<int> IdSede
        {
            get { return _IdSede; }
            set { _IdSede = value; }
        }

        public string Trama
        {
            get { return _Trama; }
            set { _Trama = value; }
        }

        public string CodigoOA
        {
            get { return _CodigoOA; }
            set { _CodigoOA = value; }
        }

        public string Parametros
        {
            get { return _Parametros; }
            set { _Parametros = value; }
        }

        public string Observacion
        {
            get { return _Observacion; }
            set { _Observacion = value; }
        }

        public string TipoMsj
        {
            get { return _TipoMsj; }
            set { _TipoMsj = value; }
        }

        public string Sucursal
        {
            get { return _Sucursal; }
            set { _Sucursal = value; }
        }

        public Nullable<DateTime> FechaIni
        {
            get { return _FechaIni; }
            set { _FechaIni = value; }
        }

        public Nullable<DateTime> FechaFin
        {
            get { return _FechaFin; }
            set { _FechaFin = value; }
        }

        #endregion
    }
}