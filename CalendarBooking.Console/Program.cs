using CalendarBooking.Business;
using CalendarBooking.Data;
using Microsoft.Extensions.DependencyInjection;

namespace CalendarBooking.ConsoleApp
{
    class Program
    {
        public static async Task Main(string[] args) 
        {

            var serviceProvider = SetupDependencyInjection();

            var commandProcessor = serviceProvider.GetService<ICommandProcessor>();

            Console.WriteLine("Welcome to Appointment Scheduler!");

            while (true)
            {
                Console.WriteLine("Enter command:");
                string input = Console.ReadLine();
                await commandProcessor.ProcessCommand(input);
                Console.WriteLine("--------------------------\n");
            }
        }

        static IServiceProvider SetupDependencyInjection()
        {
            return new ServiceCollection()
                 .AddTransient<IAppointmentService, AppointmentService>()
                 .AddTransient<ICommandProcessor, CommandProcessor>()
               .AddTransient<IAppointmentRepository>(provider =>
                   new AppointmentRepository("Server=.;Database=CalendarBooking;Trusted_Connection=True"))
               .BuildServiceProvider();
        }
    }
}