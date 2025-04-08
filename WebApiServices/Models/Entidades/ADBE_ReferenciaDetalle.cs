using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace WebApiServices.Models.Entidades
{

    [Serializable()]
    public class ADBE_ReferenciaDetallePK
    {
        #region "Variables Locales"

        private Nullable<Int32> _IdSecuencia;
        private ADBE_Referencia _Referencia;
        private ADBE_Sede _Sede;

        #endregion

        #region Constructor

        public ADBE_ReferenciaDetallePK()
        {
            _Referencia = new ADBE_Referencia();
            _Sede = new ADBE_Sede();
        }

        #endregion

        #region Propiedades

        public ADBE_Referencia Referencia
        {
            get { return _Referencia; }
            set { _Referencia = value; }
        }

        public ADBE_Sede Sede
        {
            get { return _Sede; }
            set { _Sede = value; }
        }

        public Nullable<Int32> IdSecuencia
        {
            get { return _IdSecuencia; }
            set { _IdSecuencia = value; }
        }

        #endregion

    }

    [Serializable()]
    public class ADBE_ReferenciaDetalle
    {
        private ADBE_ReferenciaDetallePK _PK;
        private string _CodigoBarra;
        private string _Identificador;
        private string _Nombre;
        private string _CodigoArea;
        private string _CodigoRes;
        private string _Activo;

        public ADBE_ReferenciaDetalle()
        {
            _PK = new ADBE_ReferenciaDetallePK();
        }

        public ADBE_ReferenciaDetallePK PK
        {
            get { return _PK; }
            set { _PK = value; }
        }

        public string CodigoBarra
        {
            get
            {
                return _CodigoBarra;
            }
            set
            {
                if (_CodigoBarra == value)
                    return;
                _CodigoBarra = value;
            }
        }

        public string CodigoRes
        {
            get
            {
                return _CodigoRes;
            }
            set
            {
                if (_CodigoRes == value)
                    return;
                _CodigoRes = value;
            }
        }

        public string Identificador
        {
            get { return _Identificador; }
            set { _Identificador = value; }
        }

        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        public string CodigoArea
        {
            get { return _CodigoArea; }
            set { _CodigoArea = value; }
        }

        public string Activo
        {
            get { return _Activo; }
            set { _Activo = value; }
        }
    }
}