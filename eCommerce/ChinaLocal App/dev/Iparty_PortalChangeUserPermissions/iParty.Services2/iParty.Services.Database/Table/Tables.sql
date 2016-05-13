use Community
Go

print 'create table [iParty_PartyApplication]'
go 

CREATE TABLE [dbo].[iParty_PartyApplication](
	PartyKey [uniqueidentifier],
	EventKey   [uniqueidentifier] not null,
	WorkshopId [INT] not null,
	PartyType  [int] not null ,
	AppliedContactID  bigint not null,
	AppliedName nvarchar(50) not null,
	OrganizationType int not null ,
	StartDate datetime not null ,
	EndDate datetime not null,
	CreateDate datetime not null ,
	UpdatedDate datetime,
	UpdatedBy nvarchar(50)
	 
 CONSTRAINT [PK_iParty_PartyApplication] PRIMARY KEY CLUSTERED 
(
	[PartyKey] ASC
) 
)  

GO

print 'create table [iParty_Event]'
go 

CREATE TABLE [dbo].[iParty_Event](
	EventKey [uniqueidentifier],
	Category   int not null,
	Title  nvarchar(50) not null,
	Description  nvarchar(200) not null,
	ApplicationStartDate datetime not null ,
	ApplicationEndDate datetime not null,
	EventStartDate datetime not null,
	EventEndDate datetime not null,
	CreateDate datetime not null   
 CONSTRAINT [PK_iParty_Event] PRIMARY KEY CLUSTERED 
(
	EventKey ASC
) 
)  

GO


print 'create table [iParty_EventDetail]'
go

CREATE TABLE [dbo].[iParty_EventDetail](
	EventDetailID int not null,
	EventKey [uniqueidentifier],
	Note nvarchar(200), 
	CreateDate datetime not null   
 CONSTRAINT [PK_iParty_EventDetail] PRIMARY KEY CLUSTERED 
(
	EventDetailID ASC
) 
)  
GO 
 
print 'create table [iParty_Unitee]'
go 

CREATE TABLE [dbo].[iParty_Unitee](
	PartyKey [uniqueidentifier],
	UniteeContactID   [bigint] not null,
	FullName nvarchar(50) not null,
	LeveLID  [int] not null ,
	ConsultantStartDate  datetime not null, 
	CreatedDate datetime not null ,
	UnitId varchar(10),
	CreatedBy VARCHAR(50)  
CONSTRAINT [PK_iParty_Unitee] PRIMARY KEY CLUSTERED 
(
	[PartyKey],UniteeContactID ASC
) 
)  

GO
create table dbo.iParty_Invitation
(
	InvitationKey uniqueidentifier primary key,
	PartyKey uniqueidentifier,
	CustomerKey uniqueidentifier,
	PhoneNumber VARCHAR(25) not null,
	Reference NVARCHAR(50),
	ReferenceKey NVARCHAR(50),
	ReferenceType INT,
	[Status] CHAR(2),
	OwnerContactID bigint,
	OwnerUnitID VARCHAR(50),
	CheckInType CHAR(2)
)
