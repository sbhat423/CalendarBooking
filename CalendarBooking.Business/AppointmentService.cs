using CalendarBooking.Data;

namespace CalendarBooking.Business
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        
        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task AddAppointment(DateTime dateTime)
        {
            var fromTime = dateTime;
            var toTime = dateTime.AddMinutes(Constants.SLOT_DURATION_MINUTES);

            AppointmentHelper.ValidateTimeslot(dateTime);

            var appointmentExists = await _appointmentRepository.CheckTimeSlot(dateTime);
            if (appointmentExists)
            {
                throw new ArgumentException($"Slot {dateTime} is already used.");
            }

            await _appointmentRepository.AddAppointment(fromTime, toTime);
        }

        public async Task DeleteAppointment(DateTime dateTime)
        {
            AppointmentHelper.ValidateTimeslot(dateTime);

            await _appointmentRepository.DeleteAppointment(dateTime);
        }

        public async Task<DateTime?> FindFreeTimeslot(DateTime date)
        {
            DateTime startDateTime = new DateTime(date.Year, date.Month, date.Day, 9, 0, 0); // Start from 9:00 AM
            DateTime endDateTime = new DateTime(date.Year, date.Month, date.Day, 17, 0, 0); // End at 5:00 PM

            var busySlots = await _appointmentRepository.ListAppointments(startDateTime, endDateTime);

            for (DateTime slot = startDateTime; slot < endDateTime; slot = slot.AddMinutes(Constants.SLOT_DURATION_MINUTES))
            {
                bool isSlotFree = true;
                foreach (var busySlot in busySlots)
                {
                    if (slot.TimeOfDay == busySlot.FromTime)
                    {
                        isSlotFree = false;
                        break;
                    }
                }

                if (isSlotFree)
                {
                    AppointmentHelper.ValidateTimeslot(slot);
                    return slot;
                }
            }
            return null;
        }

        public async Task KeepTimeslot(DateTime dateTime)
        {
            AppointmentHelper.ValidateTimeslot(dateTime);
            
            await CheckForUsedSlotThroughOutTheYear(dateTime);
            await BookSlotThroughOutTheYear(dateTime);
        }

        private async Task CheckForUsedSlotThroughOutTheYear(DateTime dateTime)
        {
            var currentYear = dateTime.Year;
            for (int month = 1; month <= 12; month++)
            {
                for (int day = 1; day <= DateTime.DaysInMonth(currentYear, month); day++)
                {
                    var currentDate = new DateTime(currentYear, month, day, dateTime.Hour, dateTime.Minute, dateTime.Second);
                    var result = await _appointmentRepository.CheckTimeSlot(currentDate);
                    if (result == true)
                    {
                        throw new ArgumentException($"Cannot keep the current time slot as the slot is already booked on {currentDate}");
                    }
                }
            }
        }

        private async Task BookSlotThroughOutTheYear(DateTime dateTime)
        {
            var currentYear = dateTime.Year;
            for (int month = 1; month <= 12; month++)
            {
                for (int day = 1; day <= DateTime.DaysInMonth(currentYear, month); day++)
                {
                    var currentDateTime = new DateTime(currentYear, month, day, dateTime.Hour, dateTime.Minute, dateTime.Second);
                    await _appointmentRepository.AddAppointment(currentDateTime, currentDateTime.AddMinutes(Constants.SLOT_DURATION_MINUTES));
                }
            }
        }
    }
}
