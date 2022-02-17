using FluentAssertions;
using HouseholdBudgetPlanner.App.Concrete;
using HouseholdBudgetPlanner.App.Managers;
using HouseholdBudgetPlanner.Domain.Entity;
using System;
using System.IO;
using Xunit;

namespace HouseholdBudgetPlanner.Tests
{
    public class AmountManagerAddExpenseTest
    {
        [Fact]
        public Amount AddAmountExpenseTest()
        {
            //Arrange
            var expenseTypeService = new ExpenseTypeService();
            var expenseTypeManager = new ExpenseTypeManager(expenseTypeService);
            var expenseTypeManagerAdd = new AmountManagerAddExpense(expenseTypeManager);
            var addAmountExpenseInput = new StringReader("" + "\r\n" + "" + "\r\n" + "200");
            Console.SetIn(addAmountExpenseInput);
            //Act
            var addAmountExpense = expenseTypeManagerAdd.AddAmountExpense();
            //Assert
            addAmountExpense.Should().BeOfType(typeof(Amount));
            addAmountExpense.Name.Should().Contain("General expenses");
            addAmountExpense.Id.Should().Be(1);
            addAmountExpense.Value.Should().Be(200.00m);
            return addAmountExpense;
        }
    }
}
