using System.Globalization;

namespace CalendarBooking.ConsoleApp
{
    public static class CommandProcessorHelper
    {
        public static void ValidateForNumberOfArguments(string[] parts, int numberOfArgument)
        {
            if (parts.Length != numberOfArgument)
            {
                throw new ArgumentException($"Invalid command. Command should contain {numberOfArgument} arguments");
            }
        }

        public static DateTime ParseTime(string inputString)
        {
            if (!DateTime.TryParseExact(inputString, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDateTime))
            {
                throw new ArgumentException("Invalid time format. Use the format - hh:mm");
            }
            return parsedDateTime;
        }

        public static DateTime ParseDate(string inputString)
        {
            if (!DateTime.TryParseExact(inputString, "dd/MM", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            {
                throw new ArgumentException("Invalid date format. Usage the format - DD/MM");
            }
            return parsedDate;
        }

        public static DateTime ParseDateAndTime(string date, string time)
        {
            if (!DateTime.TryParseExact(date + " " + time, "dd/MM HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDateTime))
            {
                throw new ArgumentException("Invalid date/time format. Usage the format - DD/MM hh:mm");
            }
            return parsedDateTime;
        }
    }
}
