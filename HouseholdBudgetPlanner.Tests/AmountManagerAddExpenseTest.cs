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
        public void AddAmountExpenseTest_First()
        {
            //Arrange
            var expenseTypeService = new ExpenseTypeService();
            ExpenseType expenseType = new ExpenseType() { Id = 2, Name = "Home" };
            expenseTypeService.AddItem(expenseType);
            var expenseTypeManager = new ExpenseTypeManager(expenseTypeService);
            var expenseTypeManagerAdd = new AmountManagerAddExpense(expenseTypeManager);
            var addAmountExpenseInputFirst = new StringReader("Home" + "\r\n" + "" + "\r\n" + "200");
            Console.SetIn(addAmountExpenseInputFirst);
            //Act
            var addAmountExpense = expenseTypeManagerAdd.AddAmountExpense();
            //Assert
            addAmountExpense.Should().NotBeNull();
            addAmountExpense.Should().BeOfType(typeof(Amount));
            addAmountExpense.Name.Should().Be("Home");
            addAmountExpense.Id.Should().Be(2);
            addAmountExpense.Date.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(5));
            addAmountExpense.Value.Should().Be(200.00m);
            //Clean
            expenseTypeService.RemoveItem(expenseType);
        }
        [Fact]
        public void AddAmountExpenseTest_Second()
        {
            //Arrange
            var expenseTypeService = new ExpenseTypeService();
            var expenseTypeManager = new ExpenseTypeManager(expenseTypeService);
            var expenseTypeManagerAdd = new AmountManagerAddExpense(expenseTypeManager);
            var addAmountExpenseInputSecond = new StringReader("" + "\r\n" + "18/02/2022" + "\r\n" + "299,245");
            Console.SetIn(addAmountExpenseInputSecond);
            //Act
            var addAmountExpense = expenseTypeManagerAdd.AddAmountExpense();
            //Assert
            addAmountExpense.Should().NotBeNull();
            addAmountExpense.Should().BeOfType(typeof(Amount));
            addAmountExpense.Name.Should().Be("General expenses");
            addAmountExpense.Id.Should().Be(1);
            addAmountExpense.Date.Should().Be(new DateTime(2022, 02, 18));
            addAmountExpense.Value.Should().Be(299.25m);
        }
        [Fact]
        public void AddAmountExpenseTest_Third()
        {
            //Arrange
            var expenseTypeService = new ExpenseTypeService();
            var expenseTypeManager = new ExpenseTypeManager(expenseTypeService);
            var expenseTypeManagerAdd = new AmountManagerAddExpense(expenseTypeManager);
            var addAmountExpenseInputThird = new StringReader("WrongName" + "\r\n" + "06/02/2022" + "\r\n" + "50,00");
            Console.SetIn(addAmountExpenseInputThird);
            //Act
            var addAmountExpense = expenseTypeManagerAdd.AddAmountExpense();
            //Assert
            addAmountExpense.Should().NotBeNull();
            addAmountExpense.Should().BeOfType(typeof(Amount));
            addAmountExpense.Name.Should().Be("General expenses");
            addAmountExpense.Id.Should().Be(1);
            addAmountExpense.Date.Should().Be(new DateTime(2022, 02, 6));
            addAmountExpense.Value.Should().Be(50.00m);
        }
    }
}
