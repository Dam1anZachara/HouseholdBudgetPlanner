using HouseholdBudgetPlanner.App.Abstract;
using HouseholdBudgetPlanner.Domain.Entity;
using HouseholdBudgetPlanner.Helpers;
using System;
using System.Collections.Generic;

namespace HouseholdBudgetPlanner.App.Managers
{
    public class BudgetStatusIncomesManagerMonth : BudgetStatusIncomesManager
    {
        private IService<Amount> _amountService;
        private List<Amount> _amountsGetList;
        private readonly AmountManagerBudgetStatus _amountManagerBudgetStatus;
        public BudgetStatusIncomesManagerMonth(IService<Amount> amountService, AmountManagerBudgetStatus amountManagerBudgetStatus)
        {
            _amountService = amountService;
            _amountsGetList = amountService.GetAllItems();
            _amountManagerBudgetStatus = amountManagerBudgetStatus;
        }
        
        public void BudgetStatusAllIncomesMonthList() //private
        {
            Console.WriteLine($"\r\nYour incomes this month:\r\n");
            foreach (var amount in _amountsGetList)
            {
                if (DateTime.Now.Month == amount.Date.Month && DateTime.Now.Year == amount.Date.Year && amount.Id < 0)
                {
                    Console.WriteLine($"{amount.Date}, Name: {amount.Name}, Value: {amount.Value}{ValueTypes.PLN}");
                }
            }
        }
        public bool MonthIncomeInAmountByNameExist(string name) //private
        {
            foreach (var amount in _amountsGetList)
            {
                if (DateTime.Now.Month == amount.Date.Month && DateTime.Now.Year == amount.Date.Year && amount.Id < 0 && amount.Name == name)
                {
                    return true;
                }
            }
            return false;
        }
        public void BudgetStatusIncomesMonthByName(bool monthIncomeInAmountByNameExist, string name) //private
        {
            if (monthIncomeInAmountByNameExist)
            {
                decimal amountSumNameIncomes = 0;
                foreach (var amount in _amountsGetList)
                {
                    if (DateTime.Now.Month == amount.Date.Month && DateTime.Now.Year == amount.Date.Year && amount.Id < 0 && amount.Name == name)
                    {
                        amountSumNameIncomes += amount.Value;
                    }
                }
                Console.WriteLine($"\r\nIncomes status this month with the name {name}: {amountSumNameIncomes}{ValueTypes.PLN}\r\n");
            }
            else
            {
                Console.WriteLine("\r\nIncome type with this name does not exist.\r\n");
            }
        }
        internal void BudgetStatusIncomesMethodMonth()
        {
            var keyInfoStatusIncomeMonthName = BudgetStatusIncomeTypeMenu();
            switch (keyInfoStatusIncomeMonthName.KeyChar)
            {
                case '1':
                    _amountManagerBudgetStatus.BudgetStatusAllIncomesMonth();
                    break;
                case '2':
                    BudgetStatusAllIncomesMonthList();
                    Console.Write("\r\nWrite the name of the selected income type to sort by a name and press \"Enter\": ");
                    string name = Console.ReadLine();
                    bool monthIncomeInAmountByNameExist = MonthIncomeInAmountByNameExist(name);
                    BudgetStatusIncomesMonthByName(monthIncomeInAmountByNameExist, name);
                    break;
                default:
                    Console.WriteLine("\r\nAction you entered does not exist\r\n");
                    break;
            }
        }
    }
}
