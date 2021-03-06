USE [master]
GO
/****** Object:  Database [CVs]    Script Date: 06/24/2011 15:11:28 ******/
CREATE DATABASE [CVs] 
GO
EXEC dbo.sp_dbcmptlevel @dbname=N'CVs', @new_cmptlevel=90
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CVs].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CVs] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [CVs] SET ANSI_NULLS OFF
GO
ALTER DATABASE [CVs] SET ANSI_PADDING OFF
GO
ALTER DATABASE [CVs] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [CVs] SET ARITHABORT OFF
GO
ALTER DATABASE [CVs] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [CVs] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [CVs] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [CVs] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [CVs] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [CVs] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [CVs] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [CVs] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [CVs] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [CVs] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [CVs] SET  DISABLE_BROKER
GO
ALTER DATABASE [CVs] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [CVs] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [CVs] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [CVs] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [CVs] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [CVs] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [CVs] SET  READ_WRITE
GO
ALTER DATABASE [CVs] SET RECOVERY SIMPLE
GO
ALTER DATABASE [CVs] SET  MULTI_USER
GO
ALTER DATABASE [CVs] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [CVs] SET DB_CHAINING OFF
GO
USE [CVs]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 06/24/2011 15:11:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[firstName] [nvarchar](100) NOT NULL,
	[lastName] [nvarchar](100) NOT NULL,
	[email] [nvarchar](300) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Users] ON
INSERT [dbo].[Users] ([ID], [firstName], [lastName], [email]) VALUES (7, N'Veselina', N'Smith', N'vesela@random.com')
INSERT [dbo].[Users] ([ID], [firstName], [lastName], [email]) VALUES (8, N'Georgi', N'Kolev', N'g_kolev@abv.bg')
SET IDENTITY_INSERT [dbo].[Users] OFF
/****** Object:  Table [dbo].[CVs]    Script Date: 06/24/2011 15:11:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CVs](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserID] [bigint] NOT NULL,
	[born] [int] NOT NULL,
	[nationality] [nvarchar](100) NOT NULL,
	[liveIncity] [nvarchar](100) NOT NULL,
	[details] [nvarchar](max) NULL,
	[dateAdded] [datetime] NOT NULL,
	[description] [nvarchar](200) NOT NULL,
	[gender] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_CVs] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[CVs] ON
INSERT [dbo].[CVs] ([ID], [UserID], [born], [nationality], [liveIncity], [details], [dateAdded], [description], [gender]) VALUES (9, 7, 1985, N'Bulgarian', N'Sofia', N'', CAST(0x00009F0C00C7D927 AS DateTime), N'English variant', N'female')
INSERT [dbo].[CVs] ([ID], [UserID], [born], [nationality], [liveIncity], [details], [dateAdded], [description], [gender]) VALUES (10, 8, 1988, N'Bulgarian', N'Veliko Turnovo', N'Idea and full development of : www.wiadvice.com
    Other developments, which can be demonstrated :
    - Gallery site (ASP.NET) for diploma thesis (2-3 weeks);
    - Short FLASH movie intro of animation serial &quot;South Park&quot;;
    - FLASH encyclopedia for animals, also for diploma thesis;
    Development tools used (mostly for course projects): JBuilder , Visual Studio 2008/2010 , Eclipse , Dreamweaver , Visual Studio 6, Adobe Flash', CAST(0x00009F0C00C7EAA8 AS DateTime), N'English variant', N'male')
INSERT [dbo].[CVs] ([ID], [UserID], [born], [nationality], [liveIncity], [details], [dateAdded], [description], [gender]) VALUES (11, 8, 1988, N'Българин', N'Велико Търново', N'Идея и пълна разработка на : www.wiadvice.com
    Други разработки, които могат да се покажат :
    - Сайт галерия (ASP.NET) разработен за дипломна работа (2-3 седмици);
    - Кратко FLASH интро на анимационния сериал &quot;South Park&quot;;
    - FLASH енциклопедия за животни, също разработена за дипломна работа;
    Използвани инструменти за разработка: JBuilder , Visual Studio 2008/2010 , Eclipse , Dreamweaver , Visual Studio 6, Adobe Flash', CAST(0x00009F0C00C7F0E4 AS DateTime), N'Bulgarian variant', N'male')
SET IDENTITY_INSERT [dbo].[CVs] OFF
/****** Object:  Table [dbo].[Subjects]    Script Date: 06/24/2011 15:11:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subjects](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[EduID] [bigint] NOT NULL,
	[subject] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_Subjects] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Subjects] ON
INSERT [dbo].[Subjects] ([ID], [EduID], [subject]) VALUES (60, 13, N'English')
INSERT [dbo].[Subjects] ([ID], [EduID], [subject]) VALUES (61, 13, N'C++ programming')
INSERT [dbo].[Subjects] ([ID], [EduID], [subject]) VALUES (62, 13, N'Visual programming')
INSERT [dbo].[Subjects] ([ID], [EduID], [subject]) VALUES (63, 13, N'Computer networks')
INSERT [dbo].[Subjects] ([ID], [EduID], [subject]) VALUES (64, 13, N'JAVA programming')
INSERT [dbo].[Subjects] ([ID], [EduID], [subject]) VALUES (65, 13, N'Data bases')
INSERT [dbo].[Subjects] ([ID], [EduID], [subject]) VALUES (66, 13, N'HTML,CSS,XML,JAVA servlets')
INSERT [dbo].[Subjects] ([ID], [EduID], [subject]) VALUES (67, 13, N'FLASH')
INSERT [dbo].[Subjects] ([ID], [EduID], [subject]) VALUES (68, 14, N'Английски език')
INSERT [dbo].[Subjects] ([ID], [EduID], [subject]) VALUES (69, 14, N'Програмиране на C++')
INSERT [dbo].[Subjects] ([ID], [EduID], [subject]) VALUES (70, 14, N'Визуално програмиране')
INSERT [dbo].[Subjects] ([ID], [EduID], [subject]) VALUES (71, 14, N'Компютърни мрежи')
INSERT [dbo].[Subjects] ([ID], [EduID], [subject]) VALUES (72, 14, N'Програмиране на JAVA')
INSERT [dbo].[Subjects] ([ID], [EduID], [subject]) VALUES (73, 14, N'Бази от данни')
INSERT [dbo].[Subjects] ([ID], [EduID], [subject]) VALUES (74, 14, N'HTML,CSS,XML,JAVA servlets')
INSERT [dbo].[Subjects] ([ID], [EduID], [subject]) VALUES (75, 14, N'FLASH')
SET IDENTITY_INSERT [dbo].[Subjects] OFF
/****** Object:  Table [dbo].[Education]    Script Date: 06/24/2011 15:11:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Education](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[CvID] [bigint] NOT NULL,
	[fromDate] [datetime] NOT NULL,
	[toDate] [datetime] NULL,
	[placeName] [nvarchar](300) NOT NULL,
	[details] [nvarchar](max) NULL,
	[level] [nvarchar](100) NULL,
	[major] [nvarchar](100) NULL,
	[location] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_Education] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Education] ON
INSERT [dbo].[Education] ([ID], [CvID], [fromDate], [toDate], [placeName], [details], [level], [major], [location]) VALUES (11, 9, CAST(0x00009AAE00000000 AS DateTime), NULL, N'University of Sofia', NULL, N'Bachelor&#39;s degree', N'Iconomics', N'Sofia')
INSERT [dbo].[Education] ([ID], [CvID], [fromDate], [toDate], [placeName], [details], [level], [major], [location]) VALUES (12, 9, CAST(0x0000843600000000 AS DateTime), CAST(0x000094DA00000000 AS DateTime), N'Some school in Sofia', NULL, N'Primary education', N'Iconomics', N'Sofia')
INSERT [dbo].[Education] ([ID], [CvID], [fromDate], [toDate], [placeName], [details], [level], [major], [location]) VALUES (13, 10, CAST(0x0000984D00000000 AS DateTime), CAST(0x00009D8800000000 AS DateTime), N'St.Cyril and St.Methodius University of Veliko Tarnovo', N'', N'Bachelor&#39;s degree', N'Computer science', N'Veliko Turnovo / Bulgaria')
INSERT [dbo].[Education] ([ID], [CvID], [fromDate], [toDate], [placeName], [details], [level], [major], [location]) VALUES (14, 11, CAST(0x0000974500000000 AS DateTime), CAST(0x00009CF600000000 AS DateTime), N'Великотърновски университет &quot;Св. Св. Кирил и Методии&quot;', N'', N'Бакалавър', N'Компютърни науки', N'Велико Търново')
SET IDENTITY_INSERT [dbo].[Education] OFF
/****** Object:  Table [dbo].[WorkExperiance]    Script Date: 06/24/2011 15:11:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkExperiance](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[CvID] [bigint] NOT NULL,
	[fromDate] [datetime] NOT NULL,
	[toDate] [datetime] NULL,
	[companyName] [nvarchar](300) NULL,
	[position] [nvarchar](300) NOT NULL,
	[details] [nvarchar](max) NULL,
 CONSTRAINT [PK_WorkExperiance] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[WorkExperiance] ON
INSERT [dbo].[WorkExperiance] ([ID], [CvID], [fromDate], [toDate], [companyName], [position], [details]) VALUES (3, 9, CAST(0x0000951700000000 AS DateTime), CAST(0x000099F700000000 AS DateTime), N'CBA company', N'Products seller', NULL)
INSERT [dbo].[WorkExperiance] ([ID], [CvID], [fromDate], [toDate], [companyName], [position], [details]) VALUES (4, 9, CAST(0x00009BA300000000 AS DateTime), NULL, N'Groceries storahouse', N'Sales representative', NULL)
SET IDENTITY_INSERT [dbo].[WorkExperiance] OFF
/****** Object:  Table [dbo].[Skills]    Script Date: 06/24/2011 15:11:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Skills](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[skill] [nvarchar](200) NOT NULL,
	[gradation] [nvarchar](100) NULL,
	[details] [nvarchar](500) NULL,
	[CvID] [bigint] NOT NULL,
 CONSTRAINT [PK_Skills] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Skills] ON
INSERT [dbo].[Skills] ([ID], [skill], [gradation], [details], [CvID]) VALUES (46, N'English', N'Very good', NULL, 9)
INSERT [dbo].[Skills] ([ID], [skill], [gradation], [details], [CvID]) VALUES (47, N'Microsoft office', N'Good', NULL, 9)
INSERT [dbo].[Skills] ([ID], [skill], [gradation], [details], [CvID]) VALUES (48, N'Social skills', N'Very good', NULL, 9)
INSERT [dbo].[Skills] ([ID], [skill], [gradation], [details], [CvID]) VALUES (49, N'IIS', N'Average', N'', 10)
INSERT [dbo].[Skills] ([ID], [skill], [gradation], [details], [CvID]) VALUES (50, N'ASP.NET', N'Good', N'', 10)
INSERT [dbo].[Skills] ([ID], [skill], [gradation], [details], [CvID]) VALUES (51, N'WebServices', N'Good', N'', 10)
INSERT [dbo].[Skills] ([ID], [skill], [gradation], [details], [CvID]) VALUES (52, N'Entity Data Model', N'Very good', N'', 10)
INSERT [dbo].[Skills] ([ID], [skill], [gradation], [details], [CvID]) VALUES (53, N'C#', N'Very good', N'', 10)
INSERT [dbo].[Skills] ([ID], [skill], [gradation], [details], [CvID]) VALUES (54, N'AJAX', N'Good', N'', 10)
INSERT [dbo].[Skills] ([ID], [skill], [gradation], [details], [CvID]) VALUES (55, N'IIS', N'Средно', N'', 11)
INSERT [dbo].[Skills] ([ID], [skill], [gradation], [details], [CvID]) VALUES (56, N'ASP.NET', N'Добро', N'', 11)
INSERT [dbo].[Skills] ([ID], [skill], [gradation], [details], [CvID]) VALUES (57, N'WebServices', N'Добро', N'', 11)
INSERT [dbo].[Skills] ([ID], [skill], [gradation], [details], [CvID]) VALUES (58, N'Entity Data Model', N'Много добро', N'', 11)
INSERT [dbo].[Skills] ([ID], [skill], [gradation], [details], [CvID]) VALUES (59, N'C#', N'Много добро', N'', 11)
INSERT [dbo].[Skills] ([ID], [skill], [gradation], [details], [CvID]) VALUES (60, N'AJAX', N'Добро', N'', 11)
SET IDENTITY_INSERT [dbo].[Skills] OFF
/****** Object:  ForeignKey [FK_CVs_User]    Script Date: 06/24/2011 15:11:28 ******/
ALTER TABLE [dbo].[CVs]  WITH CHECK ADD  CONSTRAINT [FK_CVs_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[CVs] CHECK CONSTRAINT [FK_CVs_User]
GO
/****** Object:  ForeignKey [FK_Subjects_Education]    Script Date: 06/24/2011 15:11:28 ******/
ALTER TABLE [dbo].[Subjects]  WITH CHECK ADD  CONSTRAINT [FK_Subjects_Education] FOREIGN KEY([EduID])
REFERENCES [dbo].[Education] ([ID])
GO
ALTER TABLE [dbo].[Subjects] CHECK CONSTRAINT [FK_Subjects_Education]
GO
/****** Object:  ForeignKey [FK_Education_CV]    Script Date: 06/24/2011 15:11:28 ******/
ALTER TABLE [dbo].[Education]  WITH CHECK ADD  CONSTRAINT [FK_Education_CV] FOREIGN KEY([CvID])
REFERENCES [dbo].[CVs] ([ID])
GO
ALTER TABLE [dbo].[Education] CHECK CONSTRAINT [FK_Education_CV]
GO
/****** Object:  ForeignKey [FK_WorkExperiance_CV]    Script Date: 06/24/2011 15:11:28 ******/
ALTER TABLE [dbo].[WorkExperiance]  WITH CHECK ADD  CONSTRAINT [FK_WorkExperiance_CV] FOREIGN KEY([CvID])
REFERENCES [dbo].[CVs] ([ID])
GO
ALTER TABLE [dbo].[WorkExperiance] CHECK CONSTRAINT [FK_WorkExperiance_CV]
GO
/****** Object:  ForeignKey [FK_Skills_CV]    Script Date: 06/24/2011 15:11:28 ******/
ALTER TABLE [dbo].[Skills]  WITH CHECK ADD  CONSTRAINT [FK_Skills_CV] FOREIGN KEY([CvID])
REFERENCES [dbo].[CVs] ([ID])
GO
ALTER TABLE [dbo].[Skills] CHECK CONSTRAINT [FK_Skills_CV]
GO
