using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiServices.Models.Entidades
{
     [Serializable()]
    public class COBE_ComponentePK
    {
        private string _CodigoComponente;
        #region Constructor
        public COBE_ComponentePK()
        {
          
        }

        public string CodigoComponente
        {
            get { return _CodigoComponente; }
            set { _CodigoComponente = value; }
        }

        #endregion
    }

    [Serializable()]
    public class COBE_Componente
    {
        #region Variables Locales
        private string _Nombre;
        private string _Descripcion;
        private Nullable<int> _Estado;
        private string _Observacion;
        private string _Compania;
        private string _ClasificadorMovimiento;
        private string _ConceptoGasto;
        private string _CentroCosto;
        private string _CuentaVentaComercial;
        private Nullable<Decimal> _CantidadKit;
        private Nullable<int> _IdArea;
        private Nullable<int> _IdRegimenVenta;
        private Nullable<int> _IndicadorMultiple;
        private Nullable<int> _NumeroMuestra;
        private Nullable<int> _IndicadorImpuesto;
        private Nullable<int> _IndicadorReemplazo;
        private Nullable<int> _IdUnidadMedida;
        private Nullable<int> _TipoComponente;
        private Nullable<Decimal> _TiempoMuestra;
        private COBE_ComponentePK _pk;
        private string _UsuarioCreacion;
        private Nullable<DateTime> _FechaCreacion;
        private string _UsuarioModificacion;
        private Nullable<DateTime> _FechaModificacion;
        private string _CuentaDescuentoVentaComercial;
        private Nullable<int> _Orden;
        private string _Sexo;
        private string _Abreviatura;
        private Nullable<int> _IdClasificacion;
        private Nullable<int> _IdGrupoComponente;


        #endregion

        #region Constructor


        public COBE_Componente()
        {
            _pk = new COBE_ComponentePK();
        }
        #endregion

        #region Propiedades

        public COBE_ComponentePK Pk
        {
            get { return _pk; }
            set { _pk = value; }
        }

        public Nullable<int> Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }

        public Nullable<int> IdGrupoComponente
        {
            get { return _IdGrupoComponente; }
            set { _IdGrupoComponente = value; }
        }

        public Nullable<int> IdClasificacion
        {
            get { return _IdClasificacion; }
            set { _IdClasificacion = value; }
        }

        public Nullable<int> IdRegimenVenta
        {
            get { return _IdRegimenVenta; }
            set { _IdRegimenVenta = value; }
        }

        public Nullable<Decimal> CantidadKit
        {
            get { return _CantidadKit; }
            set { _CantidadKit = value; }
        }

        public Nullable<int> IdUnidadMedida
        {
            get { return _IdUnidadMedida; }
            set { _IdUnidadMedida = value; }
        }

        public Nullable<Decimal> TiempoMuestra
        {
            get { return _TiempoMuestra; }
            set { _TiempoMuestra = value; }
        }

        public Nullable<int> IndicadorMultiple
        {
            get { return _IndicadorMultiple; }
            set { _IndicadorMultiple = value; }
        }

        public Nullable<int> IndicadorReemplazo
        {
            get { return _IndicadorReemplazo; }
            set { _IndicadorReemplazo = value; }
        }

        public Nullable<int> TipoComponente
        {
            get { return _TipoComponente; }
            set { _TipoComponente = value; }
        }

        public Nullable<int> IndicadorImpuesto
        {
            get { return _IndicadorImpuesto; }
            set { _IndicadorImpuesto = value; }
        }

        public Nullable<int> NumeroMuestra
        {
            get { return _NumeroMuestra; }
            set { _NumeroMuestra = value; }
        }

        public Nullable<int> IdArea
        {
            get { return _IdArea; }
            set { _IdArea = value; }
        }

        public string ConceptoGasto
        {
            get { return _ConceptoGasto; }
            set { _ConceptoGasto = value; }
        }

        public string Abreviatura
        {
            get { return _Abreviatura; }
            set { _Abreviatura = value; }
        }

        public string Compania
        {
            get { return _Compania; }
            set { _Compania = value; }
        }

        public string CentroCosto
        {
            get { return _CentroCosto; }
            set { _CentroCosto = value; }
        }

        public string ClasificadorMovimiento
        {
            get { return _ClasificadorMovimiento; }
            set { _ClasificadorMovimiento = value; }
        }

        public string Observacion
        {
            get { return _Observacion; }
            set { _Observacion = value; }
        }

        public string CuentaVentaComercial
        {
            get { return _CuentaVentaComercial; }
            set { _CuentaVentaComercial = value; }
        }

        public string CuentaDescuentoVentaComercial
        {
            get { return _CuentaDescuentoVentaComercial; }
            set { _CuentaDescuentoVentaComercial = value; }
        }

        public Nullable<int> Orden
        {
            get { return _Orden; }
            set { _Orden = value; }
        }


        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
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

        public string Sexo
        {
            get { return _Sexo; }
            set { _Sexo = value; }
        }

        #endregion
    }
}