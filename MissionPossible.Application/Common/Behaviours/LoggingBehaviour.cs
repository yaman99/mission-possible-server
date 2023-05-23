using MissionPossible.Application.Common.Interfaces;
using MissionPossible.Application.Repository;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;


namespace MissionPossible.Application.Common.Behaviours
{
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
    {
        private readonly ILogger _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserRepository _userRepository;

        public LoggingBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService, IUserRepository identityService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
            _userRepository = identityService;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _currentUserService.UserId ?? string.Empty;
            string userName = string.Empty;

            if (!string.IsNullOrEmpty(userId))
            {
                var user = await _userRepository.GetAsync(Guid.Parse(userId));
                userName = user.Email;
            }

            _logger.LogInformation("CleanArchitecture Request: {Name} {@UserId} {@UserName} {@Request}",
                requestName, userId, userName, request);
        }
    }
}
