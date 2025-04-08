using RoyalSystems.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebApiServices.Models.Request;

namespace WebApiServices.Models.Datos
{
    public class ADDAT_TipoCambioComercial
    {
        public List<WCO_ListarTipoCambioComercial_Result> ListarTipoCambioComercial(WCO_ListarTipoCambioComercial_Result ObjEntidad)
        {
            List<WCO_ListarTipoCambioComercial_Result> lst = new List<WCO_ListarTipoCambioComercial_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarTipoCambioComercial(ObjEntidad.IdTipoCambio, ObjEntidad.FechaInicio, 
                    ObjEntidad.FechaFin, ObjEntidad.Estado).ToList();
            }
            return lst;
        }

        public void InsertarCambio(WCO_ListarTipoCambioComercial_Result objBETipoCambio)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InsertarTipoCambioComercial");
            Parameter[] prm_Params = new Parameter[6];
            prm_Params[0] = new Parameter("@IdTipoCambio", DbType.Int32);
            prm_Params[1] = new Parameter("@FechaInicio", objBETipoCambio.FechaInicio);
            prm_Params[2] = new Parameter("@FechaFin", objBETipoCambio.FechaFin);
            prm_Params[3] = new Parameter("@Valor", objBETipoCambio.Valor);
            prm_Params[4] = new Parameter("@Estado", objBETipoCambio.Estado);
            prm_Params[5] = new Parameter("@UsuarioCreacion", objBETipoCambio.UsuarioCreacion);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }

        public void ActualizarCambio(WCO_ListarTipoCambioComercial_Result objBETipoCambio)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_ActualizarTipoCambioComercial");
            Parameter[] prm_Params = new Parameter[6];
            prm_Params[0] = new Parameter("@IdTipoCambio", objBETipoCambio.IdTipoCambio);
            prm_Params[1] = new Parameter("@FechaInicio", objBETipoCambio.FechaInicio);
            prm_Params[2] = new Parameter("@FechaFin", objBETipoCambio.FechaFin);
            prm_Params[3] = new Parameter("@Valor", objBETipoCambio.Valor);
            prm_Params[4] = new Parameter("@Estado", objBETipoCambio.Estado);
            prm_Params[5] = new Parameter("@UsuarioModificacion", objBETipoCambio.UsuarioModificacion);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }

        public void InactivarCambio(WCO_ListarTipoCambioComercial_Result objBETipoCambio)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InactivarTipoCambioComercial");
            Parameter[] prm_Params = new Parameter[3];
            prm_Params[0] = new Parameter("@IdTipoCambio", objBETipoCambio.IdTipoCambio);
            prm_Params[1] = new Parameter("@Estado", objBETipoCambio.Estado);
            prm_Params[2] = new Parameter("@UsuarioModificacion", objBETipoCambio.UsuarioModificacion);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }

        public bool ValidarCambio(WCO_ListarTipoCambioComercial_Result objBETipoCambio)
        {
            DataOperation dop_DataOperation = new DataOperation("WCO_EXISTETipoCambioComercial");
            Parameter[] prm_Params = new Parameter[3];
            prm_Params[0] = new Parameter("@FechaInicio", objBETipoCambio.FechaInicio);
            prm_Params[1] = new Parameter("@FechaFin", objBETipoCambio.FechaFin);
            prm_Params[2] = new Parameter("@flagSalida", DbType.Int32);
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