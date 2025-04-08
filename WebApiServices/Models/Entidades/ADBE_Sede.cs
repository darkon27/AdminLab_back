using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace WebApiServices.Models.Entidades
{
    [Serializable()]
    public class ADBE_SedePK
    {
        #region "Variables Locales"
        private Nullable<Int32> _Sede;
        private ADBE_PersonaMast _Empresa;

        #endregion

        public ADBE_SedePK()
        {
            _Empresa = new ADBE_PersonaMast();
         }

        public ADBE_SedePK(Nullable<Int32> pSede)
            : this()
        {
            _Sede = pSede;
        }

        #region "Propiedades Públicas"

        public Nullable<Int32> IdSede
        {
            get { return _Sede; }
            set { _Sede = value; }
        }

        public ADBE_PersonaMast Empresa
        {
            get { return _Empresa; }
            set { _Empresa = value; }
        }
        
        #endregion
    }

    [Serializable()]
    public class ADBE_Sede
    {
        #region "Variables Locales"
        private ADBE_SedePK _PK;
        private string _SedCodigo;
        private string _DescripcionLocal;
        private string _SedeGrupo;
        private string _CuentaContableDefecto;
        private string _SedIp;
        private Nullable<int> _Estado;
        private Nullable<int> _Inicial;
        private string _Ruta;
        private Nullable<int> _IdTipoAdmision;
        private Nullable<int> _IdTipoPaciente;
        private Nullable<Int32> _TipoAtencion;  //1	AMBULATORIO // 2	HOSPITALIZADO // 3	EMERGENCIA //4	DOMICILIO
        private Nullable<int> _FlatCodigo;
        private Nullable<int> _Final;
        private Nullable<int> _Val_Ini;
        private Nullable<int> _FlatImpresion;
        private Nullable<DateTime> _Fecha;
        private string _UsuarioCreacion;
        private Nullable<DateTime> _FechaCreacion;
        private string _IpCreacion;
        private string _UsuarioModificacion;
        private Nullable<DateTime> _FechaModificacion;
        private string _IpModificacion;
        private string _Direccion;

        public ADBE_Sede()
        {
            _PK = new ADBE_SedePK();
        }

        public ADBE_SedePK PK
        {
            get { return _PK; }
            set { _PK = value; }
        }

        #endregion

        #region "Propiedades Públicas"

        public string SedeGrupo
        {
            get { return _SedeGrupo; }
            set { _SedeGrupo = value; }
        }

        public string SedCodigo
        {
            get { return _SedCodigo; }
            set { _SedCodigo = value; }
        }

        public string Ruta
        {
            get { return _Ruta; }
            set { _Ruta = value; }
        }

        public Nullable<int> IdTipoAdmision
        {
            get
            {
                return _IdTipoAdmision;
            }
            set
            {
                if (_IdTipoAdmision == value)
                    return;
                _IdTipoAdmision = value;
            }
        }

        public Nullable<int> IdTipoPaciente
        {
            get
            {
                return _IdTipoPaciente;
            }
            set
            {
                if (_IdTipoPaciente == value)
                    return;
                _IdTipoPaciente = value;
            }
        }

        public Nullable<int> FlatImpresion
        {
            get
            {
                return _FlatImpresion;
            }
            set
            {
                if (_FlatImpresion == value)
                    return;
                _FlatImpresion = value;
            }
        }

        public Nullable<Int32> TipoAtencion
        {
            get { return _TipoAtencion; }
            set { _TipoAtencion = value; }
        }

        public string CuentaContableDefecto
        {
            get { return _CuentaContableDefecto; }
            set { _CuentaContableDefecto = value; }
        }

        public string DescripcionLocal
        {
            get { return _DescripcionLocal; }
            set { _DescripcionLocal = value; }
        }

        public string Direccion
        {
            get { return _Direccion; }
            set { _Direccion = value; }
        }

        public string SedIp
        {
            get { return _SedIp; }
            set { _SedIp = value; }
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

        public Nullable<DateTime> Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }

        public Nullable<int> FlatCodigo
        {
            get { return _FlatCodigo; }
            set { _FlatCodigo = value; }
        }

        public Nullable<int> Inicial
        {
            get { return _Inicial; }
            set { _Inicial = value; }
        }

        public Nullable<int> Final
        {
            get { return _Final; }
            set { _Final = value; }
        }

        public Nullable<int> Val_Ini
        {
            get { return _Val_Ini; }
            set { _Val_Ini = value; }
        }

        #endregion
    }
}