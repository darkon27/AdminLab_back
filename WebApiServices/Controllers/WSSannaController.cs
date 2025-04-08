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
using WebApiServices.Models.Entidades;
using System.Data;
using Newtonsoft.Json;
using System.IO;

namespace WebApiServices.Controllers
{
    public class WSSannaController : ApiController
    {

        AdmisionController admcon = new AdmisionController();
        Metodos me = new Metodos();


        [HttpPost]
        [Route("api/WSSanna/ListarConsultaOA")]
        public IHttpActionResult ListarConsultaOA(ViewConsultaOA ObjDetalle)
        {
            var arrayOfLines = "";
            var arrayOfiNICIA = "";
            ViewModalExite objLogin = new ViewModalExite();
            List<COBE_OrdenAtencionDetalle> obj_Atencion = new List<COBE_OrdenAtencionDetalle>();

            ADBE_WServicioLog obj_pWServicioLog = new ADBE_WServicioLog();
            try
            {
                int xa = 0;

                //ViewConsultaOA

                arrayOfiNICIA = System.DateTime.Now + " | " + "Ingreso|ConsultaOA";
                obj_pWServicioLog.Pk.IdCodigo = System.DateTime.Now.Year + "" + System.DateTime.Now.Month.ToString("D2") + System.DateTime.Now.Day.ToString("D2");
                obj_pWServicioLog.TipoMsj = "ConsultaOA";
                obj_pWServicioLog.FechaIni = System.DateTime.Now; http://localhost:3002/
                obj_pWServicioLog.Sucursal = ObjDetalle.Sucursal;
                obj_pWServicioLog.CodigoOA = ObjDetalle.CodigoOA;            
                arrayOfLines = JsonConvert.SerializeObject(ObjDetalle, Newtonsoft.Json.Formatting.Indented);
                obj_pWServicioLog.Parametros = arrayOfLines;
                //UT_Kerberos.WriteLog(System.DateTime.Now + " | " + "Ingreso|ConsultaOA =" + arrayOfLines);
               // WebSanna.SoaService Ws = new WebSanna.SoaService();
                WsSannaLab.AppService Ws = new WsSannaLab.AppService();
                

                DataTable ds_Result = null;
                if (ObjDetalle.IdCliente == 64)
                {
                    xa = 1;
                    //DataTable ds_Result = Ws.ConsultaOASanBorja("0002155798", 9, "CSB");
                    ds_Result = Ws.ConsultaOASanBorja(ObjDetalle.CodigoOA, ObjDetalle.TipoOrdenAtencion, ObjDetalle.Sucursal);
                    if (ds_Result == null || ds_Result.Rows.Count == 0)
                    {
                        objLogin.success = true;
                        objLogin.mensaje = "El registro no contiene información";
                        objLogin.valor = 0;
                        obj_pWServicioLog.Observacion = objLogin.mensaje;
                        me.InsertarWServicioLog(obj_pWServicioLog);
                        return Ok(objLogin);
                    }
                }
                if (ObjDetalle.IdCliente == 331)
                {
                    xa = 1;
                    //DataTable ds_Result = Ws.ConsultaOASanBorja("0002155798", 5, "CEG");
                    //("0002995164", 5, "CB01");
                    //("0000393491", 5, "LM01");
                    //("0000883469", 5, "AQ01");

                    ds_Result = Ws.ConsultaOAGolf(ObjDetalle.CodigoOA, ObjDetalle.TipoOrdenAtencion, ObjDetalle.Sucursal);
                    if (ds_Result == null || ds_Result.Rows.Count == 0)
                    {
                        objLogin.success = true;
                        objLogin.mensaje = "El registro no contiene información";
                        objLogin.valor = 0;
                        obj_pWServicioLog.Observacion = objLogin.mensaje;
                        me.InsertarWServicioLog(obj_pWServicioLog);
                        return Ok(objLogin);
                    }
                }
                if (ObjDetalle.IdCliente == 417)
                {
                    xa = 1;
                    //DataTable ds_Result = Ws.ConsultaOASanBorja("0002155798", 5, "CSB");
                    ds_Result = Ws.ConsultaOAAliada(ObjDetalle.CodigoOA, ObjDetalle.TipoOrdenAtencion, ObjDetalle.Sucursal);
                    if (ds_Result == null || ds_Result.Rows.Count == 0)
                    {
                        objLogin.success = true;
                        objLogin.mensaje = "El registro no contiene información";
                        objLogin.valor = 0;
                        obj_pWServicioLog.Observacion = objLogin.mensaje;
                        me.InsertarWServicioLog(obj_pWServicioLog);
                        return Ok(objLogin);
                    }
                }
                if (ObjDetalle.IdCliente == 298614)
                {
                    xa = 1;
                    //DataTable ds_Result = Ws.ConsultaOASanBorja("1915170", 5, "CSF");
                  // ds_Result = Ws.ConsultaOAFerrer(ObjDetalle.CodigoOA, ObjDetalle.TipoOrdenAtencion, ObjDetalle.Sucursal, ObjDetalle.NombreCompleto, ObjDetalle.Documento);
                     ds_Result = Ws.ConsultaOAAliada(ObjDetalle.CodigoOA, ObjDetalle.TipoOrdenAtencion, ObjDetalle.Sucursal);

                    if (ds_Result == null || ds_Result.Rows.Count == 0)
                    {
                        objLogin.success = true;
                        objLogin.mensaje = "El registro no contiene información";
                        objLogin.valor = 0;
                        obj_pWServicioLog.Observacion = objLogin.mensaje;
                        me.InsertarWServicioLog(obj_pWServicioLog);
                        return Ok(objLogin);
                    }
                }
                //if (ObjDetalle.IdCliente == 148290)
                //{
                //    //DataTable ds_Result = Ws.ConsultaOASanBorja("0002155798", 9, "CSB");
                //    ds_Result = Ws.ConsultaOASanBorja(ObjDetalle.CodigoOA, ObjDetalle.TipoOrdenAtencion, ObjDetalle.Sucursal);
                //    if (ds_Result == null || ds_Result.Rows.Count == 0)
                //    {
                //      objLogin.success = false;
                //      objLogin.mensaje = "No existe Informacion";
                //      objLogin.valor = 0;
                //      return Ok(objLogin);
                //    }
                //}
      
                //UT_Kerberos.WriteLog(System.DateTime.Now + " | " + "Resultado|ConsultaOA =" + arrayOfLines);
                string strCodigoOA = "";
                int ab = 0;

                if(ds_Result != null)
                {
                    arrayOfLines = JsonConvert.SerializeObject(ds_Result, Newtonsoft.Json.Formatting.Indented);
                    obj_pWServicioLog.Trama = arrayOfLines;
                    foreach (DataRow _row in ds_Result.AsEnumerable())
                    {
                        COBE_OrdenAtencionDetalle obj_BEAdmision = new COBE_OrdenAtencionDetalle();
                        ab++;
                        if (ObjDetalle.IdCliente == 331)
                        {
                            if (!string.IsNullOrEmpty(_row["CorreoElectronico"].ToString())) obj_BEAdmision.Pacienteemail = _row["CorreoElectronico"].ToString();
                        }
                        if (ObjDetalle.IdCliente == 64)
                        {
                            if (!string.IsNullOrEmpty(_row["CorreoPaciente"].ToString())) obj_BEAdmision.Pacienteemail = _row["CorreoPaciente"].ToString();
                        }
                        if (ObjDetalle.IdCliente == 417)
                        {
                            if (!string.IsNullOrEmpty(_row["CorreoPaciente"].ToString())) obj_BEAdmision.Pacienteemail = _row["CorreoPaciente"].ToString();
                        }
                        if (!string.IsNullOrEmpty(_row["Documento"].ToString())) obj_BEAdmision.Documento = _row["Documento"].ToString().Trim();
                        if (!string.IsNullOrEmpty(_row["TipoDocumento"].ToString())) obj_BEAdmision.TipoDocumento = _row["TipoDocumento"].ToString();
                        if (!string.IsNullOrEmpty(_row["CodigoHC"].ToString())) obj_BEAdmision.CodigoHC = _row["CodigoHC"].ToString();
                        if (!string.IsNullOrEmpty(_row["CodigoOA"].ToString())) obj_BEAdmision.CodigoOA = _row["CodigoOA"].ToString();
                        strCodigoOA = obj_BEAdmision.CodigoOA;
                        if (!string.IsNullOrEmpty(_row["PacienteAPPaterno"].ToString())) obj_BEAdmision.PacienteAPPaterno = _row["PacienteAPPaterno"].ToString().Trim().Replace("'", "");
                        if (!string.IsNullOrEmpty(_row["PacienteAPMaterno"].ToString())) obj_BEAdmision.PacienteAPMaterno = _row["PacienteAPMaterno"].ToString().Trim().Replace("'", "");
                        if (!string.IsNullOrEmpty(_row["PacienteNombres"].ToString())) obj_BEAdmision.PacienteNombres = _row["PacienteNombres"].ToString().Trim().Replace("'", "");
                        if (!string.IsNullOrEmpty(_row["FechaNacimiento"].ToString())) obj_BEAdmision.FechaNacimiento = Convert.ToDateTime(_row["FechaNacimiento"].ToString());
                        if (!string.IsNullOrEmpty(_row["Sexo"].ToString())) obj_BEAdmision.Sexo = _row["Sexo"].ToString();
                        if (!string.IsNullOrEmpty(_row["FechaLimiteAtencion"].ToString())) obj_BEAdmision.FechaLimiteAtencion = Convert.ToDateTime(_row["FechaLimiteAtencion"]);
                        if (!string.IsNullOrEmpty(_row["Especialidad_Nombre"].ToString())) obj_BEAdmision.Especialidad_Nombre = _row["Especialidad_Nombre"].ToString().Trim().Replace("'", "");
                        if (!string.IsNullOrEmpty(_row["TipoAtencion"].ToString())) obj_BEAdmision.TipoAtencion = _row["TipoAtencion"].ToString();
                        if (!string.IsNullOrEmpty(_row["FechaCreacion"].ToString())) obj_BEAdmision.FechaCreacion = Convert.ToDateTime(_row["FechaCreacion"].ToString());
                        if (ObjDetalle.IdCliente == 298614)
                        {
                            //obj_BEAdmision.PK.TipoPaciente.PK.TipoPacienteId = Convert.ToInt32(ddl_TipoPaciente.SelectedValue);
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(_row["EstadoDocumento"].ToString())) obj_BEAdmision.EstadoDocumento = Convert.ToInt32(_row["EstadoDocumento"].ToString());
                            if (!string.IsNullOrEmpty(_row["SituacionInterfase"].ToString())) obj_BEAdmision.SituacionInterfase = Convert.ToInt32(_row["SituacionInterfase"].ToString());
                            if (!string.IsNullOrEmpty(_row["GrupoInterfase"].ToString())) obj_BEAdmision.GrupoInterfase = Convert.ToInt32(_row["GrupoInterfase"].ToString());
                        }
                        if (!string.IsNullOrEmpty(_row["Empleadora_RUC"].ToString())) obj_BEAdmision.Empleadora_RUC = _row["Empleadora_RUC"].ToString();
                        if (!string.IsNullOrEmpty(_row["Empleadora_Nombre"].ToString())) obj_BEAdmision.Empleadora_Nombre = _row["Empleadora_Nombre"].ToString().Replace("'", "");
                        if (!string.IsNullOrEmpty(_row["Aseguradora_Nombre"].ToString())) obj_BEAdmision.Aseguradora_Nombre = _row["Aseguradora_Nombre"].ToString().Replace("'", "");
                        if (ObjDetalle.IdCliente == 298614)
                        {

                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(_row["Aseguradora_RUC"].ToString())) obj_BEAdmision.Aseguradora_RUC = _row["Aseguradora_RUC"].ToString().Replace("'", "");
                        }
                        if (!string.IsNullOrEmpty(_row["CMP"].ToString())) obj_BEAdmision.CMP = _row["CMP"].ToString().Trim();
                        if (!string.IsNullOrEmpty(_row["CMP"].ToString()))
                        {
                            if (ObjDetalle.IdCliente == 298614)
                            {
                                string VALORES = _row["MedicoBusqueda"].ToString();
                                if (VALORES == "NO ESPECIFICA" || _row["CMP"].ToString().Trim() == "0")
                                {
                                    obj_BEAdmision.MedicoNombres = "Medico Automatico";
                                }
                                else
                                {
                                    string[] NapeDoctor = VALORES.ToString().Split(' ');
                                    int b = 0;
                                    string app = "";
                                    string apm = "";
                                    string npm = "";
                                    foreach (string extension in NapeDoctor)
                                    {
                                        b++;
                                        if (b == 1)
                                        {
                                            app = extension;
                                        }
                                        if (b == 2)
                                        {
                                            apm = extension;
                                        }
                                        if (b > 2)
                                        {
                                            npm += extension + " ";
                                        }
                                    }
                                    obj_BEAdmision.MedicoAPPaterno = app;
                                    obj_BEAdmision.MedicoAPMaterno = apm;
                                    obj_BEAdmision.MedicoNombres = npm;
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(_row["MedicoAPPaterno"].ToString())) obj_BEAdmision.MedicoAPPaterno = _row["MedicoAPPaterno"].ToString().Trim().Replace("'", "");
                                if (!string.IsNullOrEmpty(_row["MedicoAPMaterno"].ToString())) obj_BEAdmision.MedicoAPMaterno = _row["MedicoAPMaterno"].ToString().Trim().Replace("'", "");
                                if (!string.IsNullOrEmpty(_row["MedicoNombres"].ToString())) obj_BEAdmision.MedicoNombres = _row["MedicoNombres"].ToString().Trim().Replace("'", "");
                                string completo = obj_BEAdmision.MedicoAPPaterno + obj_BEAdmision.MedicoAPMaterno + obj_BEAdmision.MedicoNombres;
                                if (completo.Length == 0)
                                {
                                    obj_BEAdmision.MedicoNombres = "Medico Automatico";
                                }
                            }
                        }
                        else
                        {
                            obj_BEAdmision.MedicoNombres = "Medico Automatico";
                        }
                        obj_BEAdmision.Componente = _row["Componente"].ToString().Trim();
                        obj_BEAdmision.ComponenteNombre = _row["ComponenteNombre"].ToString().Trim();
                        if (!string.IsNullOrEmpty(_row["Linea"].ToString())) obj_BEAdmision.Linea = Convert.ToInt32(_row["Linea"].ToString());
                        if (!string.IsNullOrEmpty(_row["IdOrdenAtencion"].ToString())) obj_BEAdmision.IdOrdenAtencion = Convert.ToInt32(_row["IdOrdenAtencion"].ToString());
                        if (!string.IsNullOrEmpty(_row["CantidadSolicitada"].ToString())) obj_BEAdmision.CantidadSolicitada = Convert.ToInt32(Convert.ToDecimal(_row["CantidadSolicitada"].ToString()));
                        obj_Atencion.Add(obj_BEAdmision);
                    }
                }      
                if (xa > 0)
                {
                    objLogin.success = true;
                    objLogin.tokem = TokenGenerator.GenerateTokenJwt(strCodigoOA);
                    objLogin.mensaje = "Respuesta correcta de ConsultaOA";
                    objLogin.valor = obj_Atencion.Count;
                    objLogin.data = obj_Atencion;
                }
                else
                {
                    objLogin.success = true;
                    objLogin.tokem = "";
                    objLogin.mensaje = "No existe este cliente configurado";
                    objLogin.valor = 0;                 
                }
                obj_pWServicioLog.FechaFin = System.DateTime.Now;
                //arrayOfLines = JsonConvert.SerializeObject(objLogin, Newtonsoft.Json.Formatting.Indented);
                //obj_pWServicioLog = arrayOfLines;
                me.InsertarWServicioLog(obj_pWServicioLog);
                //arrayOfLines = JsonConvert.SerializeObject(ds_Result, Newtonsoft.Json.Formatting.Indented);
                //UT_Kerberos.WriteLog(System.DateTime.Now + " | " + "Resultado|ConsultaOA =" + arrayOfLines);
            }
            catch (Exception ex)
            {
                //arrayOfLines += JsonConvert.SerializeObject(ex, Newtonsoft.Json.Formatting.Indented);
                //UT_Kerberos.WriteLog(System.DateTime.Now + " | " + "Error|ConsultaOA =" + arrayOfLines);
                objLogin.success = false;
                objLogin.mensaje = "Exception=" + ex.InnerException + "| Message=" + ex.Message;
                objLogin.valor = -1;
                arrayOfLines = JsonConvert.SerializeObject(objLogin, Newtonsoft.Json.Formatting.Indented);
                obj_pWServicioLog.Trama = arrayOfLines;
                obj_pWServicioLog.FechaFin = System.DateTime.Now;
                me.InsertarWServicioLog(obj_pWServicioLog);

            }
            return Ok(objLogin);
        }


        //[HttpPost]
        //[Route("api/WSSanna/ListarConsultaOAMasiva")]
        //public IHttpActionResult ListarConsultaOAMasiva(ViewConsultaOA ObjDetalle)
        //{
        //    var arrayOfLines = "";
        //    var arrayOfiNICIA = "";
        //    ViewModalExite objLogin = new ViewModalExite();
        //    List<COBE_OrdenAtencionDetalle> obj_Atencion = new List<COBE_OrdenAtencionDetalle>();
        //    try
        //    {
        //        int xa = 0;

        //        //ViewConsultaOA

        //        arrayOfiNICIA = System.DateTime.Now + " | " + "Ingreso|ConsultaOA";
        //        //ObjDetalle.NombreCompleto = arrayOfLines;
        //        arrayOfLines = JsonConvert.SerializeObject(ObjDetalle, Newtonsoft.Json.Formatting.Indented);
        //        //UT_Kerberos.WriteLog(System.DateTime.Now + " | " + "Ingreso|ConsultaOA =" + arrayOfLines);
        //        WebSanna.SoaService Ws = new WebSanna.SoaService();
        //        DataTable ds_Result = null;
        //        if (ObjDetalle.IdCliente == 64)
        //        {
        //            xa = 1;
        //            //DataTable ds_Result = Ws.ConsultaOASanBorja("0002155798", 9, "CSB");
        //            ds_Result = Ws.ConsultaOASanBorja(ObjDetalle.CodigoOA, ObjDetalle.TipoOrdenAtencion, ObjDetalle.Sucursal);
        //            if (ds_Result == null || ds_Result.Rows.Count == 0)
        //            {
        //                objLogin.success = true;
        //                objLogin.mensaje = "No existe Informacion";
        //                objLogin.valor = 0;
        //                return Ok(objLogin);
        //            }
        //        }
        //        if (ObjDetalle.IdCliente == 331)
        //        {
        //            xa = 1;
        //            //DataTable ds_Result = Ws.ConsultaOASanBorja("0002155798", 9, "CSB");
        //            ds_Result = Ws.ConsultaOAGolf(ObjDetalle.CodigoOA, ObjDetalle.TipoOrdenAtencion, ObjDetalle.Sucursal);
        //            if (ds_Result == null || ds_Result.Rows.Count == 0)
        //            {
        //                objLogin.success = true;
        //                objLogin.mensaje = "No existe Informacion";
        //                objLogin.valor = 0;
        //                return Ok(objLogin);
        //            }
        //        }
        //        if (ObjDetalle.IdCliente == 417)
        //        {
        //            xa = 1;
        //            //DataTable ds_Result = Ws.ConsultaOASanBorja("0002155798", 9, "CSB");
        //            ds_Result = Ws.ConsultaOAAliada(ObjDetalle.CodigoOA, ObjDetalle.TipoOrdenAtencion, ObjDetalle.Sucursal);
        //            if (ds_Result == null || ds_Result.Rows.Count == 0)
        //            {
        //                objLogin.success = true;
        //                objLogin.mensaje = "No existe Informacion";
        //                objLogin.valor = 0;
        //                return Ok(objLogin);
        //            }
        //        }
        //        if (ObjDetalle.IdCliente == 298614)
        //        {
        //            xa = 1;
        //            //DataTable ds_Result = Ws.ConsultaOASanBorja("1915170", 5, "CSF");
        //            ds_Result = Ws.ConsultaOAFerrer(ObjDetalle.CodigoOA, ObjDetalle.TipoOrdenAtencion, ObjDetalle.Sucursal, ObjDetalle.NombreCompleto, ObjDetalle.Documento);
        //            if (ds_Result == null || ds_Result.Rows.Count == 0)
        //            {
        //                objLogin.success = true;
        //                objLogin.mensaje = "No existe Informacion";
        //                objLogin.valor = 0;
        //                return Ok(objLogin);
        //            }
        //        }
        //        //if (ObjDetalle.IdCliente == 148290)
        //        //{
        //        //    //DataTable ds_Result = Ws.ConsultaOASanBorja("0002155798", 9, "CSB");
        //        //    ds_Result = Ws.ConsultaOASanBorja(ObjDetalle.CodigoOA, ObjDetalle.TipoOrdenAtencion, ObjDetalle.Sucursal);
        //        //    if (ds_Result == null || ds_Result.Rows.Count == 0)
        //        //    {
        //        //      objLogin.success = false;
        //        //      objLogin.mensaje = "No existe Informacion";
        //        //      objLogin.valor = 0;
        //        //      return Ok(objLogin);
        //        //    }
        //        //}

        //        arrayOfLines = JsonConvert.SerializeObject(ds_Result, Newtonsoft.Json.Formatting.Indented);
        //        //UT_Kerberos.WriteLog(System.DateTime.Now + " | " + "Resultado|ConsultaOA =" + arrayOfLines);
        //        string strCodigoOA = "";
        //        int ab = 0;

        //        if (ds_Result != null)
        //        {
        //            RequestAdmision RestAdmision = new RequestAdmision();

        //            WCO_TraerXAdmisionServicio_Result ObjeAdmision = new  WCO_TraerXAdmisionServicio_Result();
        //            foreach (DataRow _row in ds_Result.AsEnumerable())
        //            {
        //                COBE_OrdenAtencionDetalle obj_BEAdmision = new COBE_OrdenAtencionDetalle();
        //                ab++;
        //                if (ObjDetalle.IdCliente == 331)
        //                {
        //                    if (!string.IsNullOrEmpty(_row["CorreoElectronico"].ToString())) obj_BEAdmision.Pacienteemail = _row["CorreoElectronico"].ToString();
        //                }
        //                if (ObjDetalle.IdCliente == 64)
        //                {
        //                    if (!string.IsNullOrEmpty(_row["CorreoPaciente"].ToString())) obj_BEAdmision.Pacienteemail = _row["CorreoPaciente"].ToString();
        //                }
        //                if (ObjDetalle.IdCliente == 417)
        //                {
        //                    if (!string.IsNullOrEmpty(_row["CorreoPaciente"].ToString())) obj_BEAdmision.Pacienteemail = _row["CorreoPaciente"].ToString();
        //                }
        //                if (!string.IsNullOrEmpty(_row["Documento"].ToString())) obj_BEAdmision.Documento = _row["Documento"].ToString().Trim();
        //                if (!string.IsNullOrEmpty(_row["TipoDocumento"].ToString())) obj_BEAdmision.TipoDocumento = _row["TipoDocumento"].ToString();
        //                if (!string.IsNullOrEmpty(_row["CodigoHC"].ToString())) obj_BEAdmision.CodigoHC = _row["CodigoHC"].ToString();
        //                if (!string.IsNullOrEmpty(_row["CodigoOA"].ToString())) obj_BEAdmision.CodigoOA = _row["CodigoOA"].ToString();
        //                strCodigoOA = obj_BEAdmision.CodigoOA;
        //                if (!string.IsNullOrEmpty(_row["PacienteAPPaterno"].ToString())) obj_BEAdmision.PacienteAPPaterno = _row["PacienteAPPaterno"].ToString().Trim().Replace("'", "");
        //                if (!string.IsNullOrEmpty(_row["PacienteAPMaterno"].ToString())) obj_BEAdmision.PacienteAPMaterno = _row["PacienteAPMaterno"].ToString().Trim().Replace("'", "");
        //                if (!string.IsNullOrEmpty(_row["PacienteNombres"].ToString())) obj_BEAdmision.PacienteNombres = _row["PacienteNombres"].ToString().Trim().Replace("'", "");
        //                if (!string.IsNullOrEmpty(_row["FechaNacimiento"].ToString())) obj_BEAdmision.FechaNacimiento = Convert.ToDateTime(_row["FechaNacimiento"].ToString());
        //                if (!string.IsNullOrEmpty(_row["Sexo"].ToString())) obj_BEAdmision.Sexo = _row["Sexo"].ToString();
        //                if (!string.IsNullOrEmpty(_row["FechaLimiteAtencion"].ToString())) obj_BEAdmision.FechaLimiteAtencion = Convert.ToDateTime(_row["FechaLimiteAtencion"]);
        //                if (!string.IsNullOrEmpty(_row["Especialidad_Nombre"].ToString())) obj_BEAdmision.Especialidad_Nombre = _row["Especialidad_Nombre"].ToString().Trim().Replace("'", "");
        //                if (!string.IsNullOrEmpty(_row["TipoAtencion"].ToString())) obj_BEAdmision.TipoAtencion = _row["TipoAtencion"].ToString();
        //                if (!string.IsNullOrEmpty(_row["FechaCreacion"].ToString())) obj_BEAdmision.FechaCreacion = Convert.ToDateTime(_row["FechaCreacion"].ToString());
        //                if (ObjDetalle.IdCliente == 298614)
        //                {
        //                    //obj_BEAdmision.PK.TipoPaciente.PK.TipoPacienteId = Convert.ToInt32(ddl_TipoPaciente.SelectedValue);
        //                }
        //                else
        //                {
        //                    if (!string.IsNullOrEmpty(_row["EstadoDocumento"].ToString())) obj_BEAdmision.EstadoDocumento = Convert.ToInt32(_row["EstadoDocumento"].ToString());
        //                    if (!string.IsNullOrEmpty(_row["SituacionInterfase"].ToString())) obj_BEAdmision.SituacionInterfase = Convert.ToInt32(_row["SituacionInterfase"].ToString());
        //                    if (!string.IsNullOrEmpty(_row["GrupoInterfase"].ToString())) obj_BEAdmision.GrupoInterfase = Convert.ToInt32(_row["GrupoInterfase"].ToString());
        //                }
        //                if (!string.IsNullOrEmpty(_row["Empleadora_RUC"].ToString())) obj_BEAdmision.Empleadora_RUC = _row["Empleadora_RUC"].ToString();
        //                if (!string.IsNullOrEmpty(_row["Empleadora_Nombre"].ToString())) obj_BEAdmision.Empleadora_Nombre = _row["Empleadora_Nombre"].ToString().Replace("'", "");
        //                if (!string.IsNullOrEmpty(_row["Aseguradora_Nombre"].ToString())) obj_BEAdmision.Aseguradora_Nombre = _row["Aseguradora_Nombre"].ToString().Replace("'", "");
        //                if (ObjDetalle.IdCliente == 298614)
        //                {

        //                }
        //                else
        //                {
        //                    if (!string.IsNullOrEmpty(_row["Aseguradora_RUC"].ToString())) obj_BEAdmision.Aseguradora_RUC = _row["Aseguradora_RUC"].ToString().Replace("'", "");
        //                }
        //                if (!string.IsNullOrEmpty(_row["CMP"].ToString())) obj_BEAdmision.CMP = _row["CMP"].ToString().Trim();
        //                if (!string.IsNullOrEmpty(_row["CMP"].ToString()))
        //                {
        //                    if (ObjDetalle.IdCliente == 298614)
        //                    {
        //                        string VALORES = _row["MedicoBusqueda"].ToString();
        //                        if (VALORES == "NO ESPECIFICA" || _row["CMP"].ToString().Trim() == "0")
        //                        {
        //                            obj_BEAdmision.MedicoNombres = "Medico Automatico";
        //                        }
        //                        else
        //                        {
        //                            string[] NapeDoctor = VALORES.ToString().Split(' ');
        //                            int b = 0;
        //                            string app = "";
        //                            string apm = "";
        //                            string npm = "";
        //                            foreach (string extension in NapeDoctor)
        //                            {
        //                                b++;
        //                                if (b == 1)
        //                                {
        //                                    app = extension;
        //                                }
        //                                if (b == 2)
        //                                {
        //                                    apm = extension;
        //                                }
        //                                if (b > 2)
        //                                {
        //                                    npm += extension + " ";
        //                                }
        //                            }
        //                            obj_BEAdmision.MedicoAPPaterno = app;
        //                            obj_BEAdmision.MedicoAPMaterno = apm;
        //                            obj_BEAdmision.MedicoNombres = npm;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (!string.IsNullOrEmpty(_row["MedicoAPPaterno"].ToString())) obj_BEAdmision.MedicoAPPaterno = _row["MedicoAPPaterno"].ToString().Trim().Replace("'", "");
        //                        if (!string.IsNullOrEmpty(_row["MedicoAPMaterno"].ToString())) obj_BEAdmision.MedicoAPMaterno = _row["MedicoAPMaterno"].ToString().Trim().Replace("'", "");
        //                        if (!string.IsNullOrEmpty(_row["MedicoNombres"].ToString())) obj_BEAdmision.MedicoNombres = _row["MedicoNombres"].ToString().Trim().Replace("'", "");
        //                        string completo = obj_BEAdmision.MedicoAPPaterno + obj_BEAdmision.MedicoAPMaterno + obj_BEAdmision.MedicoNombres;
        //                        if (completo.Length == 0)
        //                        {
        //                            obj_BEAdmision.MedicoNombres = "Medico Automatico";
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    obj_BEAdmision.MedicoNombres = "Medico Automatico";
        //                }
        //                obj_BEAdmision.Componente = _row["Componente"].ToString().Trim();
        //                obj_BEAdmision.ComponenteNombre = _row["ComponenteNombre"].ToString().Trim();
        //                if (!string.IsNullOrEmpty(_row["Linea"].ToString())) obj_BEAdmision.Linea = Convert.ToInt32(_row["Linea"].ToString());
        //                if (!string.IsNullOrEmpty(_row["IdOrdenAtencion"].ToString())) obj_BEAdmision.IdOrdenAtencion = Convert.ToInt32(_row["IdOrdenAtencion"].ToString());
        //                if (!string.IsNullOrEmpty(_row["CantidadSolicitada"].ToString())) obj_BEAdmision.CantidadSolicitada = Convert.ToInt32(Convert.ToDecimal(_row["CantidadSolicitada"].ToString()));
        //                obj_Atencion.Add(obj_BEAdmision);


        //                WCO_ListarAdmisionServicioDetalle_Result EntiSelecc = new WCO_ListarAdmisionServicioDetalle_Result();
                      
        //                EntiSelecc.CodigoComponente = obj_BEAdmision.Componente;
        //                //EntiSelecc.com = obj_BEAdmision.ComponenteNombre;
        //                EntiSelecc.Linea = obj_BEAdmision.Linea;
        //                EntiSelecc.IdOrdenAtencion = obj_BEAdmision.IdOrdenAtencion;
        //                EntiSelecc.Cantidad = obj_BEAdmision.CantidadSolicitada;
        //                EntiSelecc.Valor = Convert.ToDecimal(0.0);
        //                EntiSelecc.Valorprecio = Convert.ToDecimal(0.0);
        //                EntiSelecc.ValorEmpresa = Convert.ToDecimal(0.0);
        //                EntiSelecc.IdListaBase = 0;
        //                EntiSelecc.CanTotal = 0;
        //                EntiSelecc.Igv = 0;
        //                EntiSelecc.ValorIgv = 0;
        //                EntiSelecc.UsuarioCreacion = "40859200";
        //                EntiSelecc.UsuarioModificacion = "40859200";
        //                EntiSelecc.IpCreacion = "172.16.3.3";
        //                EntiSelecc.IpModificacion = "172.16.3.3";
        //                EntiSelecc.Estado = 1;
        //                EntiSelecc.TipoOperacionID = 439;
        //                EntiSelecc.IdAdmision = 0;                        
                    

        //                ObjeAdmision.Estado = 1;
        //                ObjeAdmision.TipoOperacionId = 439;
        //                ObjeAdmision.IdSede = 524;
        //                ObjeAdmision.UneuNegocioId = 1;
        //                ObjeAdmision.FechaAdmision = DateTime.Now;
        //                ObjeAdmision.UsuarioCreacion = "40859200";
        //                ObjeAdmision.IpCreacion = "172.16.3.3";
        //                ObjeAdmision.IpModificacion = "172.16.3.3";

        //                ObjeAdmision.CorreoElectronico = obj_BEAdmision.Pacienteemail;
        //                ObjeAdmision.Documento = obj_BEAdmision.Documento;
        //                ObjeAdmision.TipoDocumento = obj_BEAdmision.TipoDocumento;
        //                ObjeAdmision.HistoriaClinica = obj_BEAdmision.CodigoHC;
        //                ObjeAdmision.OrdenAtencion = obj_BEAdmision.CodigoOA;
        //                ObjeAdmision.apellidopaterno = obj_BEAdmision.PacienteAPPaterno;
        //                ObjeAdmision.apellidomaterno = obj_BEAdmision.PacienteAPMaterno;
        //                RestAdmision.Admision.nombres = obj_BEAdmision.PacienteNombres;
        //                ObjeAdmision.fechanacimiento = obj_BEAdmision.FechaNacimiento;
        //                ObjeAdmision.sexo = obj_BEAdmision.Sexo;

        //                ObjeAdmision.DesEspecialidad = obj_BEAdmision.Especialidad_Nombre;
        //                ObjeAdmision.TipoAtencion = 1;
        //                ObjeAdmision.FechaCreacion = obj_BEAdmision.FechaCreacion;
        //                ObjeAdmision.RucEmpresaPaciente = obj_BEAdmision.Empleadora_RUC;
        //                ObjeAdmision.EmpresaPaciente = obj_BEAdmision.Empleadora_Nombre;
        //                ObjeAdmision.NombreAseguradora = obj_BEAdmision.Aseguradora_Nombre;
        //                ObjeAdmision.RucAseguradora = obj_BEAdmision.Aseguradora_RUC;
        //                ObjeAdmision.CMP = obj_BEAdmision.CMP;
        //                ObjeAdmision.PaternoMedico = obj_BEAdmision.MedicoAPPaterno;
        //                ObjeAdmision.MaternoMedico = obj_BEAdmision.MedicoAPMaterno;
        //                ObjeAdmision.NombreMedico = obj_BEAdmision.MedicoNombres;
        //                RestAdmision.list_AdmisionServicio.Add(EntiSelecc);
        //            }
        //            RestAdmision.IndicadorWS = 1;
        //            RestAdmision.Admision = ObjeAdmision;
        //            arrayOfLines = JsonConvert.SerializeObject(admcon.RegistrarAdmision(1, RestAdmision), Newtonsoft.Json.Formatting.Indented);
        //        }
        //        if (xa > 0)
        //        {
        //            objLogin.success = true;
        //            objLogin.tokem = TokenGenerator.GenerateTokenJwt(strCodigoOA);
        //            objLogin.mensaje = "Respuesta correcta de ConsultaOA";
        //            objLogin.valor = obj_Atencion.Count;
        //            objLogin.data = obj_Atencion;
        //        }
        //        else
        //        {
        //            objLogin.success = true;
        //            objLogin.tokem = "";
        //            objLogin.mensaje = "No existe este cliente configurado";
        //            objLogin.valor = 0;
        //        }
        //        arrayOfLines = JsonConvert.SerializeObject(ds_Result, Newtonsoft.Json.Formatting.Indented);
        //        //UT_Kerberos.WriteLog(System.DateTime.Now + " | " + "Resultado|ConsultaOA =" + arrayOfLines);
        //    }
        //    catch (Exception ex)
        //    {
        //        arrayOfLines += JsonConvert.SerializeObject(ex, Newtonsoft.Json.Formatting.Indented);
        //        //UT_Kerberos.WriteLog(System.DateTime.Now + " | " + "Error|ConsultaOA =" + arrayOfLines);
        //        objLogin.success = false;
        //        objLogin.mensaje = "Exception=" + ex.InnerException + "| Message=" + ex.Message;
        //        objLogin.valor = -1;
        //    }
        //    return Ok(objLogin);
        //}


        [HttpPost]
        [Route("api/WSSanna/ListarConsultaRest")]
        public IEnumerable<COBE_OrdenAtencionDetalle> ListarConsultaRest(ViewConsultaOA ObjDetalle)
        {
            List<COBE_OrdenAtencionDetalle> obj_Atencion = new List<COBE_OrdenAtencionDetalle>();
            try
            {
                WebSanna.SoaService Ws = new WebSanna.SoaService();
                DataTable ds_Result = null;
                if (ObjDetalle.IdCliente == 64)
                {
                    //DataTable ds_Result = Ws.ConsultaOASanBorja("0002155798", 9, "CSB");
                    ds_Result = Ws.ConsultaOASanBorja(ObjDetalle.CodigoOA, ObjDetalle.TipoOrdenAtencion, ObjDetalle.Sucursal);
                    if (ds_Result == null || ds_Result.Rows.Count == 0)
                    {
                        //obj_Atencion.message = "No hay Atenciones";
                        //obj_Atencion.fail = "0";                  
                    }
                }
                if (ObjDetalle.IdCliente == 331)
                {
                    //DataTable ds_Result = Ws.ConsultaOASanBorja("0002155798", 9, "CSB");
                    ds_Result = Ws.ConsultaOAGolf(ObjDetalle.CodigoOA, ObjDetalle.TipoOrdenAtencion, ObjDetalle.Sucursal);
                    if (ds_Result == null || ds_Result.Rows.Count == 0)
                    {
                        //obj_Atencion.message = "No hay Atenciones";
                        //obj_Atencion.fail = "0";                  
                    }
                }
                if (ObjDetalle.IdCliente == 417)
                {
                    //DataTable ds_Result = Ws.ConsultaOASanBorja("0002155798", 9, "CSB");
                    ds_Result = Ws.ConsultaOAAliada(ObjDetalle.CodigoOA, ObjDetalle.TipoOrdenAtencion, ObjDetalle.Sucursal);
                    if (ds_Result == null || ds_Result.Rows.Count == 0)
                    {
                        //obj_Atencion.message = "No hay Atenciones";
                        //obj_Atencion.fail = "0";                  
                    }
                }
                if (ObjDetalle.IdCliente == 298614)
                {
                    //DataTable ds_Result = Ws.ConsultaOASanBorja("0002155798", 9, "CSB");
                    ds_Result = Ws.ConsultaOAFerrer(ObjDetalle.CodigoOA, ObjDetalle.TipoOrdenAtencion, ObjDetalle.Sucursal, ObjDetalle.NombreCompleto, ObjDetalle.Documento);
                    if (ds_Result == null || ds_Result.Rows.Count == 0)
                    {
                        //obj_Atencion.message = "No hay Atenciones";
                        //obj_Atencion.fail = "0";                  
                    }
                }
                //if (ObjDetalle.IdCliente == 148290)
                //{
                //    //DataTable ds_Result = Ws.ConsultaOASanBorja("0002155798", 9, "CSB");
                //    ds_Result = Ws.ConsultaOASanBorja(ObjDetalle.CodigoOA, ObjDetalle.TipoOrdenAtencion, ObjDetalle.Sucursal);
                //    if (ds_Result == null || ds_Result.Rows.Count == 0)
                //    {
                //        //obj_Atencion.message = "No hay Atenciones";
                //        //obj_Atencion.fail = "0";                  
                //    }
                //}

                int ab = 0;
                foreach (DataRow _row in ds_Result.AsEnumerable())
                {
                    COBE_OrdenAtencionDetalle obj_BEAdmision = new COBE_OrdenAtencionDetalle();
                    ab++;
                    if (ObjDetalle.IdCliente == 331)
                    {
                        if (!string.IsNullOrEmpty(_row["CorreoElectronico"].ToString())) obj_BEAdmision.Pacienteemail = _row["CorreoElectronico"].ToString();
                    }
                    if (ObjDetalle.IdCliente == 64)
                    {
                        if (!string.IsNullOrEmpty(_row["CorreoPaciente"].ToString())) obj_BEAdmision.Pacienteemail = _row["CorreoPaciente"].ToString();
                    }
                    if (ObjDetalle.IdCliente == 417)
                    {
                        if (!string.IsNullOrEmpty(_row["CorreoPaciente"].ToString())) obj_BEAdmision.Pacienteemail = _row["CorreoPaciente"].ToString();
                    }
                    if (!string.IsNullOrEmpty(_row["Documento"].ToString())) obj_BEAdmision.Documento = _row["Documento"].ToString().Trim();
                    if (!string.IsNullOrEmpty(_row["TipoDocumento"].ToString())) obj_BEAdmision.TipoDocumento = _row["TipoDocumento"].ToString();
                    if (!string.IsNullOrEmpty(_row["CodigoHC"].ToString())) obj_BEAdmision.CodigoHC = _row["CodigoHC"].ToString();
                    if (!string.IsNullOrEmpty(_row["CodigoOA"].ToString())) obj_BEAdmision.CodigoOA = _row["CodigoOA"].ToString();
                    if (!string.IsNullOrEmpty(_row["PacienteAPPaterno"].ToString())) obj_BEAdmision.PacienteAPPaterno = _row["PacienteAPPaterno"].ToString().Trim().Replace("'", "");
                    if (!string.IsNullOrEmpty(_row["PacienteAPMaterno"].ToString())) obj_BEAdmision.PacienteAPMaterno = _row["PacienteAPMaterno"].ToString().Trim().Replace("'", "");
                    if (!string.IsNullOrEmpty(_row["PacienteNombres"].ToString())) obj_BEAdmision.PacienteNombres = _row["PacienteNombres"].ToString().Trim().Replace("'", "");
                    if (!string.IsNullOrEmpty(_row["FechaNacimiento"].ToString())) obj_BEAdmision.FechaNacimiento = Convert.ToDateTime(_row["FechaNacimiento"].ToString());
                    if (!string.IsNullOrEmpty(_row["Sexo"].ToString())) obj_BEAdmision.Sexo = _row["Sexo"].ToString();
                    if (!string.IsNullOrEmpty(_row["FechaLimiteAtencion"].ToString())) obj_BEAdmision.FechaLimiteAtencion = Convert.ToDateTime(_row["FechaLimiteAtencion"]);
                    if (!string.IsNullOrEmpty(_row["Especialidad_Nombre"].ToString())) obj_BEAdmision.Especialidad_Nombre = _row["Especialidad_Nombre"].ToString().Trim().Replace("'", "");
                    if (!string.IsNullOrEmpty(_row["TipoAtencion"].ToString())) obj_BEAdmision.TipoAtencion = _row["TipoAtencion"].ToString();
                    if (!string.IsNullOrEmpty(_row["FechaCreacion"].ToString())) obj_BEAdmision.FechaCreacion = Convert.ToDateTime(_row["FechaCreacion"].ToString());
                    if (ObjDetalle.IdCliente == 298614)
                    {
                        //obj_BEAdmision.PK.TipoPaciente.PK.TipoPacienteId = Convert.ToInt32(ddl_TipoPaciente.SelectedValue);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(_row["EstadoDocumento"].ToString())) obj_BEAdmision.EstadoDocumento = Convert.ToInt32(_row["EstadoDocumento"].ToString());
                        if (!string.IsNullOrEmpty(_row["SituacionInterfase"].ToString())) obj_BEAdmision.SituacionInterfase = Convert.ToInt32(_row["SituacionInterfase"].ToString());
                        if (!string.IsNullOrEmpty(_row["GrupoInterfase"].ToString())) obj_BEAdmision.GrupoInterfase = Convert.ToInt32(_row["GrupoInterfase"].ToString());
                    }
                    if (!string.IsNullOrEmpty(_row["Empleadora_RUC"].ToString())) obj_BEAdmision.Empleadora_RUC = _row["Empleadora_RUC"].ToString();
                    if (!string.IsNullOrEmpty(_row["Empleadora_Nombre"].ToString())) obj_BEAdmision.Empleadora_Nombre = _row["Empleadora_Nombre"].ToString().Replace("'", "");
                    if (!string.IsNullOrEmpty(_row["Aseguradora_Nombre"].ToString())) obj_BEAdmision.Aseguradora_Nombre = _row["Aseguradora_Nombre"].ToString().Replace("'", "");
                    if (ObjDetalle.IdCliente == 298614)
                    {

                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(_row["Aseguradora_RUC"].ToString())) obj_BEAdmision.Aseguradora_RUC = _row["Aseguradora_RUC"].ToString().Replace("'", "");
                    }
                    if (!string.IsNullOrEmpty(_row["CMP"].ToString())) obj_BEAdmision.CMP = _row["CMP"].ToString().Trim();
                    if (!string.IsNullOrEmpty(_row["CMP"].ToString()))
                    {
                        if (ObjDetalle.IdCliente == 298614)
                        {
                            string VALORES = _row["MedicoBusqueda"].ToString();
                            if (VALORES == "NO ESPECIFICA" || _row["CMP"].ToString().Trim() == "0")
                            {
                                obj_BEAdmision.MedicoNombres = "Medico Automatico";
                            }
                            else
                            {
                                string[] NapeDoctor = VALORES.ToString().Split(' ');
                                int b = 0;
                                string app = "";
                                string apm = "";
                                string npm = "";
                                foreach (string extension in NapeDoctor)
                                {
                                    b++;
                                    if (b == 1)
                                    {
                                        app = extension;
                                    }
                                    if (b == 2)
                                    {
                                        apm = extension;
                                    }
                                    if (b > 2)
                                    {
                                        npm += extension + " ";
                                    }
                                }
                                obj_BEAdmision.MedicoAPPaterno = app;
                                obj_BEAdmision.MedicoAPMaterno = apm;
                                obj_BEAdmision.MedicoNombres = npm;
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(_row["MedicoAPPaterno"].ToString())) obj_BEAdmision.MedicoAPPaterno = _row["MedicoAPPaterno"].ToString().Trim().Replace("'", "");
                            if (!string.IsNullOrEmpty(_row["MedicoAPMaterno"].ToString())) obj_BEAdmision.MedicoAPMaterno = _row["MedicoAPMaterno"].ToString().Trim().Replace("'", "");
                            if (!string.IsNullOrEmpty(_row["MedicoNombres"].ToString())) obj_BEAdmision.MedicoNombres = _row["MedicoNombres"].ToString().Trim().Replace("'", "");
                            string completo = obj_BEAdmision.MedicoAPPaterno + obj_BEAdmision.MedicoAPMaterno + obj_BEAdmision.MedicoNombres;
                            if (completo.Length == 0)
                            {
                                obj_BEAdmision.MedicoNombres = "Medico Automatico";
                            }
                        }
                    }
                    else
                    {
                        obj_BEAdmision.MedicoNombres = "Medico Automatico";
                    }
                    obj_BEAdmision.Componente = _row["Componente"].ToString().Trim();
                    obj_BEAdmision.ComponenteNombre = _row["ComponenteNombre"].ToString().Trim();
                    if (!string.IsNullOrEmpty(_row["Linea"].ToString())) obj_BEAdmision.Linea = Convert.ToInt32(_row["Linea"].ToString());
                    if (!string.IsNullOrEmpty(_row["IdOrdenAtencion"].ToString())) obj_BEAdmision.IdOrdenAtencion = Convert.ToInt32(_row["IdOrdenAtencion"].ToString());
                    if (!string.IsNullOrEmpty(_row["CantidadSolicitada"].ToString())) obj_BEAdmision.CantidadSolicitada = Convert.ToInt32(Convert.ToDecimal(_row["CantidadSolicitada"].ToString()));
                    obj_Atencion.Add(obj_BEAdmision);
                }
            }
            catch
            {

            }
            return obj_Atencion;
        }


        public DataTable selectDisctinct(DataTable dt, string columnName)
        {
            try
            {
                DataTable distintos = dt.Clone();
                foreach (DataRow dr in distintos.Rows)
                {
                    distintos.ImportRow(dt.Select(columnName + " = '" + dr[0] + "'")[0]);
                }
                return distintos;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

    }
}
