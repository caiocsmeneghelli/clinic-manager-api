﻿using MediatR;
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
            // GetAll
            return Ok();
        }

        [HttpGet("doctor/{id}")]
        public async Task<IActionResult> GetAllByDoctor(int id)
        {
            // GetAllById
            return Ok();
        }

        [HttpGet("patient/{id}")]
        public async Task<IActionResult> GetAllByPatient(int id)
        {
            // GetAllByPatient
            return Ok();
        }
    }
}
