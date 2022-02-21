using FluentAssertions;
using HouseholdBudgetPlanner.App;
using HouseholdBudgetPlanner.App.Abstract;
using HouseholdBudgetPlanner.App.Concrete;
using HouseholdBudgetPlanner.App.Managers;
using HouseholdBudgetPlanner.Domain.Entity;
using System;
using Xunit;

namespace HouseholdBudgetPlanner.Tests
{
    public class AmountManagerBudgetStatusTest
    {
        [Fact]
        public void BudgetStatusAllExpensesMonthTest()
        {
            //Arrange
            MenuActionService menuActionService = new MenuActionService();
            IService<Amount> amountService = new AmountService();
            var amountManagerBudgetStatus = new AmountManagerBudgetStatus(menuActionService, amountService);
            Amount amountExpenseOne = new Amount() { Id = 1, Name = "General expenses", Date = DateTime.Now, Value = 55.00m };
            Amount amountExpenseTwo = new Amount() { Id = 1, Name = "General expenses", Date = DateTime.Now, Value = 45.00m };
            Amount amountExpenseThree = new Amount() { Id = 1, Name = "General expenses", Date = new DateTime(2021, 01, 01), Value = 9.99m };
            amountService.AddItem(amountExpenseOne);
            amountService.AddItem(amountExpenseTwo);
            amountService.AddItem(amountExpenseThree);
            //Act
            var allExpensesMonth = amountManagerBudgetStatus.BudgetStatusAllExpensesMonth();
            //Assert
            amountManagerBudgetStatus.Should().NotBeNull();
            allExpensesMonth.Should().BeOfType(typeof(decimal));
            allExpensesMonth.Should().Be(100.00m);
            //Clear
            amountService.RemoveItem(amountExpenseOne);
            amountService.RemoveItem(amountExpenseTwo);
            amountService.RemoveItem(amountExpenseThree);
        }
        [Fact]
        public void BudgetStatusAllIncomesMonthTest()
        {
            //Arrange
            MenuActionService menuActionService = new MenuActionService();
            IService<Amount> amountService = new AmountService();
            var amountManagerBudgetStatus = new AmountManagerBudgetStatus(menuActionService, amountService);
            Amount amountIncomeOne = new Amount() { Id = -1, Name = "General incomes", Date = DateTime.Now, Value = 95.00m };
            Amount amountIncomeTwo = new Amount() { Id = -1, Name = "General incomes", Date = DateTime.Now, Value = 55.00m };
            Amount amountIncomeThree = new Amount() { Id = -1, Name = "General incomes", Date = new DateTime(2021, 01, 01), Value = 9.99m };
            amountService.AddItem(amountIncomeOne);
            amountService.AddItem(amountIncomeTwo);
            amountService.AddItem(amountIncomeThree);
            //Act
            var allIncomesMonth = amountManagerBudgetStatus.BudgetStatusAllIncomesMonth();
            //Assert
            amountManagerBudgetStatus.Should().NotBeNull();
            allIncomesMonth.Should().BeOfType(typeof(decimal));
            allIncomesMonth.Should().Be(150.00m);
            //Clear
            amountService.RemoveItem(amountIncomeOne);
            amountService.RemoveItem(amountIncomeTwo);
            amountService.RemoveItem(amountIncomeThree);
        }
        [Fact]
        public void BudgetStatusExpensesRangeDateTest()
        {
            //Arrange
            MenuActionService menuActionService = new MenuActionService();
            IService<Amount> amountService = new AmountService();
            var amountManagerBudgetStatus = new AmountManagerBudgetStatus(menuActionService, amountService);
            Amount amountExpenseOne = new Amount() { Id = 1, Name = "General expenses", Date = new DateTime(2022, 02, 10), Value = 55.00m };
            Amount amountExpenseTwo = new Amount() { Id = 1, Name = "General expenses", Date = new DateTime(2022, 02, 9), Value = 45.00m };
            Amount amountExpenseThree = new Amount() { Id = 1, Name = "General expenses", Date = new DateTime(2021, 02, 10), Value = 99.99m };
            amountService.AddItem(amountExpenseOne);
            amountService.AddItem(amountExpenseTwo);
            amountService.AddItem(amountExpenseThree);
            var dateStart = new DateTime(2022, 02, 8);
            var dateEnd = new DateTime(2022, 02, 11);
            //Act
            var allExpensesRange = amountManagerBudgetStatus.BudgetStatusExpensesRangeDate(dateStart, dateEnd);
            //Assert
            amountManagerBudgetStatus.Should().NotBeNull();
            allExpensesRange.Should().BeOfType(typeof(decimal));
            allExpensesRange.Should().Be(100.00m);
            //Clear
            amountService.RemoveItem(amountExpenseOne);
            amountService.RemoveItem(amountExpenseTwo);
            amountService.RemoveItem(amountExpenseThree);
        }
        [Fact]
        public void BudgetStatusIncomesRangeDateTest()
        {
            //Arrange
            MenuActionService menuActionService = new MenuActionService();
            IService<Amount> amountService = new AmountService();
            var amountManagerBudgetStatus = new AmountManagerBudgetStatus(menuActionService, amountService);
            Amount amountIncomeOne = new Amount() { Id = -1, Name = "General incomes", Date = new DateTime(2022, 02, 10), Value = 95.00m };
            Amount amountIncomeTwo = new Amount() { Id = -1, Name = "General incomes", Date = new DateTime(2022, 02, 9), Value = 55.00m };
            Amount amountIncomeThree = new Amount() { Id = -1, Name = "General incomes", Date = new DateTime(2021, 02, 10), Value = 9.99m };
            amountService.AddItem(amountIncomeOne);
            amountService.AddItem(amountIncomeTwo);
            amountService.AddItem(amountIncomeThree);
            var dateStart = new DateTime(2022, 02, 08);
            var dateEnd = new DateTime(2022, 02, 11);
            //Act
            var allIncomesRange = amountManagerBudgetStatus.BudgetStatusIncomesRangeDate(dateStart, dateEnd);
            //Assert
            amountManagerBudgetStatus.Should().NotBeNull();
            allIncomesRange.Should().BeOfType(typeof(decimal));
            allIncomesRange.Should().Be(150.00m);
            //Clear
            amountService.RemoveItem(amountIncomeOne);
            amountService.RemoveItem(amountIncomeTwo);
            amountService.RemoveItem(amountIncomeThree);
        }
    }
}
