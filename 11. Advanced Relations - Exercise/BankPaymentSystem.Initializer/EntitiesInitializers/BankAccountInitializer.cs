namespace BankPaymentSystem.Initializer.EntitiesInitializers
{
    using BillPaymentSystem.Models;

    public class BankAccountInitializer
    {
        public static BankAccount[] GetBankAccounts()
        {
            BankAccount[] bankAccounts = new BankAccount[]
            {
                new BankAccount() { Balance = 2500m, BankName = "UniCredit Bulbank", SwiftCode = "UNCRBGSF"},
                new BankAccount() { Balance = 3000m, BankName = "UBB", SwiftCode = "UBBBGSF"},
                new BankAccount() { Balance = 3500m, BankName = "First Investment Bank", SwiftCode = "FIBBGSF"},
                new BankAccount() { Balance = 4000m, BankName = "Pireus Bank", SwiftCode = "PIRBGSF"},
                new BankAccount() { Balance = 4500m, BankName = "Post Bank", SwiftCode = "POSTBGSF"}
            };

            return bankAccounts;
        }
    }
}
