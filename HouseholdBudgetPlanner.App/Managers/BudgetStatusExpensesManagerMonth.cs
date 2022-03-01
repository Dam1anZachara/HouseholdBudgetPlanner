using HouseholdBudgetPlanner.App.Abstract;
using HouseholdBudgetPlanner.Domain.Entity;
using HouseholdBudgetPlanner.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

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
            var expenseListString = _amountsGetList.AsQueryable()
                .Where(amount => DateTime.Now.Month == amount.Date.Month && DateTime.Now.Year == amount.Date.Year && amount.Id > 0)
                .Select(amount => $"{amount.Date}, Name: {amount.Name}, Value: {amount.Value}{ValueTypes.PLN}");
            foreach (var expenseString in expenseListString)
            {
                Console.WriteLine(expenseString);
            }
        }
        public bool MonthExpenseInAmountByNameExist(string name) //private
        {
            return _amountsGetList.AsQueryable()
                .Where(amount => DateTime.Now.Month == amount.Date.Month && DateTime.Now.Year == amount.Date.Year && 
                amount.Id > 0 && amount.Name == name).Any();
        }
        public void BudgetStatusExpensesMonthByName(bool monthExpenseInAmountByNameExist, string name) //private
        {
            if (monthExpenseInAmountByNameExist)
            {
                decimal amountSumNameExpenses = _amountsGetList.AsQueryable()
                    .Where(amount => DateTime.Now.Month == amount.Date.Month && DateTime.Now.Year == amount.Date.Year && amount.Id > 0 && amount.Name == name)
                    .Sum(amountSumNameExpenses => amountSumNameExpenses.Value);
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
