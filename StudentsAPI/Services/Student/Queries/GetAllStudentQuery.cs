using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StudentsAPI.Database;
using StudentsAPI.Models;

namespace StudentsAPI.Services.Student.Query
{
    public class GetAllStudentQuery : IRequest<IEnumerable<Database.Entities.Student>>
    {
        public PaginationModel Pagination { get; set; }

        public SearchModel SearchPhrase { get; set; } = new SearchModel("");

        public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentQuery, IEnumerable<Database.Entities.Student>>
        {
            private readonly IApplicationDbContext _context;

            public GetAllStudentsQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Database.Entities.Student>> Handle(GetAllStudentQuery request,
                CancellationToken cancellationToken)
            {
               var studentList = await _context.Students
                    .Include(x => x.Universities)
                    .Where(x => request.SearchPhrase.searchPhrase == null ||
                                x.FirstName.ToLower().Contains(request.SearchPhrase.searchPhrase.ToLower()) ||
                                x.LastName.ToLower().Contains(request.SearchPhrase.searchPhrase.ToLower()))
                    .Skip((request.Pagination.ItemsPerPage * request.Pagination.Page))
                    .Take(request.Pagination.ItemsPerPage)
                    .ToListAsync(cancellationToken: cancellationToken);

                return studentList?.AsReadOnly();
            }
        }
    }
}
