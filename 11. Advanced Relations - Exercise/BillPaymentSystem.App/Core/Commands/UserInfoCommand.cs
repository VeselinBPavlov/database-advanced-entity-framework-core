namespace BillPaymentSystem.App.Core.Commands
{
    using System;
    using System.Linq;
    using System.Text;

    using BillPaymentSystem.App.Core.Attributes;
    using BillPaymentSystem.App.Core.Services.Contracts;
  
    public class UserInfoCommand : Command
    {
        [Inject]
        public IUserService UserService { get; }

        public UserInfoCommand(string[] data, IUserService userService)
            : base(data)
        {
            this.UserService = userService;
        }

        public override string Execute()
        {
            var user = this.UserService.FindUserData(int.Parse(Data[0]));

            if (user == null)
            {
                throw new ArgumentNullException("User not found!");
            }

            var sb = new StringBuilder();
            sb.AppendLine($"User: {user.Name}");

            if (user.BankAccounts.Any())
            {
                sb.AppendLine("Bank Accounts:");
                foreach (var ba in user.BankAccounts)
                {
                    sb.AppendLine($"-- ID: {ba.BankAccountId}");
                    sb.AppendLine($"--- Balance: {ba.Balance:f2}");
                    sb.AppendLine($"--- Bank: {ba.BankName}");
                    sb.AppendLine($"--- SWIFT: {ba.SwiftCode}");
                }
            }

            if (user.CreditCards.Any())
            {
                sb.AppendLine("Credit Cards:");
                foreach (var cc in user.CreditCards)
                {
                    sb.AppendLine($"-- ID: {cc.CreditCardId}");
                    sb.AppendLine($"--- Limit: {cc.Limit:f2}");
                    sb.AppendLine($"--- Money Owed: {cc.MoneyOwed:f2}");
                    sb.AppendLine($"--- Limit Left: {cc.LimitLeft:f2}");
                    sb.AppendLine($"--- Expiration Date: {cc.ExpirationDate.Year.ToString()}/{cc.ExpirationDate.Month.ToString()}");
                }
            }

            return sb.ToString().TrimEnd();
        }
    }
}

