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
    public class AmountManagerRemoveIncomeTest
    {
        [Fact]
        public void IncomeInAmountByDateListTest()
        {
            //Arrange
            AmountService amountService = new AmountService();
            AmountManager amountManager = new AmountManager();
            AmountManagerRemoveIncome amountManagerRemoveIncome = new AmountManagerRemoveIncome(amountService, amountManager);
            Amount amountIncomeOne = new Amount() { Id = -1, Name = "General incomes", Date = new DateTime(2022, 02, 2), Value = 4500.00m };
            Amount amountIncomeTwo = new Amount() { Id = -1, Name = "General incomes", Date = new DateTime(2022, 02, 3), Value = 2500.00m };
            amountService.AddItem(amountIncomeOne);
            amountService.AddItem(amountIncomeTwo);
            var dateStart = new DateTime(2022, 02, 1);
            var dateEnd = new DateTime(2022, 02, 4);
            var expectedOutputPattern = $"\r\nYour incomes since {dateStart} to {dateEnd}\r\n\r\n" + 
                $"{amountIncomeOne.Date}; Name: {amountIncomeOne.Name}; Value: {amountIncomeOne.Value}{ValueTypes.PLN}\r\n" + 
                $"{amountIncomeTwo.Date}; Name: {amountIncomeTwo.Name}; Value: {amountIncomeTwo.Value}{ValueTypes.PLN}";
            var incomeInAmountByDateListOut = new StringWriter();
            Console.SetOut(incomeInAmountByDateListOut);
            //Act
            amountManagerRemoveIncome.IncomeInAmountByDateList(dateStart, dateEnd);
            var incomeInAmountByDateListOutString = incomeInAmountByDateListOut.ToString();
            //Assert
            incomeInAmountByDateListOutString.Should().Contain(expectedOutputPattern);
            //Clean
            amountService.RemoveItem(amountIncomeOne);
            amountService.RemoveItem(amountIncomeTwo);
        }
        [Fact]
        public void SelectedIncomeInAmountExistTest()
        {
            AmountService amountService = new AmountService();
            AmountManager amountManager = new AmountManager();
            AmountManagerRemoveIncome amountManagerRemoveIncome = new AmountManagerRemoveIncome(amountService, amountManager);
            Amount amountIncomeOne = new Amount() { Id = -1, Name = "General incomes", Date = new DateTime(2022, 02, 2), Value = 4500.00m };
            amountService.AddItem(amountIncomeOne);
            var dateStartTrue = new DateTime(2022, 02, 1);
            var dateEndTrue = new DateTime(2022, 02, 3);
            var nameTrue = "General incomes";
            var valueTrue = 4500.00m;
            var dateStartFalse = new DateTime(2021, 02, 01);
            var dateEndFalse = new DateTime(2021, 02, 03);
            var nameFalse = "Wrong name";
            var valueFalse = 449.99m;
            //Act
            var amountIncomeTrue = amountManagerRemoveIncome.SelectedIncomeInAmountExist(dateStartTrue, dateEndTrue, nameTrue, valueTrue);
            var amountIncomeFalseOne = amountManagerRemoveIncome.SelectedIncomeInAmountExist(dateStartFalse, dateEndFalse, nameTrue, valueTrue);
            var amountIncomeFalseTwo = amountManagerRemoveIncome.SelectedIncomeInAmountExist(dateStartTrue, dateEndTrue, nameFalse, valueTrue);
            var amountIncomeFalseThree = amountManagerRemoveIncome.SelectedIncomeInAmountExist(dateStartTrue, dateEndTrue, nameTrue, valueFalse);
            var amountIncomeFalseFour = amountManagerRemoveIncome.SelectedIncomeInAmountExist(dateStartFalse, dateEndFalse, nameFalse, valueFalse);
            //Assert
            amountIncomeTrue.Should().BeTrue();
            amountIncomeFalseOne.Should().BeFalse();
            amountIncomeFalseTwo.Should().BeFalse();
            amountIncomeFalseThree.Should().BeFalse();
            amountIncomeFalseFour.Should().BeFalse();
            //Clean
            amountService.RemoveItem(amountIncomeOne);
        }
    }
}
