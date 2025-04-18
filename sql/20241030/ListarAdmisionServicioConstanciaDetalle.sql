
GO

ALTER   PROCEDURE [dbo].[WCO_ListarAdmisionServicioConstanciaDetalle]
/******   
  Autor:		Jordan Mateo
  Creación:    17/10/2012
  **********************************************************/
@pIdAdmision		int=null,
@NroPeticion		varchar(15)=null 
AS
   BEGIN
           
	SELECT ADSER.IdAdmision,ADDET.IdAdmServicio Linea,CC.CodigoComponente,ADSER.NroPeticion,PER.NombreCompleto, CC.Nombre Descripcion,
		   CONVERT(int,Cantidad) Cantidad,TIP.TipoOperacionID,ADSER.Persona,SED.FlatImpresion  IdSede,TIP.TIPOADMISIONID, PER.Sexo,DBO.ObtenerEdad(PER.FechaNacimiento,GETDATE()) edad,
		   isnull(PER.Telefono,'')Telefono,MED.Busqueda MEDICO,CASE WHEN PERCLI.NombreCompleto IS NULL THEN 'PARTICULAR' ELSE  PERCLI.NombreCompleto END  CLIENTE,ESP.Descripcion PROCEDENCIA,
		   ADSER.HistoriaClinica, PEREMP.NombreCompleto EMPRESA,USU.UserNameWeb Usuario,PER.CorreoElectronico,ADSER.OrdenAtencion,
		   CASE WHEN USU.estadoActualizacion=1 THEN USU.PasswordWeb ELSE '' END Clave,PER.Documento ,'' CodigoAnalisis,'' DescripcionProveedor,'' DES,
		    CASE WHEN AB.AplicaTitular = 1 AND TIP.TIPOADMISIONID=3  THEN  isnull( ROUND(DBO.WCO_CalculoPrecioCobrar(AB.idempresa,TIP.IdSede,TIP.IdListaBase,NULL,ADDET.CodigoComponente,2,6,2),2),0)
		 ELSE  isnull( ROUND(DBO.WCO_CalculoPrecioCobrar(TIP.Persona,TIP.IdSede,TIP.IdListaBase,NULL,ADDET.CodigoComponente,TIP.TIPOADMISIONID,TIP.TipoPacienteId,2),2),0) END XPrecio,
		 case when LBC.TipoFactor='J' 
		then ROUND(isnull(LBC.PrecioUnitarioLista,0)*isnull(LBC.Factor,1)*isnull(TIP.TPFactor,1),2) else ROUND( LBC.PrecioUnitarioLista,2) end Precio,
		   CONVERT(VARCHAR,ADSER.FechaAdmision,103)FechaAdmision,ASCLI.NombreEmpresa,isnull(ADSER.OrdenSinapa ,'') OrdenSinapa,
		   TPAC.Descripcion TipoPaciente,TMORD.Descripcion TipoOrden,TMATE.Descripcion TipoAtencion, CMV.Portal  ,isnull(ADSER.redondeo,0.0)redondeo,TMD.Nombre DesEstado,ADSER.cama,
		   SAOA.ObservacionAlta, Con.CodigoContrato Contrato
    FROM WCO_AdmisionServicioDetalle ADDET WITH(NOLOCK)   
     INNER JOIN  WCO_AdmisionServicio ADSER WITH(NOLOCK)		ON ADSER.IdAdmision=ADDET.IdAdmision and ADSER.NroPeticion=ADDET.NroPeticion
     INNER JOIN SS_AD_OrdenAtencioN SAOA WITH(NOLOCK)			ON ADSER.IdAdmision=SAOA.IdOrdenAtencion
     INNER JOIN PersonaMast PER WITH(NOLOCK)					ON PER.Persona= ADSER.Persona
	 LEFT JOIN GE_ClasificadorMovimiento CMV WITH(NOLOCK)		ON ADSER.ClasificadorMovimiento=CMV.ClasificadorMovimiento
     LEFT JOIN WCO_MEDICO MED WITH(NOLOCK)						ON MED.MedicoId=ADSER.MedicoId
	 LEFT JOIN SS_GE_Especialidad ESP WITH(NOLOCK)				ON ESP.IdEspecialidad=SAOA.Especialidad
     INNER JOIN CM_CO_TablaMaestroDetalle TMD WITH(NOLOCK)      ON TMD.Codigo=ADDET.Estado       AND TMD.IdTablaMaestro=124  
     INNER JOIN WCO_TIPOPERACION TIP WITH(NOLOCK)				ON TIP.TipoOperacionID=ADSER.TipoOperacionId
     INNER JOIN WCO_TIPOPACIENTE TPAC WITH(NOLOCK)				ON TIP.TipoPacienteId =TPAC.TipoPacienteId
	 INNER JOIN WCO_SEDE		SED WITH(NOLOCK)				ON SED.IdSede =ADSER.IdSede
	 INNER JOIN CM_CO_Componente CC WITH(NOLOCK) 			ON  ADDET.CodigoComponente = CC.CodigoComponente		
	INNER JOIN CM_CO_ListaBaseComponente LBC WITH(NOLOCK) 	ON  LBC.CodigoComponente = CC.CodigoComponente		AND LBC.Estado=	CC.Estado	 and TIP.IdListaBase=LBC.IdListaBase
	LEFT JOIN SS_SG_Contrato Con WITH(NOLOCK)				ON  Con.IdContrato = TIP.TPContratoID
	LEFT JOIN SS_SG_ContratoEmpresa ConEmp WITH(NOLOCK)		ON  Con.IdAseguradora = ConEmp.IdEmpresaEmpleadora	AND Con.IdContrato = ConEmp.IdContrato
	LEFT JOIN SS_SG_ContratoPoliza Pol WITH(NOLOCK)			ON  ConEmp.IdEmpresaEmpleadora = Pol.IdEmpresaEmpleadora	AND Pol.IdContrato =	Con.IdContrato
	LEFT JOIN SS_SG_ContratoPolizaPlan Pla WITH(NOLOCK)		ON  Pla.IdContrato = Pol.IdContrato	AND Pla.SecuencialContrato = Pol.Secuencial		
     LEFT JOIN PersonaMast PERCLI WITH(NOLOCK)					ON PERCLI.Persona= TIP.Persona      
     LEFT JOIN PersonaMast PEREMP WITH(NOLOCK)					ON PEREMP.Persona= ADSER.IdEmpresaPaciente
     LEFT JOIN WCO_UsuarioWeb USU WITH(NOLOCK)					ON USU.IdPersona= ADSER.Persona   AND USU.tipoUserWeb=0  and USU.ClasificadorMovimiento =  ADSER.ClasificadorMovimiento
     LEFT JOIN AseguradorasClientes ASCLI WITH(NOLOCK)			ON ASCLI.IdAseguradora = ADSER.IdAseguradora  
     INNER JOIN CM_CO_TablaMaestroDetalle TMORD WITH(NOLOCK)    ON TMORD.Codigo=ADSER.TipoOrden   AND TMORD.IdTablaMaestro=135
     INNER JOIN CM_CO_TablaMaestroDetalle TMATE WITH(NOLOCK)    ON TMATE.Codigo=SAOA.TipoAtencion AND TMATE.IdTablaMaestro=133
	  LEFT JOIN WCO_AutorizacionBeneficiario AB WITH(NOLOCK)      ON AB.IdPaciente = ADSER.Persona      AND AB.Estado=1 AND AB.FechaFin >= GETDATE()  
     WHERE  --	ADSER.IdAdmision  =   1259374       
			 ( @pIdAdmision is null or ADSER.IdAdmision =@pIdAdmision)   
      AND	 ( @NroPeticion is null or ADSER.NroPeticion =@NroPeticion)

      ORDER BY CC.Nombre ASC
END; 

