﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project_ado.net.DAL;
using Project_ado.net.Helpers;
using Project_ado.net.Models;

namespace Project_ado.net.Modules
{
    public static class CategoryModule
    {
        public static async Task ShowOptionsCategoryAsync()
        {
            Console.WriteLine("1. See all categories     2. Find category by id     3. Add Category");
            Console.WriteLine("4. Update category        5.Delete Category        ");

            int input = ConsoleHelper.GetOptionInput();

            Console.Clear();

            switch (input)
            {
                case 1:
                    await GetAllCategoriesAsync();
                    break;
                case 2:
                    await GetCategoryByIdAsync();
                    break;
                case 3:
                    await CreateCategoryModuleAsync();
                    break;
                case 4:
                    await UpdateCategoryModulAsync();
                    break;
                case 5:
                    await DaleteCategoryModulAsync();
                    break;
                default:
                    return;
            }
        }

        public static async Task GetAllCategoriesAsync()
        {
            List<Category> categories = await CategoryService.GetAllCategoriesAsync();

            foreach (var category in categories)
            {
                Console.WriteLine(category);
            }

            Console.Write("Enter any key to continue");
            Console.ReadKey();
        }

        private static async Task CreateCategoryModuleAsync()
        {
            Console.WriteLine("Please, enter category values below.");
            Console.WriteLine();

            string categoryName = null;
            do
            {
                Console.Write("Enter new category name: ");
                categoryName = Console.ReadLine();
            }
            while (categoryName == null);

            await CategoryService.CreateCategory(new Models.Category(categoryName));

            Console.Write("Enter any key to continue");
            Console.ReadKey();
        }

        private static async Task GetCategoryByIdAsync()
        {
            Console.Write("Enter id: ");

            int input = ConsoleHelper.GetOptionInput();

            Category category = await CategoryService.GetCategoryById(input);

            if (category is null)
            {
                ConsoleHelper.WriteLineError($"Category with id: {input} does not exist.");
            }
            else
            {
                Console.WriteLine(category);
            }

            Console.Write("Enter any key to continue");
            Console.ReadKey();
        }

        private static async Task DaleteCategoryModulAsync()
        {
            Console.Write("Enter id: ");

            int input = ConsoleHelper.GetOptionInput();

            await CategoryService.DeleteCategory(input);
        }

        private static async Task UpdateCategoryModulAsync()
        {
            Console.Write("Enter id: ");
            int input = ConsoleHelper.GetOptionInput();
            Console.Write("Enter name: ");
            string str = Console.ReadLine();

            await CategoryService.UpdateCategory(new Models.Category(input, str));
        }

    }
}