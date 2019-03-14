namespace BillPaymentSystem.App.Core.Services
{
    using System.Linq;
    using BillPaymentSystem.App.Core.Attributes;
    using BillPaymentSystem.App.Core.Services.Contracts;
    using BillPaymentSystem.Data;
    using BillPaymentSystem.Models;

    public class BankService : IBankService
    {
        [Inject]
        public BillPaymentSystemContext Context { get; }    

        public BankService(BillPaymentSystemContext context)
        {
            this.Context = context;
        }

        public void Deposit(BankAccount bankAccount, decimal ammount)
        {
            bankAccount.Balance += ammount;
            this.Context.SaveChanges();
        }

        public void Withdraw(BankAccount bankAccount, CreditCard creditCard, decimal amount)
        {
            if (bankAccount != null)
            {
                bankAccount.Balance -= amount;
                this.Context.SaveChanges();
            }

            if (creditCard != null)
            {
                creditCard.MoneyOwed += amount;
                this.Context.SaveChanges();
            }
        }

        public BankAccount[] FindBankAccounts(int userId)
        {
            var bankAccounts = this.Context.BankAccounts
                .Where(b => b.PaymentMethod.UserId == userId)
                .OrderBy(b => b.BankAccountId)
                .ToArray();

            return bankAccounts;
        }

        public CreditCard[] FindCreditCards(int userId)
        {
            var creditCards = this.Context.CreditCards
                .Where(b => b.PaymentMethod.UserId == userId)
                .OrderBy(b => b.CreditCardId)
                .ToArray();

            return creditCards;
        }
    }
}
