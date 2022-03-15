using HouseholdBudgetPlanner.App.Abstract;
using HouseholdBudgetPlanner.App.Common;
using HouseholdBudgetPlanner.Domain.Entity;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace HouseholdBudgetPlanner.App.Concrete
{
    public class ExpenseTypeListService : BaseService<ExpenseType>
    {
        private IService<ExpenseType> _expenseTypeService;
        private List<ExpenseType> _expenseTypesGetList;
        string filePathExpenseTypes = (@"C:\Users\DZachara\Desktop\ExpenseTypes.xml");
        XmlRootAttribute rootExpense = new XmlRootAttribute();
        XmlSerializer xmlSerializer;
        public ExpenseTypeListService(IService<ExpenseType> expenseTypeService)
        {
            _expenseTypeService = expenseTypeService;
            rootExpense.ElementName = "ExpenseTypes";
            rootExpense.IsNullable = true;
            ExpenseTypeReadFile();
            _expenseTypesGetList = _expenseTypeService.GetAllItems();
            xmlSerializer = new XmlSerializer(typeof(List<ExpenseType>), rootExpense);
        }

        public void ExpenseTypeReadFile()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ExpenseType>), rootExpense);
            if (File.Exists(filePathExpenseTypes))
            {

                string xmlExpenseString = File.ReadAllText(filePathExpenseTypes);
                StringReader stringReaderExpense = new StringReader(xmlExpenseString);
                _expenseTypeService.Items = (List<ExpenseType>)xmlSerializer.Deserialize(stringReaderExpense);
            }
            else
            {
                _expenseTypeService.AddItem(new ExpenseType() { Id = 1, Name = "General expenses" });
                using StreamWriter swExpense = new StreamWriter(filePathExpenseTypes);
                xmlSerializer.Serialize(swExpense, _expenseTypesGetList);
            }
        }
        public void ExpenseTypeWriteFile()
        {
            using StreamWriter swExpense = new StreamWriter(filePathExpenseTypes);
            xmlSerializer.Serialize(swExpense, _expenseTypeService.Items);
        }
    }
}
