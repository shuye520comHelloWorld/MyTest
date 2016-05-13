USE [Community]
GO

-----alter columns
alter table dbo.iparty_event add FeedbackEndDate datetime 
go

alter table dbo.iParty_PartyApplication add   DisplayStartDate datetime 
go

alter table dbo.iParty_PartyApplication add   DisplayEndDate datetime 
go

alter table dbo.iParty_PartyApplication_History add   DisplayStartDate datetime 
go

alter table dbo.iParty_PartyApplication_History add   DisplayEndDate datetime 
go

alter table dbo.iParty_Invitation add CheckinDate datetime 
go

alter table dbo.iParty_Invitation_History add CheckinDate datetime 
go

alter table dbo.IParty_Customer add [Level] varchar(5) 
go

alter table dbo.IParty_Customer_History add [Level] varchar(5) 
go

alter table dbo.IParty_Customer add [CustomerContactId] bigint 
go

alter table dbo.IParty_Customer_History add [CustomerContactId] bigint 
go

alter table dbo.IParty_Customer add [HasOrder] bit 
go
alter table dbo.IParty_Customer add [IsNewer] bit 
go
alter table dbo.IParty_Customer add [IsPromoted] bit  
go

alter table dbo.IParty_Customer_History add [HasOrder] bit 
go

alter table dbo.IParty_Customer_History add [IsNewer] bit
go

alter table dbo.IParty_Customer_History add IsPromoted bit
go

alter table dbo.IParty_Customer add [NextProcessTime] datetime
go
alter table dbo.IParty_Customer_history add [NextProcessTime] datetime
go


-------alter trigger

/****** Object:  Trigger [dbo].[IParty_Customer_trigger_delete]    Script Date: 05/26/2015 10:51:52 ******/
ALTER TRIGGER [dbo].[IParty_Customer_trigger_delete] ON [dbo].[IParty_Customer]
    FOR delete
AS
    BEGIN
        INSERT  INTO dbo.IParty_Customer_History
                ( CustomerKey ,
                  Name ,
                  PhoneNumber ,
                  Career ,
                  AgeRange ,
                  IsVIP ,
                  CreatedBy ,
                  Reference ,
                  ReferenceKey ,
                  ReferenceType,
                  Inviter,
                  InviterName,
                  UnionId,
                  MaritalStatus,
                  dtCreated,
                  AlteredBy,
                  AlteredType,
                  AlteredWhen,
                  [Level],
                  [CustomerContactId],
                  [HasOrder],
                  [IsNewer],
		  [IsPromoted],
		  [NextProcessTime]
                )
                SELECT  CustomerKey ,
						  Name ,
						  PhoneNumber ,
						  Career ,
						  AgeRange ,
						  IsVIP ,
						  CreatedBy ,
						  Reference ,
						  ReferenceKey ,
						  ReferenceType,
						  Inviter,
						  InviterName,
						  UnionId,
						  MaritalStatus,
						  dtCreated,
                        HOST_NAME() ,
                        'D' ,
                        GETDATE(),
                        [Level],
                        [CustomerContactId],
                        HasOrder,
						[IsNewer],
						[IsPromoted],
                        			[NextProcessTime]
                FROM    DELETED
    END

GO




/****** Object:  Trigger [dbo].[IParty_Customer_trigger_update]    Script Date: 05/26/2015 10:52:56 ******/
ALTER TRIGGER [dbo].[IParty_Customer_trigger_update] ON [dbo].[IParty_Customer]
    FOR UPDATE
AS
    BEGIN
        INSERT  INTO dbo.IParty_Customer_History
                ( CustomerKey ,
                  Name ,
                  PhoneNumber ,
                  Career ,
                  AgeRange ,
                  IsVIP ,
                  CreatedBy ,
                  Reference ,
                  ReferenceKey ,
                  ReferenceType,
                  Inviter,
                  InviterName,
                  UnionId,
                  MaritalStatus,
                  dtCreated,
                  AlteredBy,
                  AlteredType,
                  AlteredWhen,
                  [Level],
                  [CustomerContactId],
                  HasOrder,
                  [IsNewer],
			      [IsPromoted],
		[NextProcessTime]
                )
                SELECT  CustomerKey ,
						  Name ,
						  PhoneNumber ,
						  Career ,
						  AgeRange ,
						  IsVIP ,
						  CreatedBy ,
						  Reference ,
						  ReferenceKey ,
						  ReferenceType,
						  Inviter,
						  InviterName,
						  UnionId,
						  MaritalStatus,
						  dtCreated,
                        HOST_NAME() ,
                        'U' ,
                        GETDATE(),
                        [Level],
                        [CustomerContactId],
			HasOrder,
			[IsNewer],
		  	[IsPromoted],
			[NextProcessTime]
                FROM    DELETED
    END


GO



/****** Object:  Trigger [dbo].[iParty_Invitation_trigger_delete]    Script Date: 05/26/2015 10:53:55 ******/
ALTER TRIGGER [dbo].[iParty_Invitation_trigger_delete] ON [dbo].[iParty_Invitation]
    FOR delete
AS
    BEGIN
        INSERT  INTO dbo.iParty_Invitation_History
                ( InvitationKey ,
                  PartyKey ,
                  CustomerKey ,
                  PhoneNumber ,
                  Reference ,
                  ReferenceKey ,
                  ReferenceType ,
                  Status ,
                  OwnerContactID ,
                  OwnerUnitID,
                  CheckInType,
                  AlteredBy,
                  AlteredType,
                  AlteredWhen,
                  CheckinDate
                )
                SELECT  InvitationKey ,
                  PartyKey ,
                  CustomerKey ,
                  PhoneNumber ,
                  Reference ,
                  ReferenceKey ,
                  ReferenceType ,
                  Status ,
                  OwnerContactID ,
                  OwnerUnitID,
                  CheckInType,
                        HOST_NAME() ,
                        'D' ,
                        GETDATE(),
                        CheckinDate
                FROM    DELETED
    END

GO


/****** Object:  Trigger [dbo].[iParty_Invitation_trigger_update]    Script Date: 05/26/2015 10:54:55 ******/
ALTER TRIGGER [dbo].[iParty_Invitation_trigger_update] ON [dbo].[iParty_Invitation]
    FOR update
AS
    BEGIN
        INSERT  INTO dbo.iParty_Invitation_History
                ( InvitationKey ,
                  PartyKey ,
                  CustomerKey ,
                  PhoneNumber ,
                  Reference ,
                  ReferenceKey ,
                  ReferenceType ,
                  Status ,
                  OwnerContactID ,
                  OwnerUnitID,
                  CheckInType,
                  AlteredBy,
                  AlteredType,
                  AlteredWhen,
                  CheckinDate
                )
                SELECT  InvitationKey ,
                  PartyKey ,
                  CustomerKey ,
                  PhoneNumber ,
                  Reference ,
                  ReferenceKey ,
                  ReferenceType ,
                  Status ,
                  OwnerContactID ,
                  OwnerUnitID,
                  CheckInType,
                        HOST_NAME() ,
                        'U' ,
                        GETDATE(),
                        CheckinDate
                FROM    DELETED
    END

GO



/****** Object:  Trigger [dbo].[iParty_PartyApplication_trigger_delete]    Script Date: 05/26/2015 10:56:00 ******/
ALTER TRIGGER [dbo].[iParty_PartyApplication_trigger_delete] ON [dbo].[iParty_PartyApplication]
    FOR delete
AS
    BEGIN
        INSERT  INTO dbo.iParty_PartyApplication_History
                ( PartyKey ,
                  EventKey ,
                  WorkshopId ,
                  PartyType ,
                  AppliedContactID ,
                  AppliedName ,
                  OrganizationType ,
                  StartDate ,
                  EndDate ,
                  CreateDate,
                  UpdatedBy,
                  AlteredBy,
                  AlteredType,
                  AlteredWhen,
                  DisplayStartDate,
                  DisplayEndDate
                )
                SELECT  PartyKey ,
					  EventKey ,
					  WorkshopId ,
					  PartyType ,
					  AppliedContactID ,
					  AppliedName ,
					  OrganizationType ,
					  StartDate ,
					  EndDate ,
					  CreateDate,
					  UpdatedBy,
                        HOST_NAME() ,
                        'D' ,
                        GETDATE(),
                        DisplayStartDate,
						DisplayEndDate
                FROM    DELETED
    END

GO



/****** Object:  Trigger [dbo].[iParty_PartyApplication_trigger_update]    Script Date: 05/26/2015 10:56:53 ******/
ALTER TRIGGER [dbo].[iParty_PartyApplication_trigger_update] ON [dbo].[iParty_PartyApplication]
    FOR UPDATE
AS
    BEGIN
        INSERT  INTO dbo.iParty_PartyApplication_History
                ( PartyKey ,
                  EventKey ,
                  WorkshopId ,
                  PartyType ,
                  AppliedContactID ,
                  AppliedName ,
                  OrganizationType ,
                  StartDate ,
                  EndDate ,
                  CreateDate,
                  UpdatedBy,
                  AlteredBy,
                  AlteredType,
                  AlteredWhen,
                  DisplayStartDate,
                  DisplayEndDate
                )
                SELECT  PartyKey ,
					  EventKey ,
					  WorkshopId ,
					  PartyType ,
					  AppliedContactID ,
					  AppliedName ,
					  OrganizationType ,
					  StartDate ,
					  EndDate ,
					  CreateDate,
					  UpdatedBy,
                        HOST_NAME() ,
                        'U' ,
                        GETDATE(),
                        DisplayStartDate,
						DisplayEndDate
                FROM    DELETED
    END

GO




-------------------------------------------alter sp----------------------


ALTER proc [dbo].[IParty_Customer_insert]  
(  
 @CustomerKey uniqueidentifier,  
 @Name nvarchar(50),  
 @PhoneNumber varchar(50),  
 @Career int,  
 @AgeRange int,  
 @IsVIP bit,  
 @CreatedBy varchar(50),  
 @Reference varchar(50),  
 @ReferenceKey varchar(50),  
 @ReferenceType int,  
 @Inviter bigint,  
 @InviterName nvarchar(50),  
 @UnionId varchar(50),  
 @MaritalStatus int,  
 @dtCreated datetime,
 @Level varchar(5),
 @CustomerContactId bigint,
 @HasOrder bit,
 @IsNewer bit,
 @IsPromoted bit,
 @NextProcessTime datetime
)  
  
as  
begin  
 insert into IParty_Customer
 (
	  CustomerKey,  
	  Name,  
	  PhoneNumber,  
	  Career,  
	  AgeRange,  
	  IsVIP,  
	  CreatedBy,  
	  Reference,  
	  ReferenceKey,  
	  ReferenceType,  
	  Inviter,  
	  InviterName,  
	  UnionId,  
	  MaritalStatus,  
	  dtCreated,
	  [Level],
	  CustomerContactId,
	  HasOrder,
	  IsNewer,
	  IsPromoted,
	  [NextProcessTime]
	  
 )
  values  
 (  
  @CustomerKey,  
  @Name,  
  @PhoneNumber,  
  @Career,  
  @AgeRange,  
  @IsVIP,  
  @CreatedBy,  
  @Reference,  
  @ReferenceKey,  
  @ReferenceType,  
  @Inviter,  
  @InviterName,  
  @UnionId,  
  @MaritalStatus,  
  @dtCreated,
  @Level,
  @CustomerContactId,
  @HasOrder,
  @IsNewer ,
 @IsPromoted,
 @NextProcessTime   
 )  
end
go




/****** Object:  StoredProcedure [dbo].[IParty_Customer_Update]    Script Date: 05/25/2015 15:10:17 ******/
ALTER proc [dbo].[IParty_Customer_Update]  
(  
 @CustomerKey uniqueidentifier,  
 @Name nvarchar(50),  
 @PhoneNumber varchar(50),  
 @Career int,  
 @AgeRange int,  
 @IsVIP bit,  
 @CreatedBy varchar(50),  
 @Reference varchar(50),  
 @ReferenceKey varchar(50),  
 @ReferenceType int,  
 @Inviter bigint,  
 @InviterName nvarchar(50),  
 @UnionId varchar(50),  
 @MaritalStatus int,  
 @dtCreated datetime,
 @Level varchar(5),
 @CustomerContactId bigint,
 @HasOrder bit,
 @IsNewer bit,
 @IsPromoted bit,
 @NextProcessTime datetime
)  
  
as  
begin  
 update  IParty_Customer set  
    
  Name=@Name,  
  PhoneNumber=@PhoneNumber,  
  Career=@Career,  
  AgeRange=@AgeRange,  
  IsVIP=@IsVIP,  
  CreatedBy=@CreatedBy,  
  Reference=@Reference,  
  ReferenceKey=@ReferenceKey,  
  ReferenceType=@ReferenceType,  
  Inviter=@Inviter,  
  InviterName=@InviterName,  
  UnionId=@UnionId,  
  MaritalStatus=@MaritalStatus,  
  dtCreated=@dtCreated,
  [Level]=@Level ,
  CustomerContactId=@CustomerContactId,
  HasOrder=@HasOrder,
  IsNewer=@IsNewer,
  IsPromoted=@IsPromoted,
  [NextProcessTime]=@NextProcessTime
 where CustomerKey=@CustomerKey  
end
go





/****** Object:  StoredProcedure [dbo].[IParty_Event_insert]    Script Date: 05/25/2015 15:15:04 ******/
ALTER proc [dbo].[IParty_Event_insert]    
(   
@EventKey uniqueidentifier, 
 @Category int,    
 @Title nvarchar(50),    
 @Description nvarchar(200),    
 @ApplicationStartDate datetime,    
 @ApplicationEndDate datetime,    
 @EventStartDate datetime,    
 @EventEndDate datetime,
   @CreateDate datetime,
   @PartyAllowEndDate datetime,
   @PartyAllowStartDate datetime,
   @FeedbackEndDate datetime
  
)    
as    
begin    
 INSERT INTO iParty_Event
 (
 EventKey,    
 Category,    
 Title,    
 Description,    
 ApplicationStartDate,    
 ApplicationEndDate,    
 EventStartDate,    
 EventEndDate,    
 CreateDate,
 PartyAllowStartDate,
 PartyAllowEndDate,
 FeedbackEndDate
 )
  values    
 (    
 @EventKey,    
 @Category,    
 @Title,    
 @Description,    
 @ApplicationStartDate,    
 @ApplicationEndDate,    
 @EventStartDate,    
 @EventEndDate,    
 @CreateDate,
 @PartyAllowStartDate,
 @PartyAllowEndDate,
 @FeedbackEndDate  
 )    
end
go






/****** Object:  StoredProcedure [dbo].[IParty_Event_Update]    Script Date: 05/25/2015 15:16:00 ******/   
ALTER proc [dbo].[IParty_Event_Update]    
(    
 @EventKey uniqueidentifier,    
 @Category int,    
 @Title nvarchar(50),    
 @Description nvarchar(200),    
 @ApplicationStartDate datetime,    
 @ApplicationEndDate datetime,    
 @EventStartDate datetime,    
 @EventEndDate datetime,
 @PartyAllowEndDate datetime,
 @PartyAllowStartDate datetime,
 @FeedbackEndDate  datetime   
)    
as    
begin    
 update iParty_Event     
     
 set     
 Category=@Category,    
 Title=@Title,    
 [Description]=@Description,    
 ApplicationStartDate=@ApplicationStartDate,    
 ApplicationEndDate=@ApplicationEndDate,    
 EventStartDate=@EventStartDate,    
 EventEndDate=@EventEndDate,
 PartyAllowEndDate=@PartyAllowEndDate,
 PartyAllowStartDate=@PartyAllowStartDate,
 FeedbackEndDate  =@FeedbackEndDate  
 where EventKey=@EventKey    
     
end
go







/****** Object:  StoredProcedure [dbo].[iParty_Invitation_insert]    Script Date: 05/25/2015 15:21:53 ******/
ALTER proc [dbo].[iParty_Invitation_insert]
(
	@InvitationKey uniqueidentifier,
	@PartyKey uniqueidentifier,
	@CustomerKey uniqueidentifier,
	@PhoneNumber varchar(25),
	@Reference nvarchar(50),
	@ReferenceKey nvarchar(50),
	@ReferenceType int,
	@Status char(2),
	@OwnerContactID bigint,
	@OwnerUnitID varchar(50),
	@CheckInType char(2),
	@CheckinDate datetime
)

as
begin
	insert into dbo.iParty_Invitation values
	(
		@InvitationKey ,
		@PartyKey ,
		@CustomerKey ,
		@PhoneNumber ,
		@Reference ,
		@ReferenceKey ,
		@ReferenceType ,
		@Status ,
		@OwnerContactID ,
		@OwnerUnitID ,
		@CheckInType,
		@CheckinDate
	)
end


go




/****** Object:  StoredProcedure [dbo].[iParty_Invitation_update]    Script Date: 05/25/2015 15:22:50 ******/
ALTER proc [dbo].[iParty_Invitation_update]
(
	@InvitationKey uniqueidentifier,
	@PartyKey uniqueidentifier,
	@CustomerKey uniqueidentifier,
	@PhoneNumber varchar(25),
	@Reference nvarchar(50),
	@ReferenceKey nvarchar(50),
	@ReferenceType int,
	@Status char(2),
	@OwnerContactID bigint,
	@OwnerUnitID varchar(50),
	@CheckInType char(2),
	@CheckinDate datetime
)

as
begin
	update  iParty_Invitation set
		PartyKey=@PartyKey ,
		CustomerKey=@CustomerKey ,
		PhoneNumber=@PhoneNumber ,
		Reference=@Reference ,
		ReferenceKey=@ReferenceKey ,
		ReferenceType=@ReferenceType ,
		[Status]=@Status ,
		OwnerContactID=@OwnerContactID ,
		OwnerUnitID=@OwnerUnitID ,
		CheckInType=@CheckInType,
		CheckinDate=@CheckinDate
		where InvitationKey=@InvitationKey
	
end

go






/****** Object:  StoredProcedure [dbo].[IParty_PartyApplication_insert]    Script Date: 05/25/2015 15:26:49 ******/

ALTER proc [dbo].[IParty_PartyApplication_insert]  
(  
   
 @PartyKey uniqueidentifier,  
 @EventKey uniqueidentifier,  
 @WorkshopId int,  
 @PartyType int,  
 @AppliedContactID bigint,  
 @AppliedName nvarchar(50),  
 @OrganizationType int,  
 @StartDate datetime,  
 @EndDate datetime , 
 @CreateDate datetime ,
 @DisplayStartDate datetime,
 @DisplayEndDate datetime
)  
as  
begin  
 insert into iParty_PartyApplication
 (
 PartyKey,  
 EventKey,  
 WorkshopId,  
 PartyType,  
 AppliedContactID,  
 AppliedName,  
 OrganizationType,  
 StartDate,  
 EndDate,  
 CreateDate,
 DisplayStartDate ,
 DisplayEndDate 
 )
  values  
(  
 @PartyKey,  
 @EventKey,  
 @WorkshopId,  
 @PartyType,  
 @AppliedContactID,  
 @AppliedName,  
 @OrganizationType,  
 @StartDate,  
 @EndDate,  
 @CreateDate,
 @DisplayStartDate ,
 @DisplayEndDate  

   
)   
end

go





/****** Object:  StoredProcedure [dbo].[IParty_PartyApplication_Updates]    Script Date: 05/25/2015 15:27:38 ******/
ALTER proc [dbo].[IParty_PartyApplication_Updates]  
(  
   
 @PartyKey uniqueidentifier,  
 @EventKey uniqueidentifier,  
 @WorkshopId int,  
 @PartyType int,  
 @AppliedContactID bigint,  
 @AppliedName nvarchar(50),  
 @OrganizationType int,  
 @StartDate datetime,  
 @EndDate datetime,  
 @UpdatedDate datetime,
 @UpdatedBy nvarchar(50),
 @DisplayStartDate datetime,
 @DisplayEndDate datetime
)  
as  
begin  
 update iParty_PartyApplication set  
 EventKey=@EventKey,  
 WorkshopId=@WorkshopId,  
 PartyType=@PartyType,  
 AppliedContactID=@AppliedContactID,  
 AppliedName=@AppliedName,  
 OrganizationType=@OrganizationType,  
 StartDate=@StartDate,  
 EndDate=@EndDate,  
 UpdatedDate=@UpdatedDate,  
 UpdatedBy=@UpdatedBy,
 DisplayStartDate=@DisplayStartDate,
 DisplayEndDate=@DisplayEndDate
 where PartyKey = @PartyKey  
  
end
go

--------------------------------------------------------------------------------------

alter proc [dbo].[ipartySP_GetCoHostApplications]      
(      
 @contactId bigint  
)      
as      
begin      
select p.*, e.FeedbackEndDate from iParty_PartyApplication p with(nolock)  
inner join iParty_Unitee u with(nolock)  
on u.PartyKey = p.PartyKey  
inner join iParty_Event e with (nolock)  
on p.EventKey = e.EventKey  
where u.UniteeContactID = @contactId  
    
end  
go

-----------------------------update displaystartdate and displayenddate--------------------------------------


update iParty_PartyApplication   set iParty_PartyApplication.displaystartdate=iParty_PartyApplication.StartDate
go
update iParty_PartyApplication   set iParty_PartyApplication.displayenddate=iParty_PartyApplication.EndDate
go

update IParty_Customer set HasOrder=0
go
update IParty_Customer set IsNewer=0
go
update IParty_Customer set IsPromoted=0
go




