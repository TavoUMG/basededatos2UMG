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
			WHERE OBJECT_ID NOT IN ('1726629194')
			ORDER BY [schema], [table] ASC

			DECLARE @Pointer INT = (SELECT count(*) FROM @ListTable);
			DECLARE @ID INT, @ESQUEMA NVARCHAR(MAX), @TABLA NVARCHAR(MAX);

			WHILE  @Pointer > 0
			BEGIN
				SELECT TOP 1 @ID = ID, @ESQUEMA = ESQUEMA, @TABLA = TABLA FROM @ListTable ORDER BY ESQUEMA, TABLA ASC

				--AGREGAR COLUMNAS DE AUDITORIA

				--AGREGAR COLUMNA PARA FECHA DE CREACI�N REGISTRO
				DECLARE @SQLALTER NVARCHAR(MAX) = 'ALTER TABLE ['+@ESQUEMA+'].['+@TABLA+'] ADD AuditFechaCreacion DATETIME NULL CONSTRAINT DF_'+CONVERT(nvarchar(75), @ID)+'_Audit_Fecha_Creacion DEFAULT GETDATE();';
				EXEC(@SQLALTER);

				--AGREGAR COLUMNA PARA USUARIO DE CREACI�N REGISTRO
				SET @SQLALTER = 'ALTER TABLE ['+@ESQUEMA+'].['+@TABLA+'] ADD AuditUsuarioCreacion NVARCHAR(150) NULL CONSTRAINT DF_'+CONVERT(nvarchar(75), @ID)+'_Audit_Usuario_Creacion DEFAULT NULL;';
				EXEC(@SQLALTER);

				--AGREGAR COLUMNA PARA FECHA DE MODIFICACI�N REGISTRO
				SET @SQLALTER = 'ALTER TABLE ['+@ESQUEMA+'].['+@TABLA+'] ADD AuditFechaModificacion DATETIME NULL CONSTRAINT DF_'+CONVERT(nvarchar(75), @ID)+'_Audit_Fecha_Modificacion DEFAULT NULL;';
				EXEC(@SQLALTER);

				--AGREGAR COLUMNA PARA USUARIO DE MODIFICACI�N REGISTRO
				SET @SQLALTER = 'ALTER TABLE ['+@ESQUEMA+'].['+@TABLA+'] ADD AuditUsuarioModificacion NVARCHAR(150) NULL CONSTRAINT DF_'+CONVERT(nvarchar(75), @ID)+'_Audit_Usuario_Modificacion DEFAULT NULL;';
				EXEC(@SQLALTER);

				--AGREGAR TRIGGER DE AUDITORIA
				DECLARE @PRIMARYKEY NVARCHAR(MAX)
				
				SELECT @PRIMARYKEY = COLUMN_NAME
				FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
				WHERE OBJECTPROPERTY(OBJECT_ID(CONSTRAINT_SCHEMA + '.' + QUOTENAME(CONSTRAINT_NAME)), 'IsPrimaryKey') = 1
				AND TABLE_NAME = @TABLA AND TABLE_SCHEMA = @ESQUEMA

				IF @TABLA = 'menub'
				BEGIN
					SET @PRIMARYKEY = 'IdMenu';
				END

				DECLARE @SQLTRIGGER NVARCHAR(MAX) = 'CREATE TRIGGER ['+@ESQUEMA+'].[TR_FechaModificacion_'+@TABLA+'] ON ['+@ESQUEMA+'].['+@TABLA+']
					AFTER UPDATE AS
					BEGIN
					UPDATE ['+@ESQUEMA+'].['+@TABLA+'] SET AuditFechaModificacion = GETDATE()
					FROM ['+@ESQUEMA+'].['+@TABLA+'] t
					INNER JOIN Inserted i ON t.'+@PRIMARYKEY+' = i.'+@PRIMARYKEY+';
					END;';
				EXEC(@SQLTRIGGER);

				SET @SQLTRIGGER = 'CREATE TRIGGER ['+@ESQUEMA+'].[TR_FechaCreacion_'+@TABLA+'] ON ['+@ESQUEMA+'].['+@TABLA+']
					AFTER INSERT AS
					BEGIN
					DISABLE TRIGGER ['+@ESQUEMA+'].[TR_FechaModificacion_'+@TABLA+'] ON ['+@ESQUEMA+'].['+@TABLA+'];
					UPDATE ['+@ESQUEMA+'].['+@TABLA+'] SET AuditFechaCreacion = GETDATE(), AuditUsuarioModificacion = NULL
					FROM ['+@ESQUEMA+'].['+@TABLA+'] t
					INNER JOIN Inserted i ON t.'+@PRIMARYKEY+' = i.'+@PRIMARYKEY+';
					ENABLE TRIGGER ['+@ESQUEMA+'].[TR_FechaModificacion_'+@TABLA+'] ON ['+@ESQUEMA+'].['+@TABLA+'];
					END;';
				EXEC(@SQLTRIGGER);

				DELETE FROM @ListTable WHERE @ID = ID
				SET @Pointer = (SELECT count(*) FROM @ListTable);
			END

		COMMIT TRANSACTION APLICANDO_AUDITORIA
	END TRY
	BEGIN CATCH
		PRINT 'Error al aplicar auditoria ['+@ESQUEMA+'].['+@TABLA+']: '+Error_Message();
		ROLLBACK TRANSACTION APLICANDO_AUDITORIA
	END CATCH
END