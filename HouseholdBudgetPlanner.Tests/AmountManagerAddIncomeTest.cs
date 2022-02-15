using FluentAssertions;
using HouseholdBudgetPlanner.App.Concrete;
using HouseholdBudgetPlanner.App.Managers;
using HouseholdBudgetPlanner.Domain.Entity;
using System;
using System.IO;
using Xunit;

namespace HouseholdBudgetPlanner.Tests
{
    public class AmountManagerAddIncomeTest
    {
        [Fact]
        public void AddAmountExpenseTest()
        {
            //Arrange
            var incomeTypeService = new IncomeTypeService();
            var incomeTypeManager = new IncomeTypeManager(incomeTypeService);
            var incomeTypeManagerAdd = new AmountManagerAddIncome(incomeTypeManager);
            var testInputFirst = new StringReader("" + "\r\n" + "" + "\r\n" + "100");
            Console.SetIn(testInputFirst);
            //Act
            var addAmountIncome = incomeTypeManagerAdd.AddAmountIncome();
            //Assert
            addAmountIncome.Should().BeOfType(typeof(Amount));
            addAmountIncome.Name.Should().Contain("General incomes");
            addAmountIncome.Id.Should().Be(-1);
            addAmountIncome.Value.Should().Be(100.00m);
        }
    }
}
