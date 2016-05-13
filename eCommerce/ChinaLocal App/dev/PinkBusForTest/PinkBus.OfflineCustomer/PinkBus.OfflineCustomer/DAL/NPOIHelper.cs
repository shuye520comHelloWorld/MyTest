using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using PinkBus.OfflineCustomer.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace PinkBus.OfflineCustomer.Helper
{
    public class NPOIHelper
    {

        /// <summary>
        /// DataTable导出到Excel文件
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        /// <param name="strFileName">保存位置</param>
        public static void Export(DataTable dtSource, string strHeaderText, string strFileName)
        {
            using (MemoryStream ms = Export(dtSource, strHeaderText))
            {
                using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
        }

        /// <summary>
        /// DataTable导出到Excel的MemoryStream
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        public static MemoryStream Export(DataTable dtSource, string strHeaderText)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet();

            #region 右击文件 属性信息
            {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "NPOI";
                workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = "文件作者信息"; //填加xls文件作者信息
                si.ApplicationName = "创建程序信息"; //填加xls文件创建程序信息
                si.LastAuthor = "最后保存者信息"; //填加xls文件最后保存者信息
                si.Comments = "作者信息"; //填加xls文件作者信息
                si.Title = "标题信息"; //填加xls文件标题信息
                si.Subject = "主题信息";//填加文件主题信息
                si.CreateDateTime = DateTime.Now;
                workbook.SummaryInformation = si;
            }
            #endregion

            var dateStyle = workbook.CreateCellStyle();
            var format = workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

            //取得列宽
            int[] arrColWidth = new int[dtSource.Columns.Count];
            foreach (DataColumn item in dtSource.Columns)
            {
                arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
            }
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                for (int j = 0; j < dtSource.Columns.Count; j++)
                {
                    int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                    if (intTemp > arrColWidth[j])
                    {
                        arrColWidth[j] = intTemp;
                    }
                }
            }
            int rowIndex = 0;
            foreach (DataRow row in dtSource.Rows)
            {
                #region 新建表，填充表头，填充列头，样式
                if (rowIndex == 65535 || rowIndex == 0)
                {
                    if (rowIndex != 0)
                    {
                        sheet = workbook.CreateSheet();
                    }

                    #region 表头及样式
                    {
                        var headerRow = sheet.CreateRow(0);
                        headerRow.HeightInPoints = 25;
                        headerRow.CreateCell(0).SetCellValue(strHeaderText);

                        var headStyle = workbook.CreateCellStyle();
                        headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                        var font = workbook.CreateFont();
                        font.FontHeightInPoints = 20;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);
                        headerRow.GetCell(0).CellStyle = headStyle;
                        sheet.AddMergedRegion(Region.ConvertToCellRangeAddress(new Region(0, 0, 0, dtSource.Columns.Count - 1)));
                        //headerRow.Dispose();
                    }
                    #endregion


                    #region 列头及样式
                    {
                        var headerRow = sheet.CreateRow(1);
                        var headStyle = workbook.CreateCellStyle();
                        headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                        var font = workbook.CreateFont();
                        font.FontHeightInPoints = 10;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);
                        foreach (DataColumn column in dtSource.Columns)
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                            headerRow.GetCell(column.Ordinal).CellStyle = headStyle;

                            //设置列宽
                            sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 256);
                        }
                        //headerRow.Dispose();
                    }
                    #endregion

                    rowIndex = 2;
                }
                #endregion


                #region 填充内容
                var dataRow = sheet.CreateRow(rowIndex);
                foreach (DataColumn column in dtSource.Columns)
                {
                    var newCell = dataRow.CreateCell(column.Ordinal);

                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String"://字符串类型
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime"://日期类型
                            DateTime dateV;
                            DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);

                            newCell.CellStyle = dateStyle;//格式化显示
                            break;
                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal"://浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }

                }
                #endregion

                rowIndex++;
            }
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;

                //sheet.Dispose();
                //workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet
                return ms;
            }
        }

        ///// <summary>
        ///// 用于Web导出
        ///// </summary>
        ///// <param name="dtSource">源DataTable</param>
        ///// <param name="strHeaderText">表头文本</param>
        ///// <param name="strFileName">文件名</param>
        //public static void ExportByWeb(DataTable dtSource, string strHeaderText, string strFileName)
        //{ 
        //    HttpContext curContext = HttpContext.Current;

        //    // 设置编码和附件格式
        //    curContext.Response.ContentType = "application/vnd.ms-excel";
        //    curContext.Response.ContentEncoding = Encoding.UTF8;
        //    curContext.Response.Charset = "";
        //    curContext.Response.AppendHeader("Content-Disposition",
        //        "attachment;filename=" + HttpUtility.UrlEncode(strFileName, Encoding.UTF8));

        //    curContext.Response.BinaryWrite(Export(dtSource, strHeaderText).GetBuffer());
        //    curContext.Response.End(); 
        //} 

        /// <summary>读取excel
        /// 默认第一行为标头
        /// </summary>
        /// <param name="strFileName">excel文档路径</param>
        /// <returns></returns>
        public static DataTable Import(string strFileName)
        {
            DataTable dt = new DataTable();

            XSSFWorkbook xssfworkbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                xssfworkbook = new XSSFWorkbook(file);
            }
            var sheet = xssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            var headerRow = sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;

            for (int j = 0; j < cellCount; j++)
            {
                var cell = headerRow.GetCell(j);
                dt.Columns.Add(cell.ToString());
            }
            List<Volunteer> volunteers = new List<Volunteer>();
            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                var row = sheet.GetRow(i);
                DataRow dataRow = dt.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                        dataRow[j] = row.GetCell(j).ToString();
                }

                dt.Rows.Add(dataRow);
            }
            return dt;
        }

        public static List<Volunteer> GetVolunteers(string strFileName)
        {
            DataTable dt = Import(strFileName);
            List<Volunteer> volunteers = new List<Volunteer>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                Volunteer volunteer = new Volunteer
                {
                    MappingKey = row["MappingKey"].ToString() == string.Empty ? Guid.Empty : new Guid(row["MappingKey"].ToString()),
                    DirectSellerId = row["DirectSellerId"].ToString(),
                    EventKey = row["EventKey"].ToString() == string.Empty ? Guid.Empty : new Guid(row["EventKey"].ToString()),
                    ConsultantName = row["ConsultantName"].ToString(),
                    ConsultantLevel = row["ConsultantLevel"].ToString(),
                    ConsultantPhone = row["ConsultantPhone"].ToString(),
                    County = row["County"].ToString(),
                    City = row["City"].ToString(),
                    Province = row["Province"].ToString(),
                    CustomerQuantity = 0,
                    CheckinDate = DateTime.Parse(row["CheckinDate"].ToString())
                };
                volunteers.Add(volunteer);
            }
            return volunteers;
        }

        public static void ExportVolunteers(string savedFileName, List<VolunteerForExcel> data)
        {
            var tmp = AppDomain.CurrentDomain.BaseDirectory + @"\Data\Volunteer.xlsx";
            using (FileStream file = new FileStream(tmp, FileMode.Open, FileAccess.Read))
            {
                var workBook = new XSSFWorkbook(file);
                var sheet = workBook.GetSheet("Sheet1");

                var i = 0;
                foreach (var item in data)
                {
                    var row = sheet.CreateRow(i++);
                    row.CreateCell(0).SetCellValue(item.EventKey.ToString());
                    row.CreateCell(1).SetCellValue(item.MappingKey.ToString());
                    row.CreateCell(2).SetCellValue(item.DirectSellerId);
                    row.CreateCell(3).SetCellValue(item.ConsultantName);
                    row.CreateCell(4).SetCellValue(item.ConsultantPhone);
                    row.CreateCell(5).SetCellValue(item.ConsultantLevel);
                    row.CreateCell(6).SetCellValue(item.Province);
                    row.CreateCell(7).SetCellValue(item.City);
                    row.CreateCell(8).SetCellValue(item.County);
                    row.CreateCell(9).SetCellValue(item.CheckinDate.ToString("yyyy-MM-dd HH:mm:ss"));

                    SetRowStyle(row, workBook, 10);
                }

                var newfile = new FileStream(savedFileName, FileMode.Create);
                workBook.Write(newfile);
                newfile.Close();

            }
        }

        public static void ExportAllCheckin(string savedFileName, List<Customer> data, Event ev)
        {
            var tmp = AppDomain.CurrentDomain.BaseDirectory + @"\Data\Customer.xlsx";
            using (FileStream file = new FileStream(tmp, FileMode.Open, FileAccess.Read))
            {
                var workBook = new XSSFWorkbook(file);
                var sheet = workBook.GetSheet("Sheet1");

                string timeStr = ev.CheckinStartDate.ToString("yyyy.MM.dd") + " -- " + ev.CheckinEndDate.ToString("yyyy.MM.dd");
                var titleRowCell = sheet.GetRow(0).Cells[0];
                titleRowCell.SetCellValue(titleRowCell.StringCellValue.Replace("{title}", ev.EventTitle).Replace("{time}", timeStr));
                var i = 2;
                var cStyle = workBook.CreateCellStyle();
                cStyle.BorderBottom = cStyle.BorderLeft = cStyle.BorderRight = cStyle.BorderTop = BorderStyle.Thin;

                foreach (var item in data)
                {
                    var row = sheet.CreateRow(i++);
                  
                    row.CreateCell(0).SetCellValue(item.CustomerName);
                    switch (item.ContactType)
                    {
                        case "PhoneNumber": item.ContactInfo = string.Format("{0}(电话)", item.ContactInfo); break;
                        case "QQ": item.ContactInfo = string.Format("{0}(QQ)", item.ContactInfo); break;
                        case "Wechat": item.ContactInfo = string.Format("{0}(微信)", item.ContactInfo); break;
                        case "Other": item.ContactInfo = string.Format("{0}(其它)", item.ContactInfo); break;
                    }
                    row.CreateCell(1).SetCellValue(item.ContactInfo);

                    string customeType = string.Empty;
                    switch (item.CustomerType)
                    {
                        case "0": customeType = "在校学生"; break;
                        case "1": customeType = "老顾客"; break;
                        case "2": customeType = "新顾客"; break;
                    }

                    string ageRange = string.Empty;
                    switch (item.AgeRange)
                    {
                        case "Bellow25": ageRange = "25岁以下"; break;
                        case "Between2535": ageRange = "25-35岁"; break;
                        case "Between3545": ageRange = "35-45岁"; break;
                        case "Above45": ageRange = "45岁以上"; break;
                    }
                    row.CreateCell(2).SetCellValue(customeType);
                    row.CreateCell(3).SetCellValue(ageRange);
                    row.CreateCell(4).SetCellValue(item.DirectSellerId);

                    foreach (ICell c in row.Cells)
                    {
                        c.CellStyle = cStyle;
                    }

                }

                var newfile = new FileStream(savedFileName, FileMode.Create);
                workBook.Write(newfile);
                newfile.Close();

            }
        }


        private static void SetRowStyle(IRow row, XSSFWorkbook wb, int columns)
        {
            for (var i = 0; i < columns; i++)
            {
                var m = row.Cells[i];
                var cStyle = wb.CreateCellStyle();
                cStyle.BorderBottom = BorderStyle.Thin;
                cStyle.BorderLeft = BorderStyle.Thin;
                cStyle.BorderRight = BorderStyle.Thin;
                cStyle.BorderTop = BorderStyle.Thin;
                m.CellStyle = cStyle;
            }
        }


    }
}
