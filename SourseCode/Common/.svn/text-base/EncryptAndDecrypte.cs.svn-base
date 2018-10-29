using System;

using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    /// <summary>
    /// 采用 3DES 加解密方式。
    /// </summary>
    public class EncryptAndDecrypte
    {
        static string strKey = "fdbc4y6hdhKlf4M3mjgGrMC3PbryXrxw";
        static string strIV = "RfnMfrpec48=";
        /// <summary>
        /// 默认加密字符串。 
        /// </summary>
        /// <param name="ConnString"></param>
        /// <returns></returns>
        public static string EncryptString(string encryptString)
        {
            if (string.IsNullOrWhiteSpace(encryptString)) return encryptString;

            return Convert.ToBase64String(EncryptAndDecrypte.EncryptString(
                encryptString,
                Convert.FromBase64String(strKey),
                Convert.FromBase64String(strIV)
            )
            );
        }

        /// <summary>
        /// 默认解密字符串   
        /// </summary>
        /// <param name="EncryptedConnectionString"></param>
        /// <returns></returns>
        public static string DecrypteString(string EncryptedConnectionString)
        {
            if (string.IsNullOrWhiteSpace(EncryptedConnectionString)) return EncryptedConnectionString;

            return EncryptAndDecrypte.DecrypteString(
                Convert.FromBase64String(EncryptedConnectionString),
                 Convert.FromBase64String(strKey),
                Convert.FromBase64String(strIV)
            ).TrimEnd('\0');
        }
        /// <summary>
        /// 使用指定的 Key 和 IV 加密 。 
        /// </summary>
        /// <param name="ToEncryptString"></param>
        /// <param name="byKey"></param>
        /// <param name="byIV"></param>
        /// <returns></returns>
        private static byte[] EncryptString(string ToEncryptString, byte[] byKey, byte[] byIV)
        {
            if (string.IsNullOrWhiteSpace(ToEncryptString)) return null;

            MemoryStream memStm = new MemoryStream();
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

            CryptoStream encStream = null;
            encStream = new CryptoStream(
               memStm, tdes.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write
               );

            byte[] byIn = Encoding.Default.GetBytes(ToEncryptString);
            encStream.Write(byIn, 0, byIn.Length);
            encStream.FlushFinalBlock();
            encStream.Close();
            return memStm.ToArray();
        }


        /// <summary>
        /// 使用指定的 Key 和 IV 解密。 
        /// </summary>
        /// <param name="byIn"></param>
        /// <param name="byKey"></param>
        /// <param name="byIV"></param>
        /// <returns></returns>
        private static string DecrypteString(byte[] byIn, byte[] byKey, byte[] byIV)
        {
            if (byIn == null || byIn.Length == 0) return string.Empty;

            MemoryStream memStm = new MemoryStream(byIn);
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

            CryptoStream encStream = new CryptoStream(
               memStm, tdes.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read
               );

            byte[] fromEncrypt = new byte[byIn.Length];
            encStream.Read(fromEncrypt, 0, fromEncrypt.Length);
            encStream.Close();

            string strRet = Encoding.Default.GetString(fromEncrypt);
            return strRet;
        }

        /// <summary>
        /// 获取随机（种子是 GUID 的 Byte 的和）长度的Byte数组.
        /// </summary>
        /// <param name="Len">要得到的数组的长度</param>
        /// <returns></returns>
        private static byte[] GetBytes(int Len)
        {
            int Seed = 0;
            byte[] bySeed = Guid.NewGuid().ToByteArray();

            foreach (byte byt in bySeed)
            {
                Seed += byt;
            }

            byte[] byKey = new byte[Len];
            new Random(Seed).NextBytes(byKey);
            return byKey;
        }

        /// <summary>
        /// 获取 Key 和 IV ， 如果失败，返回null。 
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="IV"></param>
        public static void TryGetKeyAndIV(out byte[] Key, out byte[] IV)
        {
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

            for (int i = 200; i > 0; i--)
            {
                try
                {
                    Key = GetBytes(i);
                    IV = GetBytes(i);
                    tdes.CreateDecryptor(Key, IV);
                    return;
                }
                catch
                {
                }
            }
            Key = null;
            IV = null;
            return;
        }
    }
}