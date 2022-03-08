using System;
using System.Xml.Serialization;

namespace HouseholdBudgetPlanner.Domain.Common
{
    public class BaseEntity : AuditableModel
    {
        [XmlAttribute("Id")]
        public int Id { get; set; }
        [XmlElement("Name")]
        public string Name { get; set; }
    }
}
