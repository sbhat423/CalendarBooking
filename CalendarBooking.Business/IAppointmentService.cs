namespace CalendarBooking.Business
{
    public interface IAppointmentService
    {
        Task AddAppointment(DateTime dateTime);

        Task DeleteAppointment(DateTime dateTime);

        Task<DateTime?> FindFreeTimeslot(DateTime date);

        Task KeepTimeslot(DateTime time);
    }
}
