USE [master]
GO
/****** Object:  Database [WMDB]    Script Date: 10.03.2015 0:17:25 ******/
CREATE DATABASE [WMDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'db1', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\WMDB.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'db1_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\WMDB_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [WMDB] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [WMDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [WMDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [WMDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [WMDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [WMDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [WMDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [WMDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [WMDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [WMDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [WMDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [WMDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [WMDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [WMDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [WMDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [WMDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [WMDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [WMDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [WMDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [WMDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [WMDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [WMDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [WMDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [WMDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [WMDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [WMDB] SET  MULTI_USER 
GO
ALTER DATABASE [WMDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [WMDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [WMDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [WMDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [WMDB]
GO
/****** Object:  Table [dbo].[Buyer]    Script Date: 10.03.2015 0:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Buyer](
	[BuyerID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Address] [varchar](50) NOT NULL,
	[Orders_count] [int] NOT NULL,
	[Registration_date] [datetime] NOT NULL,
	[Avatar] [image] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Dialog_dispute]    Script Date: 10.03.2015 0:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Dialog_dispute](
	[DisputeID] [int] IDENTITY(1,1) NOT NULL,
	[BuyerID] [int] NOT NULL,
	[SellerID] [int] NOT NULL,
	[AgentID] [int] NOT NULL,
	[Dispute_statusID] [int] NOT NULL,
	[Open_date] [datetime] NOT NULL,
	[IsDispute] [bit] NOT NULL,
	[Close_date] [datetime] NULL,
	[Resolution_text] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Dispute_status]    Script Date: 10.03.2015 0:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Dispute_status](
	[Dispute_statusID] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_DISPUTE_STATUS] PRIMARY KEY CLUSTERED 
(
	[Dispute_statusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Item]    Script Date: 10.03.2015 0:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Item](
	[ItemID] [int] IDENTITY(1,1) NOT NULL,
	[StoreID] [int] NOT NULL,
	[CategoryID] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Price] [int] NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[Orders_count] [int] NOT NULL,
	[Reviews_count] [int] NOT NULL,
	[Positive_marks] [int] NOT NULL,
	[Negative_marks] [int] NOT NULL,
	[Minimum_order] [int] NOT NULL,
	[Open_date] [datetime] NOT NULL,
	[Close_date] [datetime] NULL,
	[Left_goods_count] [int] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Item_category]    Script Date: 10.03.2015 0:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Item_category](
	[CategoryID] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ITEM_CATEGORY] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Message]    Script Date: 10.03.2015 0:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Message](
	[MessageID] [int] IDENTITY(1,1) NOT NULL,
	[DisputeID] [int] NOT NULL,
	[Text] [varchar](50) NOT NULL,
	[Post_date] [datetime] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Order]    Script Date: 10.03.2015 0:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Order](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[AgentID] [int] NOT NULL,
	[BuyerID] [int] NOT NULL,
	[ItemID] [int] NOT NULL,
	[DisputeID] [int] NOT NULL,
	[SellerID] [int] NOT NULL,
	[ShippingID] [int] NOT NULL,
	[Order_statusID] [int] NOT NULL,
	[Total_price] [int] NOT NULL,
	[Open_date] [datetime] NOT NULL,
	[Close_date] [datetime] NULL,
	[Amount] [int] NOT NULL,
	[Mark] [int] NULL,
	[Review_text] [varchar](1000) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Order_status]    Script Date: 10.03.2015 0:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_status](
	[Order_statusID] [int] NOT NULL,
	[Name] [text] NOT NULL,
 CONSTRAINT [PK_ORDER_STATUS] PRIMARY KEY CLUSTERED 
(
	[Order_statusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Picture]    Script Date: 10.03.2015 0:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Picture](
	[PictureID] [int] IDENTITY(1,1) NOT NULL,
	[ItemID] [int] NULL,
	[MessageID] [int] NULL,
	[Description] [varchar](50) NULL,
	[Image] [image] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Role]    Script Date: 10.03.2015 0:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Role](
	[RoleID] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Seller]    Script Date: 10.03.2015 0:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Seller](
	[SellerID] [int] IDENTITY(1,1) NOT NULL,
	[StoreID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Registration_date] [datetime] NOT NULL,
	[Avatar] [image] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Shipping_type]    Script Date: 10.03.2015 0:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Shipping_type](
	[ShippingID] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Price] [int] NOT NULL,
 CONSTRAINT [PK_SHIPPING_TYPE] PRIMARY KEY CLUSTERED 
(
	[ShippingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Store]    Script Date: 10.03.2015 0:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Store](
	[StoreID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Rating] [int] NOT NULL,
	[Orders_count] [int] NOT NULL,
	[Positive_marks] [int] NOT NULL,
	[Negative_marks] [int] NOT NULL,
	[Created_date] [datetime] NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[Owner_sellerID] [int] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Support_agent]    Script Date: 10.03.2015 0:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Support_agent](
	[AgentID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Solved_count] [int] NOT NULL,
	[Registration_date] [datetime] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 10.03.2015 0:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[RoleID] [int] NOT NULL,
	[Login] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Buyer] ON 

INSERT [dbo].[Buyer] ([BuyerID], [UserID], [Name], [Address], [Orders_count], [Registration_date], [Avatar]) VALUES (1, 9, N'123', N'123', 0, CAST(N'2015-03-03 10:55:13.197' AS DateTime), NULL)
INSERT [dbo].[Buyer] ([BuyerID], [UserID], [Name], [Address], [Orders_count], [Registration_date], [Avatar]) VALUES (2, 13, N'красава', N'красава', 0, CAST(N'2015-03-08 00:46:09.557' AS DateTime), NULL)
INSERT [dbo].[Buyer] ([BuyerID], [UserID], [Name], [Address], [Orders_count], [Registration_date], [Avatar]) VALUES (3, 14, N'putin', N'moscow', 0, CAST(N'2015-03-09 12:28:55.963' AS DateTime), NULL)
INSERT [dbo].[Buyer] ([BuyerID], [UserID], [Name], [Address], [Orders_count], [Registration_date], [Avatar]) VALUES (4, 22, N'11111', N'11111', 0, CAST(N'2015-03-09 12:50:37.070' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Buyer] OFF
SET IDENTITY_INSERT [dbo].[Dialog_dispute] ON 

INSERT [dbo].[Dialog_dispute] ([DisputeID], [BuyerID], [SellerID], [AgentID], [Dispute_statusID], [Open_date], [IsDispute], [Close_date], [Resolution_text]) VALUES (1, 4, 1, 1, 1, CAST(N'2015-03-01 00:00:00.000' AS DateTime), 0, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Dialog_dispute] OFF
INSERT [dbo].[Dispute_status] ([Dispute_statusID], [Name]) VALUES (1, N'поговорили')
SET IDENTITY_INSERT [dbo].[Item] ON 

INSERT [dbo].[Item] ([ItemID], [StoreID], [CategoryID], [Name], [Price], [Description], [Orders_count], [Reviews_count], [Positive_marks], [Negative_marks], [Minimum_order], [Open_date], [Close_date], [Left_goods_count]) VALUES (6, 1, 1, N'Тапочки', 1200, N'описание, картинки, всякая фигня', 20, 3, 2, 7, 1, CAST(N'2015-01-01 00:00:00.000' AS DateTime), CAST(N'2015-09-17 00:00:00.000' AS DateTime), 52)
INSERT [dbo].[Item] ([ItemID], [StoreID], [CategoryID], [Name], [Price], [Description], [Orders_count], [Reviews_count], [Positive_marks], [Negative_marks], [Minimum_order], [Open_date], [Close_date], [Left_goods_count]) VALUES (7, 1, 1, N'Носки золотые', 1200, N'описание, картинки, всякая фигня', 20, 3, 2, 7, 1, CAST(N'2015-01-01 00:00:00.000' AS DateTime), CAST(N'2015-09-17 00:00:00.000' AS DateTime), 52)
SET IDENTITY_INSERT [dbo].[Item] OFF
INSERT [dbo].[Item_category] ([CategoryID], [Name]) VALUES (1, N'Одежда')
SET IDENTITY_INSERT [dbo].[Order] ON 

INSERT [dbo].[Order] ([OrderID], [AgentID], [BuyerID], [ItemID], [DisputeID], [SellerID], [ShippingID], [Order_statusID], [Total_price], [Open_date], [Close_date], [Amount], [Mark], [Review_text]) VALUES (15, 1, 4, 6, 1, 1, 1, 1, 3270, CAST(N'2015-03-04 00:00:00.000' AS DateTime), CAST(N'2015-03-07 00:00:00.000' AS DateTime), 2, 5, N'Хороший товар и все такое')
INSERT [dbo].[Order] ([OrderID], [AgentID], [BuyerID], [ItemID], [DisputeID], [SellerID], [ShippingID], [Order_statusID], [Total_price], [Open_date], [Close_date], [Amount], [Mark], [Review_text]) VALUES (19, 1, 2, 6, 1, 1, 1, 1, 1099, CAST(N'2015-03-04 00:00:00.000' AS DateTime), CAST(N'2015-03-07 00:00:00.000' AS DateTime), 2, 1, N'Плохой товар, все плохо и дорого и вообааааааааааб ыаврыаврыавыврыавр ыв рывар вы рыв рыв рвр вравыыа рв выы арыва ва')
INSERT [dbo].[Order] ([OrderID], [AgentID], [BuyerID], [ItemID], [DisputeID], [SellerID], [ShippingID], [Order_statusID], [Total_price], [Open_date], [Close_date], [Amount], [Mark], [Review_text]) VALUES (22, 1, 2, 7, 1, 1, 1, 1, 1099, CAST(N'2015-03-04 00:00:00.000' AS DateTime), CAST(N'2015-03-07 00:00:00.000' AS DateTime), 2, 5, N'Плохой товар,')
SET IDENTITY_INSERT [dbo].[Order] OFF
INSERT [dbo].[Order_status] ([Order_statusID], [Name]) VALUES (1, N'норм')
INSERT [dbo].[Role] ([RoleID], [Name]) VALUES (0, N'Покупатель')
INSERT [dbo].[Role] ([RoleID], [Name]) VALUES (1, N'Продавец')
SET IDENTITY_INSERT [dbo].[Seller] ON 

INSERT [dbo].[Seller] ([SellerID], [StoreID], [UserID], [Name], [Registration_date], [Avatar]) VALUES (1, 1, 12, N'Китаец', CAST(N'2000-03-01 00:00:00.000' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Seller] OFF
INSERT [dbo].[Shipping_type] ([ShippingID], [Name], [Price]) VALUES (1, N'почтовый голубь', 203)
SET IDENTITY_INSERT [dbo].[Store] ON 

INSERT [dbo].[Store] ([StoreID], [Name], [Rating], [Orders_count], [Positive_marks], [Negative_marks], [Created_date], [Description], [Owner_sellerID]) VALUES (1, N'Одежда от Путина', 777, 2345, 345, 45, CAST(N'2000-03-06 00:00:00.000' AS DateTime), N'Лучший магазин одежды', 1)
SET IDENTITY_INSERT [dbo].[Store] OFF
SET IDENTITY_INSERT [dbo].[Support_agent] ON 

INSERT [dbo].[Support_agent] ([AgentID], [UserID], [Name], [Solved_count], [Registration_date]) VALUES (1, 14, N'Иван', 876, CAST(N'2010-07-04 00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Support_agent] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserID], [RoleID], [Login], [Password], [Email]) VALUES (3, 0, N'777', N'777', N'777')
INSERT [dbo].[User] ([UserID], [RoleID], [Login], [Password], [Email]) VALUES (4, 0, N'888', N'888', N'888')
INSERT [dbo].[User] ([UserID], [RoleID], [Login], [Password], [Email]) VALUES (5, 0, N'8889', N'8889', N'8889')
INSERT [dbo].[User] ([UserID], [RoleID], [Login], [Password], [Email]) VALUES (6, 0, N'2222', N'2222', N'2222')
INSERT [dbo].[User] ([UserID], [RoleID], [Login], [Password], [Email]) VALUES (7, 0, N'0', N'0', N'0')
INSERT [dbo].[User] ([UserID], [RoleID], [Login], [Password], [Email]) VALUES (8, 0, N'00', N'00', N'00')
INSERT [dbo].[User] ([UserID], [RoleID], [Login], [Password], [Email]) VALUES (9, 0, N'123', N'123', N'123')
INSERT [dbo].[User] ([UserID], [RoleID], [Login], [Password], [Email]) VALUES (10, 0, N'admin', N'admin', N'admin')
INSERT [dbo].[User] ([UserID], [RoleID], [Login], [Password], [Email]) VALUES (11, 0, N'1111', N'1111', N'1111')
INSERT [dbo].[User] ([UserID], [RoleID], [Login], [Password], [Email]) VALUES (12, 0, N'222', N'222', N'222')
INSERT [dbo].[User] ([UserID], [RoleID], [Login], [Password], [Email]) VALUES (13, 0, N'1', N'1', N'1')
INSERT [dbo].[User] ([UserID], [RoleID], [Login], [Password], [Email]) VALUES (14, 0, N'test', N'test', N'test')
INSERT [dbo].[User] ([UserID], [RoleID], [Login], [Password], [Email]) VALUES (15, 0, N'8888', N'8888', N'8888')
INSERT [dbo].[User] ([UserID], [RoleID], [Login], [Password], [Email]) VALUES (16, 0, N'9999', N'9999', N'9999')
INSERT [dbo].[User] ([UserID], [RoleID], [Login], [Password], [Email]) VALUES (17, 0, N'0000', N'0000', N'0000')
INSERT [dbo].[User] ([UserID], [RoleID], [Login], [Password], [Email]) VALUES (18, 0, N'22222', N'22222', N'22222')
INSERT [dbo].[User] ([UserID], [RoleID], [Login], [Password], [Email]) VALUES (19, 0, N'3333', N'3333', N'3333')
INSERT [dbo].[User] ([UserID], [RoleID], [Login], [Password], [Email]) VALUES (20, 0, N'7890', N'7890', N'7890')
INSERT [dbo].[User] ([UserID], [RoleID], [Login], [Password], [Email]) VALUES (21, 0, N'gsom1', N'????????', N'sfdhsgf')
INSERT [dbo].[User] ([UserID], [RoleID], [Login], [Password], [Email]) VALUES (22, 0, N'11111', N'????????', N'11111')
SET IDENTITY_INSERT [dbo].[User] OFF
/****** Object:  Index [PK_BUYER]    Script Date: 10.03.2015 0:17:25 ******/
ALTER TABLE [dbo].[Buyer] ADD  CONSTRAINT [PK_BUYER] PRIMARY KEY NONCLUSTERED 
(
	[BuyerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PK_DIALOGUE/DISPUTE]    Script Date: 10.03.2015 0:17:25 ******/
ALTER TABLE [dbo].[Dialog_dispute] ADD  CONSTRAINT [PK_DIALOGUE/DISPUTE] PRIMARY KEY NONCLUSTERED 
(
	[DisputeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Side 1_FK]    Script Date: 10.03.2015 0:17:25 ******/
CREATE NONCLUSTERED INDEX [Side 1_FK] ON [dbo].[Dialog_dispute]
(
	[BuyerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Side 2_FK]    Script Date: 10.03.2015 0:17:25 ******/
CREATE NONCLUSTERED INDEX [Side 2_FK] ON [dbo].[Dialog_dispute]
(
	[AgentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [SIde 3_FK]    Script Date: 10.03.2015 0:17:25 ******/
CREATE NONCLUSTERED INDEX [SIde 3_FK] ON [dbo].[Dialog_dispute]
(
	[SellerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PK_ITEM]    Script Date: 10.03.2015 0:17:25 ******/
ALTER TABLE [dbo].[Item] ADD  CONSTRAINT [PK_ITEM] PRIMARY KEY NONCLUSTERED 
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Place_of_sale_FK]    Script Date: 10.03.2015 0:17:25 ******/
CREATE NONCLUSTERED INDEX [Place_of_sale_FK] ON [dbo].[Item]
(
	[StoreID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PK_MESSAGE]    Script Date: 10.03.2015 0:17:25 ******/
ALTER TABLE [dbo].[Message] ADD  CONSTRAINT [PK_MESSAGE] PRIMARY KEY NONCLUSTERED 
(
	[MessageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Aggregate_FK]    Script Date: 10.03.2015 0:17:25 ******/
CREATE NONCLUSTERED INDEX [Aggregate_FK] ON [dbo].[Message]
(
	[DisputeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PK_ORDER]    Script Date: 10.03.2015 0:17:25 ******/
ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [PK_ORDER] PRIMARY KEY NONCLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Cause_FK]    Script Date: 10.03.2015 0:17:25 ******/
CREATE NONCLUSTERED INDEX [Cause_FK] ON [dbo].[Order]
(
	[DisputeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Customer_FK]    Script Date: 10.03.2015 0:17:25 ******/
CREATE NONCLUSTERED INDEX [Customer_FK] ON [dbo].[Order]
(
	[BuyerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Leader_FK]    Script Date: 10.03.2015 0:17:25 ******/
CREATE NONCLUSTERED INDEX [Leader_FK] ON [dbo].[Order]
(
	[AgentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Part_FK]    Script Date: 10.03.2015 0:17:25 ******/
CREATE NONCLUSTERED INDEX [Part_FK] ON [dbo].[Order]
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Place_of_registration_FK]    Script Date: 10.03.2015 0:17:25 ******/
CREATE NONCLUSTERED INDEX [Place_of_registration_FK] ON [dbo].[Order]
(
	[SellerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PK_PICTURE]    Script Date: 10.03.2015 0:17:25 ******/
ALTER TABLE [dbo].[Picture] ADD  CONSTRAINT [PK_PICTURE] PRIMARY KEY NONCLUSTERED 
(
	[PictureID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Attachment_FK]    Script Date: 10.03.2015 0:17:25 ******/
CREATE NONCLUSTERED INDEX [Attachment_FK] ON [dbo].[Picture]
(
	[MessageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Photo_object_FK]    Script Date: 10.03.2015 0:17:25 ******/
CREATE NONCLUSTERED INDEX [Photo_object_FK] ON [dbo].[Picture]
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PK_SELLER]    Script Date: 10.03.2015 0:17:25 ******/
ALTER TABLE [dbo].[Seller] ADD  CONSTRAINT [PK_SELLER] PRIMARY KEY NONCLUSTERED 
(
	[SellerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Place_of_work_FK]    Script Date: 10.03.2015 0:17:25 ******/
CREATE NONCLUSTERED INDEX [Place_of_work_FK] ON [dbo].[Seller]
(
	[StoreID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PK_STORE]    Script Date: 10.03.2015 0:17:25 ******/
ALTER TABLE [dbo].[Store] ADD  CONSTRAINT [PK_STORE] PRIMARY KEY NONCLUSTERED 
(
	[StoreID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PK_SUPPORT_AGENT]    Script Date: 10.03.2015 0:17:25 ******/
ALTER TABLE [dbo].[Support_agent] ADD  CONSTRAINT [PK_SUPPORT_AGENT] PRIMARY KEY NONCLUSTERED 
(
	[AgentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Buyer]  WITH CHECK ADD  CONSTRAINT [FK_Buyer_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Buyer] CHECK CONSTRAINT [FK_Buyer_User]
GO
ALTER TABLE [dbo].[Dialog_dispute]  WITH CHECK ADD  CONSTRAINT [FK_DIALOGUE_DISPUTE_S_DISPUTE_] FOREIGN KEY([Dispute_statusID])
REFERENCES [dbo].[Dispute_status] ([Dispute_statusID])
GO
ALTER TABLE [dbo].[Dialog_dispute] CHECK CONSTRAINT [FK_DIALOGUE_DISPUTE_S_DISPUTE_]
GO
ALTER TABLE [dbo].[Dialog_dispute]  WITH CHECK ADD  CONSTRAINT [FK_DIALOGUE_SIDE 1_BUYER] FOREIGN KEY([BuyerID])
REFERENCES [dbo].[Buyer] ([BuyerID])
GO
ALTER TABLE [dbo].[Dialog_dispute] CHECK CONSTRAINT [FK_DIALOGUE_SIDE 1_BUYER]
GO
ALTER TABLE [dbo].[Dialog_dispute]  WITH CHECK ADD  CONSTRAINT [FK_DIALOGUE_SIDE 2_SUPPORT_] FOREIGN KEY([AgentID])
REFERENCES [dbo].[Support_agent] ([AgentID])
GO
ALTER TABLE [dbo].[Dialog_dispute] CHECK CONSTRAINT [FK_DIALOGUE_SIDE 2_SUPPORT_]
GO
ALTER TABLE [dbo].[Dialog_dispute]  WITH CHECK ADD  CONSTRAINT [FK_DIALOGUE_SIDE 3_SELLER] FOREIGN KEY([SellerID])
REFERENCES [dbo].[Seller] ([SellerID])
GO
ALTER TABLE [dbo].[Dialog_dispute] CHECK CONSTRAINT [FK_DIALOGUE_SIDE 3_SELLER]
GO
ALTER TABLE [dbo].[Item]  WITH CHECK ADD  CONSTRAINT [FK_ITEM_CATHEGORY_ITEM_CAT] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Item_category] ([CategoryID])
GO
ALTER TABLE [dbo].[Item] CHECK CONSTRAINT [FK_ITEM_CATHEGORY_ITEM_CAT]
GO
ALTER TABLE [dbo].[Item]  WITH CHECK ADD  CONSTRAINT [FK_ITEM_PLACE_OF__STORE] FOREIGN KEY([StoreID])
REFERENCES [dbo].[Store] ([StoreID])
GO
ALTER TABLE [dbo].[Item] CHECK CONSTRAINT [FK_ITEM_PLACE_OF__STORE]
GO
ALTER TABLE [dbo].[Message]  WITH CHECK ADD  CONSTRAINT [FK_MESSAGE_AGGREGATE_DIALOGUE] FOREIGN KEY([DisputeID])
REFERENCES [dbo].[Dialog_dispute] ([DisputeID])
GO
ALTER TABLE [dbo].[Message] CHECK CONSTRAINT [FK_MESSAGE_AGGREGATE_DIALOGUE]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_ORDER_CAUSE_DIALOGUE] FOREIGN KEY([DisputeID])
REFERENCES [dbo].[Dialog_dispute] ([DisputeID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_ORDER_CAUSE_DIALOGUE]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_ORDER_CUSTOMER_BUYER] FOREIGN KEY([BuyerID])
REFERENCES [dbo].[Buyer] ([BuyerID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_ORDER_CUSTOMER_BUYER]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_ORDER_LEADER_SUPPORT_] FOREIGN KEY([AgentID])
REFERENCES [dbo].[Support_agent] ([AgentID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_ORDER_LEADER_SUPPORT_]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_ORDER_PART_ITEM] FOREIGN KEY([ItemID])
REFERENCES [dbo].[Item] ([ItemID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_ORDER_PART_ITEM]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_ORDER_PLACE_OF__SELLER] FOREIGN KEY([SellerID])
REFERENCES [dbo].[Seller] ([SellerID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_ORDER_PLACE_OF__SELLER]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_ORDER_SHIPPING_SHIPPING] FOREIGN KEY([ShippingID])
REFERENCES [dbo].[Shipping_type] ([ShippingID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_ORDER_SHIPPING_SHIPPING]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_ORDER_STATUS_ORDER_ST] FOREIGN KEY([Order_statusID])
REFERENCES [dbo].[Order_status] ([Order_statusID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_ORDER_STATUS_ORDER_ST]
GO
ALTER TABLE [dbo].[Picture]  WITH CHECK ADD  CONSTRAINT [FK_PICTURE_ATTACHMEN_MESSAGE] FOREIGN KEY([MessageID])
REFERENCES [dbo].[Message] ([MessageID])
GO
ALTER TABLE [dbo].[Picture] CHECK CONSTRAINT [FK_PICTURE_ATTACHMEN_MESSAGE]
GO
ALTER TABLE [dbo].[Picture]  WITH CHECK ADD  CONSTRAINT [FK_PICTURE_PHOTO_OBJ_ITEM] FOREIGN KEY([ItemID])
REFERENCES [dbo].[Item] ([ItemID])
GO
ALTER TABLE [dbo].[Picture] CHECK CONSTRAINT [FK_PICTURE_PHOTO_OBJ_ITEM]
GO
ALTER TABLE [dbo].[Seller]  WITH CHECK ADD  CONSTRAINT [FK_SELLER_PLACE_OF__STORE] FOREIGN KEY([StoreID])
REFERENCES [dbo].[Store] ([StoreID])
GO
ALTER TABLE [dbo].[Seller] CHECK CONSTRAINT [FK_SELLER_PLACE_OF__STORE]
GO
ALTER TABLE [dbo].[Seller]  WITH CHECK ADD  CONSTRAINT [FK_Seller_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Seller] CHECK CONSTRAINT [FK_Seller_User]
GO
ALTER TABLE [dbo].[Support_agent]  WITH CHECK ADD  CONSTRAINT [FK_Support_agent_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Support_agent] CHECK CONSTRAINT [FK_Support_agent_User]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([RoleID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
USE [master]
GO
ALTER DATABASE [WMDB] SET  READ_WRITE 
GO
