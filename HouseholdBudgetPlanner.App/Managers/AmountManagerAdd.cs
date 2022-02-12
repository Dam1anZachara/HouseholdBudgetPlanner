using HouseholdBudgetPlanner.App.Abstract;
using HouseholdBudgetPlanner.Domain.Entity;
using HouseholdBudgetPlanner.Helpers;
using System;

namespace HouseholdBudgetPlanner.App.Managers
{
    public class AmountManagerAdd : AmountManager
    {
        private IService<Amount> _amountService;
        private readonly AmountManagerAddExpense _amountManagerAddExpense;
        private readonly AmountManagerAddIncome _amountManagerAddIncome;
        public AmountManagerAdd(IService<Amount> amountService, AmountManagerAddExpense amountManagerAddExpense, AmountManagerAddIncome amountManagerAddIncome)
        {
            _amountService = amountService;
            _amountManagerAddExpense = amountManagerAddExpense;
            _amountManagerAddIncome = amountManagerAddIncome;
        }
        public AmountManagerAdd()
        {
        }
        public void AddAmount(ConsoleKeyInfo keyInfoAddAmount)
        {
            if (keyInfoAddAmount.KeyChar == '1')
            {
                var addedAmountOfExpense = _amountManagerAddExpense.AddAmountExpense();
                AddAmountExpenseAprove(addedAmountOfExpense);
            }
            else if (keyInfoAddAmount.KeyChar == '2')
            {
                var addedAmountOfIncome = _amountManagerAddIncome.AddAmountIncome();
                AddAmountIncomeAprove(addedAmountOfIncome);
            }
            else
            {
                Console.WriteLine("\r\nAction you entered does not exist\r\n");
            }
        }
        private void AddAmountExpenseAprove(Amount addedAmount) //private
        {
            Console.WriteLine($"\r\nDo you want to assign a new expense {addedAmount.Value}{ValueTypes.PLN} to the type: {addedAmount.Name}, with a date: {addedAmount.Date}\r\n");
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No");
            var keyInfoAddExpense = Console.ReadKey();
            switch (keyInfoAddExpense.KeyChar)
            {
                case '1':
                    _amountService.AddItem(addedAmount);
                    Console.WriteLine("\r\nExpense has been added!");
                    break;
                case '2':
                    Console.WriteLine("\r\nExpense has not been added!");
                    break;
                default:
                    Console.WriteLine("\r\nAction you entered does not exist\r\n");
                    Console.WriteLine("Expense has not been added!");
                    break;
            }
        }
        private void AddAmountIncomeAprove(Amount addedAmount) // private
        {
            Console.WriteLine($"\r\nDo you want to assign the new income {addedAmount.Value}{ValueTypes.PLN} to the type: {addedAmount.Name}, with a date: {addedAmount.Date}\r\n");
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No");
            var keyInfoAddExpense = Console.ReadKey();
            switch (keyInfoAddExpense.KeyChar)
            {
                case '1':
                    _amountService.AddItem(addedAmount);
                    Console.WriteLine("\r\nIncome has been added!");
                    break;
                case '2':
                    Console.WriteLine("\r\nIncome has not been added!");
                    break;
                default:
                    Console.WriteLine("\r\nAction you entered does not exist\r\n");
                    Console.WriteLine("\r\nIncome has not been added!");
                    break;
            }
        }
    }
}
