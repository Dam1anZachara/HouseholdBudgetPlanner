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

            Console.WriteLine("Welcome to the Household Budget Planner app!\r\n");
            bool appWork = true;
            while (appWork)
            {
                Console.WriteLine("Please let me know what do you want to do:\r\n");

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
                    case '8':
                        appWork = ExitApp(operation);
                        break;
                    default:
                        Console.WriteLine("Action you entered does not exist");
                        break;
                }
            }
        }
        private static MenuActionService Initialize(MenuActionService actionService)
        {
            actionService.AddNewAction(1, "Add type of an expense", "Main");
            actionService.AddNewAction(2, "Remove type of an expense", "Main");
            actionService.AddNewAction(3, "Add type of an income", "Main");
            actionService.AddNewAction(4, "Remove type of an income", "Main");
            actionService.AddNewAction(5, "Add amount", "Main");
            actionService.AddNewAction(6, "Remove amount", "Main");
            actionService.AddNewAction(7, "Budget status", "Main");
            actionService.AddNewAction(8, "Exit app", "Main");

            actionService.AddNewAction(1, "Assign as expense", "AddAmountMenu");
            actionService.AddNewAction(2, "Assign as income", "AddAmountMenu");

            actionService.AddNewAction(1, "Remove amount from expenses", "RemoveAmountMenu");
            actionService.AddNewAction(2, "Remove amount from incomes", "RemoveAmountMenu");

            actionService.AddNewAction(1, "Status of expenses", "BudgetStatusMenu");
            actionService.AddNewAction(2, "Status of incomes", "BudgetStatusMenu");
            actionService.AddNewAction(3, "Budget balance", "BudgetStatusMenu");
            return actionService;
        }
        private static bool ExitApp(ConsoleKeyInfo keyInfo)
        {
            if (keyInfo.KeyChar == '8')
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
