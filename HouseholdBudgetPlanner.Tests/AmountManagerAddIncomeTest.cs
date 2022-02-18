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
        public void AddAmountIncomeTest_First()
        {
            //Arrange
            var incomeTypeService = new IncomeTypeService();
            IncomeType incomeType = new IncomeType() { Id = -2, Name = "Work" };
            incomeTypeService.AddItem(incomeType);
            var incomeTypeManager = new IncomeTypeManager(incomeTypeService);
            var incomeTypeManagerAdd = new AmountManagerAddIncome(incomeTypeManager);
            var addAmountIncomeInputFirst = new StringReader("Work" + "\r\n" + "" + "\r\n" + "6340,49");
            Console.SetIn(addAmountIncomeInputFirst);
            //Act
            var addAmountIncome = incomeTypeManagerAdd.AddAmountIncome();
            //Assert
            addAmountIncome.Should().BeOfType(typeof(Amount));
            addAmountIncome.Name.Should().Be("Work");
            addAmountIncome.Id.Should().Be(-2);
            addAmountIncome.Date.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(5));
            addAmountIncome.Value.Should().Be(6340.49m);
            //Clean
            incomeTypeService.RemoveItem(incomeType);
        }
        [Fact]
        public void AddAmountIncomeTest_Second()
        {
            //Arrange
            var incomeTypeService = new IncomeTypeService();
            var incomeTypeManager = new IncomeTypeManager(incomeTypeService);
            var incomeTypeManagerAdd = new AmountManagerAddIncome(incomeTypeManager);
            var addAmountIncomeInputSecond = new StringReader("" + "\r\n" + "18/02/2022" + "\r\n" + "3299,249");
            Console.SetIn(addAmountIncomeInputSecond);
            //Act
            var addAmountIncome = incomeTypeManagerAdd.AddAmountIncome();
            //Assert
            addAmountIncome.Should().BeOfType(typeof(Amount));
            addAmountIncome.Name.Should().Be("General incomes");
            addAmountIncome.Id.Should().Be(-1);
            addAmountIncome.Date.Should().Be(new DateTime(2022, 02, 18));
            addAmountIncome.Value.Should().Be(3299.25m);
        }
        [Fact]
        public void AddAmountIncomeTest_Third()
        {
            //Arrange
            var incomeTypeService = new IncomeTypeService();
            var incomeTypeManager = new IncomeTypeManager(incomeTypeService);
            var incomeTypeManagerAdd = new AmountManagerAddIncome(incomeTypeManager);
            var addAmountIncomeInputThird = new StringReader("WrongName" + "\r\n" + "06/02/2022" + "\r\n" + "250,00");
            Console.SetIn(addAmountIncomeInputThird);
            //Act
            var addAmountIncome = incomeTypeManagerAdd.AddAmountIncome();
            //Assert
            addAmountIncome.Should().BeOfType(typeof(Amount));
            addAmountIncome.Name.Should().Be("General incomes");
            addAmountIncome.Id.Should().Be(-1);
            addAmountIncome.Date.Should().Be(new DateTime(2022, 02, 6));
            addAmountIncome.Value.Should().Be(250.00m);
        }
    }
}
