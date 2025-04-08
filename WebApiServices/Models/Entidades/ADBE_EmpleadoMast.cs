using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Diagnostics;

namespace WebApiServices.Models.Entidades
{
    public class ADBE_EmpleadoMastPK
    {
        #region "Variables Privadas"

        private Nullable<Int32> _Persona;
        private Nullable<Int32> _TipoTrabajador;

        #endregion
       
        #region "Propiedades Públicas"

        public Nullable<Int32> Persona
        {
            get { return _Persona; }
            set { _Persona = value; }
        }

        public Nullable<Int32> TipoTrabajador
        {
            get { return _TipoTrabajador; }
            set { _TipoTrabajador = value; }
        }

        #region Constructor

        public ADBE_EmpleadoMastPK()
        {

        }
        #endregion

        #endregion
    }

    [Serializable()]
    public class ADBE_EmpleadoMast
    {
        private string _TipoPago;
        private ADBE_EmpleadoMastPK _pk;
        private Nullable<DateTime> _Fechaingreso;
        private string _CompaniaSocio;
        private string _TipoPlanilla;
        private string _TipoComtrato;
        private string _centroConsto;
        private string _ultimousuario;
        private Nullable<DateTime> _ultimaFechaMod;

        public Nullable<DateTime> UltimaFechaMod
        {
            get { return _ultimaFechaMod; }
            set { _ultimaFechaMod = value; }
        }

        public string Ultimousuario
        {
            get { return _ultimousuario; }
            set { _ultimousuario = value; }
        }


        public string CentroConsto
        {
            get { return _centroConsto; }
            set { _centroConsto = value; }
        }

        public string TipoPlanilla
        {
            get { return _TipoPlanilla; }
            set { _TipoPlanilla = value; }
        }

        public string TipoComtrato
        {
            get { return _TipoComtrato; }
            set { _TipoComtrato = value; }
        }

        private string _Estado;

        #region Constructor


        public ADBE_EmpleadoMast()
        {
            _pk = new ADBE_EmpleadoMastPK();
        }
        #endregion

        #region Propiedades

        public ADBE_EmpleadoMastPK Pk
        {
            get { return _pk; }
            set { _pk = value; }
        }

        public string TipoPago
        {
            get { return _TipoPago; }
            set { _TipoPago = value; }
        }
        public Nullable<DateTime> Fechaingreso
        {
            get { return _Fechaingreso; }
            set { _Fechaingreso = value; }
        }
        public string CompaniaSocio
        {
            get { return _CompaniaSocio; }
            set { _CompaniaSocio = value; }
        }
        public string Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }
        #endregion
    }
}