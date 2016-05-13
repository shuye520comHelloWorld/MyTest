USE [Community]
GO

/****** Object:  StoredProcedure [dbo].[PB_ActivityCustomerSelect]    Script Date: 2016/1/26 14:57:59 ******/

PRINT '------ create PROCEDURE CA_Customer_Query_ForWechat----------------- '
GO  

IF EXISTS ( SELECT  *
            FROM    dbo.sysobjects
            WHERE   id = OBJECT_ID(N'dbo.CA_Customer_Query_ForWechat')
                    AND type = 'p' ) 
    DROP PROCEDURE dbo.[CA_Customer_Query_ForWechat]
GO  
CREATE PROCEDURE dbo.CA_Customer_Query_ForWechat	
(	
	@ContactID bigint,
	@ActivityID int
)
AS
Begin
  SELECT ct.TicketID,cc.CustomerKey, cc.CustomerPhone as PhoneNumber,
					cb.CustomerBaseId,cb.UnionId,cb.NickName,cb.HeadImgUrl,
						cw.ServiceContentID,ct.TicketStartTime,ct.CreatedDate
					from CA_Ticket as ct with(nolock) inner join CA_Customer as cc  with(nolock)
					on ct.CustomerKey = cc.CustomerKey inner join CA_CustomerBaseInfo as cb with(nolock)
					on cc.CustomerBaseId = cb.CustomerBaseId inner join CA_WelcomeCard as cw  with(nolock)
					on ct.WelcomeCardID = cw.WelcomeCardID
					where cw.ContactID=@ContactID and cw.ActivityID=@ActivityID
End

go

GRANT EXEC ON dbo.CA_Customer_Query_ForWechat TO siteuser,db_spexecute

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
SELECT c.CustomerName,c.HeadImgUrl,c.CustomerPhone,c.CreatedDate ,e.EventLocation
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

