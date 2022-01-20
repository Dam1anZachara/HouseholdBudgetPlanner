using HouseholdBudgetPlanner.App.Abstract;
using HouseholdBudgetPlanner.Domain.Entity;
using HouseholdBudgetPlanner.Helpers;
using System;
using System.Collections.Generic;

namespace HouseholdBudgetPlanner.App.Managers
{
    public class BudgetStatusExpensesManager : AmountManagerBudgetStatus
    {
        private IService<Amount> _amountService;
        private List<Amount> _amountsGetList;
        private readonly AmountManagerBudgetStatus _amountManagerBudgetStatus;
        private readonly AmountManager _amountManager;
        public BudgetStatusExpensesManager(IService<Amount> amountService, AmountManagerBudgetStatus amountManagerBudgetStatus, AmountManager amountManager)
        {
            _amountService = amountService;
            _amountsGetList = amountService.GetAllItems();
            _amountManagerBudgetStatus = amountManagerBudgetStatus;
            _amountManager = amountManager;
        }
        private bool MonthExpenseInAmountByNameExist(string name)
        {
            foreach (var amount in _amountsGetList)
            {
                if (DateTime.Now.Month == amount.Date.Month && DateTime.Now.Year == amount.Date.Year && amount.Id > 0 && amount.Name == name)
                {
                    return true;
                    break;
                }
            }
            return false;
        }
        private bool RangeExpenseInAmountByNameExist(DateTime dateStartEntered, DateTime dateEndEntered, string name)
        {
            foreach (var amount in _amountsGetList)
            {
                if ((amount.Date > dateStartEntered) && (amount.Date < dateEndEntered) && (amount.Id > 0) && (dateStartEntered < dateEndEntered) && (amount.Name == name))
                {
                    return true;
                    break;
                }
            }
            return false;
        }
        private ConsoleKeyInfo BudgetStatusExpenseTypeMenu()
        {
            Console.WriteLine($"\r\nSelect expense type: \r\n");
            Console.WriteLine("1. All expenses");
            Console.WriteLine("2. Select by a name of expenses");
            var keyInfoStatusExpense = Console.ReadKey();
            return keyInfoStatusExpense;
        }
        public void BudgetStatusExpensesMethod()
        {
            Console.WriteLine($"\r\nStatus of expenses. Select the time interval: \r\n");
            var keyInfoStatusExpenseMonthDate = BudgetStatusDateMenu();
            switch (keyInfoStatusExpenseMonthDate.KeyChar)
            {
                case '1':
                    var keyInfoStatusExpenseMonthName = BudgetStatusExpenseTypeMenu();
                    switch (keyInfoStatusExpenseMonthName.KeyChar)
                    {
                        case '1':
                            _amountManagerBudgetStatus.BudgetStatusAllExpensesMonth();
                            break;
                        case '2':
                            Console.WriteLine($"\r\nYour expenses this month:\r\n");
                            foreach (var amount in _amountsGetList)
                            {
                                if (DateTime.Now.Month == amount.Date.Month && DateTime.Now.Year == amount.Date.Year && amount.Id > 0)
                                {
                                    Console.WriteLine($"{amount.Date}, Name: {amount.Name}, value: {amount.Value}{ValueTypes.PLN}");
                                }
                            }
                            Console.Write("\r\nWrite the name of the selected expense type to sort by a name and press \"Enter\": ");
                            string name = Console.ReadLine();
                            bool monthExpenseInAmountByNameExist = MonthExpenseInAmountByNameExist(name);
                            if (monthExpenseInAmountByNameExist)
                            {
                                decimal amountSumNameExpenses = 0;
                                foreach (var amount in _amountsGetList)
                                {
                                    if (DateTime.Now.Month == amount.Date.Month && DateTime.Now.Year == amount.Date.Year && amount.Id > 0 && amount.Name == name)
                                    {
                                        amountSumNameExpenses += amount.Value;
                                    }
                                }
                                Console.WriteLine($"\r\nExpenses status this month with the name {name}: {amountSumNameExpenses}{ValueTypes.PLN}\r\n");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("\r\nExpense type with this name does not exist.\r\n");
                            }
                            break;
                        default:
                            Console.WriteLine("\r\nAction you entered does not exist\r\n");
                            break;
                    }
                    break;
                case '2':
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
                                decimal amountSumAllExpenses = 0;
                                foreach (var amount in _amountsGetList)
                                {
                                    if ((amount.Date > dateStartEntered) && (amount.Date < dateEndEntered) && (amount.Id > 0) && (dateStartEntered < dateEndEntered))
                                    {
                                        amountSumAllExpenses += amount.Value;
                                    }
                                }
                                Console.WriteLine($"\r\nExpenses status since {dateEndEntered} to {dateEndEntered}: {amountSumAllExpenses}{ValueTypes.PLN}\r\n");
                                break;
                            case '2':
                                Console.WriteLine($"\r\nYour expenses since {dateEndEntered} to {dateEndEntered}.\r\n");
                                foreach (var amount in _amountsGetList)
                                {
                                    if ((amount.Date > dateStartEntered) && (amount.Date < dateEndEntered) && (amount.Id > 0) && (dateStartEntered < dateEndEntered))
                                    {
                                        Console.WriteLine($"{amount.Date}, Name: {amount.Name}, value: {amount.Value}{ValueTypes.PLN}");
                                    }
                                }
                                Console.Write("\r\nWrite the name of the selected expense type to sort by a name and press \"Enter\": ");
                                string name = Console.ReadLine();
                                bool rangeExpenseInAmountByNameExist = RangeExpenseInAmountByNameExist(dateStartEntered, dateEndEntered, name);
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
                                    Console.WriteLine($"\r\nExpenses status with the name {name} since {dateEndEntered} to {dateEndEntered}: {amountSumNameExpenses}{ValueTypes.PLN}\r\n");
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("\r\nExpense type with this name does not exist.\r\n");
                                }
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
                    break;
                default:
                    Console.WriteLine("\r\nAction you entered does not exist\r\n");
                    break;
            }
        }
    }
}
