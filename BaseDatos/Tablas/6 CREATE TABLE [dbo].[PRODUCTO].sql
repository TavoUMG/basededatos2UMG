IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PRODUCTO]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[PRODUCTO](
		[Id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,	
		[CategoriaId] [decimal](18, 0) NOT NULL,
		[Nombre] [nvarchar](75) NOT NULL,	
		[Vencimiento] [datetime] NULL,	
		[Imagen] [nvarchar](250) NULL,			
	 CONSTRAINT [PK_PRODUCTO] PRIMARY KEY CLUSTERED	
	(
		[Id] DESC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
	) ON [PRIMARY]
	
	ALTER TABLE [dbo].[PRODUCTO]  WITH CHECK ADD  CONSTRAINT [FK_PRODUCTO_CategoriaId] FOREIGN KEY([CategoriaId])
	REFERENCES [dbo].[CATEGORIA] ([Id])

	insert into Producto (CategoriaId, Nombre, Vencimiento) values (13, 'Potatoes - Purple, Organic', '2023-01-08');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (12, 'Pastry - Chocolate Chip Muffin', '2023-05-27');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (31, 'Amarula Cream', '2023-04-08');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (9, 'Flour - Semolina', '2023-03-18');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (10, 'Celery Root', '2023-04-18');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (42, 'Tea - Mint', '2023-04-17');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (40, 'Flour - Rye', '2023-03-09');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (25, 'Steampan - Half Size Shallow', '2023-01-02');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (29, 'Wine - Chateau Timberlay', '2023-03-19');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (28, 'Pepper - White, Whole', '2023-02-08');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (27, 'Sprouts - Onion', '2023-03-27');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (29, 'Sauce - Mint', '2023-03-21');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (18, 'Nut - Pecan, Halves', '2023-01-30');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (25, 'Bread Sour Rolls', '2023-04-10');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (47, 'Peppercorns - Pink', '2023-04-29');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (19, 'Lamb - Bones', '2023-02-18');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (3, 'Beef - Ground, Extra Lean, Fresh', '2023-05-30');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (42, 'Evaporated Milk - Skim', '2023-02-21');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (49, 'Tamarind Paste', '2023-03-19');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (22, 'Pasta - Rotini, Dry', '2023-04-08');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (24, 'Chicken Giblets', '2023-01-28');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (19, 'Ice Cream Bar - Hagen Daz', '2023-01-27');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (33, 'Beans - Kidney White', '2023-01-28');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (34, 'Wheat - Soft Kernal Of Wheat', '2023-04-11');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (3, 'Longan', '2023-01-18');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (26, 'Kellogs Special K Cereal', '2023-03-17');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (36, 'Blueberries', '2023-03-14');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (15, 'Coffee Swiss Choc Almond', '2023-05-25');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (7, 'Lamb Tenderloin Nz Fr', '2023-04-08');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (24, 'Container Clear 8 Oz', '2023-01-12');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (20, 'Sauce Bbq Smokey', '2023-04-03');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (44, 'Sausage - Meat', '2023-03-31');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (24, 'Coffee Cup 16oz Foam', '2023-03-24');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (42, 'Dip - Tapenade', '2023-03-02');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (9, 'Blueberries', '2023-04-25');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (34, 'Tomato - Peeled Italian Canned', '2023-02-25');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (46, 'Red Snapper - Fresh, Whole', '2023-03-27');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (27, 'Compound - Strawberry', '2023-03-04');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (42, 'Wine - Fino Tio Pepe Gonzalez', '2023-01-01');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (42, 'Lettuce - Romaine', '2023-01-09');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (2, 'Cheese - Boursin, Garlic / Herbs', '2023-01-06');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (33, 'Bacon Strip Precooked', '2023-05-01');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (48, 'Chocolate - Semi Sweet, Calets', '2023-02-12');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (42, 'Soup Campbells - Tomato Bisque', '2023-01-12');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (43, 'Wild Boar - Tenderloin', '2023-03-09');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (20, 'Samosa - Veg', '2023-03-31');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (42, 'Cheese - Perron Cheddar', '2023-01-23');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (2, 'Beef - Chuck, Boneless', '2023-04-07');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (31, 'Bread Base - Italian', '2023-03-20');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (48, 'Curry Paste - Madras', '2023-02-11');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (21, 'Wine - Carmenere Casillero Del', '2023-04-10');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (29, 'Bread - Multigrain Oval', '2023-01-20');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (41, 'Oven Mitts 17 Inch', '2023-05-15');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (5, 'Cake - Bande Of Fruit', '2023-04-20');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (39, 'Kumquat', '2023-05-05');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (7, 'Beef - Montreal Smoked Brisket', '2023-02-27');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (42, 'Beer - Pilsner Urquell', '2023-02-10');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (14, 'Brandy Cherry - Mcguinness', '2023-03-04');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (8, 'Star Fruit', '2023-05-13');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (4, 'Thyme - Dried', '2023-01-11');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (18, 'Pork - Bones', '2023-04-11');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (7, 'Coffee - Irish Cream', '2023-05-21');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (23, 'Energy Drink - Franks Pineapple', '2023-01-12');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (40, 'Flower - Commercial Bronze', '2023-05-06');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (24, 'Champagne - Brights, Dry', '2023-04-28');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (16, 'Sauce - Sesame Thai Dressing', '2023-02-28');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (39, 'Plasticknivesblack', '2023-04-19');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (34, 'Wine - Spumante Bambino White', '2023-01-27');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (44, 'Blue Curacao - Marie Brizard', '2023-01-04');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (14, 'Cookies - Englishbay Chochip', '2023-01-11');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (30, 'Lid - Translucent, 3.5 And 6 Oz', '2023-04-13');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (41, 'Beef - Top Butt', '2023-03-29');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (29, 'Mussels - Frozen', '2023-05-25');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (38, 'Pear - Packum', '2023-03-20');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (36, 'Bread - Pumpernickle, Rounds', '2023-01-17');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (42, 'Wine - Carmenere Casillero Del', '2023-01-08');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (21, 'Initation Crab Meat', '2023-05-01');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (45, 'Onions - Green', '2023-01-25');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (32, 'Mushroom - Enoki, Fresh', '2023-03-19');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (36, 'Muffin Mix - Raisin Bran', '2023-05-24');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (23, 'Spice - Chili Powder Mexican', '2023-01-27');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (22, 'Wakami Seaweed', '2023-04-25');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (33, 'Canadian Emmenthal', '2023-05-29');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (1, 'Lamb - Bones', '2023-01-05');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (22, 'Halibut - Whole, Fresh', '2023-01-04');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (25, 'Wine - Pinot Noir Stoneleigh', '2023-02-23');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (20, 'Seedlings - Clamshell', '2023-01-19');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (11, 'Lentils - Green Le Puy', '2023-02-25');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (48, 'Yeast Dry - Fermipan', '2023-05-23');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (14, 'Extract - Raspberry', '2023-03-02');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (36, 'Pepper - Red Bell', '2023-01-12');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (1, 'Beef - Salted', '2023-05-10');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (24, 'Chicken - Whole', '2023-04-04');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (13, 'Pancetta', '2023-01-30');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (47, 'Bag - Clear 7 Lb', '2023-05-14');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (37, 'Noodles - Cellophane, Thin', '2023-02-04');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (20, 'Horseradish - Prepared', '2023-01-13');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (23, 'Cheese - Mozzarella, Buffalo', '2023-04-13');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (24, 'Langers - Cranberry Cocktail', '2023-01-18');
	insert into Producto (CategoriaId, Nombre, Vencimiento) values (6, 'Sauce - Rosee', '2023-05-26');

	PRINT N'CREATE TABLE [dbo].[PRODUCTO]'
END
ELSE
BEGIN
	DROP TABLE [dbo].[PRODUCTO];

	PRINT N'DROP TABLE [dbo].[PRODUCTO]'
END