using Cars.Domain;
using Cars.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;



namespace Cars.Application.Cars
{
    public class List
    {
        // zapytanie zwraca listę obiektów typu Car
        public class Query : IRequest<List<Car>> { }

        public class Handler : IRequestHandler<Query, List<Car>>
        {
            // przekazujemy kontekst danych
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<Car>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Cars.ToListAsync();
            }
        }
    }
}