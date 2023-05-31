using MediatR;
using MissionPossible.Application.Common.Interfaces.Repositories;
using MissionPossible.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Features.Notification.Commands
{
    public class AddNotificationCommand : IRequest<Result>
    {
        public string Message { get; set; }
        public string Type { get; set; }
        public Guid UserId { get; set; }
    }

    public class AddNotificationCommandHandler : IRequestHandler<AddNotificationCommand, Result>
    {
        private readonly INotificationRepository _notificationRepository;

        public AddNotificationCommandHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<Result> Handle(AddNotificationCommand request, CancellationToken cancellationToken)
        {
            await _notificationRepository.AddAsync(new Domain.Entitis.Notification(Guid.NewGuid())
            {
                Type = request.Type,
                UserId = request.UserId,
                Message = request.Message
            });
            return Result.Success();
        }
    }
}
