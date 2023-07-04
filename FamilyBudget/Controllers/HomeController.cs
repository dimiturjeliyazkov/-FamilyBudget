using FamilyBudget.Entities;
using FamilyBudget.Models;
using FamilyBudget.Models.Home;
using FamilyBudget.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32.TaskScheduler;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;

namespace FamilyBudget.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            Db context = new Db();
            List<Transaction> allTransactions = context.transactions.ToList();
            
            decimal totalIncome = allTransactions.Where(t => t.Value > 0 && t.RepeatDay == 0).ToList().Sum(t => t.Value);
            decimal totalExpense = allTransactions.Where(t => t.Value < 0 && t.RepeatDay == 0).ToList().Sum(t => t.Value);
            
            int lastMonth = DateTime.Now.Month;
            int year = DateTime.Now.Year;

            //gets months -> January with index 1, February with index 2 etc.
            List<MonthsOfTheYear> months = Enum.GetValues(typeof(MonthsOfTheYear)).Cast<MonthsOfTheYear>().ToList();
            
            // List of 6 Months for diagram with month name, month income and month expense
            List<MonthTransactions> monthTransactions = new();
            for (int i = 0; i < 6; i++)
            {
                if (lastMonth == 1)
                {
                    lastMonth = 12;
                    year--;
                }
                else lastMonth--;
                int monthIndex = lastMonth;

                if (monthIndex == 0) monthIndex = 11;
                else monthIndex--;

                List<Transaction> transactionsByMonth = allTransactions.Where(tr => tr.CreatedTime.Month == lastMonth && tr.CreatedTime.Year == year).ToList();

                monthTransactions.Add(
                    //month name
                    new MonthTransactions(months[monthIndex].ToString(),
                    //month income
                    transactionsByMonth.Where(t => t.Value < 0).Sum(t => t.Value),
                    //month income
                    transactionsByMonth.Where(t => t.Value > 0).Sum(t => t.Value)));
            }

            monthTransactions.Reverse();
            HomeVM model = new HomeVM()
            {
                TotalBalance = totalIncome + totalExpense,
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                MonthTransactions = monthTransactions
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult TransactionList(int filter)
        {
            Db context = new Db();
            List<Transaction> transactions = context.transactions.ToList();

            //filtering the transactions
            TransactionsVM modelList = new TransactionsVM();
            modelList.Transactions = new List<TransactionModified>();
            if (filter == (int)Filter.NormalTransactions)
            {
                modelList.TabActiveOrNot = new TabActiveOrNot((int)Filter.NormalTransactions);
                transactions = transactions.Where(tr => tr.RepeatDay == 0 && tr.Category != Category.MonthlyRepetition.ToString()).ToList();
            }
            else if (filter == (int)Filter.RepetitiveTransactions)
            {
                modelList.TabActiveOrNot = new TabActiveOrNot((int)Filter.RepetitiveTransactions);
                transactions = transactions.Where(tr => tr.RepeatDay == 0 && tr.Category == Category.MonthlyRepetition.ToString()).ToList();

            }
            else if (filter == (int)Filter.Repetitions)
            {
                modelList.TabActiveOrNot = new TabActiveOrNot((int)Filter.Repetitions);
                transactions = transactions.Where(tr => tr.RepeatDay > 0).ToList();
                
            }
            else
            {
                modelList.TabActiveOrNot = new TabActiveOrNot((int)Filter.All);
                transactions = transactions.Where(tr => tr.RepeatDay == 0).ToList();
            }

            foreach (Transaction transaction in transactions)
            {
                modelList.Transactions.Add(new TransactionModified
                {
                    CreatedTime = transaction.CreatedTime,
                    ModifiedTime = transaction.ModifiedTime,
                    Description = transaction.Description,
                    Category = transaction.Category,
                    Value = transaction.Value,
                    Id = transaction.Id,
                    RepeatDay = transaction.RepeatDay
                });
            }

            //Sorting by created time (descending)
            modelList.Transactions = modelList.Transactions.OrderByDescending(t => t.CreatedTime).ToList();
            return View(modelList);
        }

        
        public IActionResult Delete(int id)
        {
            Db context = new Db();
            //Finding and deleting the specific transaction
            Transaction? transaction = context.transactions.Where(t => t.Id == id).FirstOrDefault();
            if (transaction != null)
            {
                context.transactions.Remove(transaction);
                context.SaveChanges();
            }
            return RedirectToAction("TransactionList", "Home");
        }

        [HttpGet]
        public IActionResult Manage(int id, bool isCreateRepetitive)
        {

            //Id == 0 means creating
            if (id == 0)
            {
                return View(new ManageVM
                {
                    IsCreate = true,
                    IsRepetitive = isCreateRepetitive
                });
            }
            //Else its editing
            Db context = new();
            Transaction ?transaction = context.transactions.Where(t => t.Id == id).FirstOrDefault();

            if (transaction != null)
            {
                string type = "";
                if (transaction.Value >= 0) type = TransactionType.Income.ToString();
                else
                {
                    type = TransactionType.Expense.ToString();
                    transaction.Value *= -1;
                }

                ManageVM model = new ManageVM()
                {
                    Id = id,
                    Category = transaction.Category,
                    Value = transaction.Value,
                    Description = transaction.Description,
                    Type = type,
                    CreatedTime = transaction.CreatedTime,
                    RepeatDay = transaction.RepeatDay,
                   
                };
                return View(model);
            }

            return View();
        }

        [HttpPost]
        public IActionResult Manage(ManageVM model)
        {
            //I couldn't solve the validation problem here RepeatDay property always has error - "This field is required" even though i didn't add required in the model
            if (!ModelState.IsValid && !(ModelState.ErrorCount == 1 && model.RepeatDay == 0) && model.IsCreate)
            {
                return View(model);
            }
          
            if (!ModelState.IsValid && !model.IsCreate)
                return View(model);
            //make the number negative if the type is expense
            if (model.Type == TransactionType.Expense.ToString())
            {
                model.Value *= -1;
            }
            
            Transaction tr = new Transaction()
            {
                Id = model.Id,
                Category = model.Category,
                Value = model.Value,
                Description = model.Description,
                ModifiedTime = DateTime.Now,
                RepeatDay= model.RepeatDay
            };
            if (model.IsCreate) tr.CreatedTime = DateTime.Now;
            Db context = new Db();
            //edit
            if(!model.IsCreate) context.transactions.Update(tr);
            //add
            if(model.IsCreate) context.transactions.Add(tr);

            context.SaveChanges();
            return RedirectToAction("TransactionList", "Home");
        }

        //Called from task
        public void CheckDailyForRepetitions()
        {
            //This method is called every day -> Task.cs
            Db context = new();
            List<Transaction> repetitions  = new();
            //check if transactions repeatday is today and add new transaction if it is
            repetitions = context.transactions.Where(t => t.RepeatDay == DateTime.Now.Day).ToList();
            
            if(repetitions.Count > 0)
            {
                List<Transaction> newTransactions = new();
                foreach (var transaction in repetitions)
                {
                    Transaction newTransaction = new Transaction()
                    {
                        Category = Category.MonthlyRepetition.ToString(),
                        Value = transaction.Value,
                        CreatedTime = DateTime.Now,
                        ModifiedTime = DateTime.Now,
                        Description = "Montly Transaction (" + transaction.Category + "): " + transaction.Description,
                        RepeatDay = 0
                    };
                    newTransactions.Add(newTransaction);
                }
                context.UpdateRange(newTransactions);
                context.SaveChanges();
            }
        }

    }
} 