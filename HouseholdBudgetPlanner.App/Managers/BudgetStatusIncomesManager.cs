using HouseholdBudgetPlanner.App.Abstract;
using HouseholdBudgetPlanner.Domain.Entity;
using HouseholdBudgetPlanner.Helpers;
using System;
using System.Collections.Generic;

namespace HouseholdBudgetPlanner.App.Managers
{
    public class BudgetStatusIncomesManager:AmountManagerBudgetStatus
    {
        private IService<Amount> _amountService;
        private List<Amount> _amountsGetList;
        private readonly AmountManagerBudgetStatus _amountManagerBudgetStatus;
        private readonly AmountManager _amountManager;
        public BudgetStatusIncomesManager(IService<Amount> amountService, AmountManagerBudgetStatus amountManagerBudgetStatus, AmountManager amountManager)
        {
            _amountService = amountService;
            _amountsGetList = amountService.GetAllItems();
            _amountManagerBudgetStatus = amountManagerBudgetStatus;
            _amountManager = amountManager;
        }
        private ConsoleKeyInfo BudgetStatusIncomeTypeMenu()
        {
            Console.WriteLine($"\r\nSelect income type: \r\n");
            Console.WriteLine("1. All incomes");
            Console.WriteLine("2. Select by a name of an income type");
            var keyInfoStatusIncome = Console.ReadKey();
            return keyInfoStatusIncome;
        }
        private bool MonthIncomeInAmountByNameExist(string name)
        {
            foreach (var amount in _amountsGetList)
            {
                if (DateTime.Now.Month == amount.Date.Month && DateTime.Now.Year == amount.Date.Year && amount.Id < 0 && amount.Name == name)
                {
                    return true;
                    break;
                }
            }
            return false;
        }
        private bool RangeIncomeInAmountByNameExist(DateTime dateStartEntered, DateTime dateEndEntered, string name)
        {
            foreach (var amount in _amountsGetList)
            {
                if ((amount.Date > dateStartEntered) && (amount.Date < dateEndEntered) && (amount.Id < 0) && (dateStartEntered < dateEndEntered) && (amount.Name == name))
                {
                    return true;
                    break;
                }
            }
            return false;
        }
        public void BudgetStatusIncomesMethod()
        {
            Console.WriteLine($"\r\nStatus of incomes. Select the time interval: \r\n");
            var keyInfoStatusIncomeMonthDate = BudgetStatusDateMenu();
            switch (keyInfoStatusIncomeMonthDate.KeyChar)
            {
                case '1':
                    var keyInfoStatusIncomeMonthName = BudgetStatusIncomeTypeMenu();

                    switch (keyInfoStatusIncomeMonthName.KeyChar)
                    {
                        case '1':
                            _amountManagerBudgetStatus.BudgetStatusAllIncomesMonth();
                            break;
                        case '2':
                            Console.WriteLine($"\r\nYour incomes this month:\r\n");
                            foreach (var amount in _amountsGetList)
                            {
                                if (DateTime.Now.Month == amount.Date.Month && DateTime.Now.Year == amount.Date.Year && amount.Id < 0)
                                {
                                    Console.WriteLine($"{amount.Date}, Name: {amount.Name}, value: {amount.Value}{ValueTypes.PLN}");
                                }
                            }
                            Console.Write("\r\nWrite the name of the selected income type to sort by a name and press \"Enter\": ");
                            string name = Console.ReadLine();
                            bool monthIncomeInAmountByNameExist = MonthIncomeInAmountByNameExist(name);
                            if (monthIncomeInAmountByNameExist)
                            {
                                decimal amountSumNameIncomes = 0;
                                foreach (var amount in _amountsGetList)
                                {
                                    if (DateTime.Now.Month == amount.Date.Month && DateTime.Now.Year == amount.Date.Year && amount.Id < 0 && amount.Name == name)
                                    {
                                        amountSumNameIncomes += amount.Value;
                                    }
                                }
                                Console.WriteLine($"\r\nIncomes status this month with the name {name}: {amountSumNameIncomes}{ValueTypes.PLN}\r\n");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("\r\nIncome type with this name does not exist.\r\n");
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
                    bool incomeInAmountByDateExist = _amountManager.IncomeInAmountByDateExist(dateStartEntered, dateEndEntered);
                    if (incomeInAmountByDateExist)
                    {
                        var keyInfoStatusIncomeRangeName = BudgetStatusIncomeTypeMenu();
                        switch (keyInfoStatusIncomeRangeName.KeyChar)
                        {
                            case '1':
                                decimal amountSumAllIncomes = 0;
                                foreach (var amount in _amountsGetList)
                                {
                                    if ((amount.Date > dateStartEntered) && (amount.Date < dateEndEntered) && (amount.Id < 0) && (dateStartEntered < dateEndEntered))
                                    {
                                        amountSumAllIncomes += amount.Value;
                                    }
                                }
                                Console.WriteLine($"\r\nIncomes status since {dateEndEntered} to {dateEndEntered}: {amountSumAllIncomes}{ValueTypes.PLN}\r\n");
                                break;
                            case '2':
                                Console.WriteLine($"\r\nYour incomes since {dateEndEntered} to {dateEndEntered}.\r\n");
                                foreach (var amount in _amountsGetList)
                                {
                                    if ((amount.Date > dateStartEntered) && (amount.Date < dateEndEntered) && (amount.Id < 0) && (dateStartEntered < dateEndEntered))
                                    {
                                        Console.WriteLine($"{amount.Date}, Name: {amount.Name}, value: {amount.Value}{ValueTypes.PLN}");
                                    }
                                }
                                Console.Write("\r\nWrite the name of the selected income type to sort by a name and press \"Enter\": ");
                                string name = Console.ReadLine();
                                bool rangeIncomeInAmountByNameExist = RangeIncomeInAmountByNameExist(dateStartEntered, dateEndEntered, name);
                                if (rangeIncomeInAmountByNameExist)
                                {
                                    decimal amountSumNameIncomes = 0;
                                    foreach (var amount in _amountsGetList)
                                    {
                                        if ((amount.Date > dateStartEntered) && (amount.Date < dateEndEntered) && (amount.Id < 0) && (dateStartEntered < dateEndEntered) && (amount.Name == name))
                                        {
                                            amountSumNameIncomes += amount.Value;
                                        }
                                    }
                                    Console.WriteLine($"\r\nIncomes status with the name {name} since {dateEndEntered} to {dateEndEntered}: {amountSumNameIncomes}{ValueTypes.PLN}\r\n");
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("\r\nIncomes type with this name does not exist.\r\n");
                                }
                                break;
                            default:
                                Console.WriteLine("\r\nAction you entered does not exist\r\n");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\r\nIncomes in selected date range do not exist!");
                    }
                    break;
                default:
                    Console.WriteLine("\r\nAction you entered does not exist\r\n");
                    break;
            }
        }
    }
}
