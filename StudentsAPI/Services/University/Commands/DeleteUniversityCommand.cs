using MediatR;
using StudentsAPI.Database;

namespace StudentsAPI.Services.University.Commands
{
    public class DeleteUniversityCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
       
        public class DeleteUniversityCommandHandler : IRequestHandler<DeleteUniversityCommand, Guid>
        {
            private readonly IApplicationDbContext _context;
            public DeleteUniversityCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Guid> Handle(DeleteUniversityCommand request, CancellationToken cancellationToken)
            {
                var university = _context.Universities.FirstOrDefault(x => x.Id == request.Id);

                if (university == null) return default;


                _context.Universities.Remove(university);
                await _context.SaveChangesAsync();

                return university.Id;
            }
        }
    }
}
