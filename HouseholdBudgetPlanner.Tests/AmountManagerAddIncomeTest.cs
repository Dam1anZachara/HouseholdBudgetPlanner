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
    public class AmountManagerAddIncomeTest
    {
        [Fact]
        public void AddAmountIncomeTest_First()
        {
            //Arrange
            IService<IncomeType> incomeTypeService = new IncomeTypeService();
            IncomeType incomeType = new() { Id = -2, Name = "Work" };
            incomeTypeService.AddItem(incomeType);
            IncomeTypeManager incomeTypeManager = new(incomeTypeService);
            AmountManagerAddIncome incomeTypeManagerAdd = new(incomeTypeManager);
            StringReader addAmountIncomeInputFirst = new("Work" + "\r\n" + "" + "\r\n" + "6340,49");
            Console.SetIn(addAmountIncomeInputFirst);
            //Act
            var addAmountIncome = incomeTypeManagerAdd.AddAmountIncome();
            //Assert
            addAmountIncome.Should().NotBeNull();
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
            IService<IncomeType> incomeTypeService = new IncomeTypeService();
            IncomeTypeManager incomeTypeManager = new(incomeTypeService);
            AmountManagerAddIncome incomeTypeManagerAdd = new(incomeTypeManager);
            StringReader addAmountIncomeInputSecond = new("" + "\r\n" + "18/02/2022" + "\r\n" + "3299,249");
            Console.SetIn(addAmountIncomeInputSecond);
            //Act
            var addAmountIncome = incomeTypeManagerAdd.AddAmountIncome();
            //Assert
            addAmountIncome.Should().NotBeNull();
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
            IService<IncomeType> incomeTypeService = new IncomeTypeService();
            IncomeTypeManager incomeTypeManager = new(incomeTypeService);
            AmountManagerAddIncome incomeTypeManagerAdd = new(incomeTypeManager);
            StringReader addAmountIncomeInputThird = new("WrongName" + "\r\n" + "06/02/2022" + "\r\n" + "250,00");
            Console.SetIn(addAmountIncomeInputThird);
            //Act
            var addAmountIncome = incomeTypeManagerAdd.AddAmountIncome();
            //Assert
            addAmountIncome.Should().NotBeNull();
            addAmountIncome.Should().BeOfType(typeof(Amount));
            addAmountIncome.Name.Should().Be("General incomes");
            addAmountIncome.Id.Should().Be(-1);
            addAmountIncome.Date.Should().Be(new DateTime(2022, 02, 6));
            addAmountIncome.Value.Should().Be(250.00m);
        }
    }
}
