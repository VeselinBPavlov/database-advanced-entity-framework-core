namespace SoftJail.DataProcessor
{

    using Data;
    using System;
    using System.Linq;

    public class Bonus
    {
        public static string ReleasePrisoner(SoftJailDbContext context, int prisonerId)
        {
            string result;

            var prisoner = context.Prisoners
                 .FirstOrDefault(p => p.Id == prisonerId);

            if (prisoner.ReleaseDate == null)
            {
                result = $"Prisoner {prisoner.FullName} is sentenced to life";

                return result;
            }

            prisoner.CellId = null;
            prisoner.ReleaseDate = DateTime.Now;

            context.SaveChanges();

            result = $"Prisoner {prisoner.FullName} released";

            return result;
        }
    }
}
