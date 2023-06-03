IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CLIENTE]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[CLIENTE](
		[Id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
		[Nombre] [nvarchar] (50) NOT NULL,
		[Apellido] [nvarchar](50) NOT NULL,			
		[Telefono] [nvarchar](20) NULL,
		[Direccion] [nvarchar](250) NULL,			
	 CONSTRAINT [PK_CLIENTE] PRIMARY KEY CLUSTERED	
	(
		[Id] DESC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
	) ON [PRIMARY]	

	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Ofilia', 'Collingdon', '22261283', 'Apt 52');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Eugine', 'Nequest', '64747701', 'Apt 814');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Mal', 'Feild', '26139036', 'Suite 42');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Pat', 'Kment', '79098311', 'Room 64');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Vevay', 'Speers', '08941025', null);
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Rosaleen', 'Zorn', '08712978', 'PO Box 66788');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Brandi', 'Thorby', '43124469', 'Suite 4');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Geraldine', 'Whightman', '03445157', 'Room 1168');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Kathlin', 'Greet', '00106596', 'Room 832');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Wallis', 'Pennini', '58392699', 'Suite 71');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Floyd', 'Delmonti', '80755008', 'Apt 1731');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Kermie', 'Kembley', '25345674', 'Room 987');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Darrell', 'Halbord', null, 'PO Box 18064');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Kate', 'Risley', '79007270', 'Room 1401');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Bertine', 'Blackwell', '73287556', 'Suite 56');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Sven', 'Hundey', '50347728', 'Room 591');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Mela', 'Gillow', null, 'Apt 895');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Clarie', 'Coulsen', null, '7th Floor');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Travers', 'Kubelka', '40014403', null);
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Adore', 'Buxcey', '80029724', 'Apt 1711');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Chester', 'McCrann', '89204729', 'Suite 10');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Jeromy', 'Maxstead', null, 'Apt 1307');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Craggie', 'Giacovazzo', '74610480', 'Room 1178');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Myron', 'Villar', '39137953', 'Suite 16');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Karel', 'Drysdell', '72556457', 'Apt 1590');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Collette', 'Brimley', '40391204', '12th Floor');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Howey', 'Millthorpe', '00292173', 'Suite 51');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Gert', 'Ricart', '59782511', '19th Floor');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Tilda', 'Eccleston', null, '15th Floor');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Paule', 'McCaughren', '97309596', 'PO Box 68387');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Dorisa', 'Eiler', '36198769', 'Room 770');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Tiertza', 'Brimblecomb', '44227684', null);
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Julie', 'Colbourne', '30258245', 'Room 1044');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Mose', 'Clutheram', null, 'PO Box 91891');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Raymund', 'Goady', '10822456', 'Room 1581');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Brit', 'Edon', '42580582', 'Room 1789');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Alard', 'Dalinder', '53498580', '14th Floor');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Kalle', 'Perrat', '70962444', 'PO Box 42152');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Jemie', 'Posselow', '04968651', 'Suite 93');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Sadie', 'Lowers', '67827446', 'Room 684');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Sissy', 'Cathenod', '72968592', 'Apt 1917');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Tyrus', 'Morcomb', '10250931', 'Apt 1769');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Olly', 'Cusick', '55300743', 'Room 1036');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Garald', 'Naile', '46085261', 'Apt 1675');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Benedetta', 'Wilmington', null, '9th Floor');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Brandais', 'Jentle', '92875643', 'Suite 40');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Humfrey', 'Glanders', '47759631', 'Room 1166');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Janaya', 'Coppenhall', '93198204', 'Apt 916');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Consolata', 'Anespie', '12041363', 'Room 1334');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Lyn', 'Wrigglesworth', '67955022', '9th Floor');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Rozele', 'Jennens', '86693694', 'PO Box 3899');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Jaime', 'Heindl', '18296682', 'Suite 8');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Deana', 'Lapsley', '12033862', 'PO Box 24453');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Hewet', 'Kernoghan', '75072278', null);
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Hatti', 'Mee', '51977323', 'Room 572');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Darnall', 'Jorry', '88470009', 'Room 967');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Erskine', 'Slade', '77265379', 'Apt 1791');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Celinda', 'Olpin', '79098116', null);
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Robena', 'Curado', '35624045', 'Suite 34');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Corbie', 'Klosser', '01461140', 'Suite 97');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Leone', 'Poolman', '12151554', '4th Floor');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Riley', 'Rebbeck', '63172067', 'PO Box 74553');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Antone', 'Reinisch', '60397350', 'Room 1357');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Patton', 'Measures', null, 'Room 1537');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Gretel', 'Povey', '31524322', 'PO Box 7943');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Susanetta', 'Juliff', '60036342', '15th Floor');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Therese', 'Baudins', '08856438', null);
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Cirilo', 'Karpe', '02142345', 'Suite 28');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Kimmi', 'Boumphrey', '31756121', null);
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Raul', 'Birkin', '17482901', 'Suite 25');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Wait', 'Petch', '88225873', 'Suite 76');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Adham', 'Rossbrook', '96572078', 'Suite 92');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Lucais', 'Meegin', '42873596', 'PO Box 21203');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Reynold', 'Quade', '32224212', null);
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Denny', 'Worwood', '82191742', 'Apt 1425');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Tabina', 'Dollard', '48375690', 'Apt 211');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Rey', 'Antecki', '90296058', 'Suite 12');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Elyse', 'Tour', '38391006', 'Apt 1697');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Hersch', 'Huxstep', '85687555', 'Room 1245');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Janine', 'Lillie', '84311149', 'PO Box 71837');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Jenna', 'Cowell', '49336664', 'Room 1338');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Eduardo', 'Jaspar', '96953129', '9th Floor');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Francyne', 'Uebel', '66722743', 'Apt 1518');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Alexandra', 'Foffano', '99342639', 'Apt 18');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Abigale', 'Eim', '13315715', 'Suite 19');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Darrin', 'Nerval', '70534092', 'Apt 1889');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Coralyn', 'Di Frisco', '65919442', '8th Floor');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Connor', 'Greenless', '12622161', 'Suite 84');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Charmine', 'Dhillon', '80649503', 'Apt 886');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Lena', 'Grievson', '40349226', 'PO Box 55681');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Grange', 'Baskwell', '20711844', 'PO Box 98475');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Timmy', 'Klemps', '38272639', '7th Floor');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Fran', 'Carbett', '60950964', '7th Floor');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Glori', 'Townes', '01314025', 'Suite 28');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Kennedy', 'Oyley', '26101376', 'Suite 35');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Becky', 'Wyleman', null, 'Room 1525');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Darnell', 'Cudd', '48308487', 'Suite 52');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Hewie', 'Braam', '55552787', 'Apt 126');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Gerald', 'McIlvenny', null, 'PO Box 78254');
	insert into [dbo].[CLIENTE] (Nombre, Apellido, Telefono, Direccion) values ('Lindi', 'Reville', '82239684', 'Room 1014');

	PRINT N'CREATE TABLE [dbo].[CLIENTE]'
END
ELSE
BEGIN
	DROP TABLE [dbo].[CLIENTE];

	PRINT N'DROP TABLE [dbo].[CLIENTE]'
END