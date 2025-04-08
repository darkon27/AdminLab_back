using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using RoyalSystems.Data;
using WebApiServices.Models.Request;
using WebApiServices.Models.Entidades;
using Newtonsoft.Json;

namespace WebApiServices.Models.Datos
{
    public class ADDAT_PersonaMast
    {
        #region Persona

        public List<WCO_BUSCARPERSONAUSUARIO_Result> ListaPersonaUsuario(WCO_BUSCARPERSONAUSUARIO_Result ObjPersona)
        {
            List<WCO_BUSCARPERSONAUSUARIO_Result> lst = new List<WCO_BUSCARPERSONAUSUARIO_Result>();
            using (var context = new BDComercialEntities())
            {
                Nullable<int> idEmpresaEmpleadora = null;
                Nullable<int> SoloBeneficiarios = null;
                if (ObjPersona.TipoPersona != "ID")
                {
                    if (ObjPersona.SoloBeneficiarios != -1)
                    {
                        SoloBeneficiarios = 1;
                    }
                }
                else
                {
                    if (ObjPersona.SoloBeneficiarios > 0)
                    {
                        SoloBeneficiarios = ObjPersona.SoloBeneficiarios;
                    }
                }                    

                if (ObjPersona.UneuNegocioId != -1)
                {
                    idEmpresaEmpleadora = 1;
                }
                lst = context.WCO_BUSCARPERSONAUSUARIO(ObjPersona.NombreCompleto, ObjPersona.TipoDocumento, ObjPersona.Documento,
                    ObjPersona.TipoPersona, SoloBeneficiarios, idEmpresaEmpleadora).ToList();
            }
            return lst;
        }

        public List<WCO_ListarPersonasGeneral_Result> ListaPersona(WCO_ListarPersonasGeneral_Result ObjPersona)
        {
            List<WCO_ListarPersonasGeneral_Result> lst = new List<WCO_ListarPersonasGeneral_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarPersonasGeneral(ObjPersona.NombreCompleto, ObjPersona.TipoDocumento,
                    ObjPersona.Documento, ObjPersona.TipoPersona, ObjPersona.Estado, ObjPersona.EsEmpleado).ToList();
            }
            return lst;
        }

        public List<WCO_TraerXCodigoPersonaId_Result> TraerXPersonaId(WCO_ListarPersonasGeneral_Result ObjPersona)
        {
            List<WCO_TraerXCodigoPersonaId_Result> lst = new List<WCO_TraerXCodigoPersonaId_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_TraerXCodigoPersonaId(ObjPersona.Persona).ToList();
            }
            return lst;
        }


        public List<WCO_TraerIDPersonaxCorreo_Result> TraerIDPersonaxCorreo(WCO_TraerIDPersonaxCorreo_Result ObjPersona)
        {
            List<WCO_TraerIDPersonaxCorreo_Result> lst = new List<WCO_TraerIDPersonaxCorreo_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_TraerIDPersonaxCorreo(ObjPersona.CorreoElectronico).ToList();
            }
            return lst;
        }



        ///<summary>Inserta el registro en la tabla PERSONAMAST.</summary>
        ///<param name="obj_pSA_Curriculo">Entidad de Negocio</param>
        ///<returns>Retorna el pPersona.</returns>
        ///<remarks><list type="bullet">
        ///<item><CreadoPor>Creado PorJordan Mateo Pizarro</CreadoPor></item>
        ///<item><FecCrea>17/10/2012</FecCrea></item></list></remarks>
        public int InsertarPersona(WCO_ListarPersonasGeneral_Result BE_pPersona)
        {
            int valor = 0;
            if (!ValidarExisteNombrePersona(BE_pPersona))
            {
                valor = -2;
                return valor;
            }
            if (!ValidarExisteDNIPersona(BE_pPersona))
            {
                valor = -3;
                return valor;
            }
           
            DataOperation dop_Operacion = new DataOperation("WCO_InsertarPersonaMast");
            Parameter[] prm_Params = new Parameter[34];
            prm_Params[0] = new Parameter("@pApellidoPaterno", BE_pPersona.ApellidoPaterno);
            prm_Params[1] = new Parameter("@pApellidoMaterno", BE_pPersona.ApellidoMaterno);
            prm_Params[2] = new Parameter("@pNombres", BE_pPersona.Nombres);
            prm_Params[3] = new Parameter("@pPerUsuarioCreacion", BE_pPersona.IngresoUsuario);
            prm_Params[4] = new Parameter("@pDocumento", BE_pPersona.Documento);
            prm_Params[5] = new Parameter("@pEstadoCivil", BE_pPersona.EstadoCivil);
            prm_Params[6] = new Parameter("@pFechaNacimiento", BE_pPersona.FechaNacimiento);
            prm_Params[7] = new Parameter("@pSexo", BE_pPersona.Sexo);
            prm_Params[8] = new Parameter("@pCorreoElectronico", BE_pPersona.CorreoElectronico);
            prm_Params[9] = new Parameter("@pTelefono", BE_pPersona.Telefono);
            prm_Params[10] = new Parameter("@pSunatUbigeo", BE_pPersona.SunatUbigeo);
            prm_Params[11] = new Parameter("@pDepartamento", BE_pPersona.DEPARTAMENTO);
            prm_Params[12] = new Parameter("@pProvincia", BE_pPersona.PROVINCIA);
            prm_Params[13] = new Parameter("@pDireccion", BE_pPersona.Direccion);
            prm_Params[14] = new Parameter("@pCelular", BE_pPersona.Celular);
            prm_Params[15] = new Parameter("@pPerIpCreacion", "");
            /**/
            prm_Params[16] = new Parameter("@pCodigoHC", BE_pPersona.ipUltimo);
            prm_Params[17] = new Parameter("@pPersonaAnt  ", BE_pPersona.PersonaAnt);
            prm_Params[18] = new Parameter("@pTipoPaciente", BE_pPersona.TipoPaciente);
            prm_Params[19] = new Parameter("@pCodigoAsegurado", BE_pPersona.CodAsegurado);
            prm_Params[20] = new Parameter("@pEdad", BE_pPersona.Edad);
            prm_Params[21] = new Parameter("@pTipoDocumento", BE_pPersona.TipoDocumento);
            prm_Params[22] = new Parameter("@ptipopersona", BE_pPersona.TipoPersona);
            prm_Params[23] = new Parameter("@pestado", BE_pPersona.Estado);
            /**/
            prm_Params[24] = new Parameter("@pCliente", BE_pPersona.EsCliente);
            prm_Params[25] = new Parameter("@pProveedor", BE_pPersona.EsProveedor);
            prm_Params[26] = new Parameter("@pEmpleado", BE_pPersona.EsEmpleado);
            prm_Params[27] = new Parameter("@pOtro", BE_pPersona.EsOtro);
            prm_Params[28] = new Parameter("@pPersona", DbType.Int32);
            prm_Params[29] = new Parameter("@DocumentoFiscal", BE_pPersona.DocumentoFiscal);
            prm_Params[30] = new Parameter("@DireccionReferencia", BE_pPersona.DireccionReferencia);
            prm_Params[31] = new Parameter("@Comentario", BE_pPersona.Comentario);
            prm_Params[32] = new Parameter("@DiaVencimiento", BE_pPersona.DiaVencimiento);
            prm_Params[33] = new Parameter("@IndicadorRetencion", BE_pPersona.IndicadorRetencion);
            //prm_Params[32] = new Parameter("@Companiaowner", BE_pPersona.Companiaowner);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            return int.Parse(dop_Operacion.GetParameterByName("@pPersona").Value.ToString());
        }

        ///<summary>Actualizar el registro en la tabla PERSONAMAST.</summary>
        ///<param name="obj_pSA_Curriculo">Entidad de Negocio</param>
        ///<remarks><list type="bullet">
        ///<item><CreadoPor>Creado PorJordan Mateo Pizarro</CreadoPor></item>
        ///<item><FecCrea>17/10/2012</FecCrea></item></list></remarks>
        public int ActualizarPersona(WCO_ListarPersonasGeneral_Result BE_pPersona)
        {
            int valor = 0;
            if (!ValidarExisteDNIPersona(BE_pPersona))
            {
                valor = -3;
                return valor;
            } 

            DataOperation dop_Operacion = new DataOperation("WCO_ActualizarPersonaMast");
            Parameter[] prm_Params = new Parameter[31];
            prm_Params[0] = new Parameter("@pPersona", BE_pPersona.Persona);
            prm_Params[1] = new Parameter("@pApellidoPaterno", BE_pPersona.ApellidoPaterno);
            prm_Params[2] = new Parameter("@pApellidoMaterno", BE_pPersona.ApellidoMaterno);
            prm_Params[3] = new Parameter("@pNombres", BE_pPersona.Nombres);
            prm_Params[4] = new Parameter("@pPerUltimoUsuario", BE_pPersona.UltimoUsuario);
            prm_Params[5] = new Parameter("@pDocumento", BE_pPersona.Documento);
            prm_Params[6] = new Parameter("@pEstadoCivil", BE_pPersona.EstadoCivil);
            prm_Params[7] = new Parameter("@pFechaNacimiento", BE_pPersona.FechaNacimiento);
            prm_Params[8] = new Parameter("@pSexo", BE_pPersona.Sexo);
            prm_Params[9] = new Parameter("@pCorreoElectronico", BE_pPersona.CorreoElectronico);
            prm_Params[10] = new Parameter("@pTelefono", BE_pPersona.Telefono);
            prm_Params[11] = new Parameter("@pDepartamento", BE_pPersona.DEPARTAMENTO);
            prm_Params[12] = new Parameter("@pProvincia", BE_pPersona.PROVINCIA);
            prm_Params[13] = new Parameter("@pDireccion", BE_pPersona.Direccion);
            prm_Params[14] = new Parameter("@pCelular", BE_pPersona.Celular);
            prm_Params[15] = new Parameter("@pSunatUbigeo", BE_pPersona.SunatUbigeo);
            prm_Params[16] = new Parameter("@pPerUltimoIp", BE_pPersona.ipUltimo);
            prm_Params[17] = new Parameter("@pPersonaAnt", BE_pPersona.PersonaAnt);
            prm_Params[18] = new Parameter("@pEdad", BE_pPersona.Edad);
            prm_Params[19] = new Parameter("@ptipopersona", BE_pPersona.TipoPersona);
            prm_Params[20] = new Parameter("@pestado", BE_pPersona.Estado);
            prm_Params[21] = new Parameter("@pCliente", BE_pPersona.EsCliente);
            prm_Params[22] = new Parameter("@pProveedor", BE_pPersona.EsProveedor);
            prm_Params[23] = new Parameter("@pEmpleado", BE_pPersona.EsEmpleado);
            prm_Params[24] = new Parameter("@pOtro", BE_pPersona.EsOtro);
            prm_Params[25] = new Parameter("@pTipoDocumento", BE_pPersona.TipoDocumento);
            prm_Params[26] = new Parameter("@DocumentoFiscal", BE_pPersona.DocumentoFiscal);
            prm_Params[27] = new Parameter("@DireccionReferencia", BE_pPersona.DireccionReferencia);
            prm_Params[28] = new Parameter("@Comentario", BE_pPersona.Comentario);
            prm_Params[29] = new Parameter("@DiaVencimiento", BE_pPersona.DiaVencimiento);
            prm_Params[30] = new Parameter("@IndicadorRetencion", BE_pPersona.IndicadorRetencion);
            //prm_Params[29] = new Parameter("@Companiaowner", BE_pPersona.Companiaowner);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            valor = BE_pPersona.Persona;
            return valor;
        }


        public void UnificarPersonaMast(WCO_ListarPersonasGeneral_Result objBEPersona)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_RegistrarUnificarPersonaMast");
            Parameter[] prm_Params = new Parameter[3];
            prm_Params[0] = new Parameter("@IdPersona", objBEPersona.Persona);
            prm_Params[1] = new Parameter("@Documento", objBEPersona.Documento);
            prm_Params[2] = new Parameter("@IdUnificador", objBEPersona.TipoPaciente);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }


        public void InactivarPersona(WCO_ListarPersonasGeneral_Result objBEPersona)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InactivarPersonaMast");
            Parameter[] prm_Params = new Parameter[3];
            prm_Params[0] = new Parameter("@IdPersona", objBEPersona.Persona);
            prm_Params[1] = new Parameter("@UsuarioModificacion", objBEPersona.UltimoUsuario);
            prm_Params[2] = new Parameter("@fechamodifoca", objBEPersona.UltimaFechaModif);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }

        ///<summary>Valida si el DNI ingresado en el registro de PERSONAMAST existe ya en la tabla.</summary>
        ///<param name="WCO_ListarPersonasGeneral_Result">Entidad de Negocio</param>
        ///<returns>Retorna Veredadero o Falso en caso ya exista el DNI ingresado.</returns>
        ///<remarks><list type="bullet">
        ///<item><CreadoPor>Creado PorJordan Mateo Pizarro</CreadoPor></item>
        ///<item><FecCrea>17/10/2012</FecCrea></item></list></remarks>
        public static bool ValidarExisteNombrePersona(WCO_ListarPersonasGeneral_Result BE_pPersona)
        {
            DataOperation dop_DataOperation = new DataOperation("WCO_ExistePersonaNombre");

            Parameter[] prm_Params = new Parameter[5];
            prm_Params[0] = new Parameter("@Persona", DBNull.Value);
            if (!string.IsNullOrEmpty(BE_pPersona.Persona.ToString())) prm_Params[0] = new Parameter("@Persona", BE_pPersona.Persona);
            prm_Params[1] = new Parameter("@ApellidoPaterno", BE_pPersona.ApellidoPaterno);
            prm_Params[2] = new Parameter("@ApellidoMaterno", BE_pPersona.ApellidoMaterno);
            prm_Params[3] = new Parameter("@Nombres", BE_pPersona.Nombres);
            prm_Params[4] = new Parameter("@flagSalida", DbType.Int32);
            dop_DataOperation.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation);
            if (Convert.ToInt32(dop_DataOperation.GetParameterByName("flagSalida").Value) >= 1)
            {
                return false;
            }
            return true;
        }

        ///<summary>Valida si el DNI ingresado en el registro de PERSONAMAST existe ya en la tabla.</summary>
        ///<param name="obj_pSA_Curriculo">Entidad de Negocio</param>
        ///<returns>Retorna Veredadero o Falso en caso ya exista el DNI ingresado.</returns>
        ///<remarks><list type="bullet">
        ///<item><CreadoPor>Creado PorJordan Mateo Pizarro</CreadoPor></item>
        ///<item><FecCrea>17/10/2012</FecCrea></item></list></remarks>
        public static bool ValidarExisteDNIPersona(WCO_ListarPersonasGeneral_Result BE_pPersona)
        {
            DataOperation dop_DataOperation = new DataOperation("WCO_ExistePersonaDNI");

            Parameter[] prm_Params = new Parameter[4];
            prm_Params[0] = new Parameter("@pPersona", DBNull.Value);
            if (!string.IsNullOrEmpty(BE_pPersona.Persona.ToString())) prm_Params[0] = new Parameter("@pPersona", BE_pPersona.Persona);
            prm_Params[1] = new Parameter("@pDocumento", BE_pPersona.Documento);
            prm_Params[2] = new Parameter("@PerTipoDoc", BE_pPersona.TipoDocumento);
            prm_Params[3] = new Parameter("@pflagSalida", DbType.Int32);
            dop_DataOperation.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation);
            if (Convert.ToInt32(dop_DataOperation.GetParameterByName("pflagSalida").Value) >= 1)
            {
                return false;
            }
            return true;
        }

        ///<summary>Actualizar el registro en la tabla PERSONAMAST.</summary>
        ///<param name="obj_pSA_Curriculo">Entidad de Negocio</param>
        ///<remarks><list type="bullet">
        ///<item><CreadoPor>Creado PorJordan Mateo Pizarro</CreadoPor></item>
        ///<item><FecCrea>17/10/2012</FecCrea></item></list></remarks>
        public int InsertarUsuarioWeb(WCO_ListarUsuarioWeb_Result BE_pPersona)
        {
            int valor = 0;
            try
            {
                DataOperation dop_Operacion = new DataOperation("WCO_InsertarPersonaPassword");
                Parameter[] prm_Params = new Parameter[3];
                prm_Params[0] = new Parameter("@IdPersona", BE_pPersona.IdPersona);
                prm_Params[1] = new Parameter("@PerNumeroDocumento", BE_pPersona.Documento);
                prm_Params[2] = new Parameter("@ClasificadorMovimiento", BE_pPersona.ClasificadorMovimiento);
                dop_Operacion.Parameters.AddRange(prm_Params);
                DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
                return valor = BE_pPersona.IdPersona;
            }
            catch (Exception ex)
            {
                UT_Kerberos.WriteLog(System.DateTime.Now + " | " + "InsertarUsuarioWeb|InsertarPersonaPassword = Exception " + JsonConvert.SerializeObject(ex, Newtonsoft.Json.Formatting.Indented));
                return 0;
            }
        }

        ///<summary>Actualizar el registro en la tabla PERSONAMAST.</summary>
        ///<param name="obj_pSA_Curriculo">Entidad de Negocio</param>
        ///<remarks><list type="bullet">
        ///<item><CreadoPor>Creado PorJordan Mateo Pizarro</CreadoPor></item>
        ///<item><FecCrea>17/10/2012</FecCrea></item></list></remarks>
        public int ActualizarUsuarioWeb(WCO_ListarUsuarioWeb_Result BE_pPersona)
        {
            int valor = 0;
            try
            {
                DataOperation dop_Operacion = new DataOperation("WCO_ActualizarPersonaPassword");
                Parameter[] prm_Params = new Parameter[4];
                prm_Params[0] = new Parameter("@IdPersona", BE_pPersona.IdPersona);
                prm_Params[1] = new Parameter("@PerNumeroDocumento", BE_pPersona.Documento);
                prm_Params[2] = new Parameter("@PasswordWeb", BE_pPersona.PasswordWeb);
                prm_Params[3] = new Parameter("@ClasificadorMovimiento", BE_pPersona.ClasificadorMovimiento);
                dop_Operacion.Parameters.AddRange(prm_Params);
                DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
                return valor = BE_pPersona.IdPersona;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        public List<WCO_ListarUsuarioWeb_Result> ListarUsuarioWeb(WCO_ListarUsuarioWeb_Result ObjEntidad)
        {
            List<WCO_ListarUsuarioWeb_Result> lst = new List<WCO_ListarUsuarioWeb_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarUsuarioWeb(ObjEntidad.IdPersona, ObjEntidad.NombreCompleto,
                    ObjEntidad.Documento, ObjEntidad.ClasificadorMovimiento).ToList();
            }
            return lst;
        }

        #endregion


        public List<Sp_PDP_validacion_Result> ListarConsentimiento(WCO_ListarPersonasGeneral_Result ObjEntidad)
        {
            List<Sp_PDP_validacion_Result> lst = new List<Sp_PDP_validacion_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.Sp_PDP_validacion(ObjEntidad.Documento).ToList();
            }
            return lst;
        }

        public List<WCO_ListarHomologacionCliente_Result> ListarHomologacionCliente(WCO_ListarHomologacionCliente_Result ObjEntidad)
        {
            List<WCO_ListarHomologacionCliente_Result> lst = new List<WCO_ListarHomologacionCliente_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarHomologacionCliente(ObjEntidad.IdPersona, ObjEntidad.Estado).ToList();
            }
            return lst;
        }

        public Nullable<int> InsertarHomologacionCliente(WCO_ListarHomologacionCliente_Result obj_pPersonaMast)
        {   
            try
            {
                DataOperation dop_Operacion = new DataOperation("WCO_InsertarHomologacionCliente");
                Parameter[] prm_Params = new Parameter[1];
                prm_Params[0] = new Parameter("@IdPersona", obj_pPersonaMast.IdPersona);
                dop_Operacion.Parameters.AddRange(prm_Params);
                DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
                return obj_pPersonaMast.IdPersona;
            }
            catch (Exception ex)
            {
                return 0;
            }
        
        }


    }
}