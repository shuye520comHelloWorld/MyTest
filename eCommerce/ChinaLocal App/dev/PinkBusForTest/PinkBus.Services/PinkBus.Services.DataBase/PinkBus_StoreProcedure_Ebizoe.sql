USE Contacts
GO 

IF EXISTS ( SELECT  *
            FROM    dbo.sysobjects
            WHERE   id = OBJECT_ID(N'dbo.PB_GetCountyListByCityName')
                    AND type = 'p' ) 
DROP PROCEDURE dbo.PB_GetCountyListByCityName
GO               

Create PROCEDURE dbo.PB_GetCountyListByCityName
(
@CityName nvarchar(100)
)
AS
Begin
 SELECT co.CityID,co.CountyID,co.DisplayIndex,co.Name
 FROM Contacts..Cities (NOLOCK) ci INNER JOIN Contacts..Counties (NOLOCK) co
 ON ci.CityID=co.CityID
 WHERE CityName=@CityName
 Order by co.DisplayIndex 
End

GO
GRANT EXEC ON [dbo].PB_GetCountyListByCityName to siteuser, db_spexecute

GO