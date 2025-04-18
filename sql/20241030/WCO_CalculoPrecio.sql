GO
ALTER PROCEDURE [dbo].[WCO_InactivarTipoOperacion]
( 
/******    
  Autor: Isaac jurado
  Creacion:   23/10/2012
  Autor: Jordan Mateo
  Modificación:   23/07/2024
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

   UPDATE SS_SG_Contrato set Estado=2 ,  UsuarioModificacion = @UsuarioModificacion where IdContrato in ( SELECT  TPContratoID FROM WCO_TIPOPERACION  WHERE TPContratoID IS NOT NULL AND TipoOperacionId = @TipoOperacionId )

END;

GO
/****** Object:  UserDefinedFunction [dbo].[WCO_CalculoPrecio]    Script Date: 30/07/2024 16:32:09 ******/
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


     SELECT  @PrecioBase=isnull(CM_CO_ListaBaseComponente.PrecioUnitarioLista,0.00),  
       @FactorEmpresa=(CASE WHEN WCO_TIPOPERACION.TPAplicaFactor = 1  THEN WCO_TIPOPERACION.TPFactor ELSE 0 END ),  
       @FactorEmpresaModelo=0 ,@TipoFactor=CM_CO_ListaBaseComponente.TipoFactor,  
       @Formula=WCO_TIPOPERACION.AplicaFormula,  
       @EmpresaCopago = SS_SG_ContratoPolizaPlan.copago         
       FROM  dbo.CM_CO_ListaBaseComponente CM_CO_ListaBaseComponente WITH(NOLOCK)  
        INNER JOIN dbo.WCO_TIPOPERACION WCO_TIPOPERACION WITH(NOLOCK)  
			ON  WCO_TIPOPERACION.IdListaBase = CM_CO_ListaBaseComponente.IdListaBase  
        INNER JOIN dbo.SS_SG_Contrato SS_SG_Contrato WITH(NOLOCK)  
			ON  SS_SG_Contrato.IdContrato = WCO_TIPOPERACION.TPContratoID  
        INNER JOIN dbo.SS_SG_ContratoEmpresa SS_SG_ContratoEmpresa WITH(NOLOCK)  
			ON  SS_SG_Contrato.IdAseguradora = SS_SG_ContratoEmpresa.IdEmpresaEmpleadora  
			AND SS_SG_Contrato.IdContrato = SS_SG_ContratoEmpresa.IdContrato  
        INNER JOIN dbo.SS_SG_ContratoPoliza SS_SG_ContratoPoliza WITH(NOLOCK)  
			ON  SS_SG_ContratoEmpresa.IdEmpresaEmpleadora = SS_SG_ContratoPoliza.IdEmpresaEmpleadora  
			AND SS_SG_ContratoPoliza.IdContrato = SS_SG_Contrato.IdContrato  
        INNER JOIN dbo.SS_SG_ContratoPolizaPlan SS_SG_ContratoPolizaPlan WITH(NOLOCK)  
			ON  SS_SG_ContratoPolizaPlan.IdContrato = SS_SG_ContratoPoliza.IdContrato  
			AND SS_SG_ContratoPolizaPlan.SecuencialContrato = SS_SG_ContratoPoliza.Secuencial  
        WHERE WCO_TIPOPERACION.IdListaBase = @ListaBase     
        AND (WCO_TIPOPERACION.TipoPacienteiD = @TipoPaciente)  
        AND (CM_CO_ListaBaseComponente.CodigoComponente = @Componente)  
        AND (@EmpresaId IS NULL OR WCO_TIPOPERACION.Persona = @EmpresaId)        
		AND WCO_TIPOPERACION.TipEstado =1
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
  
  