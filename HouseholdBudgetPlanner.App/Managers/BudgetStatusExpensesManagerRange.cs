using HouseholdBudgetPlanner.App.Abstract;
using HouseholdBudgetPlanner.Domain.Entity;
using HouseholdBudgetPlanner.Helpers;
using System;
using System.Collections.Generic;

namespace HouseholdBudgetPlanner.App.Managers
{
    public class BudgetStatusExpensesManagerRange : BudgetStatusExpensesManager
    {
        private IService<Amount> _amountService;
        private List<Amount> _amountsGetList;
        private readonly AmountManagerBudgetStatus _amountManagerBudgetStatus;
        private readonly AmountManager _amountManager;
        public BudgetStatusExpensesManagerRange(IService<Amount> amountService, AmountManagerBudgetStatus amountManagerBudgetStatus, AmountManager amountManager)
        {
            _amountService = amountService;
            _amountsGetList = amountService.GetAllItems();
            _amountManagerBudgetStatus = amountManagerBudgetStatus;
            _amountManager = amountManager;
        }
        private void BudgetStatusExpensesRangeDateList(DateTime dateStartEntered, DateTime dateEndEntered)
        {
            Console.WriteLine($"\r\nYour expenses since {dateStartEntered} to {dateEndEntered}.\r\n");
            foreach (var amount in _amountsGetList)
            {
                if ((amount.Date > dateStartEntered) && (amount.Date < dateEndEntered) && (amount.Id > 0) && (dateStartEntered < dateEndEntered))
                {
                    Console.WriteLine($"{amount.Date}, Name: {amount.Name}, value: {amount.Value}{ValueTypes.PLN}");
                }
            }
        }
        private bool RangeExpenseInAmountByNameExist(DateTime dateStartEntered, DateTime dateEndEntered, string name)
        {
            foreach (var amount in _amountsGetList)
            {
                if ((amount.Date > dateStartEntered) && (amount.Date < dateEndEntered) && (amount.Id > 0) && (dateStartEntered < dateEndEntered) && (amount.Name == name))
                {
                    return true;
                }
            }
            return false;
        }
        private void BudgetStatusExpensesRangeDateByName(bool rangeExpenseInAmountByNameExist, DateTime dateStartEntered, DateTime dateEndEntered, string name)
        {
            if (rangeExpenseInAmountByNameExist)
            {
                decimal amountSumNameExpenses = 0;
                foreach (var amount in _amountsGetList)
                {
                    if ((amount.Date > dateStartEntered) && (amount.Date < dateEndEntered) && (amount.Id > 0) && (dateStartEntered < dateEndEntered) && (amount.Name == name))
                    {
                        amountSumNameExpenses += amount.Value;
                    }
                }
                Console.WriteLine($"\r\nExpenses status with the name {name} since {dateStartEntered} to {dateEndEntered}: {amountSumNameExpenses}{ValueTypes.PLN}\r\n");
            }
            else
            {
                Console.WriteLine("\r\nExpense type with this name does not exist.\r\n");
            }
        }
        internal void BudgetStatusExpensesMethodRange()
        {
            Console.Write("\r\nWrite a starting date in format \"dd/mm/yyyy\" and press \"Enter\": ");
            var dateStart = Console.ReadLine();
            DateTime dateStartEntered;
            dateStartEntered = DateSelect(dateStart);
            Console.Write("\r\nWrite an ending date in format \"dd/mm/yyyy\" and press \"Enter\": ");
            var dateEnd = Console.ReadLine();
            DateTime dateEndEntered;
            dateEndEntered = DateSelect(dateEnd);
            bool expenseInAmountByDateExist = _amountManager.ExpenseInAmountByDateExist(dateStartEntered, dateEndEntered);
            if (expenseInAmountByDateExist)
            {
                var keyInfoStatusExpenseRangeName = BudgetStatusExpenseTypeMenu();
                switch (keyInfoStatusExpenseRangeName.KeyChar)
                {
                    case '1':
                        _amountManagerBudgetStatus.BudgetStatusExpensesRangeDate(dateStartEntered, dateEndEntered);
                        break;
                    case '2':
                        BudgetStatusExpensesRangeDateList(dateStartEntered, dateEndEntered);
                        Console.Write("\r\nWrite the name of the selected expense type to sort by a name and press \"Enter\": ");
                        string name = Console.ReadLine();
                        bool rangeExpenseInAmountByNameExist = RangeExpenseInAmountByNameExist(dateStartEntered, dateEndEntered, name);
                        BudgetStatusExpensesRangeDateByName(rangeExpenseInAmountByNameExist, dateStartEntered, dateEndEntered, name);
                        break;
                    default:
                        Console.WriteLine("\r\nAction you entered does not exist\r\n");
                        break;
                }
            }
            else
            {
                Console.WriteLine($"\r\nExpenses in selected date range do not exist!");
            }
        }
    }
}
