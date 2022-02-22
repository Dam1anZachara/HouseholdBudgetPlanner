using FluentAssertions;
using HouseholdBudgetPlanner.App;
using HouseholdBudgetPlanner.App.Abstract;
using HouseholdBudgetPlanner.App.Managers;
using HouseholdBudgetPlanner.Domain.Entity;
using HouseholdBudgetPlanner.Helpers;
using System;
using System.IO;
using Xunit;

namespace HouseholdBudgetPlanner.Tests
{
    public class AmountManagerRemoveExpenseTest
    {
        [Fact]
        public void ExpenseInAmountByDateListTest()
        {
            //Arrange
            IService<Amount> amountService = new AmountService();
            AmountManager amountManager = new();
            AmountManagerRemoveExpense amountManagerRemoveExpense = new(amountService, amountManager);
            Amount amountExpenseOne = new() { Id = 1, Name = "General expenses", Date = new DateTime(2022, 02, 2), Value = 55.00m };
            Amount amountExpenseTwo = new() { Id = 1, Name = "General expenses", Date = new DateTime(2022, 02, 3), Value = 45.00m };
            amountService.AddItem(amountExpenseOne);
            amountService.AddItem(amountExpenseTwo);
            DateTime dateStart = new(2022, 02, 1);
            DateTime dateEnd = new(2022, 02, 4);
            var expectedOutputPattern = $"\r\nYour expenses since {dateStart} to {dateEnd}\r\n\r\n" +
                $"{amountExpenseOne.Date}; Name: {amountExpenseOne.Name}; Value: {amountExpenseOne.Value}{ValueTypes.PLN}\r\n" +
                $"{amountExpenseTwo.Date}; Name: {amountExpenseTwo.Name}; Value: {amountExpenseTwo.Value}{ValueTypes.PLN}";
            StringWriter expenseInAmountByDateListOut = new();
            Console.SetOut(expenseInAmountByDateListOut);
            //Act
            amountManagerRemoveExpense.ExpenseInAmountByDateList(dateStart, dateEnd);
            var expenseInAmountByDateListOutString = expenseInAmountByDateListOut.ToString();
            //Assert
            expenseInAmountByDateListOutString.Should().Contain(expectedOutputPattern);
            //Clean
            amountService.RemoveItem(amountExpenseOne);
            amountService.RemoveItem(amountExpenseTwo);
        }
        [Fact]
        public void SelectedExpenseInAmountExistTest()
        {
            //Arrange
            IService<Amount> amountService = new AmountService();
            AmountManager amountManager = new();
            AmountManagerRemoveExpense amountManagerRemoveExpense = new(amountService, amountManager);
            Amount amountExpenseOne = new() { Id = 1, Name = "General expenses", Date = new DateTime(2022, 02, 2), Value = 55.00m };
            amountService.AddItem(amountExpenseOne);
            DateTime dateStartTrue = new(2022, 02, 1);
            DateTime dateEndTrue = new(2022, 02, 3);
            var nameTrue = "General expenses";
            var valueTrue = 55.00m;
            DateTime dateStartFalse = new(2021, 02, 01);
            DateTime dateEndFalse = new(2021, 02, 03);
            var nameFalse = "Wrong name";
            var valueFalse = 49.99m;
            //Act
            var amountExpenseTrue = amountManagerRemoveExpense.SelectedExpenseInAmountExist(dateStartTrue, dateEndTrue, nameTrue, valueTrue);
            var amountExpenseFalseOne = amountManagerRemoveExpense.SelectedExpenseInAmountExist(dateStartFalse, dateEndFalse, nameTrue, valueTrue);
            var amountExpenseFalseTwo = amountManagerRemoveExpense.SelectedExpenseInAmountExist(dateStartTrue, dateEndTrue, nameFalse, valueTrue);
            var amountExpenseFalseThree = amountManagerRemoveExpense.SelectedExpenseInAmountExist(dateStartTrue, dateEndTrue, nameTrue, valueFalse);
            var amountExpenseFalseFour = amountManagerRemoveExpense.SelectedExpenseInAmountExist(dateStartFalse, dateEndFalse, nameFalse, valueFalse);
            //Assert
            amountExpenseTrue.Should().BeTrue();
            amountExpenseFalseOne.Should().BeFalse();
            amountExpenseFalseTwo.Should().BeFalse();
            amountExpenseFalseThree.Should().BeFalse();
            amountExpenseFalseFour.Should().BeFalse();
            //Clean
            amountService.RemoveItem(amountExpenseOne);
        }
    }
}
