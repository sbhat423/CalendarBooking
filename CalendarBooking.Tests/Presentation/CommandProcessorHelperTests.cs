using CalendarBooking.ConsoleApp;

namespace CalendarBooking.Tests.Presentation
{
    public class CommandProcessorHelperTests
    {
        [Theory]
        [InlineData(new string[] { "arg1", "arg2" }, 2)]
        [InlineData(new string[] { "arg1", "arg2", "arg3" }, 3)]
        [InlineData(new string[] { }, 0)]
        public void ValidateForNumberOfArguments_ValidInputs_Successful(string[] parts, int numberOfArgument)
        {
            try
            {
                // Arrange & Act & Assert
                CommandProcessorHelper.ValidateForNumberOfArguments(parts, numberOfArgument);
            }
            catch (ArgumentException)
            {
                Assert.Fail("ArgumentException should not be thrown for valid inputs.");
            }
        }

        [Theory]
        [InlineData(new string[] { "arg1" }, 2)]
        [InlineData(new string[] { "arg1", "arg2", "arg3", "arg4" }, 3)]
        public void ValidateForNumberOfArguments_InvalidInputs_ThrowsArgumentException(string[] parts, int numberOfArgument)
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => CommandProcessorHelper.ValidateForNumberOfArguments(parts, numberOfArgument));
        }

        [Theory]
        [InlineData("09:00")]
        [InlineData("12:00")]
        [InlineData("17:00")]
        public void ParseTime_ValidInputs_Successful(string inputString)
        {
            // Arrange & Act
            DateTime parsedTime = CommandProcessorHelper.ParseTime(inputString);

            // Assert
            Assert.NotNull(parsedTime);
        }

        [Theory]
        [InlineData("09:")]
        [InlineData("12:00:00")]
        [InlineData("25:00")]
        [InlineData("abc")]
        public void ParseTime_InvalidInputs_ThrowsArgumentException(string inputString)
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => CommandProcessorHelper.ParseTime(inputString));
        }

        [Theory]
        [InlineData("01/01")]
        [InlineData("31/12")]
        public void ParseDate_ValidInputs_Successful(string inputString)
        {
            // Arrange & Act
            DateTime parsedDate = CommandProcessorHelper.ParseDate(inputString);

            // Assert
            Assert.NotNull(parsedDate);
        }

        [Theory]
        [InlineData("01/")]
        [InlineData("31/12/2023")]
        [InlineData("32/12")]
        [InlineData("abc")]
        public void ParseDate_InvalidInputs_ThrowsArgumentException(string inputString)
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => CommandProcessorHelper.ParseDate(inputString));
        }

        [Theory]
        [InlineData("01/01", "09:00")]
        [InlineData("31/12", "12:00")]
        public void ParseDateAndTime_ValidInputs_Successful(string date, string time)
        {
            // Arrange & Act
            DateTime parsedDateTime = CommandProcessorHelper.ParseDateAndTime(date, time);

            // Assert
            Assert.NotNull(parsedDateTime);
        }

        [Theory]
        [InlineData("01/01", "09:")]
        [InlineData("31/12", "12:00:00")]
        [InlineData("01/", "09:00")]
        [InlineData("31/12/2023", "12:00")]
        [InlineData("32/12", "12:00")]
        [InlineData("abc", "09:00")]
        public void ParseDateAndTime_InvalidInputs_ThrowsArgumentException(string date, string time)
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => CommandProcessorHelper.ParseDateAndTime(date, time));
        }
    }
}