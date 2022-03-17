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
    public class BudgetStatusRaportManager : AmountManagerBudgetStatus
    {
        private IService<Amount> _amountService;
        private List<Amount> _amountsGetList;
        private List<Amount> _raportExpenseList;
        public BudgetStatusRaportManager(IService<Amount> amountService)
        {
            _amountService = amountService;
            _amountsGetList = amountService.GetAllItems();
            _raportExpenseList = new List<Amount>();
        }

        public void BudgetStatusRaportMethod()
        {
            var budgedStatusDateMenuKeyInfo = BudgetStatusDateMenu();
            switch (budgedStatusDateMenuKeyInfo.KeyChar)
            {
                case '1':
                    _raportExpenseList = RaportAllExpensesMonthList();

                    break;
                case '2':
                    break;
                default:
                    Console.WriteLine("\r\nAction you entered does not exist\r\n");
                    break;
            }
        }
        public List<Amount> RaportAllExpensesMonthList() //private
        {
            Console.WriteLine($"\r\nYour expenses this month:\r\n");
            var raportExpenseList = _amountsGetList.AsQueryable()
                .Where(amount => DateTime.Now.Month == amount.Date.Month && DateTime.Now.Year == amount.Date.Year && amount.Id > 0)
                .ToList();
            return raportExpenseList;
        }
        public List<Amount> RaportAllIncomesMonthList() //private
        {
            Console.WriteLine($"\r\nYour expenses this month:\r\n");
            var raportIncomeList = _amountsGetList.AsQueryable()
                .Where(amount => DateTime.Now.Month == amount.Date.Month && DateTime.Now.Year == amount.Date.Year && amount.Id < 0)
                .ToList();
            return raportIncomeList;
        }
    }
}
