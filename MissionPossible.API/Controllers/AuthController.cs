using MissionPossible.Application.EventBus.Bus;
using Microsoft.AspNetCore.Mvc;

namespace MissionPossible.API.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IDomainBus _bus;
        protected IDomainBus Bus => _bus ??= HttpContext.RequestServices.GetService(typeof(IDomainBus)) as IDomainBus;
    }
}
