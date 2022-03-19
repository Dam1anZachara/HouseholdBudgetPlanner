using HouseholdBudgetPlanner.App.Managers;
using HouseholdBudgetPlanner.Domain.Entity;
using HouseholdBudgetPlanner.Helpers;
using System;
using System.Collections.Generic;
using System.IO;

namespace HouseholdBudgetPlanner.App.Concrete
{
    public class AmountRaportListService
    {
        AmountManagerBudgetStatus _amountManagerBudgetStatus;

        public AmountRaportListService(AmountManagerBudgetStatus amountManagerBudgetStatus)
        {
            _amountManagerBudgetStatus = amountManagerBudgetStatus;
        }
        string pathFileRaport = $@"C:\Users\Damian\Desktop\{DateTime.Now.ToString("yyyy-MM-dd HHmm")} BudgetBalanceRaport.csv";
        public void GenerateRaportMonthMethod(List<Amount> getListOfExpenses, List<Amount> getListOfIncomes)
        {
            using FileStream fs = File.Create(pathFileRaport);
            using StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine($"Your Expenses in this month: {DateTime.Now.ToString("yyyy MMMM")}\r\n");
            ExpenseWriteMethod(getListOfExpenses, sw);
            sw.WriteLine($"\r\nYour Incomes in this month: {DateTime.Now.ToString("yyyy MMMM")}\r\n");
            IncomeWriteMethod(getListOfIncomes, sw);
            decimal amountSumAllExpenses = _amountManagerBudgetStatus.BudgetStatusAllExpensesMonth();
            decimal amountSumAllIncomes = _amountManagerBudgetStatus.BudgetStatusAllIncomesMonth();
            Console.WriteLine($"\r\nBudget balance this month: {amountSumAllIncomes - amountSumAllExpenses}{ ValueTypes.PLN}\r\n");
            sw.WriteLine($"\r\nYour budget balance in this month: {DateTime.Now.ToString("yyyy MMMM")}\r\n");
            sw.WriteLine($"Your all expenses in this month: {amountSumAllExpenses}{ ValueTypes.PLN}\r\n");
            sw.WriteLine($"Your all incomes in this month: {amountSumAllIncomes}{ ValueTypes.PLN}\r\n");
            sw.WriteLine($"Budget balance this month: { amountSumAllIncomes - amountSumAllExpenses}{ ValueTypes.PLN}\r\n");
        }
        public void GenerateRaportRangeDateMethod(List<Amount> getListOfExpenses, List<Amount> getListOfIncomes, DateTime dateStartEntered, DateTime dateEndEntered)
        {
            using FileStream fs = File.Create(pathFileRaport);
            using StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine($"Your expenses since {dateStartEntered} to {dateEndEntered}.\r\n");
            ExpenseWriteMethod(getListOfExpenses, sw);
            sw.WriteLine($"Your incomes since {dateStartEntered} to {dateEndEntered}.\r\n");
            IncomeWriteMethod(getListOfIncomes, sw);
            decimal amountSumAllExpensesDate = _amountManagerBudgetStatus.BudgetStatusExpensesRangeDate(dateStartEntered, dateEndEntered);
            decimal amountSumAllIncomesDate = _amountManagerBudgetStatus.BudgetStatusIncomesRangeDate(dateStartEntered, dateEndEntered);
            Console.WriteLine($"\r\nBudget balance since {dateStartEntered} to {dateEndEntered}: {amountSumAllIncomesDate - amountSumAllExpensesDate}{ValueTypes.PLN}\r\n");
            sw.WriteLine($"\r\nYour budget balance since {dateStartEntered} to {dateEndEntered}\r\n");
            sw.WriteLine($"Your all expenses: {amountSumAllExpensesDate}{ ValueTypes.PLN}\r\n");
            sw.WriteLine($"Your all incomes: {amountSumAllExpensesDate}{ ValueTypes.PLN}\r\n");
            sw.WriteLine($"Budget balance: {amountSumAllIncomesDate - amountSumAllExpensesDate}{ ValueTypes.PLN}\r\n");
        }
        private void ExpenseWriteMethod(List<Amount> getListOfExpenses, StreamWriter sw)
        {
            foreach (var amountExpense in getListOfExpenses)
            {
                sw.WriteLine($"{amountExpense.Date} {amountExpense.Name}: {amountExpense.Value}{ValueTypes.PLN}");
            }
        }
        private void IncomeWriteMethod(List<Amount> getListOfIncomes, StreamWriter sw)
        {
            foreach (var amountIncome in getListOfIncomes)
            { 
                sw.WriteLine($"{amountIncome.Date} {amountIncome.Name}: {amountIncome.Value}{ValueTypes.PLN}");
            }
        }
    }
}
