using CalendarBooking.Business;
using CalendarBooking.Data;
using CalendarBooking.Models;
using Moq;

namespace CalendarBooking.Tests.Business
{
    public class AppointmentServiceTests
    {
        [Fact]
        public async Task AddAppointment_ValidTimeSlot_Successful()
        {
            // Arrange
            var appointmentRepositoryMock = new Mock<IAppointmentRepository>();
            appointmentRepositoryMock.Setup(repo => repo.CheckTimeSlot(It.IsAny<DateTime>())).ReturnsAsync(false);
            var appointmentService = new AppointmentService(appointmentRepositoryMock.Object);
            DateTime validDateTime = DateTime.Today.AddHours(9);

            // Act & Assert
            try
            {
                await appointmentService.AddAppointment(validDateTime);
            }
            catch (Exception)
            {
                Assert.Fail("Exception should not be thrown for valid inputs.");
            }
        }

        [Fact]
        public async Task AddAppointment_AlreadyUsedTimeSlot_ThrowsArgumentException()
        {
            // Arrange
            var appointmentRepositoryMock = new Mock<IAppointmentRepository>();
            appointmentRepositoryMock.Setup(repo => repo.CheckTimeSlot(It.IsAny<DateTime>())).ReturnsAsync(true);
            var appointmentService = new AppointmentService(appointmentRepositoryMock.Object);
            DateTime validDateTime = DateTime.Today.AddHours(9);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => appointmentService.AddAppointment(validDateTime));
        }

        [Fact]
        public async Task DeleteAppointment_ValidTimeSlot_Successful()
        {
            // Arrange
            var appointmentRepositoryMock = new Mock<IAppointmentRepository>();
            var appointmentService = new AppointmentService(appointmentRepositoryMock.Object);
            DateTime validDateTime = DateTime.Today.AddHours(9);

            // Act & Assert
            try
            {
                await appointmentService.DeleteAppointment(validDateTime);
            }
            catch (Exception)
            {
                Assert.Fail("Exception should not be thrown for valid inputs.");
            }

        }

        [Fact]
        public async Task FindFreeTimeslot_ValidDate_ReturnsFreeSlot()
        {
            // Arrange
            DateTime validDate = DateTime.Today;
            var appointmentRepositoryMock = new Mock<IAppointmentRepository>();
            appointmentRepositoryMock
                .Setup(repo => repo.ListAppointments(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(new List<Appointment>() 
                {
                    new Appointment() 
                    {
                        Id = 1,
                        Date = validDate,
                        FromTime = validDate.AddHours(9).TimeOfDay,
                        ToTime = validDate.AddHours(9).AddMinutes(30).TimeOfDay,
                    }
                });
            var appointmentService = new AppointmentService(appointmentRepositoryMock.Object);

            // Act
            var result = await appointmentService.FindFreeTimeslot(validDate);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task KeepTimeslot_ValidTimeSlot_Successful()
        {
            // Arrange
            var appointmentRepositoryMock = new Mock<IAppointmentRepository>();
            appointmentRepositoryMock.Setup(repo => repo.CheckTimeSlot(It.IsAny<DateTime>())).ReturnsAsync(false);
            var appointmentService = new AppointmentService(appointmentRepositoryMock.Object);
            DateTime validDateTime = DateTime.Today.AddHours(9);

            // Act & Assert
            try
            {
                await appointmentService.KeepTimeslot(validDateTime);
            }
            catch (Exception)
            {
                Assert.Fail("Exception should not be thrown for valid inputs.");
            }
        }
    }
}
