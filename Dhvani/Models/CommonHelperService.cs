using Core.Utility.Common;

namespace Core.Models
{
    public class CommonHelperService
    {
        public class EncryptionHelper
        {
            public static string EncryptDefaultPassword()
            {
                var defaultPassword = "password";
                return new Encription().Encrypt(defaultPassword);
            }
        }
    }
}