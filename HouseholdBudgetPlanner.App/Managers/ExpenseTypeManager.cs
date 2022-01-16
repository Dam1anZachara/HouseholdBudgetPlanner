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
    public class ExpenseTypeManager : ExpenseTypeService
    {
        
        private IService<ExpenseType> _expenseType;
        //private readonly List<ExpenseType> ExpenseTypes;
        private int i;

        //public List<ExpenseType> ExpenseTypes { get; set; }

        public ExpenseTypeManager()
        {
            //_expenseType = expenseType;
            //ExpenseTypes = new List<ExpenseType>();
            ExpenseType expenseType = new ExpenseType() { Id = 1, Name = "General expenses" };
            //_expenseType.AddItem(expenseType);
            Items.Add(expenseType);
        }

        //public ExpenseTypeManager()
        //{
        //    //ExpenseTypes = _expenseType.GetAllItems();
        //}
        public void ExpenseTypeView()
        {
            //var expenseTypes = _expenseType.GetAllItems();
            Console.WriteLine("\r\nAll your expense types are:\r\n");
            for (i = 0; i < Items.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Items[i].Name}");
            }
        }
        public string AddNewExpenseTypeView()
        {
            Console.Write("\r\nPlease write the name of the new expense type (don't use space or existing name) and press \"enter\": ");
            string name = Console.ReadLine();
            return name;
        }
        private bool ExpanseTypeExist(string name)
        {
            foreach (var expenseType in Items)
            {
                if (expenseType.Name == name && expenseType.Name != "General expenses")
                {
                    return true;
                    break;
                }
            }
            return false;
        }
        public void AddNewExpanseType(string name)
        {
            bool expanseTypeExist = ExpanseTypeExist(name);
            if (name != "" && !expanseTypeExist && !name.Contains(' '))
            {
                ExpenseType expenseTypeToAdd = new ExpenseType() { Id = i + 1, Name = name };
                Items.Add(expenseTypeToAdd);
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
                foreach (var expenseType in Items)
                {
                    if (expenseType.Name == name)
                    {
                        Items.Remove(expenseType);
                        Console.WriteLine($"\r\nExpense type {name} has been removed.\r\n");
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("\r\nExpense type with this name does not exist. General expenses can not be removed!\r\n");
            }
        }
        public ExpenseType GetExpenseToAmountByName(string name)
        {
            foreach (var expenseType in Items)
            {
                if (expenseType.Name == name)
                {
                    return expenseType;
                }
            }
            var expenseTypeFirst = Items.First<ExpenseType>();
            return expenseTypeFirst;
        }
    }
}
