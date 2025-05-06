using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.UI;
using System.Web.Http;
using WebApiServices.Models;
using WebApiServices.Models.Request;
using WebApiServices.Models.Response;
using WebApiServices.Models.Entidades;
using WebApiServices.Models.Datos;
using System.Threading.Tasks;
using System.Transactions;

namespace WebApiServices.Controllers
{
    public class FacturacionController : ApiController
    {

        #region MaestroConfiguracion

        [Route("api/Facturacion/ListarConfiguracion")]
        public IEnumerable<WCO_ListarConfiguracionCorreo_Result> ListarConfiguracion(WCO_ListarConfiguracionCorreo_Result ObjDetalle)
        {
            Metodos m = new Metodos();
            List<WCO_ListarConfiguracionCorreo_Result> lst = new List<WCO_ListarConfiguracionCorreo_Result>();
            try
            {
                lst = m.ListarConfiguracion(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        #endregion

        #region Maestros

        [HttpPost]
        [Route("api/Facturacion/ListarMoneda")]
        public IEnumerable<WCO_ListarMonedaMast_Result> ListarMoneda(WCO_ListarMonedaMast_Result ObjDetalle)
        {
            ADDAT_Facturacion fac = new ADDAT_Facturacion();
            List<WCO_ListarMonedaMast_Result> lst = new List<WCO_ListarMonedaMast_Result>();
            try
            {
                lst = fac.ListarMonedaMast(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        [HttpPost]
        [Route("api/Facturacion/ListarBanco")]
        public IEnumerable<WCO_ListarBanco_Result> ListarBanco(WCO_ListarBanco_Result ObjDetalle)
        {
            ADDAT_Facturacion fac = new ADDAT_Facturacion();
            List<WCO_ListarBanco_Result> lst = new List<WCO_ListarBanco_Result>();
            try
            {
                lst = fac.ListarBanco(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        [HttpPost]
        [Route("api/Facturacion/ListarTipoCambioComercial")]
        public IEnumerable<WCO_ListarTipoCambioComercial_Result> ListarTipoCambioComercial(WCO_ListarTipoCambioComercial_Result ObjDetalle)
        {
            ADDAT_TipoCambioComercial TCC = new ADDAT_TipoCambioComercial();
            List<WCO_ListarTipoCambioComercial_Result> lst = new List<WCO_ListarTipoCambioComercial_Result>();
            try
            {
                lst = TCC.ListarTipoCambioComercial(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        [HttpPost]
        [Route("api/Facturacion/ListarTipoPago")]
        public IEnumerable<WCO_ListarTipoPago_Result> ListarTipoPago(WCO_ListarTipoPago_Result ObjDetalle)
        {
            ADDAT_TipoPago TipPago = new ADDAT_TipoPago();
            List<WCO_ListarTipoPago_Result> lst = new List<WCO_ListarTipoPago_Result>();
            try
            {
                lst = TipPago.ListarTipoPago(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        [HttpPost]
        [Route("api/Facturacion/ListarSedeSucursalNegocio")]
        public IEnumerable<WCO_ListarSedeSucursalNegocio_Result> ListarSedeSucursalNegocio(WCO_ListarSedeSucursalNegocio_Result ObjDetalle)
        {
            ADDAT_Sede sed = new ADDAT_Sede();
            List<WCO_ListarSedeSucursalNegocio_Result> lst = new List<WCO_ListarSedeSucursalNegocio_Result>();
            try
            {
                lst = sed.WCO_ListarSedeSucursalNegocio(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        [HttpPost]
        [Route("api/Facturacion/ListarSucursalCompSerie")]
        public IEnumerable<WCO_ListarSucursalCompSerie_Result> ListarSucursalCompSerie(WCO_ListarSedeSucursalNegocio_Result ObjDetalle)
        {
            ADDAT_Sede sed = new ADDAT_Sede();
            List<WCO_ListarSucursalCompSerie_Result> lst = new List<WCO_ListarSucursalCompSerie_Result>();
            try
            {
                lst = sed.WCO_ListarSucursalCompSerie(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        [HttpPost]
        [Route("api/Facturacion/ListarHomologacionCliente")]
        public IEnumerable<WCO_ListarHomologacionCliente_Result> ListarHomologacionCliente(WCO_ListarHomologacionCliente_Result ObjDetalle)
        {
            ADDAT_PersonaMast per = new ADDAT_PersonaMast();
            List<WCO_ListarHomologacionCliente_Result> lst = new List<WCO_ListarHomologacionCliente_Result>();
            try
            {
                lst = per.ListarHomologacionCliente(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        [Authorize]
        [HttpPost]
        [Route("api/Facturacion/MantenimientoHomologacionCliente/{id}")]
        public async Task<IHttpActionResult> MantenimientoHomologacionCliente(int id, [FromBody] WCO_ListarHomologacionCliente_Result ObjCabecera)
        {
            ViewModalExite objLogin = new ViewModalExite();
            HttpStatusCode statusCode = new HttpStatusCode();
            ADDAT_PersonaMast per = new ADDAT_PersonaMast();
            try
            {
                Nullable<int> valor = 0;
                if (id == 1)
                {
                    valor = per.InsertarHomologacionCliente(ObjCabecera);
                    if (valor < 0)
                    {
                        objLogin.success = false;
                        objLogin.valor = valor;
                        objLogin.mensaje = "Los campos ingresados coinciden con un registro en nuestra base. Por favor ingrese un nuevo valor";
                        objLogin.data = null;
                        statusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        objLogin.success = true;
                        objLogin.valor = valor;
                        objLogin.mensaje = "Created";
                        objLogin.data = ObjCabecera;
                        statusCode = HttpStatusCode.Created;
                    }
                }
                else
                {
                    return BadRequest();
                }
                return Content(statusCode, objLogin);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, new ViewModalExite() { success = false, mensaje = ex.Message, valor = -1 });
            }
        }

        #endregion

        #region CorrelativosMast

        [HttpPost]
        [Route("api/Facturacion/ListarCorrelativos")]
        public IEnumerable<WCO_ListarCorrelativosMast_Result> ListarCorrelativos(WCO_ListarCorrelativosMast_Result ObjDetalle)
        {
            ADDAT_CorrelativosMast per = new ADDAT_CorrelativosMast();
            List<WCO_ListarCorrelativosMast_Result> lst = new List<WCO_ListarCorrelativosMast_Result>();
            try
            {
                lst = per.ListarCorrelativos(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        [HttpPost]
        [Route("api/Facturacion/MantenimientoCorrelativos/{id}")]
        public async Task<IHttpActionResult> MantenimientoCorrelativos(int id, [FromBody] WCO_ListarCorrelativosMast_Result ObjDetalle)
        {
            ADDAT_CorrelativosMast per = new ADDAT_CorrelativosMast();
            HttpStatusCode statusCode = new HttpStatusCode();
            ViewModalExite response = new ViewModalExite();

            try
            {
                int valor = 0;
                switch (id)
                {
                    case 1:
                        valor = per.Insertar(ObjDetalle);
                        if (valor < 0)
                        {
                            response.success = false;
                            response.valor = valor;
                            response.mensaje = "Los campos ingresados coinciden con un registro en nuestra base. Por favor ingrese un nuevo valor";
                            response.data = null;
                            statusCode = HttpStatusCode.OK;
                        }
                        else
                        {
                            response.success = true;
                            response.valor = valor;
                            response.mensaje = "Se creó el nuevo correlativo";
                            response.data = ObjDetalle;
                            statusCode = HttpStatusCode.Created;
                        }
                        break;
                    case 2:
                        valor = per.Actualizar(ObjDetalle);
                        if (valor < 0)
                        {
                            response.success = false;
                            response.valor = valor;
                            response.mensaje = "Los campos ingresados coinciden con un registro en nuestra base. Por favor ingrese un nuevo valor";
                            statusCode = HttpStatusCode.OK;
                        }
                        else
                        {
                            response.success = true;
                            response.valor = 1;
                            response.mensaje = "Se actualizó el registro";
                            statusCode = HttpStatusCode.OK;
                        }
                        break;

                    case 3:
                        per.Inactivar(ObjDetalle);
                        response.success = true;
                        response.valor = 1;
                        response.mensaje = "Ok";
                        statusCode = HttpStatusCode.OK;
                        break;
                }
                return Content(statusCode, response);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, new ViewModalExite() { success = false, mensaje = ex.Message, valor = -1 });
            }
        }

        #endregion

        #region Produccion

        [HttpPost]
        [Route("api/Facturacion/ListarPeriodoEmision")]
        public IEnumerable<WCO_ListarPeriodoEmision_Result> ListarPeriodoEmision(WCO_ListarPeriodoEmision_Result ObjDetalle)
        {
            ADDAT_Produccion exp = new ADDAT_Produccion();
            List<WCO_ListarPeriodoEmision_Result> lst = new List<WCO_ListarPeriodoEmision_Result>();
            try
            {
                lst = exp.ListarPeriodoEmision(ObjDetalle);
            }
            catch (Exception ex)
            {
                string val = ex.Message;
            }
            return lst;
        }

        [HttpPost]
        [Route("api/Facturacion/ListarProduccion")]
        public IEnumerable<WCO_ListarProduccion_Result> ListarProduccion(WCO_ListarProduccion_Result ObjDetalle)
        {
            ADDAT_Produccion exp = new ADDAT_Produccion();
            List<WCO_ListarProduccion_Result> lst = new List<WCO_ListarProduccion_Result>();
            try
            {
                lst = exp.ListarProduccion(ObjDetalle);
            }
            catch (Exception ex)
            {
                string val = ex.Message;
            }
            return lst;
        }

        [HttpPost]
        [Route("api/Facturacion/ListarProduccionGeneral")]
        public IEnumerable<WCO_ListarProduccionGeneral_Result> ListarProduccionGeneral(WCO_ListarProduccionGeneral_Result ObjDetalle)
        {
            ADDAT_Produccion exp = new ADDAT_Produccion();
            List<WCO_ListarProduccionGeneral_Result> lst = new List<WCO_ListarProduccionGeneral_Result>();
            try
            {
                lst = exp.ListarProduccionGeneral(ObjDetalle);
            }
            catch (Exception ex)
            {
                string val = ex.Message;
            }
            return lst;
        }

        //[Authorize]
        [HttpPost]
        [Route("api/Facturacion/MantenimientoProduccion/{id}")]
        public async Task<IHttpActionResult> MantenimientoProduccion(int id, [FromBody] WCO_ListarProduccion_Result ObjDetalle)
        {
            ViewModalExite objLogin = new ViewModalExite();
            HttpStatusCode statusCode = new HttpStatusCode();
            ADDAT_Produccion m = new ADDAT_Produccion();
            try
            {
                int valor = 0;
                if (id == 1)
                {
                    valor = m.InsertarProduccion(ObjDetalle);
                    if (valor < 0)
                    {
                        objLogin.success = false;
                        objLogin.valor = valor;
                        if (valor == -2)
                        {
                            objLogin.mensaje = "Ya Existe el Nombre del Medico. Por favor ingrese un nuevo valor";
                        }
                        if (valor == -3)
                        {
                            objLogin.mensaje = "Ya Existe el código para el Médico. Por favor ingrese un nuevo valor";
                        }
                        if (valor == -4)
                        {
                            objLogin.mensaje = "Ya Existe el DNI para el Médico. Por favor ingrese un nuevo valor";
                        }
                        if (valor == -1)
                        {
                            objLogin.mensaje = "Los campos ingresados coinciden con un registro en nuestra base. Por favor ingrese un nuevo valor";
                        }
                        objLogin.data = null;
                        statusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        objLogin.success = true;
                        objLogin.valor = valor;
                        objLogin.mensaje = "Created";
                        objLogin.data = ObjDetalle;
                        statusCode = HttpStatusCode.Created;
                    }
                }
                else if (id == 2)
                {
                    valor = m.InsertarMasivo(ObjDetalle);
                    if (valor < 0)
                    {
                        objLogin.success = false;
                        objLogin.valor = valor;
                        if (valor == -2)
                        {
                            objLogin.mensaje = "Ya Existe el Nombre del Medico. Por favor ingrese un nuevo valor";
                        }
                        if (valor == -3)
                        {
                            objLogin.mensaje = "Ya Existe el código para el Médico. Por favor ingrese un nuevo valor";
                        }
                        if (valor == -4)
                        {
                            objLogin.mensaje = "Ya Existe el DNI para el Médico. Por favor ingrese un nuevo valor";
                        }
                        if (valor == -1)
                        {
                            objLogin.mensaje = "Los campos ingresados coinciden con un registro en nuestra base. Por favor ingrese un nuevo valor";
                        }
                        statusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        objLogin.success = true;
                        objLogin.valor = 1;
                        objLogin.mensaje = "Ok";
                        statusCode = HttpStatusCode.OK;
                    }
                }
                else if (id == 3)
                {
                    m.EliminarProduccion(ObjDetalle);
                    objLogin.success = true;
                    objLogin.valor = 1;
                    objLogin.mensaje = "Se elimino correctamente";
                    statusCode = HttpStatusCode.OK;
                }
                else if (id == 4)
                {
                    m.EliminarProduccionMasivo(ObjDetalle);
                    objLogin.success = true;
                    objLogin.valor = 1;
                    objLogin.mensaje = "Eliminación masiva realizada";
                    statusCode = HttpStatusCode.OK;
                }
                else
                {
                    return BadRequest();
                }
                return Content(statusCode, objLogin);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, new ViewModalExite() { success = false, mensaje = ex.Message, valor = -1 });
            }
        }

        #endregion

        #region Expediente

        [HttpPost]
        [Route("api/Facturacion/ListarExpedienteParticulares")]
        public IEnumerable<WCO_ListarExpedienteParticulares_Result> ListarExpedienteParticulares(WCO_ListarExpedienteParticulares_Result ObjDetalle)
        {
            ADDAT_Expediente exp = new ADDAT_Expediente();
            List<WCO_ListarExpedienteParticulares_Result> lst = new List<WCO_ListarExpedienteParticulares_Result>();
            try
            {
                lst = exp.ListarExpedienteParticulares(ObjDetalle);
            }
            catch (Exception ex)
            {
                string val = ex.Message;
            }
            return lst;
        }

        [HttpPost]
        [Route("api/Facturacion/ListarExpediente")]
        public IEnumerable<WCO_ListarExpediente_Result> ListarExpediente(WCO_ListarExpediente_Result ObjDetalle)
        {
            ADDAT_Expediente exp = new ADDAT_Expediente();
            List<WCO_ListarExpediente_Result> lst = new List<WCO_ListarExpediente_Result>();
            try
            {
                lst = exp.ListarExpediente(ObjDetalle);
            }
            catch (Exception ex)
            {
                string val = ex.Message;
            }
            return lst;
        }

        [HttpPost]
        [Route("api/Facturacion/ListarExpedienteDetalle")]
        public IEnumerable<WCO_ListarExpedienteDetalle_Result> ListarExpedienteDetalle(WCO_ListarExpedienteDetalle_Result ObjDetalle)
        {
            ADDAT_Expediente exp = new ADDAT_Expediente();
            List<WCO_ListarExpedienteDetalle_Result> lst = new List<WCO_ListarExpedienteDetalle_Result>();
            try
            {
                lst = exp.ListarExpedienteDetalle(ObjDetalle);
            }
            catch (Exception ex)
            {
                string val = ex.Message;
            }
            return lst;
        }

        [Authorize]
        [HttpPost]
        [Route("api/Facturacion/MantenimientoExpediente/{id}")]
        public async Task<IHttpActionResult> MantenimientoExpediente(int id, [FromBody] ViewModalDetalle ObjDet)
        {
            ViewModalExite objLogin = new ViewModalExite();
            HttpStatusCode statusCode = new HttpStatusCode();
            ADDAT_Expediente m = new ADDAT_Expediente();
            try
            {
                int valor = 0;
                if (id == 1)
                {
                    var ObjCabecera = (WCO_ListarExpediente_Result)Newtonsoft.Json.JsonConvert.DeserializeObject(ObjDet.cabecera.ToString(), typeof(WCO_ListarExpediente_Result));

                    // Individual
                    if (ObjCabecera.TipoExpediente == 3 || ObjCabecera.TipoExpediente == 4 || ObjCabecera.TipoExpediente == 5)
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            try
                            {
                                valor = m.Insertar(ObjCabecera);
                                if (valor < 0)
                                {
                                    objLogin.success = false;
                                    objLogin.valor = valor;
                                    objLogin.mensaje = "Los campos ingresados coinciden con un registro en nuestra base. Por favor ingrese un nuevo valor";
                                    objLogin.data = null;
                                    statusCode = HttpStatusCode.OK;
                                }
                                else
                                {
                                    int linea = 0;
                                    var ObjDetalle = (List<WCO_ListarExpedienteDetalle_Result>)Newtonsoft.Json.JsonConvert.DeserializeObject(ObjDet.cabecera.ToString(), typeof(List<WCO_ListarExpedienteDetalle_Result>));
                                    foreach (WCO_ListarExpedienteDetalle_Result Resdet in ObjDetalle)
                                    {
                                        Resdet.IdExpediente = valor;
                                        linea = m.InsertarDetalle(Resdet);
                                    }
                                    objLogin.success = true;
                                    objLogin.valor = valor;
                                    objLogin.mensaje = "Created";
                                    ObjCabecera.IdExpediente = valor;
                                    objLogin.data = ObjCabecera;
                                    statusCode = HttpStatusCode.Created;
                                }

                                scope.Complete();
                                scope.Dispose();
                            }
                            catch (TransactionAbortedException ex)
                            {
                                objLogin.success = false;
                                objLogin.valor = valor;
                                objLogin.mensaje = "Exception=" + ex.InnerException + "| Message=" + ex.Message;
                                objLogin.data = null;
                                statusCode = HttpStatusCode.OK;
                                scope.Dispose();
                            }
                            catch (ApplicationException ex)
                            {
                                objLogin.success = false;
                                objLogin.valor = valor;
                                objLogin.mensaje = "Exception=" + ex.InnerException + "| Message=" + ex.Message;
                                objLogin.data = null;
                                statusCode = HttpStatusCode.OK;
                                scope.Dispose();
                            }
                        }
                    }
                    else  // masivo
                    {
                        valor = m.Insertar(ObjCabecera);
                        if (valor < 0)
                        {
                            objLogin.success = false;
                            objLogin.valor = valor;
                            objLogin.mensaje = "Los campos ingresados coinciden con un registro en nuestra base. Por favor ingrese un nuevo valor";
                            objLogin.data = null;
                            statusCode = HttpStatusCode.OK;
                        }
                        else
                        {
                            objLogin.success = true;
                            objLogin.valor = valor;
                            objLogin.mensaje = "Created";
                            ObjCabecera.IdExpediente = valor;
                            objLogin.data = ObjCabecera;
                            statusCode = HttpStatusCode.Created;
                        }
                    }
                }
                else if (id == 2)
                {
                    var ObjCabecera = (WCO_ListarExpediente_Result)Newtonsoft.Json.JsonConvert.DeserializeObject(ObjDet.cabecera.ToString(), typeof(WCO_ListarExpediente_Result));
                    valor = m.Actualizar(ObjCabecera);
                    if (valor < 0)
                    {
                        objLogin.success = false;
                        objLogin.valor = valor;
                        objLogin.mensaje = "Los campos ingresados coinciden con un registro en nuestra base. Por favor ingrese un nuevo valor";
                        statusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        objLogin.success = true;
                        objLogin.valor = 1;
                        objLogin.mensaje = "Ok";
                        objLogin.data = ObjCabecera;
                        statusCode = HttpStatusCode.OK;
                    }

                }
                else if (id == 3)
                {
                    var ObjCabecera = (WCO_ListarExpediente_Result)Newtonsoft.Json.JsonConvert.DeserializeObject(ObjDet.cabecera.ToString(), typeof(WCO_ListarExpediente_Result));
                    m.Inactivar(ObjCabecera);
                    objLogin.success = true;
                    objLogin.valor = 1;
                    objLogin.mensaje = "Ok";
                    statusCode = HttpStatusCode.OK;
                }
                else
                {
                    return BadRequest();
                }
                return Content(statusCode, objLogin);
            }
            catch (Exception ex)
            {
                objLogin.success = false;
                objLogin.valor = -1;
                objLogin.mensaje = "Exception=" + ex.InnerException + "| Message=" + ex.Message;
                objLogin.data = null;
                statusCode = HttpStatusCode.OK;
                return Content(statusCode, objLogin);
            }
        }


        #endregion

        #region Facturacion

        [HttpPost]
        [Route("api/Facturacion/ListarComprobante")]
        public IEnumerable<WCO_ListarComprobanteId_Result> ListarComprobante(WCO_ListarComprobanteId_Result ObjDetalle)
        {
            ADDAT_Facturacion exp = new ADDAT_Facturacion();
            List<WCO_ListarComprobanteId_Result> lst = new List<WCO_ListarComprobanteId_Result>();
            try
            {
                lst = exp.ListarComprobante(ObjDetalle);
            }
            catch (Exception ex)
            {
                string val = ex.Message;
            }
            return lst;
        }

        [HttpPost]
        [Route("api/Facturacion/ListarComprobanteDetalle")]
        public IEnumerable<WCO_ListarComprobanteDetalleId_Result> ListarComprobanteDetalle(WCO_ListarComprobanteDetalleId_Result ObjDetalle)
        {
            ADDAT_Facturacion exp = new ADDAT_Facturacion();
            List<WCO_ListarComprobanteDetalleId_Result> lst = new List<WCO_ListarComprobanteDetalleId_Result>();
            try
            {
                lst = exp.ListarComprobanteDetalle(ObjDetalle);
            }
            catch (Exception ex)
            {
                string val = ex.Message;
            }
            return lst;
        }

        [HttpPost]
        [Route("api/Facturacion/ObtenerComprobante")]
        public IHttpActionResult ObtenerComprobante(WCO_ListarComprobanteId_Result ObjDetalle)
        {
            ADDAT_Facturacion exp = new ADDAT_Facturacion();
            ViewModalDetalle obj = new ViewModalDetalle();
            try
            {
                List<WCO_ListarComprobanteId_Result> lst = new List<WCO_ListarComprobanteId_Result>();
                List<WCO_ListarComprobanteDetalleId_Result> lstdet = new List<WCO_ListarComprobanteDetalleId_Result>();
                lst = exp.ListarComprobante(ObjDetalle);
                foreach (WCO_ListarComprobanteId_Result enty in lst)
                {
                    obj.cabecera = Newtonsoft.Json.JsonConvert.SerializeObject(enty);
                }
                WCO_ListarComprobanteDetalleId_Result ObjEntidad = new WCO_ListarComprobanteDetalleId_Result();
                ObjEntidad.IdComprobante = ObjDetalle.IdComprobante;
                lstdet = exp.ListarComprobanteDetalle(ObjEntidad);
                obj.detalle = Newtonsoft.Json.JsonConvert.SerializeObject(lstdet);
                obj.success = true;
                obj.mensaje = "";
                obj.valor = lstdet.Count;
            }
            catch (Exception ex)
            {
                obj.success = false;
                obj.mensaje = "";
                obj.valor = -1;
                string val = ex.Message;
            }
            return Ok(obj);
        }

        [Authorize]
        [HttpPost]
        [Route("api/Facturacion/MantenimientoComprobante/{id}")]
        public async Task<IHttpActionResult> MantenimientoComprobante(int id, [FromBody] ViewModalDetalle ObjDet)
        {
            ViewModalExite objLogin = new ViewModalExite();
            HttpStatusCode statusCode = new HttpStatusCode();
            ADDAT_Facturacion exp = new ADDAT_Facturacion();
            try
            {
                int valor = 0;
                if (id == 1)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        try
                        {
                            var ObjCabecera = (WCO_ListarComprobanteId_Result)Newtonsoft.Json.JsonConvert.DeserializeObject(ObjDet.cabecera.ToString(), typeof(WCO_ListarComprobanteId_Result));
                            valor = exp.InsertarCabecera(ObjCabecera);
                            if (valor > 0)
                            {
                                int linea = 0;
                                var ObjDetalle = (List<WCO_ListarComprobanteDetalleId_Result>)Newtonsoft.Json.JsonConvert.DeserializeObject(ObjDet.cabecera.ToString(), typeof(List<WCO_ListarComprobanteDetalleId_Result>));
                                foreach (WCO_ListarComprobanteDetalleId_Result Resdet in ObjDetalle)
                                {
                                    Resdet.IdComprobante = valor;
                                    linea = exp.InsertarDetalle(Resdet);
                                }
                            }
                            if (valor < 0)
                            {
                                objLogin.success = false;
                                objLogin.valor = valor;
                                objLogin.mensaje = "Los campos ingresados coinciden con un registro en nuestra base. Por favor ingrese un nuevo valor";
                                objLogin.data = null;
                                statusCode = HttpStatusCode.OK;
                            }
                            else
                            {
                                objLogin.success = true;
                                objLogin.valor = valor;
                                objLogin.mensaje = "Created";
                                ObjCabecera.IdComprobante = valor;
                                objLogin.data = ObjCabecera;
                                statusCode = HttpStatusCode.Created;
                            }
                            scope.Complete();
                            scope.Dispose();
                        }
                        catch (TransactionAbortedException ex)
                        {
                            objLogin.success = false;
                            objLogin.valor = valor;
                            objLogin.mensaje = "Exception=" + ex.InnerException + "| Message=" + ex.Message;
                            objLogin.data = null;
                            statusCode = HttpStatusCode.OK;
                            scope.Dispose();
                        }
                        catch (ApplicationException ex)
                        {
                            objLogin.success = false;
                            objLogin.valor = valor;
                            objLogin.mensaje = "Exception=" + ex.InnerException + "| Message=" + ex.Message;
                            objLogin.data = null;
                            statusCode = HttpStatusCode.OK;
                            scope.Dispose();
                        }
                    }
                }
                else if (id == 2)
                {
                    var ObjCabecera = (WCO_ListarComprobanteId_Result)Newtonsoft.Json.JsonConvert.DeserializeObject(ObjDet.cabecera.ToString(), typeof(WCO_ListarComprobanteId_Result));
                    valor = exp.ActualizarCabecera(ObjCabecera);
                    var ObjDetalle = (List<WCO_ListarComprobanteDetalleId_Result>)Newtonsoft.Json.JsonConvert.DeserializeObject(ObjDet.cabecera.ToString(), typeof(List<WCO_ListarComprobanteDetalleId_Result>));
                    foreach (WCO_ListarComprobanteDetalleId_Result Resdet in ObjDetalle)
                    {
                        Resdet.IdComprobante = valor;
                        exp.ActualizarDetalle(Resdet);
                    }
                    if (valor < 0)
                    {
                        objLogin.success = false;
                        objLogin.valor = valor;
                        objLogin.mensaje = "Los campos ingresados coinciden con un registro en nuestra base. Por favor ingrese un nuevo valor";
                        statusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        objLogin.success = true;
                        objLogin.valor = 1;
                        objLogin.mensaje = "Ok";
                        objLogin.data = ObjCabecera;
                        statusCode = HttpStatusCode.OK;
                    }

                }
                else if (id == 3)
                {
                    var ObjCabecera = (WCO_ListarComprobanteId_Result)Newtonsoft.Json.JsonConvert.DeserializeObject(ObjDet.cabecera.ToString(), typeof(WCO_ListarComprobanteId_Result));
                    exp.InactivarCabecera(ObjCabecera);
                    objLogin.success = true;
                    objLogin.valor = 1;
                    objLogin.mensaje = "Ok";
                    statusCode = HttpStatusCode.OK;
                }
                else if (id == 4)
                {
                    var ObjCabecera = (WCO_ListarComprobanteId_Result)Newtonsoft.Json.JsonConvert.DeserializeObject(ObjDet.cabecera.ToString(), typeof(WCO_ListarComprobanteId_Result));
                    exp.ActualizarEstadoCabecera(ObjCabecera);
                    objLogin.success = true;
                    objLogin.valor = 1;
                    objLogin.mensaje = "Ok";
                    statusCode = HttpStatusCode.OK;
                }
                else
                {
                    return BadRequest();
                }
                return Content(statusCode, objLogin);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, new ViewModalExite() { success = false, mensaje = ex.Message, valor = -1 });
            }
        }

        #endregion

        #region Cobranza
        [HttpPost]
        [Route("api/Facturacion/ListarCobranza")]
        public IEnumerable<WCO_ListarCobranza_Result> ListarCobranza(WCO_ListarCobranza_Result ObjDetalle)
        {
            ADDAT_Cobranza exp = new ADDAT_Cobranza();
            List<WCO_ListarCobranza_Result> lst = new List<WCO_ListarCobranza_Result>();
            try
            {
                lst = exp.WCO_ListarCobranza(ObjDetalle);
            }
            catch (Exception ex)
            {
                string val = ex.Message;
            }
            return lst;
        }

        [HttpPost]
        [Route("api/Facturacion/ListarCobranzaDetalle")]
        public IEnumerable<WCO_ListarCobranzaDetalleId_Result> ListarCobranzaDetalle(WCO_ListarCobranza_Result ObjDetalle)
        {
            ADDAT_Cobranza exp = new ADDAT_Cobranza();
            List<WCO_ListarCobranzaDetalleId_Result> lst = new List<WCO_ListarCobranzaDetalleId_Result>();
            try
            {
                lst = exp.WCO_ListarCobranzaDetalle(ObjDetalle);
            }
            catch (Exception ex)
            {
                string val = ex.Message;
            }
            return lst;
        }

        [Authorize]
        [HttpPost]
        [Route("api/Facturacion/MantenimientoCobranza/{id}")]
        public async Task<IHttpActionResult> MantenimientoCobranza(int id, [FromBody] ViewModalDetalle ObjDet)
        {
            ViewModalExite objLogin = new ViewModalExite();
            HttpStatusCode statusCode = new HttpStatusCode();
            ADDAT_Cobranza m = new ADDAT_Cobranza();
            try
            {
                int valor = 0;
                if (id == 1)
                {
                    var ObjDetalle = (List<WCO_ListarCobranzaDetalleId_Result>)Newtonsoft.Json.JsonConvert.DeserializeObject(ObjDet.detalle.ToString(), typeof(List<WCO_ListarCobranzaDetalleId_Result>));
                    var ObjCabecera = (WCO_ListarCobranza_Result)Newtonsoft.Json.JsonConvert.DeserializeObject(ObjDet.cabecera.ToString(), typeof(WCO_ListarCobranza_Result));

                    using (TransactionScope scope = new TransactionScope())
                    {
                        try
                        {
                            valor = m.InsertarCabecera(ObjCabecera);
                            if (valor < 0)
                            {
                                objLogin.success = false;
                                objLogin.valor = valor;
                                objLogin.mensaje = "Los campos ingresados coinciden con un registro en nuestra base. Por favor ingrese un nuevo valor";
                                objLogin.data = null;
                                statusCode = HttpStatusCode.OK;
                            }
                            else
                            {
                                int linea = 0;
                                foreach (WCO_ListarCobranzaDetalleId_Result Resdet in ObjDetalle)
                                {
                                    Resdet.IdCobranza = valor;
                                    linea = m.InsertarDetalle(Resdet);
                                }

                                objLogin.success = true;
                                objLogin.valor = valor;
                                objLogin.mensaje = "Created";
                                ObjCabecera.IdCobranza = valor;
                                objLogin.data = ObjCabecera;
                                statusCode = HttpStatusCode.Created;
                            }
                            scope.Complete();
                            scope.Dispose();
                        }
                        catch (TransactionAbortedException ex)
                        {
                            objLogin.success = false;
                            objLogin.valor = valor;
                            objLogin.mensaje = "Exception=" + ex.InnerException + "| Message=" + ex.Message;
                            objLogin.data = null;
                            statusCode = HttpStatusCode.OK;
                            scope.Dispose();
                        }
                        catch (ApplicationException ex)
                        {
                            objLogin.success = false;
                            objLogin.valor = valor;
                            objLogin.mensaje = "Exception=" + ex.InnerException + "| Message=" + ex.Message;
                            objLogin.data = null;
                            statusCode = HttpStatusCode.OK;
                            scope.Dispose();
                        }
                    }
                }
                else if (id == 2)
                {


                }
                else if (id == 3)
                {
                    var ObjCabecera = (WCO_ListarCobranza_Result)Newtonsoft.Json.JsonConvert.DeserializeObject(ObjDet.cabecera.ToString(), typeof(WCO_ListarCobranza_Result));
                    m.Anular(ObjCabecera);
                    objLogin.success = true;
                    objLogin.valor = 1;
                    objLogin.mensaje = "Ok";
                    statusCode = HttpStatusCode.OK;
                }
                else
                {
                    return BadRequest();
                }
                return Content(statusCode, objLogin);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, new ViewModalExite() { success = false, mensaje = ex.Message, valor = -1 });
            }
        }

        #endregion



    }
}
