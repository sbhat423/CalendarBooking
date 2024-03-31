using CalendarBooking.Models;

namespace CalendarBooking.Data
{
    public interface IAppointmentRepository
    {
        Task AddAppointment(DateTime fromTime, DateTime toTime);

        Task<List<Appointment>> ListAppointments(DateTime startDateTime, DateTime endDateTime);

        Task<bool> CheckTimeSlot(DateTime dateTime);

        Task DeleteAppointment(DateTime dateTime);
    }
}
