namespace Core.Utility.Common
{
    public class CommonValidationMethod
    {
        public bool TryConvertToDateFormat(string inputDate, string inputFormat, out string formattedDate)
        {
            formattedDate = null;

            if (DateTime.TryParseExact(inputDate, inputFormat, null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
            {
                formattedDate = parsedDate.ToString("yyyy-MM-dd");
                return true;
            }

            return false;
        }

        // This Method Validate the Date-Of-Birth in YYYY-MM-DD format value is proper format then give true
        public bool IsValidDateFormat(string inputDate)
        {
            // Define the expected date format
            string dateFormat = "yyyy-MM-dd";

            // Try to parse the input date using the specified format
            if (DateTime.TryParseExact(inputDate, dateFormat, null, System.Globalization.DateTimeStyles.None, out _))
            {
                // The date is valid and in the correct format
                return true;
            }
            else
            {
                // The date is either in an incorrect format or not a valid date
                return false;
            }
        }
    }
}