USE [Community]
GO

/****** Object:  StoredProcedure [dbo].[iparty_paperAddCustomer]    Script Date: 11/12/2015 14:22:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[iparty_paperAddCustomer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[iparty_paperAddCustomer]
GO

USE [Community]
GO

/****** Object:  StoredProcedure [dbo].[iparty_paperAddCustomer]    Script Date: 11/12/2015 14:22:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[iparty_paperAddCustomer]
(
	@PartyKey uniqueidentifier,
	@Name varchar(10),
	@PhoneNumber varchar(20),
	@ReferenceId varchar(20),
	@CheckInDate datetime,
	@CreateBy varchar(50)
)
as
begin
	
	if not exists(select 1 from iParty_PartyApplication (nolock) where PartyKey=@PartyKey)
		begin
			  select -2
			  return --noparty
		end
	
	if exists(select 1 from iParty_Invitation where PhoneNumber=@PhoneNumber and Status!='03')
		begin
			select -3
			return  --hasPhone
		end
	
	declare @ReferenceKey varchar(20) = null
	declare @Reference varchar(20) = null
	declare @ReferenceType varchar(5) = 0
	declare @OwnerContactID varchar(20)
	declare @OwnerUnitID varchar(20)
	declare @InvitationKey uniqueidentifier = newid()
	declare @CustomerKey uniqueidentifier = newid()
	declare @IsVIP bit =0
	declare @Inviter varchar(20)
	declare @InviterName varchar(20)
	declare @Level varchar(5)
	declare @CustomerContactId varchar(20)
	
	
	select @Inviter = p.AppliedContactID,@InviterName = c.LastName+c.FirstName from iParty_PartyApplication p (nolock)
	join ContactsLite.dbo.Consultants c (nolock) on p.AppliedContactID=c.ContactID
	
	
	if(@ReferenceId !='' )
	begin
		select @ReferenceKey=c.ContactID,@Reference=c.LastName+c.FirstName,
		@OwnerContactID=c.ContactID,@OwnerUnitID=c.UnitID
		from ContactsLite.dbo.InternationalConsultants i (nolock) 
		join ContactsLite.dbo.Consultants c (nolock) on i.ContactID=c.ContactID
		where DirectSellerID = @ReferenceId
	end
	
	if exists(select 1 from ContactsLite.dbo.PhoneNumbers p  (nolock)
			  join ContactsLite.dbo.Consultants c (nolock) on p.ContactID=c.ContactID 
			  where c.ConsultantStatus not like '%x%' and p.PhoneNumber=@PhoneNumber and PhoneNumberTypeID=5
			 )
	begin
		
		set @ReferenceType=1
		select 
		@CustomerContactId=c.ContactID,
		@Level = c.ConsultantLevelID,
		@OwnerContactID = (case when Recruiter is not null then re.Recruiter when Recruiter is null then di.Director else null end ),
		@OwnerUnitID = c.UnitID
		from ContactsLite.dbo.PhoneNumbers p  (nolock)
		join ContactsLite.dbo.Consultants c  (nolock) on p.ContactID=c.ContactID 
		left join (select cr.Consultant, cr.Recruiter,c.UnitID from ContactsLite.dbo.Consultant_To_Recruiter cr (nolock)
					inner join  ContactsLite.dbo.Consultants c (nolock) on cr.Recruiter=c.ContactID) re on p.ContactID=re.Consultant
		left join (select cd.Consultant, cd.Director,c.UnitID from ContactsLite.dbo.Consultant_To_Director cd  (nolock)
					inner join  ContactsLite.dbo.Consultants c (nolock) on cd.Director=c.ContactID ) di on di.Consultant=p.ContactID
		
		where c.ConsultantStatus not like '%x%' and p.PhoneNumber=@PhoneNumber and PhoneNumberTypeID=5
		
		if(@Level<15)
		begin
			set @IsVIP=1
		end
		
	end
	
	
	BEGIN TRAN
	  
	insert into iParty_Invitation 
	(InvitationKey,PartyKey,CustomerKey,PhoneNumber,Reference,ReferenceKey,ReferenceType,Status,OwnerContactID,OwnerUnitID,CheckInType,CheckinDate)
	values 
	(@InvitationKey,@PartyKey,@CustomerKey,@PhoneNumber,@Reference,@ReferenceKey,@ReferenceType,'04',@OwnerContactID,@OwnerUnitID,'06',@CheckInDate)
	insert into IParty_Customer 
	(CustomerKey,Name,PhoneNumber,Career,AgeRange,IsVIP,CreatedBy,Reference,ReferenceKey,ReferenceType,Inviter,InviterName,UnionId,MaritalStatus,dtCreated,Level,CustomerContactId,HasOrder,IsNewer,IsPromoted,NextProcessTime)
	values
	(@CustomerKey,@Name,@PhoneNumber,null,null,@IsVIP,@CreateBy,@Reference,@ReferenceKey,@ReferenceType,@Inviter,@InviterName,null,null,@CheckInDate,@Level,@CustomerContactId,0,0,0,null)
	 
	 --print @InvitationKey
	--print @CustomerKey
	 IF ( @@ERROR <> 0 )       
            BEGIN      
                ROLLBACK TRAN      
               
                select -1    
            END      
        ELSE       
            BEGIN      
                COMMIT TRAN      
              
                select 1      
            END      
	
end


GO




grant exec on [dbo].iparty_paperAddCustomer to siteuser  
grant exec on [dbo].iparty_paperAddCustomer to db_spexecute
go




USE [Community]
GO

/****** Object:  StoredProcedure [dbo].[iparty_paperUpdateCustomer]    Script Date: 11/12/2015 14:25:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[iparty_paperUpdateCustomer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[iparty_paperUpdateCustomer]
GO

USE [Community]
GO

/****** Object:  StoredProcedure [dbo].[iparty_paperUpdateCustomer]    Script Date: 11/12/2015 14:25:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE proc [dbo].[iparty_paperUpdateCustomer]
(
	@PartyKey uniqueidentifier,
	@PhoneNumber varchar(20),
	@CheckInDate datetime
)
as
begin
	
	if not exists(select 1 from iParty_PartyApplication p join iParty_Invitation i on p.PartyKey=i.PartyKey  where p.PartyKey=@PartyKey and i.PhoneNumber=@PhoneNumber )
		begin
			  select -2
			  return --noparty
		end
	
	if exists(select 1 from iParty_Invitation where PhoneNumber=@PhoneNumber and (Status='04' or status ='01'))
		begin
			select -3
			return  --checked
		end
		
	
	BEGIN TRAN
	  
	  update iParty_Invitation set Status ='01' ,CheckInType='06',CheckinDate=@CheckInDate
	  where PartyKey=@PartyKey and PhoneNumber=@PhoneNumber
	
	 IF ( @@ERROR <> 0 )       
            BEGIN      
                ROLLBACK TRAN      
               
                select -1    
            END      
        ELSE       
            BEGIN      
                COMMIT TRAN      
              
                select 1      
            END      
	
end




GO


grant exec on dbo.[iparty_paperUpdateCustomer]to siteuser  
grant exec on dbo.[iparty_paperUpdateCustomer]to db_spexecute
go

