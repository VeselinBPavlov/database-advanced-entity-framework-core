namespace ProductShop
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    using Data;
    using Models;
    using ViewModels.Export;

    public class StartUp
    {
        public static void Main()
        {
            const string Root = @"D:\Courses\Testing\JSON Processing\Product Shop\ProductShop\Datasets\";
            const string UsersJson = "users.json";
            const string ProductsJson = "products.json";
            const string CategoriesJson = "categories.json";
            const string CategoriesProductsJson = "categories-products.json";

            try
            {
                using (var context = new ProductShopContext())
                {
                    // Database initialize.
                    //context.Database.EnsureCreated();
                    //Console.WriteLine("Database created success!");

                    // Call methods from here.
                    // I. Import data
                    // 01. Import Users
                    //string result = ImportUsers(context, File.ReadAllText(Root + UsersJson));

                    // 02. Import Products
                    //string result = ImportProducts(context, File.ReadAllText(Root + ProductsJson));

                    // 03. Import Categories
                    //string result = ImportCategories(context, File.ReadAllText(Root + CategoriesJson));

                    // 04. Import Categories and Products 
                    //string result = ImportCategoryProducts(context, File.ReadAllText(Root + CategoriesProductsJson));

                    // II. Query and Export Data
                    // 05. Export Products In Range 
                    //string result = GetProductsInRange(context);

                    // 06.Export Sold Products
                    //string result = GetSoldProducts(context);

                    // 07. Export Categories By Products Count 
                    //string result = GetCategoriesByProductsCount(context);

                    // 08. Export Users and Products
                    //string result = GetUsersWithProducts(context);

                    //Console.WriteLine(result);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            User[] users = JsonConvert.DeserializeObject<User[]>(inputJson)
                .Where(u => u.LastName != null && u.LastName.Length >= 3)
                .ToArray();

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Length}";
        }

        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            Product[] products = JsonConvert.DeserializeObject<Product[]>(inputJson)
                .Where(p => p.Name != null && p.Name.Length >= 3)
                .ToArray();

            context.Products.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Length}";
        }

        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            Category[] categories = JsonConvert.DeserializeObject<Category[]>(inputJson)
                .Where(c => c.Name != null && c.Name.Length >= 3 && c.Name.Length <= 15)
                .ToArray();

            context.Categories.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {categories.Length}";
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            CategoryProduct[] categoriesProducts = JsonConvert.DeserializeObject<CategoryProduct[]>(inputJson);

            context.CategoryProducts.AddRange(categoriesProducts);
            context.SaveChanges();

            return $"Successfully imported {categoriesProducts.Length}";
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .Select(p => new ProductViewModel()
                {
                    Name = p.Name,
                    Price = p.Price,
                    Seller = $"{p.Seller.FirstName} {p.Seller.LastName}"
                })
                .OrderBy(p => p.Price)
                .ToList();

            var json = JsonConvert.SerializeObject(products, Formatting.Indented);

            return json;
        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(u => u.ProductsSold.Any(p => p.Buyer != null))
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Select(u => new UserViewModel()
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    SoldProducts = u.ProductsSold
                        .Where(p => p.Buyer != null)
                        .Select(p => new SoldProductViewModel()
                        {
                            Name = p.Name,
                            Price = p.Price,
                            BuyerFirstName = p.Buyer.FirstName,
                            BuyerLastName = p.Buyer.LastName
                        })
                        .ToList()
                })
                .ToList();

            var json = JsonConvert.SerializeObject(users, Formatting.Indented);

            return json;
        }

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories
                .OrderByDescending(c => c.CategoryProducts.Count)
                .Select(c => new CategoryViewModel()
                {
                    Name = c.Name,
                    ProductsCount = c.CategoryProducts.Count,
                    AveragePrice = $"{c.CategoryProducts.Average(p => p.Product.Price):f2}",
                    TotalRevenue = $"{c.CategoryProducts.Sum(p => p.Product.Price)}"
                })
                .ToList();

            var json = JsonConvert.SerializeObject(categories, Formatting.Indented);

            return json;
        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            //var users = context.Users
            //    .Include(u => u.ProductsSold)
            //    .Where(u => u.ProductsSold.Any(p => p.Buyer != null))
            //    .OrderByDescending(u => u.ProductsSold.Count(ps => ps.Buyer != null))
            //    .ThenBy(o => o.LastName)
            //    .ToList();

            //var user = new UserProductsViewModel()
            //{
            //    UserCount = users.Count,
            //    Users = users.Select(u => new UserSoldProductsViewModel()
            //    {
            //        FirstName = u.FirstName,
            //        LastName = u.LastName,
            //        Age = u.Age,
            //        SoldProducts = new SoldProductUserViewModel()
            //        {
            //            Count = u.ProductsSold.Count(ps => ps.Buyer != null),
            //            Products = u.ProductsSold.Where(ps => ps.Buyer != null).Select(p => new ProductSoldUserViewModel()
            //            {
            //                Name = p.Name,
            //                Price = p.Price
            //            })
            //            .ToList()
            //        }
            //    })
            //    .ToList()
            //};

            // UserProductsViewModel, UserSoldProductsViewModel, SoldProductUserViewModel, ProductSoldUserViewModel

            //var json = JsonConvert.SerializeObject(user, Formatting.Indented);

            //return json;

            var filteredUsers =
               context
                   .Users
                   .Where(u => u.ProductsSold.Any(ps => ps.Buyer != null))
                   .OrderByDescending(u => u.ProductsSold.Count(ps => ps.Buyer != null))
                   .Select(u =>
                       new
                       {
                           FirstName = u.FirstName,
                           LastName = u.LastName,
                           Age = u.Age,
                           SoldProducts = new
                           {
                               Count = u.ProductsSold.Count(ps => ps.Buyer != null),
                               Products = u.ProductsSold.Where(ps => ps.Buyer != null)
                                   .Select(ps => new
                                   {
                                       Name = ps.Name,
                                       Price = ps.Price
                                   }).ToArray()
                           }
                       }).ToArray();

            var result = new
            {
                UsersCount = filteredUsers.Length,
                Users = filteredUsers
            };

            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            var json = JsonConvert.SerializeObject(result,
                new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    ContractResolver = contractResolver,
                    NullValueHandling = NullValueHandling.Ignore
                });
            return json;
        }
    }
}