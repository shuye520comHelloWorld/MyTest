using iParty.Services.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Services.Infrastructure
{
    public class ThreeDES : IThreeDES
    {
        private byte[] key;
        private byte[] iv;
        private Encoding _encoding;
        const string _defaultKey = "0AD69463-D48F-4480-BE86-A6B07D71FB4B";
        public ThreeDES()
        {
            string key = System.Configuration.ConfigurationManager.AppSettings["InvitationTokenKey"];
            string tempKey = string.IsNullOrWhiteSpace(key) ? _defaultKey : key;
            _encoding = Encoding.UTF8;
            this.key = _encoding.GetBytes(tempKey.Substring(0, 8));
            iv = _encoding.GetBytes(tempKey.Substring(0, 8));
        }

        public string Encrypt(string value)
        {
            byte[] inputStr = _encoding.GetBytes(value);
            using (DESCryptoServiceProvider symmetricAlgorithm = new DESCryptoServiceProvider())
            {
                symmetricAlgorithm.Key = key;
                symmetricAlgorithm.IV = iv;

                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ms, symmetricAlgorithm.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputStr, 0, inputStr.Length);
                    cs.FlushFinalBlock();
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public string Decrypt(string value)
        {
            byte[] inputStr = Convert.FromBase64String(value);
            using (DESCryptoServiceProvider symmetricAlgorithm = new DESCryptoServiceProvider())
            {
                symmetricAlgorithm.Key = key;
                symmetricAlgorithm.IV = iv;

                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ms, symmetricAlgorithm.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputStr, 0, inputStr.Length);
                    cs.FlushFinalBlock();
                    return _encoding.GetString(ms.ToArray());
                }
            }
        }
    }
}
