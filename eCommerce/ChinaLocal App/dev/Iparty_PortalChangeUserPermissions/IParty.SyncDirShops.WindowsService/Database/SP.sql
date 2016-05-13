
USE [Community]
GO

/****** Object:  StoredProcedure [dbo].[IPartySP_SyncDirShopData]    Script Date: 02/16/2016 17:07:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IPartySP_SyncDirShopData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[IPartySP_SyncDirShopData]
GO

USE [Community]
GO

/****** Object:  StoredProcedure [dbo].[IPartySP_SyncDirShopData]    Script Date: 02/16/2016 17:07:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
CREATE PROC [dbo].[IPartySP_SyncDirShopData]    
(  
 @DateNow datetime  
)  
AS     
    BEGIN    
        BEGIN TRAN    
  --insert into IParty_DirShops   
  -- select Recordid,CID,ContactID,ShopType,(case when ShopType='普通' then '0' else '1' end) as TypeValue,  
  -- Province,City,County,ShopAddress,ShopZipCode,Contact,ContactTel,FixedTel,ShopLicenseName,SyncDate  
  --  from IParty_DirShops_Temp  
          
  --      delete from IParty_DirShops where SyncDate<@DateNow  
  --      delete from IParty_DirShops_Temp  
     
   DELETE d     
  FROM dbo.IParty_DirShops d WITH (NOLOCK) LEFT JOIN dbo.IParty_DirShops_Temp dt WITH (NOLOCK) ON d.Recordid = dt.Recordid    
  WHERE dt.RecordID is null    
        
       
  update   r    
  set r.CID =t.CID,    
   r.Recordid=t.Recordid,    
   r.ContactID=t.ContactID,    
      r.ShopType=t.ShopType,    
      r.TypeValue=(case when t.ShopType='普通' then '0' else '1' end),    
      r.Province=t.Province,    
      r.City=t.City,    
      r.County=t.County,    
      r.ShopAddress=t.ShopAddress,    
      r.ShopZipCode=t.ShopZipCode,    
      r.Contact=t.Contact,    
      r.ContactTel=t.ContactTel,    
      r.FixedTel=t.FixedTel,    
      r.ShopLicenseName=t.ShopLicenseName, 
	  r.FirstCheckOutDate=t.FirstCheckOutDate,   
      r.SyncDate=@DateNow  
  from  dbo.IParty_DirShops r with(nolock)     
  inner join dbo.IParty_DirShops_Temp t with(nolock)  on r.Recordid = t.Recordid    
     
  INSERT INTO dbo.IParty_DirShops (Recordid, CID, ContactID, ShopType, TypeValue,       
   Province,City,County,ShopAddress,ShopZipCode,Contact,ContactTel,FixedTel,ShopLicenseName,FirstCheckOutDate,SyncDate)      
  SELECT t.Recordid, t.cid, t.ContactID, t.ShopType, (case when t.ShopType='普通' then '0' else '1' end) as TypeValue, t.Province,    
  t.City,t.County,t.ShopAddress,t.ShopZipCode,t.Contact ,t.ContactTel,t.FixedTel,t.ShopLicenseName,t.FirstCheckOutDate,@DateNow  
  FROM dbo.IParty_DirShops_Temp t with(nolock)      
  left join IParty_DirShops r with(nolock)    on  r.RecordID = t.RecordID       
  where  r.RecordID is null  
  
     
     
     
        IF ( @@ERROR <> 0 )     
            BEGIN    
                ROLLBACK TRAN    
                delete from IParty_DirShops_Temp  
                SELECT  -1    
            END    
        ELSE     
            BEGIN    
                COMMIT TRAN    
                delete from IParty_DirShops_Temp  
                SELECT  1    
            END    
    END    
  
  
GO



grant exec on [dbo].[IPartySP_SyncDirShopData] to siteuser  
grant exec on [dbo].[IPartySP_SyncDirShopData] to db_spexecute 