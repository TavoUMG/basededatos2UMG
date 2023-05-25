IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[USUARIO]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[USUARIO](
		[Id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,	
		[CUI] [nvarchar](15) NOT NULL,		
		[Nombre] [nvarchar](75) NOT NULL,	
		[Apellido] [nvarchar](75) NOT NULL,	
		[Password] [nvarchar](500) NOT NULL			
	 CONSTRAINT [PK_USUARIO] PRIMARY KEY CLUSTERED	
	(
		[Id] DESC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY],
	CONSTRAINT [UNIQUE_USUARIO_CUI] UNIQUE NONCLUSTERED 
	(
		[CUI] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
	) ON [PRIMARY]

	PRINT N'CREATE TABLE [dbo].[USUARIO]'
END
ELSE
BEGIN
	DROP TABLE [dbo].[USUARIO];

	PRINT N'DROP TABLE [dbo].[USUARIO]'
END
GO
---------------------------------
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

	PRINT N'CREATE TABLE [dbo].[CAJA]'
END
ELSE
BEGIN
	DROP TABLE [dbo].[CAJA];

	PRINT N'DROP TABLE [dbo].[CAJA]'
END 
GO
-------------
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CLIENTE]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[CLIENTE](
		[Id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
		[Nombre] [nvarchar] (50) NOT NULL,
		[Apellido] [nvarchar](50) NOT NULL,			
		[Telefono] [nvarchar](20) NULL,
		[Direccion] [nvarchar](500) NULL,			
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
GO
-------------
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROVEEDOR]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[PROVEEDOR](
		[Id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,				
		[Nombre] [nvarchar](75) NOT NULL,	
		[Direccion] [nvarchar](75) NULL,	
		[Telefono] [nvarchar](25) NULL			
	 CONSTRAINT [PK_PROVEEDOR] PRIMARY KEY CLUSTERED	
	(
		[Id] DESC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
	) ON [PRIMARY]
	

	PRINT N'CREATE TABLE [dbo].[PROVEEDOR]'
END
ELSE
BEGIN
	DROP TABLE [dbo].[PROVEEDOR];

	PRINT N'DROP TABLE [dbo].[PROVEEDOR]'
END
GO
------------------------
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CATEGORIA]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[CATEGORIA](
		[Id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,	
		[Nombre] [nvarchar](75) NOT NULL		
	 CONSTRAINT [PK_CATEGORIA] PRIMARY KEY CLUSTERED	
	(
		[Id] DESC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY],
	CONSTRAINT [UNIQUE_CATEGORIA_Nombre] UNIQUE NONCLUSTERED 
	(
		[Nombre] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
	) ON [PRIMARY]

	PRINT N'CREATE TABLE [dbo].[CATEGORIA]'
END
ELSE
BEGIN
	DROP TABLE [dbo].[CATEGORIA];

	PRINT N'DROP TABLE [dbo].[CATEGORIA]'
END
GO
-----------------
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PRODUCTO]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[PRODUCTO](
		[Id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,	
		[CategoriaId] [decimal](18, 0) NOT NULL,
		[Nombre] [nvarchar](75) NOT NULL,	
		[Vencimiento] [datetime] NULL,			
	 CONSTRAINT [PK_PRODUCTO] PRIMARY KEY CLUSTERED	
	(
		[Id] DESC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
	) ON [PRIMARY]
	
	ALTER TABLE [dbo].[PRODUCTO]  WITH CHECK ADD  CONSTRAINT [FK_PRODUCTO_CategoriaId] FOREIGN KEY([CategoriaId])
	REFERENCES [dbo].[CATEGORIA] ([Id])

	PRINT N'CREATE TABLE [dbo].[PRODUCTO]'
END
ELSE
BEGIN
	DROP TABLE [dbo].[PRODUCTO];

	PRINT N'DROP TABLE [dbo].[PRODUCTO]'
END
GO
------------------------
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
GO
--------------------
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[INVENTARIO]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[INVENTARIO](
		[Id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,	
		[ProductoId] [decimal](18, 0) NOT NULL,	
		[PrecioVenta] [decimal](20, 2) NOT NULL,			
		[Stock] [int] NOT NULL,			
	 CONSTRAINT [PK_INVENTARIO] PRIMARY KEY CLUSTERED	
	(
		[Id] DESC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
	) ON [PRIMARY]
	
	ALTER TABLE [dbo].[INVENTARIO]  WITH CHECK ADD  CONSTRAINT [FK_INVENTARIO_ProductoId] FOREIGN KEY([ProductoId])
	REFERENCES [dbo].[PRODUCTO] ([Id])

	PRINT N'CREATE TABLE [dbo].[INVENTARIO]'
END
ELSE
BEGIN
	DROP TABLE [dbo].[INVENTARIO];

	PRINT N'DROP TABLE [dbo].[INVENTARIO]'
END
GO
------------------
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FACTURA]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[FACTURA](
		[Id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,	
		[ClienteId] [decimal](18, 0) NOT NULL,		
		[CUI_NIT] [nvarchar](15) NULL,		
		[Direccion] [nvarchar](250) NULL,	
		[Fecha] [datetime] NOT NULL,
		[Total] [decimal](20, 2) NOT NULL,						
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
GO
-----------------
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DETALLE]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[DETALLE](
		[Id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,	
		[FacturaId] [decimal](18, 0) NOT NULL,
		[ProductoId] [decimal](18, 0) NOT NULL,		
		[Cantidad] [int] NULL,		
		[Precio] [decimal](20, 2) NOT NULL,	
		[SubTotal] [decimal](20, 2) NOT NULL,	
	 CONSTRAINT [PK_DETALLE] PRIMARY KEY CLUSTERED	
	(
		[Id] DESC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
	) ON [PRIMARY]
	
	ALTER TABLE [dbo].[DETALLE]  WITH CHECK ADD  CONSTRAINT [FK_DETALLE_FacturaId] FOREIGN KEY([FacturaId])
	REFERENCES [dbo].[FACTURA] ([Id])
	ALTER TABLE [dbo].[DETALLE]  WITH CHECK ADD  CONSTRAINT [FK_DETALLE_ProductoId] FOREIGN KEY([ProductoId])
	REFERENCES [dbo].[PRODUCTO] ([Id])

	PRINT N'CREATE TABLE [dbo].[DETALLE]'
END
ELSE
BEGIN
	DROP TABLE [dbo].[DETALLE];

	PRINT N'DROP TABLE [dbo].[DETALLE]'
END
GO
-----------
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DEVOLUCION]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[DEVOLUCION](
		[Id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,	
		[Cantidad] [int] NOT NULL,
		[FacturaId] [decimal](18, 0) NOT NULL,			
		[DetalleId] [decimal](18, 0) NOT NULL,			
		[ProductoId] [decimal](18, 0) NOT NULL,		
		[Fecha] [datetime] NULL,		
	 CONSTRAINT [PK_DEVOLUCION] PRIMARY KEY CLUSTERED	
	(
		[Id] DESC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
	) ON [PRIMARY]
	
	ALTER TABLE [dbo].[DEVOLUCION]  WITH CHECK ADD  CONSTRAINT [FK_DEVOLUCION_FacturaId] FOREIGN KEY([FacturaId])
	REFERENCES [dbo].[FACTURA] ([Id])
	ALTER TABLE [dbo].[DEVOLUCION]  WITH CHECK ADD  CONSTRAINT [FK_DEVOLUCION_DetalleId] FOREIGN KEY([DetalleId])
	REFERENCES [dbo].[DETALLE] ([Id])
	ALTER TABLE [dbo].[DEVOLUCION]  WITH CHECK ADD  CONSTRAINT [FK_DEVOLUCION_ProductoId] FOREIGN KEY([ProductoId])
	REFERENCES [dbo].[PRODUCTO] ([Id])
	
	PRINT N'CREATE TABLE [dbo].[DEVOLUCION]'
END
ELSE
BEGIN
	DROP TABLE [dbo].[DEVOLUCION];

	PRINT N'DROP TABLE [dbo].[DEVOLUCION]'
END



