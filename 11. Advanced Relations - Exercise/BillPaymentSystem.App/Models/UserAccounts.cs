namespace BillPaymentSystem.App.Models
{
    using BillPaymentSystem.Models;

    public class UserAccounts
    {
        public string Name { get; set; }

        public BankAccount[] BankAccounts { get; set; }

        public CreditCard[] CreditCards { get; set; }
    }
}
