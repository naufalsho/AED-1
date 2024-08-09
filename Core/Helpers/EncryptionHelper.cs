using System.Security.Cryptography;
using System.Text;

namespace Core.Helpers
{
    public static class EncryptionHelper
    {
        public static string AesEncrypt(string toEncrypt)
        {
            byte[] encrypted;

            using (Aes aesAlg = Aes.Create())
            {
                using (SHA256 hash = SHA256.Create())
                {
                    aesAlg.Key = hash.ComputeHash(Encoding.ASCII.GetBytes(ConsSecurity.AesKey));
                    aesAlg.IV = Convert.FromBase64String(ConsSecurity.AesVector);
                }

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(toEncrypt);
                        }

                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(encrypted, 0, encrypted.Length).Replace('+', '-').Replace('/', '_').Replace("=", "");
        }

        public static string AesDecrypt(string toDecrypt)
        {
            while (toDecrypt.Length % 4 != 0)
                toDecrypt += "=";
            toDecrypt = toDecrypt.Replace('-', '+').Replace('_', '/');

            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            string plaintext = null;

            using (Aes aesAlg = Aes.Create())
            {
                using (SHA256 hash = SHA256.Create())
                {
                    aesAlg.Key = hash.ComputeHash(Encoding.ASCII.GetBytes(ConsSecurity.AesKey));
                    aesAlg.IV = Convert.FromBase64String(ConsSecurity.AesVector);
                }

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(toEncryptArray))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }
    }
}
