USE [Community]
GO

/****** Object:  StoredProcedure [dbo].[PB_ActivityCustomerSelect]    Script Date: 2016/1/26 14:57:59 ******/

PRINT '------ create PROCEDURE PB_ActivityCustomerSelect----------------- '
GO  

IF EXISTS ( SELECT  *
            FROM    dbo.sysobjects
            WHERE   id = OBJECT_ID(N'dbo.PB_ActivityCustomerSelect')
                    AND type = 'p' ) 
    DROP PROCEDURE dbo.[PB_ActivityCustomerSelect]
GO  

create procedure [dbo].[PB_ActivityCustomerSelect]
(
@eventKey uniqueidentifier
)as
set nocount on
begin
select top 500 case pt.TicketType
		 when 0 then '来宾券'
		 when 1 then '贵宾券' 
		 end as 'TicketType',
		 pc.CustomerKey,
		pt.SMSToken,
		pc.CustomerName,
		pc.CustomerPhone,
		pt.SessionStartDate
	into #temp
    from [dbo].[PB_Ticket]  as pt with(nolock)	
	inner join [dbo].[PB_Customer] as pc with(nolock)
	on pt.Customerkey=pc.Customerkey
where pt.EventKey =@eventKey and pt.TicketStatus=2
	and (pc.SMSStatus='failed' or  pc.SMSStatus is null)
	order by pc.CreatedDate

update [dbo].[PB_Customer] set SMSStatus='sending'
	where CustomerKey in (select CustomerKey from #temp)

select * from #temp
end

GO

GRANT EXEC ON dbo.PB_ActivityCustomerSelect TO siteuser,db_spexecute

GO



/****** Object:  StoredProcedure [dbo].[PB_CheckDuplicatePhoneNumber]    Script Date: 2016/1/26 15:02:06 ******/
PRINT '------ create PROCEDURE PB_CheckDuplicatePhoneNumber----------------- '
GO  

IF EXISTS ( SELECT  *
            FROM    dbo.sysobjects
            WHERE   id = OBJECT_ID(N'dbo.PB_CheckDuplicatePhoneNumber')
                    AND type = 'p' ) 
    DROP PROCEDURE dbo.[PB_CheckDuplicatePhoneNumber]
GO  

 CREATE PROCEDURE [dbo].[PB_CheckDuplicatePhoneNumber]          
 @PhoneNumber nvarchar(40),
 @EventKey uniqueidentifier
      
AS        
BEGIN  

SELECT 1 FROM Community..PB_Ticket (NOLOCK )t 
where TicketStatus=2 AND  t.EventKey=@EventKey
AND t.CustomerKey IN ( 
   SELECT CustomerKey 
   FROM Community..PB_Customer (NOLOCK) c
   WHERE c.CustomerPhone=@PhoneNumber  
)

END
GO
GRANT EXEC ON dbo.PB_CheckDuplicatePhoneNumber TO siteuser,db_spexecute

GO

/****** Object:  StoredProcedure [dbo].[PB_CheckDuplicatePhoneNumber]    Script Date: 2016/1/26 15:02:06 ******/
PRINT '------ create PROCEDURE PB_Consultant_TicketSetting_save----------------- '
GO  

IF EXISTS ( SELECT  *
            FROM    dbo.sysobjects
            WHERE   id = OBJECT_ID(N'dbo.PB_Consultant_TicketSetting_save')
                    AND type = 'p' ) 
    DROP PROCEDURE dbo.[PB_Consultant_TicketSetting_save]
GO  

CREATE Proc [dbo].[PB_Consultant_TicketSetting_save]
(
	@EventKey uniqueidentifier,
	@ApplyTicketTotal int,
	@TicketQuantityPerSession int,
	@NormalTicketQuantity int,
	@VIPTicketQuantity int,
	@VolunteerNormalTicketCountPerPerson int,
	@VolunteerVIPTicketCountPerPerson int,
	@NormalTicketSettingQuantity int,
	@VIPTicketSettingQuantity int,
	@CreatedDate datetime,
	@CreatedBy varchar(50),
	@EventSessionsXML xml,
	@EventConsultantsXML xml,
	@EventTicketsXML xml,
	@ImportType smallint
)
as

begin
begin tran
--set @EventSessionsXML='<root><s id="776fe222-b242-4370-bf16-19e22d385c04" /><s id="aed86501-e809-4e60-9980-6fe45c27cadc" /></root>'
--	set @EventConsultantsXML='<root><s MappingKey="3b187364-b88d-4a8f-bd61-96148bdc5719" EventKey="2ac4654b-65ee-4571-b030-45123aa9d0d9" UserType="0" ContactId="20002864083" DirectSellerID="310000000005" FirstName="炜6" LastName="孙" PhoneNumber="" Level="30" ResidenceID="000000000000000" Province="上海" City="上海" County="" NormalTicketQuantity="0" VIPTicketQuantity="0" CreatedDate="2015/12/10 10:14:58" CreatedBy="" /><s MappingKey="c0a7be8b-02da-4d67-8325-837570b481fb" EventKey="2ac4654b-65ee-4571-b030-45123aa9d0d9" UserType="0" ContactId="20002864084" DirectSellerID="310000000006" FirstName="平-" LastName="沈" PhoneNumber="" Level="30" ResidenceID="000000000000000" Province="上海" City="上海" County="" NormalTicketQuantity="0" VIPTicketQuantity="0" CreatedDate="2015/12/10 10:14:58" CreatedBy="" /><s MappingKey="4ff50084-15b4-4428-89d6-f4f61ea01c1f" EventKey="2ac4654b-65ee-4571-b030-45123aa9d0d9" UserType="0" ContactId="20002864085" DirectSellerID="310000000007" FirstName="洁清" LastName="任" PhoneNumber="" Level="30" ResidenceID="000000000000000" Province="上海" City="上海" County="" NormalTicketQuantity="0" VIPTicketQuantity="0" CreatedDate="2015/12/10 10:14:58" CreatedBy="" /><s MappingKey="c735afed-de0d-47c7-90e8-f2cf223bfe0e" EventKey="2ac4654b-65ee-4571-b030-45123aa9d0d9" UserType="0" ContactId="20002864086" DirectSellerID="310000000008" FirstName="贻清" LastName="胡" PhoneNumber="" Level="30" ResidenceID="310106690922082" Province="上海" City="上海" County="" NormalTicketQuantity="0" VIPTicketQuantity="0" CreatedDate="2015/12/10 10:14:58" CreatedBy="" /><s MappingKey="5c193975-8b24-498f-8423-8ea29a1cd45d" EventKey="2ac4654b-65ee-4571-b030-45123aa9d0d9" UserType="0" ContactId="20002864082" DirectSellerID="310000000004" FirstName="贻平" LastName="胡" PhoneNumber="" Level="30" ResidenceID="310109660604202" Province="上海" City="上海" County="" NormalTicketQuantity="0" VIPTicketQuantity="0" CreatedDate="2015/12/10 10:14:58" CreatedBy="" /></root>'
--	set @EventTicketsXML ='<root />'
	
	--创建sessionkey临时表（可抢时段）
	declare  @SessionIDTB table(t_SessionKey uniqueidentifier)
	
	--创建顾问临时表
	declare  @ConsultantsTB table(
	t_MappingKey uniqueidentifier,
	t_EventKey uniqueidentifier,
	t_UserType smallint,
	t_ContactId bigint,	
	t_DirectSellerId varchar(12),
	t_NormalTicketQuantity int,
	t_VIPTicketQuantity int,
	t_NormalTicketSettingQuantity int,
	t_VIPTicketSettingQuantity int,
	t_FirstName nvarchar(50),
	t_LastName nvarchar(50),
	t_PhoneNumber varchar(20),
	t_Level varchar(10),
	t_ResidenceID varchar(18),
	t_Province nvarchar(20),
	t_City nvarchar(20),
	t_CountyName nvarchar(20),
	t_CreatedDate datetime,
	t_CreatedBy varchar(50))
	
	--创建ticket临时表
	declare  @TicketsTB table(
	t_MappingKey uniqueidentifier,
	t_TicketKey uniqueidentifier,
	t_EventKey uniqueidentifier,
	t_SessionKey uniqueidentifier,
	t_SMSToken varchar(10),
	t_TicketType smallint,
	t_TicketFrom smallint,
	t_TicketStatus smallint,
	t_SessionStartDate datetime,
	t_SessionEndDate datetime,
	t_CreatedDate datetime,
	t_CreatedBy varchar(50))
	
	DECLARE @s int
	
	--@EventSessionsXML to @SessionIDTB
	EXEC sp_xml_preparedocument @s OUTPUT,@EventSessionsXML
	insert into  @SessionIDTB  
		SELECT * FROM  OPENXML (@s, '/root/s',8) WITH (id  uniqueidentifier)
		
	--select * from @SessionIDTB
	
	--@EventConsultantsXML to @ConsultantsTB
	EXEC sp_xml_preparedocument @s OUTPUT,@EventConsultantsXML
	insert into  @ConsultantsTB  
		SELECT * FROM  OPENXML (@s, '/root/s',8) 
		WITH (MappingKey  uniqueidentifier,
			EventKey uniqueidentifier,
			UserType smallint,
			ContactId bigint,
			DirectSellerID varchar(12),
			NormalTicketQuantity int,
			VIPTicketQuantity int,
			NormalTicketSettingQuantity int,
			VIPTicketSettingQuantity int,
			FirstName nvarchar(50),
			LastName nvarchar(50),
			PhoneNumber varchar(20),
			[Level] varchar(10),
			ResidenceID varchar(18),
			Province nvarchar(20),
			City nvarchar(20),
			County nvarchar(20),
			CreatedDate datetime,
			CreatedBy nvarchar(50)
			
		)
	
	--select * from @ConsultantsTB
	
	--@EventTicketsXML to @TicketsTB
	EXEC sp_xml_preparedocument @s OUTPUT,@EventTicketsXML
	insert into  @TicketsTB  
		SELECT * FROM  OPENXML (@s, '/root/s',8) 
		WITH (MappingKey  uniqueidentifier,
			TicketKey uniqueidentifier,
			EventKey uniqueidentifier,
			SessionKey uniqueidentifier,
			SMSToken varchar(10),
			TicketType smallint,
			TicketFrom smallint,
			TicketStatus smallint,
			SessionStartDate datetime,
			SessionEndDate datetime,
			CreatedDate datetime,
			CreatedBy varchar(50))
			
		--select * from @TicketsTB	
		
		--修改PB_EventSession 中可抢时段的canapply=1
		update PB_EventSession set CanApply=1 where SessionKey in (select t_sessionkey from @SessionIDTB)
		
		--select * from PB_EventSession
		
		
		--保存导入顾问到snapshot
		insert into [dbo].[PB_ConsultantSnapshot] 
		(MappingKey,EventKey,UserType,NormalTicketQuantity,VIPTicketQuantity,NormalTicketSettingQuantity,VIPTicketSettingQuantity,ContactId,
		DirectSellerId,FirstName,LastName,PhoneNumber,Level,ResidenceID,Province,City,
		CountyName,	CreatedDate,CreatedBy,IsLiveAdd)
		(select t_MappingKey,t_EventKey,t_UserType,t_NormalTicketQuantity,t_VIPTicketQuantity,t_NormalTicketSettingQuantity,t_VIPTicketSettingQuantity,
		t_ContactId,t_DirectSellerId,t_FirstName,t_LastName,t_PhoneNumber,t_Level,t_ResidenceID,
		t_Province,t_City,t_CountyName,t_CreatedDate,t_CreatedBy ,0
		from @ConsultantsTB)

		merge into [PB_Event-Consultant] as ec
		using @ConsultantsTB as c 
		on ec.MappingKey=c.t_MappingKey and ec.EventKey=c.t_EventKey
		when matched
		then update set ec.[UserType]=(case @ImportType when 1 then 3 when 2 then (case ec.[UserType] when 3 then 3 else 2 end) else 0 end)
					,ec.[VIPTicketSettingQuantity]=ec.[VIPTicketSettingQuantity]+c.t_VIPTicketSettingQuantity
					,ec.[NormalTicketSettingQuantity]=ec.[NormalTicketSettingQuantity]+c.t_NormalTicketSettingQuantity
					,ec.[VIPTicketQuantity]=ec.[VIPTicketQuantity]+c.t_VIPTicketQuantity
					,ec.[NormalTicketQuantity]=ec.[NormalTicketQuantity]+c.t_NormalTicketQuantity
					,ec.Province=(case @ImportType when 1 then c.t_Province  else ec.Province end)
					,ec.City=(case @ImportType when 1 then c.t_City    else ec.City end)
					,ec.CountyName=(case @ImportType when 1 then c.t_CountyName  else ec.CountyName end)
		when not matched
		then insert values(c.t_MappingKey
						  ,c.t_EventKey
						  ,c.t_UserType
						  ,c.t_NormalTicketQuantity
						  ,c.t_VIPTicketQuantity
						  ,c.t_NormalTicketSettingQuantity
						  ,c.t_VIPTicketSettingQuantity
						  ,c.t_ContactId
						  ,c.t_DirectSellerId
						  ,c.t_FirstName
						  ,c.t_LastName
						  ,c.t_PhoneNumber
						  ,c.t_Level
						  ,c.t_ResidenceID
						  ,c.t_Province
						  ,c.t_City
						  ,null
						  ,c.t_CountyName
						  ,c.t_CreatedDate
						  ,c.t_CreatedBy
						  ,null
						  ,null
						  ,0
						  ,null
						 );
		


		
		--保存顾问的ticket
		if exists(select 1 from @TicketsTB)
		begin
			insert into PB_Ticket 
			(TicketKey,MappingKey,EventKey,SessionKey,SMSToken,SessionStartDate,SessionEndDate, TicketType,TicketFrom,TicketStatus,CreatedDate,CreatedBy)
			(select t_TicketKey,t_MappingKey,t_EventKey,t_Sessionkey,t_SMSToken,t_SessionStartDate,t_SessionEndDate, t_TicketType,t_TicketFrom,t_TicketStatus,t_CreatedDate,t_CreatedBy
			 from @TicketsTB)
		end
		
		if(@ImportType=0)
		begin
			--保存抢票配置
			insert into PB_EventTicketSetting (EventKey,TicketQuantityPerSession,ApplyTicketTotal,CreatedDate,CreatedBy)
			values
			(@EventKey,@TicketQuantityPerSession,@ApplyTicketTotal,@CreatedDate,@CreatedBy)
			
			update PB_Event set BCImport=1 where EventKey=@EventKey
		end
		else if(@ImportType=1)
		begin
			update PB_Event set VolunteerImport=1 where EventKey=@EventKey
			--保存顾问票数配置
			update dbo.PB_EventTicketSetting set 
			VolunteerNormalTicketCountPerPerson=@VolunteerNormalTicketCountPerPerson,
			VolunteerVIPTicketCountPerPerson=@VolunteerVIPTicketCountPerPerson,
			UpdatedDate=@CreatedDate,
			UpdatedBy=@CreatedBy
			where EventKey=@EventKey
			
		end
		else if(@ImportType=2)
		begin
			update PB_Event set VIPImport=1 where EventKey=@EventKey
		end
			
		
if(@@error <>0)
		 begin
	     rollback tran
	     select -1
		 end
     else
		 begin
	     commit tran
	     select 1
		 end
end

GO

GRANT EXEC ON dbo.PB_Consultant_TicketSetting_save TO siteuser,db_spexecute

GO



/****** Object:  StoredProcedure [dbo].[PB_CheckDuplicatePhoneNumber]    Script Date: 2016/1/26 15:02:06 ******/
PRINT '------ create PROCEDURE PB_CheckDuplicatePhoneNumber----------------- '
GO  

IF EXISTS ( SELECT  *
            FROM    dbo.sysobjects
            WHERE   id = OBJECT_ID(N'dbo.PB_ConsultantInfo')
                    AND type = 'p' ) 
    DROP PROCEDURE dbo.[PB_ConsultantInfo]
GO  

CREATE proc [dbo].[PB_ConsultantInfo]  
(  
 @IdsXml xml,  
 @EventKey uniqueidentifier=null 
)  
as  
begin  
  
--处理sessionxml  
DECLARE @idoc int  
EXEC sp_xml_preparedocument @idoc OUTPUT,@IdsXml  
--创建session临时表  
declare  @IdsTB table(  
 t_id varchar(20) collate SQL_Latin1_General_CP1_CI_AS  
 )  
   
 --解析xml到临时表中  
 insert into @IdsTB   
  SELECT *  
    FROM  OPENXML (@idoc, '/root/s',8)  
     WITH (id  varchar(20) collate SQL_Latin1_General_CP1_CI_AS  
         
        )  
IF(@EventKey IS NOT NULL)
BEGIN
--select * from @IdsTB  
 select   
 i.DirectSellerID ,  
 i.ContactID,  
 c.LastName,  
 c.FirstName,  
 c.ConsultantLevelID as [Level],  
 i.ResidenceID,  
 a.Address4 as Province,  
 a.Address3 as City,  
 a.Address6 as County,  
 p.PhoneNumber,  
 1 as IsDir,  
 e.MappingKey,
  [ConsultantStatus]
  
   
 from ContactsLite.dbo.InternationalConsultants (nolock) i   
 left join ContactsLite.dbo.PhoneNumbers p (nolock) on i.ContactID=p.ContactID and PhoneNumberTypeID=5  
 left join ContactsLite.dbo.Consultants c (nolock) on c.ContactID=i.ContactID  
 left join ContactsLite.dbo.Addresses a (nolock) on a.ContactID=i.ContactID  
 left join [dbo].[PB_Event-Consultant] e(nolock) on e.[ContactId]=i.ContactID and e.eventkey=@EventKey  
 --where mappingkey is not null  
 where  i.DirectSellerID in (select t_id from @IdsTB)   
 order by p.PhoneNumber ,i.DirectSellerID  
  
END
ELSE
  BEGIN
  select   
 i.DirectSellerID ,  
 i.ContactID,  
 c.LastName,  
 c.FirstName,  
 c.ConsultantLevelID as [Level],  
 i.ResidenceID,  
 a.Address4 as Province,  
 a.Address3 as City,  
 a.Address6 as County,  
 p.PhoneNumber,  
 1 as IsDir
    
 from ContactsLite.dbo.InternationalConsultants (nolock) i   
 left join ContactsLite.dbo.PhoneNumbers p (nolock) on i.ContactID=p.ContactID and PhoneNumberTypeID=5  
 left join ContactsLite.dbo.Consultants c (nolock) on c.ContactID=i.ContactID  
 left join ContactsLite.dbo.Addresses a (nolock) on a.ContactID=i.ContactID   
 where  i.DirectSellerID in (select t_id from @IdsTB)
 order by p.PhoneNumber ,i.DirectSellerID  
  END
END
  

GO
GRANT EXEC ON dbo.PB_ConsultantInfo TO siteuser,db_spexecute

GO


/****** Object:  StoredProcedure [dbo].[PB_CheckDuplicatePhoneNumber]    Script Date: 2016/1/26 15:02:06 ******/
PRINT '------ create PROCEDURE PB_Customer_GetList----------------- '
GO  

IF EXISTS ( SELECT  *
            FROM    dbo.sysobjects
            WHERE   id = OBJECT_ID(N'dbo.PB_Customer_GetList')
                    AND type = 'p' ) 
    DROP PROCEDURE dbo.[PB_Customer_GetList]
GO  

CREATE PROC dbo.PB_Customer_GetList  
(  
@ContactId bigint  
)  
AS  
 Begin  
SELECT c.CustomerName,c.HeadImgUrl,c.CustomerPhone,t.CreatedDate ,e.EventLocation
FROM Community..[PB_Event-Consultant](NOLOCK) ec  
INNER JOIN Community..PB_Ticket(NOLOCK) t  
ON ec.MappingKey=t.MappingKey  INNER JOIN Community..PB_Customer(NOLOCK) c  
ON c.CustomerKey=t.CustomerKey  INNER JOIN Community..PB_Event(NOLOCK) e
ON  t.EventKey=e.EventKey
WHERE ec.ContactId=@ContactId  
End 

GO
GRANT EXEC ON dbo.PB_Customer_GetList TO siteuser,db_spexecute

GO



/****** Object:  StoredProcedure [dbo].[PB_CheckDuplicatePhoneNumber]    Script Date: 2016/1/26 15:02:06 ******/
PRINT '------ create PROCEDURE PB_Event_GetList----------------- '
GO  

IF EXISTS ( SELECT  *
            FROM    dbo.sysobjects
            WHERE   id = OBJECT_ID(N'dbo.PB_Event_GetList')
                    AND type = 'p' ) 
    DROP PROCEDURE dbo.[PB_Event_GetList]
GO  

CREATE PROCEDURE [dbo].[PB_Event_GetList]      
(       
@ContactID bigint      
)      
AS        
      
BEGIN      
           
  SELECT e.EventKey,e.CheckInStartDate,e.CheckInEndDate,    
  e.EventLocation,e.EventTitle,e.EventStartDate,e.EventEndDate,      
  e.ApplyTicketStartDate,e.ApplyTicketEndDate,e.InvitationEndDate,e.BCImport,e.VolunteerImport,      
  e.VIPImport,ec.UserType,ec.NormalTicketQuantity,ec.VIPTicketQuantity,ec.ContactId,ec.MappingKey,    
  ec.IsConfirmed,ec.Status
  FROM Community..PB_Event e WITH(NOLOCK)       
  LEFT JOIN Community..[PB_Event-Consultant] ec WITH (NOLOCK)      
  ON e.EventKey=ec.EventKey      
  WHERE ec.ContactId=@ContactID        
      
END     


GO
GRANT EXEC ON dbo.PB_Event_GetList TO siteuser,db_spexecute

GO



/****** Object:  StoredProcedure [dbo].[PB_CheckDuplicatePhoneNumber]    Script Date: 2016/1/26 15:02:06 ******/
PRINT '------ create PROCEDURE PB_GetInvitationInfo----------------- '
GO  

IF EXISTS ( SELECT  *
            FROM    dbo.sysobjects
            WHERE   id = OBJECT_ID(N'dbo.PB_GetInvitationInfo')
                    AND type = 'p' ) 
    DROP PROCEDURE dbo.PB_GetInvitationInfo
GO  

CREATE PROCEDURE [dbo].PB_GetInvitationInfo    
 @TicketKey uniqueIdentifier  
AS      
BEGIN  
 select t.TicketKey,TicketStatus,t.TicketType,
 c.CustomerName,c.CustomerPhone,c.UnionID,c.HeadImgUrl,c.CustomerKey
 from Community..PB_Ticket t (nolock) 
 left join Community..PB_Customer c (nolock) on t.CustomerKey=c.CustomerKey
 where TicketKey=@TicketKey
END 


GO
GRANT EXEC ON dbo.PB_GetInvitationInfo TO siteuser,db_spexecute

GO


/****** Object:  StoredProcedure [dbo].[PB_CheckDuplicatePhoneNumber]    Script Date: 2016/1/26 15:02:06 ******/
PRINT '------ create PROCEDURE PB_GetUnitBCList----------------- '
GO  

IF EXISTS ( SELECT  *
            FROM    dbo.sysobjects
            WHERE   id = OBJECT_ID(N'dbo.PB_GetUnitBCList')
                    AND type = 'p' ) 
    DROP PROCEDURE dbo.[PB_GetUnitBCList]
GO  

CREATE PROCEDURE [dbo].[PB_GetUnitBCList]          
 @ContactID bigint     
AS        
BEGIN     
    
  SELECT c.ContactID,FirstName,LastName,p.PhoneNumber ,IC.DirectSellerID
  FROM ContactsLite..Consultants c (NOLOCK) INNER JOIN ContactsLite..InternationalConsultants IC (NOLOCK)
  ON C.ContactID=IC.ContactID  INNER JOIN ContactsLite..Units u( NOLOCK )    
  ON c.UnitIDKey=u.UnitIDKey INNER JOIN ContactsLite..PhoneNumbers p(NOLOCK)  
  ON  p.ContactID=c.ContactID and p.PhoneNumberTypeID=5    
  WHERE c.ConsultantLevelID>=35 and u.UnitIDKey= (    
 SELECT u.UnitIDKey FROM  ContactsLite..Consultants c (NOLOCK)    
  INNER JOIN ContactsLite..Units u( NOLOCK )    
ON c.UnitIDKey=u.UnitIDKey    
WHERE c.ContactID=@ContactID)      
    
END 
GO
GRANT EXEC ON dbo.PB_GetUnitBCList TO siteuser,db_spexecute

GO


PRINT '------ create PROCEDURE PB_Pagination----------------- '
GO  

IF EXISTS ( SELECT  *
            FROM    dbo.sysobjects
            WHERE   id = OBJECT_ID(N'dbo.PB_Pagination')
                    AND type = 'p' ) 
    DROP PROCEDURE dbo.[PB_Pagination]
GO  



-- =============================================
-- 通用分页存储过程
-- =============================================
create PROCEDURE [dbo].[PB_Pagination]
@Page int = 1,      -- 当前页码
@PageSize int = 10,     -- 每页记录条数(页面大小)
@Table nvarchar(500),    -- 表名或视图名，甚至可以是嵌套SQL：(Select * From Tab Where ID>1000) Tab
@Field nvarchar(800) = '*',   -- 返回记录集字段名，","隔开，默认是"*"
@OrderBy nvarchar(100) = 'ID ASC', -- 排序规则
@Filter nvarchar(500),    -- 过滤条件
@MaxPage smallint output,   -- 执行结果 -1 error, 0 false, maxpage true
@TotalRow int output,    -- 记录总数 /* 2007-07-12 22:11:00 update */
@Descript nvarchar(100) output  -- 结果描述
AS
BEGIN
Set ROWCOUNT @PageSize;

Set @Descript = 'successful';
-------------------参数检测----------------
IF LEN(RTRIM(LTRIM(@Table))) !> 0
Begin
  Set @MaxPage = 0;
  Set @Descript = 'table name is empty';
  Return;
End

IF LEN(RTRIM(LTRIM(@OrderBy))) !> 0
Begin
  Set @MaxPage = 0;
  Set @Descript = 'order is empty';
  Return;
End

IF ISNULL(@PageSize,0) <= 0
Begin
  Set @MaxPage = 0;
  Set @Descript = 'page size error';
  Return;
End

IF ISNULL(@Page,0) <= 0
Begin
  Set @MaxPage = 0;
  Set @Descript = 'page error';
  Return;
End
-------------------检测结束----------------

Begin Try
  -- 整合SQL
  Declare @SQL nvarchar(4000), @Portion nvarchar(4000);

  Set @Portion = ' ROW_NUMBER() OVER (ORDER BY ' + @OrderBy + ') AS ROWNUM FROM ' + @Table;

  Set @Portion = @Portion + (CASE WHEN LEN(@Filter) >= 1 THEN (' Where ' + @Filter + ') AS tab') ELSE (') AS tab') END);

  Set @SQL = 'Select TOP(' + CAST(@PageSize AS nvarchar(8)) + ') ' + @Field + ' FROM (Select ' + @Field + ',' + @Portion;

  Set @SQL = @SQL + ' Where tab.ROWNUM > ' + CAST((@Page-1)*@PageSize AS nvarchar(8));

  -- 执行SQL, 取当前页记录集
  Execute(@SQL);
  --------------------------------------------------------------------

  -- 整合SQL
  Set @SQL = 'Set @Rows = (Select MAX(ROWNUM) FROM (Select' + @Portion + ')';

  -- 执行SQL, 取最大页码
  Execute sp_executesql @SQL, N'@Rows int output', @TotalRow output;
  Set @MaxPage = (CASE WHEN (@TotalRow % @PageSize)<>0 THEN (@TotalRow / @PageSize + 1) ELSE (@TotalRow / @PageSize) END);
End Try
Begin Catch
  -- 捕捉错误
  Set @MaxPage = -1;
  Set @Descript = 'error line: ' + CAST(ERROR_LINE() AS varchar(8)) + ', error number: ' + CAST(ERROR_NUMBER() AS varchar(8)) + ', error message: ' + ERROR_MESSAGE();
  Return;
End Catch;

-- 执行成功
Return;
END

GO
GRANT EXEC ON dbo.PB_Pagination TO siteuser,db_spexecute

GO


PRINT '------ create PROCEDURE PB_QueryPendingTicketTracker----------------- '
GO  

IF EXISTS ( SELECT  *
            FROM    dbo.sysobjects
            WHERE   id = OBJECT_ID(N'dbo.PB_QueryPendingTicketTracker')
                    AND type = 'p' ) 
    DROP PROCEDURE dbo.[PB_QueryPendingTicketTracker]
GO  

CREATE proc [dbo].[PB_QueryPendingTicketTracker]
(
	@Count int
)

as
 
begin
	declare @Tracker table ([TrackerKey] uniqueidentifier,
							[MappingKey] uniqueidentifier,
							[SessionKey] uniqueidentifier,
							[Status] smallint, 
							ApplyResult smallint,
							[CreatedDate] datetime,
							[CreatedBy] nvarchar(50),
							[UpdatedDate] datetime,
							[UpdatedBy] nvarchar(50)
							)

	update  PB_ApplyTicketTracker  set status=1 
	output  inserted.*   into @Tracker
	from (select top (@count) * from PB_ApplyTicketTracker where status=0  order by CreatedDate) as tc
	where tc.TrackerKey=PB_ApplyTicketTracker.TrackerKey
	
	select e.EventKey,t.* from @Tracker t inner join  [PB_EventSession] e on t.SessionKey=e.SessionKey
	
end



GO
GRANT EXEC ON dbo.PB_QueryPendingTicketTracker TO siteuser,db_spexecute

GO


PRINT '------ create PROCEDURE PB_SaveApplyTicket----------------- '
GO  

IF EXISTS ( SELECT  *
            FROM    dbo.sysobjects
            WHERE   id = OBJECT_ID(N'dbo.PB_SaveApplyTicket')
                    AND type = 'p' ) 
    DROP PROCEDURE dbo.[PB_SaveApplyTicket]
GO  


CREATE proc [dbo].[PB_SaveApplyTicket]
(
	@TrackerKey uniqueidentifier,
	@EventKey uniqueidentifier,
	@SessionKey uniqueidentifier,
	@MappingKey uniqueidentifier,
	@CreatedDate datetime,
	@Result bit =0 output ,
	@Descript nvarchar(1000) output 
	
)
as

begin
begin tran

declare @UsedCount int 
declare @PerSession int
declare @tickettotal int
declare @SessionTicketTotal int
declare @NormalTicketCount int
declare @SessionStartDate datetime
declare @SessionEndDate datetime
declare @TicketOut bit
declare @i int = 0
declare @ticketsCount int =0
	
	if not exists(select 1 from PB_ApplyTicketTracker where TrackerKey=@TrackerKey and Status=1)
		begin
			set @Descript ='has processed ,TrackerKey:'+cast(@TrackerKey as varchar(50))
			return 
		end


	select  @PerSession=[TicketQuantityPerSession],
			@TicketOut =s.TicketOut, 
			@tickettotal=applytickettotal ,
			@NormalTicketCount =s.NormalTicketQuantity,
			@SessionStartDate=s.SessionStartDate,
			@SessionEndDate=s.SessionEndDate
	from PB_EventTicketSetting t
	join PB_EventSession s on t.EventKey=s.EventKey
	where s.SessionKey=@SessionKey and t.eventkey=@EventKey
	
	--if((@tickettotal is null) or @tickettotal<1)
	--begin
		select @SessionTicketTotal=SUM(NormalTicketQuantity) from PB_EventSession where EventKey=@EventKey and CanApply=1
	--end
	if((@tickettotal is null)  or @tickettotal<1 or @SessionTicketTotal<@tickettotal)
	begin
	 set @tickettotal=@SessionTicketTotal
	end
	
	--print @tickettotal

	if (@TicketOut=0)--抢票是否结束
	begin
		set @i=@PerSession
			while @i>0
			begin
				if((select [dbo].[PB_TicketEventUsedCount](@EventKey))<@tickettotal)
				begin    
					SET @UsedCount=(select dbo.PB_TicketPerSessionUsedCount(@SessionKey))
					if(@UsedCount<@NormalTicketCount)
						begin
							
							insert into PB_Ticket (TicketKey,MappingKey,EventKey,SessionKey,TicketType,TicketFrom,TicketStatus,SMSToken,SessionStartDate,SessionEndDate,CreatedDate,CreatedBy)
							values
							(NEWID(),@MappingKey,@EventKey,@SessionKey,1,1,0,RIGHT(1000000 + CONVERT(bigint, ABS(CHECKSUM(NEWID()))), 6),@SessionStartDate,@SessionEndDate, @CreatedDate,'service')
							
							--update PB_ApplyTicketTracker set [Status]=2,ApplyResult=1,[UpdatedDate]=@CreatedDate,[UpdatedBy]='winService' where TrackerKey=@TrackerKey 
							set @ticketsCount=@ticketsCount+1

							IF(@UsedCount+1 = @NormalTicketCount)
							begin
								update PB_EventSession set TicketOut=1 where SessionKey=@SessionKey
								break
							end
						end
					else
						begin
							update PB_EventSession set TicketOut=1 where SessionKey=@SessionKey
							break
						end
				end	
				else
				begin
					update PB_EventSession set TicketOut=1 where EventKey=@EventKey	and CanApply=1
					break
				end	
			set @i=@i-1

			end

			if(@ticketsCount>0)
			begin
				update PB_ApplyTicketTracker set [Status]=2,ApplyResult=1,[UpdatedDate]=@CreatedDate,[UpdatedBy]='winService' where TrackerKey=@TrackerKey 
				update [dbo].[PB_Event-Consultant] set [NormalTicketQuantity]=[NormalTicketQuantity]+@ticketsCount,Status=1 where [MappingKey]=@MappingKey
			end
			else
			begin
				update PB_ApplyTicketTracker set [Status]=2,ApplyResult=2,[UpdatedDate]=@CreatedDate,[UpdatedBy]='winService' where TrackerKey=@TrackerKey 
			end

	end
	else
	begin 
		--抢票结束直接返回结果
		update PB_ApplyTicketTracker set [Status]=2,ApplyResult=2,[UpdatedDate]=@CreatedDate,[UpdatedBy]='winService' where TrackerKey=@TrackerKey 
	end
	
	if(@@error <>0)
		 begin
		 set @Descript=ERROR_MESSAGE()
	     set @Result=0
	     rollback tran
	    
		 end
     else
		 begin
		 set @Descript='success'
	     set @Result=1
	     commit tran
	     
		 end
end

GO
GRANT EXEC ON dbo.PB_SaveApplyTicket TO siteuser,db_spexecute

GO


PRINT '------ create PROCEDURE PB_SaveEvent----------------- '
GO  

IF EXISTS ( SELECT  *
            FROM    dbo.sysobjects
            WHERE   id = OBJECT_ID(N'dbo.PB_SaveEvent')
                    AND type = 'p' ) 
    DROP PROCEDURE dbo.[PB_SaveEvent]
GO  



create proc [dbo].[PB_SaveEvent]
(
     @EventKey uniqueidentifier,
     @EventTiTle nvarchar(100),
     @EventLocation nvarchar(100),
     @EventStartDate datetime,
     @EventEndDate datetime,
	 @InvitationStartDate datetime,
     @InvitationEndDate datetime,
     @ApplyTicketStartDate datetime,
     @ApplyTicketEndDate datetime,
	 @CheckinStartDate datetime,
	 @CheckinEndDate datetime,
     @DownloadToken varchar(10),
     @UploadToken varchar(10),
     @CreatedDate datetime,
     @CreatedBy nvarchar(50),
     @UpdatedDate datetime,
     @UpdatedBy nvarchar(50),
     @SessionXML xml
)
as
begin

begin tran
--处理sessionxml
DECLARE @idoc int
EXEC sp_xml_preparedocument @idoc OUTPUT,@SessionXML
--创建session临时表
declare  @sessionTB table(
	t_SessionKey uniqueidentifier,
	t_EventKey uniqueidentifier,
	t_DisplayOrder smallint,
	t_SessionStartDate datetime,
	t_SessionEndDate datetime,
	t_CanApply bit,
	t_TicketOut bit,
	t_VIPTicketQuantity int,
	t_NormalTicketQuantity int,
	t_CreatedDate datetime,
	t_CreatedBy varchar(50)
	)
	
	--解析xml到临时表中
	insert into @sessionTB 
		SELECT *
				FROM  OPENXML (@idoc, '/root/s',8)
					WITH (SessionKey  uniqueidentifier,
						  EventKey uniqueidentifier,
						  DisplayOrder  smallint,
						  SessionStartDate datetime,
						  SessionEndDate datetime,
						  CanApply bit,
						  TicketOut bit,
						  VIPTicketQuantity varchar(10),
						  NormalTicketQuantity  varchar(10),
						  CreatedDate datetime,
						  CreatedBy nvarchar(50)
						  )

--删除旧的eventsession
delete from PB_EventSession where  EventKey=@EventKey
--临时表内容直接插入PB_EventSession表
insert into PB_EventSession
(SessionKey,EventKey,DisplayOrder,SessionStartDate,SessionEndDate,CanApply,TicketOut,VIPTicketQuantity,NormalTicketQuantity,CreatedDate,CreatedBy)
 select  t_SessionKey,t_EventKey,t_DisplayOrder,t_SessionStartDate,t_SessionEndDate,t_CanApply,t_TicketOut,t_VIPTicketQuantity,t_NormalTicketQuantity,t_CreatedDate,t_CreatedBy
from @sessionTB


     if exists(select 1 from PB_Event (nolock) where Eventkey=@EventKey)
     begin
			
			--update event
           update PB_Event
           set EventTitle=@EventTiTle,
           EventLocation=@EventLocation,
           EventStartDate=@EventStartDate,
           EventEndDate=@EventEndDate,
		   InvitationStartDate=@InvitationStartDate,
           InvitationEndDate=@InvitationEndDate,
           ApplyTicketStartDate=@ApplyTicketStartDate,
           ApplyTicketEndDate=@ApplyTicketEndDate,
		   CheckinStartDate=@CheckinStartDate,
		   CheckinEndDate=@CheckinEndDate,
           UpdatedDate=@UpdatedDate,
           UpdatedBy=@UpdatedBy
           where EventKey=@EventKey

     end
     else
     begin
     insert into PB_Event
     (
     EventKey,EventTitle,EventLocation,
     EventStartDate,EventEndDate,
     ApplyTicketStartDate,ApplyTicketEndDate,
	 CheckinStartDate,CheckinEndDate,
     InvitationStartDate,InvitationEndDate,DownloadToken,UploadToken,
     IsUpload,BCImport,VolunteerImport,VIPImport,
     CreatedDate,CreatedBy)
     values
     (
     @EventKey,@EventTiTle,@EventLocation,
     @EventStartDate,@EventEndDate,
     @ApplyTicketStartDate,@ApplyTicketEndDate,
	 @CheckinStartDate,@CheckinEndDate,
     @InvitationStartDate,@InvitationEndDate,@DownloadToken,@UploadToken,
      0,0,0,0,
     @CreatedDate,@CreatedBy
     )
     end
     
     if(@@error <>0)
		 begin
	     rollback tran
		 end
     else
		 begin
	     commit tran
		 end
end

GO
GRANT EXEC ON dbo.PB_SaveEvent TO siteuser,db_spexecute

GO




PRINT '------ create PROCEDURE PB_SendSMSCustomerStatusUpdate----------------- '
GO  

IF EXISTS ( SELECT  *
            FROM    dbo.sysobjects
            WHERE   id = OBJECT_ID(N'dbo.PB_SendSMSCustomerStatusUpdate')
                    AND type = 'p' ) 
    DROP PROCEDURE dbo.[PB_SendSMSCustomerStatusUpdate]
GO  
create procedure [dbo].[PB_SendSMSCustomerStatusUpdate]
(@key uniqueidentifier,
@smsStatus nvarchar(30),
@whereType int
)
as
begin
	if @whereType=1
	begin
		update [dbo].[PB_Customer] set [SMSStatus]=@smsStatus 
		where [CustomerKey]=@key
	end
	else
	begin
		update [dbo].[PB_Customer] set [SMSStatus]=@smsStatus 
		where CustomerKey in(
				select pc.CustomerKey		
				 from [dbo].[PB_Ticket]  as pt with(nolock)				
				inner join [dbo].[PB_Customer] as pc with(nolock)
				on pt.Customerkey=pc.Customerkey
				where pt.EventKey =@key and pt.TicketStatus=2
				and pc.SMSStatus='sending'	
		)
	end
end



GO
GRANT EXEC ON dbo.PB_SendSMSCustomerStatusUpdate TO siteuser,db_spexecute

GO




PRINT '------ create PROCEDURE PB_Ticket_GetDetail----------------- '
GO  

IF EXISTS ( SELECT  *
            FROM    dbo.sysobjects
            WHERE   id = OBJECT_ID(N'dbo.PB_Ticket_GetDetail')
                    AND type = 'p' ) 
    DROP PROCEDURE dbo.[PB_Ticket_GetDetail]
GO  


CREATE PROCEDURE [dbo].[PB_Ticket_GetDetail]        
 @TicketKey uniqueidentifier       
AS          
BEGIN       
SELECT ec.FirstName,ec.LastName,ec.PhoneNumber,  t.TicketKey,t.TicketType,t.TicketStatus,
t.SMSToken,c.CustomerName,c.CustomerPhone,c.CustomerKey,t.SessionStartDate,t.SessionEndDate    
FROM Community..PB_Ticket t(NOLOCK)      
INNER JOIN  Community..[PB_Event-Consultant] ec (NOLOCK)      
ON t.MappingKey=ec.MappingKey      
left JOIN  Community..PB_Customer c (NOLOCK)      
ON c.CustomerKey=t.CustomerKey      
WHERE t.TicketKey=@TicketKey        
      
END 

GO
GRANT EXEC ON dbo.PB_Ticket_GetDetail TO siteuser,db_spexecute

GO




PRINT '------ create PROCEDURE PB_Ticket_GetList----------------- '
GO  

IF EXISTS ( SELECT  *
            FROM    dbo.sysobjects
            WHERE   id = OBJECT_ID(N'dbo.PB_Ticket_GetList')
                    AND type = 'p' ) 
    DROP PROCEDURE dbo.[PB_Ticket_GetList]
GO  

CREATE PROCEDURE [dbo].[PB_Ticket_GetList]      
 @ContactID bigint=NULL ,    
 @EventKey uniqueIdentifier=NULL, 
 @MappingKey uniqueIdentifier=NULL
AS        
BEGIN     
    
IF(@MappingKey IS NOT NULL)
 BEGIN

  SELECT t.TicketKey,t.CustomerKey,t.TicketStatus,t.TicketFrom,t.TicketType,t.MappingKey    
  ,t.CheckinDate,t.CreatedDate   
  from Community..PB_Ticket t (NOLOCK) 
  WHERE t.MappingKey=@MappingKey     
  ORDER BY t.CreatedDate DESC      
 END
ELSE
  Begin
    select @MappingKey=MappingKey from Community..[PB_Event-Consultant] ec (nolock)
   WHERE ec.ContactId=@ContactID and ec.EventKey=@EventKey   
   
   SELECT t.TicketKey,t.CustomerKey,t.TicketStatus,t.TicketFrom,t.TicketType,t.MappingKey    
  ,t.CheckinDate,t.CreatedDate,c.CustomerName,c.CustomerPhone    
  from Community..PB_Ticket t (NOLOCK)     
 --inner JOIN Community..[PB_Event-Consultant] ec WITH (NOLOCK) ON t.EventKey=ec.EventKey    
  LEFT JOIN Community..PB_Customer c WITH(NOLOCK) ON t.CustomerKey=c.CustomerKey    
  WHERE t.MappingKey =@MappingKey   
  ORDER BY t.CreatedDate DESC    
  END
    
END 
GO
GRANT EXEC ON dbo.PB_Ticket_GetList TO siteuser,db_spexecute

GO


PRINT '------ create PROCEDURE PB_Customer_GetList----------------- '
GO  

IF EXISTS ( SELECT  *
            FROM    dbo.sysobjects
            WHERE   id = OBJECT_ID(N'dbo.PB_Ticket_GetListByUnionID')
                    AND type = 'p' ) 
    DROP PROCEDURE dbo.PB_Ticket_GetListByUnionID
GO  

CREATE PROC dbo.PB_Ticket_GetListByUnionID  
(  
 @UnionID varchar(200)  
)  
AS  
 BEGIN  
SELECT ec.ContactId,ec.PhoneNumber as 'ConsultantPhoneNumber',ec.LastName+ec.FirstName 'ConsultantName', e.EventTitle,e.EventLocation,  
t.TicketKey,t.SessionStartDate,t.SessionEndDate,  
t.SMSToken,t.CheckinDate,t.TicketType,TicketStatus,c.CreatedDate,e.EventEndDate
FROM Community..PB_Customer (NOLOCK) c INNER JOIN Community..PB_Ticket(NOLOCK) t  
ON t.CustomerKey=c.CustomerKey INNER JOIN Community..PB_Event(NOLOCK) e  
ON t.EventKey=e.EventKey INNER JOIN Community..[PB_Event-Consultant](NOLOCK) ec  
ON ec.MappingKey=t.MappingKey  
WHERE c.UnionID=@UnionID  
 END 

GO
GRANT EXEC ON dbo.PB_Ticket_GetListByUnionID TO siteuser,db_spexecute

GO



PRINT '------ create PROCEDURE [PB_UpdateEventTitleAndLocation]----------------- '
GO  

IF EXISTS ( SELECT  *
            FROM    dbo.sysobjects
            WHERE   id = OBJECT_ID(N'dbo.PB_UpdateEventTitleAndLocation')
                    AND type = 'p' ) 
    DROP PROCEDURE dbo.[PB_UpdateEventTitleAndLocation]
GO  

CREATE proc [dbo].[PB_UpdateEventTitleAndLocation]
(
	@EventKey uniqueidentifier,
	@EventTiTle nvarchar(30),
	@EventLocation nvarchar(30)
)
as
begin
	update [dbo].[PB_Event] set EventTiTle=@EventTiTle,EventLocation=@EventLocation where EventKey=@EventKey
end


GO
GRANT EXEC ON dbo.[PB_UpdateEventTitleAndLocation] TO siteuser,db_spexecute

GO