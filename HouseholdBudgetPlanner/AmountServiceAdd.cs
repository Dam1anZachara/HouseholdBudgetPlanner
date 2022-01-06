using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseholdBudgetPlanner
{
    public class AmountServiceAdd:AmountService
    {
        ExpenseTypeService expenseTypeService;
        IncomeTypeService incomeTypeService;
        public AmountServiceAdd()
        {
            expenseTypeService = new ExpenseTypeService();
            incomeTypeService = new IncomeTypeService();
        }
        public void AddAmount(ConsoleKeyInfo keyInfoAddAmount)
        {
            Amount addedAmount = new Amount();
            if (keyInfoAddAmount.KeyChar == '1')
            {
                Console.WriteLine("\r\nYou selected as expense!");
                expenseTypeService.ExpenseTypeView();
                Console.Write("\r\nWrite name of selected expense type and press \"Enter\" or if you want to select General Expenses just press \"Enter\": ");
                string name = Console.ReadLine();
                var expenseTypeByName = expenseTypeService.GetExpenseToAmountByName(name);
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
                    Console.WriteLine($"\r\nYour amount will be assignet to the: {addedAmount.Name}");
                }
                else
                {
                    addedAmount.Name = expenseTypeByName.Name;
                    addedAmount.Id = expenseTypeByName.Id;
                    Console.WriteLine($"\r\nYou've selected wrong type name!");
                    Console.WriteLine($"\r\nYour amount will be assignet to the: {addedAmount.Name}");
                }

                Console.Write("\r\nWrite date in format \"dd/mm/yyyy\" and press \"Enter\" or if you want to set current date just press \"Enter\": ");
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
                Console.WriteLine($"\r\nDo you want to assign new expense {addedAmount.Value}{ValueTypes.PLN} to type: {addedAmount.Name}, with date: {addedAmount.Date}\r\n");
                Console.WriteLine("1. Yes");
                Console.WriteLine("2. No");
                var keyInfoAddExpense = Console.ReadKey();
                if (keyInfoAddExpense.KeyChar == '1')
                {
                    Amounts.Add(addedAmount);
                    if (Amounts.Count == 1)
                    {
                        Console.WriteLine("\r\nExpense has been added!");
                    }
                }
                else if (keyInfoAddExpense.KeyChar == '2')
                {
                    Console.WriteLine("\r\nExpense has not been added!");
                }
                else
                {
                    Console.WriteLine("\r\nAction you entered does not exist\r\n");
                    Console.WriteLine("Expense has not been added!");
                }
            }
            else if (keyInfoAddAmount.KeyChar == '2')
            {
                Console.WriteLine("\r\nYou selected as income!");
                incomeTypeService.IncomeTypeView();
                Console.Write("\r\nWrite name of selected income type and press \"Enter\" or if you want to select General Incomes just press \"Enter\": ");
                string name = Console.ReadLine();
                var incomeTypeByName = incomeTypeService.GetIncomeToAmountByName(name);
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
                    Console.WriteLine($"\r\nYour amount will be assignet to the: {addedAmount.Name}");
                }
                else
                {
                    addedAmount.Name = incomeTypeByName.Name;
                    addedAmount.Id = incomeTypeByName.Id;
                    Console.WriteLine($"\r\nYou've selected wrong type name!");
                    Console.WriteLine($"\r\nYour amount will be assignet to the: {addedAmount.Name}");
                }

                Console.Write("\r\nWrite date in format \"dd/mm/yyyy\" and press \"Enter\" or if you want to set current date just press \"Enter\": ");
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
                Console.WriteLine($"\r\nDo you want to assign new income {addedAmount.Value}{ValueTypes.PLN} to type: {addedAmount.Name}, with date: {addedAmount.Date}\r\n");
                Console.WriteLine("1. Yes");
                Console.WriteLine("2. No");
                var keyInfoAddExpense = Console.ReadKey();
                if (keyInfoAddExpense.KeyChar == '1')
                {
                    Amounts.Add(addedAmount);
                    Console.WriteLine("\r\nIncome has been added!");
                }
                else if (keyInfoAddExpense.KeyChar == '2')
                {
                    Console.WriteLine("\r\nIncome has not been added!");
                }
                else
                {
                    Console.WriteLine("\r\nAction you entered does not exist\r\n");
                    Console.WriteLine("\r\nIncome has not been added!");
                }
            }
            else
            {
                Console.WriteLine("\r\nAction you entered does not exist\r\n");
            }
        }
    }
}
