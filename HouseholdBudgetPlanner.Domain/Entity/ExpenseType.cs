using HouseholdBudgetPlanner.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseholdBudgetPlanner.Domain.Entity
{
    public class ExpenseType : BaseEntity
    {
        public string Name { get; set; }
        public ExpenseType()
        {
        }
        public ExpenseType(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
