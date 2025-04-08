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
using CrystalDecisions.CrystalReports.Engine;
using Newtonsoft.Json;

namespace WebApiServices.Models.Datos
{
    public class ADDAT_Admision
    {
        public List<WCO_ListarAdmisionServicio_Result> ListadoPaginado(WCO_ListarAdmisionServicio_Result ObjEntidad)
        {
            List<WCO_ListarAdmisionServicio_Result> lst = new List<WCO_ListarAdmisionServicio_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarAdmisionServicio(ObjEntidad.FechaCreacion, ObjEntidad.FechaAdmision, ObjEntidad.NroPeticion, ObjEntidad.TipoAdmisionId,
                    ObjEntidad.HistoriaClinica, ObjEntidad.Estado, ObjEntidad.Persona, ObjEntidad.UsuarioCreacion, ObjEntidad.IdSede,
                     ObjEntidad.OrdenAtencion, ObjEntidad.ClasificadorMovimiento, ObjEntidad.FlatCoaSeguro, ObjEntidad.FlatMovilidad).ToList();
            }
            return lst;
        }

        ///<summary>Inserta el registro en la tabla PERSONAMAST.</summary>
        ///<param name="ADBE_Admision">Entidad de Negocio</param>
        ///<returns>Retorna el pPersona.</returns>
        ///<remarks><list type="bullet">
        ///<item><CreadoPor>Creado PorJordan Mateo Pizarro</CreadoPor></item>
        ///<item><FecCrea>17/10/2012</FecCrea></item></list></remarks>
        public int InsertarAdmision(ADBE_Admision BE_pAdmision)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InsertarAdmisionServicio");
            Parameter[] prm_Params = new Parameter[39];
            prm_Params[0] = new Parameter("@pUneuNegocioId", BE_pAdmision.UneuNegocioId);
            prm_Params[1] = new Parameter("@pFechaAdmision", BE_pAdmision.FechaAdmision);
            prm_Params[2] = new Parameter("@pTipoOperacionId", BE_pAdmision.PK.TipoOperacion.Pk.TipoOperacionId);
            prm_Params[3] = new Parameter("@pHistoriaClinica", BE_pAdmision.HistoriaClinica);
            prm_Params[4] = new Parameter("@pPersona", BE_pAdmision.PK.Persona.PK.IdPersona);
            prm_Params[5] = new Parameter("@pNroPeticion", BE_pAdmision.NroPeticion);
            prm_Params[6] = new Parameter("@pOrdenAtencion", BE_pAdmision.OrdenAtencion);
            prm_Params[7] = new Parameter("@pMedicoId", BE_pAdmision.MedicoId);
            prm_Params[8] = new Parameter("@pEstado", BE_pAdmision.Estado);
            prm_Params[9] = new Parameter("@pFechaCreacion", BE_pAdmision.FechaCreacion);
            prm_Params[10] = new Parameter("@pUsuarioCreacion", BE_pAdmision.UsuarioCreacion);
            prm_Params[11] = new Parameter("@pIpCreacion", BE_pAdmision.IpCreacion);
            prm_Params[12] = new Parameter("@IdSede", BE_pAdmision.PK.Sede.PK.IdSede);
            prm_Params[13] = new Parameter("@idEmpresaEmpleadora", BE_pAdmision.PK.Titular.idEmpresaEmpleadora);
            prm_Params[14] = new Parameter("@IdEmpleado", BE_pAdmision.PK.Titular.PK.IdPersona);
            prm_Params[15] = new Parameter("@pIdAdmision", DbType.Int32);
            prm_Params[16] = new Parameter("@TipoOrdenAtencion", null);
            prm_Params[17] = new Parameter("@TipoAtencion", BE_pAdmision.TipoAtencion);
            prm_Params[18] = new Parameter("@TipoOrden", BE_pAdmision.TipoOrden);
            prm_Params[19] = new Parameter("@AseguradoraId", BE_pAdmision.AseguradoraId);
            prm_Params[20] = new Parameter("@IdEmpresa", BE_pAdmision.PK.Empresa.PK.IdPersona);
            prm_Params[21] = new Parameter("@Cama", BE_pAdmision.Cama);
            prm_Params[22] = new Parameter("@CoAseguro", BE_pAdmision.CoaSeguro);
            prm_Params[23] = new Parameter("@EmpresaSede", BE_pAdmision.PK.Empresa.idEmpresaEmpleadora);
            prm_Params[24] = new Parameter("@IdProcedencia", BE_pAdmision.IdEspecialidad);
            prm_Params[25] = new Parameter("@pIdExamen", null);
            prm_Params[26] = new Parameter("@pIdInsumo", null);
            prm_Params[27] = new Parameter("@IdClasificadorMovimiento", BE_pAdmision.ClasificadorMovimiento);
            prm_Params[28] = new Parameter("@OrdenSinapa", BE_pAdmision.OrdenSinapa);
            prm_Params[29] = new Parameter("@Comentario", BE_pAdmision.Comentario);
            prm_Params[30] = new Parameter("@IdAprobador", null);
            prm_Params[31] = new Parameter("@FlatCoaSeguro", BE_pAdmision.FlatCoaSeguro);
            prm_Params[32] = new Parameter("@FlatMovilidad", BE_pAdmision.FlatMovilidad);
            prm_Params[33] = new Parameter("@Afecto", BE_pAdmision.Afecto);
            prm_Params[34] = new Parameter("@Igv", BE_pAdmision.Igv);
            prm_Params[35] = new Parameter("@Total", BE_pAdmision.Total);
            prm_Params[36] = new Parameter("@Anticipo", BE_pAdmision.Anticipo);
            prm_Params[37] = new Parameter("@Saldo", BE_pAdmision.Saldo);
            prm_Params[38] = new Parameter("@Redondeo", BE_pAdmision.Redondeo);
            dop_Operacion.Parameters.AddRange(prm_Params);
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    //DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);  
                    DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
                    scope.Complete();
                    return int.Parse(dop_Operacion.GetParameterByName("@pIdAdmision").Value.ToString());
                }
                catch (TransactionAbortedException ex)
                {
                    UT_Kerberos.WriteLog(System.DateTime.Now + " | " + "Registrar|InsertarAdmisionOA = TransactionAborted " + JsonConvert.SerializeObject(ex, Newtonsoft.Json.Formatting.Indented));
                    return 0;
                }
                catch (ApplicationException ex)
                {
                    UT_Kerberos.WriteLog(System.DateTime.Now + " | " + "Registrar|InsertarAdmisionOA =  ApplicationException " + JsonConvert.SerializeObject(ex, Newtonsoft.Json.Formatting.Indented));
                    return 0;
                }
            }

        }

        ///<summary>Inserta el registro en la tabla PERSONAMAST.</summary>
        ///<param name="ADBE_Admision">Entidad de Negocio</param>
        ///<returns>Retorna el pPersona.</returns>
        ///<remarks><list type="bullet">
        ///<item><CreadoPor>Creado PorJordan Mateo Pizarro</CreadoPor></item>
        ///<item><FecCrea>17/10/2012</FecCrea></item></list></remarks>
        public int InsertarAdmisionOA(ADBE_Admision BE_pAdmision)
        {
            if (BE_pAdmision.PK.Persona.Nombres != null)
            {
                if (!string.IsNullOrEmpty(BE_pAdmision.PK.Persona.Nombres.ToString()))
                {
                    BE_pAdmision.PK.Persona.Nombres = BE_pAdmision.PK.Persona.Nombres.ToUpper();
                }
            }
            if (BE_pAdmision.PK.Persona.ApellidoPaterno != null)
            {
                if (!string.IsNullOrEmpty(BE_pAdmision.PK.Persona.ApellidoPaterno.ToString()))
                {
                    BE_pAdmision.PK.Persona.ApellidoPaterno = BE_pAdmision.PK.Persona.ApellidoPaterno.ToUpper();
                }
            }
            if (BE_pAdmision.PK.Persona.ApellidoMaterno != null)
            {
                if (!string.IsNullOrEmpty(BE_pAdmision.PK.Persona.ApellidoMaterno.ToString()))
                {
                    BE_pAdmision.PK.Persona.ApellidoMaterno = BE_pAdmision.PK.Persona.ApellidoMaterno.ToUpper();
                }
            }
            if (BE_pAdmision.PK.Persona.PerCorreoElectronico != null)
            {
                if (!string.IsNullOrEmpty(BE_pAdmision.PK.Persona.PerCorreoElectronico.ToString()))
                {
                    BE_pAdmision.PK.Persona.PerCorreoElectronico = BE_pAdmision.PK.Persona.PerCorreoElectronico.ToUpper();
                }
            }

            DataOperation dop_Operacion = new DataOperation("WCO_InsertarAdmisionOA");
            Parameter[] prm_Params = new Parameter[33];
            prm_Params[0] = new Parameter("@pUneuNegocioId", BE_pAdmision.UneuNegocioId);
            prm_Params[1] = new Parameter("@pFechaAdmision", BE_pAdmision.FechaAdmision);
            prm_Params[2] = new Parameter("@pTipoOperacionId", BE_pAdmision.PK.TipoOperacion.Pk.TipoOperacionId);
            prm_Params[3] = new Parameter("@pHistoriaClinica", BE_pAdmision.HistoriaClinica);
            prm_Params[4] = new Parameter("@pPersona", BE_pAdmision.PK.Persona.PK.IdPersona);
            prm_Params[5] = new Parameter("@pOrdenAtencion", BE_pAdmision.OrdenAtencion);
            prm_Params[6] = new Parameter("@NumeroDocumento", BE_pAdmision.PK.Persona.PerNumeroDocumento);
            prm_Params[7] = new Parameter("@pMedicoId", BE_pAdmision.MedicoId);
            prm_Params[8] = new Parameter("@IdSede", BE_pAdmision.PK.Sede.PK.IdSede);
            prm_Params[9] = new Parameter("@pEstado", BE_pAdmision.Estado);
            prm_Params[10] = new Parameter("@pFechaCreacion", BE_pAdmision.FechaCreacion);
            prm_Params[11] = new Parameter("@pUsuarioCreacion", BE_pAdmision.UsuarioCreacion);
            prm_Params[12] = new Parameter("@pIpCreacion", BE_pAdmision.IpCreacion);
            prm_Params[13] = new Parameter("@Nombre", BE_pAdmision.PK.Persona.Nombres);
            prm_Params[14] = new Parameter("@Paterno", BE_pAdmision.PK.Persona.ApellidoPaterno);
            prm_Params[15] = new Parameter("@Materno", BE_pAdmision.PK.Persona.ApellidoMaterno);
            prm_Params[16] = new Parameter("@PerFechaNacimiento", BE_pAdmision.PK.Persona.PerFechaNacimiento);
            prm_Params[17] = new Parameter("@Sexo", BE_pAdmision.PK.Persona.PerSexo);
            prm_Params[18] = new Parameter("@TipoDoc", BE_pAdmision.PK.Persona.PerTipoDoc);
            prm_Params[19] = new Parameter("@CorreoElectronico", BE_pAdmision.PK.Persona.PerCorreoElectronico);
            prm_Params[20] = new Parameter("@NombreMedico", BE_pAdmision.PK.Titular.Nombres);
            prm_Params[21] = new Parameter("@PaternoMedico", BE_pAdmision.PK.Titular.ApellidoPaterno);
            prm_Params[22] = new Parameter("@MaternoMedico", BE_pAdmision.PK.Titular.ApellidoMaterno);
            prm_Params[23] = new Parameter("@TipoOrdenAtencion", BE_pAdmision.TipoPacienteId);
            prm_Params[24] = new Parameter("@TipoAtencion", BE_pAdmision.TipoAtencion);
            prm_Params[25] = new Parameter("@Empresa", BE_pAdmision.PK.Empresa.NombreCompleto);
            prm_Params[26] = new Parameter("@Ruc", BE_pAdmision.PK.Empresa.PerNumeroDocumento);
            prm_Params[27] = new Parameter("@Aseguradora", BE_pAdmision.Aseguradora_Nombre);
            prm_Params[28] = new Parameter("@CMPMedico", BE_pAdmision.PK.Titular.PerNumeroDocumento);
            prm_Params[29] = new Parameter("@Especialidad", BE_pAdmision.PK.Sede.DescripcionLocal);
            prm_Params[30] = new Parameter("@FechaLimite", BE_pAdmision.FechaLimite);
            prm_Params[31] = new Parameter("@IdClasificadorMovimiento", BE_pAdmision.ClasificadorMovimiento);
            prm_Params[32] = new Parameter("@pIdAdmision", DbType.Int32);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            return int.Parse(dop_Operacion.GetParameterByName("@pIdAdmision").Value.ToString());
        }

        ///<summary>Actualizar el registro en la tabla PERSONAMAST.</summary>
        ///<param name="ADBE_Admision">Entidad de Negocio</param>
        ///<remarks><list type="bullet">
        ///<item><CreadoPor>Creado PorJordan Mateo Pizarro</CreadoPor></item>
        ///<item><FecCrea>17/10/2012</FecCrea></item></list></remarks>
        public void Actualizar(ADBE_Admision BE_pAdmision)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_ActualizarAdmisionServicio");
            Parameter[] prm_Params = new Parameter[37];
            prm_Params[0] = new Parameter("@pIdAdmision", BE_pAdmision.PK.IdAdmision);
            prm_Params[1] = new Parameter("@pFechaAdmision", BE_pAdmision.FechaAdmision);
            prm_Params[2] = new Parameter("@pTipoOperacionId", BE_pAdmision.PK.TipoOperacion.Pk.TipoOperacionId);
            prm_Params[3] = new Parameter("@pHistoriaClinica", BE_pAdmision.HistoriaClinica);
            prm_Params[4] = new Parameter("@pPersona", BE_pAdmision.PK.Persona.PK.IdPersona);
            prm_Params[5] = new Parameter("@pNroPeticion", BE_pAdmision.NroPeticion);
            prm_Params[6] = new Parameter("@pOrdenAtencion", BE_pAdmision.OrdenAtencion);
            prm_Params[7] = new Parameter("@pMedicoId", BE_pAdmision.MedicoId);
            prm_Params[8] = new Parameter("@pEstado", BE_pAdmision.Estado);
            prm_Params[9] = new Parameter("@pUneuNegocioId", BE_pAdmision.UneuNegocioId);
            prm_Params[10] = new Parameter("@pFechaModificacion", BE_pAdmision.FechaModificacion);
            prm_Params[11] = new Parameter("@pUsuarioModificacion", BE_pAdmision.UsuarioModificacion);
            prm_Params[12] = new Parameter("@pIpModificacion", BE_pAdmision.IpModificacion);
            prm_Params[13] = new Parameter("@IdSede", BE_pAdmision.PK.Sede.PK.IdSede);
            prm_Params[14] = new Parameter("@TipoOrdenAtencion", null);
            prm_Params[15] = new Parameter("@TipoAtencion", BE_pAdmision.TipoAtencion);
            prm_Params[16] = new Parameter("@TipoOrden", BE_pAdmision.TipoOrden);
            prm_Params[17] = new Parameter("@AseguradoraId", BE_pAdmision.AseguradoraId);
            prm_Params[18] = new Parameter("@IdEmpresa", BE_pAdmision.PK.Empresa.PK.IdPersona);
            prm_Params[19] = new Parameter("@Cama", BE_pAdmision.Cama);
            prm_Params[20] = new Parameter("@CoAseguro", BE_pAdmision.CoaSeguro);
            prm_Params[21] = new Parameter("@EmpresaSede", BE_pAdmision.PK.Empresa.idEmpresaEmpleadora);
            prm_Params[22] = new Parameter("@IdProcedencia", BE_pAdmision.IdEspecialidad);
            prm_Params[23] = new Parameter("@pIdExamen", null);
            prm_Params[24] = new Parameter("@pIdInsumo", null);
            prm_Params[25] = new Parameter("@IdClasificadorMovimiento", BE_pAdmision.ClasificadorMovimiento);
            prm_Params[26] = new Parameter("@OrdenSinapa", BE_pAdmision.OrdenSinapa);
            prm_Params[27] = new Parameter("@Comentario", BE_pAdmision.Comentario);
            prm_Params[28] = new Parameter("@IdAprobador", null); //BE_pAdmision.AprobadoresId
            prm_Params[29] = new Parameter("@FlatCoaSeguro", BE_pAdmision.FlatCoaSeguro);
            prm_Params[30] = new Parameter("@FlatMovilidad", BE_pAdmision.FlatMovilidad);

            prm_Params[31] = new Parameter("@Afecto", BE_pAdmision.Afecto);
            prm_Params[32] = new Parameter("@Igv", BE_pAdmision.Igv);
            prm_Params[33] = new Parameter("@Total", BE_pAdmision.Total);
            prm_Params[34] = new Parameter("@Anticipo", BE_pAdmision.Anticipo);
            prm_Params[35] = new Parameter("@Saldo", BE_pAdmision.Saldo);
            prm_Params[36] = new Parameter("@Redondeo", BE_pAdmision.Redondeo);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }

        ///<summary>Inserta el registro en la tabla ADBE_AdmisionServicio.</summary>
        ///<param name="ADBE_AdmisionServicio">Entidad de Negocio</param>
        ///<returns>Retorna el pPersona.</returns>
        ///<remarks><list type="bullet">
        ///<item><CreadoPor>Creado PorJordan Mateo Pizarro</CreadoPor></item>
        ///<item><FecCrea>17/10/2012</FecCrea></item></list></remarks>
        public int InsertarAdmisionServicio(ADBE_AdmisionServicio objBEAdmisionServicio)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InsertarAdmisionServicioDetalle");
            Parameter[] prm_Params = new Parameter[12];
            prm_Params[0] = new Parameter("@pUneuNegocioId", objBEAdmisionServicio.PK.UnidadNegocio.Pk.UneuNegocioId);
            prm_Params[1] = new Parameter("@pCodigoComponente", objBEAdmisionServicio.PK.Componente.Pk.CodigoComponente);
            prm_Params[2] = new Parameter("@pIdAdmision", objBEAdmisionServicio.PK.Admision.PK.IdAdmision);
            prm_Params[3] = new Parameter("@pCantidad", objBEAdmisionServicio.Cantidad);
            prm_Params[4] = new Parameter("@pValor", objBEAdmisionServicio.Valor);
            prm_Params[5] = new Parameter("@pIdCobertura", objBEAdmisionServicio.IdCobertura);
            prm_Params[6] = new Parameter("@pEstado", objBEAdmisionServicio.Estado);
            prm_Params[7] = new Parameter("@pFechaCreacion", objBEAdmisionServicio.FechaCreacion);
            prm_Params[8] = new Parameter("@pUsuarioCreacion", objBEAdmisionServicio.UsuarioCreacion);
            prm_Params[9] = new Parameter("@pIpCreacion", objBEAdmisionServicio.IpCreacion);
            prm_Params[10] = new Parameter("@PrecioUnitarioLocal", objBEAdmisionServicio.PK.Componente.CantidadKit);
            prm_Params[11] = new Parameter("@pIdAdmServicio", DbType.Int32);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            return int.Parse(dop_Operacion.GetParameterByName("@pIdAdmServicio").Value.ToString());
        }

        ///<summary>Inserta el registro en la tabla ADBE_AdmisionServicio.</summary>
        ///<param name="ADBE_AdmisionServicio">Entidad de Negocio</param>
        ///<returns>Retorna el pPersona.</returns>
        ///<remarks><list type="bullet">
        ///<item><CreadoPor>Creado PorJordan Mateo Pizarro</CreadoPor></item>
        ///<item><FecCrea>17/10/2012</FecCrea></item></list></remarks>
        public int InsertarAdmisionServicioOA(ADBE_AdmisionServicio objBEAdmisionServicio)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InsertarAdmisionServicioDetalleOA");
            Parameter[] prm_Params = new Parameter[15];
            prm_Params[0] = new Parameter("@pUneuNegocioId", objBEAdmisionServicio.PK.UnidadNegocio.Pk.UneuNegocioId);
            prm_Params[1] = new Parameter("@pCodigoComponente", objBEAdmisionServicio.PK.Componente.Pk.CodigoComponente);
            prm_Params[2] = new Parameter("@pIdAdmision", objBEAdmisionServicio.PK.Admision.PK.IdAdmision);
            prm_Params[3] = new Parameter("@pCantidad", objBEAdmisionServicio.Cantidad);
            prm_Params[4] = new Parameter("@pValor", objBEAdmisionServicio.Valor);
            prm_Params[5] = new Parameter("@pIdCobertura", objBEAdmisionServicio.IdCobertura);
            prm_Params[6] = new Parameter("@pEstado", objBEAdmisionServicio.Estado);
            prm_Params[7] = new Parameter("@pFechaCreacion", objBEAdmisionServicio.FechaCreacion);
            prm_Params[8] = new Parameter("@pUsuarioCreacion", objBEAdmisionServicio.UsuarioCreacion);
            prm_Params[9] = new Parameter("@pIpCreacion", objBEAdmisionServicio.IpCreacion);
            prm_Params[10] = new Parameter("@PrecioUnitarioLocal", objBEAdmisionServicio.PK.Componente.CantidadKit);
            prm_Params[11] = new Parameter("@IdEmpresa", objBEAdmisionServicio.PK.Admision.PK.Empresa.PK.IdPersona);
            prm_Params[12] = new Parameter("@TipoAtencion", objBEAdmisionServicio.PK.Componente.IdArea);//IdOrdenAtencion
            prm_Params[13] = new Parameter("@Conexion", objBEAdmisionServicio.Conexion);
            prm_Params[14] = new Parameter("@pIdAdmServicio", DbType.Int32);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            return int.Parse(dop_Operacion.GetParameterByName("@pIdAdmServicio").Value.ToString());
        }

        ///<summary>Actualizar el registro en la tabla PERSONAMAST.</summary>
        ///<param name="obj_pSA_Curriculo">Entidad de Negocio</param>
        ///<remarks><list type="bullet">
        ///<item><CreadoPor>Creado PorJordan Mateo Pizarro</CreadoPor></item>
        ///<item><FecCrea>17/10/2012</FecCrea></item></list></remarks>
        public void ActualizarAdmisionDetalle(ADBE_AdmisionServicio objBEAdmisionServicio)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_ActualizarAdmisionServicioDetalle");
            Parameter[] prm_Params = new Parameter[11];
            prm_Params[0] = new Parameter("@pIdAdmServicio", objBEAdmisionServicio.PK.IdAdmServicio);
            prm_Params[1] = new Parameter("@pCodigoComponente", objBEAdmisionServicio.PK.Componente.Pk.CodigoComponente);
            prm_Params[2] = new Parameter("@pIdAdmision", objBEAdmisionServicio.PK.Admision.PK.IdAdmision);
            prm_Params[3] = new Parameter("@pCantidad", objBEAdmisionServicio.Cantidad);
            prm_Params[4] = new Parameter("@pValor", objBEAdmisionServicio.Valor);
            prm_Params[5] = new Parameter("@pIdCobertura", objBEAdmisionServicio.IdCobertura);
            prm_Params[6] = new Parameter("@pEstado", objBEAdmisionServicio.Estado);
            prm_Params[7] = new Parameter("@pFechaModificacion", objBEAdmisionServicio.FechaModificacion);
            prm_Params[8] = new Parameter("@pUsuarioModificacion", objBEAdmisionServicio.UsuarioModificacion);
            prm_Params[9] = new Parameter("@pIpModificacion", objBEAdmisionServicio.IpModificacion);
            prm_Params[10] = new Parameter("@pUneuNegocioId", objBEAdmisionServicio.PK.UnidadNegocio.Pk.UneuNegocioId);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }

        ///<summary>Actualizar el registro en la tabla PERSONAMAST.</summary>
        ///<param name="obj_pSA_Curriculo">Entidad de Negocio</param>
        ///<remarks><list type="bullet">
        ///<item><CreadoPor>Creado PorJordan Mateo Pizarro</CreadoPor></item>
        ///<item><FecCrea>17/10/2012</FecCrea></item></list></remarks>
        public void AdmisionServicioDetalleEliminar(ADBE_AdmisionServicio objBEAdmisionServicio)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_EliminarAdmisionServicioDetalle");
            Parameter[] prm_Params = new Parameter[2];
            prm_Params[0] = new Parameter("@IdAdmision", objBEAdmisionServicio.PK.Admision.PK.IdAdmision);
            prm_Params[1] = new Parameter("@CodigoComponente", objBEAdmisionServicio.PK.Componente.Pk.CodigoComponente);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }

        public string WCO_UnificarAtencionPaciente(WCO_UnificarAtencionPaciente_Result objBEPersona)
        {
            string msg = "";
            try
            {
                DataOperation dop_Operacion = new DataOperation("WCO_UnificarAtencionPaciente");
                Parameter[] prm_Params = new Parameter[5];
                prm_Params[0] = new Parameter("@IdAdmision", objBEPersona.IdAdmision);
                prm_Params[1] = new Parameter("@IdPersona", objBEPersona.IdPersona);
                prm_Params[2] = new Parameter("@Documento", objBEPersona.Documento);
                prm_Params[3] = new Parameter("@IdUnificador", objBEPersona.IdUnificador);
                prm_Params[4] = new Parameter("@UsuarioCreacion", objBEPersona.UsuarioCreacion);
                dop_Operacion.Parameters.AddRange(prm_Params);
                DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
                msg = "Ok";
            }
            catch (Exception e) { msg = e.Message; }
            return msg;
        }

        ///<summary>Actualizar el registro en la tabla Inactivar.</summary>
        ///<param name="ADBE_Admision">Entidad de Negocio</param>
        ///<remarks><list type="bullet">
        ///<item><CreadoPor>Creado PorJordan Mateo Pizarro</CreadoPor></item>
        ///<item><FecCrea>17/10/2012</FecCrea></item></list></remarks>
        public void InactivarAdmisionControl(WCO_TraerXAdmisionServicio_Result BE_pAdmision)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InactivarAdmisionControl");
            Parameter[] prm_Params = new Parameter[4];
            prm_Params[0] = new Parameter("@IdAdmision", BE_pAdmision.IdAdmision);
            prm_Params[1] = new Parameter("@Estado", BE_pAdmision.Estado);
            prm_Params[2] = new Parameter("@UltimoUsuario", BE_pAdmision.UsuarioModificacion);
            prm_Params[3] = new Parameter("@IpModificacion", BE_pAdmision.IpModificacion);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }









        public void InactivarDetComponente(ADBE_ReferenciaComponente objBEReferenciaDetComponente)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_InactivarReferenciaComponente");
            Parameter[] prm_Params = new Parameter[4];
            prm_Params[0] = new Parameter("@IdAdmision", objBEReferenciaDetComponente.PK.Admision.PK.IdAdmision);
            prm_Params[1] = new Parameter("@CodigoComponente", objBEReferenciaDetComponente.CodigoComponente);
            prm_Params[2] = new Parameter("@UsuarioModificacion", objBEReferenciaDetComponente.UsuarioModificacion);
            prm_Params[3] = new Parameter("@Estado", objBEReferenciaDetComponente.Estado);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
        }

        public DataTable ListadoComponentePerfil(ADBE_ComponentePerfil objBEComponentePerfil)
        {
            DataSet ds_Result = null;
            DataOperation dop_DataOperation = new DataOperation("WCO_ListarComponentePerfil");
            Parameter[] prm_Params = new Parameter[4];
            prm_Params[0] = new Parameter("@UneuNegocioId", objBEComponentePerfil.Pk.UnidadNegocio.Pk.UneuNegocioId);
            prm_Params[1] = new Parameter("@CodigoComponente", objBEComponentePerfil.Pk.Componente.Pk.CodigoComponente);
            prm_Params[2] = new Parameter("@CodigoHomologado", objBEComponentePerfil.CodigoHomologado);
            prm_Params[3] = new Parameter("@Estado", objBEComponentePerfil.Estado);

            dop_DataOperation.Parameters.AddRange(prm_Params);
            ds_Result = DataManager.ExecuteDataSet(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation);

            if (ds_Result == null || ds_Result.Tables.Count == 0)
            {
                return null;
            }
            return ds_Result.Tables[0];
        }

        public int InsertarTransaccion(CABE_Transaccion obj_vBTransaccion)
        {
            DataOperation dop_Operacion = new DataOperation("WCO_RegistrarPago");
            Parameter[] prm_Params = new Parameter[40];
            prm_Params[0] = new Parameter("@IdAdmision", obj_vBTransaccion.PK.Admision.PK.IdAdmision);
            prm_Params[1] = new Parameter("@TipoAdmision", obj_vBTransaccion.PK.Admision.TipoAdmision);//Clinica // Convenio // Particular
            prm_Params[2] = new Parameter("@Compania", obj_vBTransaccion.PK.UnidadNegocio.CompaniaCodigo);
            prm_Params[3] = new Parameter("@NroPeticion", obj_vBTransaccion.PK.Admision.NroPeticion);
            prm_Params[4] = new Parameter("@TipoPaciente", obj_vBTransaccion.PK.Admision.TipoPacienteId);
            prm_Params[5] = new Parameter("@ClasificadorMovimiento", obj_vBTransaccion.PK.Admision.ClasificadorMovimiento);
            prm_Params[6] = new Parameter("@IdSede", obj_vBTransaccion.PK.Admision.PK.Sede.PK.IdSede);
            prm_Params[7] = new Parameter("@IdPaciente", obj_vBTransaccion.PK.Admision.PK.Persona.PK.IdPersona);//IdPaciente // AUTORIZADO // TITULAR
            prm_Params[8] = new Parameter("@DocumentoIdentidad", obj_vBTransaccion.PK.Admision.PK.Persona.PerNumeroDocumento);
            prm_Params[9] = new Parameter("@NombrePaciente", obj_vBTransaccion.PK.Admision.PK.Persona.NombreCompleto);
            prm_Params[10] = new Parameter("@DireccionPaciente", obj_vBTransaccion.PK.Admision.PK.Persona.Direccion);
            prm_Params[11] = new Parameter("@IdAseguradora", obj_vBTransaccion.PK.Admision.AseguradoraId);
            prm_Params[12] = new Parameter("@IdEmpresaPaciente", obj_vBTransaccion.PK.Admision.PK.Empresa.idEmpresaEmpleadora);

            prm_Params[13] = new Parameter("@IdClienteFacturacion", obj_vBTransaccion.IdClienteFacturacion);
            prm_Params[14] = new Parameter("@DocumentoCliente", obj_vBTransaccion.DocumentoCliente);
            prm_Params[15] = new Parameter("@NombreCliente", obj_vBTransaccion.NombreCliente);
            prm_Params[16] = new Parameter("@DireccionCliente", obj_vBTransaccion.DireccionCliente);
            prm_Params[17] = new Parameter("@IdEstablecimiento", obj_vBTransaccion.IdEstablecimiento);
            prm_Params[18] = new Parameter("@Sucursal", obj_vBTransaccion.Sucursal);

            prm_Params[19] = new Parameter("@TipoComprobante", obj_vBTransaccion.TipoComprobante);
            prm_Params[20] = new Parameter("@SerieComprobante", obj_vBTransaccion.SerieComprobante);
            prm_Params[21] = new Parameter("@NumeroComprobante", obj_vBTransaccion.NumeroComprobante);
            prm_Params[22] = new Parameter("@NumeroTarjeta", obj_vBTransaccion.NumeroGuiaSalida);
            prm_Params[23] = new Parameter("@Moneda", obj_vBTransaccion.Moneda);

            prm_Params[24] = new Parameter("@MontoPagado", obj_vBTransaccion.MontoPagado);
            prm_Params[25] = new Parameter("@MontoAdelantoSaldo", obj_vBTransaccion.MontoAdelantoSaldo);
            prm_Params[26] = new Parameter("@MontoTotal", obj_vBTransaccion.MontoTotal);
            prm_Params[27] = new Parameter("@MontoTotalLocal", obj_vBTransaccion.MontoTotalLocal);
            prm_Params[28] = new Parameter("@MontoNoAfectoLocal", obj_vBTransaccion.MontoNoAfectoLocal);
            prm_Params[29] = new Parameter("@PorcentajeImpuesto", obj_vBTransaccion.PorcentajeImpuesto);
            prm_Params[30] = new Parameter("@MontoAdelanto", obj_vBTransaccion.MontoAdelanto);
            prm_Params[31] = new Parameter("@MontoImpuestoVentasLocal", obj_vBTransaccion.MontoImpuestoVentasLocal);
            prm_Params[32] = new Parameter("@MontoAfectoLocal", obj_vBTransaccion.MontoAfectoLocal);

            prm_Params[33] = new Parameter("@TipoCambio", obj_vBTransaccion.TipoCambio);
            prm_Params[34] = new Parameter("@MontoDescuentos", obj_vBTransaccion.MontoDescuentos);
            prm_Params[35] = new Parameter("@UsuarioCreacion", obj_vBTransaccion.UsuarioCreacion);
            prm_Params[36] = new Parameter("@Autorizacion", obj_vBTransaccion.IdPlan);//Autorizacion
            prm_Params[37] = new Parameter("@AplicaTitular", obj_vBTransaccion.Idpoliza);//AplicaTitular   
            prm_Params[38] = new Parameter("@TipoAtencion", obj_vBTransaccion.PK.Admision.TipoAtencion);

            prm_Params[39] = new Parameter("@ll_IdTransaccionNuevo", DbType.Int32);
            dop_Operacion.Parameters.AddRange(prm_Params);
            DataManager.ExecuteNonQuery(DAT_Conexion.Co_ConnecPrecisa, dop_Operacion);
            return int.Parse(dop_Operacion.GetParameterByName("@ll_IdTransaccionNuevo").Value.ToString());
        }

        public List<ADBE_ContratoPrecio> ListadoTipoOperacionPrecio(Nullable<Int32> TipoOperacionID, Nullable<Int32> Estado)
        {
            DataSet ds_Result = null;
            DataOperation dop_DataOperation = new DataOperation("WCO_ListarTipoOperacionPrecio");
            Parameter[] prm_Params = new Parameter[2];
            prm_Params[0] = new Parameter("@TipoOperacionID", TipoOperacionID);
            prm_Params[1] = new Parameter("@Estado", Estado);
            dop_DataOperation.Parameters.AddRange(prm_Params);
            ds_Result = DataManager.ExecuteDataSet(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation);
            if (ds_Result == null || ds_Result.Tables.Count == 0)
            {
                return null;
            }
            var myData = ds_Result.Tables[0].AsEnumerable().Select(r => new ADBE_ContratoPrecio
            {
                Sexo = r.Field<string>("Sexo"),
                ClasificadorMovimiento = r.Field<string>("ClasificadorMovimiento"),
                CodigoHomologado = r.Field<string>("CodigoHomologado"),
                CodigoComponente = r.Field<string>("CodigoComponente"),
                Nombre = r.Field<string>("Nombre"),
                Abreviatura = r.Field<string>("Abreviatura"),
                TipoFactor = r.Field<string>("TipoFactor"),

                FechaInicio = r.Field<Nullable<DateTime>>("FechaInicio"),
                FechaTermino = r.Field<Nullable<DateTime>>("FechaTermino"),

                Factor = r.Field<Nullable<decimal>>("Factor"),
                PrecioUnitarioLista = r.Field<Nullable<decimal>>("PrecioUnitarioLista"),
                PrecioUnitarioBase = r.Field<Nullable<decimal>>("PrecioUnitarioBase"),
                Copago = r.Field<Nullable<decimal>>("Copago"),
                TPFactor = r.Field<Nullable<decimal>>("TPFactor"),
                Precio = r.Field<Nullable<decimal>>("Precio"),
                PrecioPaciente = r.Field<Nullable<decimal>>("PrecioPaciente"),
                PrecioEmpresa = r.Field<Nullable<decimal>>("PrecioEmpresa"),

                TipoOperacionID = r.Field<Nullable<int>>("TipoOperacionID"),
                TPContratoID = r.Field<Nullable<int>>("TPContratoID"),
                TIPOADMISIONID = r.Field<Nullable<int>>("TIPOADMISIONID"),
                IdClasificacion = r.Field<Nullable<int>>("IdClasificacion"),
                TipoPacienteId = r.Field<Nullable<int>>("TipoPacienteId"),
                IdListaBase = r.Field<Nullable<int>>("IdListaBase"),
                Persona = r.Field<Nullable<int>>("Persona"),
                TPAplicaFactor = r.Field<Nullable<int>>("TPAplicaFactor"),
                FlagRedondeo = r.Field<Nullable<int>>("FlagRedondeo"),
                AplicaFormula = r.Field<Nullable<int>>("AplicaFormula")
            });

            var list = myData.ToList();

            return list;
        }

        public DataTable ValidarOAExamen(ADBE_Admision BE_pAdmision)
        {
            DataSet ds_Result = null;
            DataOperation dop_DataOperation = new DataOperation("WCO_ValidarOAExamen");
            Parameter[] prm_Params = new Parameter[3];
            prm_Params[0] = new Parameter("@IdPersona", BE_pAdmision.PK.TipoOperacion.Pk.Persona.PK.IdPersona);
            prm_Params[1] = new Parameter("@OrdenAtencion", BE_pAdmision.OrdenAtencion);
            prm_Params[2] = new Parameter("@IdSede", BE_pAdmision.PK.Sede.PK.IdSede);
            dop_DataOperation.Parameters.AddRange(prm_Params);

            ds_Result = DataManager.ExecuteDataSet(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation);

            return ds_Result.Tables[0];
        }

        public DataTable ValidarOAExamenRest(ADBE_Admision BE_pAdmision)
        {
            DataSet ds_Result = null;
            DataOperation dop_DataOperation = new DataOperation("WCO_ValidarOAExamenRest");
            Parameter[] prm_Params = new Parameter[5];
            prm_Params[0] = new Parameter("@IdPersona", BE_pAdmision.PK.TipoOperacion.Pk.Persona.PK.IdPersona);
            prm_Params[1] = new Parameter("@OrdenAtencion", BE_pAdmision.OrdenAtencion);
            prm_Params[2] = new Parameter("@IdSede", BE_pAdmision.PK.Sede.PK.IdSede);
            prm_Params[3] = new Parameter("@IdOrdenAtencion", BE_pAdmision.PK.IdAdmision);
            prm_Params[4] = new Parameter("@IdTipoOperacion", BE_pAdmision.PK.TipoOperacion.Pk.TipoOperacionId);
            dop_DataOperation.Parameters.AddRange(prm_Params);

            ds_Result = DataManager.ExecuteDataSet(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation);

            return ds_Result.Tables[0];
        }


        public DataTable ListarAdmisionExistente(ADBE_Admision BE_pAdmision)
        {
            DataSet ds_Result = null;
            DataOperation dop_DataOperation = new DataOperation("WCO_ListarAdmisionExistente");
            Parameter[] prm_Params = new Parameter[3];
            prm_Params[0] = new Parameter("@IdPersona", BE_pAdmision.PK.TipoOperacion.Pk.Persona.PK.IdPersona);
            prm_Params[1] = new Parameter("@OrdenAtencion", BE_pAdmision.OrdenAtencion);
            prm_Params[2] = new Parameter("@IdSede", BE_pAdmision.PK.Sede.PK.IdSede);
            dop_DataOperation.Parameters.AddRange(prm_Params);
            ds_Result = DataManager.ExecuteDataSet(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation);
            return ds_Result.Tables[0];
        }

        public List<WCO_ListarAdmisionServicioDetalle_Result> ListadoAdmisionServicioDetalle(WCO_ListarAdmisionServicio_Result ObjEntidad)
        {
            List<WCO_ListarAdmisionServicioDetalle_Result> lst = new List<WCO_ListarAdmisionServicioDetalle_Result>();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_ListarAdmisionServicioDetalle(ObjEntidad.IdAdmision, ObjEntidad.NroPeticion).ToList();
            }
            return lst;
        }

        public RequestAdmision TraerXAdmisionServicio(WCO_TraerXAdmisionServicio_Result ObjEntidad)
        {
            List<WCO_TraerXAdmisionServicio_Result> lst = new List<WCO_TraerXAdmisionServicio_Result>();
            RequestAdmision objResul = new RequestAdmision();
            using (var context = new BDComercialEntities())
            {
                lst = context.WCO_TraerXAdmisionServicio(ObjEntidad.IdAdmision).ToList();
                WCO_ListarAdmisionServicio_Result Objdetalle = new WCO_ListarAdmisionServicio_Result();
                Objdetalle.IdAdmision = ObjEntidad.IdAdmision;
                objResul.list_AdmisionServicio = ListadoAdmisionServicioDetalle(Objdetalle);

                foreach (WCO_TraerXAdmisionServicio_Result obj_Seleccionados in lst)
                {
                    //WCO_ListarAdmisionServicio_Result Objdetalle = new WCO_ListarAdmisionServicio_Result();
                    //Objdetalle.IdAdmision = ObjEntidad.IdAdmision;
                    //objResul.list_AdmisionServicio = ListadoAdmisionServicioDetalle(Objdetalle);
                    objResul.Admision = obj_Seleccionados;
                }
            }
            return objResul;
        }

        public List<WCO_ListarAdmisionServicioConstancia_Result> ListadoAdmisionServicioConstancia(WCO_ListarAdmisionServicio_Result ObjEntidad)
        {
            List<WCO_ListarAdmisionServicioConstancia_Result> lst = new List<WCO_ListarAdmisionServicioConstancia_Result>();
            using (var context = new BDComercialEntities())
            {
                List<WCO_ListarAdmisionServicioConstanciaDetalle_Result> lstPrimera = new List<WCO_ListarAdmisionServicioConstanciaDetalle_Result>();
                List<WCO_ListarAdmisionReferencia_Result> lstRefe = new List<WCO_ListarAdmisionReferencia_Result>();
                lstPrimera = context.WCO_ListarAdmisionServicioConstanciaDetalle(ObjEntidad.IdAdmision, ObjEntidad.NroPeticion).ToList();
                if (lstPrimera.Count > 0)
                {
                    Nullable<int> iReturnValue = null;
                    if (ObjEntidad.IdAdmision > 0)
                    {
                        iReturnValue = ObjEntidad.IdAdmision;
                    }
                    else
                    {
                        iReturnValue = 0;
                    }
                    lstRefe = context.WCO_ListarAdmisionReferencia(iReturnValue).ToList();
                    foreach (WCO_ListarAdmisionServicioConstanciaDetalle_Result obj_Seleccionados in lstPrimera)
                    {
                        foreach (WCO_ListarAdmisionReferencia_Result obj_Sele in lstRefe)
                        {
                            if (obj_Seleccionados.CodigoComponente == obj_Sele.CodigoComponente)
                            {
                                WCO_ListarAdmisionServicioConstancia_Result ObjnUEVO = new WCO_ListarAdmisionServicioConstancia_Result();
                                ObjnUEVO.Cantidad = obj_Seleccionados.Cantidad;
                                ObjnUEVO.CLIENTE = obj_Seleccionados.CLIENTE;
                                ObjnUEVO.CodigoComponente = obj_Seleccionados.CodigoComponente;
                                ObjnUEVO.CorreoElectronico = obj_Seleccionados.CorreoElectronico;
                                ObjnUEVO.Descripcion = obj_Seleccionados.Descripcion;
                                ObjnUEVO.DesEstado = obj_Seleccionados.DesEstado;
                                ObjnUEVO.Persona = obj_Seleccionados.Persona;
                                ObjnUEVO.Documento = obj_Seleccionados.Documento;
                                ObjnUEVO.edad = obj_Seleccionados.edad;
                                ObjnUEVO.EMPRESA = obj_Seleccionados.EMPRESA;
                                ObjnUEVO.FechaAdmision = obj_Seleccionados.FechaAdmision;
                                ObjnUEVO.HistoriaClinica = obj_Seleccionados.HistoriaClinica;
                                ObjnUEVO.IdAdmision = obj_Seleccionados.IdAdmision;
                                ObjnUEVO.IdSede = obj_Seleccionados.IdSede;
                                ObjnUEVO.Linea = Convert.ToInt32(obj_Seleccionados.Linea);
                                ObjnUEVO.MEDICO = obj_Seleccionados.MEDICO;
                                ObjnUEVO.NombreCompleto = obj_Seleccionados.NombreCompleto;
                                ObjnUEVO.NombreEmpresa = obj_Seleccionados.NombreEmpresa;
                                ObjnUEVO.NroPeticion = obj_Seleccionados.NroPeticion;
                                ObjnUEVO.OrdenAtencion = obj_Seleccionados.OrdenAtencion;
                                ObjnUEVO.OrdenSinapa = obj_Seleccionados.OrdenSinapa;
                                ObjnUEVO.PROCEDENCIA = obj_Seleccionados.PROCEDENCIA;
                                ObjnUEVO.Precio = Convert.ToDecimal(obj_Seleccionados.Precio);
                                ObjnUEVO.Portal = obj_Seleccionados.Portal;
                                ObjnUEVO.redondeo = obj_Seleccionados.redondeo;
                                ObjnUEVO.Sexo = obj_Seleccionados.Sexo;
                                ObjnUEVO.Telefono = obj_Seleccionados.Telefono;
                                ObjnUEVO.TipoAtencion = obj_Seleccionados.TipoAtencion;
                                ObjnUEVO.TIPOADMISIONID = obj_Seleccionados.TIPOADMISIONID;
                                ObjnUEVO.TipoPaciente = obj_Seleccionados.TipoPaciente;
                                ObjnUEVO.TipoOrden = obj_Seleccionados.TipoOrden;
                                ObjnUEVO.Usuario = obj_Seleccionados.Usuario;
                                ObjnUEVO.DES = obj_Sele.DES;
                                ObjnUEVO.DescripcionProveedor = obj_Sele.DescripcionProveedor;
                                ObjnUEVO.CodigoAnalisis = obj_Sele.CodigoAnalisis;
                                ObjnUEVO.ObservacionAlta = obj_Seleccionados.ObservacionAlta;
                                ObjnUEVO.Contrato = obj_Seleccionados.Contrato;
                                lst.Add(ObjnUEVO);
                                break;
                            }
                        }
                    }
                }
            }
            return lst;
        }

        public List<WCO_ListarAdmisionServicioConstanciaDetalle_Result> ListadoAdmisionConstancia(WCO_ListarAdmisionServicio_Result ObjEntidad)
        {
            List<WCO_ListarAdmisionServicioConstanciaDetalle_Result> lst = new List<WCO_ListarAdmisionServicioConstanciaDetalle_Result>();
            using (var context = new BDComercialEntities())
            {
                Nullable<int> iReturnValue = null;
                if (ObjEntidad.IdAdmision > 0)
                {
                    iReturnValue = ObjEntidad.IdAdmision;
                }
                lst = context.WCO_ListarAdmisionServicioConstanciaDetalle(iReturnValue, ObjEntidad.NroPeticion).ToList();
            }
            return lst;
        }


        public DataTable ListadoConstancia(ADBE_AdmisionServicio objBEAdmisionServicio)
        {
            DataSet ds_Result = null;
            DataOperation dop_DataOperation = new DataOperation("WCO_ListarAdmisionServicioConstancia");
            Parameter[] prm_Params = new Parameter[2];
            prm_Params[0] = new Parameter("@pIdAdmision", objBEAdmisionServicio.PK.Admision.PK.IdAdmision);
            prm_Params[1] = new Parameter("@NroPeticion", objBEAdmisionServicio.PK.Admision.NroPeticion);
            dop_DataOperation.Parameters.AddRange(prm_Params);
            ds_Result = DataManager.ExecuteDataSet(DAT_Conexion.Co_ConnecPrecisa, dop_DataOperation);

            if (ds_Result == null || ds_Result.Tables.Count == 0)
            {
                return null;
            }
            return ds_Result.Tables[0];
        }





    }
}