using HouseholdBudgetPlanner.App.Abstract;
using HouseholdBudgetPlanner.App.Concrete;
using HouseholdBudgetPlanner.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseholdBudgetPlanner.App.Managers
{
    public class IncomeTypeManager : IncomeTypeService
    {
        //public List<IncomeType> IncomeTypes { get; set; }

        public IncomeTypeManager()
        {
            //IncomeTypes = new List<IncomeType>();
            IncomeType expenseType = new IncomeType() { Id = -1, Name = "General incomes" };
            Items.Add(expenseType);
        }
        //private IService<IncomeType> _incomeType;
        //private readonly List<IncomeType> IncomeTypes;
        private int i;
        //public IncomeTypeManager()
        //{
        //    //IncomeTypes = _incomeType.GetAllItems();
        //}
        public void IncomeTypeView()
        {
            Console.WriteLine("\r\n\r\nAll your income types are:\r\n");

            for (i = 0; i < Items.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Items[i].Name}");
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
            foreach (var incomeType in Items)
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
                Items.Add(incomeTypeToAdd);
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
                foreach (var incomeType in Items)
                {
                    if (incomeType.Name == name)
                    {
                        Items.Remove(incomeType);
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
            foreach (var incomeType in Items)
            {
                if (incomeType.Name == name)
                {
                    return incomeType;
                }
            }
            var incomeTypeFirst = Items.First<IncomeType>();
            return incomeTypeFirst;
        }
    }
}
