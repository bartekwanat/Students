using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentsAPI.Database;

namespace StudentsAPI.Services.University.Queries
{
    public class GetAllUniversitiesQuery : IRequest<IEnumerable<Database.Entities.University>>
    {
        public class GetAllUniversitiesQueryHandler : IRequestHandler<GetAllUniversitiesQuery,
            IEnumerable<Database.Entities.University>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllUniversitiesQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Database.Entities.University>> Handle(GetAllUniversitiesQuery request,
                CancellationToken cancellationToken)
            {
                var universitiesList = await _context.Universities
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

                return universitiesList?.AsReadOnly();
            }
        }

    }

}
