using HouseholdBudgetPlanner.App.Abstract;
using HouseholdBudgetPlanner.App.Concrete;
using HouseholdBudgetPlanner.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HouseholdBudgetPlanner.App.Managers
{
    public class BudgetStatusRaportManager : AmountManagerBudgetStatus
    {
        private IService<Amount> _amountService;
        private List<Amount> _amountsGetList;
        private List<Amount> _raportExpenseList;
        private List<Amount> _raportIncomeList;
        private AmountRaportListService _amountRaportListService;
        public BudgetStatusRaportManager(IService<Amount> amountService, AmountRaportListService amountRaportListService)
        {
            _amountService = amountService;
            _amountsGetList = amountService.GetAllItems();
            _raportExpenseList = new List<Amount>();
            _raportIncomeList = new List<Amount>();
            _amountRaportListService = amountRaportListService;
        }

        public void BudgetStatusRaportMethod()
        {
            Console.WriteLine($"\r\nBudget balance. Select time interval: \r\n");
            var budgedStatusDateMenuKeyInfo = BudgetStatusDateMenu();
            switch (budgedStatusDateMenuKeyInfo.KeyChar)
            {
                case '1':
                    _raportExpenseList = RaportAllExpensesMonthList();
                    _raportIncomeList = RaportAllIncomesMonthList();
                    _amountRaportListService.GenerateRaportMonthMethod(_raportExpenseList, _raportIncomeList);
                    Console.WriteLine("\r\nRaport has been generated!\r\n");
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
                    _raportExpenseList = RaportExpensesRangeDateList(dateStartEntered, dateEndEntered);
                    _raportIncomeList = RaportIncomesRangeDateList(dateStartEntered, dateEndEntered);
                    _amountRaportListService.GenerateRaportRangeDateMethod(_raportExpenseList, _raportIncomeList, dateStartEntered, dateEndEntered);
                    break;
                default:
                    Console.WriteLine("\r\nAction you entered does not exist\r\n");
                    break;
            }
        }
        private List<Amount> RaportAllExpensesMonthList() //private
        {
            var raportExpenseList = _amountsGetList.AsQueryable()
                .Where(amount => DateTime.Now.Month == amount.Date.Month && DateTime.Now.Year == amount.Date.Year && amount.Id > 0)
                .ToList();
            return raportExpenseList;
        }
        private List<Amount> RaportAllIncomesMonthList() //private
        {
            var raportIncomeList = _amountsGetList.AsQueryable()
                .Where(amount => DateTime.Now.Month == amount.Date.Month && DateTime.Now.Year == amount.Date.Year && amount.Id < 0)
                .ToList();
            return raportIncomeList;
        }
        private List<Amount> RaportExpensesRangeDateList(DateTime dateStartEntered, DateTime dateEndEntered) // private
        {
            Console.WriteLine($"\r\nYour expenses since {dateStartEntered} to {dateEndEntered}.\r\n");
            var raportExpenseList = _amountsGetList.AsQueryable()
                .Where(amount => amount.Date > dateStartEntered && amount.Date < dateEndEntered && amount.Id > 0 && dateStartEntered < dateEndEntered)
                .ToList();
            return raportExpenseList;
        }
        private List<Amount> RaportIncomesRangeDateList(DateTime dateStartEntered, DateTime dateEndEntered) //private
        {
            Console.WriteLine($"\r\nYour incomes since {dateStartEntered} to {dateEndEntered}.\r\n");
            var raportIncomeList = _amountsGetList.AsQueryable()
                .Where(amount => amount.Date > dateStartEntered && amount.Date < dateEndEntered && amount.Id < 0 && dateStartEntered < dateEndEntered)
                .ToList();
            return raportIncomeList;
        }
    }
}
