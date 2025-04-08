using RoyalSystems.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebApiServices.Models.Datos
{
    public class ADDAT_Cobranza
    {

        public List<WCO_ListarCobranza_Result> WCO_ListarCobranza(WCO_ListarCobranza_Result ObjEntidad)
        {
            List<WCO_ListarCobranza_Result> lst = new List<WCO_ListarCobranza_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarCobranza(ObjEntidad.IdCobranza, ObjEntidad.IdComprobante, ObjEntidad.NumeroComprobante,
                    ObjEntidad.TipoComprobante, ObjEntidad.FechaIngreso, ObjEntidad.FechaPago, ObjEntidad.Estado,
                    ObjEntidad.SerieComprobante, ObjEntidad.IdClienteFacturacion, ObjEntidad.Sucursal, ObjEntidad.DocumentoCliente,
                    ObjEntidad.ClasificadorMovimiento, ObjEntidad.Compania, ObjEntidad.UsuarioCreacion).ToList();
            }
            return lst;
        }

        public List<WCO_ListarCobranzaDetalleId_Result> WCO_ListarCobranzaDetalle(WCO_ListarCobranza_Result ObjEntidad)
        {
            List<WCO_ListarCobranzaDetalleId_Result> lst = new List<WCO_ListarCobranzaDetalleId_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarCobranzaDetalleId(ObjEntidad.IdCobranza, ObjEntidad.IdComprobante, ObjEntidad.NumeroComprobante,
                ObjEntidad.TipoComprobante, ObjEntidad.FechaIngreso, ObjEntidad.FechaPago, ObjEntidad.Estado,
                ObjEntidad.SerieComprobante, ObjEntidad.IdClienteFacturacion, ObjEntidad.Sucursal, ObjEntidad.DocumentoCliente,
                ObjEntidad.ClasificadorMovimiento, ObjEntidad.Compania, ObjEntidad.IdAdmision, ObjEntidad.IdEstablecimiento, ObjEntidad.UsuarioCreacion).ToList();
            }
            return lst;
        }

        public int InsertarCabecera(WCO_ListarCobranza_Result objBEReferencia)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InsertarCobranza");
            Parameter[] prm_Params = new Parameter[23];
            prm_Params[0] = new Parameter("@IdCobranza", DbType.Int32);
            prm_Params[1] = new Parameter("@TipoCobranza", objBEReferencia.TipoCobranza);
            prm_Params[2] = new Parameter("@SerieCobranza", objBEReferencia.SerieCobranza);
            prm_Params[3] = new Parameter("@NumeroCobranza", objBEReferencia.NumeroCobranza);
            prm_Params[4] = new Parameter("@IdVendedor", objBEReferencia.IdVendedor);
            prm_Params[5] = new Parameter("@Compania", objBEReferencia.Compania);
            prm_Params[6] = new Parameter("@Sucursal", objBEReferencia.Sucursal);
            prm_Params[7] = new Parameter("@NumeroCaja", objBEReferencia.NumeroCaja);
            prm_Params[8] = new Parameter("@Cajero", objBEReferencia.Cajero);
            prm_Params[9] = new Parameter("@IdCliente", objBEReferencia.IdCliente);
            prm_Params[10] = new Parameter("@FechaIngreso", objBEReferencia.FechaIngreso);
            prm_Params[11] = new Parameter("@NumeroDeposito", objBEReferencia.NumeroDeposito);
            prm_Params[12] = new Parameter("@Banco", objBEReferencia.Banco);
            prm_Params[13] = new Parameter("@Moneda", objBEReferencia.Moneda);
            prm_Params[14] = new Parameter("@TipoCambio", objBEReferencia.TipoCambio);
            prm_Params[15] = new Parameter("@Monto", objBEReferencia.Monto);
            prm_Params[16] = new Parameter("@Saldo", objBEReferencia.Saldo);
            prm_Params[17] = new Parameter("@Estado", objBEReferencia.Estado);
            prm_Params[18] = new Parameter("@UsuarioCreacion", objBEReferencia.UsuarioCreacion);
            prm_Params[19] = new Parameter("@IdComprobante", objBEReferencia.IdComprobante);
            prm_Params[20] = new Parameter("@PeriodoEmision", objBEReferencia.PeriodoEmision);
            prm_Params[21] = new Parameter("@Observacion", objBEReferencia.Observacion);
            prm_Params[22] = new Parameter("@FechaPago", objBEReferencia.FechaPago);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            objBEReferencia.IdCobranza = int.Parse(dop_Operacion.GetParameterByName("@IdCobranza").Value.ToString());
            return int.Parse(dop_Operacion.GetParameterByName("@IdCobranza").Value.ToString());
        }


        public int InsertarDetalle(WCO_ListarCobranzaDetalleId_Result objBEReferenciaDetalle)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InsertarCobranzaDetalle");
            Parameter[] prm_Params = new Parameter[12];
            prm_Params[0] = new Parameter("@IdCobranza", objBEReferenciaDetalle.IdCobranza);
            prm_Params[1] = new Parameter("@IdSecuencia", objBEReferenciaDetalle.Secuencial);
            prm_Params[2] = new Parameter("@FechaPago", objBEReferenciaDetalle.FechaPago);
            prm_Params[3] = new Parameter("@FormaPago", objBEReferenciaDetalle.FormaPago);
            prm_Params[4] = new Parameter("@Moneda", objBEReferenciaDetalle.Moneda);
            prm_Params[5] = new Parameter("@TipoCambio", objBEReferenciaDetalle.TipoCambio);
            prm_Params[6] = new Parameter("@Monto", objBEReferenciaDetalle.Monto);
            prm_Params[7] = new Parameter("@Banco", objBEReferenciaDetalle.Banco);
            prm_Params[8] = new Parameter("@NumeroTarjeta", objBEReferenciaDetalle.NumeroTarjeta);
            prm_Params[9] = new Parameter("@Estado", objBEReferenciaDetalle.Estado);
            prm_Params[10] = new Parameter("@UsuarioCreacion", objBEReferenciaDetalle.UsuarioCreacion);
            prm_Params[11] = new Parameter("@IdComprobante", objBEReferenciaDetalle.IdComprobante);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            return int.Parse(dop_Operacion.GetParameterByName("@IdSecuencia").Value.ToString());
        }

        public void Anular(WCO_ListarCobranza_Result objBECobranza)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InactivarCobranza");
            Parameter[] prm_Params = new Parameter[3];
            prm_Params[0] = new Parameter("@IdCobranza", objBECobranza.IdCobranza);
            prm_Params[1] = new Parameter("@Estado", objBECobranza.Estado);
            prm_Params[2] = new Parameter("@UsuarioModificacion", objBECobranza.UsuarioModificacion);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }
    }
}