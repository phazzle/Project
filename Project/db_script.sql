USE [master]
GO
/****** Object:  Database [test]    Script Date: 2022/06/07 12:41:31 ******/
CREATE DATABASE [test]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'test', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\test.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'test_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\test_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [test] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [test].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [test] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [test] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [test] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [test] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [test] SET ARITHABORT OFF 
GO
ALTER DATABASE [test] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [test] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [test] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [test] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [test] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [test] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [test] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [test] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [test] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [test] SET  DISABLE_BROKER 
GO
ALTER DATABASE [test] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [test] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [test] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [test] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [test] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [test] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [test] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [test] SET RECOVERY FULL 
GO
ALTER DATABASE [test] SET  MULTI_USER 
GO
ALTER DATABASE [test] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [test] SET DB_CHAINING OFF 
GO
ALTER DATABASE [test] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [test] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [test] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [test] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'test', N'ON'
GO
ALTER DATABASE [test] SET QUERY_STORE = OFF
GO
USE [test]
GO
/****** Object:  Table [dbo].[OrderHeader]    Script Date: 2022/06/07 12:41:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderHeader](
	[OrderHeaderId] [int] IDENTITY(1,1) NOT NULL,
	[OrderNumber] [int] NOT NULL,
	[OrderTypeId] [int] NOT NULL,
	[OrderStatusId] [int] NOT NULL,
	[CustomerName] [nvarchar](50) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_OrderHeader] PRIMARY KEY CLUSTERED 
(
	[OrderHeaderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderLine]    Script Date: 2022/06/07 12:41:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderLine](
	[OrderLineId] [int] IDENTITY(1,1) NOT NULL,
	[LineNumber] [int] NOT NULL,
	[ProductCode] [nvarchar](50) NOT NULL,
	[ProductTypeId] [int] NOT NULL,
	[CostPrice] [nvarchar](200) NOT NULL,
	[SalesPrice] [nvarchar](200) NOT NULL,
	[Quantity] [int] NOT NULL,
	[OrderHeaderId] [int] NOT NULL,
 CONSTRAINT [PK_OrderLine] PRIMARY KEY CLUSTERED 
(
	[OrderLineId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderStatus]    Script Date: 2022/06/07 12:41:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderStatus](
	[OrderStatusId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_OrderStatus] PRIMARY KEY CLUSTERED 
(
	[OrderStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderType]    Script Date: 2022/06/07 12:41:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderType](
	[OrderTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_OrderType] PRIMARY KEY CLUSTERED 
(
	[OrderTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductType]    Script Date: 2022/06/07 12:41:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductType](
	[ProductTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ProductType] PRIMARY KEY CLUSTERED 
(
	[ProductTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[OrderStatus] ON 

INSERT [dbo].[OrderStatus] ([OrderStatusId], [Name]) VALUES (1, N'New')
INSERT [dbo].[OrderStatus] ([OrderStatusId], [Name]) VALUES (2, N'Processing')
INSERT [dbo].[OrderStatus] ([OrderStatusId], [Name]) VALUES (3, N'Complete')
SET IDENTITY_INSERT [dbo].[OrderStatus] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderType] ON 

INSERT [dbo].[OrderType] ([OrderTypeId], [Name]) VALUES (1, N'Normal')
INSERT [dbo].[OrderType] ([OrderTypeId], [Name]) VALUES (2, N'Staff')
INSERT [dbo].[OrderType] ([OrderTypeId], [Name]) VALUES (3, N' Mechanical')
INSERT [dbo].[OrderType] ([OrderTypeId], [Name]) VALUES (4, N'Perishable')
SET IDENTITY_INSERT [dbo].[OrderType] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductType] ON 

INSERT [dbo].[ProductType] ([ProductTypeId], [Name]) VALUES (1, N'Apparel')
INSERT [dbo].[ProductType] ([ProductTypeId], [Name]) VALUES (2, N'Parts')
INSERT [dbo].[ProductType] ([ProductTypeId], [Name]) VALUES (3, N'Equipment')
INSERT [dbo].[ProductType] ([ProductTypeId], [Name]) VALUES (4, N'Motor')
SET IDENTITY_INSERT [dbo].[ProductType] OFF
GO
ALTER TABLE [dbo].[OrderHeader]  WITH CHECK ADD  CONSTRAINT [FK_OrderHeader_OrderStatus] FOREIGN KEY([OrderStatusId])
REFERENCES [dbo].[OrderStatus] ([OrderStatusId])
GO
ALTER TABLE [dbo].[OrderHeader] CHECK CONSTRAINT [FK_OrderHeader_OrderStatus]
GO
ALTER TABLE [dbo].[OrderHeader]  WITH CHECK ADD  CONSTRAINT [FK_OrderHeader_OrderType] FOREIGN KEY([OrderTypeId])
REFERENCES [dbo].[OrderType] ([OrderTypeId])
GO
ALTER TABLE [dbo].[OrderHeader] CHECK CONSTRAINT [FK_OrderHeader_OrderType]
GO
ALTER TABLE [dbo].[OrderLine]  WITH CHECK ADD  CONSTRAINT [FK_OrderLine_OrderHeader] FOREIGN KEY([OrderHeaderId])
REFERENCES [dbo].[OrderHeader] ([OrderHeaderId])
GO
ALTER TABLE [dbo].[OrderLine] CHECK CONSTRAINT [FK_OrderLine_OrderHeader]
GO
ALTER TABLE [dbo].[OrderLine]  WITH CHECK ADD  CONSTRAINT [FK_OrderLine_ProductType] FOREIGN KEY([ProductTypeId])
REFERENCES [dbo].[ProductType] ([ProductTypeId])
GO
ALTER TABLE [dbo].[OrderLine] CHECK CONSTRAINT [FK_OrderLine_ProductType]
GO
USE [master]
GO
ALTER DATABASE [test] SET  READ_WRITE 
GO
