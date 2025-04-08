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
using WebApiServices.Models.Datos;

namespace WebApiServices.Models
{
    public class Metodos
    {
        //static string Co_ConnecPrecisa = ConfigurationManager.ConnectionStrings["ConexionReportes"].ConnectionString;

        public void InsertarWServicioLog(ADBE_WServicioLog obj_pWServicioLog)
        {
            DataOperation dop_DataOperation = new DataOperation("WCO_InsertarWServicioLog");
            try
            {
                Parameter[] prm_Params = new Parameter[11];
                prm_Params[0] = new Parameter("@IdCodigo", obj_pWServicioLog.Pk.IdCodigo);
                prm_Params[1] = new Parameter("@Secuencia", obj_pWServicioLog.Pk.Secuencia);
                prm_Params[2] = new Parameter("@TipoMsj", obj_pWServicioLog.TipoMsj);
                prm_Params[3] = new Parameter("@Sucursal", obj_pWServicioLog.Sucursal);
                prm_Params[4] = new Parameter("@CodigoOA", obj_pWServicioLog.CodigoOA);
                prm_Params[5] = new Parameter("@IdSede", obj_pWServicioLog.IdSede);
                prm_Params[6] = new Parameter("@FechaIni", obj_pWServicioLog.FechaIni);
                prm_Params[7] = new Parameter("@FechaFin", obj_pWServicioLog.FechaFin);
                prm_Params[8] = new Parameter("@Parametros", obj_pWServicioLog.Parametros);
                prm_Params[9] = new Parameter("@Trama", obj_pWServicioLog.Trama);
                prm_Params[10] = new Parameter("@Observacion", obj_pWServicioLog.Observacion);      
                dop_DataOperation.Parameters.AddRange(prm_Params);
                DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<Sp_PDP_validacion_Result> RestValidacionConsentimiento(Sp_PDP_validacion_Result ObjUsuario)
        {
            List<Sp_PDP_validacion_Result> lst = new List<Sp_PDP_validacion_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.Sp_PDP_validacion(ObjUsuario.msn).ToList();
            }
            return lst;
        }

        public List<WCO_ListarConfiguracionCorreo_Result> ListarConfiguracion(WCO_ListarConfiguracionCorreo_Result ObjEntidad)
        {
            List<WCO_ListarConfiguracionCorreo_Result> lst = new List<WCO_ListarConfiguracionCorreo_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarConfiguracionCorreo(ObjEntidad.CompaniaCodigo, ObjEntidad.ParametroClave).ToList();
            }
            return lst;
        }


        #region usuario
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


        public bool Exiteusuario(WCO_AccesoUsuario_Result obj_pBEUsuario)
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

        public void Insertarusuario(WCO_AccesoUsuario_Result obj_pBEUsuario)
        {
            DataOperation dop_DataOperation = new DataOperation("WCO_INSERTARUSUARIO");
            try
            {
                Parameter[] prm_Params = new Parameter[9];
                prm_Params[0] = new Parameter("@USUARIO", obj_pBEUsuario.Documento);
                prm_Params[1] = new Parameter("@NOMBRE", obj_pBEUsuario.NombreCompleto);
                prm_Params[2] = new Parameter("@PERSONA", obj_pBEUsuario.Persona);
                prm_Params[3] = new Parameter("@ULTIMOUSUARIO", obj_pBEUsuario.UltimoUsuario);
                prm_Params[4] = new Parameter("@Tipousuario", obj_pBEUsuario.TipoUsuario);
                prm_Params[5] = new Parameter("@PERFIL", obj_pBEUsuario.UsuarioPerfil);
                prm_Params[6] = new Parameter("@IdSede", obj_pBEUsuario.IdSede);
                prm_Params[7] = new Parameter("@IdClasificadorMovimiento", obj_pBEUsuario.ClasificadorMovimiento);
                prm_Params[8] = new Parameter("@PASSWORD", obj_pBEUsuario.Clave);
                dop_DataOperation.Parameters.AddRange(prm_Params);
                DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Inactivarusuario(WCO_AccesoUsuario_Result obj_pBEUsuario)
        {
            DataOperation dop_DataOperation = new DataOperation("WCO_INACTIVARUSUARIO");
            Parameter[] prm_Params = new Parameter[2];
            prm_Params[0] = new Parameter("@USUARIO", obj_pBEUsuario.Usuario);
            prm_Params[1] = new Parameter("@ULTIMOUSUARIO", obj_pBEUsuario.UltimoUsuario);
            dop_DataOperation.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation);
        }

        public void Actualizarusuario(WCO_AccesoUsuario_Result obj_pBEUsuario)
        {
            DataOperation dop_DataOperation = new DataOperation("WCO_ACTUALIZARUSUARIO");
            try
            {
                Parameter[] prm_Params = new Parameter[10];
                prm_Params[0] = new Parameter("@USUARIO", obj_pBEUsuario.Usuario);
                prm_Params[1] = new Parameter("@EXPERIRARPASSWORDFLAG", obj_pBEUsuario.ExpirarPasswordFlag);
                prm_Params[2] = new Parameter("@ESTADO", obj_pBEUsuario.Estado);
                prm_Params[3] = new Parameter("@ULTIMOUSUARIO", obj_pBEUsuario.UltimoUsuario);
                prm_Params[4] = new Parameter("@Tipousuario", obj_pBEUsuario.TipoUsuario);
                prm_Params[5] = new Parameter("@PERFIL", obj_pBEUsuario.UsuarioPerfil);
                prm_Params[6] = new Parameter("@NOMBRE", obj_pBEUsuario.NombreCompleto);
                prm_Params[7] = new Parameter("@IdSede", obj_pBEUsuario.IdSede);
                prm_Params[8] = new Parameter("@IdClasificadorMovimiento", obj_pBEUsuario.ClasificadorMovimiento);
                prm_Params[9] = new Parameter("@PASSWORD", obj_pBEUsuario.Clave);
                dop_DataOperation.Parameters.AddRange(prm_Params);
                DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<WCO_ListarUsuarioSede_Result> ListaUsuarioSede(WCO_ListarUsuarioSede_Result obj_pBEUsuSede)
        {
            List<WCO_ListarUsuarioSede_Result> lst = new List<WCO_ListarUsuarioSede_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarUsuarioSede(obj_pBEUsuSede.Usuario, obj_pBEUsuSede.IdSede).ToList();
            }
            return lst;
        }

        public void Insertarusuariosede(WCO_ListarUsuarioSede_Result obj_pBEUsuSede)
        {
            DataOperation dop_DataOperation = new DataOperation("WCO_InsertarUsuarioSede");
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
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Actualizarusuariosede(WCO_ListarUsuarioSede_Result obj_pBEUsuSede)
        {
            DataOperation dop_DataOperation = new DataOperation("WCO_INSERTARUSUARIO");
            try
            {
                DataOperation dop_Operacion = new DataOperation("WCO_ActualizarUsuarioSede");
                Parameter[] prm_Params = new Parameter[4];
                prm_Params[0] = new Parameter("@usuario", obj_pBEUsuSede.Usuario);
                prm_Params[1] = new Parameter("@IdSede", obj_pBEUsuSede.IdSede);
                prm_Params[2] = new Parameter("@UsuarioModificacion", obj_pBEUsuSede.UsuarioCreacion);
                prm_Params[3] = new Parameter("@IpModificacion", obj_pBEUsuSede.IpCreacion);
                dop_Operacion.Parameters.AddRange(prm_Params);
                DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Perfiles
        public List<WCO_PerfilPaginas_Result> ListaPerfilPaginas(WCO_PerfilPaginas_Result ObjPaginas)
        {
            List<WCO_PerfilPaginas_Result> lst = new List<WCO_PerfilPaginas_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_PerfilPaginas(ObjPaginas.Usuario, ObjPaginas.TipodeDetalle, ObjPaginas.Perfil, ObjPaginas.ORDEN, ObjPaginas.Perfil).ToList();
            }
            return lst;
        }




        public List<WCO_ListarMenuOpcionesPerfil_Result> WCO_ListarMenuOpcionesPerfil(WCO_LISTARPERFILPAG_Result ObjPaginas)
        {
            List<WCO_ListarMenuOpcionesPerfil_Result> lst = new List<WCO_ListarMenuOpcionesPerfil_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarMenuOpcionesPerfil(ObjPaginas.Perfil, "").ToList();
            }
            return lst;
        }

        #endregion

        #region Portal
        public List<WCO_ListarPortal_Result> ListaPortal(WCO_ListarPortal_Result ObjPortal)
        {
            List<WCO_ListarPortal_Result> lst = new List<WCO_ListarPortal_Result>();
            using (var context = new BDComercialEntities())
            {

                lst = context.WCO_ListarPortal(ObjPortal.IdPortal, ObjPortal.Estado).ToList();

            }
            return lst;
        }

        public static void ActualizarPortal(WCO_ListarPortal_Result objBEPortal)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_ActualizarPortal");
            Parameter[] prm_Params = new Parameter[9];
            prm_Params[0] = new Parameter("@IdPortal", objBEPortal.IdPortal);
            prm_Params[1] = new Parameter("@Logo", objBEPortal.Logo);
            prm_Params[2] = new Parameter("@RutaImagen", objBEPortal.RutaImagen);
            prm_Params[3] = new Parameter("@Direccion", objBEPortal.Direccion);
            prm_Params[4] = new Parameter("@Telefono", objBEPortal.Telefono);
            prm_Params[5] = new Parameter("@Correo", objBEPortal.Correo);
            prm_Params[6] = new Parameter("@Estado", objBEPortal.Estado);
            prm_Params[7] = new Parameter("@UsuarioModificacion", objBEPortal.UsuarioModificacion);
            prm_Params[8] = new Parameter("@IpModificacion", objBEPortal.IpModificacion);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }
        #endregion

        #region MaestroTabla
        public List<WCO_ListarTablasMaestras_Result> ListaTablasMaestras(WCO_ListarTablasMaestras_Result ObjMaestras)
        {
            List<WCO_ListarTablasMaestras_Result> lst = new List<WCO_ListarTablasMaestras_Result>();
            using (var context = new BDComercialEntities())
            {
                Nullable<int> iReturnValue = null;
                if (ObjMaestras.IdTablaMaestro > 0)
                {
                    iReturnValue = ObjMaestras.IdTablaMaestro;
                }
                lst = context.WCO_ListarTablasMaestras(iReturnValue, ObjMaestras.Nombre, ObjMaestras.Estado, ObjMaestras.CodigoTabla).ToList();
            }
            return lst;
        }

        public int InsertarTablaMaestro(WCO_ListarTablasMaestras_Result objBETablaMaestro)
        {
            int valor = 0;
            WCO_ListarTablasMaestras_Result obj = new WCO_ListarTablasMaestras_Result()
            {
                IdTablaMaestro = 0,
                CodigoTabla = "",
                Nombre = ""
            };

            bool isExists = false;
            isExists = ListaTablasMaestras(obj).Exists(x => x.CodigoTabla == objBETablaMaestro.CodigoTabla || x.Nombre == objBETablaMaestro.Nombre);

            if (!isExists)
            {
                DataOperation dop_Operacion = new DataOperation("WCO_InsertarTablaMaestro");
                Parameter[] prm_Params = new Parameter[6];
                prm_Params[0] = new Parameter("@Nombre", objBETablaMaestro.Nombre);
                prm_Params[1] = new Parameter("@Descripcion", objBETablaMaestro.Descripcion);
                prm_Params[2] = new Parameter("@Codigo", objBETablaMaestro.CodigoTabla);
                prm_Params[3] = new Parameter("@Estado", objBETablaMaestro.Estado);
                prm_Params[4] = new Parameter("@UsuarioCreacion", objBETablaMaestro.UsuarioCreacion);
                prm_Params[5] = new Parameter("@IdTablaMaestro", DbType.Int32);
                dop_Operacion.Parameters.AddRange(prm_Params);
                DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
                valor = int.Parse(dop_Operacion.GetParameterByName("@IdTablaMaestro").Value.ToString());
            }
            else
            {
                valor = -1;
            }
            return valor;
        }

        public int ActualizarTablaMaestro(WCO_ListarTablasMaestras_Result objBETablaMaestro)
        {
            int valor = 0;
            WCO_ListarTablasMaestras_Result obj = new WCO_ListarTablasMaestras_Result()
            {
                IdTablaMaestro = 0,
                CodigoTabla = "",
                Nombre = ""
            };

            bool isExists = false;
            isExists = ListaTablasMaestras(obj).Exists(x => x.CodigoTabla == objBETablaMaestro.CodigoTabla || x.Nombre == objBETablaMaestro.Nombre);

            if (!isExists)
            {
                DataOperation dop_Operacion = new DataOperation("WCO_ActualizarTablaMaestro");
                Parameter[] prm_Params = new Parameter[7];
                prm_Params[0] = new Parameter("@Nombre", objBETablaMaestro.Nombre);
                prm_Params[1] = new Parameter("@Descripcion", objBETablaMaestro.Descripcion);
                prm_Params[2] = new Parameter("@Codigo", objBETablaMaestro.CodigoTabla);
                prm_Params[3] = new Parameter("@Estado", objBETablaMaestro.Estado);
                prm_Params[4] = new Parameter("@UsuarioModificacion", objBETablaMaestro.UsuarioModificacion);
                prm_Params[5] = new Parameter("@IdTablaMaestro", objBETablaMaestro.IdTablaMaestro);
                prm_Params[6] = new Parameter("@version", objBETablaMaestro.Version);
                dop_Operacion.Parameters.AddRange(prm_Params);
                DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
                valor = int.Parse(dop_Operacion.GetParameterByName("@IdTablaMaestro").Value.ToString());
            }
            else
            {
                valor = -1;
            }
            return valor;
        }

        public void InactivarTablaMaestro(WCO_ListarTablasMaestras_Result objBETablaMaestro)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InactivarTablaMaestro");
            Parameter[] prm_Params = new Parameter[2];
            prm_Params[0] = new Parameter("@UsuarioModificacion", objBETablaMaestro.UsuarioModificacion);
            prm_Params[1] = new Parameter("@IdTablaMaestro", objBETablaMaestro.IdTablaMaestro);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }

        #endregion

        #region MaestroDetalleTabla

        public List<WCO_ListarTablaMaestroDetalle_Result> ListaTablaMaestroDetalle(WCO_ListarTablaMaestroDetalle_Result ObjUsuario)
        {
            List<WCO_ListarTablaMaestroDetalle_Result> lst = new List<WCO_ListarTablaMaestroDetalle_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarTablaMaestroDetalle(ObjUsuario.Codigo).ToList();
            }
            return lst;
        }

        public List<WCO_ListarTMAestroDetalles_Result> ListaMaestroDetalle(WCO_ListarTMAestroDetalles_Result ObjDetalle)
        {
            List<WCO_ListarTMAestroDetalles_Result> lst = new List<WCO_ListarTMAestroDetalles_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarTMAestroDetalles(ObjDetalle.IdTablaMaestro, ObjDetalle.IdCodigo, ObjDetalle.Nombre, ObjDetalle.Estado).ToList();
            }
            return lst;
        }

        public int InsertarMaestroDetalle(WCO_ListarTMAestroDetalles_Result objBETablaMaestrodeta)
        {
            int valor = 0;
            WCO_ListarTMAestroDetalles_Result obj = new WCO_ListarTMAestroDetalles_Result()
            {
                IdTablaMaestro = objBETablaMaestrodeta.IdTablaMaestro,
                IdCodigo = 0,
                Nombre = ""
            };

            bool isExists = false;
            isExists = ListaMaestroDetalle(obj).Exists(x => x.IdTablaMaestro == objBETablaMaestrodeta.IdTablaMaestro 
                && x.IdCodigo == objBETablaMaestrodeta.IdCodigo );

            isExists = ListaMaestroDetalle(obj).Exists(x => x.IdTablaMaestro == objBETablaMaestrodeta.IdTablaMaestro
                 && x.Nombre == objBETablaMaestrodeta.Nombre);

            if (!isExists)
            {
                DataOperation dop_Operacion = new DataOperation("WCO_InsertarTablaMaestroDetalle");
                Parameter[] prm_Params = new Parameter[7];
                prm_Params[0] = new Parameter("@Nombre", objBETablaMaestrodeta.Nombre);
                prm_Params[1] = new Parameter("@Descripcion", objBETablaMaestrodeta.Descripcion);
                prm_Params[2] = new Parameter("@Codigo", objBETablaMaestrodeta.Codigo);
                prm_Params[3] = new Parameter("@Estado", objBETablaMaestrodeta.Estado);
                prm_Params[4] = new Parameter("@UsuarioCreacion", objBETablaMaestrodeta.UsuarioCreacion);
                prm_Params[5] = new Parameter("@IdTablaMaestro", objBETablaMaestrodeta.IdTablaMaestro);
                prm_Params[6] = new Parameter("@IdCodigo", DbType.Int32);
                dop_Operacion.Parameters.AddRange(prm_Params);
                DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
                valor = int.Parse(dop_Operacion.GetParameterByName("@IdCodigo").Value.ToString());
            }
            else
            {
                valor = -1;
            }
            return valor;
        }

        public int ActualizarMaestroDetalle(WCO_ListarTMAestroDetalles_Result objBETablaMaestrodeta)
        {
            int valor = 0;
            WCO_ListarTMAestroDetalles_Result obj = new WCO_ListarTMAestroDetalles_Result()
            {
                IdTablaMaestro = objBETablaMaestrodeta.IdTablaMaestro,
                IdCodigo = 0,
                Nombre = ""
            };

            bool isExists = false;
            //isExists = ListaMaestroDetalle(obj).Exists(x => x.IdTablaMaestro == objBETablaMaestrodeta.IdTablaMaestro && 
            //x.IdCodigo != objBETablaMaestrodeta.IdCodigo  && x.Nombre == objBETablaMaestrodeta.Nombre);

            valor = ListaMaestroDetalle(obj).Where(x => x.IdTablaMaestro == objBETablaMaestrodeta.IdTablaMaestro &&
                x.IdCodigo != objBETablaMaestrodeta.IdCodigo && x.Nombre == objBETablaMaestrodeta.Nombre).Count();
            if (valor > 0)
            {
                isExists = true;
            }

            if (isExists == false)
            {
                DataOperation dop_Operacion = new DataOperation("WCO_ActualizarDetalleMaestro");
                Parameter[] prm_Params = new Parameter[7];
                prm_Params[0] = new Parameter("@Nombre", objBETablaMaestrodeta.Nombre);
                prm_Params[1] = new Parameter("@Descripcion", objBETablaMaestrodeta.Descripcion);
                prm_Params[2] = new Parameter("@Codigo", objBETablaMaestrodeta.Codigo);
                prm_Params[3] = new Parameter("@Estado", objBETablaMaestrodeta.Estado);
                prm_Params[4] = new Parameter("@UsuarioModificacion", objBETablaMaestrodeta.UsuarioModificacion);
                prm_Params[5] = new Parameter("@IdTablaMaestro", objBETablaMaestrodeta.IdTablaMaestro);
                prm_Params[6] = new Parameter("@IdCodigo", objBETablaMaestrodeta.IdCodigo);
                dop_Operacion.Parameters.AddRange(prm_Params);
                DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            }
            else
            {
                valor = -1;
            }
            return valor;
        }

        public void InactivarMaestroDetalle(WCO_ListarTMAestroDetalles_Result objBETablaMaestrodeta)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InactivarTablaDetalles");
            //try
            //{
            Parameter[] prm_Params = new Parameter[3];
            prm_Params[0] = new Parameter("@UsuarioModificacion", objBETablaMaestrodeta.UsuarioModificacion);
            prm_Params[1] = new Parameter("@IdTablaMaestro", objBETablaMaestrodeta.IdTablaMaestro);
            prm_Params[2] = new Parameter("@idcodigo", objBETablaMaestrodeta.IdCodigo);

            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            //}
            //catch (System.Data.SqlClient.SqlException ex)
            //{
            //    throw ex;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        public bool ExisteMaestroDetalle(WCO_ListarTMAestroDetalles_Result objBETablaMaestrodeta)
        {
            DataOperation dop_DataOperation = new DataOperation("WCO_ExisteDetalleMaestro");
            Parameter[] prm_Params = new Parameter[3];
            prm_Params[0] = new Parameter("@IdTablaMaestro", objBETablaMaestrodeta.IdTablaMaestro);
            prm_Params[1] = new Parameter("@Codigo", objBETablaMaestrodeta.Codigo);
            prm_Params[2] = new Parameter("@flagSalida", DbType.Int32);
            dop_DataOperation.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation);
            if (Convert.ToInt32(dop_DataOperation.GetParameterByName("flagSalida").Value) >= 1)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Parametros
        public List<WCO_ListarParametro_Result> ListaParametro(WCO_ListarParametro_Result ObjUsuario)
        {
            List<WCO_ListarParametro_Result> lst = new List<WCO_ListarParametro_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarParametro(ObjUsuario.CompaniaCodigo, ObjUsuario.AplicacionCodigo, 
                    ObjUsuario.ParametroClave, ObjUsuario.DescripcionParametro, ObjUsuario.TipodeDatoFlag, 
                    ObjUsuario.Estado).ToList();
            }
            return lst;
        }

        public string InsertarParametro(WCO_ListarParametro_Result objBETablaMaestrodeta)
        {
            string valor = "";
            WCO_ListarParametro_Result obj = new WCO_ListarParametro_Result()
            {
                CompaniaCodigo = "",
                AplicacionCodigo = ""
            };

            bool isExists = false;
            isExists = ListaParametro(obj).Exists(x => x.CompaniaCodigo == objBETablaMaestrodeta.CompaniaCodigo 
            && x.AplicacionCodigo == objBETablaMaestrodeta.AplicacionCodigo && x.ParametroClave == objBETablaMaestrodeta.ParametroClave);

            if (!isExists)
            {
                DataOperation dop_Operacion = new DataOperation("WCO_InsertarParametro");
                Parameter[] prm_Params = new Parameter[14];
                prm_Params[0] = new Parameter("@CompaniaCodigo", objBETablaMaestrodeta.CompaniaCodigo);
                prm_Params[1] = new Parameter("@AplicacionCodigo", objBETablaMaestrodeta.AplicacionCodigo);
                prm_Params[2] = new Parameter("@ParametroClave", objBETablaMaestrodeta.ParametroClave);
                prm_Params[3] = new Parameter("@DescripcionParametro", objBETablaMaestrodeta.DescripcionParametro);
                prm_Params[4] = new Parameter("@Explicacion", objBETablaMaestrodeta.Explicacion);
                prm_Params[5] = new Parameter("@TipodeDatoFlag", objBETablaMaestrodeta.TipodeDatoFlag);
                prm_Params[6] = new Parameter("@Texto", objBETablaMaestrodeta.Texto);
                prm_Params[7] = new Parameter("@Numero", objBETablaMaestrodeta.Numero);
                prm_Params[8] = new Parameter("@Fecha", objBETablaMaestrodeta.Fecha);
                prm_Params[9] = new Parameter("@Estado", objBETablaMaestrodeta.Estado);
                prm_Params[10] = new Parameter("@ExplicacionAdicional", objBETablaMaestrodeta.ExplicacionAdicional);
                prm_Params[11] = new Parameter("@Texto1", objBETablaMaestrodeta.Texto1);
                prm_Params[12] = new Parameter("@Texto2", objBETablaMaestrodeta.Texto2);
                prm_Params[13] = new Parameter("@UsuarioCreacion", objBETablaMaestrodeta.UltimoUsuario);       
                dop_Operacion.Parameters.AddRange(prm_Params);
                DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
                valor = "true";
            }
            else
            {
                valor = "false";
            }
            return valor;
        }

        public string ActualizarParametro(WCO_ListarParametro_Result objBETablaMaestrodeta)
        {
            string valor = "0";
            WCO_ListarParametro_Result obj = new WCO_ListarParametro_Result()
            {
                CompaniaCodigo = "",
                AplicacionCodigo = ""
            };

            bool isExists = false;
            isExists = ListaParametro(obj).Exists(x => x.CompaniaCodigo == objBETablaMaestrodeta.CompaniaCodigo && x.AplicacionCodigo == objBETablaMaestrodeta.AplicacionCodigo
             &&  x.ParametroClave != objBETablaMaestrodeta.ParametroClave);

            if (!isExists)
            {
                DataOperation dop_Operacion = new DataOperation("WCO_ActualizarParametro");
                Parameter[] prm_Params = new Parameter[14];
                prm_Params[0] = new Parameter("@CompaniaCodigo", objBETablaMaestrodeta.CompaniaCodigo);
                prm_Params[1] = new Parameter("@AplicacionCodigo", objBETablaMaestrodeta.AplicacionCodigo);
                prm_Params[2] = new Parameter("@ParametroClave", objBETablaMaestrodeta.ParametroClave);
                prm_Params[3] = new Parameter("@DescripcionParametro", objBETablaMaestrodeta.DescripcionParametro);
                prm_Params[4] = new Parameter("@Explicacion", objBETablaMaestrodeta.Explicacion);
                prm_Params[5] = new Parameter("@TipodeDatoFlag", objBETablaMaestrodeta.TipodeDatoFlag);
                prm_Params[6] = new Parameter("@Texto", objBETablaMaestrodeta.Texto);
                prm_Params[7] = new Parameter("@Numero", objBETablaMaestrodeta.Numero);
                prm_Params[8] = new Parameter("@Fecha", objBETablaMaestrodeta.Fecha);
                prm_Params[9] = new Parameter("@Estado", objBETablaMaestrodeta.Estado);
                prm_Params[10] = new Parameter("@ExplicacionAdicional", objBETablaMaestrodeta.ExplicacionAdicional);
                prm_Params[11] = new Parameter("@Texto1", objBETablaMaestrodeta.Texto1);
                prm_Params[12] = new Parameter("@Texto2", objBETablaMaestrodeta.Texto2);
                prm_Params[13] = new Parameter("@UsuarioModificacion", objBETablaMaestrodeta.UltimoUsuario);
                dop_Operacion.Parameters.AddRange(prm_Params);
                DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            }
            else
            {
                valor = "-1";
            }
            return valor;
        }


        #endregion

        #region Ubigeo
        public List<WCO_ListarUbigeo_Result> ListaUbigeo(WCO_ListarUbigeo_Result ObjUbigeo)
        {
            List<WCO_ListarUbigeo_Result> lst = new List<WCO_ListarUbigeo_Result>();
            using (var context = new BDComercialEntities())
            {

                lst = context.WCO_ListarUbigeo(ObjUbigeo.Codigo, ObjUbigeo.Num).ToList();

            }
            return lst;
        }

        #endregion
            
        #region Moneda
        public List<WCO_ListarMonedaMast_Result> ListarMoneda(WCO_ListarMonedaMast_Result ObjUbigeo)
        {
            List<WCO_ListarMonedaMast_Result> lst = new List<WCO_ListarMonedaMast_Result>();
            using (var context = new BDComercialEntities())
            {

                lst = context.WCO_ListarMonedaMast(ObjUbigeo.Estado).ToList();

            }
            return lst;
        }

        #endregion



        #region Especialidad

        public List<WCO_ListarEspecialidad_Result> ListaEspecialidad(WCO_ListarEspecialidad_Result ObjEspecialidad)
        {
            List<WCO_ListarEspecialidad_Result> lst = new List<WCO_ListarEspecialidad_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarEspecialidad(ObjEspecialidad.IndicadorWeb, ObjEspecialidad.Codigo, ObjEspecialidad.Descripcion, ObjEspecialidad.Estado).ToList();
            }
            return lst;
        }

        #endregion

        #region TipoAdmision
        public List<WCO_ListarTipodeAdmision_Result> ListaTipoAdmision(WCO_ListarTipodeAdmision_Result ObjResul)
        {
            List<WCO_ListarTipodeAdmision_Result> lst = new List<WCO_ListarTipodeAdmision_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarTipodeAdmision(ObjResul.AdmCodigo, ObjResul.AdmDescripcion, ObjResul.AdmEstado).ToList();
            }
            return lst;
        }

        public int InsertarTipoAdmision(WCO_ListarTipodeAdmision_Result objBETipoAdmision)
        {
            int valor = 0;
            bool isExists = false;
            isExists = ValidarExisteDescTipoAdmision(objBETipoAdmision);
            if (!isExists)
            {
                DataOperation dop_Operacion = new DataOperation("WCO_InsertarTipoAdmision");
                Parameter[] prm_Params = new Parameter[8];
                prm_Params[0] = new Parameter("@UneuNegocioId", objBETipoAdmision.UneuNegocioId);
                prm_Params[1] = new Parameter("@TipoAdmisionId", DbType.Int32);
                prm_Params[2] = new Parameter("@AdmCodigo", objBETipoAdmision.AdmCodigo);
                prm_Params[3] = new Parameter("@AdmDescripcion", objBETipoAdmision.AdmDescripcion);
                prm_Params[4] = new Parameter("@AdmEstado", objBETipoAdmision.AdmEstado);
                prm_Params[5] = new Parameter("@FechaCreacion", objBETipoAdmision.FechaCreacion);
                prm_Params[6] = new Parameter("@UsuarioCreacion", objBETipoAdmision.UsuarioCreacion);
                prm_Params[7] = new Parameter("@IpCreacion", objBETipoAdmision.IpCreacion);
                dop_Operacion.Parameters.AddRange(prm_Params);
                DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
                return int.Parse(dop_Operacion.GetParameterByName("@TipoAdmisionId").Value.ToString());
            }
            else
            {
                valor = -1;
            }
            return valor;
        }

        public int ActualizarTipoAdmision(WCO_ListarTipodeAdmision_Result objBETipoAdmision)
        {
            int valor = 0;
            bool isExists = false;
            isExists = ValidarExisteDescTipoAdmision(objBETipoAdmision);

            if (!isExists)
            {
                DataOperation dop_Operacion = new DataOperation("WCO_ActualizarTipoAdmision");
                Parameter[] prm_Params = new Parameter[8];
                prm_Params[0] = new Parameter("@UneuNegocioId", objBETipoAdmision.TipoAdmisionId);
                prm_Params[1] = new Parameter("@TipoAdmisionId", objBETipoAdmision.TipoAdmisionId);
                prm_Params[2] = new Parameter("@AdmCodigo", objBETipoAdmision.AdmCodigo);
                prm_Params[3] = new Parameter("@AdmDescripcion", objBETipoAdmision.AdmDescripcion);
                prm_Params[4] = new Parameter("@AdmEstado", objBETipoAdmision.AdmEstado);
                prm_Params[5] = new Parameter("@FechaModificacion", objBETipoAdmision.FechaCreacion);
                prm_Params[6] = new Parameter("@UsuarioModificacion", objBETipoAdmision.UsuarioCreacion);
                prm_Params[7] = new Parameter("@IpModificacion", objBETipoAdmision.IpCreacion);
                dop_Operacion.Parameters.AddRange(prm_Params);
                DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            }
            else
            {
                valor = -1;
            }
            return valor;
        }

        public void InactivarTipoAdmision(WCO_ListarTipodeAdmision_Result objBETipoAdmision)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InactivarTipoAdmision");
            Parameter[] prm_Params = new Parameter[3];
            prm_Params[0] = new Parameter("@TipoAdmisionId", objBETipoAdmision.TipoAdmisionId);
            prm_Params[1] = new Parameter("@UsuarioModificacion", objBETipoAdmision.UsuarioCreacion);
            prm_Params[2] = new Parameter("@IpModificacion", objBETipoAdmision.IpCreacion);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }

        public static bool ValidarExisteDescTipoAdmision(WCO_ListarTipodeAdmision_Result objBETipoAdmision)
        {
            DataOperation dop_DataOperation = new DataOperation("WCO_EXISTEDescTipoAdmision");
            Parameter[] prm_Params = new Parameter[3];
            prm_Params[0] = new Parameter("@TipoAdmisionId", objBETipoAdmision.TipoAdmisionId);
            prm_Params[1] = new Parameter("@admdescripcion", objBETipoAdmision.AdmDescripcion);
            prm_Params[2] = new Parameter("@flagSalida", DbType.Int32);
            dop_DataOperation.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation);
            if (Convert.ToInt32(dop_DataOperation.GetParameterByName("@flagSalida").Value) >= 1)
            {
                return false;
            }
            return true;
        }

        #endregion

        #region Autorizacion

        public List<WCO_ListarAutorizacionBeneficiario_Result> ListaAutorizacion(WCO_ListarAutorizacionBeneficiario_Result ObjRest)
        {
            List<WCO_ListarAutorizacionBeneficiario_Result> lst = new List<WCO_ListarAutorizacionBeneficiario_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarAutorizacionBeneficiario(0, ObjRest.FechaInicio, ObjRest.FechaFin, ObjRest.IdAutorizacion, ObjRest.IdPaciente, ObjRest.Estado).ToList();
            }
            return lst;
        }

        #endregion

        #region Aprobador

        public List<WCO_ListarAprobadores_Result> ListarAprobadores(WCO_ListarAprobadores_Result ObjRest)
        {
            List<WCO_ListarAprobadores_Result> lst = new List<WCO_ListarAprobadores_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarAprobadores(ObjRest.IdAprobador, ObjRest.TipoDescuento, ObjRest.IdUsuario, ObjRest.Estado, ObjRest.UneuNegocioId).ToList();
            }
            return lst;
        }

        #endregion

        #region Aseguradora

        public List<WCO_ListarAseguradora_Result> ListaAseguradora(WCO_ListarAseguradora_Result BE_Aseguradora)
        {
            List<WCO_ListarAseguradora_Result> lst = new List<WCO_ListarAseguradora_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarAseguradora(BE_Aseguradora.NombreEmpresa, BE_Aseguradora.IdAseguradora, BE_Aseguradora.Estado).ToList();
            }
            return lst;
        }

        public int InsertarAseguradora(WCO_ListarAseguradora_Result objBEAseguradora)
        {
            int valor = 0;
            WCO_ListarAseguradora_Result obj = new WCO_ListarAseguradora_Result()
            {
                IdAseguradora = 0,
                NombreEmpresa = ""
            };

            bool isExists = false;
            isExists = ListaAseguradora(obj).Exists(x => x.IdAseguradora == objBEAseguradora.IdAseguradora && x.NombreEmpresa == objBEAseguradora.NombreEmpresa
            || x.NombreEmpresa == objBEAseguradora.NombreEmpresa);

            if (!isExists)
            {
                DataOperation dop_Operacion = new DataOperation("WCO_InsertarAseguradora");
                Parameter[] prm_Params = new Parameter[5];
                prm_Params[0] = new Parameter("@Nombre", objBEAseguradora.NombreEmpresa);
                prm_Params[1] = new Parameter("@Clasificacion", objBEAseguradora.Clasificacion_1);
                prm_Params[2] = new Parameter("@IdAseguradora", DbType.Int32);
                prm_Params[3] = new Parameter("@TipoAseguradora", objBEAseguradora.TipoAseguradora);
                prm_Params[4] = new Parameter("@Estado", objBEAseguradora.Estado);
                dop_Operacion.Parameters.AddRange(prm_Params);
                DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
                return int.Parse(dop_Operacion.GetParameterByName("@IdAseguradora").Value.ToString());
            }
            else
            {
                valor = -1;
            }
            return valor;
        }

        public int ActualizarAseguradora(WCO_ListarAseguradora_Result objBEAseguradora)
        {
            int valor = 0;
            WCO_ListarAseguradora_Result obj = new WCO_ListarAseguradora_Result()
            {
                IdAseguradora = 0,
                NombreEmpresa = ""
            };

            bool isExists = false;
            isExists = ListaAseguradora(obj).Exists(x => x.IdAseguradora == objBEAseguradora.IdAseguradora && x.NombreEmpresa == objBEAseguradora.NombreEmpresa
            || x.NombreEmpresa == objBEAseguradora.NombreEmpresa);

            if (!isExists)
            {
                DataOperation dop_Operacion = new DataOperation("WCO_ActualizarAseguradora");
                Parameter[] prm_Params = new Parameter[5];
                prm_Params[0] = new Parameter("@IdAseguradora", objBEAseguradora.IdAseguradora);
                prm_Params[1] = new Parameter("@Nombre", objBEAseguradora.NombreEmpresa);
                prm_Params[2] = new Parameter("@Clasificacion", objBEAseguradora.Clasificacion_1);
                prm_Params[3] = new Parameter("@TipoAseguradora", objBEAseguradora.TipoAseguradora);
                prm_Params[4] = new Parameter("@Estado", objBEAseguradora.Estado);
                dop_Operacion.Parameters.AddRange(prm_Params);
                DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            }
            else
            {
                valor = -1;
            }
            return valor;
        }

        public void InactivarAseguradora(WCO_ListarAseguradora_Result objBEAseguradora)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InactivarAseguradora");
            Parameter[] prm_Params = new Parameter[2];
            prm_Params[0] = new Parameter("@IdAseguradora", objBEAseguradora.IdAseguradora);
            prm_Params[1] = new Parameter("@Estado", objBEAseguradora.Estado);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }
        #endregion

        #region Medico

        public List<WCO_ListarMedico_Result> ListarMedico(WCO_ListarMedico_Result ObjResul)
        {
            List<WCO_ListarMedico_Result> lst = new List<WCO_ListarMedico_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarMedico(ObjResul.Nombres, ObjResul.Estado, ObjResul.TipoDocumento, ObjResul.Documento, ObjResul.CMP, ObjResul.MedicoId, ObjResul.IdEspecialidad).ToList();
            }
            return lst;
        }

        ///<summary>Insertar el registro en la tabla Medico.</summary>
        ///<param name="objBEMedico">Entidad de Negocio</param>
        ///<item><CreadoPor>Creado Por Jordan Mateo Pizarro</CreadoPor></item>
        ///<item><FecCrea>17/02/2013</FecCrea></item></list></remarks>
        public int InsertarMedico(WCO_ListarMedico_Result objBEMedico)
        {
            int valor = 0;
            bool isExists = true;
            if (!ValidarExisteMedico(objBEMedico))
            {
                valor = -2;
                return valor;
            }
            if (!ValidarExisteCodigoMedico(objBEMedico))
            {
                valor = -3;
                return valor;
            }
            if (!string.IsNullOrEmpty(objBEMedico.Documento))
            {
                if (!ValidarExisteDNIMedico(objBEMedico))
                {
                    valor = -4;
                    return valor;
                }
            }

            if (objBEMedico.ApellidoMaterno != null)
            {
                if (!string.IsNullOrEmpty(objBEMedico.ApellidoMaterno.ToString()))
                {
                    objBEMedico.ApellidoMaterno = objBEMedico.ApellidoMaterno.ToUpper();
                }
            }

            if (objBEMedico.ApellidoPaterno != null)
            {
                if (!string.IsNullOrEmpty(objBEMedico.ApellidoPaterno.ToString()))
                {
                    objBEMedico.ApellidoPaterno = objBEMedico.ApellidoPaterno.ToUpper();
                }
            }

            if (objBEMedico.Nombres != null)
            {
                if (!string.IsNullOrEmpty(objBEMedico.Nombres.ToString()))
                {
                    objBEMedico.Nombres = objBEMedico.Nombres.ToUpper();
                }
            }
            if (objBEMedico.Busqueda != null)
            {
                if (!string.IsNullOrEmpty(objBEMedico.Busqueda.ToString()))
                {
                    objBEMedico.Busqueda = objBEMedico.Busqueda.ToUpper();
                }
            }

            DataOperation dop_Operacion = new DataOperation("WCO_InsertarMedico");
            Parameter[] prm_Params = new Parameter[16];
            prm_Params[0] = new Parameter("@ApellidoMaterno", objBEMedico.ApellidoMaterno);
            prm_Params[1] = new Parameter("@ApellidoPaterno", objBEMedico.ApellidoPaterno);
            prm_Params[2] = new Parameter("@Nombres", objBEMedico.Nombres);
            prm_Params[3] = new Parameter("@NombreCompleto", objBEMedico.Busqueda);
            prm_Params[4] = new Parameter("@TipoDocumento", objBEMedico.TipoDocumento);
            prm_Params[5] = new Parameter("@Documento", objBEMedico.Documento);
            prm_Params[6] = new Parameter("@CMP", objBEMedico.CMP);
            prm_Params[7] = new Parameter("@Estado", objBEMedico.Estado);
            prm_Params[8] = new Parameter("@IpCreacion", objBEMedico.IpCreacion);
            prm_Params[9] = new Parameter("@UsuarioCreacion", objBEMedico.UsuarioCreacion);
            prm_Params[10] = new Parameter("@MedicoId", DbType.Int32);
            prm_Params[11] = new Parameter("@Correo", objBEMedico.Correo);
            prm_Params[12] = new Parameter("@RNE", objBEMedico.RNE);
            prm_Params[13] = new Parameter("@Sexo", objBEMedico.Sexo);
            prm_Params[14] = new Parameter("@Telefono", objBEMedico.Telefono);
            prm_Params[15] = new Parameter("@IdEspecialidad", objBEMedico.IdEspecialidad);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            return int.Parse(dop_Operacion.GetParameterByName("@MedicoId").Value.ToString());

            //return valor;
        }

        ///<summary>Actualizar el registro en la tabla Medico.</summary>
        ///<param name="objBEMedico">Entidad de Negocio</param>
        ///<item><CreadoPor>Creado Por Jordan Mateo Pizarro</CreadoPor></item>
        ///<item><FecCrea>17/02/2013</FecCrea></item></list></remarks>
        public int ActualizarMedico(WCO_ListarMedico_Result objBEMedico)
        {
            int valor = 0;
            bool isExists = true;
            if (!ValidarExisteMedico(objBEMedico))
            {
                valor = -2;
                return valor;
            }
            if (!ValidarExisteCodigoMedico(objBEMedico))
            {
                valor = -3;
                return valor;
            }
            if (!string.IsNullOrEmpty(objBEMedico.Documento))
            {
                if (!ValidarExisteDNIMedico(objBEMedico))
                {
                    valor = -4;
                    return valor;
                }
            }
            if (objBEMedico.ApellidoMaterno != null)
            {
                if (!string.IsNullOrEmpty(objBEMedico.ApellidoMaterno.ToString()))
                {
                    objBEMedico.ApellidoMaterno = objBEMedico.ApellidoMaterno.ToUpper();
                }
            }

            if (objBEMedico.ApellidoPaterno != null)
            {
                if (!string.IsNullOrEmpty(objBEMedico.ApellidoPaterno.ToString()))
                {
                    objBEMedico.ApellidoPaterno = objBEMedico.ApellidoPaterno.ToUpper();
                }
            }

            if (objBEMedico.Nombres != null)
            {
                if (!string.IsNullOrEmpty(objBEMedico.Nombres.ToString()))
                {
                    objBEMedico.Nombres = objBEMedico.Nombres.ToUpper();
                }
            }
            if (objBEMedico.Busqueda != null)
            {
                if (!string.IsNullOrEmpty(objBEMedico.Busqueda.ToString()))
                {
                    objBEMedico.Busqueda = objBEMedico.Busqueda.ToUpper();
                }
            }
            DataOperation dop_Operacion = new DataOperation("WCO_ActualizarMedico");
            Parameter[] prm_Params = new Parameter[16];
            prm_Params[0] = new Parameter("@ApellidoMaterno", objBEMedico.ApellidoMaterno);
            prm_Params[1] = new Parameter("@ApellidoPaterno", objBEMedico.ApellidoPaterno);
            prm_Params[2] = new Parameter("@Nombres", objBEMedico.Nombres);
            prm_Params[3] = new Parameter("@NombreCompleto", objBEMedico.Busqueda);
            prm_Params[4] = new Parameter("@TipoDocumento", objBEMedico.TipoDocumento);
            prm_Params[5] = new Parameter("@Documento", objBEMedico.Documento);
            prm_Params[6] = new Parameter("@CMP", objBEMedico.CMP);
            prm_Params[7] = new Parameter("@Estado", objBEMedico.Estado);
            prm_Params[8] = new Parameter("@UltimoIp", objBEMedico.IpModificacion);
            prm_Params[9] = new Parameter("@UsuarioModificacion", objBEMedico.UsuarioModificacion);
            prm_Params[10] = new Parameter("@MedicoId", objBEMedico.MedicoId);
            prm_Params[11] = new Parameter("@Correo", objBEMedico.Correo);
            prm_Params[12] = new Parameter("@RNE", objBEMedico.RNE);
            prm_Params[13] = new Parameter("@Sexo", objBEMedico.Sexo);
            prm_Params[14] = new Parameter("@Telefono", objBEMedico.Telefono);
            prm_Params[15] = new Parameter("@IdEspecialidad", objBEMedico.IdEspecialidad);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            valor = objBEMedico.MedicoId;
            return valor;
        }

        ///<summary>Inactiva el registro en la tabla Medico.</summary>
        ///<param name="objBEMedico">Entidad de Negocio</param> 
        ///<item><CreadoPor>Creado Por Jordan Mateo Pizarro</CreadoPor></item>
        ///<item><FecCrea>17/10/2012</FecCrea></item></list></remarks>
        public void InactivarMedico(WCO_ListarMedico_Result objBEMedico)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InactivarMedico");
            Parameter[] prm_Params = new Parameter[4];
            prm_Params[0] = new Parameter("@Estado", objBEMedico.Estado);
            prm_Params[1] = new Parameter("@UltimoIp", objBEMedico.IpModificacion);
            prm_Params[2] = new Parameter("@UsuarioModificacion", objBEMedico.UsuarioModificacion);
            prm_Params[3] = new Parameter("@MedicoId", objBEMedico.MedicoId);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }

        ///<summary>Valida el Nombre Completo en la tabla Medico.</summary>
        ///<param name="objBEMedico">Entidad de Negocio</param>
        ///<item><CreadoPor>Creado Por Jordan Mateo Pizarro</CreadoPor></item>
        ///<item><FecCrea>17/02/2013</FecCrea></item></list></remarks>
        public static bool ValidarExisteMedico(WCO_ListarMedico_Result objBEMedico)
        {
            DataOperation dop_DataOperation = new DataOperation("WCO_ExisteMedico");
            Parameter[] prm_Params = new Parameter[3];
            prm_Params[0] = new Parameter("@MedicoId", objBEMedico.MedicoId);
            prm_Params[1] = new Parameter("@NombreCompleto", objBEMedico.Busqueda);
            prm_Params[2] = new Parameter("@flagSalida", DbType.Int32);
            dop_DataOperation.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation);
            if (Convert.ToInt32(dop_DataOperation.GetParameterByName("flagSalida").Value) >= 1)
            {
                return false;
            }
            return true;
        }

        ///<summary>Valida el CMP en la tabla Medico.</summary>
        ///<param name="objBEMedico">Entidad de Negocio</param>
        ///<item><CreadoPor>Creado Por Jordan Mateo Pizarro</CreadoPor></item>
        ///<item><FecCrea>17/02/2013</FecCrea></item></list></remarks>
        public static bool ValidarExisteCodigoMedico(WCO_ListarMedico_Result objBEMedico)
        {
            DataOperation dop_DataOperation = new DataOperation("WCO_ExisteMedicoCodigo");
            Parameter[] prm_Params = new Parameter[3];
            prm_Params[0] = new Parameter("@MedicoId", objBEMedico.MedicoId);
            prm_Params[1] = new Parameter("@CMP", objBEMedico.CMP);
            prm_Params[2] = new Parameter("@flagSalida", DbType.Int32);
            dop_DataOperation.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation);
            if (Convert.ToInt32(dop_DataOperation.GetParameterByName("flagSalida").Value) >= 1)
            {
                return false;
            }
            return true;
        }

        ///<summary>Valida el CMP en la tabla Medico.</summary>
        ///<param name="objBEMedico">Entidad de Negocio</param>
        ///<item><CreadoPor>Creado Por Jordan Mateo Pizarro</CreadoPor></item>
        ///<item><FecCrea>17/02/2013</FecCrea></item></list></remarks>
        public static bool ValidarExisteDNIMedico(WCO_ListarMedico_Result objBEMedico)
        {
            DataOperation dop_DataOperation = new DataOperation("WCO_ExisteMedicoDNI");
            Parameter[] prm_Params = new Parameter[3];
            prm_Params[0] = new Parameter("@MedicoId", objBEMedico.MedicoId);
            prm_Params[1] = new Parameter("@Documento", objBEMedico.Documento);
            prm_Params[2] = new Parameter("@flagSalida", DbType.Int32);
            dop_DataOperation.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation);
            if (Convert.ToInt32(dop_DataOperation.GetParameterByName("flagSalida").Value) >= 1)
            {
                return false;
            }
            return true;
        }

        #endregion

        #region eeeeeeeeeeeee


        #endregion

        public int InsertarPreAdmision(WCO_ListarPreCarga_Result Obj_PreAdmision)
        {
            System.Nullable<int> iReturnValue;
            int valorRetorno = 0; //ERROR
            using (var context = new BDComercialEntities())
            {
                try
                {
                    iReturnValue = context.WCO_InsertarPreAdmision(Obj_PreAdmision.IdPreAdmision, Obj_PreAdmision.FechaProgramacion, Obj_PreAdmision.TipoOperacionId,
                        Obj_PreAdmision.Cliente, Obj_PreAdmision.Acceso, Obj_PreAdmision.Servicio,
                        Obj_PreAdmision.Sucursal, Obj_PreAdmision.CodigoHC, Obj_PreAdmision.CodigoOA,
                        Obj_PreAdmision.TipoAtencion, Obj_PreAdmision.TipoOrden, Obj_PreAdmision.Numerocama,
                        Obj_PreAdmision.PacienteNombres, Obj_PreAdmision.PacienteAPPaterno, Obj_PreAdmision.PacienteAPMaterno,
                        Obj_PreAdmision.FechaNacimiento, Obj_PreAdmision.Sexo, Obj_PreAdmision.TipoDocumento,
                        Obj_PreAdmision.Documento, Obj_PreAdmision.Especialidad_Nombre, Obj_PreAdmision.IdOrdenAtencion,
                        Obj_PreAdmision.Linea, Obj_PreAdmision.Componente, Obj_PreAdmision.ComponenteNombre,
                        Obj_PreAdmision.CantidadSolicitada, Obj_PreAdmision.MedicoNombres, Obj_PreAdmision.MedicoAPPaterno,
                        Obj_PreAdmision.MedicoAPMaterno, Obj_PreAdmision.CMP, Obj_PreAdmision.Aseguradora_RUC,
                        Obj_PreAdmision.Aseguradora_Nombre, Obj_PreAdmision.Empleadora_RUC, Obj_PreAdmision.Empleadora_Nombre,
                        Obj_PreAdmision.FechaLimiteAtencion, Obj_PreAdmision.PacienteTelefono, Obj_PreAdmision.Pacienteemail,
                        Obj_PreAdmision.Observacion, Obj_PreAdmision.NroLote, Obj_PreAdmision.Estado, Obj_PreAdmision.IpCreacion,
                        Obj_PreAdmision.UsuarioCreacion).SingleOrDefault();
                    valorRetorno = Convert.ToInt32(iReturnValue.ToString().Trim());
                }
                catch (Exception ex)
                {
                    valorRetorno = -1;
                    throw ex;
                }
            }
            return valorRetorno;
        }

        public int AnulacionPreAdmision(string EmpresaProveedor, string Acceso, int IdOrdenAtencion, int Linea, string Mensaje)
        {
            System.Nullable<int> iReturnValue;
            int valorRetorno = 0; //ERROR
            using (var context = new BDComercialEntities())
            {
                try
                {
                    int id = 0;
                    iReturnValue = context.WCO_AnulacionPreAdmision(EmpresaProveedor, Acceso, IdOrdenAtencion, Linea, Mensaje, 3, 0).SingleOrDefault();
                    if (id == 0)
                    {
                        valorRetorno = -2; //"LA ATENCION YA NO SE PUEDE ANULAR";
                    }
                    else
                    {
                        valorRetorno = Convert.ToInt32(iReturnValue);
                    }
                }
                catch (Exception ex)
                {
                    //throw ex;
                    valorRetorno = -1; //"4|Se perdio el Token";   //"Insercion CorrectA";
                }
            }
            return valorRetorno;
        }

        public List<VW_HomologacionCliente> ListaHomologacionCliente(string EmpresaProveedor)
        {
            List<VW_HomologacionCliente> lst = new List<VW_HomologacionCliente>();
            using (var context = new BDComercialEntities())
            {
                if (EmpresaProveedor.Length == 11)
                {
                    lst = context.VW_HomologacionCliente.Where(t => t.Documento == EmpresaProveedor).ToList();
                }
            }
            return lst;
        }

        public List<WCO_ListarHomologacionPreAdmision_Result> ListaHomologacionPreAdmision(string EmpresaProveedor)
        {
            List<WCO_ListarHomologacionPreAdmision_Result> lst = new List<WCO_ListarHomologacionPreAdmision_Result>();
            using (var context = new BDComercialEntities())
            {
                if (EmpresaProveedor.Length == 11)
                {
                    lst = context.WCO_ListarHomologacionPreAdmision(null, null, null, null, null, EmpresaProveedor).ToList();
                }
            }
            return lst;
        }

        ADDAT_Admision sd = new ADDAT_Admision();
        ADDAT_TipoOperacion to = new ADDAT_TipoOperacion();

        public ADBE_Admision GenerarAdmision(List<ADBE_PreAdmision> listaServicio)
        {
            ADBE_Admision RestPuestaAdmision = new ADBE_Admision();
            //      valores 0   = Error;   1= Error;  0= Error;  0= Error;  0= Error;  
            //      CantRoe     = -2;       return CantRoe.ToString() + "|No existe la Atencion";
            //      CantRoe     = -1;        
            //      return CantRoe.ToString() + "|" + ex.StackTrace;
            DataTable dtPerfil = new DataTable();
            DataTable dt_Orden = new DataTable();
            List<ADBE_ContratoPrecio> ListPrecio = new List<ADBE_ContratoPrecio>();
            List<ADBE_ContratoPrecio> ListPrecioComponente = new List<ADBE_ContratoPrecio>();
            ADBE_Admision obj_BEAdmision = new ADBE_Admision();
            int ret = 0;
            int CantRoe = 0;
            string observacion = "";
            try
            {
                if (!string.IsNullOrEmpty(listaServicio[0].FechaLimiteAtencion.ToString()))
                {
                    int FecRegistro = (DateTime.Now.Year * 12 * 30) + (DateTime.Now.Month * 30) + (DateTime.Now.Day);
                    DateTime FechaLimite = Convert.ToDateTime(listaServicio[0].FechaLimiteAtencion.ToString());
                    int FecLimite = (FechaLimite.Year * 12 * 30) + (FechaLimite.Month * 30) + (FechaLimite.Day);
                    if (FecRegistro > FecLimite)
                    {
                        //  UtilScripts.Alert(this.Page, "Los exámenes han excedido la fecha limite " + FechaLimite + ", se deben actualizar para poder registrarlos");
                        observacion += CantRoe.ToString() + "|" + "Los exámenes han excedido la fecha limite " + FechaLimite + ", se deben actualizar para poder registrarlos";
                        //return observacion;
                        RestPuestaAdmision.Comentario = observacion;
                        return RestPuestaAdmision;
                    }
                }

                if (!string.IsNullOrEmpty(listaServicio[0].Documento.ToString()))
                {
                    if (!string.IsNullOrEmpty(listaServicio[0].TipoDocumento.ToString()))
                    {
                        obj_BEAdmision.PK.Persona.PerNumeroDocumento = listaServicio[0].Documento.ToString().Trim();
                        if (listaServicio[0].TipoDocumento == "D")
                        {
                            if (listaServicio[0].Documento.ToString().Length != 8)
                            {
                                observacion += CantRoe.ToString() + "|" + "El Documento de Identidad no tiene los digitos requeridos " + listaServicio[0].Documento + ", se deben actualizar para poder registrarlos";
                                //return observacion;
                                RestPuestaAdmision.Comentario = observacion;
                                return RestPuestaAdmision;
                            }
                        }
                    }
                    else
                    {
                        observacion += CantRoe.ToString() + "|" + "El Paciente no cuenta con un Tipo Documento " + listaServicio[0].Documento + ", se deben actualizar para poder registrarlos";
                        //return observacion;
                        RestPuestaAdmision.Comentario = observacion;
                        return RestPuestaAdmision;
                    }
                }
                else
                {
                    observacion += CantRoe.ToString() + "|" + "El Paciente no cuenta con un Documento de Identidad " + listaServicio[0].Documento + ", se deben actualizar para poder registrarlos";
                    //return observacion;
                    RestPuestaAdmision.Comentario = observacion;
                    return RestPuestaAdmision;
                }
                if (!string.IsNullOrEmpty(listaServicio[0].TipoDocumento.ToString())) obj_BEAdmision.PK.Persona.PerTipoDoc = listaServicio[0].TipoDocumento.ToString();
                if (!string.IsNullOrEmpty(listaServicio[0].CodigoHC.ToString())) obj_BEAdmision.HistoriaClinica = listaServicio[0].CodigoHC.ToString();
                if (!string.IsNullOrEmpty(listaServicio[0].PacienteAPPaterno.ToString())) obj_BEAdmision.PK.Persona.ApellidoPaterno = listaServicio[0].PacienteAPPaterno.ToString().Trim().Replace("'", "");
                if (!string.IsNullOrEmpty(listaServicio[0].PacienteAPMaterno.ToString())) obj_BEAdmision.PK.Persona.ApellidoMaterno = listaServicio[0].PacienteAPMaterno.ToString().Trim().Replace("'", "");
                if (!string.IsNullOrEmpty(listaServicio[0].PacienteNombres.ToString())) obj_BEAdmision.PK.Persona.Nombres = listaServicio[0].PacienteNombres.ToString().Trim().Replace("'", "");
                if (!string.IsNullOrEmpty(listaServicio[0].FechaNacimiento.ToString())) obj_BEAdmision.PK.Persona.PerFechaNacimiento = Convert.ToDateTime(listaServicio[0].FechaNacimiento.ToString());
                if (!string.IsNullOrEmpty(listaServicio[0].Sexo.ToString())) obj_BEAdmision.PK.Persona.PerSexo = listaServicio[0].Sexo.ToString();
                if (!string.IsNullOrEmpty(listaServicio[0].FechaLimiteAtencion.ToString())) obj_BEAdmision.FechaLimite = Convert.ToDateTime(listaServicio[0].FechaLimiteAtencion);

                ADBE_TipoOperacion ObjTipoOperacion = new ADBE_TipoOperacion();
                ObjTipoOperacion.Pk.TipoOperacionId = listaServicio[0].TipoOperacionId;
                ObjTipoOperacion.TipEstado = 1;
                ObjTipoOperacion = to.ObtenerxIdTipoOperacion(ObjTipoOperacion);
                if (ObjTipoOperacion.Fechinicio > DateTime.Now.Date || ObjTipoOperacion.FechTermino < DateTime.Now.Date)
                {
                    // UtilScripts.Alert(this.Page, "El Convenio ha caducado esta cubría desde el" + ObjTipoOperacion.Fechinicio + " hasta el" + ObjTipoOperacion.FechTermino);
                    observacion += CantRoe.ToString() + "|" + "El Contrato ha caducado esta cubría desde el" + ObjTipoOperacion.Fechinicio + " hasta el" + ObjTipoOperacion.FechTermino;
                    //return observacion;
                    RestPuestaAdmision.Comentario = observacion;
                    return RestPuestaAdmision;
                }

                int IndicadorInterfase = 0;
                if (!string.IsNullOrEmpty(listaServicio[0].IndicadorWS.ToString())) IndicadorInterfase = Convert.ToInt32(listaServicio[0].IndicadorWS);

                if (IndicadorInterfase == 1)
                {
                    DataTable dt_Examen = new DataTable();
                    obj_BEAdmision.PK.TipoOperacion.Pk.Persona.PK.IdPersona = ObjTipoOperacion.Pk.Persona.PK.IdPersona;
                    obj_BEAdmision.PK.Sede.PK.IdSede = listaServicio[0].IdSede;
                    obj_BEAdmision.OrdenAtencion = listaServicio[0].CodigoOA.ToString();
                    dt_Examen = sd.ValidarOAExamen(obj_BEAdmision);
                    int Unidades = 0;
                    string NroPeticion = "";
                    foreach (ADBE_PreAdmision obj_Seleccionados in listaServicio)
                    {
                        if (dt_Examen.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt_Examen.Rows.Count; i++)
                            {
                                DataRow _row = dt_Examen.Rows[i];
                                if (_row["Componente"].ToString().Trim() == obj_Seleccionados.Componente.ToString().Trim() &&
                                    _row["Linea"].ToString().Trim() == obj_Seleccionados.Linea.ToString().Trim() &&
                                    _row["IdOrdenAtencion"].ToString().Trim() == obj_Seleccionados.IdOrdenAtencion.ToString().Trim())
                                {
                                    Unidades++;
                                }
                            }
                        }
                    }

                    if (listaServicio.Count <= Unidades)
                    {
                        DataTable dt_Peticiones = new DataTable();
                        dt_Peticiones = sd.ListarAdmisionExistente(obj_BEAdmision);
                        for (int i = 0; i < dt_Peticiones.Rows.Count; i++)
                        {
                            DataRow _row = dt_Peticiones.Rows[i];
                            if (listaServicio[0].IdOrdenAtencion == int.Parse(_row["IdOrdenAtencion"].ToString()))
                            {
                                NroPeticion += _row["NroPeticion"].ToString().Trim() + " ";
                            }
                            if (_row["OrdenSinapa"].ToString().LongCount() > 0)
                            {
                                observacion = CantRoe.ToString() + "|" + "Esta Atencion ya cuenta con una Referencia generada: " + _row["OrdenSinapa"];
                                //return observacion;
                                RestPuestaAdmision.Comentario = observacion;
                                return RestPuestaAdmision;
                            }
                        }
                        observacion += "La OA ya fue ingresada con el Nro de Petición " + NroPeticion + " Examenes " + Unidades;
                        //return observacion;
                        RestPuestaAdmision.Comentario = observacion;
                        return RestPuestaAdmision;
                    }
                }

                List<ADBE_AdmisionServicio> ListAdmisionDetalle = new List<ADBE_AdmisionServicio>();
                ListPrecio = sd.ListadoTipoOperacionPrecio(ObjTipoOperacion.Pk.TipoOperacionId, 1);
                foreach (ADBE_PreAdmision obj_Seleccionados in listaServicio)
                {
                    if (IndicadorInterfase == 1)
                    {
                        ListPrecioComponente = ListPrecio.Where(o => o.CodigoHomologado == obj_Seleccionados.Componente && o.ClasificadorMovimiento == obj_Seleccionados.ClasificadorMovimiento).ToList();
                    }
                    else
                    {
                        ListPrecioComponente = ListPrecio.Where(o => o.CodigoComponente == obj_Seleccionados.Componente && o.ClasificadorMovimiento == obj_Seleccionados.ClasificadorMovimiento).ToList();
                    }

                    if (ListPrecioComponente.Count == 1)
                    {
                        foreach (ADBE_ContratoPrecio intobj2 in ListPrecioComponente)
                        {
                            obj_BEAdmision.ClasificadorMovimiento = obj_Seleccionados.ClasificadorMovimiento;
                            obj_BEAdmision.UsuarioCreacion = obj_Seleccionados.UsuarioCreacion;
                            obj_BEAdmision.IpCreacion = obj_Seleccionados.IpCreacion;
                            obj_BEAdmision.UsuarioModificacion = obj_Seleccionados.UsuarioModificacion;
                            obj_BEAdmision.IpModificacion = obj_Seleccionados.IpModificacion;
                            obj_BEAdmision.PK.TipoOperacion.Pk.TipoOperacionId = ObjTipoOperacion.Pk.TipoOperacionId;
                            obj_BEAdmision.PK.TipoOperacion.Pk.Persona.PK.IdPersona = ObjTipoOperacion.Pk.Persona.PK.IdPersona;
                            obj_BEAdmision.PK.TipoOperacion.Pk.Persona.NombreCompleto = ObjTipoOperacion.Pk.Persona.NombreCompleto;
                            obj_BEAdmision.PK.TipoOperacion.Pk.UnidadNegocio = ObjTipoOperacion.Pk.UnidadNegocio;
                            obj_BEAdmision.PK.TipoOperacion.Pk.TipoPaciente = ObjTipoOperacion.Pk.TipoPaciente;
                            obj_BEAdmision.PK.Sede.PK.IdSede = obj_Seleccionados.IdSede;
                            obj_BEAdmision.UneuNegocioId = ObjTipoOperacion.Pk.UnidadNegocio;
                            obj_BEAdmision.Estado = 1;

                            obj_BEAdmision.TipoOrden = obj_Seleccionados.TipoOrden;
                            obj_BEAdmision.TipoAtencion = Convert.ToInt32(obj_Seleccionados.TipoAtencion);
                            if (obj_Seleccionados.Empleadora_RUC != null)
                            {
                                if (!string.IsNullOrEmpty(obj_Seleccionados.Empleadora_RUC.ToString())) obj_BEAdmision.PK.Empresa.PerNumeroDocumento = obj_Seleccionados.Empleadora_RUC.ToString();
                            }
                            if (obj_Seleccionados.Empleadora_Nombre != null)
                            {
                                if (!string.IsNullOrEmpty(obj_Seleccionados.Empleadora_Nombre.ToString())) obj_BEAdmision.PK.Empresa.NombreCompleto = obj_Seleccionados.Empleadora_Nombre.ToString().Replace("'", "");
                            }
                            if (obj_Seleccionados.Aseguradora_Nombre != null)
                            {
                                if (!string.IsNullOrEmpty(obj_Seleccionados.Aseguradora_Nombre.ToString())) obj_BEAdmision.Aseguradora_Nombre = obj_Seleccionados.Aseguradora_Nombre.ToString().Replace("'", "");
                            }

                            if (!string.IsNullOrEmpty(obj_Seleccionados.CMP.ToString()))
                            {
                                if (!string.IsNullOrEmpty(obj_Seleccionados.CMP.ToString())) obj_BEAdmision.PK.Titular.PerNumeroDocumento = obj_Seleccionados.CMP.ToString().Trim();
                                if (!string.IsNullOrEmpty(obj_Seleccionados.MedicoAPPaterno.ToString())) obj_BEAdmision.PK.Titular.ApellidoPaterno = obj_Seleccionados.MedicoAPPaterno.ToString().Trim().Replace("'", "");
                                if (!string.IsNullOrEmpty(obj_Seleccionados.MedicoAPMaterno.ToString())) obj_BEAdmision.PK.Titular.ApellidoMaterno = obj_Seleccionados.MedicoAPMaterno.ToString().Trim().Replace("'", "");
                                if (!string.IsNullOrEmpty(obj_Seleccionados.MedicoNombres.ToString())) obj_BEAdmision.PK.Titular.Nombres = obj_Seleccionados.MedicoNombres.ToString().Trim().Replace("'", "");
                                obj_BEAdmision.MedicoDescripcion = obj_BEAdmision.PK.Titular.ApellidoPaterno + " " + obj_BEAdmision.PK.Titular.ApellidoMaterno + " " + obj_BEAdmision.PK.Titular.Nombres;
                            }
                            else
                            {
                                obj_BEAdmision.MedicoDescripcion = "Medico Automatico";
                                obj_BEAdmision.PK.Titular.ApellidoPaterno = "Medico Automatico";
                            }

                            ADBE_AdmisionServicio objBEAdmisionServicio = new ADBE_AdmisionServicio();
                            objBEAdmisionServicio.PK.Admision = obj_BEAdmision;
                            objBEAdmisionServicio.PK.UnidadNegocio.Pk.UneuNegocioId = ObjTipoOperacion.Pk.UnidadNegocio;
                            objBEAdmisionServicio.UsuarioCreacion = obj_Seleccionados.UsuarioCreacion;
                            objBEAdmisionServicio.IpCreacion = obj_Seleccionados.IpCreacion;
                            objBEAdmisionServicio.UsuarioModificacion = obj_Seleccionados.UsuarioCreacion;
                            objBEAdmisionServicio.IpModificacion = obj_Seleccionados.IpCreacion;
                            objBEAdmisionServicio.PK.Admision.PK.Empresa.PK.IdPersona = ObjTipoOperacion.Pk.Persona.PK.IdPersona;
                            objBEAdmisionServicio.PK.Componente.Pk.CodigoComponente = obj_Seleccionados.Componente.ToString().Trim();
                            objBEAdmisionServicio.PK.Componente.Descripcion = obj_Seleccionados.ComponenteNombre.ToString().Trim();
                            objBEAdmisionServicio.PK.Componente.Abreviatura = intobj2.CodigoComponente;
                            objBEAdmisionServicio.PK.Componente.CentroCosto = intobj2.CodigoHomologado;
                            objBEAdmisionServicio.Valor = intobj2.Precio;
                            objBEAdmisionServicio.PK.Componente.CantidadKit = intobj2.PrecioUnitarioLista;
                            objBEAdmisionServicio.IdCobertura = obj_Seleccionados.Linea;
                            objBEAdmisionServicio.PK.Componente.IdArea = obj_Seleccionados.IdOrdenAtencion;
                            objBEAdmisionServicio.Conexion = 0;//0= directo   3=si es precarga WCO_PreAdmision o 1=si es sinapa WCO_OrdenAtencionInterfase 
                            if (obj_Seleccionados.ClasificadorMovimiento == "01")
                            {
                                objBEAdmisionServicio.Cantidad = 1;
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(obj_Seleccionados.CantidadSolicitada.ToString()))
                                {
                                    if (Convert.ToDecimal(obj_Seleccionados.CantidadSolicitada.ToString()) > 0)
                                    {
                                        objBEAdmisionServicio.Cantidad = Convert.ToInt32(Convert.ToDecimal(obj_Seleccionados.CantidadSolicitada.ToString()));
                                    }
                                    else
                                    {
                                        objBEAdmisionServicio.Cantidad = 1;
                                    }
                                }
                                else
                                {
                                    objBEAdmisionServicio.Cantidad = 1;
                                }
                            }
                            objBEAdmisionServicio.Estado = 1;//Pendiente
                            ListAdmisionDetalle.Add(objBEAdmisionServicio);
                            ADBE_ComponentePerfil obj_ComponentePerfil = new ADBE_ComponentePerfil();
                            obj_ComponentePerfil.Pk.Componente.Pk.CodigoComponente = intobj2.CodigoComponente;
                            obj_ComponentePerfil.Pk.Componente.Estado = 1;
                            dtPerfil = sd.ListadoComponentePerfil(obj_ComponentePerfil);
                            if (dtPerfil.Rows.Count > 0)
                            {
                                ret++;
                                if (ret == 1)
                                {
                                    dt_Orden = dtPerfil;
                                }
                                else
                                {
                                    foreach (DataRow row in dtPerfil.Rows)
                                    {
                                        DataRow rw = dt_Orden.NewRow();
                                        rw["CodigoComponente"] = row["CodigoComponente"];
                                        rw["CodigoHomologado"] = row["CodigoHomologado"];
                                        dt_Orden.Rows.Add(rw);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (ListPrecioComponente.Count > 1)
                        {
                            observacion += CantRoe.ToString() + "|" + "Este examen= " + obj_Seleccionados.Componente + " Duplicado de Homologación para este Contrato";
                        }
                        else
                        {
                            observacion += CantRoe.ToString() + "|" + "Este examen= " + obj_Seleccionados.Componente + " " + obj_Seleccionados.ClasificadorMovimiento +
                            " no esta en el Contrato : " + ObjTipoOperacion.Observacion + " se deben actualizar para poder registrarlos";
                        }
                    }
                }

                if (observacion.Length == 0)
                {
                    if (dt_Orden.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt_Orden.Rows)
                        {
                            foreach (ADBE_AdmisionServicio obj_Registros in ListAdmisionDetalle)
                            {
                                if (obj_Registros.PK.Componente.Abreviatura == row["CodigoHomologado"].ToString())
                                {
                                    observacion += CantRoe.ToString() + "|" + "Este examen " + obj_Registros.PK.Componente.Pk.CodigoComponente + "|" + obj_Registros.PK.Componente.Abreviatura + " ya esta contenido en un Perfil: " + row["CodigoComponente"].ToString();
                                }
                            }
                        }
                    }
                }
                ADBE_Admision NroRefer_Admsion = new ADBE_Admision();
                if (observacion.Length == 0)//CUANDO TODO ESTA BIEN SE CREA
                {
                    String retorno = "";
                    Nullable<int> IdAdmision = 0;
                    Nullable<int> IdDetalle = 0;
                    Nullable<int> IdP = 0;
                    obj_BEAdmision.Comentario = "WS-" + IndicadorInterfase + " ";
                    if (IndicadorInterfase == 1)
                    {
                        IdAdmision = sd.InsertarAdmisionOA(obj_BEAdmision);
                    }
                    if (IndicadorInterfase == 0)
                    {
                        IdAdmision = sd.InsertarAdmision(obj_BEAdmision);
                    }
                    observacion += "oK-Clinica =" + IdAdmision + "|";

                    foreach (ADBE_AdmisionServicio obj_Registros in ListAdmisionDetalle)
                    {
                        obj_Registros.PK.Admision.PK.IdAdmision = IdAdmision;
                        if (IndicadorInterfase == 1)
                        {
                            IdDetalle = sd.InsertarAdmisionServicioOA(obj_Registros);
                        }
                        if (IndicadorInterfase == 0)
                        {
                            IdDetalle = sd.InsertarAdmisionServicio(obj_Registros);
                        }
                        if (IdDetalle > 0)
                        {
                            observacion += "Detalle =" + IdDetalle + "|";
                            IdP++;
                        }
                        else
                        {
                            observacion += "Error en la Homologación =" + IdDetalle + "|";
                        }
                    }
                    if (IdP == ListAdmisionDetalle.Count)
                    {
                        NroRefer_Admsion = obj_BEAdmision;
                        NroRefer_Admsion = BaseDatos.TraerXCodigo(NroRefer_Admsion);
                        NroRefer_Admsion.PK.TipoOperacion = ObjTipoOperacion;
                        obj_BEAdmision.PK.IdAdmision = IdAdmision;
                        CABE_Transaccion obj_pTransaccion = new CABE_Transaccion();
                        obj_pTransaccion.PK.Admision = NroRefer_Admsion;
                        obj_pTransaccion.UsuarioCreacion = obj_BEAdmision.UsuarioCreacion;
                        obj_pTransaccion.IdClienteFacturacion = obj_BEAdmision.PK.TipoOperacion.Pk.Persona.PK.IdPersona;
                        obj_pTransaccion.NombreCliente = obj_BEAdmision.PK.TipoOperacion.Pk.Persona.NombreCompleto;
                        obj_pTransaccion.TipoComprobante = "FA";
                        obj_pTransaccion.Moneda = "LO";
                        obj_pTransaccion.MontoTotalLocal = Convert.ToDecimal("0,00");
                        obj_pTransaccion.MontoTotal = Convert.ToDecimal("0,00");
                        if (ObjTipoOperacion.Pk.TipoAdmision == 1)
                        {
                            observacion += "oK-Clinica =" + NroRefer_Admsion.NroPeticion;
                            sd.InsertarTransaccion(obj_pTransaccion);
                        }
                        else
                        {
                            if (ObjTipoOperacion.FlatAprobacion == 1)
                            {
                                observacion += "oK-Convenio/ Se ha Generado para su Aprobacion|" + NroRefer_Admsion.NroPeticion;
                            }
                            else
                            {
                                observacion += "oK-Convenio =" + NroRefer_Admsion.NroPeticion;
                                sd.InsertarTransaccion(obj_pTransaccion);
                            }
                        }
                    }
                }
                else
                {
                    RestPuestaAdmision.Comentario = observacion;
                    return RestPuestaAdmision;
                }
                //return observacion;
                return NroRefer_Admsion;
            }
            catch (Exception ex)
            {
                CantRoe = 0;
                observacion += CantRoe.ToString() + "|" + ex.StackTrace;
                //return observacion;
                RestPuestaAdmision.Comentario = observacion;
                return RestPuestaAdmision;
            }
        }

    }
}