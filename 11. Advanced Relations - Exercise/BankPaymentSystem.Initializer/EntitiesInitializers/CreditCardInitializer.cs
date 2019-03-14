namespace BankPaymentSystem.Initializer.EntitiesInitializers
{
    using System;

    using BillPaymentSystem.Models;

    public class CreditCardInitializer
    {
        public static CreditCard[] GetCreditCards()
        {
            CreditCard[] creditCards = new CreditCard[]
            {
                 new CreditCard() { Limit = 1000m, MoneyOwed = 500m, ExpirationDate = DateTime.ParseExact("05.06.2020", "dd.MM.yyyy", null) },
                 new CreditCard() { Limit = 2000m, MoneyOwed = 100m, ExpirationDate = DateTime.ParseExact("10.07.2021", "dd.MM.yyyy", null) },
                 new CreditCard() { Limit = 2500m, MoneyOwed = 2000m, ExpirationDate = DateTime.ParseExact("15.08.2022", "dd.MM.yyyy", null) },
                 new CreditCard() { Limit = 3000m, MoneyOwed = 1800m, ExpirationDate = DateTime.ParseExact("20.09.2023", "dd.MM.yyyy", null) },
                 new CreditCard() { Limit = 3500m, MoneyOwed = 2200m, ExpirationDate = DateTime.ParseExact("25.10.2024", "dd.MM.yyyy", null) }
            };

            return creditCards;
        }
    }
}
