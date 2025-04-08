

GO
DROP TABLE WCO_SedeCompartida
GO
CREATE TABLE [dbo].[WCO_SedeCompartida](
	[IdSede] [int] NOT NULL,
	[IdEmpresa] [int] NOT NULL,
	[IdSedeCompartida] [int] NOT NULL,
	 CONSTRAINT [PK_WCO_SedeCompartida] PRIMARY KEY CLUSTERED 
(
	[IdSede] ASC,
	[IdSedeCompartida] ASC
)
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[WCO_SedeCompartida]  WITH CHECK ADD  CONSTRAINT [R_C3] FOREIGN KEY([IdSede])
REFERENCES [dbo].[WCO_Sede] ([IdSede])
GO

ALTER TABLE [dbo].[WCO_SedeCompartida] CHECK CONSTRAINT [R_C3]
GO

ALTER TABLE [dbo].[WCO_SedeCompartida]  WITH CHECK ADD  CONSTRAINT [R_C4] FOREIGN KEY([IdEmpresa])
REFERENCES [dbo].[PersonaMast] ([Persona])
GO

ALTER TABLE [dbo].[WCO_SedeCompartida] CHECK CONSTRAINT [R_C4]
GO


CREATE PROCEDURE [dbo].[WCO_ListarSedeCompartida] 
    @IdSede				INT,
	@IdEmpresa			INT,
	@IdSedeCompartida	INT

AS
BEGIN

	SELECT   TMD.SedCodigo,TMD.SedDescripcion, TM.*
	FROM WCO_SedeCompartida TM WITH(NOLOCK)
	JOIN WCO_Sede TMD WITH(NOLOCK)	ON	 TMD.IdSede = TM.IdSedeCompartida	
	WHERE 
		( @IdSede IS NULL or   TM.IdSede = @IdSede)
	AND ( @IdEmpresa IS NULL or   TM.IdEmpresa = @IdEmpresa)
	AND ( @IdSedeCompartida IS NULL or   TM.IdSedeCompartida = @IdSedeCompartida)

END

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
	SELECT A.NroPeticion,A.OrdenAtencion,B.IdOrdenAtencion ,B.Linea,C.CodigoHomologado Componente, b.Estado ,A.OrdenSinapa   
        FROM WCO_AdmisionServicio A WITH(NOLOCK)    
		INNER JOIN WCO_AdmisionServicioDetalle B WITH(NOLOCK)  	ON A.NroPeticion =B.NroPeticion   
		INNER JOIN WCO_TIPOPERACION O WITH(NOLOCK)    		ON O.TipoOperacionID=A.TipoOperacionId     
		INNER JOIN WCO_Homologacion C WITH(NOLOCK)    		ON B.CodigoComponente = C.CodigoComponente    
	AND C.IdEmpresa = o.Persona     
	WHERE  B.Estado<>4 and A.Estado<>3    
    AND (@IdSede IS NULL OR A.IdSede = @IdSede)   
	--AND (A.OrdenAtencion =  @OrdenAtencion)    
	AND (@IdOrdenAtencion IS NULL or B.IdOrdenAtencion = @IdOrdenAtencion) 
	AND (@IdPersona IS NULL or O.Persona = @IdPersona)  and a.ClasificadorMovimiento='01'     
	END;