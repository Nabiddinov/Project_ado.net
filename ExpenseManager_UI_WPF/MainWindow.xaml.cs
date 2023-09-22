using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Project_ado.net.DAL;
using Project_ado.net.Models;

namespace ExpenseManager_UI_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnIncomes_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void CategorySeeAll_Click(object sender, RoutedEventArgs e)
        {
            // Implement logic to see all categories here
            // For example: await GetAllCategoriesAsync();

            try
            {
                // Call the method to retrieve all categories
                List<Category> categories = await CategoryService.GetAllCategoriesAsync();

                // Handle the categories, for example, display them in a ListBox or DataGrid
                if (categories != null && categories.Count > 0)
                {
                    // Assuming you have a ListBox named "categoryListBox" to display categories
                    categoryListBox.ItemsSource = categories;
                }
                else
                {
                    // Handle the case when there are no categories to display
                    MessageBox.Show("No categories found.");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during the operation
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        // Event handler for "Find category by ID" button
        private async void CategoryFindById_Click(object sender, RoutedEventArgs e)
        {
            var inputDialog = new InputDialog();
            var result = await ShowDialogAsync(inputDialog);

            if (result == true)
            {
                string userInput = inputDialog.InputValue;
                if (!string.IsNullOrEmpty(userInput) && int.TryParse(userInput, out int categoryId))
                {
                    try
                    {
                        // Call the method to get a category by its ID
                        Category foundCategory = await CategoryService.GetCategoryById(categoryId);

                        if (foundCategory != null)
                        {
                            // Display the found category information, e.g., in a MessageBox
                            MessageBox.Show($"Category Name: {foundCategory.Name}", "Category Found");
                        }
                        else
                        {
                            MessageBox.Show("Category not found.", "Category Not Found");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions that may occur during the operation
                        MessageBox.Show($"Error: {ex.Message}", "Error");
                    }
                }
            }
        }

        private async Task<bool> ShowDialogAsync(UserControl dialog)
        {
            var parentWindow = new Window
            {
                Content = dialog,
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = this
            };

            bool result = false;

            if (dialog is CustomInputDialog customDialog)
            {
                customDialog.OKButton.Click += (sender, e) =>
                {
                    result = true;
                    parentWindow.DialogResult = true;
                    parentWindow.Close();
                };

                customDialog.CancelButton.Click += (sender, e) =>
                {
                    result = false;
                    parentWindow.DialogResult = false;
                    parentWindow.Close();
                };
            }

            parentWindow.ShowDialog();

            return result;
        }


        private async void CategoryAdd_Click(object sender, RoutedEventArgs e)
        {
            // Create a custom input dialog with a title
            var inputDialog = new CustomInputDialog("Enter Category Name");

            // Show the dialog and wait for user input
            var result = await ShowDialogAsync(inputDialog);

            if (result == true)
            {
                string categoryName = inputDialog.InputValue;
                if (!string.IsNullOrEmpty(categoryName))
                {
                    try
                    {
                        // Create a new Category object with the provided name
                        Category newCategory = new Category { Name = categoryName };

                        // Call the method to add the new category
                        await CategoryService.CreateCategory(newCategory);

                        MessageBox.Show("Category added successfully.", "Category Added");
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions that may occur during the operation
                        MessageBox.Show($"Error: {ex.Message}", "Error");
                    }
                }
            }
        }


        // Event handler for "Update Category" button
        private async void CategoryUpdate_Click(object sender, RoutedEventArgs e)
        {
            // Prompt the user for the category ID to update
            /* string input = Microsoft.VisualBasic.Interaction.InputBox("Enter Category ID to Update:", "Update Category", "");

             if (!string.IsNullOrEmpty(input) && int.TryParse(input, out int categoryId))
             {
                 // Prompt the user for the new category name
                 string newCategoryName = Microsoft.VisualBasic.Interaction.InputBox("Enter New Category Name:", "Update Category", "");

                 if (!string.IsNullOrEmpty(newCategoryName))
                 {
                     try
                     {
                         // Create a Category object with the updated name and ID
                         Category updatedCategory = new Category { Id = categoryId, Name = newCategoryName };

                         // Call the method to update the category
                         await CategoryService.UpdateCategory(updatedCategory);

                         MessageBox.Show("Category updated successfully.", "Category Updated");
                     }
                     catch (Exception ex)
                     {
                         // Handle any exceptions that may occur during the operation
                         MessageBox.Show($"Error: {ex.Message}", "Error");
                     }
                 }
             }*/
        }


        // Event handler for "Delete Category" button
        private async void CategoryDelete_Click(object sender, RoutedEventArgs e)
        {
            // Implement logic to delete a category here
            // For example: await DaleteCategoryModulAsync();
        }

        // Implement similar event handlers for other buttons in other modules (Expense, Income) as needed.

        private async void ExpenseSeeAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Call the method to retrieve all expenses asynchronously
                List<Expense> expenses = await GetAllExpensesAsync();

                if (expenses != null && expenses.Count > 0)
                {
                    // Display the expenses, e.g., in a ListBox or another UI control
                    // Replace "expenseListBox" with the name of your UI control for displaying expenses
                    expenseListBox.ItemsSource = expenses;
                }
                else
                {
                    MessageBox.Show("No expenses found.", "No Expenses");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during the operation
                MessageBox.Show($"Error: {ex.Message}", "Error");
            }
        }

        private async Task<List<Expense>> GetAllExpensesAsync()
        {
            // Implement the logic to retrieve all expenses from your data source
            // For example, you can call a service method to fetch the data

            // Replace the following line with your actual code to retrieve expenses
            List<Expense> expenses = await ExpenseService.GetAllExpensesAsync();

            return expenses;
        }


        // Event handler for "Find Expense by ID" button
        private async void ExpenseFindById_Click(object sender, RoutedEventArgs e)
        {
            // Implement logic to find an expense by ID here
            // For example: await GetExpenseByIdAsync();
        }

        // Event handler for "Add Expense" button
        private async void ExpenseAdd_Click(object sender, RoutedEventArgs e)
        {
            // Implement logic to add an expense here
            // For example: await CreateExpenseModuleAsync();
        }

        // Event handler for "Update Expense" button
        private async void ExpenseUpdate_Click(object sender, RoutedEventArgs e)
        {
            // Implement logic to update an expense here
            // For example: await UpdateExpenseModulAsync();
        }

        // Event handler for "Delete Expense" button
        private async void ExpenseDelete_Click(object sender, RoutedEventArgs e)
        {
            // Implement logic to delete an expense here
            // For example: await DaleteExpenseModulAsync();
        }

        // Event handler for "See all Incomes" button
        private async void IncomeSeeAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Call the method to retrieve all incomes asynchronously
                List<Income> incomes = await GetAllIncomesAsync();

                if (incomes != null && incomes.Count > 0)
                {
                    // Display the incomes, e.g., in a ListBox or another UI control
                    // Replace "incomeListBox" with the name of your UI control for displaying incomes
                    incomeListBox.ItemsSource = incomes;
                }
                else
                {
                    MessageBox.Show("No incomes found.", "No Incomes");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during the operation
                MessageBox.Show($"Error: {ex.Message}", "Error");
            }
        }

        private async Task<List<Income>> GetAllIncomesAsync()
        {
            // Implement the logic to retrieve all incomes from your data source
            // For example, you can call a service method to fetch the data

            // Replace the following line with your actual code to retrieve incomes
            List<Income> incomes = await IncomeService.GetAllIncomesAsync();

            return incomes;
        }


        // Event handler for "Find Income by ID" button
        private async void IncomeFindById_Click(object sender, RoutedEventArgs e)
        {
            // Implement logic to find an income by ID here
            // For example: await GetIncomeByIdAsync();
        }

        // Event handler for "Add Income" button
        private async void IncomeAdd_Click(object sender, RoutedEventArgs e)
        {
            // Implement logic to add an income here
            // For example: await CreateIncomeModuleAsync();
        }

        // Event handler for "Update Income" button
        private async void IncomeUpdate_Click(object sender, RoutedEventArgs e)
        {
            // Implement logic to update an income here
            // For example: await UpdateIncomeModulAsync();
        }

        // Event handler for "Delete Income" button
        private async void IncomeDelete_Click(object sender, RoutedEventArgs e)
        {
            // Implement logic to delete an income here
            // For example: await DaleteIncomeModulAsync();
        }



    }
}
