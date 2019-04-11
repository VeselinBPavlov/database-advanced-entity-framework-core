namespace Cinema.DataProcessor
{
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    using Newtonsoft.Json;

    using ExportDto;
    using HelperClasses;
    using Data;

    public class Serializer
    {
        public static string ExportTopMovies(CinemaContext context, int rating)
        {
            var movies = context.Movies
                .Where(x => x.Rating >= (double)rating && x.Projections.SelectMany(y => y.Tickets).Any())
                .Select(x => new
                {
                    MovieName = x.Title,
                    Rating = $"{x.Rating:f2}",
                    TotalIncomes = $"{x.Projections.SelectMany(y => y.Tickets).Sum(z => z.Price):f2}",
                    Customers = x.Projections.SelectMany(y => y.Tickets).Select(z => new
                    {
                        FirstName = z.Customer.FirstName,
                        LastName = z.Customer.LastName,
                        Balance = $"{z.Customer.Balance:f2}"
                    })
                    .OrderByDescending(c => c.Balance)
                    .ThenBy(c => c.FirstName)
                    .ThenBy(c => c.LastName)
                    .ToList()
                })
                .Take(10)
                .OrderByDescending(m => double.Parse(m.Rating))
                .ThenByDescending(m => decimal.Parse(m.TotalIncomes))
                .ToList();

            var json = JsonConvert.SerializeObject(movies, Newtonsoft.Json.Formatting.Indented);

            return json;
        }

        public static string ExportTopCustomers(CinemaContext context, int age)
        {
            var customers = context.Customers
                .Where(c => c.Age >= age)
                .OrderByDescending(x => x.Tickets.Sum(t => t.Price))
                .Take(10)
                .Select(c => new ExportCustomerDto
                {
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    SpentMoney = $"{c.Tickets.Sum(t => t.Price):f2}",
                    SpentTime = TimeSpanCalculator.TotalTime(c.Tickets.Select(t => t.Projection.Movie.Duration))
                })
                .ToArray();


            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(ExportCustomerDto[]), new XmlRootAttribute("Customers"));

            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            serializer.Serialize(new StringWriter(sb), customers, namespaces);

            return sb.ToString().TrimEnd();
        }
    }
}