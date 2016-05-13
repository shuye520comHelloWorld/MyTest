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
    public class SimpleDES : IThreeDES
    {
        const string hashAlgorithm = "SHA1";    //can be "MD5"
        const int passwordIterations = 9;   //can be any number
        const int keySize = 256;    //can be 192 or 128
        const string _defaultKey = "0AD69463-D48F-4480-BE86-A6B07D71FB4B";
        private byte[] initVectorBytes;
        private byte[] keyBytes;
        public SimpleDES()
        {
            string key = System.Configuration.ConfigurationManager.AppSettings["InvitationTokenKey"];
            string tempKey = string.IsNullOrWhiteSpace(key) ? _defaultKey : key;

            initVectorBytes = Encoding.UTF8.GetBytes(tempKey.Substring(0, 16));
            byte[] saltValueBytes = Encoding.UTF8.GetBytes(tempKey.Substring(16, 10));
            string passPhrase = tempKey.Substring(26, 10);
            using (PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations))
            {
                keyBytes = password.GetBytes(keySize / 8);
            }
        }

        public string Encrypt(string value)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(value);

            using (RijndaelManaged symmetricKey = new RijndaelManaged())
            {
                symmetricKey.Mode = CipherMode.CBC;
                using (ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes))
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                        cryptoStream.FlushFinalBlock();

                        byte[] cipherTextBytes = memoryStream.ToArray();
                        return Convert.ToBase64String(cipherTextBytes);
                    }
                }
            }
        }

        public string Decrypt(string value)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(value);
            using (RijndaelManaged symmetricKey = new RijndaelManaged())
            {
                symmetricKey.Mode = CipherMode.CBC;
                using (ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes))
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(cipherTextBytes, 0, cipherTextBytes.Length);
                        cryptoStream.FlushFinalBlock();
                        return Encoding.UTF8.GetString(memoryStream.ToArray());
                    }
                }
            }
        }
    }
}
