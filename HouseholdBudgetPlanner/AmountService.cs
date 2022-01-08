using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseholdBudgetPlanner
{
    public class AmountService
    {
        public static List<Amount> Amounts { get; set; }
        public AmountService()
        {
            Amounts = new List<Amount>();
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
        protected DateTime DateSelect(string date)
        {
            DateTime dateEntered;
            while (!DateTime.TryParseExact(date, "dd/mm/yyyy", null, System.Globalization.DateTimeStyles.None, out dateEntered))
            {
                Console.WriteLine("\r\nInvalid format date, please retry in format \"dd/mm/yyyy\" and press \"Enter\": ");
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
                Console.WriteLine("\r\nInvalid format value or value is zero or less than zero. Please retry!\r\n");
                Console.Write($"Please write amount in {ValueTypes.PLN}: ");
                valueString = Console.ReadLine();
            }
            return valueInDecimal;
        }
        protected bool ExpenseInAmountByDateExist(DateTime dateStartEntered, DateTime dateEndEntered)
        {
            foreach (var amount in Amounts)
            {
                if ((amount.Date > dateStartEntered) && (amount.Date < dateEndEntered) && (amount.Id > 0) && (dateStartEntered < dateEndEntered))
                {
                    return true;
                    break;
                }
            }
            return false;
        }
        protected bool IncomeInAmountByDateExist(DateTime dateStartEntered, DateTime dateEndEntered)
        {
            foreach (var amount in Amounts)
            {
                if ((amount.Date > dateStartEntered) && (amount.Date < dateEndEntered) && (amount.Id < 0) && (dateStartEntered < dateEndEntered))
                {
                    return true;
                    break;
                }
            }
            return false;
        }
    }
}
