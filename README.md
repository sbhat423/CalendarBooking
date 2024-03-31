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

**Assumptions:**
* All the times are in local timezone
* The calendar works for the current Year as there is no way to input year based on the requirement
* Hours and minutes needs to be entered in 24 hour format
* For simplicity timeslots could be booked only in the interval of 30 minutes (for example 09:00 to 09:30, 09:30 to 10:00. But can't book slots from 09:15 to 09:45)

**Improvements, TODO:**
* Additionl flag needs to be added to the Appointment table in the database, to identiy between the slots that are booked by appointment and those that are marked by the KEEP operation.
* Would like to use Entity Framework Core, but there is an opened bug with the EFCore that can't be used with console app as Microsoft.Extensions.Hosting is used.
  <br>Source: https://github.com/dotnet/efcore/issues/32835
  <br>That is the reason for using SqlClient.
* Logs need to be added
* Appsettings.json need to be added to proivide connection strings. And also appsettings.json for different environment needs to be added.
* Due to the limited time edge cases might not be covered in the unit/integration testing.
* Due to the limited time algorithm is not optimized. The algorithm could be made efficient.
<br>
The CalendarBooking application is a software system designed to manage calendar bookings efficiently. It is structured into several libraries, each serving a specific purpose. These libraries include:

* **CalendarBooking.ConsoleApp:** This library contains the console application responsible for interacting with users through the command-line interface. Users can input commands and receive feedback regarding calendar bookings.

* **CalendarBooking.Business:** The business logic of the CalendarBooking application resides in this library. It handles the core functionalities such as adding, deleting, Finding and Keepoing appointments, as well as any other business rules associated with managing bookings.

* **CalendarBooking.Data:** The data access layer is encapsulated within this library. It provides functionalities to interact with the underlying data storage. This layer abstracts away the complexities of data access, providing a clean interface for the business logic to work with.

* **CalendarBooking.Models:** This library contains the data models used throughout the application. There is only Appointment model at the moment. They define the structure of the data and facilitate communication between different layers of the application.

* **CalendarBooking.Tests:** Unit tests for various components of the CalendarBooking application are housed in this library. These tests ensure that each part of the application behaves as expected and remains functional even after modifications or updates.
<br>

**Usage:**
To utilize the CalendarBooking application, follow these steps:

* **Console Application (CalendarBooking.ConsoleApp):**
Run the console application to interact with the CalendarBooking system.
Follow the prompts or commands provided by the application to manage calendar bookings.

* **Business Logic (CalendarBooking.Business):**
If extending or modifying business rules, make changes within this library.
Ensure that any updates align with the intended behavior of the application.

* **Data Access (CalendarBooking.Data):**
Modify data access methods or interfaces in this library to adapt to different data storage solutions.
Implement appropriate data access logic based on the requirements of the application.

* **Models (CalendarBooking.Models):**
Update or extend data models within this library to accommodate changes in the application's data structure.
Ensure consistency between data models used across different layers of the application.

* **Unit Testing (CalendarBooking.Tests):**
Run unit tests to verify the correctness of each component within the application.
Write additional tests as needed to cover new functionalities or edge cases.

**Dependencies**
The CalendarBooking application may have dependencies on external libraries or frameworks. Ensure that these dependencies are installed and configured properly to run the application successfully.

**License**
The CalendarBooking application is licensed under MIT License. See the LICENSE.txt file for more details.
