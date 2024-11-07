using Cars.Domain;
using Cars.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Cars.Application.Cars
{
    public class Delete
    {
        public class Query : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
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
                var result = await _context.SaveChangesAsync()>0;
                if (!result) return Result<Unit>.Failure("Failed to delete the carr");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
