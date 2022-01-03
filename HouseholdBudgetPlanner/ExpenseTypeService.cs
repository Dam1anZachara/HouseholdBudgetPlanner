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
                Console.WriteLine($"{i+1}. {ExpenseTypes[i].Name}");
            }
        }
        public string AddNewExpenseTypeView()
        {
            Console.Write("\r\nPlease write the name of new expense type and press \"enter\": ");
            string name = Console.ReadLine();
            return name;
        }
        public void AddNewExpanseType(string name)
        {
            if (name != "")
            {
                ExpenseType expenseTypeToAdd = new ExpenseType() { Id = i + 1, Name = name };
                ExpenseTypes.Add(expenseTypeToAdd);
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
            ExpenseType expenseTypeToRemove = new ExpenseType();
            foreach (var expenseType in ExpenseTypes)
            {
                if (expenseType.Name == name)
                {
                    expenseTypeToRemove = expenseType;
                    break;
                }
            }
            ExpenseTypes.Remove(expenseTypeToRemove);
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
