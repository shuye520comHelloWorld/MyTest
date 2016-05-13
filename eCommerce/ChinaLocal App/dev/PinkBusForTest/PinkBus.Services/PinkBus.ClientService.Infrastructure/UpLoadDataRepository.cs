using Newtonsoft.Json;
using PinkBus.ClientServices.Contract;
using PinkBus.ClientServices.Interface;
using PinkBus.Services.Common;
using PinkBus.Services.Entity;
using PinkBus.Services.Entity.Operation;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.ClientServices.Infrastructure
{
    public class UpLoadDataRepository : IUpLoadDataRepository
    {
        private TicketOperation ticketOperation;
        private ConsultantOperation consultantOperation;
        private CustomerOperation customerOperation;
        private VolunteerCheckinOperation volunteerCheckinOperation;

        private const string SP_PB_ConsultantInfo = "PB_ConsultantInfo";

        public UpLoadDataRepository()
        {
            ticketOperation = new TicketOperation(GlobalAppSettings.Community);
            consultantOperation = new ConsultantOperation(GlobalAppSettings.Community);
            customerOperation = new CustomerOperation(GlobalAppSettings.Community);
            volunteerCheckinOperation = new VolunteerCheckinOperation(GlobalAppSettings.Community);
        }

        public UploadCheckinDataResponse UploadCheckinData(UploadCheckinData dto)
        {
            UploadCheckinDataResponse res = new UploadCheckinDataResponse();

            try
            {
                switch (dto.SyncDataType)
                {
                    case 1:
                        Consultant con = JsonConvert.DeserializeObject<Consultant>(dto.UploadJsonData);
                        var conDB = consultantOperation.GetSingleData(e => e.ConsultantKey == con.ConsultantKey);

                        if (conDB == null)
                        {
                            ConsultantInfo conInfo = GetConsultantInfo(con.DirectSellerId);
                            if (conInfo != null)
                            {
                                con.ContactId = conInfo.ContactID;
                                consultantOperation.Create(con);
                                res.ContactId = con.ContactId.ToString();
                                res.Result = true;
                            }
                            else
                            {
                                con.ContactId = 0;
                                consultantOperation.Create(con);
                                res.ContactId = con.ContactId.ToString();
                                res.Result = false;
                                res.ResponseStatus = new ResponseStatus() { Message = "顾问编号不存在！" };
                            }
                        }
                        else
                        {
                            if (!con.ContactId.HasValue)
                            {
                                ConsultantInfo conInfo = GetConsultantInfo(con.DirectSellerId);
                                if (null != conInfo)
                                {
                                    con.ContactId = conInfo.ContactID;
                                }
                                else
                                {
                                    con.ContactId = 0;
                                }
                                consultantOperation.Update(con);
                                res.Result = false;
                                res.ResponseStatus = new ResponseStatus() { Message = "顾问编号不存在！" };

                            }
                            else
                            {
                                consultantOperation.Update(con);
                                res.Result = true;
                            }

                        }

                        res.ContactId = con.ContactId.ToString();

                        break;
                    case 2:
                        Ticket ticket = JsonConvert.DeserializeObject<Ticket>(dto.UploadJsonData);
                        var ticketDB = ticketOperation.GetSingleData(e => e.TicketKey == ticket.TicketKey);
                        if (ticketDB != null)
                        {
                            if (ticket != ticketDB)
                            {
                                ticketOperation.Update(ticket);
                            }
                            res.Result = true;
                        }
                        else
                        {
                            ticketOperation.Create(ticket);
                            res.Result = true;
                        }
                        break;
                    case 3:
                        Customer customer = JsonConvert.DeserializeObject<Customer>(dto.UploadJsonData);
                        var cusDB = customerOperation.GetSingleData(e => e.CustomerKey == customer.CustomerKey);
                        if (cusDB != null)
                        {
                            if (customer != cusDB)
                            {
                                customerOperation.Update(customer);
                            }
                            res.Result = true;

                        }
                        else
                        {
                            customerOperation.Create(customer);
                            res.Result = true;
                        }
                        break;
                    case 4:
                        VolunteerCheckin checkin = JsonConvert.DeserializeObject<VolunteerCheckin>(dto.UploadJsonData);
                        var checkinDB = volunteerCheckinOperation.GetSingleData(e => e.Key == checkin.Key);
                        if (checkinDB != null)
                        {
                            if (checkin != checkinDB)
                            {
                                volunteerCheckinOperation.Update(checkin);
                            }
                            res.Result = true;

                        }
                        else
                        {
                            volunteerCheckinOperation.Create(checkin);
                            res.Result = true;
                        }
                        break;

                }
                
            }
            catch (Exception ex)
            {
                res.Result = false;
                res.ResponseStatus = new ResponseStatus() { Message=ex.ToString().Replace("'","‘") };
            }

            return res;
        }


        private ConsultantInfo GetConsultantInfo(string directSellerId)
        {
            StringBuilder parameterStr = new StringBuilder();
            parameterStr.Append("<root> <s ");
            parameterStr.Append(string.Format("id='{0}'", directSellerId));
            parameterStr.Append(" /> </root>");

            var parameters = new SqlParameter[]
                        {                         
                            new SqlParameter("@IdsXml",parameterStr.ToString())                               
                        };

            var consultantInfo = RepositoryHelper.Query<ConsultantInfo>(GlobalAppSettings.Community, SP_PB_ConsultantInfo,
              parameters);

           // if (consultantInfo == null || consultantInfo.Count == 0)
                //throw new NotFoundException("编号是否存在");

            return consultantInfo.FirstOrDefault();
        }

    }

    public class ConsultantInfo
    {
        public string DirectSellerID { get; set; }

        //public string ConsultantLevelID { get; set; }

        public long ContactID { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Level { get; set; }

        public string ResidenceID { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        public string County { get; set; }

        public string PhoneNumber { get; set; }


    }
}
