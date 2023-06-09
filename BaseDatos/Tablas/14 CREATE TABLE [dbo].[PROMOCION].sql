IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROMOCION]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[PROMOCION](
		[Id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,	
		[Descripcion] [nvarchar](MAX) NOT NULL,		
		[ProductoId] [decimal](18, 0) NOT NULL,			
		[TipoPromocionId] [decimal](18, 0) NOT NULL,		
	 CONSTRAINT [PK_PROMOCION] PRIMARY KEY CLUSTERED	
	(
		[Id] DESC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
	) ON [PRIMARY]
	
	ALTER TABLE [dbo].[PROMOCION]  WITH CHECK ADD  CONSTRAINT [FK_PROMOCION_ProductoId] FOREIGN KEY([ProductoId])
	REFERENCES [dbo].[PRODUCTO] ([Id])
	ALTER TABLE [dbo].[PROMOCION]  WITH CHECK ADD  CONSTRAINT [FK_PROMOCION_TipoPromocionId] FOREIGN KEY([TipoPromocionId])
	REFERENCES [dbo].[TIPO_PROMOCION] ([Id])
	
	PRINT N'CREATE TABLE [dbo].[PROMOCION]'
END
ELSE
BEGIN
	DROP TABLE [dbo].[PROMOCION];

	PRINT N'DROP TABLE [dbo].[PROMOCION]'
END