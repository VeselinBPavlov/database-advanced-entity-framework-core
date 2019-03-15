namespace BillPaymentSystem.App.Core.Services.Contracts
{
    using BillPaymentSystem.App.Models;
    using BillPaymentSystem.Models;

    public interface IUserService
    {
        UserAccounts FindUserData(int userId);
        User FindUser(int userId);
    }
}
