using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApiServices.Models.Entidades
{
    [Serializable()]
    public class ADBE_ResumenesPK
    {
        #region Variables privadas

        private Nullable<int> _IdResumen;
        #endregion

        #region Propiedades publicas
        
        public Nullable<int> IdResumen
        {
            get { return _IdResumen; }
            set { _IdResumen = value; }
        }

  
        #endregion

        #region Constructor

        public ADBE_ResumenesPK()
        {      
           
        }

        #endregion
    }

    [Serializable()]
    public class ADBE_Resumenes
    {
        #region Variables Locales

        private ADBE_ResumenesPK _pk;
        private string _Nombre;
        private string _Ruc;
        private string _NroInicial;
        private string _NroFinal; 
        private string _RC;
        private Nullable<DateTime> _FechaEmision;
        private Nullable<DateTime> _FechaRespuesta;
        private Nullable<int> _CodigoRespuesta;
        private Nullable<int> _Estado;
        private Nullable<decimal> _TotalCargos;
        private Nullable<decimal> _TotalExoneradas;
        private Nullable<decimal> _TotalGrabadas;
        private Nullable<decimal> _TotalInafectas;
        private Nullable<decimal> _Total;
        private Nullable<decimal> _TotalExportacion;
        private Nullable<decimal> _ImporteExplicito_1000;
        private Nullable<decimal> _ImporteTotal_1000;
        private Nullable<decimal> _ImporteExplicito_2000;
        private Nullable<decimal> _ImporteTotal_2000;

        #endregion

        #region Constructor
        public ADBE_Resumenes()
        {
            _pk = new ADBE_ResumenesPK();
        }
        #endregion

        #region Propiedades

        public ADBE_ResumenesPK Pk
        {
            get { return _pk; }
            set { _pk = value; }
        }
        
        public Nullable<decimal> ImporteTotal_2000
        {
            get
            {
                return _ImporteTotal_2000;
            }
            set
            {
                if (_ImporteTotal_2000 == value)
                    return;
                _ImporteTotal_2000 = value;
            }
        }

        public Nullable<decimal> ImporteExplicito_2000
        {
            get
            {
                return _ImporteExplicito_2000;
            }
            set
            {
                if (_ImporteExplicito_2000 == value)
                    return;
                _ImporteExplicito_2000 = value;
            }
        }

        public Nullable<decimal> ImporteTotal_1000
        {
            get
            {
                return _ImporteTotal_1000;
            }
            set
            {
                if (_ImporteTotal_1000 == value)
                    return;
                _ImporteTotal_1000 = value;
            }
        }

        public Nullable<decimal> ImporteExplicito_1000
        {
            get
            {
                return _ImporteExplicito_1000;
            }
            set
            {
                if (_ImporteExplicito_1000 == value)
                    return;
                _ImporteExplicito_1000 = value;
            }
        }

        public Nullable<decimal> TotalGrabadas
        {
            get
            {
                return _TotalGrabadas;
            }
            set
            {
                if (_TotalGrabadas == value)
                    return;
                _TotalGrabadas = value;
            }
        }

        public Nullable<decimal> TotalExoneradas
        {
            get
            {
                return _TotalExoneradas;
            }
            set
            {
                if (_TotalExoneradas == value)
                    return;
                _TotalExoneradas = value;
            }
        }

        public Nullable<decimal> TotalCargos
        {
            get
            {
                return _TotalCargos;
            }
            set
            {
                if (_TotalCargos == value)
                    return;
                _TotalCargos = value;
            }
        }

        public Nullable<decimal> Total
        {
            get
            {
                return _Total;
            }
            set
            {
                if (_Total == value)
                    return;
                _Total = value;
            }
        }

        public Nullable<decimal> TotalInafectas
        {
            get
            {
                return _TotalInafectas;
            }
            set
            {
                if (_TotalInafectas == value)
                    return;
                _TotalInafectas = value;
            }
        }

        public Nullable<decimal> TotalExportacion
        {
            get
            {
                return _TotalExportacion;
            }
            set
            {
                if (_TotalExportacion == value)
                    return;
                _TotalExportacion = value;
            }
        }

        public string Ruc
        {
            get { return _Ruc; }
            set { _Ruc = value; }
        }

        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        public string NroInicial
        {
            get { return _NroInicial; }
            set { _NroInicial = value; }
        }

        public string NroFinal
        {
            get { return _NroFinal; }
            set { _NroFinal = value; }
        }

        public Nullable<int> CodigoRespuesta
        {
            get
            {
                return _CodigoRespuesta;
            }
            set
            {
                if (_CodigoRespuesta == value)
                    return;
                _CodigoRespuesta = value;
            }
        }

        public string RC
        {
            get { return _RC; }
            set { _RC = value; }
        }

        public Nullable<DateTime> FechaRespuesta
        {
            get
            {
                return _FechaRespuesta;
            }
            set
            {
                if (_FechaRespuesta == value)
                    return;
                _FechaRespuesta = value;
            }
        }

        public Nullable<DateTime> FechaEmision
        {
            get
            {
                return _FechaEmision;
            }
            set
            {
                if (_FechaEmision == value)
                    return;
                _FechaEmision = value;
            }
        }

        public Nullable<int> Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }
   
        #endregion
    }
}

