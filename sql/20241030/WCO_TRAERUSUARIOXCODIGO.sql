
GO

ALTER PROCEDURE [dbo].[WCO_TRAERUSUARIOXCODIGO]
/******   
  Autor:		Jordan Mateo
  Creaci�n:    16/11/2012
  **********************************************************/
@USUARIO		varchar(20)
AS
      BEGIN      
          SELECT usu.Usuario AS USUARIO,NOMBRE,EXPIRARPASSWORDFLAG,usu.ESTADO,usu.ULTIMOUSUARIO,
				 usu.ULTIMAFECHAMODIF,PERSONA,TIPOUSUARIO,PERFIL,IdSede,'' as Clave,usu.Clave Psw,ClasificadorMovimiento
		  FROM Usuario usu
          join	 SeguridadPerfilUsuario SU
          on	 usu.Usuario = su.Usuario
          left join   WCO_SedeUsuario SDU
          on	 usu.Usuario = SDU.Usuario
          WHERE  usu.Usuario=@USUARIO;         
END;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      