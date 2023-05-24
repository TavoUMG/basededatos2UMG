IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PRODUCTO]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[PRODUCTO](
		[Id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,				
		[Nombre] [nvarchar](75) NOT NULL,	
		[Tipo] [nvarchar](75) NOT NULL,
		[Precio] [Decimal](20,2) NOT NULL,	
		[Vencimiento] [datetime] NOT NULL,			
	 CONSTRAINT [PK_PRODUCTO] PRIMARY KEY CLUSTERED	
	(
		[Id] DESC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
	) ON [PRIMARY]
	

	PRINT N'CREATE TABLE [dbo].[PRODUCTO]'
END
ELSE
BEGIN
	DROP TABLE [dbo].[PRODUCTO];

	PRINT N'DROP TABLE [dbo].[PRODUCTO]'
END