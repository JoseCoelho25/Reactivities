using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Create
    {
        public class Command : IRequest{ //command never return anything unlike Query
            public Activity Activity { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command> {
            public CommandValidator()
            {
                RuleFor(x=>x.Activity).SetValidator(new ActivityValidator());
            }
        }

        public class Handler : IRequestHandler<Command>
        {
        private readonly DataContext _context;
            public Handler(DataContext context)
            {
            _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                _context.Activities.Add(request.Activity); //this only sets the data in memory

                await _context.SaveChangesAsync(); //to save changes in db

                return Unit.Value; 
            }
        }
    }
}