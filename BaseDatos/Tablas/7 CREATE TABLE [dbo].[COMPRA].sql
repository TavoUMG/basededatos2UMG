IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[COMPRA]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[COMPRA](
		[Id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,	
		[Cantidad] [int] NOT NULL,	
		[ProductoId] [decimal](18, 0) NOT NULL,		
		[ProveedorId] [decimal](18, 0) NOT NULL,	
		[PrecioCosto] [decimal](20, 2) NOT NULL,
	 CONSTRAINT [PK_COMPRA] PRIMARY KEY CLUSTERED	
	(
		[Id] DESC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
	) ON [PRIMARY]
	
	ALTER TABLE [dbo].[COMPRA]  WITH CHECK ADD  CONSTRAINT [FK_COMPRA_ProductoId] FOREIGN KEY([ProductoId])
	REFERENCES [dbo].[PRODUCTO] ([Id])
	ALTER TABLE [dbo].[COMPRA]  WITH CHECK ADD  CONSTRAINT [FK_COMPRA_ProveedorId] FOREIGN KEY([ProveedorId])
	REFERENCES [dbo].[PROVEEDOR] ([Id])

	PRINT N'CREATE TABLE [dbo].[COMPRA]'
END
ELSE
BEGIN
	DROP TABLE [dbo].[COMPRA];

	PRINT N'DROP TABLE [dbo].[COMPRA]'
END