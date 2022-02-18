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
    public class BudgetStatusExpensesManagerMonthTest
    {
        [Fact]
        public void BudgetStatusAllExpensesMonthListTest()
        {
            //Arrange
            AmountService amountService = new AmountService();
            AmountManagerBudgetStatus amountManagerBudgetStatus = new AmountManagerBudgetStatus();
            BudgetStatusExpensesManagerMonth budgetStatusExpensesManagerMonth = new BudgetStatusExpensesManagerMonth(amountService, amountManagerBudgetStatus);
            Amount amountExpenseOne = new Amount() { Id = 1, Name = "General expenses", Date = DateTime.Now, Value = 55.00m };
            Amount amountExpenseTwo = new Amount() { Id = 1, Name = "General expenses", Date = DateTime.Now, Value = 45.00m };
            amountService.AddItem(amountExpenseOne);
            amountService.AddItem(amountExpenseTwo);
            var expectedOutputPattern = $"\r\nYour expenses this month:\r\n\r\n" + $"{amountExpenseOne.Date}, Name: General expenses, Value: 55,00PLN\r\n" + $"{amountExpenseTwo.Date}, Name: General expenses, Value: 45,00PLN";
            var budgetStatusAllExpensesMonthOut = new StringWriter();
            Console.SetOut(budgetStatusAllExpensesMonthOut);
            //Act
            budgetStatusExpensesManagerMonth.BudgetStatusAllExpensesMonthList();
            var budgetStatusExpensesManagerMonthOutString = budgetStatusAllExpensesMonthOut.ToString();
            //Assert
            budgetStatusExpensesManagerMonthOutString.Should().Contain(expectedOutputPattern);
            //Clear
            amountService.RemoveItem(amountExpenseOne);
            amountService.RemoveItem(amountExpenseTwo);
        }
        [Fact]
        public void MonthExpenseInAmountByNameExistTest()
        {
            //Arrange
            AmountService amountService = new AmountService();
            AmountManagerBudgetStatus amountManagerBudgetStatus = new AmountManagerBudgetStatus();
            BudgetStatusExpensesManagerMonth budgetStatusExpensesManagerMonth = new BudgetStatusExpensesManagerMonth(amountService, amountManagerBudgetStatus);
            Amount amountExpenseOne = new Amount() { Id = 1, Name = "General expenses", Date = new DateTime(2022, 02, 2), Value = 55.00m };
            amountService.AddItem(amountExpenseOne);
            var nameTrue = "General expenses";
            var nameFalse = "Wrong name";
            //Act
            var amountExpenseTrue = budgetStatusExpensesManagerMonth.MonthExpenseInAmountByNameExist(nameTrue);
            var amountExpenseFalse = budgetStatusExpensesManagerMonth.MonthExpenseInAmountByNameExist(nameFalse);
            //Assert
            amountExpenseTrue.Should().BeTrue();
            amountExpenseFalse.Should().BeFalse();
            //Clean
            amountService.RemoveItem(amountExpenseOne);
        }
        [Fact]
        public void BudgetStatusExpensesMonthByNameTest_True()
        {
            //Arrange
            AmountService amountService = new AmountService();
            AmountManagerBudgetStatus amountManagerBudgetStatus = new AmountManagerBudgetStatus();
            BudgetStatusExpensesManagerMonth budgetStatusExpensesManagerMonth = new BudgetStatusExpensesManagerMonth(amountService, amountManagerBudgetStatus);
            Amount amountExpenseOne = new Amount() { Id = 1, Name = "General expenses", Date = DateTime.Now, Value = 55.00m };
            Amount amountExpenseTwo = new Amount() { Id = 1, Name = "General expenses", Date = DateTime.Now, Value = 45.00m };
            amountService.AddItem(amountExpenseOne);
            amountService.AddItem(amountExpenseTwo);
            var nameTrue = "General expenses";
            var amountExpenseExist = budgetStatusExpensesManagerMonth.MonthExpenseInAmountByNameExist(nameTrue);
            var expectedOutputTruePattern = $"Expenses status this month with the name {nameTrue}: {amountExpenseOne.Value + amountExpenseTwo.Value}{ValueTypes.PLN}";
            var budgetStatusExpensesMonthByNameOut = new StringWriter();
            Console.SetOut(budgetStatusExpensesMonthByNameOut);
            //Act
            budgetStatusExpensesManagerMonth.BudgetStatusExpensesMonthByName(amountExpenseExist, nameTrue);
            var budgetStatusExpensesMonthByNameOutString = budgetStatusExpensesMonthByNameOut.ToString();
            //Assert
            budgetStatusExpensesMonthByNameOutString.Should().Contain(expectedOutputTruePattern);
            //Clean
            amountService.RemoveItem(amountExpenseOne);
            amountService.RemoveItem(amountExpenseTwo);
        }
        [Fact]
        public void BudgetStatusExpensesMonthByNameTest_False()
        {
            //Arrange
            AmountService amountService = new AmountService();
            AmountManagerBudgetStatus amountManagerBudgetStatus = new AmountManagerBudgetStatus();
            BudgetStatusExpensesManagerMonth budgetStatusExpensesManagerMonth = new BudgetStatusExpensesManagerMonth(amountService, amountManagerBudgetStatus);
            var nameFalse = "Wrong name";
            var amountExpenseExist = budgetStatusExpensesManagerMonth.MonthExpenseInAmountByNameExist(nameFalse);
            var expectedOutputFalsePattern = "Expense type with this name does not exist.";
            var budgetStatusExpensesMonthByNameOut = new StringWriter();
            Console.SetOut(budgetStatusExpensesMonthByNameOut);
            //Act
            budgetStatusExpensesManagerMonth.BudgetStatusExpensesMonthByName(amountExpenseExist, nameFalse);
            var budgetStatusExpensesMonthByNameOutString = budgetStatusExpensesMonthByNameOut.ToString();
            //Assert
            budgetStatusExpensesMonthByNameOutString.Should().Contain(expectedOutputFalsePattern);
        }
    }
}
