﻿using MediatR;
using MissionPossible.Application.Common.Exceptions;
using MissionPossible.Application.Common.Interfaces;
using MissionPossible.Application.Common.Interfaces.Repositories;
using MissionPossible.Application.Common.Interfaces.Services;
using MissionPossible.Application.Common.Models;
using MissionPossible.Application.EventBus.Bus;
using MissionPossible.Application.Features.Notification.Commands;
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
        IRequestHandler<UpdateRequestStatusCommand, Result>,
        IRequestHandler<DeleteStudentApplicationFormRequestCommand, Result>,
        IRequestHandler<AddOfficialLetterRequestCommand, Result>,
        IRequestHandler<UploadFileToRequestCommand, Result>
    {
        private readonly IFileUploadService _fileUploadService;
        private readonly IApplicationFormRepository _applicationFormRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDomainBus _domainBus;

        public RequestManagementCommandHandlers(IFileUploadService fileUploadService, IApplicationFormRepository applicationFormRepository, ICurrentUserService currentUserService, IDomainBus domainBus)
        {
            _fileUploadService = fileUploadService;
            _applicationFormRepository = applicationFormRepository;
            _currentUserService = currentUserService;
            _domainBus = domainBus;
        }

        public async Task<Result> Handle(AddApplicationFormRequestCommand request, CancellationToken cancellationToken)
        {
            var student = Guid.Parse(_currentUserService.UserId!);
            var transcriptPath = await _fileUploadService.CreateFileAsync(request.Transcript);
            var applicationFormPath = await _fileUploadService.CreateFileAsync(request.ApplicationForm);
            var requestform = new StudentRequest(Guid.NewGuid())
            {
                TranscriptUrl = transcriptPath,
                ApplicationFormUrl = applicationFormPath,
                StudentId = student,
                Status = "pending",
                RequestType = "application",
                InternshipType = request.InternshipType,
            };
            await _applicationFormRepository.AddAsync(requestform);
            await _domainBus.ExecuteAsync<AddNotificationCommand, Result>(new AddNotificationCommand
            {
                Type = "coordinator",
                Message = "New Internship Application Request Sent By Student",
            });
            return Result.Success();
        }

        public async Task<Result> Handle(UpdateRequestStatusCommand request, CancellationToken cancellationToken)
        {
            var appRequest = await _applicationFormRepository.GetAsync(request.Id);
            appRequest.Status = request.Status;
            if(appRequest.RequestType == "official" && request.Status == "rejected")
            {
                appRequest.OfficialLetterUrl = string.Empty;
            }
            await _applicationFormRepository.UpdateAsync(appRequest);
            if (appRequest.RequestType == "official" )
            {
                await _domainBus.ExecuteAsync<AddNotificationCommand, Result>(new AddNotificationCommand
                {
                    Type = "student",
                    Message = $"Your Official Letter Request Status Changed to: {request.Status}",
                    UserId = appRequest.StudentId
                });
            }
            else
            {
                await _domainBus.ExecuteAsync<AddNotificationCommand, Result>(new AddNotificationCommand
                {
                    Type = "student",
                    Message = $"Your Internship Application Request Status Changed to: {request.Status}",
                    UserId = appRequest.StudentId
                });
                if (request.Status == "approved")
                {
                    await _domainBus.ExecuteAsync<AddNotificationCommand, Result>(new AddNotificationCommand
                    {
                        Type = "carrerCenter",
                        Message = $"New Sgk Requested",
                        UserId = appRequest.StudentId
                    });
                }
                
            }
            
            return Result.Success();
        }

        public async Task<Result> Handle(DeleteStudentApplicationFormRequestCommand request, CancellationToken cancellationToken)
        {
            var appRequest = await _applicationFormRepository.GetAsync(request.Id);
            if (appRequest == null)
                throw new NotFoundException();
            appRequest.SetDelete(true);
            await _applicationFormRepository.UpdateAsync(appRequest);
            return Result.Success();
        }

        public async Task<Result> Handle(AddOfficialLetterRequestCommand request, CancellationToken cancellationToken)
        {
            var student = Guid.Parse(_currentUserService.UserId!);
            var transcriptPath = await _fileUploadService.CreateFileAsync(request.Transcript);
            var requestform = new StudentRequest(Guid.NewGuid())
            {
                TranscriptUrl = transcriptPath,
                StudentId = student,
                Status = "pending",
                RequestType = "official",
                InternshipType = request.InternshipType,
                CompanyName = request.CompanyName
            };
            await _applicationFormRepository.AddAsync(requestform);
            await _domainBus.ExecuteAsync<AddNotificationCommand, Result>(new AddNotificationCommand
            {
                Type = "coordinator",
                Message = "New Official Letter Request Sent By Student",
            });
            return Result.Success();
        }

        public async Task<Result> Handle(UploadFileToRequestCommand request, CancellationToken cancellationToken)
        {
            var studentRequest = await _applicationFormRepository.GetAsync(request.Id);
            var filPath = await _fileUploadService.CreateFileAsync(request.File);
            if(request.Type == "official")
            {
                studentRequest.OfficialLetterUrl = filPath;
                studentRequest.Status = "approved";
                await _domainBus.ExecuteAsync<AddNotificationCommand, Result>(new AddNotificationCommand
                {
                    Type = "student",
                    Message = "Your Official Letter Uploaded",
                    UserId = studentRequest.StudentId
                });
            }
            else if(request.Type == "sgk")
            {
                studentRequest.SgkUrl = filPath;
                studentRequest.Status = "completed";
                await _domainBus.ExecuteAsync<AddNotificationCommand, Result>(new AddNotificationCommand
                {
                    Type = "student",
                    Message = "Your Sgk Uploaded",
                    UserId = studentRequest.StudentId
                });

            }
            await _applicationFormRepository.UpdateAsync(studentRequest);
            return Result.Success();
        }
    }
}
