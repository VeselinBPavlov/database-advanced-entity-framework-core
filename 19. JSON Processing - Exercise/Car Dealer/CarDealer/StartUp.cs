namespace CarDealer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using AutoMapper;
    using CarDealer.Data;
    using CarDealer.DTO;
    using CarDealer.Models;
    using Newtonsoft.Json;

    public class StartUp
    {
        public static void Main()
        {
            const string Root = @"D:\Courses\Testing\JSON Processing\Car Dealer\CarDealer\Datasets\";
            const string CarsJson = "cars.json";
            const string CustomersJson = "customers.json";
            const string PartsJson = "parts.json";
            const string SalesJson = "sales.json";
            const string SuppliersJson = "suppliers.json";

            try
            {
                using (var context = new CarDealerContext())
                {
                    // Database initialize.
                    //context.Database.EnsureCreated();
                    //Console.WriteLine("Database created successfully!");

                    // III. Import data
                    // 09. ImportSuppliers
                    //string result = ImportSuppliers(context, File.ReadAllText(Root + SuppliersJson));

                    // 10. Import Parts
                    //string result = ImportParts(context, File.ReadAllText(Root + PartsJson));

                    // 11. Import Cars
                    //string result = ImportCars(context, File.ReadAllText(Root + CarsJson));

                    // 12. Import Customers
                    //string result = ImportCustomers(context, File.ReadAllText(Root + CustomersJson));

                    // 13. Import Sales
                    //string result = ImportSales(context, File.ReadAllText(Root + SalesJson));

                    // IV. Query and Export Data
                    // 14. Export Ordered Customers 
                    //string result = GetOrderedCustomers(context);

                    // 15. Export Cars From Make Toyota 
                    //string result = GetCarsFromMakeToyota(context);

                    // 16. Export Local Suppliers
                    //string result = GetLocalSuppliers(context);

                    // 17.Export Cars With Their List Of Parts
                    //string result = GetCarsWithTheirListOfParts(context);

                    // 18. Export Total Sales By Customer
                    //string result = GetTotalSalesByCustomer(context);

                    // 19. Export Sales With Applied Discount
                    //string result = GetSalesWithAppliedDiscount(context);

                    //Console.WriteLine(result);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }            
        }

        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context.Sales
                .Take(10)
                .Select(s => new
                {
                    car = new
                    {
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                        TravelledDistance = s.Car.TravelledDistance
                    },
                    customerName = s.Customer.Name,
                    Discount = $"{s.Discount:F2}",
                    price = $"{s.Car.PartCars.Sum(p => p.Part.Price):F2}",
                    priceWithDiscount = $"{(s.Car.PartCars.Sum(p => p.Part.Price) - (s.Car.PartCars.Sum(p => p.Part.Price) * (s.Discount / 100))):F2}"
                })
                .ToList();

            var json = JsonConvert.SerializeObject(sales, Formatting.Indented);

            return json;
        }

        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.Customers
                .Where(c => c.Sales.Any())
                .Select(c => new
                {
                    fullName = c.Name,
                    boughtCars = c.Sales.Count,
                    spentMoney = c.Sales.Sum(y => y.Car.PartCars.Sum(z => z.Part.Price))
                })
                .OrderByDescending(c => c.spentMoney)
                .ThenByDescending(c => c.boughtCars)
                .ToList();

            var json = JsonConvert.SerializeObject(customers, Formatting.Indented);

            return json;
        }

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context.Cars
                .Select(c => new
                {
                    car = new
                    {
                        Make = c.Make,
                        Model = c.Model,
                        TravelledDistance = c.TravelledDistance
                    },
                    parts = c.PartCars.Select(pc => new
                    {
                        Name = pc.Part.Name,
                        Price = $"{pc.Part.Price:f2}"
                    })
                    .ToList()
                })
                .ToList();
            
            var json = JsonConvert.SerializeObject(cars, Formatting.Indented);

            return json;
        }

        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers
                .Where(s => s.IsImporter == false)
                .Select(s => new
                {
                    Id = s.Id,
                    Name = s.Name,
                    PartsCount = s.Parts.Count
                })
                .ToList();

            var json = JsonConvert.SerializeObject(suppliers, Formatting.Indented);

            return json;
        }

        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var toyotaCars = context.Cars
                .Where(c => c.Make == "Toyota")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .Select(c => new
                {
                    c.Id,
                    c.Make,
                    c.Model,
                    c.TravelledDistance
                })
                .ToList();

            var json = JsonConvert.SerializeObject(toyotaCars, Formatting.Indented);

            return json;
        }

        public static string GetOrderedCustomers(CarDealerContext context)
        {
            var customers = context.Customers
                .OrderBy(c => c.BirthDate)
                .ThenBy(c => c.IsYoungDriver)
                .Select(c => new
                {
                    Name = c.Name,
                    BirthDate = c.BirthDate.ToString("dd/MM/yyyy"),
                    IsYoungDriver = c.IsYoungDriver
                })
                .ToList();

            var json = JsonConvert.SerializeObject(customers, Formatting.Indented);

            return json;
        }

        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            var sales = JsonConvert.DeserializeObject<Sale[]>(inputJson);

            context.Sales.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Length}.";
        }

        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            var customers = JsonConvert.DeserializeObject<Customer[]>(inputJson);

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Length}.";
        }

        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var cars = JsonConvert.DeserializeObject<CarImportDto[]>(inputJson);
            var mappedCars = new HashSet<Car>();

            foreach (var car in cars)
            {
                var mappedCar = new Car()
                {
                    Make = car.Make,
                    Model = car.Model,
                    TravelledDistance = car.TravelledDistance
                };


                var partsIds = car.PartsId.Distinct().ToHashSet();

                if (partsIds != null)
                {
                    foreach (var partId in partsIds)
                    {
                        var partCar = new PartCar()
                        {
                            CarId = car.Id,
                            PartId = partId
                        };

                        mappedCar.PartCars.Add(partCar);
                    }
                }      
                
                mappedCars.Add(mappedCar);
            }

            context.Cars.AddRange(mappedCars);
            context.SaveChanges();

            return $"Successfully imported {mappedCars.Count}.";
        }

        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            var existingSuppliers = context.Suppliers
                .Select(s => s.Id)
                .ToArray();

            var parts = JsonConvert.DeserializeObject<Part[]>(inputJson)
                .Where(p => existingSuppliers.Contains(p.SupplierId))
                .ToArray();

            context.Parts.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Length}.";
        }

        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var suppliers = JsonConvert.DeserializeObject<Supplier[]>(inputJson);

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Length}.";
        }
    }
}