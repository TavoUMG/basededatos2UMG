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

	insert into CATEGORIA (Nombre) values ('Electronics');
	insert into CATEGORIA (Nombre) values ('Clothing');
	insert into CATEGORIA (Nombre) values ('Home & Garden');
	insert into CATEGORIA (Nombre) values ('Toys & Games');
	insert into CATEGORIA (Nombre) values ('Sports & Outdoors');
	insert into CATEGORIA (Nombre) values ('Books');
	insert into CATEGORIA (Nombre) values ('Movies & TV');
	insert into CATEGORIA (Nombre) values ('Music');
	insert into CATEGORIA (Nombre) values ('Beauty');
	insert into CATEGORIA (Nombre) values ('Health');
	insert into CATEGORIA (Nombre) values ('Automotive');
	insert into CATEGORIA (Nombre) values ('Pet Supplies');
	insert into CATEGORIA (Nombre) values ('Food & Grocery');
	insert into CATEGORIA (Nombre) values ('Jewelry');
	insert into CATEGORIA (Nombre) values ('Office Supplies');
	insert into CATEGORIA (Nombre) values ('Tools & Home Improvement');
	insert into CATEGORIA (Nombre) values ('Baby');
	insert into CATEGORIA (Nombre) values ('Industrial & Scientific');
	insert into CATEGORIA (Nombre) values ('Arts');
	insert into CATEGORIA (Nombre) values ('Crafts & Sewing');
	insert into CATEGORIA (Nombre) values ('Handmade');
	insert into CATEGORIA (Nombre) values ('Collectibles & Fine Art');
	insert into CATEGORIA (Nombre) values ('Digital Music');
	insert into CATEGORIA (Nombre) values ('Kindle Store');
	insert into CATEGORIA (Nombre) values ('Apps & Games');
	insert into CATEGORIA (Nombre) values ('Musical Instruments');
	insert into CATEGORIA (Nombre) values ('Gift Cards');
	insert into CATEGORIA (Nombre) values ('Magazine Subscriptions');
	insert into CATEGORIA (Nombre) values ('Amazon Pantry');
	insert into CATEGORIA (Nombre) values ('Amazon Launchpad');
	insert into CATEGORIA (Nombre) values ('Amazon Exclusives');
	insert into CATEGORIA (Nombre) values ('AmazonFresh');
	insert into CATEGORIA (Nombre) values ('AmazonBasics');
	insert into CATEGORIA (Nombre) values ('Amazon Elements');
	insert into CATEGORIA (Nombre) values ('Amazon Home');
	insert into CATEGORIA (Nombre) values ('Amazon Handmade Wedding Shop');
	insert into CATEGORIA (Nombre) values ('Amazon Renewed');
	insert into CATEGORIA (Nombre) values ('Amazon Second Chance');
	insert into CATEGORIA (Nombre) values ('AmazonSmile Charity Lists');
	insert into CATEGORIA (Nombre) values ('Amazon Business');
	insert into CATEGORIA (Nombre) values ('Amazon Global Store');
	insert into CATEGORIA (Nombre) values ('Amazon Ignite');
	insert into CATEGORIA (Nombre) values ('Amazon Photos & Prints');
	insert into CATEGORIA (Nombre) values ('Amazon Warehouse');
	insert into CATEGORIA (Nombre) values ('Amazon Live');
	insert into CATEGORIA (Nombre) values ('Amazon 4-star');
	insert into CATEGORIA (Nombre) values ('Amazon Books');
	insert into CATEGORIA (Nombre) values ('Amazon Go');
	insert into CATEGORIA (Nombre) values ('Amazon Pop-Up');
	insert into CATEGORIA (Nombre) values ('Amazon Hub Locker');


	PRINT N'CREATE TABLE [dbo].[CATEGORIA]'
END
ELSE
BEGIN
	DROP TABLE [dbo].[CATEGORIA];

	PRINT N'DROP TABLE [dbo].[CATEGORIA]'
END