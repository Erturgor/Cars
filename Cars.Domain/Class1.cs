namespace Cars.Domain
{
    public enum FuelType { Petrol, Hybrid, Diesel, LPG }

    public enum BodyType { Hatchback, Sedan, Kombi, SUV, Roadster }

    public class Car
    {
        public Guid Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int DoorsNumber { get; set; }
        public int LuggageCapacity { get; set; }
        public int EngineCapacity { get; set; }
        public FuelType FuelType { get; set; }
        public DateTime ProductionDate { get; set; }
        public double CarFuelConsumption { get; set; }
        public BodyType BodyType { get; set; }
    }
}
