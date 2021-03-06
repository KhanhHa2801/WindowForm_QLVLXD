USE [master]
GO
/****** Object:  Database [TestDuLieu]    Script Date: 11/27/2021 10:53:57 PM ******/
CREATE DATABASE [TestDuLieu]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TestDuLieu', FILENAME = N'D:\SQL\MSSQL15.SQLEXPRESS\MSSQL\DATA\TestDuLieu.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TestDuLieu_log', FILENAME = N'D:\SQL\MSSQL15.SQLEXPRESS\MSSQL\DATA\TestDuLieu_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [TestDuLieu] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TestDuLieu].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TestDuLieu] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TestDuLieu] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TestDuLieu] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TestDuLieu] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TestDuLieu] SET ARITHABORT OFF 
GO
ALTER DATABASE [TestDuLieu] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [TestDuLieu] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TestDuLieu] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TestDuLieu] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TestDuLieu] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TestDuLieu] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TestDuLieu] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TestDuLieu] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TestDuLieu] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TestDuLieu] SET  ENABLE_BROKER 
GO
ALTER DATABASE [TestDuLieu] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TestDuLieu] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TestDuLieu] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TestDuLieu] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TestDuLieu] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TestDuLieu] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TestDuLieu] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TestDuLieu] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TestDuLieu] SET  MULTI_USER 
GO
ALTER DATABASE [TestDuLieu] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TestDuLieu] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TestDuLieu] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TestDuLieu] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TestDuLieu] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TestDuLieu] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [TestDuLieu] SET QUERY_STORE = OFF
GO
USE [TestDuLieu]
GO
/****** Object:  UserDefinedFunction [dbo].[func_SinhMa]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[func_SinhMa] (@MaCuoiCung varchar(7), @TienTo varchar(3), @Size int)
returns nchar(7)
as
begin
	if @MaCuoiCung = ''
		set @MaCuoiCung = @TienTo + REPLICATE(0, @Size - LEN(@TienTo))
	declare @next_index int, @MaTiepTheo varchar(7)
	set @MaCuoiCung = LTRIM(RTRIM(@MaCuoiCung))
	set @next_index = REPLACE(@MaCuoiCung, @TienTo, '') + 1
	set @Size = @Size - LEN(@TienTo)
	set @MaTiepTheo = @TienTo + REPLICATE(0, @Size - LEN(@TienTo))
	set @MaTiepTheo = @TienTo + RIGHT(REPLICATE(0, @Size) + CONVERT(varchar(Max), @next_index), @Size)
	return @MaTiepTheo
end
GO
/****** Object:  UserDefinedFunction [dbo].[func_ThemDDH_DemBanGhiKH]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[func_ThemDDH_DemBanGhiKH](@MaKH nvarchar(10))
returns int
as
	begin
		declare @count int = 0
		select @count = count(*)
		from tblKhachHang where MaKH = @MaKH
		return @count
	end	
GO
/****** Object:  UserDefinedFunction [dbo].[func_ThemDDH_LayMaHDDVuaThem]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[func_ThemDDH_LayMaHDDVuaThem]()
returns nvarchar(10)
as
begin
	declare @MaHDD nvarchar(10)
	select @MaHDD = (select top(1) MaHDD from tblDatHang order by MaHDD desc)
	return @MaHDD
end
GO
/****** Object:  UserDefinedFunction [dbo].[sp_ThemDDT_LayTongTien]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[sp_ThemDDT_LayTongTien] (@MaHDD nvarchar(10))
returns float
as
begin
	declare @TongTien float
	select @TongTien = TongTien from tblDatHang where MaHDD = @MaHDD
	return @TongTien
end
GO
/****** Object:  Table [dbo].[tblHang]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblHang](
	[MaHang] [nchar](10) NOT NULL,
	[TenHang] [nvarchar](100) NULL,
	[GiaNhap] [float] NULL,
	[GiaBan] [float] NULL,
	[Anh] [ntext] NULL,
	[MaNhomHang] [nchar](10) NULL,
	[DonViTinh] [nvarchar](50) NULL,
	[MoTa] [ntext] NULL,
	[SoLuongTon] [int] NULL,
 CONSTRAINT [PK_tblHang] PRIMARY KEY CLUSTERED 
(
	[MaHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[func_ThemDDT_TaoBangHangChon]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[func_ThemDDT_TaoBangHangChon](@MaHang nvarchar(10), @SoLuong int)
returns table
as
	return
		select MaHang, TenHang, GiaBan, @SoLuong as SoLuong, (@SoLuong*GiaBan) as ThanhTien 
		from tblHang
		where MaHang = @MaHang
GO
/****** Object:  Table [dbo].[DsHangDaChon]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DsHangDaChon](
	[MaHang] [nchar](10) NOT NULL,
	[TenHang] [nvarchar](255) NULL,
	[GiaBan] [float] NULL,
	[SoLuong] [int] NULL,
	[ThanhTien] [float] NULL,
 CONSTRAINT [PK_DsHangDaChon] PRIMARY KEY CLUSTERED 
(
	[MaHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblChiTietDatHang]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblChiTietDatHang](
	[MaHDD] [nchar](10) NULL,
	[MaHang] [nchar](10) NULL,
	[SoLuong] [nchar](10) NULL,
	[ThanhTien] [float] NULL,
	[GiamGia] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblChiTietHDLe]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblChiTietHDLe](
	[MaHDL] [varchar](20) NULL,
	[MaHang] [varchar](20) NULL,
	[SoLuong] [int] NULL,
	[ThanhTien] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblChiTietNhapKho]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblChiTietNhapKho](
	[MaHDN] [nchar](10) NULL,
	[MaHang] [nchar](10) NULL,
	[SoLuong] [int] NULL,
	[ThanhTien] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblChiTietXuatKho]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblChiTietXuatKho](
	[MaHDX] [nchar](10) NULL,
	[MaHang] [nchar](10) NULL,
	[SoLuong] [int] NULL,
	[ThanhTien] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblChucVu]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblChucVu](
	[MaCV] [nchar](10) NOT NULL,
	[TenChucVu] [nchar](10) NULL,
 CONSTRAINT [PK_tblChucVu] PRIMARY KEY CLUSTERED 
(
	[MaCV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblDatHang]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblDatHang](
	[MaHDD] [nchar](10) NOT NULL,
	[MaKH] [nchar](10) NULL,
	[MaNV] [nchar](10) NULL,
	[NgayDatHang] [date] NULL,
	[NgayGiaoHang] [date] NULL,
	[NoiGiaoHang] [nvarchar](255) NULL,
	[PTThanhToan] [nvarchar](50) NULL,
	[TongTien] [float] NULL,
	[TraNgay] [int] NULL,
	[NoLai] [int] NULL,
 CONSTRAINT [PK_tblDatHang] PRIMARY KEY CLUSTERED 
(
	[MaHDD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblHDLe]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblHDLe](
	[MaHDL] [varchar](20) NOT NULL,
	[MaNV] [varchar](20) NULL,
	[TenKH] [nvarchar](50) NULL,
	[SDT] [varchar](20) NULL,
	[NgayDat] [date] NULL,
	[NoiGiaoHang] [nvarchar](100) NULL,
	[TongTien] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaHDL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblKhachHang]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblKhachHang](
	[MaKH] [nchar](10) NOT NULL,
	[TenKH] [nvarchar](100) NULL,
	[SDT] [nchar](15) NULL,
	[DiaChi] [nvarchar](255) NULL,
	[MaNhomKH] [nchar](10) NULL,
	[TienNo] [int] NULL,
 CONSTRAINT [PK_tblKhachHang] PRIMARY KEY CLUSTERED 
(
	[MaKH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblNhaCungCap]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblNhaCungCap](
	[MaNCC] [nchar](10) NOT NULL,
	[TenNCC] [nvarchar](255) NULL,
	[DiaChi] [nvarchar](255) NULL,
	[SDT] [nchar](15) NULL,
	[TienNo] [int] NULL,
 CONSTRAINT [PK_tblNhaCungCap] PRIMARY KEY CLUSTERED 
(
	[MaNCC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblNhanVien]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblNhanVien](
	[MaNV] [nchar](10) NOT NULL,
	[TenNV] [nvarchar](100) NULL,
	[GioiTinh] [nchar](10) NULL,
	[NgaySinh] [date] NULL,
	[SDT] [nchar](15) NULL,
	[Anh] [ntext] NULL,
	[HSL] [float] NULL,
	[LuongCB] [float] NULL,
	[Thuong] [float] NULL,
	[MaCV] [nchar](10) NULL,
 CONSTRAINT [PK_tblNhanVien] PRIMARY KEY CLUSTERED 
(
	[MaNV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblNhapKho]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblNhapKho](
	[MaHDN] [nchar](10) NOT NULL,
	[NgayNhap] [date] NULL,
	[MaNCC] [nchar](10) NULL,
	[MaNV] [nchar](10) NULL,
	[TongTien] [float] NULL,
	[TraNgay] [int] NULL,
	[NoLai] [int] NULL,
 CONSTRAINT [PK_tblNhapKho] PRIMARY KEY CLUSTERED 
(
	[MaHDN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblNhomHang]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblNhomHang](
	[MaNhomHang] [nchar](10) NOT NULL,
	[TenNhomHang] [nvarchar](255) NULL,
 CONSTRAINT [PK_tblNhomHang] PRIMARY KEY CLUSTERED 
(
	[MaNhomHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblNhomKH]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblNhomKH](
	[MaNhomKH] [nchar](10) NOT NULL,
	[TenNhomKH] [nvarchar](150) NULL,
	[UuDai] [float] NULL,
 CONSTRAINT [PK_tblNhomKH] PRIMARY KEY CLUSTERED 
(
	[MaNhomKH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblTaiKhoan2]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblTaiKhoan2](
	[TenDangNhap] [nvarchar](100) NOT NULL,
	[MatKhau] [nvarchar](50) NULL,
	[ChucVu] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[TenDangNhap] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblThongTinCongTy]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblThongTinCongTy](
	[TenCTy] [nvarchar](100) NULL,
	[TenCHang] [nvarchar](100) NULL,
	[DiaChi] [nvarchar](100) NULL,
	[SDT] [varchar](20) NULL,
	[Email] [varchar](50) NULL,
	[MaSoThue] [varchar](50) NULL,
	[SoTaiKhoan] [varchar](50) NULL,
	[TenNganHang] [nvarchar](100) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblXuatKho]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblXuatKho](
	[MaHDX] [nchar](10) NOT NULL,
	[MaKH] [nchar](10) NULL,
	[NgayXuat] [date] NULL,
	[LiDoXuat] [ntext] NULL,
	[ThueVAT] [float] NULL,
	[MaNV] [nchar](10) NULL,
	[TongTien] [float] NULL,
 CONSTRAINT [PK_tblXuatKho] PRIMARY KEY CLUSTERED 
(
	[MaHDX] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0004   ', N'MH0001    ', N'20        ', 2000000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0004   ', N'MH0002    ', N'10        ', 100000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0007   ', N'MH0011    ', N'10        ', 300000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0007   ', N'MH0012    ', N'12        ', 2400000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0007   ', N'MH0013    ', N'20        ', 80000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0005   ', N'MH0004    ', N'10        ', 200000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0005   ', N'MH0005    ', N'10        ', 200000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0005   ', N'MH0006    ', N'25        ', 400000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0005   ', N'MH0007    ', N'10        ', 200000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0006   ', N'MH0008    ', N'20        ', 400000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0005   ', N'MH0009    ', N'10        ', 20000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0005   ', N'MH0010    ', N'20        ', 40000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0005   ', N'MH0011    ', N'10        ', 300000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0006   ', N'MH0010    ', N'10        ', 20000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0006   ', N'MH0011    ', N'20        ', 600000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0006   ', N'MH0007    ', N'10        ', 200000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0008   ', N'MH0014    ', N'10        ', 200000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0008   ', N'MH0015    ', N'20        ', 4000000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0009   ', N'MH0016    ', N'20        ', 2000000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0009   ', N'MH0012    ', N'10        ', 2000000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0009   ', N'MH0017    ', N'20        ', 600000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0010   ', N'MH0018    ', N'10        ', 100000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0011   ', N'MH0019    ', N'20        ', 600000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0011   ', N'MH0020    ', N'30        ', 900000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0012   ', N'MH0021    ', N'10        ', 400000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0012   ', N'MH0020    ', N'30        ', 900000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0012   ', N'MH0022    ', N'20        ', 600000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0012   ', N'MH0019    ', N'10        ', 300000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0013   ', N'MH0023    ', N'10        ', 500000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0013   ', N'MH0024    ', N'20        ', 400000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0013   ', N'MH0025    ', N'30        ', 750000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0013   ', N'MH0022    ', N'40        ', 1200000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0014   ', N'MH0001    ', N'25        ', 1000000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0014   ', N'MH0002    ', N'25        ', 200000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0014   ', N'MH0003    ', N'30        ', 600000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0014   ', N'MH0004    ', N'10        ', 200000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0015   ', N'MH0006    ', N'15        ', 200000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0015   ', N'MH0007    ', N'20        ', 400000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0015   ', N'MH0008    ', N'10        ', 200000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0015   ', N'MH0010    ', N'20        ', 40000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0016   ', N'MH0011    ', N'10        ', 300000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0016   ', N'MH0012    ', N'20        ', 4000000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0017   ', N'MH0020    ', N'10        ', 300000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0018   ', N'MH0021    ', N'20        ', 800000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0018   ', N'MH0019    ', N'10        ', 300000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0017   ', N'MH0017    ', N'20        ', 600000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0019   ', N'MH0015    ', N'10        ', 2000000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0019   ', N'MH0016    ', N'20        ', 2000000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0019   ', N'MH0018    ', N'10        ', 100000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0019   ', N'MH0019    ', N'30        ', 900000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0035   ', N'MH0021    ', N'20        ', 800000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0039   ', N'MH0019    ', N'10        ', 300000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0027   ', N'MH0007    ', N'20        ', 400000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0027   ', N'MH0010    ', N'10        ', 20000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0020   ', N'MH0014    ', N'20        ', 400000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0020   ', N'MH0017    ', N'10        ', 300000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0021   ', N'MH0001    ', N'25        ', 1000000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0022   ', N'MH0003    ', N'20        ', 400000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0023   ', N'MH0006    ', N'35        ', 600000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0024   ', N'MH0001    ', N'25        ', 1000000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0025   ', N'MH0005    ', N'10        ', 200000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0026   ', N'MH0004    ', N'20        ', 400000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0026   ', N'MH0003    ', N'10        ', 200000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0027   ', N'MH0008    ', N'10        ', 200000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0028   ', N'MH0009    ', N'20        ', 40000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0029   ', N'MH0010    ', N'10        ', 20000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0030   ', N'MH0011    ', N'10        ', 300000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0030   ', N'MH0012    ', N'20        ', 4000000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0031   ', N'MH0014    ', N'30        ', 600000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0032   ', N'MH0021    ', N'10        ', 400000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0032   ', N'MH0020    ', N'20        ', 600000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0033   ', N'MH0022    ', N'30        ', 900000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0034   ', N'MH0021    ', N'10        ', 400000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0035   ', N'MH0022    ', N'10        ', 300000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0035   ', N'MH0023    ', N'20        ', 1000000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0036   ', N'MH0024    ', N'10        ', 200000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0037   ', N'MH0025    ', N'10        ', 250000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0037   ', N'MH0024    ', N'20        ', 400000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0038   ', N'MH0023    ', N'20        ', 1000000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0039   ', N'MH0022    ', N'10        ', 300000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0040   ', N'MH0023    ', N'20        ', 1000000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0040   ', N'MH0024    ', N'10        ', 200000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0041   ', N'MH0001    ', N'20        ', 2000000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0041   ', N'MH0002    ', N'10        ', 100000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0041   ', N'MH0003    ', N'5         ', 100000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0041   ', N'MH0012    ', N'10        ', 2000000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0041   ', N'MH0016    ', N'20        ', 2000000, NULL)
GO
INSERT [dbo].[tblChiTietDatHang] ([MaHDD], [MaHang], [SoLuong], [ThanhTien], [GiamGia]) VALUES (N'HDD0042   ', N'MH0001    ', N'10        ', 1000000, NULL)
GO
INSERT [dbo].[tblChiTietHDLe] ([MaHDL], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDL0002', N'MH0001    ', 10, 1000000)
GO
INSERT [dbo].[tblChiTietHDLe] ([MaHDL], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDL0001', N'MH0004    ', 5, 100000)
GO
INSERT [dbo].[tblChiTietHDLe] ([MaHDL], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDL0001', N'MH0005    ', 5, 100000)
GO
INSERT [dbo].[tblChiTietHDLe] ([MaHDL], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDL0001', N'MH0006    ', 5, 100000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0036   ', N'MH0001    ', 10, 1000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0036   ', N'MH0002    ', 10, 100000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0004   ', N'MH0010    ', 1000, 400000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0004   ', N'MH0022    ', 100, NULL)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0004   ', N'MH0009    ', 100, 200000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0004   ', N'MH0010    ', 200, 400000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0004   ', N'MH0007    ', 300, 6000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0005   ', N'MH0021    ', 1000, 40000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0005   ', N'MH0002    ', 1000, 10000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0005   ', N'MH0004    ', 1000, 20000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0005   ', N'MH0025    ', 1000, 25000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0005   ', N'MH0024    ', 1000, 20000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0006   ', N'MH0002    ', 100, 1000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0006   ', N'MH0009    ', 100, 200000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0006   ', N'MH0021    ', 100, 4000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0007   ', N'MH0017    ', 1000, 30000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0007   ', N'MH0018    ', 100, 1000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0008   ', N'MH0020    ', 200, 6000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0009   ', N'MH0011    ', 1000, 30000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0010   ', N'MH0005    ', 100, 2000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0010   ', N'MH0003    ', 100, 2000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0010   ', N'MH0023    ', 200, 10000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0011   ', N'MH0022    ', 100, 3000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0011   ', N'MH0011    ', 100, 3000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0011   ', N'MH0021    ', 100, 4000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0011   ', N'MH0023    ', 200, 10000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0012   ', N'MH0008    ', 200, 4000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0012   ', N'MH0010    ', 1000, 2000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0013   ', N'MH0016    ', 100, 10000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0014   ', N'MH0017    ', 1000, 30000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0014   ', N'MH0018    ', 100, 1000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0014   ', N'MH0019    ', 1000, 30000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0014   ', N'MH0020    ', 200, 6000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0015   ', N'MH0021    ', 200, 8000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0015   ', N'MH0022    ', 1000, 30000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0015   ', N'MH0023    ', 100, 5000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0015   ', N'MH0024    ', 200, 4000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0015   ', N'MH0025    ', 100, 2500000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0016   ', N'MH0001    ', 200, 20000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0016   ', N'MH0002    ', 300, 3000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0016   ', N'MH0003    ', 100, 2000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0017   ', N'MH0004    ', 100, 2000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0017   ', N'MH0005    ', 200, 4000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0018   ', N'MH0001    ', 100, 10000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0019   ', N'MH0010    ', 200, 400000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0019   ', N'MH0011    ', 100, 3000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0020   ', N'MH0014    ', 100, 2000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0021   ', N'MH0013    ', 100, 400000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0022   ', N'MH0011    ', 200, 6000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0023   ', N'MH0010    ', 100, 200000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0023   ', N'MH0009    ', 100, 200000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0023   ', N'MH0008    ', 100, 2000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0024   ', N'MH0002    ', 100, 1000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0025   ', N'MH0024    ', 200, 4000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0025   ', N'MH0025    ', 100, 2500000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0025   ', N'MH0023    ', 200, 10000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0026   ', N'MH0006    ', 1000, 20000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0026   ', N'MH0007    ', 100, 2000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0026   ', N'MH0008    ', 200, 4000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0027   ', N'MH0001    ', 100, 10000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0027   ', N'MH0002    ', 100, 1000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0028   ', N'MH0010    ', 100, 200000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0028   ', N'MH0002    ', 200, 2000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0029   ', N'MH0003    ', 100, 2000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0029   ', N'MH0002    ', 100, 1000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0030   ', N'MH0001    ', 100, 10000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0031   ', N'MH0002    ', 200, 2000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0031   ', N'MH0003    ', 300, 6000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0032   ', N'MH0004    ', 100, 2000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0035   ', N'MH0005    ', 100, 2000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0035   ', N'MH0006    ', 100, 2000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0033   ', N'MH0013    ', 100, 400000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0033   ', N'MH0015    ', 100, 20000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0033   ', N'MH0016    ', 200, 20000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0033   ', N'MH0017    ', 300, 9000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0034   ', N'MH0014    ', 100, 2000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0034   ', N'MH0015    ', 200, 40000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0034   ', N'MH0016    ', 300, 30000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0030   ', N'MH0003    ', 200, 4000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0032   ', N'MH0015    ', 100, 20000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0038   ', N'MH0001    ', 10, 100000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0037   ', N'MH0001    ', 10, 1000000)
GO
INSERT [dbo].[tblChiTietNhapKho] ([MaHDN], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDN0037   ', N'MH0002    ', 10, 100000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0001   ', N'MH0001    ', 10, NULL)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0001   ', N'MH0006    ', 1, NULL)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0001   ', N'MH0003    ', 5, NULL)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0002   ', N'MH0002    ', 5, NULL)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0002   ', N'MH0001    ', 5, NULL)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0002   ', N'MH0004    ', 10, NULL)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0003   ', N'MH0001    ', 10, NULL)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0003   ', N'MH0005    ', 10, NULL)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0004   ', N'MH0004    ', 10, 200000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0004   ', N'MH0003    ', 20, 400000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0004   ', N'MH0001    ', 10, 1000000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0005   ', N'MH0002    ', 20, 200000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0005   ', N'MH0005    ', 10, 200000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0005   ', N'MH0006    ', 5, 100000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0006   ', N'MH0001    ', 10, 1000000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0007   ', N'MH0002    ', 20, 200000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0007   ', N'MH0003    ', 10, 200000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0008   ', N'MH0020    ', 10, 300000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0009   ', N'MH0010    ', 2, 4000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0010   ', N'MH0013    ', 3, 12000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0011   ', N'MH0012    ', 5, 1000000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0012   ', N'MH0014    ', 2, 40000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0013   ', N'MH0008    ', 1, 20000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0014   ', N'MH0009    ', 3, 6000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0014   ', N'MH0010    ', 5, 10000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0015   ', N'MH0011    ', 10, 300000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0016   ', N'MH0021    ', 20, 800000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0017   ', N'MH0022    ', 5, 150000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0018   ', N'MH0001    ', 2, 200000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0019   ', N'MH0002    ', 3, 30000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0020   ', N'MH0018    ', 10, 100000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0021   ', N'MH0019    ', 20, 600000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0022   ', N'MH0010    ', 2, 4000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0023   ', N'MH0011    ', 2, 60000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0024   ', N'MH0019    ', 5, 150000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0025   ', N'MH0020    ', NULL, 0)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0026   ', N'MH0021    ', 10, 400000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0027   ', N'MH0021    ', 20, 800000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0028   ', N'MH0010    ', 3, 6000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0029   ', N'MH0009    ', 5, 10000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0030   ', NULL, NULL, NULL)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0021   ', N'MH0002    ', 10, 100000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0031   ', N'MH0001    ', 10, 1000000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0032   ', N'MH0003    ', 20, 400000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0033   ', N'MH0001    ', 10, 1000000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0034   ', N'MH0005    ', 20, 400000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0034   ', N'MH0006    ', 10, 200000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0035   ', N'MH0008    ', 20, 400000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0036   ', N'MH0009    ', 5, 10000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0037   ', N'MH0010    ', 20, 40000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0038   ', N'MH0019    ', 5, 150000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0039   ', N'MH0020    ', 1, 30000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0040   ', N'MH0022    ', 3, 90000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0040   ', N'MH0023    ', 4, 200000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0041   ', N'MH0022    ', 5, 150000)
GO
INSERT [dbo].[tblChiTietXuatKho] ([MaHDX], [MaHang], [SoLuong], [ThanhTien]) VALUES (N'HDX0042   ', N'MH0022    ', 2, 60000)
GO
INSERT [dbo].[tblChucVu] ([MaCV], [TenChucVu]) VALUES (N'CV0001    ', N'Nh??n Vi??n ')
GO
INSERT [dbo].[tblChucVu] ([MaCV], [TenChucVu]) VALUES (N'CV0002    ', N'Qu???n L??   ')
GO
INSERT [dbo].[tblDatHang] ([MaHDD], [MaKH], [MaNV], [NgayDatHang], [NgayGiaoHang], [NoiGiaoHang], [PTThanhToan], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDD0004   ', N'KH0002    ', N'NV0005    ', CAST(N'2021-11-26' AS Date), CAST(N'2021-11-26' AS Date), N'23/5 Nguy???n Tr??i - Qu???n 5 - Tp.HCM', NULL, 2100000, 2100000, 0)
GO
INSERT [dbo].[tblDatHang] ([MaHDD], [MaKH], [MaNV], [NgayDatHang], [NgayGiaoHang], [NoiGiaoHang], [PTThanhToan], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDD0005   ', N'KH0002    ', N'NV0003    ', CAST(N'2021-09-30' AS Date), CAST(N'2021-10-07' AS Date), N'23/5 Nguy???n Tr??i - Qu???n 5 - Tp.HCM', N'Ti???n M???t', 1360000, 680000, 680000)
GO
INSERT [dbo].[tblDatHang] ([MaHDD], [MaKH], [MaNV], [NgayDatHang], [NgayGiaoHang], [NoiGiaoHang], [PTThanhToan], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDD0006   ', N'KH0003    ', N'NV0003    ', CAST(N'2021-09-20' AS Date), CAST(N'2021-10-02' AS Date), N'32 C???u Gi???y - H?? N???i', N'Ti???n M???t', 1220000, 610000, 610000)
GO
INSERT [dbo].[tblDatHang] ([MaHDD], [MaKH], [MaNV], [NgayDatHang], [NgayGiaoHang], [NoiGiaoHang], [PTThanhToan], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDD0007   ', N'KH0004    ', N'NV0004    ', CAST(N'2021-11-26' AS Date), CAST(N'2021-11-26' AS Date), N'?? Y??n - Nam ?????nh', NULL, 2780000, 1390000, 1390000)
GO
INSERT [dbo].[tblDatHang] ([MaHDD], [MaKH], [MaNV], [NgayDatHang], [NgayGiaoHang], [NoiGiaoHang], [PTThanhToan], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDD0008   ', N'KH0005    ', N'NV0005    ', CAST(N'2021-09-25' AS Date), CAST(N'2021-09-30' AS Date), N'32 ???????ng B?????i - Ba ????nh - H?? N???i', N'Ti???n M???t', 4200000, 2100000, 2100000)
GO
INSERT [dbo].[tblDatHang] ([MaHDD], [MaKH], [MaNV], [NgayDatHang], [NgayGiaoHang], [NoiGiaoHang], [PTThanhToan], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDD0009   ', N'KH0003    ', N'NV0006    ', CAST(N'2021-10-02' AS Date), CAST(N'2021-10-10' AS Date), N'32 C???u Gi???y - H?? N???i', N'Ti???n M???t', 4600000, 2300000, 2300000)
GO
INSERT [dbo].[tblDatHang] ([MaHDD], [MaKH], [MaNV], [NgayDatHang], [NgayGiaoHang], [NoiGiaoHang], [PTThanhToan], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDD0010   ', N'KH0006    ', N'NV0003    ', CAST(N'2021-10-05' AS Date), CAST(N'2021-10-13' AS Date), N'Y??n ?????ng - ?? Y??n - Nam ?????nh', N'Ti???n M???t', 100000, 50000, 50000)
GO
INSERT [dbo].[tblDatHang] ([MaHDD], [MaKH], [MaNV], [NgayDatHang], [NgayGiaoHang], [NoiGiaoHang], [PTThanhToan], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDD0011   ', N'KH0007    ', N'NV0007    ', CAST(N'2021-10-10' AS Date), CAST(N'2021-10-15' AS Date), N'C???u Gi???y - H?? N???i', N'Ti???n M???t', 1500000, 750000, 750000)
GO
INSERT [dbo].[tblDatHang] ([MaHDD], [MaKH], [MaNV], [NgayDatHang], [NgayGiaoHang], [NoiGiaoHang], [PTThanhToan], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDD0012   ', N'KH0008    ', N'NV0008    ', CAST(N'2021-10-11' AS Date), CAST(N'2021-10-20' AS Date), N'????ng Anh - H?? N???i', N'Ti???n M???t', 2200000, 1100000, 1100000)
GO
INSERT [dbo].[tblDatHang] ([MaHDD], [MaKH], [MaNV], [NgayDatHang], [NgayGiaoHang], [NoiGiaoHang], [PTThanhToan], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDD0013   ', N'KH0009    ', N'NV0009    ', CAST(N'2021-09-30' AS Date), CAST(N'2021-10-10' AS Date), N'Ba ????nh - H?? N???i', N'Ti???n M???t', 2850000, 1425000, 1425000)
GO
INSERT [dbo].[tblDatHang] ([MaHDD], [MaKH], [MaNV], [NgayDatHang], [NgayGiaoHang], [NoiGiaoHang], [PTThanhToan], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDD0014   ', N'KH0010    ', N'NV0010    ', CAST(N'2021-06-01' AS Date), CAST(N'2021-06-10' AS Date), N'Ba V?? - H?? N???i', N'Ti???n M???t', 2000000, 1000000, 1000000)
GO
INSERT [dbo].[tblDatHang] ([MaHDD], [MaKH], [MaNV], [NgayDatHang], [NgayGiaoHang], [NoiGiaoHang], [PTThanhToan], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDD0015   ', N'KH0011    ', N'NV0003    ', CAST(N'2021-06-12' AS Date), CAST(N'2021-06-20' AS Date), N'Nguy???n Khanh - C???u Gi???y - H?? N???i', N'Ti???n M???t', 840000, 420000, 420000)
GO
INSERT [dbo].[tblDatHang] ([MaHDD], [MaKH], [MaNV], [NgayDatHang], [NgayGiaoHang], [NoiGiaoHang], [PTThanhToan], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDD0016   ', N'KH0012    ', N'NV0002    ', CAST(N'2021-05-30' AS Date), CAST(N'2021-06-12' AS Date), N'Tr?????ng Trinh - H?? N???i', N'Ti???n M???t', 4300000, 2150000, 2150000)
GO
INSERT [dbo].[tblDatHang] ([MaHDD], [MaKH], [MaNV], [NgayDatHang], [NgayGiaoHang], [NoiGiaoHang], [PTThanhToan], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDD0017   ', N'KH0013    ', N'NV0003    ', CAST(N'2021-05-22' AS Date), CAST(N'2021-05-29' AS Date), N'Y??n H??a - H?? N???i', N'Ti???n M???t', 900000, 450000, 450000)
GO
INSERT [dbo].[tblDatHang] ([MaHDD], [MaKH], [MaNV], [NgayDatHang], [NgayGiaoHang], [NoiGiaoHang], [PTThanhToan], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDD0018   ', N'KH0014    ', N'NV0007    ', CAST(N'2021-04-22' AS Date), CAST(N'2021-04-28' AS Date), N'Qu???n 7 - TP.HCM', N'Ti???n M???t', 1100000, 550000, 550000)
GO
INSERT [dbo].[tblDatHang] ([MaHDD], [MaKH], [MaNV], [NgayDatHang], [NgayGiaoHang], [NoiGiaoHang], [PTThanhToan], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDD0019   ', N'KH0015    ', N'NV0005    ', CAST(N'2021-07-23' AS Date), CAST(N'2021-07-28' AS Date), N'Nguy???n Tr??i - Qu???n 1 - TP.HCM', N'Ti???n M???t', 5000000, 2500000, 2500000)
GO
INSERT [dbo].[tblDatHang] ([MaHDD], [MaKH], [MaNV], [NgayDatHang], [NgayGiaoHang], [NoiGiaoHang], [PTThanhToan], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDD0020   ', N'KH0016    ', N'NV0006    ', CAST(N'2021-07-01' AS Date), CAST(N'2021-07-06' AS Date), N'Thanh Xu??n - H?? N???i', N'Ti???n M???t', 700000, 350000, 350000)
GO
INSERT [dbo].[tblDatHang] ([MaHDD], [MaKH], [MaNV], [NgayDatHang], [NgayGiaoHang], [NoiGiaoHang], [PTThanhToan], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDD0021   ', N'KH0017    ', N'NV0004    ', CAST(N'2021-05-01' AS Date), CAST(N'2021-05-11' AS Date), N'323 ???????ng B?????i - Ba ????nh - H?? N???i ', N'Ti???n M???t', 1000000, 500000, 500000)
GO
INSERT [dbo].[tblDatHang] ([MaHDD], [MaKH], [MaNV], [NgayDatHang], [NgayGiaoHang], [NoiGiaoHang], [PTThanhToan], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDD0022   ', N'KH0018    ', N'NV0008    ', CAST(N'2020-08-02' AS Date), CAST(N'2020-08-10' AS Date), N'Xu??n Th???y - C???u Gi???y - H?? N???i', N'Ti???n M???t', 400000, 200000, 200000)
GO
INSERT [dbo].[tblDatHang] ([MaHDD], [MaKH], [MaNV], [NgayDatHang], [NgayGiaoHang], [NoiGiaoHang], [PTThanhToan], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDD0023   ', N'KH0019    ', N'NV0009    ', CAST(N'2020-08-13' AS Date), CAST(N'2020-08-20' AS Date), N'B??nh L???c - H?? nam', N'Ti???n M???t', 600000, 300000, 300000)
GO
INSERT [dbo].[tblDatHang] ([MaHDD], [MaKH], [MaNV], [NgayDatHang], [NgayGiaoHang], [NoiGiaoHang], [PTThanhToan], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDD0024   ', N'KH0020    ', N'NV0010    ', CAST(N'2020-08-15' AS Date), CAST(N'2020-08-22' AS Date), N'Qu???c Oai - H?? N???i', N'Ti???n M???t', 1000000, 500000, 500000)
GO
INSERT [dbo].[tblDatHang] ([MaHDD], [MaKH], [MaNV], [NgayDatHang], [NgayGiaoHang], [NoiGiaoHang], [PTThanhToan], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDD0025   ', N'KH0021    ', N'NV0004    ', CAST(N'2020-08-09' AS Date), CAST(N'2020-08-16' AS Date), N'????ng Anh - H?? N???i', N'Ti???n M???t', 200000, 100000, 100000)
GO
INSERT [dbo].[tblDatHang] ([MaHDD], [MaKH], [MaNV], [NgayDatHang], [NgayGiaoHang], [NoiGiaoHang], [PTThanhToan], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDD0026   ', N'KH0010    ', N'NV0006    ', CAST(N'2020-09-11' AS Date), CAST(N'2020-09-20' AS Date), N'Ba V?? - H?? N???i', N'Ti???n M???t', 600000, 300000, 300000)
GO
INSERT [dbo].[tblDatHang] ([MaHDD], [MaKH], [MaNV], [NgayDatHang], [NgayGiaoHang], [NoiGiaoHang], [PTThanhToan], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDD0027   ', N'KH0011    ', N'NV0005    ', CAST(N'2020-09-01' AS Date), CAST(N'2020-09-11' AS Date), N'Nguy???n Khanh - C???u Gi???y - H?? N???i', N'Ti???n M???t', 620000, 310000, 310000)
GO
INSERT [dbo].[tblDatHang] ([MaHDD], [MaKH], [MaNV], [NgayDatHang], [NgayGiaoHang], [NoiGiaoHang], [PTThanhToan], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDD0028   ', N'KH0012    ', N'NV0009    ', CAST(N'2020-09-12' AS Date), CAST(N'2020-09-20' AS Date), N'Tr?????ng Trinh - H?? N???i', N'Ti???n M???t', 40000, 20000, 20000)
GO
INSERT [dbo].[tblDatHang] ([MaHDD], [MaKH], [MaNV], [NgayDatHang], [NgayGiaoHang], [NoiGiaoHang], [PTThanhToan], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDD0029   ', N'KH0013    ', N'NV0010    ', CAST(N'2020-09-20' AS Date), CAST(N'2020-09-26' AS Date), N'Y??n H??a - H?? N???i', N'Ti???n M???t', 20000, 10000, 10000)
GO
INSERT [dbo].[tblDatHang] ([MaHDD], [MaKH], [MaNV], [NgayDatHang], [NgayGiaoHang], [NoiGiaoHang], [PTThanhToan], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDD0030   ', N'KH0014    ', N'NV0003    ', CAST(N'2020-09-27' AS Date), CAST(N'2020-10-05' AS Date), N'Qu???n 7 - TP.HCM', N'Ti???n M???t', 4300000, 2150000, 2150000)
GO
INSERT [dbo].[tblDatHang] ([MaHDD], [MaKH], [MaNV], [NgayDatHang], [NgayGiaoHang], [NoiGiaoHang], [PTThanhToan], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDD0031   ', N'KH0011    ', N'NV0007    ', CAST(N'2020-10-02' AS Date), CAST(N'2020-10-10' AS Date), N'Nguy???n Khanh - C???u Gi???y - H?? N???i', N'Ti???n M???t', 600000, 300000, 300000)
GO
INSERT [dbo].[tblDatHang] ([MaHDD], [MaKH], [MaNV], [NgayDatHang], [NgayGiaoHang], [NoiGiaoHang], [PTThanhToan], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDD0032   ', N'KH0015    ', N'NV0008    ', CAST(N'2020-10-05' AS Date), CAST(N'2020-10-12' AS Date), N'Nguy???n Tr??i - Qu???n 1 - TP.HCM', N'Ti???n M???t', 1000000, 500000, 500000)
GO
INSERT [dbo].[tblDatHang] ([MaHDD], [MaKH], [MaNV], [NgayDatHang], [NgayGiaoHang], [NoiGiaoHang], [PTThanhToan], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDD0033   ', N'KH0015    ', N'NV0009    ', CAST(N'2020-10-12' AS Date), CAST(N'2020-10-21' AS Date), N'Nguy???n Tr??i - Qu???n 1 - TP.HCM', N'Ti???n M???t', 900000, 450000, 450000)
GO
INSERT [dbo].[tblDatHang] ([MaHDD], [MaKH], [MaNV], [NgayDatHang], [NgayGiaoHang], [NoiGiaoHang], [PTThanhToan], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDD0034   ', N'KH0015    ', N'NV0004    ', CAST(N'2020-10-23' AS Date), CAST(N'2020-10-30' AS Date), N'Nguy???n Tr??i - Qu???n 1 - TP.HCM', N'Ti???n M???t', 400000, 200000, 200000)
GO
INSERT [dbo].[tblDatHang] ([MaHDD], [MaKH], [MaNV], [NgayDatHang], [NgayGiaoHang], [NoiGiaoHang], [PTThanhToan], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDD0035   ', N'KH0020    ', N'NV0003    ', CAST(N'2020-10-27' AS Date), CAST(N'2020-11-03' AS Date), N'Qu???c Oai - H?? N???i', N'Ti???n M???t', 2100000, 1050000, 1050000)
GO
INSERT [dbo].[tblDatHang] ([MaHDD], [MaKH], [MaNV], [NgayDatHang], [NgayGiaoHang], [NoiGiaoHang], [PTThanhToan], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDD0036   ', N'KH0012    ', N'NV0005    ', CAST(N'2020-11-02' AS Date), CAST(N'2020-11-10' AS Date), N'Tr?????ng Trinh - H?? N???i', N'Ti???n M???t', 200000, 100000, 100000)
GO
INSERT [dbo].[tblDatHang] ([MaHDD], [MaKH], [MaNV], [NgayDatHang], [NgayGiaoHang], [NoiGiaoHang], [PTThanhToan], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDD0037   ', N'KH0017    ', N'NV0007    ', CAST(N'2020-11-05' AS Date), CAST(N'2020-11-11' AS Date), N'323 ???????ng B?????i - Ba ????nh - H?? N???i ', N'Ti???n M???t', 650000, 325000, 325000)
GO
INSERT [dbo].[tblDatHang] ([MaHDD], [MaKH], [MaNV], [NgayDatHang], [NgayGiaoHang], [NoiGiaoHang], [PTThanhToan], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDD0038   ', N'KH0009    ', N'NV0008    ', CAST(N'2020-11-06' AS Date), CAST(N'2020-11-13' AS Date), N'Ba ????nh - H?? N???i', N'Ti???n M???t', 1000000, 500000, 500000)
GO
INSERT [dbo].[tblDatHang] ([MaHDD], [MaKH], [MaNV], [NgayDatHang], [NgayGiaoHang], [NoiGiaoHang], [PTThanhToan], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDD0039   ', N'KH0019    ', N'NV0009    ', CAST(N'2020-11-12' AS Date), CAST(N'2020-11-20' AS Date), N'B??nh L???c - H?? nam', N'Ti???n M???t', 600000, 300000, 300000)
GO
INSERT [dbo].[tblDatHang] ([MaHDD], [MaKH], [MaNV], [NgayDatHang], [NgayGiaoHang], [NoiGiaoHang], [PTThanhToan], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDD0040   ', N'KH0021    ', N'NV0010    ', CAST(N'2020-11-12' AS Date), CAST(N'2020-11-21' AS Date), N'????ng Anh - H?? N???i', N'Ti???n M???t', 1200000, 600000, 600000)
GO
INSERT [dbo].[tblDatHang] ([MaHDD], [MaKH], [MaNV], [NgayDatHang], [NgayGiaoHang], [NoiGiaoHang], [PTThanhToan], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDD0041   ', N'KH0001    ', N'NV0001    ', CAST(N'2021-11-27' AS Date), CAST(N'2021-11-27' AS Date), N'50/34 L?? ?????i H??nh - Qu???n 10 TP.HCM', NULL, 6200000, 6200000, 0)
GO
INSERT [dbo].[tblDatHang] ([MaHDD], [MaKH], [MaNV], [NgayDatHang], [NgayGiaoHang], [NoiGiaoHang], [PTThanhToan], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDD0042   ', N'KH0023    ', N'NV0001    ', CAST(N'2021-11-27' AS Date), CAST(N'2021-11-27' AS Date), N'????ng Anh - H?? N???i', NULL, 1000000, 1000000, 0)
GO
INSERT [dbo].[tblHang] ([MaHang], [TenHang], [GiaNhap], [GiaBan], [Anh], [MaNhomHang], [DonViTinh], [MoTa], [SoLuongTon]) VALUES (N'MH0001    ', N'??o Ph???n Quang L?????i Vi???t Nam', 10000, 100000, N'AoPhanQuang.jpg', N'NH0001    ', N'C??i', N'Ph???n quang ph??t s??ng chiu chiu', 240)
GO
INSERT [dbo].[tblHang] ([MaHang], [TenHang], [GiaNhap], [GiaBan], [Anh], [MaNhomHang], [DonViTinh], [MoTa], [SoLuongTon]) VALUES (N'MH0002    ', N'Kh???u Trang 3D Tr???ng', 1000, 10000, N'KhauTrang.png', N'NH0001    ', N'C??i', N'B???t mi???ng', 130)
GO
INSERT [dbo].[tblHang] ([MaHang], [TenHang], [GiaNhap], [GiaBan], [Anh], [MaNhomHang], [DonViTinh], [MoTa], [SoLuongTon]) VALUES (N'MH0003    ', N'B???t Tai Ch???ng ???n', 10000, 20000, N'BitTaiChongOn.png', N'NH0001    ', N'C??i', N'B???t Tai', 110)
GO
INSERT [dbo].[tblHang] ([MaHang], [TenHang], [GiaNhap], [GiaBan], [Anh], [MaNhomHang], [DonViTinh], [MoTa], [SoLuongTon]) VALUES (N'MH0004    ', N'M??c Treo Qu???n ??o', 10000, 20000, N'MocTreoQuanAo.png', N'NH0002    ', N'C??i', N'Treo qu???n ??o', 110)
GO
INSERT [dbo].[tblHang] ([MaHang], [TenHang], [GiaNhap], [GiaBan], [Anh], [MaNhomHang], [DonViTinh], [MoTa], [SoLuongTon]) VALUES (N'MH0005    ', N'V??i X???t V??? Sinh', 10000, 20000, N'VoiXitVeSinh.png', N'NH0002    ', N'C??i', N'X???t....', 110)
GO
INSERT [dbo].[tblHang] ([MaHang], [TenHang], [GiaNhap], [GiaBan], [Anh], [MaNhomHang], [DonViTinh], [MoTa], [SoLuongTon]) VALUES (N'MH0006    ', N'Xi Nh??ng', 10000, 20000, N'XiNhong.png', N'NH0002    ', N'C??i', N'?????t b???n r???a m???t l??n tr??n', 110)
GO
INSERT [dbo].[tblHang] ([MaHang], [TenHang], [GiaNhap], [GiaBan], [Anh], [MaNhomHang], [DonViTinh], [MoTa], [SoLuongTon]) VALUES (N'MH0007    ', N'C??p D??? ???ng L???c Kh??ng V??? B???c', 10000, 20000, N'CapDuUngLucKhongVoBoc.png', N'NH0003    ', N'Y???n', NULL, 110)
GO
INSERT [dbo].[tblHang] ([MaHang], [TenHang], [GiaNhap], [GiaBan], [Anh], [MaNhomHang], [DonViTinh], [MoTa], [SoLuongTon]) VALUES (N'MH0008    ', N'???ng Gen', 10000, 20000, N'OngGen.png', N'NH0003    ', N'Kg', N'Tr??ng nh?? l?? xo', 120)
GO
INSERT [dbo].[tblHang] ([MaHang], [TenHang], [GiaNhap], [GiaBan], [Anh], [MaNhomHang], [DonViTinh], [MoTa], [SoLuongTon]) VALUES (N'MH0009    ', N'G???ch B?? T??ng', 1000, 2000, N'GachBeTong.png', N'NH0004    ', N'Vi??n', NULL, 120)
GO
INSERT [dbo].[tblHang] ([MaHang], [TenHang], [GiaNhap], [GiaBan], [Anh], [MaNhomHang], [DonViTinh], [MoTa], [SoLuongTon]) VALUES (N'MH0010    ', N'G???ch ?????t S??t Nung', 1000, 2000, N'GachDatSetNung.png', N'NH0004    ', N'Vi??n', NULL, 120)
GO
INSERT [dbo].[tblHang] ([MaHang], [TenHang], [GiaNhap], [GiaBan], [Anh], [MaNhomHang], [DonViTinh], [MoTa], [SoLuongTon]) VALUES (N'MH0011    ', N'Xi M??ng Po??c L??ng', 20000, 30000, N'XiMangPoocLang', N'NH0004    ', N'Bao', NULL, 120)
GO
INSERT [dbo].[tblHang] ([MaHang], [TenHang], [GiaNhap], [GiaBan], [Anh], [MaNhomHang], [DonViTinh], [MoTa], [SoLuongTon]) VALUES (N'MH0012    ', N'C??t T??? Nhi??n D??ng Cho B?? T??ng V?? V???a', 100000, 200000, N'CatTuNhienDungChoBeTongVaVua.png', N'NH0004    ', N'T???n', NULL, 120)
GO
INSERT [dbo].[tblHang] ([MaHang], [TenHang], [GiaNhap], [GiaBan], [Anh], [MaNhomHang], [DonViTinh], [MoTa], [SoLuongTon]) VALUES (N'MH0013    ', N'G???ch G???m ???p L??t', 2000, 4000, N'GachGomOpLat', N'NH0004    ', N'Vi??n', NULL, 120)
GO
INSERT [dbo].[tblHang] ([MaHang], [TenHang], [GiaNhap], [GiaBan], [Anh], [MaNhomHang], [DonViTinh], [MoTa], [SoLuongTon]) VALUES (N'MH0014    ', N'???? ???p L??t Nh??n T???o Tr??n C?? S??? Ch???t K???t D??nh H???u C??', 10000, 20000, N'DaOpLatNhanTaoTrenCoSoChatKetDinhHuuCo.png', N'NH0004    ', N'T???m', NULL, 120)
GO
INSERT [dbo].[tblHang] ([MaHang], [TenHang], [GiaNhap], [GiaBan], [Anh], [MaNhomHang], [DonViTinh], [MoTa], [SoLuongTon]) VALUES (N'MH0015    ', N'T???m t?????ng r???ng b?? t??ng ???????c ????c s???n theo c??ng ngh??? ????n ??p', 100000, 200000, N'TamTuongRongBeTongDuocDucSanTheoCongNgheDunEp.png', N'NH0004    ', N'T???m', NULL, 120)
GO
INSERT [dbo].[tblHang] ([MaHang], [TenHang], [GiaNhap], [GiaBan], [Anh], [MaNhomHang], [DonViTinh], [MoTa], [SoLuongTon]) VALUES (N'MH0016    ', N'K??nh ph???ng t??i nhi???t', 10000, 100000, N'KinhPhangToiNhiet.png', N'NH0004    ', N'T???m', NULL, 120)
GO
INSERT [dbo].[tblHang] ([MaHang], [TenHang], [GiaNhap], [GiaBan], [Anh], [MaNhomHang], [DonViTinh], [MoTa], [SoLuongTon]) VALUES (N'MH0017    ', N'K??nh h???p g???n k??n c??ch nhi???t', 10000, 30000, N'GachDatSetNung.png', N'NH0004    ', N'T???m', NULL, 120)
GO
INSERT [dbo].[tblHang] ([MaHang], [TenHang], [GiaNhap], [GiaBan], [Anh], [MaNhomHang], [DonViTinh], [MoTa], [SoLuongTon]) VALUES (N'MH0018    ', N'???ng Cao Su', 1000, 10000, N'UngCaoSu.png', N'NH0001    ', N'????i', NULL, 120)
GO
INSERT [dbo].[tblHang] ([MaHang], [TenHang], [GiaNhap], [GiaBan], [Anh], [MaNhomHang], [DonViTinh], [MoTa], [SoLuongTon]) VALUES (N'MH0019    ', N'M?? Ch???ng Ch???n Th????ng S??? N??o', 20000, 30000, N'MuChongChanThuongSoNao', N'NH0001    ', N'C??i', NULL, 120)
GO
INSERT [dbo].[tblHang] ([MaHang], [TenHang], [GiaNhap], [GiaBan], [Anh], [MaNhomHang], [DonViTinh], [MoTa], [SoLuongTon]) VALUES (N'MH0020    ', N'G??ng tay c??ch ??i???n', 10000, 30000, N'GangTayCachDien.png', N'NH0001    ', N'????i', NULL, 120)
GO
INSERT [dbo].[tblHang] ([MaHang], [TenHang], [GiaNhap], [GiaBan], [Anh], [MaNhomHang], [DonViTinh], [MoTa], [SoLuongTon]) VALUES (N'MH0021    ', N'V??i Ch???u R???a N??ng L???nh Inox', 20000, 40000, N'VoiChauRuaNongLanhInox.png', N'NH0002    ', N'C??i', NULL, 120)
GO
INSERT [dbo].[tblHang] ([MaHang], [TenHang], [GiaNhap], [GiaBan], [Anh], [MaNhomHang], [DonViTinh], [MoTa], [SoLuongTon]) VALUES (N'MH0022    ', N'K??? ????? X?? Ph??ng', 10000, 30000, N'KeDeXaPhong.png', N'NH0002    ', N'C??i', NULL, 120)
GO
INSERT [dbo].[tblHang] ([MaHang], [TenHang], [GiaNhap], [GiaBan], [Anh], [MaNhomHang], [DonViTinh], [MoTa], [SoLuongTon]) VALUES (N'MH0023    ', N'K?? K??nh', 10000, 50000, N'KeKinh.png', N'NH0002    ', N'C??i', NULL, 120)
GO
INSERT [dbo].[tblHang] ([MaHang], [TenHang], [GiaNhap], [GiaBan], [Anh], [MaNhomHang], [DonViTinh], [MoTa], [SoLuongTon]) VALUES (N'MH0024    ', N'Tho??t S??n', 10000, 20000, N'ThoatSan.png', N'NH0002    ', N'C??i', NULL, 120)
GO
INSERT [dbo].[tblHang] ([MaHang], [TenHang], [GiaNhap], [GiaBan], [Anh], [MaNhomHang], [DonViTinh], [MoTa], [SoLuongTon]) VALUES (N'MH0025    ', N'Van G??c', 10000, 25000, N'VanGoc.png', N'NH0002    ', N'C??i', NULL, 120)
GO
INSERT [dbo].[tblHang] ([MaHang], [TenHang], [GiaNhap], [GiaBan], [Anh], [MaNhomHang], [DonViTinh], [MoTa], [SoLuongTon]) VALUES (N'MH0026    ', N'tets', 10, 1, NULL, N'NH0001    ', N'C??i', N't??t', 110)
GO
INSERT [dbo].[tblHang] ([MaHang], [TenHang], [GiaNhap], [GiaBan], [Anh], [MaNhomHang], [DonViTinh], [MoTa], [SoLuongTon]) VALUES (N'MH0027    ', N'tets', NULL, NULL, NULL, N'NH0001    ', N'C??i', N't??t', NULL)
GO
INSERT [dbo].[tblHDLe] ([MaHDL], [MaNV], [TenKH], [SDT], [NgayDat], [NoiGiaoHang], [TongTien]) VALUES (N'HDL0001', N'NV0001', N'L?? Ki???n Tr??c', N'0963 398 772', CAST(N'2021-11-27' AS Date), N'Ng??i nh?? nh??? b??n H??? Sen', 300000)
GO
INSERT [dbo].[tblHDLe] ([MaHDL], [MaNV], [TenKH], [SDT], [NgayDat], [NoiGiaoHang], [TongTien]) VALUES (N'HDL0002', N'NV0001', N'BBQ', N'0963 398 772', CAST(N'2021-11-27' AS Date), N'Ng??i nh?? nh??? b??n H??? Sen bbq', 1000000)
GO
INSERT [dbo].[tblKhachHang] ([MaKH], [TenKH], [SDT], [DiaChi], [MaNhomKH], [TienNo]) VALUES (N'KH0001    ', N'Nguy???n Thanh Ch??', N'0977000111     ', N'50/34 L?? ?????i H??nh - Qu???n 10 TP.HCM', NULL, 100050000)
GO
INSERT [dbo].[tblKhachHang] ([MaKH], [TenKH], [SDT], [DiaChi], [MaNhomKH], [TienNo]) VALUES (N'KH0002    ', N'Nguy???n Ng???c H??n', N'0966000111     ', N'23/5 Nguy???n Tr??i - Qu???n 5 - Tp.HCM', NULL, 4580000)
GO
INSERT [dbo].[tblKhachHang] ([MaKH], [TenKH], [SDT], [DiaChi], [MaNhomKH], [TienNo]) VALUES (N'KH0003    ', N'L?? Th??? Nhung', N'0955000111     ', N'32 C???u Gi???y - H?? N???i', N'NKH0001   ', 2910000)
GO
INSERT [dbo].[tblKhachHang] ([MaKH], [TenKH], [SDT], [DiaChi], [MaNhomKH], [TienNo]) VALUES (N'KH0004    ', N'Tr???nh Th??? Kim Chi', N'0977111222     ', N'?? Y??n - Nam ?????nh', N'NKH0002   ', 2780000)
GO
INSERT [dbo].[tblKhachHang] ([MaKH], [TenKH], [SDT], [DiaChi], [MaNhomKH], [TienNo]) VALUES (N'KH0005    ', N'Mai Th??? G???m', N'0166666000     ', N'32 ???????ng B?????i - Ba ????nh - H?? N???i', NULL, 2100000)
GO
INSERT [dbo].[tblKhachHang] ([MaKH], [TenKH], [SDT], [DiaChi], [MaNhomKH], [TienNo]) VALUES (N'KH0006    ', N'Nguy???n Th??? V??n', N'0122333444     ', N'Y??n ?????ng - ?? Y??n - Nam ?????nh', NULL, 50000)
GO
INSERT [dbo].[tblKhachHang] ([MaKH], [TenKH], [SDT], [DiaChi], [MaNhomKH], [TienNo]) VALUES (N'KH0007    ', N'Nguy???n V?? Kh???i', N'0223334555     ', N'C???u Gi???y - H?? N???i', NULL, 750000)
GO
INSERT [dbo].[tblKhachHang] ([MaKH], [TenKH], [SDT], [DiaChi], [MaNhomKH], [TienNo]) VALUES (N'KH0008    ', N'Nguy???n Ng???c ?????c', N'0997212666     ', N'????ng Anh - H?? N???i', NULL, 1100000)
GO
INSERT [dbo].[tblKhachHang] ([MaKH], [TenKH], [SDT], [DiaChi], [MaNhomKH], [TienNo]) VALUES (N'KH0009    ', N'Phan Ti???n Anh', N'0162333452     ', N'Ba ????nh - H?? N???i', NULL, 1925000)
GO
INSERT [dbo].[tblKhachHang] ([MaKH], [TenKH], [SDT], [DiaChi], [MaNhomKH], [TienNo]) VALUES (N'KH0010    ', N'Nguy???n Trung T??i', N'0723222456     ', N'Ba V?? - H?? N???i', NULL, 1300000)
GO
INSERT [dbo].[tblKhachHang] ([MaKH], [TenKH], [SDT], [DiaChi], [MaNhomKH], [TienNo]) VALUES (N'KH0011    ', N'Nguy???n Kim ?????t', N'0324010232     ', N'Nguy???n Khanh - C???u Gi???y - H?? N???i', NULL, 1030000)
GO
INSERT [dbo].[tblKhachHang] ([MaKH], [TenKH], [SDT], [DiaChi], [MaNhomKH], [TienNo]) VALUES (N'KH0012    ', N'Tr???n Quang H???i', N'0123455677     ', N'Tr?????ng Trinh - H?? N???i', NULL, 2270000)
GO
INSERT [dbo].[tblKhachHang] ([MaKH], [TenKH], [SDT], [DiaChi], [MaNhomKH], [TienNo]) VALUES (N'KH0013    ', N'Nguy???n ?????c Hi???u', N'0224555887     ', N'Y??n H??a - H?? N???i', NULL, 460000)
GO
INSERT [dbo].[tblKhachHang] ([MaKH], [TenKH], [SDT], [DiaChi], [MaNhomKH], [TienNo]) VALUES (N'KH0014    ', N'????? Lam Chi', N'0223010558     ', N'Qu???n 7 - TP.HCM', NULL, 2700000)
GO
INSERT [dbo].[tblKhachHang] ([MaKH], [TenKH], [SDT], [DiaChi], [MaNhomKH], [TienNo]) VALUES (N'KH0015    ', N'La Tu???n Kh???i', N'0166477845     ', N'Nguy???n Tr??i - Qu???n 1 - TP.HCM', NULL, 3650000)
GO
INSERT [dbo].[tblKhachHang] ([MaKH], [TenKH], [SDT], [DiaChi], [MaNhomKH], [TienNo]) VALUES (N'KH0016    ', N'Tr???n Thanh Khi???t', N'022334551      ', N'Thanh Xu??n - H?? N???i', NULL, 350000)
GO
INSERT [dbo].[tblKhachHang] ([MaKH], [TenKH], [SDT], [DiaChi], [MaNhomKH], [TienNo]) VALUES (N'KH0017    ', N'Mai V??n H??ng', N'012398675      ', N'323 ???????ng B?????i - Ba ????nh - H?? N???i ', NULL, 825000)
GO
INSERT [dbo].[tblKhachHang] ([MaKH], [TenKH], [SDT], [DiaChi], [MaNhomKH], [TienNo]) VALUES (N'KH0018    ', N'L?? Th??? H???ng', N'0223010994     ', N'Xu??n Th???y - C???u Gi???y - H?? N???i', NULL, 200000)
GO
INSERT [dbo].[tblKhachHang] ([MaKH], [TenKH], [SDT], [DiaChi], [MaNhomKH], [TienNo]) VALUES (N'KH0019    ', N'Nguy???n Th??? Th???y', N'0223404555     ', N'B??nh L???c - H?? nam', NULL, 600000)
GO
INSERT [dbo].[tblKhachHang] ([MaKH], [TenKH], [SDT], [DiaChi], [MaNhomKH], [TienNo]) VALUES (N'KH0020    ', N'Nguy???n V??n Huy', N'01533554662    ', N'Qu???c Oai - H?? N???i', NULL, 1550000)
GO
INSERT [dbo].[tblKhachHang] ([MaKH], [TenKH], [SDT], [DiaChi], [MaNhomKH], [TienNo]) VALUES (N'KH0021    ', N'T???ng V??n Tr??n', N'023946782      ', N'????ng Anh - H?? N???i', NULL, 700000)
GO
INSERT [dbo].[tblKhachHang] ([MaKH], [TenKH], [SDT], [DiaChi], [MaNhomKH], [TienNo]) VALUES (N'KH0022    ', N'T???ng V??n Tr??n', N'023946782      ', N'????ng Anh - H?? N???i', N'NKH0001   ', NULL)
GO
INSERT [dbo].[tblKhachHang] ([MaKH], [TenKH], [SDT], [DiaChi], [MaNhomKH], [TienNo]) VALUES (N'KH0023    ', N'test', N'023946782      ', N'????ng Anh - H?? N???i', N'NKH0001   ', 0)
GO
INSERT [dbo].[tblNhaCungCap] ([MaNCC], [TenNCC], [DiaChi], [SDT], [TienNo]) VALUES (N'NCC0001   ', N'C??NG TY C??? PH???N NGUY???N H?? AN', N'?????i 8, th??n Ti??n L???, X?? Ti??n Ph????ng, Huy???n Ch????ng M???, TP H?? N???i (TPHN)', N'0242111222     ', 111850000)
GO
INSERT [dbo].[tblNhaCungCap] ([MaNCC], [TenNCC], [DiaChi], [SDT], [TienNo]) VALUES (N'NCC0002   ', N'THI???T B??? V??? SINH TOSHIO - C??NG TY C??? PH???N TOSHIO VI???T NH???T', N'Khu C??ng Nghi???p S??i ?????ng, Gia L??m, TP H?? N???i (TPHN)', N'0242333444     ', 137750000)
GO
INSERT [dbo].[tblNhaCungCap] ([MaNCC], [TenNCC], [DiaChi], [SDT], [TienNo]) VALUES (N'NCC0003   ', N'THI???T B??? AN TO??N GIAO TH??NG GIA PH??T - C??NG TY C??? PH???N GIA PH??T H?? N???I', N'S??? 32, Ng?? 16, ???????ng Phan V??n Tr?????ng, Ph?????ng D???ch V???ng H???u, Qu???n C???u Gi???y, Th??nh Ph??? H?? N???i (TPHN)', N'0242555111     ', 46400000)
GO
INSERT [dbo].[tblNhaCungCap] ([MaNCC], [TenNCC], [DiaChi], [SDT], [TienNo]) VALUES (N'NCC0004   ', N'C??NG TY ?????I NGH??A', N'P318, Chung c?? B3, ng?? 118 nguy???n Kh??nh To??n, Quan Hoa, C???u Gi???y.', N'02466577778    ', 26300000)
GO
INSERT [dbo].[tblNhaCungCap] ([MaNCC], [TenNCC], [DiaChi], [SDT], [TienNo]) VALUES (N'NCC0005   ', N'C??NG TY C??? PH???N HOA SEN', N'Ng??i nh?? nh??? b??n h??? sen', N'123            ', 1000000)
GO
INSERT [dbo].[tblNhaCungCap] ([MaNCC], [TenNCC], [DiaChi], [SDT], [TienNo]) VALUES (N'NCC0006   ', N'test', N'Ng??i nh?? nh??? b??n h??? sen', N'123            ', 6900000)
GO
INSERT [dbo].[tblNhanVien] ([MaNV], [TenNV], [GioiTinh], [NgaySinh], [SDT], [Anh], [HSL], [LuongCB], [Thuong], [MaCV]) VALUES (N'NV0001    ', N'L?? Ki???n Tr??c', N'Nam       ', CAST(N'2001-01-01' AS Date), N'0363000111     ', N'NVLeKienTruc.png', 1, 5000000, NULL, N'CV0002    ')
GO
INSERT [dbo].[tblNhanVien] ([MaNV], [TenNV], [GioiTinh], [NgaySinh], [SDT], [Anh], [HSL], [LuongCB], [Thuong], [MaCV]) VALUES (N'NV0002    ', N'Nguy???n Th??? Kh??nh', N'N???        ', CAST(N'2001-01-28' AS Date), N'0977222333     ', N'NVNguyenThiKhanh.jpeg', 1.5, 5000000, NULL, N'CV0002    ')
GO
INSERT [dbo].[tblNhanVien] ([MaNV], [TenNV], [GioiTinh], [NgaySinh], [SDT], [Anh], [HSL], [LuongCB], [Thuong], [MaCV]) VALUES (N'NV0003    ', N'Khu???t Th??? Hoa', N'N???        ', CAST(N'2001-12-20' AS Date), N'0163333111     ', N'NVKhuatThiHoa.png', 1, 4000000, NULL, N'CV0001    ')
GO
INSERT [dbo].[tblNhanVien] ([MaNV], [TenNV], [GioiTinh], [NgaySinh], [SDT], [Anh], [HSL], [LuongCB], [Thuong], [MaCV]) VALUES (N'NV0004    ', N'Ph???m Th??? Thu Trang', N'N???        ', CAST(N'2001-10-16' AS Date), N'0353444222     ', N'NVPhamThiThuTrang.png', 1, 4000000, NULL, N'CV0001    ')
GO
INSERT [dbo].[tblNhanVien] ([MaNV], [TenNV], [GioiTinh], [NgaySinh], [SDT], [Anh], [HSL], [LuongCB], [Thuong], [MaCV]) VALUES (N'NV0005    ', N'L?? Danh Tu???n', N'Nam       ', CAST(N'2001-03-24' AS Date), N'0999111222     ', N'NVLeDanhTuan.png', 1, 3000000, NULL, N'CV0001    ')
GO
INSERT [dbo].[tblNhanVien] ([MaNV], [TenNV], [GioiTinh], [NgaySinh], [SDT], [Anh], [HSL], [LuongCB], [Thuong], [MaCV]) VALUES (N'NV0006    ', N'Nguy???n Trung T??i', N'Nam       ', CAST(N'2001-04-02' AS Date), N'0166773508     ', N'NVNguyenTrungTai.png', 1, 3000000, NULL, N'CV0001    ')
GO
INSERT [dbo].[tblNhanVien] ([MaNV], [TenNV], [GioiTinh], [NgaySinh], [SDT], [Anh], [HSL], [LuongCB], [Thuong], [MaCV]) VALUES (N'NV0007    ', N'Nguy???n Kim ?????t', N'Nam       ', CAST(N'2001-02-05' AS Date), N'0123555444     ', N'NVNguyenKimDat', 1, 3000000, NULL, N'CV0001    ')
GO
INSERT [dbo].[tblNhanVien] ([MaNV], [TenNV], [GioiTinh], [NgaySinh], [SDT], [Anh], [HSL], [LuongCB], [Thuong], [MaCV]) VALUES (N'NV0008    ', N'Ng?? Ng???c ?????c', N'Nam       ', CAST(N'2001-03-23' AS Date), N'0163337749     ', N'NVNgoNgocDuc.png', 1, 1000000, NULL, N'CV0001    ')
GO
INSERT [dbo].[tblNhanVien] ([MaNV], [TenNV], [GioiTinh], [NgaySinh], [SDT], [Anh], [HSL], [LuongCB], [Thuong], [MaCV]) VALUES (N'NV0009    ', N'????? Vi???t Ho??ng', N'Nam       ', CAST(N'2001-04-03' AS Date), N'0262332456     ', N'NVDoVietHoang.png', 1, 3000000, NULL, N'CV0001    ')
GO
INSERT [dbo].[tblNhanVien] ([MaNV], [TenNV], [GioiTinh], [NgaySinh], [SDT], [Anh], [HSL], [LuongCB], [Thuong], [MaCV]) VALUES (N'NV0010    ', N'Ph???m Quang D??ng', N'N???        ', CAST(N'2001-05-20' AS Date), N'0100100001     ', N'NVPhamQuangDung.png', 1, 3000000, NULL, N'CV0001    ')
GO
INSERT [dbo].[tblNhapKho] ([MaHDN], [NgayNhap], [MaNCC], [MaNV], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDN0004   ', CAST(N'2021-02-20' AS Date), N'NCC0001   ', N'NV0005    ', 6600000, 3300000, 3300000)
GO
INSERT [dbo].[tblNhapKho] ([MaHDN], [NgayNhap], [MaNCC], [MaNV], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDN0005   ', CAST(N'2021-05-28' AS Date), N'NCC0002   ', N'NV0003    ', 115000000, 57500000, 57500000)
GO
INSERT [dbo].[tblNhapKho] ([MaHDN], [NgayNhap], [MaNCC], [MaNV], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDN0006   ', CAST(N'2021-05-22' AS Date), N'NCC0004   ', N'NV0002    ', 5200000, 2600000, 2600000)
GO
INSERT [dbo].[tblNhapKho] ([MaHDN], [NgayNhap], [MaNCC], [MaNV], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDN0007   ', CAST(N'2021-05-19' AS Date), N'NCC0001   ', N'NV0002    ', 31000000, 15500000, 15500000)
GO
INSERT [dbo].[tblNhapKho] ([MaHDN], [NgayNhap], [MaNCC], [MaNV], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDN0008   ', CAST(N'2021-05-25' AS Date), N'NCC0002   ', N'NV0002    ', 6000000, 3000000, 3000000)
GO
INSERT [dbo].[tblNhapKho] ([MaHDN], [NgayNhap], [MaNCC], [MaNV], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDN0009   ', CAST(N'2021-05-30' AS Date), N'NCC0004   ', N'NV0003    ', 30000000, 15000000, 15000000)
GO
INSERT [dbo].[tblNhapKho] ([MaHDN], [NgayNhap], [MaNCC], [MaNV], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDN0010   ', CAST(N'2021-05-01' AS Date), N'NCC0001   ', N'NV0003    ', 14000000, 7000000, 7000000)
GO
INSERT [dbo].[tblNhapKho] ([MaHDN], [NgayNhap], [MaNCC], [MaNV], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDN0011   ', CAST(N'2021-02-04' AS Date), N'NCC0001   ', N'NV0003    ', 20000000, 10000000, 10000000)
GO
INSERT [dbo].[tblNhapKho] ([MaHDN], [NgayNhap], [MaNCC], [MaNV], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDN0012   ', CAST(N'2021-02-22' AS Date), N'NCC0001   ', N'NV0003    ', 6000000, 3000000, 3000000)
GO
INSERT [dbo].[tblNhapKho] ([MaHDN], [NgayNhap], [MaNCC], [MaNV], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDN0013   ', CAST(N'2021-02-12' AS Date), N'NCC0002   ', N'NV0004    ', 10000000, 5000000, 5000000)
GO
INSERT [dbo].[tblNhapKho] ([MaHDN], [NgayNhap], [MaNCC], [MaNV], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDN0014   ', CAST(N'2021-02-15' AS Date), N'NCC0002   ', N'NV0004    ', 67000000, 33500000, 33500000)
GO
INSERT [dbo].[tblNhapKho] ([MaHDN], [NgayNhap], [MaNCC], [MaNV], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDN0015   ', CAST(N'2021-02-20' AS Date), N'NCC0001   ', N'NV0005    ', 49500000, 24750000, 24750000)
GO
INSERT [dbo].[tblNhapKho] ([MaHDN], [NgayNhap], [MaNCC], [MaNV], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDN0016   ', CAST(N'2021-02-25' AS Date), N'NCC0003   ', N'NV0005    ', 25000000, 12500000, 12500000)
GO
INSERT [dbo].[tblNhapKho] ([MaHDN], [NgayNhap], [MaNCC], [MaNV], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDN0017   ', CAST(N'2021-01-21' AS Date), N'NCC0002   ', N'NV0005    ', 6000000, 3000000, 3000000)
GO
INSERT [dbo].[tblNhapKho] ([MaHDN], [NgayNhap], [MaNCC], [MaNV], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDN0018   ', CAST(N'2021-01-22' AS Date), N'NCC0001   ', N'NV0004    ', 10000000, 5000000, 5000000)
GO
INSERT [dbo].[tblNhapKho] ([MaHDN], [NgayNhap], [MaNCC], [MaNV], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDN0019   ', CAST(N'2021-01-13' AS Date), N'NCC0001   ', N'NV0004    ', 3400000, 1700000, 1700000)
GO
INSERT [dbo].[tblNhapKho] ([MaHDN], [NgayNhap], [MaNCC], [MaNV], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDN0020   ', CAST(N'2021-01-25' AS Date), N'NCC0003   ', N'NV0006    ', 2000000, 1000000, 1000000)
GO
INSERT [dbo].[tblNhapKho] ([MaHDN], [NgayNhap], [MaNCC], [MaNV], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDN0021   ', CAST(N'2021-06-25' AS Date), N'NCC0004   ', N'NV0006    ', 400000, 200000, 200000)
GO
INSERT [dbo].[tblNhapKho] ([MaHDN], [NgayNhap], [MaNCC], [MaNV], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDN0022   ', CAST(N'2021-06-12' AS Date), N'NCC0004   ', N'NV0006    ', 6000000, 3000000, 3000000)
GO
INSERT [dbo].[tblNhapKho] ([MaHDN], [NgayNhap], [MaNCC], [MaNV], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDN0023   ', CAST(N'2021-06-30' AS Date), N'NCC0003   ', N'NV0006    ', 2400000, 1200000, 1200000)
GO
INSERT [dbo].[tblNhapKho] ([MaHDN], [NgayNhap], [MaNCC], [MaNV], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDN0024   ', CAST(N'2021-07-15' AS Date), N'NCC0001   ', N'NV0007    ', 1000000, 500000, 500000)
GO
INSERT [dbo].[tblNhapKho] ([MaHDN], [NgayNhap], [MaNCC], [MaNV], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDN0025   ', CAST(N'2021-09-18' AS Date), N'NCC0002   ', N'NV0007    ', 16500000, 8250000, 8250000)
GO
INSERT [dbo].[tblNhapKho] ([MaHDN], [NgayNhap], [MaNCC], [MaNV], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDN0026   ', CAST(N'2021-09-22' AS Date), N'NCC0002   ', N'NV0007    ', 26000000, 13000000, 13000000)
GO
INSERT [dbo].[tblNhapKho] ([MaHDN], [NgayNhap], [MaNCC], [MaNV], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDN0027   ', CAST(N'2021-10-20' AS Date), N'NCC0004   ', N'NV0008    ', 11000000, 5500000, 5500000)
GO
INSERT [dbo].[tblNhapKho] ([MaHDN], [NgayNhap], [MaNCC], [MaNV], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDN0028   ', CAST(N'2021-10-10' AS Date), N'NCC0001   ', N'NV0009    ', 2200000, 1100000, 1100000)
GO
INSERT [dbo].[tblNhapKho] ([MaHDN], [NgayNhap], [MaNCC], [MaNV], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDN0029   ', CAST(N'2021-10-24' AS Date), N'NCC0002   ', N'NV0010    ', 3000000, 1500000, 1500000)
GO
INSERT [dbo].[tblNhapKho] ([MaHDN], [NgayNhap], [MaNCC], [MaNV], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDN0030   ', CAST(N'2020-05-22' AS Date), N'NCC0003   ', N'NV0003    ', 14000000, 7000000, 7000000)
GO
INSERT [dbo].[tblNhapKho] ([MaHDN], [NgayNhap], [MaNCC], [MaNV], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDN0031   ', CAST(N'2020-05-10' AS Date), N'NCC0001   ', N'NV0003    ', 8000000, 4000000, 4000000)
GO
INSERT [dbo].[tblNhapKho] ([MaHDN], [NgayNhap], [MaNCC], [MaNV], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDN0032   ', CAST(N'2020-05-23' AS Date), N'NCC0002   ', N'NV0003    ', 22000000, 11000000, 11000000)
GO
INSERT [dbo].[tblNhapKho] ([MaHDN], [NgayNhap], [MaNCC], [MaNV], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDN0033   ', CAST(N'2020-06-20' AS Date), N'NCC0003   ', N'NV0004    ', 49400000, 24700000, 24700000)
GO
INSERT [dbo].[tblNhapKho] ([MaHDN], [NgayNhap], [MaNCC], [MaNV], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDN0034   ', CAST(N'2020-07-30' AS Date), N'NCC0001   ', N'NV0005    ', 72000000, 36000000, 36000000)
GO
INSERT [dbo].[tblNhapKho] ([MaHDN], [NgayNhap], [MaNCC], [MaNV], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDN0035   ', CAST(N'2020-09-21' AS Date), N'NCC0002   ', N'NV0009    ', 4000000, 2000000, 2000000)
GO
INSERT [dbo].[tblNhapKho] ([MaHDN], [NgayNhap], [MaNCC], [MaNV], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDN0036   ', CAST(N'2021-11-26' AS Date), N'NCC0005   ', N'NV0001    ', 1100000, 1100000, 0)
GO
INSERT [dbo].[tblNhapKho] ([MaHDN], [NgayNhap], [MaNCC], [MaNV], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDN0037   ', CAST(N'2021-11-27' AS Date), N'NCC0006   ', N'NV0001    ', 1100000, 0, 1100000)
GO
INSERT [dbo].[tblNhapKho] ([MaHDN], [NgayNhap], [MaNCC], [MaNV], [TongTien], [TraNgay], [NoLai]) VALUES (N'HDN0038   ', CAST(N'2021-11-27' AS Date), N'NCC0006   ', N'NV0001    ', 100000, 0, 100000)
GO
INSERT [dbo].[tblNhomHang] ([MaNhomHang], [TenNhomHang]) VALUES (N'NH0001    ', N'????? B???o H??? Lao ?????ng')
GO
INSERT [dbo].[tblNhomHang] ([MaNhomHang], [TenNhomHang]) VALUES (N'NH0002    ', N'Thi???t B??? Ph??ng T???m')
GO
INSERT [dbo].[tblNhomHang] ([MaNhomHang], [TenNhomHang]) VALUES (N'NH0003    ', N'V???t T?? C???u ???????ng')
GO
INSERT [dbo].[tblNhomHang] ([MaNhomHang], [TenNhomHang]) VALUES (N'NH0004    ', N'V???t Li???u X??y D???ng')
GO
INSERT [dbo].[tblNhomKH] ([MaNhomKH], [TenNhomKH], [UuDai]) VALUES (N'NKH0001   ', N'Vjp1', 0.1)
GO
INSERT [dbo].[tblNhomKH] ([MaNhomKH], [TenNhomKH], [UuDai]) VALUES (N'NKH0002   ', N'Vjp2', 0.2)
GO
INSERT [dbo].[tblNhomKH] ([MaNhomKH], [TenNhomKH], [UuDai]) VALUES (N'NKH0003   ', N'Vjp3', 0.3)
GO
INSERT [dbo].[tblTaiKhoan2] ([TenDangNhap], [MatKhau], [ChucVu]) VALUES (N'Khu???t Th??? Hoa', N'?????w???vv????????????', N'Nh??n vi??n')
GO
INSERT [dbo].[tblTaiKhoan2] ([TenDangNhap], [MatKhau], [ChucVu]) VALUES (N'L?? Ki???n Tr??c', N'???h?????h??v???w???', N'Qu???n l??')
GO
INSERT [dbo].[tblTaiKhoan2] ([TenDangNhap], [MatKhau], [ChucVu]) VALUES (N'Nguy???n Th??? Kh??nh', N'??ow???h??v????????????????', N'Qu???n l??')
GO
INSERT [dbo].[tblTaiKhoan2] ([TenDangNhap], [MatKhau], [ChucVu]) VALUES (N'Ph???m Th??? Thu Trang', N'#$%^&*', N'Qu???n l??')
GO
INSERT [dbo].[tblThongTinCongTy] ([TenCTy], [TenCHang], [DiaChi], [SDT], [Email], [MaSoThue], [SoTaiKhoan], [TenNganHang]) VALUES (N'', N'', N'', N'', N'', N'', N'', N'')
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0001   ', N'KH0001    ', CAST(N'2021-07-01' AS Date), N'Kh??ch mua', NULL, N'NV0003    ', NULL)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0002   ', N'KH0001    ', CAST(N'2021-10-11' AS Date), N'Kh??ch mua', NULL, N'NV0003    ', NULL)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0003   ', N'KH0002    ', CAST(N'2021-08-31' AS Date), N'Kh??ch mua', NULL, N'NV0004    ', NULL)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0004   ', N'KH0003    ', CAST(N'2021-07-11' AS Date), N'Kh??ch mua', NULL, N'NV0005    ', -400000)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0005   ', N'KH0001    ', CAST(N'2021-07-12' AS Date), NULL, NULL, N'NV0006    ', 500000)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0006   ', N'KH0002    ', CAST(N'2021-07-15' AS Date), NULL, NULL, N'NV0006    ', 1000000)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0007   ', N'KH0020    ', CAST(N'2021-07-12' AS Date), NULL, NULL, N'NV0006    ', 400000)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0008   ', N'KH0021    ', CAST(N'2021-07-11' AS Date), NULL, NULL, N'NV0005    ', 300000)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0009   ', N'KH0021    ', CAST(N'2021-07-19' AS Date), NULL, NULL, N'NV0006    ', 4000)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0010   ', N'KH0004    ', CAST(N'2021-07-13' AS Date), NULL, NULL, N'NV0004    ', 12000)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0011   ', N'KH0005    ', CAST(N'2021-07-20' AS Date), NULL, NULL, N'NV0003    ', 1000000)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0012   ', N'KH0006    ', CAST(N'2021-07-24' AS Date), NULL, NULL, N'NV0007    ', 40000)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0013   ', N'KH0007    ', CAST(N'2021-07-26' AS Date), NULL, NULL, N'NV0008    ', 20000)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0014   ', N'KH0003    ', CAST(N'2021-08-02' AS Date), NULL, NULL, N'NV0007    ', 16000)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0015   ', N'KH0002    ', CAST(N'2021-08-25' AS Date), NULL, NULL, N'NV0008    ', 300000)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0016   ', N'KH0009    ', CAST(N'2021-08-26' AS Date), NULL, NULL, N'NV0009    ', 800000)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0017   ', N'KH0008    ', CAST(N'2021-08-01' AS Date), NULL, NULL, N'NV0007    ', 150000)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0018   ', N'KH0004    ', CAST(N'2021-08-11' AS Date), NULL, NULL, N'NV0009    ', 200000)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0019   ', N'KH0010    ', CAST(N'2021-08-13' AS Date), NULL, NULL, N'NV0008    ', 30000)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0020   ', N'KH0012    ', CAST(N'2021-08-15' AS Date), NULL, NULL, N'NV0007    ', 100000)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0021   ', N'KH0013    ', CAST(N'2021-08-02' AS Date), NULL, NULL, N'NV0008    ', 700000)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0022   ', N'KH0014    ', CAST(N'2021-08-17' AS Date), NULL, NULL, N'NV0009    ', 4000)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0023   ', N'KH0015    ', CAST(N'2021-08-20' AS Date), NULL, NULL, N'NV0010    ', 60000)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0024   ', N'KH0016    ', CAST(N'2021-09-02' AS Date), NULL, NULL, N'NV0010    ', 150000)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0025   ', N'KH0017    ', CAST(N'2021-09-27' AS Date), NULL, NULL, N'NV0003    ', 0)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0026   ', N'KH0012    ', CAST(N'2021-09-23' AS Date), NULL, NULL, N'NV0005    ', 400000)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0027   ', N'KH0018    ', CAST(N'2021-09-20' AS Date), NULL, NULL, N'NV0007    ', 800000)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0028   ', N'KH0019    ', CAST(N'2021-09-25' AS Date), NULL, NULL, N'NV0009    ', 6000)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0029   ', N'KH0011    ', CAST(N'2021-09-26' AS Date), NULL, NULL, N'NV0004    ', 10000)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0030   ', N'KH0012    ', CAST(N'2021-09-27' AS Date), NULL, NULL, N'NV0005    ', 0)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0031   ', N'KH0013    ', CAST(N'2021-09-22' AS Date), NULL, NULL, N'NV0003    ', 1000000)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0032   ', N'KH0015    ', CAST(N'2021-09-23' AS Date), NULL, NULL, N'NV0009    ', 400000)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0033   ', N'KH0014    ', CAST(N'2021-10-02' AS Date), NULL, NULL, N'NV0004    ', 1000000)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0034   ', N'KH0002    ', CAST(N'2021-09-02' AS Date), NULL, NULL, N'NV0010    ', 600000)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0035   ', N'KH0006    ', CAST(N'2021-10-03' AS Date), NULL, NULL, N'NV0004    ', 400000)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0036   ', N'KH0008    ', CAST(N'2021-10-24' AS Date), NULL, NULL, N'NV0005    ', 10000)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0037   ', N'KH0012    ', CAST(N'2021-10-25' AS Date), NULL, NULL, N'NV0007    ', 40000)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0038   ', N'KH0014    ', CAST(N'2021-10-26' AS Date), NULL, NULL, N'NV0008    ', 150000)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0039   ', N'KH0008    ', CAST(N'2021-10-24' AS Date), NULL, NULL, N'NV0009    ', 30000)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0040   ', N'KH0009    ', CAST(N'2020-08-01' AS Date), NULL, NULL, N'NV0008    ', 290000)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0041   ', N'KH0001    ', CAST(N'2020-08-12' AS Date), NULL, NULL, N'NV0006    ', 150000)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0042   ', N'KH0010    ', CAST(N'2020-08-12' AS Date), NULL, NULL, N'NV0004    ', 60000)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0043   ', N'KH0020    ', CAST(N'2020-08-13' AS Date), NULL, NULL, N'NV0006    ', NULL)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0044   ', N'KH0020    ', CAST(N'2020-08-18' AS Date), NULL, NULL, N'NV0009    ', NULL)
GO
INSERT [dbo].[tblXuatKho] ([MaHDX], [MaKH], [NgayXuat], [LiDoXuat], [ThueVAT], [MaNV], [TongTien]) VALUES (N'HDX0045   ', N'KH0007    ', CAST(N'2020-08-20' AS Date), NULL, NULL, N'NV0010    ', NULL)
GO
ALTER TABLE [dbo].[tblChiTietDatHang]  WITH CHECK ADD  CONSTRAINT [FK_tblChiTietDatHang_tblDatHang] FOREIGN KEY([MaHDD])
REFERENCES [dbo].[tblDatHang] ([MaHDD])
GO
ALTER TABLE [dbo].[tblChiTietDatHang] CHECK CONSTRAINT [FK_tblChiTietDatHang_tblDatHang]
GO
ALTER TABLE [dbo].[tblChiTietDatHang]  WITH CHECK ADD  CONSTRAINT [FK_tblChiTietDatHang_tblHang] FOREIGN KEY([MaHang])
REFERENCES [dbo].[tblHang] ([MaHang])
GO
ALTER TABLE [dbo].[tblChiTietDatHang] CHECK CONSTRAINT [FK_tblChiTietDatHang_tblHang]
GO
ALTER TABLE [dbo].[tblChiTietNhapKho]  WITH CHECK ADD  CONSTRAINT [FK_tblChiTietNhapKho_tblHang] FOREIGN KEY([MaHang])
REFERENCES [dbo].[tblHang] ([MaHang])
GO
ALTER TABLE [dbo].[tblChiTietNhapKho] CHECK CONSTRAINT [FK_tblChiTietNhapKho_tblHang]
GO
ALTER TABLE [dbo].[tblChiTietNhapKho]  WITH CHECK ADD  CONSTRAINT [FK_tblChiTietNhapKho_tblNhapKho] FOREIGN KEY([MaHDN])
REFERENCES [dbo].[tblNhapKho] ([MaHDN])
GO
ALTER TABLE [dbo].[tblChiTietNhapKho] CHECK CONSTRAINT [FK_tblChiTietNhapKho_tblNhapKho]
GO
ALTER TABLE [dbo].[tblChiTietXuatKho]  WITH CHECK ADD  CONSTRAINT [FK_tblChiTietXuatKho_tblHang] FOREIGN KEY([MaHang])
REFERENCES [dbo].[tblHang] ([MaHang])
GO
ALTER TABLE [dbo].[tblChiTietXuatKho] CHECK CONSTRAINT [FK_tblChiTietXuatKho_tblHang]
GO
ALTER TABLE [dbo].[tblChiTietXuatKho]  WITH CHECK ADD  CONSTRAINT [FK_tblChiTietXuatKho_tblXuatKho] FOREIGN KEY([MaHDX])
REFERENCES [dbo].[tblXuatKho] ([MaHDX])
GO
ALTER TABLE [dbo].[tblChiTietXuatKho] CHECK CONSTRAINT [FK_tblChiTietXuatKho_tblXuatKho]
GO
ALTER TABLE [dbo].[tblDatHang]  WITH CHECK ADD  CONSTRAINT [FK_tblDatHang_tblKhachHang] FOREIGN KEY([MaKH])
REFERENCES [dbo].[tblKhachHang] ([MaKH])
GO
ALTER TABLE [dbo].[tblDatHang] CHECK CONSTRAINT [FK_tblDatHang_tblKhachHang]
GO
ALTER TABLE [dbo].[tblDatHang]  WITH CHECK ADD  CONSTRAINT [FK_tblDatHang_tblNhanVien] FOREIGN KEY([MaNV])
REFERENCES [dbo].[tblNhanVien] ([MaNV])
GO
ALTER TABLE [dbo].[tblDatHang] CHECK CONSTRAINT [FK_tblDatHang_tblNhanVien]
GO
ALTER TABLE [dbo].[tblHang]  WITH CHECK ADD  CONSTRAINT [FK_tblHang_tblNhomHang] FOREIGN KEY([MaNhomHang])
REFERENCES [dbo].[tblNhomHang] ([MaNhomHang])
GO
ALTER TABLE [dbo].[tblHang] CHECK CONSTRAINT [FK_tblHang_tblNhomHang]
GO
ALTER TABLE [dbo].[tblKhachHang]  WITH CHECK ADD  CONSTRAINT [FK_tblKhachHang_tblNhomKH] FOREIGN KEY([MaNhomKH])
REFERENCES [dbo].[tblNhomKH] ([MaNhomKH])
GO
ALTER TABLE [dbo].[tblKhachHang] CHECK CONSTRAINT [FK_tblKhachHang_tblNhomKH]
GO
ALTER TABLE [dbo].[tblNhanVien]  WITH CHECK ADD  CONSTRAINT [FK_tblNhanVien_tblChucVu] FOREIGN KEY([MaCV])
REFERENCES [dbo].[tblChucVu] ([MaCV])
GO
ALTER TABLE [dbo].[tblNhanVien] CHECK CONSTRAINT [FK_tblNhanVien_tblChucVu]
GO
ALTER TABLE [dbo].[tblNhapKho]  WITH CHECK ADD  CONSTRAINT [FK_tblNhapKho_tblNhaCungCap] FOREIGN KEY([MaNCC])
REFERENCES [dbo].[tblNhaCungCap] ([MaNCC])
GO
ALTER TABLE [dbo].[tblNhapKho] CHECK CONSTRAINT [FK_tblNhapKho_tblNhaCungCap]
GO
ALTER TABLE [dbo].[tblNhapKho]  WITH CHECK ADD  CONSTRAINT [FK_tblNhapKho_tblNhanVien] FOREIGN KEY([MaNV])
REFERENCES [dbo].[tblNhanVien] ([MaNV])
GO
ALTER TABLE [dbo].[tblNhapKho] CHECK CONSTRAINT [FK_tblNhapKho_tblNhanVien]
GO
ALTER TABLE [dbo].[tblXuatKho]  WITH CHECK ADD  CONSTRAINT [FK_tblXuatKho_tblKhachHang] FOREIGN KEY([MaKH])
REFERENCES [dbo].[tblKhachHang] ([MaKH])
GO
ALTER TABLE [dbo].[tblXuatKho] CHECK CONSTRAINT [FK_tblXuatKho_tblKhachHang]
GO
ALTER TABLE [dbo].[tblXuatKho]  WITH CHECK ADD  CONSTRAINT [FK_tblXuatKho_tblNhanVien] FOREIGN KEY([MaNV])
REFERENCES [dbo].[tblNhanVien] ([MaNV])
GO
ALTER TABLE [dbo].[tblXuatKho] CHECK CONSTRAINT [FK_tblXuatKho_tblNhanVien]
GO
/****** Object:  StoredProcedure [dbo].[sp_GDDatHang_DS]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_GDDatHang_DS]
as
begin
	select tblDatHang.MaHDD, TenKH, NoiGiaoHang, CONVERT(nvarchar(20), NgayDatHang, 103) as NgayDatHang, CONVERT(nvarchar(20), NgayGiaoHang, 103) as NgayGiaoHang, sum(SoLuong * GiaBan) as TongTien 
	from tblDatHang join tblKhachHang on tblDatHang.MaKH = tblKhachHang.MaKH 
					join tblChiTietDatHang on tblChiTietDatHang.MaHDD = tblDatHang.MaHDD 
					join tblHang on tblHang.MaHang = tblChiTietDatHang.MaHang 
					group by tblDatHang.MaHDD, TenKH, NoiGiaoHang, NgayDatHang, NgayGiaoHang
end 
GO
/****** Object:  StoredProcedure [dbo].[sp_GDDatHang_TimMaDH]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_GDDatHang_TimMaDH] @MaHDD nvarchar(10)
as
begin
	select tblDatHang.MaHDD, TenKH, NoiGiaoHang, CONVERT(date, NgayDatHang, 103) as NgayDatHang, CONVERT(date, NgayGiaoHang, 103) as NgayGiaoHang, sum(SoLuong * GiaBan) as TongTien
	from tblDatHang join tblKhachHang on tblDatHang.MaKH = tblKhachHang.MaKH 
					join tblChiTietDatHang on tblChiTietDatHang.MaHDD = tblDatHang.MaHDD 
					join tblHang on tblHang.MaHang = tblChiTietDatHang.MaHang
	where tblDatHang.MaHDD like @MaHDD
	group by tblDatHang.MaHDD, TenKH, NoiGiaoHang, NgayDatHang, NgayGiaoHang
end
GO
/****** Object:  StoredProcedure [dbo].[sp_GDDatHang_TimTenKH]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[sp_GDDatHang_TimTenKH] @TenKH nvarchar(100)
as
begin
	select tblDatHang.MaHDD, TenKH, NoiGiaoHang, CONVERT(date, NgayDatHang, 103) as NgayDatHang, CONVERT(date, NgayGiaoHang, 103) as NgayGiaoHang, sum(SoLuong * GiaBan) as TongTien
	from tblDatHang join tblKhachHang on tblDatHang.MaKH = tblKhachHang.MaKH 
					join tblChiTietDatHang on tblChiTietDatHang.MaHDD = tblDatHang.MaHDD 
					join tblHang on tblHang.MaHang = tblChiTietDatHang.MaHang 
	where TenKH like @TenKH 
	group by tblDatHang.MaHDD, TenKH, NoiGiaoHang, NgayDatHang, NgayGiaoHang
end 
GO
/****** Object:  StoredProcedure [dbo].[sp_HangHoa_CTHHtheoMa]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_HangHoa_CTHHtheoMa] @MaHang nchar(10)
as
begin
	Select tblHang.MaHang, TenHang, GiaNhap, GiaBan, Anh, MaNhomHang, DonViTinh, MoTa, MaNCC 
	from tblHang left join tblChiTietNhapKho on tblHang.MaHang = tblChiTietNhapKho.MaHang
				left join tblNhapKho on tblNhapKho.MaHDN = tblChiTietNhapKho.MaHDN
	where tblHang.MaHang = @MaHang
end 
GO
/****** Object:  StoredProcedure [dbo].[sp_HangHoa_DS]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_HangHoa_DS]
as
begin
	Select MaNhomHang, TenNhomHang from tblNhomHang
end 
GO
/****** Object:  StoredProcedure [dbo].[sp_HangHoa_Sua]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_HangHoa_Sua] @MaHang nchar(10), @TenHang nvarchar(255), @GiaNhap float, @GiaBan float, @Anh ntext, @MaNhomHang nchar(10), @DonViTinh nvarchar(50), @MoTa ntext
as
begin
	Update tblHang set MaHang = @MaHang, TenHang = @TenHang, GiaNhap = @GiaNhap, GiaBan = @GiaBan, Anh = @Anh, MaNhomHang = @MaNhomHang, DonViTinh = @DonViTinh, MoTa = @MoTa
	where MaHang = @MaHang
end 
GO
/****** Object:  StoredProcedure [dbo].[sp_HangHoa_Them]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_HangHoa_Them] @MaHang nchar(10), @TenHang nvarchar(255), @GiaNhap float, @GiaBan float, @Anh ntext, @MaNhomHang nchar(10), @DonViTinh nvarchar(50), @MoTa ntext
as
begin
	Insert into tblHang (MaHang, TenHang, GiaNhap, GiaBan, Anh, MaNhomHang, DonViTinh, MoTa) values (@MaHang, @TenHang, @GiaNhap, @GiaBan, @Anh, @MaNhomHang, @DonViTinh, @MoTa)
end 
GO
/****** Object:  StoredProcedure [dbo].[sp_HangHoa_Xoa]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_HangHoa_Xoa] @MaHang nchar(10)
as
begin
	Delete from tblHang where MaHang = @MaHang
end 
GO
/****** Object:  StoredProcedure [dbo].[sp_HHDanhMuc_DSHH]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_HHDanhMuc_DSHH]
as
begin
	select MaHang, TenHang, GiaBan, GiaNhap from tblHang
end 
GO
/****** Object:  StoredProcedure [dbo].[sp_HHDanhMuc_LayDSNhomHang]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_HHDanhMuc_LayDSNhomHang] @MaNhomHang nchar(10)
as
begin
	
select MaHang, TenHang, GiaBan, GiaNhap from tblHang where MaNhomHang = @MaNhomHang
end 
GO
/****** Object:  StoredProcedure [dbo].[sp_HHDanhMuc_LayDSraTuGrid]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_HHDanhMuc_LayDSraTuGrid] @MaHang nchar(10)
as
begin
	SELECT tblHang.MaHang, TenHang, Anh, TenNhomHang, DonViTinh, TenNCC, GiaNhap, GiaBan, MoTa 
	from tblHang join tblNhomHang on tblHang.MaNhomHang = tblNhomHang.MaNhomHang 
				join tblChiTietNhapKho on tblChiTietNhapKho.MaHang = tblHang.MaHang 
				join tblNhapKho on tblNhapKho.MaHDN = tblChiTietNhapKho.MaHDN join tblNhaCungCap on 
				tblNhaCungCap.MaNCC = tblNhapKho.MaNCC where tblHang.MaHang = @MaHang
end 
GO
/****** Object:  StoredProcedure [dbo].[sp_HHDanhMuc_LoadCboMaHang]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_HHDanhMuc_LoadCboMaHang]
as
begin
	Select MaNhomHang, TenNhomHang from tblNhomHang
end 
GO
/****** Object:  StoredProcedure [dbo].[sp_HHDanhMuc_TimDSHH]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_HHDanhMuc_TimDSHH] @TenHang nvarchar(255)
as
begin
	select MaHang, TenHang, GiaBan, GiaNhap from tblHang where TenHang like @TenHang
end 
GO
/****** Object:  StoredProcedure [dbo].[sp_NCC_DS]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_NCC_DS]
as
begin
	Select MaNCC, TenNCC from tblNhaCungCap
end 
GO
/****** Object:  StoredProcedure [dbo].[sp_NhomHang_ds]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_NhomHang_ds]
as
begin
	select MaNhomHang, TenNhomHang from tblNhomHang
end 

GO
/****** Object:  StoredProcedure [dbo].[sp_NhomHang_dsTheoMa]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_NhomHang_dsTheoMa] @MaNhomHang nvarchar(10)
as
begin
	select MaNhomHang, TenNhomHang from tblNhomHang where MaNhomHang = @MaNhomHang
end 
GO
/****** Object:  StoredProcedure [dbo].[sp_NhomHang_Sua]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_NhomHang_Sua] @MaNhomHang nvarchar(10), @TenNhomHang nvarchar(255)
as
begin
	update tblNhomHang set TenNhomHang = @TenNhomHang where MaNhomHang = @MaNhomHang
end 
GO
/****** Object:  StoredProcedure [dbo].[sp_NhomHang_Them]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_NhomHang_Them] @MaNhomHang nchar(10), @TenNhomHang nvarchar(255)
as
begin
	insert into tblNhomHang(MaNhomHang, TenNhomHang) values(@MaNhomHang, @TenNhomHang)
end 
GO
/****** Object:  StoredProcedure [dbo].[sp_NhomHang_Xoa]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_NhomHang_Xoa] @MaNhomHang nchar(10)
as
begin
	delete tblNhomHang where MaNhomHang = @MaNhomHang
end 
GO
/****** Object:  StoredProcedure [dbo].[sp_ThemDDH_DSHangChon]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_ThemDDH_DSHangChon]
as
begin select MaHang, TenHang, GiaBan from tblHang
end
GO
/****** Object:  StoredProcedure [dbo].[sp_ThemDDH_DsHangDaChon]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_ThemDDH_DsHangDaChon]
as
begin
	select MaHang, TenHang, GiaBan, SoLuong, ThanhTien from DsHangDaChon
end
GO
/****** Object:  StoredProcedure [dbo].[sp_ThemDDH_DSNhomHang]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_ThemDDH_DSNhomHang] @MaNhomHang nvarchar(10)
as
begin select MaHang, TenHang, GiaBan from tblHang where MaNhomHang = @MaNhomHang
end
GO
/****** Object:  StoredProcedure [dbo].[sp_ThemDDH_LamMoiBang]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[sp_ThemDDH_LamMoiBang]
as
begin
	delete DsHangDaChon
end
GO
/****** Object:  StoredProcedure [dbo].[sp_ThemDDH_LoadCboNH]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_ThemDDH_LoadCboNH]
as
begin
	select MaNhomHang, TenNhomHang from tblNhomHang
end
GO
/****** Object:  StoredProcedure [dbo].[sp_ThemDDH_ThemHangChon]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_ThemDDH_ThemHangChon] @MaHDD nvarchar(10), @MaHang nvarchar(10), @TenHang nvarchar(255), @GiaBan float, @SoLuong int
as
begin
	if((select count(*) from DsHangDaChon where MaHang = @MaHang) = 0)
	begin
		insert into DsHangDaChon(MaHang, TenHang, GiaBan, SoLuong) values (@MaHang, @TenHang, @GiaBan, @SoLuong)
		insert into tblChiTietDatHang(MaHDD, MaHang, SoLuong) values (@MaHDD, @MaHang, @SoLuong)
	end
	else
	begin
		update DsHangDaChon set SoLuong = @SoLuong where MaHang = @MaHang
		update tblChiTietDatHang set SoLuong = @SoLuong where MaHang = @MaHang and MaHDD = @MaHDD
	end
end
GO
/****** Object:  StoredProcedure [dbo].[sp_ThemDDH_ThemMaHDD]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_ThemDDH_ThemMaHDD]
as
begin
	insert into tblDatHang(MaHDD) values ('')
end
GO
/****** Object:  StoredProcedure [dbo].[sp_ThemDDH_XoaHangDaChon]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--x??a h??ng khi b???m ????p chu???t
CREATE proc [dbo].[sp_ThemDDH_XoaHangDaChon] @MaHDD varchar(10), @MaHang nvarchar(10)
as
begin
	delete DsHangDaChon where MaHang = @MaHang
	delete tblChiTietDatHang where MaHDD = @MaHDD and MaHang = @MaHang
end
GO
/****** Object:  StoredProcedure [dbo].[sp_ThemDDH_XoaMaHDDCuoiCung]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_ThemDDH_XoaMaHDDCuoiCung]
as
begin
	declare @MaHDD nvarchar(10)
	select @MaHDD = (select top(1) MaHDD from tblDatHang order by MaHDD desc)
	delete tblChiTietDatHang where MaHDD = @MaHDD
	delete tblDatHang where MaHDD = @MaHDD
end
GO
/****** Object:  StoredProcedure [dbo].[sp_ThemDDT_LayDSKH]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_ThemDDT_LayDSKH] @MaKH nvarchar(10)
as
begin
	select MaKH, TenKH, SDT, DiaChi
	from tblKhachHang where MaKH = @MaKH
end
GO
/****** Object:  StoredProcedure [dbo].[sp_ThemHDD_HuyDat]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_ThemHDD_HuyDat] @MaHDD varchar(10)
as
begin
	delete tblChiTietDatHang where MaHDD = @MaHDD
	delete tblDatHang where MaHDD = @MaHDD
end
GO
/****** Object:  StoredProcedure [dbo].[sp_ThietLapGia_Giam]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_ThietLapGia_Giam] @MaNhomHang nchar(10), @PhanTram float
as
begin
	Update tblHang set GiaBan = GiaBan - @PhanTram * GiaBan
	where MaNhomHang = @MaNhomHang
end 
GO
/****** Object:  StoredProcedure [dbo].[sp_ThietLapGia_Tang]    Script Date: 11/27/2021 10:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_ThietLapGia_Tang] @MaNhomHang nchar(10), @PhanTram float
as
begin
	Update tblHang set GiaBan = GiaBan + @PhanTram * GiaBan
	where MaNhomHang = @MaNhomHang
end 
GO
USE [master]
GO
ALTER DATABASE [TestDuLieu] SET  READ_WRITE 
GO
