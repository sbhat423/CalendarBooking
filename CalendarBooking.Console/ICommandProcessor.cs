namespace CalendarBooking.ConsoleApp
{
    public interface ICommandProcessor
    {
        Task ProcessCommand(string input);
    }
}
