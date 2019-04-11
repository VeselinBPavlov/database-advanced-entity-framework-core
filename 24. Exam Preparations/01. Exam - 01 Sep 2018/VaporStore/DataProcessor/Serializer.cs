namespace VaporStore.DataProcessor
{
	using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Newtonsoft.Json;
    using VaporStore.Data.Models;
    using VaporStore.DataProcessor.Export;

    using Data;

    public static class Serializer
	{
		public static string ExportGamesByGenres(VaporStoreDbContext context, string[] genreNames)
		{
            var genres = context.Genres
                .Where(g => genreNames.Contains(g.Name))                
                .Select(g => new
                {
                    Id = g.Id,
                    Genre = g.Name,
                    Games = g.Games
                        .Where(m => m.Purchases.Any())
                        .Select(m => new
                        {
                            Id = m.Id,
                            Title = m.Name,
                            Developer = m.Developer.Name,
                            Tags = string.Join(", ", m.GameTags.Select(t => t.Tag.Name)),
                            Players = m.Purchases.Count   
                        })
                        .OrderByDescending(x => x.Players)
                        .ThenBy(x => x.Id)
                        .ToList(),
                    TotalPlayers = g.Games.Sum(p => p.Purchases.Count)
                })
                .OrderByDescending(g => g.TotalPlayers)
                .ThenBy(g => g.Id)
                .ToList();

            var json = JsonConvert.SerializeObject(genres, Newtonsoft.Json.Formatting.Indented);

            return json;
		}

		public static string ExportUserPurchasesByType(VaporStoreDbContext context, string storeType)
		{
            var users = context.Users
                .Select(u => new ExportUserPurchasesDto
                {
                    Username = u.Username,
                    Purchases = u.Cards
                    .SelectMany(p => p.Purchases)
                    .Where(t => t.Type == Enum.Parse<PurchaseType>(storeType))
                    .Select(c => new ExportPurchaseDto
                    {
                        Card = c.Card.Number,
                        Cvc = c.Card.Cvc,
                        Date = c.Date.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                        Game = new ExportGameDto
                        {
                            Title = c.Game.Name,
                            Genre = c.Game.Genre.Name,
                            Price = c.Game.Price
                        }
                    })
                    .OrderBy(x => x.Date)
                    .ToArray(),
                    TotalSpent = u.Cards.SelectMany(p => p.Purchases).Where(x => x.Type == Enum.Parse<PurchaseType>(storeType)).Sum(p => p.Game.Price)
                })
                .Where(x => x.Purchases.Any())
                .OrderByDescending(x => x.TotalSpent)
                .ThenBy(x => x.Username)
                .ToArray();


            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(ExportUserPurchasesDto[]), new XmlRootAttribute("Users"));

            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            serializer.Serialize(new StringWriter(sb), users, namespaces);

            return sb.ToString().TrimEnd();
        }
	}
}