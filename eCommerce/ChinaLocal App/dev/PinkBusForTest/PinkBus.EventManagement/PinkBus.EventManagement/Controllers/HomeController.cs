using Newtonsoft.Json;
using PinkBus.EventManagement.Helper;
using PinkBus.EventManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace PinkBus.EventManagement.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult ApplyBC(Guid EventKey)
        {
            PB_Event Event = Common.getEvent(EventKey);
            if (Event != null)
            {
                ViewBag.Consultants = new List<EventConsultant>();
                ViewBag.EventTicketSetting = new PB_EventTicketSetting() { TicketQuantityPerSession = 0, ApplyTicketTotal = 0 };
                if (Event.BCImport)
                {
                    DataSet ds = HomeDAO.queryEventConsultant(Event.EventKey, EventUserType.NormalBC);

                    if (ds.Tables.Count > 0)
                    {
                        string json = JsonConvert.SerializeObject(ds.Tables[0]);
                        ViewBag.Consultants = JsonConvert.DeserializeObject<List<EventConsultant>>(json);

                        json = JsonConvert.SerializeObject(ds.Tables[1]);
                        List<PB_EventTicketSetting> settings = JsonConvert.DeserializeObject<List<PB_EventTicketSetting>>(json);
                        ViewBag.EventTicketSetting = settings.Count > 0 ? settings[0] : new PB_EventTicketSetting() { TicketQuantityPerSession = 0, ApplyTicketTotal = 0 };
                    }


                }

                Event.EventSessions = HomeDAO.getSessions(EventKey.ToString()).OrderBy(e => e.DisplayOrder).ToList();


                return View(Event);
            }

            return PartialView("~/Views/Shared/Error.cshtml");
        }


        public ActionResult VolunteerBC(Guid EventKey)
        {
            PB_Event Event = Common.getEvent(EventKey);
            if (Event != null)
            {
                ViewBag.Consultants = new List<EventConsultant>();
                if (Event.VolunteerImport)
                {
                    DataSet ds = HomeDAO.queryEventConsultant(Event.EventKey, EventUserType.VolunteerBC);

                    if (ds.Tables.Count > 0)
                    {
                        string json = JsonConvert.SerializeObject(ds.Tables[0]);
                        ViewBag.Consultants = JsonConvert.DeserializeObject<List<EventConsultant>>(json);

                        json = JsonConvert.SerializeObject(ds.Tables[1]);
                        List<PB_EventTicketSetting> settings = JsonConvert.DeserializeObject<List<PB_EventTicketSetting>>(json);
                        ViewBag.EventTicketSetting = settings.Count > 0 ? settings[0] : new PB_EventTicketSetting() { TicketQuantityPerSession = 0, ApplyTicketTotal = 0 };
                    }

                }

            }
            return View(Event);
        }

        public ActionResult VIPBC(Guid EventKey)
        {
            PB_Event Event = Common.getEvent(EventKey);
            if (Event != null)
            {
                ViewBag.Consultants = new List<EventConsultant>();
                if (Event.VipImport)
                {
                    DataSet ds = HomeDAO.queryEventConsultant(Event.EventKey, EventUserType.VIPBC);

                    if (ds.Tables.Count > 0)
                    {
                        string json = JsonConvert.SerializeObject(ds.Tables[0]);
                        ViewBag.Consultants = JsonConvert.DeserializeObject<List<EventConsultant>>(json);
                    }

                }

            }
            return View(Event);
        }


        [HttpPost]
        public JsonResult SaveEvent(string EventObj)
        {
            DateTime TimeNow = DateTime.Now;
            EventPost Event = JsonConvert.DeserializeObject<EventPost>(EventObj);

            if (Event.EventKey.HasValue && Event.EventKey != Guid.Empty)
            {
                PB_Event E = Common.getEvent(Event.EventKey.Value);
                if (E.EventStartDate < DateTime.Now)
                {
                    return Json(new { result = false, Msg = "抢报已开始，活动不能进行修改！" }, JsonRequestBehavior.AllowGet);
                }

                if (E.BCImport || E.VolunteerImport || E.VipImport)
                {
                    return Json(new { result = false, Msg = "已经导入过名单，活动不能进行修改！" }, JsonRequestBehavior.AllowGet);
                }
            }
            string Msg = string.Empty;

            if (!Common.CheckSessionTime(Event, out Msg))
                return Json(new { result = false, Msg = Msg }, JsonRequestBehavior.AllowGet);
            if (Event.EventKey == null)
                Event.EventKey = Guid.NewGuid();

            Event.UploadToken = Common.MakePasscode();
            Event.DownloadToken = Common.MakePasscode(Event.UploadToken);
            Event.CreatedDate = TimeNow;
            Event.UpdatedDate = TimeNow;
            Event.CreatedBy = Event.UpdatedBy = System.Web.HttpContext.Current.User.Identity.Name;

            if (HomeDAO.CreateOrUpdateEvent(Event) > 0)
            {
                return Json(new { result = true, key = Event.EventKey }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = false, Msg = "创建活动失败！" }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult SaveEventTitleAndLocation(string EventObj)
        {
            EventPost Event = JsonConvert.DeserializeObject<EventPost>(EventObj);
            if (string.IsNullOrEmpty(Event.EventTitle) || string.IsNullOrEmpty(Event.EventLocation))
            {
                return Json(new { result = false, Msg = "活动名称和地点不能为空！" }, JsonRequestBehavior.AllowGet);
            }

            if (Event.EventTitle.Length > 30)
            {
                return Json(new { result = false, Msg = "活动名称长度不能为空或者大于30个字符！！" }, JsonRequestBehavior.AllowGet);
            }

            if (Event.EventLocation.Length > 40)
            {
                return Json(new { result = false, Msg = "活动地点长度不能为空或者大于40个字符！" }, JsonRequestBehavior.AllowGet);
            }

            if (HomeDAO.UpdateEventTitleAndLocation(Event) > 0)
            {
                return Json(new { result = true, key = Event.EventKey }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = false, Msg = "创建活动失败！" }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult GetEventList(int PageNo, string EventTitle = null, string EventStartDate = null, string EventEndDate = null)
        {
            string TitleFilter = string.IsNullOrEmpty(EventTitle) ? string.Empty : "(eventtitle like '%" + EventTitle.Trim() + "%') ";
            string DateFilter = string.Empty;
            if (!string.IsNullOrEmpty(EventStartDate) && !string.IsNullOrEmpty(EventEndDate))
                DateFilter += " (CheckinStartDate  >= '" + EventStartDate + "' and EventEndDate <= '" + EventEndDate + "')";
            else if (!string.IsNullOrEmpty(EventStartDate) && string.IsNullOrEmpty(EventEndDate))
                DateFilter += " (CheckinStartDate  >= '" + EventStartDate + "' )";
            else if (string.IsNullOrEmpty(EventStartDate) && !string.IsNullOrEmpty(EventEndDate))
                DateFilter += "(EventEndDate <= '" + EventEndDate + "')";

            if (string.IsNullOrEmpty(TitleFilter))
                TitleFilter = DateFilter;
            else if (!string.IsNullOrEmpty(DateFilter))
                TitleFilter += "and " + DateFilter;


            SearchModel SM = new SearchModel
            {
                Page = PageNo,
                pageSize = AppSetting.EventPageSize,
                TableName = "PB_Event (nolock) ",
                Field = "*",
                OrderBy = "CreatedDate desc",
                Descript = "",
                Filter = TitleFilter,
                MaxPage = 0,
                TotalRow = 0
            };

            //List<PB_Event> Events = new List<PB_Event>();// HomeDAO.getEvents(SM);
            List<PB_Event> Events = HomeDAO.getEvents(SM);
            if (Events!=null&&Events.Count > 0)
            {
                string eventKeys = string.Join("','", Events.Select(e => e.EventKey.ToString()));
                List<PB_EventSession> Sessions = HomeDAO.getSessions(eventKeys).OrderBy(e => e.DisplayOrder).ToList();
                ViewBag.Sessions = Sessions;
            }
            
            //SM.MaxPage = 0;
            //SM.TotalRow = 0;
            ViewBag.SearchModel = SM;
            //Events = new List<PB_Event>();
            return PartialView("~/views/home/partial/EventList.cshtml", Events);
        }


        [HttpPost]
        public JsonResult GetBCInfo(List<string> Ids,Guid EventKey)
        {
            List<EventConsultant> Consultants = new List<EventConsultant>();
            int pages = Ids.Count % 500 == 0 ? Ids.Count / 500 : Ids.Count / 500 + 1;
            for (int i = 0; i < pages; i++)
            {
                string idstr = Common.SetIdXml(Ids.Skip(i * 500).Take(500).ToList());
                Consultants.AddRange(HomeDAO.getConsultantInfo(idstr, EventKey));
            }

            //不合法数据处理
            foreach (string i in Ids)
            {
                if (Consultants.FindAll(c => c.DirectSellerID == i).Count < 1)
                {
                    EventConsultant c = new EventConsultant();
                    c.DirectSellerID = i;
                    c.ResidenceID = "\u7f16\u53f7\u9519\u8bef\uff0c\u8bf7\u5220\u9664";
                    c.IsDir = false;
                    Consultants.Insert(0, c);
                }
                

            }

            Consultants.ForEach(c => {
                if (!string.IsNullOrEmpty(c.ConsultantStatus)&&c.ConsultantStatus.ToLower().Contains("x"))
                {
                    c.ResidenceID = "\u7f16\u53f7\u5df2\u6ce8\u9500\uff0c\u8bf7\u5220\u9664";
                    c.IsDir = false;
                }
            });


            //Consultants.ForEach(e => { e.PhoneNumber = "15555555555"; });

            return Json(new { result = true, data = JsonConvert.SerializeObject(Consultants) }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult SaveConsultant(string data)
        {
            
            ConsultantPost CP = JsonConvert.DeserializeObject<ConsultantPost>(data);
            PB_Event E = Common.getEvent(CP.EventKey);
            if (DateTime.Now<E.ApplyTicketEndDate &&CP.Type!=0)
            {
                return Json(new { result = false, Msg = "抢票时间还没结束，不能上传志愿者或赠票名单！" }, JsonRequestBehavior.AllowGet);
            }

            #region 校验数据完整
            if (CP.Type == 0)//抢票顾问
            {
                if (CP.EventConsultants.Count < 1 || CP.EventSessionkeys.Count < 1 || CP.TicketQuantityPerSession < 1)
                {
                    return Json(new { result = false, Msg = "请填写完整的数据后再进行保存！" }, JsonRequestBehavior.AllowGet);
                }
                CP.userType = EventUserType.NormalBC;
            }
            else if (CP.Type == 1)//志愿者
            {
                if (CP.EventConsultants.Count < 1 || (CP.VIPTicketQuantityPerPerson < 1 && CP.NormalTicketQuantityPerPerson < 1))
                {
                    return Json(new { result = false, Msg = "请填写完整的数据后再进行保存！" }, JsonRequestBehavior.AllowGet);
                }
                CP.userType = EventUserType.VolunteerBC;
            }
            else if (CP.Type == 2)//赠票顾问
            {
                CP.userType = EventUserType.VIPBC;
            }

            #endregion

            #region 设定票可分配数量
            CP.EventConsultants.ForEach(c =>
            {
                if (!c.MappingKey.HasValue)
                    c.MappingKey = Guid.NewGuid();
                
                if (CP.Type == 1)
                {
                    c.NormalTicketSettingQuantity = CP.NormalTicketQuantityPerPerson;
                    c.VIPTicketSettingQuantity = CP.VIPTicketQuantityPerPerson;
                }
                else if (CP.Type == 2)
                {
                    c.NormalTicketSettingQuantity = c.NormalTicketQuantity;
                    c.VIPTicketSettingQuantity = c.VIPTicketQuantity;
                }
            });
            #endregion

            List<Ticket> tickets = Common.setAllTickets(CP);
            List<Ticket> emptyTickets = tickets.FindAll(e => e.SessionKey == Guid.Empty);
            #region 检测没有分到票的情况
            if (emptyTickets.Count > 0 && CP.BtnType == 0)
            {
                List<EventConsultant> co = CP.EventConsultants;
                //var m = emptyTickets.GroupBy(e => e.MappingKey).ToList();
                var empety = co.GroupJoin(emptyTickets, c => c.MappingKey, e => e.MappingKey, (c, e) => new { c, e }).Select(o => o).ToList();
                List<TicketOutConsultant> LT = new List<TicketOutConsultant>();

                empety.ForEach(f =>
                {
                    TicketOutConsultant t = new TicketOutConsultant();
                    t.DirectSellerID = f.c.DirectSellerID;
                    t.Name = f.c.Name;
                    t.NormalSettingCount = CP.Type == 1 ? f.c.NormalTicketSettingQuantity : f.c.NormalTicketQuantity;
                    t.VIPSettingCount = CP.Type == 1 ? f.c.VIPTicketSettingQuantity : f.c.VIPTicketQuantity;
                    t.NormalRealCount = tickets.FindAll(e => e.MappingKey == f.c.MappingKey && e.SessionKey != Guid.Empty && e.TicketType == TicketType.Normal).Count;
                    t.VIPRealCount = tickets.FindAll(e => e.MappingKey == f.c.MappingKey && e.SessionKey != Guid.Empty && e.TicketType == TicketType.VIP).Count;
                    LT.Add(t);
                });

                return Json(new { result = false, showPop = true, data = JsonConvert.SerializeObject(LT) }, JsonRequestBehavior.AllowGet);

            }

            #endregion

            #region
            CP.EventConsultants.ForEach(c =>
            {
                if (CP.Type == 0)
                {
                    c.NormalTicketSettingQuantity = CP.TicketQuantityPerSession;
                    c.VIPTicketSettingQuantity = 0;
                    c.NormalTicketQuantity = 0;
                    c.VIPTicketQuantity = 0;
                }
                else if (CP.Type == 1 || CP.Type == 2)
                {
                    c.NormalTicketQuantity = tickets.FindAll(e => e.MappingKey == c.MappingKey && e.SessionKey != Guid.Empty && e.TicketType == TicketType.Normal).Count;
                    c.VIPTicketQuantity = tickets.FindAll(e => e.MappingKey == c.MappingKey && e.SessionKey != Guid.Empty && e.TicketType == TicketType.VIP).Count;
                }
                if (c.IsUpdateName)
                {
                    c.FirstName = c.Name.Length > 3 ? c.Name.Substring(2, c.Name.Length - 2) : c.Name.Substring(1, c.Name.Length - 1);
                    c.LastName = c.Name.Length > 3 ? c.Name.Substring(0, 2) : c.Name.Substring(0, 1);
                }
            });
            #endregion
            CP.CreatedDate = DateTime.Now;
            CP.CreatedBy = System.Web.HttpContext.Current.User.Identity.Name;
            CP.sessionkeysXml = Common.SetIdXml(CP.EventSessionkeys ?? new List<string>());

            CP.consultantsXml = Common.SetConsultantXml(CP);

            CP.ticketsXml = Common.SetTicketsXml(CP, tickets.FindAll(e => e.SessionKey != Guid.Empty));

            bool res = HomeDAO.saveConsultants(CP);
            return Json(new { result = res, Msg = "" }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult CheckSellerId(string SellerId, Guid EventKey)
        {
            List<string> ids = new List<string>();
            ids.Add(SellerId);
            string idstr = Common.SetIdXml(ids);
            List<EventConsultant> ECs = HomeDAO.getConsultantInfo(idstr, EventKey);

           
            if (ECs != null&&ECs.Count<1)
            {
                return Json(new { result = false, type = "none", msg = "编号不存在，请输入有效编号！" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (ECs.FindAll(e => e.ConsultantStatus.ToLower().Contains("x")).Count > 0)
                {
                    return Json(new { result = false, msg = "该编号已经注销，请输入有效编号！" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { result = true, ContactID = ECs[0].ContactID, MappingKey = ECs[0].MappingKey }, JsonRequestBehavior.AllowGet);
                }
            }
        }


    }
}
