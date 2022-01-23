using HouseholdBudgetPlanner.App.Abstract;
using HouseholdBudgetPlanner.Domain.Common;
using System.Collections.Generic;
using System.Linq;

namespace HouseholdBudgetPlanner.App.Common
{
    public class BaseService<T> : IService<T> where T : BaseEntity
    {
        public BaseService()
        {
            Items = new List<T>();
        }
        public List<T> Items { get; set; }

        public void AddItem(T item)
        {
            Items.Add(item);
            //return item.Id;
        }

        public List<T> GetAllItems()
        {
            return Items;
        }

        public T GetItemByName(string name)
        {
            foreach (var entity in Items)
            {
                if (entity.Name == name)
                {
                    return entity;
                }
            }
            var entityFirst = Items.First<T>();
            return entityFirst;
        }

        public void RemoveItem(T item)
        {
            Items.Remove(item);
        }

        //public int UpdateItem(T item)
        //{
        //    var entity = Items.FirstOrDefault(p => p.Id == item.Id);
        //    if (entity != null)
        //    {
        //        entity = item;
        //    }
        //    return entity.Id;
        //}
    }
}
