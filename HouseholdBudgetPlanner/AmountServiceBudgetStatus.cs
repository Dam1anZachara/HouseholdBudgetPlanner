using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseholdBudgetPlanner
{
    public class AmountServiceBudgetStatus:AmountService
    {
        public ConsoleKeyInfo BudgetStatusView(MenuActionService actionService)
        {
            var budgetStatusMenu = actionService.GetMenuActionsByMenuName("BudgetStatusMenu");
            Console.WriteLine("\r\n\r\nPlease select status to check:\r\n");
            for (int i = 0; i < budgetStatusMenu.Count; i++)
            {
                Console.WriteLine($"{budgetStatusMenu[i].Id}. {budgetStatusMenu[i].Name}");
            }
            var operation = Console.ReadKey();
            return operation;
        }
        protected ConsoleKeyInfo BudgetStatusDateMenu()
        {
            Console.WriteLine("1. This month");
            Console.WriteLine("2. Select range of date");
            var keyInfoStatusDate = Console.ReadKey();
            return keyInfoStatusDate;
        }
        protected decimal BudgetStatusAllExpensesMonth()
        {
            decimal amountSumAllExpenses = 0;
            foreach (var amount in Amounts)
            {
                if (DateTime.Now.Month == amount.Date.Month && DateTime.Now.Year == amount.Date.Year && amount.Id > 0)
                {
                    amountSumAllExpenses += amount.Value;
                }
            }
            Console.WriteLine($"\r\nExpenses status this month: {amountSumAllExpenses}{ValueTypes.PLN}");
            return amountSumAllExpenses;
        }
        protected decimal BudgetStatusAllIncomesMonth()
        {
            decimal amountSumAllIncomes = 0;
            foreach (var amount in Amounts)
            {
                if (DateTime.Now.Month == amount.Date.Month && DateTime.Now.Year == amount.Date.Year && amount.Id < 0)
                {
                    amountSumAllIncomes += amount.Value;
                }
            }
            Console.WriteLine($"\r\nIncomes status this month: {amountSumAllIncomes}{ValueTypes.PLN}");
            return amountSumAllIncomes;
        }
    }
}
