
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

namespace WebApiServices.Controllers
{
    public class SeguridadController : ApiController
    {
        Metodos m = new Metodos();

        ADDAT_CorreoNegocio Nego = new ADDAT_CorreoNegocio();
        ADDAT_PersonaMast per = new ADDAT_PersonaMast();

        #region usuario
        [HttpPost]
        [Route("api/Seguridad/EnviarCorreo/{id}")]
        public IHttpActionResult EnviarCorreo(int id, [FromBody] RequestCorreo ObjDetalle)
        {
            ViewModalExite objLogin = new ViewModalExite();
            try
            {
                if (id != 1)
                {
                    objLogin.success = false;
                    objLogin.mensaje = "Acceso denegado";
                    objLogin.valor = 0;
                    return Ok(objLogin);
                    //  return Content(HttpStatusCode.Unauthorized, new Response() { Message = "Acceso denegado.", IsSucces = false });
                }
                List<WCO_ListarCorreoNegocioDetalle_Result> lst = new List<WCO_ListarCorreoNegocioDetalle_Result>();
                WCO_ListarCorreoNegocio_Result Obj = new WCO_ListarCorreoNegocio_Result();
                Obj.UneuNegocioId = ObjDetalle.UneuNegocioId;
                Obj.Estado = -1;
                Obj.IdCorreo = 1;
                lst = Nego.ListadoPaginadoDetalle(Obj);
                string Nombre = "";
                string Correo = "";
                string Archivo = "";
                string asunto = "";
                string mensaje = "";
                string Acceso = "";
                if (lst.Count < 1)
                {
                    // return Content(HttpStatusCode.Unauthorized, new Response() { Message = "No existe Informacion.", IsSucces = false });
                    objLogin.success = false;
                    objLogin.mensaje = "No existe Informacion";
                    objLogin.valor = 0;
                    return Ok(objLogin);
                }
                mensaje = "";
                foreach (WCO_ListarCorreoNegocioDetalle_Result obj_Negocio in lst)
                {
                    Acceso = obj_Negocio.Passwor.ToString();
                    Nombre = obj_Negocio.Mensaje.ToString();
                    Correo = obj_Negocio.Correo.ToString();
                    Archivo = obj_Negocio.RutaAdjunto;
                    asunto = obj_Negocio.Asunto.ToString();

                    if (!string.IsNullOrEmpty(obj_Negocio.Variable.ToString()))
                    {
                        if (obj_Negocio.Desvariable.ToString() == "Paciente")
                        {
                            mensaje += "<div><p>" + obj_Negocio.Mensaje1.ToString() + ObjDetalle.NombreCompleto.ToUpper().Trim() + obj_Negocio.Mensaje2.ToString() + "</p></div></br>";
                        }
                        if (obj_Negocio.Desvariable.ToString() == "Usuario")
                        {
                            mensaje += "<div><p>" + obj_Negocio.Mensaje1.ToString() + ObjDetalle.PerNumeroDocumento.ToUpper().Trim() + "</p></div></br>";
                        }
                        if (obj_Negocio.Desvariable.ToString() == "Pasword")
                        {
                            mensaje += "<div><p>" + obj_Negocio.Mensaje1.ToString() + ObjDetalle.Password + "</p></div></br>";
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(obj_Negocio.Mensaje1.ToString()))
                        {
                            mensaje += "<div><p>" + obj_Negocio.Mensaje1.ToString() + "</p></div></br>";
                        }
                        else
                        {
                            mensaje += "<div><p>" + obj_Negocio.Mensaje2.ToString() + "</p></div></br>";
                        }
                    }
                }


                // ESTOS DATOS SON TEMPORALES CUANDO SE PASE A PRODUCCION SE TIENE Q CAMBIAR POR EL SERVIDOR
                MailMessage obj_Mail = new MailMessage();
                obj_Mail.From = new MailAddress(ObjDetalle.str_pCC, ObjDetalle.str_pBCC);
                if (ObjDetalle.str_pTo != null)
                {
                    obj_Mail.To.Add(new MailAddress(ObjDetalle.str_pTo));
                }
                obj_Mail.Subject = asunto;
                obj_Mail.IsBodyHtml = true;
                obj_Mail.Body = mensaje;
                long lng_AdjuntosTotal = 0;
                FileStream obj_Stream = new FileStream(Archivo, FileMode.Open, FileAccess.Read);
                lng_AdjuntosTotal += obj_Stream.Length;
                Attachment obj_Adjunto = new Attachment(obj_Stream, "Instructivo.pdf", MediaTypeNames.Application.Octet);
                obj_Mail.Attachments.Add(obj_Adjunto);

                // ESTOS DATOS SON TEMPORALES CUANDO SE PASE A PRODUCCION SE TIENE Q CAMBIAR POR EL SERVIDOR
                SmtpClient obj_Smtp = new SmtpClient();
                // tcpclient.Connect("imap.gmail.com", 993);

                obj_Smtp.Host = "smtp.gmail.com";
                obj_Smtp.Port = 587;
                obj_Smtp.Credentials = new System.Net.NetworkCredential(Correo, Acceso.Trim());
                //obj_Smtp.Host = "urano.royalsystems.net";
                //obj_Smtp.Port = 25;                
                //obj_Smtp.Credentials = new System.Net.NetworkCredential("mateoj@royalsystems.net", "2013mateoj08");                    

                //Con gmail y algunos servidores de correos tienen la opcion EnableSsl q sirve para q no vaya a los
                //Correos no deseados
                obj_Smtp.EnableSsl = true; // runtime encrypt the SMTP communications using SSL
                obj_Smtp.Send(obj_Mail);
                ObjDetalle.success = true;
                ObjDetalle.tokem = "Envio de Correo";

                objLogin.success = true;
                objLogin.data = ObjDetalle;
                objLogin.tokem = TokenGenerator.GenerateTokenJwt(Correo);
                objLogin.mensaje = "Envio de Correo";
            }
            catch (Exception ex)
            {
                objLogin.success = false;
                objLogin.mensaje = "Exception=" + ex.InnerException + "| Message=" + ex.Message;
                objLogin.valor = -1;
            }
            return Ok(objLogin);
        }

        [HttpPost]
        [Route("api/Seguridad/RecuperarClave/{id}")]
        public IHttpActionResult RecuperarClave(int id, [FromBody] WCO_AccesoUsuario_Result ObjDetalle)
        {
            ViewModalExite objLogin = new ViewModalExite();
            try
            {
                WCO_TraerIDPersonaxCorreo_Result Obj_vBEPerxcorreo = new WCO_TraerIDPersonaxCorreo_Result();
                ADBE_PersonaMast Obj_vBEPerxcorreoestra = new ADBE_PersonaMast();
                Obj_vBEPerxcorreo.CorreoElectronico = ObjDetalle.correo_empresa;
                var PersonaxCorreo = per.TraerIDPersonaxCorreo(Obj_vBEPerxcorreo);

                if (PersonaxCorreo == null)
                {
                    objLogin.success = false;
                    objLogin.mensaje = "Correo no Existe";
                    objLogin.valor = -1;
                    return Ok(objLogin);
                }

                List<WCO_ListarCorreoNegocio_Result> lst = new List<WCO_ListarCorreoNegocio_Result>();
                WCO_ListarCorreoNegocio_Result Obj = new WCO_ListarCorreoNegocio_Result();
                Obj.UneuNegocioId = 1;
                Obj.Estado = -1;
                Obj.IdCorreo = 1;
                lst = Nego.ListadoPaginado(Obj);
                string Nombre = "";
                string Correo = "";
                string Acceso = "";
                foreach (WCO_ListarCorreoNegocio_Result obj_Negocio in lst)
                {
                    Acceso = obj_Negocio.Passwor.ToString();
                    Nombre = obj_Negocio.Mensaje.ToString();
                    Correo = obj_Negocio.Correo.ToString();
                }
                string asunto = "Ingreso al Sistema Web";
                string mensaje = "<div><h2>Su Nuevo Acceso al Sistema Web </h2>";
                mensaje += "<p><strong>Su Usuario es : </strong>" + Obj_vBEPerxcorreoestra.PerNumeroDocumento + "</p>";
                mensaje += "<p><strong>Su Password es :</strong>" + Obj_vBEPerxcorreoestra.PerNumeroDocumento + "</p>";
                MailMessage obj_Mail = new MailMessage();
                obj_Mail.From = new MailAddress("info@precisa.com.pe", "Administrador del Sistema");

                if (ObjDetalle.correo_empresa != null)
                {
                    obj_Mail.To.Add(new MailAddress(ObjDetalle.correo_empresa));
                }

                obj_Mail.Subject = asunto;
                obj_Mail.IsBodyHtml = true;
                obj_Mail.Body = mensaje;

                SmtpClient obj_Smtp = new SmtpClient();
                obj_Smtp.Host = "smtp.gmail.com";
                obj_Smtp.Port = 587;
                obj_Smtp.Credentials = new System.Net.NetworkCredential(Correo, Acceso); //"precisainfo2012"
                obj_Smtp.Send(obj_Mail);

                objLogin.success = false;
                objLogin.mensaje = "Sr(a).:" + Obj_vBEPerxcorreoestra.NombreCompleto + " Los datos se le enviaran a su correo ";
                objLogin.valor = -1;

            }
            catch (Exception ex)
            {
                objLogin.success = false;
                objLogin.mensaje = "Exception=" + ex.InnerException + "| Message=" + ex.Message;
                objLogin.valor = -1;
            }
            return Ok(objLogin);
        }

        [HttpPost]
        [Route("api/Seguridad/ObtenerIP")]
        public IHttpActionResult ObtenerIP()
        {
            string IP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(IP) && !IP.ToLower().Equals("unknown"))
            {
                string[] IPs = IP.Split(',');
                IP = IPs[0].Trim();
            }
            else
            {
                IP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            return Ok(IP);
        }

        [HttpPost]
        [Route("api/Seguridad/RestValidacionConsentimiento")]
        public IEnumerable<Sp_PDP_validacion_Result> RestValidacionConsentimiento(string Documento)
        {
            List<Sp_PDP_validacion_Result> lst = new List<Sp_PDP_validacion_Result>();
            try
            {
                Sp_PDP_validacion_Result objNue = new Sp_PDP_validacion_Result();
                objNue.msn = Documento;
                lst = m.RestValidacionConsentimiento(objNue);
            }
            catch
            {

            }
            return lst;
        }

        [HttpPost]
        [Route("api/Seguridad/ListaLogin")]
        public IHttpActionResult ListaLogin(WCO_AccesoUsuario_Result ObjDetalle)
        {
            List<WCO_AccesoUsuario_Result> lst = new List<WCO_AccesoUsuario_Result>();
            ViewModalExite objLogin = new ViewModalExite();
            UT_Kerberos Ker = new UT_Kerberos();
            try
            {
                ObjDetalle.Clave = ObjDetalle.Clave.Replace("'", "");
                ObjDetalle.Usuario = ObjDetalle.Usuario.Replace("'", "");
                WCO_TRAERUSUARIOXCODIGO_Result ObjDet = new WCO_TRAERUSUARIOXCODIGO_Result();
                ObjDet.USUARIO = ObjDetalle.Usuario;
                var USUARIO = m.TrareUsuarioxCodigo(ObjDet);
                if (USUARIO.Count > 0)
                {
                    ObjDetalle.Clave = Ker.Encriptar(ObjDetalle.Clave);
                    var grouUser = USUARIO.GroupBy(u => u.Psw);
                    foreach (var xuUser in grouUser)
                    {
                        if (xuUser.Key == ObjDetalle.Clave)
                        {
                            foreach (var xuSede in USUARIO)
                            {
                                if (xuSede.IdSede == ObjDetalle.IdSede && xuSede.ESTADO == "A")
                                {
                                    lst = m.ListaAccesoUsuario(ObjDetalle);
                                    var user = lst.First();
                                    if (lst.Count > 0)
                                    {
                                        objLogin.success = true;
                                        objLogin.valor = 1;
                                        objLogin.data = lst;
                                        objLogin.tokem = TokenGenerator.GenerateTokenJwt(user.Usuario);
                                        objLogin.mensaje = "Acceso correcto";
                                        break;
                                    }
                                }
                                else
                                {
                                    objLogin.valor = -1;
                                    objLogin.success = false;
                                    objLogin.mensaje = "No tiene Acceso a esta Sede";
                                }
                            }
                        }
                        else
                        {
                            objLogin.valor = -2;
                            objLogin.success = false;
                            objLogin.mensaje = "La Clave esta incorrecta";
                            // return Content(HttpStatusCode.NoContent, new ViewModalExite() { Message = "La Clave esta incorrecta", IsSucces = false });
                        }
                    }
                }
                else
                {
                    objLogin.success = false;
                    objLogin.valor = -3;
                    objLogin.mensaje = "No Exite el Usuario";
                    //  return Content(HttpStatusCode.NoContent, new ViewModalExite() { Message = "No Exite el Usuario", IsSucces = false });
                }
            }
            catch (Exception ex)
            {
                objLogin.success = false;
                objLogin.valor = -4;
                objLogin.mensaje = "Exception=" + ex.InnerException + "| Message=" + ex.Message;
                objLogin.valor = -1;
            }
            return Ok(objLogin);
        }


        [HttpPost]
        [Route("api/Seguridad/CambioClave")]
        public IHttpActionResult CambioClave(WCO_AccesoUsuario_Result ObjDetalle)
        {
            ViewModalExite objLogin = new ViewModalExite();
            UT_Kerberos Ker = new UT_Kerberos();
            try
            {
                ObjDetalle.Clave = ObjDetalle.Clave.Replace("'", "");
                ObjDetalle.Usuario = ObjDetalle.Usuario.Replace("'", "");
                ObjDetalle.ACCESO = ObjDetalle.ACCESO.Replace("'", "");
                if (ObjDetalle.Clave.Trim() != ObjDetalle.ACCESO.Trim())
                {
                    WCO_TRAERUSUARIOXCODIGO_Result ObjDet = new WCO_TRAERUSUARIOXCODIGO_Result();
                    ObjDet.USUARIO = ObjDetalle.Usuario;
                    var USUARIO = m.TrareUsuarioxCodigo(ObjDet);
                    if (USUARIO.Count > 0)
                    {
                        ObjDetalle.Clave = Ker.Encriptar(ObjDetalle.Clave);
                        var grouUser = USUARIO.GroupBy(u => u.Clave);
                        // var user = grouUser.Select(u => u.Key == ObjDetalle.Clave);
                        foreach (var xuUser in grouUser)
                        {
                            if (xuUser.Key == ObjDetalle.Clave)
                            {
                                int valor = 0;
                                foreach (WCO_TRAERUSUARIOXCODIGO_Result xuEnty in USUARIO)
                                {
                                    valor++;
                                    if (valor == 1)
                                    {
                                        ObjDetalle.Clave = Ker.Encriptar(ObjDetalle.ACCESO.Trim());
                                        ObjDetalle.ClasificadorMovimiento = xuEnty.ClasificadorMovimiento;
                                        ObjDetalle.Estado = xuEnty.ESTADO;
                                        ObjDetalle.ExpirarPasswordFlag = xuEnty.EXPIRARPASSWORDFLAG;
                                        ObjDetalle.UsuarioPerfil = xuEnty.PERFIL;
                                        ObjDetalle.Persona = xuEnty.PERSONA;
                                        ObjDetalle.TipoUsuario = xuEnty.TIPOUSUARIO;
                                        ObjDetalle.UltimaFechaModif = xuEnty.ULTIMAFECHAMODIF;
                                        ObjDetalle.UltimoUsuario = ObjDetalle.Usuario;
                                        break;
                                    }
                                }
                                m.Actualizarusuario(ObjDetalle);
                                objLogin.valor = 1;
                                objLogin.mensaje = "Se Cambiado la contraseña Correctamente";
                            }
                            else
                            {
                                objLogin.valor = 0;
                                objLogin.mensaje = "La Clave esta incorrecta";
                            }
                        }
                    }
                    else
                    {
                        objLogin.valor = 0;
                        objLogin.mensaje = "No Exite el Usuario";
                    }
                }
                else
                {
                    objLogin.valor = 0;
                    objLogin.mensaje = "La Clave nueva no debe ser igual a la anterior";
                }

            }
            catch (Exception ex)
            {
                objLogin.success = false;
                objLogin.mensaje = "Exception=" + ex.InnerException + "| Message=" + ex.Message;
                objLogin.valor = -1;
            }
            return Ok(objLogin);
        }

        [HttpPost]
        [Route("api/Seguridad/ListaUsuario")]
        public IEnumerable<WCO_ListarUsuario_Result> ListaUsuario(WCO_ListarUsuario_Result ObjDetalle)
        {
            List<WCO_ListarUsuario_Result> lst = new List<WCO_ListarUsuario_Result>();
            try
            {
                ADDAT_Usuario Usuario = new ADDAT_Usuario();
                lst = Usuario.ListarUsuario(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }


        [Authorize]
        [HttpPost]
        [Route("api/Seguridad/MantenimientoUsuario/{id}")]
        public async Task<IHttpActionResult> MantenimientoUsuario(int id, [FromBody] WCO_ListarUsuario_Result ObjDetalle)
        {
            ViewModalExite objLogin = new ViewModalExite();
            HttpStatusCode statusCode = new HttpStatusCode();
            ADDAT_Usuario Usuario = new ADDAT_Usuario();
            UT_Kerberos Kerberos = new UT_Kerberos();

            try
            {
                string valor = "0";
                ObjDetalle.Clave = id != 4 ? Kerberos.Encriptar(ObjDetalle.Clave) : ObjDetalle.Clave; // No se encripta cuando el id es 4: asignar perfil
                if (ObjDetalle.Usuario != null)
                {
                    if (!string.IsNullOrEmpty(ObjDetalle.Usuario.ToString()))
                    {
                        ObjDetalle.Usuario = ObjDetalle.Usuario.ToUpper();
                    }
                }
                if (ObjDetalle.NombreCompleto != null)
                {
                    if (!string.IsNullOrEmpty(ObjDetalle.NombreCompleto.ToString()))
                    {
                        ObjDetalle.NombreCompleto = ObjDetalle.NombreCompleto.ToUpper();
                    }
                }
                if (ObjDetalle.correo_empresa != null)
                {
                    if (!string.IsNullOrEmpty(ObjDetalle.correo_empresa.ToString()))
                    {
                        ObjDetalle.correo_empresa = ObjDetalle.correo_empresa.ToUpper();
                    }
                }
                switch (id)
                {
                    case 1:
                        valor = Usuario.Insertar(ObjDetalle);
                        if (valor == "0")
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
                        break;
                    case 2:

                        valor = Usuario.Actualizar(ObjDetalle);
                        if (valor == "0")
                        {
                            objLogin.success = false;
                            objLogin.valor = 0;
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
                        valor = Usuario.Inactivar(ObjDetalle);
                        if (valor == "0")
                        {
                            objLogin.success = false;
                            objLogin.valor = 0;
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
                    case 4:

                        valor = Usuario.Actualizar(ObjDetalle);
                        if (valor == "0")
                        {
                            objLogin.success = false;
                            objLogin.valor = 0;
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
                }
                return Content(statusCode, objLogin);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, new ViewModalExite() { success = false, mensaje = ex.Message, valor = -1 });
            }
        }

        [HttpPost]
        [Route("api/Seguridad/ListaUsuarioSede")]
        public IEnumerable<WCO_ListarUsuarioSede_Result> ListaUsuarioSede(WCO_ListarUsuarioSede_Result ObjDetalle)
        {
            List<WCO_ListarUsuarioSede_Result> lst = new List<WCO_ListarUsuarioSede_Result>();
            try
            {
                ADDAT_Usuario Usuario = new ADDAT_Usuario();
                lst = Usuario.ListaUsuarioSede(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }

        [Authorize]
        [HttpPost]
        [Route("api/Seguridad/MantenimientoUsuarioSede/{id}")]
        public async Task<IHttpActionResult> MantenimientoUsuarioSede(int id, [FromBody] ViewModalExite ObjDetalle)
        {
            ViewModalExite objLogin = new ViewModalExite();
            HttpStatusCode statusCode = new HttpStatusCode();
            ADDAT_Usuario Usuario = new ADDAT_Usuario();
            try
            {
                string valor = "0";
                var ObjCabecera = (List<WCO_ListarUsuarioSede_Result>)Newtonsoft.Json.JsonConvert.DeserializeObject(ObjDetalle.data.ToString(), typeof(List<WCO_ListarUsuarioSede_Result>));

                switch (id)
                {
                    case 1:
                        foreach (WCO_ListarUsuarioSede_Result Resdet in ObjCabecera)
                        {
                            valor = Usuario.EliminarUsuarioSedeMasivo(Resdet);
                            break;
                        }

                        foreach (WCO_ListarUsuarioSede_Result Resdet in ObjCabecera)
                        {
                            valor = Usuario.InsertarUsuarioSede(Resdet);
                        }
                        if (valor == "0")
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

        #region Portal

        [Authorize]
        [HttpPost]
        [Route("api/Seguridad/ListarMenu")]
        public IEnumerable<WCO_PerfilPaginas_Result> ListarMenu(WCO_PerfilPaginas_Result ObjDetalle)
        {
            List<WCO_PerfilPaginas_Result> lst = new List<WCO_PerfilPaginas_Result>();

            try
            {
                lst = m.ListaPerfilPaginas(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }


        [HttpPost]
        [Route("api/Seguridad/ListaPortal")]
        public IEnumerable<WCO_ListarPortal_Result> ListaPortal(WCO_ListarPortal_Result ObjDetalle)
        {
            List<WCO_ListarPortal_Result> lst = new List<WCO_ListarPortal_Result>();
            try
            {
                lst = m.ListaPortal(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }
        #endregion


        #region Perfiles

        [HttpPost]
        [Route("api/Seguridad/ListarComboPerfil")]
        public IEnumerable<WCO_ListarComboPerfil_Result> ListarComboPerfil(WCO_ListarComboPerfil_Result ObjDetalle)
        {
            List<WCO_ListarComboPerfil_Result> lst = new List<WCO_ListarComboPerfil_Result>();
            try
            {
                ADDAT_Usuario Usuario = new ADDAT_Usuario();
                lst = Usuario.ListarPerfil(ObjDetalle);
            }
            catch
            {

            }
            return lst;
        }


        [HttpPost]
        [Route("api/Seguridad/ListarPerfil")]
        public IEnumerable<Perfil> ListarPerfil(WCO_LISTARPERFILPAG_Result ObjDetalle)
        {
            List<Perfil> lstGeneral = new List<Perfil>();
            List<WCO_LISTARPERFILPAG_Result> lst = new List<WCO_LISTARPERFILPAG_Result>();
            List<WCO_ListarMenuOpcionesPerfil_Result> lstMenu = new List<WCO_ListarMenuOpcionesPerfil_Result>();
            List<WCO_ListarMenuOpcionesPerfil_Result> lstGrupo = new List<WCO_ListarMenuOpcionesPerfil_Result>();
            List<WCO_ListarMenuOpcionesPerfil_Result> lstPagina = new List<WCO_ListarMenuOpcionesPerfil_Result>();
            List<WCO_ListarMenuOpcionesPerfil_Result> lstOpciones = new List<WCO_ListarMenuOpcionesPerfil_Result>();
            ADDAT_Perfil med = new ADDAT_Perfil();
            try
            {
                lst = med.ListarPerfil(ObjDetalle);
                foreach (WCO_LISTARPERFILPAG_Result objlis in lst)
                {
                    List<file> Principal_list = new List<file>();
                    Perfil EntidadGEneral = new Perfil();
                    EntidadGEneral.perfil = objlis.Perfil;
                    EntidadGEneral.tipousuario = objlis.tipousuario;
                    EntidadGEneral.desestado = objlis.VaEstado;
                    EntidadGEneral.ultimousuario = objlis.UltimoUsuario;
                    EntidadGEneral.UltimaFechaModif = objlis.UltimaFechaModif;
                    EntidadGEneral.UsuarioCreacion = objlis.UsuarioCreacion;
                    EntidadGEneral.FechaCreacion = objlis.FechaCreacion;
                    EntidadGEneral.estado = objlis.estado;
                    ObjDetalle.Perfil = objlis.Perfil;

                    lstMenu = m.WCO_ListarMenuOpcionesPerfil(ObjDetalle);
                    lstGrupo = lstMenu.Where(u => u.CONCEPTOPADRE == "W1").ToList();
                    lstPagina = lstMenu.Where(u => u.URL != null).ToList();
                    //lstOpciones = lstMenu.Where(u => u.CONCEPTOPADRE.Length>8).ToList();

                    try
                    {
                        List<file> file_list = new List<file>();
                        file Xarchivo = new file();
                        Xarchivo.expandedIcon = "pi pi-folder-open";
                        Xarchivo.collapsedIcon = "pi pi-folder";
                        Xarchivo.label = "Comercial Web";
                        Xarchivo.data = "W1";
                        Xarchivo.key = "W1";

                        foreach (WCO_ListarMenuOpcionesPerfil_Result ITEGrupo in lstGrupo)
                        {
                            file XarchGru = new file();
                            XarchGru.expandedIcon = "pi pi-folder-open";
                            XarchGru.collapsedIcon = "pi pi-folder";
                            XarchGru.label = ITEGrupo.DESCRIPCION;
                            XarchGru.data = ITEGrupo.CONCEPTO;
                            XarchGru.key = ITEGrupo.CONCEPTO;
                            List<file> Archivo_list = new List<file>();
                            foreach (WCO_ListarMenuOpcionesPerfil_Result ITEPaginas in lstPagina)
                            {
                                if (ITEGrupo.CONCEPTO == ITEPaginas.CONCEPTOPADRE)
                                {
                                    file Xfile = new file();
                                    Xfile.icon = "pi pi-image";
                                    Xfile.label = ITEPaginas.DESCRIPCION;
                                    Xfile.data = ITEPaginas.CONCEPTO;
                                    Xfile.key = ITEPaginas.CONCEPTO;
                                    //XarchGru.children = Xfile;
                                    Archivo_list.Add(Xfile);
                                }
                            }
                            XarchGru.children = Archivo_list;
                            file_list.Add(XarchGru);
                        }
                        Xarchivo.children = file_list;
                        Principal_list.Add(Xarchivo);
                        EntidadGEneral.ListaPaginas = Principal_list;
                    }
                    catch
                    {

                    }
                    lstGeneral.Add(EntidadGEneral);
                }
            }
            catch
            {

            }
            return lstGeneral;
        }


        //[Authorize]
        [HttpPost]
        [Route("api/Seguridad/MantenimientoPerfil/{id}")]
        public async Task<IHttpActionResult> MantenimientoPerfil(int id, [FromBody] Perfil ObjDetalle)
        {
            ViewModalExite objLogin = new ViewModalExite();
            HttpStatusCode statusCode = new HttpStatusCode();
            ADDAT_Perfil med = new ADDAT_Perfil();
            try
            {
                string valor = "0";
                objLogin.success = false;
                objLogin.valor = 0;
                objLogin.data = null;
                statusCode = HttpStatusCode.OK;

                switch (id)
                {
                    case 1:
                        valor = med.Insertar(ObjDetalle);
                        if (valor == "0")
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
                        break;
                    case 2:
                        valor = med.Actualizar(ObjDetalle);
                        if (valor == "0")
                        {
                            objLogin.success = false;
                            objLogin.valor = 0;
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
                        valor = med.Inactivar(ObjDetalle);
                        if (valor == "0")
                        {
                            objLogin.success = false;
                            objLogin.valor = 0;
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
                }
                return Content(statusCode, objLogin);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, new ViewModalExite() { success = false, mensaje = ex.Message, valor = -1 });
            }
        }

        #endregion


        #region Files
        [HttpPost]
        [Route("api/Seguridad/ListarDirectorio")]
        public IHttpActionResult ListarDirectorio(ViewModel obj_pRuta)
        {
            ViewModalExite objLogin = new ViewModalExite();
            List<file> file_list = new List<file>();
            try
            {
                SearchDirectory(obj_pRuta.Nombre, file_list);
                //SearchDirectory(@"C:\ARCHIVO\SERVER\", file_list);
                objLogin.success = true;
                objLogin.mensaje = "El registro";
                objLogin.valor = file_list.Count;
                objLogin.data = file_list;
            }
            catch
            {
                objLogin.success = false;
                objLogin.mensaje = "El registro no contiene información";
                objLogin.valor = 0;

            }
            return Ok(objLogin);
        }

        private void SearchDirectory(string path, List<file> file_list)
        {
            DirectoryInfo dir_info = new DirectoryInfo(path);

            //try
            //{
            //    file Xarchivo = new file();
            //    Xarchivo.expandedIcon = "pi pi-folder-open";
            //    Xarchivo.collapsedIcon = "pi pi-folder";
            //    Xarchivo.label = dir_info.Name;
            //    Xarchivo.data = dir_info.FullName;
            //    List<file> xfile_list = new List<file>();
            //    foreach (FileInfo file_info in dir_info.GetFiles())
            //    {
            //        file Xfile = new file();
            //        Xfile.icon = "pi pi-image";
            //        Xfile.label = file_info.Name;
            //        Xfile.data = file_info.DirectoryName;
            //        xfile_list.Add(Xfile);
            //    }
            //    Xarchivo.children = xfile_list;
            //    file_list.Add(Xarchivo);
            //}
            //catch
            //{
            //}

            //try
            //{
            //    foreach (DirectoryInfo subdir_info in dir_info.GetDirectories())
            //    {
            //        SearchDirectory(subdir_info.FullName, file_list);
            //    }
            //}
            //catch
            //{
            //}
            List<file> xfile_list = new List<file>();
            try
            {
                file Xarchivo = new file();
                Xarchivo.expandedIcon = "pi pi-folder-open";
                Xarchivo.collapsedIcon = "pi pi-folder";
                Xarchivo.label = dir_info.Name;
                Xarchivo.data = dir_info.FullName;
                foreach (FileInfo file_info in dir_info.GetFiles())
                {
                    file Xfile = new file();
                    Xfile.icon = "pi pi-image";
                    Xfile.label = file_info.Name;
                    Xfile.data = file_info.DirectoryName;
                    xfile_list.Add(Xfile);
                }
                Xarchivo.children = xfile_list;
                file_list.Add(Xarchivo);

            }
            catch
            {

            }

            try
            {
                foreach (DirectoryInfo subdir_info in dir_info.GetDirectories())
                {
                    SearchDirectory(subdir_info.FullName, xfile_list);
                }
            }
            catch
            {
            }

        }


        private void OrdernarDirectory(string path, List<string> file_list)
        {
            DirectoryInfo dir_info = new DirectoryInfo(path);
            try
            {
                foreach (DirectoryInfo subdir_info in dir_info.GetDirectories())
                {
                    OrdernarDirectory(subdir_info.FullName, file_list);
                }
            }
            catch
            {
            }
            try
            {
                foreach (FileInfo file_info in dir_info.GetFiles())
                {
                    if (file_info.Extension == ".pst" || file_info.Extension == ".ost")
                        file_list.Add(file_info.FullName);
                }
            }
            catch
            {
            }
        }

        #endregion

        #region Compania

        // [Authorize]
        [HttpPost]
        [Route("api/Seguridad/ListarCompania")]
        public IEnumerable<WCO_ListarCompaniaMast_Result> ListarCompania(WCO_ListarCompaniaMast_Result ObjDetalle)
        {
            List<WCO_ListarCompaniaMast_Result> lst = new List<WCO_ListarCompaniaMast_Result>();
            try
            {
                ADDAT_Compania compa = new ADDAT_Compania();
                lst = compa.ListarCompania(ObjDetalle);
            }
            catch (Exception ex)
            {

            }
            return lst;
        }

        [Authorize]
        [HttpPost]
        [Route("api/Seguridad/MantenimientoCompania/{id}")]
        public async Task<IHttpActionResult> MantenimientoCompania(int id, [FromBody] WCO_ListarCompaniaMast_Result ObjDetalle)
        {
            ViewModalExite objLogin = new ViewModalExite();
            HttpStatusCode statusCode = new HttpStatusCode();
            try
            {
                ADDAT_Compania compa = new ADDAT_Compania();
                string valor = "0";
                if (ObjDetalle.DescripcionCorta != null)
                {
                    if (!string.IsNullOrEmpty(ObjDetalle.DescripcionCorta.ToString()))
                    {
                        ObjDetalle.DescripcionCorta = ObjDetalle.DescripcionCorta.ToUpper();
                    }
                }
                if (ObjDetalle.DescripcionLarga != null)
                {
                    if (!string.IsNullOrEmpty(ObjDetalle.DescripcionLarga.ToString()))
                    {
                        ObjDetalle.DescripcionLarga = ObjDetalle.DescripcionLarga.ToUpper();
                    }
                }
                if (ObjDetalle.DireccionAdicional != null)
                {
                    if (!string.IsNullOrEmpty(ObjDetalle.DireccionAdicional.ToString()))
                    {
                        ObjDetalle.DireccionAdicional = ObjDetalle.DireccionAdicional.ToUpper();

                    }
                }
                if (ObjDetalle.RepresentanteLegal != null)
                {
                    if (!string.IsNullOrEmpty(ObjDetalle.RepresentanteLegal.ToString()))
                    {
                        ObjDetalle.RepresentanteLegal = ObjDetalle.RepresentanteLegal.ToUpper();

                    }
                }

                switch (id)
                {
                    case 1:
                        valor = compa.Insertar(ObjDetalle);
                        if (valor != "0")
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
                        break;
                    case 2:
                        valor = compa.Actualizar(ObjDetalle);
                        if (valor == "0")
                        {
                            objLogin.success = false;
                            objLogin.valor = 0;
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
                        valor = compa.Inactivar(ObjDetalle);
                        if (valor != "0")
                        {
                            objLogin.success = false;
                            objLogin.valor = 0;
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
