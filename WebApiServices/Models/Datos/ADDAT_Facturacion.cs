using RoyalSystems.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiServices.Models.Datos
{
    public class ADDAT_Facturacion
    {

        public List<WCO_ListarMonedaMast_Result> ListarMonedaMast(WCO_ListarMonedaMast_Result ObjEntidad)
        {
            List<WCO_ListarMonedaMast_Result> lst = new List<WCO_ListarMonedaMast_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarMonedaMast(ObjEntidad.Estado).ToList();
            }
            return lst;
        }

        public List<WCO_ListarBanco_Result> ListarBanco(WCO_ListarBanco_Result ObjEntidad)
        {
            List<WCO_ListarBanco_Result> lst = new List<WCO_ListarBanco_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarBanco(ObjEntidad.Banco, ObjEntidad.Estado, ObjEntidad.DescripcionCorta).ToList();
            }
            return lst;
        }

        public List<WCO_ListarComprobanteId_Result> ListarComprobante(WCO_ListarComprobanteId_Result ObjEntidad)
        {
            List<WCO_ListarComprobanteId_Result> lst = new List<WCO_ListarComprobanteId_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarComprobanteId(1, ObjEntidad.IdComprobante, ObjEntidad.NumeroComprobante, ObjEntidad.TipoComprobante,
                    ObjEntidad.FechaEmision, ObjEntidad.FechaVencimiento, ObjEntidad.Estado, ObjEntidad.SerieComprobante, ObjEntidad.IdClienteFacturacion,
                    ObjEntidad.Sucursal, ObjEntidad.DocumentoCliente, ObjEntidad.ClasificadorMovimiento, ObjEntidad.Compania, 
                    ObjEntidad.ConceptoFacturacion, ObjEntidad.EstadoSunatElectronico).ToList();
            }
            return lst;
        }


        public List<WCO_ListarComprobanteDetalleId_Result> ListarComprobanteDetalle(WCO_ListarComprobanteDetalleId_Result ObjEntidad)
        {
            List<WCO_ListarComprobanteDetalleId_Result> lst = new List<WCO_ListarComprobanteDetalleId_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarComprobanteDetalleId( ObjEntidad.IdComprobante, ObjEntidad.IdLinea, ObjEntidad.Estado, ObjEntidad.Componente).ToList();
            }
            return lst;
        }

        ///<summary>Método para inserta el registro en la tabla Comprobante.</summary>
        ///<param name="obj_Comprobante">Entidad de Negocio</param>
        ///<returns>Retorna el BE_Comprobante.</returns>
        ///<remarks><list type="bullet">
        ///<item><CreadoPor>Creado por Jordan Mateo Pizarro</CreadoPor></item>
        ///<item><FecCrea>17/01/2015</FecCrea></item></list></remarks>
        public int InsertarCabecera(WCO_ListarComprobanteId_Result BE_pComprobante)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InsertarComprobante");
            Parameter[] prm_Params = new Parameter[43];
            prm_Params[0] = new Parameter("@CompaniaCodigo", BE_pComprobante.Compania);
            prm_Params[1] = new Parameter("@IdComprobante", BE_pComprobante.IdComprobante);
            prm_Params[2] = new Parameter("@NumeroComprobante", BE_pComprobante.NumeroComprobante);
            prm_Params[3] = new Parameter("@TipoComprobante", BE_pComprobante.TipoComprobante);
            prm_Params[4] = new Parameter("@TipoVenta", BE_pComprobante.TipoVenta);
            prm_Params[5] = new Parameter("@Serie", BE_pComprobante.SerieComprobante);
            prm_Params[6] = new Parameter("@IdSede", BE_pComprobante.IdSede);
            prm_Params[7] = new Parameter("@IdPersona", BE_pComprobante.IdClienteFacturacion);
            prm_Params[8] = new Parameter("@Documento", BE_pComprobante.DocumentoCliente);
            prm_Params[9] = new Parameter("@Descripcion", BE_pComprobante.CampoReferencia);
            prm_Params[10] = new Parameter("@ClasificadorMovimiento", BE_pComprobante.ClasificadorMovimiento);
            prm_Params[11] = new Parameter("@Moneda", BE_pComprobante.Moneda);
            prm_Params[12] = new Parameter("@Periodo", BE_pComprobante.PeriodoEmision);
            prm_Params[13] = new Parameter("@FormaPago", BE_pComprobante.FormaPago);
            prm_Params[14] = new Parameter("@TipoCambio", BE_pComprobante.TipoCambio);
            prm_Params[15] = new Parameter("@MontoImpuestoVentas", BE_pComprobante.MontoImpuestoVentas);
            prm_Params[16] = new Parameter("@MontoAfecto", BE_pComprobante.MontoAfecto);
            prm_Params[17] = new Parameter("@MontoTotal", BE_pComprobante.MontoTotal);
            prm_Params[18] = new Parameter("@ValorImpuesto", BE_pComprobante.ValorImpuesto);
            prm_Params[19] = new Parameter("@FechaEmision", BE_pComprobante.FechaEmision);
            prm_Params[20] = new Parameter("@FechaVencimiento", BE_pComprobante.FechaVencimiento);
            prm_Params[21] = new Parameter("@IdDocRelacionado", BE_pComprobante.IdDocumentoRelacionado);
            prm_Params[22] = new Parameter("@DocumentoRelacionado", BE_pComprobante.DocumentoRelacionado);
            prm_Params[23] = new Parameter("@TipoComprobanteRelacionado", BE_pComprobante.TipoDocumentoRelacionado);
            prm_Params[24] = new Parameter("@SerieRelacionado", BE_pComprobante.SerieDocumentoRelacionado);
            prm_Params[25] = new Parameter("@Estado", BE_pComprobante.Estado);
            prm_Params[26] = new Parameter("@UsuarioCreacion", BE_pComprobante.UsuarioCreacion);
            prm_Params[27] = new Parameter("@Sucursal", BE_pComprobante.Sucursal);
            prm_Params[28] = new Parameter("@MontoDescuentos", BE_pComprobante.MontoDescuentos);
            prm_Params[29] = new Parameter("@MontoPagado", BE_pComprobante.MontoPagado);
            prm_Params[30] = new Parameter("@MontoAdelanto", BE_pComprobante.MontoAdelanto);
            prm_Params[31] = new Parameter("@MontoAdelantoSaldo", BE_pComprobante.MontoAdelantoSaldo);
            prm_Params[32] = new Parameter("@MontoRedondeo", BE_pComprobante.MontoRedondeo);
            prm_Params[33] = new Parameter("@Saldo", BE_pComprobante.Saldo);
            prm_Params[34] = new Parameter("@ConceptoFacturacion", BE_pComprobante.ConceptoFacturacion);
            prm_Params[35] = new Parameter("@MotivoAnulacion", BE_pComprobante.MotivoAnulacion);
            prm_Params[36] = new Parameter("@TipoMotivo", BE_pComprobante.TipoMotivo);
            prm_Params[37] = new Parameter("@TipoImpuesto", BE_pComprobante.TipoImpuesto);
            prm_Params[38] = new Parameter("@TipoMedioPago", BE_pComprobante.TipoMedioPago);
            prm_Params[39] = new Parameter("@MontoRetencion", BE_pComprobante.MontoRetencion);
            prm_Params[40] = new Parameter("@PorcentajeRetencion", BE_pComprobante.PorcentajeRetencion);
            prm_Params[41] = new Parameter("@MontoDetraccion", BE_pComprobante.MontoDetraccion);
            prm_Params[42] = new Parameter("@PorcentajeDetraccion", BE_pComprobante.PorcentajeDetraccion);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            return int.Parse(dop_Operacion.GetParameterByName("@IdComprobante").Value.ToString());
        }

        ///<summary>Método para actualizar el registro en la tabla Comprobante.</summary>
        ///<param name="obj_Comprobante">Entidad de Negocio</param>
        /// ///<returns>Retorna el BE_Comprobante.</returns>
        ///<remarks><list type="bullet">
        ///<item><CreadoPor>Creado por Jordan Mateo Pizarro</CreadoPor></item>
        ///<item><FecCrea>17/01/2015</FecCrea></item></list></remarks>
        public int ActualizarCabecera(WCO_ListarComprobanteId_Result BE_pComprobante)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_ActualizarComprobante");
            Parameter[] prm_Params = new Parameter[32];
            prm_Params[0] = new Parameter("@CompaniaCodigo", BE_pComprobante.Compania);
            prm_Params[1] = new Parameter("@IdComprobante", BE_pComprobante.IdComprobante);
            prm_Params[2] = new Parameter("@NumeroComprobante", BE_pComprobante.NumeroComprobante);
            prm_Params[3] = new Parameter("@TipoComprobante", BE_pComprobante.TipoComprobante);
            prm_Params[4] = new Parameter("@TipoVenta", BE_pComprobante.TipoVenta);
            prm_Params[5] = new Parameter("@Serie", BE_pComprobante.SerieComprobante);
            prm_Params[6] = new Parameter("@IdSede", BE_pComprobante.IdSede);
            prm_Params[7] = new Parameter("@IdPersona", BE_pComprobante.IdClienteFacturacion);
            prm_Params[8] = new Parameter("@Documento", BE_pComprobante.DocumentoCliente);
            prm_Params[9] = new Parameter("@Descripcion", BE_pComprobante.CampoReferencia);
            prm_Params[10] = new Parameter("@ClasificadorMovimiento", BE_pComprobante.ClasificadorMovimiento);
            prm_Params[11] = new Parameter("@Moneda", BE_pComprobante.Moneda);
            prm_Params[12] = new Parameter("@Periodo", BE_pComprobante.PeriodoEmision);
            prm_Params[13] = new Parameter("@FormaPago", BE_pComprobante.FormaPago);
            prm_Params[14] = new Parameter("@TipoCambio", BE_pComprobante.TipoCambio);
            prm_Params[15] = new Parameter("@MontoImpuestoVentas", BE_pComprobante.MontoImpuestoVentas);
            prm_Params[16] = new Parameter("@MontoAfecto", BE_pComprobante.MontoAfecto);
            prm_Params[17] = new Parameter("@MontoTotal", BE_pComprobante.MontoTotal);
            prm_Params[18] = new Parameter("@ValorImpuesto", BE_pComprobante.ValorImpuesto);
            prm_Params[19] = new Parameter("@FechaEmision", BE_pComprobante.FechaEmision);
            prm_Params[20] = new Parameter("@FechaVencimiento", BE_pComprobante.FechaVencimiento);
            prm_Params[21] = new Parameter("@Estado", BE_pComprobante.Estado);
            prm_Params[22] = new Parameter("@UsuarioModificacion", BE_pComprobante.UsuarioModificacion);
            prm_Params[23] = new Parameter("@ConceptoFacturacion", BE_pComprobante.ConceptoFacturacion);
            prm_Params[24] = new Parameter("@MotivoAnulacion", BE_pComprobante.MotivoAnulacion);
            prm_Params[25] = new Parameter("@TipoMotivo", BE_pComprobante.TipoMotivo);
            prm_Params[26] = new Parameter("@TipoImpuesto", BE_pComprobante.TipoImpuesto);
            prm_Params[27] = new Parameter("@TipoMedioPago", BE_pComprobante.TipoMedioPago);
            prm_Params[28] = new Parameter("@MontoRetencion", BE_pComprobante.MontoRetencion);
            prm_Params[29] = new Parameter("@PorcentajeRetencion", BE_pComprobante.PorcentajeRetencion);
            prm_Params[30] = new Parameter("@MontoDetraccion", BE_pComprobante.MontoDetraccion);
            prm_Params[31] = new Parameter("@PorcentajeDetraccion", BE_pComprobante.PorcentajeDetraccion);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            return BE_pComprobante.IdComprobante;
        }

        ///<summary>Método para actualizar el registro en la tabla Comprobante.</summary>
        ///<param name="obj_Comprobante">Entidad de Negocio</param>
        /// ///<returns>Retorna el BE_Comprobante.</returns>
        ///<remarks><list type="bullet">
        ///<item><CreadoPor>Creado por Jordan Mateo Pizarro</CreadoPor></item>
        ///<item><FecCrea>17/01/2015</FecCrea></item></list></remarks>
        public void ActualizarEstadoCabecera(WCO_ListarComprobanteId_Result BE_pComprobante)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_ActualizarComprobanteEstado");
            Parameter[] prm_Params = new Parameter[13];
            prm_Params[0] = new Parameter("@CompaniaCodigo", BE_pComprobante.Compania);
            prm_Params[1] = new Parameter("@IdComprobante", BE_pComprobante.IdComprobante);
            prm_Params[2] = new Parameter("@EstadoEnvioElectronico", BE_pComprobante.EstadoEnvioElectronico);
            prm_Params[3] = new Parameter("@CodigoHashElectronico", BE_pComprobante.CodigoHashElectronico);
            prm_Params[4] = new Parameter("@EstadoSunatElectronico", BE_pComprobante.EstadoSunatElectronico);
            prm_Params[5] = new Parameter("@DescripcionEstadoSunatElectronico", BE_pComprobante.DescripcionEstadoSunatElectronico);
            prm_Params[6] = new Parameter("@IdCorrelativoRSBV", BE_pComprobante.IdCorrelativoRSBV);
            prm_Params[7] = new Parameter("@FechaCreaciónRSBV", BE_pComprobante.FechaCreaciónRSBV);
            prm_Params[8] = new Parameter("@IdCorrelativoCB", BE_pComprobante.IdCorrelativoCB);
            prm_Params[9] = new Parameter("@FechaCreaciónCB", BE_pComprobante.FechaCreaciónCB);
            prm_Params[10] = new Parameter("@IdDocRelacionado", BE_pComprobante.IdDocumentoRelacionado);
            prm_Params[11] = new Parameter("@Estado", BE_pComprobante.Estado);
            prm_Params[12] = new Parameter("@UsuarioModificacion", BE_pComprobante.UsuarioModificacion);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }

        public void InactivarCabecera(WCO_ListarComprobanteId_Result objBEComprobante)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InactivarComprobante");
            Parameter[] prm_Params = new Parameter[5];
            prm_Params[0] = new Parameter("@CompaniaCodigo", objBEComprobante.Compania);
            prm_Params[1] = new Parameter("@IdComprobante", objBEComprobante.IdComprobante);
            prm_Params[2] = new Parameter("@NumeroComprobante", objBEComprobante.NumeroComprobante);
            prm_Params[3] = new Parameter("@Estado", objBEComprobante.Estado);
            prm_Params[4] = new Parameter("@UsuarioModificacion", objBEComprobante.UsuarioModificacion);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }


        ///<summary>Método para inserta el registro en la tabla Comprobante.</summary>
        ///<param name="obj_ComprobanteDetalle">Entidad de Negocio</param>
        ///<returns>Retorna el BE_ComprobanteDetalle.</returns>
        ///<remarks><list type="bullet">
        ///<item><CreadoPor>Creado por Jordan Mateo Pizarro</CreadoPor></item>
        ///<item><FecCrea>17/01/2015</FecCrea></item></list></remarks>
        public int InsertarDetalle(WCO_ListarComprobanteDetalleId_Result BE_pComprobanteDetalle)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InsertarComprobanteDetalle");
            Parameter[] prm_Params = new Parameter[22];
            prm_Params[0] = new Parameter("@CompaniaCodigo", BE_pComprobanteDetalle.Compania);
            prm_Params[1] = new Parameter("@IdComprobante", BE_pComprobanteDetalle.IdComprobante);
            prm_Params[2] = new Parameter("@IdLinea", BE_pComprobanteDetalle.IdLinea);
            prm_Params[3] = new Parameter("@ClasificadorMovimiento", BE_pComprobanteDetalle.ClasificadorMovimiento);
            prm_Params[4] = new Parameter("@Moneda", BE_pComprobanteDetalle.Moneda);
            prm_Params[5] = new Parameter("@Periodo", BE_pComprobanteDetalle.PeriodoEmision);
            prm_Params[6] = new Parameter("@TipoCambio", BE_pComprobanteDetalle.TipoCambio);
            prm_Params[7] = new Parameter("@MontoImpuestoVentas", BE_pComprobanteDetalle.MontoImpuestoVentas);
            prm_Params[8] = new Parameter("@MontoAfecto", BE_pComprobanteDetalle.MontoAfecto);
            prm_Params[9] = new Parameter("@MontoTotal", BE_pComprobanteDetalle.MontoTotal);
            prm_Params[10] = new Parameter("@ValorImpuesto", BE_pComprobanteDetalle.MontoImpuestos);
            prm_Params[11] = new Parameter("@Cantidad", BE_pComprobanteDetalle.Cantidad);
            prm_Params[12] = new Parameter("@IdExpediente", BE_pComprobanteDetalle.IdExpediente);
            prm_Params[13] = new Parameter("@ConceptoGasto", BE_pComprobanteDetalle.ConceptoGasto);
            prm_Params[14] = new Parameter("@Estado", BE_pComprobanteDetalle.Estado);
            prm_Params[15] = new Parameter("@UsuarioCreacion", BE_pComprobanteDetalle.UsuarioCreacion);
            prm_Params[16] = new Parameter("@CodigoComponente", BE_pComprobanteDetalle.Componente);
            prm_Params[17] = new Parameter("@TipoComprobante", BE_pComprobanteDetalle.TipoComprobante);
            prm_Params[18] = new Parameter("@Sucursal", BE_pComprobanteDetalle.Sucursal);
            prm_Params[19] = new Parameter("@NumeroComprobante", BE_pComprobanteDetalle.NumeroComprobante);
            prm_Params[20] = new Parameter("@IdPedido", BE_pComprobanteDetalle.IdPedido);
            prm_Params[21] = new Parameter("@InventarioActual", BE_pComprobanteDetalle.InventarioActual);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            return int.Parse(dop_Operacion.GetParameterByName("@IdLinea").Value.ToString());
        }

        ///<summary>Método para actualizar el registro en la tabla Comprobante.</summary>
        ///<param name="obj_ComprobanteDetalle">Entidad de Negocio</param>
        /// ///<returns>Retorna el BE_ComprobanteDetalle.</returns>
        ///<remarks><list type="bullet">
        ///<item><CreadoPor>Creado por Jordan Mateo Pizarro</CreadoPor></item>
        ///<item><FecCrea>17/01/2015</FecCrea></item></list></remarks>
        public void ActualizarDetalle(WCO_ListarComprobanteDetalleId_Result BE_pComprobanteDetalle)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_ActualizarComprobanteDetalle");
            Parameter[] prm_Params = new Parameter[20];
            prm_Params[0] = new Parameter("@CompaniaCodigo", BE_pComprobanteDetalle.Compania);
            prm_Params[1] = new Parameter("@IdComprobante", BE_pComprobanteDetalle.IdComprobante);
            prm_Params[2] = new Parameter("@IdLinea", BE_pComprobanteDetalle.IdLinea);
            prm_Params[3] = new Parameter("@ClasificadorMovimiento", BE_pComprobanteDetalle.ClasificadorMovimiento);
            prm_Params[4] = new Parameter("@Moneda", BE_pComprobanteDetalle.Moneda);
            prm_Params[5] = new Parameter("@Periodo", BE_pComprobanteDetalle.PeriodoEmision);
            prm_Params[6] = new Parameter("@TipoCambio", BE_pComprobanteDetalle.TipoCambio);
            prm_Params[7] = new Parameter("@MontoImpuestoVentas", BE_pComprobanteDetalle.MontoImpuestoVentas);
            prm_Params[8] = new Parameter("@MontoAfecto", BE_pComprobanteDetalle.MontoAfecto);
            prm_Params[9] = new Parameter("@MontoTotal", BE_pComprobanteDetalle.MontoTotal);
            prm_Params[10] = new Parameter("@ValorImpuesto", BE_pComprobanteDetalle.MontoImpuestos);
            prm_Params[11] = new Parameter("@Cantidad", BE_pComprobanteDetalle.Cantidad);
            prm_Params[12] = new Parameter("@IdExpediente", BE_pComprobanteDetalle.IdExpediente);
            prm_Params[13] = new Parameter("@ConceptoGasto", BE_pComprobanteDetalle.ConceptoGasto);
            prm_Params[14] = new Parameter("@Estado", BE_pComprobanteDetalle.Estado);
            prm_Params[15] = new Parameter("@UsuarioModificacion", BE_pComprobanteDetalle.UsuarioModificacion);
            prm_Params[16] = new Parameter("@CodigoComponente", BE_pComprobanteDetalle.Componente);
            prm_Params[17] = new Parameter("@TipoComprobante", BE_pComprobanteDetalle.TipoComprobante);
            prm_Params[18] = new Parameter("@Sucursal", BE_pComprobanteDetalle.Sucursal);
            prm_Params[19] = new Parameter("@DocumentoRelacionado", BE_pComprobanteDetalle.DocumentoRelacionado);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }

    }
}