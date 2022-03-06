using HouseholdBudgetPlanner.App.Abstract;
using HouseholdBudgetPlanner.Domain.Entity;
using HouseholdBudgetPlanner.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HouseholdBudgetPlanner.App.Managers
{
    public class AmountManagerRemoveExpense : AmountManager
    {
        private IService<Amount> _amountService;
        private List<Amount> _amountsGetList;
        private readonly AmountManager _amountManager;

        public AmountManagerRemoveExpense(IService<Amount> amountService, AmountManager amountManager)
        {
            _amountService = amountService;
            _amountsGetList = amountService.GetAllItems();
            _amountManager = amountManager;
        }
        public void ExpenseInAmountByDateList(DateTime dateStartEntered, DateTime dateEndEntered) //private
        {
            Console.WriteLine($"\r\nYour expenses since {dateStartEntered} to {dateEndEntered}\r\n");
            var expenseListString = _amountsGetList.AsQueryable()
                .Where(amount => amount.Date > dateStartEntered && amount.Date < dateEndEntered && amount.Id > 0)
                .Select(amount => $"{amount.Date}; Name: {amount.Name}; Value: {amount.Value}{ValueTypes.PLN}");
            foreach (var expenseString in expenseListString)
            {
                Console.WriteLine(expenseString);
            }
        }
        public bool SelectedExpenseInAmountExist(DateTime dateStartEntered, DateTime dateEndEntered, string name, decimal valueInDecimal) //private
        {
            return _amountsGetList.AsQueryable()
                .Where(amount => amount.Date > dateStartEntered && amount.Date < dateEndEntered && amount.Id > 0 &&
                amount.Name == name && amount.Value == valueInDecimal).Any();
        }
        private void RemoveChosenAmountExpense(DateTime dateStartEntered, DateTime dateEndEntered, string nameOfRemoveAmount, decimal valueInDecimal) //private
        {
            var amountExpense = _amountsGetList.AsQueryable()
                .Where(amount => amount.Date > dateStartEntered && amount.Date < dateEndEntered && amount.Id > 0 &&
                amount.Name == nameOfRemoveAmount && amount.Value == valueInDecimal).FirstOrDefault();
            Console.WriteLine("\r\nYou selected: " + amountExpense.Date + "; Name: " + amountExpense.Name + "; Value: " + amountExpense.Value + ValueTypes.PLN);
            Console.WriteLine($"\r\nDo you want to remove this expense?\r\n");
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No");
            var keyInfoRemoveExpense = Console.ReadKey();
            if (keyInfoRemoveExpense.KeyChar == '1')
            {
                _amountService.RemoveItem(amountExpense);
                Console.WriteLine("\r\nExpense has been removed!");
                _amountManager.AmountsWriteFile(amountExpense);
            }
            else if (keyInfoRemoveExpense.KeyChar == '2')
            {
                Console.WriteLine("\r\nExpense has not been removed!");
            }
            else
            {
                Console.WriteLine("\r\nAction you entered does not exist\r\n");
                Console.WriteLine("\r\nExpense has not been removed!");
            }
        }

        public void RemoveAmountExpenseSelect() //internal
        {
            Console.WriteLine("\r\nYou selected from expenses!");
            Console.WriteLine("\r\nPlease select a date range of removed expense");
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
                ExpenseInAmountByDateList(dateStartEntered, dateEndEntered);
                Console.Write("\r\nWrite a name of an expense to remove and press \"Enter\": ");
                var nameOfRemoveAmount = Console.ReadLine();
                Console.Write($"\r\nPlease write value of an expense to remove {ValueTypes.PLN}: ");
                decimal valueInDecimal = EnterValue();
                bool selectedExpenseInAmountExist = SelectedExpenseInAmountExist(dateStartEntered, dateEndEntered, nameOfRemoveAmount, valueInDecimal);
                if (selectedExpenseInAmountExist)
                {
                    RemoveChosenAmountExpense(dateStartEntered, dateEndEntered, nameOfRemoveAmount, valueInDecimal);
                }
                else
                {
                    Console.WriteLine($"\r\nExpense by name and value you've selected does not exist!");
                }
            }
            else
            {
                Console.WriteLine($"\r\nExpenses in selected date range do not exist!");
            }
        }
    }
}
