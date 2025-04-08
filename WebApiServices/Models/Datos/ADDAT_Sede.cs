using RoyalSystems.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApiServices.Models.Datos
{
    public class ADDAT_Sede
    {
        #region SedeHistoria
        public List<WCO_TraerXIdSede_Result> ListadoSedeHistoria(WCO_TraerXIdSede_Result ObjSede)
        {
            List<WCO_TraerXIdSede_Result> lst = new List<WCO_TraerXIdSede_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_TraerXIdSede(ObjSede.IdSede, ObjSede.Persona).ToList();
            }
            return lst;
        }
        #endregion

        #region Sede
        public List<WCO_ListarSede_Result> ListadoPaginado(WCO_ListarSede_Result ObjSede)
        {
            List<WCO_ListarSede_Result> lst = new List<WCO_ListarSede_Result>();
            using (var context = new BDComercialEntities())
            {
                Nullable<int> iReturnValue = null;
                if (ObjSede.IdSede > 0)
                {
                    iReturnValue = ObjSede.IdSede;
                }
                lst = context.WCO_ListarSede(ObjSede.SedDescripcion, iReturnValue, ObjSede.IdEmpresa, ObjSede.SedEstado).ToList();
            }
            return lst;
        }



        public int Insertar(WCO_ListarSede_Result objBESede)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InsertarSede");
            Parameter[] prm_Params = new Parameter[20];
            prm_Params[0] = new Parameter("@SedCodigo", objBESede.SedCodigo);
            prm_Params[1] = new Parameter("@IdEmpresa", objBESede.IdEmpresa);
            prm_Params[2] = new Parameter("@CuentaContableDefecto", "");
            prm_Params[3] = new Parameter("@DescripcionLocal", objBESede.SedDescripcion);
            prm_Params[4] = new Parameter("@Estado", objBESede.SedEstado);
            prm_Params[5] = new Parameter("@Final", objBESede.Final);
            prm_Params[6] = new Parameter("@Val_Ini", objBESede.Val_Ini);
            prm_Params[7] = new Parameter("@SedIp", objBESede.SedIp);
            prm_Params[8] = new Parameter("@SedeGrupo", "");
            prm_Params[9] = new Parameter("@UsuarioCreacion", objBESede.UsuarioCreacion);
            prm_Params[10] = new Parameter("@Inicial", objBESede.Inicial);
            prm_Params[11] = new Parameter("@FlatCodigo", objBESede.FlatCodigo);
            prm_Params[12] = new Parameter("@Direccion", objBESede.Direccion);
            prm_Params[13] = new Parameter("@FlatImpresion", objBESede.FlatImpresion);
            prm_Params[14] = new Parameter("@TipoAtencion", objBESede.TipoAtencion);
            prm_Params[15] = new Parameter("@ClasificadorMovimiento", objBESede.ClasificadorMovimiento);
            prm_Params[16] = new Parameter("@TipoPaciente", objBESede.TipoPaciente);
            prm_Params[17] = new Parameter("@Ruta", objBESede.Ruta);
            prm_Params[18] = new Parameter("@IdSede", DbType.Int32);
            prm_Params[19] = new Parameter("@TipoOrden", objBESede.TipoOrden);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            return int.Parse(dop_Operacion.GetParameterByName("@IdSede").Value.ToString());
        }

        public int Actualizar(WCO_ListarSede_Result objBESede)
        {
            int valor = 0;
            if (!ValidarExisteDescripcion(objBESede))
            {
                valor = -2;
                return valor;
            }
            if (!ValidarExisteCodigo(objBESede))
            {
                valor = -3;
                return valor;
            }
            DataOperation dop_Operacion = new DataOperation("WCO_ActualizarSede");
            Parameter[] prm_Params = new Parameter[20];
            prm_Params[0] = new Parameter("@SedCodigo", objBESede.SedCodigo);
            prm_Params[1] = new Parameter("@IdEmpresa", objBESede.IdEmpresa);
            prm_Params[2] = new Parameter("@CuentaContableDefecto", "");
            prm_Params[3] = new Parameter("@DescripcionLocal", objBESede.SedDescripcion);
            prm_Params[4] = new Parameter("@Estado", objBESede.SedEstado);
            prm_Params[5] = new Parameter("@Final", objBESede.Final);
            prm_Params[6] = new Parameter("@Val_Ini", objBESede.Val_Ini);
            prm_Params[7] = new Parameter("@SedIp", objBESede.SedIp);
            prm_Params[8] = new Parameter("@SucursalGrupo", "");
            prm_Params[9] = new Parameter("@UsuarioModificacion", objBESede.UsuarioModificacion);
            prm_Params[10] = new Parameter("@Inicial", objBESede.Inicial);
            prm_Params[11] = new Parameter("@FlatCodigo", objBESede.FlatCodigo);
            prm_Params[12] = new Parameter("@Direccion", objBESede.Direccion);
            prm_Params[13] = new Parameter("@FlatImpresion", objBESede.FlatImpresion);
            prm_Params[14] = new Parameter("@TipoAtencion", objBESede.TipoAtencion);
            prm_Params[15] = new Parameter("@ClasificadorMovimiento", objBESede.ClasificadorMovimiento);
            prm_Params[16] = new Parameter("@TipoPaciente", objBESede.TipoPaciente);
            prm_Params[17] = new Parameter("@Ruta", objBESede.Ruta);
            prm_Params[18] = new Parameter("@IdSede", objBESede.IdSede);
            prm_Params[19] = new Parameter("@TipoOrden", objBESede.TipoOrden);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            valor = objBESede.IdSede;
            return valor;
        }

        public void Inactivar(WCO_ListarSede_Result objBESede)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InactivarSede");
            Parameter[] prm_Params = new Parameter[3];
            prm_Params[0] = new Parameter("@IdSede", objBESede.IdSede);
            prm_Params[1] = new Parameter("@Estado", objBESede.SedEstado);
            prm_Params[2] = new Parameter("@UsuarioModificacion", objBESede.UsuarioModificacion);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }

        public static bool ValidarExisteDescripcion(WCO_ListarSede_Result objBESede)
        {
            DataOperation dop_DataOperation = new DataOperation("WCO_ExisteSede");
            Parameter[] prm_Params = new Parameter[6];
            prm_Params[0] = new Parameter("@IdSede", objBESede.IdSede);
            prm_Params[1] = new Parameter("@Sucursal", objBESede.SedCodigo);
            prm_Params[2] = new Parameter("@DescripcionLocal", objBESede.SedDescripcion);
            prm_Params[3] = new Parameter("@valor", 2);
            prm_Params[4] = new Parameter("@IdEmpresa", objBESede.IdEmpresa);
            prm_Params[5] = new Parameter("@flagSalida", DbType.Int32);
            dop_DataOperation.Parameters.AddRange(prm_Params);

            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation);
            if (Convert.ToInt32(dop_DataOperation.GetParameterByName("@flagSalida").Value) >= 1)
            {
                return false;
            }
            return true;
        }

        public static bool ValidarExisteCodigo(WCO_ListarSede_Result objBESede)
        {
            DataOperation dop_DataOperation = new DataOperation("WCO_ExisteSede");
            Parameter[] prm_Params = new Parameter[6];
            prm_Params[0] = new Parameter("@IdSede", objBESede.IdSede);
            prm_Params[1] = new Parameter("@Sucursal", objBESede.SedCodigo);
            prm_Params[2] = new Parameter("@DescripcionLocal", objBESede.SedDescripcion);
            prm_Params[3] = new Parameter("@valor", 1);
            prm_Params[4] = new Parameter("@IdEmpresa", objBESede.IdEmpresa);
            prm_Params[5] = new Parameter("@flagSalida", DbType.Int32);
            dop_DataOperation.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation);
            if (Convert.ToInt32(dop_DataOperation.GetParameterByName("@flagSalida").Value) >= 1)
            {
                return false;
            }
            return true;
        }

        #endregion

        #region SedeCliente
        public List<WCO_ListarSedeCliente_Result> ListadoSedeCliente(WCO_ListarSedeCliente_Result ObjSede)
        {
            List<WCO_ListarSedeCliente_Result> lst = new List<WCO_ListarSedeCliente_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarSedeCliente(ObjSede.IdSede, ObjSede.IdEmpresa, ObjSede.IdSedeCliente, ObjSede.NombreCompleto, ObjSede.SedCodigo).ToList();
            }
            return lst;
        }

        public void InsertarSedeCliente(WCO_ListarSedeCliente_Result objBESedeCliente)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InsertarSedeCliente");
            Parameter[] prm_Params = new Parameter[5];
            prm_Params[0] = new Parameter("@IdSede", objBESedeCliente.IdSede);
            prm_Params[1] = new Parameter("@IdEmpresa", objBESedeCliente.IdEmpresa);
            prm_Params[2] = new Parameter("@IdSedeCliente", objBESedeCliente.IdSedeCliente);
            prm_Params[3] = new Parameter("@TipoConexion", objBESedeCliente.TipoConexion);
            prm_Params[4] = new Parameter("@Homologacion", objBESedeCliente.Homologacion);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }

        public void InactivarSedeCliente(WCO_ListarSedeCliente_Result objBESedeCliente)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InactivarSedeCliente");
            Parameter[] prm_Params = new Parameter[3];
            prm_Params[0] = new Parameter("@IdSede", objBESedeCliente.IdSede);
            prm_Params[1] = new Parameter("@IdEmpresa", objBESedeCliente.IdEmpresa);
            prm_Params[2] = new Parameter("@IdSedeCliente", objBESedeCliente.IdSedeCliente);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }
        #endregion

        #region SedeCompartida

        public List<WCO_ListarSedeCompartida_Result> ListarSedeCompartida(WCO_ListarSedeCompartida_Result ObjSede)
        {
            List<WCO_ListarSedeCompartida_Result> lst = new List<WCO_ListarSedeCompartida_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarSedeCompartida(ObjSede.IdSede, ObjSede.IdEmpresa, ObjSede.IdSedeCompartida).ToList();
            }
            return lst;
        }


        public void MantenimientoSedeCompartida(int accion, WCO_ListarSedeCompartida_Result objxx)
        {
            using (BDComercialEntities context = new BDComercialEntities())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        WCO_SedeCompartida obj = new WCO_SedeCompartida();
                        obj.IdEmpresa = Convert.ToInt32(objxx.IdEmpresa);
                        obj.IdSede = Convert.ToInt32(objxx.IdSede);
                        obj.IdSedeCompartida = Convert.ToInt32(objxx.IdSedeCompartida);
                        switch (accion)
                        {
                            case 1:                   
                                context.Entry(obj).State = EntityState.Added;
                                break;
                            case 2:
                                context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                                break;
                            case 3:
                                context.Entry(obj).State = EntityState.Deleted;
                                break;
                            //default:
                            //    throw new InvalidOperationException("Acción de mantenimiento no válida.");
                        }
                        context.SaveChanges();
                        dbContextTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        throw new Exception("Error al realizar el mantenimiento del registro.", ex);
                    }
                }
            }
        }

        #endregion


        #region SedePrinter

        public List<WCO_ListarSedePrinter_Result> ListadoSedePrinter(WCO_ListarSedePrinter_Result ObjSede)
        {
            List<WCO_ListarSedePrinter_Result> lst = new List<WCO_ListarSedePrinter_Result>();
            using (var context = new BDComercialEntities())
            {
                Nullable<int> iReturnValue = null;
                if (ObjSede.idprinter > 0)
                {
                    iReturnValue = ObjSede.idprinter;
                }
                lst = context.WCO_ListarSedePrinter(iReturnValue, ObjSede.idsede, ObjSede.ip, ObjSede.shared_name).ToList();
            }
            return lst;
        }

        public void InsertarSedePrinter(WCO_ListarSedePrinter_Result objBESedePrinter)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InsertarSedePrinter");
            Parameter[] prm_Params = new Parameter[6];
            prm_Params[0] = new Parameter("@IdPrinter", objBESedePrinter.idprinter);
            prm_Params[1] = new Parameter("@IdSede", objBESedePrinter.idsede);
            prm_Params[2] = new Parameter("@Ip", objBESedePrinter.ip);
            prm_Params[3] = new Parameter("@Shared_Name", objBESedePrinter.shared_name);
            prm_Params[4] = new Parameter("@alias_name", objBESedePrinter.alias_name);
            prm_Params[5] = new Parameter("@codebar", objBESedePrinter.codebar);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }

        public void EliminarSedePrinter(WCO_ListarSedePrinter_Result objBESedePrinter)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_EliminarSedePrinter");
            Parameter[] prm_Params = new Parameter[2];
            prm_Params[0] = new Parameter("@IdPrinter", objBESedePrinter.idprinter);
            prm_Params[1] = new Parameter("@IdSede", objBESedePrinter.idsede);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }

        #endregion

        #region Sucursal
        public List<WCO_ListarSucursal_Result> WCO_ListarSucursal(WCO_ListarSucursal_Result ObjSede)
        {
            List<WCO_ListarSucursal_Result> lst = new List<WCO_ListarSucursal_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarSucursal(ObjSede.DescripcionLocal, ObjSede.Sucursal, ObjSede.CompaniaCodigo, ObjSede.Estado).ToList();
            }
            return lst;
        }

        public List<WCO_ListarSedeSucursalNegocio_Result> WCO_ListarSedeSucursalNegocio(WCO_ListarSedeSucursalNegocio_Result ObjSede)
        {
            List<WCO_ListarSedeSucursalNegocio_Result> lst = new List<WCO_ListarSedeSucursalNegocio_Result>();
            using (var context = new BDComercialEntities())
            {
                Nullable<int> iReturnValue = null;
                if (ObjSede.IdSede > 0)
                {
                    iReturnValue = ObjSede.IdSede;
                }
                lst = context.WCO_ListarSedeSucursalNegocio(ObjSede.ClasificadorMovimiento, ObjSede.CompaniaCodigo, ObjSede.Sucursal, ObjSede.IdSede, ObjSede.TipoComprobante, ObjSede.SERIE, ObjSede.Estado).ToList();
            }
            return lst;
        }
        public List<WCO_ListarSucursalCompSerie_Result> WCO_ListarSucursalCompSerie(WCO_ListarSedeSucursalNegocio_Result ObjSede)
        {
            List<WCO_ListarSucursalCompSerie_Result> lst = new List<WCO_ListarSucursalCompSerie_Result>();
            using (var context = new BDComercialEntities())
            {
                Nullable<int> iReturnValue = null;
                if (ObjSede.IdSede > 0)
                {
                    iReturnValue = ObjSede.IdSede;
                }
                lst = context.WCO_ListarSucursalCompSerie(ObjSede.ClasificadorMovimiento, ObjSede.CompaniaCodigo, ObjSede.Sucursal,ObjSede.TipoComprobante, ObjSede.Estado).ToList();
            }
            return lst;
        }
        

        #endregion

    }
}