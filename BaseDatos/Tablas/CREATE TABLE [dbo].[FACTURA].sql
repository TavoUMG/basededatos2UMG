IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FACTURA]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[FACTURA](
		[Id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,	
		[ClienteId] [decimal](18, 0) NOT NULL,	
		[Fecha] [datetime] NOT NULL,						
	 CONSTRAINT [PK_FACTURA] PRIMARY KEY CLUSTERED	
	(
		[Id] DESC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
	) ON [PRIMARY]
	
	ALTER TABLE [dbo].[FACTURA]  WITH CHECK ADD  CONSTRAINT [FK_FACTURA_ClienteId] FOREIGN KEY([ClienteId])
	REFERENCES [dbo].[CLIENTE] ([Id])

	PRINT N'CREATE TABLE [dbo].[FACTURA]'
END
ELSE
BEGIN
	DROP TABLE [dbo].[FACTURA];

	PRINT N'DROP TABLE [dbo].[FACTURA]'
END