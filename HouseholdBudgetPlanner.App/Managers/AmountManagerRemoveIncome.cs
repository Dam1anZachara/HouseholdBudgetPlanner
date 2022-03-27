﻿using HouseholdBudgetPlanner.App.Abstract;
using HouseholdBudgetPlanner.App.FileSupport;
using HouseholdBudgetPlanner.Domain.Entity;
using HouseholdBudgetPlanner.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HouseholdBudgetPlanner.App.Managers
{
    public class AmountManagerRemoveIncome : AmountManager
    {
        private readonly IService<Amount> _amountService;
        private readonly List<Amount> _amountsGetList;
        private readonly AmountManager _amountManager;
        private AmountFileService _amountFileService;
        public AmountManagerRemoveIncome(IService<Amount> amountService, AmountManager amountManager, AmountFileService amountFileService)
        {
            _amountService = amountService;
            _amountsGetList = amountService.GetAllItems();
            _amountFileService = amountFileService;
            _amountManager = amountManager;
        }
        public void IncomeInAmountByDateList(DateTime dateStartEntered, DateTime dateEndEntered) //private
        {
            Console.WriteLine($"\r\nYour incomes since {dateStartEntered} to {dateEndEntered}\r\n");
            var incomeListString = _amountsGetList.AsQueryable()
                .Where(amount => amount.Date > dateStartEntered && amount.Date < dateEndEntered && amount.Id < 0)
                .Select(amount => $"{amount.Date}; Name: {amount.Name}; Value: {amount.Value}{ValueTypes.PLN}");
            foreach (var incomeString in incomeListString)
            {
                Console.WriteLine(incomeString);
            }
        }
        public bool SelectedIncomeInAmountExist(DateTime dateStartEntered, DateTime dateEndEntered, string nameOfRemoveAmount, decimal valueInDecimal) //private
        {
            return _amountsGetList.AsQueryable()
                .Where(amount => amount.Date > dateStartEntered && amount.Date < dateEndEntered && amount.Id < 0 &&
                amount.Name == nameOfRemoveAmount && amount.Value == valueInDecimal).Any();
        }
        private void RemoveChosenAmountIncome(DateTime dateStartEntered, DateTime dateEndEntered, string nameOfRemoveAmount, decimal valueInDecimal)
        {
            var amountIncome = _amountsGetList.AsQueryable()
                .Where(amount => amount.Date > dateStartEntered && amount.Date < dateEndEntered && amount.Id < 0 &&
                amount.Name == nameOfRemoveAmount && amount.Value == valueInDecimal).FirstOrDefault();
            Console.WriteLine("\r\nYou selected: " + amountIncome.Date + "; Name: " + amountIncome.Name + "; Value: " + amountIncome.Value + ValueTypes.PLN);
            Console.WriteLine($"\r\nDo you want to remove this income?\r\n");
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No");
            var keyInfoRemoveIncome = Console.ReadKey();
            if (keyInfoRemoveIncome.KeyChar == '1')
            {
                _amountService.RemoveItem(amountIncome);
                _amountFileService.AmountWriteFile();
                Console.WriteLine("\r\nIncome has been removed!");
            }
            else if (keyInfoRemoveIncome.KeyChar == '2')
            {
                Console.WriteLine("\r\nIncome has not been removed!");
            }
            else
            {
                Console.WriteLine("\r\nAction you entered does not exist\r\n");
                Console.WriteLine("\r\nIncome has not been removed!");
            }
        }

        internal void RemoveAmountIncomeSelect()
        {
            Console.WriteLine("\r\nYou selected from incomes!");
            Console.WriteLine("\r\nPlease select a date range of removed income");
            Console.Write("\r\nWrite a starting date in format \"dd/mm/yyyy\" and press \"Enter\": ");
            var dateStart = Console.ReadLine();
            DateTime dateStartEntered;
            dateStartEntered = DateSelect(dateStart);
            Console.Write("\r\nWrite an ending date in format \"dd/mm/yyyy\" and press \"Enter\": ");
            var dateEnd = Console.ReadLine();
            DateTime dateEndEntered;
            dateEndEntered = DateSelect(dateEnd);
            bool incomeInAmountByDateExist = _amountManager.IncomeInAmountByDateExist(dateStartEntered, dateEndEntered);
            if (incomeInAmountByDateExist)
            {
                IncomeInAmountByDateList(dateStartEntered, dateEndEntered);
                Console.Write("\r\nWrite a name of an income to remove and press \"Enter\": ");
                var nameOfRemoveAmount = Console.ReadLine();
                Console.Write($"\r\nPlease write a value of an income to remove {ValueTypes.PLN}: ");
                decimal valueInDecimal = EnterValue();
                bool selectedIncomeInAmountExist = SelectedIncomeInAmountExist(dateStartEntered, dateEndEntered, nameOfRemoveAmount, valueInDecimal);
                if (selectedIncomeInAmountExist)
                {
                    RemoveChosenAmountIncome(dateStartEntered, dateEndEntered, nameOfRemoveAmount, valueInDecimal);
                }
                else
                {
                    Console.WriteLine($"\r\nIncome by name and value you've selected does not exist!");
                }
            }
            else
            {
                Console.WriteLine($"\r\nIncomes in selected date range do not exist!");
            }
        }
    }
}
