using System.Security.Cryptography.X509Certificates;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentsAPI.Database;

namespace StudentsAPI.Services.University.Queries
{
    public class GetUniversityQuery : IRequest<Database.Entities.University>
    {
        public Guid Id { get; set; }

        public class GetUniversityQueryHandler : IRequestHandler<GetUniversityQuery, Database.Entities.University>
        {
            private readonly IApplicationDbContext _context;

            public GetUniversityQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Database.Entities.University> Handle(GetUniversityQuery request, CancellationToken cancellationToken)
            {
                var university = _context.Universities
                    .Include(x => x.Students)
                    .FirstOrDefault(x => x.Id == request.Id);

                return university;
            }
        }
    }
}
