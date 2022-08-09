using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentsAPI.Database;

namespace StudentsAPI.Services.Student.Command
{
    public class DeleteStudentCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, Guid>
        {
            private readonly IApplicationDbContext _context;

            public DeleteStudentCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Guid> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
            {
                var student = await _context.Students
                    .Where(x => x.Id == request.Id)
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);

                if (student == null)
                    return default;

                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                return student.Id;
            }
        }
    }
}
