IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[COMPRA]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[COMPRA](
		[Id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,	
		[Cantidad] [int] NOT NULL,	
		[ProductoId] [decimal](18, 0) NOT NULL,		
		[ProveedorId] [decimal](18, 0) NOT NULL,	
		[PrecioCosto] [decimal](20, 2) NOT NULL,
		[CajaId] [decimal](18, 0) NOT NULL,
	 CONSTRAINT [PK_COMPRA] PRIMARY KEY CLUSTERED	
	(
		[Id] DESC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
	) ON [PRIMARY]
	
	ALTER TABLE [dbo].[COMPRA]  WITH CHECK ADD  CONSTRAINT [FK_COMPRA_ProductoId] FOREIGN KEY([ProductoId])
	REFERENCES [dbo].[PRODUCTO] ([Id])
	ALTER TABLE [dbo].[COMPRA]  WITH CHECK ADD  CONSTRAINT [FK_COMPRA_ProveedorId] FOREIGN KEY([ProveedorId])
	REFERENCES [dbo].[PROVEEDOR] ([Id])
	ALTER TABLE [dbo].[COMPRA]  WITH CHECK ADD  CONSTRAINT [FK_COMPRA_CajaId] FOREIGN KEY([CajaId])
	REFERENCES [dbo].[CAJA] ([Id])

	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (69, 10, 29, 117.58, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (37, 39, 13, 75.61, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (3, 45, 88, 180.27, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (84, 55, 90, 181.78, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (15, 52, 36, 72.83, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (87, 48, 94, 66.77, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (19, 11, 38, 37.57, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (87, 32, 25, 39.58, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (62, 79, 20, 175.89, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (26, 30, 95, 122.66, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (93, 25, 82, 81.0, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (27, 25, 88, 188.59, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (93, 56, 1, 183.4, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (1, 10, 67, 124.85, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (53, 46, 34, 62.98, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (1, 60, 5, 182.5, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (41, 100, 14, 29.56, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (67, 4, 95, 198.66, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (54, 99, 8, 27.23, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (52, 12, 25, 154.73, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (68, 95, 39, 29.4, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (65, 78, 68, 185.21, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (63, 16, 73, 24.66, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (62, 89, 33, 114.29, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (12, 5, 96, 193.47, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (65, 47, 60, 170.48, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (33, 70, 34, 62.83, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (52, 39, 51, 19.59, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (86, 93, 11, 17.86, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (6, 11, 67, 9.45, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (43, 34, 28, 55.42, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (50, 20, 5, 33.47, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (97, 11, 4, 58.48, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (62, 5, 60, 95.98, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (3, 80, 57, 4.76, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (43, 56, 47, 176.96, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (13, 93, 80, 31.88, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (57, 90, 45, 85.46, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (59, 47, 27, 18.85, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (76, 21, 19, 72.82, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (37, 99, 95, 71.1, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (33, 99, 23, 170.51, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (4, 17, 48, 183.68, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (46, 12, 82, 141.05, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (41, 93, 42, 45.62, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (92, 95, 56, 60.61, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (100, 9, 68, 6.06, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (20, 23, 86, 31.37, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (12, 73, 77, 177.8, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (21, 42, 77, 49.12, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (69, 51, 17, 53.29, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (68, 59, 86, 194.38, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (78, 27, 99, 48.83, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (25, 47, 9, 44.96, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (13, 15, 81, 123.89, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (73, 1, 2, 143.21, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (77, 57, 23, 34.31, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (96, 54, 25, 133.36, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (57, 78, 20, 146.15, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (21, 6, 57, 109.71, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (100, 65, 53, 127.22, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (18, 77, 3, 137.29, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (30, 94, 67, 139.57, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (61, 58, 51, 141.18, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (50, 6, 77, 158.27, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (38, 1, 32, 156.24, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (1, 31, 58, 29.79, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (41, 41, 96, 30.18, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (100, 6, 50, 87.38, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (74, 13, 89, 41.74, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (14, 9, 36, 15.42, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (38, 63, 54, 8.14, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (42, 14, 66, 21.47, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (37, 39, 15, 186.15, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (30, 92, 31, 38.93, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (78, 79, 31, 76.77, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (61, 96, 66, 13.49, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (41, 92, 87, 148.97, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (90, 3, 95, 158.16, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (43, 55, 29, 145.98, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (4, 16, 30, 166.33, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (83, 53, 66, 5.26, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (2, 37, 21, 122.64, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (46, 89, 3, 37.72, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (60, 12, 70, 74.84, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (93, 22, 70, 69.29, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (4, 55, 81, 92.19, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (49, 2, 67, 127.65, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (67, 22, 97, 26.17, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (92, 78, 8, 193.69, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (70, 78, 99, 183.97, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (42, 65, 15, 188.88, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (83, 18, 10, 63.94, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (14, 55, 17, 182.97, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (93, 88, 71, 141.06, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (46, 2, 11, 89.81, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (72, 1, 86, 170.01, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (35, 60, 97, 112.65, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (67, 61, 50, 26.15, 1);
	insert into Compra (Cantidad, ProductoId, ProveedorId, PrecioCosto, CajaId) values (79, 95, 20, 186.83, 1);

	PRINT N'CREATE TABLE [dbo].[COMPRA]'
END
ELSE
BEGIN
	DROP TABLE [dbo].[COMPRA];

	PRINT N'DROP TABLE [dbo].[COMPRA]'
END