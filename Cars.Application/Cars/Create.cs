using Cars.Domain;
using Cars.Infrastructure;
using FluentValidation;
using MediatR;

namespace Cars.Application.Cars
{
    public class Create
    {
        public class Query : IRequest<Result<Unit>>
        {
            public required Car Car { get; set; }
        }
        public class CommandValidator : AbstractValidator<Query>
        {
            public CommandValidator() {
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
                _context.Cars.Add(request.Car);

                // Zapisujemy zmiany w bazie danych
                var result = await _context.SaveChangesAsync() > 0;
                if (!result) return Result<Unit>.Failure("Failed to create the carr");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}

