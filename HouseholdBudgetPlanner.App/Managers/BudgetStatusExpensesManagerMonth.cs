using HouseholdBudgetPlanner.App.Abstract;
using HouseholdBudgetPlanner.Domain.Entity;
using HouseholdBudgetPlanner.Helpers;
using System;
using System.Collections.Generic;

namespace HouseholdBudgetPlanner.App.Managers
{
    public class BudgetStatusExpensesManagerMonth : BudgetStatusExpensesManager
    {
        private IService<Amount> _amountService;
        private List<Amount> _amountsGetList;
        private readonly AmountManagerBudgetStatus _amountManagerBudgetStatus;
        public BudgetStatusExpensesManagerMonth(IService<Amount> amountService, AmountManagerBudgetStatus amountManagerBudgetStatus)
        {
            _amountService = amountService;
            _amountsGetList = amountService.GetAllItems();
            _amountManagerBudgetStatus = amountManagerBudgetStatus;
        }
        public void BudgetStatusAllExpensesMonthList() //private
        {
            Console.WriteLine($"\r\nYour expenses this month:\r\n");
            foreach (var amount in _amountsGetList)
            {
                if (DateTime.Now.Month == amount.Date.Month && DateTime.Now.Year == amount.Date.Year && amount.Id > 0)
                {
                    Console.WriteLine($"{amount.Date}, Name: {amount.Name}, Value: {amount.Value}{ValueTypes.PLN}");
                }
            }
        }
        public bool MonthExpenseInAmountByNameExist(string name) //private
        {
            foreach (var amount in _amountsGetList)
            {
                if (DateTime.Now.Month == amount.Date.Month && DateTime.Now.Year == amount.Date.Year && amount.Id > 0 && amount.Name == name)
                {
                    return true;
                }
            }
            return false;
        }
        public void BudgetStatusExpensesMonthByName(bool monthExpenseInAmountByNameExist, string name) //private
        {
            if (monthExpenseInAmountByNameExist)
            {
                decimal amountSumNameExpenses = 0;
                foreach (var amount in _amountsGetList)
                {
                    if (DateTime.Now.Month == amount.Date.Month && DateTime.Now.Year == amount.Date.Year && amount.Id > 0 && amount.Name == name)
                    {
                        amountSumNameExpenses += amount.Value;
                    }
                }
                Console.WriteLine($"\r\nExpenses status this month with the name {name}: {amountSumNameExpenses}{ValueTypes.PLN}\r\n");
            }
            else
            {
                Console.WriteLine("\r\nExpense type with this name does not exist.\r\n");
            }
        }

        internal void BudgetStatusExpensesMethodMonth()
        {
            var keyInfoStatusExpenseMonthName = BudgetStatusExpenseTypeMenu();
            switch (keyInfoStatusExpenseMonthName.KeyChar)
            {
                case '1':
                    _amountManagerBudgetStatus.BudgetStatusAllExpensesMonth();
                    break;
                case '2':
                    BudgetStatusAllExpensesMonthList();
                    Console.Write("\r\nWrite the name of the selected expense type to sort by a name and press \"Enter\": ");
                    string name = Console.ReadLine();
                    bool monthExpenseInAmountByNameExist = MonthExpenseInAmountByNameExist(name);
                    BudgetStatusExpensesMonthByName(monthExpenseInAmountByNameExist, name);
                    break;
                default:
                    Console.WriteLine("\r\nAction you entered does not exist\r\n");
                    break;
            }
        }
    }
}
