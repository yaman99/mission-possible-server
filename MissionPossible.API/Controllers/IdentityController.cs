using MissionPossible.Application.Common.Exceptions;
using MissionPossible.Application.Common.Interfaces;
using MissionPossible.Application.Common.Models;
using MissionPossible.Application.Features.Identity.Commands;
using MissionPossible.Application.Features.Identity.Dtos;
using MissionPossible.Application.Features.Identity.Queries;
using MissionPossible.Domain.Events;
using MissionPossible.Features.Identity.Commands;
using MissionPossible.Shared.Authentication;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace MissionPossible.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IdentityController : AuthController
    {
        private readonly IValidator<UserSignUpCommand> _userSignUpValidator;
        private readonly IValidator<UpdateEmailCommand> _updateEmailValidator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityController(IValidator<UserSignUpCommand> validator, IValidator<UpdateEmailCommand> updateEmailValidator, IHttpContextAccessor httpContextAccessor)
        {
            _userSignUpValidator = validator;
            _updateEmailValidator = updateEmailValidator;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> UserSignIn(UserSignInCommand command)
            => Ok(await Bus.ExecuteAsync<UserSignInCommand, JsonWebToken>(command));

        

        [HttpPost("sign-out")]
        public async Task<IActionResult> UserSignOut()
        {
            var accessToken = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

            var result = await Bus.ExecuteAsync<UserSignOutCommand, Result>(new UserSignOutCommand
            {
                AccessToken = accessToken,
            });

            if (result.Succeeded)
                return Ok();
            return BadRequest(result.Errors);
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommand command)
        {
            var result = await Bus.ExecuteAsync<ChangePasswordCommand, Result>(command);

            if (result.Succeeded)
                return Ok();
            return BadRequest(result.Errors);
        }
        [HttpGet("activate/{activationCode}")]
        public async Task<IActionResult> ActivateAccountAsync(string activationCode)
        {
            var cmd = new ActivateAccountCommand(activationCode);
            await Bus.ExecuteAsync<ActivateAccountCommand, Result>(cmd);
            return Redirect("https://app.indana.io/auth/login");
        }
        [HttpGet("verify/{activationCode}")]
        public async Task<IActionResult> VerifyUpdatedEmailAsync(string activationCode)
        {
            var cmd = new VerifyUpdatedEmailCommand(activationCode);
            await Bus.ExecuteAsync<VerifyUpdatedEmailCommand, Result>(cmd);
            return Ok();
        }

        [HttpPost("refresh-token/{refreshToken}")]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            var accessToken = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var result = await Bus.ExecuteAsync<RefreshAccessTokenCommand, Result>(new RefreshAccessTokenCommand
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });

            if (result.Succeeded)
                return Ok(result.Data);
            return BadRequest(result.Errors);
        }
        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateUserAsync(UpdatePhoneCommand command)
        {
            return Ok(await Bus.ExecuteAsync<UpdatePhoneCommand, Result>(command));
        }

        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateEmailAsync(UpdateEmailCommand command)
        {
            var result = await _updateEmailValidator.ValidateAsync(command);
            if (!result.IsValid)
            {
                throw new SystemValidationException(result.Errors);
            }
            return Ok(await Bus.ExecuteAsync<UpdateEmailCommand, Result>(command));
        }

        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetUserAsync()
        {
            return Ok(await Bus.ExecuteAsync<GetUserQuery, GetUserDto>(new GetUserQuery()));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SetPasswrod(SetQuickAccountPasswordCommand command)
        {

            return Ok(await Bus.ExecuteAsync<SetQuickAccountPasswordCommand, Result>(command));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ResendEmail(ResendEmailCommand command)
        {
            return Ok(await Bus.ExecuteAsync<ResendEmailCommand, Result>(command));
        }



    }
}
