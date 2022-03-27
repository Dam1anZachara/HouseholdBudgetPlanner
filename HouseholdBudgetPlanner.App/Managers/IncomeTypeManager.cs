﻿using HouseholdBudgetPlanner.App.Abstract;
using HouseholdBudgetPlanner.App.FileSupport;
using HouseholdBudgetPlanner.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HouseholdBudgetPlanner.App.Managers
{
    public class IncomeTypeManager
    {
        private IService<IncomeType> _incomeTypeService;
        private List<IncomeType> _incomeTypesGetList;
        private IncomeTypeFileService _incomeTypeFileService;
        private int i;

        public IncomeTypeManager(IService<IncomeType> incomeTypeService, IncomeTypeFileService incomeTypeFileService)
        {
            _incomeTypeService = incomeTypeService;
            _incomeTypeFileService = incomeTypeFileService;
            _incomeTypesGetList = incomeTypeService.GetAllItems();
        }

        public void IncomeTypeView()
        {
            Console.WriteLine("\r\nAll your income types are:\r\n");
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
        public bool IncomeTypeExist(string name) //private
        {
            return _incomeTypesGetList.AsQueryable()
                .Where(incomeType => incomeType.Name == name && incomeType.Name != "General incomes").Any();
        }
        public void AddNewIncomeType(string name)
        {
            bool incomeTypeExist = IncomeTypeExist(name);
            if (name != "" && !incomeTypeExist && !name.Contains(' '))
            {
                IncomeType incomeTypeToAdd = new IncomeType() { Id = (i + 1) * -1, Name = name };
                _incomeTypeService.AddItem(incomeTypeToAdd);
                _incomeTypeFileService.IncomeTypeWriteFile();
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
                var incomeType = _incomeTypesGetList.AsQueryable().Where(incomeType => incomeType.Name == name).FirstOrDefault();
                _incomeTypeService.RemoveItem(incomeType);
                _incomeTypeFileService.IncomeTypeWriteFile();
                Console.WriteLine($"\r\nIncome type {name} has been removed.\r\n");
            }
            else
            {
                Console.WriteLine("\r\nIncome type with this name does not exist. General incomes can not be removed!\r\n");
            }
        }
        public IncomeType GetIncomeToAmountByName(string name) //internal
        {
            var incomeType = _incomeTypeService.GetItemByName(name);
            return incomeType;
        }
    }
}
