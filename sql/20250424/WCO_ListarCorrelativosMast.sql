
GO
/****** Object:  StoredProcedure [dbo].[WCO_ListarCorrelativosMast]    Script Date: 24/04/2025 11:53:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter PROCEDURE [dbo].[WCO_ListarCorrelativosMast](
/******   
  Autor:		Jordan Mateo
  Creación:    17/07/2014
  **********************************************************/

	  @CompaniaCodigo		varchar(15)=NULL ,
	  @TipoComprobante		varchar(2)=NULL ,
	  @Serie				varchar(4)=NULL ,
	  @SedCodigo			varchar(4)=NULL ,
	  @IdSede   			Int=null ,
	  @Estado				varchar(1)=NULL 
  )
AS
BEGIN      
	SELECT
    T0.*,
    CASE
        WHEN T0.Estado = 'A' THEN 'Activo'
        ELSE 'Inactivo'
    END ESTADOdesc,
    REPLACE(T1.DescripcionCorta,' ', '') [DescripcionCorta],
    T2.DescripcionLocal SedDescripcion,
    T2.IdEmpresa,
    T2.IdSede
	,T0.TipoComprobante AS TipoComprobanteDesc
	--ROW_NUMBER() OVER(ORDER BY T0.TipoComprobante, T0.Serie) AS Num,
	--

FROM
    CorrelativosMast T0 WITH(NOLOCK)
    INNER JOIN CompaniaMast T1 WITH(NOLOCK) ON T1.CompaniaCodigo + '00' = T0.CompaniaCodigo
    LEFT JOIN AC_Sucursal T2 WITH(NOLOCK) ON T2.Sucursal = T0.Sucursal --and T2.IdEmpresa=T1.Persona
WHERE
	T0.CompaniaCodigo  = CASE ISNULL(@CompaniaCodigo,'') WHEN '' THEN T0.CompaniaCodigo ELSE ISNULL(@CompaniaCodigo,'') END AND
	T0.TipoComprobante  = CASE ISNULL(@TipoComprobante,'') WHEN '' THEN T0.TipoComprobante ELSE ISNULL(@TipoComprobante,'') END AND 
	T0.Sucursal  = CASE ISNULL(@SedCodigo,'') WHEN '' THEN T0.Sucursal ELSE ISNULL(@SedCodigo,'') END AND
	T0.Serie  = CASE ISNULL(@Serie,'') WHEN '' THEN T0.Serie ELSE ISNULL(@Serie,'') END AND
	T0.Estado  = CASE ISNULL(@Estado,'') WHEN '' THEN T0.Estado ELSE ISNULL(@Estado,'') END
ORDER BY 1,2,3;


END;

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

---select * from WCO_Sede WHERE IDEM