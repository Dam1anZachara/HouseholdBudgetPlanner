using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseholdBudgetPlanner
{
    public class BudgetStatusExecute
    {
        BudgetStatusExpenses budgetStatusExpenses;
        BudgetStatusIncomes budgetStatusIncomes;
        BudgetStatusBalance budgetStatusBalance;
        public BudgetStatusExecute()
        {
            budgetStatusExpenses = new BudgetStatusExpenses();
            budgetStatusIncomes = new BudgetStatusIncomes();
            budgetStatusBalance = new BudgetStatusBalance();
        }
        public void BudgetStatus(ConsoleKeyInfo keyInfoBudgetStatus)
        {
            switch (keyInfoBudgetStatus.KeyChar)
            {
                case '1':
                    budgetStatusExpenses.BudgetStatusExpensesMethod();
                    break;
                case '2':
                    budgetStatusIncomes.BudgetStatusIncomesMethod();
                    break;
                case '3':
                    budgetStatusBalance.BudgetStatusBalanceMethod();
                    break;
                default:
                    Console.WriteLine("\r\nAction you entered does not exist\r\n");
                    break;
            }
        }
    }
}
