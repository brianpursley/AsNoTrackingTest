using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AsNoTrackingTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var useAsNoTracking = bool.Parse(args[0]);
            var iterations = int.Parse(args[1]);
            Console.WriteLine($"useAsNoTracking = {useAsNoTracking}, iterations = {iterations}");
            
            using var db = new NorthwindDbContext();
            for (var i = 0; i < iterations; i++)
            {
                if (useAsNoTracking)
                {
                    db.Customers.AsNoTracking().ToList();
                }
                else
                {
                    db.Customers.ToList();
                }
            }
        }
    }

    class NorthwindDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            optionsBuilder.UseSqlite("Data Source=./Northwind_small.sqlite");
        }
    }

    [Table("Customer")]
    class Customer
    {
        public string Id { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
    }
}