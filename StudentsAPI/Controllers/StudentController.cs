using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentsAPI.Services.Student.Command;

namespace StudentsAPI.Controllers
{
    [ApiController]
    [Route("api/Students")]
    public class StudentController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        [HttpPost]
        public async Task<IActionResult> Create(CreateStudentCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
