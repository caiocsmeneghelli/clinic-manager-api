using ClinicManager.Application.Commands.Patients.Create;
using ClinicManager.Application.Queries.Patients.GetAll;
using ClinicManager.Application.Queries.Patients.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    }
}
