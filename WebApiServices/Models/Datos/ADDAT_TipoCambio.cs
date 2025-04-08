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
    public class ADDAT_TipoCambio
    {
        public List<WCO_ListarTipoCambioMo_Result> ListarTipoPago(WCO_ListarTipoCambioMo_Result ObjEntidad)
        {
            List<WCO_ListarTipoCambioMo_Result> lst = new List<WCO_ListarTipoCambioMo_Result>();
            using (var context = new BDComercialEntities())
            {
                Nullable<DateTime> Idd = new Nullable<DateTime>();
                if (ObjEntidad.Factor == 1)
                {
                    lst = context.WCO_ListarTipoCambioMo(ObjEntidad.FechaCambio, Idd, Idd, ObjEntidad.Estado).ToList();
                }
                else
                {
                    lst = context.WCO_ListarTipoCambioMo(Idd, ObjEntidad.FechaCambio, ObjEntidad.UltimaFechaModif, ObjEntidad.Estado).ToList();

                }
            }
            return lst;
        }

        public int Insertar(WCO_ListarTipoCambioMo_Result objBETipoCambio)
        {
            try
            {
                DataOperation dop_Operacion = new DataOperation("WCO_InsertarTipoCambio");
                Parameter[] prm_Params = new Parameter[20];
                prm_Params[0] = new Parameter("@MonecaCodigo", objBETipoCambio.MonedaCodigo);
                prm_Params[1] = new Parameter("@MonedaCodigoCambio", objBETipoCambio.MonedaCambioCodigo);
                prm_Params[2] = new Parameter("@PFecha", objBETipoCambio.FechaCambio);
                prm_Params[3] = new Parameter("@PFechastrign", objBETipoCambio.FechaCambioString);
                prm_Params[4] = new Parameter("@Factor", objBETipoCambio.Factor);
                prm_Params[5] = new Parameter("@Fcompra", objBETipoCambio.FactorCompra);
                prm_Params[6] = new Parameter("@FVenta", objBETipoCambio.FactorVenta);
                prm_Params[7] = new Parameter("@FecPromedio", objBETipoCambio.FactorPromedio);
                prm_Params[8] = new Parameter("@FacCompraAfp", objBETipoCambio.FactorCompraAfp);
                prm_Params[9] = new Parameter("@FacVentaAfp", objBETipoCambio.FactorVentaAfp);
                prm_Params[10] = new Parameter("@FacCompraSBS", objBETipoCambio.FactorCompraSBS);
                prm_Params[11] = new Parameter("@FacVentaSBS", objBETipoCambio.FactorVentaSBS);
                prm_Params[12] = new Parameter("@ValorCuota", objBETipoCambio.ValorCuota);
                prm_Params[13] = new Parameter("@TasaTamex", objBETipoCambio.TasaTAMEX);
                prm_Params[14] = new Parameter("@TasaTamn", objBETipoCambio.TasaTAMN);
                prm_Params[15] = new Parameter("@TasaAnualTamex", objBETipoCambio.TasaAnualTAMEX);
                prm_Params[16] = new Parameter("@TasaAnualTamn", objBETipoCambio.TasaAnualTAMN);
                prm_Params[17] = new Parameter("@Estado", objBETipoCambio.Estado);
                prm_Params[18] = new Parameter("@FactorCobranza", objBETipoCambio.FactorCobranza);
                prm_Params[19] = new Parameter("@FactorCobranzaVenta", objBETipoCambio.FactorCobranzaVenta);
                dop_Operacion.Parameters.AddRange(prm_Params);
                DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public int Actualizar(WCO_ListarTipoCambioMo_Result objBETipoCambio)
        {
            try
            {
                DataOperation dop_Operacion = new DataOperation("WCO_ActualizarTipoCambio");
                Parameter[] prm_Params = new Parameter[18];
                prm_Params[0] = new Parameter("@UltimoUsuario", objBETipoCambio.FechaCambio);
                prm_Params[1] = new Parameter("@PFecha", objBETipoCambio.FechaCambio);
                prm_Params[2] = new Parameter("@Factor", objBETipoCambio.Factor);
                prm_Params[3] = new Parameter("@Fcompra", objBETipoCambio.FactorCompra);
                prm_Params[4] = new Parameter("@FVenta", objBETipoCambio.FactorVenta);
                prm_Params[5] = new Parameter("@FecPromedio", objBETipoCambio.FactorPromedio);
                prm_Params[6] = new Parameter("@FacCompraAfp", objBETipoCambio.FactorCompraAfp);
                prm_Params[7] = new Parameter("@FacVentaAfp", objBETipoCambio.FactorVentaAfp);
                prm_Params[8] = new Parameter("@FacCompraSBS", objBETipoCambio.FactorCompraSBS);
                prm_Params[9] = new Parameter("@FacVentaSBS", objBETipoCambio.FactorVentaSBS);
                prm_Params[10] = new Parameter("@ValorCuota", objBETipoCambio.ValorCuota);
                prm_Params[11] = new Parameter("@TasaTamex", objBETipoCambio.TasaTAMEX);
                prm_Params[12] = new Parameter("@TasaTamn", objBETipoCambio.TasaTAMN);
                prm_Params[13] = new Parameter("@TasaAnualTamex", objBETipoCambio.TasaAnualTAMEX);
                prm_Params[14] = new Parameter("@TasaAnualTamn", objBETipoCambio.TasaAnualTAMN);
                prm_Params[15] = new Parameter("@Estado", objBETipoCambio.Estado);
                prm_Params[16] = new Parameter("@FactorCobranza", objBETipoCambio.FactorCobranza);
                prm_Params[17] = new Parameter("@FactorCobranzaVenta", objBETipoCambio.FactorCobranzaVenta);
                dop_Operacion.Parameters.AddRange(prm_Params);
                DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Inactivar(WCO_ListarTipoCambioMo_Result objBETipoCambio)
        {
            try
            {
                DataOperation dop_Operacion = new DataOperation("WCO_InactivarTipocambioMoneda");
                Parameter[] prm_Params = new Parameter[2];
                prm_Params[0] = new Parameter("@PFecha", objBETipoCambio.FechaCambio);
                prm_Params[1] = new Parameter("@UsuarioModificacion", objBETipoCambio.UltimoUsuario);
                dop_Operacion.Parameters.AddRange(prm_Params);
                DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static bool ValidarExisteCambioActual(WCO_ListarTipoCambioMo_Result objBETipoCambio)
        {
            DataOperation dop_DataOperation = new DataOperation("WCO_EXISTECambioActual");
            Parameter[] prm_Params = new Parameter[2];
            prm_Params[0] = new Parameter("@PFechaID", objBETipoCambio.FechaCambio);
            prm_Params[1] = new Parameter("@flagSalida", DbType.Int32);
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