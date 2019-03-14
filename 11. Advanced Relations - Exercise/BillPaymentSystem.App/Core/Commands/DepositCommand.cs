namespace BillPaymentSystem.App.Core.Commands
{
    using System;
    using System.Linq;
    using System.Text;

    using BillPaymentSystem.App.Core.Attributes;
    using BillPaymentSystem.App.Core.Services.Contracts;

    public class DepositCommand : Command
    {
        [Inject]
        public IBankService BankService { get; }

        [Inject]
        public IUserService UserService { get; }

        public DepositCommand(string[] data, IBankService bankService, IUserService userService)
            : base(data)
        {
            this.BankService = bankService;
            this.UserService = userService;
        }

        public override string Execute()
        {
            var userId = int.Parse(Data[0]);
            var amount = decimal.Parse(Data[1]);                     

            var user = this.UserService.FindUser(userId);

            if (user == null)
            {
                throw new ArgumentNullException("User not found!");
            }           

            var bankAccount = this.BankService.FindBankAccounts(userId).FirstOrDefault();

            var sb = new StringBuilder();
            sb.AppendLine($"User: {user.FirstName} {user.LastName}");
            sb.AppendLine($"Operation: Deposit");
            sb.AppendLine($"Deposit Amount: {amount:f2}");

            if (bankAccount != null)
            {
                sb.AppendLine("Account type: Bank Account");
                sb.AppendLine($"Old Balance: {bankAccount.Balance:f2}");

                this.BankService.Deposit(bankAccount, amount);                

                var newBankAmmount = this.BankService.FindBankAccounts(userId).FirstOrDefault().Balance;

                sb.AppendLine("Operation success!");
                sb.AppendLine($"New Balance: {newBankAmmount}");
            }
            else
            {
                sb.AppendLine("Operation failed!");
                sb.AppendLine("Please, create Bank Account to make depisits.");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
