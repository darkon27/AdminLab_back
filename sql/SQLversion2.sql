GO

CREATE PROCEDURE [dbo].[WCO_EliminarUsuarioSedeMasivo]
/******   
  Autor:		Jordan Mateo
  Creación:    17/10/2012
  **********************************************************/
@usuario				VARCHAR(25),
@IdSede					INT=NULL
AS
      BEGIN      
          DELETE FROM WCO_SedeUsuario 
          WHERE   Usuario = @usuario;         
      END;
GO

ALTER PROCEDURE [dbo].[WCO_ListarModeloServicio] 
    @UneuNegocioId			INT=NULL,
    @ModeloServicioId		INT=NULL,
    @MosDescripcion			VARCHAR(150)=NULL,
    @MosEstado				INT=NULL,
    @TipoOperacionId		INT=NULL,
    @TIPOADMISIONID			INT=NULL,
    @TipoPacienteId			INT=NULL
AS
BEGIN

	if @ModeloServicioId=0
	begin
		set @ModeloServicioId= null
	end

	if @UneuNegocioId=0
	begin
		set @UneuNegocioId= null
	end

	if @TipoOperacionId=0
	begin
		set @TipoOperacionId= null
	end
	SELECT  MS.*,
	case when P.NombreCompleto IS null then 'PARTICULAR' else P.NombreCompleto end  empresa,
			case MosEstado when 1 then 'Activo' else 'Inactivo' end EstadoDesc,			
			TPA.Descripcion,TAD.AdmDescripcion,TP.IdListaBase,sed.SedDescripcion, TP.TIPOADMISIONID	, TP.TipoPacienteId		
	FROM   WCO_ModeloServicio MS
	JOIN   WCO_TIPOPERACION TP 	ON		MS.TipoOperacionId = TP.TipoOperacionId 
	LEFT JOIN   PersonaMast P	ON		p.Persona = TP.Persona
	LEFT JOIN WCO_TIPOPACIENTE TPA	ON		TP.TipoPacienteId=TPA.TipoPacienteId
	LEFT JOIN WCO_TIPOADMISION TAD	ON		TP.TIPOADMISIONID=TAD.TIPOADMISIONID 
	LEFT JOIN WCO_Sede  sed	ON		TP.IdSede=sed.IdSede 	
	WHERE (@UneuNegocioId IS NULL OR  MS.UneuNegocioId = @UneuNegocioId)
	AND	  (@ModeloServicioId IS NULL OR ModeloServicioId = @ModeloServicioId)
	AND	  (@MosDescripcion IS NULL	OR upper(MosDescripcion) like '%'+@MosDescripcion+'%')
	AND	  (@MosEstado IS NULL OR MosEstado = @MosEstado)
	AND   (@TipoOperacionId IS NULL OR MS.TipoOperacionId = @TipoOperacionId)
	AND   (@TipoPacienteId IS NULL OR @TipoPacienteId=0 OR TP.TipoPacienteId = @TipoPacienteId)
	AND   (@TIPOADMISIONID IS NULL OR @TIPOADMISIONID=0 OR TP.TIPOADMISIONID = @TIPOADMISIONID)
END


GO

ALTER PROCEDURE [dbo].[WCO_ListarAutorizacionBeneficiario]
/******   
  Autor:	   Jordan Mateo
  Creación:    17/10/2012
  **********************************************************/
    @UneuNegocioId          INT=NULL,
	@FechaInicio			datetime,
	@FechaFin				datetime ,	
	@IdTitularAutorizado	int=NULL ,		/** IdPersona  Paciente **/
	@IdPaciente				int=NULL ,		/** Codigo Omega		**/
	@Estado					int=NULL 
AS
      BEGIN      
	  
    SELECT AUTORI.*,TITULAR.NombreCompleto Persona,PACIENTE.NombreCompleto Paciente, 
          case AUTORI.Estado when 1 then 'Activo' else 'Inactivo' end EstadoDesc
          FROM WCO_AutorizacionBeneficiario AUTORI 
          INNER JOIN PersonaMast TITULAR        ON AUTORI.IdTitularAutorizado=TITULAR.Persona
          INNER JOIN PersonaMast PACIENTE       ON AUTORI.IdPaciente=PACIENTE.Persona          
			WHERE   (@IdTitularAutorizado=0 OR @IdTitularAutorizado IS NULL OR IdTitularAutorizado = @IdTitularAutorizado)
				AND (@UneuNegocioId=0 OR @UneuNegocioId IS NULL OR UneuNegocioId = @UneuNegocioId)
				AND (@IdPaciente=0 OR @IdPaciente IS NULL OR IdPaciente = @IdPaciente)
				AND (@Estado=0 OR @Estado IS NULL OR AUTORI.Estado = @Estado) ;         
      END;



GO

ALTER procedure [dbo].[WCO_ListarComponentesdeListaB]
(@idlista int= null,
@Codigo varchar(25)=null,
@Nombre varchar(40)=null,
@estado int=null,
@IdClasificacion INT=NULL)
as
begin

select CM_CO_ListaBaseComponente.IdListaBase,
       CM_CO_ListaBase.Nombre  ,
       CM_CO_ListaBaseComponente.TipoComponente,
       CM_CO_ListaBaseComponente.CodigoComponente CODIGOCOMPONENTE,
       CM_CO_Componente.Nombre DESCRIPCION,
       CM_CO_ListaBaseComponente.Periodo,
       CM_CO_ListaBaseComponente.Moneda,
       CM_CO_ListaBaseComponente.PrecioUnitarioLista,
       CM_CO_ListaBaseComponente.PrecioUnitarioLista Valor,
       CM_CO_ListaBaseComponente.PrecioUnitarioListaLocal,
       CM_CO_ListaBaseComponente.PrecioUnitarioBase,
       CM_CO_ListaBaseComponente.PrecioUnitarioBaseLocal,
       CM_CO_ListaBaseComponente.FechaValidezInicio,
       CM_CO_ListaBaseComponente.FechaValidezFin,
	   CM_CO_ListaBaseComponente.Estado,
	   CM_CO_Componente.IdClasificacion,
       case when CM_CO_ListaBaseComponente.Estado=1 then 'Activo' else 'Inactivo' end ESTADOdesc,
       CM_CO_ListaBaseComponente.TipoFactor,
	   CM_CO_ListaBaseComponente.Factor,
	   CM_CO_ListaBaseComponente.IndicadorFactor,
	   CM_CO_ListaBaseComponente.IndicadorPrecioCero,
	   CM_CO_ListaBaseComponente.FactorCosto,
	   CM_CO_ListaBaseComponente.PrecioCosto,   
	   CM_CO_ListaBaseComponente.PrecioKairos,
	   CM_CO_ListaBaseComponente.FactorKairos,
	   CM_CO_Componente.UsuarioCreacion,
	   CM_CO_Componente.FechaCreacion,
	   CM_CO_Componente.UsuarioModificacion,
	   CM_CO_Componente.FechaModificacion
       from CM_CO_ListaBaseComponente 
	   inner join CM_CO_ListaBase        on CM_CO_ListaBaseComponente.IdListaBase=CM_CO_ListaBase.IdListaBase
       inner join CM_CO_Componente       on CM_CO_ListaBaseComponente.CodigoComponente=CM_CO_Componente.CodigoComponente
       left join CM_CO_ClasificacionComponente        on CM_CO_Componente.IdClasificacion=CM_CO_ClasificacionComponente.IdClasificacion 
       where
       (@idlista IS NULL OR CM_CO_ListaBase.IdListaBase =@idlista) 
       AND(@Codigo IS NULL OR CM_CO_ListaBaseComponente.CodigoComponente LIKE '%' + @Codigo + '%')
       and (@Nombre IS NULL	OR upper(CM_CO_Componente.Nombre) like '%'+@Nombre+'%')
	   AND (@Estado IS NULL OR CM_CO_ListaBaseComponente.Estado = @Estado)	
	   AND (@IdClasificacion IS NULL OR  CM_CO_Componente.IdClasificacion = @IdClasificacion)
	   and ( CM_CO_ClasificacionComponente.IdClasificacionPadre=151)  
end;
GO


ALTER PROCEDURE [dbo].[WCO_ListarCompaniaMast]
/******   
  Autor:		Jordan Mateo
  Creación:    17/07/2014
  **********************************************************/
	@CompaniaCodigo		VARCHAR(15)=NULL,
	@DescripcionCorta	VARCHAR(30)=NULL,
	@DocumentoFiscal	VARCHAR(20)=NULL,
	@Estado				CHAR(1)=NULL   
AS
      BEGIN      
        SELECT CompaniaMast.*,WCO_Unegocio.UneuNegocioId,WCO_Unegocio.CompaniaCodigo Compania,
		EMP.Documento RUC,EMP.DEPARTAMENTO CodDep , EMP.PROVINCIA CodPro, EMP.CodigoPostal CodDis,EMP.SUNATUbigeo Ubigeo,
		case when CompaniaMast.Estado='A' then 'Activo' else 'Inactivo' end ESTADOdesc 
		FROM CompaniaMast
        INNER JOIN WCO_Unegocio           WITH(NOLOCK) ON CompaniaMast.CompaniaCodigo+'00'=WCO_Unegocio.CompaniaCodigo
		LEFT JOIN PERSONAMAST EMP		  WITH(NOLOCK) ON EMP.Persona = CompaniaMast.Persona
		LEFT JOIN PERSONAMAST REPRE		  WITH(NOLOCK) ON REPRE.Documento = CompaniaMast.RepresentanteLegal AND EMP.TipoDocumento='D'
		WHERE
		    (@CompaniaCodigo IS NULL OR  CompaniaMast.CompaniaCodigo=@CompaniaCodigo)
		AND (@DescripcionCorta IS NULL OR CompaniaMast.DescripcionCorta LIKE '%'+@DescripcionCorta+'%')
		AND (@DocumentoFiscal IS NULL OR CompaniaMast.DocumentoFiscal LIKE '%'+@DocumentoFiscal+'%')
		AND	(@Estado IS NULL OR CompaniaMast.Estado = @Estado)
      END;
 
