using HouseholdBudgetPlanner.App.Abstract;
using HouseholdBudgetPlanner.Domain.Entity;
using HouseholdBudgetPlanner.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseholdBudgetPlanner.App.Managers
{
    public class AmountManagerRemove:AmountManager
    {
        //private readonly IService<Amount> _amount;
        //private readonly List<Amount> Amounts;
        public AmountManagerRemove()
        {
            //Amounts = _amount.GetAllItems();
        }
        private bool SelectedExpenseInAmountExist(DateTime dateStartEntered, DateTime dateEndEntered, string name, decimal valueInDecimal)
        {
            foreach (var amount in Items)
            {
                if ((amount.Date > dateStartEntered) && (amount.Date < dateEndEntered) && (amount.Id > 0) &&
                        (amount.Name == name) && (amount.Value == valueInDecimal))
                {
                    return true;
                    break;
                }
            }
            return false;
        }
        private bool SelectedIncomeInAmountExist(DateTime dateStartEntered, DateTime dateEndEntered, string nameOfRemoveAmount, decimal valueInDecimal)
        {
            foreach (var amount in Items)
            {
                if ((amount.Date > dateStartEntered) && (amount.Date < dateEndEntered) && (amount.Id < 0) &&
                        (amount.Name == nameOfRemoveAmount) && (amount.Value == valueInDecimal))
                {
                    return true;
                    break;
                }
            }
            return false;
        }
        public void RemoveAmount(ConsoleKeyInfo keyInfoRemoveAmount)
        {
            if (keyInfoRemoveAmount.KeyChar == '1')
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
                bool expenseInAmountByDateExist = ExpenseInAmountByDateExist(dateStartEntered, dateEndEntered);
                if (expenseInAmountByDateExist)
                {
                    Console.WriteLine($"\r\nYour expenses since {dateStartEntered} to {dateEndEntered}\r\n");
                    foreach (var amount in Items)
                    {
                        if (amount.Date > dateStartEntered && amount.Date < dateEndEntered && amount.Id > 0)
                        {
                            Console.WriteLine(amount.Date + "; Name: " + amount.Name + "; Value: " + amount.Value + ValueTypes.PLN);
                        }
                    }
                    Console.Write("\r\nWrite a name of an expense to remove and press \"Enter\": ");
                    var nameOfRemoveAmount = Console.ReadLine();
                    Console.Write($"\r\nPlease write value of an expense to remove {ValueTypes.PLN}: ");
                    decimal valueInDecimal = EnterValue();
                    bool selectedExpenseInAmountExist = SelectedExpenseInAmountExist(dateStartEntered, dateEndEntered, nameOfRemoveAmount, valueInDecimal);
                    if (selectedExpenseInAmountExist)
                    {
                        foreach (var amount in Items)
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
                                    Items.Remove(amount);
                                    Console.WriteLine("\r\nExpense has been removed!");
                                    break;
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
                        }
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
            else if (keyInfoRemoveAmount.KeyChar == '2')
            {
                Console.WriteLine("\r\nYou selected from incomes!");
                Console.WriteLine("\r\nPlease select a date range of removed income");
                Console.Write("\r\nWrite a starting date in format \"dd/mm/yyyy\" and press \"Enter\": ");
                var dateStart = Console.ReadLine();
                DateTime dateStartEntered;
                dateStartEntered = DateSelect(dateStart);
                Console.Write("\r\nWrite an ending date in format \"dd/mm/yyyy\" and press \"Enter\": ");
                var dateEnd = Console.ReadLine();
                DateTime dateEndEntered;
                dateEndEntered = DateSelect(dateEnd);
                bool incomeInAmountByDateExist = IncomeInAmountByDateExist(dateStartEntered, dateEndEntered);
                if (incomeInAmountByDateExist)
                {
                    Console.WriteLine($"\r\nYour incomes since {dateStartEntered} to {dateEndEntered}\r\n");
                    foreach (var amount in Items)
                    {
                        if (amount.Date > dateStartEntered && amount.Date < dateEndEntered && amount.Id < 0)
                        {
                            Console.WriteLine(amount.Date + "; Name: " + amount.Name + "; Value: " + amount.Value + ValueTypes.PLN);
                        }
                    }
                    Console.Write("\r\nWrite a name of an income to remove and press \"Enter\": ");
                    var nameOfRemoveAmount = Console.ReadLine();
                    Console.Write($"\r\nPlease write a value of an income to remove {ValueTypes.PLN}: ");
                    decimal valueInDecimal = EnterValue();
                    bool selectedIncomeInAmountExist = SelectedIncomeInAmountExist(dateStartEntered, dateEndEntered, nameOfRemoveAmount, valueInDecimal);
                    if (selectedIncomeInAmountExist)
                    {
                        foreach (var amount in Items)
                        {
                            if ((amount.Date > dateStartEntered) && (amount.Date < dateEndEntered) && (amount.Id < 0) &&
                                (amount.Name == nameOfRemoveAmount) && (amount.Value == valueInDecimal))
                            {
                                Console.WriteLine("\r\nYou selected: " + amount.Date + "; Name: " + amount.Name + "; Value: " + amount.Value + ValueTypes.PLN);
                                Console.WriteLine($"\r\nDo you want to remove this income?\r\n");
                                Console.WriteLine("1. Yes");
                                Console.WriteLine("2. No");
                                var keyInfoRemoveIncome = Console.ReadKey();
                                if (keyInfoRemoveIncome.KeyChar == '1')
                                {
                                    Items.Remove(amount);
                                    Console.WriteLine("\r\nIncome has been removed!");
                                    break;
                                }
                                else if (keyInfoRemoveIncome.KeyChar == '2')
                                {
                                    Console.WriteLine("\r\nIncome has not been removed!");
                                }
                                else
                                {
                                    Console.WriteLine("\r\nAction you entered does not exist\r\n");
                                    Console.WriteLine("\r\nIncome has not been removed!");
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\r\nIncome by name and value you've selected does not exist!");
                    }
                }
                else
                {
                    Console.WriteLine($"\r\nIncomes in selected date range do not exist!");
                }
            }
            else
            {
                Console.WriteLine("\r\nAction you entered does not exist\r\n");
            }
        }
    }
}
