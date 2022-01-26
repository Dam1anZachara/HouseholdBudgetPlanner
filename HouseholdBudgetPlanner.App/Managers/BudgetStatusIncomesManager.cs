using System;

namespace HouseholdBudgetPlanner.App.Managers
{
    public class BudgetStatusIncomesManager : AmountManagerBudgetStatus
    {
        private readonly BudgetStatusIncomesManagerMonth _budgetStatusIncomesManagerMonth;
        private readonly BudgetStatusIncomesManagerRange _budgetStatusIncomesManagerRange;
        public BudgetStatusIncomesManager(BudgetStatusIncomesManagerMonth budgetStatusIncomesManagerMonth, BudgetStatusIncomesManagerRange budgetStatusIncomesManagerRange)
        {
            _budgetStatusIncomesManagerMonth = budgetStatusIncomesManagerMonth;
            _budgetStatusIncomesManagerRange = budgetStatusIncomesManagerRange;
        }
        public BudgetStatusIncomesManager()
        {
        }
        internal ConsoleKeyInfo BudgetStatusIncomeTypeMenu()
        {
            Console.WriteLine($"\r\nSelect income type: \r\n");
            Console.WriteLine("1. All incomes");
            Console.WriteLine("2. Select by a name of an income type");
            var keyInfoStatusIncome = Console.ReadKey();
            return keyInfoStatusIncome;
        }
       
        internal void BudgetStatusIncomesMethod()
        {
            Console.WriteLine($"\r\nStatus of incomes. Select the time interval: \r\n");
            var keyInfoStatusIncomeMonthDate = BudgetStatusDateMenu();
            switch (keyInfoStatusIncomeMonthDate.KeyChar)
            {
                case '1':
                    _budgetStatusIncomesManagerMonth.BudgetStatusIncomesMethodMonth();
                    break;
                case '2':
                    _budgetStatusIncomesManagerRange.BudgetStatusIncomesMethodRange();
                    break;
                default:
                    Console.WriteLine("\r\nAction you entered does not exist\r\n");
                    break;
            }
        }
    }
}
