using HouseholdBudgetPlanner.Domain.Entity;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace HouseholdBudgetPlanner.App.Concrete
{
    public class AmountListService
    {
        List<Amount> amounts;
        string filePathAmounts = (@"C:\Windows\Temp\Amounts.xml");
        XmlRootAttribute rootAmounts = new XmlRootAttribute();
        public AmountListService()
        {
            rootAmounts.ElementName = "Amounts";
            rootAmounts.IsNullable = true;
        }
        public List<Amount> AmountReadFile()
        {
            XmlSerializer xmlSerializerAmount = new XmlSerializer(typeof(List<Amount>), rootAmounts);  
            if (File.Exists(filePathAmounts))
            {
                string xmlAmountsString = File.ReadAllText(filePathAmounts);
                StringReader stringReaderAmounts = new StringReader(xmlAmountsString);
                var xmlAmounts = (List<Amount>)xmlSerializerAmount.Deserialize(stringReaderAmounts);
                return xmlAmounts;
            }
            return null;
        }
        public void AmountWriteFile(Amount amountSelect)
        {
            amounts = AmountReadFile();
            var amountExist = amounts.AsQueryable().Where(amount => amount.Name == amountSelect.Name).Any();
            if (!amountExist)
            {
                amounts.Add(amountSelect);
            }
            else
            {
                var amount = amounts.AsQueryable().Where(amount => amount.Name == amountSelect.Name).FirstOrDefault();
                amounts.Remove(amount);
            }
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Amount>), rootAmounts);
            using StreamWriter swAmounts = new StreamWriter(filePathAmounts);
            xmlSerializer.Serialize(swAmounts, amounts);
        }
    }
}
