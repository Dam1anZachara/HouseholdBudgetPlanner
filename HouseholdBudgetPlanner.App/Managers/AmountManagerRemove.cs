using System;

namespace HouseholdBudgetPlanner.App.Managers
{
    public class AmountManagerRemove
    {
        private readonly AmountManagerRemoveExpense _amountManagerRemoveExpense;
        private readonly AmountManagerRemoveIncome _amountManagerRemoveIncome;

        public AmountManagerRemove(AmountManagerRemoveExpense amountManagerRemoveExpense, AmountManagerRemoveIncome amountManagerRemoveIncome)
        {
            _amountManagerRemoveExpense = amountManagerRemoveExpense;
            _amountManagerRemoveIncome = amountManagerRemoveIncome;
        }
        
        public void RemoveAmount(ConsoleKeyInfo keyInfoRemoveAmount)
        {
            if (keyInfoRemoveAmount.KeyChar == '1')
            {
                _amountManagerRemoveExpense.RemoveAmountExpenseSelect();
            }
            else if (keyInfoRemoveAmount.KeyChar == '2')
            {
                _amountManagerRemoveIncome.RemoveAmountIncomeSelect();
            }
            else
            {
                Console.WriteLine("\r\nAction you entered does not exist\r\n");
            }
        }
    }
}
