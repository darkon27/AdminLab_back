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

namespace WebApiServices.Models.Datos
{
    public class ADDAT_Perfil
    {

        public List<WCO_LISTARPERFILPAG_Result> ListarPerfil(WCO_LISTARPERFILPAG_Result ObjPaginas)
        {
            List<WCO_LISTARPERFILPAG_Result> lst = new List<WCO_LISTARPERFILPAG_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_LISTARPERFILPAG(ObjPaginas.estado, ObjPaginas.Perfil).ToList();
            }
            return lst;
        }

        ///<summary>Valida si el perfil ingresado existe ya en la tabla.</summary>
        ///<param name="ADBE_Perfil">Entidad de Negocio</param>
        ///<returns>Retorna Veredadero o Falso en caso ya exista el perfil ingresado.</returns>
        ///<remarks><list type="bullet">
        ///<item><CreadoPor>Creado Por Isaac Jurado</CreadoPor></item>
        ///<item><FecCrea>12/12/2011</FecCrea></item></list></remarks>
        public bool ExistePerfil(Perfil obj_pBEPerfil)
        {
            DataOperation dop_DataOperation = new DataOperation("WCO_ExistePerfil");

            Parameter[] prm_Params = new Parameter[2];
            prm_Params[0] = new Parameter("@perfil", obj_pBEPerfil.perfil);
            prm_Params[1] = new Parameter("@flagSalida", DbType.Int32);
            dop_DataOperation.Parameters.AddRange(prm_Params);

            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation);
            return Convert.ToInt32(dop_DataOperation.GetParameterByName("@flagSalida").Value) >= 1;
        }

        /// <summary>Método para Eliminar un perfil. Cambio de estado de Activo a Inactivo.</summary>
        /// <param name="WCO_LISTARPERFILPAG_Result">Entidad con el id de un Perfil.</param>
        /// <returns>Cual es el Output de la función.</returns>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Creado por Isaac Jurado.</CreadoPor></item>
        /// <item><FecCrea>12/12/2011</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu></FecActu></item>
        /// <item><Resp></Resp></item>
        /// <item><Mot></Mot></item></list></remarks>
        public string Inactivar(Perfil obj_pBEPerfil)
        {
            try
            {
                DataOperation dop_DataOperation = new DataOperation("WCO_eliminarperfil");
                Parameter[] prm_Params = new Parameter[2];
                prm_Params[0] = new Parameter("@perfil", obj_pBEPerfil.perfil);
                prm_Params[1] = new Parameter("@ultimousuario", obj_pBEPerfil.ultimousuario);
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

        /// <summary>Método para Insertar la cabecera de un Perfil.</summary>
        /// <param name="WCO_LISTARPERFILPAG_Result">Entidad con el id de un Perfil.</param>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Creado por Isaac Jurado.</CreadoPor></item>
        /// <item><FecCrea>12/12/2011</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu></FecActu></item>
        /// <item><Resp></Resp></item>
        /// <item><Mot></Mot></item></list></remarks>
        public string Insertar(Perfil obj_pBEPerfil)
        {
            DataOperation dop_DataOperation = new DataOperation("WCO_insertarcabeceraperfil");
            try
            {
                Parameter[] prm_Params = new Parameter[3];
                prm_Params[0] = new Parameter("@ESTADO", obj_pBEPerfil.estado);
                prm_Params[1] = new Parameter("@PERFIL", obj_pBEPerfil.perfil);
                prm_Params[2] = new Parameter("@ULTIMOUSUARIO", obj_pBEPerfil.ultimousuario);
                dop_DataOperation.Parameters.AddRange(prm_Params);
                DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation);

                ////List<file> ListarCopy = (List<file>)obj_pBEPerfil.ListaPaginas;

                List<file> ListPrincipal = (List<file>)Newtonsoft.Json.JsonConvert.DeserializeObject(obj_pBEPerfil.ListaPaginas.ToString(), typeof(List<file>));
                foreach (file Objfila in ListPrincipal)
                {
                    List<file> ListGrupo = (List<file>)Newtonsoft.Json.JsonConvert.DeserializeObject(Objfila.children.ToString(), typeof(List<file>));
                    foreach (file Objgrupo in ListGrupo)
                    {
                        List<file> ListPagina = (List<file>)Newtonsoft.Json.JsonConvert.DeserializeObject(Objgrupo.children.ToString(), typeof(List<file>));
                        foreach (file ObjPagina in ListPagina)
                        {
                            if (ObjPagina.data.Length > 10)
                            {
                                WCO_PerfilPaginasOpciones_Result obj_Opciones = new WCO_PerfilPaginasOpciones_Result();
                                obj_Opciones.AplicacionCodigo = ObjPagina.data.Substring(0, 2);
                                obj_Opciones.Grupo = ObjPagina.data.Substring(2, 6);
                                obj_Opciones.Concepto = ObjPagina.data.Substring(8, 6);
                                obj_Opciones.ConceptoPadre = ObjPagina.data.Substring(2, 6);
                                obj_Opciones.Perfil = obj_pBEPerfil.perfil;
                                obj_Opciones.xEstado = obj_pBEPerfil.estado;
                                InsertarDetallePerfil(obj_Opciones);
                            }
                        }
                    }
                }
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

        /// <summary>Método para Actualizar la Cabecera de un perfil.</summary>
        /// <param name="WCO_LISTARPERFILPAG_Result">Entidad con el id del Perfil.</param>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Creado por Isaac Jurado.</CreadoPor></item>
        /// <item><FecCrea>09/12/2011</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu></FecActu></item>
        /// <item><Resp></Resp></item>
        /// <item><Mot></Mot></item></list></remarks>
        public string Actualizar(Perfil obj_pBEPerfil)
        {
            DataOperation dop_DataOperation = new DataOperation("WCO_ActualizarCabeceraPerfil");
            try
            {
                Parameter[] prm_Params = new Parameter[3];
                prm_Params[0] = new Parameter("@ESTADO", obj_pBEPerfil.estado);
                prm_Params[1] = new Parameter("@PERFIL", obj_pBEPerfil.perfil);
                prm_Params[2] = new Parameter("@ULTIMOUSUARIO", obj_pBEPerfil.ultimousuario);
                dop_DataOperation.Parameters.AddRange(prm_Params);
                DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation);

                ElimnarDetallePerfil(obj_pBEPerfil);
                List<file> ListPrincipal = (List<file>)Newtonsoft.Json.JsonConvert.DeserializeObject(obj_pBEPerfil.ListaPaginas.ToString(), typeof(List<file>));
                foreach (file Objfila in ListPrincipal)
                {
                    List<file> ListGrupo = (List<file>)Newtonsoft.Json.JsonConvert.DeserializeObject(Objfila.children.ToString(), typeof(List<file>));
                    foreach (file Objgrupo in ListGrupo)
                    {
                        List<file> ListPagina = (List<file>)Newtonsoft.Json.JsonConvert.DeserializeObject(Objgrupo.children.ToString(), typeof(List<file>));
                        foreach (file ObjPagina in ListPagina)
                        {
                            if (ObjPagina.data.Length > 10)
                            {
                                WCO_PerfilPaginasOpciones_Result obj_Opciones = new WCO_PerfilPaginasOpciones_Result();
                                obj_Opciones.AplicacionCodigo = ObjPagina.data.Substring(0, 2);
                                obj_Opciones.Grupo = ObjPagina.data.Substring(2, 6);
                                obj_Opciones.Concepto = ObjPagina.data.Substring(8, 6);
                                obj_Opciones.ConceptoPadre = ObjPagina.data.Substring(2, 6);
                                obj_Opciones.Perfil = obj_pBEPerfil.perfil;
                                obj_Opciones.xEstado = obj_pBEPerfil.estado;
                                InsertarDetallePerfil(obj_Opciones);
                            }
                        }
                    }
                }
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

        /// <summary>Método para Insertar el Detalle de un Perfil.</summary>
        /// <param name="obj_pBEPerfil">Entidad con el id de un Perfil.</param>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Creado por Isaac Jurado.</CreadoPor></item>
        /// <item><FecCrea>14/12/2011</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu></FecActu></item>
        /// <item><Resp></Resp></item>
        /// <item><Mot></Mot></item></list></remarks>
        public void InsertarDetallePerfil(WCO_PerfilPaginasOpciones_Result obj_pBEPerfil)
        {
            DataOperation dop_DataOperation = new DataOperation("WCO_insertardetalleperfil");
            try
            {
                Parameter[] prm_Params = new Parameter[5];
                prm_Params[0] = new Parameter("@ESTADO", 'A');
                prm_Params[1] = new Parameter("@PERFIL", obj_pBEPerfil.Perfil);
                prm_Params[2] = new Parameter("@APLICACIONCODIGO", obj_pBEPerfil.AplicacionCodigo);
                prm_Params[3] = new Parameter("@GRUPO", obj_pBEPerfil.Grupo);
                prm_Params[4] = new Parameter("@CONCEPTO", obj_pBEPerfil.Concepto);
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

        /// <summary>Método para Insertar el Detalle de un Perfil.</summary>
        /// <param name="obj_pBEPerfil">Entidad con el id de un Perfil.</param>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Creado por Isaac Jurado.</CreadoPor></item>
        /// <item><FecCrea>14/12/2011</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu></FecActu></item>
        /// <item><Resp></Resp></item>
        /// <item><Mot></Mot></item></list></remarks>
        public void ElimnarDetallePerfil(Perfil obj_pBEPerfil)
        {
            DataOperation dop_DataOperation = new DataOperation("WCO_EliminarPerfilDetalle");
            try
            {
                Parameter[] prm_Params = new Parameter[2];
                prm_Params[0] = new Parameter("@PERFIL", obj_pBEPerfil.perfil);
                prm_Params[1] = new Parameter("@ULTIMOUSUARIO", obj_pBEPerfil.ultimousuario);
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

    }
}