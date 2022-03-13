using HouseholdBudgetPlanner.Domain.Common;
using System;
using System.Xml.Serialization;

namespace HouseholdBudgetPlanner.Domain.Entity
{
    public class Amount : BaseEntity
    {
        [XmlElement("Value")]
        public decimal Value { get; set; }
        [XmlElement("Date")]
        public DateTime Date { get; set; }
    }
}
