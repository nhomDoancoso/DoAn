- Khi lấy về nhớ mở package manager console và paste: Scaffold-DbContext "Server=HIEPHUYNHBF54\SQLEXPRESS;Database=DLCT;Integrated Security=true;Encrypt=true;TrustServerCertificate=true;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -force
thay tên Server bằng tên Server DB của mình sau đó enter để upate lại DB
- Sau đó lên Github https://github.com/nhomDoancoso/DoAn mở Model vào Cart.cs copy đoạn
[

        [NotMapped]

        public double? TotalAmount

        {

                get    
                { 
                   if (Quantity.HasValue && Product != null)
                
                {
                    return Quantity.Value * Product.Price;
                }
            
                return null;}
        
                private set { }
          }
        
        
            public void UpdateTotalAmount()
        
            {
                if (Quantity.HasValue && Product != null)
                
                {
                    TotalAmount = Quantity.Value * Product.Price;
                }
                else
                {
                    TotalAmount = null;
                }
            }
]  
  -Ok chạy DoAn

-----------------------------------------------------------------------------------------------
--database--
USE [master]
GO
/****** Object:  Database [DLCT]    Script Date: 12/24/2023 12:06:22 PM ******/
CREATE DATABASE [DLCT]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DLCT', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\DLCT.mdf' , SIZE = 3136KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DLCT_log', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\DLCT_log.ldf' , SIZE = 784KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [DLCT] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DLCT].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DLCT] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DLCT] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DLCT] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DLCT] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DLCT] SET ARITHABORT OFF 
GO
ALTER DATABASE [DLCT] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [DLCT] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [DLCT] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DLCT] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DLCT] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DLCT] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DLCT] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DLCT] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DLCT] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DLCT] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DLCT] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DLCT] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DLCT] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DLCT] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DLCT] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DLCT] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DLCT] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DLCT] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DLCT] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DLCT] SET  MULTI_USER 
GO
ALTER DATABASE [DLCT] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DLCT] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DLCT] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DLCT] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [DLCT]
GO
/****** Object:  Table [dbo].[BILL]    Script Date: 12/24/2023 12:06:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BILL](
	[Bill_id] [int] IDENTITY(1,1) NOT NULL,
	[Created_at] [datetime] NULL,
	[Date] [datetime] NOT NULL,
	[Client_id] [int] NULL,
 CONSTRAINT [PK_BILL] PRIMARY KEY CLUSTERED 
(
	[Bill_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BILLDETAIL]    Script Date: 12/24/2023 12:06:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BILLDETAIL](
	[Bill_id] [int] NOT NULL,
	[Product_id] [int] NOT NULL,
	[Quantity] [int] NULL,
	[Price] [float] NULL,
 CONSTRAINT [PK_BILLDETAIL] PRIMARY KEY CLUSTERED 
(
	[Bill_id] ASC,
	[Product_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BLOG_CATEGORIES]    Script Date: 12/24/2023 12:06:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BLOG_CATEGORIES](
	[Blog_category_id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](500) NULL,
	[Description] [nvarchar](500) NULL,
 CONSTRAINT [PK_BLOG_CATEGORIES] PRIMARY KEY CLUSTERED 
(
	[Blog_category_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BLOG_POSTS]    Script Date: 12/24/2023 12:06:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BLOG_POSTS](
	[Blog_post_id] [int] IDENTITY(1,1) NOT NULL,
	[Titile] [nvarchar](100) NULL,
	[Body] [nvarchar](500) NULL,
	[Thumbnail] [nvarchar](250) NULL,
	[Date_time] [datetime] NULL,
	[Blog_category_id] [int] NULL,
	[Staff_id] [int] NULL,
 CONSTRAINT [PK_BLOG_POSTS] PRIMARY KEY CLUSTERED 
(
	[Blog_post_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BOOKING]    Script Date: 12/24/2023 12:06:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BOOKING](
	[Booking_id] [int] IDENTITY(1,1) NOT NULL,
	[Client_id] [int] NULL,
	[Staff_id] [int] NULL,
	[Name] [nvarchar](100) NULL,
	[Phone] [varchar](10) NULL,
	[Date_time] [datetime] NULL,
	[Note] [nvarchar](255) NULL,
	[Status] [bit] NULL,
	[Combo_id] [int] NULL,
	[Created_at] [datetime] NULL,
	[Branch_id] [int] NULL,
 CONSTRAINT [PK_BOOKING] PRIMARY KEY CLUSTERED 
(
	[Booking_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BOOKINGDETAIL]    Script Date: 12/24/2023 12:06:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOOKINGDETAIL](
	[Booking_id] [int] NOT NULL,
	[Service_id] [int] NOT NULL,
	[Price] [float] NULL,
 CONSTRAINT [PK_BOOKINGDETAIL] PRIMARY KEY CLUSTERED 
(
	[Booking_id] ASC,
	[Service_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BRANCH]    Script Date: 12/24/2023 12:06:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BRANCH](
	[Branch_id] [int] IDENTITY(1,1) NOT NULL,
	[Address] [nvarchar](255) NULL,
	[Hotline] [varchar](10) NULL,
 CONSTRAINT [PK_BRANCH] PRIMARY KEY CLUSTERED 
(
	[Branch_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CART]    Script Date: 12/24/2023 12:06:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CART](
	[user_id] [int] NOT NULL,
	[Product_id] [int] NOT NULL,
	[Quantity] [int] NULL,
 CONSTRAINT [PK_CART] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC,
	[Product_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CLIENT]    Script Date: 12/24/2023 12:06:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CLIENT](
	[Client_id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Username] [varchar](255) NULL,
	[Password] [varchar](255) NULL,
	[Phone] [varchar](10) NULL,
	[Avatar] [varchar](255) NULL,
	[Address] [nvarchar](255) NULL,
	[Email] [varchar](255) NULL,
	[Role_id] [int] NULL,
	[Created_at] [datetime] NULL,
	[Updated_at] [datetime] NULL,
	[Created_by] [nvarchar](255) NULL,
	[Updated_by] [nvarchar](255) NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_CILENT] PRIMARY KEY CLUSTERED 
(
	[Client_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[COMBO]    Script Date: 12/24/2023 12:06:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[COMBO](
	[Combo_id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Price] [float] NULL,
	[Created_at] [datetime] NULL,
	[Created_by] [int] NULL,
	[Updated_at] [datetime] NULL,
	[Updated_by] [int] NULL,
 CONSTRAINT [PK_COMBO] PRIMARY KEY CLUSTERED 
(
	[Combo_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[COMBODETAIL]    Script Date: 12/24/2023 12:06:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[COMBODETAIL](
	[Combo_id] [int] NOT NULL,
	[Service_id] [int] NOT NULL,
	[Price] [float] NULL,
 CONSTRAINT [PK_COMBODETAIL] PRIMARY KEY CLUSTERED 
(
	[Combo_id] ASC,
	[Service_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PRODUCT]    Script Date: 12/24/2023 12:06:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PRODUCT](
	[Product_id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[Price] [float] NULL,
	[Quantity] [int] NULL,
	[Product_type_id] [int] NULL,
	[Image] [nvarchar](200) NULL,
	[Provider_id] [int] NULL,
	[Created_at] [datetime] NULL,
	[Created_by] [int] NULL,
	[Updated_at] [datetime] NULL,
	[Updated_by] [int] NULL,
	[Sold] [int] NULL CONSTRAINT [DF_PRODUCT_Sold]  DEFAULT ((0)),
 CONSTRAINT [PK_PRODUCT] PRIMARY KEY CLUSTERED 
(
	[Product_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PRODUCTTYPE]    Script Date: 12/24/2023 12:06:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PRODUCTTYPE](
	[Product_type_id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
 CONSTRAINT [PK_PRODUCT TYPE] PRIMARY KEY CLUSTERED 
(
	[Product_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PROVIDER]    Script Date: 12/24/2023 12:06:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PROVIDER](
	[Provider_id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Address] [nvarchar](255) NULL,
	[Phone] [char](10) NULL,
	[Email] [nvarchar](255) NULL,
 CONSTRAINT [PK_PROVIDER] PRIMARY KEY CLUSTERED 
(
	[Provider_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ROLE]    Script Date: 12/24/2023 12:06:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ROLE](
	[Role_id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
 CONSTRAINT [PK_ROLE] PRIMARY KEY CLUSTERED 
(
	[Role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SCHEDULE]    Script Date: 12/24/2023 12:06:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SCHEDULE](
	[Schedule_id] [int] IDENTITY(1,1) NOT NULL,
	[Time] [time](7) NULL,
 CONSTRAINT [PK_SCHEDULE] PRIMARY KEY CLUSTERED 
(
	[Schedule_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SCHEDULEDETAIL]    Script Date: 12/24/2023 12:06:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SCHEDULEDETAIL](
	[Schedule_id] [int] NOT NULL,
	[Staff_id] [int] NOT NULL,
	[Date] [date] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_SCHEDULE DETAIL] PRIMARY KEY CLUSTERED 
(
	[Schedule_id] ASC,
	[Staff_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SERVICE]    Script Date: 12/24/2023 12:06:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SERVICE](
	[Service_id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Price] [float] NULL,
	[Status] [bit] NULL,
	[Service_type_id] [int] NULL,
	[Created_at] [datetime] NULL,
	[Created_by] [int] NULL,
	[Updated_at] [datetime] NULL,
	[Updated_by] [int] NULL,
 CONSTRAINT [PK_SERVICE] PRIMARY KEY CLUSTERED 
(
	[Service_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SERVICETYPE]    Script Date: 12/24/2023 12:06:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SERVICETYPE](
	[Service_type_id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
 CONSTRAINT [PK_ServiceType] PRIMARY KEY CLUSTERED 
(
	[Service_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[STAFF]    Script Date: 12/24/2023 12:06:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[STAFF](
	[Staff_id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Username] [varchar](255) NULL,
	[Password] [varchar](255) NULL,
	[Phone] [varchar](10) NULL,
	[Avatar] [varchar](255) NULL,
	[Address] [nvarchar](255) NULL,
	[Email] [varchar](255) NULL,
	[Status] [bit] NULL,
	[IsDisabled] [bit] NULL,
	[Role_id] [int] NULL,
	[Created_at] [datetime] NULL,
	[Updated_at] [datetime] NULL,
	[Created_by] [nvarchar](255) NULL,
	[Updated_by] [nvarchar](255) NULL,
	[Branch_id] [int] NULL,
 CONSTRAINT [PK_STAFF] PRIMARY KEY CLUSTERED 
(
	[Staff_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[BILL] ON 

INSERT [dbo].[BILL] ([Bill_id], [Created_at], [Date], [Client_id]) VALUES (101, CAST(N'2023-12-24 08:44:39.820' AS DateTime), CAST(N'2023-12-24 01:44:39.820' AS DateTime), 20)
SET IDENTITY_INSERT [dbo].[BILL] OFF
INSERT [dbo].[BILLDETAIL] ([Bill_id], [Product_id], [Quantity], [Price]) VALUES (101, 55, 1, 399000)
SET IDENTITY_INSERT [dbo].[BLOG_CATEGORIES] ON 

INSERT [dbo].[BLOG_CATEGORIES] ([Blog_category_id], [Title], [Description]) VALUES (4, N'Tất cả', N'Mọi thể loại bài viết')
INSERT [dbo].[BLOG_CATEGORIES] ([Blog_category_id], [Title], [Description]) VALUES (5, N'Tóc tai', N'Bài viết liên quan đến tóc tai')
INSERT [dbo].[BLOG_CATEGORIES] ([Blog_category_id], [Title], [Description]) VALUES (6, N'Chi nhánh', N'Bài viết liên quan đến chi nhánh')
SET IDENTITY_INSERT [dbo].[BLOG_CATEGORIES] OFF
SET IDENTITY_INSERT [dbo].[BLOG_POSTS] ON 

INSERT [dbo].[BLOG_POSTS] ([Blog_post_id], [Titile], [Body], [Thumbnail], [Date_time], [Blog_category_id], [Staff_id]) VALUES (19, N'Mới Nhất 2021: Shine Combo cắt gội 10 bước, giá chỉ 80k', N'Đặc trưng của kiểu tóc Side Part rủ là phần tóc', N'/images/30S9MT86-Xịt khử mùi Perspi-Guard Maximum Strength Antiperspirant 30ml.jpg', CAST(N'2023-12-10 14:27:20.207' AS DateTime), 4, 6)
INSERT [dbo].[BLOG_POSTS] ([Blog_post_id], [Titile], [Body], [Thumbnail], [Date_time], [Blog_category_id], [Staff_id]) VALUES (20, N'Shine Combo siêu xịn ', N'Combo gồm các bước: ...', N'/images/sap-vuot-toc-dynamite-clay_master.png', CAST(N'2023-12-16 10:36:05.693' AS DateTime), 4, 9)
SET IDENTITY_INSERT [dbo].[BLOG_POSTS] OFF
SET IDENTITY_INSERT [dbo].[BOOKING] ON 

INSERT [dbo].[BOOKING] ([Booking_id], [Client_id], [Staff_id], [Name], [Phone], [Date_time], [Note], [Status], [Combo_id], [Created_at], [Branch_id]) VALUES (150, NULL, 6, N'moew moew', N'0767537243', CAST(N'2023-12-28 18:11:00.000' AS DateTime), N'cắt chỉ cho mình nhé arigato', 0, 20, CAST(N'2023-12-23 21:11:55.227' AS DateTime), 3)
INSERT [dbo].[BOOKING] ([Booking_id], [Client_id], [Staff_id], [Name], [Phone], [Date_time], [Note], [Status], [Combo_id], [Created_at], [Branch_id]) VALUES (151, 16, 6, N'moew moew', N'0767537243', CAST(N'2023-12-29 17:12:00.000' AS DateTime), N'ccasc', 0, 21, CAST(N'2023-12-23 21:12:45.590' AS DateTime), 3)
INSERT [dbo].[BOOKING] ([Booking_id], [Client_id], [Staff_id], [Name], [Phone], [Date_time], [Note], [Status], [Combo_id], [Created_at], [Branch_id]) VALUES (152, NULL, 6, N'moew moew', N'0767537243', CAST(N'2023-12-27 05:46:00.000' AS DateTime), N'cat toc', 1, 19, CAST(N'2023-12-24 08:47:18.130' AS DateTime), 3)
SET IDENTITY_INSERT [dbo].[BOOKING] OFF
SET IDENTITY_INSERT [dbo].[BRANCH] ON 

INSERT [dbo].[BRANCH] ([Branch_id], [Address], [Hotline]) VALUES (1, N'Quận 8', N'0903555605')
INSERT [dbo].[BRANCH] ([Branch_id], [Address], [Hotline]) VALUES (3, N'Quận 9', N'0903555605')
INSERT [dbo].[BRANCH] ([Branch_id], [Address], [Hotline]) VALUES (5, N'Quận 7', N'0903556072')
INSERT [dbo].[BRANCH] ([Branch_id], [Address], [Hotline]) VALUES (6, N'Quận Cam', N'0576240751')
SET IDENTITY_INSERT [dbo].[BRANCH] OFF
INSERT [dbo].[CART] ([user_id], [Product_id], [Quantity]) VALUES (20, 55, 1)
INSERT [dbo].[CART] ([user_id], [Product_id], [Quantity]) VALUES (20, 60, 1)
SET IDENTITY_INSERT [dbo].[CLIENT] ON 

INSERT [dbo].[CLIENT] ([Client_id], [Name], [Username], [Password], [Phone], [Avatar], [Address], [Email], [Role_id], [Created_at], [Updated_at], [Created_by], [Updated_by], [Status]) VALUES (16, N'Phathuynh1', N'Phathuynh1234@', N'AQAAAAEAACcQAAAAECW4e58cJu6NYmDGTQh361/GSmrx4R5HjO9+F7U8apgtJJ2NuoZP82XNtcdm3guSaA==', N'0767537375', NULL, N'Quận 9', N'doremon8408@gmail.com', 3, CAST(N'2023-11-23 15:49:33.153' AS DateTime), CAST(N'2023-12-23 19:25:44.507' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[CLIENT] ([Client_id], [Name], [Username], [Password], [Phone], [Avatar], [Address], [Email], [Role_id], [Created_at], [Updated_at], [Created_by], [Updated_by], [Status]) VALUES (17, N'khanh', N'Khan123@', N'AQAAAAEAACcQAAAAEOx4J8/KQXb4SxSV5giVxVT0HuI7XgwTga6fYO84/yPwp4skfyVGItqTfgEMIMAiEQ==', N'0767537373', NULL, N'Quận 8', N'asd123@gmail.com', 3, CAST(N'2023-11-26 21:59:35.347' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[CLIENT] ([Client_id], [Name], [Username], [Password], [Phone], [Avatar], [Address], [Email], [Role_id], [Created_at], [Updated_at], [Created_by], [Updated_by], [Status]) VALUES (18, N'ddsdfsfd', N'Hieuvo1234ss35@', N'AQAAAAEAACcQAAAAEGdaf7U5MNjGxFBwjrU6V7IfL/Ed3ZbqySiiKxe6Y5OnynWtDhRIfELOdzD62IroxA==', N'0903555605', N'hinhhieu', N'giai viet', N'dd@gmail.com', 3, CAST(N'2023-11-28 12:36:42.863' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[CLIENT] ([Client_id], [Name], [Username], [Password], [Phone], [Avatar], [Address], [Email], [Role_id], [Created_at], [Updated_at], [Created_by], [Updated_by], [Status]) VALUES (19, N'Nam Huỳnh', N'NamPeo@', N'AQAAAAEAACcQAAAAEMOiPX60FZdThjTflxhqIXYC8fdtEks7GSZrR00rV0Gl7r1goXTSErEAOPTuLe0iTw==', N'0334567891', N'', N'25/5e Nguyen Thi Thanh', N'dungvovan311@gmail.com', 3, CAST(N'2023-12-16 09:49:28.273' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[CLIENT] ([Client_id], [Name], [Username], [Password], [Phone], [Avatar], [Address], [Email], [Role_id], [Created_at], [Updated_at], [Created_by], [Updated_by], [Status]) VALUES (20, N'hiephuynh', N'Hiephuynh123@', N'AQAAAAEAACcQAAAAEO+6DoTUAbeIOR8dtvBPuNAOfyh/IGKW6KIv58Ah7PrcGgA0wPHUji/52b2SvmGqNg==', N'0767537243', N'', N'chung cư ta quang bưu', N'huynhhiepvan1998@gmail.com', 3, CAST(N'2023-12-23 16:54:42.957' AS DateTime), NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[CLIENT] OFF
SET IDENTITY_INSERT [dbo].[COMBO] ON 

INSERT [dbo].[COMBO] ([Combo_id], [Name], [Price], [Created_at], [Created_by], [Updated_at], [Updated_by]) VALUES (16, N'Cắt tóc, gội Đầu', 70000, CAST(N'2023-11-29 20:55:53.833' AS DateTime), 6, CAST(N'2023-12-16 10:50:39.280' AS DateTime), 6)
INSERT [dbo].[COMBO] ([Combo_id], [Name], [Price], [Created_at], [Created_by], [Updated_at], [Updated_by]) VALUES (17, N'Cắt tóc, cạo mặt', 60000, CAST(N'2023-11-29 20:56:13.063' AS DateTime), 6, CAST(N'2023-12-16 10:50:49.700' AS DateTime), 6)
INSERT [dbo].[COMBO] ([Combo_id], [Name], [Price], [Created_at], [Created_by], [Updated_at], [Updated_by]) VALUES (19, N'Nhuộm tóc, Nối mi', 100000, CAST(N'2023-11-29 21:11:43.040' AS DateTime), 8, CAST(N'2023-12-16 10:51:40.010' AS DateTime), 6)
INSERT [dbo].[COMBO] ([Combo_id], [Name], [Price], [Created_at], [Created_by], [Updated_at], [Updated_by]) VALUES (20, N'Cắt tóc, gội đầu, ráy tai', 80000, CAST(N'2023-12-16 10:51:55.410' AS DateTime), 6, NULL, NULL)
INSERT [dbo].[COMBO] ([Combo_id], [Name], [Price], [Created_at], [Created_by], [Updated_at], [Updated_by]) VALUES (21, N'Cắt tóc, ráy tai, cạo mặt', 80000, CAST(N'2023-12-16 10:52:30.570' AS DateTime), 6, CAST(N'2023-12-16 10:52:38.630' AS DateTime), 6)
SET IDENTITY_INSERT [dbo].[COMBO] OFF
SET IDENTITY_INSERT [dbo].[PRODUCT] ON 

INSERT [dbo].[PRODUCT] ([Product_id], [Name], [Description], [Price], [Quantity], [Product_type_id], [Image], [Provider_id], [Created_at], [Created_by], [Updated_at], [Updated_by], [Sold]) VALUES (55, N'Sáp vuốt tóc Glanzen Clay Wax - Vô địch giữ nếp tới 24 giờ', N'Được sản xuất bởi công nghệ hiện đại của Đức - vùng đất của những sản phẩm chất lượng hàng đầu thế giới, Glanzen Clay mang lại chất lượng cực tốt cho người sử dụng. Sản phẩm thách thức thời gian với độ giữ nếp tới 12h giờ, khả năng thấm hút dầu cực tốt. ', 399000, 183, 8, N'/images/wax vuốt tóc glanzen.jpg', 9, CAST(N'2023-12-07 10:53:14.110' AS DateTime), 6, CAST(N'2023-12-22 16:45:11.167' AS DateTime), 6, 1)
INSERT [dbo].[PRODUCT] ([Product_id], [Name], [Description], [Price], [Quantity], [Product_type_id], [Image], [Provider_id], [Created_at], [Created_by], [Updated_at], [Updated_by], [Sold]) VALUES (58, N'Sáp Glanzen xanh - Glanzen Prime - Floral ', N'Luôn nằm trong Top những dòng sáp vuốt tóc bán chạy', 359000, 143, 8, N'/images/Glazen_xanh.jpg', 9, CAST(N'2023-12-07 11:00:02.067' AS DateTime), 6, CAST(N'2023-12-22 16:45:19.507' AS DateTime), 6, 0)
INSERT [dbo].[PRODUCT] ([Product_id], [Name], [Description], [Price], [Quantity], [Product_type_id], [Image], [Provider_id], [Created_at], [Created_by], [Updated_at], [Updated_by], [Sold]) VALUES (59, N'Reuzel Green Pomade (Grease Medium Hold)', N'Reuzel Green (Grease Medium Hold) là một trong những dòng pomade gốc dầu được săn đón nhiều của nhà Reuzel Schorem nhờ khả năng giữ nếp tốt, tạo độ bóng mờ và phồng tóc ấn tượng', 499000, 98, 8, N'/images/Reuzei_wax.jpg', 9, CAST(N'2023-12-07 11:01:14.283' AS DateTime), 6, CAST(N'2023-12-21 18:37:42.730' AS DateTime), 6, 0)
INSERT [dbo].[PRODUCT] ([Product_id], [Name], [Description], [Price], [Quantity], [Product_type_id], [Image], [Provider_id], [Created_at], [Created_by], [Updated_at], [Updated_by], [Sold]) VALUES (60, N'Sữa rửa mặt Dr. for Skin Charcoal than hoạt tính New 2023 100g Trắng da kiềm dầu', N'Sữa rửa mặt cho nam Skin&Dr Than Hoạt Tính 100g trắng da kiềm dầu - 30Shine phân phối chính hãng', 199000, 128, 4, N'/images/sữa-rửa-mặt-tạo-bọt.jpg', 8, CAST(N'2023-12-07 11:02:19.430' AS DateTime), 6, CAST(N'2023-12-16 10:57:55.427' AS DateTime), 6, 0)
INSERT [dbo].[PRODUCT] ([Product_id], [Name], [Description], [Price], [Quantity], [Product_type_id], [Image], [Provider_id], [Created_at], [Created_by], [Updated_at], [Updated_by], [Sold]) VALUES (61, N'Kem chống nắng chống lão hóa, bảo vệ cao cho da mặt SUN SOUL FACE CREAM SPF 30', N'KHUYẾN NGHỊ CỦA BÁC SĨ DA LIỄU ĐỂ NGĂN NGỪA NÁM VÀ ĐỐM SẬM MÀU Luôn sử dụng kem chống nắng - SUN SOUL', 99000, 109, 4, N'/images/Kem chống nắng.jpg', 8, CAST(N'2023-12-07 11:04:41.690' AS DateTime), 6, CAST(N'2023-12-16 10:58:04.540' AS DateTime), 6, 0)
INSERT [dbo].[PRODUCT] ([Product_id], [Name], [Description], [Price], [Quantity], [Product_type_id], [Image], [Provider_id], [Created_at], [Created_by], [Updated_at], [Updated_by], [Sold]) VALUES (64, N'Sữa rửa mặt giúp làm sạch và cấp ẩm Cerave', N'Sữa rửa mặt có thể loại bỏ bụi bẩn, lớp trang điểm', 199000, 150, 4, N'/images/30SRZJNG-30SCXLCT-download.jpg', 8, CAST(N'2023-12-07 11:11:28.213' AS DateTime), 6, CAST(N'2023-12-16 10:58:23.187' AS DateTime), 6, 0)
INSERT [dbo].[PRODUCT] ([Product_id], [Name], [Description], [Price], [Quantity], [Product_type_id], [Image], [Provider_id], [Created_at], [Created_by], [Updated_at], [Updated_by], [Sold]) VALUES (65, N'Dầu xả Blairsom Thảo Mộc Phục Hồi 500ml', N'Dầu xả Blairsom Thảo Mộc Phục Hồi Collagen Đại Dương & Tinh Dầu 7 Trong 1 chiết xuất từ Collagen Đại Dương và Tinh Dầu Thảo Mộc nội địa Úc, được phát triển bởi những chuyên gia với hơn 50 năm kinh nghiệm trong ngành hóa mỹ phẩm.', 99000, 150, 5, N'/images/Dầu gội giảm gàu.jpg', 8, CAST(N'2023-12-07 11:12:26.643' AS DateTime), 6, CAST(N'2023-12-16 10:58:31.450' AS DateTime), 6, 0)
INSERT [dbo].[PRODUCT] ([Product_id], [Name], [Description], [Price], [Quantity], [Product_type_id], [Image], [Provider_id], [Created_at], [Created_by], [Updated_at], [Updated_by], [Sold]) VALUES (69, N'Xịt Dưỡng Tóc It''s A 10 Miracle Leave-In Mềm Mượt ', N'Tạm biệt các bước chăm sóc tóc cầu kì, phức tạp!', 159000, 150, 5, N'/images/30SDLXT6-1.jpg', 8, CAST(N'2023-12-07 11:14:01.627' AS DateTime), 6, CAST(N'2023-12-16 10:58:40.047' AS DateTime), 6, 0)
INSERT [dbo].[PRODUCT] ([Product_id], [Name], [Description], [Price], [Quantity], [Product_type_id], [Image], [Provider_id], [Created_at], [Created_by], [Updated_at], [Updated_by], [Sold]) VALUES (73, N'Suavecito Firme Hold Pomade', N'Pomade gốc nước có mùi tuyệt vời được tạo ra để giữ cho tóc của bạn ở đúng vị trí cho cả ngày và vào ban đêm nếu cần.  Cung cấp độ bám chắc chắn cho các kiểu tóc bóng mượt, pompadours, các bộ phận bên hông hoặc bất kỳ kiểu tóc nào bạn cần', 299000, 99, 8, N'/images/a33dbdaf-f04a-4bc6-9f94-6ae17bfa58dd.jpg', 9, CAST(N'2023-12-22 16:56:44.193' AS DateTime), 6, CAST(N'2023-12-22 23:34:03.540' AS DateTime), 6, 0)
INSERT [dbo].[PRODUCT] ([Product_id], [Name], [Description], [Price], [Quantity], [Product_type_id], [Image], [Provider_id], [Created_at], [Created_by], [Updated_at], [Updated_by], [Sold]) VALUES (74, N'Hanz De Fuko Modify Pomade', N'Hanz De Fuko Modify Pomade là dòng sáp vuốt tóc độc quyền của thương hiệu Hanz De Fuko duy nhất trên thế giới. Đây là sản phẩm mang lại những ưu điểm vượt trội', 539000, 100, 8, N'/images/325f1cc1-16b0-4527-8ea6-e1c0cb10717c.png', 8, CAST(N'2023-12-22 16:58:22.450' AS DateTime), 6, CAST(N'2023-12-22 23:34:20.627' AS DateTime), 6, 0)
INSERT [dbo].[PRODUCT] ([Product_id], [Name], [Description], [Price], [Quantity], [Product_type_id], [Image], [Provider_id], [Created_at], [Created_by], [Updated_at], [Updated_by], [Sold]) VALUES (75, N'British M Regent Classic Pomade', N'Là dòng sáp pomade gốc nước. Là sản phẩm dành cho các quý ông sang trọng và lịch lãm', 599000, 100, 8, N'/images/0fbf247c-8093-432d-923e-513905c34a8b.jpg', 8, CAST(N'2023-12-22 17:00:38.183' AS DateTime), 6, CAST(N'2023-12-22 23:34:27.250' AS DateTime), 6, 0)
INSERT [dbo].[PRODUCT] ([Product_id], [Name], [Description], [Price], [Quantity], [Product_type_id], [Image], [Provider_id], [Created_at], [Created_by], [Updated_at], [Updated_by], [Sold]) VALUES (83, N' Vilain Dynamite Clay', N' Vilain Dynamite Clay là sản phẩm sáp tạo kiểu có công thức kết hợp điểm mạnh của By Vilain với đất sét Clay', 440000, 100, 8, N'/images/2038751e-7d66-4fc6-a25c-d8cb35c43002.webp', 8, CAST(N'2023-12-22 17:06:44.357' AS DateTime), 6, CAST(N'2023-12-22 23:34:53.683' AS DateTime), 6, 0)
INSERT [dbo].[PRODUCT] ([Product_id], [Name], [Description], [Price], [Quantity], [Product_type_id], [Image], [Provider_id], [Created_at], [Created_by], [Updated_at], [Updated_by], [Sold]) VALUES (85, N' Apestomen Vocanic Clay', N'Sáp Volcanic Clay được đánh giá là một sản phẩm sáp vuốt tóc chất lượng với mức giá phải chăng, phù hợp với hầu hết các bạn học sinh, sinh viên cho đến các bạn văn phòng', 280000, 100, 8, N'/images/000756a0-2be2-453d-9b96-e48685c5de7f.png', 9, CAST(N'2023-12-22 17:08:14.720' AS DateTime), 6, CAST(N'2023-12-22 23:34:45.803' AS DateTime), 6, 0)
INSERT [dbo].[PRODUCT] ([Product_id], [Name], [Description], [Price], [Quantity], [Product_type_id], [Image], [Provider_id], [Created_at], [Created_by], [Updated_at], [Updated_by], [Sold]) VALUES (86, N'18.21 Man Made Paste', N'18.21 Man Made Paste là sản phẩm ra mắt sau 2 sản phẩm đầu tiên là Wax và Clay. Paste mang độ giữ nếp vừa phải, không quá khô. Hoàn thiện tự nhiên giúp cho mái tóc trông có sức sống hơn rất nhiều sau khi sử dụng.', 530000, 100, 8, N'/images/a9d36a44-fa93-414e-b566-eb372a11f913.jpg', 8, CAST(N'2023-12-22 17:09:59.553' AS DateTime), 6, CAST(N'2023-12-22 23:34:59.710' AS DateTime), 6, 0)
INSERT [dbo].[PRODUCT] ([Product_id], [Name], [Description], [Price], [Quantity], [Product_type_id], [Image], [Provider_id], [Created_at], [Created_by], [Updated_at], [Updated_by], [Sold]) VALUES (87, N'Kevin Murphy Session Spray', N'Gôm Kevin Murphy Session.Spray là một dòng gôm cao cấp, mang trong mình những thành phần tự nhiên tốt nhất cho tóc, không để lại vẩy trắng trên tóc, không nặng tóc, giữ nếp tối đa cho tóc mà không làm mất đi vẻ tự nhiên, linh hoạt của tóc', 690000, 80, 8, N'/images/4edfe135-34a5-458c-a34a-0322c242719f.jpg', 9, CAST(N'2023-12-22 17:11:54.393' AS DateTime), 6, CAST(N'2023-12-22 23:33:56.580' AS DateTime), 6, 0)
INSERT [dbo].[PRODUCT] ([Product_id], [Name], [Description], [Price], [Quantity], [Product_type_id], [Image], [Provider_id], [Created_at], [Created_by], [Updated_at], [Updated_by], [Sold]) VALUES (88, N'La Roche-Posay Effaclar 400ml', N'Làm dịu da và giảm kích ứng. Điều tiết lượng dầu tiết ra trên da, từ đó kiểm soát bóng dầu và bã nhờn dư thừa hiệu quả, giảm hình thành mụn đầu đen. Giúp củng cố hàng rào bảo vệ da, không gây cảm giác khô căng, khó chịu.', 450000, 100, 4, N'/images/6a655961-67d9-4206-8d16-0341c844f019.jpg', 8, CAST(N'2023-12-22 17:15:16.240' AS DateTime), 6, CAST(N'2023-12-22 23:33:10.933' AS DateTime), 6, 0)
INSERT [dbo].[PRODUCT] ([Product_id], [Name], [Description], [Price], [Quantity], [Product_type_id], [Image], [Provider_id], [Created_at], [Created_by], [Updated_at], [Updated_by], [Sold]) VALUES (89, N'Xịt khoáng Vichy 300ml', N'Nước Xịt Khoáng Dưỡng Da Vichy Thermale có nguồn gốc từ thiên nhiên và không thể sản xuất nhân tạo.', 310000, 100, 4, N'/images/06ab79c6-5744-4860-99ed-b0396d40dfb3.jpg', 8, CAST(N'2023-12-22 17:16:17.767' AS DateTime), 6, CAST(N'2023-12-22 23:33:02.247' AS DateTime), 6, 0)
INSERT [dbo].[PRODUCT] ([Product_id], [Name], [Description], [Price], [Quantity], [Product_type_id], [Image], [Provider_id], [Created_at], [Created_by], [Updated_at], [Updated_by], [Sold]) VALUES (90, N'Innisfree Volcanic Pore Cleansing Foam Ex', N'Sữa rửa mặt innisfree Volcanic Pore Cleansing Foam EX chứa tinh chất chiết xuất từ các hạt tro núi lửa đảo Jeju có khả năng hút bã nhờn sâu bên trong lỗ chân lông và các tạp chất khác có trên da ngăn ngừa sự hình thành mụn', 200000, 98, 4, N'/images/32fb0be3-52d8-4136-b5c5-e6ff1bea645f.jpg', 8, CAST(N'2023-12-22 17:17:48.383' AS DateTime), 6, CAST(N'2023-12-22 23:32:53.603' AS DateTime), 6, 0)
INSERT [dbo].[PRODUCT] ([Product_id], [Name], [Description], [Price], [Quantity], [Product_type_id], [Image], [Provider_id], [Created_at], [Created_by], [Updated_at], [Updated_by], [Sold]) VALUES (91, N'Sữa rửa mặt Acne Aid ', N'Sữa rửa mặt Acne-Aid Liquid Cleanser chứa các hoạt chất làm sạch có nguồn gốc từ thực vật, thân thiện với làn da nhạy cảm, dầu mụn đem lại hiệu quả trong việc làm sạch và bảo vệ da.', 135000, 99, 4, N'/images/ca57d64c-ec78-4dd3-ae89-16db153b18ba.jpg', 8, CAST(N'2023-12-22 17:18:40.710' AS DateTime), 6, CAST(N'2023-12-22 23:32:40.483' AS DateTime), 6, 0)
INSERT [dbo].[PRODUCT] ([Product_id], [Name], [Description], [Price], [Quantity], [Product_type_id], [Image], [Provider_id], [Created_at], [Created_by], [Updated_at], [Updated_by], [Sold]) VALUES (92, N'Kem rửa mặt CM24', N'Giúp làm sạch, loại bỏ dầu thừa, dưỡng ẩm và đem lại cảm giác sảng khoái cho làn da', 109000, 100, 4, N'/images/d4d7dc7d-8dd5-458e-896a-52241cd48d83.jpg', 8, CAST(N'2023-12-22 17:30:07.387' AS DateTime), 6, CAST(N'2023-12-22 23:32:31.880' AS DateTime), 6, 0)
INSERT [dbo].[PRODUCT] ([Product_id], [Name], [Description], [Price], [Quantity], [Product_type_id], [Image], [Provider_id], [Created_at], [Created_by], [Updated_at], [Updated_by], [Sold]) VALUES (93, N'Sữa Rửa Mặt Kiehl''s Oil Eliminator Deep Cleansing Exfoliating Face Wash For Men', N'Sữa rửa mặt Kiehl''s Oil Eliminator Deep Cleansing Exfoliating Face Wash For Men có tác dụng làm sạch tế bào da chết, làm sạch bụi bẩn, dầu thừa và giúp thu nhỏ lỗ chân lông', 750000, 100, 4, N'/images/4bb150a6-2ba0-473d-a35d-f28a61ec6615.jpg', 8, CAST(N'2023-12-22 18:09:30.000' AS DateTime), 6, CAST(N'2023-12-22 23:32:22.750' AS DateTime), 6, 0)
INSERT [dbo].[PRODUCT] ([Product_id], [Name], [Description], [Price], [Quantity], [Product_type_id], [Image], [Provider_id], [Created_at], [Created_by], [Updated_at], [Updated_by], [Sold]) VALUES (94, N'Lab Series Oil Control Clearing Water Lotion', N'Lab Series Oil Control Clearing Water Lotion là một loại nước hoa hồng dành cho nam giới có làn da nhờn, dễ nổi mụn. Công thức hai pha giúp loại bỏ dầu thừa và bụi bẩn còn sót lại để có làn da sạch hơn', 1030000, 99, 4, N'/images/c24b243e-a935-4d1f-9e71-867c30615c54.jpg', 8, CAST(N'2023-12-22 18:17:44.747' AS DateTime), 6, CAST(N'2023-12-22 23:31:50.133' AS DateTime), 6, 0)
INSERT [dbo].[PRODUCT] ([Product_id], [Name], [Description], [Price], [Quantity], [Product_type_id], [Image], [Provider_id], [Created_at], [Created_by], [Updated_at], [Updated_by], [Sold]) VALUES (96, N'Medel Natural Shampoo Herbal Garden Aroma', N'Dầu gội thảo dược Medel Natural Shampoo Herbal Garden Aroma được làm hoàn toàn từ những thành phần thảo dược tự nhiên nên an toàn và lành tính cho tóc. Nuôi dưỡng tóc óng mượt, bóng khỏe từ gốc đến ngọn.', 290000, 100, 5, N'/images/7557f0dc-880f-4a45-9172-c0654784bd7d.webp', 8, CAST(N'2023-12-22 18:22:56.337' AS DateTime), 6, CAST(N'2023-12-22 23:29:46.650' AS DateTime), 6, 0)
INSERT [dbo].[PRODUCT] ([Product_id], [Name], [Description], [Price], [Quantity], [Product_type_id], [Image], [Provider_id], [Created_at], [Created_by], [Updated_at], [Updated_by], [Sold]) VALUES (97, N'Ryo Scalp Deep Cleanser Shampoo', N'Loại bỏ cặn bẩn đồng thời dưỡng ẩm cho tóc bằng công thức bạc hà lên men, mà không làm cho nó cảm thấy bị tước hoặc khô', 2600000, 100, 5, N'/images/82d77a3b-9f0e-48ca-b316-1c42e37501f7.webp', 8, CAST(N'2023-12-22 18:27:05.113' AS DateTime), 6, CAST(N'2023-12-22 23:31:35.093' AS DateTime), 6, 0)
INSERT [dbo].[PRODUCT] ([Product_id], [Name], [Description], [Price], [Quantity], [Product_type_id], [Image], [Provider_id], [Created_at], [Created_by], [Updated_at], [Updated_by], [Sold]) VALUES (99, N'Stanhome Repair Shampoo', N'Dầu gội không xà phòng Repair Shampoo giúp nuôi dưỡng, phục hồi cho tóc khô xơ chẻ ngọn, tóc uốn duỗi nhuộm, tóc hư tổn, giảm tình trạng rụng tóc. Giúp tóc suôn mượt hơn và bóng khỏe hơn. Sản xuất và lưu hành nội địa tại Pháp', 252000, 100, 5, N'/images/dbee1f10-673a-4f9d-a273-3f656cb96744.jpg', 8, CAST(N'2023-12-22 18:32:33.367' AS DateTime), 6, CAST(N'2023-12-22 23:31:21.657' AS DateTime), 6, 0)
INSERT [dbo].[PRODUCT] ([Product_id], [Name], [Description], [Price], [Quantity], [Product_type_id], [Image], [Provider_id], [Created_at], [Created_by], [Updated_at], [Updated_by], [Sold]) VALUES (100, N'OXY 240g Prime Anti-aging Hair Shampoo', N'OXY Prime Anti-aging Hair Shampoo với thành phần chiết xuất từ thiên nhiên kết hợp với các dưỡng chất có lợi cho tóc sẽ giúp nhẹ nhàng làm sạch tóc và da đầu, giúp giảm lượng tóc rụng, giúp tóc phát triển nhanh, chắc khỏe', 142000, 100, 5, N'/images/1b8abdb6-2274-4354-84c6-d17acb112167.jpeg', 8, CAST(N'2023-12-22 18:37:29.037' AS DateTime), 6, CAST(N'2023-12-22 23:30:46.427' AS DateTime), 6, 0)
INSERT [dbo].[PRODUCT] ([Product_id], [Name], [Description], [Price], [Quantity], [Product_type_id], [Image], [Provider_id], [Created_at], [Created_by], [Updated_at], [Updated_by], [Sold]) VALUES (101, N'Xịt tóc dược liệu Thái Dương', N'Sản phẩm có công dụng dưỡng tóc mọc và phát triển tốt, cung cấp cho tóc các dưỡng chất giúp bạn có một mái tóc khỏe mạnh và sạch gàu, bóng mượt.', 135000, 99, 5, N'/images/a911ffae-a3ef-4d80-9423-6d022e2f8d6f.jpg', 8, CAST(N'2023-12-22 18:39:32.843' AS DateTime), 6, CAST(N'2023-12-22 23:30:31.310' AS DateTime), 6, 0)
INSERT [dbo].[PRODUCT] ([Product_id], [Name], [Description], [Price], [Quantity], [Product_type_id], [Image], [Provider_id], [Created_at], [Created_by], [Updated_at], [Updated_by], [Sold]) VALUES (102, N'Native''s Coconut & Vanilla', N'Silicone miễn phí. Không chứa sulfat. Paraben miễn phí. Được làm chu đáo với 10 thành phần hoặc ít hơn. Được làm bằng (trái tim). An toàn. Giản dị. Hiệu quả. Được làm từ 10 thành phần chu đáo cho phép bạn nói lời tạm biệt với khô', 2000000, 100, 5, N'/images/6efc3968-cfbd-4c89-b3c8-ee409ceba84f.jpg', 8, CAST(N'2023-12-22 18:44:16.047' AS DateTime), 6, CAST(N'2023-12-22 23:30:23.753' AS DateTime), 6, 0)
INSERT [dbo].[PRODUCT] ([Product_id], [Name], [Description], [Price], [Quantity], [Product_type_id], [Image], [Provider_id], [Created_at], [Created_by], [Updated_at], [Updated_by], [Sold]) VALUES (103, N'SACHAJUAN', N'Được thiết kế để dưỡng ẩm và dưỡng tóc màu - Bao gồm công nghệ Microemulsion - Làm giàu với các bộ lọc UV lâu dài để che chắn khỏi phai màu- Nuôi dưỡng, điều kiện và gỡ rối tóc', 2000000, 100, 5, N'/images/6a82c1cc-5021-45ca-a4ef-ae4f5b97e627.jpg', 8, CAST(N'2023-12-22 18:49:02.313' AS DateTime), 6, CAST(N'2023-12-22 23:30:13.843' AS DateTime), 6, 0)
SET IDENTITY_INSERT [dbo].[PRODUCT] OFF
SET IDENTITY_INSERT [dbo].[PRODUCTTYPE] ON 

INSERT [dbo].[PRODUCTTYPE] ([Product_type_id], [Name]) VALUES (4, N'CHĂM SÓC DA MẶT')
INSERT [dbo].[PRODUCTTYPE] ([Product_type_id], [Name]) VALUES (5, N'CHĂM SÓC TÓC')
INSERT [dbo].[PRODUCTTYPE] ([Product_type_id], [Name]) VALUES (8, N'TẠO KIỂU TÓC')
SET IDENTITY_INSERT [dbo].[PRODUCTTYPE] OFF
SET IDENTITY_INSERT [dbo].[PROVIDER] ON 

INSERT [dbo].[PROVIDER] ([Provider_id], [Name], [Address], [Phone], [Email]) VALUES (8, N'Dược phẩm Kim Anh', N'Quận 8', N'0903555605', N'KimAnh123@gmail.com')
INSERT [dbo].[PROVIDER] ([Provider_id], [Name], [Address], [Phone], [Email]) VALUES (9, N'One Boutique', N'33 Man Thiện, Hiệp Phú, Quận 8, Tp.Hồ Chí Minh', N'0909094104', N'OneBouutique1@gmail.com')
SET IDENTITY_INSERT [dbo].[PROVIDER] OFF
SET IDENTITY_INSERT [dbo].[ROLE] ON 

INSERT [dbo].[ROLE] ([Role_id], [Name]) VALUES (1, N'Admin')
INSERT [dbo].[ROLE] ([Role_id], [Name]) VALUES (2, N'Staff')
INSERT [dbo].[ROLE] ([Role_id], [Name]) VALUES (3, N'Client')
SET IDENTITY_INSERT [dbo].[ROLE] OFF
SET IDENTITY_INSERT [dbo].[SCHEDULE] ON 

INSERT [dbo].[SCHEDULE] ([Schedule_id], [Time]) VALUES (1, CAST(N'07:30:00' AS Time))
INSERT [dbo].[SCHEDULE] ([Schedule_id], [Time]) VALUES (4, CAST(N'08:00:00' AS Time))
INSERT [dbo].[SCHEDULE] ([Schedule_id], [Time]) VALUES (5, CAST(N'08:30:00' AS Time))
INSERT [dbo].[SCHEDULE] ([Schedule_id], [Time]) VALUES (6, CAST(N'09:00:00' AS Time))
INSERT [dbo].[SCHEDULE] ([Schedule_id], [Time]) VALUES (7, CAST(N'09:30:00' AS Time))
INSERT [dbo].[SCHEDULE] ([Schedule_id], [Time]) VALUES (8, CAST(N'10:00:00' AS Time))
INSERT [dbo].[SCHEDULE] ([Schedule_id], [Time]) VALUES (13, CAST(N'10:30:00' AS Time))
INSERT [dbo].[SCHEDULE] ([Schedule_id], [Time]) VALUES (14, CAST(N'11:00:00' AS Time))
INSERT [dbo].[SCHEDULE] ([Schedule_id], [Time]) VALUES (16, CAST(N'12:00:00' AS Time))
INSERT [dbo].[SCHEDULE] ([Schedule_id], [Time]) VALUES (18, CAST(N'13:00:00' AS Time))
SET IDENTITY_INSERT [dbo].[SCHEDULE] OFF
INSERT [dbo].[SCHEDULEDETAIL] ([Schedule_id], [Staff_id], [Date], [Status]) VALUES (1, 6, CAST(N'2023-12-24' AS Date), 1)
INSERT [dbo].[SCHEDULEDETAIL] ([Schedule_id], [Staff_id], [Date], [Status]) VALUES (6, 9, CAST(N'2023-12-27' AS Date), 1)
SET IDENTITY_INSERT [dbo].[SERVICE] ON 

INSERT [dbo].[SERVICE] ([Service_id], [Name], [Price], [Status], [Service_type_id], [Created_at], [Created_by], [Updated_at], [Updated_by]) VALUES (8, N'Ráy tai', 30000, 1, 1, CAST(N'2023-12-07 13:33:51.590' AS DateTime), 6, CAST(N'2023-12-16 10:45:18.833' AS DateTime), 6)
INSERT [dbo].[SERVICE] ([Service_id], [Name], [Price], [Status], [Service_type_id], [Created_at], [Created_by], [Updated_at], [Updated_by]) VALUES (9, N'Gội đầu', 70000, 1, 7, CAST(N'2023-12-07 13:34:46.853' AS DateTime), 6, CAST(N'2023-12-16 10:44:50.360' AS DateTime), 6)
INSERT [dbo].[SERVICE] ([Service_id], [Name], [Price], [Status], [Service_type_id], [Created_at], [Created_by], [Updated_at], [Updated_by]) VALUES (10, N'Mụn', 50000, 1, 8, CAST(N'2023-12-16 10:40:19.267' AS DateTime), 6, CAST(N'2023-12-16 10:43:17.997' AS DateTime), 6)
SET IDENTITY_INSERT [dbo].[SERVICE] OFF
SET IDENTITY_INSERT [dbo].[SERVICETYPE] ON 

INSERT [dbo].[SERVICETYPE] ([Service_type_id], [Name]) VALUES (1, N'Ráy tai')
INSERT [dbo].[SERVICETYPE] ([Service_type_id], [Name]) VALUES (7, N'Gội Đầu')
INSERT [dbo].[SERVICETYPE] ([Service_type_id], [Name]) VALUES (8, N'Nặn Mụn')
INSERT [dbo].[SERVICETYPE] ([Service_type_id], [Name]) VALUES (9, N'Cắt tóc')
SET IDENTITY_INSERT [dbo].[SERVICETYPE] OFF
SET IDENTITY_INSERT [dbo].[STAFF] ON 

INSERT [dbo].[STAFF] ([Staff_id], [Name], [Username], [Password], [Phone], [Avatar], [Address], [Email], [Status], [IsDisabled], [Role_id], [Created_at], [Updated_at], [Created_by], [Updated_by], [Branch_id]) VALUES (6, N'Khánh', N'Khanhpeo123@', N'AQAAAAEAACcQAAAAEERBd+yXzjp49sFUevULrCVCC5J82DFyp9x8BknP6MH1dWwyJc7gYleJdEDmhSfCtA==', N'0383581802', N'/images/background.png', N'25/5d Nguyen Thi Thanh', N'huynhhiepvan1998@gmail.com', 1, 0, 1, CAST(N'2023-08-28 09:47:51.037' AS DateTime), CAST(N'2023-12-22 23:44:10.933' AS DateTime), N'Hiep', N'Khánh', 3)
INSERT [dbo].[STAFF] ([Staff_id], [Name], [Username], [Password], [Phone], [Avatar], [Address], [Email], [Status], [IsDisabled], [Role_id], [Created_at], [Updated_at], [Created_by], [Updated_by], [Branch_id]) VALUES (9, N'Trọng Quý', N'TrongQuy111', N'AQAAAAEAACcQAAAAEA1mrQDRBxTj6Cllat/d/x9/JmUmpIgthmc9MBwbhQlLZSsemXHvaOVX4CTrUtxceA==', N'0334567891', N'/images/gom-kevin-murphy.jpg', N'Trần Hưng Đạo', N'huynhphatminh1998@gmail.com', 1, 0, 2, CAST(N'2023-12-16 09:00:15.970' AS DateTime), CAST(N'2023-12-24 08:43:15.113' AS DateTime), N'Hiep', N'Khánh', 1)
INSERT [dbo].[STAFF] ([Staff_id], [Name], [Username], [Password], [Phone], [Avatar], [Address], [Email], [Status], [IsDisabled], [Role_id], [Created_at], [Updated_at], [Created_by], [Updated_by], [Branch_id]) VALUES (10, N'Tạ Lý Minh', N'MinhTa@', N'AQAAAAEAACcQAAAAEAhz9rzgcidjzTvkQeBTcZ7gei1xCyMiX+0mWxgHbOSTJUl63WsSdzjy9OrmeBY+qQ==', N'0537777984', N'/images/deav.png', N'Lê Hồng Phong', N'khanhanvo9@gmail.com', 1, 0, 2, CAST(N'2023-12-22 19:42:25.930' AS DateTime), CAST(N'2023-12-22 19:43:12.503' AS DateTime), N'Khánh', N'Khánh', 5)
INSERT [dbo].[STAFF] ([Staff_id], [Name], [Username], [Password], [Phone], [Avatar], [Address], [Email], [Status], [IsDisabled], [Role_id], [Created_at], [Updated_at], [Created_by], [Updated_by], [Branch_id]) VALUES (11, N'Nguyễn kiệt', N'nguyenkiet123', N'AQAAAAEAACcQAAAAEOibPSvH7Jl6nfhHFcAEaqxNnhRQwIgI/kUkMGyw/g42yjy279SSj0NphOeYomOvmQ==', N'0903555609', NULL, N'quận 5', N'huynhphatminh1998@gmail.com', 1, 0, 2, CAST(N'2023-12-24 06:39:13.077' AS DateTime), NULL, N'Khánh', NULL, 1)
SET IDENTITY_INSERT [dbo].[STAFF] OFF
ALTER TABLE [dbo].[BILL]  WITH CHECK ADD  CONSTRAINT [FK_BILL_CILENT] FOREIGN KEY([Client_id])
REFERENCES [dbo].[CLIENT] ([Client_id])
GO
ALTER TABLE [dbo].[BILL] CHECK CONSTRAINT [FK_BILL_CILENT]
GO
ALTER TABLE [dbo].[BILLDETAIL]  WITH CHECK ADD  CONSTRAINT [FK_BILLDETAIL_BILL] FOREIGN KEY([Bill_id])
REFERENCES [dbo].[BILL] ([Bill_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BILLDETAIL] CHECK CONSTRAINT [FK_BILLDETAIL_BILL]
GO
ALTER TABLE [dbo].[BILLDETAIL]  WITH CHECK ADD  CONSTRAINT [FK_BILLDETAIL_PRODUCT1] FOREIGN KEY([Product_id])
REFERENCES [dbo].[PRODUCT] ([Product_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BILLDETAIL] CHECK CONSTRAINT [FK_BILLDETAIL_PRODUCT1]
GO
ALTER TABLE [dbo].[BLOG_POSTS]  WITH CHECK ADD  CONSTRAINT [FK_BLOG_POSTS_BLOG_CATEGORIES] FOREIGN KEY([Blog_category_id])
REFERENCES [dbo].[BLOG_CATEGORIES] ([Blog_category_id])
GO
ALTER TABLE [dbo].[BLOG_POSTS] CHECK CONSTRAINT [FK_BLOG_POSTS_BLOG_CATEGORIES]
GO
ALTER TABLE [dbo].[BLOG_POSTS]  WITH CHECK ADD  CONSTRAINT [FK_BLOG_POSTS_STAFF] FOREIGN KEY([Staff_id])
REFERENCES [dbo].[STAFF] ([Staff_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BLOG_POSTS] CHECK CONSTRAINT [FK_BLOG_POSTS_STAFF]
GO
ALTER TABLE [dbo].[BOOKING]  WITH CHECK ADD  CONSTRAINT [FK_BOOKING_BRANCH] FOREIGN KEY([Branch_id])
REFERENCES [dbo].[BRANCH] ([Branch_id])
GO
ALTER TABLE [dbo].[BOOKING] CHECK CONSTRAINT [FK_BOOKING_BRANCH]
GO
ALTER TABLE [dbo].[BOOKING]  WITH CHECK ADD  CONSTRAINT [FK_BOOKING_CILENT] FOREIGN KEY([Client_id])
REFERENCES [dbo].[CLIENT] ([Client_id])
GO
ALTER TABLE [dbo].[BOOKING] CHECK CONSTRAINT [FK_BOOKING_CILENT]
GO
ALTER TABLE [dbo].[BOOKING]  WITH CHECK ADD  CONSTRAINT [FK_BOOKING_COMBO] FOREIGN KEY([Combo_id])
REFERENCES [dbo].[COMBO] ([Combo_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BOOKING] CHECK CONSTRAINT [FK_BOOKING_COMBO]
GO
ALTER TABLE [dbo].[BOOKING]  WITH CHECK ADD  CONSTRAINT [FK_BOOKING_STAFF] FOREIGN KEY([Staff_id])
REFERENCES [dbo].[STAFF] ([Staff_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BOOKING] CHECK CONSTRAINT [FK_BOOKING_STAFF]
GO
ALTER TABLE [dbo].[BOOKINGDETAIL]  WITH CHECK ADD  CONSTRAINT [FK_BOOKINGDETAIL_BOOKING] FOREIGN KEY([Booking_id])
REFERENCES [dbo].[BOOKING] ([Booking_id])
GO
ALTER TABLE [dbo].[BOOKINGDETAIL] CHECK CONSTRAINT [FK_BOOKINGDETAIL_BOOKING]
GO
ALTER TABLE [dbo].[BOOKINGDETAIL]  WITH CHECK ADD  CONSTRAINT [FK_BOOKINGDETAIL_SERVICE] FOREIGN KEY([Service_id])
REFERENCES [dbo].[SERVICE] ([Service_id])
GO
ALTER TABLE [dbo].[BOOKINGDETAIL] CHECK CONSTRAINT [FK_BOOKINGDETAIL_SERVICE]
GO
ALTER TABLE [dbo].[CART]  WITH CHECK ADD  CONSTRAINT [FK_CART_CILENT] FOREIGN KEY([user_id])
REFERENCES [dbo].[CLIENT] ([Client_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CART] CHECK CONSTRAINT [FK_CART_CILENT]
GO
ALTER TABLE [dbo].[CART]  WITH CHECK ADD  CONSTRAINT [FK_CART_PRODUCT] FOREIGN KEY([Product_id])
REFERENCES [dbo].[PRODUCT] ([Product_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CART] CHECK CONSTRAINT [FK_CART_PRODUCT]
GO
ALTER TABLE [dbo].[CLIENT]  WITH CHECK ADD  CONSTRAINT [FK_CILENT_ROLE] FOREIGN KEY([Role_id])
REFERENCES [dbo].[ROLE] ([Role_id])
GO
ALTER TABLE [dbo].[CLIENT] CHECK CONSTRAINT [FK_CILENT_ROLE]
GO
ALTER TABLE [dbo].[COMBODETAIL]  WITH CHECK ADD  CONSTRAINT [FK_COMBODETAIL_COMBO] FOREIGN KEY([Combo_id])
REFERENCES [dbo].[COMBO] ([Combo_id])
GO
ALTER TABLE [dbo].[COMBODETAIL] CHECK CONSTRAINT [FK_COMBODETAIL_COMBO]
GO
ALTER TABLE [dbo].[COMBODETAIL]  WITH CHECK ADD  CONSTRAINT [FK_COMBODETAIL_SERVICE] FOREIGN KEY([Service_id])
REFERENCES [dbo].[SERVICE] ([Service_id])
GO
ALTER TABLE [dbo].[COMBODETAIL] CHECK CONSTRAINT [FK_COMBODETAIL_SERVICE]
GO
ALTER TABLE [dbo].[PRODUCT]  WITH CHECK ADD  CONSTRAINT [FK_PRODUCT_PRODUCTTYPE] FOREIGN KEY([Product_type_id])
REFERENCES [dbo].[PRODUCTTYPE] ([Product_type_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PRODUCT] CHECK CONSTRAINT [FK_PRODUCT_PRODUCTTYPE]
GO
ALTER TABLE [dbo].[PRODUCT]  WITH CHECK ADD  CONSTRAINT [FK_PRODUCT_PROVIDER] FOREIGN KEY([Provider_id])
REFERENCES [dbo].[PROVIDER] ([Provider_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PRODUCT] CHECK CONSTRAINT [FK_PRODUCT_PROVIDER]
GO
ALTER TABLE [dbo].[SCHEDULEDETAIL]  WITH CHECK ADD  CONSTRAINT [FK_SCHEDULEDETAIL_SCHEDULE] FOREIGN KEY([Schedule_id])
REFERENCES [dbo].[SCHEDULE] ([Schedule_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SCHEDULEDETAIL] CHECK CONSTRAINT [FK_SCHEDULEDETAIL_SCHEDULE]
GO
ALTER TABLE [dbo].[SCHEDULEDETAIL]  WITH CHECK ADD  CONSTRAINT [FK_SCHEDULEDETAIL_STAFF1] FOREIGN KEY([Staff_id])
REFERENCES [dbo].[STAFF] ([Staff_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SCHEDULEDETAIL] CHECK CONSTRAINT [FK_SCHEDULEDETAIL_STAFF1]
GO
ALTER TABLE [dbo].[SERVICE]  WITH CHECK ADD  CONSTRAINT [FK_SERVICE_ServiceType] FOREIGN KEY([Service_type_id])
REFERENCES [dbo].[SERVICETYPE] ([Service_type_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SERVICE] CHECK CONSTRAINT [FK_SERVICE_ServiceType]
GO
ALTER TABLE [dbo].[STAFF]  WITH CHECK ADD  CONSTRAINT [FK_STAFF_BRANCH] FOREIGN KEY([Branch_id])
REFERENCES [dbo].[BRANCH] ([Branch_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[STAFF] CHECK CONSTRAINT [FK_STAFF_BRANCH]
GO
ALTER TABLE [dbo].[STAFF]  WITH CHECK ADD  CONSTRAINT [FK_STAFF_ROLE] FOREIGN KEY([Role_id])
REFERENCES [dbo].[ROLE] ([Role_id])
GO
ALTER TABLE [dbo].[STAFF] CHECK CONSTRAINT [FK_STAFF_ROLE]
GO
USE [master]
GO
ALTER DATABASE [DLCT] SET  READ_WRITE 
GO

