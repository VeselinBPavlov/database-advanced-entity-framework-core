namespace BillPaymentSystem.App.Core.Services
{
    using System.Linq;

    using BillPaymentSystem.App.Core.Attributes;
    using BillPaymentSystem.App.Core.Services.Contracts;
    using BillPaymentSystem.App.Models;
    using BillPaymentSystem.Data;
    using BillPaymentSystem.Models;
    using BillPaymentSystem.Models.Enums;

    public class UserService : IUserService
    {
        [Inject]
        public BillPaymentSystemContext Context { get; }

        public UserService(BillPaymentSystemContext context)
        {
            this.Context = context;
        }

        public UserAccounts FindUserData(int userId)
        {
            var user = Context.Users
                .Where(u => u.UserId == userId)
                .Select(u => new UserAccounts
                {
                    Name = u.FirstName + " " + u.LastName,
                    BankAccounts = u.PaymentMethods
                                    .Where(pm => pm.Type == PaymentType.BankAccount)
                                    .Select(pm => pm.BankAccount)
                                    .ToArray(),
                    CreditCards = u.PaymentMethods
                                   .Where(pm => pm.Type == PaymentType.CreditCard)
                                   .Select(pm => pm.CreditCard)
                                   .ToArray()
                })
                .FirstOrDefault();

            return user;
        }

        public User FindUser(int userId)
        {
            var user = Context.Users.FirstOrDefault(u => u.UserId == userId);

            return user;
        }
    }
}
