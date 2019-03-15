namespace BookShop
{
    using System;
    using System.Linq;
    using BookShop.Models.Enums;
    using Data;
    using Initializer;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
                //For first start - change connection string and start it.
                //DbInitializer.ResetDatabase(db);

                //01. Age Restriction
                //string command = Console.ReadLine().ToLower();
                //var result = GetBooksByAgeRestriction(db, command);

                //02. GoldenBooks.
                //var result = GetGoldenBooks(db);

                //03. Books by Price
                //var result = GetBooksByPrice(db);

                //04. Not Released In
                //int year = int.Parse(Console.ReadLine());
                //var result = GetBooksNotReleasedIn(db, year);

                //05. Book Titles by Category 
                string input = Console.ReadLine();
                var result = GetBooksByCategory(db, input);


                Console.WriteLine(result);
            }
        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var categories = input.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(c => c.ToLower())
                .ToArray();

            var books = context.Books
                .Where(b => b.BookCategories.Any(c => categories.Contains(c.Category.Name.ToLower())))
                .Select(b => b.Title)
                .OrderBy(t => t)
                .ToList();

            var result = string.Join(Environment.NewLine, books);

            return result;
        }

        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToList();

            var result = string.Join(Environment.NewLine, books);

            return result;
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.Price > 40)
                .OrderByDescending(b => b.Price)
                .Select(b => $"{b.Title} - ${b.Price:f2}")
                .ToList();

            var result = string.Join(Environment.NewLine, books);

            return result;
        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.EditionType == EditionType.Gold && b.Copies < 5000)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)                
                .ToList();

            var result = string.Join(Environment.NewLine, books);

            return result;
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            int ageRestriction = -1;

            switch (command.ToLower())
            {
                case "minor": ageRestriction = 0; break;
                case "teen": ageRestriction = 1; break;
                case "adult": ageRestriction = 2; break;
            }

            var books = context.Books
                .Where(b => (int)b.AgeRestriction == ageRestriction)
                .Select(b => b.Title)
                .OrderBy(b => b)
                .ToList();

            var result = string.Join(Environment.NewLine, books);

            return result;
        }
    }
}
