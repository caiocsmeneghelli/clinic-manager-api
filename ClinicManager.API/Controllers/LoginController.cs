﻿using ClinicManager.Application.Commands.Users.Login;
using ClinicManager.Application.Commands.Users.UpdatePassword;
using ClinicManager.Application.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediatr;
        public LoginController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            Result result = await _mediatr.Send(command);
            if (result.IsSuccess == false) { return BadRequest(result); }
            return Ok(result);
        }

        [HttpPut("resetPassword")]
        [Authorize]
        public async Task<IActionResult> ResetPassword(UpdatePasswordCommand command)
        {
            var result = await _mediatr.Send(command);
            if (result.IsSuccess == false) { return BadRequest(result); }

            return Ok(result);
        }
    }
}
