using HouseholdBudgetPlanner.App.Abstract;
using HouseholdBudgetPlanner.App.Concrete;
using HouseholdBudgetPlanner.App.Managers;
using HouseholdBudgetPlanner.Domain.Entity;
using HouseholdBudgetPlanner.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseholdBudgetPlanner.App.Managers
{
    public class AmountManagerAdd : AmountManager
    {
        private readonly ExpenseTypeManager _expenseTypeManager;
        private readonly IncomeTypeManager _incomeTypeManager;
        private IService<Amount> _amountService;
        private List<Amount> _amountsGetList;
        public AmountManagerAdd(ExpenseTypeManager expenseTypeManager, IncomeTypeManager incomeTypeManager, AmountService amountService)
        {
            _expenseTypeManager = expenseTypeManager;
            _incomeTypeManager = incomeTypeManager;
            _amountService = amountService;
            _amountsGetList = amountService.GetAllItems();
        }
        public void AddAmount(ConsoleKeyInfo keyInfoAddAmount)
        {
            Amount addedAmount = new Amount();
            if (keyInfoAddAmount.KeyChar == '1')
            {
                Console.WriteLine("\r\nYou selected as expense!");
                _expenseTypeManager.ExpenseTypeView();
                Console.Write("\r\nWrite a name of the selected expense type and press \"Enter\" or if you want to select the General Expenses just press \"Enter\": ");
                string name = Console.ReadLine();
                var expenseTypeByName = _expenseTypeManager.GetExpenseToAmountByName(name);
                if (expenseTypeByName.Name == name)
                {
                    addedAmount.Name = expenseTypeByName.Name;
                    addedAmount.Id = expenseTypeByName.Id;
                    Console.WriteLine($"\r\nYou've selected expense type: {addedAmount.Name}");
                }
                else if (name == "")
                {
                    addedAmount.Name = expenseTypeByName.Name;
                    addedAmount.Id = expenseTypeByName.Id;
                    Console.WriteLine($"\r\nYour amount will be assigned to the: {addedAmount.Name}");
                }
                else
                {
                    addedAmount.Name = expenseTypeByName.Name;
                    addedAmount.Id = expenseTypeByName.Id;
                    Console.WriteLine($"\r\nYou've selected wrong name type!");
                    Console.WriteLine($"\r\nYour amount will be assigned to the: {addedAmount.Name}");
                }
                Console.Write("\r\nWrite a date in format \"dd/mm/yyyy\" and press \"Enter\" or if you want to set current date just press \"Enter\": ");
                var date = Console.ReadLine();
                DateTime dateEntered;
                if (date == "")
                {
                    dateEntered = DateTime.Now;
                    Console.WriteLine($"\r\nYour selected date is: {dateEntered}\r\n");
                }
                else
                {
                    dateEntered = DateSelect(date);
                    Console.WriteLine($"\r\nYour selected date is: {dateEntered}\r\n");
                }
                addedAmount.Date = dateEntered;
                Console.Write($"Please write value in {ValueTypes.PLN}: ");
                decimal valueInDecimal = EnterValue();
                addedAmount.Value = valueInDecimal;
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
            else if (keyInfoAddAmount.KeyChar == '2')
            {
                Console.WriteLine("\r\nYou selected as income!");
                _incomeTypeManager.IncomeTypeView();
                Console.Write("\r\nWrite a name of a selected income type and press \"Enter\" or if you want to select the General Incomes just press \"Enter\": ");
                string name = Console.ReadLine();
                var incomeTypeByName = _incomeTypeManager.GetIncomeToAmountByName(name);
                if (incomeTypeByName.Name == name)
                {
                    addedAmount.Name = incomeTypeByName.Name;
                    addedAmount.Id = incomeTypeByName.Id;
                    Console.WriteLine($"\r\nYou've selected income type: {addedAmount.Name}");
                }
                else if (name == "")
                {
                    addedAmount.Name = incomeTypeByName.Name;
                    addedAmount.Id = incomeTypeByName.Id;
                    Console.WriteLine($"\r\nYour amount will be assigned to the: {addedAmount.Name}");
                }
                else
                {
                    addedAmount.Name = incomeTypeByName.Name;
                    addedAmount.Id = incomeTypeByName.Id;
                    Console.WriteLine($"\r\nYou've selected wrong name type!");
                    Console.WriteLine($"\r\nYour amount will be assigned to the: {addedAmount.Name}");
                }
                Console.Write("\r\nWrite a date in format \"dd/mm/yyyy\" and press \"Enter\" or if you want to set current date just press \"Enter\": ");
                var date = Console.ReadLine();
                DateTime dateEntered;
                if (date == "")
                {
                    dateEntered = DateTime.Now;
                    Console.WriteLine($"\r\nYour selected date is: {dateEntered}\r\n");
                }
                else
                {
                    dateEntered = DateSelect(date);
                    Console.WriteLine($"\r\nYour selected date is: {dateEntered}\r\n");
                }
                addedAmount.Date = dateEntered;
                Console.Write($"Please write value in {ValueTypes.PLN}: ");
                decimal valueInDecimal = EnterValue();
                addedAmount.Value = valueInDecimal;
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
            else
            {
                Console.WriteLine("\r\nAction you entered does not exist\r\n");
            }
        }
    }
}
