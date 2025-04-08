using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using RoyalSystems.Data;
using System.IO;
using System.Transactions;
using WebApiServices.Models.Entidades;

namespace WebApiServices.Models.Datos
{
    public class ADDAT_TipoOperacion
    {

        public List<WCO_ListarTipoOperacion_Result> ListadoPaginado(WCO_ListarTipoOperacion_Result ObjEntidad)
        {
            List<WCO_ListarTipoOperacion_Result> lst = new List<WCO_ListarTipoOperacion_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarTipoOperacion(ObjEntidad.UneuNegocioId, ObjEntidad.TipoOperacionID, ObjEntidad.Persona, ObjEntidad.TIPOADMISIONID, 
                    ObjEntidad.TipEstado, ObjEntidad.TipoPacienteId, ObjEntidad.IdSede, ObjEntidad.IdSedeCliente, ObjEntidad.DesSedeCliente).ToList();
            }
            return lst;
        }

        public List<WCO_ListarTipoOperacionCliente_Result> ListarTipoOperacionCliente(WCO_ListarTipoOperacion_Result ObjEntidad)
        {
            List<WCO_ListarTipoOperacionCliente_Result> lst = new List<WCO_ListarTipoOperacionCliente_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarTipoOperacionCliente(ObjEntidad.UneuNegocioId, ObjEntidad.Persona, ObjEntidad.TIPOADMISIONID,
                    ObjEntidad.TipEstado, ObjEntidad.Nombre, ObjEntidad.Codigo, ObjEntidad.IdSede, ObjEntidad.DesSedeCliente).ToList();
            }
            return lst;
        }

        public List<WCO_ListarTipoOperacionClienteTipoPaciente_Result> ListarTipoOperacionTipoPaciente(WCO_ListarTipoOperacion_Result ObjEntidad)
        {
            List<WCO_ListarTipoOperacionClienteTipoPaciente_Result> lst = new List<WCO_ListarTipoOperacionClienteTipoPaciente_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarTipoOperacionClienteTipoPaciente(ObjEntidad.UneuNegocioId, ObjEntidad.Persona, ObjEntidad.TIPOADMISIONID,
                    ObjEntidad.TipEstado).ToList();
            }
            return lst;
        }

        public List<WCO_ListarTipoOperacionClienteSede_Result> ListarTipoOperacionClienteSede(WCO_ListarTipoOperacion_Result ObjEntidad)
        {
            List<WCO_ListarTipoOperacionClienteSede_Result> lst = new List<WCO_ListarTipoOperacionClienteSede_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarTipoOperacionClienteSede(ObjEntidad.UneuNegocioId, ObjEntidad.Persona, ObjEntidad.TIPOADMISIONID,
                    ObjEntidad.TipEstado).ToList();
            }
            return lst;
        }



        public List<WCO_TraerTipoOperacionxCodigo_Result> TipoOperacionxCodigo(WCO_TraerTipoOperacionxCodigo_Result ObjEntidad)
        {
            List<WCO_TraerTipoOperacionxCodigo_Result> lst = new List<WCO_TraerTipoOperacionxCodigo_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_TraerTipoOperacionxCodigo(ObjEntidad.UneuNegocioId,  ObjEntidad.TIPOADMISIONID,ObjEntidad.Persona, 
                    ObjEntidad.TipEstado, ObjEntidad.TipoPacienteId, ObjEntidad.IdSede).ToList();
            }
            return lst;
        }
      
        public WCO_TraerTipoOperacionxId_Result TraerTipoOperacionxId(WCO_ListarTipoOperacion_Result ObjEntidad)
        {
            List<WCO_TraerTipoOperacionxId_Result> lst = new List<WCO_TraerTipoOperacionxId_Result>();
            WCO_TraerTipoOperacionxId_Result objResul = new WCO_TraerTipoOperacionxId_Result();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_TraerTipoOperacionxId(ObjEntidad.TipoOperacionID).ToList();
                foreach (WCO_TraerTipoOperacionxId_Result obj_Seleccionados in lst)
                {
                    objResul = obj_Seleccionados;

                }
            }
            return objResul;
        }

        public ADBE_TipoOperacion ObtenerxIdTipoOperacion(ADBE_TipoOperacion objBETipo)
        {
            DataOperation dop_DataOperation = new DataOperation("WCO_TraerTipoOperacionxId");
            ADBE_TipoOperacion objBETipoOperacion = new ADBE_TipoOperacion();
            Parameter[] prm_Params = new Parameter[1];
            prm_Params[0] = new Parameter("@TipoOperacionId", objBETipo.Pk.TipoOperacionId);
            dop_DataOperation.Parameters.AddRange(prm_Params);

            using (IDataReader rdr = DataManager.ExecuteDataReader(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation))
            {
                while (rdr.Read())
                {
                    objBETipoOperacion = new ADBE_TipoOperacion();

                    if (!object.Equals(rdr["UneuNegocioId"], System.DBNull.Value))
                        objBETipoOperacion.Pk.UnidadNegocio = rdr.GetInt32(rdr.GetOrdinal("UneuNegocioId"));
                    /**/
                    if (!object.Equals(rdr["IdListaBase"], System.DBNull.Value))
                        objBETipoOperacion.IdlistaBase = rdr.GetInt32(rdr.GetOrdinal("IdListaBase"));
                    if (!object.Equals(rdr["TPAplicaFactor"], System.DBNull.Value))
                        objBETipoOperacion.TPAplicaFactor = rdr.GetInt32(rdr.GetOrdinal("TPAplicaFactor"));
                    if (!object.Equals(rdr["TPFactor"], System.DBNull.Value))
                        objBETipoOperacion.Factor = rdr.GetDecimal(rdr.GetOrdinal("Tpfactor"));
                    /**/
                    if (!object.Equals(rdr["IdSede"], System.DBNull.Value))
                        objBETipoOperacion.Pk.Sede.PK.IdSede = rdr.GetInt32(rdr.GetOrdinal("IdSede"));
                    if (!object.Equals(rdr["TipoOperacionId"], System.DBNull.Value))
                        objBETipoOperacion.Pk.TipoOperacionId = rdr.GetInt32(rdr.GetOrdinal("TipoOperacionId"));
                    if (!object.Equals(rdr["Persona"], System.DBNull.Value))
                        objBETipoOperacion.Pk.Persona.PK.IdPersona = rdr.GetInt32(rdr.GetOrdinal("Persona"));
                    if (!object.Equals(rdr["NombreCompleto"], System.DBNull.Value))
                        objBETipoOperacion.Pk.Persona.NombreCompleto = rdr.GetString(rdr.GetOrdinal("NombreCompleto"));
                    if (!object.Equals(rdr["Documento"], System.DBNull.Value))
                        objBETipoOperacion.Pk.Persona.PerNumeroDocumento = rdr.GetString(rdr.GetOrdinal("Documento"));
                    if (!object.Equals(rdr["TipoAdmisionId"], System.DBNull.Value))
                        objBETipoOperacion.Pk.TipoAdmision = rdr.GetInt32(rdr.GetOrdinal("TipoAdmisionId"));
                    if (!object.Equals(rdr["TipoPacienteId"], System.DBNull.Value))
                        objBETipoOperacion.Pk.TipoPaciente = rdr.GetInt32(rdr.GetOrdinal("TipoPacienteId"));
                    //if (!object.Equals(rdr["AdmDescripcion"], System.DBNull.Value))
                    //    objBETipoOperacion.Pk.TipoAdmision.AdmDescripcion = rdr.GetString(rdr.GetOrdinal("AdmDescripcion"));
                    if (!object.Equals(rdr["TPContratoId"], System.DBNull.Value))
                        objBETipoOperacion.TpContratoId = rdr.GetInt32(rdr.GetOrdinal("TPContratoId"));
                    if (!object.Equals(rdr["CodigoContrato"], System.DBNull.Value))
                        objBETipoOperacion.CodigoContrato = rdr.GetString(rdr.GetOrdinal("CodigoContrato"));
                    if (!object.Equals(rdr["TipEstado"], System.DBNull.Value))
                        objBETipoOperacion.TipEstado = rdr.GetInt32(rdr.GetOrdinal("TipEstado"));
                    if (!object.Equals(rdr["FechaInicio"], System.DBNull.Value))
                        objBETipoOperacion.Fechinicio = rdr.GetDateTime(rdr.GetOrdinal("FechaInicio"));
                    if (!object.Equals(rdr["FechaTermino"], System.DBNull.Value))
                        objBETipoOperacion.FechTermino = rdr.GetDateTime(rdr.GetOrdinal("FechaTermino"));
                    if (!object.Equals(rdr["Moneda"], System.DBNull.Value))
                        objBETipoOperacion.Moneda = rdr.GetString(rdr.GetOrdinal("Moneda"));
                    if (!object.Equals(rdr["MontoEmpresa"], System.DBNull.Value))
                        objBETipoOperacion.MonedaEmpresa = rdr.GetDecimal(rdr.GetOrdinal("MontoEmpresa"));
                    if (!object.Equals(rdr["IdPolizaPlan"], System.DBNull.Value))
                        objBETipoOperacion.IdPolizaPlan = rdr.GetInt32(rdr.GetOrdinal("IdPolizaPlan"));
                    if (!object.Equals(rdr["AplicaFormula"], System.DBNull.Value))
                        objBETipoOperacion.AplicaFormula = rdr.GetInt32(rdr.GetOrdinal("AplicaFormula"));
                    if (!object.Equals(rdr["FlaCon"], System.DBNull.Value))
                        objBETipoOperacion.FlaCon = rdr.GetInt32(rdr.GetOrdinal("FlaCon"));
                    if (!object.Equals(rdr["FlatAprobacion"], System.DBNull.Value))
                        objBETipoOperacion.FlatAprobacion = rdr.GetInt32(rdr.GetOrdinal("FlatAprobacion"));
                    if (!object.Equals(rdr["Observacion"], System.DBNull.Value))
                        objBETipoOperacion.Observacion = rdr.GetString(rdr.GetOrdinal("Observacion"));
                    if (!object.Equals(rdr["FlagRedondeo"], System.DBNull.Value))
                        objBETipoOperacion.FlagRedondeo = rdr.GetInt32(rdr.GetOrdinal("FlagRedondeo"));

                    if (!object.Equals(rdr["IdSedeCliente"], System.DBNull.Value))
                        objBETipoOperacion.Pk.SedeCliente = rdr.GetInt32(rdr.GetOrdinal("IdSedeCliente"));

                    if (!object.Equals(rdr["FlagArchivo"], System.DBNull.Value))
                        objBETipoOperacion.FlagArchivo = rdr.GetInt32(rdr.GetOrdinal("FlagArchivo"));
                    if (!object.Equals(rdr["RutaArchivo"], System.DBNull.Value))
                        objBETipoOperacion.RutaArchivo = rdr.GetString(rdr.GetOrdinal("RutaArchivo"));
                    if (!object.Equals(rdr["FlagAdelanto"], System.DBNull.Value))
                        objBETipoOperacion.FlagAdelanto = rdr.GetInt32(rdr.GetOrdinal("FlagAdelanto"));
                    if (!object.Equals(rdr["FlagCortesia"], System.DBNull.Value))
                        objBETipoOperacion.FlagCortesia = rdr.GetInt32(rdr.GetOrdinal("FlagCortesia"));
                    if (!object.Equals(rdr["MontoMinimo"], System.DBNull.Value))
                        objBETipoOperacion.MontoMinimo = rdr.GetDecimal(rdr.GetOrdinal("MontoMinimo"));

                    if (!object.Equals(rdr["UsuarioCreacion"], System.DBNull.Value))
                        objBETipoOperacion.UsuarioCreacion = rdr.GetString(rdr.GetOrdinal("UsuarioCreacion"));
                    if (!object.Equals(rdr["FechaCreacion"], System.DBNull.Value))
                        objBETipoOperacion.FechaCreacion = rdr.GetDateTime(rdr.GetOrdinal("FechaCreacion"));
                    if (!object.Equals(rdr["UsuarioModificacion"], System.DBNull.Value))
                        objBETipoOperacion.UsuarioModificacion = rdr.GetString(rdr.GetOrdinal("UsuarioModificacion"));
                    if (!object.Equals(rdr["FechaModificacion"], System.DBNull.Value))
                        objBETipoOperacion.FechaModificacion = rdr.GetDateTime(rdr.GetOrdinal("FechaModificacion"));
                    if (!object.Equals(rdr["IpCreacion"], System.DBNull.Value))
                        objBETipoOperacion.IpCreacion = rdr.GetString(rdr.GetOrdinal("IpCreacion"));
                    if (!object.Equals(rdr["TipEstado"], System.DBNull.Value))
                        objBETipoOperacion.TipEstado = rdr.GetInt32(rdr.GetOrdinal("TipEstado"));
                    if (!object.Equals(rdr["IpModificacion"], System.DBNull.Value))
                        objBETipoOperacion.IpModificacion = rdr.GetString(rdr.GetOrdinal("IpModificacion"));
                }
                rdr.Close();

                return objBETipoOperacion;
            }
        }

        public int Insertar(WCO_ListarTipoOperacion_Result objBETipoOperacion)
        {
            int valor = 0;
            if (!ValidarExisteDescTipoOperacion(objBETipoOperacion))
            {
                valor = -2;
                return valor;
            }

            DataOperation dop_Operacion = new DataOperation("WCO_InsertarTipoOperacion");
            Parameter[] prm_Params = new Parameter[28];
            prm_Params[0] = new Parameter("@UneuNegocioId", objBETipoOperacion.UneuNegocioId);
            /**/
            prm_Params[1] = new Parameter("@IdlistaBase", objBETipoOperacion.IdListaBase);
            prm_Params[2] = new Parameter("@tpAplicaFactor", objBETipoOperacion.TPAplicaFactor);
            prm_Params[3] = new Parameter("@Factor", objBETipoOperacion.TPAplicaFactor);
            /**/
            prm_Params[4] = new Parameter("@FechaInicio", objBETipoOperacion.FechaInicio);
            prm_Params[5] = new Parameter("@FechaTermino", objBETipoOperacion.FechaTermino);
            prm_Params[6] = new Parameter("@Moneda", objBETipoOperacion.Moneda);
            prm_Params[7] = new Parameter("@TipoAdmisionId", objBETipoOperacion.TIPOADMISIONID);
            prm_Params[8] = new Parameter("@IdPersona", objBETipoOperacion.Persona);
            prm_Params[9] = new Parameter("@CodigoContrato", objBETipoOperacion.CodigoContrato);
            prm_Params[10] = new Parameter("@TipEstado", objBETipoOperacion.TipEstado);
            prm_Params[11] = new Parameter("@UsuarioCreacion", objBETipoOperacion.UsuarioCreacion);
            prm_Params[12] = new Parameter("@FechaCreacion", objBETipoOperacion.FechaCreacion);
            prm_Params[13] = new Parameter("@IpCreacion", objBETipoOperacion.IpCreacion);
            prm_Params[14] = new Parameter("@MontoEmpresa", objBETipoOperacion.Moneda);
            prm_Params[15] = new Parameter("@TipoOperacionId", DbType.Int32);
            prm_Params[16] = new Parameter("@AplicaFormula", objBETipoOperacion.AplicaFormula);
            prm_Params[17] = new Parameter("@TipoPacienteId", objBETipoOperacion.TipoPacienteId);
            prm_Params[18] = new Parameter("@IdSede", objBETipoOperacion.IdSede);
            prm_Params[19] = new Parameter("@Observacion", objBETipoOperacion.Observacion);
            prm_Params[20] = new Parameter("@FlatAprobacion", objBETipoOperacion.FlatAprobacion);
            prm_Params[21] = new Parameter("@FlaCon", objBETipoOperacion.FlaCon);
            prm_Params[22] = new Parameter("@flagRedondeo", objBETipoOperacion.FlagRedondeo);
            prm_Params[23] = new Parameter("@IdSedeCliente", objBETipoOperacion.IdSedeCliente);

            prm_Params[24] = new Parameter("@FlagAdelanto", objBETipoOperacion.FlagAdelanto);
            prm_Params[25] = new Parameter("@FlagCortesia", objBETipoOperacion.FlagCortesia);
            prm_Params[26] = new Parameter("@FlagArchivo", objBETipoOperacion.FlagArchivo);
            prm_Params[27] = new Parameter("@MontoMinimo", objBETipoOperacion.MontoMinimo);

            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            return int.Parse(dop_Operacion.GetParameterByName("@TipoOperacionId").Value.ToString());
        }

        public int Actualizar(WCO_ListarTipoOperacion_Result objBETipoOperacion)
        {
            int valor = 0;
            if (!ValidarExisteDescTipoOperacion(objBETipoOperacion))
            {
                valor = -2;
                return valor;
            }
            DataOperation dop_Operacion = new DataOperation("WCO_ActualizarTipoOperacion");
            Parameter[] prm_Params = new Parameter[29];
            prm_Params[0] = new Parameter("@UneuNegocioId", objBETipoOperacion.UneuNegocioId);
            /**/
            prm_Params[1] = new Parameter("@IdlistaBase", objBETipoOperacion.IdListaBase);
            prm_Params[2] = new Parameter("@tpAplicaFactor", objBETipoOperacion.TPAplicaFactor);
            prm_Params[3] = new Parameter("@Factor", objBETipoOperacion.TPAplicaFactor);
            /**/
            prm_Params[4] = new Parameter("@TipoAdmisionId", objBETipoOperacion.TIPOADMISIONID);
            prm_Params[5] = new Parameter("@IdPersona", objBETipoOperacion.Persona);
            prm_Params[6] = new Parameter("@TpContratoId", objBETipoOperacion.TPContratoID);
            prm_Params[7] = new Parameter("@TipEstado", objBETipoOperacion.TipEstado);
            prm_Params[8] = new Parameter("@UsuarioModificacion", objBETipoOperacion.UsuarioModificacion);
            prm_Params[9] = new Parameter("@FechaModificacion", objBETipoOperacion.FechaModificacion);
            prm_Params[10] = new Parameter("@IpModificacion", objBETipoOperacion.IpCreacion);
            prm_Params[11] = new Parameter("@TipoOperacionId", objBETipoOperacion.TipoOperacionID);
            prm_Params[12] = new Parameter("@FechaInicio", objBETipoOperacion.FechaInicio);
            prm_Params[13] = new Parameter("@FechaTermino", objBETipoOperacion.FechaTermino);
            prm_Params[14] = new Parameter("@Moneda", objBETipoOperacion.Moneda);
            prm_Params[15] = new Parameter("@MontoEmpresa", objBETipoOperacion.Moneda);
            prm_Params[16] = new Parameter("@CodigoContrato", objBETipoOperacion.CodigoContrato);
            prm_Params[17] = new Parameter("@AplicaFormula", objBETipoOperacion.AplicaFormula);
            prm_Params[18] = new Parameter("@TipoPacienteId", objBETipoOperacion.TipoPacienteId);
            prm_Params[19] = new Parameter("@IdSede", objBETipoOperacion.IdSede);
            prm_Params[20] = new Parameter("@Observacion", objBETipoOperacion.Observacion);
            prm_Params[21] = new Parameter("@FlatAprobacion", objBETipoOperacion.FlatAprobacion);
            prm_Params[22] = new Parameter("@FlaCon", objBETipoOperacion.FlaCon);
            prm_Params[23] = new Parameter("@flagRedondeo", objBETipoOperacion.FlagRedondeo);
            prm_Params[24] = new Parameter("@IdSedeCliente", objBETipoOperacion.IdSedeCliente);

            prm_Params[25] = new Parameter("@FlagAdelanto", objBETipoOperacion.FlagAdelanto);
            prm_Params[26] = new Parameter("@FlagCortesia", objBETipoOperacion.FlagCortesia);
            prm_Params[27] = new Parameter("@FlagArchivo", objBETipoOperacion.FlagArchivo);
            prm_Params[28] = new Parameter("@MontoMinimo", objBETipoOperacion.MontoMinimo);

            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);

            valor = objBETipoOperacion.TipoOperacionID;
            return valor;
        }

        public void Inactivar(WCO_ListarTipoOperacion_Result objBETipoOperacion)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InactivarTipoOperacion");
            Parameter[] prm_Params = new Parameter[3];
            prm_Params[0] = new Parameter("@TipoOperacionId", objBETipoOperacion.TipoOperacionID);
            prm_Params[1] = new Parameter("@UsuarioModificacion", objBETipoOperacion.UsuarioCreacion);
            prm_Params[2] = new Parameter("@IpModificacion", objBETipoOperacion.IpCreacion);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }

        public static bool ValidarExisteDescTipoOperacion(WCO_ListarTipoOperacion_Result objBETipoOperacion)
        {
            DataOperation dop_DataOperation = new DataOperation("WCO_EXISTEDescTipoOperacion");
            Parameter[] prm_Params = new Parameter[8];
            prm_Params[0] = new Parameter("@TipoOperacionId", objBETipoOperacion.TipoOperacionID);
            prm_Params[1] = new Parameter("@TipoAdmisionId", objBETipoOperacion.TIPOADMISIONID);
            prm_Params[2] = new Parameter("@IdPersona", objBETipoOperacion.Persona);
            prm_Params[3] = new Parameter("@TipoPacienteId", objBETipoOperacion.TipoPacienteId);
            prm_Params[4] = new Parameter("@CodigoContrato", objBETipoOperacion.CodigoContrato);
            prm_Params[5] = new Parameter("@IdSede", objBETipoOperacion.IdSede);
            prm_Params[6] = new Parameter("@UneuNegocioId", objBETipoOperacion.UneuNegocioId);
            prm_Params[7] = new Parameter("@flagSalida", DbType.Int32);
            dop_DataOperation.Parameters.AddRange(prm_Params);

            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation);
            if (Convert.ToInt32(dop_DataOperation.GetParameterByName("@flagSalida").Value) >= 1)
            {
                return false;
            }
            return true;
        }


    }
}