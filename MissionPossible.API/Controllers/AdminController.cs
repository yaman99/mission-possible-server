using Microsoft.AspNetCore.Mvc;
using MissionPossible.Application.Common.Models;
using MissionPossible.Application.Features.Admin.Commands;
using MissionPossible.Application.Features.Admin.Queries;

namespace MissionPossible.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController : MissionPossibleController
    {
        [HttpPost("assign-new-user")]
        public async Task<IActionResult> UserSignUp(AssignNewUserCommand command)
        {
            //var result = await _userSignUpValidator.ValidateAsync(command);
            //if (!result.IsValid)
            //{
            //    throw new SystemValidationException(result.Errors);
            //}

            // command

            var result = await Bus.ExecuteAsync<AssignNewUserCommand, Result>(command);
            return Ok(result);
        }

        [HttpGet("get-assigned-users")]
        public async Task<IActionResult> GetAssignedUsers()
            => Ok(await Bus.ExecuteAsync<GetAssignedUsersQuery, Result>(new GetAssignedUsersQuery()));
    }
}
