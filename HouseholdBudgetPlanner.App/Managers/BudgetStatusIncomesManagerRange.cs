using HouseholdBudgetPlanner.App.Abstract;
using HouseholdBudgetPlanner.Domain.Entity;
using HouseholdBudgetPlanner.Helpers;
using System;
using System.Collections.Generic;

namespace HouseholdBudgetPlanner.App.Managers
{
    public class BudgetStatusIncomesManagerRange : BudgetStatusIncomesManager
    {
        private IService<Amount> _amountService;
        private List<Amount> _amountsGetList;
        private readonly AmountManagerBudgetStatus _amountManagerBudgetStatus;
        private readonly AmountManager _amountManager;
        public BudgetStatusIncomesManagerRange(IService<Amount> amountService, AmountManagerBudgetStatus amountManagerBudgetStatus, AmountManager amountManager)
        {
            _amountService = amountService;
            _amountsGetList = amountService.GetAllItems();
            _amountManagerBudgetStatus = amountManagerBudgetStatus;
            _amountManager = amountManager;
        }

        public void BudgetStatusIncomesRangeDateList(DateTime dateStartEntered, DateTime dateEndEntered) //private
        {
            Console.WriteLine($"\r\nYour incomes since {dateStartEntered} to {dateEndEntered}.\r\n");
            foreach (var amount in _amountsGetList)
            {
                if ((amount.Date > dateStartEntered) && (amount.Date < dateEndEntered) && (amount.Id < 0) && (dateStartEntered < dateEndEntered))
                {
                    Console.WriteLine($"{amount.Date}, Name: {amount.Name}, Value: {amount.Value}{ValueTypes.PLN}");
                }
            }
        }
        public bool RangeIncomeInAmountByNameExist(DateTime dateStartEntered, DateTime dateEndEntered, string name) //private
        {
            foreach (var amount in _amountsGetList)
            {
                if ((amount.Date > dateStartEntered) && (amount.Date < dateEndEntered) && (amount.Id < 0) && (dateStartEntered < dateEndEntered) && (amount.Name == name))
                {
                    return true;
                }
            }
            return false;
        }
        public void BudgetStatusIncomesRangeDateByName(bool rangeIncomeInAmountByNameExist, DateTime dateStartEntered, DateTime dateEndEntered, string name) //private
        {
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
                Console.WriteLine($"\r\nIncomes status with the name {name} since {dateStartEntered} to {dateEndEntered}: {amountSumNameIncomes}{ValueTypes.PLN}\r\n");
            }
            else
            {
                Console.WriteLine("\r\nIncome type with this name does not exist.\r\n");
            }
        }
        internal void BudgetStatusIncomesMethodRange()
        {
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
                        _amountManagerBudgetStatus.BudgetStatusIncomesRangeDate(dateStartEntered, dateEndEntered);
                        break;
                    case '2':
                        BudgetStatusIncomesRangeDateList(dateStartEntered, dateEndEntered);
                        Console.Write("\r\nWrite the name of the selected income type to sort by a name and press \"Enter\": ");
                        string name = Console.ReadLine();
                        bool rangeIncomeInAmountByNameExist = RangeIncomeInAmountByNameExist(dateStartEntered, dateEndEntered, name);
                        BudgetStatusIncomesRangeDateByName(rangeIncomeInAmountByNameExist, dateStartEntered, dateEndEntered, name);
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
        }
    }
}
