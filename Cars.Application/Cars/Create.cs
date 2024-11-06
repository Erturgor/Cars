using Cars.Domain;
using Cars.Infrastructure;
using MediatR;

namespace Cars.Application.Cars
{
    public class Create
    {
        public class Query : IRequest<Car>
        {
            public required Car Car { get; set; }
        }

        public class Handler : IRequestHandler<Query, Car>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Car> Handle(Query request, CancellationToken cancellationToken)
            {
                // pobieramy samochód z bazy danych po id
                await _context.Cars.AddAsync(request.Car);

                // Zapisujemy zmiany w bazie danych
                await _context.SaveChangesAsync();

                return request.Car;
            }
        }
    }
}

