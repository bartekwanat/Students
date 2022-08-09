using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentsAPI.Models;
using StudentsAPI.Services.Student.Query;
using StudentsAPI.Services.University.Commands;
using StudentsAPI.Services.University.Queries;

namespace StudentsAPI.Controllers
{
    [ApiController]
    [Route("api/University")]
    public class UniversityController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationModel pagination)
        {
            var result = await Mediator.Send(new GetAllUniversitiesQuery(){ Pagination = pagination });

            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await Mediator.Send(new GetUniversityQuery() { Id = id });

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUniversityCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteUniversityCommand() { Id = id }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateUniversityCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            return Ok(await Mediator.Send(command));
        }
    }
}
