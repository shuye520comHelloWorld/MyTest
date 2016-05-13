USE [Community]
GO

/****** Object:  Table [dbo].[PB_Event-Consultant]    Script Date: 12/08/2015 10:48:20 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_Event-Consultant]') AND type in (N'U'))
DROP TABLE [dbo].[PB_Event-Consultant]
GO

USE [Community]
GO

/****** Object:  Table [dbo].[PB_Event-Consultant]    Script Date: 12/08/2015 10:48:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[PB_Event-Consultant](
	[MappintKey] [uniqueidentifier] NOT NULL,
	[EventKey] [uniqueidentifier] NOT NULL,
	[UserType] [smallint] NOT NULL,
	[NormalTicketQuantity] [int] NOT NULL,
	[VIPTicketQuantity] [int] NULL,
	[ContactId] [bigint] NOT NULL,
	[DirectSellerId] [varchar](12) NOT NULL,
	[FirstName] [nvarchar](10) NOT NULL,
	[LastName] [nvarchar](10) NOT NULL,
	[PhoneNumber] [varchar](20) NOT NULL,
	[Level] [varchar](10) NOT NULL,
	[ResidenceID] [varchar](18) NOT NULL,
	[Province] [nvarchar](20) NULL,
	[City] [nvarchar](20) NULL,
	[OECountyId] [smallint] NULL,
	[CountyName] [nvarchar](20) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK__PB_Event__4D06A67A7176B968] PRIMARY KEY CLUSTERED 
(
	[MappintKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


USE [Community]
GO

/****** Object:  Table [dbo].[PB_ApplySetting]    Script Date: 12/03/2015 12:51:37 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_ApplySetting]') AND type in (N'U'))
DROP TABLE [dbo].[PB_ApplySetting]
GO

USE [Community]
GO

/****** Object:  Table [dbo].[PB_ApplySetting]    Script Date: 12/03/2015 12:51:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PB_ApplySetting](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EventKey] [uniqueidentifier] NOT NULL,
	[TicketQuantityPerSession] [int] NOT NULL,
	[ApplyTicketTotal] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


USE [Community]
GO

/****** Object:  Table [dbo].[PB_ApplyTicketTracker]    Script Date: 12/03/2015 12:51:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_ApplyTicketTracker]') AND type in (N'U'))
DROP TABLE [dbo].[PB_ApplyTicketTracker]
GO

USE [Community]
GO

/****** Object:  Table [dbo].[PB_ApplyTicketTracker]    Script Date: 12/03/2015 12:51:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PB_ApplyTicketTracker](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ConsultantKey] [uniqueidentifier] NOT NULL,
	[SessionKey] [uniqueidentifier] NOT NULL,
	[Status] [smallint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


USE [Community]
GO

/****** Object:  Table [dbo].[PB_Customer]    Script Date: 12/03/2015 12:52:05 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_Customer]') AND type in (N'U'))
DROP TABLE [dbo].[PB_Customer]
GO

USE [Community]
GO

/****** Object:  Table [dbo].[PB_Customer]    Script Date: 12/03/2015 12:52:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[PB_Customer](
	[CustomerKey] [uniqueidentifier] NOT NULL,
	[CustomerName] [nvarchar](20) NOT NULL,
	[CustomerPhone] [varchar](20) NOT NULL,
	[CustomerType] [smallint] NOT NULL,
	[AgeRange] [smallint] NOT NULL,
	[Career] [smallint] NULL,
	[InterestingTopic] [smallint] NULL,
	[BeautyClass] [bit] NULL,
	[UsedProduct] [bit] NULL,
	[UsedSet] [smallint] NULL,
	[InterestInCompany] [smallint] NULL,
	[AcceptLevel] [varchar](10) NULL,
	[CustomerContactId] [bigint] NULL,
	[UnionID] [varchar](200) NULL,
	[HeadImgUrl] [varchar](400) NULL,
	[Source] [smallint] NOT NULL,
	[IsImportMyCustomer] [bit] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK__PB_Custo__95011E64267ABA7A] PRIMARY KEY CLUSTERED 
(
	[CustomerKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


USE [Community]
GO

/****** Object:  Table [dbo].[PB_Event]    Script Date: 12/03/2015 12:52:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_Event]') AND type in (N'U'))
DROP TABLE [dbo].[PB_Event]
GO

USE [Community]
GO

/****** Object:  Table [dbo].[PB_Event]    Script Date: 12/03/2015 12:52:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[PB_Event](
	[EventKey] [uniqueidentifier] NOT NULL,
	[EventTitle] [nvarchar](100) NOT NULL,
	[EventLocation] [nvarchar](100) NOT NULL,
	[EventStartDate] [datetime] NOT NULL,
	[EventEndDate] [datetime] NOT NULL,
	[ApplyTicketStartDate] [datetime] NOT NULL,
	[ApplyTicketEndDate] [datetime] NOT NULL,
	[InvitationEndDate] [datetime] NOT NULL,
	[DownloadToken] [varchar](10) NOT NULL,
	[UploadToken] [varchar](10) NOT NULL,
	[IsUpload] [bit] NULL,
	[BCImport] [bit] NULL,
	[VolunteerImport] [bit] NULL,
	[VIPImport] [bit] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_PB_Event] PRIMARY KEY CLUSTERED 
(
	[EventKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


USE [Community]
GO

/****** Object:  Table [dbo].[PB_EventSession]    Script Date: 12/03/2015 12:52:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_EventSession]') AND type in (N'U'))
DROP TABLE [dbo].[PB_EventSession]
GO

USE [Community]
GO

/****** Object:  Table [dbo].[PB_EventSession]    Script Date: 12/03/2015 12:52:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PB_EventSession](
	[SessionKey] [uniqueidentifier] NOT NULL,
	[EventKey] [uniqueidentifier] NOT NULL,
	[DisplayOrder] [smallint] NOT NULL,
	[SessionStartDate] [datetime] NOT NULL,
	[SessionEndDate] [datetime] NOT NULL,
	[CanApply] [bit] NULL,
	[VIPTicketQuantity] [int] NOT NULL,
	[NormalTicketQuantity] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[SessionKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


USE [Community]
GO

/****** Object:  Table [dbo].[PB_Ticket]    Script Date: 12/03/2015 12:52:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_Ticket]') AND type in (N'U'))
DROP TABLE [dbo].[PB_Ticket]
GO

USE [Community]
GO

/****** Object:  Table [dbo].[PB_Ticket]    Script Date: 12/03/2015 12:52:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PB_Ticket](
	[TicketKey] [uniqueidentifier] NOT NULL,
	[MappingKey] [uniqueidentifier] NOT NULL,
	[EventKey] [uniqueidentifier] NOT NULL,
	[CustomerKey] [uniqueidentifier] NOT NULL,
	[TicketType] [smallint] NOT NULL,
	[TicketFrom] [smallint] NOT NULL,
	[TicketStatus] [smallint] NOT NULL,
	[CheckinDate] [datetime] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[TicketKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


USE [Community]
GO

/****** Object:  Table [dbo].[PB_VolunteerCheckin]    Script Date: 12/03/2015 12:52:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_VolunteerCheckin]') AND type in (N'U'))
DROP TABLE [dbo].[PB_VolunteerCheckin]
GO

USE [Community]
GO

/****** Object:  Table [dbo].[PB_VolunteerCheckin]    Script Date: 12/03/2015 12:52:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PB_VolunteerCheckin](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EventKey] [uniqueidentifier] NOT NULL,
	[ConsultantKey] [uniqueidentifier] NOT NULL,
	[CheckinDate] [datetime] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


