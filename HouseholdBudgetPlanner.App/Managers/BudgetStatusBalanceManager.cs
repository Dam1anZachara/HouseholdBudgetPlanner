using HouseholdBudgetPlanner.Helpers;
using System;

namespace HouseholdBudgetPlanner.App.Managers
{
    public class BudgetStatusBalanceManager : AmountManagerBudgetStatus
    {
        private readonly AmountManagerBudgetStatus _amountManagerBudgetStatus;
        public BudgetStatusBalanceManager(AmountManagerBudgetStatus amountManagerBudgetStatus)
        {
            _amountManagerBudgetStatus = amountManagerBudgetStatus;
        }
        internal void BudgetStatusBalanceMethod()
        {
            Console.WriteLine($"\r\nBudget balance. Select time interval: \r\n");
            var keyInfoStatusBalanceMonthDate = BudgetStatusDateMenu();
            switch (keyInfoStatusBalanceMonthDate.KeyChar)
            {
                case '1':
                    decimal amountSumAllExpenses = _amountManagerBudgetStatus.BudgetStatusAllExpensesMonth();
                    decimal amountSumAllIncomes = _amountManagerBudgetStatus.BudgetStatusAllIncomesMonth();
                    Console.WriteLine($"\r\nBudget balance this month: {amountSumAllIncomes - amountSumAllExpenses}{ValueTypes.PLN}\r\n");
                    break;
                case '2':
                    Console.Write("\r\nWrite a starting date in format \"dd/mm/yyyy\" and press \"Enter\": ");
                    var dateStart = Console.ReadLine();
                    DateTime dateStartEntered;
                    dateStartEntered = DateSelect(dateStart);
                    Console.Write("\r\nWrite an ending date in format \"dd/mm/yyyy\" and press \"Enter\": ");
                    var dateEnd = Console.ReadLine();
                    DateTime dateEndEntered;
                    dateEndEntered = DateSelect(dateEnd);
                    var amountSumAllExpensesDate = _amountManagerBudgetStatus.BudgetStatusExpensesRangeDate(dateStartEntered, dateEndEntered);
                    var amountSumAllIncomesDate = _amountManagerBudgetStatus.BudgetStatusIncomesRangeDate(dateStartEntered, dateEndEntered);
                    Console.WriteLine($"\r\nBudget balance since {dateStartEntered} to {dateEndEntered}: {amountSumAllIncomesDate - amountSumAllExpensesDate}{ValueTypes.PLN}\r\n");
                    break;
                default:
                    Console.WriteLine("\r\nAction you entered does not exist\r\n");
                    break;
            }
        }
    }
}
