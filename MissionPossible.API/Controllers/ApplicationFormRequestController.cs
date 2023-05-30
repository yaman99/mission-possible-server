using Microsoft.AspNetCore.Mvc;
using MissionPossible.Application.Common.Interfaces;
using MissionPossible.Application.Common.Models;
using MissionPossible.Application.Features.RequestManagement.Commands;
using MissionPossible.Application.Features.RequestManagement.Queries;

namespace MissionPossible.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApplicationFormRequestController : MissionPossibleController
    {
        private readonly ICurrentUserService _currentUserService;

        public ApplicationFormRequestController(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        [HttpPost("addRequest")]
        public async Task<IActionResult> AddRequest([FromForm] AddApplicationFormRequestCommand command)
        {
            return Ok(await Bus.ExecuteAsync<AddApplicationFormRequestCommand, Result>(command));
        }

        [HttpPost("addOfficialRequest")]
        public async Task<IActionResult> AddOfficialRequest([FromForm] AddOfficialLetterRequestCommand command)
        {
            return Ok(await Bus.ExecuteAsync<AddOfficialLetterRequestCommand, Result>(command));
        }
        [HttpPost("upload-official-letter")]
        public async Task<IActionResult> UploadOfficialLetter([FromForm] UploadFileToRequestCommand command)
        {
            return Ok(await Bus.ExecuteAsync<UploadFileToRequestCommand, Result>(command));
        }

        [HttpPost("update-request")]
        public async Task<IActionResult> UpdateRequest(UpdateRequestStatusCommand command)
        {
            return Ok(await Bus.ExecuteAsync<UpdateRequestStatusCommand, Result>(command));
        }
        [HttpPost("delete-request")]
        public async Task<IActionResult> DeleteRequest(DeleteStudentApplicationFormRequestCommand command)
        {
            return Ok(await Bus.ExecuteAsync<DeleteStudentApplicationFormRequestCommand, Result>(command));
        }

        [HttpGet("get-requests/{requestType}/{status}")]
        public async Task<IActionResult> GetRequests(string requestType,string status)
        {
            return Ok(await Bus.ExecuteAsync<GetAllRequestsQuery, Result>(new GetAllRequestsQuery()
            {
                RequestType = requestType,
                Status = status
            }));
        }

        [HttpGet("get-student-requests/{requestType}")]
        public async Task<IActionResult> GetStudentRequests(string requestType)
        {
            return Ok(await Bus.ExecuteAsync<GetAllStudentApplicationFormRequestQuery, Result>(
                new GetAllStudentApplicationFormRequestQuery
                {
                    StudentId = Guid.Parse(_currentUserService.UserId),
                    RequestType = requestType
                }));
        }
    }
}
