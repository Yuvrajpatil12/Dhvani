using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utility.Common
{
    public class RandomGeneratorCustom
    {
        public static string Generate(int length)
        {
            string text = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder stringBuilder = new StringBuilder();
            Random random = new Random();
            while (0 < length--)
            {
                stringBuilder.Append(text[random.Next(text.Length)]);
            }

            return stringBuilder.ToString();
        }

        public static string Generate(int length, Random rnd)
        {
            string text = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder stringBuilder = new StringBuilder();
            while (0 < length--)
            {
                stringBuilder.Append(text[rnd.Next(text.Length)]);
            }

            return stringBuilder.ToString();
        }

        public string GetUniqueKey(int maxSize)
        {
            char[] array = new char[62];
            string text = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            array = text.ToCharArray();
            byte[] array2 = new byte[1];
            RNGCryptoServiceProvider rNGCryptoServiceProvider = new RNGCryptoServiceProvider();
            array2 = new byte[maxSize];
            rNGCryptoServiceProvider.GetNonZeroBytes(array2);
            StringBuilder stringBuilder = new StringBuilder(maxSize);
            byte[] array3 = array2;
            foreach (byte b in array3)
            {
                stringBuilder.Append(array[b % (array.Length - 1)]);
            }

            return stringBuilder.ToString();
        }
    }
}
