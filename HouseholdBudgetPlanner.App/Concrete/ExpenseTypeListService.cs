using HouseholdBudgetPlanner.App.Abstract;
using HouseholdBudgetPlanner.App.Common;
using HouseholdBudgetPlanner.App.FileSupport;
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
        private FilePath _filePath;
        private string _filePathExpenseTypes;
        XmlRootAttribute rootExpense = new XmlRootAttribute();
        XmlSerializer xmlSerializer;
        public ExpenseTypeListService(IService<ExpenseType> expenseTypeService, FilePath filePath)
        {
            _expenseTypeService = expenseTypeService;
            _filePath = filePath;
            _filePathExpenseTypes = _filePath.FilePathExpenseTypes();
            rootExpense.ElementName = "ExpenseTypes";
            rootExpense.IsNullable = true;
            ExpenseTypeReadFile();
            _expenseTypesGetList = _expenseTypeService.GetAllItems();
            xmlSerializer = new XmlSerializer(typeof(List<ExpenseType>), rootExpense);
        }

        public void ExpenseTypeReadFile()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ExpenseType>), rootExpense);
            if (File.Exists(_filePathExpenseTypes))
            {

                string xmlExpenseString = File.ReadAllText(_filePathExpenseTypes);
                StringReader stringReaderExpense = new StringReader(xmlExpenseString);
                _expenseTypeService.Items = (List<ExpenseType>)xmlSerializer.Deserialize(stringReaderExpense);
            }
            else
            {
                _expenseTypeService.AddItem(new ExpenseType() { Id = 1, Name = "General expenses" });
                using StreamWriter swExpense = new StreamWriter(_filePathExpenseTypes);
                xmlSerializer.Serialize(swExpense, _expenseTypesGetList);
            }
        }
        public void ExpenseTypeWriteFile()
        {
            using StreamWriter swExpense = new StreamWriter(_filePathExpenseTypes);
            xmlSerializer.Serialize(swExpense, _expenseTypeService.Items);
        }
    }
}
