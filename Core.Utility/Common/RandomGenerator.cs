using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;

namespace Core.Utility.Common
{
    public class RandomStringGenerator
    {
        public string APIKeyRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public string RandomCaps(int length = 2)
        {
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZ";
            Random random = new Random();
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }

        public string RandomChars(int length = 8)
        {
            string validChars = "abcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }

        public string RandomSymbols(int length = 2)
        {
            string validChars = "!@#$%^&*";
            Random random = new Random();
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }

        public string RandomPassword()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomCaps());
            builder.Append(RandomChars());
            builder.Append(RandomSymbols());
            return new string(builder.ToString().OrderBy(x => Guid.NewGuid()).ToArray());
        }

       public static string GenerateRandomUsersPassword()
        {
            int length = 8;
            const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+";
            char[] chars = new char[length];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] bytes = new byte[sizeof(uint)];
                for (int i = 0; i < length; i++)
                {
                    rng.GetBytes(bytes);
                    uint num = BitConverter.ToUInt32(bytes, 0);
                    chars[i] = validChars[(int)(num % (uint)validChars.Length)];
                }
            }
            return new string(chars);
        }
    }
}