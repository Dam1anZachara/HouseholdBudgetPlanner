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
        private List<string> _expenseTypesGetFileList;
        public List<string> _expenseTypesSetFileList = new List<string>();
        private int i;
        string filePath = @"C:\Users\DZachara\source\repos\HouseholdBudgetPlanner\HouseholdBudgetPlanner\expenseType.txt";

        public ExpenseTypeManager(IService<ExpenseType> expenseTypeService)
        {
            _expenseTypeService = expenseTypeService;
            _expenseTypesGetList = _expenseTypeService.GetAllItems();
            ExpenseTypeReadFile();
            
        }
        public void ExpenseTypeReadFile()
        {
            _expenseTypesGetFileList = File.ReadAllLines(filePath).ToList();
            foreach (var expenseType in _expenseTypesGetFileList)
            {
                string[] expenseText = expenseType.Split(',');
                ExpenseType expenseTypeFile = new ExpenseType();
                expenseTypeFile.Id = int.Parse(expenseText[0]);
                expenseTypeFile.Name = expenseText[1];
                _expenseTypesGetList.Add(expenseTypeFile);

            }
            if (!_expenseTypesGetFileList.AsQueryable().Where(expenseType => expenseType == "1,General expenses").Any())
            {
                ExpenseType expenseTypeGeneral = new ExpenseType() { Id = 1, Name = "General expenses" };
                _expenseTypeService.AddItem(expenseTypeGeneral);
                File.WriteAllLines(filePath, _expenseTypesSetFileList);
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

                foreach (var expenseType in _expenseTypesGetList)
                {
                    _expenseTypesSetFileList.Add($"{expenseType.Id},{expenseType.Name}");
                }
                File.WriteAllLines(filePath, _expenseTypesSetFileList);

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
