﻿using ClinicManager.Application.Commands.MedicalAppointments.Create;
using ClinicManager.Application.Queries.MedicalCare.GetAll;
using ClinicManager.Application.Queries.MedicalCare.GetAllByDoctor;
using ClinicManager.Application.Queries.MedicalCare.GetAllByPatient;
using ClinicManager.Application.Queries.MedicalCare.GetById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalAppointmentController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public MedicalAppointmentController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllMedicalAppointmentsQuery();
            var result = await _mediatr.Send(query);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetMedicalAppointmentByIdQuery(id);
            var result = await _mediatr.Send(query);

            if(!result.IsSuccess) { return NotFound(result); }
            return Ok(result);
        }

        [HttpGet("doctor/{id}")]
        public async Task<IActionResult> GetAllByDoctor(int id)
        {
            var query = new GetAllMedicalAppointmentsByDoctorQuery(id);
            var result = await _mediatr.Send(query);
            return Ok(result);
        }

        [HttpGet("patient/{id}")]
        public async Task<IActionResult> GetAllByPatient(int id)
        {
            var query = new GetAllMedicalAppointmentsByPatientQuery(id);
            var result = await _mediatr.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMedicalAppointmentCommand command)
        {
            var result = await _mediatr.Send(command);
            if(!result.IsSuccess) { return BadRequest(result); }

            return CreatedAtAction(nameof(GetById), (int)result.Data, result.Data);
        }
    }
}
