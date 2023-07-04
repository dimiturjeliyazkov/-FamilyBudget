using FamilyBudget.Entities;
using FamilyBudget.Models.Home;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace FamilyBudget.Repositories
{
    public class Db : DbContext
    {
        public DbSet<Transaction> transactions { get; set; }
        //Connection String
        private string sqlConnectionString = "Server=(localdb)\\MSSQLLocalDB;Database=Bulky;Trusted_Connection=True;";

        public Db()
        {
            this.transactions = this.Set<Transaction>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // sql connection string
            optionsBuilder
                .UseSqlServer(sqlConnectionString)
                .UseLazyLoadingProxies();

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            int id = 1;
            Random rn = new();
            List<Transaction> transactions = new List<Transaction>();

            //Adding random transactions in database
            for (int i = id; i <= 6; i++)
            {
                
                //incomes
                transactions.Add(new Transaction
                {
                    Id = id,
                    Category = Category.Salary.ToString(),
                    CreatedTime = DateTime.Now.AddMonths(-i),
                    ModifiedTime = DateTime.Now,
                    RepeatDay = 0,
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since",
                    Value = rn.Next(400, 1000) 

                });
                id++;

                //expenses

                transactions.Add(new Transaction
                {
                    Id = id,
                    Category = Category.General.ToString(),
                    CreatedTime = DateTime.Now.AddMonths(-i),
                    ModifiedTime = DateTime.Now,
                    RepeatDay = 0,
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since",
                    Value = rn.Next(100, 1000) * -1

                });
                id++;

                //MonthlyRepetitions
                transactions.Add(new Transaction
                {
                    Id = id,
                    Category = Category.MonthlyRepetition.ToString(),
                    CreatedTime = DateTime.Now.AddMonths(-i),
                    ModifiedTime = DateTime.Now,
                    RepeatDay = 0,
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since",
                    Value = -55

                });
                id++;
            }
            
            //repetition itself
            transactions.Add(new Transaction
            {
                Id = id,
                Category = Category.Health.ToString(),
                CreatedTime = DateTime.Now.AddMonths(4),
                ModifiedTime = DateTime.Now,
                RepeatDay = 14,
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since",
                Value = -55

            });


            modelBuilder.Entity<Transaction>().HasData(transactions);
        }
       

    }
    
}
