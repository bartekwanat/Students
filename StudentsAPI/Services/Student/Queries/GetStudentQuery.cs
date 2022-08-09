using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentsAPI.Database;

namespace StudentsAPI.Services.Student.Queries
{
    public class GetStudentQuery : IRequest<Database.Entities.Student>
    {
        public Guid Id { get; set; }

        public class GetStudentQueryHandler : IRequestHandler<GetStudentQuery, Database.Entities.Student>
        {
            private readonly IApplicationDbContext _context;

            public GetStudentQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Database.Entities.Student> Handle(GetStudentQuery request,
                CancellationToken cancellationToken)
            {
                var student = await _context.Students
                    .Include(x => x.Universities)
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                return student;
            }
        }
    }
}
