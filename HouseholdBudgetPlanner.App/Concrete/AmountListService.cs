using HouseholdBudgetPlanner.App.Abstract;
using HouseholdBudgetPlanner.App.Common;
using HouseholdBudgetPlanner.Domain.Entity;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace HouseholdBudgetPlanner.App.Concrete
{
    public class AmountListService : BaseService<Amount>
    {
        private IService<Amount> _amountService;
        private List<Amount> _amountsGetList;
        string filePathAmounts = (@"C:\Users\Damian\Desktop\Amounts.xml");
        XmlRootAttribute rootAmounts = new XmlRootAttribute();
        XmlSerializer xmlSerializer;
        public AmountListService(IService<Amount> amountService)
        {
            _amountService = amountService;
            rootAmounts.ElementName = "Amounts";
            rootAmounts.IsNullable = true;
            AmountReadFile();
            _amountsGetList = _amountService.GetAllItems();
            xmlSerializer = new XmlSerializer(typeof(List<Amount>), rootAmounts);
        }

        public void AmountReadFile()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Amount>), rootAmounts);
            if (File.Exists(filePathAmounts))
            {
                string xmlAmountString = File.ReadAllText(filePathAmounts);
                StringReader stringReaderAmount = new StringReader(xmlAmountString);
                _amountService.Items = (List<Amount>)xmlSerializer.Deserialize(stringReaderAmount);
            }
            else
            {
                using StreamWriter swAmount = new StreamWriter(filePathAmounts);
                xmlSerializer.Serialize(swAmount, _amountsGetList);
            }
        }
        public void AmountWriteFile()
        {
            using StreamWriter swAmount = new StreamWriter(filePathAmounts);
            xmlSerializer.Serialize(swAmount, _amountService.Items);
        }
    }
}
