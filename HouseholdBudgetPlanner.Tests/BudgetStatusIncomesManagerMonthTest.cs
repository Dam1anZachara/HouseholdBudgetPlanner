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
    public class BudgetStatusIncomesManagerMonthTest
    {
        [Fact]
        public void BudgetStatusAllIncomesMonthListTest()
        {
            //Arrange
            AmountService amountService = new AmountService();
            AmountManagerBudgetStatus amountManagerBudgetStatus = new AmountManagerBudgetStatus();
            BudgetStatusIncomesManagerMonth budgetStatusIncomesManagerMonth = new BudgetStatusIncomesManagerMonth(amountService, amountManagerBudgetStatus);
            Amount amountIncomeOne = new Amount() { Id = -1, Name = "General incomes", Date = DateTime.Now, Value = 5500.00m };
            Amount amountIncomeTwo = new Amount() { Id = -1, Name = "General incomes", Date = DateTime.Now, Value = 4500.00m };
            amountService.AddItem(amountIncomeOne);
            amountService.AddItem(amountIncomeTwo);
            var expectedOutputPattern = $"\r\nYour incomes this month:\r\n\r\n" + $"{amountIncomeOne.Date}, Name: {amountIncomeOne.Name}, Value: {amountIncomeOne.Value}{ValueTypes.PLN}\r\n" + $"{amountIncomeTwo.Date}, Name: {amountIncomeTwo.Name}, Value: {amountIncomeTwo.Value}{ValueTypes.PLN}";
            var budgetStatusAllIncomesMonthOut = new StringWriter();
            Console.SetOut(budgetStatusAllIncomesMonthOut);
            //Act
            budgetStatusIncomesManagerMonth.BudgetStatusAllIncomesMonthList();
            var budgetStatusIncomesManagerMonthOutString = budgetStatusAllIncomesMonthOut.ToString();
            //Assert
            budgetStatusIncomesManagerMonthOutString.Should().Contain(expectedOutputPattern);
            //Clear
            amountService.RemoveItem(amountIncomeOne);
            amountService.RemoveItem(amountIncomeTwo);
        }
        [Fact]
        public void MonthIncomeInAmountByNameExistTest()
        {
            //Arrange
            AmountService amountService = new AmountService();
            AmountManagerBudgetStatus amountManagerBudgetStatus = new AmountManagerBudgetStatus();
            BudgetStatusIncomesManagerMonth budgetStatusIncomesManagerMonth = new BudgetStatusIncomesManagerMonth(amountService, amountManagerBudgetStatus);
            Amount amountIncomeOne = new Amount() { Id = -1, Name = "General incomes", Date = new DateTime(2022, 02, 2), Value = 4500.00m };
            amountService.AddItem(amountIncomeOne);
            var nameTrue = "General incomes";
            var nameFalse = "Wrong name";
            //Act
            var amountIncomeTrue = budgetStatusIncomesManagerMonth.MonthIncomeInAmountByNameExist(nameTrue);
            var amountIncomeFalse = budgetStatusIncomesManagerMonth.MonthIncomeInAmountByNameExist(nameFalse);
            //Assert
            amountIncomeTrue.Should().BeTrue();
            amountIncomeFalse.Should().BeFalse();
            //Clean
            amountService.RemoveItem(amountIncomeOne);
        }
        [Fact]
        public void BudgetStatusIncomesMonthByNameTest_True()
        {
            //Arrange
            AmountService amountService = new AmountService();
            AmountManagerBudgetStatus amountManagerBudgetStatus = new AmountManagerBudgetStatus();
            BudgetStatusIncomesManagerMonth budgetStatusIncomesManagerMonth = new BudgetStatusIncomesManagerMonth(amountService, amountManagerBudgetStatus);
            Amount amountIncomeOne = new Amount() { Id = -1, Name = "General incomes", Date = DateTime.Now, Value = 5500.00m };
            Amount amountIncomeTwo = new Amount() { Id = -1, Name = "General incomes", Date = DateTime.Now, Value = 4500.00m };
            amountService.AddItem(amountIncomeOne);
            amountService.AddItem(amountIncomeTwo);
            var nameTrue = "General incomes";
            var amountIncomeExist = budgetStatusIncomesManagerMonth.MonthIncomeInAmountByNameExist(nameTrue);
            var expectedOutputTruePattern = $"Incomes status this month with the name {nameTrue}: {amountIncomeOne.Value + amountIncomeTwo.Value}{ValueTypes.PLN}";
            var budgetStatusIncomesMonthByNameOut = new StringWriter();
            Console.SetOut(budgetStatusIncomesMonthByNameOut);
            //Act
            budgetStatusIncomesManagerMonth.BudgetStatusIncomesMonthByName(amountIncomeExist, nameTrue);
            var budgetStatusIncomesMonthByNameOutString = budgetStatusIncomesMonthByNameOut.ToString();
            //Assert
            budgetStatusIncomesMonthByNameOutString.Should().Contain(expectedOutputTruePattern);
            //Clean
            amountService.RemoveItem(amountIncomeOne);
            amountService.RemoveItem(amountIncomeTwo);
        }
        [Fact]
        public void BudgetStatusIncomesMonthByNameTest_False()
        {
            //Arrange
            AmountService amountService = new AmountService();
            AmountManagerBudgetStatus amountManagerBudgetStatus = new AmountManagerBudgetStatus();
            BudgetStatusIncomesManagerMonth budgetStatusIncomesManagerMonth = new BudgetStatusIncomesManagerMonth(amountService, amountManagerBudgetStatus);
            var nameFalse = "Wrong name";
            var amountIncomeExist = budgetStatusIncomesManagerMonth.MonthIncomeInAmountByNameExist(nameFalse);
            var expectedOutputFalsePattern = "Income type with this name does not exist.";
            var budgetStatusIncomesMonthByNameOut = new StringWriter();
            Console.SetOut(budgetStatusIncomesMonthByNameOut);
            //Act
            budgetStatusIncomesManagerMonth.BudgetStatusIncomesMonthByName(amountIncomeExist, nameFalse);
            var budgetStatusIncomesMonthByNameOutString = budgetStatusIncomesMonthByNameOut.ToString();
            //Assert
            budgetStatusIncomesMonthByNameOutString.Should().Contain(expectedOutputFalsePattern);
        }
    }
}
