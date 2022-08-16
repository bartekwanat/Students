using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentsAPI.Database.Entities;
using StudentsAPI.Models;
using StudentsAPI.Services.Student.Command;
using StudentsAPI.Services.Student.Queries;
using StudentsAPI.Services.Student.Query;

namespace StudentsAPI.Controllers
{
    [ApiController]
    [Route("api/Students")]
    public class StudentController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationModel pagination, [FromQuery] SearchModel search)
        {
            var result = await Mediator.Send(new GetAllStudentQuery {Pagination = pagination, SearchPhrase = search});

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await Mediator.Send(new GetStudentQuery() { Id = id });

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStudentCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteStudentCommand { Id = id }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateStudentCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            return Ok(await Mediator.Send(command));
        }

        [HttpPut("{id}/AddUniversity")]
        public async Task<IActionResult> AddUniversity(Guid id, UpdateStudentCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            return Ok(await Mediator.Send(command));
        }
    }
}
