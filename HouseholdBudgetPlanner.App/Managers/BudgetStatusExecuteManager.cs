using System;

namespace HouseholdBudgetPlanner.App.Managers
{
    public class BudgetStatusExecuteManager
    {
        private readonly BudgetStatusExpensesManager _budgetStatusExpensesManager;
        private readonly BudgetStatusIncomesManager _budgetStatusIncomesManager;
        private readonly BudgetStatusBalanceManager _budgetStatusBalanceManager;
        private readonly BudgetStatusRaportManager _budgetStatusRaportManager;
        public BudgetStatusExecuteManager(BudgetStatusExpensesManager budgetStatusExpensesManager, BudgetStatusIncomesManager budgetStatusIncomesManager, BudgetStatusBalanceManager budgetStatusBalanceManager, BudgetStatusRaportManager budgetStatusRaportManager)
        {
            _budgetStatusExpensesManager = budgetStatusExpensesManager;
            _budgetStatusIncomesManager = budgetStatusIncomesManager;
            _budgetStatusBalanceManager = budgetStatusBalanceManager;
            _budgetStatusRaportManager = budgetStatusRaportManager;
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
                case '4':
                    _budgetStatusRaportManager.BudgetStatusRaportMethod();
                    break;
                default:
                    Console.WriteLine("\r\nAction you entered does not exist\r\n");
                    break;
            }
        }
    }
}
