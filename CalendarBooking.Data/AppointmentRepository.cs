using CalendarBooking.Models;
using System.Data.SqlClient;

namespace CalendarBooking.Data
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly string _connectionString;

        public AppointmentRepository(string connectionString)
        {
            _connectionString = connectionString;
            EnsureDatabaseCreated();
            EnsureTableCreated();
        }

        public async Task AddAppointment(DateTime fromTime, DateTime toTime)
        {
            string query = "INSERT INTO Appointments (Date, FromTime, ToTime) VALUES (@Date, @FromTime, @ToTime)";
            SqlParameter[] parameters =
            {
                new SqlParameter("@Date", fromTime),
                new SqlParameter("@FromTime", fromTime),
                new SqlParameter("@ToTime", toTime)
            };

            await ExecuteNonQueryAsync(query, parameters);
        }

        public async Task<bool> CheckTimeSlot(DateTime dateTime)
        {
            string query = "SELECT COUNT(*) FROM Appointments WHERE Date = @Date AND FromTime = @Time";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    DateTime dateOnly = dateTime.Date;
                    TimeSpan timeOnly = dateTime.TimeOfDay;

                    command.Parameters.AddWithValue("@Date", dateOnly);
                    command.Parameters.AddWithValue("@Time", timeOnly);

                    var result = await command.ExecuteScalarAsync();

                    int count = Convert.ToInt32(result);
                    return count > 0;
                }
            }
        }

        public async Task DeleteAppointment(DateTime dateTime)
        {
            var date = dateTime.Date;
            var time = dateTime.TimeOfDay;

            string query = "DELETE FROM Appointments WHERE Date = @Date AND FromTime = @Time";
            SqlParameter[] parameters =
            {
                new SqlParameter("@Date", date),
                new SqlParameter("@Time", time)
            };

            await ExecuteNonQueryAsync(query, parameters);
        }

        public async Task<List<Appointment>> ListAppointments(DateTime startDateTime, DateTime endDateTime)
        {
            var appointments = new List<Appointment>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM Appointments WHERE Date = @date AND FromTime >= @startTime AND ToTime <= @endTime ORDER BY FromTime ASC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@date", startDateTime.Date);
                    command.Parameters.AddWithValue("@startTime", startDateTime.TimeOfDay);
                    command.Parameters.AddWithValue("@endTime", endDateTime.TimeOfDay);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            appointments.Add(new Appointment
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                                FromTime = reader.GetTimeSpan(reader.GetOrdinal("FromTime")),
                                ToTime = reader.GetTimeSpan(reader.GetOrdinal("ToTime")),
                            });
                        }
                    }
                }
            }
            return appointments;
        }

        private void EnsureDatabaseCreated()
        {
            string databaseName = new SqlConnectionStringBuilder(_connectionString).InitialCatalog;

            using (var masterConnection = new SqlConnection(_connectionString.Replace(databaseName, "master")))
            {
                masterConnection.Open();
                using (var command = masterConnection.CreateCommand())
                {
                    command.CommandText = $"IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '{databaseName}') CREATE DATABASE {databaseName}";
                    command.ExecuteNonQuery();
                }
            }
        }

        private void EnsureTableCreated()
        {
            string query = @"
                IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'Appointments')
                BEGIN
                    CREATE TABLE Appointments (
                        Id INT PRIMARY KEY IDENTITY,
                        Date DATE,
                        FromTime TIME,
                        ToTime TIME,
                    );
                END";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        private async Task ExecuteNonQueryAsync(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                await connection.OpenAsync();
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
