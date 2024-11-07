using Cars.Domain;
using Cars.Infrastructure;
using FluentValidation;
using MediatR;

namespace Cars.Application.Cars
{
    public class Edit
    {
        public class Query : IRequest<Result<Unit>>
        {
            public required Car Car { get; set; }
        }
        public class CommandValidator : AbstractValidator<Query>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Car).SetValidator(new CarValidator());
            }
        }
        public class Handler : IRequestHandler<Query, Result<Unit>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Query request, CancellationToken cancellationToken)
            {
                // pobieramy samochód z bazy danych po id
                var car = await _context.Cars.FindAsync(request.Car.Id);

                // edytujemy wybrane pola obiektu
                car.Brand = request.Car.Brand ?? car.Brand;
                car.Model = request.Car.Model ?? car.Model;
                car.DoorsNumber = request.Car.DoorsNumber;
                car.EngineCapacity = request.Car.EngineCapacity;
                car.FuelType = request.Car.FuelType;
                car.LuggageCapacity = request.Car.LuggageCapacity;
                car.ProductionDate = request.Car.ProductionDate;
                car.CarFuelConsumption = request.Car.CarFuelConsumption;
                car.BodyType = request.Car.BodyType;

                // zapisujemy zmiany
               var result =  await _context.SaveChangesAsync() > 0;
                if (!result) return Result<Unit>.Failure("Failed to update the carr");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}

