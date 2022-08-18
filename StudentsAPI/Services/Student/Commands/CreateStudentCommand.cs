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
        public List<string> UniversityNames{ get; set; }



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
                student.Universities = new List<Database.Entities.University>();

                var universitiesToAdd = _context.Universities
                    .AsTracking()
                    .Where(x => request.UniversityNames.Contains(x.UniversityName))
                    .ToList();

                student.Universities = universitiesToAdd
                    .Select(universityName => new Database.Entities.University()
                        {UniversityName = universityName.UniversityName})
                    .ToList();

                var result = _context.Students.Add(student).Entity;
                await _context.SaveChangesAsync();

                return result;
            }
        }
    }
}