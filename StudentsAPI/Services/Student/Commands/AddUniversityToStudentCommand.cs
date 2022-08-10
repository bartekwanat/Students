using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentsAPI.Database;
using StudentsAPI.Database.Entities;
using StudentsAPI.Services.Student.Command;

namespace StudentsAPI.Services.Student.Commands
{
    public class AddUniversityToStudentCommand
    {
        public Guid Id { get; set; }

        public List<Guid> UniversityIds { get; set; }

        public class AddUniversityToStudentCommandHandler : IRequest<AddUniversityToStudentCommand>
        {
            private readonly IApplicationDbContext _context;

            public AddUniversityToStudentCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Guid> Handle(AddUniversityToStudentCommand request, CancellationToken cancellationToken)
            {
                var student = _context.Students.FirstOrDefault(a => a.Id == request.Id);

                if (student == null) return default;


                var universitiesToAdd = _context.Universities
                    .AsTracking()
                    .Where(x => request.UniversityIds.Contains(x.Id))
                    .ToList();

                student.UniversityStudents = universitiesToAdd
                    .Select(universityId => new UniversityStudents()
                        { UniversityId = universityId.Id, Student = student })
                    .ToList();

                _context.Students.Update(student);
                await _context.SaveChangesAsync();
                return student.Id;
            }
        }
    }
}
