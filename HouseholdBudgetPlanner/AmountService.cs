using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseholdBudgetPlanner
{
    public class AmountService
    {
        public List<Amount> Amounts { get; set; }

        ExpenseTypeService expenseTypeService;
        IncomeTypeService incomeTypeService;

        public AmountService()
        {
            Amounts = new List<Amount>();
            expenseTypeService = new ExpenseTypeService();
            incomeTypeService = new IncomeTypeService();
        }
        public ConsoleKeyInfo AddAmountView(MenuActionService actionService)
        {
            var addAmountMenu = actionService.GetMenuActionsByMenuName("AddAmountMenu");
            Console.WriteLine("\r\n\r\nPlease select where you want to assign the amount \r\n");
            for (int i = 0; i < addAmountMenu.Count; i++)
            {
                Console.WriteLine($"{addAmountMenu[i].Id}. {addAmountMenu[i].Name}");
            }
            var operation = Console.ReadKey();
            return operation;
        }
        public ConsoleKeyInfo RemoveAmountView(MenuActionService actionService)
        {
            var removeAmountMenu = actionService.GetMenuActionsByMenuName("RemoveAmountMenu");
            Console.WriteLine("\r\n\r\nPlease select from where do you want remove the amount \r\n");
            for (int i = 0; i < removeAmountMenu.Count; i++)
            {
                Console.WriteLine($"{removeAmountMenu[i].Id}. {removeAmountMenu[i].Name}");
            }
            var operation = Console.ReadKey();
            return operation;
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
                else if(name =="")
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
                    while (!DateTime.TryParseExact(date, "dd/mm/yyyy", null, System.Globalization.DateTimeStyles.None, out dateEntered))
                    {
                        Console.WriteLine("\r\nInvalid format date, please retry in format \"dd/mm/yyyy\" and press \"Enter\": ");
                        date = Console.ReadLine();
                    }
                    Console.WriteLine($"\r\nYour selected date is: {dateEntered}\r\n");
                }
                addedAmount.Date = dateEntered;

                Console.Write($"Please write amount in {ValueTypes.PLN}: ");
                var valueString = Console.ReadLine();
                decimal valueInDecimal;
                while (!decimal.TryParse(valueString, out valueInDecimal))
                {
                    Console.WriteLine("\r\nInvalid format amount, please retry!\r\n");
                    Console.Write($"Please write amount in {ValueTypes.PLN}: ");
                    valueString = Console.ReadLine();
                }
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
                        Console.WriteLine(Amounts[0].Id);
                        Console.WriteLine(Amounts[0].Name);
                        Console.WriteLine(Amounts[0].Value);
                        Console.WriteLine(Amounts[0].Date);
                        Console.WriteLine();
                    }

                    else if (Amounts.Count == 2)
                    {
                        Console.WriteLine("\r\nExpense has been added!");
                        Console.WriteLine(Amounts[0].Id);
                        Console.WriteLine(Amounts[0].Name);
                        Console.WriteLine(Amounts[0].Value);
                        Console.WriteLine(Amounts[0].Date);
                        Console.WriteLine();
                        Console.WriteLine(Amounts[1].Id);
                        Console.WriteLine(Amounts[1].Name);
                        Console.WriteLine(Amounts[1].Value);
                        Console.WriteLine(Amounts[1].Date);
                    }
                    else if (Amounts.Count == 3)
                    {
                        Console.WriteLine("\r\nExpense has been added!");
                        Console.WriteLine(Amounts[0].Id);
                        Console.WriteLine(Amounts[0].Name);
                        Console.WriteLine(Amounts[0].Value);
                        Console.WriteLine(Amounts[0].Date);
                        Console.WriteLine();
                        Console.WriteLine(Amounts[1].Id);
                        Console.WriteLine(Amounts[1].Name);
                        Console.WriteLine(Amounts[1].Value);
                        Console.WriteLine(Amounts[1].Date);
                        Console.WriteLine();
                        Console.WriteLine(Amounts[2].Id);
                        Console.WriteLine(Amounts[2].Name);
                        Console.WriteLine(Amounts[2].Value);
                        Console.WriteLine(Amounts[2].Date);
                    }
                    else
                    {
                        Console.WriteLine("\r\nNothing!");
                    }
                }
                else if (keyInfoAddExpense.KeyChar == '2')
                {
                    Console.WriteLine("\r\nExpense has not been added!");
                    if (Amounts.Count == 1)
                    {
                        Console.WriteLine(Amounts[0].Id);
                        Console.WriteLine(Amounts[0].Name);
                        Console.WriteLine(Amounts[0].Value);
                        Console.WriteLine(Amounts[0].Date);
                        Console.WriteLine();
                    }

                    else if (Amounts.Count == 2)
                    {
                        Console.WriteLine(Amounts[0].Id);
                        Console.WriteLine(Amounts[0].Name);
                        Console.WriteLine(Amounts[0].Value);
                        Console.WriteLine(Amounts[0].Date);
                        Console.WriteLine();
                        Console.WriteLine(Amounts[1].Id);
                        Console.WriteLine(Amounts[1].Name);
                        Console.WriteLine(Amounts[1].Value);
                        Console.WriteLine(Amounts[1].Date);
                    }
                    else if (Amounts.Count == 3)
                    {
                        Console.WriteLine(Amounts[0].Id);
                        Console.WriteLine(Amounts[0].Name);
                        Console.WriteLine(Amounts[0].Value);
                        Console.WriteLine(Amounts[0].Date);
                        Console.WriteLine();
                        Console.WriteLine(Amounts[1].Id);
                        Console.WriteLine(Amounts[1].Name);
                        Console.WriteLine(Amounts[1].Value);
                        Console.WriteLine(Amounts[1].Date);
                        Console.WriteLine();
                        Console.WriteLine(Amounts[2].Id);
                        Console.WriteLine(Amounts[2].Name);
                        Console.WriteLine(Amounts[2].Value);
                        Console.WriteLine(Amounts[2].Date);
                    }
                    else
                    {
                        Console.WriteLine("\r\nNothing!");
                    }
                }
                else
                {
                    Console.WriteLine("\r\nAction you entered does not exist\r\n");
                    Console.WriteLine("Expense has not been added!");
                    if (Amounts.Count == 1)
                    {
                        Console.WriteLine(Amounts[0].Id);
                        Console.WriteLine(Amounts[0].Name);
                        Console.WriteLine(Amounts[0].Value);
                        Console.WriteLine(Amounts[0].Date);
                        Console.WriteLine();
                    }

                    else if (Amounts.Count == 2)
                    {
                        Console.WriteLine(Amounts[0].Id);
                        Console.WriteLine(Amounts[0].Name);
                        Console.WriteLine(Amounts[0].Value);
                        Console.WriteLine(Amounts[0].Date);
                        Console.WriteLine();
                        Console.WriteLine(Amounts[1].Id);
                        Console.WriteLine(Amounts[1].Name);
                        Console.WriteLine(Amounts[1].Value);
                        Console.WriteLine(Amounts[1].Date);
                    }
                    else if (Amounts.Count == 3)
                    {
                        Console.WriteLine(Amounts[0].Id);
                        Console.WriteLine(Amounts[0].Name);
                        Console.WriteLine(Amounts[0].Value);
                        Console.WriteLine(Amounts[0].Date);
                        Console.WriteLine();
                        Console.WriteLine(Amounts[1].Id);
                        Console.WriteLine(Amounts[1].Name);
                        Console.WriteLine(Amounts[1].Value);
                        Console.WriteLine(Amounts[1].Date);
                        Console.WriteLine();
                        Console.WriteLine(Amounts[2].Id);
                        Console.WriteLine(Amounts[2].Name);
                        Console.WriteLine(Amounts[2].Value);
                        Console.WriteLine(Amounts[2].Date);
                    }
                    else
                    {
                        Console.WriteLine("\r\nNothing!");
                    }
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
                    while (!DateTime.TryParseExact(date, "dd/mm/yyyy", null, System.Globalization.DateTimeStyles.None, out dateEntered))
                    {
                        Console.WriteLine("\r\nInvalid format date, please retry in format \"dd/mm/yyyy\" and press \"Enter\": ");
                        date = Console.ReadLine();
                    }
                    Console.WriteLine($"\r\nYour selected date is: {dateEntered}\r\n");
                }
                addedAmount.Date = dateEntered;

                Console.Write($"Please write amount in {ValueTypes.PLN}: ");
                var valueString = Console.ReadLine();
                decimal valueInDecimal;
                while (!decimal.TryParse(valueString, out valueInDecimal))
                {
                    Console.WriteLine("\r\nInvalid format amount, please retry!\r\n");
                    Console.Write($"Please write amount in {ValueTypes.PLN}: ");
                    valueString = Console.ReadLine();
                }
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
        public void RemoveAmount(ConsoleKeyInfo keyInfoRemoveAmount)
        {
            if (keyInfoRemoveAmount.KeyChar == '1')
            {
                Console.WriteLine("\r\nYou selected from expense!");
                Console.WriteLine("\r\nPlease select a date range of removed expense");
                Console.Write("\r\nWrite starting date in format \"dd/mm/yyyy\" and press \"Enter\": ");
                var dateStart = Console.ReadLine();
                DateTime dateStartEntered;
                while (!DateTime.TryParseExact(dateStart, "dd/mm/yyyy", null, System.Globalization.DateTimeStyles.None, out dateStartEntered))
                {
                    Console.WriteLine("\r\nInvalid format date, please retry in format \"dd/mm/yyyy\" and press \"Enter\": ");
                    dateStart = Console.ReadLine();
                }
                Console.Write("\r\nWrite end date in format \"dd/mm/yyyy\" and press \"Enter\": ");
                var dateEnd = Console.ReadLine();
                DateTime dateEndEntered;
                while (!DateTime.TryParseExact(dateEnd, "dd/mm/yyyy", null, System.Globalization.DateTimeStyles.None, out dateEndEntered))
                {
                    Console.WriteLine("\r\nInvalid format date, please retry in format \"dd/mm/yyyy\" and press \"Enter\": ");
                    dateEnd = Console.ReadLine();
                }
                Console.WriteLine($"\r\nYour expenses in date range is from {dateStartEntered} to {dateEndEntered}\r\n");
                foreach (var amount in Amounts)
                {
                    if (amount.Date > dateStartEntered && amount.Date < dateEndEntered && amount.Id > 0)
                    {
                        Console.WriteLine(amount.Date + "; Name: " + amount.Name + "; Value: " + amount.Value + ValueTypes.PLN);
                    }
                }
                Console.Write("\r\nWrite name of expense to remove and press \"Enter\": ");
                var nameOfRemoveAmount = Console.ReadLine();
                Console.Write($"\r\nPlease write value of expense to remove {ValueTypes.PLN}: ");
                var valueString = Console.ReadLine();
                decimal valueInDecimal;
                while (!decimal.TryParse(valueString, out valueInDecimal))
                {
                    Console.WriteLine("\r\nInvalid format amount, please retry!\r\n");
                    Console.Write($"Please write value in {ValueTypes.PLN}: ");
                    valueString = Console.ReadLine();
                }

                foreach (var amount in Amounts)
                {
                    if ((amount.Date > dateStartEntered) && (amount.Date < dateEndEntered) && (amount.Id > 0) && 
                        (amount.Name == nameOfRemoveAmount) && (amount.Value == valueInDecimal))
                    {
                        Console.WriteLine("\r\nYou selected: " + amount.Date + "; Name: " + amount.Name + "; Value: " + amount.Value + ValueTypes.PLN);
                        Console.WriteLine($"\r\nDo you want to remove this expense?\r\n");
                        Console.WriteLine("1. Yes");
                        Console.WriteLine("2. No");
                        var keyInfoRemoveExpense = Console.ReadKey();
                        if (keyInfoRemoveExpense.KeyChar == '1')
                        {
                            Amounts.Remove(amount);
                            Console.WriteLine("\r\nExpense has been removed!");

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
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"\r\nExpense by name and value you've selected does not exist!");
                    }
                }
            }
            else if (keyInfoRemoveAmount.KeyChar == '2')
            {
                Console.WriteLine("\r\nYou selected from income!");
                Console.WriteLine("\r\nPlease select a date range of removed income");
                Console.Write("\r\nWrite starting date in format \"dd/mm/yyyy\" and press \"Enter\": ");
                var dateStart = Console.ReadLine();
                DateTime dateStartEntered;
                while (!DateTime.TryParseExact(dateStart, "dd/mm/yyyy", null, System.Globalization.DateTimeStyles.None, out dateStartEntered))
                {
                    Console.WriteLine("\r\nInvalid format date, please retry in format \"dd/mm/yyyy\" and press \"Enter\": ");
                    dateStart = Console.ReadLine();
                }
                Console.Write("\r\nWrite end date in format \"dd/mm/yyyy\" and press \"Enter\": ");
                var dateEnd = Console.ReadLine();
                DateTime dateEndEntered;
                while (!DateTime.TryParseExact(dateEnd, "dd/mm/yyyy", null, System.Globalization.DateTimeStyles.None, out dateEndEntered))
                {
                    Console.WriteLine("\r\nInvalid format date, please retry in format \"dd/mm/yyyy\" and press \"Enter\": ");
                    dateEnd = Console.ReadLine();
                }
                Console.WriteLine($"\r\nYour incomes in date range is from {dateStartEntered} to {dateEndEntered}\r\n");
                foreach (var amount in Amounts)
                {
                    if (amount.Date > dateStartEntered && amount.Date < dateEndEntered && amount.Id < 0)
                    {
                        Console.WriteLine(amount.Date + "; Name: " + amount.Name + "; Value: " + amount.Value + ValueTypes.PLN);
                    }
                }
                Console.Write("\r\nWrite name of income to remove and press \"Enter\": ");
                var nameOfRemoveAmount = Console.ReadLine();
                Console.Write($"\r\nPlease write amount of income to remove {ValueTypes.PLN}: ");
                var valueString = Console.ReadLine();
                decimal valueInDecimal;
                while (!decimal.TryParse(valueString, out valueInDecimal))
                {
                    Console.WriteLine("\r\nInvalid format amount, please retry!\r\n");
                    Console.Write($"Please write value in {ValueTypes.PLN}: ");
                    valueString = Console.ReadLine();
                }
                foreach (var amount in Amounts)
                {
                    if ((amount.Date > dateStartEntered) && (amount.Date < dateEndEntered) && (amount.Id < 0) &&
                        (amount.Name == nameOfRemoveAmount) && (amount.Value == valueInDecimal))
                    {
                        Console.WriteLine("\r\nYou selected: " + amount.Date + "; Name: " + amount.Name + "; Value: " + amount.Value + ValueTypes.PLN);
                        Console.WriteLine($"\r\nDo you want to remove this income?\r\n");
                        Console.WriteLine("1. Yes");
                        Console.WriteLine("2. No");
                        var keyInfoRemoveExpense = Console.ReadKey();
                        if (keyInfoRemoveExpense.KeyChar == '1')
                        {
                            Amounts.Remove(amount);
                            Console.WriteLine("\r\nIncome has been removed!");

                        }
                        else if (keyInfoRemoveExpense.KeyChar == '2')
                        {
                            Console.WriteLine("\r\nIncome has not been removed!");
                        }
                        else
                        {
                            Console.WriteLine("\r\nAction you entered does not exist\r\n");
                            Console.WriteLine("\r\nIncome has not been removed!");
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"\r\nIncome by name and value you've selected does not exist!");
                    }
                }
            }
            else
            {
                Console.WriteLine("\r\nAction you entered does not exist\r\n");
            }
        }
        public void AmountExist()
        {

        }
    }
}
