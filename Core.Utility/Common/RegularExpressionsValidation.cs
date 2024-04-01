namespace Core.Utility.Common
{
    public class RegularExpressionsValidation
    {
        // check only number is found if any alphabeate is found then give the false
        public bool IsNumeric(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            foreach (char c in input)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }

            return true;
        }
    }
}