using ClinicManager.API.Extensions;
using ClinicManager.Application.Commands.Doctors.Cancel;
using ClinicManager.Application.Commands.Doctors.Create;
using ClinicManager.Application.Commands.Doctors.Update;
using ClinicManager.Application.Commands.Doctors.UpdateAddress;
using ClinicManager.Application.Commands.Doctors.UpdatePersonalDetail;
using ClinicManager.Application.Helpers;
using ClinicManager.Application.Queries.Doctors.GetAll;
using ClinicManager.Application.Queries.Doctors.GetById;
using ClinicManager.Application.Queries.Doctors.ListAll;
using ClinicManager.Application.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllDoctorsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("list")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ListAll([FromQuery]PageParams pageParams)
        {
            var query = new ListAllDoctorsQuery(pageParams);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Doctor")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetDoctorByIdQuery(id);
            var result = await _mediator.Send(query);
            if (result.IsSuccess == false)
            {
                return NotFound(result);
            }

            var isAdmin = User.IsAdmin();
            if (!isAdmin)
            {
                var doctor = (DoctorViewModel)result.Data;
                var userName = User.UserName();
                if (userName == null || userName != doctor.UserName)
                {
                    return Forbid();
                }
            }
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin, Doctor")]
        public async Task<IActionResult> UpdateDoctor([FromRoute] int id, UpdateDoctorCommand command)
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
        [Authorize(Roles = "Admin, Doctor")]
        public async Task<IActionResult> UpdatePersonalDetail([FromRoute] int id, UpdateDoctorPersonalDetailCommand command)
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

        [HttpPut("address/{id}")]
        [Authorize(Roles = "Admin, Doctor")]
        public async Task<IActionResult> UpdateAddress([FromRoute] int id, UpdateDoctorAddressCommand command)
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
