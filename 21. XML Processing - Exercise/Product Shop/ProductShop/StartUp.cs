namespace ProductShop
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using ProductShop.Data;
    using ProductShop.Dtos.Export;
    using ProductShop.Dtos.Import;
    using ProductShop.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    public class StartUp
    {
        public static void Main()
        {
            const string Root = @"../../../Datasets/";
            const string UsersXml = "users.xml";
            const string ProductsXml = "products.xml";
            const string CategoriesXml = "categories.xml";
            const string CategoriesProductsXml = "categories-products.xml";

            Mapper.Initialize(x =>
            {
                x.AddProfile<ProductShopProfile>();
            });

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
                    //string result = ImportUsers(context, File.ReadAllText(Root + UsersXml));

                    // 02. Import Products
                    //string result = ImportProducts(context, File.ReadAllText(Root + ProductsXml));

                    // 03. Import Categories
                    //string result = ImportCategories(context, File.ReadAllText(Root + CategoriesXml));

                    // 04. Import Categories and Products 
                    //string result = ImportCategoryProducts(context, File.ReadAllText(Root + CategoriesProductsXml));

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

        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportUserDto[]), new XmlRootAttribute("Users"));

            var usersDto = (ImportUserDto[])xmlSerializer.Deserialize(new StringReader(inputXml));

            var users = new List<User>();

            foreach (var userDto in usersDto)
            {
                var user = Mapper.Map<User>(userDto);

                users.Add(user);
            }

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Count}";
        }

        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportProductDto[]), new XmlRootAttribute("Products"));

            var productsDto = (ImportProductDto[])xmlSerializer.Deserialize(new StringReader(inputXml));

            var products = new List<Product>();

            foreach (var productDto in productsDto)
            {
                var product = Mapper.Map<Product>(productDto);

                products.Add(product);
            }

            context.Products.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Count}";
        }

        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportCategoryDto[]), new XmlRootAttribute("Categories"));

            var categoriesDto = (ImportCategoryDto[])xmlSerializer.Deserialize(new StringReader(inputXml));

            var categories = new List<Category>();

            foreach (var categoryDto in categoriesDto)
            {
                var category = Mapper.Map<Category>(categoryDto);

                categories.Add(category);
            }

            context.Categories.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {categories.Count}";
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportCategoryProductDto[]), new XmlRootAttribute("CategoryProducts"));

            var categoryProductsDto = (ImportCategoryProductDto[])xmlSerializer.Deserialize(new StringReader(inputXml));

            var categoryProducts = new List<CategoryProduct>();

            foreach (var categoryProductDto in categoryProductsDto)
            {
                var categoryProduct = Mapper.Map<CategoryProduct>(categoryProductDto);

                categoryProducts.Add(categoryProduct);
            }

            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Count}";
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .Select(p => new ExportProductsInRangeDto()
                {
                    Name = p.Name,
                    Price = p.Price,
                    Buyer = p.Buyer.FirstName + " " + p.Buyer.LastName ?? p.Buyer.LastName
                })
                .OrderBy(p => p.Price)
                .Take(10)
                .ToArray();

            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(ExportProductsInRangeDto[]), new XmlRootAttribute("Products"));

            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            serializer.Serialize(new StringWriter(sb), products, namespaces);

            return sb.ToString().TrimEnd();
        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(u => u.ProductsSold.Any(p => p.Buyer != null))
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Select(u => new ExportUserSoldProductDto()
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    SoldProducts = u.ProductsSold
                        .Where(p => p.Buyer != null)
                        .Select(p => new ExportSoldProductDto()
                        {
                            Name = p.Name,
                            Price = p.Price
                        })
                        .ToArray()
                })
                .Take(5)
                .ToArray();

            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(ExportUserSoldProductDto[]), new XmlRootAttribute("Users"));

            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            serializer.Serialize(new StringWriter(sb), users, namespaces);

            return sb.ToString().TrimEnd();
        }

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context
                .Categories
                .Select(x => new ExportCategoryByProductCountDto
                {
                    Name = x.Name,
                    ProductCount = x.CategoryProducts.Count,
                    AveragePrice = x.CategoryProducts.Select(a => a.Product.Price).Average(),
                    TotalRevenue = x.CategoryProducts.Select(а => а.Product.Price).Sum()
                })
                .OrderByDescending(x => x.ProductCount)
                .ThenBy(x => x.TotalRevenue)
                .ToArray();

            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(ExportCategoryByProductCountDto[]), new XmlRootAttribute("Categories"));

            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            serializer.Serialize(new StringWriter(sb), categories, namespaces);

            return sb.ToString().TrimEnd();
        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(u => u.ProductsSold.Any())
                .Select(u => new ExportUserAndProductDto
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Age = u.Age,
                    SoldProductDto = new SoldProductDto()
                    {
                        Count = u.ProductsSold.Count(ps => ps.Buyer != null),
                        Products = u.ProductsSold.Where(ps => ps.Buyer != null)
                                   .Select(ps => new ProductDto
                                   {
                                       Name = ps.Name,
                                       Price = ps.Price
                                   })
                                   .OrderByDescending(x => x.Price)
                                   .ToArray()
                    }

                })
                .OrderByDescending(x => x.SoldProductDto.Count)
                .Take(10)
                .ToArray();

            var customExport = new ExportCustomUserProductDto()
            {
                Count = context.Users
                    .Where(u => u.ProductsSold.Any()).Count(),
                ExportUserAndProductDtos = users
            };

            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(ExportCustomUserProductDto), new XmlRootAttribute("Users"));

            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            serializer.Serialize(new StringWriter(sb), customExport, namespaces);

            return sb.ToString().TrimEnd();
        }
    }
}