using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using RoyalSystems.Data;

namespace WebApiServices.Models.Datos
{
    public class ADDAT_TipoPaciente
    {
        public List<WCO_ListarTipoPaciente_Result> ListadoPaginado(WCO_ListarTipoPaciente_Result ObjEntidad)
        {
            using (var context = new BDComercialEntities())
            {
                Nullable<int> iReturnValue = null;
                if (ObjEntidad.TipoPacienteId > 0)
                {
                    iReturnValue = ObjEntidad.TipoPacienteId;
                }
                return context.WCO_ListarTipoPaciente(iReturnValue, ObjEntidad.TipoAdmisionId, ObjEntidad.Codigo, 
                    ObjEntidad.Descripcion, ObjEntidad.Estado).ToList();
            }
            //return null;
        }

        public void Inactivar(WCO_ListarTipoPaciente_Result objBETipoPaciente)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InactivarTipoPaciente");
            Parameter[] prm_Params = new Parameter[3];
            prm_Params[0] = new Parameter("@UsuarioModificacion", objBETipoPaciente.UsuarioModificacion);
            prm_Params[1] = new Parameter("@TipoPacienteId", objBETipoPaciente.TipoPacienteId);
            prm_Params[2] = new Parameter("@IpModificacion", objBETipoPaciente.IpModificacion);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }

        /// <param name="WCO_ListarTipoPaciente_Result"></param>
        /// <returns></returns>
        public int Insertar(WCO_ListarTipoPaciente_Result objBETipoPaciente)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InsertarTipoPaciente");
            Parameter[] prm_Params = new Parameter[7];
            prm_Params[0] = new Parameter("@Codigo", objBETipoPaciente.Codigo);
            prm_Params[1] = new Parameter("@Descripcion", objBETipoPaciente.Descripcion);
            prm_Params[2] = new Parameter("@Estado", objBETipoPaciente.Estado);
            prm_Params[3] = new Parameter("@TipoAdmisionId", objBETipoPaciente.TipoAdmisionId);
            prm_Params[4] = new Parameter("@UsuarioCreacion", objBETipoPaciente.UsuarioCreacion);
            prm_Params[5] = new Parameter("@IpCreacion", objBETipoPaciente.IpCreacion);
            prm_Params[6] = new Parameter("@TipoPacienteId", DbType.Int32);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            return int.Parse(dop_Operacion.GetParameterByName("@TipoPacienteId").Value.ToString());
        }

        public bool ValidarExiste(WCO_ListarTipoPaciente_Result objBETipoPaciente)
        {
            DataOperation dop_DataOperation = new DataOperation("WCO_ExisteTipoPaciente");
            Parameter[] prm_Params = new Parameter[3];
            prm_Params[0] = new Parameter("@TipoPacienteId", objBETipoPaciente.TipoPacienteId);
            prm_Params[1] = new Parameter("@Codigo", objBETipoPaciente.Codigo);
            prm_Params[2] = new Parameter("@flagSalida", DbType.Int32);
            dop_DataOperation.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation);
            if (Convert.ToInt32(dop_DataOperation.GetParameterByName("flagSalida").Value) >= 1)
            {
                return false;
            }
            return true;
        }

        public int Actualizar(WCO_ListarTipoPaciente_Result objBETipoPaciente)
        {
            int valor = 0;
            if (!ValidarExiste(objBETipoPaciente))
            {
                valor = -2;
                return valor;
            }
            DataOperation dop_Operacion = new DataOperation("WCO_ActualizarTipoPaciente");
            Parameter[] prm_Params = new Parameter[7];
            prm_Params[0] = new Parameter("@Codigo", objBETipoPaciente.Codigo);
            prm_Params[1] = new Parameter("@Descripcion", objBETipoPaciente.Descripcion);
            prm_Params[2] = new Parameter("@Estado", objBETipoPaciente.Estado);
            prm_Params[3] = new Parameter("@TipoAdmisionId", objBETipoPaciente.TipoAdmisionId);
            prm_Params[4] = new Parameter("@UsuarioModificacion", objBETipoPaciente.UsuarioModificacion);
            prm_Params[5] = new Parameter("@IpModificacion", objBETipoPaciente.IpModificacion);
            prm_Params[6] = new Parameter("@TipoPacienteId", objBETipoPaciente.TipoPacienteId);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            valor = objBETipoPaciente.TipoPacienteId;
            return valor;
        }
    }
}