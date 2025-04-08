using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiServices.Models.Datos
{
    public class ADDAT_ClasificadorMovimiento
    {
        public List<WCO_ListarClasificadorMovimiento_Result> ListadoPaginado(WCO_ListarClasificadorMovimiento_Result ObjEntidad)
        {
            List<WCO_ListarClasificadorMovimiento_Result> lst = new List<WCO_ListarClasificadorMovimiento_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarClasificadorMovimiento(ObjEntidad.ClasificadorMovimiento, ObjEntidad.Compania, ObjEntidad.Nombre, ObjEntidad.Estado).ToList();
            }
            return lst;
        }

        //public static string Insertar(WCO_ListarClasificadorMovimiento_Result objBEClasificadorMovimiento)
        //{
        //    DataOperation dop_Operacion = new DataOperation("WCO_InsertarClasificadorMovimiento");
        //    Parameter[] prm_Params = new Parameter[6];
        //    prm_Params[0] = new Parameter("@IdClasificadorMovimiento", DbType.Int32);
        //    prm_Params[1] = new Parameter("@Compania", objBEClasificadorMovimiento.Pk.UnidadNegocio.CompaniaCodigo);
        //    prm_Params[2] = new Parameter("@Nombre", objBEClasificadorMovimiento.Nombre);
        //    prm_Params[3] = new Parameter("@Descripcion", objBEClasificadorMovimiento.Descripcion);
        //    prm_Params[4] = new Parameter("@Estado", objBEClasificadorMovimiento.Estado);
        //    prm_Params[5] = new Parameter("@UsuarioCreacion", objBEClasificadorMovimiento.UsuarioCreacion);
        //    dop_Operacion.Parameters.AddRange(prm_Params);
        //    DataManager.ExecuteNonQuery(DAT_Conexion.Co_NameConnection, dop_Operacion);
        //    return dop_Operacion.GetParameterByName("@IdClasificadorMovimiento").Value.ToString();
        //}

        //public static void Actualizar(ADBE_ClasificadorMovimiento objBEClasificadorMovimiento)
        //{
        //    DataOperation dop_Operacion = new DataOperation("WCO_ActualizarClasificadorMovimiento");
        //    Parameter[] prm_Params = new Parameter[6];
        //    prm_Params[0] = new Parameter("@IdClasificadorMovimiento", objBEClasificadorMovimiento.Pk.IdClasificadorMovimiento);
        //    prm_Params[1] = new Parameter("@Compania", objBEClasificadorMovimiento.Pk.UnidadNegocio.CompaniaCodigo);
        //    prm_Params[2] = new Parameter("@Nombre", objBEClasificadorMovimiento.Nombre);
        //    prm_Params[3] = new Parameter("@Descripcion", objBEClasificadorMovimiento.Descripcion);
        //    prm_Params[4] = new Parameter("@Estado", objBEClasificadorMovimiento.Estado);
        //    prm_Params[5] = new Parameter("@UsuarioModificacion", objBEClasificadorMovimiento.UsuarioCreacion);

        //    dop_Operacion.Parameters.AddRange(prm_Params);
        //    DataManager.ExecuteNonQuery(DAT_Conexion.Co_NameConnection, dop_Operacion);
        //}

        //public static void Inactivar(ADBE_ClasificadorMovimiento objBEClasificadorMovimiento)
        //{

        //    DataOperation dop_Operacion = new DataOperation("WCO_InactivarClasificadorMovimiento");
        //    Parameter[] prm_Params = new Parameter[4];
        //    prm_Params[0] = new Parameter("@IdClasificadorMovimiento", objBEClasificadorMovimiento.Pk.IdClasificadorMovimiento);
        //    prm_Params[1] = new Parameter("@Compania", objBEClasificadorMovimiento.Pk.UnidadNegocio.CompaniaCodigo);
        //    prm_Params[2] = new Parameter("@Estado", objBEClasificadorMovimiento.Estado);
        //    prm_Params[3] = new Parameter("@UsuarioModificacion", objBEClasificadorMovimiento.UsuarioModificacion);
        //    dop_Operacion.Parameters.AddRange(prm_Params);
        //    DataManager.ExecuteNonQuery(DAT_Conexion.Co_NameConnection, dop_Operacion);
        //}

        //public static bool ValidarExisteDescClasificadorMovimiento(ADBE_ClasificadorMovimiento objBEClasificadorMovimiento)
        //{
        //    DataOperation dop_DataOperation = new DataOperation("WCO_ExisteClasificadorMovimiento");

        //    Parameter[] prm_Params = new Parameter[4];
        //    prm_Params[0] = new Parameter("@IdClasificadorMovimiento", objBEClasificadorMovimiento.Pk.IdClasificadorMovimiento);
        //    prm_Params[1] = new Parameter("@Compania", objBEClasificadorMovimiento.Pk.UnidadNegocio.CompaniaCodigo);
        //    prm_Params[2] = new Parameter("@Nombre", objBEClasificadorMovimiento.Nombre);
        //    prm_Params[3] = new Parameter("@flagSalida", DbType.Int32);
        //    dop_DataOperation.Parameters.AddRange(prm_Params);

        //    DataManager.ExecuteNonQuery(DAT_Conexion.Co_NameConnection, dop_DataOperation);
        //    if (Convert.ToInt32(dop_DataOperation.GetParameterByName("@flagSalida").Value) >= 1)
        //    {
        //        return false;
        //    }
        //    return true;
        //}

    }
}