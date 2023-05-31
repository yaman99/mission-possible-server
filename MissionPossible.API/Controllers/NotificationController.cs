using Microsoft.AspNetCore.Mvc;
using MissionPossible.Application.Common.Interfaces;
using MissionPossible.Application.Common.Models;
using MissionPossible.Application.Features.AnnouncementManager.Commands;
using MissionPossible.Application.Features.AnnouncementManager.Queries;
using MissionPossible.Application.Features.Notification.Query;
using MissionPossible.Application.Features.RequestManagement.Commands;
using MissionPossible.Application.Features.RequestManagement.Queries;

namespace MissionPossible.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NotificationController : MissionPossibleController
    {
        

        [HttpGet("get-notifications/{type}")]
        public async Task<IActionResult> GetNotifications(string type )
        {
            return Ok(await Bus.ExecuteAsync<GetNotificatiorQuery, Result>(new GetNotificatiorQuery
            {
                Type = type,
            }));
        }


    }
}
