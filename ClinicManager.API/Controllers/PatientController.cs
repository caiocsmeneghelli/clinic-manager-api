using ClinicManager.API.Extensions;
using ClinicManager.Application.Commands.Patients.Cancel;
using ClinicManager.Application.Commands.Patients.Create;
using ClinicManager.Application.Commands.Patients.Update;
using ClinicManager.Application.Commands.Patients.UpdateAddress;
using ClinicManager.Application.Commands.Patients.UpdatePersonalDetail;
using ClinicManager.Application.Helpers;
using ClinicManager.Application.Queries.Patients.GetAll;
using ClinicManager.Application.Queries.Patients.GetById;
using ClinicManager.Application.Queries.Patients.ListAll;
using ClinicManager.Application.ViewModel;
using ClinicManager.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace ClinicManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public PatientController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllPatientQuery();
            var result = await _mediatr.Send(query);
            return Ok(result);
        }

        [HttpGet("list")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ListAll([FromQuery] PageParams pageParams)
        {
            var query = new ListAllPatientsQuery() { PageSize = pageParams.PageSize, PageNumber = pageParams.PageNumber };
            var result = await _mediatr.Send(query);
            return Ok(query);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Patient")]
        public async Task<IActionResult> GetById(int id)
        {
            var isAdmin = User.IsAdmin();
            var userName = User.UserName();

            var query = new GetPatientByIdQuery(id);
            var result = await _mediatr.Send(query);

            if (result.IsSuccess == false)
            {
                return NotFound(result);
            }

            // Caso nao seja Admin, retornar apenas busca pelo seu usuario.
            if (!isAdmin)
            {
                var patient = (PatientViewModel)result.Data;
                if (userName == null || userName != patient.Username)
                {
                    return Forbid();
                }
            }

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreatePatientCommand command)
        {
            var result = await _mediatr.Send(command);
            if (result.IsSuccess == false)
            {
                return BadRequest(result);
            }

            return Created();
        }

        [HttpPut("{idPatient}")]
        [Authorize(Roles = "Admin, Patient")]
        public async Task<IActionResult> Update([FromRoute] int idPatient, UpdatePatientCommand command)
        {
            command.IdPatient = idPatient;
            var result = await _mediatr.Send(command);

            if (!result.IsSuccess)
            {
                if (result.StatusCode == (int)HttpStatusCode.NotFound) { return NotFound(result); }

                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("cancel/{idPatient}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Cancel(int idPatient)
        {
            var command = new CancelPatientCommand(idPatient);
            var result = await _mediatr.Send(command);
            if (result.IsSuccess == false) { return NotFound(result); }

            return Ok(result);
        }

        [HttpPut("personalDetail/{idPatient}")]
        [Authorize(Roles = "Admin, Patient")]
        public async Task<IActionResult> UpdatePersonal([FromRoute] int idPatient, UpdatePatientPersonalDetailCommand command)
        {
            command.IdPatient = idPatient;
            var result = await _mediatr.Send(command);

            if (!result.IsSuccess)
            {
                if (result.StatusCode == (int)HttpStatusCode.NotFound) { return NotFound(result); }

                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("address/{idPatient}")]
        [Authorize(Roles = "Admin, Patient")]
        public async Task<IActionResult> UpdateAddress([FromRoute] int idPatient, UpdatePatientAddressCommand command)
        {
            command.IdPatient = idPatient;
            var result = await _mediatr.Send(command);

            if (!result.IsSuccess)
            {
                if (result.StatusCode == (int)HttpStatusCode.NotFound) { return NotFound(result); }

                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
