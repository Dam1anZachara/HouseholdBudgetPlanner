﻿using FluentAssertions;
using HouseholdBudgetPlanner.App;
using HouseholdBudgetPlanner.App.Managers;
using HouseholdBudgetPlanner.Domain.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HouseholdBudgetPlanner.Tests
{
    public class BudgetStatusExpensesManagerRangeTest
    {
        [Fact]
        public void BudgetStatusExpensesRangeDateListTest()
        {
            //Arrange
            AmountService amountService = new AmountService();
            AmountManagerBudgetStatus amountManagerBudgetStatus = new AmountManagerBudgetStatus();
            AmountManager amountManager = new AmountManager();
            BudgetStatusExpensesManagerRange budgetStatusExpensesManagerRange = new BudgetStatusExpensesManagerRange(amountService, amountManagerBudgetStatus, amountManager);
            Amount amountExpenseOne = new Amount() { Id = 1, Name = "General expenses", Date = new DateTime(2021, 02, 01), Value = 55.00m };
            Amount amountExpenseTwo = new Amount() { Id = 1, Name = "General expenses", Date = new DateTime(2022, 02, 01), Value = 45.00m };
            amountService.AddItem(amountExpenseOne);
            amountService.AddItem(amountExpenseTwo);
            var dateStart = new DateTime(2021, 01, 01);
            var dateEnd = new DateTime(2022, 02, 10);
            var expectedOutputPattern = $"\r\nYour expenses since {dateStart} to {dateEnd}.\r\n\r\n" + $"{amountExpenseOne.Date}, Name: {amountExpenseOne.Name}, Value: {amountExpenseOne.Value}PLN\r\n" + $"{amountExpenseTwo.Date}, Name: {amountExpenseTwo.Name}, Value: {amountExpenseTwo.Value}PLN";
            var budgetStatusExpensesRangeDateListOut = new StringWriter();
            Console.SetOut(budgetStatusExpensesRangeDateListOut);
            //Act
            budgetStatusExpensesManagerRange.BudgetStatusExpensesRangeDateList(dateStart, dateEnd);
            var budgetStatusExpensesRangeDateListOutString = budgetStatusExpensesRangeDateListOut.ToString();
            //Assert
            budgetStatusExpensesRangeDateListOutString.Should().Contain(expectedOutputPattern);
            //Clean
            amountService.RemoveItem(amountExpenseOne);
            amountService.RemoveItem(amountExpenseTwo);
        }
        [Fact]
        public void RangeExpenseInAmountByNameExistTest()
        {
            //Arrange
            AmountService amountService = new AmountService();
            AmountManagerBudgetStatus amountManagerBudgetStatus = new AmountManagerBudgetStatus();
            AmountManager amountManager = new AmountManager();
            BudgetStatusExpensesManagerRange budgetStatusExpensesManagerRange = new BudgetStatusExpensesManagerRange(amountService, amountManagerBudgetStatus, amountManager);
            Amount amountExpenseOne = new Amount() { Id = 1, Name = "General expenses", Date = new DateTime(2021, 02, 01), Value = 55.00m };
            Amount amountExpenseTwo = new Amount() { Id = 1, Name = "General expenses", Date = new DateTime(2022, 02, 01), Value = 45.00m };
            amountService.AddItem(amountExpenseOne);
            amountService.AddItem(amountExpenseTwo);
            var dateStartTrue = new DateTime(2021, 01, 01);
            var dateEndTrue = new DateTime(2022, 02, 10);
            var dateStartFalse = new DateTime(2019, 01, 01);
            var dateEndFalse = new DateTime(2020, 02, 10);
            var nameTrue = "General expenses";
            var nameFalse = "Wrong name";
            //Act
            var rangeExpenseInAmountByNameExistTrue = budgetStatusExpensesManagerRange.RangeExpenseInAmountByNameExist(dateStartTrue, dateEndTrue, nameTrue);
            var rangeExpenseInAmountByNameExistFalseOne = budgetStatusExpensesManagerRange.RangeExpenseInAmountByNameExist(dateStartTrue, dateEndTrue, nameFalse);
            var rangeExpenseInAmountByNameExistFalseTwo = budgetStatusExpensesManagerRange.RangeExpenseInAmountByNameExist(dateStartFalse, dateEndFalse, nameFalse);
            //Assert
            rangeExpenseInAmountByNameExistTrue.Should().BeTrue();
            rangeExpenseInAmountByNameExistFalseOne.Should().BeFalse();
            rangeExpenseInAmountByNameExistFalseTwo.Should().BeFalse();
        }
        [Fact]
        public void BudgetStatusExpensesRangeDateByNameTest_True()
        {
            //Arrange
            AmountService amountService = new AmountService();
            AmountManagerBudgetStatus amountManagerBudgetStatus = new AmountManagerBudgetStatus();
            AmountManager amountManager = new AmountManager();
            BudgetStatusExpensesManagerRange budgetStatusExpensesManagerRange = new BudgetStatusExpensesManagerRange(amountService, amountManagerBudgetStatus, amountManager);

        }
    }
}