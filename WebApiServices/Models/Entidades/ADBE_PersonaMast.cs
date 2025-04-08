using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace WebApiServices.Models.Entidades
{
    [Serializable()]
    public class ADBE_PersonaMastPK
    {
        #region "Variables Locales"
        #endregion
        private Nullable<Int32> _Persona;
        public ADBE_PersonaMastPK()
        {
        }
        public ADBE_PersonaMastPK(Nullable<Int32> pPersona)
            : this()
        {
            _Persona = pPersona;
        }

        #region "Propiedades Públicas"
        public Nullable<Int32> IdPersona
        {
            get { return _Persona; }
            set { _Persona = value; }
        }

        #endregion
    }

    [Serializable()]
    public class ADBE_PersonaMast
    {
        private ADBE_PersonaMastPK _PK;
        private string _ApellidoPaterno;
        private string _ApellidoMaterno;
        private string _Nombres;
        private string _NombreCompleto;
        private string _Telefono;
        private string _PerSexo;
        private string _PerEstadoCivil;
        private string _PerTipoDoc;
        private string _PerNumeroDocumento;
        private string _PasswordWeb;
        private string _PerDocumentoFiscal;
        private string _Nacionalidad;
        private string _Direccion;
        private string _DireccionReferencia;
        private Nullable<DateTime> _PerFechaNacimiento;
        private Nullable<int> _PerPaisNacimiento;
        private Nullable<int> _PerCiudadNacimiento;
        private Nullable<int> _PerPais;
        private string _PerDpto;
        private string _PerProvincia;
        private string _PerDistrito;
        private string _PerTipoVia;
        private string _EsEmpleado; //se adiciono esta propiedad
        private string _EsCliente;
        private string _EsProveedor;
        private string _EsOtro;
        private string _Espaciente;
        private string _EsEmpresa;
        private string _PerCalle;
        private string _PerUrbanizacion;
        private string _PerTelefono;
        private string _PerCelular;
        private string _PerCorreoElectronico;
        private string _PerPerfilProfesional;
        private string _PerUsuarioCreacion;
        private Nullable<DateTime> _PerFechaCreacion;
        private string _PerIpCreacion;
        private string _PerUltimoUsuario;
        private Nullable<DateTime> _PerUltimaFecModif;
        private string _Companiaowner;
        private string _SunatUbigeo;
        private string _PerUltimoIp;
        private Nullable<int> _SoloBeneficiarios;
        private Nullable<int> _idEmpresaEmpleadora;
        private Nullable<int> _Edad;
        private string _codigohc;
        private string _conpersonclinica;
        private Nullable<int> _tipoPaciente;
        private Nullable<int> _codasegurado;
        private string _tipopersona;
        private string _estado;
        private string _Comentario;
        private Nullable<int> _DiaVencimiento;
        private Nullable<int> _IndicadorRetencion;
        
        public string Espaciente
        {
            get { return _Espaciente; }
            set { _Espaciente = value; }
        }

        public string EsEmpresa
        {
            get { return _EsEmpresa; }
            set { _EsEmpresa = value; }
        }

        public string PasswordWeb
        {
            get { return _PasswordWeb; }
            set { _PasswordWeb = value; }
        }

        public string Comentario
        {
            get
            {
                return _Comentario;
            }
            set
            {
                if (_Comentario == value)
                    return;
                _Comentario = value;
            }
        }

        public string EsOtro
        {
            get { return _EsOtro; }
            set { _EsOtro = value; }
        }
        public string EsProveedor
        {
            get { return _EsProveedor; }
            set { _EsProveedor = value; }
        }
        public string EsCliente
        {
            get { return _EsCliente; }
            set { _EsCliente = value; }
        }

        public string Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }
        /**/
        public string Tipopersona
        {
            get { return _tipopersona; }
            set { _tipopersona = value; }
        }
        
        public Nullable<int> Edad
        {
            get { return _Edad; }
            set { _Edad = value; }
        }

        public Nullable<int> IndicadorRetencion
        {
            get { return _IndicadorRetencion; }
            set { _IndicadorRetencion = value; }
        }
        public string Codigohc
        {
            get { return _codigohc; }
            set { _codigohc = value; }
        }

        public string Conpersonclinica
        {
            get { return _conpersonclinica; }
            set { _conpersonclinica = value; }
        }

        public Nullable<int> SoloBeneficiarios
        {
            get { return _SoloBeneficiarios; }
            set { _SoloBeneficiarios = value; }
        }

        public Nullable<int> DiaVencimiento
        {
            get { return _DiaVencimiento; }
            set { _DiaVencimiento = value; }
        }

        public Nullable<int> idEmpresaEmpleadora
        {
            get { return _idEmpresaEmpleadora; }
            set { _idEmpresaEmpleadora = value; }
        }

        public Nullable<int> TipoPaciente
        {
            get { return _tipoPaciente; }
            set { _tipoPaciente = value; }
        }
        public Nullable<int> Codasegurado
        {
            get { return _codasegurado; }
            set { _codasegurado = value; }
        }
        /**/
        public string Companiaowner
        {
            get { return _Companiaowner; }
            set { _Companiaowner = value; }
        }

        public ADBE_PersonaMast()
        {
            _PK = new ADBE_PersonaMastPK();
        }

        public ADBE_PersonaMast(Nullable<Int32> pPersona)
            : this()
        {
            _PK = new ADBE_PersonaMastPK(pPersona);
        }

        public ADBE_PersonaMastPK PK
        {
            get { return _PK; }
            set { _PK = value; }
        }

        public string ApellidoPaterno
        {
            get { return _ApellidoPaterno; }
            set { _ApellidoPaterno = value; }
        }
        public string ApellidoMaterno
        {
            get { return _ApellidoMaterno; }
            set { _ApellidoMaterno = value; }
        }
        public string Nombres
        {
            get { return _Nombres; }
            set { _Nombres = value; }
        }
        public string Nacionalidad
        {
            get { return _Nacionalidad; }
            set { _Nacionalidad = value; }
        }

        public string NombreCompleto
        {
            get { return _NombreCompleto; }
            set { _NombreCompleto = value; }
        }

        public string Direccion
        {
            get { return _Direccion; }
            set { _Direccion = value; }
        }

        public string DireccionReferencia
        {
            get { return _DireccionReferencia; }
            set { _DireccionReferencia = value; }
        }

        public string Telefono
        {
            get { return _Telefono; }
            set { _Telefono = value; }
        }

        public string PerSexo
        {
            get { return _PerSexo; }
            set { _PerSexo = value; }
        }
        public string PerEstadoCivil
        {
            get { return _PerEstadoCivil; }
            set { _PerEstadoCivil = value; }
        }
        public string PerTipoDoc
        {
            get { return _PerTipoDoc; }
            set { _PerTipoDoc = value; }
        }

        public string PerNumeroDocumento
        {
            get { return _PerNumeroDocumento; }
            set { _PerNumeroDocumento = value; }
        }

        public string PerDocumentoFiscal
        {
            get { return _PerDocumentoFiscal; }
            set { _PerDocumentoFiscal = value; }
        }

        public Nullable<DateTime> PerFechaNacimiento
        {
            get { return _PerFechaNacimiento; }
            set { _PerFechaNacimiento = value; }
        }
        public Nullable<int> PerPaisNacimiento
        {
            get { return _PerPaisNacimiento; }
            set { _PerPaisNacimiento = value; }
        }
        public Nullable<int> PerCiudadNacimiento
        {
            get { return _PerCiudadNacimiento; }
            set { _PerCiudadNacimiento = value; }
        }
        public Nullable<int> PerPais
        {
            get { return _PerPais; }
            set { _PerPais = value; }
        }

        public string PerDpto
        {
            get { return _PerDpto; }
            set { _PerDpto = value; }
        }

        public string PerProvincia
        {
            get { return _PerProvincia; }
            set { _PerProvincia = value; }
        }

        public string PerDistrito
        {
            get { return _PerDistrito; }
            set { _PerDistrito = value; }
        }

        public string PerTipoVia
        {
            get { return _PerTipoVia; }
            set { _PerTipoVia = value; }
        }

        //se adiciono esta propiedad
        public string EsEmpleado
        {
            get { return _EsEmpleado; }
            set { _EsEmpleado = value; }
        }

        public string PerCalle
        {
            get { return _PerCalle; }
            set { _PerCalle = value; }
        }

        public string PerUrbanizacion
        {
            get { return _PerUrbanizacion; }
            set { _PerUrbanizacion = value; }
        }

        public string PerTelefono
        {
            get { return _PerTelefono; }
            set { _PerTelefono = value; }
        }

        public string PerCelular
        {
            get { return _PerCelular; }
            set { _PerCelular = value; }
        }

        public string PerCorreoElectronico
        {
            get { return _PerCorreoElectronico; }
            set { _PerCorreoElectronico = value; }
        }

        public string PerPerfilProfesional
        {
            get { return _PerPerfilProfesional; }
            set { _PerPerfilProfesional = value; }
        }

        public string PerUsuarioCreacion
        {
            get { return _PerUsuarioCreacion; }
            set { _PerUsuarioCreacion = value; }
        }

        public Nullable<DateTime> PerFechaCreacion
        {
            get { return _PerFechaCreacion; }
            set { _PerFechaCreacion = value; }
        }

        public string PerIpCreacion
        {
            get { return _PerIpCreacion; }
            set { _PerIpCreacion = value; }
        }

        public string PerUltimoUsuario
        {
            get { return _PerUltimoUsuario; }
            set { _PerUltimoUsuario = value; }
        }

        public Nullable<DateTime> PerUltimaFecModif
        {
            get { return _PerUltimaFecModif; }
            set { _PerUltimaFecModif = value; }
        }

        public string PerUltimoIp
        {
            get { return _PerUltimoIp; }
            set { _PerUltimoIp = value; }
        }

        public string SunatUbigeo
        {
            get { return _SunatUbigeo; }
            set { _SunatUbigeo = value; }
        }

    }
}