namespace BankPaymentSystem.Initializer
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BillPaymentSystem.Data;
    using EntitiesInitializers;

    public class Initializer
    {
        public static void Seed(BillPaymentSystemContext context)
        {
            InsertUsers(context);
            InserCreditCards(context);
            InsertBankAccounts(context);
            InsertPaymentMethods(context);
        }

        private static void InsertUsers(BillPaymentSystemContext context)
        {
            var users = UserInitializer.GetUsers();

            for (int i = 0; i < users.Length; i++)
            {
                if (IsValid(users[i]))
                {
                    context.Users.Add(users[i]);
                }
            }

            context.SaveChanges();
        }

        private static void InserCreditCards(BillPaymentSystemContext context)
        {
            var creditCards = CreditCardInitializer.GetCreditCards();

            for (int i = 0; i < creditCards.Length; i++)
            {
                if (IsValid(creditCards[i]))
                {
                    context.CreditCards.Add(creditCards[i]);
                }
            }

            context.SaveChanges();
        }

        private static void InsertBankAccounts(BillPaymentSystemContext context)
        {
            var bankAccounts = BankAccountInitializer.GetBankAccounts();

            for (int i = 0; i < bankAccounts.Length; i++)
            {
                if (IsValid(bankAccounts[i]))
                {
                    context.BankAccounts.Add(bankAccounts[i]);
                }
            }

            context.SaveChanges();
        }

        private static void InsertPaymentMethods(BillPaymentSystemContext context)
        {
            var patymentMethods = PaymentMethodInitializer.GetPaymentMethods();

            for (int i = 0; i < patymentMethods.Length; i++)
            {
                if (IsValid(patymentMethods[i]))
                {
                    context.PaymentMethods.Add(patymentMethods[i]);
                }
            }

            context.SaveChanges();
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var result = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, result, true);
        }
    }
}
