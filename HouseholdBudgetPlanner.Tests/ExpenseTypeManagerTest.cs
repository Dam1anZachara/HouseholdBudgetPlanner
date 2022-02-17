using FluentAssertions;
using HouseholdBudgetPlanner.App.Abstract;
using HouseholdBudgetPlanner.App.Concrete;
using HouseholdBudgetPlanner.App.Managers;
using HouseholdBudgetPlanner.Domain.Entity;
using Moq;
using System;
using System.IO;
using Xunit;

namespace HouseholdBudgetPlanner.Tests
{
    public class ExpenseTypeManagerTest
    {
        [Fact]
        public void ExpenseTypeViewTest()
        {
            //Arrange
            ExpenseTypeService expenseTypeService = new ExpenseTypeService();
            var manager = new ExpenseTypeManager(expenseTypeService);
            var expectedOutputPattern = "\r\nAll your expense types are:\r\n" + "\r\n1. General expenses\r\n";
            var expenseTypeViewOut = new StringWriter();
            Console.SetOut(expenseTypeViewOut); 
            //Act
            manager.ExpenseTypeView();
            var expenseTypeViewOutString = expenseTypeViewOut.ToString();
            //Assert
            manager.Should().NotBeNull();
            expenseTypeViewOutString.Should().Contain(expectedOutputPattern);
        }
        [Fact]
        public void AddNewExpenseTypeViewTest()
        {
            //Arrange
            var mock = new Mock<IService<ExpenseType>>();
            var expectedInputPattern = "TestName";
            var manager = new ExpenseTypeManager(mock.Object);
            var testInput = new StringReader("TestName");
            Console.SetIn(testInput);
            //Act
            var returnedString = manager.AddNewExpenseTypeView();
            //Assert
            returnedString.Should().BeOfType(typeof(string));
            returnedString.Should().NotBeEmpty();
            returnedString.Should().BeSameAs(expectedInputPattern);
        }
        [Fact]
        public void ExpenseTypeExistTest()
        {
            //Arrange
            ExpenseTypeService expenseTypeService = new ExpenseTypeService();
            var manager = new ExpenseTypeManager(expenseTypeService);
            ExpenseType expenseType = new ExpenseType() { Id = 2, Name = "Home" };
            expenseTypeService.AddItem(expenseType);
            //Act
            var returnedExpenseTypeTrue = manager.ExpanseTypeExist("Home");
            var returnedExpenseTypeFalseOne = manager.ExpanseTypeExist("General expenses");
            var returnedExpenseTypeFalseTwo = manager.ExpanseTypeExist("Food");
            var returnedExpenseTypeFaleThree = manager.ExpanseTypeExist("Ho me");
            //Assert
            expenseTypeService.Should().NotBeNull();
            manager.Should().NotBeNull();
            returnedExpenseTypeTrue.Should().BeTrue();
            returnedExpenseTypeFalseOne.Should().BeFalse();
            returnedExpenseTypeFalseTwo.Should().BeFalse();
            returnedExpenseTypeFaleThree.Should().BeFalse();
            //clean
            expenseTypeService.RemoveItem(expenseType);
        }
        [Fact]
        public void AddNewExpanseTypeTest()
        {
            //Arrange
            ExpenseTypeService expenseTypeService = new ExpenseTypeService();
            var manager = new ExpenseTypeManager(expenseTypeService);
            //Act
            manager.AddNewExpanseType("Home");
            var listAfterAdd = expenseTypeService.GetAllItems();
            //Assert
            listAfterAdd.Should().NotBeNull();
            listAfterAdd.Should().NotBeEmpty();
            listAfterAdd.Should().AllBeOfType(typeof(ExpenseType));
            listAfterAdd.Should().HaveCount(2);
            //clean
            var expenseTypeToClean = expenseTypeService.GetItemByName("Home");
            expenseTypeService.RemoveItem(expenseTypeToClean);
        }
        [Fact]
        public void RemoveExpenseTypeViewTest()
        {
            //Arrange
            var mock = new Mock<IService<ExpenseType>>();
            var expectedInputPattern = "TestName";
            var manager = new ExpenseTypeManager(mock.Object);
            var removeExpenseTypeViewInput = new StringReader("TestName");
            Console.SetIn(removeExpenseTypeViewInput);
            //Act
            var removeExpenseTypeViewInputString = manager.RemoveExpenseTypeView();
            //Assert
            removeExpenseTypeViewInputString.Should().NotBeEmpty();
            removeExpenseTypeViewInputString.Should().BeOfType(typeof(string));
            removeExpenseTypeViewInputString.Should().BeSameAs(expectedInputPattern);
        }
        [Fact]
        public void RemoveExpanseTypeTest()
        {
            //Arrange
            ExpenseTypeService expenseTypeService = new ExpenseTypeService();
            var manager = new ExpenseTypeManager(expenseTypeService);
            ExpenseType expenseType = new ExpenseType() { Id = 2, Name = "Home" };
            expenseTypeService.AddItem(expenseType);
            var expenseTypeAfterAdd = expenseTypeService.GetItemByName("Home");
            //Act
            manager.RemoveExpanseType(expenseType.Name);
            var expenseTypeAfterRemove = expenseTypeService.GetItemByName("Home");
            //Assert
            //expenseTypeAfterAdd.Should().NotBeNull();
            //expenseTypeAfterAdd.Should().BeOfType(typeof(ExpenseType));
            //expenseTypeAfterAdd.Name.Should().BeOfType(typeof(string));
            expenseTypeAfterAdd.Name.Should().Contain("Home");
            //expenseTypeAfterRemove.Should().NotBeNull();
            //expenseTypeAfterRemove.Should().BeOfType(typeof(ExpenseType));
            //expenseTypeAfterRemove.Should().NotBeOfType(typeof(string));
            expenseTypeAfterRemove.Name.Should().NotContain("Home");
        }
        [Fact]
        public void GetExpenseToAmountByNameTest()
        {
            //Arrange
            ExpenseType expenseType = new ExpenseType();
            expenseType.Id = 2;
            expenseType.Name = "Home";
            var mock = new Mock<IService<ExpenseType>>();
            mock.Setup(s => s.GetItemByName("Home")).Returns(expenseType);
            var manager = new ExpenseTypeManager(mock.Object);
            //Act
            var returnedExpenseType = manager.GetExpenseToAmountByName(expenseType.Name);
            //Assert
            returnedExpenseType.Should().BeOfType(typeof(ExpenseType));
            returnedExpenseType.Should().NotBeNull();
            returnedExpenseType.Should().BeSameAs(expenseType);
        }
    }
}
