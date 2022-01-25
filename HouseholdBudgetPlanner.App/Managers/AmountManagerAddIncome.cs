using HouseholdBudgetPlanner.Domain.Entity;
using HouseholdBudgetPlanner.Helpers;
using System;

namespace HouseholdBudgetPlanner.App.Managers
{
    public class AmountManagerAddIncome : AmountManagerAdd
    {
        private readonly IncomeTypeManager _incomeTypeManager;
        public AmountManagerAddIncome(IncomeTypeManager incomeTypeManager)
        {
            _incomeTypeManager = incomeTypeManager;
        }
        internal Amount AddAmountIncome()
        {
            Amount addedAmount = new Amount();
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

            return addedAmount;
        }
    }
}
