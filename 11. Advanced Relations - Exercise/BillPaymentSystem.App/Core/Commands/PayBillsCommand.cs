namespace BillPaymentSystem.App.Core.Commands
{
    using BillPaymentSystem.App.Core.Attributes;
    using BillPaymentSystem.App.Core.Services.Contracts;
    using System.Linq;
    using System.Text;

    public class PayBillsCommand : Command
    {
        [Inject]
        public IBankService BankService { get; }

        [Inject]
        public IUserService UserService { get; }

        public PayBillsCommand(string[] data, IBankService bankService, IUserService userService)
            : base(data)
        {
            this.BankService = bankService;
            this.UserService = userService;
        }

        public override string Execute()
        {
            var userId = int.Parse(Data[0]);
            var amount = decimal.Parse(Data[1]);

            var moneyBankAccount = this.BankService.FindBankAccounts(userId).Sum(b => b.Balance);
            var moneyCreditCards = this.BankService.FindCreditCards(userId).Sum(c => c.LimitLeft);

            var user = UserService.FindUser(userId);            

            var sb = new StringBuilder();
            sb.AppendLine($"User: {user.FirstName} {user.LastName}");


            if (amount > moneyBankAccount + moneyCreditCards)
            {
                sb.AppendLine("Insufficient funds!");
                return sb.ToString().TrimEnd();
            }

            var bankAccounts = this.BankService.FindBankAccounts(userId);
            var creditCards = this.BankService.FindCreditCards(userId);


            foreach (var bankAccount in bankAccounts)
            {
                if (bankAccount.Balance < amount)
                {
                    amount -= bankAccount.Balance;
                    this.BankService.Withdraw(bankAccount, null, bankAccount.Balance);
                }
                else
                {
                    this.BankService.Withdraw(bankAccount, null, amount);
                    sb.AppendLine($"The bills are paid up to bank account number {bankAccount.BankAccountId}!");

                    return sb.ToString().TrimEnd();
                }

            }

            foreach (var creditCard in creditCards)
            {
                if (creditCard.LimitLeft < amount)
                {
                    amount -= creditCard.LimitLeft;
                    this.BankService.Withdraw(null, creditCard, creditCard.LimitLeft);
                }
                else
                {
                    this.BankService.Withdraw(null, creditCard, amount);
                    sb.AppendLine($"The bills are paid up to credit card number {creditCard.CreditCardId}!");

                    return sb.ToString().TrimEnd();
                }
            }

            return sb.ToString().TrimEnd();
        }
    }
}
