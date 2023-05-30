using AutoMapper;
using MediatR;
using MissionPossible.Application.Common.Interfaces.Repositories;
using MissionPossible.Application.Common.Models;
using MissionPossible.Application.Features.RequestManagement.Dtos;
using MissionPossible.Application.Features.RequestManagement.Queries;
using MissionPossible.Domain.Entitis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Features.RequestManagement.Handlers
{
    public class RequestManagementQueryHandlers : 
        IRequestHandler<GetAllRequestsQuery, Result>,
        IRequestHandler<GetAllStudentApplicationFormRequestQuery, Result>
    {
        private readonly IApplicationFormRepository _applicationFormRepository;
        private readonly IMapper _mapper;

        public RequestManagementQueryHandlers(IApplicationFormRepository applicationFormRepository, IMapper mapper)
        {
            _applicationFormRepository = applicationFormRepository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(GetAllRequestsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<StudentRequest> requests = new List<StudentRequest>();
            if(request.Status != null && request.Status != "any")
            {
                requests =  await _applicationFormRepository.GetAllAsync(request.RequestType , request.Status);
            }
            else
            {
                requests = await _applicationFormRepository.GetAllAsync(request.RequestType);
            }
            if(request.RequestType == "official")
            {
                var dto = _mapper.Map<IEnumerable<GetOfficialLetterRequestDto>>(requests);
                return Result.Success(dto);
            }
            else if(request.RequestType == "application")
            {
                var dto = _mapper.Map<IEnumerable<GetApplicationFormRequestDto>>(requests);
                return Result.Success(dto);
            }

            return Result.Success();
        }

        public async Task<Result> Handle(GetAllStudentApplicationFormRequestQuery request, CancellationToken cancellationToken)
        {
            var requests = await _applicationFormRepository.GetAllByStudentAsync(request.StudentId , request.RequestType);

            if (request.RequestType == "official")
            {
                var dto = _mapper.Map<IEnumerable<GetOfficialLetterRequestDto>>(requests);
                return Result.Success(dto);
            }
            else if (request.RequestType == "application")
            {
                var dto = _mapper.Map<IEnumerable<GetApplicationFormRequestDto>>(requests);
                return Result.Success(dto);
            }
            return Result.Success();
        }
    }
}
