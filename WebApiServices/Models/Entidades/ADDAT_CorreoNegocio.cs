using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiServices.Models.Entidades
{
    public class ADDAT_CorreoNegocio
    {
        public List<WCO_ListarCorreoNegocio_Result> ListadoPaginado(WCO_ListarCorreoNegocio_Result ObjPersona)
        {
            List<WCO_ListarCorreoNegocio_Result> lst = new List<WCO_ListarCorreoNegocio_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarCorreoNegocio(ObjPersona.UneuNegocioId, ObjPersona.IdCorreo,
                    ObjPersona.Correo, ObjPersona.Estado).ToList();
            }
            return lst;
        }
        public List<WCO_ListarCorreoNegocioDetalle_Result> ListadoPaginadoDetalle(WCO_ListarCorreoNegocio_Result ObjPersona)
        {
            List<WCO_ListarCorreoNegocioDetalle_Result> lst = new List<WCO_ListarCorreoNegocioDetalle_Result>();
            using (var context = new BDComercialEntities())
            {
                Nullable<int> IdCorreo = null;
                Nullable<int> IdCorreoDet = null;
                if (ObjPersona.Estado != -1)
                {
                    IdCorreoDet = 1;
                }
                if (ObjPersona.IdCorreo != -1)
                {
                    IdCorreo = 1;
                }
                lst = context.WCO_ListarCorreoNegocioDetalle(IdCorreo, IdCorreoDet,
                    ObjPersona.UneuNegocioId).ToList();
            }
            return lst;
        }
    }
}