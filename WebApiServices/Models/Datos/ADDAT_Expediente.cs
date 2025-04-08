using RoyalSystems.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiServices.Models.Datos
{
    public class ADDAT_Expediente
    {
        public List<WCO_ListarExpedienteParticulares_Result> ListarExpedienteParticulares(WCO_ListarExpedienteParticulares_Result ObjEntidad)
        {
            List<WCO_ListarExpedienteParticulares_Result> lst = new List<WCO_ListarExpedienteParticulares_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarExpedienteParticulares(ObjEntidad.UneuNegocioId, ObjEntidad.IdExpediente, ObjEntidad.NroPeticion, 
                      ObjEntidad.ClasificadorMovimiento, ObjEntidad.TipoExpediente, ObjEntidad.TipoAdmisionId, ObjEntidad.Persona,  
                      ObjEntidad.OrdenAtencion, ObjEntidad.HistoriaClinica,ObjEntidad.IdSede, ObjEntidad.FechaInicio, ObjEntidad.FechaFinal,
                      ObjEntidad.Estado, ObjEntidad.EstadoAdm).ToList();
            }
            return lst;
        }

        public List<WCO_ListarExpediente_Result> ListarExpediente(WCO_ListarExpediente_Result ObjEntidad)
        {
            List<WCO_ListarExpediente_Result> lst = new List<WCO_ListarExpediente_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarExpediente(ObjEntidad.UneuNegocioId, ObjEntidad.IdExpediente, ObjEntidad.CodigoExpediente, ObjEntidad.ClasificadorMovimiento,
                    ObjEntidad.TipoExpediente, ObjEntidad.FechaInicio, ObjEntidad.FechaFinal, ObjEntidad.Estado, ObjEntidad.TipoAdmisionId,
                    ObjEntidad.IdClienteFacturacion, ObjEntidad.IdContrato, ObjEntidad.Descripcion).ToList();
            }
            return lst;
        }

        public List<WCO_ListarExpedienteDetalle_Result> ListarExpedienteDetalle(WCO_ListarExpedienteDetalle_Result ObjEntidad)
        {
            List<WCO_ListarExpedienteDetalle_Result> lst = new List<WCO_ListarExpedienteDetalle_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarExpedienteDetalle(ObjEntidad.IdExpediente, ObjEntidad.IdSecuencia, ObjEntidad.Estado, ObjEntidad.Periodo, ObjEntidad.NroPeticion, ObjEntidad.CodigoComponente).ToList();
            }
            return lst;
        }


        ///<summary>Método para inserta el registro en la tabla Expediente.</summary>
        ///<param name="obj_Expediente">Entidad de Negocio</param>
        ///<returns>Retorna el BE_Expediente.</returns>
        ///<remarks><list type="bullet">
        ///<item><CreadoPor>Creado por Jordan Mateo Pizarro</CreadoPor></item>
        ///<item><FecCrea>17/01/2015</FecCrea></item></list></remarks>
        public int Insertar(WCO_ListarExpediente_Result BE_pExpediente)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InsertarExpediente");
            Parameter[] prm_Params = new Parameter[26];
            prm_Params[0] = new Parameter("@UneuNegocioId", BE_pExpediente.UneuNegocioId);
            prm_Params[1] = new Parameter("@IdExpediente", BE_pExpediente.IdExpediente);
            prm_Params[2] = new Parameter("@CodigoExpediente", BE_pExpediente.CodigoExpediente);
            prm_Params[3] = new Parameter("@ClasificadorMovimiento", BE_pExpediente.ClasificadorMovimiento);
            prm_Params[4] = new Parameter("@IdTipoExpediente", BE_pExpediente.TipoExpediente);
            prm_Params[5] = new Parameter("@FechaInicio", BE_pExpediente.FechaInicio);
            prm_Params[6] = new Parameter("@FechaFinal", BE_pExpediente.FechaFinal);
            prm_Params[7] = new Parameter("@Estado", BE_pExpediente.Estado);
            prm_Params[8] = new Parameter("@UsuarioCreacion", BE_pExpediente.UsuarioCreacion);
            prm_Params[9] = new Parameter("@TipoAdmisionId", BE_pExpediente.TipoAdmisionId);
            prm_Params[10] = new Parameter("@IdCliente", BE_pExpediente.IdClienteFacturacion);
            prm_Params[11] = new Parameter("@IdContrato", BE_pExpediente.IdContrato);
            prm_Params[12] = new Parameter("@Descripcion", BE_pExpediente.Descripcion);
            prm_Params[13] = new Parameter("@TipoPacienteId", BE_pExpediente.TipoPaciente);
            prm_Params[14] = new Parameter("@IdSede", BE_pExpediente.IdSede);
            prm_Params[15] = new Parameter("@TipoAtencionId", BE_pExpediente.TipoAtencion);
            prm_Params[16] = new Parameter("@AseguradoraId", BE_pExpediente.IdAseguradora);
            prm_Params[17] = new Parameter("@EmpresaId", BE_pExpediente.IdEmpresaEmpleadora);
            prm_Params[18] = new Parameter("@PacienteId", BE_pExpediente.IdPaciente);
            prm_Params[19] = new Parameter("@TipoOperacionId", BE_pExpediente.IdContrato);
            prm_Params[20] = new Parameter("@IdClasificacion", BE_pExpediente.IdClasificacion);
            prm_Params[21] = new Parameter("@CodigoComponente", BE_pExpediente.CodigoComponente);
            prm_Params[22] = new Parameter("@NombreComponente", BE_pExpediente.Nombre);
            prm_Params[23] = new Parameter("@pFecha1", BE_pExpediente.FechaCreacion);
            prm_Params[24] = new Parameter("@pFecha2", BE_pExpediente.FechaModificacion);
            prm_Params[25] = new Parameter("@Periodo", BE_pExpediente.Periodo);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            return int.Parse(dop_Operacion.GetParameterByName("@IdExpediente").Value.ToString());
        }

        ///<summary>Método para actualizar el registro en la tabla Expediente.</summary>
        ///<param name="obj_Expediente">Entidad de Negocio</param>
        /// ///<returns>Retorna el BE_Expediente.</returns>
        ///<remarks><list type="bullet">
        ///<item><CreadoPor>Creado por Jordan Mateo Pizarro</CreadoPor></item>
        ///<item><FecCrea>17/01/2015</FecCrea></item></list></remarks>
        public int Actualizar(WCO_ListarExpediente_Result BE_pExpediente)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_ActualizarExpediente");
            Parameter[] prm_Params = new Parameter[15];
            prm_Params[0] = new Parameter("@UneuNegocioId", BE_pExpediente.UneuNegocioId);
            prm_Params[1] = new Parameter("@IdExpediente", BE_pExpediente.IdExpediente);
            prm_Params[2] = new Parameter("@CodigoExpediente", BE_pExpediente.CodigoExpediente);
            prm_Params[3] = new Parameter("@ClasificadorMovimiento", BE_pExpediente.ClasificadorMovimiento);
            prm_Params[4] = new Parameter("@IdTipoExpediente", BE_pExpediente.TipoExpediente);
            prm_Params[5] = new Parameter("@FechaInicio", BE_pExpediente.FechaInicio);
            prm_Params[6] = new Parameter("@FechaFinal", BE_pExpediente.FechaFinal);
            prm_Params[7] = new Parameter("@Estado", BE_pExpediente.Estado);
            prm_Params[8] = new Parameter("@UsuarioModificacion", BE_pExpediente.UsuarioModificacion);
            prm_Params[9] = new Parameter("@UsuarioAprobacion", BE_pExpediente.UsuarioAprobacion);
            prm_Params[10] = new Parameter("@FechaAprobacion", BE_pExpediente.FechaAprobacion);
            prm_Params[11] = new Parameter("@TipoAdmisionId", BE_pExpediente.TipoAdmisionId);
            prm_Params[12] = new Parameter("@IdCliente", BE_pExpediente.IdClienteFacturacion);
            prm_Params[13] = new Parameter("@IdContrato", BE_pExpediente.IdContrato);
            prm_Params[14] = new Parameter("@Descripcion", BE_pExpediente.Descripcion);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            return BE_pExpediente.IdExpediente;
        }

        /////<summary>Método para listar registros de la tabla Expediente</summary>
        /////<param name="obj_Expediente">Entidad de Negocio</param>
        /////<returns>Listado de los registros de la página actual.</returns>
        /////<remarks><list type="bullet">
        /////<item><CreadoPor>Creado por Jordan Mateo Pizarro</CreadoPor></item>
        /////<item><FecCrea>17/01/2015</FecCrea></item></list></remarks>
        public void Inactivar(WCO_ListarExpediente_Result objBEExpediente)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InactivarExpediente");
            Parameter[] prm_Params = new Parameter[3];
            prm_Params[0] = new Parameter("@IdExpediente", objBEExpediente.IdExpediente);
            prm_Params[1] = new Parameter("@Estado", objBEExpediente.Estado);
            prm_Params[2] = new Parameter("@UltimoUsuario", objBEExpediente.UsuarioModificacion);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }

        /////<summary>Método para inserta el registro en la tabla Expediente.</summary>
        /////<param name="obj_ExpedienteDetalle">Entidad de Negocio</param>
        /////<returns>Retorna el BE_ExpedienteDetalle.</returns>
        /////<remarks><list type="bullet">
        /////<item><CreadoPor>Creado por Jordan Mateo Pizarro</CreadoPor></item>
        /////<item><FecCrea>17/01/2015</FecCrea></item></list></remarks>
        public int InsertarDetalle(WCO_ListarExpedienteDetalle_Result BE_pExpedienteDetalle)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InsertarExpedienteDetalle");
            Parameter[] prm_Params = new Parameter[14];
            prm_Params[0] = new Parameter("@IdExpediente", BE_pExpedienteDetalle.IdExpediente);
            prm_Params[1] = new Parameter("@IdSecuencia", BE_pExpedienteDetalle.IdSecuencia);
            prm_Params[2] = new Parameter("@NroPeticion", BE_pExpedienteDetalle.NroPeticion);
            prm_Params[3] = new Parameter("@CodigoComponente", BE_pExpedienteDetalle.CodigoComponente);
            prm_Params[4] = new Parameter("@IdProduccion", BE_pExpedienteDetalle.Id);
            prm_Params[5] = new Parameter("@IdAdmision", BE_pExpedienteDetalle.IdAdmision);
            prm_Params[6] = new Parameter("@IdDetalle", BE_pExpedienteDetalle.Linea);
            prm_Params[7] = new Parameter("@IdSede", BE_pExpedienteDetalle.IdSede); // Idsede
            prm_Params[8] = new Parameter("@Cantidad", BE_pExpedienteDetalle.Cantidad);
            prm_Params[9] = new Parameter("@Monto", BE_pExpedienteDetalle.MontoTotal);
            prm_Params[10] = new Parameter("@Estado", BE_pExpedienteDetalle.Estado);
            prm_Params[11] = new Parameter("@UsuarioCreacion", BE_pExpedienteDetalle.UsuarioCreacion);
            prm_Params[12] = new Parameter("@Periodo", BE_pExpedienteDetalle.Periodo);
            prm_Params[13] = new Parameter("@MontoCopagoFijo", BE_pExpedienteDetalle.Copago);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            return int.Parse(dop_Operacion.GetParameterByName("@IdSecuencia").Value.ToString());
        }

        /////<summary>Método para actualizar el registro en la tabla Expediente.</summary>
        /////<param name="obj_ExpedienteDetalle">Entidad de Negocio</param>
        ///// ///<returns>Retorna el BE_ExpedienteDetalle.</returns>
        /////<remarks><list type="bullet">
        /////<item><CreadoPor>Creado por Jordan Mateo Pizarro</CreadoPor></item>
        /////<item><FecCrea>17/01/2015</FecCrea></item></list></remarks>
        //public static void ActualizarDetalle(ADBE_ExpedienteDetalle BE_pExpedienteDetalle)
        //{
        //    DataOperation dop_Operacion = new DataOperation("WCO_ActualizarExpedienteDetalle");
        //    Parameter[] prm_Params = new Parameter[12];
        //    prm_Params[0] = new Parameter("@IdExpediente", BE_pExpedienteDetalle.Pk.Expediente.Pk.ExpedienteId);
        //    prm_Params[1] = new Parameter("@IdSecuencia", BE_pExpedienteDetalle.Pk.Secuencial);
        //    prm_Params[2] = new Parameter("@NroPeticion", BE_pExpedienteDetalle.Pk.Expediente.Pk.Admision.NroPeticion);
        //    prm_Params[3] = new Parameter("@CodigoComponente", BE_pExpedienteDetalle.Pk.AdmisionServicio.PK.Componente.Pk.CodigoComponente);
        //    prm_Params[4] = new Parameter("@IdProduccion", BE_pExpedienteDetalle.Pk.Expediente.Pk.Produccion.PK.Id);
        //    prm_Params[5] = new Parameter("@IdAdmision", BE_pExpedienteDetalle.Pk.Expediente.Pk.Admision.PK.IdAdmision);
        //    prm_Params[6] = new Parameter("@IdDetalle", BE_pExpedienteDetalle.Pk.Expediente.Pk.Produccion.PK.AdmisionServicio.PK.IdAdmServicio);
        //    prm_Params[7] = new Parameter("@Cantidad", BE_pExpedienteDetalle.Cantidad);
        //    prm_Params[8] = new Parameter("@Monto", BE_pExpedienteDetalle.Monto);
        //    prm_Params[9] = new Parameter("@Estado", BE_pExpedienteDetalle.Estado);
        //    prm_Params[10] = new Parameter("@UsuarioModificacion", BE_pExpedienteDetalle.UsuarioModificacion);
        //    prm_Params[11] = new Parameter("@Periodo", BE_pExpedienteDetalle.Periodo);
        //    dop_Operacion.Parameters.AddRange(prm_Params);
        //    DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        //}



    }
}