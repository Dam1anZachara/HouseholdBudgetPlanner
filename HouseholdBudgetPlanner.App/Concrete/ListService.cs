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
        List<ExpenseType> expenseTypes = new List<ExpenseType>();
        string filePathExpenseTypes = (@"C:\Windows\Temp\ExpenseTypes.xml");
        XmlRootAttribute rootExpense = new XmlRootAttribute();
        public ListService()
        {
            rootExpense.ElementName = "ExpenseTypes";
            rootExpense.IsNullable = true;
        }
        public List<ExpenseType> ExpenseTypeReadFile()
        {
            
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ExpenseType>), rootExpense);
            if (File.Exists(filePathExpenseTypes))
            {
                string xmlExpenseString = File.ReadAllText(filePathExpenseTypes);
                StringReader stringReaderExpense = new StringReader(xmlExpenseString);
                var xmlExpenseTypes = (List<ExpenseType>)xmlSerializer.Deserialize(stringReaderExpense);
                return xmlExpenseTypes;
            }
            else
            {
                expenseTypes.Add(new ExpenseType() { Id = 1, Name = "General expenses" });
                using StreamWriter swExpense = new StreamWriter(filePathExpenseTypes);
                xmlSerializer.Serialize(swExpense, expenseTypes);
                return expenseTypes;
            }
        }
        public void ExpenseTypeWriteFile(List<ExpenseType> expenseTypes)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ExpenseType>), rootExpense);
            using StreamWriter swExpense = new StreamWriter(filePathExpenseTypes);
            xmlSerializer.Serialize(swExpense, expenseTypes);
        }
    }
}
