using FluentAssertions;
using HouseholdBudgetPlanner.App;
using HouseholdBudgetPlanner.App.Concrete;
using HouseholdBudgetPlanner.App.Managers;
using HouseholdBudgetPlanner.Domain.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HouseholdBudgetPlanner.Tests
{
    public class AmountManagerRemoveExpenseTest
    {
        [Fact]
        public void ExpenseInAmountByDateListTest()
        {
            //Arrange
            MenuActionService menuActionService = new MenuActionService();
            AmountService amountService = new AmountService();
            AmountManager amountManager = new AmountManager(menuActionService, amountService);
            AmountManagerRemoveExpense amountManagerRemoveExpense = new AmountManagerRemoveExpense(amountService, amountManager);
            Amount amountExpenseOne = new Amount() { Id = 1, Name = "General expenses", Date = new DateTime(2022, 02, 2), Value = 55.00m };
            Amount amountExpenseTwo = new Amount() { Id = 1, Name = "General expenses", Date = new DateTime(2022, 02, 3), Value = 45.00m };
            amountService.AddItem(amountExpenseOne);
            amountService.AddItem(amountExpenseTwo);
            var dateStart = new DateTime(2022, 02, 1);
            var dateEnd = new DateTime(2022, 02, 4);
            var expectedOutputPattern = $"\r\nYour expenses since {dateStart} to {dateEnd}\r\n" + "2022-02-02 00:00:00; Name: General expenses; Value: 55.00PLN\r\n" + "2022-02-03 00:00:00; Name: General expenses; Value: 45.00PLN";
            var testOutput = new StringWriter();
            TextWriter textWriter;
            textWriter = Console.Out;
            Console.SetOut(testOutput);
            var string1 = testOutput.ToString();
            //Act
            amountManagerRemoveExpense.ExpenseInAmountByDateList(dateStart, dateEnd);
            //var testOutputToString = textWriter.ToString();
            //Assert
            string1.Should().Be(expectedOutputPattern);

        }
    }
}
