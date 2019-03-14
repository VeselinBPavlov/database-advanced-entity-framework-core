namespace BankPaymentSystem.Initializer.EntitiesInitializers
{
    using BillPaymentSystem.Models;

    public class UserInitializer
    {
        public static User[] GetUsers()
        {
            User[] users = new User[]
            {
                new User() { FirstName = "Pesho", LastName = "Peshev", Email = "pesho@peshev.bg", Password = "123456"},
                new User() { FirstName = "Gosho", LastName = "Goshev", Email = "gosho@goshev.bg", Password = "123456"},
                new User() { FirstName = "Sasho", LastName = "Sashev", Email = "sasho@sashev.bg", Password = "123456"},
                new User() { FirstName = "Tosho", LastName = "Toshev", Email = "tosho@toshev.bg", Password = "123456"},
                new User() { FirstName = "Misho", LastName = "Mishev", Email = "misho@mishev.bg", Password = "123456"}
            };

            return users;
        }
    }
}
