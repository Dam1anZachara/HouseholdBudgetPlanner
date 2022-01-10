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
            Console.WriteLine("\r\n\r\nAll your income types are:\r\n");

            for (i = 0; i < IncomeTypes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {IncomeTypes[i].Name}");
            }
        }
        public string AddNewIncomeTypeView()
        {
            Console.Write("\r\nPlease write the name of the new income type (don't use space or existing name) and press \"enter\": ");
            string name = Console.ReadLine();
            return name;
        }
        private bool IncomeTypeExist(string name)
        {
            foreach (var incomeType in IncomeTypes)
            {
                if (incomeType.Name == name && incomeType.Name != "General incomes")
                {
                    return true;
                    break;
                }
            }
            return false;
        }
        public void AddNewIncomeType(string name)
        {
            bool incomeTypeExist = IncomeTypeExist(name);
            if (name != "" && !incomeTypeExist && !name.Contains(' '))
            {
                IncomeType incomeTypeToAdd = new IncomeType() { Id = (i + 1) * -1, Name = name };
                IncomeTypes.Add(incomeTypeToAdd);
                Console.WriteLine($"\r\nIncome type {name} has been added.");
            }
            else
            {
                Console.WriteLine("\r\nIncome type with this name exists, your name is empty or contains space.\r\n");
            }
        }
        public string RemoveIncomeTypeView()
        {
            Console.Write("\r\nPlease write the name of the income type that you want to remove and press \"enter\": ");
            string name = Console.ReadLine();
            return name;
        }
        public void RemoveIncomeType(string name)
        {
            bool incomeTypeExist = IncomeTypeExist(name);
            if (incomeTypeExist)
            {
                foreach (var incomeType in IncomeTypes)
                {
                    if (incomeType.Name == name)
                    {
                        IncomeTypes.Remove(incomeType);
                        Console.WriteLine($"\r\nIncome type {name} has been removed.\r\n");
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("\r\nIncome type with this name does not exist. General incomes can not be removed!\r\n");
            }
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
