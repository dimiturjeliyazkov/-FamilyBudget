using FamilyBudget.Entities;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;

namespace FamilyBudget.Models.Home
{
    //transaction categories
    public enum Category
    {
        General,
        Bill,
        Food,
        Fun,
        Shopping,
        Health,
        Home,
        Investment,
        Maintenance,
        Salary,
        Accumulation,
        Transport,
        Travel,
        Scholarship,
        MonthlyRepetition
    }

    public enum TransactionType
    {
        Income,
        Expense
    }

    //Transaction list filters
    public enum Filter
    {
        All = 0,
        NormalTransactions = 1,
        RepetitiveTransactions = 2,
        Repetitions = 3
    }


    public class TransactionsVM
    {
        public List<TransactionModified>? Transactions { get; set; }
        public TabActiveOrNot? TabActiveOrNot { get; set; }
    }

    public class TransactionModified : Transaction
    {
        //added x daysago
        public int DaysAgo
        {
            get
            {
                return (int)(DateTime.Now - this.CreatedTime).TotalDays;
            }
        }
        //added x hoursago
        public int HoursAgo
        {
            get
            {
                return (int)(DateTime.Now - this.CreatedTime).TotalHours;
            }
        }

        //next repetition date of the repetitive transaction
        public DateTime NextRepetitionDate
        {
            get
            {
                DateTime now = DateTime.Now;
                if (this.RepeatDay < DateTime.Now.Day)
                {
                    DateTime afterOneMonth = now.AddMonths(1);
                    return new DateTime(afterOneMonth.Year, afterOneMonth.Month, this.RepeatDay,12,00,00);
                }

                else return new DateTime(now.Year, now.Month, this.RepeatDay, 12, 00, 00);
            }
        }

        public bool IsRepetitive
        {
            get
            {
                if (this.RepeatDay > 0) return true;
                else return false;
            }
        }

        public string GetTimeText { get {
                if(this.RepeatDay == 0)
                {
                    if(this.DaysAgo == 0)
                    {
                        if(this.HoursAgo == 0) return "Just now";
                        else return $"{this.HoursAgo} hours ago";

                    }else return $"{this.DaysAgo} days ago";
                        
                }else return $"Next repetition {this.NextRepetitionDate}";
        }}
    }

    //Defining which tab is active and which counter is visible 
    public class TabActiveOrNot
    {
        //stylings
        private string TabStyleClass = "active";
        private string CountStyleClass = "visible";

        //Tab class name
        public string? All { get; set; }
        public string? RepetitiveTransactions { get; set; }
        public string? NormalTransactions { get; set; }
        public string? Repetitions { get; set; }

        //Counter class name
        public string? AllCount { get; set; }
        public string? NormalTransactionsCount { get; set; }
        public string? RepetitiveTransactionsCount { get; set; }
        public string? RepetitionsCount { get; set; }
        
        public TabActiveOrNot(int filter)
        {
            this.All = "";
            this.NormalTransactions = "";
            this.RepetitiveTransactions = "";
            this.Repetitions = "";
            this.AllCount = "invisible";
            this.NormalTransactionsCount = "invisible";
            this.RepetitiveTransactionsCount = "invisible";
            this.RepetitionsCount = "invisible";

            
            if (filter == (int)Filter.NormalTransactions)
            {
                this.NormalTransactions = TabStyleClass;
                this.NormalTransactionsCount = CountStyleClass;
            }
            else if (filter == (int)Filter.RepetitiveTransactions)
            {
                this.RepetitiveTransactions = TabStyleClass;
                this.RepetitiveTransactionsCount = CountStyleClass;
            }
            else if(filter == (int)Filter.Repetitions)
            {
                this.Repetitions = TabStyleClass;
                this.RepetitionsCount = CountStyleClass;
            }
            else
            {
                this.All = TabStyleClass;
                this.AllCount = CountStyleClass;
            }

        }
        
    }
}
