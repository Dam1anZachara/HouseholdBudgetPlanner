using HouseholdBudgetPlanner.App.Abstract;
using HouseholdBudgetPlanner.Domain.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HouseholdBudgetPlanner.App.Managers
{
    public class ExpenseTypeManager
    {
        private IService<ExpenseType> _expenseTypeService;
        private List<ExpenseType> _expenseTypesGetList;
        private List<string> _expenseTypesFileList;
        private int i;
        private string filePath = @"C:\Users\DZachara\source\repos\HouseholdBudgetPlanner\HouseholdBudgetPlanner\expenseType.txt";

        public ExpenseTypeManager(IService<ExpenseType> expenseTypeService)
        {
            _expenseTypeService = expenseTypeService;
            _expenseTypesGetList = _expenseTypeService.GetAllItems();
            ExpenseTypeReadFile();
        }
        private void ExpenseTypeReadFile()
        {
            _expenseTypesFileList = File.ReadAllLines(filePath).ToList();
            foreach (var expenseType in _expenseTypesFileList)
            {
                string[] expenseText = expenseType.Split(',');
                ExpenseType expenseTypeFile = new ExpenseType();
                expenseTypeFile.Id = int.Parse(expenseText[0]);
                expenseTypeFile.Name = expenseText[1];
                _expenseTypeService.AddItem(expenseTypeFile);
            }
            if (!_expenseTypesFileList.AsQueryable().Where(expenseType => expenseType == "1,General expenses").Any())
            {
                ExpenseType expenseTypeGeneral = new ExpenseType() { Id = 1, Name = "General expenses" };
                _expenseTypeService.AddItem(expenseTypeGeneral);
                ExpenseTypeWriteFile(expenseTypeGeneral);
            }
        }
        private void ExpenseTypeWriteFile(ExpenseType expenseType)
        {
            if (_expenseTypesGetList.Contains(expenseType))
            {
                _expenseTypesFileList.Add($"{expenseType.Id},{expenseType.Name}");
                File.WriteAllLines(filePath, _expenseTypesFileList);
            }
            else
            {
                _expenseTypesFileList.Remove($"{expenseType.Id},{expenseType.Name}");
                File.WriteAllLines(filePath, _expenseTypesFileList);
            }
        }
        public void ExpenseTypeView()
        {
            Console.WriteLine("\r\nAll your expense types are:\r\n");
            for (i = 0; i < _expenseTypesGetList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_expenseTypesGetList[i].Name}");
            }
        }
        public string AddNewExpenseTypeView()
        {
            Console.Write("\r\nPlease write the name of the new expense type (don't use space or existing name) and press \"enter\": ");
            string name = Console.ReadLine();
            return name;
        }
        public bool ExpanseTypeExist(string name)//private
        {
            return _expenseTypesGetList.AsQueryable()
                .Where(expenseType => expenseType.Name == name && expenseType.Name != "General expenses").Any();
        }
        public void AddNewExpanseType(string name)
        {
            bool expanseTypeExist = ExpanseTypeExist(name);
            if (name != "" && !expanseTypeExist && !name.Contains(' '))
            {
                ExpenseType expenseTypeToAdd = new ExpenseType() { Id = i + 1, Name = name };
                _expenseTypeService.AddItem(expenseTypeToAdd);
                Console.WriteLine($"\r\nExpense type {name} has been added.");
                ExpenseTypeWriteFile(expenseTypeToAdd);
            }
            else
            {
                Console.WriteLine("\r\nExpense type with this name exists, your name is empty or contains space.\r\n");
            }
            
        }
        public string RemoveExpenseTypeView()
        {
            Console.Write("\r\nPlease write the name of the expense type that you want to remove and press \"enter\": ");
            string name = Console.ReadLine();
            return name;
        }
        public void RemoveExpanseType(string name)
        {
            bool expanseTypeExist = ExpanseTypeExist(name);
            if (expanseTypeExist)
            {
                var expenseType =_expenseTypesGetList.AsQueryable().Where(expenseType => expenseType.Name == name).FirstOrDefault();
                _expenseTypeService.RemoveItem(expenseType);
                Console.WriteLine($"\r\nExpense type {name} has been removed.\r\n");
                ExpenseTypeWriteFile(expenseType);
            }
            else
            {
                Console.WriteLine("\r\nExpense type with this name does not exist. General expenses can not be removed!\r\n");
            }
        }
        public ExpenseType GetExpenseToAmountByName (string name) // internal
        {
            var expenseType = _expenseTypeService.GetItemByName(name);
            return expenseType;
        }
    }
}
