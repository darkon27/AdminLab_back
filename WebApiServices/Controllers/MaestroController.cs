using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using WebApiServices.Models;
using WebApiServices.Models.Request;
using WebApiServices.Models.Datos;
using WebApiServices.Models.Response;
using Newtonsoft.Json;

namespace WebApiServices.Controllers
{
    public class MaestroController : ApiController
    {
        Metodos m = new Metodos();
        UT_Kerberos Ker = new UT_Kerberos();
        ADDAT_Sede sd = new ADDAT_Sede();
        ADDAT_PersonaMast per = new ADDAT_PersonaMast();
        ADDAT_TipoPaciente tpac = new ADDAT_TipoPaciente();
        CODAT_Componente serv = new CODAT_Componente();

        ADDAT_TipoCambio tipcam = new ADDAT_TipoCambio();

        #region Maestro

        [HttpPost]
        [Route("api/Maestro/ListaTablasMaestras")]
        public IEnumerable<WCO_ListarTablasMaestras_Result> ListaTablasMaestras(WCO_ListarTablasMaestras_Result ObjDetalle)
        {
            List<WCO_ListarTablasMaestras_Result> lst = new List<WCO_ListarTablasMaestras_Result>();
            try
            {
                lst = m.ListaTablasMaestras(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        [Authorize]
        [HttpPost]
        [Route("api/Maestro/MantenimientoTablaMaestro/{id}")]
        public async Task<IHttpActionResult> MantenimientoTablaMaestro(int id, [FromBody] WCO_ListarTablasMaestras_Result ObjDetalle)
        {
            ViewModalExite objLogin = new ViewModalExite();
            HttpStatusCode statusCode = new HttpStatusCode();

            try
            {
                int valor = 0;
                if (id < 1) { return NotFound(); }
                else if (id == 1)
                {
                    valor = m.InsertarTablaMaestro(ObjDetalle);
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
                        ObjDetalle.IdTablaMaestro = valor;
                        objLogin.data = ObjDetalle;
                        statusCode = HttpStatusCode.Created;
                    }
                }
                else if (id == 2)
                {
                    valor = m.ActualizarTablaMaestro(ObjDetalle);
                    if (valor != 0)
                    {
                        objLogin.success = false;
                        objLogin.valor = valor;
                        objLogin.mensaje = "Se generó un error al actualizar";
                        statusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        objLogin.success = true;
                        objLogin.valor = 1;
                        objLogin.mensaje = "Actualización realizada";
                        statusCode = HttpStatusCode.OK;
                    }

                }
                else if (id == 3)
                {
                    m.InactivarTablaMaestro(ObjDetalle);
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

        #region Parametros
        [HttpPost]
        [Route("api/Maestro/ListaParametros")]
        public IEnumerable<WCO_ListarParametro_Result> ListaParametro(WCO_ListarParametro_Result ObjDetalle)
        {
            List<WCO_ListarParametro_Result> lst = new List<WCO_ListarParametro_Result>();
            try
            {
                lst = m.ListaParametro(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        [Authorize]
        [HttpPost]
        [Route("api/Maestro/MantenimientoParametro/{id}")]
        public async Task<IHttpActionResult> MantenimientoParametro(int id, [FromBody] WCO_ListarParametro_Result ObjDetalle)
        {
            if (id < 1)
            {
                return NotFound();
            }

            if (ObjDetalle == null)
            {
                return BadRequest("El objeto de detalle no puede ser nulo.");
            }

            ViewModalExite objLogin = new ViewModalExite();
            HttpStatusCode statusCode = HttpStatusCode.OK;
            try
            {
                string resultado = string.Empty;
                switch (id)
                {
                    case 1:
                        resultado = m.InsertarParametro(ObjDetalle);
                        objLogin.success = true;
                        objLogin.valor = 0;
                        objLogin.mensaje = "Creación correcta";
                        objLogin.data = null;

                        break;
                    case 2:
                        resultado = m.ActualizarParametro(ObjDetalle);
                        objLogin.success = true;
                        objLogin.valor = 0;
                        objLogin.mensaje = "Se actualizó el registro";
                        objLogin.data = null;
                        break;
                    case 3:
                        ObjDetalle.Estado = "I";
                        resultado = m.ActualizarParametro(ObjDetalle);
                        objLogin.success = true;
                        objLogin.valor = 0;
                        objLogin.mensaje = "Se inactivó el registro";
                        objLogin.data = null;
                        break;
                }

                return Content(statusCode, objLogin);
            }
            catch (Exception ex)
            {
                // Aquí puedes agregar logging del error
                return Content(HttpStatusCode.InternalServerError, new ViewModalExite() { success = false, mensaje = ex.Message, valor = -1 });
            }
        }

        #endregion

        #region MaestroDetalleTabla

        [Route("api/Maestro/ListaTablaMaestroDetalle")]
        public IEnumerable<WCO_ListarTablaMaestroDetalle_Result> ListaTablaMaestroDetalle(WCO_ListarTablaMaestroDetalle_Result ObjDetalle)
        {
            List<WCO_ListarTablaMaestroDetalle_Result> lst = new List<WCO_ListarTablaMaestroDetalle_Result>();
            try
            {
                lst = m.ListaTablaMaestroDetalle(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }


        [HttpPost]
        [Route("api/Maestro/ListaMaestroDetalle")]
        public IEnumerable<WCO_ListarTMAestroDetalles_Result> ListaMaestroDetalle(WCO_ListarTMAestroDetalles_Result ObjDetalle)
        {
            List<WCO_ListarTMAestroDetalles_Result> lst = new List<WCO_ListarTMAestroDetalles_Result>();
            try
            {
                lst = m.ListaMaestroDetalle(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        [Authorize]
        [HttpPost]
        [Route("api/Maestro/MantenimientoMaestroDetalle/{id}")]
        public async Task<IHttpActionResult> MantenimientoMaestroDetalle(int id, [FromBody] WCO_ListarTMAestroDetalles_Result ObjDetalle)
        {
            ViewModalExite objLogin = new ViewModalExite();
            HttpStatusCode statusCode = new HttpStatusCode();

            try
            {
                int valor = 0;
                if (id < 1 || id == null) { return NotFound(); }
                else if (id == 1)
                {
                    valor = m.InsertarMaestroDetalle(ObjDetalle);
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
                        ObjDetalle.IdTablaMaestro = valor;
                        objLogin.data = ObjDetalle;
                        statusCode = HttpStatusCode.Created;
                    }
                }
                else if (id == 2)
                {
                    valor = m.ActualizarMaestroDetalle(ObjDetalle);
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
                        statusCode = HttpStatusCode.OK;
                    }

                }
                else if (id == 3)
                {
                    m.InactivarMaestroDetalle(ObjDetalle);
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

        #region Ubigeo
        [HttpPost]
        [Route("api/Maestro/ListaUbigeo")]
        public IEnumerable<WCO_ListarUbigeo_Result> ListaUbigeo(WCO_ListarUbigeo_Result ObjDetalle)
        {
            List<WCO_ListarUbigeo_Result> lst = new List<WCO_ListarUbigeo_Result>();
            try
            {
                lst = m.ListaUbigeo(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }
        #endregion

        #region ListarMoneda
        [HttpPost]
        [Route("api/Maestro/ListarMoneda")]
        public IEnumerable<WCO_ListarMonedaMast_Result> ListarMoneda(WCO_ListarMonedaMast_Result ObjDetalle)
        {
            List<WCO_ListarMonedaMast_Result> lst = new List<WCO_ListarMonedaMast_Result>();
            try
            {
                lst = m.ListarMoneda(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }
        #endregion

        #region ModeloServicio
        [HttpPost]
        [Route("api/Maestro/ListarModeloServicio")]
        public IEnumerable<WCO_ListarModeloServicio_Result> ListarModeloServicio(WCO_ListarModeloServicio_Result ObjDetalle)
        {
            List<WCO_ListarModeloServicio_Result> lst = new List<WCO_ListarModeloServicio_Result>();
            try
            {
                ADDAT_ModeloServicio Modelo = new ADDAT_ModeloServicio();
                lst = Modelo.ListadoPaginado(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        [Authorize]
        [HttpPost]
        [Route("api/Maestro/MantenimientoModeloServicio/{id}")]
        public async Task<IHttpActionResult> MantenimientoModeloServicio(int id, [FromBody] WCO_ListarModeloServicio_Result ObjDetalle)
        {
            ViewModalExite objLogin = new ViewModalExite();
            HttpStatusCode statusCode = new HttpStatusCode();
            ADDAT_ModeloServicio Modelo = new ADDAT_ModeloServicio();
            try
            {
                statusCode = HttpStatusCode.OK;
                int valor = 0;
                if (id < 1 || id == null) { return NotFound(); }

                if (id == 1)
                {
                    valor = Modelo.Insertar(ObjDetalle);
                    if (valor < 0)
                    {
                        objLogin.success = false;
                        objLogin.valor = 0;
                        objLogin.mensaje = "Los campos ingresados coinciden con un registro en nuestra base. Por favor ingrese un nuevo valor";
                        objLogin.data = null;

                    }
                    else
                    {
                        objLogin.success = true;
                        objLogin.valor = 1;
                        objLogin.mensaje = "Created";
                        objLogin.data = ObjDetalle;
                        statusCode = HttpStatusCode.Created;
                    }
                }

                if (id == 3)
                {
                    valor = Modelo.Inactivar(ObjDetalle);
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

        #region Autorizacion
        [HttpPost]
        [Route("api/Maestro/ListaAutorizacion")]
        public IEnumerable<WCO_ListarAutorizacionBeneficiario_Result> ListaAutorizacion(WCO_ListarAutorizacionBeneficiario_Result ObjDetalle)
        {
            List<WCO_ListarAutorizacionBeneficiario_Result> lst = new List<WCO_ListarAutorizacionBeneficiario_Result>();
            try
            {
                lst = m.ListaAutorizacion(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }
        #endregion

        #region Especialidad

        [HttpPost]
        [Route("api/Maestro/ListaEspecialidad")]
        public IEnumerable<WCO_ListarEspecialidad_Result> ListaEspecialidad(WCO_ListarEspecialidad_Result ObjDetalle)
        {
            List<WCO_ListarEspecialidad_Result> lst = new List<WCO_ListarEspecialidad_Result>();
            try
            {
                lst = m.ListaEspecialidad(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        #endregion

        #region TipoAdmision

        [HttpPost]
        [Route("api/Maestro/ListaTipoAdmision")]
        public IEnumerable<WCO_ListarTipodeAdmision_Result> ListaTipoAdmision(WCO_ListarTipodeAdmision_Result ObjDetalle)
        {
            List<WCO_ListarTipodeAdmision_Result> lst = new List<WCO_ListarTipodeAdmision_Result>();
            try
            {
                lst = m.ListaTipoAdmision(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        [Authorize]
        [HttpPost]
        [Route("api/Maestro/MantenimientoTipoAdmision/{id}")]
        public async Task<IHttpActionResult> MantenimientoTipoAdmision(int id, [FromBody] WCO_ListarTipodeAdmision_Result ObjDetalle)
        {
            ViewModalExite objLogin = new ViewModalExite();
            HttpStatusCode statusCode = new HttpStatusCode();

            try
            {
                int valor = 0;
                if (id < 1 || id == null) { return NotFound(); }
                else if (id == 1)
                {
                    valor = m.InsertarTipoAdmision(ObjDetalle);
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
                        ObjDetalle.TipoAdmisionId = valor;
                        objLogin.data = ObjDetalle;
                        statusCode = HttpStatusCode.Created;
                    }
                }
                else if (id == 2)
                {
                    valor = m.ActualizarTipoAdmision(ObjDetalle);
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
                        statusCode = HttpStatusCode.OK;
                    }

                }
                else if (id == 3)
                {
                    m.InactivarTipoAdmision(ObjDetalle);
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

        #region TipoPago
        [HttpPost]
        [Route("api/Maestro/ListaTipoPago")]
        public IEnumerable<WCO_ListarTipoPago_Result> ListaTipoPago(WCO_ListarTipoPago_Result ObjDetalle)
        {
            ADDAT_TipoPago p = new ADDAT_TipoPago();
            List<WCO_ListarTipoPago_Result> lst = new List<WCO_ListarTipoPago_Result>();
            try
            {
                lst = p.ListarTipoPago(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        [Authorize]
        [HttpPost]
        [Route("api/Maestro/MantenimientoTipoPago/{id}")]
        public async Task<IHttpActionResult> MantenimientoTipoPago(int id, [FromBody] WCO_ListarTipoPago_Result ObjDetalle)
        {
            ViewModalExite objLogin = new ViewModalExite();
            HttpStatusCode statusCode = new HttpStatusCode();
            ADDAT_TipoPago p = new ADDAT_TipoPago();
            try
            {
                int valor = 0;
                if (id < 1 || id == null) { return NotFound(); }
                else if (id == 1)
                {
                    valor = p.Insertar(ObjDetalle);
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
                        ObjDetalle.Id = valor;
                        objLogin.data = ObjDetalle;
                        statusCode = HttpStatusCode.Created;
                    }
                }
                else if (id == 2)
                {
                    p.Actualizar(ObjDetalle);

                    objLogin.success = true;
                    objLogin.valor = valor;
                    objLogin.mensaje = "Actualización realizada";
                    statusCode = HttpStatusCode.OK;


                }
                else if (id == 3)
                {
                    p.Inactivar(ObjDetalle);
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

        #region Aprobador

        [HttpPost]
        [Route("api/Maestro/ListarAprobadores")]
        public IEnumerable<WCO_ListarAprobadores_Result> ListarAprobadores(WCO_ListarAprobadores_Result ObjDetalle)
        {
            List<WCO_ListarAprobadores_Result> lst = new List<WCO_ListarAprobadores_Result>();
            try
            {
                lst = m.ListarAprobadores(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        [Authorize]
        [HttpPost]
        [Route("api/Maestro/MantenimientoAprobadores/{id}")]
        public async Task<IHttpActionResult> MantenimientoAprobadores(int id, [FromBody] WCO_ListarAprobadores_Result ObjDetalle)
        {
            ViewModalExite objLogin = new ViewModalExite();
            HttpStatusCode statusCode = new HttpStatusCode();
            List<WCO_ListarAprobadores_Result> lst = new List<WCO_ListarAprobadores_Result>();

            try
            {
                int valor = 0;
                switch (id)
                {
                    case 1:
                        valor = m.InsertarAprobadores(ObjDetalle);
                        if (valor < 0)
                        {
                            objLogin.success = false;
                            objLogin.valor = valor;
                            objLogin.mensaje = "Hubo un error al ingresar el registro";
                            objLogin.data = null;
                            statusCode = HttpStatusCode.OK;
                        }
                        else
                        {
                            objLogin.success = true;
                            objLogin.valor = valor;
                            objLogin.mensaje = "Se creó el registro con éxito";
                            objLogin.data = ObjDetalle;
                            statusCode = HttpStatusCode.Created;
                        }
                        break;
                    case 2:
                        valor = m.ActualizarAprobadores(ObjDetalle);
                        if (valor < 0)
                        {
                            objLogin.success = false;
                            objLogin.valor = valor;
                            objLogin.mensaje = "Hubo un error al actualizar el registro";
                            statusCode = HttpStatusCode.OK;
                        }
                        else
                        {
                            objLogin.success = true;
                            objLogin.valor = 1;
                            objLogin.mensaje = "Se actualizó el registro con éxito";
                            statusCode = HttpStatusCode.OK;
                        }
                        break;

                    case 3:
                        valor = m.InactivarAprobadores(ObjDetalle);
                        if (valor < 0)
                        {
                            objLogin.success = false;
                            objLogin.valor = valor;
                            objLogin.mensaje = "Hubo un error al inactivar el registro";
                            statusCode = HttpStatusCode.OK;
                        }
                        else
                        {
                            objLogin.success = true;
                            objLogin.valor = 1;
                            objLogin.mensaje = "Se inactivó el registro con éxito";
                            statusCode = HttpStatusCode.OK;
                        }
                        break;
                }
                return Content(statusCode, objLogin);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, new ViewModalExite() { success = false, mensaje = ex.Message, valor = -1 });
            }
        }

        #endregion

        #region Insumo

        [HttpPost]
        [Route("api/Maestro/ListarInsumos")]
        public IEnumerable<WCO_ListarInsumo_Result> ListarInsumos(WCO_ListarInsumo_Result ObjDetalle)
        {
            List<WCO_ListarInsumo_Result> lst = new List<WCO_ListarInsumo_Result>();
            try
            {
                lst = m.ListarInsumo(ObjDetalle);
            }
            catch
            {


            }
            return lst;
        }

        [Authorize]
        [HttpPost]
        [Route("api/Maestro/MantenimientoInsumos/{id}")]
        public async Task<IHttpActionResult> MantenimientoInsumos(int id, [FromBody] WCO_ListarInsumo_Result ObjDetalle)
        {
            ViewModalExite objLogin = new ViewModalExite();
            HttpStatusCode statusCode = new HttpStatusCode();
            List<WCO_ListarInsumo_Result> lst = new List<WCO_ListarInsumo_Result>();

            try
            {
                int valor = 0;
                switch (id)
                {
                    case 1:
                        valor = m.InsertarInsumo(ObjDetalle);
                        if (valor < 0)
                        {
                            objLogin.success = false;
                            objLogin.valor = valor;
                            objLogin.mensaje = "Hubo un error al ingresar el registro";
                            objLogin.data = null;
                            statusCode = HttpStatusCode.OK;
                        }
                        else
                        {
                            objLogin.success = true;
                            objLogin.valor = valor;
                            objLogin.mensaje = "Se creó el registro con éxito";
                            objLogin.data = ObjDetalle;
                            statusCode = HttpStatusCode.Created;
                        }
                        break;
                    case 2:
                        valor = m.ActualizarInsumo(ObjDetalle);
                        if (valor < 0)
                        {
                            objLogin.success = false;
                            objLogin.valor = valor;
                            objLogin.mensaje = "Hubo un error al actualizar el registro";
                            statusCode = HttpStatusCode.OK;
                        }
                        else
                        {
                            objLogin.success = true;
                            objLogin.valor = 1;
                            objLogin.mensaje = "Se actualizó el registro con éxito";
                            statusCode = HttpStatusCode.OK;
                        }
                        break;

                    case 3:
                        valor = m.InactivarInsumo(ObjDetalle);
                        if (valor < 0)
                        {
                            objLogin.success = false;
                            objLogin.valor = valor;
                            objLogin.mensaje = "Hubo un error al inactivar el registro";
                            statusCode = HttpStatusCode.OK;
                        }
                        else
                        {
                            objLogin.success = true;
                            objLogin.valor = 1;
                            objLogin.mensaje = "Se inactivó el registro con éxito";
                            statusCode = HttpStatusCode.OK;
                        }
                        break;
                }
                return Content(statusCode, objLogin);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, new ViewModalExite() { success = false, mensaje = ex.Message, valor = -1 });
            }
        }

        #endregion

        #region Tipo Trabajador

        [HttpPost]
        [Route("api/Maestro/ListarTipoTrabajador")]
        public IEnumerable<WCO_ListarTipoTrabajador_Result> ListarTipoTrabajador(WCO_ListarTipoTrabajador_Result ObjDetalle)
        {
            List<WCO_ListarTipoTrabajador_Result> lst = new List<WCO_ListarTipoTrabajador_Result>();
            try
            {
                lst = m.ListarTipoTrabajor(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        [Authorize]
        [HttpPost]
        [Route("api/Maestro/MantenimientoTipoTrabajador/{id}")]
        public async Task<IHttpActionResult> MantenimientoTipoTrabajador(int id, [FromBody] WCO_ListarTipoTrabajador_Result ObjDetalle)
        {
            ViewModalExite objLogin = new ViewModalExite();
            HttpStatusCode statusCode = new HttpStatusCode();

            try
            {
                int valor = 0;
                switch (id)
                {
                    case 1:
                        valor = m.InsertarTipoTrabajador(ObjDetalle);
                        if (valor < 0)
                        {
                            objLogin.success = false;
                            objLogin.valor = valor;
                            objLogin.mensaje = "Hubo un error al ingresar el registro";
                            objLogin.data = null;
                            statusCode = HttpStatusCode.OK;
                        }
                        else
                        {
                            objLogin.success = true;
                            objLogin.valor = valor;
                            objLogin.mensaje = "Se creó el registro con éxito";
                            objLogin.data = ObjDetalle;
                            statusCode = HttpStatusCode.Created;
                        }
                        break;
                    case 2:
                        valor = m.ActualizarTipoTrabajador(ObjDetalle);
                        if (valor < 0)
                        {
                            objLogin.success = false;
                            objLogin.valor = valor;
                            objLogin.mensaje = "Hubo un error al actualizar el registro";
                            statusCode = HttpStatusCode.OK;
                        }
                        else
                        {
                            objLogin.success = true;
                            objLogin.valor = 1;
                            objLogin.mensaje = "Se actualizó el registro con éxito";
                            statusCode = HttpStatusCode.OK;
                        }
                        break;

                    case 3:
                        valor = m.InactivarTipotrabajador(ObjDetalle);
                        if (valor < 0)
                        {
                            objLogin.success = false;
                            objLogin.valor = valor;
                            objLogin.mensaje = "Hubo un error al inactivar el registro";
                            statusCode = HttpStatusCode.OK;
                        }
                        else
                        {
                            objLogin.success = true;
                            objLogin.valor = 1;
                            objLogin.mensaje = "Se inactivó el registro con éxito";
                            statusCode = HttpStatusCode.OK;
                        }
                        break;
                }
                return Content(statusCode, objLogin);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, new ViewModalExite() { success = false, mensaje = ex.Message, valor = -1 });
            }
        }

        #endregion

        #region TipoPaciente
        [HttpPost]
        [Route("api/Maestro/ListaTipoPaciente")]
        public IEnumerable<WCO_ListarTipoPaciente_Result> ListaTipoPaciente(WCO_ListarTipoPaciente_Result ObjDetalle)
        {
            List<WCO_ListarTipoPaciente_Result> lst = new List<WCO_ListarTipoPaciente_Result>();
            try
            {
                lst = tpac.ListadoPaginado(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        [Authorize]
        [HttpPost]
        [Route("api/Maestro/MantenimientoTipoPaciente/{id}")]
        public async Task<IHttpActionResult> MantenimientoTipoPaciente(int id, [FromBody] WCO_ListarTipoPaciente_Result ObjDetalle)
        {
            ViewModalExite objLogin = new ViewModalExite();
            HttpStatusCode statusCode = new HttpStatusCode();
            try
            {
                int valor = 0;
                switch (id)
                {
                    case 1:
                        valor = tpac.Insertar(ObjDetalle);
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
                            ObjDetalle.TipoPacienteId = valor;
                            objLogin.data = ObjDetalle;
                            statusCode = HttpStatusCode.Created;
                        }
                        break;
                    case 2:
                        valor = tpac.Actualizar(ObjDetalle);
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
                            statusCode = HttpStatusCode.OK;
                        }
                        break;

                    case 3:
                        tpac.Inactivar(ObjDetalle);
                        objLogin.success = true;
                        objLogin.valor = 1;
                        objLogin.mensaje = "Ok";
                        statusCode = HttpStatusCode.OK;
                        break;
                }
                return Content(statusCode, objLogin);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, new ViewModalExite() { success = false, mensaje = ex.Message, valor = -1 });
            }
        }

        #endregion

        #region Aseguradora

        [HttpPost]
        [Route("api/Maestro/ListaAseguradora")]
        public IEnumerable<WCO_ListarAseguradora_Result> ListaAseguradora(WCO_ListarAseguradora_Result ObjDetalle)
        {
            List<WCO_ListarAseguradora_Result> lst = new List<WCO_ListarAseguradora_Result>();
            try
            {
                lst = m.ListaAseguradora(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        [Authorize]
        [HttpPost]
        [Route("api/Maestro/MantenimientoAseguradora/{id}")]
        public async Task<IHttpActionResult> MantenimientoAseguradora(int id, [FromBody] WCO_ListarAseguradora_Result ObjDetalle)
        {
            ViewModalExite objLogin = new ViewModalExite();
            HttpStatusCode statusCode = new HttpStatusCode();

            try
            {
                int valor = 0;
                if (id < 1 || id == null) { return NotFound(); }
                else if (id == 1)
                {
                    valor = m.InsertarAseguradora(ObjDetalle);
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
                        ObjDetalle.IdAseguradora = valor;
                        objLogin.data = ObjDetalle;
                        statusCode = HttpStatusCode.Created;
                    }
                }
                else if (id == 2)
                {
                    valor = m.ActualizarAseguradora(ObjDetalle);
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
                        statusCode = HttpStatusCode.OK;
                    }

                }
                else if (id == 3)
                {
                    m.InactivarAseguradora(ObjDetalle);
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

        #region Medico
        [HttpPost]
        [Route("api/Maestro/ListaMedico")]
        public IEnumerable<WCO_ListarMedico_Result> ListaMedico(WCO_ListarMedico_Result ObjDetalle)
        {
            List<WCO_ListarMedico_Result> lst = new List<WCO_ListarMedico_Result>();
            try
            {
                lst = m.ListarMedico(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        //[Authorize]
        [HttpPost]
        [Route("api/Maestro/MantenimientoMedico/{id}")]
        public async Task<IHttpActionResult> MantenimientoMedico(int id, [FromBody] WCO_ListarMedico_Result ObjDetalle)
        {
            ViewModalExite objLogin = new ViewModalExite();
            HttpStatusCode statusCode = new HttpStatusCode();

            try
            {
                int valor = 0;
                if (id < 1 || id == null) { return NotFound(); }
                else if (id == 1)
                {
                    valor = m.InsertarMedico(ObjDetalle);
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
                        ObjDetalle.MedicoId = valor;
                        objLogin.data = ObjDetalle;
                        statusCode = HttpStatusCode.Created;
                    }
                }
                else if (id == 2)
                {
                    valor = m.ActualizarMedico(ObjDetalle);
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
                    m.InactivarMedico(ObjDetalle);
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

        #region Persona

        [HttpPost]
        [Route("api/Maestro/ListaPersona")]
        public IEnumerable<WCO_ListarPersonasGeneral_Result> ListaPersona(WCO_ListarPersonasGeneral_Result ObjDetalle)
        {
            List<WCO_ListarPersonasGeneral_Result> lst = new List<WCO_ListarPersonasGeneral_Result>();
            try
            {
                lst = per.ListaPersona(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        [HttpPost]
        [Route("api/Maestro/TraerXPersonaId")]
        public IEnumerable<WCO_TraerXCodigoPersonaId_Result> TraerXPersonaId(WCO_ListarPersonasGeneral_Result ObjDetalle)
        {
            List<WCO_TraerXCodigoPersonaId_Result> lst = new List<WCO_TraerXCodigoPersonaId_Result>();
            try
            {
                lst = per.TraerXPersonaId(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        [HttpPost]
        [Route("api/Maestro/ListaPersonaUsuario")]
        public IEnumerable<WCO_BUSCARPERSONAUSUARIO_Result> ListaPersonaUsuario(WCO_BUSCARPERSONAUSUARIO_Result ObjDetalle)
        {
            List<WCO_BUSCARPERSONAUSUARIO_Result> lst = new List<WCO_BUSCARPERSONAUSUARIO_Result>();
            try
            {
                lst = per.ListaPersonaUsuario(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        [Authorize]
        [HttpPost]
        [Route("api/Maestro/UnificarPersonaMast/{id}")]
        public async Task<IHttpActionResult> UnificarPersonaMast(int id, [FromBody] WCO_ListarPersonasGeneral_Result ObjDetalle)
        {
            ViewModalExite objLogin = new ViewModalExite();
            HttpStatusCode statusCode = new HttpStatusCode();
            try
            {
                if (id == 4)
                {
                    per.UnificarPersonaMast(ObjDetalle);
                    objLogin.success = true;
                    objLogin.valor = 1;
                    objLogin.mensaje = "Ok";
                    statusCode = HttpStatusCode.OK;
                }
                else
                {
                    return Content(HttpStatusCode.BadRequest, new ViewModalExite() { success = false, mensaje = "Ingrese el Tokem Correcto", valor = -1 });
                }
                return Content(statusCode, objLogin);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, new ViewModalExite() { success = false, mensaje = ex.Message, valor = -1 });
            }
        }


        [Authorize]
        [HttpPost]
        [Route("api/Maestro/MantenimientoPersona/{id}")]
        public async Task<IHttpActionResult> MantenimientoPersona(int id, [FromBody] WCO_ListarPersonasGeneral_Result ObjDetalle)
        {
            ViewModalExite objLogin = new ViewModalExite();
            HttpStatusCode statusCode = new HttpStatusCode();
            int valor = 0;
            try
            {
                if (ObjDetalle.TipoPersona != null)
                {
                    if (ObjDetalle.TipoPersona == "N")
                    {
                        if (ObjDetalle.Nombres != null)
                        {
                            if (!string.IsNullOrEmpty(ObjDetalle.Nombres.ToString()))
                            {
                                ObjDetalle.Nombres = ObjDetalle.Nombres.ToUpper();
                            }
                        }
                        if (ObjDetalle.ApellidoPaterno != null)
                        {
                            if (!string.IsNullOrEmpty(ObjDetalle.ApellidoPaterno.ToString()))
                            {
                                ObjDetalle.ApellidoPaterno = ObjDetalle.ApellidoPaterno.ToUpper();
                            }
                        }
                        else
                        {
                            valor = -2;
                        }
                        if (ObjDetalle.ApellidoMaterno != null)
                        {
                            if (!string.IsNullOrEmpty(ObjDetalle.ApellidoMaterno.ToString()))
                            {
                                ObjDetalle.ApellidoMaterno = ObjDetalle.ApellidoMaterno.ToUpper();
                            }
                        }
                    }
                    else
                    {
                        if (ObjDetalle.Nombres != null)
                        {
                            if (!string.IsNullOrEmpty(ObjDetalle.Nombres.ToString()))
                            {
                                ObjDetalle.Nombres = ObjDetalle.Nombres.ToUpper();
                            }
                        }
                        if (ObjDetalle.NombreCompleto != null)
                        {
                            if (!string.IsNullOrEmpty(ObjDetalle.NombreCompleto.ToString()))
                            {
                                ObjDetalle.NombreCompleto = ObjDetalle.NombreCompleto.ToUpper();
                            }
                        }

                    }
                }

                if (ObjDetalle.Documento != null)
                {
                    if (!string.IsNullOrEmpty(ObjDetalle.Documento.ToString()))
                    {
                        ObjDetalle.Documento = ObjDetalle.Documento.ToUpper();
                    }
                }
                else
                {
                    valor = -2;
                }

                if (ObjDetalle.CorreoElectronico != null)
                {
                    if (!string.IsNullOrEmpty(ObjDetalle.CorreoElectronico.ToString()))
                    {
                        ObjDetalle.CorreoElectronico = ObjDetalle.CorreoElectronico.ToUpper();
                    }
                }
                if (ObjDetalle.DireccionReferencia != null)
                {
                    if (!string.IsNullOrEmpty(ObjDetalle.DireccionReferencia.ToString()))
                    {
                        ObjDetalle.DireccionReferencia = ObjDetalle.DireccionReferencia.ToUpper();
                    }
                }
                if (ObjDetalle.Direccion != null)
                {
                    if (!string.IsNullOrEmpty(ObjDetalle.Direccion.ToString()))
                    {
                        ObjDetalle.Direccion = ObjDetalle.Direccion.ToUpper();
                    }
                }
                if (ObjDetalle.Comentario != null)
                {
                    if (!string.IsNullOrEmpty(ObjDetalle.Comentario.ToString()))
                    {
                        ObjDetalle.Comentario = ObjDetalle.Comentario.ToUpper();
                    }
                }


                if (id < 1 || id == null) { return NotFound(); }
                else if (id == 1)
                {
                    valor = per.InsertarPersona(ObjDetalle);
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
                        ObjDetalle.Persona = valor;
                        objLogin.data = ObjDetalle;
                        statusCode = HttpStatusCode.Created;
                    }
                }
                else if (id == 2)
                {
                    valor = per.ActualizarPersona(ObjDetalle);
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
                        statusCode = HttpStatusCode.OK;
                    }

                }
                else if (id == 3)
                {
                    per.InactivarPersona(ObjDetalle);
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


        [Authorize]
        [HttpPost]
        [Route("api/Maestro/MantenimientoUsuarioWebExternos/{id}")]
        public async Task<IHttpActionResult> MantenimientoUsuarioWebExternos(int id, [FromBody] WCO_ListarUsuarioWeb_Result ObjDetalle)
        {
            ViewModalExite objLogin = new ViewModalExite();
            HttpStatusCode statusCode = new HttpStatusCode();

            try
            {
                int valor = 0;
                if (id < 1 || id == null) { return NotFound(); }
                else if (id == 1)
                {
                    valor = per.InsertarUsuarioWeb(ObjDetalle);
                    if (valor > 0)
                    {
                        objLogin.success = true;
                        objLogin.valor = valor;
                        objLogin.mensaje = "Created";
                        ObjDetalle.IdPersona = valor;
                        objLogin.data = ObjDetalle;
                        statusCode = HttpStatusCode.Created;
                    }
                    else
                    {
                        objLogin.success = false;
                        objLogin.valor = valor;
                        objLogin.mensaje = "Los campos ingresados coinciden con un registro en nuestra base. Por favor ingrese un nuevo valor";
                        objLogin.data = null;
                        statusCode = HttpStatusCode.OK;
                    }
                }
                else if (id == 2)
                {
                    valor = per.ActualizarUsuarioWeb(ObjDetalle);
                    if (valor > 0)
                    {
                        objLogin.success = true;
                        objLogin.valor = 1;
                        objLogin.mensaje = "Ok";
                        statusCode = HttpStatusCode.OK;

                    }
                    else
                    {
                        objLogin.success = false;
                        objLogin.valor = valor;
                        objLogin.mensaje = "Los campos ingresados coinciden con un registro en nuestra base. Por favor ingrese un nuevo valor";
                        statusCode = HttpStatusCode.OK;
                    }

                }
                return Content(statusCode, objLogin);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, new ViewModalExite() { success = false, mensaje = ex.Message, valor = -1 });
            }
        }

        [HttpPost]
        [Route("api/Maestro/ListaUsuarioWeb")]
        public IEnumerable<WCO_ListarUsuarioWeb_Result> ListaUsuarioWeb(WCO_ListarUsuarioWeb_Result ObjDetalle)
        {
            List<WCO_ListarUsuarioWeb_Result> lst = new List<WCO_ListarUsuarioWeb_Result>();
            try
            {
                lst = per.ListarUsuarioWeb(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        [HttpPost]
        [Route("api/Maestro/ListarConsentimiento")]
        public IEnumerable<Sp_PDP_validacion_Result> ListarConsentimiento(WCO_ListarPersonasGeneral_Result ObjDetalle)
        {
            List<Sp_PDP_validacion_Result> lst = new List<Sp_PDP_validacion_Result>();
            try
            {
                lst = per.ListarConsentimiento(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }
        #endregion

        #region EmpleadoMast

        [HttpPost]
        [Route("api/Maestro/ListarEmpleadoMast")]
        public IEnumerable<WCO_ListarEmpleados_Result> ListarEmpleadoMast(WCO_ListarEmpleados_Result ObjDetalle)
        {
            ADDAT_EmpleadoMast Empleado = new ADDAT_EmpleadoMast();
            List<WCO_ListarEmpleados_Result> lst = new List<WCO_ListarEmpleados_Result>();
            try
            {
                lst = Empleado.ListarEmpleados(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        [Authorize]
        [HttpPost]
        [Route("api/Maestro/MantenimientoEmpleado/{id}")]
        public async Task<IHttpActionResult> MantenimientoEmpleado(int id, [FromBody] WCO_ListarEmpleados_Result ObjDetalle)
        {
            ViewModalExite objLogin = new ViewModalExite();
            HttpStatusCode statusCode = new HttpStatusCode();
            ADDAT_EmpleadoMast Empleado = new ADDAT_EmpleadoMast();
            statusCode = HttpStatusCode.OK;
            try
            {
                int valor = 0;
                if (id == 2)
                {
                    valor = Empleado.Actualizar(ObjDetalle);
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
                        statusCode = HttpStatusCode.OK;
                    }
                }
                else if (id == 3)
                {
                    Empleado.Inactivar(ObjDetalle);
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

        #region MaestroSede

        [Route("api/Maestro/ListaSede")]
        public IEnumerable<WCO_ListarSede_Result> ListaSede(WCO_ListarSede_Result ObjDetalle)
        {
            string arrayOfLines = "";
            List<WCO_ListarSede_Result> lst = new List<WCO_ListarSede_Result>();
            try
            {
                lst = sd.ListadoPaginado(ObjDetalle);
            }
            catch (Exception ex)
            {
                arrayOfLines = JsonConvert.SerializeObject(ex, Newtonsoft.Json.Formatting.Indented);
                //return Content(HttpStatusCode.BadRequest, new ViewModalExite() { success = false, mensaje = ex.Message, valor = -1 });
                arrayOfLines = arrayOfLines + "Error";
            }
            return lst;
        }

        [HttpPost]
        [Route("api/Maestro/MantenimientoSede/{id}")]
        public async Task<IHttpActionResult> MantenimientoSede(int id, [FromBody] WCO_ListarSede_Result ObjDetalle)
        {
            ViewModalExite objLogin = new ViewModalExite();
            HttpStatusCode statusCode = new HttpStatusCode();

            try
            {
                int valor = 0;
                if (id < 1 || id == null) { return NotFound(); }
                else if (id == 1)
                {
                    valor = sd.Insertar(ObjDetalle);
                    if (valor < 0)
                    {
                        objLogin.success = false;
                        objLogin.valor = valor;
                        if (valor == -2)
                        {
                            objLogin.mensaje = "Ya Existe el Nombre. Por favor ingrese un nuevo valor";
                        }
                        if (valor == -3)
                        {
                            objLogin.mensaje = "Ya Existe el código. Por favor ingrese un nuevo valor";
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
                        ObjDetalle.IdSede = valor;
                        objLogin.data = ObjDetalle;
                        statusCode = HttpStatusCode.Created;
                    }
                }
                else if (id == 2)
                {
                    valor = sd.Actualizar(ObjDetalle);
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
                    sd.Inactivar(ObjDetalle);
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


        [Route("api/Maestro/ListadoSedeHistoria")]
        public IEnumerable<WCO_TraerXIdSede_Result> ListadoSedeHistoria(WCO_TraerXIdSede_Result ObjDetalle)
        {
            List<WCO_TraerXIdSede_Result> lst = new List<WCO_TraerXIdSede_Result>();
            try
            {
                lst = sd.ListadoSedeHistoria(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }


        [HttpPost]
        [Route("api/Maestro/MantenimientoSedeCliente/{id}")]
        public async Task<IHttpActionResult> MantenimientoSedeCliente(int id, [FromBody] WCO_ListarSedeCliente_Result ObjDetalle)
        {
            ViewModalExite objLogin = new ViewModalExite();
            HttpStatusCode statusCode = new HttpStatusCode();

            try
            {
                int valor = 0;
                if (id < 1 || id == null) { return NotFound(); }
                else if (id == 1)
                {
                    sd.InsertarSedeCliente(ObjDetalle);
                    if (valor < 0)
                    {
                        objLogin.success = false;
                        objLogin.valor = valor;
                        if (valor == -2)
                        {
                            objLogin.mensaje = "Ya Existe el Nombre. Por favor ingrese un nuevo valor";
                        }
                        if (valor == -3)
                        {
                            objLogin.mensaje = "Ya Existe el código. Por favor ingrese un nuevo valor";
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
                        ObjDetalle.IdSede = valor;
                        objLogin.data = ObjDetalle;
                        statusCode = HttpStatusCode.Created;
                    }
                }

                else if (id == 3)
                {
                    sd.InactivarSedeCliente(ObjDetalle);
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


        [Route("api/Maestro/ListadoSedePrinter")]
        public IEnumerable<WCO_ListarSedePrinter_Result> ListadoSedePrinter(WCO_ListarSedePrinter_Result ObjDetalle)
        {
            List<WCO_ListarSedePrinter_Result> lst = new List<WCO_ListarSedePrinter_Result>();
            try
            {
                lst = sd.ListadoSedePrinter(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        [HttpPost]
        [Route("api/Maestro/MantenimientoSedePrinter/{id}")]
        public async Task<IHttpActionResult> MantenimientoSedePrinter(int id, [FromBody] WCO_ListarSedePrinter_Result ObjDetalle)
        {
            ViewModalExite objLogin = new ViewModalExite();
            HttpStatusCode statusCode = new HttpStatusCode();

            try
            {
                int valor = 0;
                if (id < 1 || id == null) { return NotFound(); }
                else if (id == 1)
                {
                    sd.InsertarSedePrinter(ObjDetalle);
                    if (valor < 0)
                    {
                        objLogin.success = false;
                        objLogin.valor = valor;
                        if (valor == -2)
                        {
                            objLogin.mensaje = "Ya Existe el Nombre. Por favor ingrese un nuevo valor";
                        }
                        if (valor == -3)
                        {
                            objLogin.mensaje = "Ya Existe el código. Por favor ingrese un nuevo valor";
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

                else if (id == 3)
                {
                    sd.EliminarSedePrinter(ObjDetalle);
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

        [Route("api/Maestro/WCO_ListarSucursal")]
        public IEnumerable<WCO_ListarSucursal_Result> WCO_ListarSucursal(WCO_ListarSucursal_Result ObjDetalle)
        {
            List<WCO_ListarSucursal_Result> lst = new List<WCO_ListarSucursal_Result>();
            try
            {
                lst = sd.WCO_ListarSucursal(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }


        #endregion

        #region SedeCompartida

        [Route("api/Maestro/ListaSedeCompartida")]
        public IEnumerable<WCO_ListarSedeCompartida_Result> ListaSedeCompartida(WCO_ListarSedeCompartida_Result ObjDetalle)
        {
            string arrayOfLines = "";
            List<WCO_ListarSedeCompartida_Result> lst = new List<WCO_ListarSedeCompartida_Result>();
            try
            {
                lst = sd.ListarSedeCompartida(ObjDetalle);
            }
            catch (Exception ex)
            {
                arrayOfLines = JsonConvert.SerializeObject(ex, Newtonsoft.Json.Formatting.Indented);
                //return Content(HttpStatusCode.BadRequest, new ViewModalExite() { success = false, mensaje = ex.Message, valor = -1 });
                arrayOfLines = arrayOfLines + "Error";
            }
            return lst;
        }

        [HttpPost]
        [Route("api/Maestro/MantenimientoSedeCompartida/{id}")]
        public async Task<IHttpActionResult> MantenimientoSedeCompartida(int id, [FromBody] WCO_ListarSedeCompartida_Result ObjDetalle)
        {
            ViewModalExite objLogin = new ViewModalExite();
            HttpStatusCode statusCode = new HttpStatusCode();

            try
            {
                sd.MantenimientoSedeCompartida(id, ObjDetalle);
                objLogin.success = true;
                objLogin.valor = 1;
                objLogin.mensaje = "Created";
                objLogin.data = ObjDetalle;
                statusCode = HttpStatusCode.OK;
                return Content(statusCode, objLogin);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, new ViewModalExite() { success = false, mensaje = ex.Message, valor = -1 });
            }
        }



        #endregion

        //#region ClasificadorComponente

        //[Route("api/Maestro/ListaSede")]
        //public IEnumerable<WCO_Listacla_Result> ListaSede(WCO_ListarSede_Result ObjDetalle)
        //{
        //    List<WCO_ListarSede_Result> lst = new List<WCO_ListarSede_Result>();
        //    try
        //    {
        //        lst = sd.ListadoPaginado(ObjDetalle);
        //    }
        //    catch
        //    {

        //    }
        //    return lst;
        //}
        //#endregion

        #region TipoCambio 
        [HttpPost]
        [Route("api/Admision/ListadoTipoCambio")]
        public IEnumerable<WCO_ListarTipoCambioMo_Result> ListadoTipoCambio(WCO_ListarTipoCambioMo_Result ObjDetalle)
        {
            List<WCO_ListarTipoCambioMo_Result> lst = new List<WCO_ListarTipoCambioMo_Result>();
            try
            {
                lst = tipcam.ListarTipoPago(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        [Authorize]
        [HttpPost]
        [Route("api/Admision/MantenimientoTipoCambio/{id}")]
        public async Task<IHttpActionResult> MantenimientoTipoCambio(int id, [FromBody] WCO_ListarTipoCambioMo_Result ObjDetalle)
        {
            ViewModalExite objLogin = new ViewModalExite();
            HttpStatusCode statusCode = new HttpStatusCode();
            try
            {
                int valor = 0;
                switch (id)
                {
                    case 1:
                        valor = tipcam.Insertar(ObjDetalle);
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
                            objLogin.mensaje = "Se creó con exito el tipo de cambio";
                            objLogin.data = ObjDetalle;
                            statusCode = HttpStatusCode.Created;
                        }
                        break;
                    case 2:
                        valor = tipcam.Actualizar(ObjDetalle);
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
                            objLogin.mensaje = "Se actualizó el tipo de cambio";
                            statusCode = HttpStatusCode.OK;
                        }
                        break;

                    case 3:
                        tipcam.Inactivar(ObjDetalle);
                        objLogin.success = true;
                        objLogin.valor = 1;
                        objLogin.mensaje = "Se inactivó el tipo de cambio";
                        statusCode = HttpStatusCode.OK;
                        break;
                }
                return Content(statusCode, objLogin);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, new ViewModalExite() { success = false, mensaje = ex.Message, valor = -1 });
            }
        }


        #endregion

        #region Muestra
        [HttpPost]
        [Route("api/Maestro/ListadoMuestra")]
        public IEnumerable<WCO_ListarMuestra_Result> ListadoMuestra(WCO_ListarMuestra_Result ObjDetalle)
        {
            List<WCO_ListarMuestra_Result> lst = new List<WCO_ListarMuestra_Result>();
            try
            {
                lst = serv.ListarMuestra(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        [Authorize]
        [HttpPost]
        [Route("api/Maestro/MantenimientoMuestra/{id}")]
        public async Task<IHttpActionResult> MantenimientoMuestra(int id, [FromBody] WCO_ListarMuestra_Result ObjDetalle)
        {
            ViewModalExite objLogin = new ViewModalExite();
            HttpStatusCode statusCode = new HttpStatusCode();

            try
            {
                statusCode = HttpStatusCode.OK;
                int valor = 0;
                if (id < 1 || id == null) { return NotFound(); }

                if (id == 1)
                {
                    valor = serv.InsertarMuestra(ObjDetalle);
                    if (valor < 0)
                    {
                        objLogin.success = false;
                        objLogin.valor = 0;
                        objLogin.mensaje = "Los campos ingresados coinciden con un registro en nuestra base. Por favor ingrese un nuevo valor";
                        objLogin.data = null;

                    }
                    else
                    {
                        objLogin.success = true;
                        objLogin.valor = 1;
                        objLogin.mensaje = "Created";
                        objLogin.data = ObjDetalle;
                        statusCode = HttpStatusCode.Created;
                    }
                }

                if (id == 3)
                {
                    valor = serv.InactivarMuestra(ObjDetalle);
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

        #region ListaBase
        [HttpPost]
        [Route("api/Maestro/ListadoBase")]
        public IEnumerable<WCO_ListarListaBase_Result> ListadoBase(WCO_ListarListaBase_Result ObjDetalle)
        {
            List<WCO_ListarListaBase_Result> lst = new List<WCO_ListarListaBase_Result>();
            try
            {
                ADDAT_ListaBase Base = new ADDAT_ListaBase();
                lst = Base.ListadoBasePaginado(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        [Authorize]
        [HttpPost]
        [Route("api/Maestro/MantenimientoBase/{id}")]
        public async Task<IHttpActionResult> MantenimientoBase(int id, [FromBody] WCO_ListarListaBase_Result ObjDetalle)
        {
            ViewModalExite objLogin = new ViewModalExite();
            HttpStatusCode statusCode = new HttpStatusCode();
            ADDAT_ListaBase Base = new ADDAT_ListaBase();
            try
            {
                statusCode = HttpStatusCode.OK;
                int valor = 0;
                if (id < 1 || id == null) { return NotFound(); }

                if (id == 1)
                {
                    valor = Base.Insertar(ObjDetalle);
                    if (valor < 0)
                    {
                        objLogin.success = false;
                        objLogin.valor = 0;
                        objLogin.mensaje = "Los campos ingresados coinciden con un registro en nuestra base. Por favor ingrese un nuevo valor";
                        objLogin.data = null;

                    }
                    else
                    {
                        objLogin.success = true;
                        objLogin.valor = 1;
                        objLogin.mensaje = "Created";
                        objLogin.data = ObjDetalle;
                        statusCode = HttpStatusCode.Created;
                    }
                }

                //if (id == 3)
                //{
                //    valor = Base.Inactivar(ObjDetalle);
                //    objLogin.success = true;
                //    objLogin.valor = 1;
                //    objLogin.mensaje = "Ok";
                //    statusCode = HttpStatusCode.OK;
                //}
                //else
                //{
                //    return BadRequest();
                //}
                return Content(statusCode, objLogin);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, new ViewModalExite() { success = false, mensaje = ex.Message, valor = -1 });
            }
        }


        #endregion


        #region ListaBaseComponente
        [HttpPost]
        [Route("api/Maestro/ListadoBaseComponente")]
        public IEnumerable<WCO_ListarComponentesdeListaB_Result> ListaBaseComponente(WCO_ListarComponentesdeListaB_Result ObjDetalle)
        {
            List<WCO_ListarComponentesdeListaB_Result> lst = new List<WCO_ListarComponentesdeListaB_Result>();
            try
            {
                ADDAT_ListaBase Base = new ADDAT_ListaBase();
                lst = Base.ListaBaseComponente(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        [Authorize]
        [HttpPost]
        [Route("api/Maestro/MantenimientoBaseComponente/{id}")]
        public async Task<IHttpActionResult> MantenimientoBaseComponente(int id, [FromBody] WCO_ListarComponentesdeListaB_Result ObjDetalle)
        {
            ViewModalExite objLogin = new ViewModalExite();
            HttpStatusCode statusCode = new HttpStatusCode();
            ADDAT_ListaBase Base = new ADDAT_ListaBase();
            try
            {
                statusCode = HttpStatusCode.OK;
                int valor = 0;
                if (id == 1)
                {
                    valor = Base.InsertarComponente(ObjDetalle);
                    if (valor < 0)
                    {
                        objLogin.success = false;
                        objLogin.valor = 0;
                        objLogin.mensaje = "Los campos ingresados coinciden con un registro en nuestra base. Por favor ingrese un nuevo valor";
                        objLogin.data = null;

                    }
                    else
                    {
                        objLogin.success = true;
                        objLogin.valor = 1;
                        objLogin.mensaje = "Created";
                        objLogin.data = ObjDetalle;
                        statusCode = HttpStatusCode.Created;
                    }
                }
                if (id == 2)
                {
                    Base.ActualizarComponente(ObjDetalle);
                    objLogin.success = true;
                    objLogin.valor = 1;
                    objLogin.mensaje = "Ok";
                    statusCode = HttpStatusCode.OK;
                }
                if (id == 3)
                {
                    Base.InactivarComponente(ObjDetalle);
                    objLogin.success = true;
                    objLogin.valor = 1;
                    objLogin.mensaje = "Ok";
                    statusCode = HttpStatusCode.OK;
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
