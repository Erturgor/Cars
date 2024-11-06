using Cars.Domain;
using Cars.Infrastructure;
using MediatR;

namespace Cars.Application.Cars
{
    public class Edit
    {
        public class Query : IRequest<Unit>
        {
            public required Car Car { get; set; }
        }

        public class Handler : IRequestHandler<Query, Unit>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Query request, CancellationToken cancellationToken)
            {
                // pobieramy samochód z bazy danych po id
                var car = await _context.Cars.FindAsync(request.Car.Id);

                // edytujemy wybrane pola obiektu
                car.Brand = request.Car.Brand ?? car.Brand;
                car.Model = request.Car.Model ?? car.Model;
                car.DoorsNumber = request.Car.DoorsNumber;
                car.EngineCapacity = request.Car.EngineCapacity;
                car.FuelType = request.Car.FuelType;
                car.ProductionDate = request.Car.ProductionDate;
                car.CarFuelConsumption = request.Car.CarFuelConsumption;
                car.BodyType = request.Car.BodyType;

                // zapisujemy zmiany
                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}

