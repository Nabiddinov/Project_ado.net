﻿using System;

namespace Project_ado.net.Models
{
    public class Income
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }

        public Income()
        {

        }

        public Income(string description, decimal amount, DateTime date, int categoryId)
        {
            Description = description;
            Amount = amount;
            Date = date;
            CategoryId = categoryId;
        }

        public Income(int id, string description, decimal amount, DateTime date, int categoryId)
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