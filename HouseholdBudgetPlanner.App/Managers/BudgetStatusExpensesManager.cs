using System;

namespace HouseholdBudgetPlanner.App.Managers
{
    public class BudgetStatusExpensesManager : AmountManagerBudgetStatus
    {
        private readonly BudgetStatusExpensesManagerMonth _budgetStatusExpensesManagerMonth;
        private readonly BudgetStatusExpensesManagerRange _budgetStatusExpensesManagerRange;
        public BudgetStatusExpensesManager(BudgetStatusExpensesManagerMonth budgetStatusExpensesManagerMonth, BudgetStatusExpensesManagerRange budgetStatusExpensesManagerRange)
        {
            _budgetStatusExpensesManagerMonth = budgetStatusExpensesManagerMonth;
            _budgetStatusExpensesManagerRange = budgetStatusExpensesManagerRange;
        }
        public BudgetStatusExpensesManager()
        {
        }

        internal ConsoleKeyInfo BudgetStatusExpenseTypeMenu()
        {
            Console.WriteLine($"\r\nSelect expense type: \r\n");
            Console.WriteLine("1. All expenses");
            Console.WriteLine("2. Select by a name of expenses");
            var keyInfoStatusExpense = Console.ReadKey();
            return keyInfoStatusExpense;
        }

        internal void BudgetStatusExpensesMethod()
        {
            Console.WriteLine($"\r\nStatus of expenses. Select the time interval: \r\n");
            var keyInfoStatusExpenseMonthDate = BudgetStatusDateMenu();
            switch (keyInfoStatusExpenseMonthDate.KeyChar)
            {
                case '1':
                    _budgetStatusExpensesManagerMonth.BudgetStatusExpensesMethodMonth();
                    break;
                case '2':
                    _budgetStatusExpensesManagerRange.BudgetStatusExpensesMethodRange();
                    break;
                default:
                    Console.WriteLine("\r\nAction you entered does not exist\r\n");
                    break;
            }
        }
    }
}
