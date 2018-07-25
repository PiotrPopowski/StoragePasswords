using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StoragePasswords.Models
{
    static class CryptographyService
    {
        public static string Encrypt(string textToEncrypt, string hashCode, string VIKey, string SaltKey)
        {
            int bcount = Encoding.UTF8.GetByteCount(VIKey);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(textToEncrypt);
            byte[] keyBytes = new Rfc2898DeriveBytes(hashCode, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros, BlockSize=256 };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes.ToArray());

        }

        public static ObservableCollection<string> Encrypt(ObservableCollection<string> collection, string hashCode, string VIKey, string SaltKey)
        {
            ObservableCollection<string> encryptedCollection = new ObservableCollection<string>();
            foreach (string s in collection)
            {
                encryptedCollection.Add(Encrypt(s, hashCode, VIKey, SaltKey));
            }
            return encryptedCollection;
        }

        public static string Decrypt(string encryptedText, string hashCode, string VIKey, string SaltKey)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = new Rfc2898DeriveBytes(hashCode, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None, BlockSize=256 };
            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());

        }

        public static ObservableCollection<string> Decrypt(ObservableCollection<string> collection, string hashCode, string VIKey, string SaltKey)
        {
            ObservableCollection<string> newCollection = new ObservableCollection<string>();
            foreach (string s in collection)
            {
                newCollection.Add(Decrypt(s, hashCode, VIKey, SaltKey));
            }
            return newCollection;
        }
        public static byte[] GenerateKey(int maxLength = 32)
        {
            var key = new byte[maxLength];
            var random = RNGCryptoServiceProvider.Create();
            Random rng = new Random();
            random.GetNonZeroBytes(key);
            int bsize;
            for(int i=0;i<key.Length;i++)
            {
                bsize = key[i];
                if(bsize<32 || bsize>126)
                {
                    key[i] = (byte)rng.Next(32, 126);
                }
            }
            int bcount = key.Length;
            return key;
        }
    }
}
