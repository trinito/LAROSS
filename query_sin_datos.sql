USE [master]
GO
/****** Object:  Database [la_ross_db]    Script Date: 21/06/2025 04:28:56 a. m. ******/
CREATE DATABASE [la_ross_db]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LaRoss_db', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\LaRoss_db.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'LaRoss_db_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\LaRoss_db_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [la_ross_db] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [la_ross_db].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [la_ross_db] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [la_ross_db] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [la_ross_db] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [la_ross_db] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [la_ross_db] SET ARITHABORT OFF 
GO
ALTER DATABASE [la_ross_db] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [la_ross_db] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [la_ross_db] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [la_ross_db] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [la_ross_db] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [la_ross_db] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [la_ross_db] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [la_ross_db] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [la_ross_db] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [la_ross_db] SET  ENABLE_BROKER 
GO
ALTER DATABASE [la_ross_db] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [la_ross_db] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [la_ross_db] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [la_ross_db] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [la_ross_db] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [la_ross_db] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [la_ross_db] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [la_ross_db] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [la_ross_db] SET  MULTI_USER 
GO
ALTER DATABASE [la_ross_db] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [la_ross_db] SET DB_CHAINING OFF 
GO
ALTER DATABASE [la_ross_db] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [la_ross_db] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [la_ross_db] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [la_ross_db] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [la_ross_db] SET QUERY_STORE = ON
GO
ALTER DATABASE [la_ross_db] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [la_ross_db]
GO
/****** Object:  Table [dbo].[Articulos]    Script Date: 21/06/2025 04:28:56 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Articulos](
	[id_producto] [int] IDENTITY(1,1) NOT NULL,
	[codigo_barras_original] [varchar](50) NOT NULL,
	[codigo_barras] [varchar](50) NOT NULL,
	[nombre] [varchar](100) NOT NULL,
	[id_marca] [int] NOT NULL,
	[id_color] [int] NOT NULL,
	[id_talla] [int] NOT NULL,
	[id_sexo] [int] NOT NULL,
	[foto] [varchar](255) NULL,
	[precio_venta] [money] NOT NULL,
	[precio_costo] [money] NOT NULL,
	[stock] [int] NOT NULL,
	[estatus] [bit] NOT NULL,
	[id_categoria] [int] NULL,
 CONSTRAINT [PK__Articulo__FF341C0DEB0384E7] PRIMARY KEY CLUSTERED 
(
	[id_producto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ__Articulo__730FA6AB51D9D30A] UNIQUE NONCLUSTERED 
(
	[codigo_barras] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Articulos_codigo_barras_original] UNIQUE NONCLUSTERED 
(
	[codigo_barras_original] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Venta]    Script Date: 21/06/2025 04:28:56 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venta](
	[id_venta] [int] IDENTITY(1,1) NOT NULL,
	[fecha] [date] NOT NULL,
	[hora] [varchar](50) NULL,
	[cantidad_productos] [int] NOT NULL,
	[total] [money] NOT NULL,
	[estatus] [bit] NOT NULL,
	[forma_pago] [varchar](50) NOT NULL,
	[fecha_editado] [datetime] NULL,
	[id_usuario_editado] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_venta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetalleVenta]    Script Date: 21/06/2025 04:28:56 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetalleVenta](
	[id_detalle] [int] IDENTITY(1,1) NOT NULL,
	[id_venta] [int] NOT NULL,
	[id_producto] [int] NOT NULL,
	[cantidad] [int] NOT NULL,
	[precio_unitario] [money] NOT NULL,
	[subtotal] [money] NOT NULL,
	[fecha_editado] [datetime] NULL,
	[id_usuario_editado] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_detalle] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ViewCorte]    Script Date: 21/06/2025 04:28:56 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [dbo].[ViewCorte] AS
SELECT 
    a.nombre AS Nombre,
    SUM(dv.cantidad) AS Cantidad,
    SUM(dv.subtotal) AS Total,
    v.fecha
FROM DetalleVenta dv
INNER JOIN Venta v ON dv.id_venta = v.id_venta
INNER JOIN Articulos a ON dv.id_producto = a.id_producto
GROUP BY a.nombre, v.fecha;
GO
/****** Object:  Table [dbo].[Categorias]    Script Date: 21/06/2025 04:28:56 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categorias](
	[id_categoria] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[estatus] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_categoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Colores]    Script Date: 21/06/2025 04:28:56 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Colores](
	[id_color] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[estatus] [bit] NOT NULL,
 CONSTRAINT [PK__Colores__7CF2AF03B233D635] PRIMARY KEY CLUSTERED 
(
	[id_color] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ__Colores__72AFBCC62ECAC330] UNIQUE NONCLUSTERED 
(
	[nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Marcas]    Script Date: 21/06/2025 04:28:56 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Marcas](
	[id_marca] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[estatus] [bit] NOT NULL,
 CONSTRAINT [PK__Marcas__7E43E99EE265E680] PRIMARY KEY CLUSTERED 
(
	[id_marca] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ__Marcas__72AFBCC603CBF681] UNIQUE NONCLUSTERED 
(
	[nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sexos]    Script Date: 21/06/2025 04:28:56 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sexos](
	[id_sexo] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [char](1) NOT NULL,
	[estatus] [bit] NOT NULL,
 CONSTRAINT [PK__Sexos__D692FEE6EF3B14E6] PRIMARY KEY CLUSTERED 
(
	[id_sexo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ__Sexos__72AFBCC67960A8A3] UNIQUE NONCLUSTERED 
(
	[nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tallas]    Script Date: 21/06/2025 04:28:56 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tallas](
	[id_talla] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](10) NOT NULL,
	[estatus] [bit] NOT NULL,
 CONSTRAINT [PK__Tallas__C16F403D13654B32] PRIMARY KEY CLUSTERED 
(
	[id_talla] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ__Tallas__72AFBCC60C530EE3] UNIQUE NONCLUSTERED 
(
	[nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 21/06/2025 04:28:56 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[apellido] [varchar](50) NOT NULL,
	[contra] [varchar](50) NOT NULL,
	[username] [varchar](50) NOT NULL,
	[tipo] [varchar](50) NOT NULL,
	[permisos] [nvarchar](max) NULL,
	[estatus] [bit] NOT NULL,
 CONSTRAINT [PK__Usuarios__3213E83FAECDB762] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Articulos] ADD  CONSTRAINT [DF__Articulos__stock__4AB81AF0]  DEFAULT ((0)) FOR [stock]
GO
ALTER TABLE [dbo].[Articulos] ADD  CONSTRAINT [DF__Articulos__estat__4BAC3F29]  DEFAULT ((1)) FOR [estatus]
GO
ALTER TABLE [dbo].[Categorias] ADD  DEFAULT ((1)) FOR [estatus]
GO
ALTER TABLE [dbo].[Usuarios] ADD  CONSTRAINT [DF__Usuarios__apelli__5BE2A6F2]  DEFAULT ('') FOR [apellido]
GO
ALTER TABLE [dbo].[Usuarios] ADD  CONSTRAINT [DF__Usuarios__userna__5AEE82B9]  DEFAULT ('') FOR [username]
GO
ALTER TABLE [dbo].[Usuarios] ADD  CONSTRAINT [DF__Usuarios__estatu__46E78A0C]  DEFAULT ((1)) FOR [estatus]
GO
ALTER TABLE [dbo].[Venta] ADD  DEFAULT ((1)) FOR [estatus]
GO
ALTER TABLE [dbo].[Articulos]  WITH CHECK ADD  CONSTRAINT [FK_Articulos_Categorias] FOREIGN KEY([id_categoria])
REFERENCES [dbo].[Categorias] ([id_categoria])
GO
ALTER TABLE [dbo].[Articulos] CHECK CONSTRAINT [FK_Articulos_Categorias]
GO
ALTER TABLE [dbo].[Articulos]  WITH CHECK ADD  CONSTRAINT [FK_Articulos_Colores] FOREIGN KEY([id_color])
REFERENCES [dbo].[Colores] ([id_color])
GO
ALTER TABLE [dbo].[Articulos] CHECK CONSTRAINT [FK_Articulos_Colores]
GO
ALTER TABLE [dbo].[Articulos]  WITH CHECK ADD  CONSTRAINT [FK_Articulos_Marcas] FOREIGN KEY([id_marca])
REFERENCES [dbo].[Marcas] ([id_marca])
GO
ALTER TABLE [dbo].[Articulos] CHECK CONSTRAINT [FK_Articulos_Marcas]
GO
ALTER TABLE [dbo].[Articulos]  WITH CHECK ADD  CONSTRAINT [FK_Articulos_Sexos] FOREIGN KEY([id_sexo])
REFERENCES [dbo].[Sexos] ([id_sexo])
GO
ALTER TABLE [dbo].[Articulos] CHECK CONSTRAINT [FK_Articulos_Sexos]
GO
ALTER TABLE [dbo].[Articulos]  WITH CHECK ADD  CONSTRAINT [FK_Articulos_Tallas] FOREIGN KEY([id_talla])
REFERENCES [dbo].[Tallas] ([id_talla])
GO
ALTER TABLE [dbo].[Articulos] CHECK CONSTRAINT [FK_Articulos_Tallas]
GO
ALTER TABLE [dbo].[DetalleVenta]  WITH CHECK ADD  CONSTRAINT [FK_DetalleVenta_Articulo] FOREIGN KEY([id_producto])
REFERENCES [dbo].[Articulos] ([id_producto])
GO
ALTER TABLE [dbo].[DetalleVenta] CHECK CONSTRAINT [FK_DetalleVenta_Articulo]
GO
ALTER TABLE [dbo].[DetalleVenta]  WITH CHECK ADD  CONSTRAINT [FK_DetalleVenta_UsuarioEditado] FOREIGN KEY([id_usuario_editado])
REFERENCES [dbo].[Usuarios] ([id])
GO
ALTER TABLE [dbo].[DetalleVenta] CHECK CONSTRAINT [FK_DetalleVenta_UsuarioEditado]
GO
ALTER TABLE [dbo].[DetalleVenta]  WITH CHECK ADD  CONSTRAINT [FK_DetalleVenta_Venta] FOREIGN KEY([id_venta])
REFERENCES [dbo].[Venta] ([id_venta])
GO
ALTER TABLE [dbo].[DetalleVenta] CHECK CONSTRAINT [FK_DetalleVenta_Venta]
GO
ALTER TABLE [dbo].[Venta]  WITH CHECK ADD  CONSTRAINT [FK_Venta_UsuarioEditado] FOREIGN KEY([id_usuario_editado])
REFERENCES [dbo].[Usuarios] ([id])
GO
ALTER TABLE [dbo].[Venta] CHECK CONSTRAINT [FK_Venta_UsuarioEditado]
GO
USE [master]
GO
ALTER DATABASE [la_ross_db] SET  READ_WRITE 
GO
