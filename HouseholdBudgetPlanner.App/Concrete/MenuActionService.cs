using HouseholdBudgetPlanner.App.Common;
using HouseholdBudgetPlanner.Domain.Entity;
using System.Collections.Generic;

namespace HouseholdBudgetPlanner.App.Concrete
{
    public class MenuActionService : BaseService<MenuAction>
    {
        public MenuActionService()
        {
            Initialize();
        }
        public List<MenuAction> GetMenuActionsByMenuName(string menuName)
        {
            List<MenuAction> result = new List<MenuAction>();
            foreach (var menuAction in Items)
            {
                if (menuAction.MenuName == menuName)
                {
                    result.Add(menuAction);
                }
            }
            return result;
        }
        private void Initialize()
        {
            AddItem(new MenuAction (1, "Add type of an expense", "Main"));
            AddItem(new MenuAction (2, "Remove type of an expense", "Main"));
            AddItem(new MenuAction (3, "Add type of an income", "Main"));
            AddItem(new MenuAction (4, "Remove type of an income", "Main"));
            AddItem(new MenuAction (5, "Add amount", "Main"));
            AddItem(new MenuAction (6, "Remove amount", "Main"));
            AddItem(new MenuAction (7, "Budget status", "Main"));
            AddItem(new MenuAction (8, "Exit app", "Main"));

            AddItem(new MenuAction (1, "Assign as expense", "AddAmountMenu"));
            AddItem(new MenuAction (2, "Assign as income", "AddAmountMenu"));

            AddItem(new MenuAction (1, "Remove amount from expenses", "RemoveAmountMenu"));
            AddItem(new MenuAction (2, "Remove amount from incomes", "RemoveAmountMenu"));

            AddItem(new MenuAction (1, "Status of expenses", "BudgetStatusMenu"));
            AddItem(new MenuAction (2, "Status of incomes", "BudgetStatusMenu"));
            AddItem(new MenuAction (3, "Budget balance", "BudgetStatusMenu"));
        }
    }
}
