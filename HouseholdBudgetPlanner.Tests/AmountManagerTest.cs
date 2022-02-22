using FluentAssertions;
using HouseholdBudgetPlanner.App;
using HouseholdBudgetPlanner.App.Abstract;
using HouseholdBudgetPlanner.App.Concrete;
using HouseholdBudgetPlanner.App.Managers;
using HouseholdBudgetPlanner.Domain.Entity;
using System;
using System.IO;
using Xunit;

namespace HouseholdBudgetPlanner.Tests
{
    public class AmountManagerTest
    {
        [Fact]
        public void DateSelectTest()
        {
            //Arrange
            AmountManager amountManager = new();
            string dateEnteredTestCorrect = "05/02/2022";
            string dateEnteredTestIncorrect = "wrongFormatString";
            DateTime expectedDateTime = new(2022, 02, 05);
            DateTime expectedDateTimeCorrected = new(2021, 03, 10);
            StringReader dateEnteredTestCorrected = new("10/03/2021");
            Console.SetIn(dateEnteredTestCorrected);
            //Act
            var returnedDateCorrectString = amountManager.DateSelect(dateEnteredTestCorrect);
            var returnedDateIncorrectString = amountManager.DateSelect(dateEnteredTestIncorrect);
            //Assert
            returnedDateCorrectString.Should().HaveDay(05);
            returnedDateCorrectString.Should().HaveMonth(02);
            returnedDateCorrectString.Should().HaveYear(2022);
            returnedDateCorrectString.Should().BeSameDateAs(expectedDateTime);
            returnedDateIncorrectString.Should().HaveDay(10);
            returnedDateIncorrectString.Should().HaveMonth(03);
            returnedDateIncorrectString.Should().HaveYear(2021);
            returnedDateIncorrectString.Should().BeSameDateAs(expectedDateTimeCorrected);
        }
        [Fact]
        public void EnterValueTest()
        {
            //Arrange
            AmountManager amountManager = new();
            StringReader valueEnteredTestCorrect = new("100,49");
            Console.SetIn(valueEnteredTestCorrect);
            //Act
            var returnedValueCorrectFormat = amountManager.EnterValue();
            //Assert
            returnedValueCorrectFormat.Should().BeOfType(typeof(decimal));
            returnedValueCorrectFormat.Should().Be(100.49m);
        }
        [Fact]
        public void ExpenseInAmountByDateExistTest()
        {
            //Arrange
            MenuActionService menuActionService = new();
            IService<Amount> amountService = new AmountService();
            AmountManager amountManager = new(menuActionService, amountService);
            var amountExpense = new Amount() { Id = 2, Name = "ExpenseTest", Value = 19.99m, Date = new DateTime(2022, 02, 05) };
            amountService.AddItem(amountExpense);
            DateTime dateStartTrue = new(2022, 02, 04);
            DateTime dateEndTrue = new(2022, 02, 06);
            DateTime dateStartFalse = new(2022, 01, 01);
            DateTime dateEndFalse = new(2022, 01, 02);
            //Act
            var expenseExist = amountManager.ExpenseInAmountByDateExist(dateStartTrue, dateEndTrue);
            var expenseNotExist = amountManager.ExpenseInAmountByDateExist(dateStartFalse, dateEndFalse);
            //Assert
            expenseExist.Should().BeTrue();
            expenseNotExist.Should().BeFalse();
            //Clean
            amountService.RemoveItem(amountExpense);

        }
        [Fact]
        public void IncomeInAmountByDateExistTest()
        {
            //Arrange
            MenuActionService menuActionService = new();
            IService<Amount> amountService = new AmountService();
            AmountManager amountManager = new(menuActionService, amountService);
            var amountIncome = new Amount() { Id = -2, Name = "IncomeTest", Value = 19.99m, Date = new DateTime(2022, 02, 05) };
            amountService.AddItem(amountIncome);
            DateTime dateStartTrue = new(2022, 02, 04);
            DateTime dateEndTrue = new(2022, 02, 06);
            DateTime dateStartFalse = new(2022, 01, 01);
            DateTime dateEndFalse = new(2022, 01, 02);
            //Act
            var incomeExist = amountManager.IncomeInAmountByDateExist(dateStartTrue, dateEndTrue);
            var incomeNotExist = amountManager.IncomeInAmountByDateExist(dateStartFalse, dateEndFalse);
            //Assert
            incomeExist.Should().BeTrue();
            incomeNotExist.Should().BeFalse();
            //Clean
            amountService.RemoveItem(amountIncome);
        }
    }
}
