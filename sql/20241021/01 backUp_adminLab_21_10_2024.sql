---BACK up 21/10/2024

USE [BDComercial]
GO
/****** Object:  StoredProcedure [dbo].[WCO_InsertarPersonaMast]    Script Date: 21/10/2024 12:43:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[WCO_InsertarPersonaMast] (
/******   
  Autor:		Jordan Mateo
  Creación:    18/07/2014
    Autor:		Jordan Mateo
  	Modificación:   31/07/2018
  **********************************************************/
  @pApellidoPaterno   VARCHAR(40) = null ,
  @pApellidoMaterno   VARCHAR(40)= null ,
  @pNombres  VARCHAR(140)= null ,
  @pPerUsuarioCreacion   VARCHAR(50)= null ,
  @pDocumento  VARCHAR(20)= null ,
  @pEstadoCivil  CHAR(1)  ,
  @pFechaNacimiento  DATE ,
  @pSexo  CHAR(1)  ,
  @pCorreoElectronico  VARCHAR(50)= null ,
  @pTelefono  VARCHAR(15)= null ,
  @pSunatUbigeo  CHAR(10) ,
  @pDepartamento  VARCHAR(3) ,
  @pProvincia  VARCHAR(3) ,
  @pDireccion  VARCHAR(190) , 
  @pCelular  VARCHAR(40) ,
  @pPerIpCreacion  VARCHAR(20) ,
  @pCodigoHC  VARCHAR(20)= null ,
  @pPersonaAnt  VARCHAR(20)=null ,
  @pTipoPaciente int = null,
  @pCodigoAsegurado int = null,
  @pEdad int = null,
  @pTipoDocumento VARCHAR(1)=null,
  @ptipopersona char(1),
  @pestado char(1),
  @pCliente char(1),
  @pProveedor char(1),
  @pEmpleado char(1),
  @pOtro char(1),
  @pPersona  integer OUTPUT,
  @DocumentoFiscal VARCHAR(20),
  @DireccionReferencia VARCHAR(250),
  @Comentario VARCHAR(300),
  @DiaVencimiento int = null,
  @IndicadorRetencion  INT=NULL     )
AS

BEGIN
DECLARE @NUM INT
DECLARE @VAR INT

    SELECT @NUM=COUNT(*), @pPersona=MAX(Persona) FROM PersonaMast WITH(NOLOCK) WHERE Documento=@pDocumento AND  TipoDocumento=@pTipoDocumento

	IF @NUM=0 
		BEGIN
		
			SELECT @pPersona=(case when MAX(Persona)IS NULL then 1 else MAX(Persona) + 1 end) 
			FROM PersonaMast WITH(NOLOCK);
		     if @ptipopersona='J'
		     begin
		      	INSERT INTO  PersonaMast
				(persona, origen, BUSQUEDA , TIPODOCUMENTO , NombreCompleto , 
				 IngresoUsuario , IngresoFechaRegistro, 
				 Documento,DocumentoIdentidad,DocumentoFiscal, EstadoCivil ,FechaNacimiento , Sexo ,CorreoElectronico ,  
				 Telefono , DEPARTAMENTO , PROVINCIA , Celular , Direccion , CodigoPostal,
				 UltimoUsuario,UltimaFechaModif,edad, PersonaAnt,TipoPersona,Estado,EsCliente,EsEmpleado,
				 EsProveedor,EsOtro,DireccionReferencia,Comentario,DiaVencimiento,IndicadorRetencion)
				VALUES 
				 (@pPersona, 'LIMA', @pNombres ,@pTipoDocumento, @pNombres ,
				 @pPerUsuarioCreacion, SYSDATETIME() , 
				 @pDocumento,@pDocumento ,@DocumentoFiscal, @pEstadoCivil, @pFechaNacimiento, @pSexo, @pCorreoElectronico,
				 @pTelefono ,  @pDepartamento , @pProvincia, @pCelular, @pDireccion, @pSunatUbigeo,
				 @pPerUsuarioCreacion,SYSDATETIME(),@pEdad,@pPersonaAnt,@ptipopersona ,@pestado,@pCliente,
				 @pEmpleado,@pProveedor,@pOtro,@DireccionReferencia,@Comentario,@DiaVencimiento,@IndicadorRetencion) ; 
		     end
		     else
		     begin
		     	INSERT INTO  PersonaMast
				(persona, origen, BUSQUEDA , TIPODOCUMENTO ,
				 ApellidoPaterno ,ApellidoMaterno , Nombres , NombreCompleto , 
				 IngresoUsuario , IngresoFechaRegistro, 
				 Documento,DocumentoIdentidad,DocumentoFiscal, EstadoCivil ,FechaNacimiento , Sexo ,CorreoElectronico ,  
				 Telefono , DEPARTAMENTO , PROVINCIA , Celular , Direccion , CodigoPostal,
				 UltimoUsuario,UltimaFechaModif,edad, PersonaAnt,TipoPersona,Estado,EsCliente,EsEmpleado,
				 EsProveedor,EsOtro,DireccionReferencia,Comentario)
				VALUES 
				 (@pPersona, 'LIMA', 
				 case when @ptipopersona='J' then '' else @pApellidoPaterno + ' ' + @pApellidoMaterno + ' ' + SUBSTRING(@pNombres,0,40) end  ,
				 case when @pTipoDocumento is not null then @pTipoDocumento else 'D' end,
				 @pApellidoPaterno, @pApellidoMaterno, 
				 case when @ptipopersona='J' then  '' else SUBSTRING(@pNombres,0,40) end,
				 case when @ptipopersona='J' then  ''  else	@pApellidoPaterno + ' ' + @pApellidoMaterno + ', ' + SUBSTRING(@pNombres,0,40) end ,
				 @pPerUsuarioCreacion, SYSDATETIME() , @pDocumento,@pDocumento ,@DocumentoFiscal, @pEstadoCivil, @pFechaNacimiento, @pSexo, @pCorreoElectronico,
				 @pTelefono ,  @pDepartamento , @pProvincia, @pCelular, @pDireccion, @pSunatUbigeo,
				 @pPerUsuarioCreacion,SYSDATETIME(),@pEdad,@pPersonaAnt,@ptipopersona ,@pestado,@pCliente,
				 @pEmpleado,@pProveedor,@pOtro,@DireccionReferencia,@Comentario) ; 
		     end 

			  /*Ingresar Medico a EmpleadoMast*/
			  IF @pEmpleado ='S'
				if @pTipoDocumento='B'
					begin
						Insert into EmpleadoMast(Empleado,CompaniaSocio,Estado,FechaIngreso,UnidadNegocioAsignada,CMP,TipoTrabajador)
						values (@pPersona,'00000000','A',SYSDATETIME(),'LIMA',@pDocumento,'08');
					end
				else
					begin
						Insert into EmpleadoMast(Empleado,CompaniaSocio,Estado,FechaIngreso,UnidadNegocioAsignada)
						values (@pPersona,'00000000','A',SYSDATETIME(),'01');
					end
			  IF @pCliente='S'
				  if @ptipopersona='N'
					  begin
					   Insert into SS_GE_Paciente(IdPaciente,TipoPaciente, Edad, 
											CodigoHC, FechaIngreso, Estado, CodigoAsegurado)
					   Values (@pPersona, @pTipoPaciente,  year(SYSDATETIME())- year(@pFechaNacimiento),
										@pCodigoHC, getdate(), 1, @pCodigoHC);
					   update PersonaMast set EsPaciente='S' where persona=@pPersona;
					  end
				  else
					  begin
					  update PersonaMast set EsEmpresa='S' where persona=@pPersona;
					  end  				  	  
		 END
	 ELSE
		 BEGIN	 
	
			INSERT INTO WCO_CargaDuplicada (ApellidoPaterno,ApellidoMaterno,Nombres,TipoDocumento,Documento,FechaNacimiento,
				Sexo,CorreoElectronico,TelefonoFijo,Celular,Referencia,Observacion)
				VALUES (@pApellidoPaterno,@pApellidoMaterno,@pNombres,case when @pTipoDocumento is not null then @pTipoDocumento else 'D' end,
				@pDocumento,@pFechaNacimiento,@pSexo,@pCorreoElectronico,@pTelefono,@pCelular,@DireccionReferencia,'Ya existe la Persona' )	
	
			SELECT @VAR=COUNT(*) FROM SS_GE_Paciente WITH(NOLOCK) WHERE IdPaciente=@pPersona			
			IF @VAR=0
				BEGIN
					   Insert into SS_GE_Paciente(IdPaciente,TipoPaciente, Edad, 
							CodigoHC, FechaIngreso, Estado, CodigoAsegurado)
						Values (@pPersona, @pTipoPaciente,  year(SYSDATETIME())- year(@pFechaNacimiento),
						@pCodigoHC, getdate(), 1, @pCodigoHC);
				END
		 END

	EXEC	WCO_InsertarPersonaPassword @pPersona,@pDocumento,'01'
	--SELECT @NUM=COUNT(*)  FROM WCO_UsuarioWeb WITH(NOLOCK) WHERE IdPersona=@pPersona --and  TipoUserWeb<>3
		
	--IF @NUM=0 
	--	BEGIN
	--		INSERT WCO_UsuarioWeb (UserNameWeb,PasswordWeb,TipoUserWeb,estadoActualizacion,IdPersona,clasificadorMovimiento)
	--		VALUES	( @pDocumento ,dbo.fnCustomPass(8,'N'), 0 ,1,@pPersona,'01')				
	--	END	
END;

GO
/****** Object:  StoredProcedure [dbo].[WCO_ActualizarPersonaMast]    Script Date: 21/10/2024 12:45:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[WCO_ActualizarPersonaMast]
( 
/******    
  Autor: Antonio Martinez
  Creación:    31/05/2012
	Autor:			Jordan Mateo
	Modificación:    07/03/2021
	***********************************/
  @pPersona				INT ,
  @pApellidoPaterno		VARCHAR(40) ,
  @pApellidoMaterno		VARCHAR(40) ,
  @pNombres				VARCHAR(90) ,
  @pPerUltimoUsuario	VARCHAR(50) ,
  @pDocumento			VARCHAR(20)  ,
  @pEstadoCivil			CHAR(1)  ,
  @pFechaNacimiento		DATE ,
  @pSexo				CHAR(1)  ,
  @pCorreoElectronico	VARCHAR(50)  ,
  @pTelefono			VARCHAR(15)  ,
  @pDepartamento		VARCHAR(3) ,
  @pProvincia			VARCHAR(3) ,
  @pDireccion			VARCHAR(190) , 
  @pCelular				VARCHAR(15) ,
  @pSunatUbigeo			CHAR(10) , 
  @pPerUltimoIp			VARCHAR(20),
  @pEdad				int = null,
  @ptipopersona			char(1),
  @pPersonaAnt			VARCHAR(20)=null ,
  @pCliente				char(1),
  @pProveedor			char(1),
  @pEmpleado			char(1),
  @pOtro				char(1),
  @pestado				char(1),
  @pTipoDocumento		char(1),
  @DocumentoFiscal		VARCHAR(20),
  @DireccionReferencia	VARCHAR(250),
  @Comentario			VARCHAR(300),
  @DiaVencimiento		int = null,
  @IndicadorRetencion  INT=NULL
)
AS

BEGIN
DECLARE @as_mensaje Varchar(250) 
DECLARE @VAR INT
--BEGIN TRY


   UPDATE PersonaMast
      SET ApellidoPaterno = @pApellidoPaterno,
          ApellidoMaterno = @pApellidoMaterno,
          Nombres =  case when @ptipopersona='N' OR @ptipopersona='M' then   
			 @pNombres else
			 @pNombres end ,
          NombreCompleto =  case when @ptipopersona='N' OR @ptipopersona='M' then   
			 @pApellidoPaterno + ' ' + @pApellidoMaterno + ', ' + SUBSTRING(@pNombres,0,40) else
			 SUBSTRING(@pNombres,0,90) end , 
          BUSQUEDA=  case when @ptipopersona='N' OR @ptipopersona='M' then   
			 @pApellidoPaterno + ' ' + @pApellidoMaterno + ' ' + SUBSTRING(@pNombres,0,40) else
			 SUBSTRING(@pNombres,0,90) end ,      
          UltimoUsuario = @pPerUltimoUsuario,
          UltimaFechaModif = SYSDATETIME(),          
          Documento = @pDocumento,
          DocumentoIdentidad=@pDocumento,
          DocumentoFiscal=@DocumentoFiscal,
          EstadoCivil = @pEstadoCivil,
          FechaNacimiento = @pFechaNacimiento,
          Sexo = @pSexo,
          edad=@pEdad,
          TipoDocumento=@pTipoDocumento,
          CorreoElectronico = @pCorreoElectronico,
          Telefono = @pTelefono ,         
          DEPARTAMENTO=@pDepartamento ,
          PROVINCIA = @pProvincia,
          Direccion = @pDireccion,
          Celular = @pCelular,  
          CodigoPostal = @pSunatUbigeo,
          TipoPersona=@ptipopersona,
          Estado =@pestado ,
          EsCliente =@pCliente,
          EsEmpleado =@pEmpleado,
          EsProveedor=@pProveedor,
          EsOtro=@pOtro, 
          PersonaAnt=@pPersonaAnt,
          DireccionReferencia=@DireccionReferencia,
          Comentario=@Comentario , DiaVencimiento=@DiaVencimiento , 
			IndicadorRetencion  =@IndicadorRetencion
      WHERE Persona = @pPersona;
      print 'llego '
  		--SELECT TOP 1 @pPersona=Persona  FROM PersonaMast WITH(NOLOCK) WHERE Documento=@pDocumento			
		SELECT @VAR=COUNT(*) FROM SS_GE_Paciente WITH(NOLOCK) WHERE IdPaciente=@pPersona
				
		if @ptipopersona='N'
			BEGIN	
			IF @VAR=0
				BEGIN
					   Insert into SS_GE_Paciente(IdPaciente,TipoPaciente, Edad, FechaIngreso, Estado)
						Values (@pPersona, '3',  year(SYSDATETIME())- year(@pFechaNacimiento), getdate(), 1);
				END
			END
			  print 'llego B'
		  /*Ingresar Medico a EmpleadoMast*/
	  IF @pEmpleado ='S'
			BEGIN
			SELECT @VAR=COUNT(*) FROM EmpleadoMast WITH(NOLOCK) WHERE Empleado=@pPersona
				IF @VAR=0
					BEGIN						
						Insert into EmpleadoMast(Empleado,CompaniaSocio,Estado,FechaIngreso,UnidadNegocioAsignada)
						values (@pPersona,'00000000','A',SYSDATETIME(),'01');
					END
			END
				  print 'llego C'
      	SELECT @VAR=COUNT(*)  FROM WCO_UsuarioWeb WITH(NOLOCK) WHERE IdPersona=@pPersona and  TipoUserWeb<>3
		
		IF @VAR=0 
			BEGIN
				INSERT WCO_UsuarioWeb (UserNameWeb,PasswordWeb,TipoUserWeb,estadoActualizacion,IdPersona,clasificadorMovimiento)
				VALUES	( @pDocumento ,dbo.fnCustomPass(8,'N'), 0 ,1,@pPersona,'01')				
			END	
		ELSE
			BEGIN
				UPDATE 	WCO_UsuarioWeb SET UserNameWeb=@pDocumento WHERE IdPersona=@pPersona and  TipoUserWeb<>3
			END 
				  print @as_mensaje + 'llego D'
--END TRY
--BEGIN CATCH
--		--SET @as_mensaje = 'Linea: ' + ERROR_LINE() + ' Mensaje: ' + ERROR_MESSAGE() + @as_mensaje
--		SET @as_mensaje =' Mensaje: ' + ERROR_MESSAGE() + @as_mensaje
--		print @as_mensaje
--		RAISERROR(@as_mensaje, 10, 1);	
--END CATCH
END;


GO
/****** Object:  StoredProcedure [dbo].[WCO_InactivarTipoOperacion]    Script Date: 21/10/2024 12:48:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[WCO_InactivarTipoOperacion]
( 
/******    
  Autor: Isaac jurado
  Modificación:
    23/10/2012
  **********************************************************/
  @TipoOperacionId		int,
  @UsuarioModificacion	varchar(25),
  @IpModificacion		varchar(25)
  )
AS

BEGIN
   UPDATE WCO_TIPOPERACION
      SET TipEstado =2,
		  UsuarioModificacion = @UsuarioModificacion,
		  IpModificacion = @IpModificacion
      
      WHERE TipoOperacionId = @TipoOperacionId;
END;

GO
/****** Object:  UserDefinedFunction [dbo].[WCO_CalculoPrecio]    Script Date: 21/10/2024 12:50:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER FUNCTION [dbo].[WCO_CalculoPrecio](  
 @EmpresaId  int=null,   
 @PersonaId  int=null,   
 @ListaBase  int,   
 @ModeloServicio int=null,   
 @Componente  varchar(30),  
 @TipoAdmision int,  
 @TipoPaciente int)  
--Returns decimal(20,6)   
Returns varchar(800)  
AS  
Begin  
  DECLARE @PrecioBase  decimal(20,6)  
  DECLARE @FactorEmpresa decimal(20,6)  
  DECLARE @Formula     int  
  DECLARE @FactorEmpresaModelo decimal(20,6)  
  DECLARE @EmpresaCopago decimal(20,6)  
  DECLARE @PrecioPago  decimal(20,6)  
  DECLARE @PrecioPresente decimal(20,6)  
  DECLARE @PrecioPaciente decimal(20,6)  
  DECLARE @PrecioEmpresa decimal(20,6)  
  DECLARE @TipoFactor  VARCHAR(1)  
    
    
  IF @TipoAdmision = 1  
   BEGIN     
    
    SELECT   @PrecioBase=isnull(b.PrecioUnitarioLista,0.00),  
       @FactorEmpresa=(CASE WHEN a.TPAplicaFactor = 1  THEN a.TPFactor   
       ELSE 0 END ),@TipoFactor=b.TipoFactor,   
       @Formula=a.AplicaFormula  
     FROM  WCO_TIPOPERACION a  with(nolock)
       inner join CM_CO_ListaBaseComponente b with(nolock) 
      on a.IdListaBase=b.IdListaBase  
     WHERE (a.IdListaBase = @ListaBase)  
       AND (a.TIPOADMISIONID = @TipoAdmision)  
       AND (a.TipoPacienteiD = @TipoPaciente)  
       AND (b.CodigoComponente = @Componente)  
       AND (a.Persona = @EmpresaId)  
       AND (@PersonaId IS NULL OR A.IdSede =@PersonaId)  

    set @PrecioPago = @PrecioBase   
    IF @TipoFactor='J'  
     BEGIN  
     if @FactorEmpresa>0  
        Begin  
     -- Set @PrecioPago = @PrecioPago + (@PrecioPago * (@FactorEmpresa/100))  
      Set @PrecioPago = @PrecioPago * (@FactorEmpresa)  
        End  
     END  
         
    set  @PrecioPresente = @PrecioPago;   
    Set  @PrecioPaciente = @PrecioPago;  
    Set  @PrecioEmpresa  = @PrecioPago;  
  
   END  
   IF @TipoAdmision = 2  
   BEGIN     
    --IF (@PersonaId IS not NULL)  
    --  SELECT  @PrecioBase=isnull(CM_CO_ListaBaseComponente.PrecioUnitarioLista,0.00),  
    --    @FactorEmpresa=(CASE WHEN WCO_TIPOPERACION.TPAplicaFactor = 1  THEN WCO_TIPOPERACION.TPFactor ELSE 0 END ),  
    --    @FactorEmpresaModelo=0 ,@TipoFactor=CM_CO_ListaBaseComponente.TipoFactor,  
    --    @Formula=WCO_TIPOPERACION.AplicaFormula,  
    --    @EmpresaCopago = SS_SG_ContratoPolizaPlan.copago  
    --    FROM CM_CO_ListaBaseComponente CM_CO_ListaBaseComponente WITH(NOLOCK)  
    --    INNER JOIN  
    --    dbo.WCO_TIPOPERACION WCO_TIPOPERACION WITH(NOLOCK)  
    --    ON  WCO_TIPOPERACION.IdListaBase =  
    --     CM_CO_ListaBaseComponente.IdListaBase    
    --      INNER JOIN  
    --    dbo.SS_SG_Contrato SS_SG_Contrato WITH(NOLOCK)  
    --    ON  SS_SG_Contrato.IdContrato =  
    --     WCO_TIPOPERACION.TPContratoID  
    --    INNER JOIN  
    --    dbo.SS_SG_ContratoEmpresa SS_SG_ContratoEmpresa WITH(NOLOCK)  
    --    ON  SS_SG_Contrato.IdAseguradora =  
    --     SS_SG_ContratoEmpresa.IdEmpresaEmpleadora  
    --    AND SS_SG_Contrato.IdContrato =  
    --     SS_SG_ContratoEmpresa.IdContrato  
    --    INNER JOIN  
    --    dbo.SS_SG_ContratoPoliza SS_SG_ContratoPoliza WITH(NOLOCK)  
    --    ON  SS_SG_ContratoEmpresa.IdEmpresaEmpleadora =  
    --     SS_SG_ContratoPoliza.IdEmpresaEmpleadora  
    --    AND SS_SG_ContratoPoliza.IdContrato =  
    --     SS_SG_Contrato.IdContrato  
    --    INNER JOIN  
    --    dbo.SS_SG_ContratoPolizaPlan SS_SG_ContratoPolizaPlan WITH(NOLOCK)  
    --    ON  SS_SG_ContratoPolizaPlan.IdContrato =  
    --     SS_SG_ContratoPoliza.IdContrato  
    --    AND SS_SG_ContratoPolizaPlan.SecuencialContrato =  
    --    SS_SG_ContratoPoliza.Secuencial  
    --    INNER JOIN  
    --    dbo.SS_SG_ContratoPlanBeneficiario SS_SG_ContratoPlanBeneficiario WITH(NOLOCK)  
    --    ON  SS_SG_ContratoPlanBeneficiario.IdPolizaPlan =  
    --     SS_SG_ContratoPolizaPlan.IdPolizaPlan  
    --  WHERE   
    --     (WCO_TIPOPERACION.IdListaBase = @ListaBase)        
    --    AND (CM_CO_ListaBaseComponente.CodigoComponente = @Componente)  
    --    AND (WCO_TIPOPERACION.TipoPacienteiD = @TipoPaciente)  
    --    AND (@EmpresaId IS NULL OR WCO_TIPOPERACION.Persona = @EmpresaId)  
    --    AND (@PersonaId IS NULL OR SS_SG_ContratoPlanBeneficiario.IdPaciente = @PersonaId)  
    --else  
     SELECT  @PrecioBase=isnull(CM_CO_ListaBaseComponente.PrecioUnitarioLista,0.00),  
       @FactorEmpresa=(CASE WHEN WCO_TIPOPERACION.TPAplicaFactor = 1  THEN WCO_TIPOPERACION.TPFactor ELSE 0 END ),  
       @FactorEmpresaModelo=0 ,@TipoFactor=CM_CO_ListaBaseComponente.TipoFactor,  
       @Formula=WCO_TIPOPERACION.AplicaFormula,  
       @EmpresaCopago = SS_SG_ContratoPolizaPlan.copago         
       FROM  dbo.CM_CO_ListaBaseComponente CM_CO_ListaBaseComponente WITH(NOLOCK)  
        INNER JOIN  
        dbo.WCO_TIPOPERACION WCO_TIPOPERACION WITH(NOLOCK)  
        ON  WCO_TIPOPERACION.IdListaBase =  
         CM_CO_ListaBaseComponente.IdListaBase  
          INNER JOIN  
        dbo.SS_SG_Contrato SS_SG_Contrato WITH(NOLOCK)  
        ON  SS_SG_Contrato.IdContrato =  
         WCO_TIPOPERACION.TPContratoID  
        INNER JOIN  
        dbo.SS_SG_ContratoEmpresa SS_SG_ContratoEmpresa WITH(NOLOCK)  
        ON  SS_SG_Contrato.IdAseguradora =  
         SS_SG_ContratoEmpresa.IdEmpresaEmpleadora  
        AND SS_SG_Contrato.IdContrato =  
         SS_SG_ContratoEmpresa.IdContrato  
        INNER JOIN  
        dbo.SS_SG_ContratoPoliza SS_SG_ContratoPoliza WITH(NOLOCK)  
        ON  SS_SG_ContratoEmpresa.IdEmpresaEmpleadora =  
         SS_SG_ContratoPoliza.IdEmpresaEmpleadora  
        AND SS_SG_ContratoPoliza.IdContrato =  
         SS_SG_Contrato.IdContrato  
        INNER JOIN  
        dbo.SS_SG_ContratoPolizaPlan SS_SG_ContratoPolizaPlan WITH(NOLOCK)  
        ON  SS_SG_ContratoPolizaPlan.IdContrato =  
         SS_SG_ContratoPoliza.IdContrato  
        AND SS_SG_ContratoPolizaPlan.SecuencialContrato =  
        SS_SG_ContratoPoliza.Secuencial  
        WHERE WCO_TIPOPERACION.IdListaBase = @ListaBase     
        AND (WCO_TIPOPERACION.TipoPacienteiD = @TipoPaciente)  
        AND (CM_CO_ListaBaseComponente.CodigoComponente = @Componente)  
        AND (@EmpresaId IS NULL OR WCO_TIPOPERACION.Persona = @EmpresaId)         
        GROUP BY CM_CO_ListaBaseComponente.PrecioUnitarioLista,WCO_TIPOPERACION.TPFactor,  
       WCO_TIPOPERACION.TPAplicaFactor,SS_SG_ContratoPolizaPlan.copago,  
       AplicaFormula,CM_CO_ListaBaseComponente.TipoFactor  
     set @PrecioPago = @PrecioBase  
     set @PrecioPresente = @PrecioPago   
     IF @TipoFactor='J'  
      BEGIN  
      if @FactorEmpresa>0  
         Begin  
       Set @PrecioPago = @PrecioPago * @FactorEmpresa  
         End   
  
      if @FactorEmpresaModelo>0  
         Begin  
       Set @PrecioPago = @PrecioPago + (@PrecioPago * (@FactorEmpresaModelo/100))          
         End     
        set  @PrecioPresente = @PrecioPago;   
        if @Formula=2 --Porcentaje  
       begin  
       Set  @PrecioPaciente = @PrecioPago - (@PrecioPago * (@EmpresaCopago/100))       
       Set  @PrecioEmpresa  = @PrecioPago - (@PrecioPago * ((100-@EmpresaCopago)/100))  
       end  
      else  
       begin  
       Set  @PrecioPaciente = @PrecioPago        
       Set  @PrecioEmpresa  = @PrecioPago   
       end   
      END  
     ELSE  
      BEGIN    
       if @Formula=2 --Porcentaje  
        begin  
        Set  @PrecioPaciente = @PrecioPago - (@PrecioPago * (@EmpresaCopago/100))       
        Set  @PrecioEmpresa  = @PrecioPago - (@PrecioPago * ((100-@EmpresaCopago)/100))  
        end  
       else  
        begin  
        Set  @PrecioPaciente = @PrecioPago        
        Set  @PrecioEmpresa  = @PrecioPago   
        end         
      END       
   END     
  
   IF @TipoAdmision = 3  
    BEGIN  
    SELECT   @PrecioBase=isnull(CM_CO_ListaBaseComponente.PrecioUnitarioLista,0.00),  
       @Formula=WCO_TIPOPERACION.AplicaFormula,  
       @TipoFactor=CM_CO_ListaBaseComponente.TipoFactor,  
       @FactorEmpresa=(CASE WHEN WCO_TIPOPERACION.TPAplicaFactor = 1  THEN WCO_TIPOPERACION.TPFactor ELSE 0 END )        
      FROM    (   (   (   WCO_TIPOPERACION WCO_TIPOPERACION WITH(NOLOCK)  
           INNER JOIN  
           CM_CO_ListaBase CM_CO_ListaBase WITH(NOLOCK)  
           ON (WCO_TIPOPERACION.IdListaBase =  
            CM_CO_ListaBase.IdListaBase))  
          INNER JOIN  
          WCO_TIPOADMISION WCO_TIPOADMISION WITH(NOLOCK)  
          ON (WCO_TIPOPERACION.TIPOADMISIONID =  
           WCO_TIPOADMISION.TipoAdmisionId))  
         INNER JOIN  
         CM_CO_ListaBaseComponente CM_CO_ListaBaseComponente WITH(NOLOCK)  
         ON (CM_CO_ListaBaseComponente.IdListaBase =  
          CM_CO_ListaBase.IdListaBase))  
        INNER JOIN  
        CM_CO_Componente CM_CO_Componente WITH(NOLOCK)  
        ON (CM_CO_Componente.CodigoComponente =  
         CM_CO_ListaBaseComponente.CodigoComponente)  
     WHERE (WCO_TIPOPERACION.IdListaBase = @ListaBase)  
       AND (WCO_TIPOPERACION.TIPOADMISIONID = @TipoAdmision)  
       AND (WCO_TIPOPERACION.TipoPacienteiD = @TipoPaciente)  
       AND (CM_CO_ListaBaseComponente.CodigoComponente = @Componente)  
      
    set  @PrecioPago = @PrecioBase   
    set  @PrecioPresente = @PrecioPago;   
    Set  @PrecioPaciente = @PrecioPago;  
    Set  @PrecioEmpresa  = @PrecioPago;   
     IF @TipoFactor='J'  
      BEGIN   
        IF @FactorEmpresa>0  
           Begin  
             
         Set @PrecioPago = @PrecioPago * @FactorEmpresa  
           
           End   
         set  @PrecioPresente = @PrecioPago;   
         Set  @PrecioPaciente = @PrecioPago;  
         Set  @PrecioEmpresa  = @PrecioPago;  
        END  
      END  
  
 RETURN Convert(varchar,@PrecioPresente) +'|'+ Convert(varchar,@PrecioPaciente)+ '|'+ Convert(varchar,@PrecioEmpresa)  
 End
 
GO
/****** Object:  StoredProcedure [dbo].[WCO_ValidarOAExamenRest]    Script Date: 21/10/2024 12:59:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[WCO_ValidarOAExamenRest]    
/******       
 Autor: Jordan Mateo    
 Creación:    17/07/2015    
 **********************************************************/    
@IdPersona			int=null,    
@OrdenAtencion		varchar(20)=null,    
@IdSede				int=null,
@IdOrdenAtencion	int=null,
@IdTipoOperacion	int=null
AS    
     BEGIN    

	set transaction isolation level read uncommitted;  

	SELECT  @IdPersona=Persona FROM  WCO_TIPOPERACION WHERE TipoOperacionID= @IdTipoOperacion         
	SELECT A.NroPeticion,A.OrdenAtencion,B.IdOrdenAtencion ,B.Linea,C.CodigoHomologado Componente, b.EstadoProceso ,A.OrdenSinapa   
        FROM WCO_AdmisionServicio A WITH(NOLOCK)    
		INNER JOIN WCO_AdmisionServicioDetalle B WITH(NOLOCK)  	ON A.NroPeticion =B.NroPeticion   
		INNER JOIN WCO_TIPOPERACION O WITH(NOLOCK)    		ON O.TipoOperacionID=A.TipoOperacionId     
		INNER JOIN WCO_Homologacion C WITH(NOLOCK)    		ON B.CodigoComponente = C.CodigoComponente   AND  C.IdEmpresa=O.Persona   
	AND C.IdEmpresa = o.Persona     
	WHERE  B.Estadoproceso<>4 and A.Estado<>3    
    AND (@IdSede IS NULL OR A.IdSede = @IdSede)   
	--AND (A.OrdenAtencion =  @OrdenAtencion)    
	AND (@IdOrdenAtencion IS NULL or B.IdOrdenAtencion = @IdOrdenAtencion) 
	AND (@IdPersona IS NULL or O.Persona = @IdPersona)  and a.ClasificadorMovimiento='01'     
	END;

GO
/****** Object:  StoredProcedure [dbo].[WCO_ListarAdmisionServicioConstanciaDetalle]    Script Date: 21/10/2024 13:02:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
		   SAOA.ObservacionAlta, TIP.Observacion Contrato
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

