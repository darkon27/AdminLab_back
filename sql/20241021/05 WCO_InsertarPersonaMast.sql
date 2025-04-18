USE [BDComercial]
GO
/****** Object:  StoredProcedure [dbo].[WCO_InsertarPersonaMast]    Script Date: 19/09/2024 17:52:40 ******/
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
				 case when @ptipopersona='J' then '' else @pApellidoPaterno + ' ' + isnull(@pApellidoMaterno,'') + ' ' + SUBSTRING(@pNombres,0,40) end  ,
				 case when @pTipoDocumento is not null then @pTipoDocumento else 'D' end,
				 @pApellidoPaterno, isnull(@pApellidoMaterno,''), 
				 case when @ptipopersona='J' then  '' else SUBSTRING(@pNombres,0,40) end,
				 case when @ptipopersona='J' then  ''  else	  @pApellidoPaterno + ' ' + isnull(@pApellidoMaterno,'') + ', ' + SUBSTRING(@pNombres,0,40) end ,
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
          ApellidoMaterno = isnull(@pApellidoMaterno,''),
          Nombres =  case when @ptipopersona='N' OR @ptipopersona='M' then   
			 @pNombres else
			 @pNombres end ,
          NombreCompleto =  case when @ptipopersona='N' OR @ptipopersona='M' then   
			 @pApellidoPaterno + ' ' + isnull(@pApellidoMaterno,'') + ', ' + SUBSTRING(@pNombres,0,40) else
			 SUBSTRING(@pNombres,0,90) end , 
          BUSQUEDA=  case when @ptipopersona='N' OR @ptipopersona='M' then   
			 @pApellidoPaterno + ' ' + isnull(@pApellidoMaterno,'') + ' ' + SUBSTRING(@pNombres,0,40) else
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

END;

