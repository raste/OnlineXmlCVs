USE [master]
GO
/****** Object:  Database [CVs]    Script Date: 06/24/2011 15:06:17 ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 06/24/2011 15:06:18 ******/
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
/****** Object:  Table [dbo].[CVs]    Script Date: 06/24/2011 15:06:18 ******/
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
/****** Object:  Table [dbo].[Subjects]    Script Date: 06/24/2011 15:06:18 ******/
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
/****** Object:  Table [dbo].[Education]    Script Date: 06/24/2011 15:06:18 ******/
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
/****** Object:  Table [dbo].[WorkExperiance]    Script Date: 06/24/2011 15:06:18 ******/
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
/****** Object:  Table [dbo].[Skills]    Script Date: 06/24/2011 15:06:18 ******/
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
/****** Object:  ForeignKey [FK_CVs_User]    Script Date: 06/24/2011 15:06:18 ******/
ALTER TABLE [dbo].[CVs]  WITH CHECK ADD  CONSTRAINT [FK_CVs_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[CVs] CHECK CONSTRAINT [FK_CVs_User]
GO
/****** Object:  ForeignKey [FK_Subjects_Education]    Script Date: 06/24/2011 15:06:18 ******/
ALTER TABLE [dbo].[Subjects]  WITH CHECK ADD  CONSTRAINT [FK_Subjects_Education] FOREIGN KEY([EduID])
REFERENCES [dbo].[Education] ([ID])
GO
ALTER TABLE [dbo].[Subjects] CHECK CONSTRAINT [FK_Subjects_Education]
GO
/****** Object:  ForeignKey [FK_Education_CV]    Script Date: 06/24/2011 15:06:18 ******/
ALTER TABLE [dbo].[Education]  WITH CHECK ADD  CONSTRAINT [FK_Education_CV] FOREIGN KEY([CvID])
REFERENCES [dbo].[CVs] ([ID])
GO
ALTER TABLE [dbo].[Education] CHECK CONSTRAINT [FK_Education_CV]
GO
/****** Object:  ForeignKey [FK_WorkExperiance_CV]    Script Date: 06/24/2011 15:06:18 ******/
ALTER TABLE [dbo].[WorkExperiance]  WITH CHECK ADD  CONSTRAINT [FK_WorkExperiance_CV] FOREIGN KEY([CvID])
REFERENCES [dbo].[CVs] ([ID])
GO
ALTER TABLE [dbo].[WorkExperiance] CHECK CONSTRAINT [FK_WorkExperiance_CV]
GO
/****** Object:  ForeignKey [FK_Skills_CV]    Script Date: 06/24/2011 15:06:18 ******/
ALTER TABLE [dbo].[Skills]  WITH CHECK ADD  CONSTRAINT [FK_Skills_CV] FOREIGN KEY([CvID])
REFERENCES [dbo].[CVs] ([ID])
GO
ALTER TABLE [dbo].[Skills] CHECK CONSTRAINT [FK_Skills_CV]
GO
