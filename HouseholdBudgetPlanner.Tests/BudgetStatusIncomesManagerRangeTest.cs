using FluentAssertions;
using HouseholdBudgetPlanner.App;
using HouseholdBudgetPlanner.App.Managers;
using HouseholdBudgetPlanner.Domain.Entity;
using HouseholdBudgetPlanner.Helpers;
using System;
using System.IO;
using Xunit;

namespace HouseholdBudgetPlanner.Tests
{
    public class BudgetStatusIncomesManagerRangeTest
    {
        [Fact]
        public void BudgetStatusIncomesRangeDateListTest()
        {
            //Arrange
            AmountService amountService = new AmountService();
            AmountManagerBudgetStatus amountManagerBudgetStatus = new AmountManagerBudgetStatus();
            AmountManager amountManager = new AmountManager();
            BudgetStatusIncomesManagerRange budgetStatusIncomesManagerRange = new BudgetStatusIncomesManagerRange(amountService, amountManagerBudgetStatus, amountManager);
            Amount amountIncomeOne = new Amount() { Id = -1, Name = "General incomes", Date = new DateTime(2021, 02, 01), Value = 5500.00m };
            Amount amountIncomeTwo = new Amount() { Id = -1, Name = "General incomes", Date = new DateTime(2022, 02, 01), Value = 4500.00m };
            amountService.AddItem(amountIncomeOne);
            amountService.AddItem(amountIncomeTwo);
            var dateStart = new DateTime(2021, 01, 01);
            var dateEnd = new DateTime(2022, 02, 10);
            var expectedOutputPattern = $"\r\nYour incomes since {dateStart} to {dateEnd}.\r\n\r\n" +
                $"{amountIncomeOne.Date}, Name: {amountIncomeOne.Name}, Value: {amountIncomeOne.Value}{ValueTypes.PLN}\r\n" +
                $"{amountIncomeTwo.Date}, Name: {amountIncomeTwo.Name}, Value: {amountIncomeTwo.Value}{ValueTypes.PLN}";
            var budgetStatusIncomesRangeDateListOut = new StringWriter();
            Console.SetOut(budgetStatusIncomesRangeDateListOut);
            //Act
            budgetStatusIncomesManagerRange.BudgetStatusIncomesRangeDateList(dateStart, dateEnd);
            var budgetStatusIncomesRangeDateListOutString = budgetStatusIncomesRangeDateListOut.ToString();
            //Assert
            budgetStatusIncomesRangeDateListOutString.Should().Contain(expectedOutputPattern);
            //Clean
            amountService.RemoveItem(amountIncomeOne);
            amountService.RemoveItem(amountIncomeTwo);
        }
        [Fact]
        public void RangeIncomeInAmountByNameExistTest()
        {
            //Arrange
            AmountService amountService = new AmountService();
            AmountManagerBudgetStatus amountManagerBudgetStatus = new AmountManagerBudgetStatus();
            AmountManager amountManager = new AmountManager();
            BudgetStatusIncomesManagerRange budgetStatusIncomesManagerRange = new BudgetStatusIncomesManagerRange(amountService, amountManagerBudgetStatus, amountManager);
            Amount amountIncomeOne = new Amount() { Id = -1, Name = "General incomes", Date = new DateTime(2021, 02, 01), Value = 5500.00m };
            Amount amountIncomeTwo = new Amount() { Id = -1, Name = "General incomes", Date = new DateTime(2022, 02, 01), Value = 4500.00m };
            amountService.AddItem(amountIncomeOne);
            amountService.AddItem(amountIncomeTwo);
            var dateStartTrue = new DateTime(2021, 01, 01);
            var dateEndTrue = new DateTime(2022, 02, 10);
            var dateStartFalse = new DateTime(2019, 01, 01);
            var dateEndFalse = new DateTime(2020, 02, 10);
            var nameTrue = "General incomes";
            var nameFalse = "Wrong name";
            //Act
            var rangeIncomeInAmountByNameExistTrue = budgetStatusIncomesManagerRange.RangeIncomeInAmountByNameExist(dateStartTrue, dateEndTrue, nameTrue);
            var rangeIncomeInAmountByNameExistFalseOne = budgetStatusIncomesManagerRange.RangeIncomeInAmountByNameExist(dateStartTrue, dateEndTrue, nameFalse);
            var rangeIncomeInAmountByNameExistFalseTwo = budgetStatusIncomesManagerRange.RangeIncomeInAmountByNameExist(dateStartFalse, dateEndFalse, nameFalse);
            //Assert
            rangeIncomeInAmountByNameExistTrue.Should().BeTrue();
            rangeIncomeInAmountByNameExistFalseOne.Should().BeFalse();
            rangeIncomeInAmountByNameExistFalseTwo.Should().BeFalse();
        }
        [Fact]
        public void BudgetStatusIncomesRangeDateByNameTest_True()
        {
            //Arrange
            AmountService amountService = new AmountService();
            AmountManagerBudgetStatus amountManagerBudgetStatus = new AmountManagerBudgetStatus();
            AmountManager amountManager = new AmountManager();
            BudgetStatusIncomesManagerRange budgetStatusIncomesManagerRange = new BudgetStatusIncomesManagerRange(amountService, amountManagerBudgetStatus, amountManager);
            Amount amountIncomeOne = new Amount() { Id = -1, Name = "General incomes", Date = new DateTime(2021, 02, 01), Value = 5500.00m };
            Amount amountIncomeTwo = new Amount() { Id = -1, Name = "General incomes", Date = new DateTime(2022, 02, 01), Value = 4500.00m };
            amountService.AddItem(amountIncomeOne);
            amountService.AddItem(amountIncomeTwo);
            var dateStartTrue = new DateTime(2021, 01, 01);
            var dateEndTrue = new DateTime(2022, 02, 10);
            var nameTrue = "General incomes";
            var amountIncomeExist = budgetStatusIncomesManagerRange.RangeIncomeInAmountByNameExist(dateStartTrue, dateEndTrue, nameTrue);
            var expectedOutputTruePattern = $"\r\nIncomes status with the name {nameTrue} since {dateStartTrue} to {dateEndTrue}: {amountIncomeOne.Value + amountIncomeTwo.Value}{ValueTypes.PLN}\r\n";
            var budgetStatusIncomesRangeDateByNameOut = new StringWriter();
            Console.SetOut(budgetStatusIncomesRangeDateByNameOut);
            //Act
            budgetStatusIncomesManagerRange.BudgetStatusIncomesRangeDateByName(amountIncomeExist, dateStartTrue, dateEndTrue, nameTrue);
            var budgetStatusIncomesRangeDateByNameOutString = budgetStatusIncomesRangeDateByNameOut.ToString();
            //Assert
            budgetStatusIncomesRangeDateByNameOutString.Should().Contain(expectedOutputTruePattern);
            //Clean
            amountService.RemoveItem(amountIncomeOne);
            amountService.RemoveItem(amountIncomeTwo);
        }
        [Fact]
        public void BudgetStatusIncomesRangeDateByNameTest_False()
        {
            //Arrange
            AmountService amountService = new AmountService();
            AmountManagerBudgetStatus amountManagerBudgetStatus = new AmountManagerBudgetStatus();
            AmountManager amountManager = new AmountManager();
            BudgetStatusIncomesManagerRange budgetStatusIncomesManagerRange = new BudgetStatusIncomesManagerRange(amountService, amountManagerBudgetStatus, amountManager);
            var dateStartFalse = new DateTime(2021, 01, 01);
            var dateEndFalse = new DateTime(2022, 02, 10);
            var nameFalse = "Wrong name";
            var amountIncomeExist = budgetStatusIncomesManagerRange.RangeIncomeInAmountByNameExist(dateStartFalse, dateEndFalse, nameFalse);
            var expectedOutputFalsePattern = "Income type with this name does not exist.";
            var budgetStatusIncomesRangeDateByNameOut = new StringWriter();
            Console.SetOut(budgetStatusIncomesRangeDateByNameOut);
            //Act
            budgetStatusIncomesManagerRange.BudgetStatusIncomesRangeDateByName(amountIncomeExist, dateStartFalse, dateEndFalse, nameFalse);
            var budgetStatusIncomesRangeDateByNameOutString = budgetStatusIncomesRangeDateByNameOut.ToString();
            //Assert
            budgetStatusIncomesRangeDateByNameOutString.Should().Contain(expectedOutputFalsePattern);
            //Clean
        }
    }
}
