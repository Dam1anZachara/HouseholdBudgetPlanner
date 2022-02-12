using FluentAssertions;
using HouseholdBudgetPlanner.App;
using HouseholdBudgetPlanner.App.Concrete;
using HouseholdBudgetPlanner.App.Managers;
using HouseholdBudgetPlanner.Domain.Entity;
using Moq;
using System;
using System.IO;
using Xunit;

namespace HouseholdBudgetPlanner.Tests
{
    public class AmountManagerTest
    {
        //[Fact]
        //public void AddAmountViewTest()
        //{
        //    //Arrange
        //    MenuActionService menuActionService = new MenuActionService();
        //    AmountService amountService = new AmountService();
        //    var amountManager = new AmountManager(menuActionService, amountService);
        //    var expectedInputPattern = ConsoleKey.D1;
        //    //Act
        //    var returnedKeyInfo = amountManager.AddAmountView();
        //    //Assert
        //    returnedKeyInfo.Should().BeSameAs(expectedInputPattern);
        //}
        [Fact]
        public void DateSelectTest()
        {
            //Arrange
            var amountManager = new AmountManager();
            string dateEnteredTestCorrect = "05/02/2022";
            string dateEnteredTestIncorrect = "wrongFormatString";
            DateTime expectedDateTime = new DateTime(2022, 02, 05);
            var dateEnteredTestCorrected = new StringReader("05/02/2022");
            Console.SetIn(dateEnteredTestCorrected);
            //Act
            var returnedDateCorrectString = amountManager.DateSelect(dateEnteredTestCorrect);
            var returnedDateIncorrectString = amountManager.DateSelect(dateEnteredTestIncorrect);
            //Assert
            returnedDateCorrectString.Should().HaveDay(05);
            returnedDateCorrectString.Should().HaveMonth(02);
            returnedDateCorrectString.Should().HaveYear(2022);
            returnedDateCorrectString.Should().BeSameDateAs(expectedDateTime);
            returnedDateIncorrectString.Should().HaveDay(05);
            returnedDateIncorrectString.Should().HaveMonth(02);
            returnedDateIncorrectString.Should().HaveYear(2022);
            returnedDateIncorrectString.Should().BeSameDateAs(expectedDateTime);
        }
        [Fact]
        public void EnterValueTest()
        {
            //Arrange
            var amountManager = new AmountManager();
            decimal x = 100.49m;
            var valueEnteredTestCorrect = new StringReader("100,49");
            Console.SetIn(valueEnteredTestCorrect);
            //Act
            var returnedValueCorrectFormat = amountManager.EnterValue();
            //Assert
            returnedValueCorrectFormat.Should().BeOfType(typeof(decimal));
            returnedValueCorrectFormat.Should().Be(x);
        }
        [Fact]
        public void ExpenseInAmountByDateExistTest()
        {
            //Arrange
            var menuActionService = new MenuActionService();
            var amountService = new AmountService();
            var amountManager = new AmountManager(menuActionService, amountService);
            var amountExpense = new Amount();
            amountExpense.Id = 2;
            amountExpense.Name = "ExpenseTest";
            amountExpense.Value = 19.99m;
            amountExpense.Date = new DateTime(2022, 02, 05);
            amountService.AddItem(amountExpense);
            var dateStartTrue = new DateTime(2022, 02, 04);
            var dateEndTrue = new DateTime(2022, 02, 06);
            var dateStartFalse = new DateTime(2022, 01, 01);
            var dateEndFalse = new DateTime(2022, 01, 02);
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
            var menuActionService = new MenuActionService();
            var amountService = new AmountService();
            var amountManager = new AmountManager(menuActionService, amountService);
            var amountIncome = new Amount();
            amountIncome.Id = -2;
            amountIncome.Name = "IncomeTest";
            amountIncome.Value = 19.99m;
            amountIncome.Date = new DateTime(2022, 02, 05);
            amountService.AddItem(amountIncome);
            var dateStartTrue = new DateTime(2022, 02, 04);
            var dateEndTrue = new DateTime(2022, 02, 06);
            var dateStartFalse = new DateTime(2022, 01, 01);
            var dateEndFalse = new DateTime(2022, 01, 02);
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
