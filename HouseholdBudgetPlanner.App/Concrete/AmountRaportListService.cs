using HouseholdBudgetPlanner.Domain.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseholdBudgetPlanner.App.Concrete
{
    public class AmountRaportListService
    {
        private List<Amount> _getListOfExpenses;
        public AmountRaportListService()
        {
            _getListOfExpenses = new List<Amount>();
        }
        string pathFileRaport = $@"C:\Users\Damian\Desktop\{DateTime.Now.ToString("G")}BudgetBalanceRaport.csv";
        public void Method1(List<Amount> getListOfExpenses, List<Amount> getListOfIncomes)
        {
            //using FileStream fs = File.Open(pathFileRaport, FileMode.Open, FileAccess.Read);
            //byte[] buf = new byte[1024];
            //int c;
            //while ((c = fs.Read(buf, 0, buf.Length)) > 0)
            //{
            //    string text = Encoding.UTF8.GetString(buf, 0, c);
            //}
            //var lines = File.ReadLines(pathFileRaport);
            using FileStream fs = File.Create(pathFileRaport);
            using StreamWriter sw = new StreamWriter(fs);

            //sw.Write
        }
    }
}
