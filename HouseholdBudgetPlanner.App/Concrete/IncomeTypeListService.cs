using HouseholdBudgetPlanner.Domain.Entity;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace HouseholdBudgetPlanner.App.Concrete
{
    public class IncomeTypeListService
    {
        List<IncomeType> incomeTypes;
        string filePathIncomeTypes = (@"C:\Windows\Temp\IncomeTypes.xml");
        XmlRootAttribute rootIncome = new XmlRootAttribute();
        public IncomeTypeListService()
        {
            rootIncome.ElementName = "IncomeTypes";
            rootIncome.IsNullable = true;
        }
        public List<IncomeType> IncomeTypeReadFile()
        {
            
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<IncomeType>), rootIncome);
            if (File.Exists(filePathIncomeTypes))
            {
                string xmlIncomeString = File.ReadAllText(filePathIncomeTypes);
                StringReader stringReaderIncome = new StringReader(xmlIncomeString);
                var xmlIncomeTypes = (List<IncomeType>)xmlSerializer.Deserialize(stringReaderIncome);
                return xmlIncomeTypes;
            }
            else
            {
                incomeTypes = new List<IncomeType>();
                incomeTypes.Add(new IncomeType() { Id = -1, Name = "General incomes" });
                using StreamWriter swIncome = new StreamWriter(filePathIncomeTypes);
                xmlSerializer.Serialize(swIncome, incomeTypes);
                return incomeTypes;
            }
        }
        public void IncomeTypeWriteFile(IncomeType incomeTypeSelect)
        {
            incomeTypes = IncomeTypeReadFile();
            var incomeExist = incomeTypes.AsQueryable().Where(incomeType => incomeType.Name == incomeTypeSelect.Name).Any();
            if (!incomeExist)
            {
                incomeTypes.Add(incomeTypeSelect);
            }
            else
            {
                var incomeType = incomeTypes.AsQueryable().Where(incomeType => incomeType.Name == incomeTypeSelect.Name).FirstOrDefault();
                incomeTypes.Remove(incomeType);
            }
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<IncomeType>), rootIncome);
            using StreamWriter swIncome = new StreamWriter(filePathIncomeTypes);
            xmlSerializer.Serialize(swIncome, incomeTypes);
        }
    }
}
