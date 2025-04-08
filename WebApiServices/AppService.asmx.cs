using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;
using WebApiServices.Models;
using WebApiServices.Models.Datos;
using WebApiServices.Models.Entidades;

namespace WebApiServices
{
    /// <summary>
    /// Descripción breve de AppService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class AppService : System.Web.Services.WebService
    {
        Metodos m = new Metodos();
        ADDAT_Admision adm = new ADDAT_Admision();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hola a todos";
        }

        [WebMethod]
        public ADBE_Admision GenerarAdmision(List<ADBE_PreAdmision> listaServicio)
        {
            ADBE_Admision RestPuestaAdmision = new ADBE_Admision();
            try
            {
                RestPuestaAdmision = m.GenerarAdmision(listaServicio);
            }
            catch (Exception ex)
            {
                RestPuestaAdmision.Comentario = "0|" + ex.StackTrace;
            }
            return RestPuestaAdmision;
        }

        [WebMethod]
        public ADBE_Admision GenerarAdmisionReferencia(List<ADBE_PreAdmision> listaServicio)
        {
            ADBE_Admision RestPuestaAdmision = new ADBE_Admision();
            try
            {
                RestPuestaAdmision = m.GenerarAdmision(listaServicio);
            }
            catch (Exception ex)
            {
                RestPuestaAdmision.Comentario = "0|" + ex.StackTrace;
            }
            return RestPuestaAdmision;
        }

        //private void InsertarTransaccion(ADBE_Admision ObjAdmsion)
        //{
        //    CABE_Transaccion obj_pTransaccion = new CABE_Transaccion();
        //    obj_pTransaccion.PK.Admision = ObjAdmsion;
        //    obj_pTransaccion.UsuarioCreacion = ObjAdmsion.UsuarioCreacion;
        //    obj_pTransaccion.IdClienteFacturacion = ObjAdmsion.PK.TipoOperacion.Pk.Persona.PK.IdPersona;
        //    obj_pTransaccion.NombreCliente = ObjAdmsion.PK.TipoOperacion.Pk.Persona.NombreCompleto;
        //    obj_pTransaccion.TipoComprobante = "FA";
        //    obj_pTransaccion.Moneda = "LO";
        //    obj_pTransaccion.MontoTotal = Convert.ToDecimal("0,00");
        //    BaseDatos.InsertarTransaccion(obj_pTransaccion);
        //}

        [WebMethod]
        public List<WCO_ListarAdmisionServicioConstancia_Result> ListadoAdmisionConstancia(int valor,  int IdAdmision,string NroPeticion)
        {           
            List<WCO_ListarAdmisionServicioConstancia_Result> lst = new List<WCO_ListarAdmisionServicioConstancia_Result>();
            try
            {
                if (valor == 1)
                {
                    WCO_ListarAdmisionServicio_Result ObjDetalle = new WCO_ListarAdmisionServicio_Result();
                    ObjDetalle.IdAdmision = IdAdmision;
                    ObjDetalle.NroPeticion = NroPeticion;
                    lst = adm.ListadoAdmisionServicioConstancia(ObjDetalle);
                }
            }
            catch
            {

            }
            return lst;
        }

        [WebMethod]
        public DataTable ConsultaAdmisionConstancia(int valor, int IdAdmision, string NroPeticion)
        {
            DataTable ds_Result = null;
            try
            {
                if (valor == 1 || valor == 2)
                {
                    ADBE_AdmisionServicio ObjDetalle = new ADBE_AdmisionServicio();
                    ObjDetalle.PK.Admision.PK.IdAdmision = IdAdmision;
                    ObjDetalle.PK.Admision.NroPeticion = NroPeticion;
                    ds_Result = adm.ListadoConstancia(ObjDetalle);
                }
            }
            catch
            {    
                return ds_Result;
            }
            return ds_Result;
        }
    }
}
