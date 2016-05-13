using ICSharpCode.SharpZipLib.BZip2;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace PinkBus.CheckInClient.Helper
{
    public static class Common
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



        #region  判断网络连接状态

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int connectionDescription, int reservedValue);
        public static bool IsConnected()
        {
            int I = 0;
            bool state = InternetGetConnectedState(out I, 0);
            return state;
        }

        #endregion


        public static string EncodeByBase64(string stringParam)
        {
            byte[] abytes = Encoding.Default.GetBytes(stringParam);
            return Convert.ToBase64String(abytes);

        }

        public static void Base64StringToImage(string contentStr, string savedImageFileName)
        {
            try
            {
                byte[] arr = Convert.FromBase64String(contentStr);
                MemoryStream ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);

                var headerFolder = AppDomain.CurrentDomain.BaseDirectory + @"\Data\HeaderImgs\";

                if (!System.IO.Directory.Exists(headerFolder))
                {
                    System.IO.Directory.CreateDirectory(headerFolder);
                }
                bmp.Save(headerFolder + savedImageFileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                ms.Close();
            }
            catch (Exception ex)
            {
                // MessageBox.Show("Base64StringToImage 转换失败\nException：" + ex.Message);
            }
        }

        public static string MakeSMSToken()
        {
            object syncLock = new object();
            lock (syncLock)
            {
                string guid = Guid.NewGuid().ToString().ToUpper().Replace("A", "2").Replace("B", "6").Replace("C", "8").Replace("D", "1").Replace("E", "9").Replace("F", "3");
                string token = guid.Split('-')[4].Substring(3, 6);
                return token;
            }

        }


        public static string DeserializeQRCode(string codeString)
        {
            try
            {
                byte[] bytetest = System.Text.Encoding.Default.GetBytes(codeString.ToUpper());
                List<string> strs = new List<string>();
                foreach (byte b in bytetest)
                {
                    int n = (b - AppSetting.Offset - 65) / AppSetting.Pivot;
                    strs.Add(n.ToString());
                }

                strs.Reverse();
                string sellerId = "";
                for (int i = 0; i < strs.Count; i++)
                {
                    if (i == 1 || i == 7 || i == 13) continue;

                    sellerId += strs[i];
                }
                return sellerId;
            }
            catch (Exception ex)
            {
                return "0";
            }
        }

        public static string SerializeQRCode(string codeString)
        {
            List<string> strs=new List<string>();
            char[] codeChar=codeString.ToCharArray();
            int ii=0;
            for (int i = 0; i < codeChar.Length+3; i++)
            {
                if (i == 1 || i == 7 || i == 13)
                {
                    string str = Guid.NewGuid().ToString().Substring(0, 1);
                    strs.Add(str);
                    continue;
                }
                
                strs.Add(codeChar[ii].ToString());
                ii++;
            }
            strs.Reverse();
            string ASCIIString = "";
            for (int i = 0; i < 15;i++ )
            {
                if (i == 1 || i == 7 || i == 13)
                {
                    ASCIIString += strs[i];
                }
                else
                {
                    int n = 65 + int.Parse(strs[i]) * AppSetting.Pivot + AppSetting.Offset;
                    string s = ((char)n).ToString();
                    ASCIIString += s;
                }
            }
            return ("https://qrCode.mkiapp.com/Dashboard/ECard/" + ASCIIString).ToLower();

        }
        


    }

}
