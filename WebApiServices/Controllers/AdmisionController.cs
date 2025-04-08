using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiServices.Models;
using WebApiServices.Models.Datos;
using WebApiServices.Models.Entidades;
using WebApiServices.Models.Request;
using WebApiServices.Models.Response;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace WebApiServices.Controllers
{
    public class AdmisionController : ApiController
    {
        Metodos m = new Metodos();
        UT_Kerberos Ker = new UT_Kerberos();
        ADDAT_ClasificadorMovimiento cm = new ADDAT_ClasificadorMovimiento();
        ADDAT_Sede sd = new ADDAT_Sede();
        ADDAT_TipoOperacion tp = new ADDAT_TipoOperacion();
        ADDAT_Admision adm = new ADDAT_Admision();
        CODAT_Componente serv = new CODAT_Componente();
        ADDAT_PersonaMast per = new ADDAT_PersonaMast();

        #region Admision
        [HttpPost]
        [Route("api/Admision/GenerarAdmision")]
        public IHttpActionResult GenerarAdmision(Admision Model)
        {//ViewAdmision
            ViewAdmision objLogin = new ViewAdmision();
            try
            {
                ADBE_Admision objAdmision = new ADBE_Admision();
                objAdmision = m.GenerarAdmision(Model.list_Admision);

                if (!string.IsNullOrEmpty(objAdmision.NroPeticion))
                {
                    objLogin.success = true;
                    objLogin.valor = 1;
                    objLogin.mensaje = "Ok|" + objAdmision.Comentario;
                    objLogin.NroPeticion = objAdmision.NroPeticion;
                    objLogin.OrdenAtencion = objAdmision.OrdenAtencion;
                    objLogin.IdAdmision = objAdmision.PK.IdAdmision;
                    objLogin.Comentario = objAdmision.Comentario;
                }
                else
                {
                    objLogin.success = true;
                    objLogin.valor = 0;
                    objLogin.mensaje = "Error|" + objAdmision.Comentario;
                    objLogin.NroPeticion = "";
                    objLogin.OrdenAtencion = "";
                    objLogin.IdAdmision = 0;
                    objLogin.Comentario = objAdmision.Comentario;
                }
            }
            catch (Exception ex)
            {
                objLogin.success = false;
                objLogin.mensaje = ex.Message;
                objLogin.valor = 0;
            }
            return Ok(objLogin);
        }

        [HttpPost]
        [Route("api/Admision/ListarAdmision")]
        public IEnumerable<WCO_ListarAdmisionServicio_Result> ListarAdmision(WCO_ListarAdmisionServicio_Result ObjDetalle)
        {
            List<WCO_ListarAdmisionServicio_Result> lst = new List<WCO_ListarAdmisionServicio_Result>();
            try
            {
                lst = adm.ListadoPaginado(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        [HttpPost]
        [Route("api/Admision/ListadoAdmisionConstancia")]
        public IEnumerable<WCO_ListarAdmisionServicioConstancia_Result> ListadoAdmisionConstancia(WCO_ListarAdmisionServicio_Result ObjDetalle)
        {
            List<WCO_ListarAdmisionServicioConstancia_Result> lst = new List<WCO_ListarAdmisionServicioConstancia_Result>();
            try
            {
                lst = adm.ListadoAdmisionServicioConstancia(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        [HttpPost]
        [Route("api/Admision/ListadoConstancia")]
        public IEnumerable<WCO_ListarAdmisionServicioConstanciaDetalle_Result> ListadoConstancia(WCO_ListarAdmisionServicio_Result ObjDetalle)
        {
            List<WCO_ListarAdmisionServicioConstanciaDetalle_Result> lst = new List<WCO_ListarAdmisionServicioConstanciaDetalle_Result>();
            try
            {
                lst = adm.ListadoAdmisionConstancia(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        [HttpPost]
        [Route("api/Admision/ListadoReporte")]
        public ViewModalReporte ListadoReporte(ImpresionRequest ObjDetalle)
        {
            string request;
            ViewModalReporte result = new ViewModalReporte();
            try
            {
                WsReporte.WServiceReporte Ws = new WsReporte.WServiceReporte();
                WsReporte.ImpresionRequest Obj = new WsReporte.ImpresionRequest();
                Obj.IdAdmision = ObjDetalle.IdAdmision;
                Obj.IdReporte = ObjDetalle.IdReporte;
                Obj.NroPeticion = ObjDetalle.NroPeticion;
                request = Ws.ListadoImpresion(Obj);
                string Jsons = Newtonsoft.Json.JsonConvert.SerializeObject(request);
                result = (ViewModalReporte)Newtonsoft.Json.JsonConvert.DeserializeObject(request, typeof(ViewModalReporte));
            }
            catch (Exception ex)
            {
                result.CodigoError = 200;
                result.MensajeError = "ERROR_SERVICIO   : PROBLEMAS DE COMUNICACION";
                result.ValorDevolucion = "";
                result.LineaError = 0;
                return result;
            }
            return result;
        }

        [HttpPost]
        [Route("api/Admision/TraerXAdmisionServicio")]
        public IHttpActionResult TraerXAdmisionServicio(WCO_TraerXAdmisionServicio_Result ObjDetalle)
        {
            RequestAdmision lst = new RequestAdmision();
            try
            {
                lst = adm.TraerXAdmisionServicio(ObjDetalle);
            }
            catch
            {
                return Content(HttpStatusCode.Unauthorized, new Response() { Message = "Acceso denegado.", IsSucces = false });
            }
            return Ok(lst);
        }

        [HttpPost]
        [Route("api/Admision/CambioContratoTipoPaciente/{id}")]
        public IHttpActionResult CambioContratoTipoPaciente(int id, [FromBody] RequestAdmision ObjDetalle)
        {
            HttpStatusCode statusCode = new HttpStatusCode();
            RequestAdmision lstAdm = new RequestAdmision();
            string observacion = "";
            string examen = "";

            try
            {
                int k = 0;
                foreach (WCO_ListarAdmisionServicioDetalle_Result drow in ObjDetalle.list_AdmisionServicio)
                {
                    if (drow.EstadoAdm.Trim() == "Terminado")
                    {
                        k = 2;
                        break;
                    }
                }
                if (k == 0)
                {
                    List<WCO_ListarAdmisionServicioDetalle_Result> lstDetalleCopia = new List<WCO_ListarAdmisionServicioDetalle_Result>();
                    List<WCO_ListarComponente_Result> lstCopia = new List<WCO_ListarComponente_Result>();
                    List<WCO_ListarComponente_Result> lst = new List<WCO_ListarComponente_Result>();
                    WCO_ListarComponente_Result BEComponente = new WCO_ListarComponente_Result();


                    BEComponente.TipoOperacionID = (int)ObjDetalle.list_AdmisionServicio[0].TipoOperacionID;
                    BEComponente.Estado = 1;
                    lst = serv.ListadoPaginado(BEComponente);
                    foreach (WCO_ListarAdmisionServicioDetalle_Result drow in ObjDetalle.list_AdmisionServicio)
                    {
                        List<WCO_ListarComponente_Result> LISTNEWCAB = lst.Where(x => x.CodigoComponente == drow.CodigoComponente).ToList();
                        if (LISTNEWCAB.Count > 0)
                        {
                            foreach (WCO_ListarComponente_Result xxrow in LISTNEWCAB)
                            {
                                lstCopia.Add(xxrow);
                            }
                        }
                        else
                        {
                            examen = drow.CodigoComponente.Trim();
                            k = 1;
                            break;
                        }
                    }
                    if (k == 0)
                    {
                        foreach (WCO_ListarAdmisionServicioDetalle_Result dr in ObjDetalle.list_AdmisionServicio)
                        {
                            foreach (WCO_ListarComponente_Result row in lstCopia)
                            {
                                if (row.CodigoComponente.ToString().Equals(dr.CodigoComponente.ToString()))
                                {
                                    WCO_ListarAdmisionServicioDetalle_Result rw1 = new WCO_ListarAdmisionServicioDetalle_Result();
                                    rw1.Descripcion = row.Descripcion;
                                    rw1.CodigoComponente = row.CodigoComponente;
                                    rw1.EstadoAdm = dr.EstadoAdm.ToString();
                                    rw1.Valor = (decimal)row.Valor;
                                    rw1.CanTotal = rw1.Valor * (int)dr.Cantidad;
                                    rw1.Cantidad = (int)dr.Cantidad;
                                    rw1.Estado = dr.Estado;
                                    rw1.Sexo = dr.Sexo.ToString();
                                    rw1.ClasificadorMovimiento = dr.ClasificadorMovimiento.ToString();
                                    string Estado = dr.Estado.ToString();
                                    if (Estado != "1")
                                    {
                                        ADBE_AdmisionServicio objBEAdmisionServicio = new ADBE_AdmisionServicio();
                                        objBEAdmisionServicio.Cantidad = rw1.Cantidad;
                                        objBEAdmisionServicio.Estado = rw1.Estado;
                                        objBEAdmisionServicio.Valor = rw1.Valor;
                                        //objBEAdmisionServicio.Valor = rw1.Valor;
                                        objBEAdmisionServicio.PK.IdAdmServicio = rw1.IdAdmServicio;
                                        objBEAdmisionServicio.PK.Componente.Pk.CodigoComponente = rw1.CodigoComponente;
                                        objBEAdmisionServicio.PK.Admision.PK.IdAdmision = ObjDetalle.Admision.IdAdmision;
                                        objBEAdmisionServicio.PK.UnidadNegocio.Pk.UneuNegocioId = ObjDetalle.Admision.UneuNegocioId;
                                        objBEAdmisionServicio.UsuarioCreacion = ObjDetalle.Admision.UsuarioCreacion;
                                        objBEAdmisionServicio.IpCreacion = ObjDetalle.Admision.IpCreacion;
                                        objBEAdmisionServicio.UsuarioModificacion = ObjDetalle.Admision.UsuarioModificacion;
                                        objBEAdmisionServicio.IpModificacion = ObjDetalle.Admision.IpModificacion;
                                        adm.ActualizarAdmisionDetalle(objBEAdmisionServicio);
                                    }
                                    lstDetalleCopia.Add(rw1);
                                    break;
                                }
                            }
                        }
                        ObjDetalle.list_AdmisionServicio = lstDetalleCopia;
                    }
                }
                if (k == 0)
                {
                    ObjDetalle.Admision.TipoOperacionId = ObjDetalle.list_AdmisionServicio[0].TipoOperacionID;
                    observacion += "Esta Petición ha cambiado de Contrato";
                }
                if (k == 1)
                {
                    observacion += "Este tipo Paciente no tiene configurado este examen: " + examen;
                }
                if (k == 2)
                {
                    observacion += "Esta Petición ya tiene un examen Terminado";
                }
                lstAdm = ObjDetalle;
                lstAdm.mensaje = observacion;
                return Content(statusCode, lstAdm);
            }
            catch (Exception ex)
            {            
                return Content(HttpStatusCode.BadRequest, new ViewModalExite() { success = false, mensaje = ex.Message, valor = -1, IsSucces = false });
            }
            //return Ok(lst);
        }

        [Authorize]
        [HttpPost]
        [Route("api/Admision/RegistrarAdmision/{id}")]
        public async Task<IHttpActionResult> RegistrarAdmision(int id, [FromBody] RequestAdmision ObjDetalle)
        {
            HttpStatusCode statusCode = new HttpStatusCode();
            RequestAdmision lstAdm = new RequestAdmision();
            string arrayOfLines = "";
            List<WCO_ListarAdmisionServicioDetalle_Result> list_ServicioGrupo = new List<WCO_ListarAdmisionServicioDetalle_Result>();
            List<WCO_ListarAdmisionServicioDetalle_Result> list_Servicio = new List<WCO_ListarAdmisionServicioDetalle_Result>();
            List<ADBE_AdmisionServicio> lst = new List<ADBE_AdmisionServicio>();
            ADBE_WServicioLog obj_pWServicioLog = new ADBE_WServicioLog();
            string arrayOfiNICIA = System.DateTime.Now + " | " + "Ingreso|RegistrarAdmision";
            obj_pWServicioLog.Pk.IdCodigo = System.DateTime.Now.Year + "" + System.DateTime.Now.Month.ToString("D2") + System.DateTime.Now.Day.ToString("D2");
            obj_pWServicioLog.TipoMsj = "RegistrarAdmision";
            obj_pWServicioLog.FechaIni = System.DateTime.Now;
            obj_pWServicioLog.IdSede = ObjDetalle.Admision.IdSede;
            obj_pWServicioLog.CodigoOA = ObjDetalle.Admision.OrdenAtencion;

            try
            {
                lstAdm.mensaje = "";
                ADBE_Admision BE_pAdmision = new ADBE_Admision();
                BE_pAdmision.UneuNegocioId = ObjDetalle.Admision.UneuNegocioId;
                BE_pAdmision.FechaAdmision = ObjDetalle.Admision.FechaAdmision;
                BE_pAdmision.PK.Persona.PK.IdPersona = ObjDetalle.Admision.Persona;
                BE_pAdmision.PK.TipoOperacion.Pk.TipoOperacionId = ObjDetalle.Admision.TipoOperacionId;
                BE_pAdmision.MedicoId = ObjDetalle.Admision.MedicoId;
                BE_pAdmision.PK.Sede.PK.IdSede = ObjDetalle.Admision.IdSede;
                BE_pAdmision.HistoriaClinica = ObjDetalle.Admision.HistoriaClinica;
                BE_pAdmision.OrdenAtencion = ObjDetalle.Admision.OrdenAtencion;
                BE_pAdmision.TipoAtencion = ObjDetalle.Admision.TipoAtencion;
                BE_pAdmision.TipoOrden = ObjDetalle.Admision.TipoOrden;
                BE_pAdmision.Cama = ObjDetalle.Admision.Cama;
                BE_pAdmision.CoaSeguro = ObjDetalle.Admision.CoaSeguro;
                if (!string.IsNullOrEmpty(ObjDetalle.Admision.ClasificadorMovimiento.ToString())) BE_pAdmision.ClasificadorMovimiento = ObjDetalle.Admision.ClasificadorMovimiento;
                BE_pAdmision.OrdenSinapa = ObjDetalle.Admision.OrdenSinapa;
                BE_pAdmision.FlatCoaSeguro = ObjDetalle.Admision.FlatCoaSeguro;
                BE_pAdmision.FlatMovilidad = ObjDetalle.Admision.FlatMovilidad;
                BE_pAdmision.Afecto = ObjDetalle.Admision.Afecto;
                BE_pAdmision.Igv = ObjDetalle.Admision.Igv;
                if (ObjDetalle.Admision.ObservacionAlta != null)
                {
                    if (!string.IsNullOrEmpty(ObjDetalle.Admision.ObservacionAlta.ToString()))
                    {
                        BE_pAdmision.Comentario = ObjDetalle.Admision.ObservacionAlta.ToUpper();
                    }
                }
                BE_pAdmision.IdEspecialidad = ObjDetalle.Admision.IdEspecialidad;
                BE_pAdmision.AseguradoraId = ObjDetalle.Admision.IdAseguradora;
                //BE_pAdmision.IdAprobador = ObjDetalle.Admision.IdAprobador;
                BE_pAdmision.PK.Empresa.PK.IdPersona = ObjDetalle.Admision.IdEmpresaPaciente;
                BE_pAdmision.PK.Empresa.idEmpresaEmpleadora = ObjDetalle.Admision.IdSedeEmpresa;
                BE_pAdmision.Estado = ObjDetalle.Admision.Estado;
                BE_pAdmision.FechaCreacion = ObjDetalle.Admision.FechaCreacion;
                BE_pAdmision.UsuarioCreacion = ObjDetalle.Admision.UsuarioCreacion;
                BE_pAdmision.IpCreacion = ObjDetalle.Admision.IpCreacion;
                BE_pAdmision.FechaModificacion = ObjDetalle.Admision.FechaModificacion;
                BE_pAdmision.UsuarioModificacion = ObjDetalle.Admision.UsuarioModificacion;
                BE_pAdmision.IpModificacion = ObjDetalle.Admision.IpModificacion;
                BE_pAdmision.Total = ObjDetalle.Admision.Total;
                BE_pAdmision.Anticipo = ObjDetalle.Admision.Anticipo;
                BE_pAdmision.Saldo = ObjDetalle.Admision.Saldo;
                BE_pAdmision.Redondeo = ObjDetalle.Admision.Redondeo;
                if (!string.IsNullOrEmpty(ObjDetalle.Admision.TipoPaciente.ToString())) BE_pAdmision.TipoPacienteId = ObjDetalle.Admision.TipoPaciente;

                DataTable dtPerfil = new DataTable();
                DataTable dt_Orden = new DataTable();
                int ret = 0;
                int CantRoe = 0;
                string observacion = "";
                string msjObserva = "";
                #region InsertarOA

                #endregion

                #region UpdateOA

                #endregion

                #region AnularOA

                #endregion
                statusCode = HttpStatusCode.OK;
                lstAdm.valor = 0;
                if (id == 1)
                {
                    if (ObjDetalle.IndicadorWS == 1)
                    {
                        ADDAT_Sede sd = new ADDAT_Sede();
                        if (string.IsNullOrEmpty(ObjDetalle.Admision?.apellidopaterno))
                            throw new CampoInvalidoException("ApellidoPaterno", "El campo 'Apellido Paterno' es obligatorio.");
                        if (string.IsNullOrEmpty(ObjDetalle.Admision?.sexo))
                            throw new CampoInvalidoException("Sexo", "El campo 'Sexo' es obligatorio.");
                        if (string.IsNullOrEmpty(ObjDetalle.Admision?.nombres))
                            throw new CampoInvalidoException("nombres", "El campo 'nombres' es obligatorio.");
                        if (string.IsNullOrEmpty(ObjDetalle.Admision?.Documento))
                            throw new CampoInvalidoException("Documento", "El campo 'Documento' es obligatorio.");
                        if (string.IsNullOrEmpty(ObjDetalle.Admision?.TipoDocumento))
                            throw new CampoInvalidoException("TipoDocumento", "El campo 'TipoDocumento' es obligatorio.");

                        if (!string.IsNullOrEmpty(ObjDetalle.Admision.apellidopaterno)) BE_pAdmision.PK.Persona.ApellidoPaterno = ObjDetalle.Admision.apellidopaterno.Trim().Replace("'", "");
                        if (!string.IsNullOrEmpty(ObjDetalle.Admision.apellidomaterno)) BE_pAdmision.PK.Persona.ApellidoMaterno = ObjDetalle.Admision.apellidomaterno.Trim().Replace("'", "");
                        if (!string.IsNullOrEmpty(ObjDetalle.Admision.nombres)) BE_pAdmision.PK.Persona.Nombres = ObjDetalle.Admision.nombres.Trim().Replace("'", "");
                        if (!string.IsNullOrEmpty(ObjDetalle.Admision.Documento)) BE_pAdmision.PK.Persona.PerNumeroDocumento = ObjDetalle.Admision.Documento.Trim().Replace("'", "");
                        if (!string.IsNullOrEmpty(ObjDetalle.Admision.TipoDocumento.ToString())) BE_pAdmision.PK.Persona.PerTipoDoc = ObjDetalle.Admision.TipoDocumento;
                        if (!string.IsNullOrEmpty(ObjDetalle.Admision.CorreoElectronico.ToString())) BE_pAdmision.PK.Persona.PerCorreoElectronico = ObjDetalle.Admision.CorreoElectronico;
                        if (!string.IsNullOrEmpty(ObjDetalle.Admision.fechanacimiento.ToString())) BE_pAdmision.PK.Persona.PerFechaNacimiento = ObjDetalle.Admision.fechanacimiento;
                        if (!string.IsNullOrEmpty(ObjDetalle.Admision.sexo.ToString())) BE_pAdmision.PK.Persona.PerSexo = ObjDetalle.Admision.sexo.Trim().Replace("'", "");
                        if (!string.IsNullOrEmpty(ObjDetalle.Admision.FechaLimite.ToString())) BE_pAdmision.FechaLimite = ObjDetalle.Admision.FechaLimite;
                        if (!string.IsNullOrEmpty(ObjDetalle.Admision.Documento.ToString()))
                        {
                            if (!string.IsNullOrEmpty(ObjDetalle.Admision.TipoDocumento.ToString()))
                            {
                                BE_pAdmision.PK.Persona.PerNumeroDocumento = ObjDetalle.Admision.Documento.ToString().Trim();
                                if (ObjDetalle.Admision.TipoDocumento == "D")
                                {
                                    if (ObjDetalle.Admision.Documento.ToString().Length != 8)
                                    {
                                        //lstAdm.valor = 0;
                                        msjObserva = "El Documento de Identidad no tiene los digitos requeridos " + ObjDetalle.Admision.Documento + ", se deben actualizar para poder registrarlos";
                                        //observacion += CantRoe.ToString() + "|" + "El Documento de Identidad no tiene los digitos requeridos " + ObjDetalle.Admision.Documento + ", se deben actualizar para poder registrarlos";
                                        //lstAdm.Admision = ObjDetalle.Admision;
                                        //lstAdm.Mensaje = observacion;
                                        //return Content(statusCode, lstAdm);
                                    }
                                }
                            }
                            else
                            {
                                observacion += CantRoe.ToString() + "|" + "El Paciente no cuenta con un Tipo Documento " + ObjDetalle.Admision.Documento + ", se deben actualizar para poder registrarlos";
                            }
                        }
                        else
                        {
                            lstAdm.valor = 0;
                            observacion += CantRoe.ToString() + "|" + "El Paciente no cuenta con un Documento de Identidad " + ObjDetalle.Admision.Documento + ", se deben actualizar para poder registrarlos";
                            lstAdm.Admision = ObjDetalle.Admision;
                            lstAdm.mensaje = observacion;
                            return Content(statusCode, lstAdm);
                        }

                        if (ObjDetalle.Admision.FechaLimite != null)
                        {
                            if (!string.IsNullOrEmpty(ObjDetalle.Admision.FechaLimite.ToString()))
                            {
                                int FecRegistro = (DateTime.Now.Year * 12 * 30) + (DateTime.Now.Month * 30) + (DateTime.Now.Day);
                                DateTime FechaLimite = Convert.ToDateTime(ObjDetalle.Admision.FechaLimite);
                                int FecLimite = (FechaLimite.Year * 12 * 30) + (FechaLimite.Month * 30) + (FechaLimite.Day);
                                if (FecRegistro > FecLimite)
                                {
                                    observacion += CantRoe.ToString() + "|" + "Los exámenes han excedido la fecha limite " + FechaLimite + ", se deben actualizar para poder registrarlos";
                                    lstAdm.valor = 0;
                                    lstAdm.mensaje = observacion;
                                    lstAdm.Admision = ObjDetalle.Admision;
                                    return Content(statusCode, lstAdm);
                                }
                            }
                        }

                        DataTable dt_Examen = new DataTable();
                        DataTable dt_Examendet = new DataTable();
                        ADBE_Admision BE_pAdmisionValida = new ADBE_Admision();
                        BE_pAdmisionValida.PK.Sede.PK.IdSede = ObjDetalle.Admision.IdSede;
                        BE_pAdmisionValida.OrdenAtencion = ObjDetalle.Admision.OrdenAtencion;
                        BE_pAdmisionValida.PK.TipoOperacion.Pk.TipoOperacionId = ObjDetalle.Admision.TipoOperacionId;
                        foreach (WCO_ListarAdmisionServicioDetalle_Result obj_Seleccionados in ObjDetalle.list_AdmisionServicio)
                        {
                            BE_pAdmisionValida.PK.IdAdmision = obj_Seleccionados.IdOrdenAtencion;
                            break;
                        }
                        dt_Examen = adm.ValidarOAExamenRest(BE_pAdmisionValida);

                        WCO_ListarSedeCompartida_Result ObjSedeCompartida = new WCO_ListarSedeCompartida_Result();
                        ObjSedeCompartida.IdSede = ObjDetalle.Admision.IdSede;
                        var lstCompartido = sd.ListarSedeCompartida(ObjSedeCompartida);
                        if (lstCompartido.Count > 0)
                        {
                            foreach (WCO_ListarSedeCompartida_Result obj_Seleccionados in lstCompartido)
                            {
                                BE_pAdmisionValida.PK.Sede.PK.IdSede = obj_Seleccionados.IdSedeCompartida;
                                dt_Examendet = adm.ValidarOAExamenRest(BE_pAdmisionValida);
                                foreach (DataRow drRefere in dt_Examendet.AsEnumerable())
                                {
                                    DataRow rw = dt_Examen.NewRow();
                                    rw["NroPeticion"] = drRefere["NroPeticion"];
                                    rw["OrdenAtencion"] = drRefere["OrdenAtencion"];
                                    rw["IdOrdenAtencion"] = drRefere["IdOrdenAtencion"];
                                    rw["Linea"] = drRefere["Linea"];
                                    rw["Componente"] = drRefere["Componente"];
                                    rw["Estado"] = drRefere["Estado"];
                                    rw["OrdenSinapa"] = drRefere["OrdenSinapa"];
                                    dt_Examen.Rows.Add(rw);
                                }
                             }
                        }

                        int Unidades = 0;
                        string NroPeticion = "";
                        if (dt_Examen.Rows.Count > 0)
                        {
                            foreach (WCO_ListarAdmisionServicioDetalle_Result obj_Seleccionados in ObjDetalle.list_AdmisionServicio)
                            {
                                int aa = 0;
                                for (int i = 0; i < dt_Examen.Rows.Count; i++)
                                {
                                    DataRow _row = dt_Examen.Rows[i];
                                    if (
                                        _row["Componente"].ToString().Trim() == obj_Seleccionados.CodigoComponente.ToString().Trim() &&
                                        _row["Linea"].ToString().Trim() == obj_Seleccionados.Linea.ToString().Trim() &&
                                        _row["IdOrdenAtencion"].ToString().Trim() == obj_Seleccionados.IdOrdenAtencion.ToString().Trim())
                                    {
                                        Unidades++;
                                        aa++;
                                        break;
                                    }
                                }
                                if (aa == 0)
                                {
                                    list_Servicio.Add(obj_Seleccionados);
                                }
                            }
                        }
                        else
                        {
                            list_Servicio = ObjDetalle.list_AdmisionServicio;
                        }

                        foreach (var groupSeleccionados in list_Servicio.GroupBy(u => u.CodigoComponente).ToList())
                        {
                            foreach (WCO_ListarAdmisionServicioDetalle_Result EntiSelecc in list_Servicio)
                            {
                                if (groupSeleccionados.Key == EntiSelecc.CodigoComponente)
                                {
                                    list_ServicioGrupo.Add(EntiSelecc);
                                    break;
                                }
                            }
                        }

                        if (ObjDetalle.list_AdmisionServicio.Count <= Unidades)
                        {
                            DataTable dt_Ser = new DataTable();
                            dt_Ser = selectDisctinct(dt_Examen, "NroPeticion");
                            foreach (DataRow _row in dt_Ser.Rows)
                            {
                                NroPeticion += _row["NroPeticion"].ToString().Trim() + " ";
                            }
                            observacion += "La OA ya fue ingresada con el Nro de Petición " + NroPeticion + " Examenes " + Unidades;
                            lstAdm.valor = 0;
                            lstAdm.Admision = ObjDetalle.Admision;
                            lstAdm.mensaje = observacion;
                            return Content(statusCode, lstAdm);
                        }
                        else
                        {
                            observacion = "Examenes registrado(s) = " + list_ServicioGrupo.Count + "|de " + list_Servicio.Count;
                            if (ObjDetalle.Admision.DesEspecialidad != null) if (!string.IsNullOrEmpty(ObjDetalle.Admision.DesEspecialidad.ToString())) BE_pAdmision.PK.Sede.DescripcionLocal = ObjDetalle.Admision.DesEspecialidad;
                            if (ObjDetalle.Admision.RucEmpresaPaciente != null) if (!string.IsNullOrEmpty(ObjDetalle.Admision.RucEmpresaPaciente.ToString())) BE_pAdmision.PK.Empresa.PerNumeroDocumento = ObjDetalle.Admision.RucEmpresaPaciente;
                            if (ObjDetalle.Admision.EmpresaPaciente != null) if (!string.IsNullOrEmpty(ObjDetalle.Admision.EmpresaPaciente.ToString())) BE_pAdmision.PK.Empresa.NombreCompleto = ObjDetalle.Admision.EmpresaPaciente;
                            if (ObjDetalle.Admision.NombreAseguradora != null) if (!string.IsNullOrEmpty(ObjDetalle.Admision.NombreAseguradora.ToString())) BE_pAdmision.Aseguradora_Nombre = ObjDetalle.Admision.NombreAseguradora;
                            if (ObjDetalle.Admision.PaternoMedico != null) if (!string.IsNullOrEmpty(ObjDetalle.Admision.PaternoMedico.ToString())) BE_pAdmision.PK.Titular.ApellidoPaterno = ObjDetalle.Admision.PaternoMedico.ToString().Trim().Replace("'", "");
                            if (ObjDetalle.Admision.MaternoMedico != null) if (!string.IsNullOrEmpty(ObjDetalle.Admision.MaternoMedico.ToString())) BE_pAdmision.PK.Titular.ApellidoMaterno = ObjDetalle.Admision.MaternoMedico.ToString().Trim().Replace("'", "");
                            if (ObjDetalle.Admision.NombreMedico != null) if (!string.IsNullOrEmpty(ObjDetalle.Admision.NombreMedico.ToString())) BE_pAdmision.PK.Titular.Nombres = ObjDetalle.Admision.NombreMedico.ToString().Trim().Replace("'", "");
                            if (ObjDetalle.Admision.CMP != null) if (!string.IsNullOrEmpty(ObjDetalle.Admision.CMP.ToString())) BE_pAdmision.PK.Titular.PerNumeroDocumento = ObjDetalle.Admision.CMP.ToString().Trim();
                            foreach (WCO_ListarAdmisionServicioDetalle_Result obj_Seleccionados in list_ServicioGrupo)
                            {
                                ADBE_AdmisionServicio objBEAdmisionServicio = new ADBE_AdmisionServicio();
                                objBEAdmisionServicio.PK.Componente.Pk.CodigoComponente = obj_Seleccionados.CodigoComponente;
                                objBEAdmisionServicio.Cantidad = obj_Seleccionados.Cantidad;
                                objBEAdmisionServicio.Valor = obj_Seleccionados.Valor;
                                objBEAdmisionServicio.PK.Componente.CantidadKit = obj_Seleccionados.Valorprecio;
                                objBEAdmisionServicio.PK.UnidadNegocio.Pk.UneuNegocioId = ObjDetalle.Admision.UneuNegocioId;
                                objBEAdmisionServicio.Estado = obj_Seleccionados.Estado;
                                objBEAdmisionServicio.PK.Componente.IdArea = obj_Seleccionados.IdOrdenAtencion;
                                objBEAdmisionServicio.IdCobertura = obj_Seleccionados.Linea;
                                if (!string.IsNullOrEmpty(obj_Seleccionados.IdAdmServicio.ToString())) objBEAdmisionServicio.PK.IdAdmServicio = obj_Seleccionados.IdAdmServicio;
                                objBEAdmisionServicio.UsuarioCreacion = obj_Seleccionados.UsuarioCreacion;
                                objBEAdmisionServicio.IpCreacion = obj_Seleccionados.IpCreacion;
                                objBEAdmisionServicio.UsuarioModificacion = obj_Seleccionados.UsuarioModificacion;
                                objBEAdmisionServicio.IpModificacion = obj_Seleccionados.IpModificacion;
                                lst.Add(objBEAdmisionServicio);
                            }
                            //arrayOfLines = JsonConvert.SerializeObject(BE_pAdmision, Newtonsoft.Json.Formatting.Indented);
                            //UT_Kerberos.WriteLog(System.DateTime.Now + " | " + "Registrar|InsertarAdmisionOA =" + arrayOfLines);
                            using (var context = new BDComercialEntities())
                            {
                                using (var dbContextTransaction = context.Database.BeginTransaction())
                                {
                                    try
                                    {
                                        BE_pAdmision.PK.IdAdmision = adm.InsertarAdmisionOA(BE_pAdmision);
                                        foreach (ADBE_AdmisionServicio obj_Seleccionados in lst)
                                        {
                                            obj_Seleccionados.PK.Admision.PK.IdAdmision = BE_pAdmision.PK.IdAdmision;
                                            obj_Seleccionados.PK.IdAdmServicio = adm.InsertarAdmisionServicioOA(obj_Seleccionados);
                                        }
                                        dbContextTransaction.Commit();
                                    }
                                    catch (Exception ex)
                                    {
                                        dbContextTransaction.Rollback();
                                        throw ex;
                                    }
                                }
                            }
                            //arrayOfLines = JsonConvert.SerializeObject(lst, Newtonsoft.Json.Formatting.Indented);
                            //UT_Kerberos.WriteLog(System.DateTime.Now + " | " + "Registrar Detalle|InsertarAdmisionServicioOA =" + arrayOfLines);

                            WCO_TraerXAdmisionServicio_Result ObjEntidad = new WCO_TraerXAdmisionServicio_Result();
                            ObjEntidad.IdAdmision = Convert.ToInt32(BE_pAdmision.PK.IdAdmision);
                            lstAdm = adm.TraerXAdmisionServicio(ObjEntidad);

                            lstAdm.Admision.ObservacionAlta = msjObserva;
                            if (lstAdm.list_AdmisionServicio.Count > 0)
                            {
                                //ObjDetalle.list_AdmisionServicio.Count()
                                lstAdm.mensaje = "La OA fue ingresada Correctamente Cantidad de Examenes Agregados =" + lstAdm.list_AdmisionServicio.Count();
                                if (list_Servicio.Count() > 0)
                                {
                                    lstAdm.mensaje = lstAdm.mensaje;
                                }
                            }
                            else
                            {
                                lstAdm.mensaje = "La OA fue ingresada sin detalle";
                            }
                        }
                    }
                    if (ObjDetalle.IndicadorWS != 1) //Nuevas manuales
                    {                       

                        foreach (WCO_ListarAdmisionServicioDetalle_Result obj_Seleccionados in ObjDetalle.list_AdmisionServicio)
                        {
                            ADBE_AdmisionServicio objBEAdmisionServicio = new ADBE_AdmisionServicio();
                            //objBEAdmisionServicio.PK.Admision.PK.IdAdmision = BE_pAdmision.PK.IdAdmision;
                            objBEAdmisionServicio.PK.Componente.Pk.CodigoComponente = obj_Seleccionados.CodigoComponente;
                            objBEAdmisionServicio.Cantidad = obj_Seleccionados.Cantidad;
                            objBEAdmisionServicio.Valor = obj_Seleccionados.Valor;
                            objBEAdmisionServicio.PK.Componente.CantidadKit = obj_Seleccionados.Valorprecio;
                            objBEAdmisionServicio.PK.UnidadNegocio.Pk.UneuNegocioId = ObjDetalle.Admision.UneuNegocioId;
                            objBEAdmisionServicio.Estado = obj_Seleccionados.Estado;
                            objBEAdmisionServicio.PK.Componente.IdArea = obj_Seleccionados.IdOrdenAtencion;
                            objBEAdmisionServicio.IdCobertura = obj_Seleccionados.Linea;
                            if (!string.IsNullOrEmpty(obj_Seleccionados.IdAdmServicio.ToString())) objBEAdmisionServicio.PK.IdAdmServicio = obj_Seleccionados.IdAdmServicio;
                            objBEAdmisionServicio.UsuarioCreacion = obj_Seleccionados.UsuarioCreacion;
                            objBEAdmisionServicio.IpCreacion = obj_Seleccionados.IpCreacion;
                            objBEAdmisionServicio.UsuarioModificacion = obj_Seleccionados.UsuarioModificacion;
                            objBEAdmisionServicio.IpModificacion = obj_Seleccionados.IpModificacion;
                            lst.Add(objBEAdmisionServicio);
                        }

                        CABE_Transaccion obj_pTransaccion = new CABE_Transaccion();
                        obj_pTransaccion.UsuarioCreacion = BE_pAdmision.UsuarioCreacion;
                        obj_pTransaccion.IdClienteFacturacion = BE_pAdmision.PK.TipoOperacion.Pk.Persona.PK.IdPersona;
                        obj_pTransaccion.NombreCliente = BE_pAdmision.PK.TipoOperacion.Pk.Persona.NombreCompleto;
                        obj_pTransaccion.TipoComprobante = "FA";
                        obj_pTransaccion.Moneda = "LO";
                        obj_pTransaccion.MontoTotalLocal = Convert.ToDecimal("0,00");
                        obj_pTransaccion.MontoTotal = Convert.ToDecimal("0,00");
                        if (lstAdm.mensaje.Length == 0)
                        {
                            if (BE_pAdmision.TipoAtencion == 4)
                            {
                                BE_pAdmision.FlatMovilidad = 1;
                            }
                            using (var context = new BDComercialEntities())
                            {
                                using (var dbContextTransaction = context.Database.BeginTransaction())
                                {
                                    try
                                    {
                                        BE_pAdmision.PK.IdAdmision = adm.InsertarAdmision(BE_pAdmision);
                                        foreach (ADBE_AdmisionServicio obj_Seleccionados in lst)
                                        {
                                            obj_Seleccionados.PK.Admision.PK.IdAdmision = BE_pAdmision.PK.IdAdmision;
                                            obj_Seleccionados.PK.IdAdmServicio = adm.InsertarAdmisionServicio(obj_Seleccionados);
                                        }
                                        dbContextTransaction.Commit();
                                    }
                                    catch (Exception ex)
                                    {
                                        dbContextTransaction.Rollback();
                                        throw ex;
                                    }
                                }
                            }
                            if (ObjDetalle.Admision.ClasificadorMovimiento == "04")
                            {
                                WCO_ListarUsuarioWeb_Result Obj_Mast = new WCO_ListarUsuarioWeb_Result();
                                Obj_Mast.Documento = ObjDetalle.Admision.Documento;
                                Obj_Mast.IdPersona = Convert.ToInt32(ObjDetalle.Admision.Persona);
                                Obj_Mast.ClasificadorMovimiento = ObjDetalle.Admision.ClasificadorMovimiento;
                                int PerUsuvalor = per.InsertarUsuarioWeb(Obj_Mast);
                            }
                            WCO_TraerXAdmisionServicio_Result ObjEntidad = new WCO_TraerXAdmisionServicio_Result();
                            ObjEntidad.IdAdmision = Convert.ToInt32(BE_pAdmision.PK.IdAdmision);
                            lstAdm = adm.TraerXAdmisionServicio(ObjEntidad);
                            BE_pAdmision.NroPeticion = lstAdm.Admision.NroPeticion;

                            if (ObjDetalle.Admision.TIPOADMISIONID == 3)
                            {
                                ////************************************ AGREGAR LOGICA DE LIQUIDACION ***********************/
                                //lstAdm = adm.TraerXAdmisionServicio(ObjEntidad);
                                if (lstAdm.list_AdmisionServicio.Count > 0)
                                {
                                    lstAdm.mensaje = "La OA fue ingresada Correctamente :" + BE_pAdmision.NroPeticion + " " + observacion;// Cantidad de Examenes Agregados =" + lstAdm.list_AdmisionServicio.Count() + "| Pendientes= " + list_Servicio.Count();
                                }
                                else
                                {
                                    lstAdm.mensaje = "La OA fue ingresada sin detalle";
                                }
                            }
                            else
                            {
                                if (ObjDetalle.Admision.FlatAprobacion == 1)
                                {
                                    //lstAdm = adm.TraerXAdmisionServicio(ObjEntidad);
                                    if (lstAdm.list_AdmisionServicio.Count > 0)
                                    {
                                        lstAdm.mensaje = "La OA fue ingresada Correctamente :" + BE_pAdmision.NroPeticion + " " + observacion;// Cantidad de Examenes Agregados =" + lstAdm.list_AdmisionServicio.Count() + "| Pendientes= " + list_Servicio.Count();
                                    }
                                    else
                                    {
                                        lstAdm.mensaje = "La OA fue ingresada sin detalle";
                                    }
                                }
                                else
                                {
                                    obj_pTransaccion.PK.Admision = BE_pAdmision;
                                    ObjDetalle.Admision.ObservacionAlta = "oK-Convenio =" + BE_pAdmision.NroPeticion;
                                    adm.InsertarTransaccion(obj_pTransaccion);
                                    lstAdm = adm.TraerXAdmisionServicio(ObjEntidad);
                                    if (lstAdm.list_AdmisionServicio.Count > 0)
                                    {
                                        lstAdm.mensaje = "La OA fue ingresada Correctamente :" + BE_pAdmision.NroPeticion + " " + observacion;// Cantidad de Examenes Agregados =" + lstAdm.list_AdmisionServicio.Count() + "| Pendientes= " + list_Servicio.Count();
                                    }
                                    else
                                    {
                                        lstAdm.mensaje = "La OA fue ingresada sin detalle";
                                    }
                                }
                            }
                        }
                    }
                }
                if (id == 2)
                {
                    if (ObjDetalle.Admision.NroPeticion != null)
                    {

                    }
                    else
                    {
                        lstAdm.mensaje = "No se puede Actualizar esta Atencion ";
                        statusCode = HttpStatusCode.OK;
                        lstAdm.valor = 0;
                        return Content(statusCode, lstAdm);
                    }
                    if (ObjDetalle.Admision.IdAdmision == 0)
                    {
                        lstAdm.mensaje = "No se puede Actualizar esta Atencion ";
                        statusCode = HttpStatusCode.OK;
                        lstAdm.valor = 0;
                        return Content(statusCode, lstAdm);
                    }
                    foreach (WCO_ListarAdmisionServicioDetalle_Result obj_Seleccionados in ObjDetalle.list_AdmisionServicio)
                    {                        
                        ADBE_AdmisionServicio objBEAdmisionServicio = new ADBE_AdmisionServicio();
                        //objBEAdmisionServicio.PK.Admision.PK.IdAdmision = BE_pAdmision.PK.IdAdmision;
                        objBEAdmisionServicio.PK.Componente.Pk.CodigoComponente = obj_Seleccionados.CodigoComponente;
                        objBEAdmisionServicio.Cantidad = obj_Seleccionados.Cantidad;
                        objBEAdmisionServicio.Valor = obj_Seleccionados.Valor;
                        objBEAdmisionServicio.PK.Componente.CantidadKit = obj_Seleccionados.Valorprecio;
                        objBEAdmisionServicio.PK.UnidadNegocio.Pk.UneuNegocioId = ObjDetalle.Admision.UneuNegocioId;
                        objBEAdmisionServicio.Estado = obj_Seleccionados.Estado;
                        objBEAdmisionServicio.PK.Componente.IdArea = obj_Seleccionados.IdOrdenAtencion;
                        objBEAdmisionServicio.IdCobertura = obj_Seleccionados.Linea;
                        if (!string.IsNullOrEmpty(obj_Seleccionados.IdAdmServicio.ToString())) objBEAdmisionServicio.PK.IdAdmServicio = obj_Seleccionados.IdAdmServicio;
                        objBEAdmisionServicio.UsuarioCreacion = obj_Seleccionados.UsuarioCreacion;
                        objBEAdmisionServicio.IpCreacion = obj_Seleccionados.IpCreacion;
                        objBEAdmisionServicio.UsuarioModificacion = obj_Seleccionados.UsuarioModificacion;
                        objBEAdmisionServicio.IpModificacion = obj_Seleccionados.IpModificacion;
                        lst.Add(objBEAdmisionServicio);
                    }
                                      

                    BE_pAdmision.NroPeticion = ObjDetalle.Admision.NroPeticion.Trim();
                    BE_pAdmision.PK.IdAdmision = ObjDetalle.Admision.IdAdmision;
                    if (BE_pAdmision.TipoAtencion == 4)
                    {
                        BE_pAdmision.FlatMovilidad = 1;
                    }
                    adm.Actualizar(BE_pAdmision);
                    foreach (ADBE_AdmisionServicio obj_Seleccionados in lst)
                    {
                        if (!string.IsNullOrEmpty(ObjDetalle.Admision.NroPeticion.ToString())) BE_pAdmision.NroPeticion = ObjDetalle.Admision.NroPeticion.Trim();
                        obj_Seleccionados.PK.Admision.PK.IdAdmision = BE_pAdmision.PK.IdAdmision;
                        adm.ActualizarAdmisionDetalle(obj_Seleccionados);
                    }
                    if (ObjDetalle.Admision.FlatAprobacion == 1)
                    {

                        ObjDetalle.mensaje = "oK-Convenio/ Se ha Generado para su Aprobacion|" + BE_pAdmision.NroPeticion;
                    }
                    else
                    {
                        CABE_Transaccion obj_pTransaccion = new CABE_Transaccion();
                        obj_pTransaccion.UsuarioCreacion = BE_pAdmision.UsuarioCreacion;
                        obj_pTransaccion.IdClienteFacturacion = BE_pAdmision.PK.TipoOperacion.Pk.Persona.PK.IdPersona;
                        obj_pTransaccion.NombreCliente = BE_pAdmision.PK.TipoOperacion.Pk.Persona.NombreCompleto;
                        obj_pTransaccion.TipoComprobante = "FA";
                        obj_pTransaccion.Moneda = "LO";
                        obj_pTransaccion.MontoTotalLocal = Convert.ToDecimal("0,00");
                        obj_pTransaccion.MontoTotal = Convert.ToDecimal("0,00");
                        obj_pTransaccion.PK.Admision = BE_pAdmision;
                        ObjDetalle.Admision.ObservacionAlta = "oK-Convenio =" + BE_pAdmision.NroPeticion;
                        adm.InsertarTransaccion(obj_pTransaccion);
                        WCO_TraerXAdmisionServicio_Result ObjEntidad = new WCO_TraerXAdmisionServicio_Result();
                        ObjEntidad.IdAdmision = Convert.ToInt32(BE_pAdmision.PK.IdAdmision);
                        lstAdm = adm.TraerXAdmisionServicio(ObjEntidad);
                    }
                }

                if (id == 3)
                {
                    BE_pAdmision.PK.IdAdmision = ObjDetalle.Admision.IdAdmision;
                    if (!string.IsNullOrEmpty(ObjDetalle.Admision.NroPeticion.ToString())) BE_pAdmision.NroPeticion = ObjDetalle.Admision.NroPeticion.Trim();
                    BE_pAdmision.Estado = 3;
                    adm.Actualizar(BE_pAdmision);
                    WCO_TraerXAdmisionServicio_Result ObjEntidad = new WCO_TraerXAdmisionServicio_Result();
                    ObjEntidad.IdAdmision = Convert.ToInt32(BE_pAdmision.PK.IdAdmision);
                    lstAdm = adm.TraerXAdmisionServicio(ObjEntidad);
                }
                lstAdm.valor = 1;
                obj_pWServicioLog.Pk.IdCodigo = lstAdm.Admision.NroPeticion;
                obj_pWServicioLog.Observacion = lstAdm.mensaje;
                arrayOfLines = JsonConvert.SerializeObject(lstAdm, Newtonsoft.Json.Formatting.Indented);
                obj_pWServicioLog.Trama = arrayOfLines;
                obj_pWServicioLog.FechaFin = System.DateTime.Now;
                //m.InsertarWServicioLog(obj_pWServicioLog);
                statusCode = HttpStatusCode.OK;
                arrayOfLines = JsonConvert.SerializeObject(obj_pWServicioLog, Newtonsoft.Json.Formatting.Indented);
                UT_Kerberos.WriteLog(System.DateTime.Now + " | " + "Registrar|InsertarAdmisionOA =" + arrayOfLines);
                return Content(statusCode, lstAdm);
            }
            catch (CampoInvalidoException ex)
            {
                // Retorna un mensaje personalizado con el nombre del campo
                return Content(HttpStatusCode.BadRequest, new ViewModalExite() { success = false, mensaje = $"{ex.Campo}: {ex.Message}", valor = -1 });
            }
            catch (Exception ex)
            {
                // Maneja otros errores genéricos
                return Content(HttpStatusCode.InternalServerError, new ViewModalExite() { success = false, mensaje = "Ocurrió un error inesperado: " + ex.Message, valor = -1 });
            }
        }

        public class CampoInvalidoException : Exception
        {
            public string Campo { get; }

            public CampoInvalidoException(string campo, string mensaje)
                : base(mensaje)
            {
                Campo = campo;
            }
        }



        [Authorize]
        [HttpPost]
        [Route("api/Admision/MantenimientoTransaccion/{id}")]
        public async Task<IHttpActionResult> MantenimientoTransaccion(int id, [FromBody] ViewTransaccion ViewTransaccion)
        {
            ViewModalExite objLogin = new ViewModalExite();
            HttpStatusCode statusCode = new HttpStatusCode();
            try
            {
                int valor = 0;
                if (id > 0)
                {
                    if (id == 1)
                    {
                        CABE_Transaccion obj_pTransaccion = new CABE_Transaccion();
                        obj_pTransaccion.UsuarioCreacion = ViewTransaccion.UsuarioCreacion;
                        obj_pTransaccion.IdClienteFacturacion = ViewTransaccion.IdClienteFacturacion;
                        obj_pTransaccion.NombreCliente = ViewTransaccion.NombreCliente;
                        obj_pTransaccion.TipoComprobante = ViewTransaccion.TipoComprobante;
                        obj_pTransaccion.Moneda = ViewTransaccion.Moneda;
                        obj_pTransaccion.MontoTotalLocal = ViewTransaccion.MontoTotalLocal;
                        obj_pTransaccion.MontoTotal = ViewTransaccion.MontoTotal;
                        obj_pTransaccion.PK.Admision.PK.IdAdmision = ViewTransaccion.IdAdmision;
                        obj_pTransaccion.PK.Admision.NroPeticion = ViewTransaccion.NroPeticion;
                        valor = adm.InsertarTransaccion(obj_pTransaccion);
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
                            objLogin.data = obj_pTransaccion;
                            statusCode = HttpStatusCode.Created;
                        }
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

        [Authorize]
        [HttpPost]
        [Route("api/Admision/ValidarComponentePerfil/{id}")]
        public async Task<IHttpActionResult> ValidarComponentePerfil(int id, [FromBody] RequestAdmision objDetalle)
        {
            RequestAdmision response = new RequestAdmision();
            try
            {
                if (objDetalle == null || objDetalle.list_AdmisionServicio == null || !objDetalle.list_AdmisionServicio.Any())
                {
                    return Content(HttpStatusCode.BadRequest, new ViewModalExite { success = false, mensaje = "Datos de entrada inválidos", valor = -1 });
                }
                var perfiles = ObtenerPerfiles(objDetalle.list_AdmisionServicio);
                var observacion = ValidarComponentesEnPerfiles(perfiles, objDetalle.list_AdmisionServicio);
                 string   arrayOfLines = JsonConvert.SerializeObject(perfiles, Newtonsoft.Json.Formatting.Indented);
                UT_Kerberos.WriteLog(System.DateTime.Now + " | " + "ValidarComponentePerfil =" + arrayOfLines);
                if (!string.IsNullOrEmpty(observacion))
                {
                    response.mensaje = observacion;
                    response.valor = 0;
                    return Content(HttpStatusCode.OK, response);
                }
                response.valor = 1;
                return Content(HttpStatusCode.OK, response);
            }
            catch (SqlException sqlEx)
            {
                return Content(HttpStatusCode.InternalServerError, new ViewModalExite { success = false, mensaje = "Error de base de datos: " + sqlEx.Message, valor = -1 });
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new ViewModalExite { success = false, mensaje = ex.Message, valor = -1 });
            }
        }

        private IEnumerable<DataTable> ObtenerPerfiles(IEnumerable<WCO_ListarAdmisionServicioDetalle_Result> admisionServicios)
        {
            var perfiles = new List<DataTable>();
            foreach (var servicio in admisionServicios)
            {
                var componentePerfil = new ADBE_ComponentePerfil();
                componentePerfil.Pk.Componente.Pk.CodigoComponente = servicio.CodigoComponente;
                componentePerfil.Estado = 1;
                var dtPerfil = adm.ListadoComponentePerfil(componentePerfil);
                if (dtPerfil.Rows.Count > 0)
                {
                    perfiles.Add(dtPerfil);
                }
            }
            return perfiles;
        }

        private string ValidarComponentesEnPerfiles(IEnumerable<DataTable> perfiles, IEnumerable<WCO_ListarAdmisionServicioDetalle_Result> admisionServicios)
        {
            foreach (var perfil in perfiles)
            {
                foreach (DataRow row in perfil.Rows)
                {
                    foreach (var servicio in admisionServicios)
                    {
                        if (servicio.CodigoComponente == row["CodigoHomologado"].ToString())
                        {
                            return $"Este examen {servicio.CodigoComponente} ya está contenido en un Perfil: {row["CodigoComponente"]}";
                        }
                    }
                }
            }
            return string.Empty;
        }

        [Authorize]
        [HttpPost]
        [Route("api/Admision/MantenimientoAdmision/{id}")]
        public async Task<IHttpActionResult> MantenimientoAdmision(int id, [FromBody] WCO_TraerXAdmisionServicio_Result ViewAdmision)
        {
            ViewModalExite objLogin = new ViewModalExite();
            HttpStatusCode statusCode = new HttpStatusCode();
            try
            {
                int valor = 0;
                if (id > 0)
                {
                    if (id == 3)
                    {
                        adm.InactivarAdmisionControl(ViewAdmision);
                        objLogin.success = true;
                        objLogin.valor = valor;
                        objLogin.mensaje = "Inactivar";
                        objLogin.data = ViewAdmision;
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

        [Authorize]
        [HttpPost]
        [Route("api/Admision/MantenimientoAdmisionDetalle/{id}")]
        public async Task<IHttpActionResult> MantenimientoAdmisionDetalle(int id, [FromBody] ADBE_AdmisionServicio ViewAdmision)
        {
            ViewModalExite objLogin = new ViewModalExite();
            HttpStatusCode statusCode = new HttpStatusCode();
            try
            {
                int valor = 0;
                if (id > 0)
                {
                    if (id == 2)
                    {
                        adm.ActualizarAdmisionDetalle(ViewAdmision);
                        objLogin.success = true;
                        objLogin.valor = valor;
                        objLogin.mensaje = "Correcto";
                        objLogin.data = ViewAdmision;
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

        public static DataTable selectDisctinct(DataTable dt, string columnName)
        {
            try
            {
                if (columnName == null || columnName.Length == 0)
                    throw new ArgumentNullException(columnName, "El parámetro no puede ser nulo");
                DataTable distintos = dt.DefaultView.ToTable(true, columnName);
                DataTable aux = new DataTable();
                foreach (DataColumn dc in dt.Columns)
                    aux.Columns.Add(new DataColumn(dc.Caption, dc.DataType));
                foreach (DataRow dr in distintos.Rows)
                {
                    aux.ImportRow(dt.Select(columnName + " = '" + dr[0] + "'")[0]);
                }
                return aux;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [HttpPost]
        [Route("api/Admision/AnularAdmisionDetalle/{id}")]
        public IHttpActionResult AnularAdmisionDetalle(int id, [FromBody] List<WCO_ListarAdmisionServicioDetalle_Result> ObjDetalle)
        {
            HttpStatusCode statusCode = new HttpStatusCode();
            RequestAdmision lstAdm = new RequestAdmision();
            WCO_TraerXAdmisionServicio_Result ObjAdmision = new WCO_TraerXAdmisionServicio_Result();
            string observacion = "";
            try
            {
                int k = 0;
                if (id == 4)
                {
                    foreach (WCO_ListarAdmisionServicioDetalle_Result obj_Seleccionados in ObjDetalle)
                    {
                        ADBE_AdmisionServicio objBEAdmisionServicio = new ADBE_AdmisionServicio();
                        if (obj_Seleccionados.Estado != 6 || obj_Seleccionados.Estado != 3)
                        {
                            obj_Seleccionados.Estado = 4;
                            ObjAdmision.IdAdmision = obj_Seleccionados.IdAdmision;
                            objBEAdmisionServicio.PK.Admision.PK.IdAdmision = obj_Seleccionados.IdAdmision;
                            objBEAdmisionServicio.PK.UnidadNegocio.Pk.UneuNegocioId = 1;
                            objBEAdmisionServicio.UsuarioCreacion = obj_Seleccionados.UsuarioCreacion;
                            objBEAdmisionServicio.IpCreacion = obj_Seleccionados.IpCreacion;
                            objBEAdmisionServicio.UsuarioModificacion = obj_Seleccionados.UsuarioModificacion;
                            objBEAdmisionServicio.IpModificacion = obj_Seleccionados.IpModificacion;
                            objBEAdmisionServicio.PK.Componente.Pk.CodigoComponente = obj_Seleccionados.CodigoComponente;
                            objBEAdmisionServicio.Cantidad = obj_Seleccionados.Cantidad;
                            objBEAdmisionServicio.Valor = obj_Seleccionados.Valor;
                            objBEAdmisionServicio.Estado = obj_Seleccionados.Estado;
                            adm.ActualizarAdmisionDetalle(objBEAdmisionServicio);
                            k++;

                            ADBE_ReferenciaComponente obj_pReferenciaComponente = new ADBE_ReferenciaComponente();
                            obj_pReferenciaComponente.PK.Admision.PK.IdAdmision = obj_Seleccionados.IdAdmision;
                            obj_pReferenciaComponente.CodigoComponente = obj_Seleccionados.CodigoComponente;
                            obj_pReferenciaComponente.UsuarioModificacion = obj_Seleccionados.UsuarioModificacion;
                            obj_pReferenciaComponente.Estado = objBEAdmisionServicio.Estado;
                            adm.InactivarDetComponente(obj_pReferenciaComponente);
                        }
                        //statusCode. = observacion;
                    }

                    if (k == 0)
                    {
                        observacion += "Seleccione Examen(es) a quitar";
                        lstAdm.mensaje = observacion;
                    }
                    else
                    {
                        observacion += "Se anuló " + k + " Examen(es) satisfactoriamente";
                        lstAdm = adm.TraerXAdmisionServicio(ObjAdmision);
                        lstAdm.mensaje = observacion;
                    }
                }
                if (id == 5)
                {
                    foreach (WCO_ListarAdmisionServicioDetalle_Result obj_Seleccionados in ObjDetalle)
                    {
                        ObjAdmision.IdAdmision = obj_Seleccionados.IdAdmision;
                        ADBE_AdmisionServicio objBEAdmisionServicio = new ADBE_AdmisionServicio();

                        if (obj_Seleccionados.Estado == 1)
                        {
                            objBEAdmisionServicio.PK.Admision.PK.IdAdmision = obj_Seleccionados.IdAdmision;
                            objBEAdmisionServicio.PK.Componente.Pk.CodigoComponente = obj_Seleccionados.CodigoComponente;
                            adm.AdmisionServicioDetalleEliminar(objBEAdmisionServicio);
                            k++;
                        }
                    }
                    if (k == 0)
                    {
                        observacion += "Seleccione Examen(es) a quitar";
                        lstAdm.mensaje = observacion;
                    }
                    else
                    {
                        observacion += "Se quitó " + k + " Examen(es) satisfactoriamente";
                        lstAdm = adm.TraerXAdmisionServicio(ObjAdmision);
                        lstAdm.mensaje = observacion;
                    }
                }
                statusCode = HttpStatusCode.OK;
                return Content(statusCode, lstAdm);
            }
            catch (Exception ex)
            {
                //return Content(HttpStatusCode.Unauthorized, new Response() { Message = "Acceso denegado.", IsSucces = false });
                return Content(HttpStatusCode.InternalServerError, new ViewModalExite { success = false, mensaje = ex.Message, valor = -1 });
            }
            //return Ok(lst);
        }

        [Authorize]
        [HttpPost]
        [Route("api/Admision/MantenimientoUnificarAtencionPaciente/{id}")]
        public async Task<IHttpActionResult> MantenimientoUnificarAtencionPaciente(int id, [FromBody] List<WCO_UnificarAtencionPaciente_Result> ObjDetalle)
        {
            ViewModalExite objLogin = new ViewModalExite();
            HttpStatusCode statusCode = new HttpStatusCode();
            try
            {
                if (id == 1)
                {
                    string result = "";
                    foreach (WCO_UnificarAtencionPaciente_Result detalle in ObjDetalle)
                    {
                        result = adm.WCO_UnificarAtencionPaciente(detalle);
                        if (result != "Ok") { break; }
                    }

                    objLogin.success = result != "Ok" ? false : true;
                    objLogin.valor = result != "Ok" ? 0 : 1;
                    objLogin.mensaje = result;
                    statusCode = result != "Ok" ? HttpStatusCode.NotAcceptable : HttpStatusCode.OK;
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

        #endregion

        #region ClasificadorMovimiento

        [HttpPost]
        [Route("api/Admision/ListaClasificadorMovimiento")]
        public IEnumerable<WCO_ListarClasificadorMovimiento_Result> ListaClasificadorMovimiento(WCO_ListarClasificadorMovimiento_Result ObjDetalle)
        {
            List<WCO_ListarClasificadorMovimiento_Result> lst = new List<WCO_ListarClasificadorMovimiento_Result>();
            try
            {
                lst = cm.ListadoPaginado(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        [HttpPost]
        [Route("api/Admision/ListarClasificadorComponente")]
        public IEnumerable<WCO_ListarClasiComponente_Result> ListarClasificadorComponente(WCO_ListarClasiComponente_Result ObjDetalle)
        {
            List<WCO_ListarClasiComponente_Result> lst = new List<WCO_ListarClasiComponente_Result>();
            try
            {
                lst = serv.ListarClasificadorComponente(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        #endregion

        #region TipoOperacion

        [HttpPost]
        [Route("api/Admision/ListarTipoOperacion")]
        public IEnumerable<WCO_ListarTipoOperacion_Result> ListarTipoOperacion(WCO_ListarTipoOperacion_Result ObjDetalle)
        {
            List<WCO_ListarTipoOperacion_Result> lst = new List<WCO_ListarTipoOperacion_Result>();
            try
            {
                lst = tp.ListadoPaginado(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        [HttpPost]
        [Route("api/Admision/ListarTipoOperacionCliente")]
        public IEnumerable<WCO_ListarTipoOperacionCliente_Result> ListarTipoOperacionCliente(WCO_ListarTipoOperacion_Result ObjDetalle)
        {
            List<WCO_ListarTipoOperacionCliente_Result> lst = new List<WCO_ListarTipoOperacionCliente_Result>();
            try
            {
                lst = tp.ListarTipoOperacionCliente(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        [HttpPost]
        [Route("api/Admision/ListarTipoOperacionTipoPaciente")]
        public IEnumerable<WCO_ListarTipoOperacionClienteTipoPaciente_Result> ListarTipoOperacionTipoPaciente(WCO_ListarTipoOperacion_Result ObjDetalle)
        {
            List<WCO_ListarTipoOperacionClienteTipoPaciente_Result> lst = new List<WCO_ListarTipoOperacionClienteTipoPaciente_Result>();
            try
            {
                lst = tp.ListarTipoOperacionTipoPaciente(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        [HttpPost]
        [Route("api/Admision/ListarTipoOperacionClienteSede")]
        public IEnumerable<WCO_ListarTipoOperacionClienteSede_Result> ListarTipoOperacionClienteSede(WCO_ListarTipoOperacion_Result ObjDetalle)
        {
            List<WCO_ListarTipoOperacionClienteSede_Result> lst = new List<WCO_ListarTipoOperacionClienteSede_Result>();
            try
            {
                lst = tp.ListarTipoOperacionClienteSede(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }


        [HttpPost]
        [Route("api/Admision/ListaTipoOperacionxCodigo")]
        public IEnumerable<WCO_TraerTipoOperacionxCodigo_Result> ListaTipoOperacionxCodigo(WCO_TraerTipoOperacionxCodigo_Result ObjDetalle)
        {
            List<WCO_TraerTipoOperacionxCodigo_Result> lst = new List<WCO_TraerTipoOperacionxCodigo_Result>();
            try
            {
                lst = tp.TipoOperacionxCodigo(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        [HttpPost]
        [Route("api/Admision/TraerTipoOperacionxId")]
        public IHttpActionResult TraerTipoOperacionxId(WCO_ListarTipoOperacion_Result ObjDetalle)
        {
            WCO_TraerTipoOperacionxId_Result lst = new WCO_TraerTipoOperacionxId_Result();
            try
            {
                lst = tp.TraerTipoOperacionxId(ObjDetalle);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.Unauthorized, new Response() { Message = "Acceso denegado." + ex.Message, IsSucces = false });
            }
            return Ok(lst);
        }

        [Authorize]
        [HttpPost]
        [Route("api/Admision/MantenimientoTipoOperacion/{id}")]
        public async Task<IHttpActionResult> MantenimientoTipoOperacion(int id, [FromBody] WCO_ListarTipoOperacion_Result ObjDetalle)
        {
            ViewModalExite objLogin = new ViewModalExite();
            HttpStatusCode statusCode = new HttpStatusCode();
            try
            {
                int valor = 0;
                if (id > 0)
                {
                    if (id == 1)
                    {
                        valor = tp.Insertar(ObjDetalle);
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
                    }
                    if (id == 2)
                    {
                        valor = tp.Actualizar(ObjDetalle);
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
                    if (id == 3)
                    {
                        tp.Inactivar(ObjDetalle);
                        objLogin.success = true;
                        objLogin.valor = 1;
                        objLogin.mensaje = "Ok";
                        statusCode = HttpStatusCode.OK;
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

        #region Componente
        [HttpPost]
        [Route("api/Admision/ListadoComponenteMaestro")]
        public IEnumerable<WCO_ListarComponenteMaestro_Result> ListadoComponenteMaestro(WCO_ListarComponenteMaestro_Result ObjDetalle)
        {
            List<WCO_ListarComponenteMaestro_Result> lst = new List<WCO_ListarComponenteMaestro_Result>();
            try
            {
                lst = serv.ListadoPaginadoMaestro(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        [HttpPost]
        [Route("api/Admision/ListaComponente")]
        public IEnumerable<WCO_ListarComponente_Result> ListaComponente(WCO_ListarComponente_Result ObjDetalle)
        {
            List<WCO_ListarComponente_Result> lst = new List<WCO_ListarComponente_Result>();
            try
            {
                lst = serv.ListadoPaginado(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        [Authorize]
        [HttpPost]
        [Route("api/Admision/MantenimientoComponente/{id}")]
        public async Task<IHttpActionResult> MantenimientoComponente(int id, [FromBody] WCO_ListarComponenteMaestro_Result ObjDetalle)
        {
            ViewModalExite objLogin = new ViewModalExite();
            HttpStatusCode statusCode = new HttpStatusCode();

            try
            {
                string valor = "";
                if (id < 1)
                { return NotFound(); }

                if (id == 1)
                {
                    valor = serv.Insertar(ObjDetalle);
                    if (valor.Length > 0)
                    {
                        objLogin.success = true;
                        objLogin.valor = 1;
                        objLogin.mensaje = "Created";
                        ObjDetalle.CodigoComponente = valor;
                        objLogin.data = ObjDetalle;
                        statusCode = HttpStatusCode.Created;
                    }
                    else
                    {
                        objLogin.success = false;
                        objLogin.valor = 0;
                        objLogin.mensaje = "Los campos ingresados coinciden con un registro en nuestra base. Por favor ingrese un nuevo valor";
                        objLogin.data = "Error";
                        statusCode = HttpStatusCode.OK;
                    }
                }
                if (id == 2)
                {
                    valor = serv.Actualizar(ObjDetalle);
                    if (valor.Length > 0)
                    {
                        objLogin.success = true;
                        objLogin.valor = 1;
                        objLogin.mensaje = "Ok";
                        statusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        objLogin.success = false;
                        objLogin.valor = 0;
                        objLogin.mensaje = "Los campos ingresados coinciden con un registro en nuestra base. Por favor ingrese un nuevo valor";
                        statusCode = HttpStatusCode.OK;
                    }

                }
                else if (id == 3)
                {
                    serv.Inactivar(ObjDetalle);
                    objLogin.success = true;
                    objLogin.valor = 1;
                    objLogin.mensaje = "Ok";
                    statusCode = HttpStatusCode.OK;
                }
                else
                {
                    objLogin.success = false;
                    objLogin.valor = 0;
                    objLogin.mensaje = "Los campos ingresados coinciden con un registro en nuestra base.";

                }
                statusCode = HttpStatusCode.OK;
                return Content(statusCode, objLogin);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, new ViewModalExite() { success = false, mensaje = ex.Message, valor = -1 });
            }
        }
        #endregion

        #region ComponentePerfil
        [HttpPost]
        [Route("api/Admision/ListadoComponentePerfil")]
        public IEnumerable<WCO_ListarComponentePerfil_Result> ListadoComponentePerfil(WCO_ListarComponentePerfil_Result ObjDetalle)
        {
            List<WCO_ListarComponentePerfil_Result> lst = new List<WCO_ListarComponentePerfil_Result>();
            try
            {
                lst = serv.ListadoComponentePerfil(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        [Authorize]
        [HttpPost]
        [Route("api/Admision/MantenimientoComponentePerfil/{id}")]
        public async Task<IHttpActionResult> MantenimientoComponentePerfil(int id, [FromBody] WCO_ListarComponentePerfil_Result ObjDetalle)
        {
            ViewModalExite objLogin = new ViewModalExite();
            HttpStatusCode statusCode = new HttpStatusCode();

            try
            {
                int valor = 0;
                statusCode = HttpStatusCode.OK;
                if (id < 1 || id == null) { return NotFound(); }

                if (id == 1)
                {
                    valor = serv.InsertarComponentePerfil(ObjDetalle);
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
                    valor = serv.InactivarComponentePerfil(ObjDetalle);
                    objLogin.success = true;
                    objLogin.valor = 1;
                    objLogin.mensaje = "Ok";
                }
                else
                {
                    return BadRequest();
                }
                statusCode = HttpStatusCode.OK;
                return Content(statusCode, objLogin);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, new ViewModalExite() { success = false, mensaje = ex.Message, valor = -1 });
            }
        }
        #endregion

        #region ComponenteMuestra
        [HttpPost]
        [Route("api/Admision/ListadoComponenteMuestra")]
        public IEnumerable<WCO_ListarComponenteMuestra_Result> ListadoComponenteMuestra(WCO_ListarComponenteMuestra_Result ObjDetalle)
        {
            List<WCO_ListarComponenteMuestra_Result> lst = new List<WCO_ListarComponenteMuestra_Result>();
            try
            {
                lst = serv.ListarComponenteMuestra(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        [Authorize]
        [HttpPost]
        [Route("api/Admision/MantenimientoComponenteMuestra/{id}")]
        public async Task<IHttpActionResult> MantenimientoComponenteMuestra(int id, [FromBody] WCO_ListarComponenteMuestra_Result ObjDetalle)
        {
            ViewModalExite objLogin = new ViewModalExite();
            HttpStatusCode statusCode = new HttpStatusCode();

            try
            {
                string valor = "";
                statusCode = HttpStatusCode.OK;
                if (id < 1 || id == null)
                {
                    return NotFound();
                }

                if (id == 1)
                {
                    valor = serv.InsertarComponenteMuestra(ObjDetalle);
                    if (valor.Length < 0)
                    {
                        objLogin.success = false;
                        objLogin.valor = 0;
                        objLogin.mensaje = "Los campos ingresados coinciden con un registro en nuestra base. Por favor ingrese un nuevo valor";
                        objLogin.data = null;
                        statusCode = HttpStatusCode.OK;
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
                    serv.EliminarComponenteMuestra(ObjDetalle);
                    objLogin.success = true;
                    objLogin.valor = 1;
                    objLogin.mensaje = "Ok";

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

        #region ComponenteHomologacion
        [HttpPost]
        [Route("api/Admision/ListadoComponenteHomologacion")]
        public IEnumerable<WCO_ListarHomologacion_Result> ListadoComponenteHomologacion(WCO_ListarHomologacion_Result ObjDetalle)
        {
            List<WCO_ListarHomologacion_Result> lst = new List<WCO_ListarHomologacion_Result>();
            try
            {
                lst = serv.ListadoComponenteHomologacion(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }


        [Authorize]
        [HttpPost]
        [Route("api/Admision/MantenimientoComponenteHomologacion/{id}")]
        public async Task<IHttpActionResult> MantenimientoComponenteHomologacion(int id, [FromBody] WCO_ListarHomologacion_Result ObjDetalle)
        {
            if (id < 1) return NotFound();
            if (ObjDetalle == null) return BadRequest("El cuerpo de la solicitud no puede ser nulo.");

            var objLogin = new ViewModalExite();
            HttpStatusCode statusCode = HttpStatusCode.OK;

            try
            {
                switch (id)
                {
                    case 1:
                        int valor = serv.InsertarHomologacion(ObjDetalle);
                        if (valor < 0)
                        {
                            objLogin.success = false;
                            objLogin.valor = 0;
                            objLogin.mensaje = "Los campos ingresados coinciden con un registro en nuestra base. Por favor ingrese un nuevo valor";
                            objLogin.data = null;
                            statusCode = HttpStatusCode.BadRequest;
                        }
                        else
                        {
                            objLogin.success = true;
                            objLogin.valor = 1;
                            objLogin.mensaje = "Created";
                            objLogin.data = ObjDetalle;
                            statusCode = HttpStatusCode.Created;
                        }
                        break;

                    case 3:
                        serv.InactivarHomologacion(ObjDetalle);
                        objLogin.success = true;
                        objLogin.valor = 1;
                        objLogin.mensaje = "Ok";
                        statusCode = HttpStatusCode.OK;
                        break;

                    default:
                        return BadRequest("El ID proporcionado no es válido para esta operación.");
                }

                return Content(statusCode, objLogin);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, new ViewModalExite() { success = false, mensaje = ex.Message, valor = -1 });
            }
        }

        #endregion


        #region ModeloServicio

        [HttpPost]
        [Route("api/Admision/ListarModeloServicio")]
        public IEnumerable<WCO_ListarModeloServicio_Result> ListarModeloServicio(WCO_ListarModeloServicio_Result ObjDetalle)
        {
            List<WCO_ListarModeloServicio_Result> lst = new List<WCO_ListarModeloServicio_Result>();
            try
            {
                lst = serv.ListarModeloServicio(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        #endregion

        #region Sede 

        [HttpPost]
        [Route("api/Admision/ListaSede")]
        public IEnumerable<WCO_ListarSede_Result> ListaSede(WCO_ListarSede_Result ObjDetalle)
        {
            List<WCO_ListarSede_Result> lst = new List<WCO_ListarSede_Result>();
            try
            {
                lst = sd.ListadoPaginado(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        [HttpPost]
        [Route("api/Admision/ListarSedeCliente")]
        public IEnumerable<WCO_ListarSedeCliente_Result> ListarSedeCliente(WCO_ListarSedeCliente_Result ObjDetalle)
        {
            List<WCO_ListarSedeCliente_Result> lst = new List<WCO_ListarSedeCliente_Result>();
            try
            {
                lst = sd.ListadoSedeCliente(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        #endregion


    }
}
