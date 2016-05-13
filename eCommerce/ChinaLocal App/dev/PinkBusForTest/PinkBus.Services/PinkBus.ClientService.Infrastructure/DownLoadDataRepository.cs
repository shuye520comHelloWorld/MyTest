using ICSharpCode.SharpZipLib.BZip2;
using Newtonsoft.Json;
using PinkBus.ClientServices.Contract;
using PinkBus.ClientServices.Interface;
using PinkBus.Services.Common;
using PinkBus.Services.Entity.Operation;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PinkBus.ClientServices.Infrastructure
{
    public class DownLoadDataRepository : IDownLoadDataRepository
    {
        private TicketOperation ticketOperation;
        private ConsultantOperation consultantOperation;
        private CustomerOperation customerOperation;

        public DownLoadDataRepository()
        {
            ticketOperation = new TicketOperation(GlobalAppSettings.Community);
            consultantOperation = new ConsultantOperation(GlobalAppSettings.Community);
            customerOperation = new CustomerOperation(GlobalAppSettings.Community);
        }


        public DownloadDataResponse DownloadData(DownloadData dto)
        {
            object obj =null;
            //obj = null;
            if (dto.SyncDataType == 1)
            {
                 obj = consultantOperation.GetDataByFunc(e => e.EventKey == dto.EventKey);
            }
            else if (dto.SyncDataType == 2)
            {
                obj = ticketOperation.GetDataByFunc(e => e.EventKey == dto.EventKey && (e.TicketStatus == 0 || e.TicketStatus == 1 || e.TicketStatus == 2));
            }
            else if (dto.SyncDataType == 3)
            {
                string sql = "select c.* from pb_ticket t join pb_customer c on t.customerkey=c.customerkey where t.eventkey='{0}' and t.TicketStatus in (0,1,2)";
               obj = customerOperation.GetDataByRawSql(string.Format(sql, dto.EventKey));
            }

            if (obj == null)
            {
                throw new HttpError(HttpStatusCode.NotFound, "NotFound", "没有对应的key");
            }

            return new DownloadDataResponse() { data = Compress(JsonConvert.SerializeObject(obj)) };

            //Thread.Sleep(10000);
            //if (dto.DownloadPassCode == "1")
            //{
            //    var date = DateTime.Now;
            //    string d = Compress(JsonConvert.SerializeObject(data));
            //    LogHelper.Info("Compress");
            //    LogHelper.Error("Compress+"+(DateTime.Now - date).TotalSeconds.ToString());
            //    return new CheckTokenResponse() { data =d };
            //}
            //else
            //{
            //    var date = DateTime.Now;
            //    string d = JsonConvert.SerializeObject(data);
            //    LogHelper.Info("NoCompress");
            //    LogHelper.Error("NoCompress+"+(DateTime.Now - date).TotalSeconds.ToString());
            return new DownloadDataResponse() { data = JsonConvert.SerializeObject("1") };
            //}
        }


        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string Compress(string input)
        {
            string result = string.Empty;
            byte[] buffer = Encoding.UTF8.GetBytes(input);
            using (MemoryStream outputStream = new MemoryStream())
            {
                using (BZip2OutputStream zipStream = new BZip2OutputStream(outputStream))
                {
                    zipStream.Write(buffer, 0, buffer.Length);
                    zipStream.Close();
                }
                return Convert.ToBase64String(outputStream.ToArray());
            }
        }
    }
}
