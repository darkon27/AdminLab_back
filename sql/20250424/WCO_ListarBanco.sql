USE [BDComercial]
GO
/****** Object:  StoredProcedure [dbo].[WCO_ListarBanco]    Script Date: 24/04/2025 15:21:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[WCO_ListarBanco] 
    @Banco				VARCHAR(3)=null,
    @Estado				VARCHAR(1)=null,
	@DescripcionCorta	VARCHAR(20)=null
AS
BEGIN
	SELECT  * FROM Banco WITH(NOLOCK)	
	WHERE (@Banco IS NULL OR Banco  =@Banco)
	AND	  (@Estado IS NULL OR Estado =@Estado)

END  