using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TravelerShop.Helpers
{
    public static class CookieGenerator
    {
        private const string SaltData = "8C6a11BcdC0AD4918c992cC8672Bc42CF21A666379325C937c87e891a693F185" +
                                        "fF64E6979De62c1C3d424949ECcDD9B4D290Fe6f9e051F44DAa858297c4779a5" +
                                        "3483d57d2b493D5a8D1B9CB0D031D8fe249829eCf9b5219a7F6d4107d7813be0" +
                                        "3eaaEB0D70628F36EC4dD361c2E0868Aa1Bc44A8Ac8d4Ea54d46Dw1fu8due899";

        private static readonly byte[] Salt = Encoding.ASCII.GetBytes(SaltData);

        public static string Create(string value)
        {
            return EncryptStringAes(value, "8FeqwR53108c85ee4f41Fj8Ud9I12Qb4364177a6Ld3O01cb68fFhwu36IEFuajkIW3EU83873ddd4ad8bfdeead722ccfb886cFhe28fd9d08bb5182e316f417055e84f00");
        }
        public static string Validate(string value)
        {
            return DecryptStringAes(value, "8FeqwR53108c85ee4f41Fj8Ud9I12Qb4364177a6Ld3O01cb68fFhwu36IEFuajkIW3EU83873ddd4ad8bfdeead722ccfb886cFhe28fd9d08bb5182e316f417055e84f00");
        }
        private static string EncryptStringAes(string plainText, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = Salt;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        private static string DecryptStringAes(string cipherText, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = Salt;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
