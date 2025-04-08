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
    public class ADDAT_TipoPago
    {

        public List<WCO_ListarTipoPago_Result> ListarTipoPago(WCO_ListarTipoPago_Result ObjEntidad)
        {
            List<WCO_ListarTipoPago_Result> lst = new List<WCO_ListarTipoPago_Result>();
            using (var context = new BDComercialEntities())
            {
                Nullable<int> Idd = new Nullable<int>();
                if (ObjEntidad.Id > 0)
                { Idd = ObjEntidad.Id; }
                lst = context.WCO_ListarTipoPago(Idd, ObjEntidad.IdCodigo, ObjEntidad.Nombre, ObjEntidad.Estado, 
                    ObjEntidad.Visible).ToList();
            }
            return lst;
        }

        public int Insertar(WCO_ListarTipoPago_Result objBETipoPago)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InsertarTipoPago");
            Parameter[] prm_Params = new Parameter[11];
            prm_Params[0] = new Parameter("@IdCodigo", objBETipoPago.IdCodigo);
            prm_Params[1] = new Parameter("@Codigo", objBETipoPago.Codigo);
            prm_Params[2] = new Parameter("@Nombre", objBETipoPago.Nombre);
            prm_Params[3] = new Parameter("@Descripcion", objBETipoPago.Descripcion);
            prm_Params[4] = new Parameter("@Visible", objBETipoPago.Visible);
            prm_Params[5] = new Parameter("@Orden", objBETipoPago.Orden);
            prm_Params[6] = new Parameter("@FlagReferencia", objBETipoPago.FlagReferencia);
            prm_Params[7] = new Parameter("@FlagBanco", objBETipoPago.FlagBanco);
            prm_Params[8] = new Parameter("@Estado", objBETipoPago.Estado);
            prm_Params[9] = new Parameter("@UsuarioCreacion", objBETipoPago.UsuarioCreacion);
            prm_Params[10] = new Parameter("@Id", DbType.Int32);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            return int.Parse(dop_Operacion.GetParameterByName("@Id").Value.ToString());
        }

        public bool ValidarExiste(WCO_ListarTipoPago_Result objBETipoPago)
        {
            DataOperation dop_DataOperation = new DataOperation("WCO_ExisteTipoPago");
            Parameter[] prm_Params = new Parameter[5];
            prm_Params[0] = new Parameter("@Id", objBETipoPago.Id);
            prm_Params[1] = new Parameter("@IdCodigo", objBETipoPago.IdCodigo);
            prm_Params[2] = new Parameter("@Codigo", objBETipoPago.Codigo);
            prm_Params[3] = new Parameter("@Nombre", objBETipoPago.Nombre);
            prm_Params[4] = new Parameter("@flagSalida", DbType.Int32);
            dop_DataOperation.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation);
            if (Convert.ToInt32(dop_DataOperation.GetParameterByName("flagSalida").Value) >= 1)
            {
                return false;
            }
            return true;
        }

        public void Actualizar(WCO_ListarTipoPago_Result objBETipoPago)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_ActualizarTipoPago");
            Parameter[] prm_Params = new Parameter[11];
            prm_Params[0] = new Parameter("@IdCodigo", objBETipoPago.IdCodigo);
            prm_Params[1] = new Parameter("@Codigo", objBETipoPago.Codigo);
            prm_Params[2] = new Parameter("@Nombre", objBETipoPago.Nombre);
            prm_Params[3] = new Parameter("@Descripcion", objBETipoPago.Descripcion);
            prm_Params[4] = new Parameter("@Visible", objBETipoPago.Visible);
            prm_Params[5] = new Parameter("@Orden", objBETipoPago.Orden);
            prm_Params[6] = new Parameter("@FlagReferencia", objBETipoPago.FlagReferencia);
            prm_Params[7] = new Parameter("@FlagBanco", objBETipoPago.FlagBanco);
            prm_Params[8] = new Parameter("@Estado", objBETipoPago.Estado);
            prm_Params[9] = new Parameter("@UsuarioCreacion", objBETipoPago.UsuarioModificacion);
            prm_Params[10] = new Parameter("@Id", objBETipoPago.Id);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }

        public void Inactivar(WCO_ListarTipoPago_Result objBETipoPago)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InactivarTipoPago");
            Parameter[] prm_Params = new Parameter[2];
            prm_Params[0] = new Parameter("@UsuarioModificacion", objBETipoPago.UsuarioModificacion);
            prm_Params[1] = new Parameter("@Id", objBETipoPago.Id);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }

    }
}