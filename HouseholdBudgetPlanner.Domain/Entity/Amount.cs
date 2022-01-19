using HouseholdBudgetPlanner.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseholdBudgetPlanner.Domain.Entity
{
    public class Amount : BaseEntity
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        //public Amount(int id, string name, decimal value, DateTime date)
        //{
        //    Id = id;
        //    Name = name;
        //    Value = value;
        //    Date = date;
        //}
    }
}
