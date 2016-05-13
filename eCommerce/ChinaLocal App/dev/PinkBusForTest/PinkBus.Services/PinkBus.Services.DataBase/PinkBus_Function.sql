
USE [Community]
GO



PRINT '------ create function PB_TicketEventUsedCount----------------- '
GO  

IF EXISTS ( SELECT  *
            FROM    dbo.sysobjects
            WHERE   id = OBJECT_ID(N'dbo.PB_TicketEventUsedCount')
                    AND type = 'fn' ) 
    DROP function dbo.PB_TicketEventUsedCount
GO  

create function [dbo].[PB_TicketEventUsedCount]
(
	@EventKey uniqueidentifier
)

RETURNS int
AS
BEGIN
DECLARE @UsedCount int
SELECT  @UsedCount = count(1) from PB_Ticket
		where TicketStatus in (0,1,2) and EventKey=@EventKey
RETURN  @UsedCount
END

GO


PRINT '------ create function PB_TicketEventUsedCount----------------- '
GO  

IF EXISTS ( SELECT  *
            FROM    dbo.sysobjects
            WHERE   id = OBJECT_ID(N'dbo.PB_TicketPerSessionUsedCount')
                    AND type = 'fn' ) 
    DROP function dbo.PB_TicketPerSessionUsedCount
GO  



CREATE function [dbo].[PB_TicketPerSessionUsedCount]
(
	@SessionKey uniqueidentifier
)
RETURNS int
AS
BEGIN
DECLARE @UsedCount int
SELECT  @UsedCount = count(1) from community.dbo.PB_Ticket (NOLOCK)
		WHERE TicketStatus in (0,1,2) and SessionKey=@SessionKey
RETURN @UsedCount
end

GO

