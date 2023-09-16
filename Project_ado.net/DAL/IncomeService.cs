using Project_ado.net.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            string command = $"INSERT INTO {TABLE_NAME} (Description ,Amount,Date,CategoryId) VALUES ('{newIncome.Description},{newIncome.Amount},{newIncome.Date},{newIncome.CategoryId}')";

            await DataAccessLayer.ExecuteNonQueryAsync(command);
        }



        public static async Task UpdateIncome(Income IncomeToUpdate)
        {
            ThrowIfNull(IncomeToUpdate);

            string command = $"UPDATE {TABLE_NAME} SET Description = @Description, Amount = @Amount, Date = @Date, CategoryId = @CategoryId WHERE Id = @Id";

            SqlParameter[] parameters =
            {
        new SqlParameter("@Description", IncomeToUpdate.Description),
        new SqlParameter("@Amount", IncomeToUpdate.Amount),
        new SqlParameter("@Date", IncomeToUpdate.Date),
        new SqlParameter("@CategoryId", IncomeToUpdate.CategoryId),
        new SqlParameter("@Id", IncomeToUpdate.Id)
    };

            await DataAccessLayer.ExecuteNonQueryAsync(command);
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
                        Amount = reader.GetInt32(2),
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
                    Income Income = new Income
                    {
                        Id = reader.GetInt32(0),
                        Description = reader.GetString(1),
                        Amount = reader.GetInt32(2),
                        Date = reader.GetDateTime(3),
                        CategoryId = reader.GetInt32(4)
                    };

                    result.Add(Income);
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
