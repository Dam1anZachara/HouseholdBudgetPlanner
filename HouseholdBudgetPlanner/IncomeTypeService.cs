using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseholdBudgetPlanner
{
    public class IncomeTypeService
    {
        public static List<IncomeType> IncomeTypes { get; set; }

        private int i;
        public IncomeTypeService()
        {
            IncomeTypes = new List<IncomeType>();
            IncomeType expenseType = new IncomeType() { Id = -1, Name = "General incomes" };
            IncomeTypes.Add(expenseType);
        }

        public void IncomeTypeView()
        {
            Console.WriteLine("\r\n\r\nYour all income types are:\r\n");

            for (i = 0; i < IncomeTypes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {IncomeTypes[i].Name}");
            }
        }
        public string AddNewIncomeTypeView()
        {
            Console.Write("\r\nPlease write the name of new income type and press \"enter\": ");
            string name = Console.ReadLine();
            return name;
        }
        public void AddNewIncomeType(string name)
        {
            if (name != "")
            {
                IncomeType incomeTypeToAdd = new IncomeType() { Id = (i + 1) * -1, Name = name };
                IncomeTypes.Add(incomeTypeToAdd);
            }
        }
        public string RemoveIncomeTypeView()
        {
            Console.Write("\r\nPlease write the name of income type that you want to remove and press \"enter\": ");
            string name = Console.ReadLine();
            return name;
        }
        public void RemoveIncomeType(string name)
        {
            IncomeType incomeTypeToRemove = new IncomeType();
            foreach (var incomeType in IncomeTypes)
            {
                if (incomeType.Name == name)
                {
                    incomeTypeToRemove = incomeType;
                    break;
                }
            }
            IncomeTypes.Remove(incomeTypeToRemove);
        }
        public IncomeType GetIncomeToAmountByName(string name)
        {
            foreach (var incomeType in IncomeTypes)
            {
                if (incomeType.Name == name)
                {
                    return incomeType;
                }
            }
            var incomeTypeFirst = IncomeTypes.First<IncomeType>();
            return incomeTypeFirst;
        }
    }
}
