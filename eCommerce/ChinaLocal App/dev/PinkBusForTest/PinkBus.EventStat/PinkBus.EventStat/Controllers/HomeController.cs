using PinkBus.EventStat.Helper;
using PinkBus.EventStat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace PinkBus.EventStat.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CheckIn()
        {
            return View();
        }

        public ActionResult Inviter()
        {
            return View();
        }

        public ActionResult NewCustomer()
        {

            return View();
        }



        public ActionResult EventList(int PageNo, string eventTitle, DateTime? startDate, DateTime? endDate)
        {

            string filter = "";

            if (!string.IsNullOrEmpty(eventTitle))
            {
                filter = " eventtitle like '%"+eventTitle.Trim()+"%' # ";
            }

            if (startDate.HasValue)
            {
                filter += " CheckinStartDate >='" + startDate + "' # ";
            }

            if (endDate.HasValue)
            {
                filter += " EventEndDate <='" + endDate + "' # ";
            }

            filter = filter.TrimEnd().Trim('#').Replace("# ", " and ");

            SearchModel SM = new SearchModel();
            SM.TableName = "PB_Event";
            SM.Page = PageNo;
            SM.pageSize = 10;
            SM.OrderBy = "CreatedDate desc";
            SM.Filter = filter;
            List<PB_Event> events = HomeDAO.GetPageItems<PB_Event>(SM);
            List<IndexEvent> eventList = new List<IndexEvent>();

            foreach (PB_Event e in events)
            {

                IndexEvent ie = new IndexEvent();
                ie.EventKey = e.EventKey;
                ie.EventTitle = e.EventTitle;
                ie.EventStartDate = e.EventStartDate;
                ie.EventEndDate = e.EventEndDate;
                ie.CheckinStartDate = e.CheckinStartDate;
                int NormalTicketCount =0, VIPTicketCount =0, inviterCount =0, checkinCount  =0, needCheckinCount =0, newCustomerCount = 0;
                HomeDAO.GetEventInfo(e.EventKey, ref NormalTicketCount, ref VIPTicketCount, ref inviterCount, ref checkinCount, ref needCheckinCount, ref newCustomerCount);
                ie.NormalTicketCount = NormalTicketCount;
                ie.VIPTicketCount = VIPTicketCount;
                ie.InviterCount = inviterCount;
                ie.CheckinCount = checkinCount;
                ie.NeedCheckinCount = needCheckinCount;
                ie.NewCustomerCount = newCustomerCount;

                eventList.Add(ie);
            }

            ViewBag.SearchModel = SM;

            return PartialView("~/views/home/partial/eventList.cshtml", eventList);
        }

        public JsonResult EventSessionList(Guid EventKey)
        {
            var sessions = HomeDAO.GetEventSessions(EventKey);
           
            return Json(new { sessions }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult NewCustomerList(Guid eventKey, int pageNo, string name, string phone)
        {
            string filter = "";
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(phone))
                filter = " eventkey='" + eventKey + "' and (CustomerName like '%" + name.Trim() + "%' and ContactInfo like '%" + phone.Trim() + "%')";
            else if (!string.IsNullOrEmpty(name))
                filter = " (CustomerName like '%" + name.Trim() + "%')";
            else if (!string.IsNullOrEmpty(phone))
                filter = " eventkey='" + eventKey + "' and  (ContactInfo like '%" + phone.Trim() + "%')";
            else
                filter = " eventkey='" + eventKey + "' ";

            SearchModel SM = new SearchModel();
            SM.TableName = "PB_OfflineCustomer";
            SM.Page = pageNo;
            SM.pageSize = 10;
            SM.OrderBy = "CreatedDate desc";
            SM.Filter = filter;
            List<PB_OfflineCustomer> newCustomers = HomeDAO.GetPageNewCustomer(SM, eventKey);

            ViewBag.SearchModel = SM;
            return PartialView("~/views/home/partial/NewCustomerList.cshtml", newCustomers);
        }

        public ActionResult CheckinList(Guid eventKey, int pageNO, string name = "", int ticketType = -1, int ticketStatus = -1, string checkinType="-1")
        {
            List<CheckinUser> CheckinList = new List<CheckinUser>();

            string table = @"((select   c.MappingKey,c.DirectSellerId,c.FirstName,cu.CustomerKey,c.LastName, 
                            cu.CustomerName,cu.CustomerPhone,t.TicketType, t.TicketStatus,t.UpdatedBy,
                             cu.CreatedDate,t.CheckinDate 
                            from [PB_Event-Consultant] c join PB_Ticket t on c.MappingKey=t.MappingKey
                            join PB_Customer cu on cu.CustomerKey=t.CustomerKey
                            where t.EventKey='{0}' 
                            and t.CustomerKey is not null and (TicketStatus in (2,5,6)))
                            union 
                            (select  distinct ec.MappingKey,ec.DirectSellerId,ec.FirstName,CustomerKey=null,
                            ec.LastName, CustomerName=ec.LastName+ec.FirstName,ec.PhoneNumber as CustomerPhone, 
                            TicketType=2,(case when c.CheckinDate is null then 2 else 5 end) TicketStatus,c.UpdatedBy, ec.CreatedDate,c.CheckinDate 
                            from  [PB_Event-Consultant] ec left join
                            (select MAX(CheckinDate) CheckinDate,mappingkey,UpdatedBy from  PB_VolunteerCheckin v where EventKey='{0}' group by MappingKey,UpdatedBy) c
                             on c.MappingKey=ec.MappingKey
                            where ec.EventKey='{0}' and ec.UserType=3 and ec.Status in (0,1,3))) as t";
            table = string.Format(table, eventKey);
            string filter = "";
            if (!string.IsNullOrEmpty(name))
            {
                filter = "CustomerName like '%" + name.Trim() + "%' # ";
            }

            if (ticketType != -1)
            {
                filter += " TicketType=" + ticketType + " # ";
            }

            if (ticketStatus != -1)
            {
                filter += "TicketStatus=" + ticketStatus+" #";
            }

            if(checkinType != "-1")
            {
                filter += "UpdatedBy='" + checkinType+"'";
            }


            filter = filter.TrimEnd().TrimEnd('#').Replace("# ", " and ");

            SearchModel SM = new SearchModel();
            SM.TableName = table;
            SM.Page = pageNO;
            SM.pageSize = 10;
            SM.OrderBy = " TicketType , DirectSellerId";
            SM.Filter = filter;


            List<CheckinUser> newCustomers = HomeDAO.GetPageItems<CheckinUser>(SM);




            CheckinSummary summay = HomeDAO.GetCheckSummary(eventKey);
           // summay.VolunteerCheckinCount = volunteerCheckin.FindAll(e=>e.TicketStatus==TicketStatus.Checkin).Count;
          //  summay.VolunteerCount = volunteerCheckin.Count;

           // SM.TotalRow = SM.TotalRow + summay.VolunteerCheckinCount;
           // SM.MaxPage= SM.TotalRow/SM.pageSize
            ViewBag.SearchModel = SM;
            ViewBag.Summay = summay;
            return PartialView("~/views/home/partial/CheckinList.cshtml", newCustomers);
        }

        public ActionResult InviterList(Guid eventKey,int pageNo,string name="",string SellerId="")
        {
            string sqlTable = @"(select UserType,NormalTicketQuantity,VIPTicketQuantity,DirectSellerId,ConsultantLevelID as Level,c.FirstName,c.LastName,c.PhoneNumber,c.CreatedBy
                           from [PB_Event-Consultant] c 
                           left join ContactsLite.dbo.Consultants co on c.ContactId=co.ContactID 
                           where  (NormalTicketQuantity !=0 or VIPTicketQuantity!=0) and ";


             string filter = " c.EventKey='" + eventKey + "' and  ConsultantLevelID is not null ";
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(SellerId))
            {
                filter = "c.EventKey='" + eventKey + "' and directSellerid like '%" + SellerId.Trim() + "%' and (c.lastname+c.firstname) like '%" + name.Trim() + "%'";
            }
            else if (!string.IsNullOrEmpty(name))
            {
                filter = " c.EventKey='" + eventKey + "' and  (c.lastname+c.firstname) like '%" + name.Trim() + "%'";
            }
            else if (!string.IsNullOrEmpty(SellerId))
            {
                filter = "c.EventKey='" + eventKey + "' and directSellerid like '%" + SellerId.Trim() + "%' ";
            }

            SearchModel SM = new SearchModel();
            SM.TableName = sqlTable + filter +" ) as t ";
            SM.Page = pageNo;
            SM.pageSize = 10;
            SM.OrderBy = "directSellerid ";
            SM.Filter ="";
            List<pb_Inviter> inviters = HomeDAO.GetPageItems<pb_Inviter>(SM);
            ViewBag.SearchModel = SM;
            return PartialView("~/views/home/partial/InviterList.cshtml", inviters);
        }
        //得到签到者名单的Exl表
        public FileResult ExportStuCheckin(Guid eventKey)
        {

            List<CheckinUser> CheckXelList = HomeDAO.GetCheckinExl(eventKey);

            //创建Excel文件的对象            
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet           
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
            //貌似这里可以设置各种样式字体颜色背景等，但是不是很方便，这里就不设置了

            sheet1.SetColumnWidth(0, 20 * 256);
            sheet1.SetColumnWidth(1, 20 * 256);
            sheet1.SetColumnWidth(2, 20 * 256);
            sheet1.SetColumnWidth(3, 20 * 256);
            sheet1.SetColumnWidth(4, 20 * 256);
            sheet1.SetColumnWidth(5, 20 * 256);
            sheet1.SetColumnWidth(6, 20 * 256);
            sheet1.SetColumnWidth(7, 20 * 256);
            sheet1.SetColumnWidth(8, 20 * 256);

            //给sheet1添加第一行的头部标题           
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);

            row1.Height = 25 * 20;
            row1.CreateCell(0).SetCellValue("签到者姓名");
            row1.CreateCell(1).SetCellValue("签到者身份");
            row1.CreateCell(2).SetCellValue("电话");
            row1.CreateCell(3).SetCellValue("邀约人");
            row1.CreateCell(4).SetCellValue("直销员编号");
            row1.CreateCell(5).SetCellValue("签到状态");
            row1.CreateCell(6).SetCellValue("签到方式");
            row1.CreateCell(7).SetCellValue("创建时间");
            row1.CreateCell(8).SetCellValue("签到时间");


            //将数据逐步写入sheet1各个行            
            for (int i = 0; i < CheckXelList.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue(CheckXelList[i].CustomerName);
                rowtemp.CreateCell(1).SetCellValue(CheckXelList[i].TicketType == TicketType.VIP ? "贵宾" : (CheckXelList[i].TicketType == TicketType.Normal ? "来宾" : "志愿者"));
                rowtemp.CreateCell(2).SetCellValue(CheckXelList[i].CustomerPhone);
                rowtemp.CreateCell(3).SetCellValue(CheckXelList[i].LastName+CheckXelList[i].FirstName);
                rowtemp.CreateCell(4).SetCellValue(CheckXelList[i].DirectSellerId);
                rowtemp.CreateCell(5).SetCellValue((int)CheckXelList[i].TicketType == 2 ? (CheckXelList[i].CheckinDate.HasValue ? "已签到" : "未签到") : ((int)CheckXelList[i].TicketStatus.Value == 5 ? "已签到" : "未签到"));
                rowtemp.CreateCell(6).SetCellValue(CheckXelList[i].UpdatedBy == "SMS" ? "短信口令" : (CheckXelList[i].UpdatedBy == "QRCode" ? "二维码" : (CheckXelList[i].UpdatedBy == "ClientCreateNew" ? "现场签到" : "")));
                rowtemp.CreateCell(7).SetCellValue(CheckXelList[i].CreatedDate.ToString("yyyy.MM.dd HH:mm"));
                rowtemp.CreateCell(8).SetCellValue(CheckXelList[i].CheckinDate.HasValue ? (CheckXelList[i].CheckinDate.Value.ToString("yyyy.MM.dd HH:mm")) : "");
            }

            // 写入到客户端            
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            DateTime dt = DateTime.Now;
            string dateTime = dt.ToString("yyMMdd");
            string fileName = "签到者名单" + dateTime + ".xls";
            return File(ms, "application/vnd.ms-excel", fileName);
        }

        //得到邀约者的Exl表
        public FileResult ExportStuInviter(Guid eventKey)
        {

            List<pb_Inviter> InviterXelList = HomeDAO.GetInviterExl(eventKey);

            //创建Excel文件的对象            
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet           
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
            //貌似这里可以设置各种样式字体颜色背景等，但是不是很方便，这里就不设置了


            sheet1.SetColumnWidth(0, 20 * 256);
            sheet1.SetColumnWidth(1, 20 * 256);
            sheet1.SetColumnWidth(2, 20 * 256);
            sheet1.SetColumnWidth(3, 20 * 256);
            sheet1.SetColumnWidth(4, 20 * 256);
            sheet1.SetColumnWidth(5, 20 * 256);


            //给sheet1添加第一行的头部标题           
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.Height = 25 * 20;
            row1.CreateCell(0).SetCellValue("直销员编号");
            row1.CreateCell(1).SetCellValue("顾问姓名");
            row1.CreateCell(2).SetCellValue("职级");
            row1.CreateCell(3).SetCellValue("手机号");
            row1.CreateCell(4).SetCellValue("来宾券数");
            row1.CreateCell(5).SetCellValue("贵宾券数");
            //将数据逐步写入sheet1各个行            
            for (int i = 0; i < InviterXelList.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue(InviterXelList[i].DirectSellerId);
                rowtemp.CreateCell(1).SetCellValue(InviterXelList[i].LastName + InviterXelList[i].FirstName);
                rowtemp.CreateCell(2).SetCellValue(InviterXelList[i].Level);
                rowtemp.CreateCell(3).SetCellValue(InviterXelList[i].PhoneNumber);
                rowtemp.CreateCell(4).SetCellValue(InviterXelList[i].NormalTicketQuantity.ToString());
                rowtemp.CreateCell(5).SetCellValue(InviterXelList[i].VIPTicketQuantity.ToString());
            }

            // 写入到客户端            
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            DateTime dt = DateTime.Now;
            string dateTime = dt.ToString("yyMMdd");
            string fileName = "邀约者表" + dateTime + ".xls";
            return File(ms, "application/vnd.ms-excel", fileName);
        }

        //得到新顾客的Exl表
        public FileResult ExportStuNewCustomer(Guid eventKey)
        {

            List<PB_OfflineCustomer> NewCustmerXelList = HomeDAO.GetNewCustomerExl(eventKey);

            //创建Excel文件的对象            
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet           
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
            //貌似这里可以设置各种样式字体颜色背景等，但是不是很方便，这里就不设置了   


            sheet1.SetColumnWidth(0, 20 * 256);
            sheet1.SetColumnWidth(1, 20 * 256);
            sheet1.SetColumnWidth(2, 20 * 256);
            sheet1.SetColumnWidth(3, 20 * 256);
            sheet1.SetColumnWidth(4, 20 * 256);
            sheet1.SetColumnWidth(5, 20 * 256);
            sheet1.SetColumnWidth(6, 20 * 256);
            sheet1.SetColumnWidth(7, 20 * 256);
            sheet1.SetColumnWidth(8, 20 * 256);
            sheet1.SetColumnWidth(9, 20 * 256);
            sheet1.SetColumnWidth(10, 20 * 256);
            sheet1.SetColumnWidth(11, 20 * 256);
            sheet1.SetColumnWidth(12, 20 * 256);
            sheet1.SetColumnWidth(13, 20 * 256);
            sheet1.SetColumnWidth(14, 20 * 256);
            sheet1.SetColumnWidth(15, 20 * 256);
            //给sheet1添加第一行的头部标题           
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.Height = 25 * 20;
            row1.CreateCell(0).SetCellValue("新顾客姓名");
            row1.CreateCell(1).SetCellValue("联系方式");
            row1.CreateCell(2).SetCellValue("年龄");
            row1.CreateCell(3).SetCellValue("顾客身份");
            row1.CreateCell(4).SetCellValue("创建时间");
            row1.CreateCell(5).SetCellValue("分配顾问姓名");
            row1.CreateCell(6).SetCellValue("分配顾问编号");
            row1.CreateCell(7).SetCellValue("顾客职业");

            row1.CreateCell(8).SetCellValue("是否听说过玫琳凯");
            row1.CreateCell(9).SetCellValue("愿意参加玫琳凯活动");
            row1.CreateCell(10).SetCellValue("是否使用过产品");
            row1.CreateCell(11).SetCellValue("顾客现场反应");
            row1.CreateCell(12).SetCellValue("最佳联系时间");
            row1.CreateCell(13).SetCellValue("建议时间段");
            row1.CreateCell(14).SetCellValue("您感兴趣的话题");
            row1.CreateCell(15).SetCellValue("常住地址");


            //将数据逐步写入sheet1各个行            
            for (int i = 0; i < NewCustmerXelList.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue(NewCustmerXelList[i].CustomerName);
                rowtemp.CreateCell(1).SetCellValue(NewCustmerXelList[i].ContactInfo + " (" + (string.IsNullOrEmpty(NewCustmerXelList[i].ContactType) ? "" : NewCustmerXelList[i].ContactType.Replace("PhoneNumber", "手机").Replace("Wechat", "微信").Replace("Other", "其他")) + ")");
                rowtemp.CreateCell(2).SetCellValue(NewCustmerXelList[i].AgeRange.HasValue ? (NewCustmerXelList[i].AgeRange.Value.ToString().Replace("Bellow25", "25岁以下").Replace("Between2535", "25-35岁").Replace("Between3545", "35-45岁").Replace("Above45", "大于45岁")) : "");
                rowtemp.CreateCell(3).SetCellValue(NewCustmerXelList[i].CustomerType.HasValue ? ((int)NewCustmerXelList[i].CustomerType.Value == 0 ? "老顾客" : ((int)NewCustmerXelList[i].CustomerType.Value == 1 ? "新顾客" : "在校学生")) : "");
                rowtemp.CreateCell(4).SetCellValue(NewCustmerXelList[i].CreatedDate.ToString("yyyy.MM.dd HH:mm"));
                rowtemp.CreateCell(5).SetCellValue(NewCustmerXelList[i].LastName + NewCustmerXelList[i].FirstName);
                rowtemp.CreateCell(6).SetCellValue(NewCustmerXelList[i].DirectSellerId);
                rowtemp.CreateCell(7).SetCellValue(NewCustmerXelList[i].Career);

                rowtemp.CreateCell(8).SetCellValue(NewCustmerXelList[i].IsHearMaryKay.HasValue ? (NewCustmerXelList[i].IsHearMaryKay.Value ? "是" : "否") : "");
                rowtemp.CreateCell(9).SetCellValue(NewCustmerXelList[i].IsJoinEvent.HasValue ? (NewCustmerXelList[i].IsJoinEvent == 1 ? "是" : "否") : "");
                rowtemp.CreateCell(10).SetCellValue(NewCustmerXelList[i].UsedProduct.HasValue ? (NewCustmerXelList[i].UsedProduct.Value? "是" : "否") : "");
                rowtemp.CreateCell(11).SetCellValue(NewCustmerXelList[i].CustomerResponse.HasValue ? (NewCustmerXelList[i].CustomerResponse == 0 ? "对产品有兴趣" : (NewCustmerXelList[i].CustomerResponse == 1 ? "对公司有兴趣" : (NewCustmerXelList[i].CustomerResponse == 2 ? "一般" : "没兴趣"))) : "");
                rowtemp.CreateCell(12).SetCellValue(NewCustmerXelList[i].BestContactDate.HasValue ? (NewCustmerXelList[i].BestContactDate == 0 ? "工作日" : "双休日") : "");
                rowtemp.CreateCell(13).SetCellValue(NewCustmerXelList[i].AdviceContactDate.HasValue ? (NewCustmerXelList[i].AdviceContactDate == 0 ? "白天" : "晚上") : "");
                rowtemp.CreateCell(14).SetCellValue(NewCustmerXelList[i].InterestingTopic.Replace("SkinCare", "美容护肤").Replace("MakeUp", "彩妆技巧").Replace("DressUp", "服饰搭配").Replace("FamilyTies", "家庭关系").TrimEnd(','));
                rowtemp.CreateCell(15).SetCellValue(NewCustmerXelList[i].Province + " " + (string.IsNullOrEmpty(NewCustmerXelList[i].City)?"": NewCustmerXelList[i].City.Replace("县", "")) + " " + NewCustmerXelList[i].County);
            }

            // 写入到客户端            
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            DateTime dt = DateTime.Now;
            string dateTime = dt.ToString("yyMMdd");
            string fileName = "新顾客表" + dateTime + ".xls";
            return File(ms, "application/vnd.ms-excel", fileName);
        }

        //产品使用数据json
        public JsonResult EventTracking(Guid eventKey)
        {
            var EventTrack = HomeDAO.GetTractingInfo(eventKey);
            return Json(EventTrack , JsonRequestBehavior.AllowGet);
        }

        //签到者名单来宾信息显示
        public JsonResult CheckGuestInforma(Guid CustomerKey)
        {
            var CheckGuestInforma = HomeDAO.GetCheckGuest(CustomerKey);
            return Json(CheckGuestInforma,JsonRequestBehavior.AllowGet);
        }

        //签到者名单志愿者信息显示
        public JsonResult CheckVolunteerInfo(Guid MappingKey)
        {
            var CheckVolunteerInforma = HomeDAO.GetCheckinVolunteerInfo(MappingKey);
            return Json(CheckVolunteerInforma, JsonRequestBehavior.AllowGet);
        }

        //邀约者邀请顾客的名单
        public JsonResult InviterCustomerList(string DirectSellerId, Guid eventKey)
        {
            var InviterCustomer = HomeDAO.GetInviterCustomer(DirectSellerId,eventKey);
            return Json(new { InviterCustomer }, JsonRequestBehavior.AllowGet);
        }
    }
}
