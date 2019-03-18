namespace BookShop
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    using Models.Enums;
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
                //string result = GetBooksByAgeRestriction(db, command);

                //02. GoldenBooks.
                //string result = GetGoldenBooks(db);

                //03. Books by Price
                //string result = GetBooksByPrice(db);

                //04. Not Released In
                //int year = int.Parse(Console.ReadLine());
                //string result = GetBooksNotReleasedIn(db, year);

                //05. Book Titles by Category 
                //string input = Console.ReadLine();
                //string result = GetBooksByCategory(db, input);

                //06. Released Before Date 
                //string date = Console.ReadLine();
                //string result = GetBooksReleasedBefore(db, date);

                //07. Author Search 
                //string input = Console.ReadLine();
                //string result = GetAuthorNamesEndingIn(db, input);

                //08. Book Search
                //string input = Console.ReadLine();
                //string result = GetBookTitlesContaining(db, input);

                //09. Book Search by Author 
                //string input = Console.ReadLine();
                //string result = GetBooksByAuthor(db, input);

                //10. Count Books 
                //int lengthCheck = int.Parse(Console.ReadLine());
                //int result = CountBooks(db, lengthCheck);

                //11. Total Book Copies 
                //string result = CountCopiesByAuthor(db);

                //12. Profit by Category 
                //string result = GetTotalProfitByCategory(db);

                //13. Most Recent Books 
                //string result = GetMostRecentBooks(db);

                //14. Increase Prices
                //IncreasePrices(db);

                //15.Remove Books
                //int result = RemoveBooks(db);

                //Console.WriteLine(result);
            }
        }

        public static int RemoveBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.Copies < 4200)
                .ToArray();

            context.RemoveRange(books);

            context.SaveChanges();

            return books.Length;
        }

        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Year < 2010)
                .ToList();

            books.ForEach(b => b.Price += 5);

            context.SaveChanges();
        }

        public static string GetMostRecentBooks(BookShopContext context)
        {
            var books = context.Categories
                .OrderBy(c => c.Name)
                .Select(c => $"--{c.Name}{Environment.NewLine}{string.Join(Environment.NewLine, c.CategoryBooks.OrderByDescending(cb => cb.Book.ReleaseDate).Take(3).Select(cb => $"{cb.Book.Title} ({cb.Book.ReleaseDate.Value.Year})"))}")
                .ToList();

            var result = string.Join(Environment.NewLine, books);

            return result;
        }

        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var books = context.Categories
                .OrderByDescending(c => c.CategoryBooks.Sum(x => x.Book.Price * x.Book.Copies))
                .ThenBy(c => c.Name)
                .Select(c => $"{c.Name} ${c.CategoryBooks.Sum(x => x.Book.Price * x.Book.Copies):f2}")
                .ToList();

            var result = string.Join(Environment.NewLine, books);

            return result;
        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var authors = context.Authors
                .OrderByDescending(a => a.Books.Sum(x => x.Copies))
                .Select(a => $"{a.FirstName} {a.LastName} - {a.Books.Sum(x => x.Copies)}")
                .ToList();

            var result = string.Join(Environment.NewLine, authors);

            return result;
        }

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var result = context.Books
                .Where(b => b.Title.Length > lengthCheck)
                .Count();

            return result;
        }

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var books = context.Books
                .Where(b => EF.Functions.Like(b.Author.LastName, $"{input.ToLower()}%"))
                .OrderBy(b => b.BookId)
                .Select(b => $"{b.Title} ({b.Author.FirstName} {b.Author.LastName})")
                .ToList();

            var result = string.Join(Environment.NewLine, books);

            return result;
        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var books = context.Books
                .Where(b => EF.Functions.Like(b.Title, $"%{input.ToLower()}%"))
                .OrderBy(b => b.Title)
                .Select(b => b.Title)
                .ToList();

            var result = string.Join(Environment.NewLine, books);

            return result;
        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var authors = context.Authors
                .Where(a => EF.Functions.Like(a.FirstName, $"%{input}"))
                .OrderBy(a => a.FirstName)
                .ThenBy(a => a.LastName)
                .Select(a => $"{a.FirstName} {a.LastName}")
                .ToList();

            var result = string.Join(Environment.NewLine, authors);

            return result;
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            var books = context.Books
                .Where(b => b.ReleaseDate < DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture))
                .OrderByDescending(b => b.ReleaseDate)
                .Select(b => $"{b.Title} - {b.EditionType} - ${b.Price:f2}")
                .ToList();

            var result = string.Join(Environment.NewLine, books);

            return result;
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
