using HouseholdBudgetPlanner.App.Abstract;
using HouseholdBudgetPlanner.Domain.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HouseholdBudgetPlanner.App.Managers
{
    public class IncomeTypeManager
    {
        private IService<IncomeType> _incomeTypeService;
        private List<IncomeType> _incomeTypesGetList;
        private List<string> _incomeTypesFileList;
        private int i;
        private string filePath = @"C:\Users\Damian\source\repos\HouseholdBudgetPlanner\HouseholdBudgetPlanner\incomeType.txt";

        public IncomeTypeManager(IService<IncomeType> incomeTypeService)
        {
            _incomeTypeService = incomeTypeService;
            _incomeTypesGetList = incomeTypeService.GetAllItems();
            IncomeTypeReadFile();
        }
        private void IncomeTypeReadFile()
        {
            _incomeTypesFileList = File.ReadAllLines(filePath).ToList();
            foreach (var incomeType in _incomeTypesFileList)
            {
                string[] incomeText = incomeType.Split(',');
                IncomeType incomeTypeFile = new IncomeType();
                incomeTypeFile.Id = int.Parse(incomeText[0]);
                incomeTypeFile.Name = incomeText[1];
                _incomeTypeService.AddItem(incomeTypeFile);
            }
            if (!_incomeTypesFileList.AsQueryable().Where(incomeType => incomeType == "-1,General incomes").Any())
            {
                IncomeType incomeTypeGeneral = new IncomeType() { Id = -1, Name = "General incomes" };
                _incomeTypeService.AddItem(incomeTypeGeneral);
                IncomeTypeWriteFile(incomeTypeGeneral);
            }
        }
        private void IncomeTypeWriteFile(IncomeType incomeType)
        {
            if (_incomeTypesGetList.Contains(incomeType))
            {
                _incomeTypesFileList.Add($"{incomeType.Id},{incomeType.Name}");
                File.WriteAllLines(filePath, _incomeTypesFileList);
            }
            else
            {
                _incomeTypesFileList.Remove($"{incomeType.Id},{incomeType.Name}");
                File.WriteAllLines(filePath, _incomeTypesFileList);
            }
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
                Console.WriteLine($"\r\nIncome type {name} has been added.");
                IncomeTypeWriteFile(incomeTypeToAdd);
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
                Console.WriteLine($"\r\nIncome type {name} has been removed.\r\n");
                IncomeTypeWriteFile(incomeType);
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
