using System.ComponentModel.DataAnnotations;
using System.Linq;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentsAPI.Database;
using StudentsAPI.Database.Entities;

namespace StudentsAPI.Services.Student.Command
{
    public class CreateStudentCommand : IRequest<Database.Entities.Student>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirthday { get; set; }
        public List<Guid> UniversityIds { get; set; }



        public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Database.Entities.Student>
        {
            private readonly IApplicationDbContext _context;

            public CreateStudentCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Database.Entities.Student> Handle(CreateStudentCommand request,
                CancellationToken cancellationTokene)
            {
                var student = new Database.Entities.Student();
                student.FirstName = request.FirstName;
                student.LastName = request.LastName;
                student.DateOfBirth = request.DateOfBirthday;
                student.UniversityStudents = new List<UniversityStudents>();

                var universitiesToAdd = _context.Universities
                    .AsTracking()
                    .Where(x => request.UniversityIds.Contains(x.Id))
                    .ToList();

                student.UniversityStudents = universitiesToAdd
                    .Select(universityId => new UniversityStudents()
                        {UniversityId = universityId.Id, Student = student})
                    .ToList();

                var result = _context.Students.Add(student).Entity;
                await _context.SaveChangesAsync();

                return result;
            }
        }
    }
}