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
            IService<ExpenseType> expenseTypeService = new ExpenseTypeService();
            ExpenseTypeManager expenseTypeManager = new(expenseTypeService);
            var expectedOutputPattern = "\r\nAll your expense types are:\r\n" + "\r\n1. General expenses\r\n";
            var expenseTypeViewOut = new StringWriter();
            Console.SetOut(expenseTypeViewOut); 
            //Act
            expenseTypeManager.ExpenseTypeView();
            var expenseTypeViewOutString = expenseTypeViewOut.ToString();
            //Assert
            expenseTypeViewOutString.Should().Contain(expectedOutputPattern);
        }
        [Fact]
        public void AddNewExpenseTypeViewTest()
        {
            //Arrange
            var mock = new Mock<IService<ExpenseType>>();
            var expectedInputPattern = "TestName";
            ExpenseTypeManager expenseTypeManager = new(mock.Object);
            var addNewExpenseTypeVievInput = new StringReader("TestName");
            Console.SetIn(addNewExpenseTypeVievInput);
            //Act
            var returnedString = expenseTypeManager.AddNewExpenseTypeView();
            //Assert
            returnedString.Should().BeOfType(typeof(string));
            returnedString.Should().NotBeEmpty();
            returnedString.Should().BeSameAs(expectedInputPattern);
        }
        [Fact]
        public void ExpenseTypeExistTest()
        {
            //Arrange
            IService<ExpenseType> expenseTypeService = new ExpenseTypeService();
            ExpenseTypeManager expenseTypeManager = new(expenseTypeService);
            ExpenseType expenseType = new() { Id = 2, Name = "Home" };
            expenseTypeService.AddItem(expenseType);
            //Act
            var returnedExpenseTypeTrue = expenseTypeManager.ExpanseTypeExist("Home");
            var returnedExpenseTypeFalseOne = expenseTypeManager.ExpanseTypeExist("General expenses");
            var returnedExpenseTypeFalseTwo = expenseTypeManager.ExpanseTypeExist("Food");
            var returnedExpenseTypeFaleThree = expenseTypeManager.ExpanseTypeExist("Ho me");
            //Assert
            returnedExpenseTypeTrue.Should().BeTrue();
            returnedExpenseTypeFalseOne.Should().BeFalse();
            returnedExpenseTypeFalseTwo.Should().BeFalse();
            returnedExpenseTypeFaleThree.Should().BeFalse();
            //Clean
            expenseTypeService.RemoveItem(expenseType);
        }
        [Fact]
        public void AddNewExpanseTypeTest()
        {
            //Arrange
            IService<ExpenseType> expenseTypeService = new ExpenseTypeService();
            ExpenseTypeManager expenseTypeManager = new(expenseTypeService);
            //Act
            expenseTypeManager.AddNewExpanseType("Home");
            var listAfterAdd = expenseTypeService.GetAllItems();
            //Assert
            listAfterAdd.Should().NotBeNull();
            listAfterAdd.Should().NotBeEmpty();
            listAfterAdd.Should().AllBeOfType(typeof(ExpenseType));
            listAfterAdd.Should().HaveCount(2);
            //Clean
            var expenseTypeToClean = expenseTypeService.GetItemByName("Home");
            expenseTypeService.RemoveItem(expenseTypeToClean);
        }
        [Fact]
        public void RemoveExpenseTypeViewTest()
        {
            //Arrange
            var mock = new Mock<IService<ExpenseType>>();
            var expectedInputPattern = "TestName";
            ExpenseTypeManager expenseTypeManager = new(mock.Object);
            var removeExpenseTypeViewInput = new StringReader("TestName");
            Console.SetIn(removeExpenseTypeViewInput);
            //Act
            var removeExpenseTypeViewInputString = expenseTypeManager.RemoveExpenseTypeView();
            //Assert
            removeExpenseTypeViewInputString.Should().NotBeEmpty();
            removeExpenseTypeViewInputString.Should().BeOfType(typeof(string));
            removeExpenseTypeViewInputString.Should().BeSameAs(expectedInputPattern);
        }
        [Fact]
        public void RemoveExpanseTypeTest()
        {
            //Arrange
            IService<ExpenseType> expenseTypeService = new ExpenseTypeService();
            ExpenseTypeManager expenseTypeManager = new(expenseTypeService);
            ExpenseType expenseType = new() { Id = 2, Name = "Home" };
            expenseTypeService.AddItem(expenseType);
            var expenseTypeAfterAdd = expenseTypeService.GetItemByName("Home");
            //Act
            expenseTypeManager.RemoveExpanseType(expenseType.Name);
            var expenseTypeAfterRemove = expenseTypeService.GetItemByName("Home");
            //Assert
            expenseTypeAfterAdd.Name.Should().Contain("Home");
            expenseTypeAfterRemove.Name.Should().NotContain("Home");
        }
        [Fact]
        public void GetExpenseToAmountByNameTest()
        {
            //Arrange
            ExpenseType expenseType = new() { Id = 2, Name = "Home" };
            var mock = new Mock<IService<ExpenseType>>();
            mock.Setup(s => s.GetItemByName("Home")).Returns(expenseType);
            ExpenseTypeManager expenseTypeManager = new(mock.Object);
            //Act
            var returnedExpenseType = expenseTypeManager.GetExpenseToAmountByName(expenseType.Name);
            //Assert
            returnedExpenseType.Should().BeOfType(typeof(ExpenseType));
            returnedExpenseType.Should().NotBeNull();
            returnedExpenseType.Should().BeSameAs(expenseType);
        }
    }
}
