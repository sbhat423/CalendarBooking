using CalendarBooking.Business;

namespace CalendarBooking.Tests.Business
{
    public class AppointmentHelperTests
    {
        [Theory]
        [InlineData("2024-03-31 09:00:00")]
        [InlineData("2024-03-31 09:30:00")]
        [InlineData("2024-03-31 16:30:00")]
        public void ValidateTimeslot_ValidTimeSlot_Successful(string dateTimeString)
        {
            // Arrange
            DateTime dateTime = DateTime.Parse(dateTimeString);
            try
            {
                // Act & Assert
                AppointmentHelper.ValidateTimeslot(dateTime);
            }
            catch (ArgumentException)
            {
                Assert.Fail("ArgumentException should not be thrown for valid inputs.");
            }
        }

        [Theory]
        [InlineData("2024-03-31 08:59:59")]
        [InlineData("2024-03-31 17:00:01")]
        [InlineData("2024-03-19 16:30:00")]
        [InlineData("2024-03-19 16:59:59")]
        [InlineData("2024-03-19 15:59:59")]
        [InlineData("2024-03-19 17:00:00")]
        public void ValidateTimeslot_InvalidTimeSlot_ThrowsArgumentException(string dateTimeString)
        {
            // Arrange
            DateTime dateTime = DateTime.Parse(dateTimeString);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => AppointmentHelper.ValidateTimeslot(dateTime));
        }

        [Theory]
        [InlineData("2024-03-31 09:15:00")]
        [InlineData("2024-03-31 09:32:00")]
        public void ValidateTimeslot_InvalidInterval_ThrowsArgumentException(string dateTimeString)
        {
            // Arrange
            DateTime dateTime = DateTime.Parse(dateTimeString);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => AppointmentHelper.ValidateTimeslot(dateTime));
        }

        [Theory]
        [InlineData("2024-03-19 16:30:00")]
        [InlineData("2024-03-19 16:59:59")]
        [InlineData("2024-03-19 15:59:59")]
        [InlineData("2024-03-19 17:00:00")]
        public void ValidateTimeslot_ExceptionPeriod_ThrowsArgumentException(string dateTimeString)
        {
            // Arrange
            DateTime dateTime = DateTime.Parse(dateTimeString);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => AppointmentHelper.ValidateTimeslot(dateTime));
        }
    }
}
