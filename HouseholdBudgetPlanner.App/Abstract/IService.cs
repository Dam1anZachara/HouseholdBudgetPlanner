using System.Collections.Generic;

namespace HouseholdBudgetPlanner.App.Abstract
{
    public interface IService<T>
    {
        List<T> Items { get; set; }

        List <T> GetAllItems();
        void AddItem(T item);
        //int UpdateItem(T item);
        void RemoveItem(T item);
    }
}
