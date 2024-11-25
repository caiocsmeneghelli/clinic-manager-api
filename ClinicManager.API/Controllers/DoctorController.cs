using ClinicManager.Application.Commands.Doctors.CreateDoctor;
using ClinicManager.Application.Queries.Doctors.GetAll;
using ClinicManager.Application.Queries.Doctors.GetDoctorById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DoctorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllDoctorsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetDoctorByIdQuery(id);
            var result = await _mediator.Send(query);
            if(result.IsSuccess == false)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDoctorCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess == false)
            {
                return BadRequest(result);
            }

            return Created();
        }
    }
}
