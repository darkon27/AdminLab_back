using RoyalSystems.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebApiServices.Models.Datos
{
    public class ADDAT_ListaBase
    {
        #region ListaBase

        public List<WCO_ListarListaBase_Result> ListadoBasePaginado(WCO_ListarListaBase_Result ObjEntidad)
        {
            List<WCO_ListarListaBase_Result> lst = new List<WCO_ListarListaBase_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarListaBase(ObjEntidad.Descripcion, ObjEntidad.Estado).ToList();
            }
            return lst;
        }
        public int Insertar(WCO_ListarListaBase_Result objBEListaBase)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InsertarListaBase");
            Parameter[] prm_Params = new Parameter[15];
            prm_Params[0] = new Parameter("@Moneda", objBEListaBase.Moneda);
            prm_Params[1] = new Parameter("@TipoLista", objBEListaBase.TipoLista);
            prm_Params[2] = new Parameter("@IdCliente", objBEListaBase.IdCliente);
            prm_Params[3] = new Parameter("@FechaValidezInicio", objBEListaBase.FechaValidezInicio);
            prm_Params[4] = new Parameter("@FechaValidezFin", objBEListaBase.FechaValidezFin);
            prm_Params[5] = new Parameter("@Nombre", objBEListaBase.Nombre);
            prm_Params[6] = new Parameter("@Descripcion", objBEListaBase.Descripcion);
            prm_Params[7] = new Parameter("@Codigo", objBEListaBase.Codigo);
            prm_Params[8] = new Parameter("@IndicadorPrecioFijo", objBEListaBase.IndicadorPrecioFijo);
            prm_Params[9] = new Parameter("@IdEmpresaAnteriorUnificacion", objBEListaBase.IdEmpresaAnteriorUnificacion);
            prm_Params[10] = new Parameter("@TipoPaciente", objBEListaBase.TipoPaciente);
            prm_Params[11] = new Parameter("@Estado", objBEListaBase.Estado);
            prm_Params[12] = new Parameter("@FechaCreacion", objBEListaBase.FechaCreacion);
            prm_Params[13] = new Parameter("@UsuarioCreacion", objBEListaBase.UsuarioCreacion);
            prm_Params[14] = new Parameter("@IdListaBase", DbType.Int32);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            return int.Parse(dop_Operacion.GetParameterByName("@IdListaBase").Value.ToString());
        }

        ///<summary>Actualizar el registro en la tabla PERSONAMAST.</summary>
        ///<param name="obj_pSA_Curriculo">Entidad de Negocio</param>
        ///<remarks><list type="bullet">
        ///<item><CreadoPor>Creado PorJordan Mateo Pizarro</CreadoPor></item>
        ///<item><FecCrea>17/10/2012</FecCrea></item></list></remarks>
        public void Actualizar(WCO_ListarListaBase_Result objBEListaBase)
        {

            DataOperation dop_Operacion = new DataOperation("WCO_ActualizarListaBase");
            Parameter[] prm_Params = new Parameter[15];
            prm_Params[0] = new Parameter("@Moneda", objBEListaBase.Moneda);
            prm_Params[1] = new Parameter("@TipoLista", objBEListaBase.TipoLista);
            prm_Params[2] = new Parameter("@IdCliente", objBEListaBase.IdCliente);
            prm_Params[3] = new Parameter("@FechaValidezInicio", objBEListaBase.FechaValidezInicio);
            prm_Params[4] = new Parameter("@FechaValidezFin", objBEListaBase.FechaValidezFin);
            prm_Params[5] = new Parameter("@Nombre", objBEListaBase.Nombre);
            prm_Params[6] = new Parameter("@Descripcion", objBEListaBase.Descripcion);
            prm_Params[7] = new Parameter("@Codigo", objBEListaBase.Codigo);
            prm_Params[8] = new Parameter("@IndicadorPrecioFijo", objBEListaBase.IndicadorPrecioFijo);
            prm_Params[9] = new Parameter("@IdEmpresaAnteriorUnificacion", objBEListaBase.IdEmpresaAnteriorUnificacion);
            prm_Params[10] = new Parameter("@TipoPaciente", objBEListaBase.TipoPaciente);
            prm_Params[11] = new Parameter("@Estado", objBEListaBase.Estado);
            prm_Params[12] = new Parameter("@FechaModificacion", objBEListaBase.FechaModificacion);
            prm_Params[13] = new Parameter("@UsuarioModificacion", objBEListaBase.UsuarioModificacion);
            prm_Params[14] = new Parameter("@IdListaBase", objBEListaBase.IdListaBase);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }

        ///<summary>Actualizar el registro en la tabla PERSONAMAST.</summary>
        ///<param name="obj_pSA_Curriculo">Entidad de Negocio</param>
        ///<remarks><list type="bullet">
        ///<item><CreadoPor>Creado PorJordan Mateo Pizarro</CreadoPor></item>
        ///<item><FecCrea>17/10/2012</FecCrea></item></list></remarks>
        public void Inactivar(WCO_ListarListaBase_Result objBEListaBase)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InactivarListaBase");
            Parameter[] prm_Params = new Parameter[4];
            prm_Params[0] = new Parameter("@Estado", objBEListaBase.Estado);
            prm_Params[1] = new Parameter("@FechaModificacion", objBEListaBase.FechaModificacion);
            prm_Params[2] = new Parameter("@UsuarioModificacion", objBEListaBase.UsuarioModificacion);
            prm_Params[3] = new Parameter("@IdListaBase", objBEListaBase.IdListaBase);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }

        public bool ValidarExiste(WCO_ListarListaBase_Result objBEListaBase)
        {
            DataOperation dop_DataOperation = new DataOperation("WCO_ExisteListaBase");
            Parameter[] prm_Params = new Parameter[3];
            prm_Params[0] = new Parameter("@IdListaBase", objBEListaBase.IdListaBase);
            prm_Params[1] = new Parameter("@Descripcion", objBEListaBase.Descripcion);
            prm_Params[2] = new Parameter("@flagSalida", DbType.Int32);
            dop_DataOperation.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation);
            if (Convert.ToInt32(dop_DataOperation.GetParameterByName("flagSalida").Value) >= 1)
            {
                return false;
            }
            return true;
        }

        public bool ValidarExisteCodigo(WCO_ListarListaBase_Result objBEListaBase)
        {
            DataOperation dop_DataOperation = new DataOperation("WCO_ExisteListaBaseCodigo");
            Parameter[] prm_Params = new Parameter[3];
            prm_Params[0] = new Parameter("@IdListaBase", objBEListaBase.IdListaBase);
            prm_Params[1] = new Parameter("@Codigo", objBEListaBase.Codigo);
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

        #region ListaBaseComponente

        public List<WCO_ListarComponentesdeListaB_Result> ListaBaseComponente(WCO_ListarComponentesdeListaB_Result ObjEntidad)
        {
            List<WCO_ListarComponentesdeListaB_Result> lst = new List<WCO_ListarComponentesdeListaB_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarComponentesdeListaB(ObjEntidad.IdListaBase, ObjEntidad.CODIGOCOMPONENTE,
                    ObjEntidad.Nombre, 0, 0).ToList();
            }
            return lst;
        }
        public int InsertarComponente(WCO_ListarComponentesdeListaB_Result objBEListaBaseComponente)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InsertarListabaseComponente");
            Parameter[] prm_Params = new Parameter[3];
            prm_Params[0] = new Parameter("@IdListaBase", objBEListaBaseComponente.IdListaBase);
            prm_Params[1] = new Parameter("@CodigoComponente", objBEListaBaseComponente.CODIGOCOMPONENTE);
            prm_Params[2] = new Parameter("@UsuarioCreacion", objBEListaBaseComponente.UsuarioCreacion);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            return int.Parse(dop_Operacion.GetParameterByName("@IdListaBase").Value.ToString());
        }

        public int InsertarComponenteCargaMasiva(WCO_ListarComponentesdeListaB_Result objBEListaBaseComponente)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InsertarListabaseComponenteMasiva");
            Parameter[] prm_Params = new Parameter[15];
            prm_Params[0] = new Parameter("@CodigoComponente", objBEListaBaseComponente.CODIGOCOMPONENTE);
            prm_Params[1] = new Parameter("@Periodo", objBEListaBaseComponente.Periodo);
            prm_Params[2] = new Parameter("@Moneda", objBEListaBaseComponente.Moneda);
            prm_Params[3] = new Parameter("@PrecioUnitarioLista", objBEListaBaseComponente.PrecioUnitarioLista);
            prm_Params[4] = new Parameter("@PrecioUnitarioListaLocal", objBEListaBaseComponente.PrecioUnitarioListaLocal);
            prm_Params[5] = new Parameter("@PrecioUnitarioBase", objBEListaBaseComponente.PrecioUnitarioBase);
            prm_Params[6] = new Parameter("@PrecioUnitarioBaseLocal", objBEListaBaseComponente.PrecioUnitarioBaseLocal);
            prm_Params[7] = new Parameter("@FechaValidezInicio", objBEListaBaseComponente.FechaValidezInicio);
            prm_Params[8] = new Parameter("@FechaValidezFin", objBEListaBaseComponente.FechaValidezFin);
            prm_Params[9] = new Parameter("@Factor", objBEListaBaseComponente.Factor);
            prm_Params[10] = new Parameter("@TipoFactor", objBEListaBaseComponente.TipoFactor);
            prm_Params[11] = new Parameter("@Estado", objBEListaBaseComponente.Estado);
            prm_Params[12] = new Parameter("@UsuarioCreacion", objBEListaBaseComponente.UsuarioCreacion);
            prm_Params[13] = new Parameter("@IdListaBase", objBEListaBaseComponente.IdListaBase);
            prm_Params[14] = new Parameter("@Id", DbType.Int32);
            dop_Operacion.Parameters.AddRange(prm_Params);

            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            return int.Parse(dop_Operacion.GetParameterByName("@Id").Value.ToString());
        }

        ///<summary>Actualizar el registro en la tabla PERSONAMAST.</summary>
        ///<param name="obj_pSA_Curriculo">Entidad de Negocio</param>
        ///<remarks><list type="bullet">
        ///<item><CreadoPor>Creado PorJordan Mateo Pizarro</CreadoPor></item>
        ///<item><FecCrea>17/10/2012</FecCrea></item></list></remarks>
        public void ActualizarComponente(WCO_ListarComponentesdeListaB_Result objBEListaBaseComponente)
        {

            DataOperation dop_Operacion = new DataOperation("WCO_ActualizarListaBaseComponente");
            Parameter[] prm_Params = new Parameter[21];
            prm_Params[0] = new Parameter("@CodigoComponente", objBEListaBaseComponente.CODIGOCOMPONENTE);
            prm_Params[1] = new Parameter("@IdListaBase", objBEListaBaseComponente.IdListaBase);
            prm_Params[2] = new Parameter("@Periodo", objBEListaBaseComponente.Periodo);
            prm_Params[3] = new Parameter("@Moneda", objBEListaBaseComponente.Moneda);
            prm_Params[4] = new Parameter("@PrecioUnitarioLista", objBEListaBaseComponente.PrecioUnitarioLista);
            prm_Params[5] = new Parameter("@PrecioUnitarioListaLocal", objBEListaBaseComponente.PrecioUnitarioListaLocal);
            prm_Params[6] = new Parameter("@PrecioUnitarioBase", objBEListaBaseComponente.PrecioUnitarioBase);
            prm_Params[7] = new Parameter("@PrecioUnitarioBaseLocal", objBEListaBaseComponente.PrecioUnitarioBaseLocal);
            prm_Params[8] = new Parameter("@FechaValidezInicio", objBEListaBaseComponente.FechaValidezInicio);
            prm_Params[9] = new Parameter("@FechaValidezFin", objBEListaBaseComponente.FechaValidezFin);
            prm_Params[10] = new Parameter("@IndicadorPrecioCero", objBEListaBaseComponente.IndicadorPrecioCero);
            prm_Params[11] = new Parameter("@Factor", objBEListaBaseComponente.Factor);
            prm_Params[12] = new Parameter("@TipoFactor", objBEListaBaseComponente.TipoFactor);
            prm_Params[13] = new Parameter("@IndicadorFactor", objBEListaBaseComponente.IndicadorFactor);
            prm_Params[14] = new Parameter("@PrecioCosto", objBEListaBaseComponente.PrecioCosto);
            prm_Params[15] = new Parameter("@Estado", objBEListaBaseComponente.Estado);
            prm_Params[16] = new Parameter("@FechaModificacion", objBEListaBaseComponente.FechaModificacion);
            prm_Params[17] = new Parameter("@UsuarioModificacion", objBEListaBaseComponente.UsuarioModificacion);
            prm_Params[18] = new Parameter("@FactorCosto", objBEListaBaseComponente.FactorCosto);
            prm_Params[19] = new Parameter("@PrecioKairos", objBEListaBaseComponente.PrecioKairos);
            prm_Params[20] = new Parameter("@FactorKairos", objBEListaBaseComponente.FactorKairos);

            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }

        /*Eliminar componentes de lista seleccionada*/
        public void EliminarComponente(WCO_ListarComponentesdeListaB_Result objBEComponentelistaBase)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_EliminarMComponentesListaBase");
            Parameter[] prm_Params = new Parameter[2];
            prm_Params[0] = new Parameter("@IdListaBase", objBEComponentelistaBase.IdListaBase);
            prm_Params[1] = new Parameter("@CodigoComponente", objBEComponentelistaBase.CODIGOCOMPONENTE);

            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }

        ///<summary>Actualizar el registro en la tabla PERSONAMAST.</summary>
        ///<param name="obj_pSA_Curriculo">Entidad de Negocio</param>
        ///<remarks><list type="bullet">
        ///<item><CreadoPor>Creado PorJordan Mateo Pizarro</CreadoPor></item>
        ///<item><FecCrea>17/10/2012</FecCrea></item></list></remarks>
        public void InactivarComponente(WCO_ListarComponentesdeListaB_Result objBEListaBaseComponente)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InactivarListaBaseComponente");
            Parameter[] prm_Params = new Parameter[3];
            prm_Params[0] = new Parameter("@UsuarioModificacion", objBEListaBaseComponente.UsuarioModificacion);
            prm_Params[1] = new Parameter("@CodigoComponente", objBEListaBaseComponente.CODIGOCOMPONENTE);
            prm_Params[2] = new Parameter("@IdListaBase", objBEListaBaseComponente.IdListaBase);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }
        #endregion

    }
}