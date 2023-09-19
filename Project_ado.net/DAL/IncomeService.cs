using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Project_ado.net.Models;

namespace Project_ado.net.DAL
{
    internal class IncomeService
    {
        private const string TABLE_NAME = "dbo.Income";

        public static async Task<List<Income>> GetAllIncomesAsync()
        {
            string query = $"SELECT * FROM {TABLE_NAME};";

            return await DataAccessLayer.ExecuteQueryAsync(query, ReaderToIncomeList);
        }

        public static async Task<Income> GetIncomeById(int id)
        {
            string query = $"SELECT *" +
                $"FROM {TABLE_NAME}" +
                $" WHERE Id = {id}";

            return await DataAccessLayer.ExecuteQueryAsync(query, ReadToIncome);
        }

        public static async Task CreateIncome(Income newIncome)
        {
            ThrowIfNull(newIncome);

            string formattedDate = newIncome.Date.ToString("yyyy-MM-dd HH:mm:ss");

            string command = $"INSERT INTO {TABLE_NAME} (Description, Amount, Date, CategoryId) VALUES ('{newIncome.Description}', {newIncome.Amount}, '{formattedDate}', {newIncome.CategoryId})";

            await DataAccessLayer.ExecuteNonQueryAsync(command);
        }

        public static async Task UpdateIncome(Income incomeToUpdate)
        {
            ThrowIfNull(incomeToUpdate);

            string command = $"UPDATE {TABLE_NAME} SET Description = @Description, Amount = @Amount, Date = @Date, CategoryId = @CategoryId WHERE Id = @Id";

            SqlParameter[] parameters =
            {
            new SqlParameter("@Description", incomeToUpdate.Description),
            new SqlParameter("@Amount", incomeToUpdate.Amount),
            new SqlParameter("@Date", incomeToUpdate.Date),
            new SqlParameter("@CategoryId", incomeToUpdate.CategoryId),
            new SqlParameter("@Id", incomeToUpdate.Id)
        };

            await DataAccessLayer.ExecuteNonQueryAsync(command, parameters);
        }

        public static async Task DeleteIncome(int id)
        {
            string command = $"DELETE {TABLE_NAME} WHERE Id = {id}";

            await DataAccessLayer.ExecuteNonQueryAsync(command);
        }

        private static Income ReadToIncome(SqlDataReader reader)
        {
            ThrowIfNull(reader);

            if (reader.HasRows)
            {
                Income Income = null;
                while (reader.Read())
                {
                    Income = new Income
                    {
                        Id = reader.GetInt32(0),
                        Description = reader.GetString(1),
                        Amount = reader.GetDecimal(2),
                        Date = reader.GetDateTime(3),
                        CategoryId = reader.GetInt32(4)
                    };
                }

                return Income;
            }

            return null;
        }

        private static List<Income> ReaderToIncomeList(SqlDataReader reader)
        {
            ThrowIfNull(reader);

            List<Income> result = new List<Income>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(reader.GetOrdinal("Id"));
                    string description = reader.GetString(reader.GetOrdinal("Description"));
                    decimal amount = reader.GetDecimal(reader.GetOrdinal("Amount"));
                    DateTime date = reader.GetDateTime(reader.GetOrdinal("Date"));
                    int categoryId = reader.GetInt32(reader.GetOrdinal("CategoryId"));

                    Income income = new Income(id, description, amount, date, categoryId);

                    result.Add(income);
                }
            }

            return result;
        }

        private static void ThrowIfNull<T>(T value) where T : class
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }
        }

    }
}