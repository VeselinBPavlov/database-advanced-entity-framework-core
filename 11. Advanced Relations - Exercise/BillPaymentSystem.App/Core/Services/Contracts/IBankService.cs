using BillPaymentSystem.Models;

namespace BillPaymentSystem.App.Core.Services.Contracts
{
    public interface IBankService
    {
        void Deposit(BankAccount bankAccount, decimal ammount);
        void Withdraw(BankAccount bankAccount, CreditCard creditCard, decimal ammount);
        BankAccount[] FindBankAccounts(int userId);
        CreditCard[] FindCreditCards(int userId);
    }
}
