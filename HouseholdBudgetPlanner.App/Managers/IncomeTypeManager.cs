using HouseholdBudgetPlanner.App.Abstract;
using HouseholdBudgetPlanner.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HouseholdBudgetPlanner.App.Managers
{
    public class IncomeTypeManager
    {
        private IService<IncomeType> _IncomeTypeService;
        private List<IncomeType> _incomeTypesGetList;
        private int i;

        public IncomeTypeManager(IService<IncomeType> incomeTypeService)
        {
            _IncomeTypeService = incomeTypeService;
            IncomeType incomeType = new IncomeType() { Id = -1, Name = "General incomes" };
            _IncomeTypeService.AddItem(incomeType);
            _incomeTypesGetList = incomeTypeService.GetAllItems();
        }
        public void IncomeTypeView()
        {
            Console.WriteLine("\r\n\r\nAll your income types are:\r\n");

            for (i = 0; i < _incomeTypesGetList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_incomeTypesGetList[i].Name}");
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
            foreach (var incomeType in _incomeTypesGetList)
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
                _IncomeTypeService.AddItem(incomeTypeToAdd);
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
                foreach (var incomeType in _incomeTypesGetList)
                {
                    if (incomeType.Name == name)
                    {
                        _IncomeTypeService.RemoveItem(incomeType);
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
        internal IncomeType GetIncomeToAmountByName(string name)
        {
            var incomeType = _IncomeTypeService.GetItemByName(name);
            return incomeType;
        }
    }
}
