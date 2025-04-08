using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using RoyalSystems.Data;
using System.IO;
using System.Transactions;
using System.Web.Services;
using WebApiServices.Models;
using WebApiServices.Models.Entidades;

namespace WebApiServices
{
    /// <summary>
    /// Descripción breve de SoaService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class SoaService : System.Web.Services.WebService
    {
        WebSanna.SoaService Ws = new WebSanna.SoaService();

        [WebMethod]
        public DataTable ConsultaOAFerrer(string OrdenAtencion, int TipoOrden, string Sucursal, string NombreCompleto, string Documento)
        {
            DataTable ds_Result = null;          
            //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            ds_Result = Ws.ConsultaOAFerrer(OrdenAtencion, TipoOrden, Sucursal, NombreCompleto, Documento);
            if (ds_Result == null)
            {
                return null;
            }
            return ds_Result;
        }

        [WebMethod]
        public DataTable ConsultaOASanBorja(string OrdenAtencion, int TipoOrden, string Unidad)
        {
            DataTable ds_Result = null;
           //DataTable ds_Result = Ws.ConsultaOASanBorja("0002155798", 9, "CSB");
            ds_Result = Ws.ConsultaOASanBorja(OrdenAtencion, TipoOrden, Unidad);
            if (ds_Result == null || ds_Result.Rows.Count == 0)
            {
                return null;
            }
            return ds_Result;
        }

        [WebMethod]
        public DataTable ConsultaOAGolf(string OrdenAtencion, int TipoOrden, string Unidad)
        {
            DataTable ds_Result = null;
            ds_Result = Ws.ConsultaOAGolf(OrdenAtencion, TipoOrden, Unidad);
            if (ds_Result == null || ds_Result.Rows.Count == 0)
            {
                return null;
            }
            return ds_Result;
        }

        [WebMethod]
        public DataTable ConsultaOAAliada(string OrdenAtencion, int TipoOrden, string Unidad)
        {
            DataTable ds_Result = null;
            ds_Result = Ws.ConsultaOAAliada(OrdenAtencion, TipoOrden, Unidad);
            if (ds_Result == null || ds_Result.Rows.Count == 0)
            {
                return null;
            }
            return ds_Result;
        }
    }
}
