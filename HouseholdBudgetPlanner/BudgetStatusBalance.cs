using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseholdBudgetPlanner
{
    public class BudgetStatusBalance:AmountServiceBudgetStatus
    {
        public void BudgetStatusBalanceMethod()
        {
            Console.WriteLine($"\r\nBudget balance. Select time interval: \r\n");
            var keyInfoStatusBalanceMonthDate = BudgetStatusDateMenu();
            switch (keyInfoStatusBalanceMonthDate.KeyChar)
            {
                case '1':
                    decimal amountSumAllExpenses = BudgetStatusAllExpensesMonth();
                    decimal amountSumAllIncomes = BudgetStatusAllIncomesMonth();
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
                    foreach (var amount in Amounts)
                    {
                        if ((amount.Date > dateStartEntered) && (amount.Date < dateEndEntered) && (amount.Id > 0) && (dateStartEntered < dateEndEntered))
                        {
                            amountSumAllExpensesDate += amount.Value;
                        }
                    }
                    Console.WriteLine($"\r\nExpenses status since {dateEndEntered} to {dateEndEntered}: {amountSumAllExpensesDate}{ValueTypes.PLN}");
                    decimal amountSumAllIncomesDate = 0;
                    foreach (var amount in Amounts)
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
