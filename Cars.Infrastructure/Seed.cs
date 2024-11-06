using Cars.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.Infrastructure
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            // jeśli baza ma jakieś rekordy to nic nie rób
            if (context.Cars.Any()) return;

            var cars = new List<Car>
            {
                new Car
                {
                    Brand = "Mazda",
                    Model = "CX60",
                    DoorsNumber = 5,
                    LuggageCapacity = 570,
                    EngineCapacity = 2488,
                    FuelType = FuelType.Hybrid,
                    ProductionDate = DateTime.UtcNow.AddMonths(-1),
                    CarFuelConsumption = 18.1,
                    BodyType = BodyType.SUV
                },
                new Car
                {
                    Brand = "Renault",
                    Model = "Clio II",
                    DoorsNumber = 5,
                    LuggageCapacity = 300,
                    EngineCapacity = 1149,
                    FuelType = FuelType.Petrol,
                    ProductionDate = DateTime.UtcNow.AddYears(-18),
                    CarFuelConsumption = 7.2,
                    BodyType = BodyType.Hatchback
                },
                new Car
                {
                    Brand = "Volvo",
                    Model = "XC60",
                    DoorsNumber = 5,
                    LuggageCapacity = 600,
                    EngineCapacity = 1969,
                    FuelType = FuelType.Diesel,
                    ProductionDate = DateTime.UtcNow.AddYears(-3),
                    CarFuelConsumption = 10.0,
                    BodyType = BodyType.SUV
                },
                new Car
                {
                    Brand = "Kia",
                    Model = "Ceed II",
                    DoorsNumber = 5,
                    LuggageCapacity = 400,
                    EngineCapacity = 1591,
                    FuelType = FuelType.Petrol,
                    ProductionDate = DateTime.UtcNow.AddYears(-8),
                    CarFuelConsumption = 7.4,
                    BodyType = BodyType.Hatchback
                },
                new Car
                {
                    Brand = "Mazda",
                    Model = "MX-5",
                    DoorsNumber = 2,
                    LuggageCapacity = 130,
                    EngineCapacity = 1496,
                    FuelType = FuelType.Petrol,
                    ProductionDate = DateTime.UtcNow.AddYears(-5),
                    CarFuelConsumption = 10.1,
                    BodyType = BodyType.Roadster
                },
                new Car
                {
                    Brand = "Toyota",
                    Model = "Corolla XII",
                    DoorsNumber = 5,
                    LuggageCapacity = 1000,
                    EngineCapacity = 1197,
                    FuelType = FuelType.Diesel,
                    ProductionDate = DateTime.UtcNow.AddYears(-1),
                    CarFuelConsumption = 9.5,
                    BodyType = BodyType.Kombi
                },
                new Car
                {
                    Brand = "Audi",
                    Model = "A4",
                    DoorsNumber = 5,
                    LuggageCapacity = 685,
                    EngineCapacity = 1781,
                    FuelType = FuelType.Petrol,
                    ProductionDate = DateTime.UtcNow.AddYears(-22),
                    CarFuelConsumption = 8.2,
                    BodyType = BodyType.Sedan
                },
                new Car
                {
                    Brand = "Seat",
                    Model = "Leon IV",
                    DoorsNumber = 5,
                    LuggageCapacity = 800,
                    EngineCapacity = 1395,
                    FuelType = FuelType.Hybrid,
                    ProductionDate = DateTime.UtcNow.AddMonths(-2),
                    CarFuelConsumption = 6.8,
                    BodyType = BodyType.Hatchback
                },
                new Car
                {
                    Brand = "Fiat",
                    Model = "Tipo II",
                    DoorsNumber = 5,
                    LuggageCapacity = 450,
                    EngineCapacity = 1368,
                    FuelType = FuelType.Diesel,
                    ProductionDate = DateTime.UtcNow.AddYears(-8),
                    CarFuelConsumption = 7.4,
                    BodyType = BodyType.Hatchback
                },
                new Car
                {
                    Brand = "Skoda",
                    Model = "Octavia",
                    DoorsNumber = 5,
                    LuggageCapacity = 900,
                    EngineCapacity = 1489,
                    FuelType = FuelType.LPG,
                    ProductionDate = DateTime.UtcNow.AddYears(-10),
                    CarFuelConsumption = 9.9,
                    BodyType = BodyType.Kombi
                }
            };

            // załadowanie danych do pamięci
            await context.Cars.AddRangeAsync(cars);
            // dodanie rekordów do bazy danych
            await context.SaveChangesAsync();
        }
    }
}
