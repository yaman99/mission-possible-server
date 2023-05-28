using MediatR;
using MissionPossible.Application.Common.Interfaces;
using MissionPossible.Application.Common.Interfaces.Repositories;
using MissionPossible.Application.Common.Interfaces.Services;
using MissionPossible.Application.Common.Models;
using MissionPossible.Application.Features.RequestManagement.Commands;
using MissionPossible.Domain.Entitis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Features.RequestManagement.Handlers
{
    public class RequestManagementCommandHandlers : 
        IRequestHandler<AddApplicationFormRequestCommand, Result>,
        IRequestHandler<UpdateApplicationFormRequestStatusCommand, Result>
    {
        private readonly IFileUploadService _fileUploadService;
        private readonly IApplicationFormRepository _applicationFormRepository;
        private readonly ICurrentUserService _currentUserService;

        public RequestManagementCommandHandlers(IFileUploadService fileUploadService, IApplicationFormRepository applicationFormRepository, ICurrentUserService currentUserService)
        {
            _fileUploadService = fileUploadService;
            _applicationFormRepository = applicationFormRepository;
            _currentUserService = currentUserService;
        }

        public async Task<Result> Handle(AddApplicationFormRequestCommand request, CancellationToken cancellationToken)
        {
            var student = Guid.Parse(_currentUserService.UserId!);
            var transcriptPath = await _fileUploadService.CreateFileAsync(request.Transcript);
            var applicationFormPath = await _fileUploadService.CreateFileAsync(request.ApplicationForm);
            var requestform = new ApplicationFormRequest(Guid.NewGuid())
            {
                TranscriptUrl = transcriptPath,
                ApplicationFormUrl = applicationFormPath,
                StudentId = student,
                Status = "pending"
            };
            await _applicationFormRepository.AddAsync(requestform);
            return Result.Success();
        }

        public async Task<Result> Handle(UpdateApplicationFormRequestStatusCommand request, CancellationToken cancellationToken)
        {
            var appRequest = await _applicationFormRepository.GetAsync(request.Id);
            appRequest.Status = request.Status;
            await _applicationFormRepository.UpdateAsync(appRequest);
            return Result.Success();
        }
    }
}
