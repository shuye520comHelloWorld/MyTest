USE [Community]
GO
/****** Object:  Table [dbo].[PB_ApplyTicketTracker]    Script Date: 2016/1/26 12:07:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


PRINT '------ create table PB_ApplyTicketTracker----------------- '
GO  

IF EXISTS (SELECT * FROM  sysobjects where id = OBJECT_ID('[PB_ApplyTicketTracker]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE dbo.[PB_ApplyTicketTracker]

CREATE TABLE [dbo].[PB_ApplyTicketTracker](
	[TrackerKey] [uniqueidentifier] NOT NULL,
	[MappingKey] [uniqueidentifier] NOT NULL,
	[SessionKey] [uniqueidentifier] NOT NULL,
	[Status] [smallint] NOT NULL,
	[ApplyResult] [smallint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[TrackerKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PB_ConsultantSnapshot]    Script Date: 2016/1/26 12:07:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

PRINT '------ create table PB_ConsultantSnapshot----------------- '
GO  

IF EXISTS (SELECT * FROM  sysobjects where id = OBJECT_ID('[PB_ConsultantSnapshot]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE dbo.[PB_ConsultantSnapshot]

CREATE TABLE [dbo].[PB_ConsultantSnapshot](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MappingKey] [uniqueidentifier] NOT NULL,
	[EventKey] [uniqueidentifier] NOT NULL,
	[UserType] [smallint] NOT NULL,
	[NormalTicketQuantity] [int] NOT NULL,
	[VIPTicketQuantity] [int] NOT NULL,
	[NormalTicketSettingQuantity] [int] NOT NULL,
	[VIPTicketSettingQuantity] [int] NOT NULL,
	[ContactId] [bigint] NOT NULL,
	[DirectSellerId] [varchar](12) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[PhoneNumber] [varchar](20) NOT NULL,
	[Level] [varchar](10) NOT NULL,
	[ResidenceID] [varchar](18) NOT NULL,
	[Province] [varchar](20) NULL,
	[City] [varchar](20) NULL,
	[OECountyId] [smallint] NULL,
	[CountyName] [nvarchar](20) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[IsLiveAdd] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [DATA]
) ON [DATA]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PB_Customer]    Script Date: 2016/1/26 12:07:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

PRINT '------ create table PB_Customer----------------- '
GO  

IF EXISTS (SELECT * FROM  sysobjects where id = OBJECT_ID('[PB_Customer]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE dbo.[PB_Customer]

CREATE TABLE [dbo].[PB_Customer](
	[CustomerKey] [uniqueidentifier] NOT NULL,
	[CustomerName] [nvarchar](20) NOT NULL,
	[CustomerPhone] [nvarchar](20) NOT NULL,
	[CustomerType] [smallint] NOT NULL,
	[AgeRange] [smallint] NOT NULL,
	[Career] [smallint] NULL,
	[InterestingTopic] [nvarchar](100) NULL,
	[BeautyClass] [bit] NULL,
	[UsedProduct] [bit] NULL,
	[UsedSet] [nvarchar](100) NULL,
	[InterestInCompany] [nvarchar](100) NULL,
	[AcceptLevel] [varchar](10) NULL,
	[CustomerContactId] [bigint] NULL,
	[UnionID] [varchar](200) NULL,
	[HeadImgUrl] [varchar](400) NULL,
	[Source] [smallint] NOT NULL,
	[IsImportMyCustomer] [bit] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[SMSStatus] [nvarchar](30) NULL,
 CONSTRAINT [PK_PB_Customer] PRIMARY KEY CLUSTERED 
(
	[CustomerKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [DATA]
) ON [DATA]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PB_Event]    Script Date: 2016/1/26 12:07:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

PRINT '------ create table PB_Event----------------- '
GO  

IF EXISTS (SELECT * FROM  sysobjects where id = OBJECT_ID('[PB_Event]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE dbo.[PB_Event]

CREATE TABLE [dbo].[PB_Event](
	[EventKey] [uniqueidentifier] NOT NULL,
	[EventTitle] [nvarchar](100) NOT NULL,
	[EventLocation] [nvarchar](100) NOT NULL,
	[EventStartDate] [datetime] NOT NULL,
	[EventEndDate] [datetime] NOT NULL,
	[ApplyTicketStartDate] [datetime] NOT NULL,
	[ApplyTicketEndDate] [datetime] NOT NULL,
	[CheckinStartDate] [datetime] NOT NULL,
	[CheckinEndDate] [datetime] NOT NULL,
	[InvitationStartDate] [datetime] NULL,
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
 CONSTRAINT [PK__PB_Event__17F7F9384D5AEB31] PRIMARY KEY CLUSTERED 
(
	[EventKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [DATA]
) ON [DATA]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PB_Event-Consultant]    Script Date: 2016/1/26 12:07:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

PRINT '------ create table PB_Event-Consultant----------------- '
GO  

IF EXISTS (SELECT * FROM  sysobjects where id = OBJECT_ID('[PB_Event-Consultant]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE dbo.[PB_Event-Consultant]

CREATE TABLE [dbo].[PB_Event-Consultant](
	[MappingKey] [uniqueidentifier] NOT NULL,
	[EventKey] [uniqueidentifier] NOT NULL,
	[UserType] [smallint] NOT NULL,
	[NormalTicketQuantity] [int] NOT NULL,
	[VIPTicketQuantity] [int] NOT NULL,
	[NormalTicketSettingQuantity] [int] NOT NULL,
	[VIPTicketSettingQuantity] [int] NOT NULL,
	[ContactId] [bigint] NOT NULL,
	[DirectSellerId] [varchar](12) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[PhoneNumber] [varchar](20) NOT NULL,
	[Level] [varchar](10) NOT NULL,
	[ResidenceID] [varchar](18) NOT NULL,
	[Province] [varchar](20) NULL,
	[City] [varchar](20) NULL,
	[OECountyId] [smallint] NULL,
	[CountyName] [nvarchar](20) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[Status] [smallint] NULL,
	[IsConfirmed] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[MappingKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [DATA]
) ON [DATA]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PB_EventSession]    Script Date: 2016/1/26 12:07:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
PRINT '------ create table PB_EventSession----------------- '
GO  

IF EXISTS (SELECT * FROM  sysobjects where id = OBJECT_ID('[PB_EventSession]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE dbo.[PB_EventSession]

CREATE TABLE [dbo].[PB_EventSession](
	[SessionKey] [uniqueidentifier] NOT NULL,
	[EventKey] [uniqueidentifier] NOT NULL,
	[DisplayOrder] [smallint] NOT NULL,
	[SessionStartDate] [datetime] NOT NULL,
	[SessionEndDate] [datetime] NOT NULL,
	[CanApply] [bit] NULL,
	[TicketOut] [bit] NULL,
	[VIPTicketQuantity] [int] NOT NULL,
	[NormalTicketQuantity] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK__PB_Event__DCEB791E512B7C15] PRIMARY KEY CLUSTERED 
(
	[SessionKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [DATA]
) ON [DATA]

GO
/****** Object:  Table [dbo].[PB_EventTicketSetting]    Script Date: 2016/1/26 12:07:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

PRINT '------ create table PB_EventTicketSetting----------------- '
GO  

IF EXISTS (SELECT * FROM  sysobjects where id = OBJECT_ID('[PB_EventTicketSetting]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE dbo.[PB_EventTicketSetting]


CREATE TABLE [dbo].[PB_EventTicketSetting](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EventKey] [uniqueidentifier] NOT NULL,
	[TicketQuantityPerSession] [int] NOT NULL,
	[ApplyTicketTotal] [int] NOT NULL,
	[VolunteerNormalTicketCountPerPerson] [int] NULL,
	[VolunteerVIPTicketCountPerPerson] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [DATA]
) ON [DATA]

GO
/****** Object:  Table [dbo].[PB_OfflineCustomer]    Script Date: 2016/1/26 12:07:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

PRINT '------ create table PB_OfflineCustomer----------------- '
GO  

IF EXISTS (SELECT * FROM  sysobjects where id = OBJECT_ID('[PB_OfflineCustomer]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE dbo.[PB_OfflineCustomer]

CREATE TABLE [dbo].[PB_OfflineCustomer](
	[CustomerKey] [uniqueidentifier] NOT NULL,
	[CustomerName] [nvarchar](20) NOT NULL,
	[ContactType] [nvarchar](20) NOT NULL,
    [ContactInfo] [nvarchar](50) NOT NULL,
	[DirectSellerId] [varchar](12) NOT NULL,
	[EventKey] [varchar](50) NOT NULL,
	[AgeRange] [varchar](50) NULL,
	[IsHearMaryKay] [bit] NULL,
	[InterestingTopic] [nvarchar](100) NULL,
	[CustomerType] [smallint]  NULL,
	[Career] [varchar](50) NULL,
	[IsJoinEvent] [int] NULL,
	[CustomerResponse] [int] NULL,
	[UsedProduct] [bit] NULL,
	[BestContactDate] [int] NULL,
	[AdviceContactDate] [int] NULL,
	[Province] [varchar](50) NULL,
	[City] [varchar](50) NULL,
	[County] [varchar](50) NULL,
	[IsImport] [bit] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_PB_OfflineCustomer] PRIMARY KEY CLUSTERED 
(
	[CustomerKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [DATA]
) ON [DATA]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PB_Ticket]    Script Date: 2016/1/26 12:07:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
PRINT '------ create table PB_Ticket----------------- '
GO  

IF EXISTS (SELECT * FROM  sysobjects where id = OBJECT_ID('[PB_Ticket]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE dbo.[PB_Ticket]


CREATE TABLE [dbo].[PB_Ticket](
	[TicketKey] [uniqueidentifier] NOT NULL,
	[MappingKey] [uniqueidentifier] NOT NULL,
	[EventKey] [uniqueidentifier] NOT NULL,
	[SessionKey] [uniqueidentifier] NOT NULL,
	[CustomerKey] [uniqueidentifier] NULL,
	[TicketType] [smallint] NOT NULL,
	[TicketFrom] [smallint] NULL,
	[TicketStatus] [smallint] NOT NULL,
	[SMSToken] [varchar](10) NOT NULL,
	[SessionStartDate] [datetime] NOT NULL,
	[SessionEndDate] [datetime] NOT NULL,
	[CheckinDate] [datetime] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_PB_Ticket] PRIMARY KEY CLUSTERED 
(
	[TicketKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [DATA]
) ON [DATA]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PB_VolunteerCheckin]    Script Date: 2016/1/26 12:07:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

PRINT '------ create table PB_VolunteerCheckin----------------- '
GO  

IF EXISTS (SELECT * FROM  sysobjects where id = OBJECT_ID('[PB_VolunteerCheckin]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE dbo.[PB_VolunteerCheckin]

CREATE TABLE [dbo].[PB_VolunteerCheckin](
	[Key] [uniqueidentifier] NOT NULL,
	[EventKey] [uniqueidentifier] NOT NULL,
	[MappingKey] [uniqueidentifier] NOT NULL,
	[CheckinDate] [datetime] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [DATA]
) ON [DATA]

GO
