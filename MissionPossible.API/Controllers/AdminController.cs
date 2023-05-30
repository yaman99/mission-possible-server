using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MissionPossible.Application.Common.Exceptions;
using MissionPossible.Application.Common.Models;
using MissionPossible.Application.Features.Admin.Commands;
using MissionPossible.Application.Features.Admin.Queries;
using MissionPossible.Application.Features.Admin.Validators;

namespace MissionPossible.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController : MissionPossibleController
    {
        private readonly IValidator<AssignNewUserCommand> _addNewUserValidator;

        public AdminController(IValidator<AssignNewUserCommand> addNewUserValidator)
        {
            _addNewUserValidator = addNewUserValidator;
        }

        [HttpPost("assign-new-user")]
        public async Task<IActionResult> UserSignUp(AssignNewUserCommand command)
        {
            var validationResult = await _addNewUserValidator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                throw new SystemValidationException(validationResult.Errors);
            }

            var result = await Bus.ExecuteAsync<AssignNewUserCommand, Result>(command);
            return Ok(result);
        }
        [HttpPost("delete-user")]
        public async Task<IActionResult> DeleteUser(DeleteUserCommand command)
        {
            return Ok(await Bus.ExecuteAsync<DeleteUserCommand, Result>(command));
        }

        [HttpGet("get-assigned-users")]
        public async Task<IActionResult> GetAssignedUsers()
            => Ok(await Bus.ExecuteAsync<GetAssignedUsersQuery, Result>(new GetAssignedUsersQuery()));
    }
}
