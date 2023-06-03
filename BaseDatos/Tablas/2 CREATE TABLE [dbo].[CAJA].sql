IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CAJA]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[CAJA](
		[Id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,	
		[UsuarioId] [decimal](18, 0) NOT NULL,				
		[efectivoApertura] [decimal](20,2) NOT NULL,
		[efectivoCierre] [decimal](20,2) NOT NULL,	
	 CONSTRAINT [PK_CAJA] PRIMARY KEY CLUSTERED	
	(
		[Id] DESC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
	) ON [PRIMARY]
	
	ALTER TABLE [dbo].[CAJA]  WITH CHECK ADD  CONSTRAINT [FK_CAJA_UsuarioId] FOREIGN KEY([UsuarioId])
	REFERENCES [dbo].[USUARIO] ([Id])

	INSERT INTO Caja(UsuarioId, efectivoApertura, efectivoCierre) VALUES(1, 10000, 0);

	PRINT N'CREATE TABLE [dbo].[CAJA]'
END
ELSE
BEGIN
	DROP TABLE [dbo].[CAJA];

	PRINT N'DROP TABLE [dbo].[CAJA]'
END