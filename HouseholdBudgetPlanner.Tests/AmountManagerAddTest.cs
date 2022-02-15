using FluentAssertions;
using HouseholdBudgetPlanner.App;
using HouseholdBudgetPlanner.App.Concrete;
using HouseholdBudgetPlanner.App.Managers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HouseholdBudgetPlanner.Tests
{
    public class AmountManagerAddTest
    {
        //[Fact]
        //public void AddAmountTest()
        //{
        //    AmountManagerAddExpenseTest amountManagerAddExpenseTest = new AmountManagerAddExpenseTest();
        //    var amountExpense = amountManagerAddExpenseTest.AddAmountExpenseTest();
        //    //Arrange
        //    AmountService amountService = new AmountService();
        //    ExpenseTypeService expenseTypeService = new ExpenseTypeService();
        //    ExpenseTypeManager expenseTypeManager = new ExpenseTypeManager(expenseTypeService);
        //    AmountManagerAddExpense amountManagerAddExpense = new AmountManagerAddExpense(expenseTypeManager);
        //    IncomeTypeService incomeTypeService = new IncomeTypeService();
        //    IncomeTypeManager incomeTypeManager = new IncomeTypeManager(incomeTypeService);
        //    AmountManagerAddIncome amountManagerAddIncome = new AmountManagerAddIncome(incomeTypeManager);
        //    AmountManagerAdd amountManagerAdd = new AmountManagerAdd(amountService, amountManagerAddExpense, amountManagerAddIncome);
        //    ConsoleKeyInfo keyInfoAddAmount = new ConsoleKeyInfo('3', ConsoleKey.D3, false, false, false);
        //    var expectedOutputPattern = "\r\nAction you entered does not exist\r\n";
        //    var testOutput = new StringWriter();
        //    Console.SetOut(testOutput);
        //    var testOutputToString = testOutput.ToString();

        //    //Act
        //    amountManagerAdd.AddAmount(keyInfoAddAmount);

        //    //Assert
        //    testOutputToString.Should().Contain(expectedOutputPattern);
        //}
    }
}
