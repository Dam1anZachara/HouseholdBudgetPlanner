using HouseholdBudgetPlanner.App.Abstract;
using HouseholdBudgetPlanner.App.Concrete;
using HouseholdBudgetPlanner.Domain.Entity;
using HouseholdBudgetPlanner.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseholdBudgetPlanner.App.Managers
{
    public class AmountManagerBudgetStatus:AmountManager
    {
        //private readonly IService<Amount> _amount;
        //public List<Amount> amount;
        //private readonly List<Amount> Amounts;
        private readonly MenuActionService _actionService;
        public AmountManagerBudgetStatus(MenuActionService actionService)
        {
            //amount = new List<Amount>(_amount.GetAllItems());
            _actionService = actionService;
        }
        public AmountManagerBudgetStatus()
        {
        }
        public ConsoleKeyInfo BudgetStatusView()
        {
            var budgetStatusMenu = _actionService.GetMenuActionsByMenuName("BudgetStatusMenu");
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
            //var amountX = _amount.GetAllItems();
            decimal amountSumAllExpenses = 0;
            foreach (var amount in Items)
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
            foreach (var amount in Items)
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
