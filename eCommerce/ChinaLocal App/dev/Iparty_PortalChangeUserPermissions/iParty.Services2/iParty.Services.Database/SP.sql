use ContactsLite
go 
  
CREATE  PROCEDURE [dbo].[usp_ProfileGet]      
 @ContactID bigint  
AS    
BEGIN   
select   c.ContactID,  
       ic.DirectSellerID,  
       ic.ResidenceId,  
       c.LastName,  
       c.FirstName,  
       c.ConsultantStatus,  
       c.ConsultantLevelID,
       c.StartDate,  
       c.UnitID,  
       p.PhoneNumber,  
       a.Address1 as StreetAddress,  
       a.Address4 as ProvinceName,  
       a.Address3 as CityName,  
       a.Address6 as CountyName,  
       a.Address5 as PostalCode,  
       cc.LastName as DirectorLastName,  
       cc.FirstName as DirectorFirstName  
from InternationalConsultants ic with(nolock)  
inner join Consultants c with(nolock)  
on ic.ContactID = c.ContactID   
inner join Addresses a with(nolock)  
on c.ContactID = a.ContactID   
left  join PhoneNumbers p with(nolock)  
on p.ContactID = c.ContactID  
inner join Consultant_To_Director ctd with(nolock)  
on ctd.Consultant = c.ContactID   
inner join Consultants cc with(nolock)  
on ctd.Director = cc.ContactID  
where p.PhoneNumberTypeID = 5  
and c.ContactID = @ContactID  
and c.ConsultantStatus not like '%X%'  
  
end  

go 
GRANT EXEC  ON [dbo].[usp_ProfileGet]   TO  [db_spexecute],[SiteUser] 
go


use Community
Go

create proc dbo.[ipartySP_GetUsablenessConsultants]  
(  
 @WorkShopId int,
 @EventKey uniqueidentifier  
   
)  
as  
begin  

 declare   @t table (worhshopid int, contactId bigint,isHost bit )
 
 insert into @t
 select d.RecordId,l.ContactId,1 IsHost  
 from IParty_DirShops d with(nolock)  
 inner join IParty_Lessee l  with (nolock) on l.ContactId=d.ContactID   
 inner join ContactsLite.dbo.Consultants c with(nolock) on c.ContactID  = l.ContactId  
 where d.Recordid=@WorkShopId  
  union  
 select ShopRID as RecordId,s.ContactID,0 IsHost    
 from IParty_ShopPartners s with(nolock)  
 inner join ContactsLite.dbo.Consultants c with(nolock) on s.ContactID=c.ContactID  
 where ShopRID=@WorkShopId and s.ContactID != 0  
 
 select a.AppliedContactID from iParty_PartyApplication a with(nolock)
 inner join @t t 
 on t.contactId = a.AppliedContactID
 where a.EventKey = @EventKey 

end
go
GRANT EXEC  ON [ipartySP_GetUsablenessConsultants] TO  [db_spexecute],[SiteUser] 
go





CREATE proc [dbo].[GetWorkShopPartnersByWorkShopId] 
(  
      @ShopId int,
      @EventKey uniqueidentifier    
)  
as  
begin  
	
      declare   @t table (worhshopid int, contactId bigint,lastName nvarchar(200),firstname nvarchar(200), 
      StartDate datetime, LevelCode nvarchar(20),MobilePhone nvarchar(50), isHost bit,isApplied bit,UnitID varchar(10) )
      insert into @t
      select d.RecordId,l.ContactId,l.LastName,l.FirstName,c.StartDate, l.LevelCode,l.MobilePhone ,1 IsHost ,0,c.UnitID 
      from IParty_DirShops d with(nolock)  
      inner join IParty_Lessee l  with (nolock) on l.ContactId=d.ContactID   
      inner join ContactsLite.dbo.Consultants c with(nolock) on c.ContactID  = l.ContactId
        
      where d.Recordid=@ShopId  
      union  
      select ShopRID as RecordId,s.ContactID,s.LastName,s.FirstName,c.StartDate,s.LevelCode ,null as MobilePhone,0 IsHost ,0,c.UnitID   
      from IParty_ShopPartners s with(nolock)  
      inner join ContactsLite.dbo.Consultants c with(nolock) on s.ContactID=c.ContactID  
      where ShopRID=@ShopId and s.ContactID != 0 
      
      update  t
      set t.isApplied = 1
      from @t t
      inner join iParty_PartyApplication p with(nolock)
      on t.contactId = p.AppliedContactID
      and p.EventKey =@EventKey
     
      
      declare @c table (contactId bigint)
      insert into @c 
      select u.UniteeContactID from iParty_PartyApplication p with(nolock)
      inner join iParty_Unitee u with(nolock)
      on u.PartyKey = p.PartyKey
      where p.EventKey = @EventKey
      
        update  t
      set t.isApplied = 1
      from @t t
      inner join @c c 
      on t.contactId = c.contactId
      
      
      
      select * from @t  
end 
Go 
GRANT EXEC  ON [dbo].[GetWorkShopPartnersByWorkShopId] TO  [db_spexecute],[SiteUser] 
go







create proc dbo.[ipartySP_GetCoHostApplications]    
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

GRANT EXEC  ON [dbo].[ipartySP_GetCoHostApplications] TO  [db_spexecute],[SiteUser] 
go



CREATE proc [dbo].[GetUnitInvitationSummary]   
(    
      @UnitID varchar(10),  
      @PartyKey uniqueidentifier      
)    
as    
begin   
 declare @unit varchar(10)  
 select @unit=UnitID from ContactsLite.dbo.Consultants where ContactID=  
      (select AppliedContactID from iParty_PartyApplication   
      where PartyKey=@PartyKey)         
  
 select uc.UnitID,  
   iu.UniteeContactID as ContactID,    
   uc.LastName + uc.FirstName as UnitName ,  
   case     
    when uc.UnitID = @UnitID then 1    
    else 0    
   end as IsMyUnit  ,  
   COUNT(ip.PartyKey) AS [Count]    
 from iParty_Unitee iu with(nolock)    
 left join ContactsLite.dbo.Consultants uc with(nolock) on iu.UniteeContactID = uc.ContactID    
 left join iParty_Invitation ip with(nolock) on iu.UnitId = ip.OwnerUnitID  and ip.partykey=@partykey    
 where iu.PartyKey = @PartyKey    
 group by uc.UnitID, iu.UniteeContactID, uc.LastName + uc.FirstName    
 union all   
 select c.UnitID,  
 c.ContactID,  
 c.LastName + c.FirstName as UnitName,  
 case when c.UnitID=@UnitID then 1 else 0 end as IsMyUnit,  
 COUNT(i.PartyKey) as [Count]  
 from iParty_PartyApplication p   
 inner join ContactsLite.dbo.Consultants c on p.AppliedContactID=c.ContactID  
 inner join iParty_Invitation i on p.PartyKey=i.PartyKey and i.OwnerUnitID=@unit  
 where p.PartyKey=@PartyKey   
 group by c.UnitID, c.ContactID,c.LastName+c.FirstName  
  
end
Go 


GRANT EXEC  ON [dbo].[GetUnitInvitationSummary] TO  [db_spexecute],[SiteUser] 
go


 
CREATE  PROCEDURE [dbo].[usp_ProfileGet]        
 @ContactID bigint    
AS      
BEGIN     
select   c.ContactID,    
       ic.DirectSellerID,    
       ic.ResidenceId,
       ic.BusinessDirector,  
       c.LastName,    
       c.FirstName,    
       c.ConsultantStatus,    
       c.ConsultantLevelID,  
       c.StartDate,    
       c.UnitID,    
       p.PhoneNumber,    
       a.Address1 as StreetAddress,    
       a.Address4 as ProvinceName,    
       a.Address3 as CityName,    
       a.Address6 as CountyName,    
       a.Address5 as PostalCode,    
       cc.LastName as DirectorLastName,    
       cc.FirstName as DirectorFirstName    
from contactslite.dbo.InternationalConsultants ic with(nolock)    
inner join contactslite.dbo.Consultants c with(nolock)    
on ic.ContactID = c.ContactID     
inner join contactslite.dbo.Addresses a with(nolock)    
on c.ContactID = a.ContactID     
left  join contactslite.dbo.PhoneNumbers p with(nolock)    
on p.ContactID = c.ContactID    
inner join contactslite.dbo.Consultant_To_Director ctd with(nolock)    
on ctd.Consultant = c.ContactID     
inner join contactslite.dbo.Consultants cc with(nolock)    
on ctd.Director = cc.ContactID    
where p.PhoneNumberTypeID = 5    
and c.ContactID = @ContactID    
and c.ConsultantStatus not like '%X%'    
    
end    
GO

GRANT EXEC  ON [dbo].[usp_ProfileGet] TO  [db_spexecute],[SiteUser] 
go

  
create proc [dbo].[GetWorkShopInfoByContactId]  
(  
 @ContactId bigint  
)  
as  
  
begin  
    declare   @t table (RecordId int, contactId bigint,ShopType nvarchar(200),TypeValue int,Province nvarchar(50),City nvarchar(50),County nvarchar(50),
      ShopAddress nvarchar(200), ShopZipCode nvarchar(20),Contact nvarchar(50), ContactTel nvarchar(50),FixedTel  nvarchar(50),
      ShopLicenseName varchar(100),IsHost bit )

   insert into @t
   select RecordId,ContactID,ShopType,TypeValue,Province,City,County,ShopAddress,ShopZipCode,Contact,ContactTel, FixedTel,ShopLicenseName,1 as IsHost from IParty_DirShops  with (nolock) where contactid=@ContactId  
   insert into @t
   select d.RecordId,s.ContactID,ShopType,TypeValue,Province,City,County,ShopAddress,ShopZipCode,Contact,ContactTel, FixedTel,ShopLicenseName,0 as IsHost   
   from IParty_DirShops d  with (nolock)  
   inner join IParty_ShopPartners s  with (nolock)  
   on d.Recordid=s.ShopRID   
   where s.ContactID=@ContactId  
 select * from  @t
end  
  go 
GRANT EXEC  ON [dbo].[GetWorkShopInfoByContactId] TO  [db_spexecute],[SiteUser] 
go  



CREATE proc [dbo].[GetCoHostApplications]   
(    
      @contactId bigint,  
      @EventKey uniqueidentifier      
)    
as    
begin    
      select p.* from iParty_PartyApplication p with(nolock)
      inner join iParty_Unitee u with(nolock)
      on u.PartyKey = p.PartyKey
      where u.UniteeContactID = @contactId and p.EventKey = @EventKey
         
end  
GO 
GRANT EXEC  ON [dbo].[GetCoHostApplications] TO  [db_spexecute],[SiteUser] 
go 
