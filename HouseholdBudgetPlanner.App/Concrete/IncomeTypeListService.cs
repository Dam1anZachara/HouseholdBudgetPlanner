using HouseholdBudgetPlanner.App.Abstract;
using HouseholdBudgetPlanner.App.Common;
using HouseholdBudgetPlanner.Domain.Entity;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace HouseholdBudgetPlanner.App.Concrete
{
    public class IncomeTypeListService : BaseService<IncomeType>
    {
        private IService<IncomeType> _incomeTypeService;
        private List<IncomeType> _incomeTypesGetList;
        string filePathIncomeTypes = (@"C:\Users\DZachara\Desktop\IncomeTypes.xml");
        XmlRootAttribute rootIncome = new XmlRootAttribute();
        XmlSerializer xmlSerializer;
        public IncomeTypeListService(IService<IncomeType> incomeTypeService)
        {
            _incomeTypeService = incomeTypeService;
            rootIncome.ElementName = "IncomeTypes";
            rootIncome.IsNullable = true;
            IncomeTypeReadFile();
            _incomeTypesGetList = _incomeTypeService.GetAllItems();
            xmlSerializer = new XmlSerializer(typeof(List<IncomeType>), rootIncome);
        }

        public void IncomeTypeReadFile()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<IncomeType>), rootIncome);
            if (File.Exists(filePathIncomeTypes))
            {

                string xmlIncomeString = File.ReadAllText(filePathIncomeTypes);
                StringReader stringReaderIncome = new StringReader(xmlIncomeString);
                _incomeTypeService.Items = (List<IncomeType>)xmlSerializer.Deserialize(stringReaderIncome);
            }
            else
            {
                _incomeTypeService.AddItem(new IncomeType() { Id = -1, Name = "General incomes" });
                using StreamWriter swIncome = new StreamWriter(filePathIncomeTypes);
                xmlSerializer.Serialize(swIncome, _incomeTypesGetList);
            }
        }
        public void IncomeTypeWriteFile()
        {
            using StreamWriter swIncome = new StreamWriter(filePathIncomeTypes);
            xmlSerializer.Serialize(swIncome, _incomeTypeService.Items);
        }
    }
}
