using Expense.Application.ModelViews;
using Expense.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Expense.Infrastructure
{
    public class DatabaseConnection :DbContext
    {

        public DatabaseConnection(DbContextOptions<DatabaseConnection> options)
       : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ExpenseData> ExpenseData {  get; set; }
        public DbSet<UserCredential> UserCredential {  get; set; }
        public DbSet<IncomeData> IncomeData {  get; set; }
        public DbSet<ActivityLog> ActivityLog {  get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Category>().HasKey(i => i.CategoryId);
            modelBuilder.Entity<User>().HasKey(i => i.UserId);
            modelBuilder.Entity<ExpenseData>().HasKey(i => i.ExpenseId);
            modelBuilder.Entity<UserCredential>().HasKey(i => i.Id);
            modelBuilder.Entity<IncomeData>().HasKey(i => i.IncomeId);
            modelBuilder.Entity<ActivityLog>().HasKey(i => i.Id);
            //modelBuilder.Entity<MenuSetUp>().HasNoKey();
            base.OnModelCreating(modelBuilder);
        }

    }
}
