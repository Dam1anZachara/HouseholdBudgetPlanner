using HouseholdBudgetPlanner.Domain.Common;
using System;

namespace HouseholdBudgetPlanner.Domain.Entity
{
    public class Amount : BaseEntity
    {
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
    }
}
