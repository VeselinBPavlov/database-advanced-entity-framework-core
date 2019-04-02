namespace ProductShop
{
    using AutoMapper;
    using ProductShop.Data;
    using ProductShop.Dtos.Import;
    using ProductShop.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
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
                    string result = ImportProducts(context, File.ReadAllText(Root + ProductsXml));

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

                    Console.WriteLine(result);
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

        //public static string ImportCategories(ProductShopContext context, string inputJson)
        //{


        //    context.Categories.AddRange(categories);
        //    context.SaveChanges();

        //    return $"Successfully imported {categories.Length}";
        //}

        //public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        //{


        //    return $"Successfully imported {categoriesProducts.Length}";
        //}

        //public static string GetProductsInRange(ProductShopContext context)
        //{




        //    return json;
        //}

        //public static string GetSoldProducts(ProductShopContext context)
        //{


        //    return json;
        //}

        //public static string GetCategoriesByProductsCount(ProductShopContext context)
        //{


        //    return json;
        //}

        //public static string GetUsersWithProducts(ProductShopContext context)
        //{

        //    return json;
        //}
    }
}