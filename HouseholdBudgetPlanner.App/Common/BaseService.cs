using HouseholdBudgetPlanner.App.Abstract;
using HouseholdBudgetPlanner.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseholdBudgetPlanner.App.Common
{
    public class BaseService<T> : IService<T> where T : BaseEntity
    {
        public BaseService()
        {
            Items = new List<T>();
        }
        public List<T> Items { get; set; }

        public int AddItem(T item)
        {
            Items.Add(item);
            return item.Id;
        }

        public List<T> GetAllItems()
        {
            return Items;
        }

        public void RemoveItem(T item)
        {
            Items.Remove(item);
        }

        public int UpdateItem(T item)
        {
            var entity = Items.FirstOrDefault(p => p.Id == item.Id);
            if (entity != null)
            {
                entity = item;
            }
            return entity.Id;
        }
    }
}
