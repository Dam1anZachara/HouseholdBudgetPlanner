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
            IService<IncomeType> incomeTypeService = new IncomeTypeService();
            IncomeTypeManager incomeTypeManager = new(incomeTypeService);
            var expectedOutputPattern = "\r\nAll your income types are:\r\n" + "\r\n1. General incomes\r\n";
            var incomeTypeViewOut = new StringWriter();
            Console.SetOut(incomeTypeViewOut);
            //Act
            incomeTypeManager.IncomeTypeView();
            var incomeTypeViewOutString = incomeTypeViewOut.ToString();
            //Assert
            incomeTypeViewOutString.Should().Contain(expectedOutputPattern);
        }
        [Fact]
        public void AddNewIncomeTypeViewTest()
        {
            //Arrange
            var mock = new Mock<IService<IncomeType>>();
            var expectedInputPattern = "TestName";
            IncomeTypeManager incomeTypeManager = new(mock.Object);
            var addNewIncomeTypeViewInput = new StringReader("TestName");
            Console.SetIn(addNewIncomeTypeViewInput);
            //Act
            var returnedString = incomeTypeManager.AddNewIncomeTypeView();
            //Assert
            returnedString.Should().BeOfType(typeof(string));
            returnedString.Should().NotBeEmpty();
            returnedString.Should().BeSameAs(expectedInputPattern);
        }
        [Fact]
        public void IncomeTypeExistTest()
        {
            //Arrange
            IService<IncomeType> incomeTypeService = new IncomeTypeService();
            IncomeTypeManager incomeTypeManager = new(incomeTypeService);
            IncomeType incomeType = new() { Id = -2, Name = "Work" };
            incomeTypeService.AddItem(incomeType);
            //Act
            var returnedIncomeTypeTrue = incomeTypeManager.IncomeTypeExist("Work");
            var returnedIncomeTypeFalseOne = incomeTypeManager.IncomeTypeExist("General expenses");
            var returnedIncomeTypeFalseTwo = incomeTypeManager.IncomeTypeExist("SecondWork");
            var returnedIncomeTypeFaleThree = incomeTypeManager.IncomeTypeExist("Wo rk");
            //Assert
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
            IService<IncomeType> incomeTypeService = new IncomeTypeService();
            IncomeTypeManager incomeTypeManager = new(incomeTypeService);
            //Act
            incomeTypeManager.AddNewIncomeType("Work");
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
            IncomeTypeManager incomeTypeManager = new(mock.Object);
            var removeIncomeTypeViewInput = new StringReader("TestName");
            Console.SetIn(removeIncomeTypeViewInput);
            //Act
            var returnedString = incomeTypeManager.RemoveIncomeTypeView();
            //Assert
            returnedString.Should().NotBeEmpty();
            returnedString.Should().BeOfType(typeof(string));
            returnedString.Should().BeSameAs(expectedInputPattern);
        }
        [Fact]
        public void RemoveIncomeTypeTest()
        {
            //Arrange
            IService<IncomeType> incomeTypeService = new IncomeTypeService();
            IncomeTypeManager incomeTypeManager = new(incomeTypeService);
            IncomeType incomeType = new() { Id = -2, Name = "Work" };
            incomeTypeService.AddItem(incomeType);
            var incomeTypeAfterAdd = incomeTypeService.GetItemByName("Work");
            //Act
            incomeTypeManager.RemoveIncomeType(incomeType.Name);
            var incomeTypeAfterRemove = incomeTypeService.GetItemByName("Work");
            //Assert
            incomeTypeAfterAdd.Name.Should().Contain("Work");
            incomeTypeAfterRemove.Name.Should().NotContain("Work");
        }
        [Fact]
        public void GetIncomeToAmountByNameTest()
        {
            //Arrange
            IncomeType incomeType = new() { Id = -2, Name = "Work"};
            var mock = new Mock<IService<IncomeType>>();
            mock.Setup(s => s.GetItemByName("Work")).Returns(incomeType);
            IncomeTypeManager incomeTypeManager = new(mock.Object);
            //Act
            var returnedIncomeType = incomeTypeManager.GetIncomeToAmountByName(incomeType.Name);
            //Assert
            returnedIncomeType.Should().BeOfType(typeof(IncomeType));
            returnedIncomeType.Should().NotBeNull();
            returnedIncomeType.Should().BeSameAs(incomeType);
        }
    }
}
