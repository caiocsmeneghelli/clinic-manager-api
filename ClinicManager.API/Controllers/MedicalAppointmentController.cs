﻿using ClinicManager.Application.Commands.MedicalAppointments.Cancel;
using ClinicManager.Application.Commands.MedicalAppointments.Create;
using ClinicManager.Application.Commands.MedicalAppointments.Reschedule;
using ClinicManager.Application.Queries.MedicalAppointments.GetAll;
using ClinicManager.Application.Queries.MedicalAppointments.GetAllByDoctor;
using ClinicManager.Application.Queries.MedicalAppointments.GetAllByPatient;
using ClinicManager.Application.Queries.MedicalAppointments.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllMedicalAppointmentsQuery();
            var result = await _mediatr.Send(query);

            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetMedicalAppointmentByIdQuery(id);
            var result = await _mediatr.Send(query);

            if(!result.IsSuccess) { return NotFound(result); }
            return Ok(result);
        }

        [HttpGet("doctor/{id}")]
        [Authorize(Roles = "Admin, Doctor")]
        public async Task<IActionResult> GetAllByDoctor(int id)
        {
            var query = new GetAllMedicalAppointmentsByDoctorQuery(id);
            var result = await _mediatr.Send(query);
            return Ok(result);
        }

        [HttpGet("patient/{id}")]
        [Authorize(Roles = "Patient, Admin")]
        public async Task<IActionResult> GetAllByPatient(int id)
        {
            var query = new GetAllMedicalAppointmentsByPatientQuery(id);
            var result = await _mediatr.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateMedicalAppointmentCommand command)
        {
            var result = await _mediatr.Send(command);
            if(!result.IsSuccess) { return BadRequest(result); }

            return NoContent();
        }

        [HttpPut("cancel/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Cancel(int id)
        {
            var command = new CancelMedicalAppointmentCommand(id);
            var result = await _mediatr.Send(command);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("reschedule/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Reschedule(int id, RescheduleMedicalAppointmentCommand command)
        {
            command.IdAppointment = id;
            var result = await _mediatr.Send(command);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
