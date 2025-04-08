using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using RoyalSystems.Data;
using System.IO;
using System.Transactions;
using WebApiServices.Models.Entidades;
using WebApiServices.Models.Datos;

namespace WebApiServices.Models
{
    public class BaseDatos
    {
        public static void SaveLog(String documento, int tipoerror, String mensaje, String JsonSeguimiento, int lineaerror, String usuario, String Proceso)
        {
            //var sqlString = UtilsGlobal.ConvertLinesSqlXml("QueryMiscelaneos", "Miscelaneos.ControlErroresLOG");
            // List<SqlParameter> parametros = new List<SqlParameter>();
            // parametros.Add(new SqlParameter("@Documento", documento));
            // parametros.Add(new SqlParameter("@TipoError", tipoerror));
            // parametros.Add(new SqlParameter("@MensajeError", mensaje));
            // parametros.Add(new SqlParameter("@JsonSeguimiento", JsonSeguimiento));
            // parametros.Add(new SqlParameter("@LineaError", lineaerror));
            // parametros.Add(new SqlParameter("@Usuario", usuario));
            // parametros.Add(new SqlParameter("@Proceso", Proceso));
            // UtilsDAO.ExecuteQueryCRUD(sqlString, parametros);
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public static int InsertarLog(string IdOrdenAtencion, string Lineas, string Trama, string Observacion)
        {
            try
            {
                DataOperation dop_Operacion = new DataOperation("WCO_InsertarPdfLog");
                Parameter[] prm_Params = new Parameter[5];
                prm_Params[0] = new Parameter("@Id", DbType.Int32);
                prm_Params[1] = new Parameter("@IdOrdenAtencion", IdOrdenAtencion);
                prm_Params[2] = new Parameter("@Lineas", Lineas);
                prm_Params[3] = new Parameter("@Trama", Trama);
                prm_Params[4] = new Parameter("@Observacion", Observacion);
                dop_Operacion.Parameters.AddRange(prm_Params);
                DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
                return int.Parse(dop_Operacion.GetParameterByName("@Id").Value.ToString());
            }
            catch (Exception ex)
            {
                string asunto = System.DateTime.Now + " | " + "Error Conexión con el SQl ";
                WriteLog(asunto + " | " + ex.ToString().Substring(25, 50));
                return 0;
            }
        }

        public static void WriteLog(string strLog)
        {
            StreamWriter log;
            FileStream fileStream = null;
            DirectoryInfo logDirInfo = null;
            FileInfo logFileInfo;

            string logFilePath = Path.GetFullPath("\\Logs\\");
            logFilePath = logFilePath + "Log-" + System.DateTime.Today.ToString("dd-MM-yyyy") + "." + "txt";
            logFileInfo = new FileInfo(logFilePath);
            logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
            if (!logDirInfo.Exists) logDirInfo.Create();
            if (!logFileInfo.Exists)
            {
                fileStream = logFileInfo.Create();
            }
            else
            {
                fileStream = new FileStream(logFilePath, FileMode.Append);
            }
            log = new StreamWriter(fileStream);
            log.WriteLine(strLog);
            log.Close();
        }

        ///<summary>Método para listar registros de la tabla PersonaMast, teniendo como filtro obligatorio el Nombre Completo.</summary>
        ///<param name="pBECurriculo">Entidad de Negocio</param>
        ///<returns>Listado de los registros de la página actual.</returns>
        ///<remarks><list type="bullet">
        ///<item><CreadoPor>Creado Por Isaac Jurado</CreadoPor></item>
        ///<item><FecCrea>17/10/2012</FecCrea></item></list></remarks>
        public static DataTable ListadoPaginado(ADBE_Admision BE_pAdmision)
        {
            DataSet ds_Result = null;

            DataOperation dop_DataOperation = new DataOperation("WCO_ListarAdmisionServicio");
            Parameter[] prm_Params = new Parameter[13];
            prm_Params[0] = new Parameter("@pFechaFin", BE_pAdmision.FechaCreacion);
            prm_Params[1] = new Parameter("@pFechaIni", BE_pAdmision.FechaAdmision);
            prm_Params[2] = new Parameter("@pNroPeticion", BE_pAdmision.NroPeticion);
            prm_Params[3] = new Parameter("@pTipoAdmision", BE_pAdmision.PK.TipoOperacion.Pk.TipoAdmision);
            prm_Params[4] = new Parameter("@pHistoriaClinica", BE_pAdmision.HistoriaClinica);
            prm_Params[5] = new Parameter("@pEstado", BE_pAdmision.Estado);
            prm_Params[6] = new Parameter("@Persona", BE_pAdmision.PK.Persona.PK.IdPersona);
            prm_Params[7] = new Parameter("@UsuarioCreacion", BE_pAdmision.UsuarioCreacion);
            prm_Params[8] = new Parameter("@IdSede", BE_pAdmision.PK.Sede.PK.IdSede);
            prm_Params[9] = new Parameter("@OrdenAtencion", BE_pAdmision.OrdenAtencion);
            prm_Params[10] = new Parameter("@IdClasificadorMovimiento", BE_pAdmision.ClasificadorMovimiento);
            prm_Params[11] = new Parameter("@FlatCoaSeguro", BE_pAdmision.FlatCoaSeguro);
            prm_Params[12] = new Parameter("@FlatMovilidad", BE_pAdmision.FlatMovilidad);
            dop_DataOperation.Parameters.AddRange(prm_Params);

            ds_Result = DataManager.ExecuteDataSet(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation);
            if (ds_Result == null || ds_Result.Tables.Count == 0)
            {
                return null;
            }
            return ds_Result.Tables[0];
        }

        public static DataTable GroupBy(string i_sGroupByColumn, string i_sAggregateColumn, DataTable i_dSourceTable)
        {
            DataView dv = new DataView(i_dSourceTable);
            //getting distinct values for group column
            //DataTable dtGroup = dv.ToTable(true, new string[] { "IdProvedor" });
            DataTable dtGroup = dv.ToTable(true, new string[] { i_sGroupByColumn });
            //adding column for the row count
            dtGroup.Columns.Add("Count", typeof(int));
            //looping thru distinct values for the group, counting
            foreach (DataRow dr in dtGroup.Rows)
            {
                dr["Count"] = i_dSourceTable.Compute("Count(" + i_sAggregateColumn + ")", i_sGroupByColumn + " = '" + dr[i_sGroupByColumn] + "'");
            }
            //returning grouped/counted result
            return dtGroup;
        }

        public static ADBE_Admision TraerXCodigo(ADBE_Admision BE_pAdmision)
        {
            DataOperation dop_DataOperation = new DataOperation("WCO_TraerXAdmisionServicio");
            ADBE_Admision obj_vBEAdmision = null;
            Parameter[] prm_Params = new Parameter[1];
            prm_Params[0] = new Parameter("@pIdAdmision", BE_pAdmision.PK.IdAdmision);
            dop_DataOperation.Parameters.AddRange(prm_Params);
            using (IDataReader rdr = DataManager.ExecuteDataReader(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation))
            {
                while (rdr.Read())
                {
                    obj_vBEAdmision = new ADBE_Admision();
                    if (!object.Equals(rdr["IdAdmision"], System.DBNull.Value))
                        obj_vBEAdmision.PK.IdAdmision = rdr.GetInt32(rdr.GetOrdinal("IdAdmision"));
                    if (!object.Equals(rdr["UneuNegocioId"], System.DBNull.Value))
                        obj_vBEAdmision.UneuNegocioId = rdr.GetInt32(rdr.GetOrdinal("UneuNegocioId"));
                    if (!object.Equals(rdr["FechaAdmision"], System.DBNull.Value))
                        obj_vBEAdmision.FechaAdmision = rdr.GetDateTime(rdr.GetOrdinal("FechaAdmision"));
                    if (!object.Equals(rdr["HistoriaClinica"], System.DBNull.Value))
                        obj_vBEAdmision.HistoriaClinica = rdr.GetString(rdr.GetOrdinal("HistoriaClinica"));
                    if (!object.Equals(rdr["NroPeticion"], System.DBNull.Value))
                        obj_vBEAdmision.NroPeticion = rdr.GetString(rdr.GetOrdinal("NroPeticion"));
                    if (!object.Equals(rdr["OrdenAtencion"], System.DBNull.Value))
                        obj_vBEAdmision.OrdenAtencion = rdr.GetString(rdr.GetOrdinal("OrdenAtencion"));
                    if (!object.Equals(rdr["Especialidad"], System.DBNull.Value))
                        obj_vBEAdmision.IdEspecialidad = rdr.GetInt32(rdr.GetOrdinal("Especialidad"));
                    if (!object.Equals(rdr["TipoOrden"], System.DBNull.Value))
                        obj_vBEAdmision.TipoOrden = rdr.GetString(rdr.GetOrdinal("TipoOrden"));
                    if (!object.Equals(rdr["Cama"], System.DBNull.Value))
                        obj_vBEAdmision.Cama = rdr.GetString(rdr.GetOrdinal("Cama"));
                    if (!object.Equals(rdr["TipoAtencion"], System.DBNull.Value))
                        obj_vBEAdmision.TipoAtencion = rdr.GetInt32(rdr.GetOrdinal("TipoAtencion"));
                    if (!object.Equals(rdr["TipoOrdenAtencion"], System.DBNull.Value))
                        obj_vBEAdmision.TipoPacienteId = rdr.GetInt32(rdr.GetOrdinal("TipoOrdenAtencion"));
                    if (!object.Equals(rdr["IdAseguradora"], System.DBNull.Value))
                        obj_vBEAdmision.AseguradoraId = rdr.GetInt32(rdr.GetOrdinal("IdAseguradora"));
                    if (!object.Equals(rdr["IdEmpresaPaciente"], System.DBNull.Value))
                        obj_vBEAdmision.PK.Empresa.PK.IdPersona = rdr.GetInt32(rdr.GetOrdinal("IdEmpresaPaciente"));
                    if (!object.Equals(rdr["TipoOperacionId"], System.DBNull.Value))
                        obj_vBEAdmision.PK.TipoOperacion.Pk.TipoOperacionId = rdr.GetInt32(rdr.GetOrdinal("TipoOperacionId"));
                    if (!object.Equals(rdr["Persona"], System.DBNull.Value))
                        obj_vBEAdmision.PK.Persona.PK.IdPersona = rdr.GetInt32(rdr.GetOrdinal("Persona"));
                    if (!object.Equals(rdr["MedicoId"], System.DBNull.Value))
                        obj_vBEAdmision.MedicoId = rdr.GetInt32(rdr.GetOrdinal("MedicoId"));
                    if (!object.Equals(rdr["MedicoDescripcion"], System.DBNull.Value))
                        obj_vBEAdmision.MedicoDescripcion = rdr.GetString(rdr.GetOrdinal("MedicoDescripcion"));
                    if (!object.Equals(rdr["Estado"], System.DBNull.Value))
                        obj_vBEAdmision.Estado = rdr.GetInt32(rdr.GetOrdinal("Estado"));
                    if (!object.Equals(rdr["IdSede"], System.DBNull.Value))
                        obj_vBEAdmision.PK.Sede.PK.IdSede = rdr.GetInt32(rdr.GetOrdinal("IdSede"));
                    if (!object.Equals(rdr["ObservacionAlta"], System.DBNull.Value))
                        obj_vBEAdmision.Comentario = rdr.GetString(rdr.GetOrdinal("ObservacionAlta"));
                    if (!object.Equals(rdr["ClasificadorMovimiento"], System.DBNull.Value))
                        obj_vBEAdmision.ClasificadorMovimiento = rdr.GetString(rdr.GetOrdinal("ClasificadorMovimiento"));
                    if (!object.Equals(rdr["OrdenSinapa"], System.DBNull.Value))
                        obj_vBEAdmision.OrdenSinapa = rdr.GetString(rdr.GetOrdinal("OrdenSinapa"));

                    if (!object.Equals(rdr["CoaSeguro"], System.DBNull.Value))
                        obj_vBEAdmision.CoaSeguro = rdr.GetDecimal(rdr.GetOrdinal("CoaSeguro"));
                    if (!object.Equals(rdr["FechaCreacion"], System.DBNull.Value))
                        obj_vBEAdmision.FechaCreacion = rdr.GetDateTime(rdr.GetOrdinal("FechaCreacion"));
                    if (!object.Equals(rdr["FechaModificacion"], System.DBNull.Value))
                        obj_vBEAdmision.FechaModificacion = rdr.GetDateTime(rdr.GetOrdinal("FechaModificacion"));
                    if (!object.Equals(rdr["UsuarioCreacion"], System.DBNull.Value))
                        obj_vBEAdmision.UsuarioCreacion = rdr.GetString(rdr.GetOrdinal("UsuarioCreacion"));
                    if (!object.Equals(rdr["UsuarioModificacion"], System.DBNull.Value))
                        obj_vBEAdmision.UsuarioModificacion = rdr.GetString(rdr.GetOrdinal("UsuarioModificacion"));
                    if (!object.Equals(rdr["IpCreacion"], System.DBNull.Value))
                        obj_vBEAdmision.IpCreacion = rdr.GetString(rdr.GetOrdinal("IpCreacion"));
                    if (!object.Equals(rdr["IpModificacion"], System.DBNull.Value))
                        obj_vBEAdmision.IpModificacion = rdr.GetString(rdr.GetOrdinal("IpModificacion"));

                    if (!object.Equals(rdr["NombreCompleto"], System.DBNull.Value))
                        obj_vBEAdmision.PK.Persona.NombreCompleto = rdr.GetString(rdr.GetOrdinal("NombreCompleto"));
                    if (!object.Equals(rdr["fechanacimiento"], System.DBNull.Value))
                        obj_vBEAdmision.PK.Persona.PerFechaNacimiento = rdr.GetDateTime(rdr.GetOrdinal("fechanacimiento"));
                    if (!object.Equals(rdr["Documento"], System.DBNull.Value))
                        obj_vBEAdmision.PK.Persona.PerNumeroDocumento = rdr.GetString(rdr.GetOrdinal("Documento"));
                    if (!object.Equals(rdr["nombres"], System.DBNull.Value))
                        obj_vBEAdmision.PK.Persona.Nombres = rdr.GetString(rdr.GetOrdinal("nombres"));
                    if (!object.Equals(rdr["apellidopaterno"], System.DBNull.Value))
                        obj_vBEAdmision.PK.Persona.ApellidoPaterno = rdr.GetString(rdr.GetOrdinal("apellidopaterno"));
                    if (!object.Equals(rdr["apellidomaterno"], System.DBNull.Value))
                        obj_vBEAdmision.PK.Persona.ApellidoMaterno = rdr.GetString(rdr.GetOrdinal("apellidomaterno"));
                    if (!object.Equals(rdr["sexo"], System.DBNull.Value))
                        obj_vBEAdmision.PK.Persona.PerSexo = rdr.GetString(rdr.GetOrdinal("sexo"));

                    if (!object.Equals(rdr["FlatCoaSeguro"], System.DBNull.Value))
                        obj_vBEAdmision.FlatCoaSeguro = rdr.GetInt32(rdr.GetOrdinal("FlatCoaSeguro"));
                    if (!object.Equals(rdr["FlatMovilidad"], System.DBNull.Value))
                        obj_vBEAdmision.FlatMovilidad = rdr.GetInt32(rdr.GetOrdinal("FlatMovilidad"));
                    if (!object.Equals(rdr["Total"], System.DBNull.Value))
                        obj_vBEAdmision.Total = rdr.GetDecimal(rdr.GetOrdinal("Total"));
                    if (!object.Equals(rdr["Afecto"], System.DBNull.Value))
                        obj_vBEAdmision.Afecto = rdr.GetDecimal(rdr.GetOrdinal("Afecto"));
                    if (!object.Equals(rdr["Igv"], System.DBNull.Value))
                        obj_vBEAdmision.Igv = rdr.GetDecimal(rdr.GetOrdinal("Igv"));
                    if (!object.Equals(rdr["Anticipo"], System.DBNull.Value))
                        obj_vBEAdmision.Anticipo = rdr.GetDecimal(rdr.GetOrdinal("Anticipo"));
                    if (!object.Equals(rdr["Saldo"], System.DBNull.Value))
                        obj_vBEAdmision.Saldo = rdr.GetDecimal(rdr.GetOrdinal("Saldo"));
                    if (!object.Equals(rdr["Redondeo"], System.DBNull.Value))
                        obj_vBEAdmision.Redondeo = rdr.GetDecimal(rdr.GetOrdinal("Redondeo"));
                }
                rdr.Close();

                return obj_vBEAdmision;
            }
        }

        ///<summary>Actualizar el registro en la tabla PERSONAMAST.</summary>
        ///<param name="obj_pSA_Curriculo">Entidad de Negocio</param>
        ///<remarks><list type="bullet">
        ///<item><CreadoPor>Creado PorJordan Mateo Pizarro</CreadoPor></item>
        ///<item><FecCrea>17/10/2012</FecCrea></item></list></remarks>
        public static void InsertarUsuarioWeb(ADBE_PersonaMast BE_pPersona)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InsertarPersonaPassword");
            Parameter[] prm_Params = new Parameter[3];
            prm_Params[0] = new Parameter("@IdPersona", BE_pPersona.PK.IdPersona);
            prm_Params[1] = new Parameter("@PerNumeroDocumento", BE_pPersona.PerNumeroDocumento);
            prm_Params[2] = new Parameter("@ClasificadorMovimiento", BE_pPersona.SunatUbigeo);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }

        ///<item><Estado>valor 10 actualiza Admision para IP</Estado></item></list></remarks>
        public static void Inactivar(ADBE_Admision BE_pAdmision)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InactivarAdmisionControl");
            Parameter[] prm_Params = new Parameter[4];
            prm_Params[0] = new Parameter("@IdAdmision", BE_pAdmision.PK.IdAdmision);
            prm_Params[1] = new Parameter("@Estado", BE_pAdmision.Estado);
            prm_Params[2] = new Parameter("@UltimoUsuario", BE_pAdmision.UsuarioModificacion);
            prm_Params[3] = new Parameter("@IpModificacion", BE_pAdmision.IpModificacion);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }

        ///<summary>Actualizar el registro en la tabla PERSONAMAST.</summary>
        ///<param name="obj_pSA_Curriculo">Entidad de Negocio</param>
        ///<remarks><list type="bullet">
        ///<item><CreadoPor>Creado PorJordan Mateo Pizarro</CreadoPor></item>
        ///<item><FecCrea>17/10/2012</FecCrea></item></list></remarks>
        public static void ActualizarOrdenExterna(ADBE_Admision BE_pAdmision)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_ActualizarAdmisionServicioSinapa");
            Parameter[] prm_Params = new Parameter[2];
            prm_Params[0] = new Parameter("@IdAdmision", BE_pAdmision.PK.IdAdmision);
            prm_Params[1] = new Parameter("@OrdenSinapa", BE_pAdmision.OrdenSinapa);

            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }

        public static DataTable ListadoPaginadoRoe(ADBE_Admision obj_pBEAdmision)
        {
            DataSet ds_Result = null;
            DataOperation dop_DataOperation = new DataOperation("WCO_ListarAdmisionRoe");
            Parameter[] prm_Params = new Parameter[2];
            prm_Params[0] = new Parameter("@IdAdmision", obj_pBEAdmision.PK.IdAdmision);
            prm_Params[1] = new Parameter("@NroPeticion", obj_pBEAdmision.NroPeticion);
            dop_DataOperation.Parameters.AddRange(prm_Params);
            ds_Result = DataManager.ExecuteDataSet(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation);

            if (ds_Result == null || ds_Result.Tables.Count == 0)
            {
                return null;
            }
            return ds_Result.Tables[0];
        }

        public static DataTable ListadoAdmisionReferenciaGrupo(ADBE_Admision obj_pBEAdmision)
        {
            DataSet ds_Result = null;
            DataOperation dop_DataOperation = new DataOperation("WCO_ListarAdmisionReferenciaGrupo");
            Parameter[] prm_Params = new Parameter[1];
            prm_Params[0] = new Parameter("@IdAdmision", obj_pBEAdmision.PK.IdAdmision);
            dop_DataOperation.Parameters.AddRange(prm_Params);
            ds_Result = DataManager.ExecuteDataSet(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation);

            if (ds_Result == null || ds_Result.Tables.Count == 0)
            {
                return null;
            }
            return ds_Result.Tables[0];
        }

        public static DataTable ListadoAdmisionReferencia(ADBE_Admision obj_pBEAdmision)
        {
            DataSet ds_Result = null;
            DataOperation dop_DataOperation = new DataOperation("WCO_ListarAdmisionReferencia");
            Parameter[] prm_Params = new Parameter[1];
            prm_Params[0] = new Parameter("@IdAdmision", obj_pBEAdmision.PK.IdAdmision);
            dop_DataOperation.Parameters.AddRange(prm_Params);
            ds_Result = DataManager.ExecuteDataSet(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation);

            if (ds_Result == null || ds_Result.Tables.Count == 0)
            {
                return null;
            }
            return ds_Result.Tables[0];
        }

        public static int InsertarProcesoReferencia(List<ADBE_ReferenciaDetalle> listaDetalle)
        {
            try
            {
                int valor = 0;
                ADBE_Referencia objReferencia = new ADBE_Referencia();
                foreach (ADBE_ReferenciaDetalle objEntity in listaDetalle)
                {
                    objReferencia = objEntity.PK.Referencia;
                }
                valor = InsertarReferencia(objReferencia);
                foreach (ADBE_ReferenciaDetalle objEntityDetalle in listaDetalle)
                {
                    objEntityDetalle.PK.Referencia.PK.IdReferencia = valor;
                    InsertarReferenciaDetalle(objEntityDetalle);
                }
                return valor;
            }
            catch (ApplicationException ex)
            {
                //scope.Dispose();
                return 0;
            }
        }

        public static int InsertarReferencia(ADBE_Referencia objBEReferencia)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InsertarReferencia");
            Parameter[] prm_Params = new Parameter[13];
            prm_Params[0] = new Parameter("@IdReferencia", DbType.Int32);
            prm_Params[1] = new Parameter("@IdAdmision", objBEReferencia.PK.Admision.PK.IdAdmision);
            prm_Params[2] = new Parameter("@NroAtencion", objBEReferencia.PK.Admision.NroPeticion);
            prm_Params[3] = new Parameter("@CodigoOrden", objBEReferencia.CodigoOrden);
            prm_Params[4] = new Parameter("@CodigoIngreso", objBEReferencia.CodigoIngreso);
            prm_Params[5] = new Parameter("@Comentario", objBEReferencia.Comentario);
            prm_Params[6] = new Parameter("@FechaRecepcionOrden", objBEReferencia.FechaRecepcionOrden);
            prm_Params[7] = new Parameter("@HoraRecepcionOrden", objBEReferencia.HoraRecepcionOrden);
            prm_Params[8] = new Parameter("@CodigoExcepcion", objBEReferencia.CodigoExcepcion);
            prm_Params[9] = new Parameter("@Estado", 1);
            prm_Params[10] = new Parameter("@UsuarioCreacion", objBEReferencia.PK.Admision.UsuarioCreacion);
            prm_Params[11] = new Parameter("@IpCreacion", objBEReferencia.PK.Admision.IpCreacion);
            prm_Params[12] = new Parameter("@IdSede", objBEReferencia.PK.Admision.PK.Sede.PK.IdSede);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            return int.Parse(dop_Operacion.GetParameterByName("@IdReferencia").Value.ToString());
        }

        public static int InsertarReferenciaDetalle(ADBE_ReferenciaDetalle objBEReferenciaDetalle)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InsertarReferenciaDetalle");
            Parameter[] prm_Params = new Parameter[8];
            prm_Params[0] = new Parameter("@IdReferencia", objBEReferenciaDetalle.PK.Referencia.PK.IdReferencia);
            prm_Params[1] = new Parameter("@IdSecuencia", DbType.Int32);
            prm_Params[2] = new Parameter("@CodigoBarra", objBEReferenciaDetalle.CodigoBarra);
            prm_Params[3] = new Parameter("@Identificador", objBEReferenciaDetalle.Identificador);
            prm_Params[4] = new Parameter("@Nombre", objBEReferenciaDetalle.Nombre);
            prm_Params[5] = new Parameter("@CodigoArea", objBEReferenciaDetalle.CodigoArea);
            prm_Params[6] = new Parameter("@CodigoRes", objBEReferenciaDetalle.CodigoRes);
            prm_Params[7] = new Parameter("@Activo", objBEReferenciaDetalle.Activo);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            return int.Parse(dop_Operacion.GetParameterByName("@IdSecuencia").Value.ToString());
        }

        public static int InsertarComponente(List<ADBE_ReferenciaComponente> listaDetComponente)
        {
            try
            {
                int valor = 0;
                foreach (ADBE_ReferenciaComponente objEntityDet in listaDetComponente)
                {
                    valor = InsertarReferenciaComponente(objEntityDet);
                }
                return valor;
            }
            catch (Exception ex)
            {
                //scope.Dispose();
                return 0;
            }
            //catch (ApplicationException ex)
            //{
            //    //scope.Dispose();
            //    return 0;
            //}
        }

        public static int InsertarReferenciaComponente(ADBE_ReferenciaComponente objBEReferenciaDetComponente)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InsertarReferenciaComponente");
            Parameter[] prm_Params = new Parameter[17];
            prm_Params[0] = new Parameter("@IdAdmision", objBEReferenciaDetComponente.PK.Admision.PK.IdAdmision);
            prm_Params[1] = new Parameter("@IdSecuencia", DbType.Int32);
            prm_Params[2] = new Parameter("@IdProveedorReferencia", objBEReferenciaDetComponente.IdProveedorReferencia);
            prm_Params[3] = new Parameter("@NroPeticion", objBEReferenciaDetComponente.PK.Admision.NroPeticion);
            prm_Params[4] = new Parameter("@FecRegistro", objBEReferenciaDetComponente.PK.Admision.FechaAdmision);
            prm_Params[5] = new Parameter("@IdPersona", objBEReferenciaDetComponente.PK.Admision.PK.Persona.PK.IdPersona);
            prm_Params[6] = new Parameter("@TipoAtencion", objBEReferenciaDetComponente.PK.Admision.TipoAtencion);
            prm_Params[7] = new Parameter("@TipoOrden", objBEReferenciaDetComponente.PK.Admision.TipoOrden);
            prm_Params[8] = new Parameter("@CodigoComponente", objBEReferenciaDetComponente.CodigoComponente);
            prm_Params[9] = new Parameter("@Des", objBEReferenciaDetComponente.Des);
            prm_Params[10] = new Parameter("@CodigoHomologacion", objBEReferenciaDetComponente.CodigoHomologacion);
            prm_Params[11] = new Parameter("@CodigoExterno", objBEReferenciaDetComponente.CodigoExterno);
            prm_Params[12] = new Parameter("@IdSede", objBEReferenciaDetComponente.PK.Admision.PK.Sede.PK.IdSede);
            prm_Params[13] = new Parameter("@IdSedeReferencia", objBEReferenciaDetComponente.IdSedeReferencia);
            prm_Params[14] = new Parameter("@ClasificadorMovimiento", objBEReferenciaDetComponente.ClasificadorMovimiento);
            prm_Params[15] = new Parameter("@UsuarioCreacion", objBEReferenciaDetComponente.UsuarioCreacion);
            prm_Params[16] = new Parameter("@Estado", objBEReferenciaDetComponente.Estado);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            return int.Parse(dop_Operacion.GetParameterByName("@IdSecuencia").Value.ToString());
        }

        public static void EliminarReferenciaComponente(ADBE_ReferenciaComponente objBEReferenciaDetComponente)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_EliminarReferenciaComponente");
            Parameter[] prm_Params = new Parameter[1];
            prm_Params[0] = new Parameter("@IdAdmision", objBEReferenciaDetComponente.PK.Admision.PK.IdAdmision);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }

        public static int InsertarProcesoPruebaReferencia(List<ADBE_PruebaReferenciaDetalle> listaDetalle)
        {
            //using (TransactionScope scope = new TransactionScope())
            //{
            try
            {
                int valor = 0;
                ADBE_PruebaReferencia objReferencia = new ADBE_PruebaReferencia();
                foreach (ADBE_PruebaReferenciaDetalle objEntity in listaDetalle)
                {
                    objReferencia = objEntity.PK.Referencia;
                }
                valor = InsertarPruebaReferencia(objReferencia);
                foreach (ADBE_PruebaReferenciaDetalle objEntityDetalle in listaDetalle)
                {
                    objEntityDetalle.PK.Referencia.PK.IdPruebaReferencia = valor;
                    InsertarPruebaReferenciaDetalle(objEntityDetalle);
                }
                //scope.Complete();
                //scope.Dispose();
                //MessageBox.Show("Transaction Exception Occured");
                return valor;
            }
            catch (Exception ex)
            {
                //scope.Dispose();
                return 0;
            }
            //catch (ApplicationException ex)
            //{
            //    //scope.Dispose();
            //    return 0;
            //}
            //}
        }

        public static int InsertarPruebaReferencia(ADBE_PruebaReferencia objBEReferencia)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InsertarPruebaReferencia");
            Parameter[] prm_Params = new Parameter[12];
            prm_Params[0] = new Parameter("@Id", DbType.Int32);
            prm_Params[1] = new Parameter("@IdAdmision", objBEReferencia.PK.Admision.PK.IdAdmision);
            prm_Params[2] = new Parameter("@Idsede", objBEReferencia.Idsede);
            prm_Params[3] = new Parameter("@IdSedeReferencia", objBEReferencia.IdSedeReferencia);
            prm_Params[4] = new Parameter("@Prioridad", objBEReferencia.Prioridad);
            prm_Params[5] = new Parameter("@PrioridadReferencia", objBEReferencia.PrioridadReferencia);
            prm_Params[6] = new Parameter("@TipoAtencion", objBEReferencia.TipoAtencion);
            prm_Params[7] = new Parameter("@TipoAtencionReferencia", objBEReferencia.TipoAtencionReferencia);
            prm_Params[8] = new Parameter("@TipoContratoID", objBEReferencia.TipoContratoID);
            prm_Params[9] = new Parameter("@TipoContratoReferencia", objBEReferencia.TipoContratoReferencia);
            prm_Params[10] = new Parameter("@Estado", 1);
            prm_Params[11] = new Parameter("@UsuarioCreacion", objBEReferencia.UsuarioCreacion);

            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            return int.Parse(dop_Operacion.GetParameterByName("@Id").Value.ToString());
        }

        public static int InsertarPruebaReferenciaDetalle(ADBE_PruebaReferenciaDetalle objBEReferenciaDetalle)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InsertarPruebaReferenciaDetalle");
            Parameter[] prm_Params = new Parameter[8];
            prm_Params[0] = new Parameter("@IdPruebaReferencia", objBEReferenciaDetalle.PK.Referencia.PK.IdPruebaReferencia);
            prm_Params[1] = new Parameter("@Secuencia", DbType.Int32);
            prm_Params[2] = new Parameter("@IdAdmisionReferencia", objBEReferenciaDetalle.IdAdmisionReferencia);
            prm_Params[3] = new Parameter("@IdProvedorReferencia", objBEReferenciaDetalle.IdProvedorReferencia);
            prm_Params[4] = new Parameter("@CodigoComponente", objBEReferenciaDetalle.CodigoComponente);
            prm_Params[5] = new Parameter("@CodigoComponenteReferencia", objBEReferenciaDetalle.CodigoComponenteReferencia);
            prm_Params[6] = new Parameter("@Estado", 1);
            prm_Params[7] = new Parameter("@UsuarioCreacion", objBEReferenciaDetalle.UsuarioCreacion);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            return int.Parse(dop_Operacion.GetParameterByName("@Secuencia").Value.ToString());
        }

  
    }
}