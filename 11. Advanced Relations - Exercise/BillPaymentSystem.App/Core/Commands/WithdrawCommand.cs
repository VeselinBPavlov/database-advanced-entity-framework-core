namespace BillPaymentSystem.App.Core.Commands
{
    using System;
    using System.Linq;
    using System.Text;

    using BillPaymentSystem.App.Core.Attributes;
    using BillPaymentSystem.App.Core.Services.Contracts;

    public class WithdrawCommand : Command
    {
        [Inject]
        public IBankService BankService { get; }

        [Inject]
        public IUserService UserService { get; }

        public WithdrawCommand(string[] data, IBankService bankService, IUserService userService)
            : base(data)
        {
            this.BankService = bankService;
            this.UserService = userService;
        }

        public override string Execute()
        {
            var userId = int.Parse(Data[0]);
            var amount = decimal.Parse(Data[1]);

            var user = UserService.FindUser(userId);

            if (user == null)
            {
                throw new ArgumentNullException("User not found!");
            }

            var bankAccount = this.BankService.FindBankAccounts(userId).FirstOrDefault(b => b.Balance - amount >= 0);
            var creditCard = this.BankService.FindCreditCards(userId).FirstOrDefault(c => c.MoneyOwed + amount < c.Limit);

            var sb = new StringBuilder();
            sb.AppendLine($"User: {user.FirstName} {user.LastName}");
            sb.AppendLine($"Operation: Withdraw");
            sb.AppendLine($"Withdraw Amount: {amount:f2}");

            if (bankAccount != null)
            {
                sb.AppendLine($"Your balance is: {bankAccount.Balance}");

                this.BankService.Withdraw(bankAccount, null, amount);

                sb.AppendLine($"Operation success!");
                sb.AppendLine($"Your new balance is: {bankAccount.Balance}");

                return sb.ToString().TrimEnd();
            }

            if (creditCard != null)
            {
                sb.AppendLine($"Your limit is: {creditCard.LimitLeft}");
                this.BankService.Withdraw(null, creditCard, amount);
                sb.AppendLine($"Operation success!");
                sb.AppendLine($"Your money owed is: {creditCard.MoneyOwed + amount}");

                return sb.ToString().TrimEnd();
            }


            sb.AppendLine($"Not enough money in your accounts!");

            return sb.ToString().TrimEnd();
        }
    }
}
