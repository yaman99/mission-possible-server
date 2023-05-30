using Microsoft.AspNetCore.Mvc;
using MissionPossible.Application.Common.Interfaces;
using MissionPossible.Application.Common.Models;
using MissionPossible.Application.Features.AnnouncementManager.Commands;
using MissionPossible.Application.Features.AnnouncementManager.Queries;
using MissionPossible.Application.Features.RequestManagement.Commands;
using MissionPossible.Application.Features.RequestManagement.Queries;

namespace MissionPossible.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AnnouncementController : MissionPossibleController
    {
        

        [HttpPost("add-announcement")]
        public async Task<IActionResult> AddAnnouncement(AddNewAnnouncementCommand command)
        {
            return Ok(await Bus.ExecuteAsync<AddNewAnnouncementCommand, Result>(command));
        }

        [HttpPost("delete-announcement")]
        public async Task<IActionResult> DeleteAnnouncement(DeleteAnnouncementCommand command)
        {
            return Ok(await Bus.ExecuteAsync<DeleteAnnouncementCommand, Result>(command));
        }

        [HttpGet("get-all-announcements")]
        public async Task<IActionResult> GetRequests()
        {
            return Ok(await Bus.ExecuteAsync<GetAllAnnouncementsQuery, Result>(new GetAllAnnouncementsQuery()));
        }

    }
}
