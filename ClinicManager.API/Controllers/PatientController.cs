using ClinicManager.Application.Commands.Patients.Create;
using ClinicManager.Application.Commands.Patients.Update;
using ClinicManager.Application.Commands.Patients.UpdateAddress;
using ClinicManager.Application.Commands.Patients.UpdatePersonalDetail;
using ClinicManager.Application.Queries.Patients.GetAll;
using ClinicManager.Application.Queries.Patients.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllPatientQuery();
            var result = await _mediatr.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetPatientByIdQuery(id);
            var result = await _mediatr.Send(query);
            
            if(result.IsSuccess == false)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePatientCommand command)
        {
            var result = await _mediatr.Send(command);
            if(result.IsSuccess == false)
            {
                return BadRequest(result);
            }

            return Created();
        }

        [HttpPut("{idPatient}")]
        public async Task<IActionResult> Update([FromRoute]int idPatient, UpdatePatientCommand command)
        {
            command.IdPatient = idPatient;
            var result = await _mediatr.Send(command);

            if (!result.IsSuccess)
            {
                if(result.StatusCode == (int)HttpStatusCode.NotFound) { return NotFound(result); }

                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("personalDetail/{idPatient}")]
        public async Task<IActionResult> UpdatePersonal([FromRoute]int idPatient, UpdatePatientPersonalDetailCommand command)
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
        public async Task<IActionResult> UpdateAddress([FromRoute]int idPatient, UpdatePatientAddressCommand command)
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
