using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiServices.Models;
using WebApiServices.Models.Request;

namespace WebApiServices.Controllers
{
    public class ConsultaController : ApiController
    {
        Metodos m = new Metodos();
        #region PreCarga

        [HttpPost]
        public IHttpActionResult WSPreAdmision(RegistroPreAdmision Model)
        {
            int c = 0;
            string observacion = "UserName= " + Model.UserName + "| Password= " + Model.Password + " ¬ ";
            if (Model.UserName == "40850000" && Model.Password == "pmMpLQiMkeo")
            {
                if (Model != null)
                {
                    string ValorCliente = "";
                    string ValidaDetalle = "";
                    int ValidaError = 0;
                    Nullable<int> Id = 0;

                    List<WCO_ListarPreCarga_Result> list_PreOk = new List<WCO_ListarPreCarga_Result>();
                    List<WCO_ListarPreCarga_Result> list_PreHomoError = new List<WCO_ListarPreCarga_Result>();
                    List<WCO_ListarHomologacionPreAdmision_Result> list_Homolo = new List<WCO_ListarHomologacionPreAdmision_Result>();
                    try
                    {
                        foreach (WCO_ListarPreCarga_Result intobj2 in Model.list_PreAdmision)
                        {
                            try
                            {
                                if (intobj2.Cliente.Length > 0)
                                    ValorCliente = intobj2.Cliente;
                                break;
                            }
                            catch (Exception exception)
                            {
                                string asun = System.DateTime.Now + " | " + "Error en los datos";
                                observacion += asun + " | " + exception.GetType();
                            }
                        }
                        if (ValorCliente.Length == 0)
                        {
                            observacion += observacion + " Debe Ingresar el codigo Cliente = " + ValorCliente;
                        }
                        else
                        {
                            list_Homolo = m.ListaHomologacionPreAdmision(ValorCliente);
                        }

                        if (list_Homolo.Count == 0)
                        {
                            observacion += observacion + " No hay ninguna homologacion para este cliente =" + ValorCliente;
                        }
                        else
                        {
                            foreach (WCO_ListarPreCarga_Result intobj2 in Model.list_PreAdmision)
                            {
                                try
                                {
                                    //WCO_ListarPreCarga_Result Obj_PreAdmision = new WCO_ListarPreCarga_Result();
                                    ValidaDetalle = "";
                                    c++;

                                    #region ValidacionObligatoria

                                    if (string.IsNullOrEmpty(intobj2.Cliente.ToString()))
                                    {
                                        ValidaDetalle += "Error Cliente =" + intobj2.Cliente;
                                    }
                                    if (string.IsNullOrEmpty(intobj2.Sucursal.ToString()))
                                    {
                                        ValidaDetalle += "Error Sucursal =" + intobj2.Sucursal;
                                    }
                                    if (string.IsNullOrEmpty(intobj2.CodigoOA.ToString()))
                                    {
                                        ValidaDetalle += "Error CodigoOA =" + intobj2.CodigoOA;
                                    }
                                    if (string.IsNullOrEmpty(intobj2.FechaNacimiento.ToString()))
                                    {
                                        ValidaDetalle += "Error FechaNacimiento =" + intobj2.FechaNacimiento;
                                    }
                                    if (string.IsNullOrEmpty(intobj2.Sexo.ToString()))
                                    {
                                        ValidaDetalle += "Error Sexo =" + intobj2.Sexo;
                                    }
                                    if (string.IsNullOrEmpty(intobj2.TipoDocumento.ToString()))
                                    {
                                        ValidaDetalle += "Error TipoDocumento =" + intobj2.TipoDocumento;
                                    }
                                    if (string.IsNullOrEmpty(intobj2.Documento.ToString()))
                                    {
                                        ValidaDetalle += "Error Documento =" + intobj2.Documento;
                                    }
                                    if (string.IsNullOrEmpty(intobj2.IdOrdenAtencion.ToString()))
                                    {
                                        ValidaDetalle += "Error IdOrdenAtencion =" + intobj2.IdOrdenAtencion;
                                    }
                                    if (string.IsNullOrEmpty(intobj2.Linea.ToString()))
                                    {
                                        ValidaDetalle += "Error Linea =" + intobj2.Linea;
                                    }
                                    if (string.IsNullOrEmpty(intobj2.Componente.ToString()))
                                    {
                                        ValidaDetalle += "Error Componente =" + intobj2.Componente;
                                    }
                                    if (string.IsNullOrEmpty(intobj2.ComponenteNombre.ToString()))
                                    {
                                        ValidaDetalle += "Error ComponenteNombre =" + intobj2.ComponenteNombre;
                                    }

                                    #endregion

                                    if (ValidaDetalle.Length == 0)
                                    {
                                        List<WCO_ListarHomologacionPreAdmision_Result> list_Validacion = new List<WCO_ListarHomologacionPreAdmision_Result>();
                                        list_Validacion = list_Homolo.Where(o => o.CodigoHomologado.Trim() == intobj2.Componente.Trim()).ToList();
                                        if (list_Validacion.Count > 0)
                                        {
                                            list_PreOk.Add(intobj2);
                                        }
                                        else
                                        {
                                            list_PreHomoError.Add(intobj2);
                                            observacion += c + "|" + intobj2.Componente + " Error de Homologacion para este Examen ¬ ";
                                            ValidaError += c;
                                        }
                                        //Id = m.InsertarPreAdmision(Obj_PreAdmision);
                                        //observacion += c + "|" + Id + "Correcto ¬ ";
                                    }
                                    else
                                    {
                                        observacion += c + "|" + ValidaDetalle + " ¬ ";
                                        ValidaError += c;
                                    }
                                }
                                catch (FormatException exception)
                                {
                                    string asun = System.DateTime.Now + " | " + "Error en la FormatException ";
                                    observacion += asun + " | " + exception.GetType();
                                }
                                catch (TimeoutException exception)
                                {
                                    string asun = System.DateTime.Now + " | " + "Error en el TimeoutException ";
                                    observacion += asun + " | " + exception.GetType();
                                }
                                catch (Exception exception)
                                {
                                    string asun = System.DateTime.Now + " | " + "Error en los datos";
                                    observacion += asun + " | " + exception.GetType();
                                }
                            }
                            if (ValidaError == 0)
                            {
                                foreach (WCO_ListarPreCarga_Result Obj_PreAdmision in list_PreOk)
                                {
                                    //list_PreOk.Add(Obj_PreAdmision);
                                    Id = m.InsertarPreAdmision(Obj_PreAdmision);
                                    if (Id > 0)
                                    {
                                        observacion += c + "|" + Id + "Correcto ¬ ";
                                    }
                                    else
                                    {
                                        observacion += c + "|" + Id + "Ya existe ¬ ";
                                    }
                                }
                            }
                        }

                    }
                    catch (Exception exception)
                    {
                        string asun = System.DateTime.Now + " | " + "Error en los datos";
                        observacion += asun + " | " + exception.GetType();
                    }
                }
                else
                {
                    observacion += observacion + " Lista vacia";
                }
            }
            else
            {
                observacion += observacion + " Error en el Usuario y el Password";
            }
            //Json(observacion, JsonRequestBehavior.AllowGet
            return Ok(observacion);
        }

        // DELETE api/Consulta/2
        public void Delete(AnulacionPreAdmision PreModel)
        {
            int Id = 0;
            Id = m.AnulacionPreAdmision(PreModel.Cliente, PreModel.Acceso, PreModel.IdOrdenAtencion, PreModel.Linea, PreModel.Mensaje);
            // return Json(Id, JsonRequestBehavior.AllowGet);
            //return Ok(Id);
        }

        #endregion
    }
}
