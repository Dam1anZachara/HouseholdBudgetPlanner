using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseholdBudgetPlanner
{
    public class AmountServiceRemove:AmountService
    {
        public void RemoveAmount(ConsoleKeyInfo keyInfoRemoveAmount)
        {
            if (keyInfoRemoveAmount.KeyChar == '1')
            {
                Console.WriteLine("\r\nYou selected from expense!");
                Console.WriteLine("\r\nPlease select a date range of removed expense");
                Console.Write("\r\nWrite starting date in format \"dd/mm/yyyy\" and press \"Enter\": ");
                var dateStart = Console.ReadLine();
                DateTime dateStartEntered;
                dateStartEntered = DateSelect(dateStart);
                Console.Write("\r\nWrite end date in format \"dd/mm/yyyy\" and press \"Enter\": ");
                var dateEnd = Console.ReadLine();
                DateTime dateEndEntered;
                dateEndEntered = DateSelect(dateEnd);
                bool expenseInAmountByDateExist = ExpenseInAmountByDateExist(dateStartEntered, dateEndEntered);
                if (expenseInAmountByDateExist)
                {
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
                    decimal valueInDecimal = EnterValue();
                    bool selectedExpenseInAmountExist = SelectedExpenseInAmountExist(dateStartEntered, dateEndEntered, nameOfRemoveAmount, valueInDecimal);
                    if (selectedExpenseInAmountExist)
                    {
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
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\r\nExpense by name and value you've selected don't exist!");
                    }
                }
                else
                {
                    Console.WriteLine($"\r\nExpenses in selected date range does not exist!");
                }
            }
            else if (keyInfoRemoveAmount.KeyChar == '2')
            {
                Console.WriteLine("\r\nYou selected from income!");
                Console.WriteLine("\r\nPlease select a date range of removed income");
                Console.Write("\r\nWrite starting date in format \"dd/mm/yyyy\" and press \"Enter\": ");
                var dateStart = Console.ReadLine();
                DateTime dateStartEntered;
                dateStartEntered = DateSelect(dateStart);
                Console.Write("\r\nWrite end date in format \"dd/mm/yyyy\" and press \"Enter\": ");
                var dateEnd = Console.ReadLine();
                DateTime dateEndEntered;
                dateEndEntered = DateSelect(dateEnd);
                bool incomeInAmountByDateExist = IncomeInAmountByDateExist(dateStartEntered, dateEndEntered);
                if (incomeInAmountByDateExist)
                {
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
                    Console.Write($"\r\nPlease write value of income to remove {ValueTypes.PLN}: ");
                    decimal valueInDecimal = EnterValue();
                    bool selectedIncomeInAmountExist = SelectedIncomeInAmountExist(dateStartEntered, dateEndEntered, nameOfRemoveAmount, valueInDecimal);
                    if (selectedIncomeInAmountExist)
                    {
                        foreach (var amount in Amounts)
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
                                    Amounts.Remove(amount);
                                    Console.WriteLine("\r\nIncome has been removed!");

                                }
                                else if (keyInfoRemoveIncome.KeyChar == '2')
                                {
                                    Console.WriteLine("\r\nIncome has not been removed!");
                                }
                                else
                                {
                                    Console.WriteLine("\r\nAction you entered don't exist\r\n");
                                    Console.WriteLine("\r\nIncome has not been removed!");
                                }
                                break;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\r\nIncome by name and value you've selected don't exist!");
                    }
                }
                else
                {
                    Console.WriteLine($"\r\nIncomes in selected date range does not exist!");
                }
            }
            else
            {
                Console.WriteLine("\r\nAction you entered don't exist\r\n");
            }
        }
    }
}
