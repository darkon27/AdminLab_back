using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiServices.Models.Entidades
{
    public class ADBE_ContratoPrecio
    {
        private string _Sexo;
        private string _ClasificadorMovimiento;
        private string _CodigoHomologado;
        private string _CodigoComponente;
        private string _Nombre;
        private string _Abreviatura;
        private string _TipoFactor;

        private Nullable<DateTime> _FechaInicio;
        private Nullable<DateTime> _FechaTermino;
        
        private Nullable<Decimal> _Factor;
        private Nullable<Decimal> _PrecioUnitarioLista;
        private Nullable<Decimal> _PrecioUnitarioBase;
        private Nullable<Decimal> _Copago;
        private Nullable<Decimal> _TPFactor;
        private Nullable<Decimal> _Precio;
        private Nullable<Decimal> _PrecioPaciente;
        private Nullable<Decimal> _PrecioEmpresa;

        private Nullable<Int32> _TipoOperacionID;
        private Nullable<Int32> _TPContratoID;
        private Nullable<Int32> _TIPOADMISIONID;
        private Nullable<Int32> _IdClasificacion;
        private Nullable<Int32> _TipoPacienteId;
        private Nullable<Int32> _IdListaBase;
        private Nullable<Int32> _Persona;

        private Nullable<Int32> _TPAplicaFactor;
        private Nullable<Int32> _FlagRedondeo;
        private Nullable<Int32> _AplicaFormula;

        public Nullable<int> TipoOperacionID
        {
            get { return _TipoOperacionID; }
            set { _TipoOperacionID = value; }
        }

        public Nullable<int> TPContratoID
        {
            get { return _TPContratoID; }
            set { _TPContratoID = value; }
        }

        public Nullable<int> TIPOADMISIONID
        {
            get { return _TIPOADMISIONID; }
            set { _TIPOADMISIONID = value; }
        }

        public Nullable<int> IdClasificacion
        {
            get { return _IdClasificacion; }
            set { _IdClasificacion = value; }
        }

        public Nullable<int> TipoPacienteId
        {
            get { return _TipoPacienteId; }
            set { _TipoPacienteId = value; }
        }

        public Nullable<int> IdListaBase
        {
            get { return _IdListaBase; }
            set { _IdListaBase = value; }
        }

        public Nullable<int> Persona
        {
            get { return _Persona; }
            set { _Persona = value; }
        }

        public Nullable<int> TPAplicaFactor
        {
            get { return _TPAplicaFactor; }
            set { _TPAplicaFactor = value; }
        }

        public Nullable<int> FlagRedondeo
        {
            get { return _FlagRedondeo; }
            set { _FlagRedondeo = value; }
        }

        public Nullable<int> AplicaFormula
        {
            get { return _AplicaFormula; }
            set { _AplicaFormula = value; }
        }

        public Nullable<Decimal> PrecioEmpresa
        {
            get { return _PrecioEmpresa; }
            set { _PrecioEmpresa = value; }
        }

        public Nullable<Decimal> PrecioPaciente
        {
            get { return _PrecioPaciente; }
            set { _PrecioPaciente = value; }
        }

        public Nullable<Decimal> Precio
        {
            get { return _Precio; }
            set { _Precio = value; }
        }

        public Nullable<Decimal> TPFactor
        {
            get { return _TPFactor; }
            set { _TPFactor = value; }
        }

        public Nullable<Decimal> Copago
        {
            get { return _Copago; }
            set { _Copago = value; }
        }

        public Nullable<Decimal> PrecioUnitarioBase
        {
            get { return _PrecioUnitarioBase; }
            set { _PrecioUnitarioBase = value; }
        }

        public Nullable<Decimal> PrecioUnitarioLista
        {
            get { return _PrecioUnitarioLista; }
            set { _PrecioUnitarioLista = value; }
        }

        public Nullable<Decimal> Factor
        {
            get { return _Factor; }
            set { _Factor = value; }
        }
        
        public string TipoFactor
        {
            get
            {
                return _TipoFactor;
            }
            set
            {
                if (_TipoFactor == value)
                    return;
                _TipoFactor = value;
            }
        }

        public string Abreviatura
        {
            get
            {
                return _Abreviatura;
            }
            set
            {
                if (_Abreviatura == value)
                    return;
                _Abreviatura = value;
            }
        }

        public string Nombre
        {
            get
            {
                return _Nombre;
            }
            set
            {
                if (_Nombre == value)
                    return;
                _Nombre = value;
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

        public string Sexo
        {
            get { return _Sexo; }
            set { _Sexo = value; }
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
               
        public Nullable<DateTime> FechaInicio
        {
            get { return _FechaInicio; }
            set { _FechaInicio = value; }
        }

        public Nullable<DateTime> FechaTermino
        {
            get { return _FechaTermino; }
            set { _FechaTermino = value; }
        }

    }
}