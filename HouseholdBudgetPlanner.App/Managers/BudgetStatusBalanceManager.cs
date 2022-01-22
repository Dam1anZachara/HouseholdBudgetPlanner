using HouseholdBudgetPlanner.App.Abstract;
using HouseholdBudgetPlanner.Domain.Entity;
using HouseholdBudgetPlanner.Helpers;
using System;
using System.Collections.Generic;

namespace HouseholdBudgetPlanner.App.Managers
{
    public class BudgetStatusBalanceManager : AmountManagerBudgetStatus
    {
        private IService<Amount> _amountService;
        private List<Amount> _amountsGetList;
        private readonly AmountManagerBudgetStatus _amountManagerBudgetStatus;
        public BudgetStatusBalanceManager(IService<Amount> amountService, AmountManagerBudgetStatus amountManagerBudgetStatus)
        {
            _amountService = amountService;
            _amountsGetList = amountService.GetAllItems();
            _amountManagerBudgetStatus = amountManagerBudgetStatus;
        }
        internal void BudgetStatusBalanceMethod()
        {
            Console.WriteLine($"\r\nBudget balance. Select time interval: \r\n");
            var keyInfoStatusBalanceMonthDate = BudgetStatusDateMenu();
            switch (keyInfoStatusBalanceMonthDate.KeyChar)
            {
                case '1':
                    decimal amountSumAllExpenses = _amountManagerBudgetStatus.BudgetStatusAllExpensesMonth();
                    decimal amountSumAllIncomes = _amountManagerBudgetStatus.BudgetStatusAllIncomesMonth();
                    Console.WriteLine($"\r\nBudget balance this month: {amountSumAllIncomes - amountSumAllExpenses}{ValueTypes.PLN}\r\n");
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
                    decimal amountSumAllExpensesDate = 0;
                    foreach (var amount in _amountsGetList)
                    {
                        if ((amount.Date > dateStartEntered) && (amount.Date < dateEndEntered) && (amount.Id > 0) && (dateStartEntered < dateEndEntered))
                        {
                            amountSumAllExpensesDate += amount.Value;
                        }
                    }
                    Console.WriteLine($"\r\nExpenses status since {dateEndEntered} to {dateEndEntered}: {amountSumAllExpensesDate}{ValueTypes.PLN}");
                    decimal amountSumAllIncomesDate = 0;
                    foreach (var amount in _amountsGetList)
                    {
                        if ((amount.Date > dateStartEntered) && (amount.Date < dateEndEntered) && (amount.Id < 0) && (dateStartEntered < dateEndEntered))
                        {
                            amountSumAllIncomesDate += amount.Value;
                        }
                    }
                    Console.WriteLine($"\r\nIncomes status since {dateEndEntered} to {dateEndEntered}: {amountSumAllIncomesDate}{ValueTypes.PLN}");
                    Console.WriteLine($"\r\nBudget balance since {dateEndEntered} to {dateEndEntered}: {amountSumAllIncomesDate - amountSumAllExpensesDate}{ValueTypes.PLN}\r\n");
                    break;
                default:
                    Console.WriteLine("\r\nAction you entered does not exist\r\n");
                    break;
            }
        }
    }
}
