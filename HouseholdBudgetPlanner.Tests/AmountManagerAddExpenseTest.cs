using FluentAssertions;
using HouseholdBudgetPlanner.App.Abstract;
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
            IService<ExpenseType> expenseTypeService = new ExpenseTypeService();
            ExpenseType expenseType = new() { Id = 2, Name = "Home" };
            expenseTypeService.AddItem(expenseType);
            ExpenseTypeManager expenseTypeManager = new(expenseTypeService);
            AmountManagerAddExpense expenseTypeManagerAdd = new(expenseTypeManager);
            StringReader addAmountExpenseInputFirst = new("Home" + "\r\n" + "" + "\r\n" + "200");
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
            IService<ExpenseType> expenseTypeService = new ExpenseTypeService();
            ExpenseTypeManager expenseTypeManager = new(expenseTypeService);
            AmountManagerAddExpense expenseTypeManagerAdd = new(expenseTypeManager);
            StringReader addAmountExpenseInputSecond = new("" + "\r\n" + "18/02/2022" + "\r\n" + "299,245");
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
            IService<ExpenseType> expenseTypeService = new ExpenseTypeService();
            ExpenseTypeManager expenseTypeManager = new(expenseTypeService);
            AmountManagerAddExpense expenseTypeManagerAdd = new(expenseTypeManager);
            StringReader addAmountExpenseInputThird = new("WrongName" + "\r\n" + "06/02/2022" + "\r\n" + "50,00");
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
