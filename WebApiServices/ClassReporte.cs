using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using WebApiServices.Models;
using WebApiServices.Models.Datos;
using WebApiServices.Models.Entidades;
using WebApiServices.Models.Request;

namespace WebApiServices
{
    public class ClassReporte
    {
        public ViewModalReporte ListadoImpresion(ImpresionRequest ObjDetalle)
        {
            List<WCO_ListarAdmisionServicioConstancia_Result> lstPfdss = new List<WCO_ListarAdmisionServicioConstancia_Result>();
            ViewModalReporte objerror = new ViewModalReporte();
            try
            {
                ADDAT_Admision conex = new ADDAT_Admision();
                //if (ObjDetalle.IdReporte == 1 && ObjDetalle.IdReporte == 2)
                //{
                WCO_ListarAdmisionServicio_Result EntityObj = new WCO_ListarAdmisionServicio_Result();
                EntityObj.IdAdmision = ObjDetalle.IdAdmision;
                EntityObj.NroPeticion = ObjDetalle.NroPeticion;
                lstPfdss = conex.ListadoAdmisionServicioConstancia(EntityObj);
                if (lstPfdss.Count == 0)
                {
                    objerror.CodigoError = 500;
                    objerror.MensajeError = "No se encotraron datos";
                    return objerror;
                }
                else
                {
                    rptView_AdmisionServicioConstancia objRPT = new rptView_AdmisionServicioConstancia();
                    List<rptView_AdmisionServicioConstancia> result = new List<rptView_AdmisionServicioConstancia>();
                    foreach (WCO_ListarAdmisionServicioConstancia_Result objReport in lstPfdss) // Loop through List with foreach.
                    {
                        objRPT = new rptView_AdmisionServicioConstancia();
                        objRPT.IdAdmision = objReport.IdAdmision;
                        objRPT.Linea = Convert.ToInt32(objReport.Linea);
                        objRPT.CodigoComponente = objReport.CodigoComponente;
                        objRPT.NroPeticion = objReport.NroPeticion;
                        objRPT.NombreCompleto = objReport.NombreCompleto;
                        objRPT.Descripcion = objReport.Descripcion;
                        objRPT.Cantidad = Convert.ToInt32(objReport.Cantidad.ToString());
                        objRPT.TipoOperacionID = objReport.TipoOperacionID;
                        objRPT.Persona = Convert.ToInt32(objReport.Persona.ToString());
                        objRPT.IdSede = Convert.ToInt32(objReport.IdSede.ToString());
                        objRPT.TIPOADMISIONID = int.Parse(objReport.TIPOADMISIONID.ToString());
                        objRPT.Sexo = objReport.Sexo;
                        objRPT.edad = Convert.ToInt32(objReport.edad.ToString());
                        objRPT.Telefono = objReport.Telefono;
                        objRPT.MEDICO = objReport.MEDICO;
                        objRPT.CLIENTE = objReport.CLIENTE;
                        objRPT.PROCEDENCIA = objReport.PROCEDENCIA;
                        objRPT.HistoriaClinica = objReport.HistoriaClinica;
                        objRPT.EMPRESA = objReport.EMPRESA;
                        objRPT.Usuario = objReport.Usuario;
                        objRPT.CorreoElectronico = objReport.CorreoElectronico;
                        objRPT.OrdenAtencion = objReport.OrdenAtencion;
                        objRPT.Clave = objReport.Clave;
                        objRPT.Documento = objReport.Documento;
                        objRPT.FechaAdmision = objReport.FechaAdmision;
                        objRPT.NombreEmpresa = objReport.NombreEmpresa;
                        objRPT.OrdenSinapa = objReport.OrdenSinapa;
                        objRPT.TipoPaciente = objReport.TipoPaciente;
                        objRPT.TipoOrden = objReport.TipoOrden;
                        objRPT.TipoAtencion = objReport.TipoAtencion;
                        objRPT.Portal = objReport.Portal;
                        objRPT.redondeo = decimal.Parse(objReport.redondeo.ToString());
                        result.Add(objRPT);
                    }
                    ReportDocument rd = new ReportDocument();
                    byte[] bytes = null;
                    String pdfPath = System.Web.HttpContext.Current.Server.MapPath("~/Reporte/Crystal1.rpt");
                    //  rd.Load(Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Reporte"), "Rpt_Constancia.rpt"));
                    rd.Load(System.Web.HttpContext.Current.Server.MapPath("~/Reporte/Crystal1.rpt"));
                    rd.SetDataSource(result);
                    System.Web.HttpContext.Current.Response.Buffer = false;
                    System.Web.HttpContext.Current.Response.ClearContent();
                    System.Web.HttpContext.Current.Response.ClearHeaders();
                    String BASE = null;
                    try
                    {
                        Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                        stream.Seek(0, SeekOrigin.Begin);
                        var test = BaseDatos.ReadFully(stream);
                        String file = Convert.ToBase64String(test);
                        BASE = file;
                        objerror.CodigoError = 200;
                        objerror.MensajeError = BASE;
                        objerror.ValorByte = test;
                        return objerror;
                    }
                    catch (Exception e)
                    {
                        var st = new StackTrace(e, true);
                        // Get the top stack frame
                        var frame = st.GetFrame(0);
                        // Get the line number from the stack frame
                        var line = frame.GetFileLineNumber();
                        objerror.CodigoError = 500;
                        objerror.MensajeError = "ERROR_GENRACION_rEPORTE   : " + e.Message;
                        objerror.ValorDevolucion = BASE;
                        objerror.LineaError = line;
                        BaseDatos.WriteLog(ObjDetalle.IdReporte + "|" + ObjDetalle.NroPeticion + "|" + e.Message + "|404|GENERACION_REPORTE");
                        return objerror;
                    }
                }
                //}
            }
            catch (Exception e)
            {
                BaseDatos.WriteLog(ObjDetalle.IdReporte + "|" + ObjDetalle.NroPeticion + "|" + e.Message + "|404|GENERACION_REPORTE");
                return objerror;
            }
            return objerror;
        }
    }
}