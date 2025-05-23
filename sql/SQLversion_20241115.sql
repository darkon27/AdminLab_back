USE [Precisa]
GO

GO

ALTER PROCEDURE [dbo].[WCO_ListarAprobadores](
/******   
  Autor:		Jordan Mateo
  Creación:    17/07/2014
  **********************************************************/
  @IdAprobador			int =null , 
  @TipoDescuento		int =null ,
  @IdUsuario			char(20) ,
  @Estado				int =null ,
  @UneuNegocioId		int =null
  )
AS
BEGIN      
	SELECT A.*,U.Clave,P.NombreCompleto, case when A.Estado=1 then 'Activo' else 'Inactivo' end ESTADOdesc FROM WCO_Aprobadores A
	INNER JOIN Usuario U ON A.IdUsuario=U.Usuario 
	INNER JOIN PersonaMast P ON U.Persona =P.Persona 
	WHERE
		(@IdAprobador = 0 OR @IdAprobador IS NULL OR  A.IdAprobador=@IdAprobador)
	AND (@TipoDescuento = 0 OR @TipoDescuento IS NULL OR  A.TipoDescuento=@TipoDescuento)
	AND (@IdUsuario IS NULL OR  A.IdUsuario=@IdUsuario)
	AND (@UneuNegocioId = 0 OR @UneuNegocioId IS NULL OR  A.UneuNegocioId=@UneuNegocioId)
	AND	(@Estado IS NULL OR A.Estado = @Estado)
END;

