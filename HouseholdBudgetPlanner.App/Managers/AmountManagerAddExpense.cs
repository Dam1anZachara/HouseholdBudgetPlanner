using HouseholdBudgetPlanner.Domain.Entity;
using HouseholdBudgetPlanner.Helpers;
using System;

namespace HouseholdBudgetPlanner.App.Managers
{
    public class AmountManagerAddExpense : AmountManagerAdd
    {
        private readonly ExpenseTypeManager _expenseTypeManager;
        public AmountManagerAddExpense(ExpenseTypeManager expenseTypeManager)
        {
            _expenseTypeManager = expenseTypeManager;
        }
        public Amount AddAmountExpense() //internal
        {
            Amount addedAmount = new Amount();
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

            return addedAmount;
        }
    }
}
