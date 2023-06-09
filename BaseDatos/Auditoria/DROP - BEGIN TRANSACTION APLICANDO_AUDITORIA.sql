BEGIN

	BEGIN TRY

		BEGIN TRANSACTION APLICANDO_AUDITORIA

			DECLARE @ListTable TABLE (
				ID INT,
				ESQUEMA NVARCHAR(MAX),
				TABLA NVARCHAR(MAX)
			)

			INSERT INTO @ListTable
			SELECT 
				OBJECT_ID AS [ID],
				SCHEMA_NAME(schema_id) AS [schema],
				name AS [table]
			FROM sys.tables
			ORDER BY [schema], [table] ASC

			DECLARE @Pointer INT = (SELECT count(*) FROM @ListTable);
			DECLARE @ID INT, @ESQUEMA NVARCHAR(MAX), @TABLA NVARCHAR(MAX);

			WHILE  @Pointer > 0
			BEGIN
				SELECT TOP 1 @ID = ID, @ESQUEMA = ESQUEMA, @TABLA = TABLA FROM @ListTable ORDER BY ESQUEMA, TABLA ASC

				--ELIMINAR COLUMNAS DE AUDITORIA

				--ELIMINAR COLUMNA PARA FECHA DE CREACIÓN REGISTRO
				DECLARE @SQLALTER NVARCHAR(MAX) = 'ALTER TABLE ['+@ESQUEMA+'].['+@TABLA+'] DROP CONSTRAINT IF EXISTS [DF_'+CONVERT(nvarchar(75), @ID)+'_Audit_Fecha_Creacion];';
				EXEC(@SQLALTER);
				SET @SQLALTER = 'ALTER TABLE ['+@ESQUEMA+'].['+@TABLA+'] DROP COLUMN IF EXISTS AuditFechaCreacion;';
				EXEC(@SQLALTER);
				
				--ELIMINAR COLUMNA PARA USUARIO DE CREACIÓN REGISTRO
				SET @SQLALTER = 'ALTER TABLE ['+@ESQUEMA+'].['+@TABLA+'] DROP CONSTRAINT IF EXISTS [DF_'+CONVERT(nvarchar(75), @ID)+'_Audit_Usuario_Creacion];';
				EXEC(@SQLALTER);
				SET @SQLALTER = 'ALTER TABLE ['+@ESQUEMA+'].['+@TABLA+'] DROP COLUMN IF EXISTS AuditUsuarioCreacion;';
				EXEC(@SQLALTER);
				
				--ELIMINAR COLUMNA PARA FECHA DE MODIFIACIÓN REGISTRO
				SET @SQLALTER = 'ALTER TABLE ['+@ESQUEMA+'].['+@TABLA+'] DROP CONSTRAINT IF EXISTS [DF_'+CONVERT(nvarchar(75), @ID)+'_Audit_Fecha_Modificacion];';
				EXEC(@SQLALTER);
				SET @SQLALTER = 'ALTER TABLE ['+@ESQUEMA+'].['+@TABLA+'] DROP COLUMN IF EXISTS AuditFechaModificacion;';
				EXEC(@SQLALTER);
				
				--ELIMINAR COLUMNA PARA USUARIO DE MODIFIACIÓN REGISTRO
				SET @SQLALTER = 'ALTER TABLE ['+@ESQUEMA+'].['+@TABLA+'] DROP CONSTRAINT IF EXISTS [DF_'+CONVERT(nvarchar(75), @ID)+'_Audit_Usuario_Modificacion];';
				EXEC(@SQLALTER);
				SET @SQLALTER = 'ALTER TABLE ['+@ESQUEMA+'].['+@TABLA+'] DROP COLUMN IF EXISTS AuditUsuarioModificacion;';
				EXEC(@SQLALTER);

				--ELIMINAR TRIGGER DE AUDITORIA
				DECLARE @SQLTRIGGER NVARCHAR(MAX) = 'DROP TRIGGER IF EXISTS ['+@ESQUEMA+'].[TR_FechaModificacion_'+@TABLA+'];';
				EXEC(@SQLTRIGGER);

				SET @SQLTRIGGER = 'DROP TRIGGER IF EXISTS ['+@ESQUEMA+'].[TR_FechaCreacion_'+@TABLA+'];';
				EXEC(@SQLTRIGGER);

				DELETE FROM @ListTable WHERE @ID = ID
				SET @Pointer = @Pointer - 1
			END

		COMMIT TRANSACTION APLICANDO_AUDITORIA
	END TRY
	BEGIN CATCH
		PRINT 'Error al eliminar auditoria: ['+@ESQUEMA+'].['+@TABLA+']: '+Error_Message();
		ROLLBACK TRANSACTION APLICANDO_AUDITORIA
	END CATCH
END