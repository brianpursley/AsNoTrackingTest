using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.EntityFrameworkCore;

namespace AsNoTrackingTest
{
    public class Program
    {
        
        [Benchmark]
        public void Tracking_OneRow()
        {
            using var db = new NorthwindDbContext();
            db.Customers.Take(1).ToList();
        }
        
        [Benchmark]
        public void NoTracking_OneRow()
        {
            using var db = new NorthwindDbContext();
            db.Customers.Take(1).AsNoTracking().ToList();
        }
        
        [Benchmark]
        public void Tracking_OneRow_Twice()
        {
            using var db = new NorthwindDbContext();
            db.Customers.Take(1).ToList();
            db.Customers.Take(1).ToList();
        }
        
        [Benchmark]
        public void NoTracking_OneRow_Twice()
        {
            using var db = new NorthwindDbContext();
            db.Customers.Take(1).AsNoTracking().ToList();
            db.Customers.Take(1).AsNoTracking().ToList();
        }
        
        [Benchmark]
        public void Tracking_AllRows()
        {
            using var db = new NorthwindDbContext();
            db.Customers.ToList();
        }
        
        [Benchmark]
        public void NoTracking_AllRows()
        {
            using var db = new NorthwindDbContext();
            db.Customers.AsNoTracking().ToList();
        }
        
        [Benchmark]
        public void Tracking_AllRows_Twice()
        {
            using var db = new NorthwindDbContext();
            db.Customers.ToList();
            db.Customers.ToList();
        }
        
        [Benchmark]
        public void NoTracking_AllRows_Twice()
        {
            using var db = new NorthwindDbContext();
            db.Customers.AsNoTracking().ToList();
            db.Customers.AsNoTracking().ToList();
        }

        [Benchmark]
        public void Tracking_Disjoint()
        {
            using var db = new NorthwindDbContext();
            db.Customers.Take(40).ToList();
            db.Customers.Skip(40).ToList();
        }
        
        [Benchmark]
        public void NoTracking_Disjoint()
        {
            using var db = new NorthwindDbContext();
            db.Customers.Take(40).AsNoTracking().ToList();
            db.Customers.Skip(40).AsNoTracking().ToList();
        }

        static void Main(string[] args)
            => BenchmarkRunner.Run<Program>();
    }

    class NorthwindDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            optionsBuilder.UseSqlite("Data Source=northwind.db");
        }
    }

    public class Customer
    {
        public string CustomerId { get; set; }
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