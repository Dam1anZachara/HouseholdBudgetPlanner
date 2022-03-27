using HouseholdBudgetPlanner.App.Abstract;
using HouseholdBudgetPlanner.App.FileSupport;
using HouseholdBudgetPlanner.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HouseholdBudgetPlanner.App.Managers
{
    public class ExpenseTypeManager
    {
        private IService<ExpenseType> _expenseTypeService;
        private List<ExpenseType> _expenseTypesGetList;
        private ExpenseTypeFileService _expenseTypeFileService;
        private int i;

        public ExpenseTypeManager(IService<ExpenseType> expenseTypeService, ExpenseTypeFileService expenseTypeFileService)
        {
            _expenseTypeService = expenseTypeService;
            _expenseTypeFileService = expenseTypeFileService;
            _expenseTypesGetList = _expenseTypeService.GetAllItems();
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
                _expenseTypeFileService.ExpenseTypeWriteFile();
                Console.WriteLine($"\r\nExpense type {name} has been added.");
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
                var expenseType = _expenseTypesGetList.AsQueryable().Where(expenseType => expenseType.Name == name).FirstOrDefault();
                _expenseTypeService.RemoveItem(expenseType);
                _expenseTypeFileService.ExpenseTypeWriteFile();
                Console.WriteLine($"\r\nExpense type {name} has been removed.\r\n");
            }
            else
            {
                Console.WriteLine("\r\nExpense type with this name does not exist. General expenses can not be removed!\r\n");
            }
        }
        public ExpenseType GetExpenseToAmountByName(string name) // internal
        {
            var expenseType = _expenseTypeService.GetItemByName(name);
            return expenseType;
        }
    }
}
