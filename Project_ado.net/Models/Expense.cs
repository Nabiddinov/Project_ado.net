using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ado.net.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }

        public Expense()
        {}

        public Expense(string description, decimal amount, DateTime date, int categoryId)
        {
            Description = description;
            Amount = amount;
            Date = date;
            CategoryId = categoryId;
        }

        public Expense(int id, string description, decimal amount, DateTime date, int categoryId)
        {
            Id = id;
            Description = description;
            Amount = amount;
            Date = date;
            CategoryId = categoryId;
        }

        public override string ToString() => $"Id: {Id}, Description: {Description} ,amount: {Amount},date: {Date},categoryId: {CategoryId} ";

    }
}