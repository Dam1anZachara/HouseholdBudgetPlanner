using HouseholdBudgetPlanner.Domain.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HouseholdBudgetPlanner.App.Concrete
{
    public class ListService
    {
        public List<ExpenseType> ExpenseTypeReadFile()
        {
            List<ExpenseType> expenseTypes = new List<ExpenseType>();
            string filePath = (@"C:\Windows\Temp\ExpenseTypes.xml");
            XmlRootAttribute root = new XmlRootAttribute();
            root.ElementName = "ExpenseTypes";
            root.IsNullable = true;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ExpenseType>), root);
            if (File.Exists(filePath))
            {
                string xml = File.ReadAllText(filePath);
                

                StringReader stringReader = new StringReader(xml);
                var xmlExpense = (List<ExpenseType>)xmlSerializer.Deserialize(stringReader);
                return xmlExpense;

            }
            else
            {
                expenseTypes.Add(new ExpenseType() { Id = 1, Name = "General expenses" });
                using StreamWriter sw = new StreamWriter(filePath);
                xmlSerializer.Serialize(sw, expenseTypes);
                return expenseTypes;
            }
        }
        //private void ExpenseTypeReadFile()
        //{
        //    _expenseTypesFileList = File.ReadAllLines(filePath).ToList();
        //    foreach (var expenseType in _expenseTypesFileList)
        //    {
        //        string[] expenseText = expenseType.Split(',');
        //        ExpenseType expenseTypeFile = new ExpenseType();
        //        expenseTypeFile.Id = int.Parse(expenseText[0]);
        //        expenseTypeFile.Name = expenseText[1];
        //        _expenseTypeService.AddItem(expenseTypeFile);
        //    }
        //    if (!_expenseTypesFileList.AsQueryable().Where(expenseType => expenseType == "1,General expenses").Any())
        //    {
        //        ExpenseType expenseTypeGeneral = new ExpenseType() { Id = 1, Name = "General expenses" };
        //        _expenseTypeService.AddItem(expenseTypeGeneral);
        //        ExpenseTypeWriteFile(expenseTypeGeneral);
        //    }
        //}
    }
}
