using RoyalSystems.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebApiServices.Models.Datos
{
    public class ADDAT_ModeloServicio
    {


        #region Cabecera

        public List<WCO_ListarModeloServicio_Result> ListadoPaginado(WCO_ListarModeloServicio_Result objBEModeloServicio)
        {
            List<WCO_ListarModeloServicio_Result> lst = new List<WCO_ListarModeloServicio_Result>();
            using (var context = new BDComercialEntities())
            {
                Nullable<int> iReturnValue = null;
                if (objBEModeloServicio.ModeloServicioId > 0)
                {
                    iReturnValue = objBEModeloServicio.ModeloServicioId;
                }
                lst = context.WCO_ListarModeloServicio(objBEModeloServicio.UneuNegocioId, iReturnValue, objBEModeloServicio.MosDescripcion, objBEModeloServicio.MosEstado, objBEModeloServicio.TipoOperacionId, objBEModeloServicio.TIPOADMISIONID, objBEModeloServicio.TipoPacienteId).ToList();
            }
            return lst;
        }

        public int Insertar(WCO_ListarModeloServicio_Result objBEModeloServicio)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InsertarModeloServicio");
            Parameter[] prm_Params = new Parameter[10];
            prm_Params[0] = new Parameter("@UneuNegocioId", objBEModeloServicio.UneuNegocioId);
            prm_Params[1] = new Parameter("@MosDescripcion", objBEModeloServicio.MosDescripcion);
            prm_Params[2] = new Parameter("@MosEstado", objBEModeloServicio.MosEstado);
            prm_Params[3] = new Parameter("@TipoOperacionId", objBEModeloServicio.TipoOperacionId);
            prm_Params[4] = new Parameter("@UsuarioCreacion", objBEModeloServicio.UsuarioCreacion);
            prm_Params[5] = new Parameter("@FechaCreacion", objBEModeloServicio.FechaCreacion);
            prm_Params[6] = new Parameter("@IpCreacion", objBEModeloServicio.IpCreacion);
            prm_Params[7] = new Parameter("@MosFactor", objBEModeloServicio.MosFactor);
            prm_Params[8] = new Parameter("@MosAplicaFactor", objBEModeloServicio.MosAplicaFactor);
            prm_Params[9] = new Parameter("@ModeloServicioId", DbType.Int32);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            return int.Parse(dop_Operacion.GetParameterByName("@ModeloServicioId").Value.ToString());
        }

        public int Actualizar(WCO_ListarModeloServicio_Result objBEModeloServicio)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_ActualizarModeloServicio");
            Parameter[] prm_Params = new Parameter[10];
            prm_Params[0] = new Parameter("@UneuNegocioId", objBEModeloServicio.UneuNegocioId);
            prm_Params[1] = new Parameter("@MosDescripcion", objBEModeloServicio.MosDescripcion);
            prm_Params[2] = new Parameter("@MosEstado", objBEModeloServicio.MosEstado);
            prm_Params[3] = new Parameter("@TipoOperacionId", objBEModeloServicio.TipoOperacionId);
            prm_Params[4] = new Parameter("@UsuarioModificacion", objBEModeloServicio.UsuarioCreacion);
            prm_Params[5] = new Parameter("@FechaModificacion", objBEModeloServicio.FechaCreacion);
            prm_Params[6] = new Parameter("@IpModificacion", objBEModeloServicio.IpCreacion);
            prm_Params[7] = new Parameter("@MosFactor", objBEModeloServicio.MosFactor);
            prm_Params[8] = new Parameter("@MosAplicaFactor", objBEModeloServicio.MosAplicaFactor);
            prm_Params[9] = new Parameter("@ModeloServicioId", objBEModeloServicio.ModeloServicioId);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            return objBEModeloServicio.ModeloServicioId;


        }

        public int Inactivar(WCO_ListarModeloServicio_Result objBEModeloServicio)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InactivarModeloServicio");
            Parameter[] prm_Params = new Parameter[3];
            prm_Params[0] = new Parameter("@ModeloServicioId", objBEModeloServicio.ModeloServicioId);
            prm_Params[1] = new Parameter("@UsuarioModificacion", objBEModeloServicio.UsuarioCreacion);
            prm_Params[2] = new Parameter("@IpModificacion", objBEModeloServicio.IpCreacion);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            return objBEModeloServicio.ModeloServicioId;
        }

        public bool ValidarExisteDescModeloServicio(WCO_ListarModeloServicio_Result objBEModeloServicio)
        {
            DataOperation dop_DataOperation = new DataOperation("WCO_EXISTEDescModeloServicio");
            Parameter[] prm_Params = new Parameter[4];
            prm_Params[0] = new Parameter("@UneuNegocioId", objBEModeloServicio.UneuNegocioId);
            prm_Params[1] = new Parameter("@ModeloServicioId", objBEModeloServicio.ModeloServicioId);
            prm_Params[2] = new Parameter("@MosDescripcion", objBEModeloServicio.MosDescripcion);
            prm_Params[3] = new Parameter("@flagSalida", DbType.Int32);
            dop_DataOperation.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation);
            if (Convert.ToInt32(dop_DataOperation.GetParameterByName("@flagSalida").Value) >= 1)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Detalle
        public List<WCO_ListarModServDetalle_Result> ListadoDetalle(WCO_ListarModServDetalle_Result objBEModeloServicio)
        {
            List<WCO_ListarModServDetalle_Result> lst = new List<WCO_ListarModServDetalle_Result>();
            using (var context = new BDComercialEntities())
            {
                Nullable<int> iReturnValue = null;
                if (objBEModeloServicio.ModeloServicioId > 0)
                {
                    iReturnValue = objBEModeloServicio.ModeloServicioId;
                }
                lst = context.WCO_ListarModServDetalle(objBEModeloServicio.UneuNegocioId, iReturnValue, objBEModeloServicio.codigocomponente, objBEModeloServicio.TipoOperacionId, objBEModeloServicio.Descripcion).ToList();
            }
            return lst;
        }

        public void InsertarDetalle(WCO_ListarModServDetalle_Result objBEModServDetalle)
        {

            DataOperation dop_Operacion = new DataOperation("WCO_InsertarModServDetalle");
            Parameter[] prm_Params = new Parameter[5];
            prm_Params[0] = new Parameter("@UneuNegocioId", objBEModServDetalle.UneuNegocioId);
            prm_Params[1] = new Parameter("@ModeloServicioId", objBEModServDetalle.ModeloServicioId);
            prm_Params[2] = new Parameter("@CodigoComponente", objBEModServDetalle.codigocomponente);
            prm_Params[3] = new Parameter("@UsuarioCreacion", objBEModServDetalle.UsuarioCreacion);
            prm_Params[4] = new Parameter("@IpCreacion", objBEModServDetalle.IpCreacion);

            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }

        public void EliminarDetalle(WCO_ListarModServDetalle_Result objBEModServDetalle)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_EliminarModServDetalle");
            Parameter[] prm_Params = new Parameter[3];
            prm_Params[0] = new Parameter("@UneuNegocioId", objBEModServDetalle.UneuNegocioId);
            prm_Params[1] = new Parameter("@ModeloServicioId", objBEModServDetalle.ModeloServicioId);
            prm_Params[2] = new Parameter("@CodigoComponente", objBEModServDetalle.codigocomponente);

            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }
        #endregion
    }
}