using System.Collections.Generic;
using System;
using System.Linq;

namespace HouseholdBudgetPlanner
{
    public class ExpenseTypeService
    {
        public static List<ExpenseType> ExpenseTypes { get; set; }

        private int i;
        public ExpenseTypeService()
        {
            ExpenseTypes = new List<ExpenseType>();
            ExpenseType expenseType = new ExpenseType() { Id = 1, Name = "General expenses" };
            ExpenseTypes.Add(expenseType);
        }

        public void ExpenseTypeView()
        {
            Console.WriteLine("\r\nYour all expense types are:\r\n");
            for (i = 0; i < ExpenseTypes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {ExpenseTypes[i].Name}");
            }
        }
        public string AddNewExpenseTypeView()
        {
            Console.Write("\r\nPlease write the name of new expense type (don't use space or existing name) and press \"enter\": ");
            string name = Console.ReadLine();
            return name;
        }
        private bool ExpanseTypeExist(string name)
        {
            foreach (var expenseType in ExpenseTypes)
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
                ExpenseTypes.Add(expenseTypeToAdd);
                Console.WriteLine($"\r\nExpense type {name} has been added.");
            }
            else
            {
                Console.WriteLine("\r\nExpense type with this name exist or your name is empty or contains space.\r\n");
            }
        }
        public string RemoveExpenseTypeView()
        {
            Console.Write("\r\nPlease write the name of expense type that you want to remove and press \"enter\": ");
            string name = Console.ReadLine();
            return name;
        }
        public void RemoveExpanseType(string name)
        {
            bool expanseTypeExist = ExpanseTypeExist(name);
            if (expanseTypeExist)
            {
                foreach (var expenseType in ExpenseTypes)
                {
                    if (expenseType.Name == name)
                    {
                        ExpenseTypes.Remove(expenseType);
                        Console.WriteLine($"\r\nExpense type {name} has been removed.\r\n");
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("\r\nExpense type with this name does not exist. General expenses can not been removed!\r\n");
            }
        }
        public ExpenseType GetExpenseToAmountByName(string name)
        {
            foreach (var expenseType in ExpenseTypes)
            {
                if (expenseType.Name == name)
                {
                    return expenseType;
                }
            }
            var expenseTypeFirst = ExpenseTypes.First<ExpenseType>();
            return expenseTypeFirst;
        }
    }
}
