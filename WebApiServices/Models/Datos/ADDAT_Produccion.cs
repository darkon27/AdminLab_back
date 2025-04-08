using RoyalSystems.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiServices.Models.Datos
{
    public class ADDAT_Produccion
    {


        public List<WCO_ListarPeriodoEmision_Result> ListarPeriodoEmision(WCO_ListarPeriodoEmision_Result ObjEntidad)
        {
            List<WCO_ListarPeriodoEmision_Result> lst = new List<WCO_ListarPeriodoEmision_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarPeriodoEmision(ObjEntidad.UneuNegocioId).ToList();
            }
            return lst;
        }


        public List<WCO_ListarProduccion_Result> ListarProduccion(WCO_ListarProduccion_Result ObjEntidad)
        {
            List<WCO_ListarProduccion_Result> lst = new List<WCO_ListarProduccion_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarProduccion(ObjEntidad.TipoAdmisionId, ObjEntidad.Persona, ObjEntidad.TipoPacienteId, ObjEntidad.IdSede,
                    0, ObjEntidad.IdAseguradora, ObjEntidad.IdEmpresaPaciente, ObjEntidad.IdPaciente, ObjEntidad.TipoOperacionId,
                    ObjEntidad.IdClasificacion, ObjEntidad.CodigoComponente, ObjEntidad.Nombre, ObjEntidad.FechaAdmision, ObjEntidad.FechaProduccion,
                    ObjEntidad.ClasificadorMovimiento, ObjEntidad.Estado, ObjEntidad.Periodo, ObjEntidad.NroPeticion).ToList();
            }
            return lst;
        }

        public List<WCO_ListarProduccionGeneral_Result> ListarProduccionGeneral(WCO_ListarProduccionGeneral_Result ObjEntidad)
        {
            List<WCO_ListarProduccionGeneral_Result> lst = new List<WCO_ListarProduccionGeneral_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarProduccionGeneral(ObjEntidad.UneuNegocioId, ObjEntidad.Periodo).ToList();
            }
            return lst;
        }

        ///<summary>Inserta el registro en la tabla ADBE_Produccion.</summary>
        ///<param name="ADBE_Produccion">Entidad de Negocio</param>
        ///<returns>No Retorna .</returns>
        ///<remarks><list type="bullet">
        ///<item><CreadoPor>Creado Por Jordan Mateo Pizarro</CreadoPor></item>
        ///<item><FecCrea>10/12/2014</FecCrea></item></list></remarks>
        public int InsertarProduccion(WCO_ListarProduccion_Result objBEProduccion)
        {
            int valor = 0;
            try
            {
                DataOperation dop_Operacion = new DataOperation("WCO_InsertarProduccionIndividual");
                Parameter[] prm_Params = new Parameter[12];
                prm_Params[0] = new Parameter("@UneuNegocioId", objBEProduccion.UneuNegocioId);
                prm_Params[1] = new Parameter("@Periodo", objBEProduccion.Periodo);
                prm_Params[2] = new Parameter("@FechaProduccion", objBEProduccion.FechaProduccion);
                prm_Params[3] = new Parameter("@IdAdmision", objBEProduccion.IdAdmision);
                prm_Params[4] = new Parameter("@Linea", objBEProduccion.IdAdmServicio);
                prm_Params[5] = new Parameter("@NroPeticion", objBEProduccion.NroPeticion);
                prm_Params[6] = new Parameter("@CodigoComponente", objBEProduccion.CodigoComponente);
                prm_Params[7] = new Parameter("@Valor", objBEProduccion.Monto);
                prm_Params[8] = new Parameter("@Copago", objBEProduccion.Copago);
                prm_Params[9] = new Parameter("@Cantidad", objBEProduccion.Cantidad);
                prm_Params[10] = new Parameter("@Estado", objBEProduccion.Estado);
                prm_Params[11] = new Parameter("@UsuarioCreacion", objBEProduccion.UsuarioCreacion);
                dop_Operacion.Parameters.AddRange(prm_Params);
                DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
                valor = 1;
            }
            catch (Exception ex)
            {
            }
            return valor;
        }

        ///<summary>Inserta el registro en la tabla ADBE_Produccion.</summary>
        ///<param name="ADBE_Produccion">Entidad de Negocio</param>
        ///<returns>No Retorna .</returns>
        ///<remarks><list type="bullet">
        ///<item><CreadoPor>Creado Por Jordan Mateo Pizarro</CreadoPor></item>
        ///<item><FecCrea>10/12/2014</FecCrea></item></list></remarks>
        public int InsertarMasivo(WCO_ListarProduccion_Result objBEProduccion)
        {
            int valor = 0;
            try
            {
                DataOperation dop_Operacion = new DataOperation("WCO_InsertarProduccionMasiva");
                Parameter[] prm_Params = new Parameter[8];
                prm_Params[0] = new Parameter("@UneuNegocioId", objBEProduccion.UneuNegocioId);
                prm_Params[1] = new Parameter("@Periodo", objBEProduccion.Periodo);
                prm_Params[2] = new Parameter("@FechaInicio", objBEProduccion.FechaAdmision);
                prm_Params[3] = new Parameter("@FechaFin", objBEProduccion.FechaProduccion);
                prm_Params[4] = new Parameter("@Estado", objBEProduccion.Estado);
                prm_Params[5] = new Parameter("@UsuarioCreacion", objBEProduccion.UsuarioCreacion);
                prm_Params[6] = new Parameter("@ClasificadorMovimiento", objBEProduccion.ClasificadorMovimiento);
                prm_Params[7] = new Parameter("@IdCliente", objBEProduccion.Persona);
                dop_Operacion.Parameters.AddRange(prm_Params);
                DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
                valor = 1;
            }
            catch (Exception ex)
            {
            }
            return valor;
        }


        ///<summary>Inserta el registro en la tabla ADBE_Produccion.</summary>
        ///<param name="ADBE_Produccion">Entidad de Negocio</param>
        ///<returns>No Retorna .</returns>
        ///<remarks><list type="bullet">
        ///<item><CreadoPor>Creado Por Jordan Mateo Pizarro</CreadoPor></item>
        ///<item><FecCrea>10/12/2014</FecCrea></item></list></remarks>
        public int EliminarProduccion(WCO_ListarProduccion_Result objBEProduccion)
        {
            int valor = 0;
            try
            {
                DataOperation dop_Operacion = new DataOperation("WCO_EliminarProduccion");
                Parameter[] prm_Params = new Parameter[5];
                prm_Params[0] = new Parameter("@UneuNegocioId", objBEProduccion.UneuNegocioId);
                prm_Params[1] = new Parameter("@NroPeticion", objBEProduccion.NroPeticion);
                prm_Params[2] = new Parameter("@CodigoComponente", objBEProduccion.CodigoComponente);
                prm_Params[3] = new Parameter("@IdAdmision", objBEProduccion.IdAdmision);
                prm_Params[4] = new Parameter("@Linea", objBEProduccion.IdAdmServicio);
                dop_Operacion.Parameters.AddRange(prm_Params);
                DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
                valor = 1;
            }
            catch (Exception ex)
            {
            }
            return valor;
        }

        ///<summary>Inserta el registro en la tabla ADBE_Produccion.</summary>
        ///<param name="obj_pSA_Curriculo">Entidad de Negocio</param>
        ///<returns>No Retorna .</returns>
        ///<remarks><list type="bullet">
        ///<item><CreadoPor>Creado Por Jordan Mateo Pizarro</CreadoPor></item>
        ///<item><FecCrea>10/12/2014</FecCrea></item></list></remarks>
        public int EliminarProduccionMasivo(WCO_ListarProduccion_Result objBEProduccion)
        {
            int valor = 0;
            try
            {
                DataOperation dop_Operacion = new DataOperation("WCO_EliminarProduccionMasiva");
                Parameter[] prm_Params = new Parameter[4];
                prm_Params[0] = new Parameter("@UneuNegocioId", objBEProduccion.UneuNegocioId);
                prm_Params[1] = new Parameter("@Periodo", objBEProduccion.Periodo);
                prm_Params[2] = new Parameter("@Estado", objBEProduccion.Estado);
                prm_Params[3] = new Parameter("@UsuarioCreacion", objBEProduccion.UsuarioCreacion);
                dop_Operacion.Parameters.AddRange(prm_Params);
                DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
                valor = 1;
            }
            catch (Exception ex)
            {
            }
            return valor;
        }
    }
}