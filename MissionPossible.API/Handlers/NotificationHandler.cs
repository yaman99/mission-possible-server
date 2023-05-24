using MediatR;
using MissionPossible.API.Helper;
using MissionPossible.Domain.Events;
using MissionPossible.Shared.Services;

namespace MissionPossible.API.Handlers
{
    public class NotificationHandler : INotificationHandler<NewAccountWithTempPasswordCreated>
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailSenderService _emailSenderService;

        public NotificationHandler(IWebHostEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor, IEmailSenderService emailSenderService)
        {
            _hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _emailSenderService = emailSenderService;

            EmailTemplateParser.Initialize(_hostingEnvironment);
        }

        public async Task Handle(NewAccountWithTempPasswordCreated notification, CancellationToken cancellationToken)
        {
            string host = _httpContextAccessor.HttpContext.Request.Host.Host;
            var port = _httpContextAccessor.HttpContext.Request.Host.Port;
            var scheme = _httpContextAccessor.HttpContext.Request.Scheme;

            var loginUrl = port.HasValue ? $"{scheme}://{host}:{port}/auth/login" :
                $"{scheme}://{host}/auth/login";
            var logo = port.HasValue ? $"{scheme}://{host}:{port}/assets/images/mdi_logo_aside.png" :
                $"{scheme}://{host}/assets/images/mdi_logo_aside.png";
            var msg = EmailTemplateParser.SendInvitationToSignIn(notification.Email, loginUrl, notification.Password, logo);
            var subject = "Inivitaion Request To Join Uskudar Internship Management System";
            await _emailSenderService.SendEmailAsync(notification.Email, notification.Email, subject, msg);
        }
    }
}
