using System;

namespace HouseholdBudgetPlanner
{
    public class Program
    {
        //const string FILE_NAME = "C:\\HouseholdBudgetPlannerFiles\\ImportFile.xlsx";
        static void Main(string[] args)
        {
            MenuActionService actionService = new MenuActionService();
            actionService = Initialize(actionService);
            ExpenseTypeService expenseTypeService = new ExpenseTypeService();
            IncomeTypeService incomeTypeService = new IncomeTypeService();
            AmountService amountService = new AmountService();
            AmountServiceAdd amountServiceAdd = new AmountServiceAdd();
            AmountServiceRemove amountServiceRemove = new AmountServiceRemove();
            AmountServiceBudgetStatus amountServiceBudgetStatus = new AmountServiceBudgetStatus();
            BudgetStatusExecute budgetStatusExecute = new BudgetStatusExecute();

            Console.WriteLine("Welcome to Household Budget Planner app!\r\n");
            while (true)
            {
                Console.WriteLine("Please let me know what you want to do:\r\n");

                var mainMenu = actionService.GetMenuActionsByMenuName("Main");
                for (int i = 0; i < mainMenu.Count; i++)
                {
                    Console.WriteLine($"{mainMenu[i].Id}. {mainMenu[i].Name}");
                }

                var operation = Console.ReadKey();

                switch (operation.KeyChar)
                {
                    case '1':
                        expenseTypeService.ExpenseTypeView();
                        var expenseNameToAdd = expenseTypeService.AddNewExpenseTypeView();
                        expenseTypeService.AddNewExpanseType(expenseNameToAdd);
                        break;
                    case '2':
                        expenseTypeService.ExpenseTypeView();
                        var expenseNameToRemove = expenseTypeService.RemoveExpenseTypeView();
                        expenseTypeService.RemoveExpanseType(expenseNameToRemove);
                        break;
                    case '3':
                        incomeTypeService.IncomeTypeView();
                        var incomeNameToAdd = incomeTypeService.AddNewIncomeTypeView();
                        incomeTypeService.AddNewIncomeType(incomeNameToAdd);
                        break;
                    case '4':
                        incomeTypeService.IncomeTypeView();
                        var incomeNameToRemove = incomeTypeService.RemoveIncomeTypeView();
                        incomeTypeService.RemoveIncomeType(incomeNameToRemove);
                        break;
                    case '5':
                        var keyInfoAddAmount = amountService.AddAmountView(actionService);
                        amountServiceAdd.AddAmount(keyInfoAddAmount);
                        break;
                    case '6':
                        var keyInfoRemoveAmount = amountService.RemoveAmountView(actionService);
                        amountServiceRemove.RemoveAmount(keyInfoRemoveAmount);
                        break;
                    case '7':
                        var keyInfoBudgetStatus = amountServiceBudgetStatus.BudgetStatusView(actionService);
                        budgetStatusExecute.BudgetStatus(keyInfoBudgetStatus);
                        break;
                    default:
                        Console.WriteLine("Action you entered does not exist");
                        break;
                }
            }
        }
        private static MenuActionService Initialize(MenuActionService actionService)
        {
            actionService.AddNewAction(1, "Add type of expense", "Main");
            actionService.AddNewAction(2, "Remove type of expense", "Main");
            actionService.AddNewAction(3, "Add type of income", "Main");
            actionService.AddNewAction(4, "Remove type of income", "Main");
            actionService.AddNewAction(5, "Add amount", "Main");
            actionService.AddNewAction(6, "Remove amount", "Main");
            actionService.AddNewAction(7, "Budget status", "Main");

            actionService.AddNewAction(1, "Set amount as expense", "AddAmountMenu");
            actionService.AddNewAction(2, "Set amount as income", "AddAmountMenu");

            actionService.AddNewAction(1, "Remove amount from expense", "RemoveAmountMenu");
            actionService.AddNewAction(2, "Remove amount from income", "RemoveAmountMenu");

            actionService.AddNewAction(1, "Status of expenses", "BudgetStatusMenu");
            actionService.AddNewAction(2, "Status of incomes", "BudgetStatusMenu");
            actionService.AddNewAction(3, "Budget balance", "BudgetStatusMenu");
            return actionService;
        }
    }
}
