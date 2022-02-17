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
    public class IncomeTypeManagerTest
    {
        [Fact]
        public void IncomeTypeViewTest()
        {
            //Arrange
            IncomeTypeService incomeTypeService = new IncomeTypeService();
            var manager = new IncomeTypeManager(incomeTypeService);
            var expectedOutputPattern = "\r\nAll your income types are:\r\n" + "\r\n1. General incomes\r\n";
            var incomeTypeViewOut = new StringWriter();
            Console.SetOut(incomeTypeViewOut); 
            //Act
            manager.IncomeTypeView();
            var incomeTypeViewOutString = incomeTypeViewOut.ToString();
            //Assert
            manager.Should().NotBeNull();
            incomeTypeViewOutString.Should().Contain(expectedOutputPattern);
        }
        [Fact]
        public void AddNewIncomeTypeViewTest()
        {
            //Arrange
            var mock = new Mock<IService<IncomeType>>();
            var expectedInputPattern = "TestName";
            var manager = new IncomeTypeManager(mock.Object);
            var testInput = new StringReader("TestName");
            Console.SetIn(testInput);
            //Act
            var returnedString = manager.AddNewIncomeTypeView();
            //Assert
            returnedString.Should().BeOfType(typeof(string));
            returnedString.Should().NotBeEmpty();
            returnedString.Should().BeSameAs(expectedInputPattern);
        }
        [Fact]
        public void IncomeTypeExistTest()
        {
            //Arrange
            IncomeTypeService incomeTypeService = new IncomeTypeService();
            var manager = new IncomeTypeManager(incomeTypeService);
            IncomeType incomeType = new IncomeType() { Id = -2, Name = "Work" };
            incomeTypeService.AddItem(incomeType);
            //Act
            var returnedIncomeTypeTrue = manager.IncomeTypeExist("Work");
            var returnedIncomeTypeFalseOne = manager.IncomeTypeExist("General expenses");
            var returnedIncomeTypeFalseTwo = manager.IncomeTypeExist("SecondWork");
            var returnedIncomeTypeFaleThree = manager.IncomeTypeExist("Wo rk");
            //Assert
            incomeTypeService.Should().NotBeNull();
            manager.Should().NotBeNull();
            returnedIncomeTypeTrue.Should().BeTrue();
            returnedIncomeTypeFalseOne.Should().BeFalse();
            returnedIncomeTypeFalseTwo.Should().BeFalse();
            returnedIncomeTypeFaleThree.Should().BeFalse();
            //clean
            incomeTypeService.RemoveItem(incomeType);
        }
        [Fact]
        public void AddNewIncomeTypeTest()
        {
            //Arrange
            IncomeTypeService incomeTypeService = new IncomeTypeService();
            var manager = new IncomeTypeManager(incomeTypeService);
            //Act
            manager.AddNewIncomeType("Work");
            var listAfterAdd = incomeTypeService.GetAllItems();
            //Assert
            listAfterAdd.Should().NotBeNull();
            listAfterAdd.Should().NotBeEmpty();
            listAfterAdd.Should().AllBeOfType(typeof(IncomeType));
            listAfterAdd.Should().HaveCount(2);
            //clean
            var incomeypeToClean = incomeTypeService.GetItemByName("Home");
            incomeTypeService.RemoveItem(incomeypeToClean);
        }
        [Fact]
        public void RemoveIncomeTypeViewTest()
        {
            //Arrange
            var mock = new Mock<IService<IncomeType>>();
            var expectedInputPattern = "TestName";
            var manager = new IncomeTypeManager(mock.Object);
            var removeIncomeTypeViewInput = new StringReader("TestName");
            Console.SetIn(removeIncomeTypeViewInput);
            //Act
            var returnedString = manager.RemoveIncomeTypeView();
            //Assert
            returnedString.Should().NotBeEmpty();
            returnedString.Should().BeOfType(typeof(string));
            returnedString.Should().BeSameAs(expectedInputPattern);
        }
        [Fact]
        public void RemoveIncomeTypeTest()
        {
            //Arrange
            IncomeTypeService incomeTypeService = new IncomeTypeService();
            var manager = new IncomeTypeManager(incomeTypeService);
            IncomeType incomeType = new IncomeType() { Id = -2, Name = "Work" };
            incomeTypeService.AddItem(incomeType);
            var incomeTypeAfterAdd = incomeTypeService.GetItemByName("Work");
            //Act
            manager.RemoveIncomeType(incomeType.Name);
            var incomeTypeAfterRemove = incomeTypeService.GetItemByName("Work");
            //Assert
            //incomeTypeAfterAdd.Should().NotBeNull();
            //incomeTypeAfterAdd.Should().BeOfType(typeof(ExpenseType));
            //incomeTypeAfterAdd.Name.Should().BeOfType(typeof(string));
            incomeTypeAfterAdd.Name.Should().Contain("Work");
            //incomeTypeAfterRemove.Should().NotBeNull();
            //incomeTypeAfterRemove.Should().BeOfType(typeof(ExpenseType));
            //incomeTypeAfterRemove.Should().NotBeOfType(typeof(string));
            incomeTypeAfterRemove.Name.Should().NotContain("Work");
        }
        [Fact]
        public void GetIncomeToAmountByNameTest()
        {
            //Arrange
            IncomeType incomeType = new IncomeType();
            incomeType.Id = -2;
            incomeType.Name = "Work";
            var mock = new Mock<IService<IncomeType>>();
            mock.Setup(s => s.GetItemByName("Work")).Returns(incomeType);
            var manager = new IncomeTypeManager(mock.Object);
            //Act
            var returnedIncomeType = manager.GetIncomeToAmountByName(incomeType.Name);
            //Assert
            returnedIncomeType.Should().BeOfType(typeof(IncomeType));
            returnedIncomeType.Should().NotBeNull();
            returnedIncomeType.Should().BeSameAs(incomeType);
        }
    }
}
