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
    public class ADDAT_CorrelativosMast
    {
        public List<WCO_ListarCorrelativosMast_Result> ListarCorrelativos(WCO_ListarCorrelativosMast_Result ObjEntidad)
        {
            List<WCO_ListarCorrelativosMast_Result> lst = new List<WCO_ListarCorrelativosMast_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarCorrelativosMast(ObjEntidad.CompaniaCodigo, ObjEntidad.TipoComprobante, ObjEntidad.Serie,
                    ObjEntidad.Sucursal, ObjEntidad.IdSede, ObjEntidad.Estado).ToList();
            }
            return lst;
        }

        public  string Insertar(WCO_ListarCorrelativosMast_Result objBECorrelativosMast)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InsertarCorrelativosMast");
            Parameter[] prm_Params = new Parameter[11];
            prm_Params[0] = new Parameter("@CompaniaCodigo", objBECorrelativosMast.CompaniaCodigo);
            prm_Params[1] = new Parameter("@TipoComprobante", objBECorrelativosMast.TipoComprobante);
            prm_Params[2] = new Parameter("@Serie", objBECorrelativosMast.Serie);
            prm_Params[3] = new Parameter("@SedCodigo", objBECorrelativosMast.Sucursal);
            prm_Params[4] = new Parameter("@IdEstablecimiento", objBECorrelativosMast.IdEstablecimiento);
            prm_Params[5] = new Parameter("@Descripcion", objBECorrelativosMast.Descripcion);
            prm_Params[6] = new Parameter("@CorrelativoNumero", objBECorrelativosMast.CorrelativoNumero);
            prm_Params[7] = new Parameter("@CorrelativoDesde", objBECorrelativosMast.CorrelativoDesde);
            prm_Params[8] = new Parameter("@CorrelativoHasta", objBECorrelativosMast.CorrelativoHasta);
            prm_Params[9] = new Parameter("@Estado", objBECorrelativosMast.Estado);
            prm_Params[10] = new Parameter("@UsuarioCreacion", objBECorrelativosMast.UsuarioCreacion);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            return dop_Operacion.GetParameterByName("@Serie").Value.ToString();
        }

        public  void Actualizar(WCO_ListarCorrelativosMast_Result objBECorrelativosMast)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_ActualizarCorrelativosMast");
            Parameter[] prm_Params = new Parameter[11];
            prm_Params[0] = new Parameter("@CompaniaCodigo", objBECorrelativosMast.CompaniaCodigo);
            prm_Params[1] = new Parameter("@TipoComprobante", objBECorrelativosMast.TipoComprobante);
            prm_Params[2] = new Parameter("@Serie", objBECorrelativosMast.Serie);
            prm_Params[3] = new Parameter("@SedCodigo", objBECorrelativosMast.Sucursal);
            prm_Params[4] = new Parameter("@IdEstablecimiento", objBECorrelativosMast.IdEstablecimiento);
            prm_Params[5] = new Parameter("@Descripcion", objBECorrelativosMast.Descripcion);
            prm_Params[6] = new Parameter("@CorrelativoNumero", objBECorrelativosMast.CorrelativoNumero);
            prm_Params[7] = new Parameter("@CorrelativoDesde", objBECorrelativosMast.CorrelativoDesde);
            prm_Params[8] = new Parameter("@CorrelativoHasta", objBECorrelativosMast.CorrelativoHasta);
            prm_Params[9] = new Parameter("@Estado", objBECorrelativosMast.Estado);
            prm_Params[10] = new Parameter("@UsuarioModificacion", objBECorrelativosMast.UltimoUsuario);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }

        public  void Inactivar(WCO_ListarCorrelativosMast_Result objBECorrelativosMast)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InactivarCorrelativosMast");
            Parameter[] prm_Params = new Parameter[6];
            prm_Params[0] = new Parameter("@CompaniaCodigo", objBECorrelativosMast.CompaniaCodigo);
            prm_Params[1] = new Parameter("@TipoComprobante", objBECorrelativosMast.TipoComprobante);
            prm_Params[2] = new Parameter("@Serie", objBECorrelativosMast.Serie);
            prm_Params[3] = new Parameter("@SedCodigo", objBECorrelativosMast.Sucursal);
            prm_Params[4] = new Parameter("@Estado", objBECorrelativosMast.Estado);
            prm_Params[5] = new Parameter("@UltimoUsuario", objBECorrelativosMast.UltimoUsuario);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }

        public  bool ValidarExisteDescCorrelativosMast(WCO_ListarCorrelativosMast_Result objBECorrelativosMast)
        {
            DataOperation dop_DataOperation = new DataOperation("WCO_ExisteCorrelativosMast");
            Parameter[] prm_Params = new Parameter[6];
            prm_Params[0] = new Parameter("@CompaniaCodigo", objBECorrelativosMast.CompaniaCodigo);
            prm_Params[1] = new Parameter("@TipoComprobante", objBECorrelativosMast.TipoComprobante);
            prm_Params[2] = new Parameter("@Serie", objBECorrelativosMast.Serie);
            prm_Params[3] = new Parameter("@SedCodigo", objBECorrelativosMast.Sucursal);
            prm_Params[4] = new Parameter("@Descripcion", objBECorrelativosMast.Descripcion);
            prm_Params[5] = new Parameter("@flagSalida", DbType.Int32);
            dop_DataOperation.Parameters.AddRange(prm_Params);

            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation);
            if (Convert.ToInt32(dop_DataOperation.GetParameterByName("@flagSalida").Value) > 1)
            {
                return false;
            }
            return true;
        }

        public  DataTable ListarSucursalSerie(WCO_ListarCorrelativosMast_Result objBECorrelativosMast)
        {
            DataSet ds_Result = null;
            DataOperation dop_DataOperation = new DataOperation("WCO_ListarSucursalSerie");
            Parameter[] prm_Params = new Parameter[4];
            prm_Params[0] = new Parameter("@CompaniaCodigo", objBECorrelativosMast.CompaniaCodigo);
            prm_Params[1] = new Parameter("@TipoComprobante", objBECorrelativosMast.TipoComprobante);
            prm_Params[2] = new Parameter("@Serie", objBECorrelativosMast.Serie);
            prm_Params[3] = new Parameter("@Sucursal", objBECorrelativosMast.Sucursal);
            dop_DataOperation.Parameters.AddRange(prm_Params);
            ds_Result = DataManager.ExecuteDataSet(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation);
            if (ds_Result == null || ds_Result.Tables.Count == 0)
            {
                return null;
            }
            return ds_Result.Tables[0];
        }

        public  string InsertarSerieSucursal(WCO_ListarCorrelativosMast_Result objBECorrelativosMast)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InsertarSerieSucursal");
            Parameter[] prm_Params = new Parameter[4];
            prm_Params[0] = new Parameter("@CompaniaCodigo", objBECorrelativosMast.CompaniaCodigo);
            prm_Params[1] = new Parameter("@TipoComprobante", objBECorrelativosMast.TipoComprobante);
            prm_Params[2] = new Parameter("@Serie", objBECorrelativosMast.Serie);
            prm_Params[3] = new Parameter("@Sucursal", objBECorrelativosMast.Sucursal);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            return dop_Operacion.GetParameterByName("@Sucursal").Value.ToString();
        }

        public  void EliminarSerieSucursal(WCO_ListarCorrelativosMast_Result objBECorrelativosMast)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_EliminarSerieSucursal");
            Parameter[] prm_Params = new Parameter[4];
            prm_Params[0] = new Parameter("@CompaniaCodigo", objBECorrelativosMast.CompaniaCodigo);
            prm_Params[1] = new Parameter("@TipoComprobante", objBECorrelativosMast.TipoComprobante);
            prm_Params[2] = new Parameter("@Serie", objBECorrelativosMast.Serie);
            prm_Params[3] = new Parameter("@Sucursal", objBECorrelativosMast.Sucursal);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }

    }
}