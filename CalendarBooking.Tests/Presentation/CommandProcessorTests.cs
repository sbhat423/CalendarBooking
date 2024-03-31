using CalendarBooking.Business;
using CalendarBooking.ConsoleApp;
using Moq;

namespace CalendarBooking.Tests.Presentation
{
    public class CommandProcessorTests
    {
        [Fact]
        public async Task ProcessCommand_ValidAddCommand_CallsAddAppointment()
        {
            // Arrange
            var mockService = new Mock<IAppointmentService>();
            var processor = new CommandProcessor(mockService.Object);
            string input = "ADD 02/04 10:00";

            // Act
            await processor.ProcessCommand(input);

            // Assert
            mockService.Verify(s => s.AddAppointment(It.IsAny<DateTime>()), Times.Once);
        }

        [Fact]
        public async Task ProcessCommand_ValidDeleteCommand_CallsDeleteAppointment()
        {
            // Arrange
            var mockService = new Mock<IAppointmentService>();
            var processor = new CommandProcessor(mockService.Object);
            string input = "DELETE 02/04 10:00";

            // Act
            await processor.ProcessCommand(input);

            // Assert
            mockService.Verify(s => s.DeleteAppointment(It.IsAny<DateTime>()), Times.Once);
        }

        [Fact]
        public async Task ProcessCommand_ValidFindCommand_CallsFindFreeTimeslot()
        {
            // Arrange
            var mockService = new Mock<IAppointmentService>();
            var processor = new CommandProcessor(mockService.Object);
            string input = "FIND 02/04";

            // Act
            await processor.ProcessCommand(input);

            // Assert
            mockService.Verify(s => s.FindFreeTimeslot(It.IsAny<DateTime>()), Times.Once);
        }

        [Fact]
        public async Task ProcessCommand_ValidKeepCommand_CallsKeepTimeslot()
        {
            // Arrange
            var mockService = new Mock<IAppointmentService>();
            var processor = new CommandProcessor(mockService.Object);
            string input = "KEEP 10:00";

            // Act
            await processor.ProcessCommand(input);

            // Assert
            mockService.Verify(s => s.KeepTimeslot(It.IsAny<DateTime>()), Times.Once);
        }

        [Fact]
        public async Task ProcessCommand_AddOperationWithInvalidArguments_DisplaysMessageOnConsole()
        {
            // Arrange
            var mockConsole = new Mock<TextWriter>();
            Console.SetOut(mockConsole.Object);

            var mockService = new Mock<IAppointmentService>();
            var processor = new CommandProcessor(mockService.Object);
            string input = "ADD"; // Invalid command, missing arguments

            // Act
            await processor.ProcessCommand(input);

            // Assert
            mockConsole.Verify(c => c.WriteLine("Invalid command. Command should contain 3 arguments"), Times.Once);
        }

        [Fact]
        public async Task ProcessCommand_FindOperationWithInvalidDate_DisplaysMessageOnConsole()
        {
            // Arrange
            var mockConsole = new Mock<TextWriter>();
            Console.SetOut(mockConsole.Object);

            var mockService = new Mock<IAppointmentService>();
            var processor = new CommandProcessor(mockService.Object);
            string input = "FIND not_a_date"; // Invalid date format

            // Act
            await processor.ProcessCommand(input);
            
            // Assert
            mockConsole.Verify(c => c.WriteLine("Invalid date format. Usage the format - DD/MM"), Times.Once);
        }

        [Fact]
        public async Task ProcessCommand_KeepOperationWithInvalidTime_DisplaysMessageOnConsole()
        {
            // Arrange
            var mockConsole = new Mock<TextWriter>();
            Console.SetOut(mockConsole.Object);

            var mockService = new Mock<IAppointmentService>();
            var processor = new CommandProcessor(mockService.Object);
            string input = "KEEP not_a_time"; // Invalid time format

            // Act
            await processor.ProcessCommand(input);

            // Assert
            mockConsole.Verify(c => c.WriteLine("Invalid time format. Use the format - hh:mm"), Times.Once);
        }
    }
}
