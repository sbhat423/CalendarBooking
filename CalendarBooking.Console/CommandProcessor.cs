using CalendarBooking.Business;

namespace CalendarBooking.ConsoleApp
{
    public class CommandProcessor : ICommandProcessor
    {
        private readonly IAppointmentService _appointmentService;

        public CommandProcessor(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public async Task ProcessCommand(string input)
        {
            try
            {
                string[] args = input.Split(' ');
                string command = args[0].ToUpper();

                switch (command)
                {
                    case "ADD":
                        await ProcessAddOperation(args);
                        break;

                    case "DELETE":
                        await ProcessDeleteOperation(args);
                        break;

                    case "FIND":
                        await ProcessFindOperation(args);
                        break;

                    case "KEEP":
                        await ProcessKeepOperation(args);
                        break;

                    default:
                        Console.WriteLine("Invalid command.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task ProcessAddOperation(string[] args)
        {
            CommandProcessorHelper.ValidateForNumberOfArguments(args, 3);
            var parsedDateTime = CommandProcessorHelper.ParseDateAndTime(args[1], args[2]);

            await _appointmentService.AddAppointment(parsedDateTime);

            Console.WriteLine("ADD operation completed successfully!");
        }

        private async Task ProcessDeleteOperation(string[] args)
        {
            CommandProcessorHelper.ValidateForNumberOfArguments(args, 3);
            var parsedDateTime = CommandProcessorHelper.ParseDateAndTime(args[1], args[2]);

            await _appointmentService.DeleteAppointment(parsedDateTime);

            Console.WriteLine("DELETE operation completed successfully!");
        }

        private async Task ProcessFindOperation(string[] args)
        {
            CommandProcessorHelper.ValidateForNumberOfArguments(args, 2);
            var parsedDate = CommandProcessorHelper.ParseDate(args[1]);

            var freeSlot = await _appointmentService.FindFreeTimeslot(parsedDate)
                ?? throw new ArgumentException("No free slots available for the day");

            Console.WriteLine($"Free slot is available on: {freeSlot}");
        }

        private async Task ProcessKeepOperation(string[] args)
        {
            CommandProcessorHelper.ValidateForNumberOfArguments(args, 2);
            var parsedTime = CommandProcessorHelper.ParseTime(args[1]);

            await _appointmentService.KeepTimeslot(parsedTime);

            Console.WriteLine("KEEP operation completed successfully!");
        }
    }
}
