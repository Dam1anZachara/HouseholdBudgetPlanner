using HouseholdBudgetPlanner.App.Abstract;
using HouseholdBudgetPlanner.App.Concrete;
using HouseholdBudgetPlanner.Domain.Entity;
using HouseholdBudgetPlanner.Helpers;
using System;
using System.Collections.Generic;

namespace HouseholdBudgetPlanner.App.Managers
{
    public class AmountManagerBudgetStatus : AmountManager
    {
        private readonly MenuActionService _actionService;
        private IService<Amount> _amountService;
        private List<Amount> _amountsGetList;
        public AmountManagerBudgetStatus(MenuActionService actionService, IService<Amount> amountService)
        {
            _actionService = actionService;
            _amountService = amountService;
            _amountsGetList = amountService.GetAllItems();
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
        public decimal BudgetStatusAllExpensesMonth()
        {
            decimal amountSumAllExpenses = 0;
            foreach (var amount in _amountsGetList)
            {
                if (DateTime.Now.Month == amount.Date.Month && DateTime.Now.Year == amount.Date.Year && amount.Id > 0)
                {
                    amountSumAllExpenses += amount.Value;
                }
            }
            Console.WriteLine($"\r\nExpenses status this month: {amountSumAllExpenses}{ValueTypes.PLN}");
            return amountSumAllExpenses;
        }
        public decimal BudgetStatusAllIncomesMonth()
        {
            decimal amountSumAllIncomes = 0;
            foreach (var amount in _amountsGetList)
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
