USE [master]
GO
/****** Object:  Database [Animal Hospital]    Script Date: 2/20/2018 1:57:07 PM ******/
CREATE DATABASE [Animal Hospital]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Animal Hospital', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\Animal Hospital.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Animal Hospital_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\Animal Hospital_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Animal Hospital] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Animal Hospital].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Animal Hospital] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Animal Hospital] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Animal Hospital] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Animal Hospital] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Animal Hospital] SET ARITHABORT OFF 
GO
ALTER DATABASE [Animal Hospital] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Animal Hospital] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Animal Hospital] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Animal Hospital] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Animal Hospital] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Animal Hospital] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Animal Hospital] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Animal Hospital] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Animal Hospital] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Animal Hospital] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Animal Hospital] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Animal Hospital] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Animal Hospital] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Animal Hospital] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Animal Hospital] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Animal Hospital] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Animal Hospital] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Animal Hospital] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Animal Hospital] SET  MULTI_USER 
GO
ALTER DATABASE [Animal Hospital] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Animal Hospital] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Animal Hospital] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Animal Hospital] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Animal Hospital] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Animal Hospital] SET QUERY_STORE = OFF
GO
USE [Animal Hospital]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [Animal Hospital]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 2/20/2018 1:57:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[address_id] [int] IDENTITY(1,1) NOT NULL,
	[owner_id] [int] NOT NULL,
	[Line1] [varchar](50) NOT NULL,
	[Line2] [varchar](50) NULL,
	[City] [varchar](50) NOT NULL,
	[state] [varchar](2) NOT NULL,
	[zip] [int] NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[address_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Customer_invoice]    Script Date: 2/20/2018 1:57:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer_invoice](
	[invoice_id] [int] IDENTITY(1,1) NOT NULL,
	[date_visited] [datetime] NOT NULL,
	[owner_id] [int] NOT NULL,
	[procedure_id] [int] NOT NULL,
 CONSTRAINT [PK_Customer_invoice] PRIMARY KEY CLUSTERED 
(
	[invoice_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Pet]    Script Date: 2/20/2018 1:57:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pet](
	[pet_id] [int] IDENTITY(1,1) NOT NULL,
	[type_id] [int] NOT NULL,
	[Age] [int] NULL,
	[owner_id] [int] NOT NULL,
 CONSTRAINT [PK_Pet] PRIMARY KEY CLUSTERED 
(
	[pet_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Pet_id]    Script Date: 2/20/2018 1:57:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pet_id](
	[pet_id] [int] IDENTITY(1,1) NOT NULL,
	[pet_name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Pet_id] PRIMARY KEY CLUSTERED 
(
	[pet_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[pet_owner]    Script Date: 2/20/2018 1:57:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pet_owner](
	[owner_id] [int] IDENTITY(1,1) NOT NULL,
	[First_name] [char](20) NOT NULL,
	[Last_name] [char](20) NOT NULL,
 CONSTRAINT [PK_pet_owner] PRIMARY KEY CLUSTERED 
(
	[owner_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Price]    Script Date: 2/20/2018 1:57:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Price](
	[procedure_id] [int] NOT NULL,
	[type_id] [int] NOT NULL,
	[cost] [money] NOT NULL,
 CONSTRAINT [PK_Price] PRIMARY KEY CLUSTERED 
(
	[procedure_id] ASC,
	[type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Procedure]    Script Date: 2/20/2018 1:57:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Procedure](
	[procedure_id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Procedure] PRIMARY KEY CLUSTERED 
(
	[procedure_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Type]    Script Date: 2/20/2018 1:57:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Type](
	[type_id] [int] IDENTITY(1,1) NOT NULL,
	[type_name] [char](10) NULL,
 CONSTRAINT [PK_Type] PRIMARY KEY CLUSTERED 
(
	[type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Address] ON 

INSERT [dbo].[Address] ([address_id], [owner_id], [Line1], [Line2], [City], [state], [zip]) VALUES (4, 1, N'223 Candy Cane Lane', NULL, N'New York ', N'NY', 11111)
INSERT [dbo].[Address] ([address_id], [owner_id], [Line1], [Line2], [City], [state], [zip]) VALUES (5, 2, N'300 Bullocks Ave', NULL, N'Boise', N'ID', 22222)
INSERT [dbo].[Address] ([address_id], [owner_id], [Line1], [Line2], [City], [state], [zip]) VALUES (6, 3, N'7983 Grey Lane', NULL, N'Columbus', N'OH', 43230)
INSERT [dbo].[Address] ([address_id], [owner_id], [Line1], [Line2], [City], [state], [zip]) VALUES (7, 4, N'1212 Twelth Street', NULL, N'Columbus ', N'OH', 44444)
INSERT [dbo].[Address] ([address_id], [owner_id], [Line1], [Line2], [City], [state], [zip]) VALUES (8, 5, N'44 Trash TV Trail', NULL, N'New York', N'NY', 55555)
INSERT [dbo].[Address] ([address_id], [owner_id], [Line1], [Line2], [City], [state], [zip]) VALUES (9, 6, N'100 Black Hole', NULL, N'Sun', N'MW', 666)
SET IDENTITY_INSERT [dbo].[Address] OFF
SET IDENTITY_INSERT [dbo].[Customer_invoice] ON 

INSERT [dbo].[Customer_invoice] ([invoice_id], [date_visited], [owner_id], [procedure_id]) VALUES (1, CAST(N'2018-02-19T00:00:00.000' AS DateTime), 1, 2)
INSERT [dbo].[Customer_invoice] ([invoice_id], [date_visited], [owner_id], [procedure_id]) VALUES (2, CAST(N'2018-02-02T00:00:00.000' AS DateTime), 2, 5)
INSERT [dbo].[Customer_invoice] ([invoice_id], [date_visited], [owner_id], [procedure_id]) VALUES (3, CAST(N'2017-12-25T00:00:00.000' AS DateTime), 3, 3)
INSERT [dbo].[Customer_invoice] ([invoice_id], [date_visited], [owner_id], [procedure_id]) VALUES (4, CAST(N'2018-01-01T00:00:00.000' AS DateTime), 4, 4)
INSERT [dbo].[Customer_invoice] ([invoice_id], [date_visited], [owner_id], [procedure_id]) VALUES (5, CAST(N'2018-01-20T00:00:00.000' AS DateTime), 5, 4)
INSERT [dbo].[Customer_invoice] ([invoice_id], [date_visited], [owner_id], [procedure_id]) VALUES (6, CAST(N'2018-02-01T00:00:00.000' AS DateTime), 6, 1)
SET IDENTITY_INSERT [dbo].[Customer_invoice] OFF
SET IDENTITY_INSERT [dbo].[Pet] ON 

INSERT [dbo].[Pet] ([pet_id], [type_id], [Age], [owner_id]) VALUES (1, 2, 8, 1)
INSERT [dbo].[Pet] ([pet_id], [type_id], [Age], [owner_id]) VALUES (2, 1, 12, 2)
INSERT [dbo].[Pet] ([pet_id], [type_id], [Age], [owner_id]) VALUES (3, 3, 1, 3)
INSERT [dbo].[Pet] ([pet_id], [type_id], [Age], [owner_id]) VALUES (4, 5, 4, 4)
INSERT [dbo].[Pet] ([pet_id], [type_id], [Age], [owner_id]) VALUES (5, 5, 3, 5)
INSERT [dbo].[Pet] ([pet_id], [type_id], [Age], [owner_id]) VALUES (6, 1, 2, 6)
SET IDENTITY_INSERT [dbo].[Pet] OFF
SET IDENTITY_INSERT [dbo].[Pet_id] ON 

INSERT [dbo].[Pet_id] ([pet_id], [pet_name]) VALUES (1, N'Morris')
INSERT [dbo].[Pet_id] ([pet_id], [pet_name]) VALUES (2, N'Rover')
INSERT [dbo].[Pet_id] ([pet_id], [pet_name]) VALUES (3, N'Tweety')
INSERT [dbo].[Pet_id] ([pet_id], [pet_name]) VALUES (4, N'Truffles')
INSERT [dbo].[Pet_id] ([pet_id], [pet_name]) VALUES (5, N'Mickey')
INSERT [dbo].[Pet_id] ([pet_id], [pet_name]) VALUES (6, N'Lana')
SET IDENTITY_INSERT [dbo].[Pet_id] OFF
SET IDENTITY_INSERT [dbo].[pet_owner] ON 

INSERT [dbo].[pet_owner] ([owner_id], [First_name], [Last_name]) VALUES (1, N'Sam                 ', N'Cook                ')
INSERT [dbo].[pet_owner] ([owner_id], [First_name], [Last_name]) VALUES (2, N'Terry               ', N'Kim                 ')
INSERT [dbo].[pet_owner] ([owner_id], [First_name], [Last_name]) VALUES (3, N'David               ', N'Shanks              ')
INSERT [dbo].[pet_owner] ([owner_id], [First_name], [Last_name]) VALUES (4, N'Amanda              ', N'Gamberale           ')
INSERT [dbo].[pet_owner] ([owner_id], [First_name], [Last_name]) VALUES (5, N'Jerry               ', N'Springer            ')
INSERT [dbo].[pet_owner] ([owner_id], [First_name], [Last_name]) VALUES (6, N'Steven              ', N'Hawkings            ')
SET IDENTITY_INSERT [dbo].[pet_owner] OFF
INSERT [dbo].[Price] ([procedure_id], [type_id], [cost]) VALUES (1, 1, 30.0000)
INSERT [dbo].[Price] ([procedure_id], [type_id], [cost]) VALUES (1, 2, 24.0000)
INSERT [dbo].[Price] ([procedure_id], [type_id], [cost]) VALUES (1, 3, 20.0000)
INSERT [dbo].[Price] ([procedure_id], [type_id], [cost]) VALUES (1, 4, 20.0000)
INSERT [dbo].[Price] ([procedure_id], [type_id], [cost]) VALUES (2, 1, 50.0000)
INSERT [dbo].[Price] ([procedure_id], [type_id], [cost]) VALUES (2, 2, 40.0000)
INSERT [dbo].[Price] ([procedure_id], [type_id], [cost]) VALUES (2, 3, 30.0000)
INSERT [dbo].[Price] ([procedure_id], [type_id], [cost]) VALUES (2, 4, 30.0000)
INSERT [dbo].[Price] ([procedure_id], [type_id], [cost]) VALUES (3, 1, 30.0000)
INSERT [dbo].[Price] ([procedure_id], [type_id], [cost]) VALUES (3, 2, 28.0000)
INSERT [dbo].[Price] ([procedure_id], [type_id], [cost]) VALUES (3, 3, 23.0000)
INSERT [dbo].[Price] ([procedure_id], [type_id], [cost]) VALUES (3, 4, 23.0000)
INSERT [dbo].[Price] ([procedure_id], [type_id], [cost]) VALUES (4, 1, 25.0000)
INSERT [dbo].[Price] ([procedure_id], [type_id], [cost]) VALUES (4, 2, 25.0000)
INSERT [dbo].[Price] ([procedure_id], [type_id], [cost]) VALUES (4, 3, 20.0000)
INSERT [dbo].[Price] ([procedure_id], [type_id], [cost]) VALUES (4, 4, 20.0000)
INSERT [dbo].[Price] ([procedure_id], [type_id], [cost]) VALUES (5, 1, 60.0000)
INSERT [dbo].[Price] ([procedure_id], [type_id], [cost]) VALUES (5, 2, 55.0000)
INSERT [dbo].[Price] ([procedure_id], [type_id], [cost]) VALUES (5, 3, 30.0000)
INSERT [dbo].[Price] ([procedure_id], [type_id], [cost]) VALUES (5, 4, 30.0000)
INSERT [dbo].[Price] ([procedure_id], [type_id], [cost]) VALUES (6, 1, 10.0000)
INSERT [dbo].[Price] ([procedure_id], [type_id], [cost]) VALUES (6, 2, 8.0000)
INSERT [dbo].[Price] ([procedure_id], [type_id], [cost]) VALUES (6, 3, 5.0000)
INSERT [dbo].[Price] ([procedure_id], [type_id], [cost]) VALUES (6, 4, 5.0000)
SET IDENTITY_INSERT [dbo].[Procedure] ON 

INSERT [dbo].[Procedure] ([procedure_id], [Description]) VALUES (1, N'Rabbies Vaccination')
INSERT [dbo].[Procedure] ([procedure_id], [Description]) VALUES (2, N'Examine and Treat Wound')
INSERT [dbo].[Procedure] ([procedure_id], [Description]) VALUES (3, N'Heart Worm Test')
INSERT [dbo].[Procedure] ([procedure_id], [Description]) VALUES (4, N'Tetanus Vaccination')
INSERT [dbo].[Procedure] ([procedure_id], [Description]) VALUES (5, N'Annual Check Up')
INSERT [dbo].[Procedure] ([procedure_id], [Description]) VALUES (6, N'EyeWash')
INSERT [dbo].[Procedure] ([procedure_id], [Description]) VALUES (7, N'Nail Clip')
SET IDENTITY_INSERT [dbo].[Procedure] OFF
SET IDENTITY_INSERT [dbo].[Type] ON 

INSERT [dbo].[Type] ([type_id], [type_name]) VALUES (1, N'Dog       ')
INSERT [dbo].[Type] ([type_id], [type_name]) VALUES (2, N'Cat       ')
INSERT [dbo].[Type] ([type_id], [type_name]) VALUES (3, N'Bird      ')
INSERT [dbo].[Type] ([type_id], [type_name]) VALUES (5, N'Rodent    ')
SET IDENTITY_INSERT [dbo].[Type] OFF
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_pet_owner] FOREIGN KEY([owner_id])
REFERENCES [dbo].[pet_owner] ([owner_id])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_pet_owner]
GO
ALTER TABLE [dbo].[Customer_invoice]  WITH CHECK ADD  CONSTRAINT [FK_Customer_invoice_pet_owner] FOREIGN KEY([invoice_id])
REFERENCES [dbo].[pet_owner] ([owner_id])
GO
ALTER TABLE [dbo].[Customer_invoice] CHECK CONSTRAINT [FK_Customer_invoice_pet_owner]
GO
ALTER TABLE [dbo].[Customer_invoice]  WITH CHECK ADD  CONSTRAINT [FK_Customer_invoice_Procedure] FOREIGN KEY([procedure_id])
REFERENCES [dbo].[Procedure] ([procedure_id])
GO
ALTER TABLE [dbo].[Customer_invoice] CHECK CONSTRAINT [FK_Customer_invoice_Procedure]
GO
ALTER TABLE [dbo].[Pet]  WITH CHECK ADD  CONSTRAINT [FK_Pet_owner_id] FOREIGN KEY([owner_id])
REFERENCES [dbo].[pet_owner] ([owner_id])
GO
ALTER TABLE [dbo].[Pet] CHECK CONSTRAINT [FK_Pet_owner_id]
GO
ALTER TABLE [dbo].[Pet]  WITH CHECK ADD  CONSTRAINT [FK_Pet_Type] FOREIGN KEY([type_id])
REFERENCES [dbo].[Type] ([type_id])
GO
ALTER TABLE [dbo].[Pet] CHECK CONSTRAINT [FK_Pet_Type]
GO
ALTER TABLE [dbo].[Pet_id]  WITH CHECK ADD  CONSTRAINT [FK_Pet_id_Pet] FOREIGN KEY([pet_id])
REFERENCES [dbo].[Pet] ([pet_id])
GO
ALTER TABLE [dbo].[Pet_id] CHECK CONSTRAINT [FK_Pet_id_Pet]
GO
ALTER TABLE [dbo].[Price]  WITH CHECK ADD  CONSTRAINT [FK_Price_Procedure1] FOREIGN KEY([procedure_id])
REFERENCES [dbo].[Procedure] ([procedure_id])
GO
ALTER TABLE [dbo].[Price] CHECK CONSTRAINT [FK_Price_Procedure1]
GO
USE [master]
GO
ALTER DATABASE [Animal Hospital] SET  READ_WRITE 
GO
