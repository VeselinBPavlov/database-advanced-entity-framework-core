using BillPaymentSystem.App.Models;
using BillPaymentSystem.Models;

namespace BillPaymentSystem.App.Core.Services.Contracts
{
    public interface IUserService
    {
        UserAccounts FindUserData(int userId);
        User FindUser(int userId);
    }
}
