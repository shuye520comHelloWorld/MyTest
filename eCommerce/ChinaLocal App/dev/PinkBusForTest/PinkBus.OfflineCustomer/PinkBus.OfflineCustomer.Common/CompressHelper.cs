using ICSharpCode.SharpZipLib.BZip2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.OfflineCustomer.Common
{
    public class CompressHelper
    {
        /// <summary>
        /// 解压缩
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Decompress(string input)
        {
            string result = string.Empty;
            byte[] buffer = Convert.FromBase64String(input);
            using (Stream inputStream = new MemoryStream(buffer))
            {
                BZip2InputStream zipStream = new BZip2InputStream(inputStream);

                using (StreamReader reader = new StreamReader(zipStream, Encoding.UTF8))
                {
                    //输出
                    result = reader.ReadToEnd();
                }
            }

            return result;
        }


        public static string EncodeByBase64(string stringParam)
        {
            byte[] abytes = Encoding.Default.GetBytes(stringParam);
            return Convert.ToBase64String(abytes);

        }

        //public static void Base64StringToImage(string contentStr, string savedImageFileName)
        //{
        //    try
        //    {
        //        byte[] arr = Convert.FromBase64String(contentStr);
        //        MemoryStream ms = new MemoryStream(arr);
        //        Bitmap bmp = new Bitmap(ms);

        //        var headerFolder = AppDomain.CurrentDomain.BaseDirectory + @"\Data\HeaderImgs\";

        //        if (!System.IO.Directory.Exists(headerFolder))
        //        {
        //            System.IO.Directory.CreateDirectory(headerFolder);
        //        }
        //        bmp.Save(headerFolder + savedImageFileName, System.Drawing.Imaging.ImageFormat.Jpeg);
        //        ms.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        // MessageBox.Show("Base64StringToImage 转换失败\nException：" + ex.Message);
        //    }
        //}
    }
}
