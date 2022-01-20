using HouseholdBudgetPlanner.App;
using HouseholdBudgetPlanner.App.Concrete;
using HouseholdBudgetPlanner.App.Managers;
using System;

namespace HouseholdBudgetPlanner
{
    public class Program
    {
        //const string FILE_NAME = "C:\\HouseholdBudgetPlannerFiles\\ImportFile.xlsx";
        static void Main(string[] args)
        {
            ExpenseTypeService expenseTypeService = new ExpenseTypeService();
            IncomeTypeService incomeTypeService = new IncomeTypeService();
            MenuActionService actionService = new MenuActionService();
            AmountService amountService = new AmountService();
            ExpenseTypeManager expenseTypeManager = new ExpenseTypeManager(expenseTypeService);
            IncomeTypeManager incomeTypeManager = new IncomeTypeManager(incomeTypeService);
            AmountManager amountManager = new AmountManager(actionService, amountService);
            AmountManagerAdd amountManagerAdd = new AmountManagerAdd(expenseTypeManager, incomeTypeManager, amountService);
            AmountManagerRemove amountManagerRemove = new AmountManagerRemove(amountService, amountManager);
            AmountManagerBudgetStatus amountManagerBudgetStatus = new AmountManagerBudgetStatus(actionService, amountService);
            BudgetStatusExpensesManager budgetStatusExpensesManager = new BudgetStatusExpensesManager(amountService, amountManagerBudgetStatus, amountManager);
            BudgetStatusIncomesManager budgetStatusIncomesManager = new BudgetStatusIncomesManager(amountService, amountManagerBudgetStatus, amountManager);
            BudgetStatusBalanceManager budgetStatusBalanceManager = new BudgetStatusBalanceManager(amountService, amountManagerBudgetStatus);
            BudgetStatusExecuteManager budgetStatusExecuteManager = new BudgetStatusExecuteManager(budgetStatusExpensesManager, budgetStatusIncomesManager, budgetStatusBalanceManager);

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
                        expenseTypeManager.ExpenseTypeView();
                        var expenseNameToAdd = expenseTypeManager.AddNewExpenseTypeView();
                        expenseTypeManager.AddNewExpanseType(expenseNameToAdd);
                        break;
                    case '2':
                        expenseTypeManager.ExpenseTypeView();
                        var expenseNameToRemove = expenseTypeManager.RemoveExpenseTypeView();
                        expenseTypeManager.RemoveExpanseType(expenseNameToRemove);
                        break;
                    case '3':
                        incomeTypeManager.IncomeTypeView();
                        var incomeNameToAdd = incomeTypeManager.AddNewIncomeTypeView();
                        incomeTypeManager.AddNewIncomeType(incomeNameToAdd);
                        break;
                    case '4':
                        incomeTypeManager.IncomeTypeView();
                        var incomeNameToRemove = incomeTypeManager.RemoveIncomeTypeView();
                        incomeTypeManager.RemoveIncomeType(incomeNameToRemove);
                        break;
                    case '5':
                        var keyInfoAddAmount = amountManager.AddAmountView();
                        amountManagerAdd.AddAmount(keyInfoAddAmount);
                        break;
                    case '6':
                        var keyInfoRemoveAmount = amountManager.RemoveAmountView();
                        amountManagerRemove.RemoveAmount(keyInfoRemoveAmount);
                        break;
                    case '7':
                        var keyInfoBudgetStatus = amountManagerBudgetStatus.BudgetStatusView();
                        budgetStatusExecuteManager.BudgetStatus(keyInfoBudgetStatus);
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
