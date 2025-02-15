using ClinicManager.Application.Commands.Doctors.Cancel;
using ClinicManager.Application.Commands.Doctors.Create;
using ClinicManager.Application.Commands.Doctors.Update;
using ClinicManager.Application.Commands.Doctors.UpdatePersonalDetail;
using ClinicManager.Application.Queries.Doctors.GetAll;
using ClinicManager.Application.Queries.Doctors.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
            if (result.IsSuccess == false)
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

        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> Cancel(int id)
        {
            var command = new CancelDoctorCommand(id);
            var result = await _mediator.Send(command);

            if (result.IsSuccess == false)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoctor([FromRoute]int id, UpdateDoctorCommand command)
        {
            command.IdDoctor = id;
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                if (result.StatusCode == (int)HttpStatusCode.NotFound) { return NotFound(result); }

                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("personalDetail/{id}")]
        public async Task<IActionResult> UpdatePersonalDetail([FromRoute]int id, UpdatePersonalDetailDoctorCommand command)
        {
            command.IdDoctor = id;
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                if (result.StatusCode == (int)HttpStatusCode.NotFound) { return NotFound(result); }
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
