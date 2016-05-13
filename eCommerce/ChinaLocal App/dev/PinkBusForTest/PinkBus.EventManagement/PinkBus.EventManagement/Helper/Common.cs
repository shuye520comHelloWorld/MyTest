using PinkBus.EventManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Xml;

namespace PinkBus.EventManagement.Helper
{
    public class Common
    {
        public static string MakePasscode(string code = null)
        {
            object syncLock = new object();
            Random random = new Random();
            lock (syncLock)
            {
                string str = "ABCDEFGHKLMNPQRSTUVWXYZ23456789";
                string result = string.Empty;
                while (true)
                {
                    Thread.Sleep(200);
                    result = string.Empty;
                    for (int i = 0; i < 6; i++)
                    {
                        result += str[random.Next(str.Length)];
                    }
                    if (code == result || HomeDAO.CheckPassToken(result))
                    {
                        continue;
                    }

                    break;
                }

                return result;
            }
        }

        public static string MakeSMSToken()
        {
            object syncLock = new object();
           // Random random = new Random();
            lock (syncLock)
            {

                string guid = Guid.NewGuid().ToString().ToUpper().Replace("A","2").Replace("B","4").Replace("C","5").Replace("D","7").Replace("E","9").Replace("F","8");
                string token = guid.Split('-')[4].Substring(3, 6);
                return token;
                //string str = "01234567890123456789012345678901234567890123456789";
                //string result = string.Empty;

                //Thread.Sleep(25);
                //result = string.Empty;
                //for (int i = 0; i < 6; i++)
                //{
                //    Random random = new Random();
                //    result += str[random.Next(str.Length)];
                //}
                //return result;
            }
        }

        public static T ConvertTo<T>(object val, T defaultVal)
        {
            if (Convert.IsDBNull(val) || val == null)
            {
                return defaultVal;
            }
            else
            {
                try
                {
                    return (T)Convert.ChangeType(val, typeof(T));
                }
                catch
                {
                    return defaultVal;
                }
            }
        }

        public static bool CheckSessionTime(EventPost Event, out string Msg)
        {
            if (Event.ApplyTicketEndDate <= Event.ApplyTicketStartDate)
            {
                Msg = "抢票活动结束时间不能小于开始时间！";
                return false;
            }

            if (Event.InvitationEndDate <= Event.ApplyTicketEndDate)
            {
                Msg = "邀约截止时间必须在抢票活动结束之后！";
                return false;
            }

            List<EventSession> sessions = Event.EventSessions;
            if (sessions.Count < 1)
            {
                Msg = "请至少设置一场活动时段！";
                return false;
            }
            sessions = sessions.OrderBy(e => e.TimesNo).ToList();
            DateTime dt;
            for (int i = 0; i < sessions.Count; i++)
            {
                if (sessions[i].DateStart <= Event.InvitationEndDate)
                {
                    Msg = "活动时段必须在邀约截止时间之后！";
                    return false;
                }

                dt = sessions[i].DateEnd;
                if (sessions[i].DateEnd <= sessions[i].DateStart)
                {
                    Msg = "活动时段" + sessions[i].TimesNo + " 开始时间必须小于结束时间！";
                    return false;
                }
                if (i != sessions.Count - 1 && dt > sessions[i + 1].DateStart)
                {
                    Msg = "活动时段" + sessions[i].TimesNo + " 结束时间必须要小于活动时段 " + sessions[i + 1].TimesNo + "的开始时间！";
                    return false;
                }

            }

            Event.EventStartDate = Event.ApplyTicketStartDate;
            Event.EventEndDate = Event.CheckinEndDate =sessions[sessions.Count - 1].DateEnd;
            Event.CheckinStartDate = sessions[0].DateStart;
            
            Msg = "";
            return true;
        }

        public static string SetSessionXml(EventPost Event)
        {

            string userName = System.Web.HttpContext.Current.User.Identity.Name;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes");
            XmlElement root = xmlDoc.CreateElement("root");
            xmlDoc.AppendChild(root);
            foreach (var session in Event.EventSessions)
            {
                XmlElement s = xmlDoc.CreateElement("s");
                s.SetAttribute("SessionKey", Guid.NewGuid().ToString());
                s.SetAttribute("EventKey", Event.EventKey.ToString());
                s.SetAttribute("DisplayOrder", session.TimesNo.ToString());
                s.SetAttribute("SessionStartDate", session.DateStart.ToString());
                s.SetAttribute("SessionEndDate", session.DateEnd.ToString());
                s.SetAttribute("CanApply", "0");
                s.SetAttribute("TicketOut", "0");
                s.SetAttribute("VIPTicketQuantity", session.VIPTotal.ToString());
                s.SetAttribute("NormalTicketQuantity", session.NormalTotal.ToString());
                s.SetAttribute("CreatedDate", Event.CreatedDate.ToString());
                s.SetAttribute("CreatedBy", userName);
                //s.SetAttribute("UpdatedDate", "NULL");
                //s.SetAttribute("UpdatedBy", "NULL");
                root.AppendChild(s);
            }
            return xmlDoc.InnerXml;
        }

        public static string SetIdXml(List<string> ids)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes");
            XmlElement root = xmlDoc.CreateElement("root");
            xmlDoc.AppendChild(root);
            foreach (var id in ids)
            {
                XmlElement s = xmlDoc.CreateElement("s");
                s.SetAttribute("id", id);
                root.AppendChild(s);
            }
            return xmlDoc.InnerXml;
        }

        public static string SetConsultantXml(ConsultantPost CP)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes");
            XmlElement root = xmlDoc.CreateElement("root");
            xmlDoc.AppendChild(root);

            foreach (var m in CP.EventConsultants)
            {
                if (string.IsNullOrEmpty(m.DirectSellerID) || m.DirectSellerID.Length != 12) continue;

                XmlElement s = xmlDoc.CreateElement("s");
                s.SetAttribute("MappingKey", m.MappingKey.ToString());
                s.SetAttribute("EventKey", CP.EventKey.ToString());
                s.SetAttribute("UserType", ((int)CP.userType).ToString());
                s.SetAttribute("ContactId", m.ContactID);
                s.SetAttribute("DirectSellerID", m.DirectSellerID);
                s.SetAttribute("FirstName", m.FirstName);
                s.SetAttribute("LastName", m.LastName);
                s.SetAttribute("PhoneNumber", m.PhoneNumber);
                s.SetAttribute("Level", m.Level);
                s.SetAttribute("ResidenceID", m.ResidenceID);
                s.SetAttribute("Province", m.Province);
                s.SetAttribute("City", m.City);
                s.SetAttribute("County", m.CountyName);
                s.SetAttribute("NormalTicketQuantity", m.NormalTicketQuantity.ToString());
                s.SetAttribute("VIPTicketQuantity", m.VIPTicketQuantity.ToString());
                s.SetAttribute("NormalTicketSettingQuantity", m.NormalTicketSettingQuantity.ToString());
                s.SetAttribute("VIPTicketSettingQuantity", m.VIPTicketSettingQuantity.ToString());
                s.SetAttribute("CreatedDate", DateTime.Now.ToString());
                s.SetAttribute("CreatedBy", System.Web.HttpContext.Current.User.Identity.Name);
                root.AppendChild(s);
            }
            return xmlDoc.InnerXml;
        }

        public static string SetTicketsXml(ConsultantPost CP, List<Ticket> Tickets)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes");
            XmlElement root = xmlDoc.CreateElement("root");
            xmlDoc.AppendChild(root);

            //List<Ticket> T = setAllTickets(CP);
            string timeNow = DateTime.Now.ToString();
            foreach (var t in Tickets)
            {

                XmlElement s = xmlDoc.CreateElement("s");
                s.SetAttribute("MappingKey", t.MappingKey.ToString());
                s.SetAttribute("EventKey", CP.EventKey.ToString());
                s.SetAttribute("TicketKey", t.TicketKey.ToString());
                s.SetAttribute("SessionKey", t.SessionKey.ToString());
                s.SetAttribute("SMSToken", t.SMSToken);
                s.SetAttribute("TicketType", ((short)t.TicketType).ToString());
                s.SetAttribute("TicketFrom", ((short)TicketFrom.Import).ToString());
                s.SetAttribute("TicketStatus", ((short)TicketStatus.Created).ToString());
                s.SetAttribute("SessionStartDate", t.SessionStartDate.ToString());
                s.SetAttribute("SessionEndDate", t.SessionEndDate.ToString());
                s.SetAttribute("CreatedDate", timeNow);
                s.SetAttribute("CreatedBy", System.Web.HttpContext.Current.User.Identity.Name);
                root.AppendChild(s);
            }


            return xmlDoc.InnerXml;
        }

        public static List<Ticket> setAllTickets(ConsultantPost CP)
        {
            List<Ticket> Tickets = new List<Ticket>();

            if (CP.Type == 0) return Tickets;

            List<Ticket> applyTickets = HomeDAO.queryApplyTickets(CP.EventKey);

            List<PB_EventSession> Sessions = HomeDAO.getSessions(CP.EventKey.ToString());

            Sessions.ForEach(s =>
            {
                s.NormalTicketUsedCount = applyTickets.FindAll(e => e.SessionKey == s.SessionKey && e.TicketType == TicketType.Normal).Count;
                s.VipTicketUsedCount = applyTickets.FindAll(e => e.SessionKey == s.SessionKey && e.TicketType == TicketType.VIP).Count;

            });

            #region 生成所有票
            for (int j = 0; j < CP.EventConsultants.Count; j++)
            {
                var m = CP.EventConsultants[j];

                if (string.IsNullOrEmpty(m.DirectSellerID) || m.DirectSellerID.Length != 12) continue;

                for (int i = 0; i < m.NormalTicketSettingQuantity; i++)
                {
                    Ticket t = new Ticket();
                    t.TicketKey = Guid.NewGuid();
                    t.MappingKey = m.MappingKey.Value;
                    t.EventKey = CP.EventKey;
                    t.SMSToken = MakeSMSToken();
                    t.TicketType = TicketType.Normal;
                    t.SessionKey = findSessionKey(TicketType.Normal, Sessions);
                    t.SessionStartDate = t.SessionKey !=Guid.Empty ?Sessions.FirstOrDefault(e => e.SessionKey == t.SessionKey).SessionStartDate:DateTime.MinValue;
                    t.SessionEndDate = t.SessionKey!=Guid.Empty? Sessions.FirstOrDefault(e => e.SessionKey == t.SessionKey).SessionEndDate:DateTime.MinValue;
                    Tickets.Add(t);

                    if (t.SessionKey == Guid.Empty) continue;
                    Sessions.First(e => e.SessionKey == t.SessionKey).NormalTicketUsedCount += 1;
                }
                for (int i = 0; i < m.VIPTicketSettingQuantity; i++)
                {
                    Ticket t = new Ticket();
                    t.TicketKey = Guid.NewGuid();
                    t.MappingKey = m.MappingKey.Value;
                    t.EventKey = CP.EventKey;
                    t.SMSToken = MakeSMSToken();
                    t.TicketType = TicketType.VIP;
                    t.SessionKey = findSessionKey(TicketType.VIP, Sessions);
                    t.SessionStartDate = t.SessionKey != Guid.Empty ? Sessions.FirstOrDefault(e => e.SessionKey == t.SessionKey).SessionStartDate : DateTime.MinValue;
                    t.SessionEndDate = t.SessionKey != Guid.Empty ? Sessions.FirstOrDefault(e => e.SessionKey == t.SessionKey).SessionEndDate : DateTime.MinValue;
                    Tickets.Add(t);

                    if (t.SessionKey == Guid.Empty) continue;
                    Sessions.First(e => e.SessionKey == t.SessionKey).VipTicketUsedCount += 1;
                }
            }
            #endregion


            return Tickets;
        }

        private static Guid findSessionKey(TicketType TicketType, List<PB_EventSession> Sessions)
        {
            Guid sessionKey = Guid.Empty;
            if (TicketType == TicketType.Normal)
            {
                PB_EventSession session = Sessions.OrderBy(e => e.NormalTicketUsedCount).ToList().FirstOrDefault(e => e.NormalTicketQuantity > e.NormalTicketUsedCount);
                if (session == null) return sessionKey;
                sessionKey = session.SessionKey;
            }
            else if (TicketType == TicketType.VIP)
            {
                PB_EventSession session = Sessions.OrderBy(e => e.VipTicketUsedCount).ToList().FirstOrDefault(e => e.VIPTicketQuantity > e.VipTicketUsedCount);
                if (session == null) return sessionKey;
                sessionKey = session.SessionKey;
            }
            return sessionKey;

        }

        public static PB_Event getEvent(Guid EventKey)
        {
            SearchModel SM = new SearchModel
            {
                Page = 1,
                pageSize = 10,
                TableName = "PB_Event (nolock) ",
                Field = "*",
                OrderBy = "CreatedDate desc",
                Descript = "",
                Filter = "eventkey = '" + EventKey + "'",
                MaxPage = 0,
                TotalRow = 0
            };

            List<PB_Event> Events = HomeDAO.getEvents(SM);

            return Events.Count > 0 ? Events[0] : null;
        }


    }

    
}