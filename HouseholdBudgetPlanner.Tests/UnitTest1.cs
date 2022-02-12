using FluentAssertions;
using HouseholdBudgetPlanner.App.Abstract;
using HouseholdBudgetPlanner.App.Managers;
using HouseholdBudgetPlanner.Domain.Entity;
using Moq;
using System;
using Xunit;

namespace HouseholdBudgetPlanner.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            ////Arrange
            //int a = 5;
            //int b = 3;
            ////Act
            //int result = a + b;
            ////Assert
            //Assert.Equal(8, result);

            // TESTY JEDNOSTKOWE

            //Arrange
            //ExpenseType expenseType = new ExpenseType();
            //expenseType.Id = 1;
            //expenseType.Name = "Home";
            //var mock = new Mock<IService<ExpenseType>>();
            //mock.Setup(s => s.GetItemByName("Home")).Returns(expenseType);

            //var manager = new ExpenseTypeManager(mock.Object);
            ////Act

            //var returnedExpenseType = manager.GetExpenseToAmountByName(expenseType.Name);
            ////Assert

            ////Assert.Equal(expenseType, returnedExpenseType);

            ////FluentAssertions
            //returnedExpenseType.Should().BeOfType(typeof(ExpenseType));
            //returnedExpenseType.Should().NotBeNull();
            //returnedExpenseType.Should().BeSameAs(expenseType);
        }
    }
}
