# CalendarBooking
This is a simple calendar application used to book appointments.
<br>
This project is used to demonstrate the skills in the following areas:
* Database Development
* C# Programming
* Dependency Injection
* Unit Testing
* Documentation

**NOTE:** To run this application set the project **CalendarBooking.ConsoleApp** as the **startup project**
<br> The connection string is provided in the program.cs file.

Assumptions:
* All the times are in local timezone
* The calendar works for the current Year as there is no way to input year based on the requirement
* Hours and minutes needs to be entered in 24 hour format
* For simplicity timeslots could be booked only in the interval of 30 minutes (for example 09:00 to 09:30, 09:30 to 10:00. But can't book slots from 09:15 to 09:45)

Improvements todo:
* Additionl flag needs to be added to the Appointment table in the database, to identiy between the slots that are booked by appointment and those that are marked by the KEEP operation.
* Would like to use Entity Framework Core, but there is an opened bug with the EFCore that can't be used with console app as Microsoft.Extensions.Hosting is used.
  <br>Source: https://github.com/dotnet/efcore/issues/32835
  <br>That is the reason for using SqlClient.
* Logs need to be added
* Appsettings.json need to be added to proivide connection strings. And also appsettings.json for different environment needs to be added.
* Due to the limited time edge cases might not be covered in the unit/integration testing.
* Due to the limited time algorithm is not optimized. The algorithm could be made efficient.
