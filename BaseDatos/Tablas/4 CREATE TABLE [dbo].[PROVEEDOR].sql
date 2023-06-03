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
	
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Nicki', '522 Northland Drive', '77247422');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Saxon', null, null);
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Giustino', '850 Mccormick Plaza', '62678191');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Vick', '2 Vahlen Plaza', '05664351');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Darren', null, null);
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Michel', '299 Iowa Center', '31778310');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Mendel', '3 Dixon Road', '19243741');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Chelsae', null, null);
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Marion', '82 Reindahl Road', '15718872');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Lorenza', '067 Ronald Regan Way', '88823156');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Garfield', '318 Garrison Lane', '18307438');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Shelden', '0267 Loeprich Avenue', '50392777');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Bjorn', '57 Carey Court', '79433851');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Jennette', '3 Lien Center', '24258793');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Jocelyn', '80891 Cottonwood Park', '73492611');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Annadiana', '6 Erie Court', '77682507');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Waldemar', '593 Superior Circle', '39347001');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Riki', '829 Laurel Avenue', '89591110');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Polly', '236 Clemons Point', '30597270');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Chantalle', '1 School Way', '84810535');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Cordy', null, null);
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Viv', null, null);
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Janean', '210 Chinook Crossing', '64634561');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Corrina', '1183 Warrior Trail', '39696578');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Catlaina', '2 Laurel Point', '96457540');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Lorianna', '75 Moulton Point', '15490045');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Levy', '485 Pankratz Terrace', '50253552');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Gnni', null, null);
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Celesta', '4194 Continental Lane', '42101199');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Dolorita', '0 Merry Plaza', '18177096');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Kelli', '1672 Daystar Park', '25605733');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Joann', null, null);
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Spencer', null, null);
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Bruno', '6456 Bartillon Plaza', '73080021');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Gweneth', '56 Dakota Court', '89441545');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Marten', '5208 Atwood Hill', '98849250');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Martyn', '7 Mendota Road', '79925272');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Morse', null, null);
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Chris', '21123 Dottie Circle', '72391500');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Roselin', '86 Banding Park', '48311531');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Iver', '1 Pond Park', '25381563');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Iorgos', '7075 Moulton Terrace', '26065074');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Rozanna', '881 Maple Wood Crossing', '13758851');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Fanchon', '1 Cambridge Crossing', '78230534');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Mikol', '35669 Charing Cross Street', '68358200');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Rancell', '240 Union Pass', '96073897');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Bax', '14232 Menomonie Avenue', '79902025');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Evita', '1519 Westridge Plaza', '30637564');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Ninnette', '26974 Bultman Trail', '34439634');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Gavrielle', '8921 Arrowood Road', '21136926');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Stefanie', '38 School Plaza', '62915248');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Harriett', '70 Porter Plaza', '88059545');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Danie', '7 Randy Parkway', '78241493');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Lona', '09 Paget Junction', '68077781');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Maia', '210 Crownhardt Hill', '35294139');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Agnes', '118 Schlimgen Avenue', '72491080');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Koenraad', null, null);
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Babette', '324 Starling Parkway', '13060701');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Tatiania', '9668 Ronald Regan Alley', '04346372');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Ford', '82 Trailsway Circle', '95871336');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Dorris', '312 Schurz Circle', '52324145');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Sabra', '1324 Fisk Way', '13229279');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Bartholemy', null, null);
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Inessa', '33290 Riverside Drive', '04034326');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Josey', '90 Duke Street', '94062658');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Ada', '081 Warner Road', '91148458');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Aprilette', '18 Center Center', '04819773');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Paulina', '29 Hanson Junction', '01325531');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Clem', null, null);
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Anissa', '6 Towne Alley', '18175161');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Shelden', '3983 Mariners Cove Junction', '14148966');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Dirk', '67 Pawling Crossing', '34999238');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Boy', '33899 Fisk Center', '38070158');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Husain', '2113 Colorado Court', '56147459');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Tybi', '7 Eagle Crest Terrace', '30734758');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Chastity', null, null);
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Ortensia', '7 Starling Junction', '32777533');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Florinda', '4 Eagle Crest Circle', '17779781');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Jonah', '614 Arrowood Pass', '96468241');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Emili', '5854 Doe Crossing Trail', '73215460');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Sonnie', '92 Spenser Street', '48886010');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Madeline', '0 Crest Line Trail', '65032303');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Antoinette', '5871 Arrowood Way', '90008183');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Donnajean', '20482 Commercial Junction', '10441837');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Brittni', '8 Butterfield Pass', '13428898');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Nomi', '5348 Hooker Lane', '84488843');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Adore', '07 Fairview Circle', '42928557');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Wenonah', '44 Lakewood Gardens Street', '30179820');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Teddie', null, null);
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Constancia', '01 Mandrake Drive', '17617073');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Franciskus', null, null);
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Krissie', '200 Main Hill', '19297517');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Alric', null, null);
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Elysha', null, null);
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Wake', '7732 Mayer Junction', '12822988');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Cthrine', '00 Morrow Hill', '94741073');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Pieter', '541 Stoughton Terrace', '18912288');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Lamar', '18698 Union Junction', '49648457');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Jilleen', '1 Dapin Trail', '37348599');
	insert into PROVEEDOR (Nombre, Direccion, Telefono) values ('Ricardo', '598 Lakewood Plaza', '35042251');

	PRINT N'CREATE TABLE [dbo].[PROVEEDOR]'
END
ELSE
BEGIN
	DROP TABLE [dbo].[PROVEEDOR];

	PRINT N'DROP TABLE [dbo].[PROVEEDOR]'
END