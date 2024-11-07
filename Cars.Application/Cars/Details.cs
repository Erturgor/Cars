using Cars.Domain;
using Cars.Infrastructure;
using MediatR;

namespace Cars.Application.Cars
{
    public class Details
    {
        public class Query : IRequest<Result<Car>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<Car>>
        {
            // przekazujemy kontekst danych
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<Car>> Handle(Query request, CancellationToken cancellationToken)
            {
                var car = await _context.Cars.FindAsync(request.Id);
                return Result<Car>.Success(car);
            }
        }
    }
}