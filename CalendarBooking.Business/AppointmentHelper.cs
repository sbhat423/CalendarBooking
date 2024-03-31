namespace CalendarBooking.Business
{
    public static class AppointmentHelper
    {
        public static void ValidateTimeslot(DateTime dateTime)
        {
            ValidateTimeSlotInterval(dateTime);
            ValidateBetweenRegularStartAndEndTimes(dateTime);
            ValidateBetweenReservedTimes(dateTime);
        }

        private static void ValidateBetweenReservedTimes(DateTime dateTime)
        {
            if (dateTime.DayOfWeek == DayOfWeek.Tuesday && dateTime.Day >= 15 && dateTime.Day <= 21 &&
                            dateTime.TimeOfDay >= TimeSpan.FromHours(16) && dateTime.TimeOfDay < TimeSpan.FromHours(17))
            {
                throw new ArgumentException($"Time slot falls within the exception period (4 PM to 5 PM) on the second day of the third week of any month");
            }
        }

        private static void ValidateBetweenRegularStartAndEndTimes(DateTime dateTime)
        {
            if (dateTime.TimeOfDay < TimeSpan.FromHours(9) || dateTime.TimeOfDay >= TimeSpan.FromHours(17))
            {
                throw new ArgumentException($"Time slot falls outside the acceptable range (9AM to 5PM)");
            }
        }

        private static void ValidateTimeSlotInterval(DateTime dateTime)
        {
            if (dateTime.Minute % Constants.SLOT_DURATION_MINUTES != 0)
            {
                throw new ArgumentException($"Time should be in the interval of {Constants.SLOT_DURATION_MINUTES} minutes");
            }
        }
    }
}
