/****** Object:  StoredProcedure [dbo].[IParty_Event_GetList]    Script Date: 2016/4/14 18:02:06 ******/
USE [Community]
PRINT '------ create PROCEDURE IParty_Event_GetList----------------- '
GO  

IF EXISTS ( SELECT  *
            FROM    dbo.sysobjects
            WHERE   id = OBJECT_ID(N'dbo.IParty_Event_GetList')
                    AND type = 'p' ) 
    DROP PROCEDURE dbo.[IParty_Event_GetList]
GO  


CREATE PROCEDURE [dbo].[IParty_Event_GetList]      
(       
@ContactID bigint      
)      
AS        
      
BEGIN      
    
declare @UnitId varchar(20)

select @UnitId=UnitID from ContactsLite.dbo.Consultants WITH(NOLOCK)  where ContactID=@ContactID and ConsultantLevelID>=15 

SELECT e.[EventKey],e.[Title],e.[EventStartDate],e.[EventEndDate]  
FROM [Community]..[iParty_Event] e WITH(NOLOCK)  
left join [Community]..iParty_PartyApplication ep WITH(NOLOCK)  
on e.EventKey=ep.EventKey   
left join ContactsLite.dbo.Consultants c WITH(NOLOCK)  
on c.ContactID=ep.AppliedContactID
where c.UnitID=@UnitId and ep.DisplayEndDate>=CONVERT(varchar(100), GETDATE(), 23) 
union
select e.[EventKey],e.[Title],e.[EventStartDate],e.[EventEndDate]  
 from iParty_Event e (nolock)
join iParty_PartyApplication p (nolock) on e.EventKey=p.EventKey
join iParty_Unitee u (nolock) on p.PartyKey=u.PartyKey
where u.UnitId=@UnitId and p.DisplayEndDate>=CONVERT(varchar(100), GETDATE(), 23)
      
END     

GO
GRANT EXEC ON dbo.IParty_Event_GetList TO siteuser,db_spexecute

GO
