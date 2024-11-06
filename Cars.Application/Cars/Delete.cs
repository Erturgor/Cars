using Cars.Domain;
using Cars.Infrastructure;
using MediatR;

namespace Cars.Application.Cars
{
    public class Delete
    {
        public class Query : IRequest<Unit>
        {
            public Guid Id { get; set; }
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
                // Znajdujemy samochód w bazie danych po Id
                var car = await _context.Cars.FindAsync(request.Id);

                // Sprawdzamy, czy samochód istnieje
                if (car == null)
                {
                    throw new Exception("Samochod nie został znaleziony");
                }

                // Usuwamy samochód z kontekstu
                _context.Cars.Remove(car);

                // Zapisujemy zmiany w bazie danych
                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
