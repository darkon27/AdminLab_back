using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using RoyalSystems.Data;
using System.IO;
using System.Transactions;
using WebApiServices.Models.Entidades;
using WebApiServices.Models.Request;
using System.Diagnostics;
using System.Data.Entity;

namespace WebApiServices.Models.Datos
{
    public class ADDAT_Usuario
    {
        #region usuario
        public List<WCO_TraerIDPersonaxCorreo_Result> TraerIDPersonaxCorreo(WCO_ListarUsuario_Result ObjEntidad)
        {
            List<WCO_TraerIDPersonaxCorreo_Result> lst = new List<WCO_TraerIDPersonaxCorreo_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_TraerIDPersonaxCorreo(ObjEntidad.correo_empresa).ToList();
            }
            return lst;
        }

        public List<WCO_ListarUsuario_Result> ListarUsuario(WCO_ListarUsuario_Result ObjEntidad)
        {
            List<WCO_ListarUsuario_Result> lst = new List<WCO_ListarUsuario_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarUsuario(ObjEntidad.Perfil, ObjEntidad.Usuario, ObjEntidad.NombreCompleto, ObjEntidad.Estado).ToList();
            }
            return lst;
        }

        public List<WCO_ListarComboPerfil_Result> ListarPerfil(WCO_ListarComboPerfil_Result ObjEntidad)
        {
            List<WCO_ListarComboPerfil_Result> lst = new List<WCO_ListarComboPerfil_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarComboPerfil().ToList();
            }
            return lst;
        }


        public List<WCO_AccesoUsuario_Result> ListaAccesoUsuario(WCO_AccesoUsuario_Result ObjUsuario)
        {
            List<WCO_AccesoUsuario_Result> lst = new List<WCO_AccesoUsuario_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_AccesoUsuario(ObjUsuario.Usuario, ObjUsuario.Clave, ObjUsuario.IdSede, ObjUsuario.Persona, ObjUsuario.Documento, ObjUsuario.ExpirarPasswordFlag).ToList();
            }
            return lst;
        }

        ///<summary>Método para listar registros de la tabla Usuario con paginación</summary>
        ///<param name="obj_pBEPerfil">Entidad de Negocio</param>
        ///<returns>Listado de los registros de la página actual.</returns>
        ///<remarks><list type="bullet">
        ///<item><CreadoPor>Creado Por Isaac Jurado</CreadoPor></item>
        ///<item><FecCrea>09/12/2011</FecCrea></item></list></remarks>
        public List<WCO_LISTARASIGNARPERFILPAG_Result> ListaUsuario(WCO_LISTARASIGNARPERFILPAG_Result obj_pBEPerfil)
        {
            List<WCO_LISTARASIGNARPERFILPAG_Result> lst = new List<WCO_LISTARASIGNARPERFILPAG_Result>();
            using (var context = new BDComercialEntities())
            {


                lst = context.WCO_LISTARASIGNARPERFILPAG(obj_pBEPerfil.PERFIL, obj_pBEPerfil.USUARIO, obj_pBEPerfil.NOMBRECOMPLETO).ToList();
            }
            return lst;
        }

        ///<summary>Método para listar registros de la tabla Usuario con paginación</summary>
        ///<param name="obj_pBEPerfil">Entidad de Negocio</param>
        ///<returns>Listado de los registros de la página actual.</returns>
        ///<remarks><list type="bullet">
        ///<item><CreadoPor>Creado Por Isaac Jurado</CreadoPor></item>
        ///<item><FecCrea>09/12/2011</FecCrea></item></list></remarks>
        public List<WCO_TRAERUSUARIOXCODIGO_Result> TrareUsuarioxCodigo(WCO_TRAERUSUARIOXCODIGO_Result obj_pBEPerfil)
        {
            List<WCO_TRAERUSUARIOXCODIGO_Result> lst = new List<WCO_TRAERUSUARIOXCODIGO_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_TRAERUSUARIOXCODIGO(obj_pBEPerfil.USUARIO).ToList();
            }
            return lst;
        }

        public bool Exiteusuario(WCO_ListarUsuario_Result obj_pBEUsuario)
        {
            DataOperation dop_DataOperation = new DataOperation("WCO_Exiteusuario");
            Parameter[] prm_Params = new Parameter[2];
            prm_Params[0] = new Parameter("@pUsuario", obj_pBEUsuario.Usuario);
            prm_Params[1] = new Parameter("@pExiste", DbType.Int32);
            dop_DataOperation.Parameters.AddRange(prm_Params);

            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation);
            if (Convert.ToInt32(dop_DataOperation.GetParameterByName("@pExiste").Value) == 1)
            {
                return true;
            }
            return false;
        }

        public string Insertar(WCO_ListarUsuario_Result obj_pBEUsuario)
        {
            DataOperation dop_DataOperation = new DataOperation("WCO_INSERTARUSUARIO");
            try
            {
                Parameter[] prm_Params = new Parameter[10];
                prm_Params[0] = new Parameter("@USUARIO", obj_pBEUsuario.Usuario);
                prm_Params[1] = new Parameter("@NOMBRE", obj_pBEUsuario.NombreCompleto);
                prm_Params[2] = new Parameter("@PERSONA", obj_pBEUsuario.Persona);
                prm_Params[3] = new Parameter("@ULTIMOUSUARIO", obj_pBEUsuario.UltimoUsuario);
                prm_Params[4] = new Parameter("@Tipousuario", obj_pBEUsuario.TipoUsuario);
                prm_Params[5] = new Parameter("@PERFIL", obj_pBEUsuario.Perfil);
                prm_Params[6] = new Parameter("@IdSede", 1);
                prm_Params[7] = new Parameter("@IdClasificadorMovimiento", obj_pBEUsuario.ClasificadorMovimiento);
                prm_Params[8] = new Parameter("@PASSWORD", obj_pBEUsuario.Clave);
                prm_Params[9] = new Parameter("@CorreoElectronico", obj_pBEUsuario.correo_empresa);

                dop_DataOperation.Parameters.AddRange(prm_Params);
                DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation);
                return "1";
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                return "0" + ex;
            }
            catch (Exception ex)
            {
                return "0" + ex;
            }
        }

        public string Inactivar(WCO_ListarUsuario_Result obj_pBEUsuario)
        {
            try
            {
                DataOperation dop_DataOperation = new DataOperation("WCO_INACTIVARUSUARIO");
                Parameter[] prm_Params = new Parameter[2];
                prm_Params[0] = new Parameter("@USUARIO", obj_pBEUsuario.Usuario);
                prm_Params[1] = new Parameter("@ULTIMOUSUARIO", obj_pBEUsuario.UltimoUsuario);
                dop_DataOperation.Parameters.AddRange(prm_Params);
                DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation);
                return "1";
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                return "0" + ex;
            }
            catch (Exception ex)
            {
                return "0" + ex;
            }
        }

        public string Actualizar(WCO_ListarUsuario_Result obj_pBEUsuario)
        {
            DataOperation dop_DataOperation = new DataOperation("WCO_ACTUALIZARUSUARIO");
            try
            {
                Parameter[] prm_Params = new Parameter[11];
                prm_Params[0] = new Parameter("@USUARIO", obj_pBEUsuario.Usuario);
                prm_Params[1] = new Parameter("@EXPERIRARPASSWORDFLAG", obj_pBEUsuario.ExpirarPasswordFlag);
                prm_Params[2] = new Parameter("@ESTADO", obj_pBEUsuario.Estado);
                prm_Params[3] = new Parameter("@ULTIMOUSUARIO", obj_pBEUsuario.UltimoUsuario);
                prm_Params[4] = new Parameter("@Tipousuario", obj_pBEUsuario.TipoUsuario);
                prm_Params[5] = new Parameter("@PERFIL", obj_pBEUsuario.Perfil);
                prm_Params[6] = new Parameter("@NOMBRE", obj_pBEUsuario.NombreCompleto);
                prm_Params[7] = new Parameter("@IdSede", 1);
                prm_Params[8] = new Parameter("@IdClasificadorMovimiento", obj_pBEUsuario.ClasificadorMovimiento);
                prm_Params[9] = new Parameter("@PASSWORD", obj_pBEUsuario.Clave);
                prm_Params[10] = new Parameter("@CorreoElectronico", obj_pBEUsuario.correo_empresa);
                dop_DataOperation.Parameters.AddRange(prm_Params);
                DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation);
                return "1";
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                //throw ex;
                return "0" + ex;
            }
            catch (Exception ex)
            {
                //throw ex;
                return "0" + ex;
            }
        }

        #endregion

        #region UsuarioSede

        public List<WCO_ListarUsuarioSede_Result> ListaUsuarioSede(WCO_ListarUsuarioSede_Result obj_pBEUsuSede)
        {
            List<WCO_ListarUsuarioSede_Result> lst = new List<WCO_ListarUsuarioSede_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarUsuarioSede(obj_pBEUsuSede.Usuario, obj_pBEUsuSede.IdSede).ToList();
            }
            return lst;
        }

        public string EliminarUsuarioSedeMasivo(WCO_ListarUsuarioSede_Result obj_pBEUsuSede)
        {
            string rpt = "";
            try
            {
                DataOperation dop_Operacion = new DataOperation("WCO_EliminarUsuarioSedeMasivo");
                Parameter[] prm_Params = new Parameter[2];
                prm_Params[0] = new Parameter("@usuario", obj_pBEUsuSede.Usuario);
                prm_Params[1] = new Parameter("@IdSede", obj_pBEUsuSede.IdSede);
                dop_Operacion.Parameters.AddRange(prm_Params);
                DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
                rpt = "1|Correcto";
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                rpt = "0|" + ex;
            }
            catch (Exception ex)
            {
                rpt = "0|" + ex;
            }
            return rpt;
        }


        public string InsertarUsuarioSede(WCO_ListarUsuarioSede_Result obj_pBEUsuSede)
        {
            try
            {
                DataOperation dop_Operacion = new DataOperation("WCO_InsertarUsuarioSede");
                Parameter[] prm_Params = new Parameter[4];
                prm_Params[0] = new Parameter("@usuario", obj_pBEUsuSede.Usuario);
                prm_Params[1] = new Parameter("@IdSede", obj_pBEUsuSede.IdSede);
                prm_Params[2] = new Parameter("@UsuarioCreacion", obj_pBEUsuSede.UsuarioCreacion);
                prm_Params[3] = new Parameter("@IpCreacion", obj_pBEUsuSede.IpCreacion);
                dop_Operacion.Parameters.AddRange(prm_Params);
                DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
                return "1";
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                return "0" + ex;
            }
            catch (Exception ex)
            {
                return "0" + ex;
            }
        }

        public string InactivarUsuarioSede(WCO_ListarUsuarioSede_Result obj_pBEUsuSede)
        {
            try
            {
                DataOperation dop_Operacion = new DataOperation("WCO_ActualizarUsuarioSede");
                Parameter[] prm_Params = new Parameter[2];
                prm_Params[0] = new Parameter("@usuario", obj_pBEUsuSede.Usuario);
                prm_Params[1] = new Parameter("@IdSede", obj_pBEUsuSede.IdSede);
                dop_Operacion.Parameters.AddRange(prm_Params);
                DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
                return "1";
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                return "0" + ex;
            }
            catch (Exception ex)
            {
                return "0" + ex;
            }
        }

        #endregion
    }
}