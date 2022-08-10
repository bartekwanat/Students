using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using AutoMapper.Configuration.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentsAPI.Database;
using StudentsAPI.Database.Entities;

namespace StudentsAPI.Services.Student.Command
{
    public class UpdateStudentCommand : IRequest<Guid>
    {
        
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirthday { get; set; }

        public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, Guid>
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

                var result = _context.Students.Update(student).Entity;
                await _context.SaveChangesAsync();
                return student.Id;
            }
        }
    }
}
