using System;

namespace HouseholdBudgetPlanner.App.FileSupport
{
    public class FilePath
    {
        private string filePathAmounts = @"C:\Users\Damian\Desktop\Amounts.xml";
        private string filePathExpenseTypes = @"C:\Users\Damian\Desktop\ExpenseTypes.xml";
        private string filePathIncomeTypes = @"C:\Users\Damian\Desktop\IncomeTypes.xml";
        private string filePathRaport = $@"C:\Users\Damian\Desktop\{DateTime.Now.ToString("yyyy-MM-dd HHmm")} BudgetBalanceRaport.csv";

        public string FilePathAmounts()
        {
            return filePathAmounts;
        }
        public string FilePathExpenseTypes()
        {
            return filePathExpenseTypes;
        }
        public string FilePathIncomeTypes()
        {
            return filePathIncomeTypes;
        }
        public string FilePathRaport()
        {
            return filePathRaport;
        }
    }
}
