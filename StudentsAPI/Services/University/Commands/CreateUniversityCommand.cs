using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentsAPI.Database;

namespace StudentsAPI.Services.University.Commands
{
    public class CreateUniversityCommand : IRequest<Database.Entities.University>
    {
        public string UniversityName { get; set; }

        public CreateUniversityCommand(string universityName)
        {
            UniversityName = universityName;
        }

        public class CreateUniversityCommandHandler : IRequestHandler<CreateUniversityCommand, Database.Entities.University>
        {
            private readonly IApplicationDbContext _context;

            public CreateUniversityCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Database.Entities.University> Handle(CreateUniversityCommand request,
                CancellationToken cancellationToken)
            {
                var exist = await _context.Universities.AnyAsync(x => x.UniversityName == request.UniversityName,
                    cancellationToken);
                if (exist)
                    throw new Exception("University with given name already exists");

                var university = _context.Universities.Add(new Database.Entities.University()
                {
                    UniversityName = request.UniversityName,
                });

                await _context.SaveChangesAsync();

                return university.Entity;
            }
        }
    }
}
