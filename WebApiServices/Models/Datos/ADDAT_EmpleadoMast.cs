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

namespace WebApiServices.Models.Datos
{
    public class ADDAT_EmpleadoMast
    {
        public List<WCO_ListarEmpleados_Result> ListarEmpleados(WCO_ListarEmpleados_Result ObjEntidad)
        {
            List<WCO_ListarEmpleados_Result> lst = new List<WCO_ListarEmpleados_Result>();
            using (var context = new BDComercialEntities())
            {
                Nullable<int> UneuNegocioId = null;
                Nullable<int> iReturnValue = null;
                if (ObjEntidad.IdEmpleado > 0)
                {
                    iReturnValue = ObjEntidad.IdEmpleado;
                }
                lst = context.WCO_ListarEmpleados(UneuNegocioId, ObjEntidad.CompaniaSocio, iReturnValue, ObjEntidad.TIPODOCUMENTO, ObjEntidad.Documento, ObjEntidad.NombreCompleto, ObjEntidad.TipoTrabajador, ObjEntidad.Cargo, ObjEntidad.Estado).ToList();
            }
            return lst;
        }

        ///<summary>Actualizar el registro en la tabla EmpleadoMast.</summary>
        ///<param name="obj_pSA_Curriculo">Entidad de Negocio</param>
        ///<remarks><list type="bullet">
        ///<item><CreadoPor>Creado PorJordan Mateo Pizarro</CreadoPor></item>
        ///<item><FecCrea>17/10/2012</FecCrea></item></list></remarks>
        public int Actualizar(WCO_ListarEmpleados_Result BE_pEmpleado)
        {
            int valor = 0;
            bool isExists = false;
            if (!isExists)
            {
                DataOperation dop_Operacion = new DataOperation("WCO_ActualizarEmpleadoMast");
                Parameter[] prm_Params = new Parameter[13];
                prm_Params[0] = new Parameter("@UneuNegocioId", BE_pEmpleado.UneuNegocioId);
                prm_Params[1] = new Parameter("@pEmpleado", BE_pEmpleado.IdEmpleado);
                prm_Params[2] = new Parameter("@pFechaInicioContrato", BE_pEmpleado.FechaInicioContrato);
                prm_Params[3] = new Parameter("@pFechaCese", BE_pEmpleado.FechaCese);
                prm_Params[4] = new Parameter("@pTipoTrabajador", BE_pEmpleado.TipoTrabajador);
                prm_Params[5] = new Parameter("@pCargo", BE_pEmpleado.Cargo);
                prm_Params[6] = new Parameter("@pCorreoInterno", BE_pEmpleado.CorreoInterno);
                prm_Params[7] = new Parameter("@pCompaniaSocio", BE_pEmpleado.CompaniaSocio);
                prm_Params[8] = new Parameter("@pDireccion", BE_pEmpleado.Direccion);
                prm_Params[9] = new Parameter("@pTelefono", BE_pEmpleado.Telefono);
                prm_Params[10] = new Parameter("@pPerUltimoUsuario", BE_pEmpleado.UltimoUsuario);
                prm_Params[11] = new Parameter("@pFechaFinContrato", BE_pEmpleado.FechaFinContrato);
                prm_Params[12] = new Parameter("@pEstado", BE_pEmpleado.Estado);

                dop_Operacion.Parameters.AddRange(prm_Params);
                DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
                valor = BE_pEmpleado.IdEmpleado;
            }
            else
            {
                valor = -1;
            }
            return valor;
        }


        public int Inactivar(WCO_ListarEmpleados_Result BE_pEmpleado)
        {
            int valor = 0;
            bool isExists = false;
            if (!isExists)
            {
                DataOperation dop_Operacion = new DataOperation("WCO_InactivarEmpleado");
                Parameter[] prm_Params = new Parameter[2];
                prm_Params[0] = new Parameter("@idempleado", BE_pEmpleado.IdEmpleado);
                prm_Params[1] = new Parameter("@pUltimoUSuario", BE_pEmpleado.UltimoUsuario);
                dop_Operacion.Parameters.AddRange(prm_Params);
                DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
                valor = BE_pEmpleado.IdEmpleado;

            }
            else
            {
                valor = -1;
            }
            return valor;
        }


    }
}