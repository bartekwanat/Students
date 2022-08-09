using MediatR;
using StudentsAPI.Database;

namespace StudentsAPI.Services.University.Commands
{
    public class UpdateUniversityCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string UniversityName { get; set; }

        public class UpdateUniversityCommandHandler : IRequestHandler<UpdateUniversityCommand, Guid>
        {
            private readonly IApplicationDbContext _context;

            public UpdateUniversityCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Guid> Handle(UpdateUniversityCommand request, CancellationToken cancellationToken)
            {
                var university = _context.Universities.FirstOrDefault(x => x.Id == request.Id);

                if (university == null) return default;

                university.UniversityName = request.UniversityName;

                _context.Universities.Update(university);
                await _context.SaveChangesAsync();
                return university.Id;
            }
        }
    }
}
