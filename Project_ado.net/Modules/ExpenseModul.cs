using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_ado.net.DAL;
using Project_ado.net.Helpers;
using Project_ado.net.Models;

namespace Project_ado.net.Modules
{
    internal class ExpenseModul
    {
        public static async Task ShowOptionsExpenseAsync()
        {
            Console.WriteLine("1. See all Expenses     2. Find Expense by id     3. Add Expense");
            Console.WriteLine("4. Update Expense        5.Delete Expense        ");

            int input = ConsoleHelper.GetOptionInput();

            Console.Clear();

            switch (input)
            {
                case 1:
                    await GetAllExpensesAsync();
                    break;
                case 2:
                    await GetExpenseByIdAsync();
                    break;
                case 3:
                    await CreateExpenseModuleAsync();
                    break;
                case 4:
                    await UpdateExpenseModulAsync();
                    break;
                case 5:
                    await DaleteExpenseModulAsync();
                    break;
                default:
                    return;
            }
        }
        private static async Task GetAllExpensesAsync()
        {
            List<Expense> Expenses = await ExpenseService.GetAllExpensesAsync();

            foreach (var Expense in Expenses)
            {
                Console.WriteLine(Expense);
            }

            Console.Write("Enter any key to continue");
            Console.ReadKey();
        }
        private static async Task CreateExpenseModuleAsync()
        {
            Console.Write("Enter description: ");
            string description = Console.ReadLine();

            Console.Write("Amount: ");
            int amount = int.Parse(Console.ReadLine());

            Console.Write("Enter category_id: ");
            int categoryId = int.Parse(Console.ReadLine());

            await ExpenseService.CreateExpense(new Models.Expense(description, amount, DateTime.Today, categoryId));

            Console.Write("Enter any key to continue");
            Console.ReadKey();
        }
        private static async Task GetExpenseByIdAsync()
        {
            Console.Write("Enter id: ");

            int input = ConsoleHelper.GetOptionInput();

            Expense Expense = await ExpenseService.GetExpenseById(input);

            if (Expense is null)
            {
                ConsoleHelper.WriteLineError($"Expense with id: {input} does not exist.");
            }
            else
            {
                Console.WriteLine(Expense);
            }

            Console.Write("Enter any key to continue");
            Console.ReadKey();
        }
        private static async Task DaleteExpenseModulAsync()
        {
            Console.Write("Enter id: ");

            int input = ConsoleHelper.GetOptionInput();

            await ExpenseService.DeleteExpense(input);
        }
        private static async Task UpdateExpenseModulAsync()
        {
            Console.Write("Enter id: ");
            int input = ConsoleHelper.GetOptionInput();
            Console.Write("Enter description: ");
            string description = Console.ReadLine();

            Console.Write("Amount: ");
            int amount = int.Parse(Console.ReadLine());

            Console.Write("Enter category_id: ");
            int categoryId = int.Parse(Console.ReadLine());

            await ExpenseService.UpdateExpense(new Models.Expense(input, description, amount, DateTime.Today, categoryId));
        }
    }
}
