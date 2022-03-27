using HouseholdBudgetPlanner.App;
using HouseholdBudgetPlanner.App.Concrete;
using HouseholdBudgetPlanner.App.FileSupport;
using HouseholdBudgetPlanner.App.Managers;
using System;

namespace HouseholdBudgetPlanner
{
    public class Program
    {
        static void Main(string[] args)
        {
            FilePath filePath = new FilePath();
            ExpenseTypeService expenseTypeService = new ExpenseTypeService();
            IncomeTypeService incomeTypeService = new IncomeTypeService();
            MenuActionService actionService = new MenuActionService();
            AmountService amountService = new AmountService();
            ExpenseTypeFileService expenseTypeFileService = new ExpenseTypeFileService(expenseTypeService, filePath);
            ExpenseTypeManager expenseTypeManager = new ExpenseTypeManager(expenseTypeService, expenseTypeFileService);
            IncomeTypeFileService incomeTypeFileService = new IncomeTypeFileService(incomeTypeService, filePath);
            IncomeTypeManager incomeTypeManager = new IncomeTypeManager(incomeTypeService, incomeTypeFileService);
            AmountFileService amountFileService = new AmountFileService(amountService, filePath);
            AmountManager amountManager = new AmountManager(actionService, amountService);
            AmountManagerAddExpense amountManagerAddExpense = new AmountManagerAddExpense(expenseTypeManager);
            AmountManagerAddIncome amountManagerAddIncome = new AmountManagerAddIncome(incomeTypeManager);
            AmountManagerAdd amountManagerAdd = new AmountManagerAdd(amountService, amountManagerAddExpense, amountManagerAddIncome, amountFileService);
            AmountManagerRemoveExpense amountManagerRemoveExpense = new AmountManagerRemoveExpense(amountService, amountManager, amountFileService);
            AmountManagerRemoveIncome amountManagerRemoveIncome = new AmountManagerRemoveIncome(amountService, amountManager, amountFileService);
            AmountManagerRemove amountManagerRemove = new AmountManagerRemove(amountManagerRemoveExpense, amountManagerRemoveIncome);
            AmountManagerBudgetStatus amountManagerBudgetStatus = new AmountManagerBudgetStatus(actionService, amountService);
            AmountRaportFileService amountRaportFileService = new AmountRaportFileService(amountManagerBudgetStatus, filePath);
            BudgetStatusRaportManager budgetStatusRaportManager = new BudgetStatusRaportManager(amountService, amountRaportFileService);
            BudgetStatusExpensesManagerMonth budgetStatusExpensesManagerMonth = new BudgetStatusExpensesManagerMonth(amountService, amountManagerBudgetStatus);
            BudgetStatusExpensesManagerRange budgetStatusExpensesManagerRange = new BudgetStatusExpensesManagerRange(amountService, amountManagerBudgetStatus, amountManager);
            BudgetStatusExpensesManager budgetStatusExpensesManager = new BudgetStatusExpensesManager(budgetStatusExpensesManagerMonth, budgetStatusExpensesManagerRange);
            BudgetStatusIncomesManagerMonth budgetStatusIncomesManagerMonth = new BudgetStatusIncomesManagerMonth(amountService, amountManagerBudgetStatus);
            BudgetStatusIncomesManagerRange budgetStatusIncomesManagerRange = new BudgetStatusIncomesManagerRange(amountService, amountManagerBudgetStatus, amountManager);
            BudgetStatusIncomesManager budgetStatusIncomesManager = new BudgetStatusIncomesManager(budgetStatusIncomesManagerMonth, budgetStatusIncomesManagerRange);
            BudgetStatusBalanceManager budgetStatusBalanceManager = new BudgetStatusBalanceManager(amountManagerBudgetStatus);
            BudgetStatusExecuteManager budgetStatusExecuteManager = new BudgetStatusExecuteManager(budgetStatusExpensesManager, budgetStatusIncomesManager, budgetStatusBalanceManager, budgetStatusRaportManager);

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
