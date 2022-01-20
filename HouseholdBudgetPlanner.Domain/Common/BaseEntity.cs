using System;

namespace HouseholdBudgetPlanner.Domain.Common
{
    public class BaseEntity : AuditableModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
