using ClinicManager.Application.Queries.MedicalCare.GetAll;
using ClinicManager.Application.Queries.MedicalCare.GetById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalCareController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public MedicalCareController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllMedicalCareQuery();
            var result = await _mediatr.Send(query);

            return Ok(result);
        }

        [HttpGet("doctor/{id}")]
        public async Task<IActionResult> GetAllByDoctor(int id)
        {
            var query = new GetMedicalCareByIdQuery(id);
            var result = await _mediatr.Send(query);

            if(!result.IsSuccess) { return NotFound(result); }
            return Ok(result);
        }

        [HttpGet("patient/{id}")]
        public async Task<IActionResult> GetAllByPatient(int id)
        {
            // GetAllByPatient
            return Ok();
        }
    }
}
