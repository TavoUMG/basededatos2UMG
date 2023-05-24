IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CLIENTE]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[CLIENTE](
		[Id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
		[Nombre] [nvarchar] (50) NOT NULL,
		[Apellido] [nvarchar](50) NOT NULL,			
		[Telefono] [nvarchar](20) NULL,
		[Direccion] [nvarchar] NULL,			
	 CONSTRAINT [PK_CLIENTE] PRIMARY KEY CLUSTERED	
	(
		[Id] DESC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
	) ON [PRIMARY]	

	PRINT N'CREATE TABLE [dbo].[CLIENTE]'
END
ELSE
BEGIN
	DROP TABLE [dbo].[CLIENTE];

	PRINT N'DROP TABLE [dbo].[CLIENTE]'
END