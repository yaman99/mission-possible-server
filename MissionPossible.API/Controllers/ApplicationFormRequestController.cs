using Microsoft.AspNetCore.Mvc;
using MissionPossible.Application.Common.Models;
using MissionPossible.Application.Features.RequestManagement.Commands;
using MissionPossible.Application.Features.RequestManagement.Queries;

namespace MissionPossible.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApplicationFormRequestController : MissionPossibleController
    {
        [HttpPost("addRequest")]
        public async Task<IActionResult> AddRequest([FromForm] AddApplicationFormRequestCommand command)
        {
            return Ok(await Bus.ExecuteAsync<AddApplicationFormRequestCommand, Result>(command));
        }

        [HttpPost("update-request")]
        public async Task<IActionResult> UpdateRequest( UpdateApplicationFormRequestStatusCommand command)
        {
            return Ok(await Bus.ExecuteAsync<UpdateApplicationFormRequestStatusCommand, Result>(command));
        }

        [HttpGet("get-requests")]
        public async Task<IActionResult> GetRequests()
        {
            return Ok(await Bus.ExecuteAsync<GetAllApplicationFormRequestQuery, Result>(new GetAllApplicationFormRequestQuery()));
        }
        [HttpGet("get-student-requests/{studentId}")]
        public async Task<IActionResult> GetStudentRequests(Guid studentId)
        {
            return Ok(await Bus.ExecuteAsync<GetAllStudentApplicationFormRequestQuery, Result>(new GetAllStudentApplicationFormRequestQuery { StudentId = studentId}));
        }
    }
}
