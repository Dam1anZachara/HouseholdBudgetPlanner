using HouseholdBudgetPlanner.App.Abstract;
using HouseholdBudgetPlanner.App.Common;
using HouseholdBudgetPlanner.Domain.Entity;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace HouseholdBudgetPlanner.App.Concrete
{
    public class ExpenseTypeListService : BaseService<ExpenseType>
    {
        private IService<ExpenseType> _expenseTypeService;
        private List<ExpenseType> _expenseTypesGetList;
        string filePathExpenseTypes = (@"C:\Windows\Temp\ExpenseTypes.xml");
        XmlRootAttribute rootExpense = new XmlRootAttribute();
        public ExpenseTypeListService(IService<ExpenseType> expenseTypeService)
        {
            _expenseTypeService = expenseTypeService;
            rootExpense.ElementName = "ExpenseTypes";
            rootExpense.IsNullable = true;
            //_expenseTypesGetList = _expenseTypeService.GetAllItems();
            _expenseTypesGetList = ExpenseTypeReadFile();
        }
        public List<ExpenseType> ExpenseTypeReadFile()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ExpenseType>), rootExpense);
            if (File.Exists(filePathExpenseTypes))
            {
                string xmlExpenseString = File.ReadAllText(filePathExpenseTypes);
                StringReader stringReaderExpense = new StringReader(xmlExpenseString);
                var xmlExpenseTypes = (List<ExpenseType>)xmlSerializer.Deserialize(stringReaderExpense);
                //_expenseTypesGetList = (List<ExpenseType>)xmlSerializer.Deserialize(stringReaderExpense);
                return xmlExpenseTypes;
            }
            else
            {
                //_expenseTypesGetList = new List<ExpenseType>();
                _expenseTypeService.AddItem(new ExpenseType() { Id = 1, Name = "General expenses" });
                using StreamWriter swExpense = new StreamWriter(filePathExpenseTypes);
                xmlSerializer.Serialize(swExpense, _expenseTypesGetList);
                return _expenseTypesGetList;
            }
        }
        public void ExpenseTypeWriteFile(List<ExpenseType> expenseTypes)
        {
            
            //var expenseExist = _expenseTypesGetList.AsQueryable().Where(expenseType => expenseType.Name == expenseTypeSelect.Name).Any();
            //if (!expenseExist)
            //{
            //    _expenseTypeService.AddItem(expenseTypeSelect);
            //}
            //else
            //{
            //    var expenseType = _expenseTypesGetList.AsQueryable().Where(expenseType => expenseType.Name == expenseTypeSelect.Name).FirstOrDefault();
            //    _expenseTypesGetList.Remove(expenseType);
            //}
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ExpenseType>), rootExpense);
            using StreamWriter swExpense = new StreamWriter(filePathExpenseTypes);
            xmlSerializer.Serialize(swExpense, expenseTypes);
        }
    }
}
