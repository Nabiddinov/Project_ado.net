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
    internal class IncomeModule
    {
        public static async Task ShowOptionsIncomeAsync()
        {
            Console.WriteLine("1. See all categories     2. Find Income by id     3. Add Income");
            Console.WriteLine("4. Update Income        5.Delete Income        ");

            int input = ConsoleHelper.GetOptionInput();

            Console.Clear();

            switch (input)
            {
                case 1:
                    await GetAllCategoriesAsync();
                    break;
                case 2:
                    await GetIncomeByIdAsync();
                    break;
                case 3:
                    await CreateIncomeModuleAsync();
                    break;
                case 4:
                    await UpdateIncomeModulAsync();
                    break;
                case 5:
                    await DaleteIncomeModulAsync();
                    break;
                default:
                    return;
            }
        }
        private static async Task GetAllCategoriesAsync()
        {
            List<Income> categories = await IncomeService.GetAllIncomesAsync();

            foreach (var Income in categories)
            {
                Console.WriteLine(Income);
            }

            Console.Write("Enter any key to continue");
            Console.ReadKey();
        }
        private static async Task CreateIncomeModuleAsync()
        {
            Console.Write("Enter description: ");
            string description = Console.ReadLine();

            Console.WriteLine("Amount: ");
            int amount = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter category_id: ");
            int categoryId = int.Parse(Console.ReadLine());

            await IncomeService.CreateIncome(new Models.Income(description, amount, DateTime.Today, categoryId));

            Console.Write("Enter any key to continue");
            Console.ReadKey();
        }
       

        private static async Task GetIncomeByIdAsync()
        {
            Console.Write("Enter id: ");

            int input = ConsoleHelper.GetOptionInput();

            Income Income = await IncomeService.GetIncomeById(input);

            if (Income is null)
            {
                ConsoleHelper.WriteLineError($"Income with id: {input} does not exist.");
            }
            else
            {
                Console.WriteLine(Income);
            }

            Console.Write("Enter any key to continue");
            Console.ReadKey();
        }
        private static async Task DaleteIncomeModulAsync()
        {
            Console.Write("Enter id: ");

            int input = ConsoleHelper.GetOptionInput();

            await IncomeService.DeleteIncome(input);
        }
        private static async Task UpdateIncomeModulAsync()
        {
            Console.Write("Enter id: ");
            int input = ConsoleHelper.GetOptionInput();
            Console.Write("Enter description: ");
            string description = Console.ReadLine();

            Console.WriteLine("Amount: ");
            int amount = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter category_id: ");
            int categoryId = int.Parse(Console.ReadLine());

            await IncomeService.UpdateIncome(new Models.Income(input,description,amount, DateTime.Today, categoryId));
        }

    }
}
