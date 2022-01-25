using HouseholdBudgetPlanner.App.Abstract;
using HouseholdBudgetPlanner.App.Concrete;
using HouseholdBudgetPlanner.Domain.Entity;
using HouseholdBudgetPlanner.Helpers;
using System;
using System.Collections.Generic;

namespace HouseholdBudgetPlanner.App.Managers
{
    public class AmountManager
    {
        private readonly MenuActionService _actionService;
        private readonly IService<Amount> _amountService;
        private readonly List<Amount> _amountsGetList;
       
        public AmountManager(MenuActionService actionService, IService<Amount> amountService)
        {
            _amountService = amountService;
            _actionService = actionService;
            _amountsGetList = _amountService.GetAllItems();
        }
        public AmountManager()
        {
        }

        public ConsoleKeyInfo AddAmountView()
        {
            var addAmountMenu = _actionService.GetMenuActionsByMenuName("AddAmountMenu");
            Console.WriteLine("\r\n\r\nPlease select the assignment of the amount\r\n");
            for (int i = 0; i < addAmountMenu.Count; i++)
            {
                Console.WriteLine($"{addAmountMenu[i].Id}. {addAmountMenu[i].Name}");
            }
            var operation = Console.ReadKey();
            return operation;
        }
        public ConsoleKeyInfo RemoveAmountView()
        {
            var removeAmountMenu = _actionService.GetMenuActionsByMenuName("RemoveAmountMenu");
            Console.WriteLine("\r\n\r\nPlease select from where you want to remove the amount \r\n");
            for (int i = 0; i < removeAmountMenu.Count; i++)
            {
                Console.WriteLine($"{removeAmountMenu[i].Id}. {removeAmountMenu[i].Name}");
            }
            var operation = Console.ReadKey();
            return operation;
        }
        protected DateTime DateSelect(string date)
        {
            DateTime dateEntered;
            while (!DateTime.TryParseExact(date, "dd/mm/yyyy", null, System.Globalization.DateTimeStyles.None, out dateEntered))
            {
                Console.WriteLine("\r\nInvalid date format, please retry in format \"dd/mm/yyyy\" and press \"Enter\": ");
                date = Console.ReadLine();
            }
            return dateEntered;
        }
        protected decimal EnterValue()
        {
            var valueString = Console.ReadLine();
            decimal valueInDecimal;
            while (!(decimal.TryParse(valueString, out valueInDecimal) && valueInDecimal > 0))
            {
                Console.WriteLine("\r\nInvalid value format, value is zero or less than zero. Please retry!\r\n");
                Console.Write($"Please write value in {ValueTypes.PLN}: ");
                valueString = Console.ReadLine();
            }
            return Decimal.Round(decimal.Parse(String.Format("{0:0.00}", valueInDecimal)), 2);
        }
        internal bool ExpenseInAmountByDateExist(DateTime dateStartEntered, DateTime dateEndEntered)
        {
            foreach (var amount in _amountsGetList)
            {
                if ((amount.Date > dateStartEntered) && (amount.Date < dateEndEntered) && (amount.Id > 0) && (dateStartEntered < dateEndEntered))
                {
                    return true;
                }
            }
            return false;
        }
        internal bool IncomeInAmountByDateExist(DateTime dateStartEntered, DateTime dateEndEntered)
        {
            foreach (var amount in _amountsGetList)
            {
                if ((amount.Date > dateStartEntered) && (amount.Date < dateEndEntered) && (amount.Id < 0) && (dateStartEntered < dateEndEntered))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
