using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_ado.net.Models;

namespace Project_ado.net.DAL
{
    internal class ExpenseService
    {

        private const string TABLE_NAME = "dbo.Expense";

        public static async Task<List<Expense>> GetAllExpensesAsync()
        {
            string query = $"SELECT * FROM {TABLE_NAME};";

            return await DataAccessLayer.ExecuteQueryAsync(query, ReaderToExpenseList);
        }

        public static async Task<Expense> GetExpenseById(int id)
        {
            string query = $"SELECT *" +
                $"FROM {TABLE_NAME}" +
                $" WHERE Id = {id}";

            return await DataAccessLayer.ExecuteQueryAsync(query, ReadToExpense);
        }

        public static async Task CreateExpense(Expense newExpense)
        {
            ThrowIfNull(newExpense);

            // Properly format the Date value as a string and add single quotes
            string formattedDate = newExpense.Date.ToString("yyyy-MM-dd HH:mm:ss");

            // Use parameterized queries to prevent SQL injection
            string command = $"INSERT INTO {TABLE_NAME} (Description, Amount, Date, CategoryId) VALUES ('{newExpense.Description}', {newExpense.Amount}, '{formattedDate}', {newExpense.CategoryId})";

            await DataAccessLayer.ExecuteNonQueryAsync(command);
        }

        public static async Task UpdateExpense(Expense ExpenseToUpdate)
        {
            ThrowIfNull(ExpenseToUpdate);

            string command = $"UPDATE {TABLE_NAME} SET Description = @Description, Amount = @Amount, Date = @Date, CategoryId = @CategoryId WHERE Id = @Id";

            SqlParameter[] parameters =
            {
            new SqlParameter("@Description", ExpenseToUpdate.Description),
            new SqlParameter("@Amount", ExpenseToUpdate.Amount),
            new SqlParameter("@Date", ExpenseToUpdate.Date),
            new SqlParameter("@CategoryId", ExpenseToUpdate.CategoryId),
            new SqlParameter("@Id", ExpenseToUpdate.Id)
        };

            await DataAccessLayer.ExecuteNonQueryAsync(command, parameters);
        }

        public static async Task DeleteExpense(int id)
        {
            string command = $"DELETE {TABLE_NAME} WHERE Id = {id}";

            await DataAccessLayer.ExecuteNonQueryAsync(command);
        }

        private static Expense ReadToExpense(SqlDataReader reader)
        {
            ThrowIfNull(reader);

            if (reader.HasRows)
            {
                Expense Expense = null;
                while (reader.Read())
                {
                    Expense = new Expense
                    {
                        Id = reader.GetInt32(0),
                        Description = reader.GetString(1),
                        Amount = reader.GetDecimal(2),
                        Date = reader.GetDateTime(3),
                        CategoryId = reader.GetInt32(4)
                    };
                }

                return Expense;
            }

            return null;
        }

        private static List<Expense> ReaderToExpenseList(SqlDataReader reader)
        {
            ThrowIfNull(reader);

            List<Expense> result = new List<Expense>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(reader.GetOrdinal("Id"));
                    string description = reader.GetString(reader.GetOrdinal("Description"));
                    decimal amount = reader.GetDecimal(reader.GetOrdinal("Amount"));
                    DateTime date = reader.GetDateTime(reader.GetOrdinal("Date"));
                    int categoryId = reader.GetInt32(reader.GetOrdinal("CategoryId"));

                    Expense Expense = new Expense(id, description, amount, date, categoryId);

                    result.Add(Expense);
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