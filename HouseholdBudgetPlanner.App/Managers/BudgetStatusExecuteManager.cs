using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseholdBudgetPlanner.App.Managers
{
    public class BudgetStatusExecuteManager
    {
        private readonly BudgetStatusExpensesManager _budgetStatusExpensesManager;
        private readonly BudgetStatusIncomesManager _budgetStatusIncomesManager;
        private readonly BudgetStatusBalanceManager _budgetStatusBalanceManager;
        public BudgetStatusExecuteManager()
        {
            _budgetStatusExpensesManager = new BudgetStatusExpensesManager();
            _budgetStatusIncomesManager = new BudgetStatusIncomesManager();
            _budgetStatusBalanceManager = new BudgetStatusBalanceManager();
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
