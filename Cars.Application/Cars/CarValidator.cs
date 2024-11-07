using Cars.Domain;
using FluentValidation;

namespace Cars.Application.Cars
{
    public class CarValidator:AbstractValidator<Car>
    {
        public CarValidator() {
            RuleFor(x => x.Brand).NotEmpty().WithMessage("Brand is required");
            RuleFor(x => x.Model).NotEmpty().WithMessage("Model is required");
            RuleFor(x => x.DoorsNumber).InclusiveBetween(2, 10).NotEmpty().WithMessage("Doors number is required between 2 and 10");
            RuleFor(x => x.LuggageCapacity).NotEmpty().WithMessage("LuggageCapacity is required");
            RuleFor(x => x.EngineCapacity).NotEmpty().WithMessage("LuggageCapacity is required");
            RuleFor(x => x.FuelType).NotEmpty().WithMessage("FuelType is required");
            RuleFor(x => x.ProductionDate).NotEmpty().WithMessage("ProductionDate is required");
            RuleFor(x => x.CarFuelConsumption).NotEmpty().WithMessage("CarFuelConsumption is required");
            RuleFor(x => x.BodyType).NotEmpty().WithMessage("BodyType is required");
        }   
    }
}
