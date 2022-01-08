using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseholdBudgetPlanner
{
    public class BudgetStatusIncomes:AmountServiceBudgetStatus
    {
        private ConsoleKeyInfo BudgetStatusIncomeTypeMenu()
        {
            Console.WriteLine($"\r\nSelect income type: \r\n");
            Console.WriteLine("1. All incomes");
            Console.WriteLine("2. Select by name of incomes");
            var keyInfoStatusIncome = Console.ReadKey();
            return keyInfoStatusIncome;
        }
        private bool MonthIncomeInAmountByNameExist(string name)
        {
            foreach (var amount in Amounts)
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
            foreach (var amount in Amounts)
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
            Console.WriteLine($"\r\nStatus of incomes. Select date: \r\n");
            var keyInfoStatusIncomeMonthDate = BudgetStatusDateMenu();
            switch (keyInfoStatusIncomeMonthDate.KeyChar)
            {
                case '1':
                    var keyInfoStatusIncomeMonthName = BudgetStatusIncomeTypeMenu();

                    switch (keyInfoStatusIncomeMonthName.KeyChar)
                    {
                        case '1':
                            BudgetStatusAllIncomesMonth();
                            break;
                        case '2':
                            Console.WriteLine($"\r\nYour incomes in this month:\r\n");
                            foreach (var amount in Amounts)
                            {
                                if (DateTime.Now.Month == amount.Date.Month && DateTime.Now.Year == amount.Date.Year && amount.Id < 0)
                                {
                                    Console.WriteLine($"{amount.Date}, Name: {amount.Name}, value: {amount.Value}{ValueTypes.PLN}");
                                }
                            }
                            Console.Write("\r\nWrite name of selected income type to sort by name and press \"Enter\": ");
                            string name = Console.ReadLine();
                            bool monthIncomeInAmountByNameExist = MonthIncomeInAmountByNameExist(name);
                            if (monthIncomeInAmountByNameExist)
                            {
                                decimal amountSumNameIncomes = 0;
                                foreach (var amount in Amounts)
                                {
                                    if (DateTime.Now.Month == amount.Date.Month && DateTime.Now.Year == amount.Date.Year && amount.Id < 0 && amount.Name == name)
                                    {
                                        amountSumNameIncomes += amount.Value;
                                    }
                                }
                                Console.WriteLine($"\r\nIncomes status in this month: {amountSumNameIncomes}{ValueTypes.PLN}\r\n");
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
                        var keyInfoStatusIncomeRangeName = BudgetStatusIncomeTypeMenu();
                        switch (keyInfoStatusIncomeRangeName.KeyChar)
                        {
                            case '1':
                                decimal amountSumAllIncomes = 0;
                                foreach (var amount in Amounts)
                                {
                                    if ((amount.Date > dateStartEntered) && (amount.Date < dateEndEntered) && (amount.Id < 0) && (dateStartEntered < dateEndEntered))
                                    {
                                        amountSumAllIncomes += amount.Value;
                                    }
                                }
                                Console.WriteLine($"\r\nIncomes status from {dateEndEntered} to {dateEndEntered}: {amountSumAllIncomes}{ValueTypes.PLN}\r\n");
                                break;
                            case '2':
                                Console.WriteLine($"\r\nYour incomes in date from {dateEndEntered} to {dateEndEntered}.\r\n");
                                foreach (var amount in Amounts)
                                {
                                    if ((amount.Date > dateStartEntered) && (amount.Date < dateEndEntered) && (amount.Id < 0) && (dateStartEntered < dateEndEntered))
                                    {
                                        Console.WriteLine($"{amount.Date}, Name: {amount.Name}, value: {amount.Value}{ValueTypes.PLN}");
                                    }
                                }
                                Console.Write("\r\nWrite name of selected income type to sort by name and press \"Enter\": ");
                                string name = Console.ReadLine();
                                bool rangeIncomeInAmountByNameExist = RangeIncomeInAmountByNameExist(dateStartEntered, dateEndEntered, name);
                                if (rangeIncomeInAmountByNameExist)
                                {
                                    decimal amountSumNameIncomes = 0;
                                    foreach (var amount in Amounts)
                                    {
                                        if ((amount.Date > dateStartEntered) && (amount.Date < dateEndEntered) && (amount.Id < 0) && (dateStartEntered < dateEndEntered) && (amount.Name == name))
                                        {
                                            amountSumNameIncomes += amount.Value;
                                        }
                                    }
                                    Console.WriteLine($"\r\nIncomes status with name {name} from {dateEndEntered} to {dateEndEntered}: {amountSumNameIncomes}{ValueTypes.PLN}\r\n");
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
