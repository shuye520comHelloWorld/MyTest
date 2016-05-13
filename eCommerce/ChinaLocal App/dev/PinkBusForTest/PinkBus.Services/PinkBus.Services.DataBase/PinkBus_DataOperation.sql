USE [Community]
GO
/****** Object:  StoredProcedure [dbo].[PB_ApplyTicketTracker_Insert]    Script Date: 2016-01-30 14:20:17 ******/
PRINT '----- create PROCEDURE  StoredProcedure [dbo].[PB_ApplyTicketTracker_Insert]    Script Date: 2016-01-30 14:20:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_ApplyTicketTracker_Insert]') AND type in (N'P', N'PC') ) 
   DROP PROCEDURE [dbo].[PB_ApplyTicketTracker_Insert]
GO
  Create Procedure [dbo].[PB_ApplyTicketTracker_Insert]
  (
        @TrackerKey uniqueidentifier,
        @MappingKey uniqueidentifier,
        @SessionKey uniqueidentifier,
        @Status smallint,
        @ApplyResult smallint,
        @CreatedDate datetime,
        @CreatedBy nvarchar(100),
        @UpdatedDate datetime,
        @UpdatedBy nvarchar(100) = null
  )
  as
  begin
      insert into [dbo].[PB_ApplyTicketTracker] 
      (
          TrackerKey,
          MappingKey,
          SessionKey,
          Status,
          ApplyResult,
          CreatedDate,
          CreatedBy,
          UpdatedDate,
          UpdatedBy
      )
      values
      (
          @TrackerKey,
          @MappingKey,
          @SessionKey,
          @Status,
          @ApplyResult,
          @CreatedDate,
          @CreatedBy,
          @UpdatedDate,
          @UpdatedBy
      )
  end
GO
grant exec on [dbo].[PB_ApplyTicketTracker_Insert] TO siteuser,db_spexecute
GO
USE [Community]
GO
/****** Object:  StoredProcedure [dbo].[PB_ApplyTicketTracker_Update]    Script Date: 2016-01-30 14:20:17 ******/
PRINT '----- create PROCEDURE  StoredProcedure [dbo].[PB_ApplyTicketTracker_Update]    Script Date: 2016-01-30 14:20:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_ApplyTicketTracker_Update]') AND type in (N'P', N'PC') ) 
   DROP PROCEDURE [dbo].[PB_ApplyTicketTracker_Update]
GO
  Create Procedure [dbo].[PB_ApplyTicketTracker_Update]
  (
        @TrackerKey uniqueidentifier,
        @MappingKey uniqueidentifier = null ,
        @SessionKey uniqueidentifier = null ,
        @Status smallint = null ,
        @ApplyResult smallint = null ,
        @CreatedDate datetime = null ,
        @CreatedBy nvarchar(100) = null ,
        @UpdatedDate datetime = null ,
        @UpdatedBy nvarchar(100) = null 
  )
  as
  begin
      update [dbo].[PB_ApplyTicketTracker] set
      MappingKey = ISNULL(@MappingKey,MappingKey),
      SessionKey = ISNULL(@SessionKey,SessionKey),
      Status = ISNULL(@Status,Status),
      ApplyResult = ISNULL(@ApplyResult,ApplyResult),
      CreatedDate = ISNULL(@CreatedDate,CreatedDate),
      CreatedBy = ISNULL(@CreatedBy,CreatedBy),
      UpdatedDate = ISNULL(@UpdatedDate,UpdatedDate),
      UpdatedBy = ISNULL(@UpdatedBy,UpdatedBy)
      where TrackerKey =@TrackerKey
  end
GO
grant exec on [dbo].[PB_ApplyTicketTracker_Update] TO siteuser,db_spexecute
GO
USE [Community]
GO
/****** Object:  StoredProcedure [dbo].[PB_ApplyTicketTracker_Delete]    Script Date: 2016-01-30 14:20:17 ******/
PRINT '----- create PROCEDURE  StoredProcedure [dbo].[PB_ApplyTicketTracker_Delete]    Script Date: 2016-01-30 14:20:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_ApplyTicketTracker_Delete]') AND type in (N'P', N'PC') ) 
   DROP PROCEDURE [dbo].[PB_ApplyTicketTracker_Delete]
GO
  Create Procedure [dbo].[PB_ApplyTicketTracker_Delete]
  (
        @TrackerKey uniqueidentifier
  )
  as
  begin
      delete from [dbo].[PB_ApplyTicketTracker] 
      where TrackerKey =@TrackerKey
  end
GO
grant exec on [dbo].[PB_ApplyTicketTracker_Delete] TO siteuser,db_spexecute
GO
USE [Community]
GO
/****** Object:  Table [dbo].[PB_ApplyTicketTracker_History]    Script Date: 2016-01-30 14:20:17 ******/
PRINT '----- create Table  [dbo].[PB_ApplyTicketTracker_History]    Script Date: 2016-01-30 14:20:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_ApplyTicketTracker_History]') AND type in (N'U') ) 
   DROP TABLE [dbo].[PB_ApplyTicketTracker_History]
GO
  Create TABLE [dbo].[PB_ApplyTicketTracker_History]
  (
         HistoryId bigint primary key identity(1,1) not null,
        TrackerKey uniqueidentifier not null,
        MappingKey uniqueidentifier not null,
        SessionKey uniqueidentifier not null,
        Status smallint not null,
        ApplyResult smallint not null,
        CreatedDate datetime not null,
        CreatedBy nvarchar(100) not null,
        UpdatedDate datetime null,
        UpdatedBy nvarchar(100) null,
         AlteredBy nvarchar(50) null,
         AlteredType char(1) null,
         AlteredWhen datetime null
  )
GO
USE [Community]
GO
/****** Object:  Trigger [dbo].[PB_ApplyTicketTracker_Trigger_Update]    Script Date: 2016-01-30 14:20:17 ******/
PRINT '----- create Trigger   [dbo].[PB_ApplyTicketTracker_Trigger_Update]    Script Date: 2016-01-30 14:20:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[PB_ApplyTicketTracker_Trigger_Update]')) 
   DROP TRIGGER [dbo].[PB_ApplyTicketTracker_Trigger_Update]
GO
  Create TRIGGER [dbo].[PB_ApplyTicketTracker_Trigger_Update] on [dbo].[PB_ApplyTicketTracker]
  for UPDATE
  as
  begin
  insert into [dbo].[PB_ApplyTicketTracker_History]
  (
        TrackerKey,
        MappingKey,
        SessionKey,
        Status,
        ApplyResult,
        CreatedDate,
        CreatedBy,
        UpdatedDate,
        UpdatedBy,
        AlteredBy,
        AlteredType,
        AlteredWhen
  )
  select
          TrackerKey,
          MappingKey,
          SessionKey,
          Status,
          ApplyResult,
          CreatedDate,
          CreatedBy,
          UpdatedDate,
          UpdatedBy,
          HOST_NAME(),
          'U',
          getdate()
          from   deleted
  end
GO
USE [Community]
GO
/****** Object:  Trigger [dbo].[PB_ApplyTicketTracker_Trigger_Delete]    Script Date: 2016-01-30 14:20:17 ******/
PRINT '----- create Trigger   [dbo].[PB_ApplyTicketTracker_Trigger_Delete]    Script Date: 2016-01-30 14:20:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[PB_ApplyTicketTracker_Trigger_Delete]')) 
   DROP TRIGGER [dbo].[PB_ApplyTicketTracker_Trigger_Delete]
GO
  Create TRIGGER [dbo].[PB_ApplyTicketTracker_Trigger_Delete] on [dbo].[PB_ApplyTicketTracker]
  for delete
  as
  begin
  insert into [dbo].[PB_ApplyTicketTracker_History]
  (
        TrackerKey,
        MappingKey,
        SessionKey,
        Status,
        ApplyResult,
        CreatedDate,
        CreatedBy,
        UpdatedDate,
        UpdatedBy,
        AlteredBy,
        AlteredType,
        AlteredWhen
  )
  select
          TrackerKey,
          MappingKey,
          SessionKey,
          Status,
          ApplyResult,
          CreatedDate,
          CreatedBy,
          UpdatedDate,
          UpdatedBy,
          HOST_NAME(),
          'U',
          getdate()
          from   deleted
  end
GO
USE [Community]
GO
/****** Object:  StoredProcedure [dbo].[PB_Customer_Insert]    Script Date: 2016-01-30 14:20:17 ******/
PRINT '----- create PROCEDURE  StoredProcedure [dbo].[PB_Customer_Insert]    Script Date: 2016-01-30 14:20:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_Customer_Insert]') AND type in (N'P', N'PC') ) 
   DROP PROCEDURE [dbo].[PB_Customer_Insert]
GO
  Create Procedure [dbo].[PB_Customer_Insert]
  (
        @CustomerKey uniqueidentifier,
        @CustomerName nvarchar(40),
        @CustomerPhone nvarchar(40),
        @CustomerType smallint,
        @AgeRange smallint,
        @Career smallint,
        @InterestingTopic nvarchar(200) = null,
        @BeautyClass bit,
        @UsedProduct bit,
        @UsedSet nvarchar(200) = null,
        @InterestInCompany nvarchar(200) = null,
        @AcceptLevel varchar(10) = null,
        @CustomerContactId bigint,
        @UnionID varchar(200) = null,
        @HeadImgUrl varchar(400) = null,
        @Source smallint,
        @IsImportMyCustomer bit,
        @CreatedDate datetime,
        @CreatedBy nvarchar(100) = null,
        @UpdatedDate datetime,
        @UpdatedBy nvarchar(100) = null,
        @SMSStatus nvarchar(60) = null
  )
  as
  begin
      insert into [dbo].[PB_Customer] 
      (
          CustomerKey,
          CustomerName,
          CustomerPhone,
          CustomerType,
          AgeRange,
          Career,
          InterestingTopic,
          BeautyClass,
          UsedProduct,
          UsedSet,
          InterestInCompany,
          AcceptLevel,
          CustomerContactId,
          UnionID,
          HeadImgUrl,
          Source,
          IsImportMyCustomer,
          CreatedDate,
          CreatedBy,
          UpdatedDate,
          UpdatedBy,
          SMSStatus
      )
      values
      (
          @CustomerKey,
          @CustomerName,
          @CustomerPhone,
          @CustomerType,
          @AgeRange,
          @Career,
          @InterestingTopic,
          @BeautyClass,
          @UsedProduct,
          @UsedSet,
          @InterestInCompany,
          @AcceptLevel,
          @CustomerContactId,
          @UnionID,
          @HeadImgUrl,
          @Source,
          @IsImportMyCustomer,
          @CreatedDate,
          @CreatedBy,
          @UpdatedDate,
          @UpdatedBy,
          @SMSStatus
      )
  end
GO
grant exec on [dbo].[PB_Customer_Insert] TO siteuser,db_spexecute
GO
USE [Community]
GO
/****** Object:  StoredProcedure [dbo].[PB_Customer_Update]    Script Date: 2016-01-30 14:20:17 ******/
PRINT '----- create PROCEDURE  StoredProcedure [dbo].[PB_Customer_Update]    Script Date: 2016-01-30 14:20:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_Customer_Update]') AND type in (N'P', N'PC') ) 
   DROP PROCEDURE [dbo].[PB_Customer_Update]
GO
  Create Procedure [dbo].[PB_Customer_Update]
  (
        @CustomerKey uniqueidentifier,
        @CustomerName nvarchar(40) = null ,
        @CustomerPhone nvarchar(40) = null ,
        @CustomerType smallint = null ,
        @AgeRange smallint = null ,
        @Career smallint = null ,
        @InterestingTopic nvarchar(200) = null ,
        @BeautyClass bit = null ,
        @UsedProduct bit = null ,
        @UsedSet nvarchar(200) = null ,
        @InterestInCompany nvarchar(200) = null ,
        @AcceptLevel varchar(10) = null ,
        @CustomerContactId bigint = null ,
        @UnionID varchar(200) = null ,
        @HeadImgUrl varchar(400) = null ,
        @Source smallint = null ,
        @IsImportMyCustomer bit = null ,
        @CreatedDate datetime = null ,
        @CreatedBy nvarchar(100) = null ,
        @UpdatedDate datetime = null ,
        @UpdatedBy nvarchar(100) = null ,
        @SMSStatus nvarchar(60) = null 
  )
  as
  begin
      update [dbo].[PB_Customer] set
      CustomerName = ISNULL(@CustomerName,CustomerName),
      CustomerPhone = ISNULL(@CustomerPhone,CustomerPhone),
      CustomerType = ISNULL(@CustomerType,CustomerType),
      AgeRange = ISNULL(@AgeRange,AgeRange),
      Career = ISNULL(@Career,Career),
      InterestingTopic = ISNULL(@InterestingTopic,InterestingTopic),
      BeautyClass = ISNULL(@BeautyClass,BeautyClass),
      UsedProduct = ISNULL(@UsedProduct,UsedProduct),
      UsedSet = ISNULL(@UsedSet,UsedSet),
      InterestInCompany = ISNULL(@InterestInCompany,InterestInCompany),
      AcceptLevel = ISNULL(@AcceptLevel,AcceptLevel),
      CustomerContactId = ISNULL(@CustomerContactId,CustomerContactId),
      UnionID = ISNULL(@UnionID,UnionID),
      HeadImgUrl = ISNULL(@HeadImgUrl,HeadImgUrl),
      Source = ISNULL(@Source,Source),
      IsImportMyCustomer = ISNULL(@IsImportMyCustomer,IsImportMyCustomer),
      CreatedDate = ISNULL(@CreatedDate,CreatedDate),
      CreatedBy = ISNULL(@CreatedBy,CreatedBy),
      UpdatedDate = ISNULL(@UpdatedDate,UpdatedDate),
      UpdatedBy = ISNULL(@UpdatedBy,UpdatedBy),
      SMSStatus = ISNULL(@SMSStatus,SMSStatus)
      where CustomerKey =@CustomerKey
  end
GO
grant exec on [dbo].[PB_Customer_Update] TO siteuser,db_spexecute
GO
USE [Community]
GO
/****** Object:  StoredProcedure [dbo].[PB_Customer_Delete]    Script Date: 2016-01-30 14:20:17 ******/
PRINT '----- create PROCEDURE  StoredProcedure [dbo].[PB_Customer_Delete]    Script Date: 2016-01-30 14:20:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_Customer_Delete]') AND type in (N'P', N'PC') ) 
   DROP PROCEDURE [dbo].[PB_Customer_Delete]
GO
  Create Procedure [dbo].[PB_Customer_Delete]
  (
        @CustomerKey uniqueidentifier
  )
  as
  begin
      delete from [dbo].[PB_Customer] 
      where CustomerKey =@CustomerKey
  end
GO
grant exec on [dbo].[PB_Customer_Delete] TO siteuser,db_spexecute
GO
USE [Community]
GO
/****** Object:  Table [dbo].[PB_Customer_History]    Script Date: 2016-01-30 14:20:17 ******/
PRINT '----- create Table  [dbo].[PB_Customer_History]    Script Date: 2016-01-30 14:20:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_Customer_History]') AND type in (N'U') ) 
   DROP TABLE [dbo].[PB_Customer_History]
GO
  Create TABLE [dbo].[PB_Customer_History]
  (
         HistoryId bigint primary key identity(1,1) not null,
        CustomerKey uniqueidentifier not null,
        CustomerName nvarchar(40) not null,
        CustomerPhone nvarchar(40) not null,
        CustomerType smallint not null,
        AgeRange smallint not null,
        Career smallint null,
        InterestingTopic nvarchar(200) null,
        BeautyClass bit null,
        UsedProduct bit null,
        UsedSet nvarchar(200) null,
        InterestInCompany nvarchar(200) null,
        AcceptLevel varchar(10) null,
        CustomerContactId bigint null,
        UnionID varchar(200) null,
        HeadImgUrl varchar(400) null,
        Source smallint not null,
        IsImportMyCustomer bit null,
        CreatedDate datetime not null,
        CreatedBy nvarchar(100) null,
        UpdatedDate datetime null,
        UpdatedBy nvarchar(100) null,
        SMSStatus nvarchar(60) null,
         AlteredBy nvarchar(50) null,
         AlteredType char(1) null,
         AlteredWhen datetime null
  )
GO
USE [Community]
GO
/****** Object:  Trigger [dbo].[PB_Customer_Trigger_Update]    Script Date: 2016-01-30 14:20:17 ******/
PRINT '----- create Trigger   [dbo].[PB_Customer_Trigger_Update]    Script Date: 2016-01-30 14:20:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[PB_Customer_Trigger_Update]')) 
   DROP TRIGGER [dbo].[PB_Customer_Trigger_Update]
GO
  Create TRIGGER [dbo].[PB_Customer_Trigger_Update] on [dbo].[PB_Customer]
  for UPDATE
  as
  begin
  insert into [dbo].[PB_Customer_History]
  (
        CustomerKey,
        CustomerName,
        CustomerPhone,
        CustomerType,
        AgeRange,
        Career,
        InterestingTopic,
        BeautyClass,
        UsedProduct,
        UsedSet,
        InterestInCompany,
        AcceptLevel,
        CustomerContactId,
        UnionID,
        HeadImgUrl,
        Source,
        IsImportMyCustomer,
        CreatedDate,
        CreatedBy,
        UpdatedDate,
        UpdatedBy,
        SMSStatus,
        AlteredBy,
        AlteredType,
        AlteredWhen
  )
  select
          CustomerKey,
          CustomerName,
          CustomerPhone,
          CustomerType,
          AgeRange,
          Career,
          InterestingTopic,
          BeautyClass,
          UsedProduct,
          UsedSet,
          InterestInCompany,
          AcceptLevel,
          CustomerContactId,
          UnionID,
          HeadImgUrl,
          Source,
          IsImportMyCustomer,
          CreatedDate,
          CreatedBy,
          UpdatedDate,
          UpdatedBy,
          SMSStatus,
          HOST_NAME(),
          'U',
          getdate()
          from   deleted
  end
GO
USE [Community]
GO
/****** Object:  Trigger [dbo].[PB_Customer_Trigger_Delete]    Script Date: 2016-01-30 14:20:17 ******/
PRINT '----- create Trigger   [dbo].[PB_Customer_Trigger_Delete]    Script Date: 2016-01-30 14:20:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[PB_Customer_Trigger_Delete]')) 
   DROP TRIGGER [dbo].[PB_Customer_Trigger_Delete]
GO
  Create TRIGGER [dbo].[PB_Customer_Trigger_Delete] on [dbo].[PB_Customer]
  for delete
  as
  begin
  insert into [dbo].[PB_Customer_History]
  (
        CustomerKey,
        CustomerName,
        CustomerPhone,
        CustomerType,
        AgeRange,
        Career,
        InterestingTopic,
        BeautyClass,
        UsedProduct,
        UsedSet,
        InterestInCompany,
        AcceptLevel,
        CustomerContactId,
        UnionID,
        HeadImgUrl,
        Source,
        IsImportMyCustomer,
        CreatedDate,
        CreatedBy,
        UpdatedDate,
        UpdatedBy,
        SMSStatus,
        AlteredBy,
        AlteredType,
        AlteredWhen
  )
  select
          CustomerKey,
          CustomerName,
          CustomerPhone,
          CustomerType,
          AgeRange,
          Career,
          InterestingTopic,
          BeautyClass,
          UsedProduct,
          UsedSet,
          InterestInCompany,
          AcceptLevel,
          CustomerContactId,
          UnionID,
          HeadImgUrl,
          Source,
          IsImportMyCustomer,
          CreatedDate,
          CreatedBy,
          UpdatedDate,
          UpdatedBy,
          SMSStatus,
          HOST_NAME(),
          'U',
          getdate()
          from   deleted
  end
GO
USE [Community]
GO
/****** Object:  StoredProcedure [dbo].[PB_Event_Insert]    Script Date: 2016-01-30 14:20:17 ******/
PRINT '----- create PROCEDURE  StoredProcedure [dbo].[PB_Event_Insert]    Script Date: 2016-01-30 14:20:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_Event_Insert]') AND type in (N'P', N'PC') ) 
   DROP PROCEDURE [dbo].[PB_Event_Insert]
GO
  Create Procedure [dbo].[PB_Event_Insert]
  (
        @EventKey uniqueidentifier,
        @EventTitle nvarchar(200),
        @EventLocation nvarchar(200),
        @EventStartDate datetime,
        @EventEndDate datetime,
        @ApplyTicketStartDate datetime,
        @ApplyTicketEndDate datetime,
        @CheckinStartDate datetime,
        @CheckinEndDate datetime,
        @InvitationStartDate datetime,
        @InvitationEndDate datetime,
        @DownloadToken varchar(10),
        @UploadToken varchar(10),
        @IsUpload bit,
        @BCImport bit,
        @VolunteerImport bit,
        @VIPImport bit,
        @CreatedDate datetime,
        @CreatedBy nvarchar(100),
        @UpdatedDate datetime,
        @UpdatedBy nvarchar(100) = null
  )
  as
  begin
      insert into [dbo].[PB_Event] 
      (
          EventKey,
          EventTitle,
          EventLocation,
          EventStartDate,
          EventEndDate,
          ApplyTicketStartDate,
          ApplyTicketEndDate,
          CheckinStartDate,
          CheckinEndDate,
          InvitationStartDate,
          InvitationEndDate,
          DownloadToken,
          UploadToken,
          IsUpload,
          BCImport,
          VolunteerImport,
          VIPImport,
          CreatedDate,
          CreatedBy,
          UpdatedDate,
          UpdatedBy
      )
      values
      (
          @EventKey,
          @EventTitle,
          @EventLocation,
          @EventStartDate,
          @EventEndDate,
          @ApplyTicketStartDate,
          @ApplyTicketEndDate,
          @CheckinStartDate,
          @CheckinEndDate,
          @InvitationStartDate,
          @InvitationEndDate,
          @DownloadToken,
          @UploadToken,
          @IsUpload,
          @BCImport,
          @VolunteerImport,
          @VIPImport,
          @CreatedDate,
          @CreatedBy,
          @UpdatedDate,
          @UpdatedBy
      )
  end
GO
grant exec on [dbo].[PB_Event_Insert] TO siteuser,db_spexecute
GO
USE [Community]
GO
/****** Object:  StoredProcedure [dbo].[PB_Event_Update]    Script Date: 2016-01-30 14:20:17 ******/
PRINT '----- create PROCEDURE  StoredProcedure [dbo].[PB_Event_Update]    Script Date: 2016-01-30 14:20:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_Event_Update]') AND type in (N'P', N'PC') ) 
   DROP PROCEDURE [dbo].[PB_Event_Update]
GO
  Create Procedure [dbo].[PB_Event_Update]
  (
        @EventKey uniqueidentifier,
        @EventTitle nvarchar(200) = null ,
        @EventLocation nvarchar(200) = null ,
        @EventStartDate datetime = null ,
        @EventEndDate datetime = null ,
        @ApplyTicketStartDate datetime = null ,
        @ApplyTicketEndDate datetime = null ,
        @CheckinStartDate datetime = null ,
        @CheckinEndDate datetime = null ,
        @InvitationStartDate datetime = null ,
        @InvitationEndDate datetime = null ,
        @DownloadToken varchar(10) = null ,
        @UploadToken varchar(10) = null ,
        @IsUpload bit = null ,
        @BCImport bit = null ,
        @VolunteerImport bit = null ,
        @VIPImport bit = null ,
        @CreatedDate datetime = null ,
        @CreatedBy nvarchar(100) = null ,
        @UpdatedDate datetime = null ,
        @UpdatedBy nvarchar(100) = null 
  )
  as
  begin
      update [dbo].[PB_Event] set
      EventTitle = ISNULL(@EventTitle,EventTitle),
      EventLocation = ISNULL(@EventLocation,EventLocation),
      EventStartDate = ISNULL(@EventStartDate,EventStartDate),
      EventEndDate = ISNULL(@EventEndDate,EventEndDate),
      ApplyTicketStartDate = ISNULL(@ApplyTicketStartDate,ApplyTicketStartDate),
      ApplyTicketEndDate = ISNULL(@ApplyTicketEndDate,ApplyTicketEndDate),
      CheckinStartDate = ISNULL(@CheckinStartDate,CheckinStartDate),
      CheckinEndDate = ISNULL(@CheckinEndDate,CheckinEndDate),
      InvitationStartDate = ISNULL(@InvitationStartDate,InvitationStartDate),
      InvitationEndDate = ISNULL(@InvitationEndDate,InvitationEndDate),
      DownloadToken = ISNULL(@DownloadToken,DownloadToken),
      UploadToken = ISNULL(@UploadToken,UploadToken),
      IsUpload = ISNULL(@IsUpload,IsUpload),
      BCImport = ISNULL(@BCImport,BCImport),
      VolunteerImport = ISNULL(@VolunteerImport,VolunteerImport),
      VIPImport = ISNULL(@VIPImport,VIPImport),
      CreatedDate = ISNULL(@CreatedDate,CreatedDate),
      CreatedBy = ISNULL(@CreatedBy,CreatedBy),
      UpdatedDate = ISNULL(@UpdatedDate,UpdatedDate),
      UpdatedBy = ISNULL(@UpdatedBy,UpdatedBy)
      where EventKey =@EventKey
  end
GO
grant exec on [dbo].[PB_Event_Update] TO siteuser,db_spexecute
GO
USE [Community]
GO
/****** Object:  StoredProcedure [dbo].[PB_Event_Delete]    Script Date: 2016-01-30 14:20:17 ******/
PRINT '----- create PROCEDURE  StoredProcedure [dbo].[PB_Event_Delete]    Script Date: 2016-01-30 14:20:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_Event_Delete]') AND type in (N'P', N'PC') ) 
   DROP PROCEDURE [dbo].[PB_Event_Delete]
GO
  Create Procedure [dbo].[PB_Event_Delete]
  (
        @EventKey uniqueidentifier
  )
  as
  begin
      delete from [dbo].[PB_Event] 
      where EventKey =@EventKey
  end
GO
grant exec on [dbo].[PB_Event_Delete] TO siteuser,db_spexecute
GO
USE [Community]
GO
/****** Object:  Table [dbo].[PB_Event_History]    Script Date: 2016-01-30 14:20:17 ******/
PRINT '----- create Table  [dbo].[PB_Event_History]    Script Date: 2016-01-30 14:20:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_Event_History]') AND type in (N'U') ) 
   DROP TABLE [dbo].[PB_Event_History]
GO
  Create TABLE [dbo].[PB_Event_History]
  (
         HistoryId bigint primary key identity(1,1) not null,
        EventKey uniqueidentifier not null,
        EventTitle nvarchar(200) not null,
        EventLocation nvarchar(200) not null,
        EventStartDate datetime not null,
        EventEndDate datetime not null,
        ApplyTicketStartDate datetime not null,
        ApplyTicketEndDate datetime not null,
        CheckinStartDate datetime not null,
        CheckinEndDate datetime not null,
        InvitationStartDate datetime null,
        InvitationEndDate datetime not null,
        DownloadToken varchar(10) not null,
        UploadToken varchar(10) not null,
        IsUpload bit null,
        BCImport bit null,
        VolunteerImport bit null,
        VIPImport bit null,
        CreatedDate datetime not null,
        CreatedBy nvarchar(100) not null,
        UpdatedDate datetime null,
        UpdatedBy nvarchar(100) null,
         AlteredBy nvarchar(50) null,
         AlteredType char(1) null,
         AlteredWhen datetime null
  )
GO
USE [Community]
GO
/****** Object:  Trigger [dbo].[PB_Event_Trigger_Update]    Script Date: 2016-01-30 14:20:17 ******/
PRINT '----- create Trigger   [dbo].[PB_Event_Trigger_Update]    Script Date: 2016-01-30 14:20:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[PB_Event_Trigger_Update]')) 
   DROP TRIGGER [dbo].[PB_Event_Trigger_Update]
GO
  Create TRIGGER [dbo].[PB_Event_Trigger_Update] on [dbo].[PB_Event]
  for UPDATE
  as
  begin
  insert into [dbo].[PB_Event_History]
  (
        EventKey,
        EventTitle,
        EventLocation,
        EventStartDate,
        EventEndDate,
        ApplyTicketStartDate,
        ApplyTicketEndDate,
        CheckinStartDate,
        CheckinEndDate,
        InvitationStartDate,
        InvitationEndDate,
        DownloadToken,
        UploadToken,
        IsUpload,
        BCImport,
        VolunteerImport,
        VIPImport,
        CreatedDate,
        CreatedBy,
        UpdatedDate,
        UpdatedBy,
        AlteredBy,
        AlteredType,
        AlteredWhen
  )
  select
          EventKey,
          EventTitle,
          EventLocation,
          EventStartDate,
          EventEndDate,
          ApplyTicketStartDate,
          ApplyTicketEndDate,
          CheckinStartDate,
          CheckinEndDate,
          InvitationStartDate,
          InvitationEndDate,
          DownloadToken,
          UploadToken,
          IsUpload,
          BCImport,
          VolunteerImport,
          VIPImport,
          CreatedDate,
          CreatedBy,
          UpdatedDate,
          UpdatedBy,
          HOST_NAME(),
          'U',
          getdate()
          from   deleted
  end
GO
USE [Community]
GO
/****** Object:  Trigger [dbo].[PB_Event_Trigger_Delete]    Script Date: 2016-01-30 14:20:17 ******/
PRINT '----- create Trigger   [dbo].[PB_Event_Trigger_Delete]    Script Date: 2016-01-30 14:20:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[PB_Event_Trigger_Delete]')) 
   DROP TRIGGER [dbo].[PB_Event_Trigger_Delete]
GO
  Create TRIGGER [dbo].[PB_Event_Trigger_Delete] on [dbo].[PB_Event]
  for delete
  as
  begin
  insert into [dbo].[PB_Event_History]
  (
        EventKey,
        EventTitle,
        EventLocation,
        EventStartDate,
        EventEndDate,
        ApplyTicketStartDate,
        ApplyTicketEndDate,
        CheckinStartDate,
        CheckinEndDate,
        InvitationStartDate,
        InvitationEndDate,
        DownloadToken,
        UploadToken,
        IsUpload,
        BCImport,
        VolunteerImport,
        VIPImport,
        CreatedDate,
        CreatedBy,
        UpdatedDate,
        UpdatedBy,
        AlteredBy,
        AlteredType,
        AlteredWhen
  )
  select
          EventKey,
          EventTitle,
          EventLocation,
          EventStartDate,
          EventEndDate,
          ApplyTicketStartDate,
          ApplyTicketEndDate,
          CheckinStartDate,
          CheckinEndDate,
          InvitationStartDate,
          InvitationEndDate,
          DownloadToken,
          UploadToken,
          IsUpload,
          BCImport,
          VolunteerImport,
          VIPImport,
          CreatedDate,
          CreatedBy,
          UpdatedDate,
          UpdatedBy,
          HOST_NAME(),
          'U',
          getdate()
          from   deleted
  end
GO
USE [Community]
GO
/****** Object:  StoredProcedure [dbo].[PB_Event-Consultant_Insert]    Script Date: 2016-01-30 14:20:17 ******/
PRINT '----- create PROCEDURE  StoredProcedure [dbo].[PB_Event-Consultant_Insert]    Script Date: 2016-01-30 14:20:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_Event-Consultant_Insert]') AND type in (N'P', N'PC') ) 
   DROP PROCEDURE [dbo].[PB_Event-Consultant_Insert]
GO
  Create Procedure [dbo].[PB_Event-Consultant_Insert]
  (
        @MappingKey uniqueidentifier,
        @EventKey uniqueidentifier,
        @UserType smallint,
        @NormalTicketQuantity int,
        @VIPTicketQuantity int,
        @NormalTicketSettingQuantity int,
        @VIPTicketSettingQuantity int,
        @ContactId bigint,
        @DirectSellerId varchar(12),
        @FirstName nvarchar(100),
        @LastName nvarchar(100),
        @PhoneNumber varchar(20),
        @Level varchar(10),
        @ResidenceID varchar(18),
        @Province varchar(20) = null,
        @City varchar(20) = null,
        @OECountyId smallint,
        @CountyName nvarchar(40) = null,
        @CreatedDate datetime,
        @CreatedBy nvarchar(100),
        @UpdatedDate datetime,
        @UpdatedBy nvarchar(100) = null,
        @Status smallint,
        @IsConfirmed bit
  )
  as
  begin
      insert into [dbo].[PB_Event-Consultant] 
      (
          MappingKey,
          EventKey,
          UserType,
          NormalTicketQuantity,
          VIPTicketQuantity,
          NormalTicketSettingQuantity,
          VIPTicketSettingQuantity,
          ContactId,
          DirectSellerId,
          FirstName,
          LastName,
          PhoneNumber,
          Level,
          ResidenceID,
          Province,
          City,
          OECountyId,
          CountyName,
          CreatedDate,
          CreatedBy,
          UpdatedDate,
          UpdatedBy,
          Status,
          IsConfirmed
      )
      values
      (
          @MappingKey,
          @EventKey,
          @UserType,
          @NormalTicketQuantity,
          @VIPTicketQuantity,
          @NormalTicketSettingQuantity,
          @VIPTicketSettingQuantity,
          @ContactId,
          @DirectSellerId,
          @FirstName,
          @LastName,
          @PhoneNumber,
          @Level,
          @ResidenceID,
          @Province,
          @City,
          @OECountyId,
          @CountyName,
          @CreatedDate,
          @CreatedBy,
          @UpdatedDate,
          @UpdatedBy,
          @Status,
          @IsConfirmed
      )
  end
GO
grant exec on [dbo].[PB_Event-Consultant_Insert] TO siteuser,db_spexecute
GO
USE [Community]
GO
/****** Object:  StoredProcedure [dbo].[PB_Event-Consultant_Update]    Script Date: 2016-01-30 14:20:17 ******/
PRINT '----- create PROCEDURE  StoredProcedure [dbo].[PB_Event-Consultant_Update]    Script Date: 2016-01-30 14:20:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_Event-Consultant_Update]') AND type in (N'P', N'PC') ) 
   DROP PROCEDURE [dbo].[PB_Event-Consultant_Update]
GO
  Create Procedure [dbo].[PB_Event-Consultant_Update]
  (
        @MappingKey uniqueidentifier,
        @EventKey uniqueidentifier = null ,
        @UserType smallint = null ,
        @NormalTicketQuantity int = null ,
        @VIPTicketQuantity int = null ,
        @NormalTicketSettingQuantity int = null ,
        @VIPTicketSettingQuantity int = null ,
        @ContactId bigint = null ,
        @DirectSellerId varchar(12) = null ,
        @FirstName nvarchar(100) = null ,
        @LastName nvarchar(100) = null ,
        @PhoneNumber varchar(20) = null ,
        @Level varchar(10) = null ,
        @ResidenceID varchar(18) = null ,
        @Province varchar(20) = null ,
        @City varchar(20) = null ,
        @OECountyId smallint = null ,
        @CountyName nvarchar(40) = null ,
        @CreatedDate datetime = null ,
        @CreatedBy nvarchar(100) = null ,
        @UpdatedDate datetime = null ,
        @UpdatedBy nvarchar(100) = null ,
        @Status smallint = null ,
        @IsConfirmed bit = null 
  )
  as
  begin
      update [dbo].[PB_Event-Consultant] set
      EventKey = ISNULL(@EventKey,EventKey),
      UserType = ISNULL(@UserType,UserType),
      NormalTicketQuantity = ISNULL(@NormalTicketQuantity,NormalTicketQuantity),
      VIPTicketQuantity = ISNULL(@VIPTicketQuantity,VIPTicketQuantity),
      NormalTicketSettingQuantity = ISNULL(@NormalTicketSettingQuantity,NormalTicketSettingQuantity),
      VIPTicketSettingQuantity = ISNULL(@VIPTicketSettingQuantity,VIPTicketSettingQuantity),
      ContactId = ISNULL(@ContactId,ContactId),
      DirectSellerId = ISNULL(@DirectSellerId,DirectSellerId),
      FirstName = ISNULL(@FirstName,FirstName),
      LastName = ISNULL(@LastName,LastName),
      PhoneNumber = ISNULL(@PhoneNumber,PhoneNumber),
      Level = ISNULL(@Level,Level),
      ResidenceID = ISNULL(@ResidenceID,ResidenceID),
      Province = ISNULL(@Province,Province),
      City = ISNULL(@City,City),
      OECountyId = ISNULL(@OECountyId,OECountyId),
      CountyName = ISNULL(@CountyName,CountyName),
      CreatedDate = ISNULL(@CreatedDate,CreatedDate),
      CreatedBy = ISNULL(@CreatedBy,CreatedBy),
      UpdatedDate = ISNULL(@UpdatedDate,UpdatedDate),
      UpdatedBy = ISNULL(@UpdatedBy,UpdatedBy),
      Status = ISNULL(@Status,Status),
      IsConfirmed = ISNULL(@IsConfirmed,IsConfirmed)
      where MappingKey =@MappingKey
  end
GO
grant exec on [dbo].[PB_Event-Consultant_Update] TO siteuser,db_spexecute
GO
USE [Community]
GO
/****** Object:  StoredProcedure [dbo].[PB_Event-Consultant_Delete]    Script Date: 2016-01-30 14:20:17 ******/
PRINT '----- create PROCEDURE  StoredProcedure [dbo].[PB_Event-Consultant_Delete]    Script Date: 2016-01-30 14:20:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_Event-Consultant_Delete]') AND type in (N'P', N'PC') ) 
   DROP PROCEDURE [dbo].[PB_Event-Consultant_Delete]
GO
  Create Procedure [dbo].[PB_Event-Consultant_Delete]
  (
        @MappingKey uniqueidentifier
  )
  as
  begin
      delete from [dbo].[PB_Event-Consultant] 
      where MappingKey =@MappingKey
  end
GO
grant exec on [dbo].[PB_Event-Consultant_Delete] TO siteuser,db_spexecute
GO
USE [Community]
GO
/****** Object:  Table [dbo].[PB_Event-Consultant_History]    Script Date: 2016-01-30 14:20:17 ******/
PRINT '----- create Table  [dbo].[PB_Event-Consultant_History]    Script Date: 2016-01-30 14:20:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_Event-Consultant_History]') AND type in (N'U') ) 
   DROP TABLE [dbo].[PB_Event-Consultant_History]
GO
  Create TABLE [dbo].[PB_Event-Consultant_History]
  (
         HistoryId bigint primary key identity(1,1) not null,
        MappingKey uniqueidentifier not null,
        EventKey uniqueidentifier not null,
        UserType smallint not null,
        NormalTicketQuantity int not null,
        VIPTicketQuantity int not null,
        NormalTicketSettingQuantity int not null,
        VIPTicketSettingQuantity int not null,
        ContactId bigint not null,
        DirectSellerId varchar(12) not null,
        FirstName nvarchar(100) not null,
        LastName nvarchar(100) not null,
        PhoneNumber varchar(20) not null,
        Level varchar(10) not null,
        ResidenceID varchar(18) not null,
        Province varchar(20) null,
        City varchar(20) null,
        OECountyId smallint null,
        CountyName nvarchar(40) null,
        CreatedDate datetime not null,
        CreatedBy nvarchar(100) not null,
        UpdatedDate datetime null,
        UpdatedBy nvarchar(100) null,
        Status smallint null,
        IsConfirmed bit null,
         AlteredBy nvarchar(50) null,
         AlteredType char(1) null,
         AlteredWhen datetime null
  )
GO
USE [Community]
GO
/****** Object:  Trigger [dbo].[PB_Event-Consultant_Trigger_Update]    Script Date: 2016-01-30 14:20:17 ******/
PRINT '----- create Trigger   [dbo].[PB_Event-Consultant_Trigger_Update]    Script Date: 2016-01-30 14:20:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[PB_Event-Consultant_Trigger_Update]')) 
   DROP TRIGGER [dbo].[PB_Event-Consultant_Trigger_Update]
GO
  Create TRIGGER [dbo].[PB_Event-Consultant_Trigger_Update] on [dbo].[PB_Event-Consultant]
  for UPDATE
  as
  begin
  insert into [dbo].[PB_Event-Consultant_History]
  (
        MappingKey,
        EventKey,
        UserType,
        NormalTicketQuantity,
        VIPTicketQuantity,
        NormalTicketSettingQuantity,
        VIPTicketSettingQuantity,
        ContactId,
        DirectSellerId,
        FirstName,
        LastName,
        PhoneNumber,
        Level,
        ResidenceID,
        Province,
        City,
        OECountyId,
        CountyName,
        CreatedDate,
        CreatedBy,
        UpdatedDate,
        UpdatedBy,
        Status,
        IsConfirmed,
        AlteredBy,
        AlteredType,
        AlteredWhen
  )
  select
          MappingKey,
          EventKey,
          UserType,
          NormalTicketQuantity,
          VIPTicketQuantity,
          NormalTicketSettingQuantity,
          VIPTicketSettingQuantity,
          ContactId,
          DirectSellerId,
          FirstName,
          LastName,
          PhoneNumber,
          Level,
          ResidenceID,
          Province,
          City,
          OECountyId,
          CountyName,
          CreatedDate,
          CreatedBy,
          UpdatedDate,
          UpdatedBy,
          Status,
          IsConfirmed,
          HOST_NAME(),
          'U',
          getdate()
          from   deleted
  end
GO
USE [Community]
GO
/****** Object:  Trigger [dbo].[PB_Event-Consultant_Trigger_Delete]    Script Date: 2016-01-30 14:20:17 ******/
PRINT '----- create Trigger   [dbo].[PB_Event-Consultant_Trigger_Delete]    Script Date: 2016-01-30 14:20:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[PB_Event-Consultant_Trigger_Delete]')) 
   DROP TRIGGER [dbo].[PB_Event-Consultant_Trigger_Delete]
GO
  Create TRIGGER [dbo].[PB_Event-Consultant_Trigger_Delete] on [dbo].[PB_Event-Consultant]
  for delete
  as
  begin
  insert into [dbo].[PB_Event-Consultant_History]
  (
        MappingKey,
        EventKey,
        UserType,
        NormalTicketQuantity,
        VIPTicketQuantity,
        NormalTicketSettingQuantity,
        VIPTicketSettingQuantity,
        ContactId,
        DirectSellerId,
        FirstName,
        LastName,
        PhoneNumber,
        Level,
        ResidenceID,
        Province,
        City,
        OECountyId,
        CountyName,
        CreatedDate,
        CreatedBy,
        UpdatedDate,
        UpdatedBy,
        Status,
        IsConfirmed,
        AlteredBy,
        AlteredType,
        AlteredWhen
  )
  select
          MappingKey,
          EventKey,
          UserType,
          NormalTicketQuantity,
          VIPTicketQuantity,
          NormalTicketSettingQuantity,
          VIPTicketSettingQuantity,
          ContactId,
          DirectSellerId,
          FirstName,
          LastName,
          PhoneNumber,
          Level,
          ResidenceID,
          Province,
          City,
          OECountyId,
          CountyName,
          CreatedDate,
          CreatedBy,
          UpdatedDate,
          UpdatedBy,
          Status,
          IsConfirmed,
          HOST_NAME(),
          'U',
          getdate()
          from   deleted
  end
GO
USE [Community]
GO
/****** Object:  StoredProcedure [dbo].[PB_EventSession_Insert]    Script Date: 2016-01-30 14:20:17 ******/
PRINT '----- create PROCEDURE  StoredProcedure [dbo].[PB_EventSession_Insert]    Script Date: 2016-01-30 14:20:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_EventSession_Insert]') AND type in (N'P', N'PC') ) 
   DROP PROCEDURE [dbo].[PB_EventSession_Insert]
GO
  Create Procedure [dbo].[PB_EventSession_Insert]
  (
        @SessionKey uniqueidentifier,
        @EventKey uniqueidentifier,
        @DisplayOrder smallint,
        @SessionStartDate datetime,
        @SessionEndDate datetime,
        @CanApply bit,
        @TicketOut bit,
        @VIPTicketQuantity int,
        @NormalTicketQuantity int,
        @CreatedDate datetime,
        @CreatedBy nvarchar(100),
        @UpdatedDate datetime,
        @UpdatedBy nvarchar(100) = null
  )
  as
  begin
      insert into [dbo].[PB_EventSession] 
      (
          SessionKey,
          EventKey,
          DisplayOrder,
          SessionStartDate,
          SessionEndDate,
          CanApply,
          TicketOut,
          VIPTicketQuantity,
          NormalTicketQuantity,
          CreatedDate,
          CreatedBy,
          UpdatedDate,
          UpdatedBy
      )
      values
      (
          @SessionKey,
          @EventKey,
          @DisplayOrder,
          @SessionStartDate,
          @SessionEndDate,
          @CanApply,
          @TicketOut,
          @VIPTicketQuantity,
          @NormalTicketQuantity,
          @CreatedDate,
          @CreatedBy,
          @UpdatedDate,
          @UpdatedBy
      )
  end
GO
grant exec on [dbo].[PB_EventSession_Insert] TO siteuser,db_spexecute
GO
USE [Community]
GO
/****** Object:  StoredProcedure [dbo].[PB_EventSession_Update]    Script Date: 2016-01-30 14:20:17 ******/
PRINT '----- create PROCEDURE  StoredProcedure [dbo].[PB_EventSession_Update]    Script Date: 2016-01-30 14:20:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_EventSession_Update]') AND type in (N'P', N'PC') ) 
   DROP PROCEDURE [dbo].[PB_EventSession_Update]
GO
  Create Procedure [dbo].[PB_EventSession_Update]
  (
        @SessionKey uniqueidentifier,
        @EventKey uniqueidentifier = null ,
        @DisplayOrder smallint = null ,
        @SessionStartDate datetime = null ,
        @SessionEndDate datetime = null ,
        @CanApply bit = null ,
        @TicketOut bit = null ,
        @VIPTicketQuantity int = null ,
        @NormalTicketQuantity int = null ,
        @CreatedDate datetime = null ,
        @CreatedBy nvarchar(100) = null ,
        @UpdatedDate datetime = null ,
        @UpdatedBy nvarchar(100) = null 
  )
  as
  begin
      update [dbo].[PB_EventSession] set
      EventKey = ISNULL(@EventKey,EventKey),
      DisplayOrder = ISNULL(@DisplayOrder,DisplayOrder),
      SessionStartDate = ISNULL(@SessionStartDate,SessionStartDate),
      SessionEndDate = ISNULL(@SessionEndDate,SessionEndDate),
      CanApply = ISNULL(@CanApply,CanApply),
      TicketOut = ISNULL(@TicketOut,TicketOut),
      VIPTicketQuantity = ISNULL(@VIPTicketQuantity,VIPTicketQuantity),
      NormalTicketQuantity = ISNULL(@NormalTicketQuantity,NormalTicketQuantity),
      CreatedDate = ISNULL(@CreatedDate,CreatedDate),
      CreatedBy = ISNULL(@CreatedBy,CreatedBy),
      UpdatedDate = ISNULL(@UpdatedDate,UpdatedDate),
      UpdatedBy = ISNULL(@UpdatedBy,UpdatedBy)
      where SessionKey =@SessionKey
  end
GO
grant exec on [dbo].[PB_EventSession_Update] TO siteuser,db_spexecute
GO
USE [Community]
GO
/****** Object:  StoredProcedure [dbo].[PB_EventSession_Delete]    Script Date: 2016-01-30 14:20:17 ******/
PRINT '----- create PROCEDURE  StoredProcedure [dbo].[PB_EventSession_Delete]    Script Date: 2016-01-30 14:20:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_EventSession_Delete]') AND type in (N'P', N'PC') ) 
   DROP PROCEDURE [dbo].[PB_EventSession_Delete]
GO
  Create Procedure [dbo].[PB_EventSession_Delete]
  (
        @SessionKey uniqueidentifier
  )
  as
  begin
      delete from [dbo].[PB_EventSession] 
      where SessionKey =@SessionKey
  end
GO
grant exec on [dbo].[PB_EventSession_Delete] TO siteuser,db_spexecute
GO
USE [Community]
GO
/****** Object:  Table [dbo].[PB_EventSession_History]    Script Date: 2016-01-30 14:20:17 ******/
PRINT '----- create Table  [dbo].[PB_EventSession_History]    Script Date: 2016-01-30 14:20:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_EventSession_History]') AND type in (N'U') ) 
   DROP TABLE [dbo].[PB_EventSession_History]
GO
  Create TABLE [dbo].[PB_EventSession_History]
  (
         HistoryId bigint primary key identity(1,1) not null,
        SessionKey uniqueidentifier not null,
        EventKey uniqueidentifier not null,
        DisplayOrder smallint not null,
        SessionStartDate datetime not null,
        SessionEndDate datetime not null,
        CanApply bit null,
        TicketOut bit null,
        VIPTicketQuantity int not null,
        NormalTicketQuantity int not null,
        CreatedDate datetime not null,
        CreatedBy nvarchar(100) not null,
        UpdatedDate datetime null,
        UpdatedBy nvarchar(100) null,
         AlteredBy nvarchar(50) null,
         AlteredType char(1) null,
         AlteredWhen datetime null
  )
GO
USE [Community]
GO
/****** Object:  Trigger [dbo].[PB_EventSession_Trigger_Update]    Script Date: 2016-01-30 14:20:17 ******/
PRINT '----- create Trigger   [dbo].[PB_EventSession_Trigger_Update]    Script Date: 2016-01-30 14:20:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[PB_EventSession_Trigger_Update]')) 
   DROP TRIGGER [dbo].[PB_EventSession_Trigger_Update]
GO
  Create TRIGGER [dbo].[PB_EventSession_Trigger_Update] on [dbo].[PB_EventSession]
  for UPDATE
  as
  begin
  insert into [dbo].[PB_EventSession_History]
  (
        SessionKey,
        EventKey,
        DisplayOrder,
        SessionStartDate,
        SessionEndDate,
        CanApply,
        TicketOut,
        VIPTicketQuantity,
        NormalTicketQuantity,
        CreatedDate,
        CreatedBy,
        UpdatedDate,
        UpdatedBy,
        AlteredBy,
        AlteredType,
        AlteredWhen
  )
  select
          SessionKey,
          EventKey,
          DisplayOrder,
          SessionStartDate,
          SessionEndDate,
          CanApply,
          TicketOut,
          VIPTicketQuantity,
          NormalTicketQuantity,
          CreatedDate,
          CreatedBy,
          UpdatedDate,
          UpdatedBy,
          HOST_NAME(),
          'U',
          getdate()
          from   deleted
  end
GO
USE [Community]
GO
/****** Object:  Trigger [dbo].[PB_EventSession_Trigger_Delete]    Script Date: 2016-01-30 14:20:17 ******/
PRINT '----- create Trigger   [dbo].[PB_EventSession_Trigger_Delete]    Script Date: 2016-01-30 14:20:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[PB_EventSession_Trigger_Delete]')) 
   DROP TRIGGER [dbo].[PB_EventSession_Trigger_Delete]
GO
  Create TRIGGER [dbo].[PB_EventSession_Trigger_Delete] on [dbo].[PB_EventSession]
  for delete
  as
  begin
  insert into [dbo].[PB_EventSession_History]
  (
        SessionKey,
        EventKey,
        DisplayOrder,
        SessionStartDate,
        SessionEndDate,
        CanApply,
        TicketOut,
        VIPTicketQuantity,
        NormalTicketQuantity,
        CreatedDate,
        CreatedBy,
        UpdatedDate,
        UpdatedBy,
        AlteredBy,
        AlteredType,
        AlteredWhen
  )
  select
          SessionKey,
          EventKey,
          DisplayOrder,
          SessionStartDate,
          SessionEndDate,
          CanApply,
          TicketOut,
          VIPTicketQuantity,
          NormalTicketQuantity,
          CreatedDate,
          CreatedBy,
          UpdatedDate,
          UpdatedBy,
          HOST_NAME(),
          'U',
          getdate()
          from   deleted
  end
GO
USE [Community]
GO
/****** Object:  StoredProcedure [dbo].[PB_EventTicketSetting_Insert]    Script Date: 2016-01-30 14:20:17 ******/
PRINT '----- create PROCEDURE  StoredProcedure [dbo].[PB_EventTicketSetting_Insert]    Script Date: 2016-01-30 14:20:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_EventTicketSetting_Insert]') AND type in (N'P', N'PC') ) 
   DROP PROCEDURE [dbo].[PB_EventTicketSetting_Insert]
GO
  Create Procedure [dbo].[PB_EventTicketSetting_Insert]
  (
        @Id int,
        @EventKey uniqueidentifier,
        @TicketQuantityPerSession int,
        @ApplyTicketTotal int,
        @VolunteerNormalTicketCountPerPerson int,
        @VolunteerVIPTicketCountPerPerson int,
        @CreatedDate datetime,
        @CreatedBy nvarchar(100),
        @UpdatedDate datetime,
        @UpdatedBy nvarchar(100) = null
  )
  as
  begin
      insert into [dbo].[PB_EventTicketSetting] 
      (
          EventKey,
          TicketQuantityPerSession,
          ApplyTicketTotal,
          VolunteerNormalTicketCountPerPerson,
          VolunteerVIPTicketCountPerPerson,
          CreatedDate,
          CreatedBy,
          UpdatedDate,
          UpdatedBy
      )
      values
      (
          @EventKey,
          @TicketQuantityPerSession,
          @ApplyTicketTotal,
          @VolunteerNormalTicketCountPerPerson,
          @VolunteerVIPTicketCountPerPerson,
          @CreatedDate,
          @CreatedBy,
          @UpdatedDate,
          @UpdatedBy
      )
  end
GO
grant exec on [dbo].[PB_EventTicketSetting_Insert] TO siteuser,db_spexecute
GO
USE [Community]
GO
/****** Object:  StoredProcedure [dbo].[PB_EventTicketSetting_Update]    Script Date: 2016-01-30 14:20:17 ******/
PRINT '----- create PROCEDURE  StoredProcedure [dbo].[PB_EventTicketSetting_Update]    Script Date: 2016-01-30 14:20:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_EventTicketSetting_Update]') AND type in (N'P', N'PC') ) 
   DROP PROCEDURE [dbo].[PB_EventTicketSetting_Update]
GO
  Create Procedure [dbo].[PB_EventTicketSetting_Update]
  (
        @Id int,
        @EventKey uniqueidentifier = null ,
        @TicketQuantityPerSession int = null ,
        @ApplyTicketTotal int = null ,
        @VolunteerNormalTicketCountPerPerson int = null ,
        @VolunteerVIPTicketCountPerPerson int = null ,
        @CreatedDate datetime = null ,
        @CreatedBy nvarchar(100) = null ,
        @UpdatedDate datetime = null ,
        @UpdatedBy nvarchar(100) = null 
  )
  as
  begin
      update [dbo].[PB_EventTicketSetting] set
      EventKey = ISNULL(@EventKey,EventKey),
      TicketQuantityPerSession = ISNULL(@TicketQuantityPerSession,TicketQuantityPerSession),
      ApplyTicketTotal = ISNULL(@ApplyTicketTotal,ApplyTicketTotal),
      VolunteerNormalTicketCountPerPerson = ISNULL(@VolunteerNormalTicketCountPerPerson,VolunteerNormalTicketCountPerPerson),
      VolunteerVIPTicketCountPerPerson = ISNULL(@VolunteerVIPTicketCountPerPerson,VolunteerVIPTicketCountPerPerson),
      CreatedDate = ISNULL(@CreatedDate,CreatedDate),
      CreatedBy = ISNULL(@CreatedBy,CreatedBy),
      UpdatedDate = ISNULL(@UpdatedDate,UpdatedDate),
      UpdatedBy = ISNULL(@UpdatedBy,UpdatedBy)
      where Id =@Id
  end
GO
grant exec on [dbo].[PB_EventTicketSetting_Update] TO siteuser,db_spexecute
GO
USE [Community]
GO
/****** Object:  StoredProcedure [dbo].[PB_EventTicketSetting_Delete]    Script Date: 2016-01-30 14:20:17 ******/
PRINT '----- create PROCEDURE  StoredProcedure [dbo].[PB_EventTicketSetting_Delete]    Script Date: 2016-01-30 14:20:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_EventTicketSetting_Delete]') AND type in (N'P', N'PC') ) 
   DROP PROCEDURE [dbo].[PB_EventTicketSetting_Delete]
GO
  Create Procedure [dbo].[PB_EventTicketSetting_Delete]
  (
        @Id int
  )
  as
  begin
      delete from [dbo].[PB_EventTicketSetting] 
      where Id =@Id
  end
GO
grant exec on [dbo].[PB_EventTicketSetting_Delete] TO siteuser,db_spexecute
GO
USE [Community]
GO
/****** Object:  Table [dbo].[PB_EventTicketSetting_History]    Script Date: 2016-01-30 14:20:17 ******/
PRINT '----- create Table  [dbo].[PB_EventTicketSetting_History]    Script Date: 2016-01-30 14:20:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_EventTicketSetting_History]') AND type in (N'U') ) 
   DROP TABLE [dbo].[PB_EventTicketSetting_History]
GO
  Create TABLE [dbo].[PB_EventTicketSetting_History]
  (
         HistoryId bigint primary key identity(1,1) not null,
        Id int not null,
        EventKey uniqueidentifier not null,
        TicketQuantityPerSession int not null,
        ApplyTicketTotal int not null,
        VolunteerNormalTicketCountPerPerson int null,
        VolunteerVIPTicketCountPerPerson int null,
        CreatedDate datetime not null,
        CreatedBy nvarchar(100) not null,
        UpdatedDate datetime null,
        UpdatedBy nvarchar(100) null,
         AlteredBy nvarchar(50) null,
         AlteredType char(1) null,
         AlteredWhen datetime null
  )
GO
USE [Community]
GO
/****** Object:  Trigger [dbo].[PB_EventTicketSetting_Trigger_Update]    Script Date: 2016-01-30 14:20:17 ******/
PRINT '----- create Trigger   [dbo].[PB_EventTicketSetting_Trigger_Update]    Script Date: 2016-01-30 14:20:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[PB_EventTicketSetting_Trigger_Update]')) 
   DROP TRIGGER [dbo].[PB_EventTicketSetting_Trigger_Update]
GO
  Create TRIGGER [dbo].[PB_EventTicketSetting_Trigger_Update] on [dbo].[PB_EventTicketSetting]
  for UPDATE
  as
  begin
  insert into [dbo].[PB_EventTicketSetting_History]
  (
        Id,
        EventKey,
        TicketQuantityPerSession,
        ApplyTicketTotal,
        VolunteerNormalTicketCountPerPerson,
        VolunteerVIPTicketCountPerPerson,
        CreatedDate,
        CreatedBy,
        UpdatedDate,
        UpdatedBy,
        AlteredBy,
        AlteredType,
        AlteredWhen
  )
  select
          Id,
          EventKey,
          TicketQuantityPerSession,
          ApplyTicketTotal,
          VolunteerNormalTicketCountPerPerson,
          VolunteerVIPTicketCountPerPerson,
          CreatedDate,
          CreatedBy,
          UpdatedDate,
          UpdatedBy,
          HOST_NAME(),
          'U',
          getdate()
          from   deleted
  end
GO
USE [Community]
GO
/****** Object:  Trigger [dbo].[PB_EventTicketSetting_Trigger_Delete]    Script Date: 2016-01-30 14:20:17 ******/
PRINT '----- create Trigger   [dbo].[PB_EventTicketSetting_Trigger_Delete]    Script Date: 2016-01-30 14:20:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[PB_EventTicketSetting_Trigger_Delete]')) 
   DROP TRIGGER [dbo].[PB_EventTicketSetting_Trigger_Delete]
GO
  Create TRIGGER [dbo].[PB_EventTicketSetting_Trigger_Delete] on [dbo].[PB_EventTicketSetting]
  for delete
  as
  begin
  insert into [dbo].[PB_EventTicketSetting_History]
  (
        Id,
        EventKey,
        TicketQuantityPerSession,
        ApplyTicketTotal,
        VolunteerNormalTicketCountPerPerson,
        VolunteerVIPTicketCountPerPerson,
        CreatedDate,
        CreatedBy,
        UpdatedDate,
        UpdatedBy,
        AlteredBy,
        AlteredType,
        AlteredWhen
  )
  select
          Id,
          EventKey,
          TicketQuantityPerSession,
          ApplyTicketTotal,
          VolunteerNormalTicketCountPerPerson,
          VolunteerVIPTicketCountPerPerson,
          CreatedDate,
          CreatedBy,
          UpdatedDate,
          UpdatedBy,
          HOST_NAME(),
          'U',
          getdate()
          from   deleted
  end
GO
USE [Community]
GO
/****** Object:  StoredProcedure [dbo].[PB_Ticket_Insert]    Script Date: 2016-01-30 14:20:17 ******/
PRINT '----- create PROCEDURE  StoredProcedure [dbo].[PB_Ticket_Insert]    Script Date: 2016-01-30 14:20:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_Ticket_Insert]') AND type in (N'P', N'PC') ) 
   DROP PROCEDURE [dbo].[PB_Ticket_Insert]
GO
  Create Procedure [dbo].[PB_Ticket_Insert]
  (
        @TicketKey uniqueidentifier,
        @MappingKey uniqueidentifier,
        @EventKey uniqueidentifier,
        @SessionKey uniqueidentifier,
        @CustomerKey uniqueidentifier,
        @TicketType smallint,
        @TicketFrom smallint,
        @TicketStatus smallint,
        @SMSToken varchar(10),
        @SessionStartDate datetime,
        @SessionEndDate datetime,
        @CheckinDate datetime,
        @CreatedDate datetime,
        @CreatedBy nvarchar(100),
        @UpdatedDate datetime,
        @UpdatedBy nvarchar(100) = null
  )
  as
  begin
      insert into [dbo].[PB_Ticket] 
      (
          TicketKey,
          MappingKey,
          EventKey,
          SessionKey,
          CustomerKey,
          TicketType,
          TicketFrom,
          TicketStatus,
          SMSToken,
          SessionStartDate,
          SessionEndDate,
          CheckinDate,
          CreatedDate,
          CreatedBy,
          UpdatedDate,
          UpdatedBy
      )
      values
      (
          @TicketKey,
          @MappingKey,
          @EventKey,
          @SessionKey,
          @CustomerKey,
          @TicketType,
          @TicketFrom,
          @TicketStatus,
          @SMSToken,
          @SessionStartDate,
          @SessionEndDate,
          @CheckinDate,
          @CreatedDate,
          @CreatedBy,
          @UpdatedDate,
          @UpdatedBy
      )
  end
GO
grant exec on [dbo].[PB_Ticket_Insert] TO siteuser,db_spexecute
GO
USE [Community]
GO
/****** Object:  StoredProcedure [dbo].[PB_Ticket_Update]    Script Date: 2016-01-30 14:20:17 ******/
PRINT '----- create PROCEDURE  StoredProcedure [dbo].[PB_Ticket_Update]    Script Date: 2016-01-30 14:20:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_Ticket_Update]') AND type in (N'P', N'PC') ) 
   DROP PROCEDURE [dbo].[PB_Ticket_Update]
GO
  Create Procedure [dbo].[PB_Ticket_Update]
  (
        @TicketKey uniqueidentifier,
        @MappingKey uniqueidentifier = null ,
        @EventKey uniqueidentifier = null ,
        @SessionKey uniqueidentifier = null ,
        @CustomerKey uniqueidentifier = null ,
        @TicketType smallint = null ,
        @TicketFrom smallint = null ,
        @TicketStatus smallint = null ,
        @SMSToken varchar(10) = null ,
        @SessionStartDate datetime = null ,
        @SessionEndDate datetime = null ,
        @CheckinDate datetime = null ,
        @CreatedDate datetime = null ,
        @CreatedBy nvarchar(100) = null ,
        @UpdatedDate datetime = null ,
        @UpdatedBy nvarchar(100) = null 
  )
  as
  begin
      update [dbo].[PB_Ticket] set
      MappingKey = ISNULL(@MappingKey,MappingKey),
      EventKey = ISNULL(@EventKey,EventKey),
      SessionKey = ISNULL(@SessionKey,SessionKey),
      CustomerKey = ISNULL(@CustomerKey,CustomerKey),
      TicketType = ISNULL(@TicketType,TicketType),
      TicketFrom = ISNULL(@TicketFrom,TicketFrom),
      TicketStatus = ISNULL(@TicketStatus,TicketStatus),
      SMSToken = ISNULL(@SMSToken,SMSToken),
      SessionStartDate = ISNULL(@SessionStartDate,SessionStartDate),
      SessionEndDate = ISNULL(@SessionEndDate,SessionEndDate),
      CheckinDate = ISNULL(@CheckinDate,CheckinDate),
      CreatedDate = ISNULL(@CreatedDate,CreatedDate),
      CreatedBy = ISNULL(@CreatedBy,CreatedBy),
      UpdatedDate = ISNULL(@UpdatedDate,UpdatedDate),
      UpdatedBy = ISNULL(@UpdatedBy,UpdatedBy)
      where TicketKey =@TicketKey
  end
GO
grant exec on [dbo].[PB_Ticket_Update] TO siteuser,db_spexecute
GO
USE [Community]
GO
/****** Object:  StoredProcedure [dbo].[PB_Ticket_Delete]    Script Date: 2016-01-30 14:20:17 ******/
PRINT '----- create PROCEDURE  StoredProcedure [dbo].[PB_Ticket_Delete]    Script Date: 2016-01-30 14:20:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_Ticket_Delete]') AND type in (N'P', N'PC') ) 
   DROP PROCEDURE [dbo].[PB_Ticket_Delete]
GO
  Create Procedure [dbo].[PB_Ticket_Delete]
  (
        @TicketKey uniqueidentifier
  )
  as
  begin
      delete from [dbo].[PB_Ticket] 
      where TicketKey =@TicketKey
  end
GO
grant exec on [dbo].[PB_Ticket_Delete] TO siteuser,db_spexecute
GO
USE [Community]
GO
/****** Object:  Table [dbo].[PB_Ticket_History]    Script Date: 2016-01-30 14:20:17 ******/
PRINT '----- create Table  [dbo].[PB_Ticket_History]    Script Date: 2016-01-30 14:20:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_Ticket_History]') AND type in (N'U') ) 
   DROP TABLE [dbo].[PB_Ticket_History]
GO
  Create TABLE [dbo].[PB_Ticket_History]
  (
         HistoryId bigint primary key identity(1,1) not null,
        TicketKey uniqueidentifier not null,
        MappingKey uniqueidentifier not null,
        EventKey uniqueidentifier not null,
        SessionKey uniqueidentifier not null,
        CustomerKey uniqueidentifier null,
        TicketType smallint not null,
        TicketFrom smallint null,
        TicketStatus smallint not null,
        SMSToken varchar(10) not null,
        SessionStartDate datetime not null,
        SessionEndDate datetime not null,
        CheckinDate datetime null,
        CreatedDate datetime not null,
        CreatedBy nvarchar(100) not null,
        UpdatedDate datetime null,
        UpdatedBy nvarchar(100) null,
         AlteredBy nvarchar(50) null,
         AlteredType char(1) null,
         AlteredWhen datetime null
  )
GO
USE [Community]
GO
/****** Object:  Trigger [dbo].[PB_Ticket_Trigger_Update]    Script Date: 2016-01-30 14:20:17 ******/
PRINT '----- create Trigger   [dbo].[PB_Ticket_Trigger_Update]    Script Date: 2016-01-30 14:20:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[PB_Ticket_Trigger_Update]')) 
   DROP TRIGGER [dbo].[PB_Ticket_Trigger_Update]
GO
  Create TRIGGER [dbo].[PB_Ticket_Trigger_Update] on [dbo].[PB_Ticket]
  for UPDATE
  as
  begin
  insert into [dbo].[PB_Ticket_History]
  (
        TicketKey,
        MappingKey,
        EventKey,
        SessionKey,
        CustomerKey,
        TicketType,
        TicketFrom,
        TicketStatus,
        SMSToken,
        SessionStartDate,
        SessionEndDate,
        CheckinDate,
        CreatedDate,
        CreatedBy,
        UpdatedDate,
        UpdatedBy,
        AlteredBy,
        AlteredType,
        AlteredWhen
  )
  select
          TicketKey,
          MappingKey,
          EventKey,
          SessionKey,
          CustomerKey,
          TicketType,
          TicketFrom,
          TicketStatus,
          SMSToken,
          SessionStartDate,
          SessionEndDate,
          CheckinDate,
          CreatedDate,
          CreatedBy,
          UpdatedDate,
          UpdatedBy,
          HOST_NAME(),
          'U',
          getdate()
          from   deleted
  end
GO
USE [Community]
GO
/****** Object:  Trigger [dbo].[PB_Ticket_Trigger_Delete]    Script Date: 2016-01-30 14:20:17 ******/
PRINT '----- create Trigger   [dbo].[PB_Ticket_Trigger_Delete]    Script Date: 2016-01-30 14:20:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[PB_Ticket_Trigger_Delete]')) 
   DROP TRIGGER [dbo].[PB_Ticket_Trigger_Delete]
GO
  Create TRIGGER [dbo].[PB_Ticket_Trigger_Delete] on [dbo].[PB_Ticket]
  for delete
  as
  begin
  insert into [dbo].[PB_Ticket_History]
  (
        TicketKey,
        MappingKey,
        EventKey,
        SessionKey,
        CustomerKey,
        TicketType,
        TicketFrom,
        TicketStatus,
        SMSToken,
        SessionStartDate,
        SessionEndDate,
        CheckinDate,
        CreatedDate,
        CreatedBy,
        UpdatedDate,
        UpdatedBy,
        AlteredBy,
        AlteredType,
        AlteredWhen
  )
  select
          TicketKey,
          MappingKey,
          EventKey,
          SessionKey,
          CustomerKey,
          TicketType,
          TicketFrom,
          TicketStatus,
          SMSToken,
          SessionStartDate,
          SessionEndDate,
          CheckinDate,
          CreatedDate,
          CreatedBy,
          UpdatedDate,
          UpdatedBy,
          HOST_NAME(),
          'U',
          getdate()
          from   deleted
  end
GO


/****** Object:  StoredProcedure [dbo].[PB_OfflineCustomer_Insert]    Script Date: 2016-02-22 12:47:19 ******/
PRINT '----- create PROCEDURE  StoredProcedure [dbo].[PB_OfflineCustomer_Insert]    Script Date: 2016-02-22 12:47:19------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_OfflineCustomer_Insert]') AND type in (N'P', N'PC') ) 
   DROP PROCEDURE [dbo].[PB_OfflineCustomer_Insert]
GO
  Create Procedure [dbo].[PB_OfflineCustomer_Insert]
  (
        @CustomerKey uniqueidentifier,
        @CustomerName nvarchar(40),
        @ContactType nvarchar(40),
        @ContactInfo nvarchar(100),
        @DirectSellerId varchar(12),
        @EventKey varchar(50),
        @AgeRange varchar(50) = null,
        @IsHearMaryKay bit,
        @InterestingTopic nvarchar(200) = null,
        @CustomerType smallint,
        @Career varchar(50) = null,
        @IsJoinEvent int,
        @CustomerResponse int,
        @UsedProduct bit,
        @BestContactDate int,
        @AdviceContactDate int,
        @Province varchar(50) = null,
        @City varchar(50) = null,
        @County varchar(50) = null,
        @IsImport bit,
        @CreatedDate datetime,
        @CreatedBy nvarchar(100) = null,
        @UpdatedDate datetime,
        @UpdatedBy nvarchar(100) = null
  )
  as
  begin
      insert into [dbo].[PB_OfflineCustomer] 
      (
          CustomerKey,
          CustomerName,
          ContactType,
          ContactInfo,
          DirectSellerId,
          EventKey,
          AgeRange,
          IsHearMaryKay,
          InterestingTopic,
          CustomerType,
          Career,
          IsJoinEvent,
          CustomerResponse,
          UsedProduct,
          BestContactDate,
          AdviceContactDate,
          Province,
          City,
          County,
          IsImport,
          CreatedDate,
          CreatedBy,
          UpdatedDate,
          UpdatedBy
      )
      values
      (
          @CustomerKey,
          @CustomerName,
          @ContactType,
          @ContactInfo,
          @DirectSellerId,
          @EventKey,
          @AgeRange,
          @IsHearMaryKay,
          @InterestingTopic,
          @CustomerType,
          @Career,
          @IsJoinEvent,
          @CustomerResponse,
          @UsedProduct,
          @BestContactDate,
          @AdviceContactDate,
          @Province,
          @City,
          @County,
          @IsImport,
          @CreatedDate,
          @CreatedBy,
          @UpdatedDate,
          @UpdatedBy
      )
  end
GO
grant exec on [dbo].[PB_OfflineCustomer_Insert] to db_spexecute,siteuser
GO

USE [Community]
GO
/****** Object:  StoredProcedure [dbo].[PB_OfflineCustomer_Update]    Script Date: 2016-02-22 12:47:19 ******/
PRINT '----- create PROCEDURE  StoredProcedure [dbo].[PB_OfflineCustomer_Update]    Script Date: 2016-02-22 12:47:19------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_OfflineCustomer_Update]') AND type in (N'P', N'PC') ) 
   DROP PROCEDURE [dbo].[PB_OfflineCustomer_Update]
GO
  Create Procedure [dbo].[PB_OfflineCustomer_Update]
  (
        @CustomerKey uniqueidentifier,
        @CustomerName nvarchar(40) = null ,
        @ContactType nvarchar(40) = null ,
        @ContactInfo nvarchar(100) = null ,
        @DirectSellerId varchar(12) = null ,
        @EventKey varchar(50) = null ,
        @AgeRange varchar(50) = null ,
        @IsHearMaryKay bit = null ,
        @InterestingTopic nvarchar(200) = null ,
        @CustomerType smallint = null ,
        @Career varchar(50) = null ,
        @IsJoinEvent int = null ,
        @CustomerResponse int = null ,
        @UsedProduct bit = null ,
        @BestContactDate int = null ,
        @AdviceContactDate int = null ,
        @Province varchar(50) = null ,
        @City varchar(50) = null ,
        @County varchar(50) = null ,
        @IsImport bit = null ,
        @CreatedDate datetime = null ,
        @CreatedBy nvarchar(100) = null ,
        @UpdatedDate datetime = null ,
        @UpdatedBy nvarchar(100) = null 
  )
  as
  begin
      update [dbo].[PB_OfflineCustomer] set
      CustomerName = ISNULL(@CustomerName,CustomerName),
      ContactType = ISNULL(@ContactType,ContactType),
      ContactInfo = ISNULL(@ContactInfo,ContactInfo),
      DirectSellerId = ISNULL(@DirectSellerId,DirectSellerId),
      EventKey = ISNULL(@EventKey,EventKey),
      AgeRange = ISNULL(@AgeRange,AgeRange),
      IsHearMaryKay = ISNULL(@IsHearMaryKay,IsHearMaryKay),
      InterestingTopic = ISNULL(@InterestingTopic,InterestingTopic),
      CustomerType = ISNULL(@CustomerType,CustomerType),
      Career = ISNULL(@Career,Career),
      IsJoinEvent = ISNULL(@IsJoinEvent,IsJoinEvent),
      CustomerResponse = ISNULL(@CustomerResponse,CustomerResponse),
      UsedProduct = ISNULL(@UsedProduct,UsedProduct),
      BestContactDate = ISNULL(@BestContactDate,BestContactDate),
      AdviceContactDate = ISNULL(@AdviceContactDate,AdviceContactDate),
      Province = ISNULL(@Province,Province),
      City = ISNULL(@City,City),
      County = ISNULL(@County,County),
      IsImport = ISNULL(@IsImport,IsImport),
      CreatedDate = ISNULL(@CreatedDate,CreatedDate),
      CreatedBy = ISNULL(@CreatedBy,CreatedBy),
      UpdatedDate = ISNULL(@UpdatedDate,UpdatedDate),
      UpdatedBy = ISNULL(@UpdatedBy,UpdatedBy)
      where CustomerKey =@CustomerKey
  end
GO
grant exec on [dbo].[PB_OfflineCustomer_Update] to db_spexecute,siteuser
GO
USE [Community]
GO
/****** Object:  StoredProcedure [dbo].[PB_OfflineCustomer_Delete]    Script Date: 2016-02-22 12:47:19 ******/
PRINT '----- create PROCEDURE  StoredProcedure [dbo].[PB_OfflineCustomer_Delete]    Script Date: 2016-02-22 12:47:19------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_OfflineCustomer_Delete]') AND type in (N'P', N'PC') ) 
   DROP PROCEDURE [dbo].[PB_OfflineCustomer_Delete]
GO
  Create Procedure [dbo].[PB_OfflineCustomer_Delete]
  (
        @CustomerKey uniqueidentifier
  )
  as
  begin
      delete from [dbo].[PB_OfflineCustomer] 
      where CustomerKey =@CustomerKey
  end
GO
grant exec on [dbo].[PB_OfflineCustomer_Delete] to db_spexecute,siteuser



USE [Community]
GO
/****** Object:  StoredProcedure [dbo].[PB_ConsultantSnapshot_Insert]    Script Date: 2016-03-02 13:04:17 ******/
PRINT '----- create PROCEDURE  StoredProcedure [dbo].[PB_ConsultantSnapshot_Insert]    Script Date: 2016-03-02 13:04:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_ConsultantSnapshot_Insert]') AND type in (N'P', N'PC') ) 
   DROP PROCEDURE [dbo].[PB_ConsultantSnapshot_Insert]
GO
  Create Procedure [dbo].[PB_ConsultantSnapshot_Insert]
  (
        @ID int,
        @MappingKey uniqueidentifier,
        @EventKey uniqueidentifier,
        @UserType smallint,
        @NormalTicketQuantity int,
        @VIPTicketQuantity int,
        @NormalTicketSettingQuantity int,
        @VIPTicketSettingQuantity int,
        @ContactId bigint,
        @DirectSellerId varchar(12),
        @FirstName nvarchar(100),
        @LastName nvarchar(100),
        @PhoneNumber varchar(20),
        @Level varchar(10),
        @ResidenceID varchar(18),
        @Province varchar(20) = null,
        @City varchar(20) = null,
        @OECountyId smallint,
        @CountyName nvarchar(40) = null,
        @CreatedDate datetime,
        @CreatedBy nvarchar(100),
        @UpdatedDate datetime,
        @UpdatedBy nvarchar(100) = null,
        @IsLiveAdd bit
  )
  as
  begin
      insert into [dbo].[PB_ConsultantSnapshot] 
      (
          MappingKey,
          EventKey,
          UserType,
          NormalTicketQuantity,
          VIPTicketQuantity,
          NormalTicketSettingQuantity,
          VIPTicketSettingQuantity,
          ContactId,
          DirectSellerId,
          FirstName,
          LastName,
          PhoneNumber,
          Level,
          ResidenceID,
          Province,
          City,
          OECountyId,
          CountyName,
          CreatedDate,
          CreatedBy,
          UpdatedDate,
          UpdatedBy,
          IsLiveAdd
      )
      values
      (
          @MappingKey,
          @EventKey,
          @UserType,
          @NormalTicketQuantity,
          @VIPTicketQuantity,
          @NormalTicketSettingQuantity,
          @VIPTicketSettingQuantity,
          @ContactId,
          @DirectSellerId,
          @FirstName,
          @LastName,
          @PhoneNumber,
          @Level,
          @ResidenceID,
          @Province,
          @City,
          @OECountyId,
          @CountyName,
          @CreatedDate,
          @CreatedBy,
          @UpdatedDate,
          @UpdatedBy,
          @IsLiveAdd
      )
  end
GO
grant exec on [dbo].[PB_ConsultantSnapshot_Insert] to db_spexecute,siteuser
GO
USE [Community]
GO
/****** Object:  StoredProcedure [dbo].[PB_ConsultantSnapshot_Update]    Script Date: 2016-03-02 13:04:17 ******/
PRINT '----- create PROCEDURE  StoredProcedure [dbo].[PB_ConsultantSnapshot_Update]    Script Date: 2016-03-02 13:04:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_ConsultantSnapshot_Update]') AND type in (N'P', N'PC') ) 
   DROP PROCEDURE [dbo].[PB_ConsultantSnapshot_Update]
GO
  Create Procedure [dbo].[PB_ConsultantSnapshot_Update]
  (
        @ID int,
        @MappingKey uniqueidentifier = null ,
        @EventKey uniqueidentifier = null ,
        @UserType smallint = null ,
        @NormalTicketQuantity int = null ,
        @VIPTicketQuantity int = null ,
        @NormalTicketSettingQuantity int = null ,
        @VIPTicketSettingQuantity int = null ,
        @ContactId bigint = null ,
        @DirectSellerId varchar(12) = null ,
        @FirstName nvarchar(100) = null ,
        @LastName nvarchar(100) = null ,
        @PhoneNumber varchar(20) = null ,
        @Level varchar(10) = null ,
        @ResidenceID varchar(18) = null ,
        @Province varchar(20) = null ,
        @City varchar(20) = null ,
        @OECountyId smallint = null ,
        @CountyName nvarchar(40) = null ,
        @CreatedDate datetime = null ,
        @CreatedBy nvarchar(100) = null ,
        @UpdatedDate datetime = null ,
        @UpdatedBy nvarchar(100) = null ,
        @IsLiveAdd bit = null 
  )
  as
  begin
      update [dbo].[PB_ConsultantSnapshot] set
      MappingKey = ISNULL(@MappingKey,MappingKey),
      EventKey = ISNULL(@EventKey,EventKey),
      UserType = ISNULL(@UserType,UserType),
      NormalTicketQuantity = ISNULL(@NormalTicketQuantity,NormalTicketQuantity),
      VIPTicketQuantity = ISNULL(@VIPTicketQuantity,VIPTicketQuantity),
      NormalTicketSettingQuantity = ISNULL(@NormalTicketSettingQuantity,NormalTicketSettingQuantity),
      VIPTicketSettingQuantity = ISNULL(@VIPTicketSettingQuantity,VIPTicketSettingQuantity),
      ContactId = ISNULL(@ContactId,ContactId),
      DirectSellerId = ISNULL(@DirectSellerId,DirectSellerId),
      FirstName = ISNULL(@FirstName,FirstName),
      LastName = ISNULL(@LastName,LastName),
      PhoneNumber = ISNULL(@PhoneNumber,PhoneNumber),
      Level = ISNULL(@Level,Level),
      ResidenceID = ISNULL(@ResidenceID,ResidenceID),
      Province = ISNULL(@Province,Province),
      City = ISNULL(@City,City),
      OECountyId = ISNULL(@OECountyId,OECountyId),
      CountyName = ISNULL(@CountyName,CountyName),
      CreatedDate = ISNULL(@CreatedDate,CreatedDate),
      CreatedBy = ISNULL(@CreatedBy,CreatedBy),
      UpdatedDate = ISNULL(@UpdatedDate,UpdatedDate),
      UpdatedBy = ISNULL(@UpdatedBy,UpdatedBy),
      IsLiveAdd = ISNULL(@IsLiveAdd,IsLiveAdd)
      where ID =@ID
  end
GO
grant exec on [dbo].[PB_ConsultantSnapshot_Update] to db_spexecute,siteuser
GO
USE [Community]
GO
/****** Object:  StoredProcedure [dbo].[PB_ConsultantSnapshot_Delete]    Script Date: 2016-03-02 13:04:17 ******/
PRINT '----- create PROCEDURE  StoredProcedure [dbo].[PB_ConsultantSnapshot_Delete]    Script Date: 2016-03-02 13:04:17------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_ConsultantSnapshot_Delete]') AND type in (N'P', N'PC') ) 
   DROP PROCEDURE [dbo].[PB_ConsultantSnapshot_Delete]
GO
  Create Procedure [dbo].[PB_ConsultantSnapshot_Delete]
  (
        @ID int
  )
  as
  begin
      delete from [dbo].[PB_ConsultantSnapshot] 
      where ID =@ID
  end
GO
grant exec on [dbo].[PB_ConsultantSnapshot_Delete] to db_spexecute,siteuser
GO


USE [Community]
GO
/****** Object:  StoredProcedure [dbo].[PB_VolunteerCheckin_Insert]    Script Date: 2016-03-02 13:17:42 ******/
PRINT '----- create PROCEDURE  StoredProcedure [dbo].[PB_VolunteerCheckin_Insert]    Script Date: 2016-03-02 13:17:42------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_VolunteerCheckin_Insert]') AND type in (N'P', N'PC') ) 
   DROP PROCEDURE [dbo].[PB_VolunteerCheckin_Insert]
GO
  Create Procedure [dbo].[PB_VolunteerCheckin_Insert]
  (
        @Key uniqueidentifier,
        @EventKey uniqueidentifier,
        @MappingKey uniqueidentifier,
        @CheckinDate datetime,
        @CreatedDate datetime,
        @CreatedBy nvarchar(100),
        @UpdatedDate datetime,
        @UpdatedBy nvarchar(100) = null
  )
  as
  begin
      insert into [dbo].[PB_VolunteerCheckin] 
      (
          [Key],
          EventKey,
          MappingKey,
          CheckinDate,
          CreatedDate,
          CreatedBy,
          UpdatedDate,
          UpdatedBy
      )
      values
      (
          @Key,
          @EventKey,
          @MappingKey,
          @CheckinDate,
          @CreatedDate,
          @CreatedBy,
          @UpdatedDate,
          @UpdatedBy
      )
  end
GO
grant exec on [dbo].[PB_VolunteerCheckin_Insert] to db_spexecute,siteuser
GO
USE [Community]
GO
/****** Object:  StoredProcedure [dbo].[PB_VolunteerCheckin_Update]    Script Date: 2016-03-02 13:17:42 ******/
PRINT '----- create PROCEDURE  StoredProcedure [dbo].[PB_VolunteerCheckin_Update]    Script Date: 2016-03-02 13:17:42------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_VolunteerCheckin_Update]') AND type in (N'P', N'PC') ) 
   DROP PROCEDURE [dbo].[PB_VolunteerCheckin_Update]
GO
  Create Procedure [dbo].[PB_VolunteerCheckin_Update]
  (
        @Key uniqueidentifier,
        @EventKey uniqueidentifier = null ,
        @MappingKey uniqueidentifier = null ,
        @CheckinDate datetime = null ,
        @CreatedDate datetime = null ,
        @CreatedBy nvarchar(100) = null ,
        @UpdatedDate datetime = null ,
        @UpdatedBy nvarchar(100) = null 
  )
  as
  begin
      update [dbo].[PB_VolunteerCheckin] set
      EventKey = ISNULL(@EventKey,EventKey),
      MappingKey = ISNULL(@MappingKey,MappingKey),
      CheckinDate = ISNULL(@CheckinDate,CheckinDate),
      CreatedDate = ISNULL(@CreatedDate,CreatedDate),
      CreatedBy = ISNULL(@CreatedBy,CreatedBy),
      UpdatedDate = ISNULL(@UpdatedDate,UpdatedDate),
      UpdatedBy = ISNULL(@UpdatedBy,UpdatedBy)
      where [Key] =@Key
  end
GO
grant exec on [dbo].[PB_VolunteerCheckin_Update] to db_spexecute,siteuser
GO
USE [Community]
GO
/****** Object:  StoredProcedure [dbo].[PB_VolunteerCheckin_Delete]    Script Date: 2016-03-02 13:17:42 ******/
PRINT '----- create PROCEDURE  StoredProcedure [dbo].[PB_VolunteerCheckin_Delete]    Script Date: 2016-03-02 13:17:42------------- '
GO
IF EXISTS ( SELECT  *  FROM  sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PB_VolunteerCheckin_Delete]') AND type in (N'P', N'PC') ) 
   DROP PROCEDURE [dbo].[PB_VolunteerCheckin_Delete]
GO
  Create Procedure [dbo].[PB_VolunteerCheckin_Delete]
  (
        @Key uniqueidentifier
  )
  as
  begin
      delete from [dbo].[PB_VolunteerCheckin] 
      where [Key] =@Key
  end
GO
grant exec on [dbo].[PB_VolunteerCheckin_Delete] to db_spexecute,siteuser
GO