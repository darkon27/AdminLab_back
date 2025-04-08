using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using RoyalSystems.Data;
using WebApiServices.Models.Request;
using WebApiServices.Models.Entidades;


namespace WebApiServices.Models.Datos
{
    public class CODAT_Componente
    {
        public List<WCO_ListarComponente_Result> ListadoPaginado(WCO_ListarComponente_Result ObjEntidad)
        {
            List<WCO_ListarComponente_Result> lst = new List<WCO_ListarComponente_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarComponente(ObjEntidad.IdClasificacion, ObjEntidad.CodigoComponente, ObjEntidad.Descripcion, ObjEntidad.Estado,
                    ObjEntidad.ModeloServicioId, ObjEntidad.TipoOperacionID, ObjEntidad.TipoPacienteId, ObjEntidad.ClasificadorMovimiento).ToList();
            }
            return lst;
        }

        public List<WCO_ListarComponenteMaestro_Result> ListadoPaginadoMaestro(WCO_ListarComponenteMaestro_Result ObjEntidad)
        {
            List<WCO_ListarComponenteMaestro_Result> lst = new List<WCO_ListarComponenteMaestro_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarComponenteMaestro(ObjEntidad.IdClasificacion, ObjEntidad.CodigoComponente, ObjEntidad.Descripcion, ObjEntidad.Estado,
                    ObjEntidad.ClasificadorMovimiento).ToList();
            }
            return lst;
        }

        //public List<WCO_TraerXComponente_Result> WCO_TraerXComponente(WCO_ListarComponente_Result ObjEntidad)
        //{
        //    List<WCO_TraerXComponente_Result> lst = new List<WCO_TraerXComponente_Result>();
        //    using (var context = new BDComercialEntities())
        //    {
        //        lst = context.WCO_TraerXComponente(ObjEntidad.CodigoComponente).ToList();
        //    }
        //    return lst;
        //}

        public string Insertar(WCO_ListarComponenteMaestro_Result objBEComponente)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InsertarComponente");
            Parameter[] prm_Params = new Parameter[25];
            prm_Params[0] = new Parameter("@CodigoComponente", objBEComponente.CodigoComponente);
            prm_Params[1] = new Parameter("@IdClasificacion", objBEComponente.IdClasificacion);
            prm_Params[2] = new Parameter("@IdGrupoCaracteristica", 0);
            prm_Params[3] = new Parameter("@Nombre", objBEComponente.Abreviatura);
            prm_Params[4] = new Parameter("@Descripcion", objBEComponente.Descripcion);
            prm_Params[5] = new Parameter("@Observacion", objBEComponente.Observacion);
            prm_Params[6] = new Parameter("@Compania", objBEComponente.Compania);
            prm_Params[7] = new Parameter("@ClasificadorMovimiento", objBEComponente.ClasificadorMovimiento);
            prm_Params[8] = new Parameter("@ConceptoGasto", objBEComponente.ConceptoGasto);
            prm_Params[9] = new Parameter("@IdRegimenVenta", objBEComponente.IdRegimenVenta);
            prm_Params[10] = new Parameter("@CentroCosto", objBEComponente.CentroCosto);
            prm_Params[11] = new Parameter("@IdArea", objBEComponente.IdArea);
            prm_Params[12] = new Parameter("@IndicadorMultiple", objBEComponente.IndicadorMultiple);
            prm_Params[13] = new Parameter("@NumeroMuestra", objBEComponente.NumeroMuestra);
            prm_Params[14] = new Parameter("@TiempoMuestra", objBEComponente.TiempoMuestra);
            prm_Params[15] = new Parameter("@Estado", objBEComponente.Estado);
            prm_Params[16] = new Parameter("@FechaCreacion", objBEComponente.FechaCreacion);
            prm_Params[17] = new Parameter("@UsuarioCreacion", objBEComponente.UsuarioCreacion);
            prm_Params[18] = new Parameter("@IndicadorImpuesto", objBEComponente.IndicadorImpuesto);
            prm_Params[19] = new Parameter("@IndicadorReemplazo", objBEComponente.IndicadorReemplazo);
            prm_Params[20] = new Parameter("@IdUnidadMedida", objBEComponente.IdUnidadMedida);
            prm_Params[21] = new Parameter("@CuentaVentaComercial", objBEComponente.CuentaVentaComercial);
            prm_Params[22] = new Parameter("@CantidadKit", objBEComponente.CantidadKit);
            prm_Params[23] = new Parameter("@TipoComponente", objBEComponente.TipoComponente);
            prm_Params[24] = new Parameter("@Sexo", objBEComponente.Sexo);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);

            return objBEComponente.CodigoComponente;
        }

        ///<summary>Actualizar el registro en la tabla PERSONAMAST.</summary>
        ///<param name="obj_pSA_Curriculo">Entidad de Negocio</param>
        ///<remarks><list type="bullet">
        ///<item><CreadoPor>Creado PorJordan Mateo Pizarro</CreadoPor></item>
        ///<item><FecCrea>17/10/2012</FecCrea></item></list></remarks>
        public string Actualizar(WCO_ListarComponenteMaestro_Result objBEComponente)
        {

            DataOperation dop_Operacion = new DataOperation("WCO_ActualizarComponente");
            Parameter[] prm_Params = new Parameter[25];
            prm_Params[0] = new Parameter("@CodigoComponente", objBEComponente.CodigoComponente);
            prm_Params[1] = new Parameter("@IdClasificacion", objBEComponente.IdClasificacion);
            prm_Params[2] = new Parameter("@IdGrupoCaracteristica", 0);
            prm_Params[3] = new Parameter("@Nombre", objBEComponente.Abreviatura);
            prm_Params[4] = new Parameter("@Descripcion", objBEComponente.Descripcion);
            prm_Params[5] = new Parameter("@Observacion", objBEComponente.Observacion);
            prm_Params[6] = new Parameter("@Compania", objBEComponente.Compania);
            prm_Params[7] = new Parameter("@ClasificadorMovimiento", objBEComponente.ClasificadorMovimiento);
            prm_Params[8] = new Parameter("@ConceptoGasto", objBEComponente.ConceptoGasto);
            prm_Params[9] = new Parameter("@IdRegimenVenta", objBEComponente.IdRegimenVenta);
            prm_Params[10] = new Parameter("@CentroCosto", objBEComponente.CentroCosto);
            prm_Params[11] = new Parameter("@IdArea", objBEComponente.IdArea);
            prm_Params[12] = new Parameter("@IndicadorMultiple", objBEComponente.IndicadorMultiple);
            prm_Params[13] = new Parameter("@NumeroMuestra", objBEComponente.NumeroMuestra);
            prm_Params[14] = new Parameter("@TiempoMuestra", objBEComponente.TiempoMuestra);
            prm_Params[15] = new Parameter("@Estado", objBEComponente.Estado);
            prm_Params[16] = new Parameter("@FechaModificacion", objBEComponente.FechaModificacion);
            prm_Params[17] = new Parameter("@UsuarioModificacion", objBEComponente.UsuarioModificacion);
            prm_Params[18] = new Parameter("@IndicadorImpuesto", objBEComponente.IndicadorImpuesto);
            prm_Params[19] = new Parameter("@IndicadorReemplazo", objBEComponente.IndicadorReemplazo);
            prm_Params[20] = new Parameter("@IdUnidadMedida", objBEComponente.IdUnidadMedida);
            prm_Params[21] = new Parameter("@CuentaVentaComercial", objBEComponente.CuentaVentaComercial);
            prm_Params[22] = new Parameter("@CantidadKit", objBEComponente.CantidadKit);
            prm_Params[23] = new Parameter("@TipoComponente", objBEComponente.TipoComponente);
            prm_Params[24] = new Parameter("@Sexo", objBEComponente.Sexo);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            return objBEComponente.CodigoComponente;
        }

        ///<summary>Actualizar el registro en la tabla PERSONAMAST.</summary>
        ///<param name="obj_pSA_Curriculo">Entidad de Negocio</param>
        ///<remarks><list type="bullet">
        ///<item><CreadoPor>Creado PorJordan Mateo Pizarro</CreadoPor></item>
        ///<item><FecCrea>17/10/2012</FecCrea></item></list></remarks>
        public string Inactivar(WCO_ListarComponenteMaestro_Result objBEComponente)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InactivarComponente");
            Parameter[] prm_Params = new Parameter[4];
            prm_Params[0] = new Parameter("@Estado", objBEComponente.Estado);
            prm_Params[1] = new Parameter("@FechaModificacion", objBEComponente.FechaModificacion);
            prm_Params[2] = new Parameter("@UsuarioModificacion", objBEComponente.UsuarioModificacion);
            prm_Params[3] = new Parameter("@CodigoComponente", objBEComponente.CodigoComponente);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            return objBEComponente.CodigoComponente;
        }

        public bool ValidarExiste(WCO_ListarComponenteMaestro_Result objBEComponente)
        {
            DataOperation dop_DataOperation = new DataOperation("WCO_ExisteComponente");
            Parameter[] prm_Params = new Parameter[3];
            prm_Params[0] = new Parameter("@CodigoComponente", objBEComponente.CodigoComponente);
            prm_Params[1] = new Parameter("@Descripcion", objBEComponente.Descripcion);
            prm_Params[2] = new Parameter("@flagSalida", DbType.Int32);
            dop_DataOperation.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation);
            if (Convert.ToInt32(dop_DataOperation.GetParameterByName("flagSalida").Value) >= 1)
            {
                return false;
            }
            return true;
        }

        public static bool ValidarExisteCodigo(WCO_ListarComponenteMaestro_Result objBEComponente)
        {
            DataOperation dop_DataOperation = new DataOperation("WCO_ExisteComponenteCodigo");
            Parameter[] prm_Params = new Parameter[2];
            prm_Params[0] = new Parameter("@CodigoComponente", objBEComponente.CodigoComponente);
            prm_Params[1] = new Parameter("@flagSalida", DbType.Int32);
            dop_DataOperation.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation);
            if (Convert.ToInt32(dop_DataOperation.GetParameterByName("flagSalida").Value) >= 1)
            {
                return false;
            }
            return true;
        }

        public List<WCO_ListarComponentePerfil_Result> ListadoComponentePerfil(WCO_ListarComponentePerfil_Result ObjEntidad)
        {
            List<WCO_ListarComponentePerfil_Result> lst = new List<WCO_ListarComponentePerfil_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarComponentePerfil(ObjEntidad.UneuNegocioId, ObjEntidad.CodigoComponente, ObjEntidad.CodigoHomologado, ObjEntidad.Estado).ToList();
            }
            return lst;
        }

        public int InsertarComponentePerfil(WCO_ListarComponentePerfil_Result objBEComponentePerfil)
        {
            int valo = 0;
            try
            {
                DataOperation dop_Operacion = new DataOperation("WCO_InsertarComponentePerfil");
                Parameter[] prm_Params = new Parameter[6];
                prm_Params[0] = new Parameter("@UneuNegocioId", objBEComponentePerfil.UneuNegocioId);
                prm_Params[1] = new Parameter("@CodigoHomologado", objBEComponentePerfil.CodigoHomologado);
                prm_Params[2] = new Parameter("@Estado", objBEComponentePerfil.Estado);
                prm_Params[3] = new Parameter("@UsuarioCreacion", objBEComponentePerfil.UsuarioCreacion);
                prm_Params[4] = new Parameter("@IpCreacion", objBEComponentePerfil.IpCreacion);
                prm_Params[5] = new Parameter("@CodigoComponente", objBEComponentePerfil.CodigoComponente);
                dop_Operacion.Parameters.AddRange(prm_Params);
                DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
                valo = 1;
            }
            catch (Exception ex)
            {

            }
            return valo;
        }

        public int InactivarComponentePerfil(WCO_ListarComponentePerfil_Result objBEComponentePerfil)
        {
            int valo = 0;
            try
            {

                DataOperation dop_Operacion = new DataOperation("WCO_InactivarComponentePerfil");
                Parameter[] prm_Params = new Parameter[4];
                prm_Params[0] = new Parameter("@CodigoHomologado", objBEComponentePerfil.CodigoHomologado);
                prm_Params[1] = new Parameter("@UsuarioModificacion", objBEComponentePerfil.UsuarioCreacion);
                prm_Params[2] = new Parameter("@IpModificacion", objBEComponentePerfil.IpCreacion);
                prm_Params[3] = new Parameter("@CodigoComponente", objBEComponentePerfil.CodigoComponente);
                dop_Operacion.Parameters.AddRange(prm_Params);
                DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
                valo = 1;
            }
            catch (Exception ex)
            {

            }
            return valo;
        }

        public List<WCO_ListarModeloServicio_Result> ListarModeloServicio(WCO_ListarModeloServicio_Result ObjEntidad)
        {
            List<WCO_ListarModeloServicio_Result> lst = new List<WCO_ListarModeloServicio_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarModeloServicio(ObjEntidad.UneuNegocioId, ObjEntidad.ModeloServicioId, ObjEntidad.MosDescripcion,
                    ObjEntidad.MosEstado, ObjEntidad.TipoOperacionId, ObjEntidad.TIPOADMISIONID, ObjEntidad.TipoPacienteId).ToList();
            }
            return lst;
        }

        public List<WCO_ListarHomologacion_Result> ListadoComponenteHomologacion(WCO_ListarHomologacion_Result ObjEntidad)
        {
            List<WCO_ListarHomologacion_Result> lst = new List<WCO_ListarHomologacion_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarHomologacion(ObjEntidad.UneuNegocioId, ObjEntidad.CodigoComponente, ObjEntidad.CodigoHomologado, ObjEntidad.Estado, ObjEntidad.IdEmpresa).ToList();
            }
            return lst;
        }

        public int InsertarHomologacion(WCO_ListarHomologacion_Result objBEHomologacion)
        {
            int valo = 0;
            try
            {
                DataOperation dop_Operacion = new DataOperation("WCO_InsertarHomologacion");
                Parameter[] prm_Params = new Parameter[7];
                prm_Params[0] = new Parameter("@UneuNegocioId", objBEHomologacion.UneuNegocioId);
                prm_Params[1] = new Parameter("@CodigoHomologado", objBEHomologacion.CodigoHomologado);
                prm_Params[2] = new Parameter("@Estado", objBEHomologacion.Estado);
                prm_Params[3] = new Parameter("@IdEmpresa", objBEHomologacion.IdEmpresa);
                prm_Params[4] = new Parameter("@UsuarioCreacion", objBEHomologacion.UsuarioCreacion);
                prm_Params[5] = new Parameter("@IpCreacion", objBEHomologacion.IpCreacion);
                prm_Params[6] = new Parameter("@CodigoComponente", objBEHomologacion.CodigoComponente);
                dop_Operacion.Parameters.AddRange(prm_Params);
                DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);

                valo = 1;
            }
            catch (Exception ex)
            {

            }
            return valo;
        }

        public int InactivarHomologacion(WCO_ListarHomologacion_Result objBEHomologacion)
        {
            int valo = 0;
            try
            {
                DataOperation dop_Operacion = new DataOperation("WCO_InactivarHomologacion");
                Parameter[] prm_Params = new Parameter[5];
                prm_Params[0] = new Parameter("@CodigoHomologado", objBEHomologacion.CodigoHomologado);
                prm_Params[1] = new Parameter("@UsuarioModificacion", objBEHomologacion.UsuarioCreacion);
                prm_Params[2] = new Parameter("@IpModificacion", objBEHomologacion.IpCreacion);
                prm_Params[3] = new Parameter("@CodigoComponente", objBEHomologacion.CodigoComponente);
                prm_Params[4] = new Parameter("@IdEmpresa", objBEHomologacion.IdEmpresa);
                dop_Operacion.Parameters.AddRange(prm_Params);
                DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);

                valo = 1;
            }
            catch (Exception ex)
            {

            }
            return valo;
        }


        public List<WCO_ListarMuestra_Result> ListarMuestra(WCO_ListarMuestra_Result ObjEntidad)
        {
            List<WCO_ListarMuestra_Result> lst = new List<WCO_ListarMuestra_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarMuestra(ObjEntidad.Nombre, ObjEntidad.IdMuestra, ObjEntidad.Estado).ToList();
            }
            return lst;
        }


        public int InsertarMuestra(WCO_ListarMuestra_Result objBEMuestra)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InsertarMuestra");
            Parameter[] prm_Params = new Parameter[9];
            prm_Params[0] = new Parameter("@Nombre", objBEMuestra.Nombre);
            prm_Params[1] = new Parameter("@Observacion", objBEMuestra.Observacion);
            prm_Params[2] = new Parameter("@Cantidad", objBEMuestra.Cantidad);
            prm_Params[3] = new Parameter("@Estado", objBEMuestra.Estado);
            prm_Params[4] = new Parameter("@IpCreacion", objBEMuestra.IpCreacion);
            prm_Params[5] = new Parameter("@UsuarioCreacion", objBEMuestra.UsuarioCreacion);
            prm_Params[6] = new Parameter("@abreviatura", objBEMuestra.abreviatura);
            prm_Params[7] = new Parameter("@IdMuestra", DbType.Int32);
            prm_Params[8] = new Parameter("@FlgTipoEntrada", objBEMuestra.FlgTipoEntrada);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            return int.Parse(dop_Operacion.GetParameterByName("@IdMuestra").Value.ToString());
        }

        public int InactivarMuestra(WCO_ListarMuestra_Result objBEMuestra)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InactivarMuestra");
            Parameter[] prm_Params = new Parameter[4];
            prm_Params[0] = new Parameter("@IdMuestra", objBEMuestra.IdMuestra);
            prm_Params[1] = new Parameter("@UsuarioModificacion", objBEMuestra.UsuarioModificacion);
            prm_Params[2] = new Parameter("@IpModificacion", objBEMuestra.IpModificacion);
            prm_Params[3] = new Parameter("@Estado", objBEMuestra.Estado);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            return objBEMuestra.IdMuestra;
        }

        public List<WCO_ListarComponenteMuestra_Result> ListarComponenteMuestra(WCO_ListarComponenteMuestra_Result ObjEntidad)
        {
            List<WCO_ListarComponenteMuestra_Result> lst = new List<WCO_ListarComponenteMuestra_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarComponenteMuestra(ObjEntidad.Componente, ObjEntidad.IdMuestra, ObjEntidad.Nombre, ObjEntidad.Abreviatura, ObjEntidad.Cantidad).ToList();
            }
            return lst;
        }

        public string EliminarComponenteMuestra(WCO_ListarComponenteMuestra_Result pBEComponenteMuestra)
        {
            DataOperation dop_DataOperation = new DataOperation("WCO_EliminarComponenteMuestra");
            Parameter[] prm_Params = new Parameter[2];
            prm_Params[0] = new Parameter("@CodigoComponente", pBEComponenteMuestra.Componente);
            prm_Params[1] = new Parameter("@IdMuestra", pBEComponenteMuestra.IdMuestra);
            dop_DataOperation.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation);
            return pBEComponenteMuestra.IdMuestra.ToString();
        }

        public string InsertarComponenteMuestra(WCO_ListarComponenteMuestra_Result pBEComponenteMuestra)
        {
            DataOperation dop_DataOperation = new DataOperation("WCO_InsertarComponenteMuestra");
            Parameter[] prm_Params = new Parameter[4];
            prm_Params[0] = new Parameter("@CodigoComponente", pBEComponenteMuestra.Componente);
            prm_Params[1] = new Parameter("@IdMuestra", pBEComponenteMuestra.IdMuestra);
            prm_Params[2] = new Parameter("@Cantidad", pBEComponenteMuestra.Cantidad);
            prm_Params[3] = new Parameter("@Abreviatura", pBEComponenteMuestra.Abreviatura);
            dop_DataOperation.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation);
            return pBEComponenteMuestra.IdMuestra.ToString();
        }

        public string ActualizarComponenteMuestra(WCO_ListarComponenteMuestra_Result pBEComponenteMuestra)
        {
            DataOperation dop_DataOperation = new DataOperation("WCO_ActualizarComponenteMuestra");

            Parameter[] prm_Params = new Parameter[4];
            prm_Params[0] = new Parameter("@CodigoComponente", pBEComponenteMuestra.Componente);
            prm_Params[1] = new Parameter("@IdMuestra", pBEComponenteMuestra.IdMuestra);
            prm_Params[2] = new Parameter("@Cantidad", pBEComponenteMuestra.Cantidad);
            prm_Params[3] = new Parameter("@Abreviatura", pBEComponenteMuestra.Abreviatura);
            dop_DataOperation.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation);
            return pBEComponenteMuestra.IdMuestra.ToString();
        }



        public List<WCO_ListarClasiComponente_Result> ListarClasificadorComponente(WCO_ListarClasiComponente_Result ObjEntidad)
        {
            List<WCO_ListarClasiComponente_Result> lst = new List<WCO_ListarClasiComponente_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarClasiComponente(ObjEntidad.IdClasificacion, ObjEntidad.IdClasificacionPadre, ObjEntidad.Nombre, 0, 0, ObjEntidad.Estado).ToList();
            }
            return lst;
        }
    }
}