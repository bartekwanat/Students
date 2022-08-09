using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentsAPI.Database;
using StudentsAPI.Database.Entities;

namespace StudentsAPI.Services.Student.Command
{
    public class UpdateStudentCommand
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirthday { get; set; }
        public List<Guid> UniversityIds { get; set; }

        public class UpdateStudentCommandHandler : IRequest<UpdateStudentCommand>
        {
            private readonly IApplicationDbContext _context;

            public UpdateStudentCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Guid> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
            {
                var student = _context.Students.FirstOrDefault(a => a.Id == request.Id);

                if (student == null) return default;

                student.FirstName = request.FirstName;
                student.LastName = request.LastName;
                student.DateOfBirth = request.DateOfBirthday;

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
