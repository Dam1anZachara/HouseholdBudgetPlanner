using System;

namespace HouseholdBudgetPlanner.App.Managers
{
    public class BudgetStatusExecuteManager
    {
        private readonly BudgetStatusExpensesManager _budgetStatusExpensesManager;
        private readonly BudgetStatusIncomesManager _budgetStatusIncomesManager;
        private readonly BudgetStatusBalanceManager _budgetStatusBalanceManager;
        public BudgetStatusExecuteManager(BudgetStatusExpensesManager budgetStatusExpensesManager, BudgetStatusIncomesManager budgetStatusIncomesManager, BudgetStatusBalanceManager budgetStatusBalanceManager)
        {
            _budgetStatusExpensesManager = budgetStatusExpensesManager;
            _budgetStatusIncomesManager = budgetStatusIncomesManager;
            _budgetStatusBalanceManager = budgetStatusBalanceManager;
        }
        public void BudgetStatus(ConsoleKeyInfo keyInfoBudgetStatus)
        {
            switch (keyInfoBudgetStatus.KeyChar)
            {
                case '1':
                    _budgetStatusExpensesManager.BudgetStatusExpensesMethod();
                    break;
                case '2':
                    _budgetStatusIncomesManager.BudgetStatusIncomesMethod();
                    break;
                case '3':
                    _budgetStatusBalanceManager.BudgetStatusBalanceMethod();
                    break;
                default:
                    Console.WriteLine("\r\nAction you entered does not exist\r\n");
                    break;
            }
        }
    }
}
