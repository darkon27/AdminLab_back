using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using RoyalSystems.Data;
using System.IO;
using System.Transactions;
using WebApiServices.Models.Entidades;
using WebApiServices.Models.Request;
using System.Diagnostics;
using Newtonsoft.Json;

namespace WebApiServices.Models.Datos
{
    public class ADDAT_Compania
    {
        public List<WCO_ListarCompaniaMast_Result> ListarCompania(WCO_ListarCompaniaMast_Result ObjEntidad)
        {
            List<WCO_ListarCompaniaMast_Result> lst = new List<WCO_ListarCompaniaMast_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarCompaniaMast(ObjEntidad.CompaniaCodigo, ObjEntidad.DescripcionCorta, ObjEntidad.DocumentoFiscal, ObjEntidad.Estado).ToList();
            }
            return lst;
        }

        public string Insertar(WCO_ListarCompaniaMast_Result objBECompaniaMast)
        {
            int valor = 0;
            var lst = ListarCompania(objBECompaniaMast);
            valor = lst.Count;
            if (valor == 0)
            {
                DataOperation dop_Operacion = new DataOperation("WCO_InsertarCompaniaMast");
                Parameter[] prm_Params = new Parameter[17];
                prm_Params[0] = new Parameter("@Persona", objBECompaniaMast.Persona);
                prm_Params[1] = new Parameter("@DescripcionCorta", objBECompaniaMast.DescripcionCorta);
                prm_Params[2] = new Parameter("@DocumentoFiscal", objBECompaniaMast.DocumentoFiscal);
                prm_Params[3] = new Parameter("@DireccionComun", objBECompaniaMast.DireccionComun);
                prm_Params[4] = new Parameter("@DireccionAdicional", objBECompaniaMast.DireccionAdicional);
                prm_Params[5] = new Parameter("@AfectoIGVFlag", objBECompaniaMast.AfectoIGVFlag);
                prm_Params[6] = new Parameter("@AfectoRetencionIGVFlag", objBECompaniaMast.AfectoRetencionIGVFlag);
                prm_Params[7] = new Parameter("@Telefono1", objBECompaniaMast.Telefono1);
                prm_Params[8] = new Parameter("@Fax1", objBECompaniaMast.Fax1);
                prm_Params[9] = new Parameter("@FechaFundacion", objBECompaniaMast.FechaFundacion);
                prm_Params[10] = new Parameter("@RepresentanteLegal", objBECompaniaMast.RepresentanteLegal);
                prm_Params[11] = new Parameter("@RepresentanteLegalDocumento", objBECompaniaMast.RepresentanteLegalDocumento);
                prm_Params[12] = new Parameter("@Estado", objBECompaniaMast.Estado);
                prm_Params[13] = new Parameter("@UsuarioCreacion", objBECompaniaMast.UsuarioCreacion);
                prm_Params[14] = new Parameter("@CompaniaCodigo", "0");
                prm_Params[15] = new Parameter("@Detraccion", objBECompaniaMast.DetraccionCuentaBancaria);
                prm_Params[16] = new Parameter("@Grupo", objBECompaniaMast.Grupo);
                //prm_Params[17] = new Parameter("@CodigoAnterior", objBECompaniaMast.CodigoAnterior);                
                dop_Operacion.Parameters.AddRange(prm_Params);
                DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
                return dop_Operacion.GetParameterByName("@CompaniaCodigo").Value.ToString();
            }
            else
            {
                return "";
            }
        }

        public string Actualizar(WCO_ListarCompaniaMast_Result objBECompaniaMast)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_ActualizarCompaniaMast");
            Parameter[] prm_Params = new Parameter[17];
            prm_Params[0] = new Parameter("@Persona", objBECompaniaMast.Persona);
            prm_Params[1] = new Parameter("@DescripcionCorta", objBECompaniaMast.DescripcionCorta);
            prm_Params[2] = new Parameter("@DocumentoFiscal", objBECompaniaMast.DocumentoFiscal);
            prm_Params[3] = new Parameter("@DireccionComun", objBECompaniaMast.DireccionComun);
            prm_Params[4] = new Parameter("@DireccionAdicional", objBECompaniaMast.DireccionAdicional);
            prm_Params[5] = new Parameter("@AfectoIGVFlag", objBECompaniaMast.AfectoIGVFlag);
            prm_Params[6] = new Parameter("@AfectoRetencionIGVFlag", objBECompaniaMast.AfectoRetencionIGVFlag);
            prm_Params[7] = new Parameter("@Telefono1", objBECompaniaMast.Telefono1);
            prm_Params[8] = new Parameter("@Fax1", objBECompaniaMast.Fax1);
            prm_Params[9] = new Parameter("@FechaFundacion", objBECompaniaMast.FechaFundacion);
            prm_Params[10] = new Parameter("@RepresentanteLegal", objBECompaniaMast.RepresentanteLegal);
            prm_Params[11] = new Parameter("@RepresentanteLegalDocumento", objBECompaniaMast.RepresentanteLegalDocumento.TrimEnd());
            prm_Params[12] = new Parameter("@Estado", objBECompaniaMast.Estado);
            prm_Params[13] = new Parameter("@UltimoUsuario", objBECompaniaMast.UltimoUsuario);
            prm_Params[14] = new Parameter("@CompaniaCodigo", objBECompaniaMast.CompaniaCodigo);
            prm_Params[15] = new Parameter("@Detraccion", objBECompaniaMast.DetraccionCuentaBancaria);
            prm_Params[16] = new Parameter("@Grupo", objBECompaniaMast.Grupo);
            //prm_Params[17] = new Parameter("@CodigoAnterior", objBECompaniaMast.CodigoAnterior);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            return objBECompaniaMast.CompaniaCodigo;
        }

        public string Inactivar(WCO_ListarCompaniaMast_Result objBECompaniaMast)
        {
            try
            {
                DataOperation dop_Operacion = new DataOperation("WCO_InactivarCompaniaMast");
                Parameter[] prm_Params = new Parameter[4];
                prm_Params[0] = new Parameter("@CompaniaCodigo", objBECompaniaMast.CompaniaCodigo);
                prm_Params[1] = new Parameter("@Persona", objBECompaniaMast.Persona);
                prm_Params[2] = new Parameter("@UltimoUsuario", objBECompaniaMast.UsuarioCreacion);
                prm_Params[3] = new Parameter("@Estado", objBECompaniaMast.Estado);
                dop_Operacion.Parameters.AddRange(prm_Params);
                DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
                return objBECompaniaMast.CompaniaCodigo;
            }
            catch (Exception ex)
            {
                UT_Kerberos.WriteLog(System.DateTime.Now + " | " + "Inactivar|InactivarCompaniaMast = Exception " + JsonConvert.SerializeObject(ex, Newtonsoft.Json.Formatting.Indented));
                return "0";
            }
        }
    }
}