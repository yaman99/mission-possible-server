using MediatR;
using MissionPossible.Application.Common.Interfaces;
using MissionPossible.Application.Common.Interfaces.Repositories;
using MissionPossible.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Features.Notification.Query
{
    public class GetNotificatiorQuery : IRequest<Result>
    {
        public string Type { get; set; }
    }
    public class GetNotificatiorQueryHandler : IRequestHandler<GetNotificatiorQuery,Result>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetNotificatiorQueryHandler(ICurrentUserService currentUserService, INotificationRepository notificationRepository)
        {
            _currentUserService = currentUserService;
            _notificationRepository = notificationRepository;
        }

        public async Task<Result> Handle(GetNotificatiorQuery request, CancellationToken cancellationToken)
        {
            if(request.Type == "student")
            {
                var userId = Guid.Parse(_currentUserService.UserId!);
                var data = await _notificationRepository.GetAllAsync(userId);
                return Result.Success(data);
            }
            else
            {
                var data = await _notificationRepository.GetAllAsync(request.Type);
                return Result.Success(data);
            }
        }
    }
}
